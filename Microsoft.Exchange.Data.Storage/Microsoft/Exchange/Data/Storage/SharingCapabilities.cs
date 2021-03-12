using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000245 RID: 581
	[Flags]
	internal enum SharingCapabilities
	{
		// Token: 0x0400116F RID: 4463
		None = 0,
		// Token: 0x04001170 RID: 4464
		UrlConfiguration = 1,
		// Token: 0x04001171 RID: 4465
		FileAssociation = 2,
		// Token: 0x04001172 RID: 4466
		AssociatedStore = 4,
		// Token: 0x04001173 RID: 4467
		RestrictedStorage = 8,
		// Token: 0x04001174 RID: 4468
		ReadSharing = 16,
		// Token: 0x04001175 RID: 4469
		WriteSharing = 32,
		// Token: 0x04001176 RID: 4470
		PublishSharing = 64,
		// Token: 0x04001177 RID: 4471
		ItemPrivacy = 128,
		// Token: 0x04001178 RID: 4472
		ScopeItems = 256,
		// Token: 0x04001179 RID: 4473
		ScopeSingleFolder = 512,
		// Token: 0x0400117A RID: 4474
		ScopeMultipleFolder = 1024,
		// Token: 0x0400117B RID: 4475
		ScopeHierarchy = 2048,
		// Token: 0x0400117C RID: 4476
		NoRoamBinding = 32768,
		// Token: 0x0400117D RID: 4477
		FreeBinding = 131072,
		// Token: 0x0400117E RID: 4478
		AccessControl = 262144,
		// Token: 0x0400117F RID: 4479
		SubfolderBinding = 1048576
	}
}
