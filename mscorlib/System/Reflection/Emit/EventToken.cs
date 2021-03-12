using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200060D RID: 1549
	[ComVisible(true)]
	[Serializable]
	public struct EventToken
	{
		// Token: 0x06004951 RID: 18769 RVA: 0x00108AF1 File Offset: 0x00106CF1
		internal EventToken(int str)
		{
			this.m_event = str;
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06004952 RID: 18770 RVA: 0x00108AFA File Offset: 0x00106CFA
		public int Token
		{
			get
			{
				return this.m_event;
			}
		}

		// Token: 0x06004953 RID: 18771 RVA: 0x00108B02 File Offset: 0x00106D02
		public override int GetHashCode()
		{
			return this.m_event;
		}

		// Token: 0x06004954 RID: 18772 RVA: 0x00108B0A File Offset: 0x00106D0A
		public override bool Equals(object obj)
		{
			return obj is EventToken && this.Equals((EventToken)obj);
		}

		// Token: 0x06004955 RID: 18773 RVA: 0x00108B22 File Offset: 0x00106D22
		public bool Equals(EventToken obj)
		{
			return obj.m_event == this.m_event;
		}

		// Token: 0x06004956 RID: 18774 RVA: 0x00108B32 File Offset: 0x00106D32
		public static bool operator ==(EventToken a, EventToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004957 RID: 18775 RVA: 0x00108B3C File Offset: 0x00106D3C
		public static bool operator !=(EventToken a, EventToken b)
		{
			return !(a == b);
		}

		// Token: 0x04001DF8 RID: 7672
		public static readonly EventToken Empty;

		// Token: 0x04001DF9 RID: 7673
		internal int m_event;
	}
}
