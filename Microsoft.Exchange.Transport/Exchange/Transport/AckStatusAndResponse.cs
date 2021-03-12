using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000153 RID: 339
	internal struct AckStatusAndResponse
	{
		// Token: 0x06000ED2 RID: 3794 RVA: 0x00039A05 File Offset: 0x00037C05
		public AckStatusAndResponse(AckStatus ackStatus, SmtpResponse smtpResponse)
		{
			this.AckStatus = ackStatus;
			this.SmtpResponse = smtpResponse;
		}

		// Token: 0x0400074E RID: 1870
		public AckStatus AckStatus;

		// Token: 0x0400074F RID: 1871
		public SmtpResponse SmtpResponse;
	}
}
