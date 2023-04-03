# ObservableObject
This is a base class for simplifying C# ViewModels. Instead of implementing INotifyPropertyChanged on each ViewModel and writing out a dozen lines of code for each property, this base class allows you to set up properties in a single line of code.

**Typical Bindable Property**
```
private string _FooBar;
public string FooBar
{
  get
  {
    return _FooBar;
  }
  set
  {
    if(value != _FooBar)
    {
      _FooBar = value;
      NotifyPropertyChanged("FooBar");
    }
  }
}
```

**With ObservableObject**
```
public string FooBar { get { return Get<string>(); } set { Set(value); } }
```




This class uses a backend dictionary to store the property values so you don't have to 

## How to Use
After adding ObservableObject.cs to your project, just use it as the base class when creating your new ViewModel classes, and then implement your properties the way you normally would, except use the proper-case Get and Set methods get/set like this, passing the property's data type to the Get method:

`public DATATYPE Foobar { get { return Get<DATATYPE>(); } set { Set(value); }`

Here is a simple example of a ViewModel:

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject
{
    public class MyViewModel : BaseClasses.ObservableObject
    {
        public string Name {  get { return Get<string>(); } set { Set(value); } }
        public int ID { get { return Get<int>(); } set { Set(value); } }
        public DateTime DoB { get { return Get<DateTime>(); } set { Set(value); } }
    }
}
```
