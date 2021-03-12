using System;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x0200007F RID: 127
	internal class LogonCacheConfig
	{
		// Token: 0x06000467 RID: 1127 RVA: 0x00024C08 File Offset: 0x00022E08
		private LogonCacheConfig(int badCredsLifetime, int badCredsRecoverableLifetime)
		{
			this.badCredsLifetime = badCredsLifetime;
			this.badCredsRecoverableLifetime = badCredsRecoverableLifetime;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00024C1E File Offset: 0x00022E1E
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x00024C25 File Offset: 0x00022E25
		public static LogonCacheConfig Config
		{
			get
			{
				return LogonCacheConfig.config;
			}
			private set
			{
				LogonCacheConfig.config = value;
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00024C2D File Offset: 0x00022E2D
		public static void Initialize(int badCredsLifetime, int badCredsRecoverableLifetime)
		{
			LogonCacheConfig.config = new LogonCacheConfig(badCredsLifetime, badCredsRecoverableLifetime);
		}

		// Token: 0x040004EC RID: 1260
		public readonly int badCredsLifetime;

		// Token: 0x040004ED RID: 1261
		public readonly int badCredsRecoverableLifetime;

		// Token: 0x040004EE RID: 1262
		private static LogonCacheConfig config;
	}
}
