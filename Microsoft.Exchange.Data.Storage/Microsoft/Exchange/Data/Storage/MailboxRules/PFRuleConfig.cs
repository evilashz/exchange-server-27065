using System;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BF4 RID: 3060
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PFRuleConfig : IRuleConfig
	{
		// Token: 0x17001DB9 RID: 7609
		// (get) Token: 0x06006D26 RID: 27942 RVA: 0x001D2575 File Offset: 0x001D0775
		public static PFRuleConfig Instance
		{
			get
			{
				return PFRuleConfig.instance;
			}
		}

		// Token: 0x17001DBA RID: 7610
		// (get) Token: 0x06006D27 RID: 27943 RVA: 0x001D257C File Offset: 0x001D077C
		public IsMemberOfResolver<string> IsMemberOfResolver
		{
			get
			{
				if (this.isMemberOfResolver == null)
				{
					this.isMemberOfResolver = new IsMemberOfResolver<string>(new PFRuleConfig.PFIsMemberOfResolverConfig(), new IsMemberOfResolverPerformanceCounters("PublicFolderRules"), new IsMemberOfResolverADAdapter<string>.LegacyDNResolver(false));
				}
				return this.isMemberOfResolver;
			}
		}

		// Token: 0x17001DBB RID: 7611
		// (get) Token: 0x06006D28 RID: 27944 RVA: 0x001D25AC File Offset: 0x001D07AC
		public object SCLJunkThreshold
		{
			get
			{
				return this.sclJunkThreshold;
			}
		}

		// Token: 0x04003E26 RID: 15910
		private const int DefaultSCLJunkThreshold = 4;

		// Token: 0x04003E27 RID: 15911
		private static PFRuleConfig instance = new PFRuleConfig();

		// Token: 0x04003E28 RID: 15912
		private IsMemberOfResolver<string> isMemberOfResolver;

		// Token: 0x04003E29 RID: 15913
		private object sclJunkThreshold = 4;

		// Token: 0x02000BF5 RID: 3061
		private class PFIsMemberOfResolverConfig : IsMemberOfResolverConfig
		{
			// Token: 0x17001DBC RID: 7612
			// (get) Token: 0x06006D2B RID: 27947 RVA: 0x001D25D4 File Offset: 0x001D07D4
			public bool Enabled
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001DBD RID: 7613
			// (get) Token: 0x06006D2C RID: 27948 RVA: 0x001D25D7 File Offset: 0x001D07D7
			public TimeSpan ExpandedGroupsCleanupInterval
			{
				get
				{
					return TimeSpan.FromHours(1.0);
				}
			}

			// Token: 0x17001DBE RID: 7614
			// (get) Token: 0x06006D2D RID: 27949 RVA: 0x001D25E7 File Offset: 0x001D07E7
			public TimeSpan ExpandedGroupsExpirationInterval
			{
				get
				{
					return TimeSpan.FromHours(3.0);
				}
			}

			// Token: 0x17001DBF RID: 7615
			// (get) Token: 0x06006D2E RID: 27950 RVA: 0x001D25F7 File Offset: 0x001D07F7
			public long ExpandedGroupsMaxSize
			{
				get
				{
					return 536870912L;
				}
			}

			// Token: 0x17001DC0 RID: 7616
			// (get) Token: 0x06006D2F RID: 27951 RVA: 0x001D25FF File Offset: 0x001D07FF
			public TimeSpan ExpandedGroupsPurgeInterval
			{
				get
				{
					return TimeSpan.FromMinutes(5.0);
				}
			}

			// Token: 0x17001DC1 RID: 7617
			// (get) Token: 0x06006D30 RID: 27952 RVA: 0x001D260F File Offset: 0x001D080F
			public TimeSpan ExpandedGroupsRefreshInterval
			{
				get
				{
					return TimeSpan.FromMinutes(10.0);
				}
			}

			// Token: 0x17001DC2 RID: 7618
			// (get) Token: 0x06006D31 RID: 27953 RVA: 0x001D261F File Offset: 0x001D081F
			public TimeSpan ResolvedGroupsCleanupInterval
			{
				get
				{
					return TimeSpan.FromHours(1.0);
				}
			}

			// Token: 0x17001DC3 RID: 7619
			// (get) Token: 0x06006D32 RID: 27954 RVA: 0x001D262F File Offset: 0x001D082F
			public TimeSpan ResolvedGroupsExpirationInterval
			{
				get
				{
					return TimeSpan.FromHours(3.0);
				}
			}

			// Token: 0x17001DC4 RID: 7620
			// (get) Token: 0x06006D33 RID: 27955 RVA: 0x001D263F File Offset: 0x001D083F
			public long ResolvedGroupsMaxSize
			{
				get
				{
					return 33554432L;
				}
			}

			// Token: 0x17001DC5 RID: 7621
			// (get) Token: 0x06006D34 RID: 27956 RVA: 0x001D2647 File Offset: 0x001D0847
			public TimeSpan ResolvedGroupsPurgeInterval
			{
				get
				{
					return TimeSpan.FromMinutes(5.0);
				}
			}

			// Token: 0x17001DC6 RID: 7622
			// (get) Token: 0x06006D35 RID: 27957 RVA: 0x001D2657 File Offset: 0x001D0857
			public TimeSpan ResolvedGroupsRefreshInterval
			{
				get
				{
					return TimeSpan.FromMinutes(10.0);
				}
			}
		}
	}
}
