using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Configuration.TenantMonitoring;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001A1 RID: 417
	internal sealed class NewOrganizationHealthCountersModule : CmdletHealthCountersModule
	{
		// Token: 0x06000F19 RID: 3865 RVA: 0x00042FB9 File Offset: 0x000411B9
		public NewOrganizationHealthCountersModule(TaskContext context) : base(context)
		{
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00042FC2 File Offset: 0x000411C2
		protected override CounterType CounterTypeForAttempts
		{
			get
			{
				return CounterType.NewOrganizationAttempts;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x00042FC6 File Offset: 0x000411C6
		protected override CounterType CounterTypeForSuccesses
		{
			get
			{
				return CounterType.NewOrganizationSuccesses;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x00042FCA File Offset: 0x000411CA
		protected override CounterType CounterTypeForIterationAttempts
		{
			get
			{
				return CounterType.NewOrganizationIterationAttempts;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x00042FCE File Offset: 0x000411CE
		protected override CounterType CounterTypeForIterationSuccesses
		{
			get
			{
				return CounterType.NewOrganizationIterationSuccesses;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x00042FD2 File Offset: 0x000411D2
		protected override string TenantNameForMonitoringCounters
		{
			get
			{
				return (string)base.CurrentTaskContext.InvocationInfo.Fields["TenantName"];
			}
		}
	}
}
