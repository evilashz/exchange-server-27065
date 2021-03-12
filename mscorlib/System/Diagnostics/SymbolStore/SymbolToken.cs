using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x020003DC RID: 988
	[ComVisible(true)]
	public struct SymbolToken
	{
		// Token: 0x060032C6 RID: 12998 RVA: 0x000C1C10 File Offset: 0x000BFE10
		public SymbolToken(int val)
		{
			this.m_token = val;
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x000C1C19 File Offset: 0x000BFE19
		public int GetToken()
		{
			return this.m_token;
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000C1C21 File Offset: 0x000BFE21
		public override int GetHashCode()
		{
			return this.m_token;
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x000C1C29 File Offset: 0x000BFE29
		public override bool Equals(object obj)
		{
			return obj is SymbolToken && this.Equals((SymbolToken)obj);
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000C1C41 File Offset: 0x000BFE41
		public bool Equals(SymbolToken obj)
		{
			return obj.m_token == this.m_token;
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x000C1C51 File Offset: 0x000BFE51
		public static bool operator ==(SymbolToken a, SymbolToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x000C1C5B File Offset: 0x000BFE5B
		public static bool operator !=(SymbolToken a, SymbolToken b)
		{
			return !(a == b);
		}

		// Token: 0x0400164B RID: 5707
		internal int m_token;
	}
}
