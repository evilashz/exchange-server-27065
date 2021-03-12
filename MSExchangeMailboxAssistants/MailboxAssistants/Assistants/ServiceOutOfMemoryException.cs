using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x02000017 RID: 23
	internal sealed class ServiceOutOfMemoryException : LocalizedException
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x000051B2 File Offset: 0x000033B2
		public ServiceOutOfMemoryException() : base(Strings.descServiceOutOfMemory)
		{
		}
	}
}
