using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ExamXAML.Behavior
{
    public class SelectionChangeBehavior
    {
        private static void ExecuteSelectionChangedCommand(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ListView;
            if (control == null)
            {
                return;
            }
            if ((e.NewValue != null) && (e.OldValue == null))
            {
                control.SelectionChanged += (snd, args) =>
                {
                    var command = (snd as ListView).GetValue(SelectionChangeBehavior.SelectionChangedProperty) as ICommand;
                    if (command != null)
                    {
                        command.Execute(args);
                    }
                };
            }
        }

        public static ICommand GetSelectionChanged(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(SelectionChangedProperty);
        }

        public static void SetSelectionChanged(DependencyObject obj, ICommand value)
        {
            obj.SetValue(SelectionChangedProperty, value);
        }

        public static readonly DependencyProperty SelectionChangedProperty =
            DependencyProperty.RegisterAttached("SelectionChanged",
                typeof(ICommand),
                typeof(SelectionChangeBehavior),
                new PropertyMetadata(ExecuteSelectionChangedCommand));
    }
}
