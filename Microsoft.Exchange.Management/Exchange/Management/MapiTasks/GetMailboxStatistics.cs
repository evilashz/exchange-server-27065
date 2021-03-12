using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Management.MapiTasks.Presentation;

namespace Microsoft.Exchange.Management.MapiTasks
{
	// Token: 0x02000485 RID: 1157
	[Cmdlet("Get", "MailboxStatistics", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxStatistics : GetStatisticsBase<GeneralMailboxOrMailUserIdParameter, Microsoft.Exchange.Data.Mapi.MailboxStatistics, Microsoft.Exchange.Management.MapiTasks.Presentation.MailboxStatistics>
	{
		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x060028E3 RID: 10467 RVA: 0x000A1DFF File Offset: 0x0009FFFF
		// (set) Token: 0x060028E4 RID: 10468 RVA: 0x000A1E25 File Offset: 0x000A0025
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeMoveHistory
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeMoveHistory"] ?? false);
			}
			set
			{
				base.Fields["IncludeMoveHistory"] = value;
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x060028E5 RID: 10469 RVA: 0x000A1E3D File Offset: 0x000A003D
		// (set) Token: 0x060028E6 RID: 10470 RVA: 0x000A1E63 File Offset: 0x000A0063
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeMoveReport
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeMoveReport"] ?? false);
			}
			set
			{
				base.Fields["IncludeMoveReport"] = value;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x060028E7 RID: 10471 RVA: 0x000A1E7B File Offset: 0x000A007B
		// (set) Token: 0x060028E8 RID: 10472 RVA: 0x000A1EA1 File Offset: 0x000A00A1
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter Archive
		{
			get
			{
				return (SwitchParameter)(base.Fields["Archive"] ?? false);
			}
			set
			{
				base.Fields["Archive"] = value;
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x060028E9 RID: 10473 RVA: 0x000A1EB9 File Offset: 0x000A00B9
		// (set) Token: 0x060028EA RID: 10474 RVA: 0x000A1EDF File Offset: 0x000A00DF
		[Parameter(Mandatory = false, ParameterSetName = "AuditLog")]
		public SwitchParameter AuditLog
		{
			get
			{
				return (SwitchParameter)(base.Fields["AuditLog"] ?? false);
			}
			set
			{
				base.Fields["AuditLog"] = value;
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x060028EB RID: 10475 RVA: 0x000A1EF7 File Offset: 0x000A00F7
		// (set) Token: 0x060028EC RID: 10476 RVA: 0x000A1F0E File Offset: 0x000A010E
		[Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = "Database")]
		public StoreMailboxIdParameter StoreMailboxIdentity
		{
			get
			{
				return (StoreMailboxIdParameter)base.Fields["StoreMailboxIdentity"];
			}
			set
			{
				base.Fields["StoreMailboxIdentity"] = value;
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x060028ED RID: 10477 RVA: 0x000A1F21 File Offset: 0x000A0121
		// (set) Token: 0x060028EE RID: 10478 RVA: 0x000A1F47 File Offset: 0x000A0147
		[Parameter(Mandatory = false)]
		public SwitchParameter NoADLookup
		{
			get
			{
				return (SwitchParameter)(base.Fields["NoADLookup"] ?? false);
			}
			set
			{
				base.Fields["NoADLookup"] = value;
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x060028EF RID: 10479 RVA: 0x000A1F5F File Offset: 0x000A015F
		// (set) Token: 0x060028F0 RID: 10480 RVA: 0x000A1F85 File Offset: 0x000A0185
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeQuarantineDetails
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeQuarantineDetails"] ?? false);
			}
			set
			{
				base.Fields["IncludeQuarantineDetails"] = value;
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x060028F1 RID: 10481 RVA: 0x000A1F9D File Offset: 0x000A019D
		// (set) Token: 0x060028F2 RID: 10482 RVA: 0x000A1FB4 File Offset: 0x000A01B4
		[Parameter(Mandatory = false, ParameterSetName = "Server")]
		[Parameter(Mandatory = false, ParameterSetName = "Database")]
		public string Filter
		{
			get
			{
				return (string)base.Fields["Filter"];
			}
			set
			{
				base.Fields["Filter"] = value;
			}
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x000A1FC7 File Offset: 0x000A01C7
		internal override MoveHistoryOption GetMoveHistoryOption()
		{
			if (this.IncludeMoveReport)
			{
				return MoveHistoryOption.IncludeMoveHistoryAndReport;
			}
			if (this.IncludeMoveHistory)
			{
				return MoveHistoryOption.IncludeMoveHistory;
			}
			return MoveHistoryOption.None;
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000A1FE8 File Offset: 0x000A01E8
		internal override bool GetArchiveMailboxStatistics()
		{
			return this.Archive;
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x000A1FFA File Offset: 0x000A01FA
		internal override bool GetAuditLogMailboxStatistics()
		{
			return this.AuditLog;
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x060028F6 RID: 10486 RVA: 0x000A2008 File Offset: 0x000A0208
		protected override QueryFilter InternalFilter
		{
			get
			{
				if (this.Filter != null)
				{
					QueryParser.ConvertValueFromStringDelegate convertDelegate = new QueryParser.ConvertValueFromStringDelegate(MonadFilter.ConvertValueFromString);
					QueryParser queryParser = new QueryParser(this.Filter, ObjectSchema.GetInstance<MailboxStatisticsSchema>(), QueryParser.Capabilities.All, new QueryParser.EvaluateVariableDelegate(base.GetVariableValue), convertDelegate);
					return queryParser.ParseTree;
				}
				return null;
			}
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x000A2058 File Offset: 0x000A0258
		protected override void WriteResult(IConfigurable dataObject)
		{
			Microsoft.Exchange.Management.MapiTasks.Presentation.MailboxStatistics mailboxStatistics = (Microsoft.Exchange.Management.MapiTasks.Presentation.MailboxStatistics)((MapiObject)dataObject);
			mailboxStatistics.IncludeQuarantineDetails = this.IncludeQuarantineDetails;
			base.WriteResult(dataObject);
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x060028F8 RID: 10488 RVA: 0x000A2089 File Offset: 0x000A0289
		protected override StoreMailboxIdParameter StoreMailboxId
		{
			get
			{
				return this.StoreMailboxIdentity;
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x060028F9 RID: 10489 RVA: 0x000A2091 File Offset: 0x000A0291
		protected override bool NoADLookupForMailboxStatistics
		{
			get
			{
				return this.NoADLookup;
			}
		}

		// Token: 0x04001E32 RID: 7730
		private const string ParameterIncludeMoveHistory = "IncludeMoveHistory";

		// Token: 0x04001E33 RID: 7731
		private const string ParameterIncludeMoveReport = "IncludeMoveReport";

		// Token: 0x04001E34 RID: 7732
		private const string ParameterFilter = "Filter";

		// Token: 0x04001E35 RID: 7733
		private const string ParameterStoreMailboxIdentity = "StoreMailboxIdentity";

		// Token: 0x04001E36 RID: 7734
		private const string ParameterNoAdLookup = "NoADLookup";

		// Token: 0x04001E37 RID: 7735
		private const string ParameterIncludeQuarantineDetails = "IncludeQuarantineDetails";
	}
}
