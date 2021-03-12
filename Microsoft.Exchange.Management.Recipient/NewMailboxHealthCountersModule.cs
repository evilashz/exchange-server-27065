using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Configuration.TenantMonitoring;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000071 RID: 113
	internal sealed class NewMailboxHealthCountersModule : CmdletHealthCountersModule
	{
		// Token: 0x06000852 RID: 2130 RVA: 0x00025550 File Offset: 0x00023750
		public NewMailboxHealthCountersModule(TaskContext context) : base(context)
		{
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x00025559 File Offset: 0x00023759
		protected override CounterType CounterTypeForAttempts
		{
			get
			{
				return CounterType.NewMailboxAttempts;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0002555D File Offset: 0x0002375D
		protected override CounterType CounterTypeForSuccesses
		{
			get
			{
				return CounterType.NewMailboxSuccesses;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00025561 File Offset: 0x00023761
		protected override CounterType CounterTypeForIterationAttempts
		{
			get
			{
				return CounterType.NewMailboxIterationAttempts;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00025565 File Offset: 0x00023765
		protected override CounterType CounterTypeForIterationSuccesses
		{
			get
			{
				return CounterType.NewMailboxIterationSuccesses;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x0002556C File Offset: 0x0002376C
		protected override string TenantNameForMonitoringCounters
		{
			get
			{
				string text = string.Empty;
				base.CurrentTaskContext.TryGetItem<string>("TenantNameForMonitoring", ref text);
				if (string.IsNullOrEmpty(text))
				{
					object obj = base.CurrentTaskContext.InvocationInfo.Fields["Organization"];
					text = ((obj != null) ? obj.ToString() : base.TenantNameForMonitoringCounters);
				}
				return text;
			}
		}
	}
}
