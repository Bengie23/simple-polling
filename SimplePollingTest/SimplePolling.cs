using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplePollingTest
{
    public class SimplePolling
    {

        internal string hasChangedKey = "hasChangedKey";
        internal string mainKey = "mykey";

        internal ConcurrentDictionary<string, object> data = new ConcurrentDictionary<string, object>();

        public SimplePolling()
        {
            UpdateHasChanged(false);
        }
        public void Set(string value)
        {
            data.AddOrUpdate(mainKey, value, ((key, existingValue) => value));
            UpdateHasChanged(true);
        }

        public string TryGet()
        {
            if (HasChanged())
            {
                if (data.TryGetValue(mainKey, out object result))
                {
                    UpdateHasChanged(false);
                    return result.ToString();
                }
            }

            return null;
        }

        private bool HasChanged()
        {
            if (data.TryGetValue(hasChangedKey, out object result))
            {
                return Boolean.Parse(result.ToString());
            }

            throw new InvalidOperationException();
        }

        private void UpdateHasChanged(bool value)
        {
            data.AddOrUpdate(hasChangedKey, value, ((key, existingValue) => value));
        }
    }
}
