using System;
using System.Runtime.CompilerServices;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001DC RID: 476
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ADConfigLookupComponent : IServiceComponent
	{
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x0004CDC8 File Offset: 0x0004AFC8
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MonitoringTracer;
			}
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0004CDD0 File Offset: 0x0004AFD0
		internal ADConfigLookupComponent()
		{
			IReplayAdObjectLookup replayAdObjectLookup = Dependencies.ReplayAdObjectLookup;
			IReplayAdObjectLookup replayAdObjectLookupPartiallyConsistent = Dependencies.ReplayAdObjectLookupPartiallyConsistent;
			this.AdSession = ADSessionFactory.CreateIgnoreInvalidRootOrgSession(true);
			this.AdSessionPartiallyConsistent = ADSessionFactory.CreatePartiallyConsistentRootOrgSession(true);
			this.ADConfigManager = new MonitoringADConfigManager(replayAdObjectLookup, replayAdObjectLookupPartiallyConsistent, this.AdSession, this.AdSessionPartiallyConsistent);
			Dependencies.Container.RegisterInstance<IMonitoringADConfigProvider>(this.ADConfigManager);
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x0004CE31 File Offset: 0x0004B031
		// (set) Token: 0x060012F6 RID: 4854 RVA: 0x0004CE39 File Offset: 0x0004B039
		public MonitoringADConfigManager ADConfigManager { get; private set; }

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x0004CE42 File Offset: 0x0004B042
		// (set) Token: 0x060012F8 RID: 4856 RVA: 0x0004CE4A File Offset: 0x0004B04A
		public IADToplogyConfigurationSession AdSession { get; private set; }

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x0004CE53 File Offset: 0x0004B053
		// (set) Token: 0x060012FA RID: 4858 RVA: 0x0004CE5B File Offset: 0x0004B05B
		public IADToplogyConfigurationSession AdSessionPartiallyConsistent { get; private set; }

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x0004CE64 File Offset: 0x0004B064
		public string Name
		{
			get
			{
				return "Active Directory Lookup Component";
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x0004CE6B File Offset: 0x0004B06B
		public FacilityEnum Facility
		{
			get
			{
				return FacilityEnum.ADConfigLookup;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x0004CE6F File Offset: 0x0004B06F
		public bool IsCritical
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x0004CE72 File Offset: 0x0004B072
		public bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x0004CE75 File Offset: 0x0004B075
		public bool IsRetriableOnError
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0004CE78 File Offset: 0x0004B078
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public void Invoke(Action toInvoke)
		{
			toInvoke();
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0004CE80 File Offset: 0x0004B080
		public bool Start()
		{
			this.ADConfigManager.Start();
			return true;
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0004CE8E File Offset: 0x0004B08E
		public void Stop()
		{
			this.ADConfigManager.Stop();
		}
	}
}
