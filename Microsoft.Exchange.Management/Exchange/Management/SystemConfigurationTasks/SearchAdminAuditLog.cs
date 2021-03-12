using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000055 RID: 85
	[Cmdlet("Search", "AdminAuditLog", DefaultParameterSetName = "Identity")]
	public sealed class SearchAdminAuditLog : GetMultitenancySingletonSystemConfigurationObjectTask<AdminAuditLogConfig>
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00009259 File Offset: 0x00007459
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00009270 File Offset: 0x00007470
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public MultiValuedProperty<string> Cmdlets
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["Cmdlets"];
			}
			set
			{
				base.Fields["Cmdlets"] = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00009283 File Offset: 0x00007483
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000929A File Offset: 0x0000749A
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> Parameters
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["Parameters"];
			}
			set
			{
				base.Fields["Parameters"] = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000092AD File Offset: 0x000074AD
		// (set) Token: 0x06000215 RID: 533 RVA: 0x000092C4 File Offset: 0x000074C4
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public ExDateTime? StartDate
		{
			get
			{
				return (ExDateTime?)base.Fields["StartDate"];
			}
			set
			{
				base.Fields["StartDate"] = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000092DC File Offset: 0x000074DC
		// (set) Token: 0x06000217 RID: 535 RVA: 0x000092F3 File Offset: 0x000074F3
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public ExDateTime? EndDate
		{
			get
			{
				return (ExDateTime?)base.Fields["EndDate"];
			}
			set
			{
				base.Fields["EndDate"] = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000930B File Offset: 0x0000750B
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00009322 File Offset: 0x00007522
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool? ExternalAccess
		{
			get
			{
				return (bool?)base.Fields["ExternalAccess"];
			}
			set
			{
				base.Fields["ExternalAccess"] = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000933A File Offset: 0x0000753A
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00009351 File Offset: 0x00007551
		[ValidateRange(1, 250000)]
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public int ResultSize
		{
			get
			{
				return (int)base.Fields["ResultSize"];
			}
			set
			{
				base.Fields["ResultSize"] = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00009369 File Offset: 0x00007569
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00009380 File Offset: 0x00007580
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> ObjectIds
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["ObjectIds"];
			}
			set
			{
				base.Fields["ObjectIds"] = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00009393 File Offset: 0x00007593
		// (set) Token: 0x0600021F RID: 543 RVA: 0x000093AA File Offset: 0x000075AA
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<SecurityPrincipalIdParameter> UserIds
		{
			get
			{
				return (MultiValuedProperty<SecurityPrincipalIdParameter>)base.Fields["UserIds"];
			}
			set
			{
				base.Fields["UserIds"] = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000093BD File Offset: 0x000075BD
		// (set) Token: 0x06000221 RID: 545 RVA: 0x000093D4 File Offset: 0x000075D4
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool? IsSuccess
		{
			get
			{
				return (bool?)base.Fields["IsSuccess"];
			}
			set
			{
				base.Fields["IsSuccess"] = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000093EC File Offset: 0x000075EC
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00009403 File Offset: 0x00007603
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public int StartIndex
		{
			get
			{
				return (int)base.Fields["StartIndex"];
			}
			set
			{
				base.Fields["StartIndex"] = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000941B File Offset: 0x0000761B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmSearchAdminAuditLogConfigTask(base.CurrentOrgContainerId.ToString());
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00009430 File Offset: 0x00007630
		protected override ObjectId RootId
		{
			get
			{
				ADObjectId orgContainerId = base.CurrentOrgContainerId;
				if (base.SharedConfiguration != null)
				{
					orgContainerId = base.SharedConfiguration.SharedConfigurationCU.Id;
				}
				return AdminAuditLogConfig.GetWellKnownParentLocation(orgContainerId);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00009463 File Offset: 0x00007663
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00009468 File Offset: 0x00007668
		protected override void InternalValidate()
		{
			if (this.StartDate == null && this.EndDate == null)
			{
				this.EndDate = new ExDateTime?(ExDateTime.Now);
				this.StartDate = new ExDateTime?(this.EndDate.Value.AddDays(-15.0));
			}
			if (this.StartDate != null && this.EndDate == null)
			{
				this.EndDate = new ExDateTime?(this.StartDate.Value.AddDays(15.0));
			}
			if (this.StartDate == null && this.EndDate != null)
			{
				this.StartDate = new ExDateTime?(this.EndDate.Value.AddDays(-15.0));
			}
			if (this.StartDate.Value > this.EndDate.Value)
			{
				base.WriteError(new ArgumentException(Strings.AdminAuditLogSearchStartDateIsLaterThanEndDate(this.StartDate.Value.ToString(), this.EndDate.Value.ToString())), ErrorCategory.InvalidArgument, null);
			}
			if (base.Fields["ResultSize"] == null)
			{
				this.ResultSize = 1000;
			}
			int num = 250000;
			if (base.Fields["StartIndex"] == null)
			{
				this.StartIndex = 0;
			}
			if (this.StartIndex < 0 || this.StartIndex > num)
			{
				base.WriteError(new ArgumentOutOfRangeException("StartIndex", this.StartIndex, Strings.AdminAuditLogSearchOutOfRangeStartIndex(num)), ErrorCategory.InvalidArgument, null);
			}
			this.searchObject = new AdminAuditLogSearch
			{
				OrganizationId = base.CurrentOrganizationId,
				Cmdlets = this.Cmdlets,
				Parameters = this.Parameters,
				ObjectIds = this.ObjectIds,
				UserIdsUserInput = this.UserIds,
				Succeeded = this.IsSuccess,
				StartIndex = this.StartIndex,
				ExternalAccess = this.ExternalAccess,
				ResultSize = this.ResultSize,
				RedactDatacenterAdmins = !AdminAuditExternalAccessDeterminer.IsExternalAccess(base.SessionSettings.ExecutingUserIdentityName, base.SessionSettings.ExecutingUserOrganizationId, base.SessionSettings.CurrentOrganizationId)
			};
			if (!this.StartDate.Value.HasTimeZone)
			{
				ExDateTime exDateTime = ExDateTime.Create(ExTimeZone.CurrentTimeZone, this.StartDate.Value.UniversalTime)[0];
				this.searchObject.StartDateUtc = new DateTime?(exDateTime.UniversalTime);
			}
			else
			{
				this.searchObject.StartDateUtc = new DateTime?(this.StartDate.Value.UniversalTime);
			}
			if (!this.EndDate.Value.HasTimeZone)
			{
				ExDateTime exDateTime2 = ExDateTime.Create(ExTimeZone.CurrentTimeZone, this.EndDate.Value.UniversalTime)[0];
				this.searchObject.EndDateUtc = new DateTime?(exDateTime2.UniversalTime);
			}
			else
			{
				this.searchObject.EndDateUtc = new DateTime?(this.EndDate.Value.UniversalTime);
			}
			AdminAuditLogHelper.SetResolveUsers(this.searchObject, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			this.searchObject.Validate(new Task.TaskErrorLoggingDelegate(base.WriteError));
			base.InternalValidate();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000985C File Offset: 0x00007A5C
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
			try
			{
				TaskLogger.LogEnter(new object[]
				{
					dataObjects
				});
				DiagnosticContext.Reset();
				using (AuditLogOpticsLogData auditLogOpticsLogData = new AuditLogOpticsLogData())
				{
					auditLogOpticsLogData.IsAsynchronous = false;
					auditLogOpticsLogData.CallResult = false;
					auditLogOpticsLogData.SearchStartDateTime = this.searchObject.StartDateUtc;
					auditLogOpticsLogData.SearchEndDateTime = this.searchObject.EndDateUtc;
					try
					{
						if (AdminAuditLogHelper.ShouldIssueWarning(base.CurrentOrganizationId))
						{
							DiagnosticContext.TraceLocation((LID)42684U);
							this.WriteWarning(Strings.WarningSearchAdminAuditLogOnPreE15(base.CurrentOrganizationId.ToString()));
						}
						else
						{
							if (dataObjects != null)
							{
								using (IEnumerator<AdminAuditLogConfig> enumerator = (IEnumerator<AdminAuditLogConfig>)dataObjects.GetEnumerator())
								{
									this.GetAuditConfigObject(enumerator);
									auditLogOpticsLogData.SearchType = "Admin";
									auditLogOpticsLogData.OrganizationId = this.searchObject.OrganizationId;
									auditLogOpticsLogData.ShowDetails = true;
									auditLogOpticsLogData.MailboxCount = 1;
									AdminAuditLogSearchWorker adminAuditLogSearchWorker = new AdminAuditLogSearchWorker(600, this.searchObject, auditLogOpticsLogData);
									base.WriteVerbose(Strings.VerboseStartAuditLogSearch);
									AdminAuditLogEvent[] array = adminAuditLogSearchWorker.Search();
									base.WriteVerbose(Strings.VerboseSearchCompleted((array != null) ? array.Length : 0));
									foreach (AdminAuditLogEvent dataObject in array)
									{
										this.WriteResult(dataObject);
									}
									auditLogOpticsLogData.CallResult = true;
									goto IL_181;
								}
							}
							DiagnosticContext.TraceLocation((LID)59068U);
							Exception ex = new AdminAuditLogSearchException(Strings.ErrorAdminAuditLogConfig(base.CurrentOrganizationId.ToString()));
							auditLogOpticsLogData.ErrorType = ex;
							auditLogOpticsLogData.ErrorCount++;
							base.WriteError(ex, ErrorCategory.ObjectNotFound, null);
							IL_181:;
						}
					}
					catch (ArgumentException ex2)
					{
						DiagnosticContext.TraceLocation((LID)34492U);
						auditLogOpticsLogData.ErrorType = ex2;
						auditLogOpticsLogData.ErrorCount++;
						base.WriteError(ex2, ErrorCategory.InvalidArgument, null);
					}
					catch (AdminAuditLogSearchException ex3)
					{
						DiagnosticContext.TraceLocation((LID)50876U);
						auditLogOpticsLogData.ErrorType = ex3;
						auditLogOpticsLogData.ErrorCount++;
						base.WriteError(ex3, ErrorCategory.NotSpecified, null);
					}
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00009AE4 File Offset: 0x00007CE4
		private AdminAuditLogConfig GetAuditConfigObject(IEnumerator<AdminAuditLogConfig> dataObjects)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObjects
			});
			base.WriteDebug(Strings.DebugStartRetrievingAuditConfig);
			dataObjects.MoveNext();
			AdminAuditLogConfig adminAuditLogConfig = dataObjects.Current;
			if (adminAuditLogConfig == null || adminAuditLogConfig.Identity == null)
			{
				base.WriteError(new AdminAuditLogSearchException(Strings.ErrorAdminAuditLogConfig(base.CurrentOrganizationId.ToString())), ErrorCategory.ObjectNotFound, null);
			}
			TaskLogger.LogExit();
			return adminAuditLogConfig;
		}

		// Token: 0x04000138 RID: 312
		internal const int defaultSearchTimeoutSeconds = 600;

		// Token: 0x04000139 RID: 313
		private const int DefaultSearchResultSize = 1000;

		// Token: 0x0400013A RID: 314
		private const int DefaultSearchTimePeriodInDays = 15;

		// Token: 0x0400013B RID: 315
		private AdminAuditLogSearch searchObject;
	}
}
