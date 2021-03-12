using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200074A RID: 1866
	[Serializable]
	public class MailboxOfflineException : ConnectionFailedTransientException
	{
		// Token: 0x06004828 RID: 18472 RVA: 0x00130A69 File Offset: 0x0012EC69
		public MailboxOfflineException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004829 RID: 18473 RVA: 0x00130A72 File Offset: 0x0012EC72
		public MailboxOfflineException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600482A RID: 18474 RVA: 0x00130A7C File Offset: 0x0012EC7C
		protected MailboxOfflineException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
