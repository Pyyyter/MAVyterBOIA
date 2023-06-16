using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleExample
{
    // This class is used to access components of a different form without requiring modification of accessors or implementation of methods for each component.
    // It searchs for the form name on a collection of active forms and looks for the name of the component on the form.
    // It returns a Control object, which can be cast to the specific type of control you intend to handle.
    public static class FormExchange
    {
        public static T GetControl<T>(string formName, string controlName) where T : Control
        {
            var form = Application.OpenForms[formName];
            if (form == null) return null;

            if (form.Controls[controlName] is T control)
            {
                return control;
            }

            return null;
        }

    }
}
