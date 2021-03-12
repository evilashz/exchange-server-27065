using System;
using System.Data.SqlTypes;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x02000237 RID: 567
	internal class ReportScheduleSchema
	{
		// Token: 0x04000B8C RID: 2956
		public static ProviderPropertyDefinition Id = ADObjectSchema.Id;

		// Token: 0x04000B8D RID: 2957
		public static ProviderPropertyDefinition TenantId = new HygienePropertyDefinition("OrganizationalUnitRoot", typeof(Guid));

		// Token: 0x04000B8E RID: 2958
		public static HygienePropertyDefinition Enabled = new HygienePropertyDefinition("Enabled", typeof(bool), true, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000B8F RID: 2959
		public static ProviderPropertyDefinition ScheduleName = ADObjectSchema.RawName;

		// Token: 0x04000B90 RID: 2960
		public static HygienePropertyDefinition ScheduleFrequency = new HygienePropertyDefinition("ScheduleFrequency", typeof(ReportScheduleFrequencyType), ReportScheduleFrequencyType.Daily, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000B91 RID: 2961
		public static HygienePropertyDefinition ScheduleMask = new HygienePropertyDefinition("ScheduleMask", typeof(byte));

		// Token: 0x04000B92 RID: 2962
		public static HygienePropertyDefinition ScheduleStartTime = new HygienePropertyDefinition("ScheduleStartTime", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000B93 RID: 2963
		public static ProviderPropertyDefinition ReportName = new HygienePropertyDefinition("ReportName", typeof(string));

		// Token: 0x04000B94 RID: 2964
		public static HygienePropertyDefinition ReportFormat = new HygienePropertyDefinition("ReportFormat", typeof(ReportFormatType), ReportFormatType.CSV, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000B95 RID: 2965
		public static ProviderPropertyDefinition ReportSubject = new HygienePropertyDefinition("ReportSubject", typeof(string));

		// Token: 0x04000B96 RID: 2966
		public static ProviderPropertyDefinition ReportRecipients = new HygienePropertyDefinition("ReportRecipients", typeof(string));

		// Token: 0x04000B97 RID: 2967
		public static HygienePropertyDefinition ReportFilter = new HygienePropertyDefinition("ReportFilter", typeof(string));

		// Token: 0x04000B98 RID: 2968
		public static HygienePropertyDefinition ReportLanguage = new HygienePropertyDefinition("ReportLanguage", typeof(string));

		// Token: 0x04000B99 RID: 2969
		public static HygienePropertyDefinition BatchId = new HygienePropertyDefinition("BatchId", typeof(Guid?));

		// Token: 0x04000B9A RID: 2970
		public static HygienePropertyDefinition LastScheduleTime = new HygienePropertyDefinition("LastScheduleTime", typeof(DateTime), DateTime.Today, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000B9B RID: 2971
		public static HygienePropertyDefinition LastExecutionTime = new HygienePropertyDefinition("LastExecutionTime", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000B9C RID: 2972
		public static HygienePropertyDefinition LastExecutionStatus = new HygienePropertyDefinition("LastExecutionStatus", typeof(ReportExecutionStatusType), ReportExecutionStatusType.None, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000B9D RID: 2973
		public static HygienePropertyDefinition LastExecutionContextId = new HygienePropertyDefinition("LastExecutionContextId", typeof(Guid));

		// Token: 0x04000B9E RID: 2974
		public static HygienePropertyDefinition CurrentExecutionStatus = new HygienePropertyDefinition("CurrentExecutionStatus", typeof(ReportExecutionStatusType), ReportExecutionStatusType.None, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000B9F RID: 2975
		public static HygienePropertyDefinition CurrentExecutionContextId = new HygienePropertyDefinition("CurrentExecutionContextId", typeof(Guid));

		// Token: 0x04000BA0 RID: 2976
		public static HygienePropertyDefinition ScheduleDateTimeFilterQueryProp = new HygienePropertyDefinition("ScheduleDateTime", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
