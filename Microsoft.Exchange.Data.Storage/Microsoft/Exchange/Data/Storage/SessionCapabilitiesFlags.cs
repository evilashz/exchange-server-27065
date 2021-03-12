using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000248 RID: 584
	[Flags]
	internal enum SessionCapabilitiesFlags
	{
		// Token: 0x04001188 RID: 4488
		None = 0,
		// Token: 0x04001189 RID: 4489
		CanSend = 1,
		// Token: 0x0400118A RID: 4490
		CanDeliver = 2,
		// Token: 0x0400118B RID: 4491
		CanCreateDefaultFolders = 4,
		// Token: 0x0400118C RID: 4492
		MustHideDefaultFolders = 8,
		// Token: 0x0400118D RID: 4493
		CanHaveDelegateUsers = 16,
		// Token: 0x0400118E RID: 4494
		CanHaveExternalUsers = 32,
		// Token: 0x0400118F RID: 4495
		CanHaveRules = 64,
		// Token: 0x04001190 RID: 4496
		CanHaveJunkEmailRule = 128,
		// Token: 0x04001191 RID: 4497
		CanHaveMasterCategoryList = 256,
		// Token: 0x04001192 RID: 4498
		CanHaveOof = 512,
		// Token: 0x04001193 RID: 4499
		CanHaveUserConfigurationManager = 1024,
		// Token: 0x04001194 RID: 4500
		MustCreateFolderHierarchy = 2048,
		// Token: 0x04001195 RID: 4501
		CanHaveCulture = 4096,
		// Token: 0x04001196 RID: 4502
		CanSetCalendarAPIProperties = 8192,
		// Token: 0x04001197 RID: 4503
		ReadOnly = 16384,
		// Token: 0x04001198 RID: 4504
		Default = 6135
	}
}
