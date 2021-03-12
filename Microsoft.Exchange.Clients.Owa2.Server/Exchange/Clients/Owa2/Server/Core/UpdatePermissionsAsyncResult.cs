using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200004C RID: 76
	public class UpdatePermissionsAsyncResult
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000880C File Offset: 0x00006A0C
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00008814 File Offset: 0x00006A14
		public AttachmentResultCode ResultCode { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000881D File Offset: 0x00006A1D
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00008825 File Offset: 0x00006A25
		public Dictionary<string, IEnumerable<IUserSharingResult>> ResultsDictionary { get; set; }
	}
}
