using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000401 RID: 1025
	internal struct SessionMask
	{
		// Token: 0x0600343F RID: 13375 RVA: 0x000CA56F File Offset: 0x000C876F
		public SessionMask(SessionMask m)
		{
			this.m_mask = m.m_mask;
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x000CA57D File Offset: 0x000C877D
		public SessionMask(uint mask = 0U)
		{
			this.m_mask = (mask & 15U);
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x000CA589 File Offset: 0x000C8789
		public bool IsEqualOrSupersetOf(SessionMask m)
		{
			return (this.m_mask | m.m_mask) == this.m_mask;
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06003442 RID: 13378 RVA: 0x000CA5A0 File Offset: 0x000C87A0
		public static SessionMask All
		{
			get
			{
				return new SessionMask(15U);
			}
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x000CA5A9 File Offset: 0x000C87A9
		public static SessionMask FromId(int perEventSourceSessionId)
		{
			return new SessionMask(1U << perEventSourceSessionId);
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x000CA5B6 File Offset: 0x000C87B6
		public ulong ToEventKeywords()
		{
			return (ulong)this.m_mask << 44;
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x000CA5C2 File Offset: 0x000C87C2
		public static SessionMask FromEventKeywords(ulong m)
		{
			return new SessionMask((uint)(m >> 44));
		}

		// Token: 0x170007DC RID: 2012
		public bool this[int perEventSourceSessionId]
		{
			get
			{
				return ((ulong)this.m_mask & (ulong)(1L << (perEventSourceSessionId & 31))) > 0UL;
			}
			set
			{
				if (value)
				{
					this.m_mask |= 1U << perEventSourceSessionId;
					return;
				}
				this.m_mask &= ~(1U << perEventSourceSessionId);
			}
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x000CA610 File Offset: 0x000C8810
		public static SessionMask operator |(SessionMask m1, SessionMask m2)
		{
			return new SessionMask(m1.m_mask | m2.m_mask);
		}

		// Token: 0x06003449 RID: 13385 RVA: 0x000CA624 File Offset: 0x000C8824
		public static SessionMask operator &(SessionMask m1, SessionMask m2)
		{
			return new SessionMask(m1.m_mask & m2.m_mask);
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x000CA638 File Offset: 0x000C8838
		public static SessionMask operator ^(SessionMask m1, SessionMask m2)
		{
			return new SessionMask(m1.m_mask ^ m2.m_mask);
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x000CA64C File Offset: 0x000C884C
		public static SessionMask operator ~(SessionMask m)
		{
			return new SessionMask(15U & ~m.m_mask);
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x000CA65D File Offset: 0x000C885D
		public static explicit operator ulong(SessionMask m)
		{
			return (ulong)m.m_mask;
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x000CA666 File Offset: 0x000C8866
		public static explicit operator uint(SessionMask m)
		{
			return m.m_mask;
		}

		// Token: 0x04001703 RID: 5891
		private uint m_mask;

		// Token: 0x04001704 RID: 5892
		internal const int SHIFT_SESSION_TO_KEYWORD = 44;

		// Token: 0x04001705 RID: 5893
		internal const uint MASK = 15U;

		// Token: 0x04001706 RID: 5894
		internal const uint MAX = 4U;
	}
}
