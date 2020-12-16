using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OverviewRkiData.Components.Ui.Eventbus
{
    public static class EventbusManager
    {
        public static int RegisteredCount => EventbusManagerInternal.GetEventbus().RegisteredCount;

        public static int RegisteredCountAll => EventbusManagerInternal.GetEventbus().RegisteredCountAll;

        public static int RegisteredCountByView<TView>() where TView : DependencyObject => EventbusManagerInternal.GetEventbus().RegisteredCountByView<TView>();
        internal static ViewOpen GetViewOpened(int channel) => EventbusManagerInternal.GetEventbus().GeViewOpenend(channel);


        /// <summary>
        /// create new internal instance host for message event. 
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="receiverMethod"></param>
        public static void Register<TView, TMessage>(Action<IMessageContainer> receiverMethod)
            where TView : DependencyObject
            where TMessage : IMessageContainer => EventbusManagerInternal.GetEventbus().Register<TView, TMessage>(receiverMethod);

        /// <summary>
        /// Registers an associated view that is located in another channel. 
        /// Enables the view to be removed when the associated main view is closed.
        /// </summary>
        /// <typeparam name="TViewParent">Main view</typeparam>
        /// <typeparam name="TViewChild">Child view</typeparam>
        /// <param name="channel">The channel of the child view.</param>
        public static void AddRegisterChildView<TViewParent, TViewChild>(int channel)
        {
            EventbusManagerInternal.GetEventbus().AddRegisterChildView<TViewParent, TViewChild>(channel);
        }

        /// <summary>
        /// Close target view.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="v"></param>
        public static void CloseView<TView>(int channel) => EventbusManagerInternal.GetEventbus().CloseView<TView>(channel);

        /// <summary>
        /// Deregister closing content. 
        /// Obsolete: Every view need an dispose interface to cleanup events and unused references.
        /// </summary>
        /// <typeparam name="TView">Set the view for deregister</typeparam>
        public static void Deregister<TView>() => EventbusManagerInternal.GetEventbus().Deregister<TView>();

        /// <summary>
        /// Deregister closing content.
        /// </summary>
        /// <param name="view">Set the view for deregister</param>
        public static void Deregister(Type view) => EventbusManagerInternal.GetEventbus().Deregister(view);

        internal static bool IsViewOpen<TView>(int channel) => EventbusManagerInternal.GetEventbus().IsViewOpen(typeof(TView), channel);
        internal static bool IsViewOpen(Type type, int channel) => EventbusManagerInternal.GetEventbus().IsViewOpen(type, channel);

        /// <summary>
        /// Open new instance of a view. The view must setup the viewModel to the DataContext.
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        public static void OpenView<TView>(int channel) => EventbusManagerInternal.GetEventbus().OpenView<TView>(channel);

        public static bool Send<TView, TMessageType>(TMessageType message, int channel, bool openView = false) where TMessageType : IMessageContainer => EventbusManagerInternal.GetEventbus().Send<TView, TMessageType>(message, channel, openView);

        /// <summary>
        /// Eventbus singleton. can only one instance exist for the application
        /// </summary>
        private class EventbusManagerInternal
        {
            private static EventbusManagerInternal eventbus;

            private readonly IDictionary<Type, IList<IMessageEventHost>> _viewsWithMessageEventHosts = new Dictionary<Type, IList<IMessageEventHost>>();
            private readonly IDictionary<Type, IList<ViewChildItem>> _viewsWithChildrens = new Dictionary<Type, IList<ViewChildItem>>();

            private EventbusManagerInternal() { }

            public static EventbusManagerInternal GetEventbus()
            {
                if (eventbus == null)
                {
                    eventbus = new EventbusManagerInternal();
                }

                return eventbus;
            }

            public int RegisteredCount => this._viewsWithMessageEventHosts.Count;

            public int RegisteredCountAll => this._viewsWithMessageEventHosts.Sum(c => c.Value.Count);

            public int RegisteredCountByView<TView>() where TView : DependencyObject
            {
                if (this._viewsWithMessageEventHosts.Count == 0)
                {
                    return 0;
                }

                return this._viewsWithMessageEventHosts[typeof(TView)].Count;
            }

            /// <summary>
            /// create new internal instance host for message event. 
            /// </summary>
            /// <typeparam name="TView"></typeparam>
            /// <typeparam name="TMessage"></typeparam>
            /// <param name="receiverMethod"></param>
            public void Register<TView, TMessage>(Action<IMessageContainer> receiverMethod)
                where TView : DependencyObject
                where TMessage : IMessageContainer
            {

                var host = new MessageEventHost<TView, TMessage>();
                host.Subscribe(receiverMethod);

                if (!this._viewsWithMessageEventHosts.ContainsKey(host.ViewType))
                {
                    this._viewsWithMessageEventHosts.Add(host.ViewType, new List<IMessageEventHost>());
                }

                if (this._viewsWithMessageEventHosts[host.ViewType].Any(a => a.MessageType == host.MessageType))
                {
                    throw new EventbusException($"can not register one moretime to the viewType {host.ViewType.Name} about message type: {host.MessageType.Name}");
                }

                this._viewsWithMessageEventHosts[host.ViewType].Add(host);
            }

            /// <summary>
            /// Registers an associated view that is located in another channel. 
            /// Enables the view to be removed when the associated main view is closed.
            /// </summary>
            /// <typeparam name="TViewParent">Main view</typeparam>
            /// <typeparam name="TViewChild">Child view</typeparam>
            /// <param name="channel">The channel of the child view.</param>
            public void AddRegisterChildView<TViewParent, TViewChild>(int channel)
            {
                if (!this._viewsWithChildrens.ContainsKey(typeof(TViewParent)))
                {
                    this._viewsWithChildrens.Add(typeof(TViewParent), new List<ViewChildItem>());
                }

                this._viewsWithChildrens[typeof(TViewParent)].Add(new ViewChildItem(typeof(TViewChild), channel));
            }


            /// <summary>
            /// Deregister closing content. Every view need an dispose interface to cleanup events and unused references.
            /// </summary>
            /// <typeparam name="TView"></typeparam>
            public void Deregister<TView>() => this.Deregister(typeof(TView));

            public void Deregister(Type view)
            {
                if (this._viewsWithMessageEventHosts.ContainsKey(view))
                {
                    foreach (var item in this._viewsWithMessageEventHosts[view])
                    {
                        item.RemoveEventMethod();
                    }
                    this._viewsWithMessageEventHosts.Remove(view);
                }

                if (this._viewsWithChildrens.ContainsKey(view))
                {
                    foreach (var item in this._viewsWithChildrens[view])
                    {
                        this.CloseView(item.Type, item.Channel);
                    }
                }
            }

            internal bool IsViewOpen(Type type, int channel)
            {
                return SideHostControl.IsViewOpen(type, channel);
            }

            internal ViewOpen GeViewOpenend(int channel)
            {
                if (SideHostControl.TypeViews.All(a => a.Channel != channel) ||
                    !SideHostControl.TypeViews.Any(a => a.Channel == channel && a.TypeView.Name.EndsWith("View")))
                {
                    return ViewOpen.Nothing;
                }

                var typeView = SideHostControl.TypeViews.FirstOrDefault(f => f.Channel == channel);

                var viewName = typeView.TypeView.Name.Remove(typeView.TypeView.Name.Length - 4);

                if (Enum.TryParse(typeof(ViewOpen), viewName, out object result))
                {
                    return (ViewOpen)result;
                }

                throw new Exception($"Type has no enum: {typeView.TypeView.Name}");
            }

            /// <summary>
            /// Open new instance of a view. The view must setup the viewModel to the DataContext.
            /// </summary>
            /// <typeparam name="TView"></typeparam>
            public void OpenView<TView>(int channel)
            {
                this.OpenViewEvent?.Invoke((TView)Activator.CreateInstance(typeof(TView)), channel);
            }

            internal void CloseView<TView>(int channel)
            {
                this.CloseView(typeof(TView), channel);
            }

            internal void CloseView(Type view, int channel)
            {
                this.CloseViewEvent?.Invoke(view, channel);
            }

            public bool Send<TView, TMessageType>(TMessageType message, int channel = 0, bool openView = false) where TMessageType : IMessageContainer
            {
                foreach (var itemEventHosts in this._viewsWithMessageEventHosts)
                {
                    if (itemEventHosts.Key != typeof(TView))
                    {
                        continue;
                    }

                    foreach (var itemEventHost in itemEventHosts.Value)
                    {
                        if (itemEventHost.MessageType != message.GetType())
                        {
                            continue;
                        }

                        itemEventHost.Send(message);
                        return true;
                    }
                }

                if (this.OpenNewView<TView, TMessageType>(message, openView, channel))
                {
                    return true;
                }

                throw new EventbusException($"Not found or registered. View: {typeof(TView).Name}, {typeof(TMessageType).Name}");
            }

            private bool OpenNewView<TView, TMessageType>(TMessageType message, bool openView, int channel) where TMessageType : IMessageContainer
            {
                if (openView)
                {
                    this.OpenView<TView>(channel);

                    if (this.Send<TView, TMessageType>(message, channel))
                    {
                        return true;
                    }
                }

                return false;
            }

            public event OpenViewEventHandler OpenViewEvent;

            public event CloseViewEventHandler CloseViewEvent;
        }

        public delegate void OpenViewEventHandler(object obj, int channel);

        public static event OpenViewEventHandler OpenViewEvent
        {
            add { EventbusManagerInternal.GetEventbus().OpenViewEvent += value; }
            remove { EventbusManagerInternal.GetEventbus().OpenViewEvent -= value; }
        }

        public delegate void CloseViewEventHandler(Type view, int channel);
        public static event CloseViewEventHandler CloseViewEvent
        {
            add { EventbusManagerInternal.GetEventbus().CloseViewEvent += value; }
            remove { EventbusManagerInternal.GetEventbus().CloseViewEvent -= value; }
        }
    }
}
