using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Configuration.TenantMonitoring;

namespace Microsoft.Exchange.Management.ManagementEndpoint
{
	// Token: 0x0200047A RID: 1146
	internal sealed class GetManagementEndpointHealthCountersModule : CmdletHealthCountersModule
	{
		// Token: 0x06002852 RID: 10322 RVA: 0x0009EA7B File Offset: 0x0009CC7B
		public GetManagementEndpointHealthCountersModule(TaskContext context) : base(context)
		{
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06002853 RID: 10323 RVA: 0x0009EA84 File Offset: 0x0009CC84
		protected override CounterType CounterTypeForAttempts
		{
			get
			{
				return CounterType.GetManagementEndpointAttempts;
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06002854 RID: 10324 RVA: 0x0009EA88 File Offset: 0x0009CC88
		protected override CounterType CounterTypeForSuccesses
		{
			get
			{
				return CounterType.GetManagementEndpointSuccesses;
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x0009EA8C File Offset: 0x0009CC8C
		protected override CounterType CounterTypeForIterationAttempts
		{
			get
			{
				return CounterType.GetManagementEndpointIterationAttempts;
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06002856 RID: 10326 RVA: 0x0009EA90 File Offset: 0x0009CC90
		protected override CounterType CounterTypeForIterationSuccesses
		{
			get
			{
				return CounterType.GetManagementEndpointIterationSuccesses;
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06002857 RID: 10327 RVA: 0x0009EA94 File Offset: 0x0009CC94
		protected override string TenantNameForMonitoringCounters
		{
			get
			{
				return base.CurrentTaskContext.InvocationInfo.Fields["DomainName"].ToString();
			}
		}
	}
}
