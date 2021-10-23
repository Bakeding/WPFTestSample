using System;
using System.Linq;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using System.Windows;
using System.Collections.Generic;

namespace WpfChart
{
    [Export(typeof(IMain))]
    public class MainViewModel : Conductor<IScreen>.Collection.OneActive, IMain
    {
        [ImportingConstructor]
        public MainViewModel([ImportMany]IEnumerable<IScreen> screens)
        {
            var sc = screens.OrderBy(item => item.DisplayName);
            Items.AddRange(sc);
            DisplayName = "WPF Chart";
        }
    }
}
