using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007BF RID: 1983
	internal enum PermissionLevel
	{
		// Token: 0x04002859 RID: 10329
		None,
		// Token: 0x0400285A RID: 10330
		Owner,
		// Token: 0x0400285B RID: 10331
		PublishingEditor,
		// Token: 0x0400285C RID: 10332
		Editor,
		// Token: 0x0400285D RID: 10333
		PublishingAuthor,
		// Token: 0x0400285E RID: 10334
		Author,
		// Token: 0x0400285F RID: 10335
		NonEditingAuthor,
		// Token: 0x04002860 RID: 10336
		Reviewer,
		// Token: 0x04002861 RID: 10337
		Contributor,
		// Token: 0x04002862 RID: 10338
		Custom
	}
}
