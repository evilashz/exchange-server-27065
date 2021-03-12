using System;
using System.Collections;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000826 RID: 2086
	internal class AggregateDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06005937 RID: 22839 RVA: 0x00138F37 File Offset: 0x00137137
		public AggregateDictionary(ICollection dictionaries)
		{
			this._dictionaries = dictionaries;
		}

		// Token: 0x17000EFB RID: 3835
		public virtual object this[object key]
		{
			get
			{
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					if (dictionary.Contains(key))
					{
						return dictionary[key];
					}
				}
				return null;
			}
			set
			{
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					if (dictionary.Contains(key))
					{
						dictionary[key] = value;
					}
				}
			}
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x0600593A RID: 22842 RVA: 0x00139014 File Offset: 0x00137214
		public virtual ICollection Keys
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					ICollection keys = dictionary.Keys;
					if (keys != null)
					{
						foreach (object value in keys)
						{
							arrayList.Add(value);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x0600593B RID: 22843 RVA: 0x001390C4 File Offset: 0x001372C4
		public virtual ICollection Values
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					ICollection values = dictionary.Values;
					if (values != null)
					{
						foreach (object value in values)
						{
							arrayList.Add(value);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x0600593C RID: 22844 RVA: 0x00139174 File Offset: 0x00137374
		public virtual bool Contains(object key)
		{
			foreach (object obj in this._dictionaries)
			{
				IDictionary dictionary = (IDictionary)obj;
				if (dictionary.Contains(key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x0600593D RID: 22845 RVA: 0x001391D8 File Offset: 0x001373D8
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x0600593E RID: 22846 RVA: 0x001391DB File Offset: 0x001373DB
		public virtual bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600593F RID: 22847 RVA: 0x001391DE File Offset: 0x001373DE
		public virtual void Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005940 RID: 22848 RVA: 0x001391E5 File Offset: 0x001373E5
		public virtual void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005941 RID: 22849 RVA: 0x001391EC File Offset: 0x001373EC
		public virtual void Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005942 RID: 22850 RVA: 0x001391F3 File Offset: 0x001373F3
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new DictionaryEnumeratorByKeys(this);
		}

		// Token: 0x06005943 RID: 22851 RVA: 0x001391FB File Offset: 0x001373FB
		public virtual void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06005944 RID: 22852 RVA: 0x00139204 File Offset: 0x00137404
		public virtual int Count
		{
			get
			{
				int num = 0;
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					num += dictionary.Count;
				}
				return num;
			}
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06005945 RID: 22853 RVA: 0x00139264 File Offset: 0x00137464
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06005946 RID: 22854 RVA: 0x00139267 File Offset: 0x00137467
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005947 RID: 22855 RVA: 0x0013926A File Offset: 0x0013746A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new DictionaryEnumeratorByKeys(this);
		}

		// Token: 0x04002849 RID: 10313
		private ICollection _dictionaries;
	}
}
