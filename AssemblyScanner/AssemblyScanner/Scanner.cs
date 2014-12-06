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
        //lockTocken 
        private static object lockToken = new object();

        public static Dictionary<Type, ConstructorInfo> SupportedTypes = new Dictionary<Type, ConstructorInfo>();

        public static async Task ScanAssemblys()
        {
            lock (lockToken)
            {
                /*
                Use PLINQ because it automatically choose between synchronous and asynchronous way
                */

                //Select all types from all assemblys where type have default ctor 
                SupportedTypes = (from asm in AppDomain.CurrentDomain.GetAssemblies().AsParallel() //All assemblys
                                  from type in asm.GetTypes().AsParallel() //All types
                                  where (type.GetConstructor(Type.EmptyTypes) != null)
                                  select type //With default ctor
                                ).ToDictionary((type) => type, (type) => type.GetConstructor(Type.EmptyTypes)); //Format as dictionary (type,ctor)
            }
        }

        public static Object Create(this Type sourceType)
        {
            lock (lockToken)
            {
                if (Scanner.SupportedTypes.ContainsKey(sourceType) == false)
                    throw new ArgumentException("Default constructor for " + sourceType.GetType().ToString() + " not found!");

                return Scanner.SupportedTypes[sourceType].Invoke(Type.EmptyTypes);
            }
        }
    }
}

