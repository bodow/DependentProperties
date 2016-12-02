using System;
using System.Linq;
using System.Reflection;

namespace PasDependency
{
    /**********************************************************************************
     * Pas, 2/12/2016
     * Προσπαθεί να δώσει μια σχετικά κομψή λύση στο πρόβλημα:
     *      NotifyPropertyChanged σε calculated readonly properties του MVVM Pattern
     *      Αφορά ViewModel classes πχ σε περιβάλλον Xamarin.Forms
     *      
     * Παράδειγμα:
     *      Στην class ChorusViewModel εκτός από τις αντίστοιχες με το (POCO) model properties
     *      στις οποίες υλοποιούμε το INotifyPropertyChanged ευθέως,
     *      ορίζεται πχ κάποια public int CalculatedSum property => SomeInternalList.Where(..linq...)
     *      Η τιμή αυτής δεν αλλάζει σε setter αλλά όταν αλλάξει κάτι σε άλλη property, εδώ στην SomeInternalList
     *      
     * Προσέγγιση λύσης με την συγκεκριμένη library:
     *      a. using PasDependency, incluse this source in project
     *      b. Decorate properties with [DependentProperty] or [DependentProperty("GroupAlpha")]
     *         multiple decorations allowed. GroupAlpha είναι ένα αυθαίρετο string. Αν δοθεί, επιτρέπει
     *         να γίνει NotifyPropertyChange σε κάποιες μόνο από τις decorated properties
     *      c. Call Notifier.Notify(this) from a method inside the ViewModel class
     *      d. Στην ViewModel ή σε πατρική class θα πρέπει να ορίζεται η μέθοδος: OnPropertyChanged(string name)
     * 
     * Example class:
     *         [DependentProperty("GroupAlpha")]
     *         [DependentProperty("GroupBeta")]
     *         public int MyIntProperty { get; set; }
     *         
     *         [DependentProperty]
     *         public string MyStringProperty { get; set; }
     *         
     *         [DependentProperty("GroupBeta")]
     *         public DateTime MyDateTimeProperty { get; set; }
     *         
     *         public List<string> MyInnocentProperty { get; set; }
     * 
     * Example call:
     *          Notifier.Notify(this, "GroupAlpha");
     * 
     * *******************************************************************************/

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
    public class DependentPropertyAttribute: Attribute
    {
        public string PropertyGroup { get; set; }

        public DependentPropertyAttribute(string propertyGroup = null)
        {
            this.PropertyGroup = propertyGroup ?? "";
        }
    }


    public static class Notifier
    {
        public static void Notify(object caller, string propertyGroup = null)
        {
            if (String.IsNullOrWhiteSpace(propertyGroup))
            {
                propertyGroup = String.Empty;
            }
            var type = caller.GetType();
            var dependentProperties = type.GetProperties()
                .Where(p => p.GetCustomAttributes(false)
                    .Any(a => a.GetType() == typeof(DependentPropertyAttribute) && (
                        String.IsNullOrEmpty(propertyGroup) || (a as DependentPropertyAttribute).PropertyGroup.Equals(propertyGroup)
                        )
                        ));

            foreach (var prop in dependentProperties)
            {
                //System.Diagnostics.Debug.WriteLine(prop.Name);

                // Invoke OnPropertyChanged on caller instance
                MethodInfo mi = type.GetMethod("OnPropertyChanged");
                if (mi != null)
                    mi.Invoke(caller, new string[] { prop.Name });
            }
        }
    }
}
