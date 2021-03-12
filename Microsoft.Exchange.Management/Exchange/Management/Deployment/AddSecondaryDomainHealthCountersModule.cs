using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Configuration.TenantMonitoring;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200019C RID: 412
	internal sealed class AddSecondaryDomainHealthCountersModule : CmdletHealthCountersModule
	{
		// Token: 0x06000F00 RID: 3840 RVA: 0x00042F3C File Offset: 0x0004113C
		public AddSecondaryDomainHealthCountersModule(TaskContext context) : base(context)
		{
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00042F45 File Offset: 0x00041145
		protected override CounterType CounterTypeForAttempts
		{
			get
			{
				return CounterType.AddSecondaryDomainAttempts;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x00042F49 File Offset: 0x00041149
		protected override CounterType CounterTypeForSuccesses
		{
			get
			{
				return CounterType.AddSecondaryDomainSuccesses;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00042F4D File Offset: 0x0004114D
		protected override CounterType CounterTypeForIterationAttempts
		{
			get
			{
				return CounterType.AddSecondaryDomainIterationAttempts;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x00042F51 File Offset: 0x00041151
		protected override CounterType CounterTypeForIterationSuccesses
		{
			get
			{
				return CounterType.AddSecondaryDomainIterationSuccesses;
			}
		}
	}
}
