using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002E8 RID: 744
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ADConsumer : ICacheConsumer
	{
		// Token: 0x06001FAF RID: 8111 RVA: 0x00086703 File Offset: 0x00084903
		internal ADConsumer(ADObjectId id, ITopologyConfigurationSession configSession, OrganizationCache cache)
		{
			this.id = id;
			this.configSession = configSession;
			this.cache = cache;
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x00086720 File Offset: 0x00084920
		object ICacheConsumer.Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x00086728 File Offset: 0x00084928
		// (set) Token: 0x06001FB2 RID: 8114 RVA: 0x00086730 File Offset: 0x00084930
		protected ADObjectId Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06001FB3 RID: 8115 RVA: 0x00086739 File Offset: 0x00084939
		internal IConfigurationSession ConfigSession
		{
			get
			{
				return this.configSession;
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x00086741 File Offset: 0x00084941
		internal OrganizationCache Cache
		{
			get
			{
				return this.cache;
			}
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x00086749 File Offset: 0x00084949
		protected void OnChange(ADNotificationEventArgs args)
		{
			this.cache.InvalidateCache(this.id);
		}

		// Token: 0x040013AA RID: 5034
		internal static readonly ITopologyConfigurationSession ADSystemConfigurationSessionInstance = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 41, "ADSystemConfigurationSessionInstance", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ActiveDirectory\\ADConsumer.cs");

		// Token: 0x040013AB RID: 5035
		private readonly IConfigurationSession configSession;

		// Token: 0x040013AC RID: 5036
		private readonly OrganizationCache cache;

		// Token: 0x040013AD RID: 5037
		private ADObjectId id;
	}
}
