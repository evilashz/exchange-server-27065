using System;
using System.Collections.Generic;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200022F RID: 559
	internal class ClientScanResultStorage
	{
		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x0004BFD1 File Offset: 0x0004A1D1
		// (set) Token: 0x06001555 RID: 5461 RVA: 0x0004BFD9 File Offset: 0x0004A1D9
		public List<string> ClassifiedParts { get; set; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x0004BFE2 File Offset: 0x0004A1E2
		// (set) Token: 0x06001557 RID: 5463 RVA: 0x0004BFEA File Offset: 0x0004A1EA
		public List<DiscoveredDataClassification> DlpDetectedClassificationObjects { get; set; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x0004BFF3 File Offset: 0x0004A1F3
		// (set) Token: 0x06001559 RID: 5465 RVA: 0x0004BFFB File Offset: 0x0004A1FB
		public int RecoveryOptions { get; set; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x0004C004 File Offset: 0x0004A204
		// (set) Token: 0x0600155B RID: 5467 RVA: 0x0004C00C File Offset: 0x0004A20C
		public string DetectedClassificationIds { get; set; }

		// Token: 0x0600155C RID: 5468 RVA: 0x0004C015 File Offset: 0x0004A215
		internal ClientScanResultStorage()
		{
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0004C020 File Offset: 0x0004A220
		public static ClientScanResultStorage CreateInstance(string clientData)
		{
			if (string.IsNullOrEmpty(clientData))
			{
				return new ClientScanResultStorage
				{
					ClassifiedParts = new List<string>(),
					DetectedClassificationIds = string.Empty,
					DlpDetectedClassificationObjects = new List<DiscoveredDataClassification>(),
					RecoveryOptions = 0
				};
			}
			return clientData.ToClientScanResultStorage();
		}
	}
}
