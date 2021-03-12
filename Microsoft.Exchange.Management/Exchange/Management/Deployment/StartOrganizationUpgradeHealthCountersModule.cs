using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Configuration.TenantMonitoring;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200019F RID: 415
	internal sealed class StartOrganizationUpgradeHealthCountersModule : CmdletHealthCountersModule
	{
		// Token: 0x06000F0F RID: 3855 RVA: 0x00042F87 File Offset: 0x00041187
		public StartOrganizationUpgradeHealthCountersModule(TaskContext context) : base(context)
		{
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00042F90 File Offset: 0x00041190
		protected override CounterType CounterTypeForAttempts
		{
			get
			{
				return CounterType.StartOrganizationUpgradeAttempts;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x00042F94 File Offset: 0x00041194
		protected override CounterType CounterTypeForSuccesses
		{
			get
			{
				return CounterType.StartOrganizationUpgradeSuccesses;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x00042F98 File Offset: 0x00041198
		protected override CounterType CounterTypeForIterationAttempts
		{
			get
			{
				return CounterType.StartOrganizationUpgradeIterationAttempts;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x00042F9C File Offset: 0x0004119C
		protected override CounterType CounterTypeForIterationSuccesses
		{
			get
			{
				return CounterType.StartOrganizationUpgradeIterationSuccesses;
			}
		}
	}
}
