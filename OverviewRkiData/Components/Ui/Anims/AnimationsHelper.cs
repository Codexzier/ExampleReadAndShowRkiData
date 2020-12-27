using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace OverviewRkiData.Components.Ui.Anims
{
    public static class AnimationsHelper
    {
        /// <summary>
        /// Ruft eine Entsprechende Animation nach den angegebenen Werten
        /// </summary>
        /// <param name="from">Von welchen Tranzparenz Wert soll gestartet werden</param>
        /// <param name="to">Welcher Tranzparenz Wert soll am Ende erreicht werden</param>
        /// <param name="beginTime">Wann so die Animations starten</param>
        /// <param name="duration">Wie lange soll die Animation abgespielt werden</param>
        /// <param name="autoReverse">Soll die Animation am Ende rückwärts abgespielt werden</param>
        /// <returns></returns>
        public static DoubleAnimation GetDoubleAnimation(double from, double to, double beginTime, double duration, bool autoReverse)
        {
            var da = new DoubleAnimation
            {
                From = from,
                To = to,
                BeginTime = TimeSpan.FromMilliseconds(beginTime),
                Duration = new Duration(TimeSpan.FromMilliseconds(duration)),
                AutoReverse = autoReverse,
                FillBehavior = FillBehavior.HoldEnd,
                DecelerationRatio = 0.3
            };

            return da;
        }
        /// <summary>
        /// Ruft eine Entsprechende Animation zum verschieben eines Controls ab.
        /// </summary>
        /// <param name="from">Von wo aus so das Control starten</param>
        /// <param name="to">Wohin soll sich das Control bewegen</param>
        /// <param name="beginTime">Wann soll die Animation gestartet werden.</param>
        /// <param name="duration">Wie lange sll die Animation abgespielt werden</param>
        /// <param name="autoReverse">Soll die Animation am Ende rückwärts abgespielt werden.</param>
        /// <returns></returns>
        public static ThicknessAnimation GetThicknessAnimation(Thickness from, Thickness to, double beginTime, double duration, bool autoReverse)
        {
            var ta = new ThicknessAnimation
            {
                From = from,
                To = to,
                BeginTime = TimeSpan.FromMilliseconds(beginTime),
                Duration = new Duration(TimeSpan.FromMilliseconds(duration)),
                AutoReverse = autoReverse,
                DecelerationRatio = 0.3
            };

            return ta;
        }

        public static Storyboard CreateStorybord(double begin, double duration, bool autoReverse)
        {
            var sb = new Storyboard
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(duration)),
                BeginTime = TimeSpan.FromMilliseconds(begin),
                FillBehavior = FillBehavior.HoldEnd,
                AutoReverse = autoReverse
            };
            return sb;
        }
    }
}
