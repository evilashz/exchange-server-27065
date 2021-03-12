using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200074B RID: 1867
	[Serializable]
	public class MailboxUnavailableException : StoragePermanentException
	{
		// Token: 0x0600482B RID: 18475 RVA: 0x00130A86 File Offset: 0x0012EC86
		public MailboxUnavailableException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600482C RID: 18476 RVA: 0x00130A8F File Offset: 0x0012EC8F
		public MailboxUnavailableException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600482D RID: 18477 RVA: 0x00130A99 File Offset: 0x0012EC99
		protected MailboxUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
