using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000824 RID: 2084
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public abstract class BaseChannelObjectWithProperties : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x0600591F RID: 22815 RVA: 0x00138D51 File Offset: 0x00136F51
		public virtual IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				return this;
			}
		}

		// Token: 0x17000EEF RID: 3823
		public virtual object this[object key]
		{
			[SecuritySafeCritical]
			get
			{
				return null;
			}
			[SecuritySafeCritical]
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06005922 RID: 22818 RVA: 0x00138D5E File Offset: 0x00136F5E
		public virtual ICollection Keys
		{
			[SecuritySafeCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06005923 RID: 22819 RVA: 0x00138D64 File Offset: 0x00136F64
		public virtual ICollection Values
		{
			[SecuritySafeCritical]
			get
			{
				ICollection keys = this.Keys;
				if (keys == null)
				{
					return null;
				}
				ArrayList arrayList = new ArrayList();
				foreach (object key in keys)
				{
					arrayList.Add(this[key]);
				}
				return arrayList;
			}
		}

		// Token: 0x06005924 RID: 22820 RVA: 0x00138DD0 File Offset: 0x00136FD0
		[SecuritySafeCritical]
		public virtual bool Contains(object key)
		{
			if (key == null)
			{
				return false;
			}
			ICollection keys = this.Keys;
			if (keys == null)
			{
				return false;
			}
			string text = key as string;
			foreach (object obj in keys)
			{
				if (text != null)
				{
					string text2 = obj as string;
					if (text2 != null)
					{
						if (string.Compare(text, text2, StringComparison.OrdinalIgnoreCase) == 0)
						{
							return true;
						}
						continue;
					}
				}
				if (key.Equals(obj))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06005925 RID: 22821 RVA: 0x00138E64 File Offset: 0x00137064
		public virtual bool IsReadOnly
		{
			[SecuritySafeCritical]
			get
			{
				return false;
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06005926 RID: 22822 RVA: 0x00138E67 File Offset: 0x00137067
		public virtual bool IsFixedSize
		{
			[SecuritySafeCritical]
			get
			{
				return true;
			}
		}

		// Token: 0x06005927 RID: 22823 RVA: 0x00138E6A File Offset: 0x0013706A
		[SecuritySafeCritical]
		public virtual void Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005928 RID: 22824 RVA: 0x00138E71 File Offset: 0x00137071
		[SecuritySafeCritical]
		public virtual void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005929 RID: 22825 RVA: 0x00138E78 File Offset: 0x00137078
		[SecuritySafeCritical]
		public virtual void Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600592A RID: 22826 RVA: 0x00138E7F File Offset: 0x0013707F
		[SecuritySafeCritical]
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new DictionaryEnumeratorByKeys(this);
		}

		// Token: 0x0600592B RID: 22827 RVA: 0x00138E87 File Offset: 0x00137087
		[SecuritySafeCritical]
		public virtual void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x0600592C RID: 22828 RVA: 0x00138E90 File Offset: 0x00137090
		public virtual int Count
		{
			[SecuritySafeCritical]
			get
			{
				ICollection keys = this.Keys;
				if (keys == null)
				{
					return 0;
				}
				return keys.Count;
			}
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x0600592D RID: 22829 RVA: 0x00138EAF File Offset: 0x001370AF
		public virtual object SyncRoot
		{
			[SecuritySafeCritical]
			get
			{
				return this;
			}
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x0600592E RID: 22830 RVA: 0x00138EB2 File Offset: 0x001370B2
		public virtual bool IsSynchronized
		{
			[SecuritySafeCritical]
			get
			{
				return false;
			}
		}

		// Token: 0x0600592F RID: 22831 RVA: 0x00138EB5 File Offset: 0x001370B5
		[SecuritySafeCritical]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new DictionaryEnumeratorByKeys(this);
		}
	}
}
