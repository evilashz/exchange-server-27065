using System;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x0200020B RID: 523
	internal class SpamRuleProcessorBlobSchema
	{
		// Token: 0x04000AEC RID: 2796
		public static HygienePropertyDefinition IdProperty = new HygienePropertyDefinition("id_ProcessorId", typeof(Guid));

		// Token: 0x04000AED RID: 2797
		public static HygienePropertyDefinition ProcessorIdProperty = new HygienePropertyDefinition("nvc_ProcessorId", typeof(string));

		// Token: 0x04000AEE RID: 2798
		public static HygienePropertyDefinition DataProperty = new HygienePropertyDefinition("nvc_Data", typeof(string));

		// Token: 0x04000AEF RID: 2799
		public static HygienePropertyDefinition CreatedDatetimeProperty = new HygienePropertyDefinition("dt_CreatedDatetime", typeof(DateTime?));

		// Token: 0x04000AF0 RID: 2800
		public static HygienePropertyDefinition ChangedDatetimeProperty = new HygienePropertyDefinition("dt_ChangedDatetime", typeof(DateTime?));

		// Token: 0x04000AF1 RID: 2801
		public static HygienePropertyDefinition DeletedDatetimeProperty = new HygienePropertyDefinition("dt_DeletedDatetime", typeof(DateTime?));
	}
}
