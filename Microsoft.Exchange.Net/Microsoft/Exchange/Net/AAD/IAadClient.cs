using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Net.AAD
{
	// Token: 0x02000582 RID: 1410
	internal interface IAadClient
	{
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001272 RID: 4722
		// (set) Token: 0x06001273 RID: 4723
		int Timeout { get; set; }

		// Token: 0x06001274 RID: 4724
		string GetUserObjectId(string userPrincipalName);

		// Token: 0x06001275 RID: 4725
		List<AadDevice> GetUserDevicesWithEasID(string easId, string userObjectId);

		// Token: 0x06001276 RID: 4726
		string EvaluateAuthPolicy(string easId, string userObjectId, bool isSupportedPlatform);

		// Token: 0x06001277 RID: 4727
		bool IsUserMemberOfGroup(string userObjectId, string groupObjectId);

		// Token: 0x06001278 RID: 4728
		string[] GetGroupMembership(string userObjectId);
	}
}
