using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000212 RID: 530
	[Flags]
	internal enum PropertyChangeMetadataProcessingFlags
	{
		// Token: 0x04000F51 RID: 3921
		None = 0,
		// Token: 0x04000F52 RID: 3922
		SeriesMasterDataPropagationOperation = 1,
		// Token: 0x04000F53 RID: 3923
		MarkAllPropagatedPropertiesAsException = 2,
		// Token: 0x04000F54 RID: 3924
		OverrideMetadata = 4
	}
}
