using System;
using System.Diagnostics;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200001F RID: 31
	internal class AmConfig
	{
		// Token: 0x06000118 RID: 280 RVA: 0x000074E5 File Offset: 0x000056E5
		internal AmConfig() : this(ReplayStrings.ErrorAmConfigNotInitialized)
		{
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000074F7 File Offset: 0x000056F7
		internal AmConfig(string lastError)
		{
			this.Initialize(AmRole.Unknown, null, null, lastError);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007509 File Offset: 0x00005709
		internal AmConfig(AmRole role, IAmDbState dbState, AmDagConfig dagCfg, string lastError)
		{
			this.Initialize(role, dbState, dagCfg, lastError);
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000751C File Offset: 0x0000571C
		internal static AmConfig UnknownConfig
		{
			get
			{
				return AmConfig.sm_unknownConfig;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00007523 File Offset: 0x00005723
		// (set) Token: 0x0600011D RID: 285 RVA: 0x0000752B File Offset: 0x0000572B
		internal bool IsInternalObjectsDisposed { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00007534 File Offset: 0x00005734
		// (set) Token: 0x0600011F RID: 287 RVA: 0x0000753C File Offset: 0x0000573C
		internal bool IsCurrentConfiguration { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00007545 File Offset: 0x00005745
		// (set) Token: 0x06000121 RID: 289 RVA: 0x0000754D File Offset: 0x0000574D
		internal AmRole Role { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00007556 File Offset: 0x00005756
		// (set) Token: 0x06000123 RID: 291 RVA: 0x0000755E File Offset: 0x0000575E
		internal IAmDbState DbState { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00007567 File Offset: 0x00005767
		// (set) Token: 0x06000125 RID: 293 RVA: 0x0000756F File Offset: 0x0000576F
		internal AmDagConfig DagConfig { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00007578 File Offset: 0x00005778
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00007580 File Offset: 0x00005780
		internal string LastError { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00007589 File Offset: 0x00005789
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00007591 File Offset: 0x00005791
		internal bool IsUnknownTriggeredByADError { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000759A File Offset: 0x0000579A
		// (set) Token: 0x0600012B RID: 299 RVA: 0x000075A2 File Offset: 0x000057A2
		internal ExDateTime TimeCreated { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000075AB File Offset: 0x000057AB
		// (set) Token: 0x0600012D RID: 301 RVA: 0x000075B3 File Offset: 0x000057B3
		internal ExDateTime TimeRoleLastChanged { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000075BC File Offset: 0x000057BC
		// (set) Token: 0x0600012F RID: 303 RVA: 0x000075C4 File Offset: 0x000057C4
		internal Stopwatch PeriodicEventWatch { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000075CD File Offset: 0x000057CD
		internal bool IsPAM
		{
			get
			{
				return this.Role == AmRole.PAM;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000075D8 File Offset: 0x000057D8
		internal bool IsSAM
		{
			get
			{
				return this.Role == AmRole.SAM;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000075E3 File Offset: 0x000057E3
		internal bool IsStandalone
		{
			get
			{
				return this.Role == AmRole.Standalone;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000075EE File Offset: 0x000057EE
		internal bool IsDecidingAuthority
		{
			get
			{
				return this.IsPAM || this.IsStandalone;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00007600 File Offset: 0x00005800
		internal bool IsPamOrSam
		{
			get
			{
				return this.IsPAM || this.IsSAM;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00007612 File Offset: 0x00005812
		internal bool IsUnknown
		{
			get
			{
				return this.Role == AmRole.Unknown;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007620 File Offset: 0x00005820
		internal static AmConfigChangedFlags CheckForChanges(AmConfig left, AmConfig right)
		{
			AmConfigChangedFlags amConfigChangedFlags = AmConfigChangedFlags.None;
			if (left.Role != right.Role)
			{
				amConfigChangedFlags |= AmConfigChangedFlags.Role;
			}
			if (!object.ReferenceEquals(left.DbState, right.DbState))
			{
				amConfigChangedFlags |= AmConfigChangedFlags.DbState;
			}
			if (!SharedHelper.StringIEquals(left.LastError, right.LastError))
			{
				amConfigChangedFlags |= AmConfigChangedFlags.LastError;
			}
			if ((left.DagConfig == null && right.DagConfig != null) || (left.DagConfig != null && right.DagConfig == null))
			{
				amConfigChangedFlags |= AmConfigChangedFlags.DagConfig;
			}
			if (left.DagConfig != null && right.DagConfig != null)
			{
				if (!left.DagConfig.Id.Equals(right.DagConfig.Id))
				{
					amConfigChangedFlags |= AmConfigChangedFlags.DagId;
				}
				if (!AmServerName.IsArrayEquals(left.DagConfig.MemberServers, right.DagConfig.MemberServers))
				{
					amConfigChangedFlags |= AmConfigChangedFlags.MemberServers;
				}
				if (!AmServerName.IsEqual(left.DagConfig.CurrentPAM, right.DagConfig.CurrentPAM))
				{
					amConfigChangedFlags |= AmConfigChangedFlags.CurrentPAM;
				}
				if (!object.ReferenceEquals(left.DagConfig.Cluster, right.DagConfig.Cluster))
				{
					amConfigChangedFlags |= AmConfigChangedFlags.Cluster;
				}
			}
			return amConfigChangedFlags;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007738 File Offset: 0x00005938
		private void Initialize(AmRole role, IAmDbState dbState, AmDagConfig dagCfg, string lastError)
		{
			this.Role = role;
			this.DbState = dbState;
			this.DagConfig = dagCfg;
			this.LastError = lastError;
			this.TimeCreated = ExDateTime.Now;
			this.TimeRoleLastChanged = this.TimeCreated;
			this.PeriodicEventWatch = new Stopwatch();
			this.PeriodicEventWatch.Start();
			this.IsUnknownTriggeredByADError = false;
			this.IsCurrentConfiguration = true;
			this.IsInternalObjectsDisposed = false;
		}

		// Token: 0x0400007C RID: 124
		private static AmConfig sm_unknownConfig = new AmConfig();
	}
}
