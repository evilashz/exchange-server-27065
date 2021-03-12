using System;
using System.Data.SqlTypes;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000EE RID: 238
	internal class OnDemandQueryRequestSchema
	{
		// Token: 0x040004D4 RID: 1236
		public static ProviderPropertyDefinition Id = ADObjectSchema.Id;

		// Token: 0x040004D5 RID: 1237
		public static ProviderPropertyDefinition RequestId = ADObjectSchema.RawName;

		// Token: 0x040004D6 RID: 1238
		public static ProviderPropertyDefinition Container = DalHelper.ContainerProp;

		// Token: 0x040004D7 RID: 1239
		public static HygienePropertyDefinition QueryDefinition = new HygienePropertyDefinition("QueryDefinition", typeof(string));

		// Token: 0x040004D8 RID: 1240
		public static HygienePropertyDefinition RequestStatus = new HygienePropertyDefinition("RequestStatus", typeof(OnDemandQueryRequestStatus), OnDemandQueryRequestStatus.NotStarted, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040004D9 RID: 1241
		public static HygienePropertyDefinition TenantId = new HygienePropertyDefinition("OrganizationalUnitRoot", typeof(Guid));

		// Token: 0x040004DA RID: 1242
		public static HygienePropertyDefinition SubmissionTime = new HygienePropertyDefinition("SubmissionTime", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040004DB RID: 1243
		public static HygienePropertyDefinition BatchId = new HygienePropertyDefinition("ReportBatchId", typeof(Guid?));

		// Token: 0x040004DC RID: 1244
		public static HygienePropertyDefinition Region = new HygienePropertyDefinition("Region", typeof(string));

		// Token: 0x040004DD RID: 1245
		public static HygienePropertyDefinition QuerySubject = new HygienePropertyDefinition("QuerySubject", typeof(string));

		// Token: 0x040004DE RID: 1246
		public static HygienePropertyDefinition QueryType = new HygienePropertyDefinition("QueryType", typeof(OnDemandQueryType), OnDemandQueryType.MTSummary, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040004DF RID: 1247
		public static HygienePropertyDefinition QueryPriority = new HygienePropertyDefinition("QueryPriority", typeof(OnDemandQueryPriority), OnDemandQueryPriority.Normal, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040004E0 RID: 1248
		public static HygienePropertyDefinition CallerType = new HygienePropertyDefinition("CallerType", typeof(OnDemandQueryCallerType), OnDemandQueryCallerType.Customer, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040004E1 RID: 1249
		public static HygienePropertyDefinition ResultSize = new HygienePropertyDefinition("ResultSize", typeof(long), 0L, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040004E2 RID: 1250
		public static HygienePropertyDefinition MatchRowCounts = new HygienePropertyDefinition("MatchRowCounts", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040004E3 RID: 1251
		public static HygienePropertyDefinition ResultRowCounts = new HygienePropertyDefinition("ResultRowCounts", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040004E4 RID: 1252
		public static HygienePropertyDefinition ViewCounts = new HygienePropertyDefinition("ViewCounts", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040004E5 RID: 1253
		public static HygienePropertyDefinition ResultUri = new HygienePropertyDefinition("ResultUri", typeof(string));

		// Token: 0x040004E6 RID: 1254
		public static HygienePropertyDefinition CosmosResultUri = new HygienePropertyDefinition("CosmosResultUri", typeof(string));

		// Token: 0x040004E7 RID: 1255
		public static HygienePropertyDefinition CosmosJobId = new HygienePropertyDefinition("CosmosJobId", typeof(Guid?));

		// Token: 0x040004E8 RID: 1256
		public static HygienePropertyDefinition InBatchQueryId = new HygienePropertyDefinition("InBatchQueryId", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040004E9 RID: 1257
		public static HygienePropertyDefinition NotificationEmail = new HygienePropertyDefinition("NotificationEmail", typeof(string));

		// Token: 0x040004EA RID: 1258
		public static HygienePropertyDefinition RetryCount = new HygienePropertyDefinition("RetryCount", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040004EB RID: 1259
		public static HygienePropertyDefinition ResultLocale = new HygienePropertyDefinition("ResultLocale", typeof(string));
	}
}
