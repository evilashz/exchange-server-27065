using System;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000071 RID: 113
	internal class LiveIdBasicAuthInfo
	{
		// Token: 0x040003CF RID: 975
		public byte[] username;

		// Token: 0x040003D0 RID: 976
		public byte[] password;

		// Token: 0x040003D1 RID: 977
		public byte[] passwordHash;

		// Token: 0x040003D2 RID: 978
		public bool isValidCookie;

		// Token: 0x040003D3 RID: 979
		public string key;

		// Token: 0x040003D4 RID: 980
		public UserType userType;

		// Token: 0x040003D5 RID: 981
		public string puid;

		// Token: 0x040003D6 RID: 982
		public string ticket;

		// Token: 0x040003D7 RID: 983
		public bool isExpired = true;

		// Token: 0x040003D8 RID: 984
		public string passwordExpirationHint;

		// Token: 0x040003D9 RID: 985
		public bool isAppPassword;

		// Token: 0x040003DA RID: 986
		public bool authenticatedByOfflineOrgId;

		// Token: 0x040003DB RID: 987
		public LiveIdAuthResult? OfflineOrgIdFailureResult;
	}
}
