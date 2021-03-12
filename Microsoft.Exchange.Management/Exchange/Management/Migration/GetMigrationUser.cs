using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004DE RID: 1246
	[Cmdlet("Get", "MigrationUser", DefaultParameterSetName = "Identity")]
	public sealed class GetMigrationUser : MigrationGetTaskBase<MigrationUserIdParameter, MigrationUser>
	{
		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06002B58 RID: 11096 RVA: 0x000AD799 File Offset: 0x000AB999
		// (set) Token: 0x06002B59 RID: 11097 RVA: 0x000AD7A1 File Offset: 0x000AB9A1
		private new SwitchParameter Diagnostic
		{
			get
			{
				return base.Diagnostic;
			}
			set
			{
				base.Diagnostic = value;
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06002B5A RID: 11098 RVA: 0x000AD7AA File Offset: 0x000AB9AA
		// (set) Token: 0x06002B5B RID: 11099 RVA: 0x000AD7B2 File Offset: 0x000AB9B2
		private new string DiagnosticArgument
		{
			get
			{
				return base.DiagnosticArgument;
			}
			set
			{
				base.DiagnosticArgument = value;
			}
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x000AD7BB File Offset: 0x000AB9BB
		public GetMigrationUser()
		{
			base.InternalResultSize = new Unlimited<uint>(1000U);
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06002B5D RID: 11101 RVA: 0x000AD7D3 File Offset: 0x000AB9D3
		public override string Action
		{
			get
			{
				return "GetMigrationUser";
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06002B5E RID: 11102 RVA: 0x000AD7DA File Offset: 0x000AB9DA
		// (set) Token: 0x06002B5F RID: 11103 RVA: 0x000AD7E2 File Offset: 0x000AB9E2
		[Parameter]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return base.InternalResultSize;
			}
			set
			{
				base.InternalResultSize = value;
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06002B60 RID: 11104 RVA: 0x000AD7EB File Offset: 0x000AB9EB
		// (set) Token: 0x06002B61 RID: 11105 RVA: 0x000AD802 File Offset: 0x000ABA02
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "MailboxGuid")]
		public Guid? MailboxGuid
		{
			get
			{
				return (Guid?)base.Fields["MailboxGuid"];
			}
			set
			{
				base.Fields["MailboxGuid"] = value;
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06002B62 RID: 11106 RVA: 0x000AD81A File Offset: 0x000ABA1A
		// (set) Token: 0x06002B63 RID: 11107 RVA: 0x000AD831 File Offset: 0x000ABA31
		[ValidateNotNullOrEmpty]
		[Parameter(ParameterSetName = "StatusAndBatchId")]
		public MigrationBatchIdParameter BatchId
		{
			get
			{
				return (MigrationBatchIdParameter)base.Fields["BatchId"];
			}
			set
			{
				base.Fields["BatchId"] = value;
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06002B64 RID: 11108 RVA: 0x000AD844 File Offset: 0x000ABA44
		// (set) Token: 0x06002B65 RID: 11109 RVA: 0x000AD85B File Offset: 0x000ABA5B
		[ValidateNotNull]
		[Parameter(ParameterSetName = "StatusAndBatchId")]
		public MigrationUserStatus? Status
		{
			get
			{
				return (MigrationUserStatus?)base.Fields["Status"];
			}
			set
			{
				base.Fields["Status"] = value;
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06002B66 RID: 11110 RVA: 0x000AD873 File Offset: 0x000ABA73
		// (set) Token: 0x06002B67 RID: 11111 RVA: 0x000AD88C File Offset: 0x000ABA8C
		[ValidateNotNull]
		[Parameter(ParameterSetName = "StatusAndBatchId")]
		public MigrationUserStatusSummary? StatusSummary
		{
			get
			{
				return (MigrationUserStatusSummary?)base.Fields["StatusSummary"];
			}
			set
			{
				if (!MigrationUser.MapFromSummaryToStatus.ContainsKey(value.Value))
				{
					throw new ArgumentOutOfRangeException("StatusSummary", (int)value.Value, Strings.UnknownMigrationUserStatusSummaryValue);
				}
				base.Fields["StatusSummary"] = value;
			}
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x000AD8E4 File Offset: 0x000ABAE4
		protected override IConfigDataProvider CreateSession()
		{
			MigrationLogger.Initialize();
			MigrationLogContext.Current.Source = "Get-MigrationUser";
			MigrationLogContext.Current.Organization = base.CurrentOrganizationId.OrganizationalUnit;
			return MigrationUserDataProvider.CreateDataProvider(this.Action, base.TenantGlobalCatalogSession, this.partitionMailbox, null);
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06002B69 RID: 11113 RVA: 0x000AD934 File Offset: 0x000ABB34
		protected override QueryFilter InternalFilter
		{
			get
			{
				if (base.ParameterSetName.Equals("MailboxGuid"))
				{
					if (this.MailboxGuid == null || this.MailboxGuid.Value == Guid.Empty)
					{
						return null;
					}
					return new ComparisonFilter(ComparisonOperator.Equal, MigrationUserSchema.MailboxGuid, this.MailboxGuid.Value);
				}
				else
				{
					if (base.ParameterSetName.Equals("StatusAndBatchId"))
					{
						QueryFilter queryFilter = null;
						if (this.Status != null)
						{
							queryFilter = new ComparisonFilter(ComparisonOperator.Equal, MigrationUserSchema.Status, this.Status.Value);
						}
						else if (this.StatusSummary != null)
						{
							queryFilter = new ComparisonFilter(ComparisonOperator.Equal, MigrationUserSchema.StatusSummary, this.StatusSummary.Value);
						}
						if (this.BatchId != null)
						{
							queryFilter = QueryFilter.AndTogether(new QueryFilter[]
							{
								queryFilter,
								new ComparisonFilter(ComparisonOperator.Equal, MigrationUserSchema.BatchId, this.BatchId.MigrationBatchId)
							});
						}
						return queryFilter;
					}
					return null;
				}
			}
		}

		// Token: 0x04002014 RID: 8212
		private const string ParameterMailboxGuid = "MailboxGuid";

		// Token: 0x04002015 RID: 8213
		private const string ParameterBatchId = "BatchId";

		// Token: 0x04002016 RID: 8214
		private const string ParameterStatus = "Status";

		// Token: 0x04002017 RID: 8215
		private const string ParameterStatusSummary = "StatusSummary";

		// Token: 0x04002018 RID: 8216
		private const string ParameterSetStatusAndBatchId = "StatusAndBatchId";
	}
}
