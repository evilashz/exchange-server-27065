using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020006FD RID: 1789
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ConnectionDroppedNotification : Notification
	{
		// Token: 0x060046E3 RID: 18147 RVA: 0x0012D9AE File Offset: 0x0012BBAE
		internal ConnectionDroppedNotification(string serverDN, string userDN, int tickDeath) : base(NotificationType.ConnectionDropped)
		{
			this.serverDN = serverDN;
			this.userDN = userDN;
			this.tickDeath = tickDeath;
		}

		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x060046E4 RID: 18148 RVA: 0x0012D9D0 File Offset: 0x0012BBD0
		public string ServerDN
		{
			get
			{
				return this.serverDN;
			}
		}

		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x060046E5 RID: 18149 RVA: 0x0012D9D8 File Offset: 0x0012BBD8
		public string UserDN
		{
			get
			{
				return this.userDN;
			}
		}

		// Token: 0x1700149D RID: 5277
		// (get) Token: 0x060046E6 RID: 18150 RVA: 0x0012D9E0 File Offset: 0x0012BBE0
		public int TickDeath
		{
			get
			{
				return this.tickDeath;
			}
		}

		// Token: 0x040026B8 RID: 9912
		private readonly string serverDN;

		// Token: 0x040026B9 RID: 9913
		private readonly string userDN;

		// Token: 0x040026BA RID: 9914
		private readonly int tickDeath;
	}
}
