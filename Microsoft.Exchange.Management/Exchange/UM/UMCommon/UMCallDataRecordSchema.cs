using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000D5B RID: 3419
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UMCallDataRecordSchema : UMCallReportBaseSchema
	{
		// Token: 0x04003FA2 RID: 16290
		public static SimpleProviderPropertyDefinition Date = new SimpleProviderPropertyDefinition("Date", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.PersistDefaultValue, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003FA3 RID: 16291
		public static SimpleProviderPropertyDefinition Duration = UMCallReportBaseSchema.CreatePropertyDefinition("Duration", typeof(TimeSpan), TimeSpan.MinValue);

		// Token: 0x04003FA4 RID: 16292
		public static SimpleProviderPropertyDefinition AudioCodec = UMCallReportBaseSchema.CreatePropertyDefinition("AudioCodec", typeof(string), string.Empty);

		// Token: 0x04003FA5 RID: 16293
		public static SimpleProviderPropertyDefinition DialPlan = UMCallReportBaseSchema.CreatePropertyDefinition("DialPlan", typeof(string), string.Empty);

		// Token: 0x04003FA6 RID: 16294
		public static SimpleProviderPropertyDefinition CallType = UMCallReportBaseSchema.CreatePropertyDefinition("CallType", typeof(string), string.Empty);

		// Token: 0x04003FA7 RID: 16295
		public static SimpleProviderPropertyDefinition CallingNumber = UMCallReportBaseSchema.CreatePropertyDefinition("CallingNumber", typeof(string), string.Empty);

		// Token: 0x04003FA8 RID: 16296
		public static SimpleProviderPropertyDefinition CalledNumber = UMCallReportBaseSchema.CreatePropertyDefinition("CalledNumber", typeof(string), string.Empty);

		// Token: 0x04003FA9 RID: 16297
		public static SimpleProviderPropertyDefinition Gateway = UMCallReportBaseSchema.CreatePropertyDefinition("Gateway", typeof(string), string.Empty);

		// Token: 0x04003FAA RID: 16298
		public static SimpleProviderPropertyDefinition UserMailboxName = UMCallReportBaseSchema.CreatePropertyDefinition("UserMailboxName", typeof(string), string.Empty);
	}
}
