using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AssemblyScanner
{

    public static class Scanner
    {
        static public Dictionary<Type, ConstructorInfo> SupportedTypes = new Dictionary<Type, ConstructorInfo>();

        public static async Task Initialize()
        {
                var allTypes = from asm in AppDomain.CurrentDomain.GetAssemblies()
                               from t in asm.GetTypes()
                               select t;


                SupportedTypes = (from type in allTypes.AsParallel()
                                  where (type.GetConstructor(Type.EmptyTypes) != null)
                                  select type).ToDictionary((t) => t, (t) => t.GetConstructor(Type.EmptyTypes));
        }

        public static Object Create(this Type sourceType)
        {
            if (Scanner.SupportedTypes.ContainsKey(sourceType) == false)
                throw new ArgumentException("Default constructor for " + sourceType.GetType().ToString() + " not found!");

            return Scanner.SupportedTypes[sourceType].Invoke(Type.EmptyTypes);
        }
    }
}

