using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200063A RID: 1594
	[ComVisible(true)]
	[Serializable]
	public struct TypeToken
	{
		// Token: 0x06004D96 RID: 19862 RVA: 0x001171D1 File Offset: 0x001153D1
		internal TypeToken(int str)
		{
			this.m_class = str;
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06004D97 RID: 19863 RVA: 0x001171DA File Offset: 0x001153DA
		public int Token
		{
			get
			{
				return this.m_class;
			}
		}

		// Token: 0x06004D98 RID: 19864 RVA: 0x001171E2 File Offset: 0x001153E2
		public override int GetHashCode()
		{
			return this.m_class;
		}

		// Token: 0x06004D99 RID: 19865 RVA: 0x001171EA File Offset: 0x001153EA
		public override bool Equals(object obj)
		{
			return obj is TypeToken && this.Equals((TypeToken)obj);
		}

		// Token: 0x06004D9A RID: 19866 RVA: 0x00117202 File Offset: 0x00115402
		public bool Equals(TypeToken obj)
		{
			return obj.m_class == this.m_class;
		}

		// Token: 0x06004D9B RID: 19867 RVA: 0x00117212 File Offset: 0x00115412
		public static bool operator ==(TypeToken a, TypeToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004D9C RID: 19868 RVA: 0x0011721C File Offset: 0x0011541C
		public static bool operator !=(TypeToken a, TypeToken b)
		{
			return !(a == b);
		}

		// Token: 0x04002118 RID: 8472
		public static readonly TypeToken Empty;

		// Token: 0x04002119 RID: 8473
		internal int m_class;
	}
}
