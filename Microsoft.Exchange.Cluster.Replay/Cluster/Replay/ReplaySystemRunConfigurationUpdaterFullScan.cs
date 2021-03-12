using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000302 RID: 770
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplaySystemRunConfigurationUpdaterFullScan : ReplaySystemQueuedItem
	{
		// Token: 0x06001F43 RID: 8003 RVA: 0x0008DC6E File Offset: 0x0008BE6E
		public ReplaySystemRunConfigurationUpdaterFullScan(ReplicaInstanceManager riManager, ReplayConfigChangeHints changeHint) : base(riManager)
		{
			this.WaitForCompletion = false;
			this.ChangeHint = changeHint;
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06001F44 RID: 8004 RVA: 0x0008DC85 File Offset: 0x0008BE85
		// (set) Token: 0x06001F45 RID: 8005 RVA: 0x0008DC8D File Offset: 0x0008BE8D
		public bool WaitForCompletion { get; set; }

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06001F46 RID: 8006 RVA: 0x0008DC96 File Offset: 0x0008BE96
		// (set) Token: 0x06001F47 RID: 8007 RVA: 0x0008DC9E File Offset: 0x0008BE9E
		public ReplayConfigChangeHints ChangeHint { get; private set; }

		// Token: 0x06001F48 RID: 8008 RVA: 0x0008DCA8 File Offset: 0x0008BEA8
		public override bool IsEquivalentOrSuperset(IQueuedCallback otherCallback)
		{
			if (base.IsEquivalentOrSuperset(otherCallback))
			{
				return true;
			}
			if (otherCallback is ReplaySystemRunConfigurationUpdaterFullScan)
			{
				return !((ReplaySystemRunConfigurationUpdaterFullScan)otherCallback).WaitForCompletion || this.WaitForCompletion;
			}
			if (otherCallback is ReplaySystemRunConfigurationUpdaterSingleConfig)
			{
				bool flag = !((ReplaySystemRunConfigurationUpdaterSingleConfig)otherCallback).WaitForCompletion || this.WaitForCompletion;
				bool flag2 = !((ReplaySystemRunConfigurationUpdaterSingleConfig)otherCallback).ForceRestart;
				return flag && flag2;
			}
			return false;
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x0008DD20 File Offset: 0x0008BF20
		protected override void ExecuteInternal()
		{
			try
			{
				base.ReplicaInstanceManager.ConfigurationUpdater(null, this.WaitForCompletion, false, false, this.ChangeHint);
			}
			catch (TaskServerException ex)
			{
				ReplayEventLogConstants.Tuple_ConfigUpdaterScanFailed.LogEvent(base.ReplicaInstanceManager.GetHashCode().ToString(), new object[]
				{
					ex.Message
				});
				throw;
			}
			catch (TaskServerTransientException ex2)
			{
				ReplayEventLogConstants.Tuple_ConfigUpdaterScanFailed.LogEvent(base.ReplicaInstanceManager.GetHashCode().ToString(), new object[]
				{
					ex2.Message
				});
				throw;
			}
		}
	}
}
