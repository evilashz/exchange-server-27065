using System;

namespace Microsoft.Exchange.Server.Storage.PropTags
{
	// Token: 0x02000002 RID: 2
	public enum PropCategory
	{
		// Token: 0x04000002 RID: 2
		NoGetProp,
		// Token: 0x04000003 RID: 3
		NoGetPropList,
		// Token: 0x04000004 RID: 4
		NoGetPropListForFastTransfer,
		// Token: 0x04000005 RID: 5
		SetPropRestricted,
		// Token: 0x04000006 RID: 6
		SetPropAllowedForMailboxMove,
		// Token: 0x04000007 RID: 7
		SetPropAllowedForAdmin,
		// Token: 0x04000008 RID: 8
		SetPropAllowedForTransport,
		// Token: 0x04000009 RID: 9
		SetPropAllowedOnEmbeddedMessage,
		// Token: 0x0400000A RID: 10
		FacebookProtectedProperties,
		// Token: 0x0400000B RID: 11
		NoCopy,
		// Token: 0x0400000C RID: 12
		Computed,
		// Token: 0x0400000D RID: 13
		IgnoreSetError,
		// Token: 0x0400000E RID: 14
		MessageBody,
		// Token: 0x0400000F RID: 15
		CAI,
		// Token: 0x04000010 RID: 16
		ServerOnlySyncGroupProperty,
		// Token: 0x04000011 RID: 17
		Sensitive,
		// Token: 0x04000012 RID: 18
		DoNotBumpChangeNumber,
		// Token: 0x04000013 RID: 19
		DoNotDeleteAtFXCopyToDestination,
		// Token: 0x04000014 RID: 20
		Test,
		// Token: 0x04000015 RID: 21
		Count
	}
}
