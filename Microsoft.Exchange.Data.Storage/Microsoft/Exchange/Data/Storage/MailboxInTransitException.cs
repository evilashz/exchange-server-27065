using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000746 RID: 1862
	[Serializable]
	public class MailboxInTransitException : ConnectionFailedTransientException
	{
		// Token: 0x0600481B RID: 18459 RVA: 0x001309AE File Offset: 0x0012EBAE
		public MailboxInTransitException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600481C RID: 18460 RVA: 0x001309B7 File Offset: 0x0012EBB7
		public MailboxInTransitException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600481D RID: 18461 RVA: 0x001309C1 File Offset: 0x0012EBC1
		protected MailboxInTransitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
