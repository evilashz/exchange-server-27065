using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000D5A RID: 3418
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class UMCallReportBaseSchema : SimpleProviderObjectSchema
	{
		// Token: 0x06008317 RID: 33559 RVA: 0x00217CCF File Offset: 0x00215ECF
		protected static SimpleProviderPropertyDefinition CreatePropertyDefinition(string propertyName, Type propertyType, object defaultValue)
		{
			return new SimpleProviderPropertyDefinition(propertyName, ExchangeObjectVersion.Exchange2010, propertyType, PropertyDefinitionFlags.None, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x04003F9C RID: 16284
		public static SimpleProviderPropertyDefinition NMOS = UMCallReportBaseSchema.CreatePropertyDefinition("NMOS", typeof(float?), null);

		// Token: 0x04003F9D RID: 16285
		public static SimpleProviderPropertyDefinition NMOSDegradation = UMCallReportBaseSchema.CreatePropertyDefinition("NMOSDegradation", typeof(float?), null);

		// Token: 0x04003F9E RID: 16286
		public static SimpleProviderPropertyDefinition PercentPacketLoss = UMCallReportBaseSchema.CreatePropertyDefinition("PercentPacketLoss", typeof(float?), null);

		// Token: 0x04003F9F RID: 16287
		public static SimpleProviderPropertyDefinition Jitter = UMCallReportBaseSchema.CreatePropertyDefinition("Jitter", typeof(float?), null);

		// Token: 0x04003FA0 RID: 16288
		public static SimpleProviderPropertyDefinition RoundTripMilliseconds = UMCallReportBaseSchema.CreatePropertyDefinition("RoundTripMilliseconds", typeof(float?), null);

		// Token: 0x04003FA1 RID: 16289
		public static SimpleProviderPropertyDefinition BurstLossDurationMilliseconds = UMCallReportBaseSchema.CreatePropertyDefinition("BurstLossDurationMilliseconds", typeof(float?), null);
	}
}
