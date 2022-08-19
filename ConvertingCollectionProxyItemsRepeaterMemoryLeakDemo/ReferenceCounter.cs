using System;
using System.Collections.Generic;

namespace ConvertingCollectionProxyItemsRepeaterMemoryLeakDemo
{
    public static class ReferenceCounter
    {
        private static readonly List<WeakReference<object>> References = new List<WeakReference<object>>();

        public static void Add(object obj)
        {
            if (obj is null)
            {
                return;
            }

            References.Add(new WeakReference<object>(obj));
        }

        public static IList<object> Instances
        {
            get
            {
                GC.Collect();
                var instances = new List<object>();
                for (int i = 0; i < References.Count; i++)
                {
                    var reference = References[i];
                    if (reference.TryGetTarget(out var obj))
                    {
                        instances.Add(obj);
                    }
                    else
                    {
                        References.RemoveAt(i);
                        i--;
                    }
                }

                return instances;
            }
        }

        public static int AliveCount
        {
            get
            {
                int count = 0;
                GC.Collect();
                foreach (var reference in References)
                {
                    if (reference.TryGetTarget(out _))
                    {
                        count++;
                    }
                }

                return count;
            }
        }
    }
}
