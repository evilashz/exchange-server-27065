using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Monitoring.Reporting
{
	// Token: 0x020005F0 RID: 1520
	public struct StartEndDateTimePair
	{
		// Token: 0x0600362F RID: 13871 RVA: 0x000DF36C File Offset: 0x000DD56C
		public StartEndDateTimePair(ExDateTime startDate, ExDateTime endDate)
		{
			this.startDate = startDate;
			this.endDate = endDate;
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06003630 RID: 13872 RVA: 0x000DF37C File Offset: 0x000DD57C
		public ExDateTime StartDate
		{
			get
			{
				return this.startDate;
			}
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06003631 RID: 13873 RVA: 0x000DF384 File Offset: 0x000DD584
		public ExDateTime EndDate
		{
			get
			{
				return this.endDate;
			}
		}

		// Token: 0x04002504 RID: 9476
		private ExDateTime startDate;

		// Token: 0x04002505 RID: 9477
		private ExDateTime endDate;
	}
}
