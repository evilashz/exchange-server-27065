using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000072 RID: 114
	public enum MapiObjectTrackedType : uint
	{
		// Token: 0x0400023F RID: 575
		Session,
		// Token: 0x04000240 RID: 576
		Logon,
		// Token: 0x04000241 RID: 577
		Message,
		// Token: 0x04000242 RID: 578
		Attachment,
		// Token: 0x04000243 RID: 579
		Folder,
		// Token: 0x04000244 RID: 580
		Notify,
		// Token: 0x04000245 RID: 581
		Stream,
		// Token: 0x04000246 RID: 582
		MessageView,
		// Token: 0x04000247 RID: 583
		FolderView,
		// Token: 0x04000248 RID: 584
		AttachmentView,
		// Token: 0x04000249 RID: 585
		PermissionView,
		// Token: 0x0400024A RID: 586
		FastTransferSource,
		// Token: 0x0400024B RID: 587
		FastTransferDestination,
		// Token: 0x0400024C RID: 588
		UntrackedObject,
		// Token: 0x0400024D RID: 589
		MaxTrackedObjectType = 13U
	}
}
