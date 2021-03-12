using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200020E RID: 526
	[Serializable]
	public enum RequestDirection
	{
		// Token: 0x04000B0E RID: 2830
		[LocDescription(MrsStrings.IDs.MoveRequestDirectionPull)]
		Pull = 1,
		// Token: 0x04000B0F RID: 2831
		[LocDescription(MrsStrings.IDs.MoveRequestDirectionPush)]
		Push
	}
}
