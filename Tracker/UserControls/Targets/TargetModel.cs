using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Tracker.UserControls.Targets
{
    public class TargetModel
    {
        public int x;
        public int y;
        public int ttl;
        public int sections;
        public int size;

        public TargetModel(int _sections) { 
            this.sections = _sections;
            switch (sections)
            {
                case 3:
                    size = 200;
                    break;
                case 4:
                    size = 250;
                    break;
                case 5:
                    size = 300;
                    break;
                default:
                    break;
            }
        }

        public UserControl Produce(float scale) {
            UserControl cntrl = new UserControl();
            switch (sections)
            {
                case 3:
                    cntrl = new _3SectionTargetControl();
                    break;
                case 4:
                    cntrl = new _4SectionTargetControl();
                    break;
                case 5:
                    cntrl = new _5SectionTargetControl();
                    break;
                default:
                    break;
            }
            cntrl.Width = size * scale;
            cntrl.Height = size * scale;
            return cntrl;
        }
    }
}
