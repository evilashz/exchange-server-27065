using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BEB RID: 3051
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OofHistoryCorruptionException : Exception
	{
		// Token: 0x06006C49 RID: 27721 RVA: 0x001D0D9E File Offset: 0x001CEF9E
		internal OofHistoryCorruptionException(string message) : base(message)
		{
		}
	}
}
