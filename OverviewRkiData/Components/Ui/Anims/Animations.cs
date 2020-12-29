using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace OverviewRkiData.Components.Ui.Anims
{
    public static class Animations
    {
        /// <summary>
        /// Animiert die Bewegung eines Controls
        /// </summary>
        /// <param name="control">Ein Control bewegen</param>
        /// <param name="storyboard"></param>
        /// <param name="from">Von wo aus so das Control starten</param>
        /// <param name="to">Wohin soll sich das Control bewegen</param>
        /// <param name="beginTime">Wann soll die Animation gestartet werden.</param>
        /// <param name="duration">Wie lange sll die Animation abgespielt werden</param>
        /// <param name="autoReverse">Soll die Animation am Ende rückwärts abgespielt werden.</param>
        /// <returns></returns>
        public static DependencyObject SetMove(
            DependencyObject control,
            Storyboard storyboard,
            Thickness from,
            Thickness to,
            double beginTime = 0,
            double duration = 100,
            bool autoReverse = false)
        {
            if (storyboard == null)
            {
                storyboard = AnimationsHelper.CreateStorybord(0, duration, autoReverse);
            }

            var ta = AnimationsHelper.GetThicknessAnimation(from, to, beginTime, duration, autoReverse);

            ta.AccelerationRatio = .2;
            ta.DecelerationRatio = .2;

            storyboard.Children.Add(ta);

            Storyboard.SetTarget(ta, control);
            Storyboard.SetTargetProperty(ta, new PropertyPath("(Canvas.Margin)"));

            return control;
        }

        public static void SetFade<TDependencyObject>(
            TDependencyObject control,
            Storyboard storyboard,
            double from,
            double to,
            double beginTime = 0,
            double duration = 100,
            bool autoReverse = false) where TDependencyObject : DependencyObject
        {
            if (storyboard == null)
            {
                storyboard = AnimationsHelper.CreateStorybord(0, duration, autoReverse);
            }

            var da = AnimationsHelper.GetDoubleAnimation(from, to, beginTime, duration, autoReverse);

            storyboard.Children.Add(da);

            Storyboard.SetTarget(da, control);
            Storyboard.SetTargetProperty(da, new PropertyPath(UIElement.OpacityProperty));

        }
    }
}
