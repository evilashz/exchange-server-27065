using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x02000206 RID: 518
	internal class SpamRuleBlobSchema
	{
		// Token: 0x04000AD8 RID: 2776
		public static HygienePropertyDefinition IdProperty = new HygienePropertyDefinition("id_RuleId", typeof(Guid));

		// Token: 0x04000AD9 RID: 2777
		public static HygienePropertyDefinition RuleIdProperty = new HygienePropertyDefinition("bi_RuleId", typeof(long), 0L, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000ADA RID: 2778
		public static HygienePropertyDefinition GroupIdProperty = new HygienePropertyDefinition("bi_GroupId", typeof(long), 0L, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000ADB RID: 2779
		public static HygienePropertyDefinition ScopeIdProperty = new HygienePropertyDefinition("ti_ScopeId", typeof(byte));

		// Token: 0x04000ADC RID: 2780
		public static HygienePropertyDefinition PriorityProperty = new HygienePropertyDefinition("ti_Priority", typeof(byte));

		// Token: 0x04000ADD RID: 2781
		public static HygienePropertyDefinition PublishingStateProperty = new HygienePropertyDefinition("ti_PublishingState", typeof(byte));

		// Token: 0x04000ADE RID: 2782
		public static HygienePropertyDefinition RuleDataProperty = new HygienePropertyDefinition("nvc_RuleData", typeof(string));

		// Token: 0x04000ADF RID: 2783
		public static HygienePropertyDefinition RuleMetaDataProperty = new HygienePropertyDefinition("nvc_RuleMetaData", typeof(string));

		// Token: 0x04000AE0 RID: 2784
		public static HygienePropertyDefinition ProcessorDataProperty = new HygienePropertyDefinition("nvc_ProcessorData", typeof(string));

		// Token: 0x04000AE1 RID: 2785
		public static HygienePropertyDefinition CreatedDatetimeProperty = new HygienePropertyDefinition("dt_CreatedDatetime", typeof(DateTime?));

		// Token: 0x04000AE2 RID: 2786
		public static HygienePropertyDefinition ChangedDatetimeProperty = new HygienePropertyDefinition("dt_ChangedDatetime", typeof(DateTime?));

		// Token: 0x04000AE3 RID: 2787
		public static HygienePropertyDefinition DeletedDatetimeProperty = new HygienePropertyDefinition("dt_DeletedDatetime", typeof(DateTime?));
	}
}
