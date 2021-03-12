using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Configuration.TenantMonitoring;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001A2 RID: 418
	internal sealed class RemoveOrgHealthCountersModule : CmdletHealthCountersModule
	{
		// Token: 0x06000F1F RID: 3871 RVA: 0x00042FF3 File Offset: 0x000411F3
		public RemoveOrgHealthCountersModule(TaskContext context) : base(context)
		{
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x00042FFC File Offset: 0x000411FC
		protected override CounterType CounterTypeForAttempts
		{
			get
			{
				return CounterType.RemoveOrganizationAttempts;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x00043000 File Offset: 0x00041200
		protected override CounterType CounterTypeForSuccesses
		{
			get
			{
				return CounterType.RemoveOrganizationSuccesses;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x00043004 File Offset: 0x00041204
		protected override CounterType CounterTypeForIterationAttempts
		{
			get
			{
				return CounterType.RemoveOrganizationIterationAttempts;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x00043008 File Offset: 0x00041208
		protected override CounterType CounterTypeForIterationSuccesses
		{
			get
			{
				return CounterType.RemoveOrganizationIterationSuccesses;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0004300C File Offset: 0x0004120C
		protected override string TenantNameForMonitoringCounters
		{
			get
			{
				OrganizationIdParameter organizationIdParameter = base.CurrentTaskContext.InvocationInfo.Fields["Identity"] as OrganizationIdParameter;
				if (organizationIdParameter != null)
				{
					return organizationIdParameter.ToString();
				}
				return base.TenantNameForMonitoringCounters;
			}
		}
	}
}
