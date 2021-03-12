using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D9A RID: 3482
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class InvalidExternalSharingSubscriberException : InvalidSharingTargetRecipientException
	{
		// Token: 0x060077D0 RID: 30672 RVA: 0x00211164 File Offset: 0x0020F364
		public InvalidExternalSharingSubscriberException(Exception innerException) : base(innerException)
		{
		}

		// Token: 0x060077D1 RID: 30673 RVA: 0x0021116D File Offset: 0x0020F36D
		public InvalidExternalSharingSubscriberException()
		{
		}
	}
}
