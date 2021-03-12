using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000244 RID: 580
	[Flags]
	internal enum SharingFlavor
	{
		// Token: 0x0400115B RID: 4443
		None = 0,
		// Token: 0x0400115C RID: 4444
		SharingIn = 1,
		// Token: 0x0400115D RID: 4445
		BindingIn = 2,
		// Token: 0x0400115E RID: 4446
		IndexIn = 4,
		// Token: 0x0400115F RID: 4447
		ExclusiveIn = 8,
		// Token: 0x04001160 RID: 4448
		SharingOut = 16,
		// Token: 0x04001161 RID: 4449
		BindingOut = 32,
		// Token: 0x04001162 RID: 4450
		IndexOut = 64,
		// Token: 0x04001163 RID: 4451
		ExclusiveOut = 128,
		// Token: 0x04001164 RID: 4452
		SharingMessage = 256,
		// Token: 0x04001165 RID: 4453
		SharingMessageInvitation = 512,
		// Token: 0x04001166 RID: 4454
		SharingMessageRequest = 1024,
		// Token: 0x04001167 RID: 4455
		SharingMessageUpdate = 2048,
		// Token: 0x04001168 RID: 4456
		SharingMessageResponse = 4096,
		// Token: 0x04001169 RID: 4457
		SharingMessageAccept = 8192,
		// Token: 0x0400116A RID: 4458
		SharingMessageDeny = 16384,
		// Token: 0x0400116B RID: 4459
		SharingMessageRevoke = 32768,
		// Token: 0x0400116C RID: 4460
		SharingReciprocation = 65536,
		// Token: 0x0400116D RID: 4461
		PrimaryOwnership = 131072
	}
}
