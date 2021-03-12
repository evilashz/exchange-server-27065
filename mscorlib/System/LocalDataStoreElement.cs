using System;

namespace System
{
	// Token: 0x02000106 RID: 262
	internal sealed class LocalDataStoreElement
	{
		// Token: 0x06000FD3 RID: 4051 RVA: 0x000302F8 File Offset: 0x0002E4F8
		public LocalDataStoreElement(long cookie)
		{
			this.m_cookie = cookie;
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00030307 File Offset: 0x0002E507
		// (set) Token: 0x06000FD5 RID: 4053 RVA: 0x0003030F File Offset: 0x0002E50F
		public object Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00030318 File Offset: 0x0002E518
		public long Cookie
		{
			get
			{
				return this.m_cookie;
			}
		}

		// Token: 0x040005AB RID: 1451
		private object m_value;

		// Token: 0x040005AC RID: 1452
		private long m_cookie;
	}
}
