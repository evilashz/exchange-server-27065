using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200062F RID: 1583
	[ComVisible(true)]
	[Serializable]
	public struct ParameterToken
	{
		// Token: 0x06004BA9 RID: 19369 RVA: 0x0011251C File Offset: 0x0011071C
		internal ParameterToken(int tkParam)
		{
			this.m_tkParameter = tkParam;
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06004BAA RID: 19370 RVA: 0x00112525 File Offset: 0x00110725
		public int Token
		{
			get
			{
				return this.m_tkParameter;
			}
		}

		// Token: 0x06004BAB RID: 19371 RVA: 0x0011252D File Offset: 0x0011072D
		public override int GetHashCode()
		{
			return this.m_tkParameter;
		}

		// Token: 0x06004BAC RID: 19372 RVA: 0x00112535 File Offset: 0x00110735
		public override bool Equals(object obj)
		{
			return obj is ParameterToken && this.Equals((ParameterToken)obj);
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x0011254D File Offset: 0x0011074D
		public bool Equals(ParameterToken obj)
		{
			return obj.m_tkParameter == this.m_tkParameter;
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x0011255D File Offset: 0x0011075D
		public static bool operator ==(ParameterToken a, ParameterToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004BAF RID: 19375 RVA: 0x00112567 File Offset: 0x00110767
		public static bool operator !=(ParameterToken a, ParameterToken b)
		{
			return !(a == b);
		}

		// Token: 0x040020D4 RID: 8404
		public static readonly ParameterToken Empty;

		// Token: 0x040020D5 RID: 8405
		internal int m_tkParameter;
	}
}
