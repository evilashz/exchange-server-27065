using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002DC RID: 732
	internal class ReportConstraints
	{
		// Token: 0x04000DAD RID: 3501
		internal DeliveryStatus? Status;

		// Token: 0x04000DAE RID: 3502
		internal SmtpAddress[] RecipientPathFilter;

		// Token: 0x04000DAF RID: 3503
		internal string[] Recipients;

		// Token: 0x04000DB0 RID: 3504
		internal ReportTemplate ReportTemplate;

		// Token: 0x04000DB1 RID: 3505
		internal bool BypassDelegateChecking;

		// Token: 0x04000DB2 RID: 3506
		internal MessageTrackingDetailLevel DetailLevel;

		// Token: 0x04000DB3 RID: 3507
		internal bool DoNotResolve;

		// Token: 0x04000DB4 RID: 3508
		internal Unlimited<uint> ResultSize;

		// Token: 0x04000DB5 RID: 3509
		internal bool TrackingAsSender;

		// Token: 0x04000DB6 RID: 3510
		internal SmtpAddress Sender;

		// Token: 0x04000DB7 RID: 3511
		internal bool ReturnQueueEvents;
	}
}
