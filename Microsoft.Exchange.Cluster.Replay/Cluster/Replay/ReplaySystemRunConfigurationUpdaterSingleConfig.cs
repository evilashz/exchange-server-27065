using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000303 RID: 771
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplaySystemRunConfigurationUpdaterSingleConfig : ReplaySystemDatabaseQueuedItem
	{
		// Token: 0x06001F4A RID: 8010 RVA: 0x0008DDD8 File Offset: 0x0008BFD8
		public ReplaySystemRunConfigurationUpdaterSingleConfig(ReplicaInstanceManager riManager, Guid dbGuid, ReplayConfigChangeHints changeHint) : base(riManager, dbGuid)
		{
			this.WaitForCompletion = false;
			this.IsHighPriority = false;
			this.ForceRestart = false;
			this.ChangeHint = changeHint;
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x0008DDFE File Offset: 0x0008BFFE
		// (set) Token: 0x06001F4C RID: 8012 RVA: 0x0008DE06 File Offset: 0x0008C006
		public bool WaitForCompletion { get; set; }

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x0008DE0F File Offset: 0x0008C00F
		// (set) Token: 0x06001F4E RID: 8014 RVA: 0x0008DE17 File Offset: 0x0008C017
		public bool IsHighPriority { get; set; }

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06001F4F RID: 8015 RVA: 0x0008DE20 File Offset: 0x0008C020
		// (set) Token: 0x06001F50 RID: 8016 RVA: 0x0008DE28 File Offset: 0x0008C028
		public bool ForceRestart { get; set; }

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06001F51 RID: 8017 RVA: 0x0008DE31 File Offset: 0x0008C031
		// (set) Token: 0x06001F52 RID: 8018 RVA: 0x0008DE39 File Offset: 0x0008C039
		public ReplayConfigChangeHints ChangeHint { get; private set; }

		// Token: 0x06001F53 RID: 8019 RVA: 0x0008DE44 File Offset: 0x0008C044
		public override bool IsEquivalentOrSuperset(IQueuedCallback otherCallback)
		{
			if (base.IsEquivalentOrSuperset(otherCallback))
			{
				return true;
			}
			if (otherCallback is ReplaySystemRunConfigurationUpdaterSingleConfig)
			{
				bool flag = ((ReplaySystemRunConfigurationUpdaterSingleConfig)otherCallback).DbGuid.Equals(base.DbGuid);
				bool flag2 = !((ReplaySystemRunConfigurationUpdaterSingleConfig)otherCallback).WaitForCompletion || this.WaitForCompletion;
				bool flag3 = !((ReplaySystemRunConfigurationUpdaterSingleConfig)otherCallback).ForceRestart || this.ForceRestart;
				return flag && flag2 && flag3;
			}
			return false;
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x0008DEBC File Offset: 0x0008C0BC
		protected override void ExecuteInternal()
		{
			Dependencies.ADConfig.Refresh(string.Format("ReplaySystemRunConfigurationUpdaterSingleConfig: db={0}, hint={1}", base.DbGuid, this.ChangeHint));
			base.ReplicaInstanceManager.ConfigurationUpdater(new Guid?(base.DbGuid), this.WaitForCompletion, this.IsHighPriority, this.ForceRestart, this.ChangeHint);
		}
	}
}
