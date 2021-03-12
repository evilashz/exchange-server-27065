using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000030 RID: 48
	internal class JobPickupResultsCsvSchema : BaseMigMonCsvSchema
	{
		// Token: 0x06000190 RID: 400 RVA: 0x000087FF File Offset: 0x000069FF
		public JobPickupResultsCsvSchema() : base(BaseMigMonCsvSchema.GetRequiredColumns(JobPickupResultsCsvSchema.requiredColumnsIds, JobPickupResultsCsvSchema.requiredColumnsAsIs, "Time"), BaseMigMonCsvSchema.GetOptionalColumns(JobPickupResultsCsvSchema.optionalColumnsIds, JobPickupResultsCsvSchema.optionalColumnsAsIs), null)
		{
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000882C File Offset: 0x00006A2C
		public override DataTable GetCsvSchemaDataTable()
		{
			return base.GetCsvSchemaDataTable();
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008841 File Offset: 0x00006A41
		public override List<ColumnDefinition<int>> GetRequiredColumnsIds()
		{
			return JobPickupResultsCsvSchema.requiredColumnsIds;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00008848 File Offset: 0x00006A48
		public override List<IColumnDefinition> GetRequiredColumnsAsIs()
		{
			return JobPickupResultsCsvSchema.requiredColumnsAsIs;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000884F File Offset: 0x00006A4F
		public override List<ColumnDefinition<int>> GetOptionalColumnsIds()
		{
			return JobPickupResultsCsvSchema.optionalColumnsIds;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00008856 File Offset: 0x00006A56
		public override List<IColumnDefinition> GetOptionalColumnsAsIs()
		{
			return JobPickupResultsCsvSchema.optionalColumnsAsIs;
		}

		// Token: 0x0400012A RID: 298
		public const string RequestGuidColumnName = "RequestGuid";

		// Token: 0x0400012B RID: 299
		public const string JobTimestampColumnName = "TimeStamp";

		// Token: 0x0400012C RID: 300
		public const string RequestTypeColumnName = "RequestType";

		// Token: 0x0400012D RID: 301
		public const string RequestStatusColumnName = "RequestStatus";

		// Token: 0x0400012E RID: 302
		public const string WorkloadTypeColumnName = "WorkloadType";

		// Token: 0x0400012F RID: 303
		public const string PriorityColumnName = "Priority";

		// Token: 0x04000130 RID: 304
		public const string LastUpdateTimestampColumnName = "LastUpdateTimeStamp";

		// Token: 0x04000131 RID: 305
		public const string PickupResultsColumnName = "PickupResult";

		// Token: 0x04000132 RID: 306
		public const string NextScanTimestampColumnName = "NextRecommendedPickup";

		// Token: 0x04000133 RID: 307
		public const string MessageColumnName = "Message";

		// Token: 0x04000134 RID: 308
		public const string ReservationFailureReasonColumnName = "ReservationFailureReason";

		// Token: 0x04000135 RID: 309
		public const string ReservationFailureResourceTypeColumnName = "ReservationFailureResourceType";

		// Token: 0x04000136 RID: 310
		public const string ReservationFailureWLMResourceTypeColumnName = "ReservationFailureWLMResourceType";

		// Token: 0x04000137 RID: 311
		private static readonly List<ColumnDefinition<int>> requiredColumnsIds = new List<ColumnDefinition<int>>
		{
			new ColumnDefinition<int>("RequestType", "RequestTypeID", KnownStringType.RequestType),
			new ColumnDefinition<int>("RequestStatus", "RequestStatusID", KnownStringType.RequestStatus),
			new ColumnDefinition<int>("WorkloadType", "WorkloadTypeID", KnownStringType.RequestWorkloadType),
			new ColumnDefinition<int>("PickupResult", "PickupResultID", KnownStringType.PickupResultsType)
		};

		// Token: 0x04000138 RID: 312
		private static readonly List<IColumnDefinition> requiredColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<Guid>("RequestGuid"),
			new ColumnDefinition<SqlDateTime>("TimeStamp"),
			new ColumnDefinition<int>("Priority"),
			new ColumnDefinition<SqlDateTime>("LastUpdateTimeStamp"),
			new ColumnDefinition<SqlDateTime>("NextRecommendedPickup")
		};

		// Token: 0x04000139 RID: 313
		private static readonly List<ColumnDefinition<int>> optionalColumnsIds = new List<ColumnDefinition<int>>
		{
			new ColumnDefinition<int>("ReservationFailureReason", "ReservationFailureReasonID", KnownStringType.ReservationFailureReasonType),
			new ColumnDefinition<int>("ReservationFailureResourceType", "ReservationFailureResourceTypeID", KnownStringType.ReservationFailureResourceTypeType),
			new ColumnDefinition<int>("ReservationFailureWLMResourceType", "ReservationFailureWLMResourceTypeID", KnownStringType.ReservationFailureWLMResourceTypeType)
		};

		// Token: 0x0400013A RID: 314
		private static readonly List<IColumnDefinition> optionalColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<int>("Message")
		};
	}
}
