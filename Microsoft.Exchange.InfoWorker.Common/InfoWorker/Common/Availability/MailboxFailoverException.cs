using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000A1 RID: 161
	internal class MailboxFailoverException : AvailabilityException
	{
		// Token: 0x0600036B RID: 875 RVA: 0x0000EA3E File Offset: 0x0000CC3E
		public MailboxFailoverException() : base(ErrorConstants.MailboxFailover, Strings.descMailboxFailover)
		{
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000EA50 File Offset: 0x0000CC50
		public MailboxFailoverException(Exception innerException) : base(ErrorConstants.MailboxFailover, Strings.descMailboxFailover, innerException)
		{
		}
	}
}
