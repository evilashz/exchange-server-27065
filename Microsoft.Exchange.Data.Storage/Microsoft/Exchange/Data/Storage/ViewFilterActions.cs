using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007B8 RID: 1976
	[Flags]
	internal enum ViewFilterActions
	{
		// Token: 0x0400280B RID: 10251
		None = 0,
		// Token: 0x0400280C RID: 10252
		BindToExisting = 1,
		// Token: 0x0400280D RID: 10253
		FindExisting = 2,
		// Token: 0x0400280E RID: 10254
		FilterFound = 4,
		// Token: 0x0400280F RID: 10255
		CreateFilter = 8,
		// Token: 0x04002810 RID: 10256
		DeleteInvalidSearchFolder = 16,
		// Token: 0x04002811 RID: 10257
		SearchCriteriaApplied = 32,
		// Token: 0x04002812 RID: 10258
		SearchCompleted = 64,
		// Token: 0x04002813 RID: 10259
		SearchFolderPopulateFailed = 128,
		// Token: 0x04002814 RID: 10260
		SubscribeForNotification = 256,
		// Token: 0x04002815 RID: 10261
		PopulateSearchFolderTimedOut = 512,
		// Token: 0x04002816 RID: 10262
		ObjectNotFoundException = 1024,
		// Token: 0x04002817 RID: 10263
		CorruptDataException = 2048,
		// Token: 0x04002818 RID: 10264
		ObjectNotInitializedException = 4096,
		// Token: 0x04002819 RID: 10265
		QueryInProgressException = 8192,
		// Token: 0x0400281A RID: 10266
		Exception = 16384,
		// Token: 0x0400281B RID: 10267
		LinkToSourceFolderSucceeded = 32768
	}
}
