using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000634 RID: 1588
	[ComVisible(true)]
	[Serializable]
	public struct StringToken
	{
		// Token: 0x06004C1B RID: 19483 RVA: 0x00113B78 File Offset: 0x00111D78
		internal StringToken(int str)
		{
			this.m_string = str;
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06004C1C RID: 19484 RVA: 0x00113B81 File Offset: 0x00111D81
		public int Token
		{
			get
			{
				return this.m_string;
			}
		}

		// Token: 0x06004C1D RID: 19485 RVA: 0x00113B89 File Offset: 0x00111D89
		public override int GetHashCode()
		{
			return this.m_string;
		}

		// Token: 0x06004C1E RID: 19486 RVA: 0x00113B91 File Offset: 0x00111D91
		public override bool Equals(object obj)
		{
			return obj is StringToken && this.Equals((StringToken)obj);
		}

		// Token: 0x06004C1F RID: 19487 RVA: 0x00113BA9 File Offset: 0x00111DA9
		public bool Equals(StringToken obj)
		{
			return obj.m_string == this.m_string;
		}

		// Token: 0x06004C20 RID: 19488 RVA: 0x00113BB9 File Offset: 0x00111DB9
		public static bool operator ==(StringToken a, StringToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004C21 RID: 19489 RVA: 0x00113BC3 File Offset: 0x00111DC3
		public static bool operator !=(StringToken a, StringToken b)
		{
			return !(a == b);
		}

		// Token: 0x040020EC RID: 8428
		internal int m_string;
	}
}
