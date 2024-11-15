/* ObservableObject.cs
 * @version: 1.1
 * @author: Jonathan Hilgeman <jhilgeman@gmail.com>
 * @created: 2020-03-04
 * @updated: 2024-11-15
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Serialization;

namespace BaseClasses
{
    [Serializable]
    public class ObservableObject : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null, params string[] addlNotify)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            foreach (string alsoNotify in addlNotify)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(alsoNotify));
            }
        }
        #endregion

        // Where all the values are truly stored
        [XmlIgnore]
        private Dictionary<string, object> __Values = new Dictionary<string, object>();

        public T Get<T>([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (!__Values.ContainsKey(propertyName))
            {
                // Trying to get a value that isn't set yet
                return default(T);
            }

            // Value exists, cast it to our desired type and return it
            return (T)__Values[propertyName];
        }

        public bool Set(object value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (!__Values.ContainsKey(propertyName))
            {
                // Value isn't set yet
                __Values.Add(propertyName, value);
                NotifyPropertyChanged(propertyName);
                return true;
            }

            if (value != __Values[propertyName])
            {
                // Value exists and is different
                __Values[propertyName] = value;
                NotifyPropertyChanged(propertyName);
                return true;
            }

            // Value exists but isn't different
            return false;
        }

    }
}
