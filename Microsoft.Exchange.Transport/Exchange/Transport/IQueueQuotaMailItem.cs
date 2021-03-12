using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020001A7 RID: 423
	internal interface IQueueQuotaMailItem
	{
		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001219 RID: 4633
		string ExoAccountForest { get; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600121A RID: 4634
		Guid ExternalOrganizationId { get; }

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600121B RID: 4635
		string OriginalFromAddress { get; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600121C RID: 4636
		QueueQuotaTrackingBits QueueQuotaTrackingBits { get; }
	}
}
