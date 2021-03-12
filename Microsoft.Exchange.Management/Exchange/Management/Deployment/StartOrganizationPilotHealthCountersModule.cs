using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Configuration.TenantMonitoring;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200019E RID: 414
	internal sealed class StartOrganizationPilotHealthCountersModule : CmdletHealthCountersModule
	{
		// Token: 0x06000F0A RID: 3850 RVA: 0x00042F6E File Offset: 0x0004116E
		public StartOrganizationPilotHealthCountersModule(TaskContext context) : base(context)
		{
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x00042F77 File Offset: 0x00041177
		protected override CounterType CounterTypeForAttempts
		{
			get
			{
				return CounterType.StartOrganizationPilotAttempts;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x00042F7B File Offset: 0x0004117B
		protected override CounterType CounterTypeForSuccesses
		{
			get
			{
				return CounterType.StartOrganizationPilotSuccesses;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x00042F7F File Offset: 0x0004117F
		protected override CounterType CounterTypeForIterationAttempts
		{
			get
			{
				return CounterType.StartOrganizationPilotIterationAttempts;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x00042F83 File Offset: 0x00041183
		protected override CounterType CounterTypeForIterationSuccesses
		{
			get
			{
				return CounterType.StartOrganizationPilotIterationSuccesses;
			}
		}
	}
}
