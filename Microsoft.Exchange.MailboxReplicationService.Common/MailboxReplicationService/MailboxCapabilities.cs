using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200002F RID: 47
	internal enum MailboxCapabilities
	{
		// Token: 0x04000191 RID: 401
		PagedEnumerateChanges,
		// Token: 0x04000192 RID: 402
		RunIsInteg,
		// Token: 0x04000193 RID: 403
		ExtendedAclInformation,
		// Token: 0x04000194 RID: 404
		PagedEnumerateHierarchyChanges,
		// Token: 0x04000195 RID: 405
		FolderRules,
		// Token: 0x04000196 RID: 406
		FolderAcls,
		// Token: 0x04000197 RID: 407
		ExportFolders,
		// Token: 0x04000198 RID: 408
		MaxElement,
		// Token: 0x04000199 RID: 409
		PagedGetActions = 1011,
		// Token: 0x0400019A RID: 410
		ReplayActions = 1111
	}
}
