using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000424 RID: 1060
	[Cmdlet("Search", "MailboxAuditLog", DefaultParameterSetName = "Identity")]
	public sealed class SearchMailboxAuditLog : GetTenantADObjectWithIdentityTaskBase<MailboxIdParameter, ADUser>
	{
		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x060024D2 RID: 9426 RVA: 0x00092EB0 File Offset: 0x000910B0
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x060024D3 RID: 9427 RVA: 0x00092EB4 File Offset: 0x000910B4
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter internalFilter = base.InternalFilter;
				QueryFilter queryFilter = this.searchCriteria.GetRecipientFilter();
				if (internalFilter != null)
				{
					queryFilter = new AndFilter(new QueryFilter[]
					{
						internalFilter,
						queryFilter
					});
				}
				return queryFilter;
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x00092EEE File Offset: 0x000910EE
		// (set) Token: 0x060024D5 RID: 9429 RVA: 0x00092F05 File Offset: 0x00091105
		[Parameter(Mandatory = false, ParameterSetName = "MultipleMailboxesSearch")]
		public MultiValuedProperty<MailboxIdParameter> Mailboxes
		{
			get
			{
				return (MultiValuedProperty<MailboxIdParameter>)base.Fields["Mailboxes"];
			}
			set
			{
				base.Fields["Mailboxes"] = value;
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x060024D6 RID: 9430 RVA: 0x00092F18 File Offset: 0x00091118
		// (set) Token: 0x060024D7 RID: 9431 RVA: 0x00092F2F File Offset: 0x0009112F
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x060024D8 RID: 9432 RVA: 0x00092F42 File Offset: 0x00091142
		// (set) Token: 0x060024D9 RID: 9433 RVA: 0x00092F59 File Offset: 0x00091159
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[Parameter(Mandatory = false, ParameterSetName = "MultipleMailboxesSearch")]
		public MultiValuedProperty<AuditScopes> LogonTypes
		{
			get
			{
				return (MultiValuedProperty<AuditScopes>)base.Fields["LogonTypes"];
			}
			set
			{
				base.Fields["LogonTypes"] = value;
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x060024DA RID: 9434 RVA: 0x00092F6C File Offset: 0x0009116C
		// (set) Token: 0x060024DB RID: 9435 RVA: 0x00092F83 File Offset: 0x00091183
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[Parameter(Mandatory = false, ParameterSetName = "MultipleMailboxesSearch")]
		public MultiValuedProperty<MailboxAuditOperations> Operations
		{
			get
			{
				return (MultiValuedProperty<MailboxAuditOperations>)base.Fields["Operations"];
			}
			set
			{
				base.Fields["Operations"] = value;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x00092F96 File Offset: 0x00091196
		// (set) Token: 0x060024DD RID: 9437 RVA: 0x00092FAD File Offset: 0x000911AD
		[Parameter(Mandatory = false, ParameterSetName = "MultipleMailboxesSearch")]
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

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x060024DE RID: 9438 RVA: 0x00092FC5 File Offset: 0x000911C5
		// (set) Token: 0x060024DF RID: 9439 RVA: 0x00092FDC File Offset: 0x000911DC
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[Parameter(Mandatory = false, ParameterSetName = "MultipleMailboxesSearch")]
		[ValidateRange(1, 250000)]
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

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x060024E0 RID: 9440 RVA: 0x00092FF4 File Offset: 0x000911F4
		// (set) Token: 0x060024E1 RID: 9441 RVA: 0x0009300B File Offset: 0x0009120B
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[Parameter(Mandatory = false, ParameterSetName = "MultipleMailboxesSearch")]
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

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x060024E2 RID: 9442 RVA: 0x00093023 File Offset: 0x00091223
		// (set) Token: 0x060024E3 RID: 9443 RVA: 0x0009303A File Offset: 0x0009123A
		[Parameter(Mandatory = false, ParameterSetName = "MultipleMailboxesSearch")]
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

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x060024E4 RID: 9444 RVA: 0x00093052 File Offset: 0x00091252
		// (set) Token: 0x060024E5 RID: 9445 RVA: 0x00093078 File Offset: 0x00091278
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter ShowDetails
		{
			get
			{
				return (SwitchParameter)(base.Fields["ShowDetails"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ShowDetails"] = value;
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x060024E6 RID: 9446 RVA: 0x00093090 File Offset: 0x00091290
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmSearchMailboxAuditLogTask(base.CurrentOrgContainerId.ToString());
			}
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000930A4 File Offset: 0x000912A4
		protected override void InternalValidate()
		{
			base.InternalValidate();
			this.resultCount = 0;
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
				base.WriteError(new ArgumentException(Strings.ErrorMailboxAuditLogSearchStartDateIsLaterThanEndDate(this.StartDate.Value.ToString(), this.EndDate.Value.ToString())), ErrorCategory.InvalidArgument, null);
			}
			if (base.Fields["ResultSize"] == null)
			{
				this.ResultSize = 10000;
			}
			this.searchCriteria = new MailboxAuditLogSearch
			{
				OrganizationId = base.CurrentOrganizationId,
				LogonTypesUserInput = this.LogonTypes,
				OperationsUserInput = this.Operations,
				ShowDetails = this.ShowDetails.ToBool(),
				ExternalAccess = this.ExternalAccess
			};
			if (!this.StartDate.Value.HasTimeZone)
			{
				ExDateTime exDateTime = ExDateTime.Create(ExTimeZone.CurrentTimeZone, this.StartDate.Value.UniversalTime)[0];
				this.searchCriteria.StartDateUtc = new DateTime?(exDateTime.UniversalTime);
			}
			else
			{
				this.searchCriteria.StartDateUtc = new DateTime?(this.StartDate.Value.UniversalTime);
			}
			if (!this.EndDate.Value.HasTimeZone)
			{
				ExDateTime exDateTime2 = ExDateTime.Create(ExTimeZone.CurrentTimeZone, this.EndDate.Value.UniversalTime)[0];
				this.searchCriteria.EndDateUtc = new DateTime?(exDateTime2.UniversalTime);
			}
			else
			{
				this.searchCriteria.EndDateUtc = new DateTime?(this.EndDate.Value.UniversalTime);
			}
			this.searchCriteria.Mailboxes = MailboxAuditLogSearch.ConvertTo((IRecipientSession)base.DataSession, this.Mailboxes, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), new Task.TaskErrorLoggingDelegate(base.WriteError));
			this.searchCriteria.Validate(new Task.TaskErrorLoggingDelegate(base.WriteError));
			this.searchStatistics = new AuditLogOpticsLogData();
			this.searchStatistics.OrganizationId = this.searchCriteria.OrganizationId;
			this.searchStatistics.SearchType = "Mailbox";
			this.searchStatistics.CallResult = false;
			this.searchStatistics.QueryComplexity = this.searchCriteria.QueryComplexity;
			this.searchStatistics.IsAsynchronous = false;
			this.searchStatistics.ShowDetails = this.searchCriteria.ShowDetails;
			this.searchStatistics.SearchStartDateTime = this.searchCriteria.StartDateUtc;
			this.searchStatistics.SearchEndDateTime = this.searchCriteria.EndDateUtc;
			this.worker = new MailboxAuditLogSearchWorker(600, this.searchCriteria, this.ResultSize, this.searchStatistics);
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x000934C0 File Offset: 0x000916C0
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.NetCredential, sessionSettings, ConfigScopes.TenantSubTree, 344, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxAuditLog\\SearchMailboxAuditLog.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				return adorganizationalUnit.OrganizationId;
			}
			return base.ResolveCurrentOrganization();
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x00093574 File Offset: 0x00091774
		protected override void InternalProcessRecord()
		{
			DiagnosticContext.Reset();
			if (base.ParameterSetName == "MultipleMailboxesSearch")
			{
				if (this.Mailboxes != null && this.Mailboxes.Count == 0)
				{
					this.ProcessRecordInternal();
					return;
				}
				using (MultiValuedProperty<MailboxIdParameter>.Enumerator enumerator = this.Mailboxes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MailboxIdParameter identity = enumerator.Current;
						this.Identity = identity;
						this.ProcessRecordInternal();
					}
					return;
				}
			}
			this.ProcessRecordInternal();
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x00093608 File Offset: 0x00091808
		private void ProcessRecordInternal()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			if (this.Identity != null)
			{
				LocalizedString? localizedString;
				IEnumerable<ADUser> dataObjects = base.GetDataObjects<ADUser>(this.Identity, base.DataSession, this.RootId, base.OptionalIdentityData, out localizedString);
				this.WriteResult<ADUser>(dataObjects);
				if (!base.HasErrors && base.WriteObjectCount == 0U && !this.isUserObjValid)
				{
					base.WriteError(new ManagementObjectNotFoundException(localizedString ?? base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ADUser).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), (ErrorCategory)1003, null);
				}
			}
			else
			{
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000936E0 File Offset: 0x000918E0
		protected override void WriteResult(IConfigurable dataObject)
		{
			ADUser mailbox = (ADUser)dataObject;
			this.isUserObjValid = true;
			if (this.resultCount <= this.ResultSize)
			{
				DiagnosticContext.TraceLocation((LID)47804U);
				Exception ex = null;
				try
				{
					IEnumerable<MailboxAuditLogRecord> enumerable = this.worker.SearchMailboxAudits(mailbox);
					foreach (MailboxAuditLogRecord dataObject2 in enumerable)
					{
						this.resultCount++;
						if (this.resultCount > this.ResultSize)
						{
							DiagnosticContext.TraceLocation((LID)64188U);
							break;
						}
						base.WriteResult(dataObject2);
					}
				}
				catch (StorageTransientException ex2)
				{
					DiagnosticContext.TraceLocation((LID)39612U);
					TaskLogger.Trace("Search mailbox audit log failed with transient storage exception. {0}", new object[]
					{
						ex2
					});
					ex = ex2;
					base.WriteError(ex2, ErrorCategory.ReadError, null);
				}
				catch (StoragePermanentException ex3)
				{
					DiagnosticContext.TraceLocation((LID)55996U);
					TaskLogger.Trace("Search mailbox audit log failed with permanent storage exception. {0}", new object[]
					{
						ex3
					});
					ex = ex3;
					base.WriteError(ex3, ErrorCategory.ReadError, null);
				}
				catch (MailboxAuditLogSearchException ex4)
				{
					DiagnosticContext.TraceLocation((LID)43708U);
					ex = ex4;
					base.WriteError(ex4, ErrorCategory.NotSpecified, null);
				}
				catch (AuditLogException ex5)
				{
					DiagnosticContext.TraceLocation((LID)60092U);
					ex = ex5;
					base.WriteError(ex5, ErrorCategory.NotSpecified, null);
				}
				finally
				{
					if (this.searchStatistics != null)
					{
						this.searchStatistics.MailboxCount++;
						if (ex != null)
						{
							this.searchStatistics.ErrorType = ex;
							this.searchStatistics.ErrorCount++;
						}
					}
					TaskLogger.LogExit();
				}
			}
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x00093920 File Offset: 0x00091B20
		protected override IConfigDataProvider CreateSession()
		{
			TaskLogger.LogEnter();
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 531, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxAuditLog\\SearchMailboxAuditLog.cs");
			TaskLogger.LogExit();
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x00093968 File Offset: 0x00091B68
		protected override void InternalEndProcessing()
		{
			TaskLogger.LogEnter();
			try
			{
				if (this.searchStatistics != null)
				{
					this.searchStatistics.CallResult = (this.searchStatistics.ErrorCount == 0);
					this.searchStatistics.Dispose();
					this.searchStatistics = null;
				}
			}
			finally
			{
				base.InternalEndProcessing();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x000939CC File Offset: 0x00091BCC
		protected override void InternalStopProcessing()
		{
			TaskLogger.LogEnter();
			try
			{
				if (this.searchStatistics != null)
				{
					this.searchStatistics.Dispose();
					this.searchStatistics = null;
				}
			}
			finally
			{
				base.InternalStopProcessing();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04001D1D RID: 7453
		private const string ParameterSetMailboxes = "MultipleMailboxesSearch";

		// Token: 0x04001D1E RID: 7454
		private const int DefaultSearchTimePeriodInDays = 15;

		// Token: 0x04001D1F RID: 7455
		internal const int DefaultSearchTimeoutSeconds = 600;

		// Token: 0x04001D20 RID: 7456
		private const int DefaultSearchResultSize = 10000;

		// Token: 0x04001D21 RID: 7457
		private bool isUserObjValid;

		// Token: 0x04001D22 RID: 7458
		private int resultCount;

		// Token: 0x04001D23 RID: 7459
		private AuditLogOpticsLogData searchStatistics;

		// Token: 0x04001D24 RID: 7460
		private MailboxAuditLogSearchWorker worker;

		// Token: 0x04001D25 RID: 7461
		private MailboxAuditLogSearch searchCriteria;
	}
}
