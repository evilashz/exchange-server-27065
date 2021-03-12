using System;
using System.Security.Principal;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x0200004A RID: 74
	public class QueryableSessionsPerUser
	{
		// Token: 0x0600024F RID: 591 RVA: 0x0000E562 File Offset: 0x0000C762
		public QueryableSessionsPerUser(SecurityIdentifier userSid, ClientType clientType, long sessionCount)
		{
			this.UserSid = userSid.ToString();
			this.ClientType = clientType.ToString();
			this.SessionCount = sessionCount;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000E58E File Offset: 0x0000C78E
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000E596 File Offset: 0x0000C796
		public string UserSid { get; private set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000E59F File Offset: 0x0000C79F
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000E5A7 File Offset: 0x0000C7A7
		public string ClientType { get; private set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000E5B0 File Offset: 0x0000C7B0
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0000E5B8 File Offset: 0x0000C7B8
		public long SessionCount { get; private set; }
	}
}
