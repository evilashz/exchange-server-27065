using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x02000080 RID: 128
	internal class HashedCredInfo
	{
		// Token: 0x0600046B RID: 1131 RVA: 0x00024C3B File Offset: 0x00022E3B
		public HashedCredInfo(byte[] hash, ExDateTime time, CredFailure mode, string tag, UserType userType)
		{
			this.Hash = hash;
			this.Time = time;
			this.Mode = mode;
			this.Tag = tag;
			this.UserType = userType;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00024C68 File Offset: 0x00022E68
		public bool IsExpired(LogonCacheConfig config, ExDateTime time)
		{
			int num;
			switch (this.Mode)
			{
			case CredFailure.Expired:
			case CredFailure.LockedOut:
			case CredFailure.LiveIdFailure:
			case CredFailure.STSFailure:
				num = config.badCredsRecoverableLifetime;
				goto IL_35;
			}
			num = config.badCredsLifetime;
			IL_35:
			return time >= this.Time.Add(TimeSpan.FromMinutes((double)num));
		}

		// Token: 0x040004EF RID: 1263
		public byte[] Hash;

		// Token: 0x040004F0 RID: 1264
		public ExDateTime Time;

		// Token: 0x040004F1 RID: 1265
		public CredFailure Mode;

		// Token: 0x040004F2 RID: 1266
		public string Tag;

		// Token: 0x040004F3 RID: 1267
		public UserType UserType;
	}
}
