using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Configuration.TenantMonitoring;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001A0 RID: 416
	internal sealed class CompleteOrganizationUpgradeHealthCountersModule : CmdletHealthCountersModule
	{
		// Token: 0x06000F14 RID: 3860 RVA: 0x00042FA0 File Offset: 0x000411A0
		public CompleteOrganizationUpgradeHealthCountersModule(TaskContext context) : base(context)
		{
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x00042FA9 File Offset: 0x000411A9
		protected override CounterType CounterTypeForAttempts
		{
			get
			{
				return CounterType.CompleteOrganizationUpgradeAttempts;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x00042FAD File Offset: 0x000411AD
		protected override CounterType CounterTypeForSuccesses
		{
			get
			{
				return CounterType.CompleteOrganizationUpgradeSuccesses;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x00042FB1 File Offset: 0x000411B1
		protected override CounterType CounterTypeForIterationAttempts
		{
			get
			{
				return CounterType.CompleteOrganizationUpgradeIterationAttempts;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00042FB5 File Offset: 0x000411B5
		protected override CounterType CounterTypeForIterationSuccesses
		{
			get
			{
				return CounterType.CompleteOrganizationUpgradeIterationSuccesses;
			}
		}
	}
}
