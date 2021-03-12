using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x0200017C RID: 380
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderSplitLogger : PublicFolderMailboxLoggerBase
	{
		// Token: 0x06000F22 RID: 3874 RVA: 0x0005A650 File Offset: 0x00058850
		public PublicFolderSplitLogger(IPublicFolderSession publicFolderSession, string logComponent) : base(publicFolderSession, null)
		{
			this.logComponent = logComponent;
			this.logSuffixName = logComponent;
		}
	}
}
