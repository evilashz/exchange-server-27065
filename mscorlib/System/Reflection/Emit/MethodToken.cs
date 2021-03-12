using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000622 RID: 1570
	[ComVisible(true)]
	[Serializable]
	public struct MethodToken
	{
		// Token: 0x06004AE1 RID: 19169 RVA: 0x0010E740 File Offset: 0x0010C940
		internal MethodToken(int str)
		{
			this.m_method = str;
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06004AE2 RID: 19170 RVA: 0x0010E749 File Offset: 0x0010C949
		public int Token
		{
			get
			{
				return this.m_method;
			}
		}

		// Token: 0x06004AE3 RID: 19171 RVA: 0x0010E751 File Offset: 0x0010C951
		public override int GetHashCode()
		{
			return this.m_method;
		}

		// Token: 0x06004AE4 RID: 19172 RVA: 0x0010E759 File Offset: 0x0010C959
		public override bool Equals(object obj)
		{
			return obj is MethodToken && this.Equals((MethodToken)obj);
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x0010E771 File Offset: 0x0010C971
		public bool Equals(MethodToken obj)
		{
			return obj.m_method == this.m_method;
		}

		// Token: 0x06004AE6 RID: 19174 RVA: 0x0010E781 File Offset: 0x0010C981
		public static bool operator ==(MethodToken a, MethodToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004AE7 RID: 19175 RVA: 0x0010E78B File Offset: 0x0010C98B
		public static bool operator !=(MethodToken a, MethodToken b)
		{
			return !(a == b);
		}

		// Token: 0x04001E98 RID: 7832
		public static readonly MethodToken Empty;

		// Token: 0x04001E99 RID: 7833
		internal int m_method;
	}
}
