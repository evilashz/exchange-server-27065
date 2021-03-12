using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200074C RID: 1868
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MessageLoadFailedInConversationException : StoragePermanentException
	{
		// Token: 0x0600482E RID: 18478 RVA: 0x00130AA3 File Offset: 0x0012ECA3
		internal MessageLoadFailedInConversationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600482F RID: 18479 RVA: 0x00130AAC File Offset: 0x0012ECAC
		internal MessageLoadFailedInConversationException(LocalizedString message, Exception exception) : base(message, exception)
		{
		}
	}
}
