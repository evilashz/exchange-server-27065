using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Rpc.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000068 RID: 104
	internal class AmSelfDismounter : TimerComponent
	{
		// Token: 0x0600047D RID: 1149 RVA: 0x00018070 File Offset: 0x00016270
		public AmSelfDismounter(AmConfigManager cfgMgr) : base(TimeSpan.FromMilliseconds(-1.0), TimeSpan.FromMilliseconds(-1.0), "AmSelfDismounter")
		{
			this.m_configManager = cfgMgr;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000180A0 File Offset: 0x000162A0
		protected override void TimerCallbackInternal()
		{
			if (this.IsRequested)
			{
				if (this.m_configManager.CurrentConfig.IsUnknown)
				{
					AmStoreHelper.DismountAll("TransientFailoverSuppression");
				}
				else
				{
					ReplayCrimsonEvents.DelayedSelfDismountAllSkipped.Log<AmRole>(this.m_configManager.CurrentConfig.Role);
				}
				this.IsRequested = false;
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000180F4 File Offset: 0x000162F4
		internal void ScheduleDismountAllRequest()
		{
			if (!this.IsRequested)
			{
				this.IsRequested = true;
				ReplayCrimsonEvents.DelayedSelfDismountAllQueued.Log<int>(RegistryParameters.SelfDismountAllDelayInSec);
				base.ChangeTimer(TimeSpan.FromSeconds((double)RegistryParameters.SelfDismountAllDelayInSec), TimeSpan.FromMilliseconds(-1.0));
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00018133 File Offset: 0x00016333
		internal void CancelDismountAllRequest()
		{
			if (this.IsRequested)
			{
				ReplayCrimsonEvents.DelayedSelfDismountAllCancelled.Log();
				base.ChangeTimer(TimeSpan.FromMilliseconds(-1.0), TimeSpan.FromMilliseconds(-1.0));
				this.IsRequested = false;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00018170 File Offset: 0x00016370
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x00018178 File Offset: 0x00016378
		internal bool IsRequested { get; set; }

		// Token: 0x040001EA RID: 490
		private AmConfigManager m_configManager;
	}
}
