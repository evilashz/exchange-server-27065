using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000034 RID: 52
	internal class SkipFolderException : SkipException
	{
		// Token: 0x06000190 RID: 400 RVA: 0x0000B90F File Offset: 0x00009B0F
		internal SkipFolderException(LocalizedString message) : base(message)
		{
		}
	}
}
