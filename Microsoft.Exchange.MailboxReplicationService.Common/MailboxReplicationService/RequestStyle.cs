using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200020D RID: 525
	[Serializable]
	public enum RequestStyle
	{
		// Token: 0x04000B0B RID: 2827
		[LocDescription(MrsStrings.IDs.MoveRequestTypeIntraOrg)]
		IntraOrg = 1,
		// Token: 0x04000B0C RID: 2828
		[LocDescription(MrsStrings.IDs.MoveRequestTypeCrossOrg)]
		CrossOrg
	}
}
