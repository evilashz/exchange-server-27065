using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.Cluster.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002F1 RID: 753
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplayAdObjectLookup : IReplayAdObjectLookup
	{
		// Token: 0x06001E66 RID: 7782 RVA: 0x0008A679 File Offset: 0x00088879
		public ReplayAdObjectLookup()
		{
			this.Initialize(ConsistencyMode.IgnoreInvalid);
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06001E67 RID: 7783 RVA: 0x0008A688 File Offset: 0x00088888
		// (set) Token: 0x06001E68 RID: 7784 RVA: 0x0008A690 File Offset: 0x00088890
		public ITopologyConfigurationSession AdSession { get; private set; }

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x0008A699 File Offset: 0x00088899
		// (set) Token: 0x06001E6A RID: 7786 RVA: 0x0008A6A1 File Offset: 0x000888A1
		public IFindAdObject<IADDatabaseAvailabilityGroup> DagLookup { get; private set; }

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001E6B RID: 7787 RVA: 0x0008A6AA File Offset: 0x000888AA
		// (set) Token: 0x06001E6C RID: 7788 RVA: 0x0008A6B2 File Offset: 0x000888B2
		public IFindAdObject<IADDatabase> DatabaseLookup { get; private set; }

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x0008A6BB File Offset: 0x000888BB
		// (set) Token: 0x06001E6E RID: 7790 RVA: 0x0008A6C3 File Offset: 0x000888C3
		public IFindAdObject<IADServer> ServerLookup { get; private set; }

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x0008A6CC File Offset: 0x000888CC
		// (set) Token: 0x06001E70 RID: 7792 RVA: 0x0008A6D4 File Offset: 0x000888D4
		public IFindMiniServer MiniServerLookup { get; set; }

		// Token: 0x06001E71 RID: 7793 RVA: 0x0008A6E0 File Offset: 0x000888E0
		public void Clear()
		{
			ReplayAdObjectLookup.Tracer.TraceDebug((long)this.GetHashCode(), "Clearing cache");
			this.DagLookup.Clear();
			this.DatabaseLookup.Clear();
			this.ServerLookup.Clear();
			this.MiniServerLookup.Clear();
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x0008A730 File Offset: 0x00088930
		protected void Initialize(ConsistencyMode adConsistencyMode)
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(adConsistencyMode, ADSessionSettings.FromRootOrgScopeSet(), 101, "Initialize", "f:\\15.00.1497\\sources\\dev\\cluster\\src\\Replay\\Service\\ReplayAdObjectLookup.cs");
			IADToplogyConfigurationSession adSession = ADSessionFactory.CreateWrapper(topologyConfigurationSession);
			TimeSpan timeToLive = TimeSpan.FromSeconds((double)RegistryParameters.AdObjectCacheHitTtlInSec);
			TimeSpan timeToNegativeLive = TimeSpan.FromSeconds((double)RegistryParameters.AdObjectCacheMissTtlInSec);
			this.AdSession = topologyConfigurationSession;
			this.DagLookup = new AdObjectLookupCache<IADDatabaseAvailabilityGroup>(adSession, timeToLive, timeToNegativeLive);
			this.DatabaseLookup = new AdObjectLookupCache<IADDatabase>(adSession, timeToLive, timeToNegativeLive);
			this.ServerLookup = new AdObjectLookupCache<IADServer>(adSession, timeToLive, timeToNegativeLive);
			this.MiniServerLookup = new MiniServerLookupCache(adSession, timeToLive, timeToNegativeLive);
		}

		// Token: 0x04000CC3 RID: 3267
		public static readonly Trace Tracer = ExTraceGlobals.ADCacheTracer;
	}
}
