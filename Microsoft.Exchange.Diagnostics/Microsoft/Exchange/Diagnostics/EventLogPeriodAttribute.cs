using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000025 RID: 37
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class EventLogPeriodAttribute : Attribute
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x000041CB File Offset: 0x000023CB
		public EventLogPeriodAttribute()
		{
			this.period = string.Empty;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000041DE File Offset: 0x000023DE
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x000041E6 File Offset: 0x000023E6
		public string Period
		{
			get
			{
				return this.period;
			}
			set
			{
				this.period = value;
			}
		}

		// Token: 0x040000B5 RID: 181
		private string period;
	}
}
