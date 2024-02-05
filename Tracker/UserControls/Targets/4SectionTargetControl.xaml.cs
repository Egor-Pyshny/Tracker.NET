using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tracker.UserControls.Targets
{
    /// <summary>
    /// Логика взаимодействия для _4SectionTargetControl.xaml
    /// </summary>
    public partial class _4SectionTargetControl : UserControl, ITarget
    {
        public Mode mode { get; set; }
        public _4SectionTargetControl()
        {
            InitializeComponent();
        }

        public int GetPoints(int x, int y, float scale)
        {
            throw new NotImplementedException();
        }
    }
}
