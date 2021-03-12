using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000033 RID: 51
	internal class InvalidExpiryDestinationException : SkipException
	{
		// Token: 0x0600018F RID: 399 RVA: 0x0000B906 File Offset: 0x00009B06
		internal InvalidExpiryDestinationException(LocalizedString message) : base(message)
		{
		}
	}
}
