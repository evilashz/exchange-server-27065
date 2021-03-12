using System;

namespace System.Collections
{
	// Token: 0x02000467 RID: 1127
	[Serializable]
	internal class CompatibleComparer : IEqualityComparer
	{
		// Token: 0x06003718 RID: 14104 RVA: 0x000D3374 File Offset: 0x000D1574
		internal CompatibleComparer(IComparer comparer, IHashCodeProvider hashCodeProvider)
		{
			this._comparer = comparer;
			this._hcp = hashCodeProvider;
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x000D338C File Offset: 0x000D158C
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			if (this._comparer != null)
			{
				return this._comparer.Compare(a, b);
			}
			IComparable comparable = a as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(b);
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_ImplementIComparable"));
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x000D33E0 File Offset: 0x000D15E0
		public bool Equals(object a, object b)
		{
			return this.Compare(a, b) == 0;
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x000D33ED File Offset: 0x000D15ED
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._hcp != null)
			{
				return this._hcp.GetHashCode(obj);
			}
			return obj.GetHashCode();
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x0600371C RID: 14108 RVA: 0x000D3418 File Offset: 0x000D1618
		internal IComparer Comparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x0600371D RID: 14109 RVA: 0x000D3420 File Offset: 0x000D1620
		internal IHashCodeProvider HashCodeProvider
		{
			get
			{
				return this._hcp;
			}
		}

		// Token: 0x04001820 RID: 6176
		private IComparer _comparer;

		// Token: 0x04001821 RID: 6177
		private IHashCodeProvider _hcp;
	}
}
