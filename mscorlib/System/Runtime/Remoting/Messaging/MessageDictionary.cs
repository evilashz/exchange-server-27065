using System;
using System.Collections;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000838 RID: 2104
	internal abstract class MessageDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x060059F2 RID: 23026 RVA: 0x0013AD43 File Offset: 0x00138F43
		internal MessageDictionary(string[] keys, IDictionary idict)
		{
			this._keys = keys;
			this._dict = idict;
		}

		// Token: 0x060059F3 RID: 23027 RVA: 0x0013AD59 File Offset: 0x00138F59
		internal bool HasUserData()
		{
			return this._dict != null && this._dict.Count > 0;
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x060059F4 RID: 23028 RVA: 0x0013AD74 File Offset: 0x00138F74
		internal IDictionary InternalDictionary
		{
			get
			{
				return this._dict;
			}
		}

		// Token: 0x060059F5 RID: 23029
		internal abstract object GetMessageValue(int i);

		// Token: 0x060059F6 RID: 23030
		[SecurityCritical]
		internal abstract void SetSpecialKey(int keyNum, object value);

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x060059F7 RID: 23031 RVA: 0x0013AD7C File Offset: 0x00138F7C
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x060059F8 RID: 23032 RVA: 0x0013AD7F File Offset: 0x00138F7F
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x060059F9 RID: 23033 RVA: 0x0013AD82 File Offset: 0x00138F82
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x060059FA RID: 23034 RVA: 0x0013AD85 File Offset: 0x00138F85
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060059FB RID: 23035 RVA: 0x0013AD88 File Offset: 0x00138F88
		public virtual bool Contains(object key)
		{
			return this.ContainsSpecialKey(key) || (this._dict != null && this._dict.Contains(key));
		}

		// Token: 0x060059FC RID: 23036 RVA: 0x0013ADAC File Offset: 0x00138FAC
		protected virtual bool ContainsSpecialKey(object key)
		{
			if (!(key is string))
			{
				return false;
			}
			string text = (string)key;
			for (int i = 0; i < this._keys.Length; i++)
			{
				if (text.Equals(this._keys[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060059FD RID: 23037 RVA: 0x0013ADF0 File Offset: 0x00138FF0
		public virtual void CopyTo(Array array, int index)
		{
			for (int i = 0; i < this._keys.Length; i++)
			{
				array.SetValue(this.GetMessageValue(i), index + i);
			}
			if (this._dict != null)
			{
				this._dict.CopyTo(array, index + this._keys.Length);
			}
		}

		// Token: 0x17000F45 RID: 3909
		public virtual object this[object key]
		{
			get
			{
				string text = key as string;
				if (text != null)
				{
					for (int i = 0; i < this._keys.Length; i++)
					{
						if (text.Equals(this._keys[i]))
						{
							return this.GetMessageValue(i);
						}
					}
					if (this._dict != null)
					{
						return this._dict[key];
					}
				}
				return null;
			}
			[SecuritySafeCritical]
			set
			{
				if (!this.ContainsSpecialKey(key))
				{
					if (this._dict == null)
					{
						this._dict = new Hashtable();
					}
					this._dict[key] = value;
					return;
				}
				if (key.Equals(Message.UriKey))
				{
					this.SetSpecialKey(0, value);
					return;
				}
				if (key.Equals(Message.CallContextKey))
				{
					this.SetSpecialKey(1, value);
					return;
				}
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKey"));
			}
		}

		// Token: 0x06005A00 RID: 23040 RVA: 0x0013AF0A File Offset: 0x0013910A
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new MessageDictionaryEnumerator(this, this._dict);
		}

		// Token: 0x06005A01 RID: 23041 RVA: 0x0013AF18 File Offset: 0x00139118
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005A02 RID: 23042 RVA: 0x0013AF1F File Offset: 0x0013911F
		public virtual void Add(object key, object value)
		{
			if (this.ContainsSpecialKey(key))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKey"));
			}
			if (this._dict == null)
			{
				this._dict = new Hashtable();
			}
			this._dict.Add(key, value);
		}

		// Token: 0x06005A03 RID: 23043 RVA: 0x0013AF5A File Offset: 0x0013915A
		public virtual void Clear()
		{
			if (this._dict != null)
			{
				this._dict.Clear();
			}
		}

		// Token: 0x06005A04 RID: 23044 RVA: 0x0013AF6F File Offset: 0x0013916F
		public virtual void Remove(object key)
		{
			if (this.ContainsSpecialKey(key) || this._dict == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKey"));
			}
			this._dict.Remove(key);
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06005A05 RID: 23045 RVA: 0x0013AFA0 File Offset: 0x001391A0
		public virtual ICollection Keys
		{
			get
			{
				int num = this._keys.Length;
				ICollection collection = (this._dict != null) ? this._dict.Keys : null;
				if (collection != null)
				{
					num += collection.Count;
				}
				ArrayList arrayList = new ArrayList(num);
				for (int i = 0; i < this._keys.Length; i++)
				{
					arrayList.Add(this._keys[i]);
				}
				if (collection != null)
				{
					arrayList.AddRange(collection);
				}
				return arrayList;
			}
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06005A06 RID: 23046 RVA: 0x0013B010 File Offset: 0x00139210
		public virtual ICollection Values
		{
			get
			{
				int num = this._keys.Length;
				ICollection collection = (this._dict != null) ? this._dict.Keys : null;
				if (collection != null)
				{
					num += collection.Count;
				}
				ArrayList arrayList = new ArrayList(num);
				for (int i = 0; i < this._keys.Length; i++)
				{
					arrayList.Add(this.GetMessageValue(i));
				}
				if (collection != null)
				{
					arrayList.AddRange(collection);
				}
				return arrayList;
			}
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06005A07 RID: 23047 RVA: 0x0013B07C File Offset: 0x0013927C
		public virtual int Count
		{
			get
			{
				if (this._dict != null)
				{
					return this._dict.Count + this._keys.Length;
				}
				return this._keys.Length;
			}
		}

		// Token: 0x04002889 RID: 10377
		internal string[] _keys;

		// Token: 0x0400288A RID: 10378
		internal IDictionary _dict;
	}
}
