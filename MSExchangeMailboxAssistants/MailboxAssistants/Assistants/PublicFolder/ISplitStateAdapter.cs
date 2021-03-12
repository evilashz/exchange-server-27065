using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x0200016F RID: 367
	internal interface ISplitStateAdapter
	{
		// Token: 0x06000EC3 RID: 3779
		IPublicFolderSplitState ReadFromStore(out Exception error);

		// Token: 0x06000EC4 RID: 3780
		Exception SaveToStore(IPublicFolderSplitState splitState);
	}
}
