using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000128 RID: 296
	public enum ClientAccessAuthenticationMethod
	{
		// Token: 0x0400064F RID: 1615
		[LocDescription(DataStrings.IDs.ClientAccessBasicAuthentication)]
		BasicAuthentication,
		// Token: 0x04000650 RID: 1616
		[LocDescription(DataStrings.IDs.ClientAccessNonBasicAuthentication)]
		NonBasicAuthentication,
		// Token: 0x04000651 RID: 1617
		[LocDescription(DataStrings.IDs.ClientAccessAdfsAuthentication)]
		AdfsAuthentication
	}
}
