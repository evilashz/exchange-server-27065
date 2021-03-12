using System;
using Microsoft.Filtering;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200001B RID: 27
	internal abstract class FilteringServiceInvokerRequest
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00004598 File Offset: 0x00002798
		protected FilteringServiceInvokerRequest(string organizationId, TimeSpan scanTimeout, int textScanLimit, FipsDataStreamFilteringRequest fipsDataStreamFilteringRequest)
		{
			this.OrganizationId = organizationId;
			this.ScanTimeout = scanTimeout;
			this.TextScanLimit = textScanLimit;
			this.FipsDataStreamFilteringRequest = fipsDataStreamFilteringRequest;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000045BD File Offset: 0x000027BD
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x000045C5 File Offset: 0x000027C5
		public string OrganizationId { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000045CE File Offset: 0x000027CE
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x000045D6 File Offset: 0x000027D6
		public TimeSpan ScanTimeout { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000045DF File Offset: 0x000027DF
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x000045E7 File Offset: 0x000027E7
		public int TextScanLimit { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000045F0 File Offset: 0x000027F0
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000045F8 File Offset: 0x000027F8
		public FipsDataStreamFilteringRequest FipsDataStreamFilteringRequest { get; private set; }
	}
}
