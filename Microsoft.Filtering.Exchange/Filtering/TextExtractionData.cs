using System;

namespace Microsoft.Filtering
{
	// Token: 0x02000014 RID: 20
	internal struct TextExtractionData
	{
		// Token: 0x0400001A RID: 26
		public int StreamId;

		// Token: 0x0400001B RID: 27
		public long StreamSize;

		// Token: 0x0400001C RID: 28
		public int ParentId;

		// Token: 0x0400001D RID: 29
		public string Types;

		// Token: 0x0400001E RID: 30
		public string ModuleUsed;

		// Token: 0x0400001F RID: 31
		public string SkippedModules;

		// Token: 0x04000020 RID: 32
		public string FailedModules;

		// Token: 0x04000021 RID: 33
		public string DisabledModules;

		// Token: 0x04000022 RID: 34
		public int TextExtractionResult;

		// Token: 0x04000023 RID: 35
		public string AdditionalInformation;
	}
}
