using System;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Threading;

namespace Fractal_Design
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        double a, b, r, r2, x, y, angle, precision;
        Stroke newStroke;
        StylusPointCollection pts;
        DispatcherTimer drawtime = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            drawtime.Tick += drawtime_Tick;
            drawtime.Interval = TimeSpan.FromMilliseconds(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pts = new StylusPointCollection();
            drawtime.Start();
            a = inkSurface.ActualWidth / 2;
            b = inkSurface.ActualHeight / 2;
            r = Convert.ToInt32(radius.Text);
            r2 = Convert.ToInt32(radius2.Text);
            angle = 0;
            precision = smoothness.Value;
        }

        void drawtime_Tick(object sender, EventArgs e)
        {
            try
            {
                angle += precision;
                x = a + r * Math.Cos(angle * (Math.PI / firstAngle.Value));
                y = b + r * Math.Sin(angle * (Math.PI / firstAngle.Value));

                x = x + r2 * Math.Cos(angle * (Math.PI / secondAngle.Value));
                y = y + r2 * Math.Sin(angle * (Math.PI / secondAngle.Value));

                pts.Add(new StylusPoint(x, y));

                if (angle >= Convert.ToInt32(length.Text))
                {
                    drawtime.Stop();
                }

                inkSurface.Strokes.Clear();
                newStroke = new Stroke(pts);
                inkSurface.Strokes.Add(newStroke);
            }
            catch (Exception)
            {
                drawtime.Stop();
            }
        }
    }
}