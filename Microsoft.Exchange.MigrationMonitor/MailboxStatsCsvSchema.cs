using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000017 RID: 23
	internal class MailboxStatsCsvSchema : BaseMigMonCsvSchema
	{
		// Token: 0x0600009B RID: 155 RVA: 0x000046C0 File Offset: 0x000028C0
		public MailboxStatsCsvSchema() : base(BaseMigMonCsvSchema.GetRequiredColumns(MailboxStatsCsvSchema.requiredColumnsIds, MailboxStatsCsvSchema.requiredColumnsAsIs, "Time"), BaseMigMonCsvSchema.GetOptionalColumns(MailboxStatsCsvSchema.optionalColumnsIds, MailboxStatsCsvSchema.optionalColumnsAsIs), null)
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000046EC File Offset: 0x000028EC
		public override List<ColumnDefinition<int>> GetRequiredColumnsIds()
		{
			return MailboxStatsCsvSchema.requiredColumnsIds;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000046F3 File Offset: 0x000028F3
		public override List<IColumnDefinition> GetRequiredColumnsAsIs()
		{
			return MailboxStatsCsvSchema.requiredColumnsAsIs;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000046FA File Offset: 0x000028FA
		public override List<ColumnDefinition<int>> GetOptionalColumnsIds()
		{
			return MailboxStatsCsvSchema.optionalColumnsIds;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004701 File Offset: 0x00002901
		public override List<IColumnDefinition> GetOptionalColumnsAsIs()
		{
			return MailboxStatsCsvSchema.optionalColumnsAsIs;
		}

		// Token: 0x04000085 RID: 133
		public const string MailboxGuidColumn = "MailboxGuid";

		// Token: 0x04000086 RID: 134
		private static readonly List<ColumnDefinition<int>> requiredColumnsIds = new List<ColumnDefinition<int>>
		{
			new ColumnDefinition<int>("DatabaseName", "DatabaseId", KnownStringType.DatabaseName),
			new ColumnDefinition<int>("MailboxType", "MailboxTypeId", KnownStringType.MailboxType)
		};

		// Token: 0x04000087 RID: 135
		private static readonly List<IColumnDefinition> requiredColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<Guid>("MailboxGuid"),
			new ColumnDefinition<Guid>("ExternalDirectoryOrganizationId"),
			new ColumnDefinition<long>("ItemCount"),
			new ColumnDefinition<long>("DeletedItemCount"),
			new ColumnDefinition<long>("TotalItemSizeInBytes"),
			new ColumnDefinition<long>("TotalDeletedItemSizeInBytes"),
			new ColumnDefinition<long>("MessageTableTotalSizeInBytes"),
			new ColumnDefinition<long>("AttachmentTableTotalSizeInBytes"),
			new ColumnDefinition<long>("OtherTablesTotalSizeInBytes"),
			new ColumnDefinition<SqlDateTime>("DisconnectDate"),
			new ColumnDefinition<double>("LastLogonTime"),
			new ColumnDefinition<bool>("IsArchiveMailbox"),
			new ColumnDefinition<bool>("IsMoveDestination"),
			new ColumnDefinition<bool>("IsQuarantined")
		};

		// Token: 0x04000088 RID: 136
		private static readonly List<ColumnDefinition<int>> optionalColumnsIds = new List<ColumnDefinition<int>>
		{
			new ColumnDefinition<int>("DisconnectReason", "DisconnectReasonId", KnownStringType.DisconnectReason)
		};

		// Token: 0x04000089 RID: 137
		private static readonly List<IColumnDefinition> optionalColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<double>("LogicalSizeInM"),
			new ColumnDefinition<double>("PhysicalSizeInM")
		};
	}
}
