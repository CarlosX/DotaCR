using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dota2CustomRealms
{
    class EventfulDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {

        public delegate void DataChangedEventHandler(object sender, DataChangedEventArgs e);

        public event DataChangedEventHandler BeforeAdd;
        public event DataChangedEventHandler AfterAdd;
        public event DataChangedEventHandler BeforeRemove;
        public event DataChangedEventHandler AfterRemove;

        public class DataChangedEventArgs : EventArgs
        {

            public DataChangedEventArgs(TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
            }

            public TKey key;
            public TValue value;
        }

        public void Add(TKey key, TValue value, bool Event)
        {
            if (Event)
            {
                this.Add(key, value);
            }
            else
            {
                base.Add(key, value);
            }
        }

        public new void Add(TKey key, TValue value)
        {
            if(BeforeAdd != null)
                BeforeAdd(this, new EventfulDictionary<TKey,TValue>.DataChangedEventArgs(key, value));
            base.Add(key, value);
            if (AfterAdd != null)
                AfterAdd(this, new EventfulDictionary<TKey, TValue>.DataChangedEventArgs(key, value));
        }

        public new void Remove(TKey key)
        {
            TValue oldval = default(TValue);
            if (this.ContainsKey(key))
            {
                if (BeforeRemove != null)
                    BeforeRemove(this, new EventfulDictionary<TKey, TValue>.DataChangedEventArgs(key, oldval));
                oldval = this[key];
                base.Remove(key);
                if (AfterRemove != null)
                    AfterRemove(this, new EventfulDictionary<TKey, TValue>.DataChangedEventArgs(key, oldval));
            }
            
        }

        public void Remove(TKey key, bool Event)
        {
            if (Event)
            {
                this.Remove(key);
            }
            else
            {
                base.Remove(key);
            }
        }
    }
}
