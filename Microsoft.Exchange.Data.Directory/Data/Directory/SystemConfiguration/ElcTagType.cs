using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000428 RID: 1064
	[Flags]
	public enum ElcTagType
	{
		// Token: 0x0400204D RID: 8269
		None = 0,
		// Token: 0x0400204E RID: 8270
		[LocDescription(DirectoryStrings.IDs.SystemTag)]
		SystemTag = 1,
		// Token: 0x0400204F RID: 8271
		[LocDescription(DirectoryStrings.IDs.MustDisplayComment)]
		MustDisplayComment = 2,
		// Token: 0x04002050 RID: 8272
		[LocDescription(DirectoryStrings.IDs.PrimaryDefault)]
		PrimaryDefault = 4,
		// Token: 0x04002051 RID: 8273
		[LocDescription(DirectoryStrings.IDs.AutoGroup)]
		AutoGroup = 8,
		// Token: 0x04002052 RID: 8274
		[LocDescription(DirectoryStrings.IDs.ModeratedRecipients)]
		ModeratedRecipients = 16
	}
}
