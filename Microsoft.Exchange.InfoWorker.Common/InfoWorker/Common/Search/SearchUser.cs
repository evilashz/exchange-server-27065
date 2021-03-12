using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x0200022E RID: 558
	internal class SearchUser
	{
		// Token: 0x06000F46 RID: 3910 RVA: 0x00044125 File Offset: 0x00042325
		public SearchUser(ADObjectId id, string displayName, string serverName)
		{
			this.id = id;
			this.serverName = serverName;
			this.displayName = displayName;
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x00044142 File Offset: 0x00042342
		public ADObjectId Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x0004414A File Offset: 0x0004234A
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x00044152 File Offset: 0x00042352
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04000A78 RID: 2680
		private ADObjectId id;

		// Token: 0x04000A79 RID: 2681
		private string serverName;

		// Token: 0x04000A7A RID: 2682
		private string displayName;
	}
}
