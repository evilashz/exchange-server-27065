using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000749 RID: 1865
	[Serializable]
	public class MailboxMustBeAccessedAsOwnerException : StoragePermanentException
	{
		// Token: 0x06004826 RID: 18470 RVA: 0x00130A56 File Offset: 0x0012EC56
		public MailboxMustBeAccessedAsOwnerException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004827 RID: 18471 RVA: 0x00130A5F File Offset: 0x0012EC5F
		protected MailboxMustBeAccessedAsOwnerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
