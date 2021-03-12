using System;

namespace Microsoft.Exchange.Connections.Eas.Commands.MoveItems
{
	// Token: 0x0200005A RID: 90
	[Flags]
	public enum MoveItemsStatus
	{
		// Token: 0x0400017C RID: 380
		InvalidSourceId = 4097,
		// Token: 0x0400017D RID: 381
		InvalidDestinationId = 4098,
		// Token: 0x0400017E RID: 382
		Success = 3,
		// Token: 0x0400017F RID: 383
		SourceDestinationIdentical = 4100,
		// Token: 0x04000180 RID: 384
		CannotMove = 4101,
		// Token: 0x04000181 RID: 385
		Retry = 263,
		// Token: 0x04000182 RID: 386
		CompositeStatusError = 510
	}
}
