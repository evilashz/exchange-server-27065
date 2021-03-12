using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000168 RID: 360
	internal interface ISplitOperation
	{
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000E86 RID: 3718
		string Name { get; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000E87 RID: 3719
		ISplitOperationState OperationState { get; }

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000E88 RID: 3720
		IPublicFolderSession CurrentPublicFolderSession { get; }

		// Token: 0x06000E89 RID: 3721
		void Invoke();
	}
}
