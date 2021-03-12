using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.Cluster.DirectoryServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002EA RID: 746
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NoncachingReplayAdObjectLookup : IReplayAdObjectLookup
	{
		// Token: 0x06001E06 RID: 7686 RVA: 0x00089946 File Offset: 0x00087B46
		public NoncachingReplayAdObjectLookup()
		{
			this.Initialize(ConsistencyMode.IgnoreInvalid);
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x00089955 File Offset: 0x00087B55
		// (set) Token: 0x06001E08 RID: 7688 RVA: 0x0008995D File Offset: 0x00087B5D
		public ITopologyConfigurationSession AdSession { get; private set; }

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x00089966 File Offset: 0x00087B66
		// (set) Token: 0x06001E0A RID: 7690 RVA: 0x0008996E File Offset: 0x00087B6E
		public IFindAdObject<IADDatabaseAvailabilityGroup> DagLookup { get; private set; }

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x00089977 File Offset: 0x00087B77
		// (set) Token: 0x06001E0C RID: 7692 RVA: 0x0008997F File Offset: 0x00087B7F
		public IFindAdObject<IADDatabase> DatabaseLookup { get; private set; }

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x00089988 File Offset: 0x00087B88
		// (set) Token: 0x06001E0E RID: 7694 RVA: 0x00089990 File Offset: 0x00087B90
		public IFindAdObject<IADServer> ServerLookup { get; private set; }

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x00089999 File Offset: 0x00087B99
		// (set) Token: 0x06001E10 RID: 7696 RVA: 0x000899A1 File Offset: 0x00087BA1
		public IFindMiniServer MiniServerLookup { get; set; }

		// Token: 0x06001E11 RID: 7697 RVA: 0x000899AA File Offset: 0x00087BAA
		public void Clear()
		{
			this.DagLookup.Clear();
			this.DatabaseLookup.Clear();
			this.ServerLookup.Clear();
			this.MiniServerLookup.Clear();
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x000899D8 File Offset: 0x00087BD8
		protected void Initialize(ConsistencyMode adConsistencyMode)
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(adConsistencyMode, ADSessionSettings.FromRootOrgScopeSet(), 92, "Initialize", "f:\\15.00.1497\\sources\\dev\\cluster\\src\\Replay\\Service\\NoncachingReplayAdObjectLookup.cs");
			this.AdSession = topologyConfigurationSession;
			IADToplogyConfigurationSession adSession = ADSessionFactory.CreateWrapper(topologyConfigurationSession);
			this.DagLookup = new SimpleAdObjectLookup<IADDatabaseAvailabilityGroup>(adSession);
			this.DatabaseLookup = new SimpleAdObjectLookup<IADDatabase>(adSession);
			this.ServerLookup = new SimpleAdObjectLookup<IADServer>(adSession);
			this.MiniServerLookup = new SimpleMiniServerLookup(adSession);
		}
	}
}
