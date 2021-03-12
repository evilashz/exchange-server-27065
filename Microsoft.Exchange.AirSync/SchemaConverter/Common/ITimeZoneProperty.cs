using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x0200012C RID: 300
	internal interface ITimeZoneProperty : IProperty
	{
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06000F59 RID: 3929
		ExTimeZone TimeZone { get; }

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06000F5A RID: 3930
		ExDateTime EffectiveTime { get; }
	}
}
