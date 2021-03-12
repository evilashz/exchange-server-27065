using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000747 RID: 1863
	[Serializable]
	public class MailboxInSiteFailoverException : MailboxInTransitException
	{
		// Token: 0x0600481E RID: 18462 RVA: 0x001309CB File Offset: 0x0012EBCB
		public MailboxInSiteFailoverException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600481F RID: 18463 RVA: 0x001309D4 File Offset: 0x0012EBD4
		public MailboxInSiteFailoverException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004820 RID: 18464 RVA: 0x001309DE File Offset: 0x0012EBDE
		protected MailboxInSiteFailoverException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
