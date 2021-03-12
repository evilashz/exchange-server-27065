using System;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;

namespace Microsoft.Exchange.Transport.RecipientAPI
{
	// Token: 0x02000532 RID: 1330
	internal class TransportIsMemberOfResolverConfig : IsMemberOfResolverConfig
	{
		// Token: 0x06003DFB RID: 15867 RVA: 0x001043F8 File Offset: 0x001025F8
		public TransportIsMemberOfResolverConfig(TransportAppConfig.IsMemberOfResolverConfiguration configuration)
		{
			this.configuration = configuration;
		}

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x06003DFC RID: 15868 RVA: 0x00104407 File Offset: 0x00102607
		public bool Enabled
		{
			get
			{
				return this.configuration.Enabled;
			}
		}

		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x06003DFD RID: 15869 RVA: 0x00104414 File Offset: 0x00102614
		public long ResolvedGroupsMaxSize
		{
			get
			{
				return (long)this.configuration.ResolvedGroupsCacheConfiguration.MaxSize.ToBytes();
			}
		}

		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x06003DFE RID: 15870 RVA: 0x00104439 File Offset: 0x00102639
		public TimeSpan ResolvedGroupsExpirationInterval
		{
			get
			{
				return this.configuration.ResolvedGroupsCacheConfiguration.ExpirationInterval;
			}
		}

		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06003DFF RID: 15871 RVA: 0x0010444B File Offset: 0x0010264B
		public TimeSpan ResolvedGroupsCleanupInterval
		{
			get
			{
				return this.configuration.ResolvedGroupsCacheConfiguration.CleanupInterval;
			}
		}

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x06003E00 RID: 15872 RVA: 0x0010445D File Offset: 0x0010265D
		public TimeSpan ResolvedGroupsPurgeInterval
		{
			get
			{
				return this.configuration.ResolvedGroupsCacheConfiguration.PurgeInterval;
			}
		}

		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x06003E01 RID: 15873 RVA: 0x0010446F File Offset: 0x0010266F
		public TimeSpan ResolvedGroupsRefreshInterval
		{
			get
			{
				return this.configuration.ResolvedGroupsCacheConfiguration.RefreshInterval;
			}
		}

		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x06003E02 RID: 15874 RVA: 0x00104484 File Offset: 0x00102684
		public long ExpandedGroupsMaxSize
		{
			get
			{
				return (long)this.configuration.ExpandedGroupsCacheConfiguration.MaxSize.ToBytes();
			}
		}

		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x06003E03 RID: 15875 RVA: 0x001044A9 File Offset: 0x001026A9
		public TimeSpan ExpandedGroupsExpirationInterval
		{
			get
			{
				return this.configuration.ExpandedGroupsCacheConfiguration.ExpirationInterval;
			}
		}

		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x06003E04 RID: 15876 RVA: 0x001044BB File Offset: 0x001026BB
		public TimeSpan ExpandedGroupsCleanupInterval
		{
			get
			{
				return this.configuration.ExpandedGroupsCacheConfiguration.CleanupInterval;
			}
		}

		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x06003E05 RID: 15877 RVA: 0x001044CD File Offset: 0x001026CD
		public TimeSpan ExpandedGroupsPurgeInterval
		{
			get
			{
				return this.configuration.ExpandedGroupsCacheConfiguration.PurgeInterval;
			}
		}

		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x06003E06 RID: 15878 RVA: 0x001044DF File Offset: 0x001026DF
		public TimeSpan ExpandedGroupsRefreshInterval
		{
			get
			{
				return this.configuration.ExpandedGroupsCacheConfiguration.RefreshInterval;
			}
		}

		// Token: 0x04001F9D RID: 8093
		private readonly TransportAppConfig.IsMemberOfResolverConfiguration configuration;
	}
}
