using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000166 RID: 358
	internal interface IStartDueDateProperty : IProperty
	{
		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001027 RID: 4135
		ExDateTime? DueDate { get; }

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001028 RID: 4136
		ExDateTime? StartDate { get; }

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001029 RID: 4137
		ExDateTime? UtcDueDate { get; }

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x0600102A RID: 4138
		ExDateTime? UtcStartDate { get; }
	}
}
