using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000831 RID: 2097
	[Flags]
	public enum SyncPropertyDefinitionFlags
	{
		// Token: 0x04004469 RID: 17513
		Ignore = 1048576,
		// Token: 0x0400446A RID: 17514
		ForwardSync = 2097152,
		// Token: 0x0400446B RID: 17515
		BackSync = 4194304,
		// Token: 0x0400446C RID: 17516
		TwoWay = 6291456,
		// Token: 0x0400446D RID: 17517
		Immutable = 8388608,
		// Token: 0x0400446E RID: 17518
		Shadow = 16777216,
		// Token: 0x0400446F RID: 17519
		Cloud = 33554432,
		// Token: 0x04004470 RID: 17520
		AlwaysReturned = 67108864,
		// Token: 0x04004471 RID: 17521
		NotInMsoDirectory = 134217728,
		// Token: 0x04004472 RID: 17522
		FilteringOnly = 268435456,
		// Token: 0x04004473 RID: 17523
		TaskPopulated = 256,
		// Token: 0x04004474 RID: 17524
		MultiValued = 2,
		// Token: 0x04004475 RID: 17525
		Calculated = 4,
		// Token: 0x04004476 RID: 17526
		ReadOnly = 1,
		// Token: 0x04004477 RID: 17527
		PersistDefaultValue = 32
	}
}
