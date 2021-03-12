using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200008B RID: 139
	internal class MailboxLogonFailedException : AvailabilityException
	{
		// Token: 0x0600034E RID: 846 RVA: 0x0000E84C File Offset: 0x0000CA4C
		public MailboxLogonFailedException() : base(ErrorConstants.MailboxLogonFailed, Strings.descMailboxLogonFailed)
		{
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000E85E File Offset: 0x0000CA5E
		public MailboxLogonFailedException(Exception innerException) : base(ErrorConstants.MailboxLogonFailed, Strings.descMailboxLogonFailed, innerException)
		{
		}
	}
}
