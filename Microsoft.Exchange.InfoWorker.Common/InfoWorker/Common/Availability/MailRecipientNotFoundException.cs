using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200008A RID: 138
	internal class MailRecipientNotFoundException : AvailabilityException
	{
		// Token: 0x0600034C RID: 844 RVA: 0x0000E829 File Offset: 0x0000CA29
		public MailRecipientNotFoundException(LocalizedString message, uint locationIdentifier) : base(ErrorConstants.MailRecipientNotFound, message, locationIdentifier)
		{
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000E838 File Offset: 0x0000CA38
		public MailRecipientNotFoundException(Exception innerException, uint locationIdentifier) : base(ErrorConstants.MailRecipientNotFound, Strings.descMailRecipientNotFound, innerException, locationIdentifier)
		{
		}
	}
}
