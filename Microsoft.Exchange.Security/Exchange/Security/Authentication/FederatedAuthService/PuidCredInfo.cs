using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x02000081 RID: 129
	internal class PuidCredInfo
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x00024CC4 File Offset: 0x00022EC4
		public PuidCredInfo(ExDateTime time, string puid, byte[] hash, string tag, int lifeTimeInMinutes, string passwordExpiry, UserType userType, bool appPassword, bool requestPasswordConfidenceCheckInBackend)
		{
			this.Time = time;
			this.PUID = puid;
			this.Tag = tag;
			this.Hash = hash;
			this.LifeTimeInMinutes = lifeTimeInMinutes;
			this.RequestPasswordConfidenceCheckInBackend = requestPasswordConfidenceCheckInBackend;
			this.passwordExpiry = passwordExpiry;
			this.userType = userType;
			this.appPassword = appPassword;
		}

		// Token: 0x040004F4 RID: 1268
		public ExDateTime Time;

		// Token: 0x040004F5 RID: 1269
		public string PUID;

		// Token: 0x040004F6 RID: 1270
		public string Tag;

		// Token: 0x040004F7 RID: 1271
		public byte[] Hash;

		// Token: 0x040004F8 RID: 1272
		public int LifeTimeInMinutes;

		// Token: 0x040004F9 RID: 1273
		public bool RequestPasswordConfidenceCheckInBackend;

		// Token: 0x040004FA RID: 1274
		public string passwordExpiry;

		// Token: 0x040004FB RID: 1275
		public UserType userType;

		// Token: 0x040004FC RID: 1276
		public bool appPassword;
	}
}
