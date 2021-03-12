using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Logging.MailboxStatistics
{
	// Token: 0x020000AD RID: 173
	internal class MailboxStatisticsLogEntry : ConfigurableObject
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x0000F97D File Offset: 0x0000DB7D
		public MailboxStatisticsLogEntry() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0000F98A File Offset: 0x0000DB8A
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<MailboxStatisticsLogEntrySchema>();
			}
		}
	}
}
