using System;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200001B RID: 27
	internal enum CommandType
	{
		// Token: 0x04000212 RID: 530
		Unknown,
		// Token: 0x04000213 RID: 531
		Options,
		// Token: 0x04000214 RID: 532
		GetHierarchy,
		// Token: 0x04000215 RID: 533
		Sync,
		// Token: 0x04000216 RID: 534
		GetItemEstimate,
		// Token: 0x04000217 RID: 535
		FolderSync,
		// Token: 0x04000218 RID: 536
		FolderUpdate,
		// Token: 0x04000219 RID: 537
		FolderDelete,
		// Token: 0x0400021A RID: 538
		FolderCreate,
		// Token: 0x0400021B RID: 539
		CreateCollection,
		// Token: 0x0400021C RID: 540
		MoveCollection,
		// Token: 0x0400021D RID: 541
		DeleteCollection,
		// Token: 0x0400021E RID: 542
		GetAttachment,
		// Token: 0x0400021F RID: 543
		MoveItems,
		// Token: 0x04000220 RID: 544
		MeetingResponse,
		// Token: 0x04000221 RID: 545
		SendMail,
		// Token: 0x04000222 RID: 546
		SmartReply,
		// Token: 0x04000223 RID: 547
		SmartForward,
		// Token: 0x04000224 RID: 548
		Search,
		// Token: 0x04000225 RID: 549
		Settings,
		// Token: 0x04000226 RID: 550
		Ping,
		// Token: 0x04000227 RID: 551
		ItemOperations,
		// Token: 0x04000228 RID: 552
		Provision,
		// Token: 0x04000229 RID: 553
		ResolveRecipients,
		// Token: 0x0400022A RID: 554
		ValidateCert,
		// Token: 0x0400022B RID: 555
		ProxyLogin
	}
}
