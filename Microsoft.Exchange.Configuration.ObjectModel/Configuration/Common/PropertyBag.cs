using System;
using System.Collections;
using System.Collections.Specialized;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Diagnostics.Components.ObjectModel;

namespace Microsoft.Exchange.Configuration.Common
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public class PropertyBag : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00003940 File Offset: 0x00001B40
		public static PropertyBag Synchronized(PropertyBag oldBag)
		{
			PropertyBag.SynchronizedPropertyBag synchronizedPropertyBag = null;
			if (oldBag != null)
			{
				synchronizedPropertyBag = new PropertyBag.SynchronizedPropertyBag(oldBag.Count);
				foreach (object obj in oldBag.FieldDictionary)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					synchronizedPropertyBag.Add(dictionaryEntry.Key, dictionaryEntry.Value);
				}
				synchronizedPropertyBag.readOnly = oldBag.readOnly;
			}
			else
			{
				synchronizedPropertyBag = new PropertyBag.SynchronizedPropertyBag(0);
			}
			return synchronizedPropertyBag;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000039D0 File Offset: 0x00001BD0
		public PropertyBag(int initialSize)
		{
			this.readOnly = false;
			this.fieldDictionary = new HybridDictionary(initialSize);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000039EB File Offset: 0x00001BEB
		public PropertyBag() : this(0)
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000039F4 File Offset: 0x00001BF4
		protected PropertyBag(bool isSynchronized)
		{
			this.readOnly = false;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003A03 File Offset: 0x00001C03
		public int Count
		{
			get
			{
				return this.FieldDictionary.Count;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003A10 File Offset: 0x00001C10
		public bool IsSynchronized
		{
			get
			{
				return this.FieldDictionary.IsSynchronized;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003A1D File Offset: 0x00001C1D
		public object SyncRoot
		{
			get
			{
				return this.FieldDictionary.SyncRoot;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003A2C File Offset: 0x00001C2C
		public virtual void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			foreach (object obj in this.FieldDictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				array.SetValue(new DictionaryEntry(dictionaryEntry.Key, ((Field)dictionaryEntry.Value).Data), index);
				index++;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003ABC File Offset: 0x00001CBC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new PropertyBag.PropertyBagEnumerator(this);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003AC4 File Offset: 0x00001CC4
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003AC7 File Offset: 0x00001CC7
		public bool IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x17000014 RID: 20
		public virtual object this[object key]
		{
			get
			{
				Field field = (Field)this.FieldDictionary[key];
				if (field == null)
				{
					return null;
				}
				return field.Data;
			}
			set
			{
				ExTraceGlobals.PropertyBagTracer.Information((long)this.GetHashCode(), "PropertyBag[{0}]={1}.", new object[]
				{
					key,
					value
				});
				if (this.IsReadOnly)
				{
					throw new ReadOnlyPropertyBagException();
				}
				Field field = (Field)this.FieldDictionary[key];
				if (field == null)
				{
					field = (this.FieldDictionary[key] = new Field(null));
				}
				field.Data = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003B70 File Offset: 0x00001D70
		public virtual ICollection Keys
		{
			get
			{
				object[] array = new object[this.Count];
				int num = 0;
				foreach (object obj in this.FieldDictionary.Keys)
				{
					array[num] = obj;
					num++;
				}
				return array;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003BDC File Offset: 0x00001DDC
		public virtual ICollection Values
		{
			get
			{
				object[] array = new object[this.Count];
				int num = 0;
				foreach (object obj in this.FieldDictionary.Values)
				{
					array[num] = ((Field)obj).Data;
					num++;
				}
				return array;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003C54 File Offset: 0x00001E54
		public void Add(object key, object value)
		{
			this[key] = value;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003C5E File Offset: 0x00001E5E
		public virtual void Clear()
		{
			ExTraceGlobals.PropertyBagTracer.Information((long)this.GetHashCode(), "PropertyBag::Clear()");
			if (this.IsReadOnly)
			{
				throw new ReadOnlyPropertyBagException();
			}
			this.FieldDictionary.Clear();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003C8F File Offset: 0x00001E8F
		public bool Contains(object key)
		{
			return this.FieldDictionary.Contains(key);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003C9D File Offset: 0x00001E9D
		public IDictionaryEnumerator GetEnumerator()
		{
			return new PropertyBag.PropertyBagEnumerator(this);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003CA5 File Offset: 0x00001EA5
		public virtual void Remove(object key)
		{
			if (this.IsReadOnly)
			{
				throw new ReadOnlyPropertyBagException();
			}
			this.FieldDictionary.Remove(key);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003CC4 File Offset: 0x00001EC4
		public bool IsChanged(object key)
		{
			Field field = (Field)this.FieldDictionary[key];
			return field != null && field.IsChanged;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003CF0 File Offset: 0x00001EF0
		public bool IsModified(object key)
		{
			Field field = (Field)this.FieldDictionary[key];
			return field != null && field.IsModified;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003D1C File Offset: 0x00001F1C
		public virtual void ResetChangeTracking()
		{
			ExTraceGlobals.PropertyBagTracer.Information((long)this.GetHashCode(), "PropertyBag::ResetChangeTracking()");
			if (this.IsReadOnly)
			{
				throw new ReadOnlyPropertyBagException();
			}
			ICollection values = this.FieldDictionary.Values;
			foreach (object obj in values)
			{
				((Field)obj).ResetChangeTracking();
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003DA0 File Offset: 0x00001FA0
		public virtual void MakeReadOnly()
		{
			ExTraceGlobals.PropertyBagTracer.Information((long)this.GetHashCode(), "DesynchedPropertyBag::MakeReadOnly()");
			this.readOnly = true;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003DBF File Offset: 0x00001FBF
		public virtual void MakeReadWrite()
		{
			ExTraceGlobals.PropertyBagTracer.Information((long)this.GetHashCode(), "DesynchedPropertyBag::MakeReadWrite()");
			this.readOnly = false;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003DDE File Offset: 0x00001FDE
		internal IDictionary FieldDictionary
		{
			get
			{
				return (IDictionary)this.fieldDictionary;
			}
		}

		// Token: 0x04000036 RID: 54
		protected bool readOnly;

		// Token: 0x04000037 RID: 55
		protected object fieldDictionary;

		// Token: 0x0200000A RID: 10
		private sealed class PropertyBagEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06000071 RID: 113 RVA: 0x00003DEB File Offset: 0x00001FEB
			public PropertyBagEnumerator(PropertyBag bag)
			{
				this.fieldEnumerator = bag.FieldDictionary.GetEnumerator();
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000072 RID: 114 RVA: 0x00003E04 File Offset: 0x00002004
			public object Current
			{
				get
				{
					DictionaryEntry entry = this.FieldEnumerator.Entry;
					return new DictionaryEntry(entry.Key, ((Field)entry.Value).Data);
				}
			}

			// Token: 0x06000073 RID: 115 RVA: 0x00003E3F File Offset: 0x0000203F
			public bool MoveNext()
			{
				return this.FieldEnumerator.MoveNext();
			}

			// Token: 0x06000074 RID: 116 RVA: 0x00003E4C File Offset: 0x0000204C
			public void Reset()
			{
				this.FieldEnumerator.Reset();
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x06000075 RID: 117 RVA: 0x00003E59 File Offset: 0x00002059
			public DictionaryEntry Entry
			{
				get
				{
					return (DictionaryEntry)this.Current;
				}
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x06000076 RID: 118 RVA: 0x00003E66 File Offset: 0x00002066
			public object Key
			{
				get
				{
					return this.FieldEnumerator.Key;
				}
			}

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x06000077 RID: 119 RVA: 0x00003E73 File Offset: 0x00002073
			public object Value
			{
				get
				{
					return ((Field)this.FieldEnumerator.Value).Data;
				}
			}

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x06000078 RID: 120 RVA: 0x00003E8A File Offset: 0x0000208A
			private IDictionaryEnumerator FieldEnumerator
			{
				get
				{
					return this.fieldEnumerator;
				}
			}

			// Token: 0x04000038 RID: 56
			private IDictionaryEnumerator fieldEnumerator;
		}

		// Token: 0x0200000B RID: 11
		private sealed class SynchronizedPropertyBag : PropertyBag
		{
			// Token: 0x06000079 RID: 121 RVA: 0x00003E92 File Offset: 0x00002092
			public SynchronizedPropertyBag(int initialSize) : base(true)
			{
				this.fieldDictionary = Hashtable.Synchronized(new Hashtable(initialSize));
			}

			// Token: 0x0600007A RID: 122 RVA: 0x00003EAC File Offset: 0x000020AC
			public override void CopyTo(Array array, int index)
			{
				lock (base.SyncRoot)
				{
					base.CopyTo(array, index);
				}
			}

			// Token: 0x1700001D RID: 29
			public override object this[object key]
			{
				set
				{
					lock (base.SyncRoot)
					{
						base[key] = value;
					}
				}
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x0600007C RID: 124 RVA: 0x00003F34 File Offset: 0x00002134
			public override ICollection Keys
			{
				get
				{
					ICollection keys;
					lock (base.SyncRoot)
					{
						keys = base.Keys;
					}
					return keys;
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x0600007D RID: 125 RVA: 0x00003F78 File Offset: 0x00002178
			public override ICollection Values
			{
				get
				{
					ICollection values;
					lock (base.SyncRoot)
					{
						values = base.Values;
					}
					return values;
				}
			}

			// Token: 0x0600007E RID: 126 RVA: 0x00003FBC File Offset: 0x000021BC
			public override void Clear()
			{
				lock (base.SyncRoot)
				{
					base.Clear();
				}
			}

			// Token: 0x0600007F RID: 127 RVA: 0x00003FFC File Offset: 0x000021FC
			public override void Remove(object key)
			{
				lock (base.SyncRoot)
				{
					base.Remove(key);
				}
			}

			// Token: 0x06000080 RID: 128 RVA: 0x00004040 File Offset: 0x00002240
			public override void ResetChangeTracking()
			{
				lock (base.SyncRoot)
				{
					base.ResetChangeTracking();
				}
			}

			// Token: 0x06000081 RID: 129 RVA: 0x00004080 File Offset: 0x00002280
			public override void MakeReadOnly()
			{
				lock (base.SyncRoot)
				{
					base.MakeReadOnly();
				}
			}

			// Token: 0x06000082 RID: 130 RVA: 0x000040C0 File Offset: 0x000022C0
			public override void MakeReadWrite()
			{
				lock (base.SyncRoot)
				{
					base.MakeReadWrite();
				}
			}
		}
	}
}
