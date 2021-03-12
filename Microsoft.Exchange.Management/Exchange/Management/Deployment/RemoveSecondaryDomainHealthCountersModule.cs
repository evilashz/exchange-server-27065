using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Configuration.TenantMonitoring;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200019D RID: 413
	internal sealed class RemoveSecondaryDomainHealthCountersModule : CmdletHealthCountersModule
	{
		// Token: 0x06000F05 RID: 3845 RVA: 0x00042F55 File Offset: 0x00041155
		public RemoveSecondaryDomainHealthCountersModule(TaskContext context) : base(context)
		{
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00042F5E File Offset: 0x0004115E
		protected override CounterType CounterTypeForAttempts
		{
			get
			{
				return CounterType.RemoveSecondaryDomainAttempts;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x00042F62 File Offset: 0x00041162
		protected override CounterType CounterTypeForSuccesses
		{
			get
			{
				return CounterType.RemoveSecondaryDomainSuccesses;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x00042F66 File Offset: 0x00041166
		protected override CounterType CounterTypeForIterationAttempts
		{
			get
			{
				return CounterType.RemoveSecondaryDomainIterationAttempts;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x00042F6A File Offset: 0x0004116A
		protected override CounterType CounterTypeForIterationSuccesses
		{
			get
			{
				return CounterType.RemoveSecondaryDomainIterationSuccesses;
			}
		}
	}
}
