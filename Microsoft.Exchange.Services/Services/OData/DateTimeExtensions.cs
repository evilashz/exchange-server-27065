using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000DF2 RID: 3570
	internal static class DateTimeExtensions
	{
		// Token: 0x06005C87 RID: 23687 RVA: 0x001206ED File Offset: 0x0011E8ED
		internal static ExDateTime ToExDateTime(this DateTimeOffset dateTimeOffset)
		{
			return (ExDateTime)dateTimeOffset.UtcDateTime;
		}

		// Token: 0x06005C88 RID: 23688 RVA: 0x001206FB File Offset: 0x0011E8FB
		internal static DateTimeOffset ToDateTimeOffset(this ExDateTime exDateTime)
		{
			return new DateTimeOffset((DateTime)exDateTime, exDateTime.Bias);
		}
	}
}
