using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x02000460 RID: 1120
	[ComVisible(true)]
	[Serializable]
	public abstract class DictionaryBase : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06003677 RID: 13943 RVA: 0x000D0FFD File Offset: 0x000CF1FD
		protected Hashtable InnerHashtable
		{
			get
			{
				if (this.hashtable == null)
				{
					this.hashtable = new Hashtable();
				}
				return this.hashtable;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06003678 RID: 13944 RVA: 0x000D1018 File Offset: 0x000CF218
		protected IDictionary Dictionary
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06003679 RID: 13945 RVA: 0x000D101B File Offset: 0x000CF21B
		public int Count
		{
			get
			{
				if (this.hashtable != null)
				{
					return this.hashtable.Count;
				}
				return 0;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x0600367A RID: 13946 RVA: 0x000D1032 File Offset: 0x000CF232
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.InnerHashtable.IsReadOnly;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x0600367B RID: 13947 RVA: 0x000D103F File Offset: 0x000CF23F
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.InnerHashtable.IsFixedSize;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x0600367C RID: 13948 RVA: 0x000D104C File Offset: 0x000CF24C
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerHashtable.IsSynchronized;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x0600367D RID: 13949 RVA: 0x000D1059 File Offset: 0x000CF259
		ICollection IDictionary.Keys
		{
			get
			{
				return this.InnerHashtable.Keys;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x0600367E RID: 13950 RVA: 0x000D1066 File Offset: 0x000CF266
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerHashtable.SyncRoot;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x0600367F RID: 13951 RVA: 0x000D1073 File Offset: 0x000CF273
		ICollection IDictionary.Values
		{
			get
			{
				return this.InnerHashtable.Values;
			}
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x000D1080 File Offset: 0x000CF280
		public void CopyTo(Array array, int index)
		{
			this.InnerHashtable.CopyTo(array, index);
		}

		// Token: 0x1700081E RID: 2078
		object IDictionary.this[object key]
		{
			get
			{
				object obj = this.InnerHashtable[key];
				this.OnGet(key, obj);
				return obj;
			}
			set
			{
				this.OnValidate(key, value);
				bool flag = true;
				object obj = this.InnerHashtable[key];
				if (obj == null)
				{
					flag = this.InnerHashtable.Contains(key);
				}
				this.OnSet(key, obj, value);
				this.InnerHashtable[key] = value;
				try
				{
					this.OnSetComplete(key, obj, value);
				}
				catch
				{
					if (flag)
					{
						this.InnerHashtable[key] = obj;
					}
					else
					{
						this.InnerHashtable.Remove(key);
					}
					throw;
				}
			}
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x000D113C File Offset: 0x000CF33C
		bool IDictionary.Contains(object key)
		{
			return this.InnerHashtable.Contains(key);
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x000D114C File Offset: 0x000CF34C
		void IDictionary.Add(object key, object value)
		{
			this.OnValidate(key, value);
			this.OnInsert(key, value);
			this.InnerHashtable.Add(key, value);
			try
			{
				this.OnInsertComplete(key, value);
			}
			catch
			{
				this.InnerHashtable.Remove(key);
				throw;
			}
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x000D11A0 File Offset: 0x000CF3A0
		public void Clear()
		{
			this.OnClear();
			this.InnerHashtable.Clear();
			this.OnClearComplete();
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x000D11BC File Offset: 0x000CF3BC
		void IDictionary.Remove(object key)
		{
			if (this.InnerHashtable.Contains(key))
			{
				object value = this.InnerHashtable[key];
				this.OnValidate(key, value);
				this.OnRemove(key, value);
				this.InnerHashtable.Remove(key);
				try
				{
					this.OnRemoveComplete(key, value);
				}
				catch
				{
					this.InnerHashtable.Add(key, value);
					throw;
				}
			}
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x000D122C File Offset: 0x000CF42C
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.InnerHashtable.GetEnumerator();
		}

		// Token: 0x06003688 RID: 13960 RVA: 0x000D1239 File Offset: 0x000CF439
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.InnerHashtable.GetEnumerator();
		}

		// Token: 0x06003689 RID: 13961 RVA: 0x000D1246 File Offset: 0x000CF446
		protected virtual object OnGet(object key, object currentValue)
		{
			return currentValue;
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x000D1249 File Offset: 0x000CF449
		protected virtual void OnSet(object key, object oldValue, object newValue)
		{
		}

		// Token: 0x0600368B RID: 13963 RVA: 0x000D124B File Offset: 0x000CF44B
		protected virtual void OnInsert(object key, object value)
		{
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x000D124D File Offset: 0x000CF44D
		protected virtual void OnClear()
		{
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x000D124F File Offset: 0x000CF44F
		protected virtual void OnRemove(object key, object value)
		{
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x000D1251 File Offset: 0x000CF451
		protected virtual void OnValidate(object key, object value)
		{
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x000D1253 File Offset: 0x000CF453
		protected virtual void OnSetComplete(object key, object oldValue, object newValue)
		{
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x000D1255 File Offset: 0x000CF455
		protected virtual void OnInsertComplete(object key, object value)
		{
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x000D1257 File Offset: 0x000CF457
		protected virtual void OnClearComplete()
		{
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x000D1259 File Offset: 0x000CF459
		protected virtual void OnRemoveComplete(object key, object value)
		{
		}

		// Token: 0x040017FE RID: 6142
		private Hashtable hashtable;
	}
}
