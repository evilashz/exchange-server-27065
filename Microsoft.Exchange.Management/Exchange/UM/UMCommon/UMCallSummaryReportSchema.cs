using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000D5D RID: 3421
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UMCallSummaryReportSchema : UMCallReportBaseSchema
	{
		// Token: 0x06008339 RID: 33593 RVA: 0x002180CC File Offset: 0x002162CC
		private static SimpleProviderPropertyDefinition CreateUlongPropertyDefinition(string propertyName)
		{
			return new SimpleProviderPropertyDefinition(propertyName, ExchangeObjectVersion.Exchange2010, typeof(ulong), PropertyDefinitionFlags.PersistDefaultValue, 0UL, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x04003FAC RID: 16300
		public static SimpleProviderPropertyDefinition AutoAttendant = UMCallSummaryReportSchema.CreateUlongPropertyDefinition("AutoAttendant");

		// Token: 0x04003FAD RID: 16301
		public static SimpleProviderPropertyDefinition FailedOrRejectedCalls = UMCallSummaryReportSchema.CreateUlongPropertyDefinition("FailedOrRejectedCalls");

		// Token: 0x04003FAE RID: 16302
		public static SimpleProviderPropertyDefinition Fax = UMCallSummaryReportSchema.CreateUlongPropertyDefinition("Fax");

		// Token: 0x04003FAF RID: 16303
		public static SimpleProviderPropertyDefinition MissedCalls = UMCallSummaryReportSchema.CreateUlongPropertyDefinition("MissedCalls");

		// Token: 0x04003FB0 RID: 16304
		public static SimpleProviderPropertyDefinition OtherCalls = UMCallSummaryReportSchema.CreateUlongPropertyDefinition("OtherCalls");

		// Token: 0x04003FB1 RID: 16305
		public static SimpleProviderPropertyDefinition Outbound = UMCallSummaryReportSchema.CreateUlongPropertyDefinition("Outbound");

		// Token: 0x04003FB2 RID: 16306
		public static SimpleProviderPropertyDefinition SubscriberAccess = UMCallSummaryReportSchema.CreateUlongPropertyDefinition("SubscriberAccess");

		// Token: 0x04003FB3 RID: 16307
		public static SimpleProviderPropertyDefinition VoiceMessages = UMCallSummaryReportSchema.CreateUlongPropertyDefinition("VoiceMessages");

		// Token: 0x04003FB4 RID: 16308
		public static SimpleProviderPropertyDefinition TotalCalls = UMCallSummaryReportSchema.CreateUlongPropertyDefinition("TotalCalls");

		// Token: 0x04003FB5 RID: 16309
		public static SimpleProviderPropertyDefinition TotalAudioQualityCallsSampled = UMCallSummaryReportSchema.CreateUlongPropertyDefinition("TotalAudioQualityCallsSampled");

		// Token: 0x04003FB6 RID: 16310
		public static SimpleProviderPropertyDefinition Date = UMCallReportBaseSchema.CreatePropertyDefinition("Date", typeof(string), string.Empty);

		// Token: 0x04003FB7 RID: 16311
		public static SimpleProviderPropertyDefinition UMDialPlanName = UMCallReportBaseSchema.CreatePropertyDefinition("UMDialPlanName", typeof(string), string.Empty);

		// Token: 0x04003FB8 RID: 16312
		public static SimpleProviderPropertyDefinition UMIPGatewayName = UMCallReportBaseSchema.CreatePropertyDefinition("UMIPGatewayName", typeof(string), string.Empty);
	}
}
