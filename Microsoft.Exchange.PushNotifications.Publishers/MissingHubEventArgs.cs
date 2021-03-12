using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200008C RID: 140
	internal class MissingHubEventArgs : EventArgs
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x0000FA44 File Offset: 0x0000DC44
		public MissingHubEventArgs(string targetAppId, string hubName)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("targetAppId", targetAppId);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("hubName", hubName);
			this.TargetAppId = targetAppId;
			this.HubName = hubName;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000FA70 File Offset: 0x0000DC70
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x0000FA78 File Offset: 0x0000DC78
		public string HubName { get; private set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0000FA81 File Offset: 0x0000DC81
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0000FA89 File Offset: 0x0000DC89
		public string TargetAppId { get; private set; }
	}
}
