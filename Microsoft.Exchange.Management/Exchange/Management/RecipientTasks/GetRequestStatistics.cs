using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C60 RID: 3168
	public abstract class GetRequestStatistics<TIdentity, TDataObject> : GetTaskBase<TDataObject> where TIdentity : MRSRequestIdParameter where TDataObject : RequestStatisticsBase, new()
	{
		// Token: 0x06007859 RID: 30809 RVA: 0x001EA28C File Offset: 0x001E848C
		protected GetRequestStatistics()
		{
			this.fromMdb = null;
			this.gcSession = null;
			this.recipSession = null;
			this.configSession = null;
			this.currentOrgConfigSession = null;
			this.rjProvider = null;
		}

		// Token: 0x17002536 RID: 9526
		// (get) Token: 0x0600785A RID: 30810 RVA: 0x001EA2BE File Offset: 0x001E84BE
		// (set) Token: 0x0600785B RID: 30811 RVA: 0x001EA2D5 File Offset: 0x001E84D5
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public TIdentity Identity
		{
			get
			{
				return (TIdentity)((object)base.Fields["Identity"]);
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17002537 RID: 9527
		// (get) Token: 0x0600785C RID: 30812 RVA: 0x001EA2ED File Offset: 0x001E84ED
		// (set) Token: 0x0600785D RID: 30813 RVA: 0x001EA313 File Offset: 0x001E8513
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeReport
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeReport"] ?? false);
			}
			set
			{
				base.Fields["IncludeReport"] = value;
			}
		}

		// Token: 0x17002538 RID: 9528
		// (get) Token: 0x0600785E RID: 30814 RVA: 0x001EA32B File Offset: 0x001E852B
		// (set) Token: 0x0600785F RID: 30815 RVA: 0x001EA342 File Offset: 0x001E8542
		[Parameter(Mandatory = true, ParameterSetName = "MigrationRequestQueue")]
		[ValidateNotNull]
		public DatabaseIdParameter RequestQueue
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["RequestQueue"];
			}
			set
			{
				base.Fields["RequestQueue"] = value;
			}
		}

		// Token: 0x17002539 RID: 9529
		// (get) Token: 0x06007860 RID: 30816 RVA: 0x001EA355 File Offset: 0x001E8555
		// (set) Token: 0x06007861 RID: 30817 RVA: 0x001EA37A File Offset: 0x001E857A
		[Parameter(Mandatory = false, ParameterSetName = "MigrationRequestQueue")]
		public Guid RequestGuid
		{
			get
			{
				return (Guid)(base.Fields["RequestGuid"] ?? Guid.Empty);
			}
			set
			{
				base.Fields["RequestGuid"] = value;
			}
		}

		// Token: 0x1700253A RID: 9530
		// (get) Token: 0x06007862 RID: 30818 RVA: 0x001EA392 File Offset: 0x001E8592
		// (set) Token: 0x06007863 RID: 30819 RVA: 0x001EA39A File Offset: 0x001E859A
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x1700253B RID: 9531
		// (get) Token: 0x06007864 RID: 30820 RVA: 0x001EA3A3 File Offset: 0x001E85A3
		// (set) Token: 0x06007865 RID: 30821 RVA: 0x001EA3C9 File Offset: 0x001E85C9
		[Parameter(Mandatory = false)]
		public SwitchParameter Diagnostic
		{
			get
			{
				return (SwitchParameter)(base.Fields["Diagnostic"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Diagnostic"] = value;
			}
		}

		// Token: 0x1700253C RID: 9532
		// (get) Token: 0x06007866 RID: 30822 RVA: 0x001EA3E1 File Offset: 0x001E85E1
		// (set) Token: 0x06007867 RID: 30823 RVA: 0x001EA3F8 File Offset: 0x001E85F8
		[ValidateNotNull]
		[ValidateLength(1, 1048576)]
		[Parameter(Mandatory = false)]
		public string DiagnosticArgument
		{
			get
			{
				return (string)base.Fields["DiagnosticArgument"];
			}
			set
			{
				base.Fields["DiagnosticArgument"] = value;
			}
		}

		// Token: 0x1700253D RID: 9533
		// (get) Token: 0x06007868 RID: 30824 RVA: 0x001EA40B File Offset: 0x001E860B
		protected virtual RequestIndexId DefaultRequestIndexId
		{
			get
			{
				return new RequestIndexId(RequestIndexLocation.AD);
			}
		}

		// Token: 0x1700253E RID: 9534
		// (get) Token: 0x06007869 RID: 30825 RVA: 0x001EA413 File Offset: 0x001E8613
		protected MRSRequestType RequestType
		{
			get
			{
				return GetRequestStatistics<TIdentity, TDataObject>.requestType;
			}
		}

		// Token: 0x1700253F RID: 9535
		// (get) Token: 0x0600786A RID: 30826 RVA: 0x001EA41A File Offset: 0x001E861A
		protected override ObjectId RootId
		{
			get
			{
				return ADHandler.GetRootId(this.currentOrgConfigSession, this.RequestType);
			}
		}

		// Token: 0x17002540 RID: 9536
		// (get) Token: 0x0600786B RID: 30827 RVA: 0x001EA42D File Offset: 0x001E862D
		protected override QueryFilter InternalFilter
		{
			get
			{
				if (base.ParameterSetName.Equals("MigrationRequestQueue"))
				{
					return new RequestJobQueryFilter(this.RequestGuid, this.fromMdb.ObjectGuid, this.RequestType);
				}
				return null;
			}
		}

		// Token: 0x0600786C RID: 30828 RVA: 0x001EA45F File Offset: 0x001E865F
		internal virtual void CheckIndexEntry(IRequestIndexEntry index)
		{
		}

		// Token: 0x0600786D RID: 30829 RVA: 0x001EA464 File Offset: 0x001E8664
		protected override IConfigDataProvider CreateSession()
		{
			ADObjectId rootOrgContainerId = ADSystemConfigurationSession.GetRootOrgContainerId(this.DomainController, null);
			ADSessionSettings adsessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, rootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			this.currentOrgConfigSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, null, adsessionSettings, 313, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\RequestBase\\GetRequestStatistics.cs");
			adsessionSettings = ADSessionSettings.RescopeToSubtree(adsessionSettings);
			if (MapiTaskHelper.IsDatacenter || MapiTaskHelper.IsDatacenterDedicated)
			{
				adsessionSettings.IncludeSoftDeletedObjects = true;
				adsessionSettings.IncludeInactiveMailbox = true;
			}
			this.gcSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, adsessionSettings, 330, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\RequestBase\\GetRequestStatistics.cs");
			this.recipSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, adsessionSettings, 337, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\RequestBase\\GetRequestStatistics.cs");
			this.configSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromRootOrgScopeSet(), 343, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\RequestBase\\GetRequestStatistics.cs");
			if (this.rjProvider != null)
			{
				this.rjProvider.Dispose();
				this.rjProvider = null;
			}
			if (base.ParameterSetName.Equals("MigrationRequestQueue"))
			{
				MailboxDatabase mailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.RequestQueue, this.configSession, null, new LocalizedString?(Strings.ErrorMailboxDatabaseNotFound(this.RequestQueue.ToString())), new LocalizedString?(Strings.ErrorMailboxDatabaseNotUnique(this.RequestQueue.ToString())));
				this.rjProvider = new RequestJobProvider(mailboxDatabase.Guid);
			}
			else
			{
				this.rjProvider = new RequestJobProvider(this.gcSession, this.currentOrgConfigSession);
			}
			this.rjProvider.LoadReport = this.IncludeReport;
			return this.rjProvider;
		}

		// Token: 0x0600786E RID: 30830 RVA: 0x001EA625 File Offset: 0x001E8825
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.rjProvider != null)
			{
				this.rjProvider.Dispose();
				this.rjProvider = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600786F RID: 30831 RVA: 0x001EA64C File Offset: 0x001E884C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				if (base.ParameterSetName.Equals("Identity"))
				{
					TIdentity identity = this.Identity;
					if (identity.OrganizationId != null)
					{
						IDirectorySession dataSession = this.recipSession;
						TIdentity identity2 = this.Identity;
						if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(dataSession, identity2.OrganizationId))
						{
							IDirectorySession session = this.recipSession;
							TIdentity identity3 = this.Identity;
							this.recipSession = (IRecipientSession)TaskHelper.UnderscopeSessionToOrganization(session, identity3.OrganizationId, true);
						}
						IDirectorySession dataSession2 = this.currentOrgConfigSession;
						TIdentity identity4 = this.Identity;
						if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(dataSession2, identity4.OrganizationId))
						{
							IDirectorySession session2 = this.currentOrgConfigSession;
							TIdentity identity5 = this.Identity;
							this.currentOrgConfigSession = (ITenantConfigurationSession)TaskHelper.UnderscopeSessionToOrganization(session2, identity5.OrganizationId, true);
							this.rjProvider.IndexProvider.ConfigSession = this.currentOrgConfigSession;
						}
					}
					ADUser aduser = null;
					TIdentity identity6 = this.Identity;
					if (!string.IsNullOrEmpty(identity6.MailboxName))
					{
						IRecipientSession dataSession3 = this.recipSession;
						IRecipientSession globalCatalogSession = this.gcSession;
						ADServerSettings serverSettings = base.ServerSettings;
						TIdentity identity7 = this.Identity;
						aduser = RequestTaskHelper.ResolveADUser(dataSession3, globalCatalogSession, serverSettings, new MailboxOrMailUserIdParameter(identity7.MailboxName), base.OptionalIdentityData, this.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), false);
						if (aduser != null)
						{
							TIdentity identity8 = this.Identity;
							identity8.MailboxId = aduser.Id;
							if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(this.recipSession, aduser))
							{
								this.recipSession = (IRecipientSession)TaskHelper.UnderscopeSessionToOrganization(this.recipSession, aduser.OrganizationId, true);
							}
							if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(this.currentOrgConfigSession, aduser))
							{
								this.currentOrgConfigSession = (ITenantConfigurationSession)TaskHelper.UnderscopeSessionToOrganization(this.currentOrgConfigSession, aduser.OrganizationId, true);
								this.rjProvider.IndexProvider.ConfigSession = this.currentOrgConfigSession;
							}
						}
					}
					TIdentity identity9 = this.Identity;
					if (!string.IsNullOrEmpty(identity9.OrganizationName))
					{
						IConfigurationSession configurationSession = RequestTaskHelper.CreateOrganizationFindingSession(base.CurrentOrganizationId, base.ExecutingUserOrganizationId);
						TIdentity identity10 = this.Identity;
						IIdentityParameter id = new OrganizationIdParameter(identity10.OrganizationName);
						IConfigDataProvider session3 = configurationSession;
						ObjectId rootID = null;
						TIdentity identity11 = this.Identity;
						LocalizedString? notFoundError = new LocalizedString?(Strings.ErrorOrganizationNotFound(identity11.OrganizationName));
						TIdentity identity12 = this.Identity;
						ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(id, session3, rootID, notFoundError, new LocalizedString?(Strings.ErrorOrganizationNotUnique(identity12.OrganizationName)));
						if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(this.recipSession, adorganizationalUnit))
						{
							this.recipSession = (IRecipientSession)TaskHelper.UnderscopeSessionToOrganization(this.recipSession, adorganizationalUnit.OrganizationId, true);
						}
						if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(this.currentOrgConfigSession, adorganizationalUnit))
						{
							this.currentOrgConfigSession = (ITenantConfigurationSession)TaskHelper.UnderscopeSessionToOrganization(this.currentOrgConfigSession, adorganizationalUnit.OrganizationId, true);
							this.rjProvider.IndexProvider.ConfigSession = this.currentOrgConfigSession;
						}
					}
					TIdentity identity13 = this.Identity;
					identity13.SetDefaultIndex(this.DefaultRequestIndexId);
					IRequestIndexEntry entry = this.GetEntry();
					RequestJobObjectId requestJobId = entry.GetRequestJobId();
					if (entry.TargetUserId != null)
					{
						if (aduser != null && aduser.Id.Equals(entry.TargetUserId))
						{
							requestJobId.TargetUser = aduser;
						}
						else
						{
							requestJobId.TargetUser = RequestTaskHelper.ResolveADUser(this.recipSession, this.gcSession, base.ServerSettings, new MailboxOrMailUserIdParameter(entry.TargetUserId), base.OptionalIdentityData, this.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), false);
						}
					}
					if (entry.SourceUserId != null)
					{
						if (aduser != null && aduser.Id.Equals(entry.SourceUserId))
						{
							requestJobId.SourceUser = aduser;
						}
						else
						{
							requestJobId.SourceUser = RequestTaskHelper.ResolveADUser(this.recipSession, this.gcSession, base.ServerSettings, new MailboxOrMailUserIdParameter(entry.SourceUserId), base.OptionalIdentityData, this.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), false);
						}
					}
					this.CheckIndexEntry(entry);
					TDataObject tdataObject = (TDataObject)((object)this.rjProvider.Read<TDataObject>(requestJobId));
					if (tdataObject == null || tdataObject.Status == RequestStatus.None)
					{
						TIdentity identity14 = this.Identity;
						base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorCouldNotFindRequest(identity14.ToString())), ErrorCategory.InvalidArgument, this.Identity);
					}
					else if (tdataObject.RequestType != this.RequestType)
					{
						base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorNotEnoughInformationToFindRequest), ErrorCategory.InvalidArgument, this.Identity);
					}
					else
					{
						this.WriteResult(tdataObject);
					}
				}
				else if (base.ParameterSetName.Equals("MigrationRequestQueue"))
				{
					if (this.RequestQueue != null)
					{
						MailboxDatabase mailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.RequestQueue, this.configSession, null, new LocalizedString?(Strings.ErrorMailboxDatabaseNotFound(this.RequestQueue.ToString())), new LocalizedString?(Strings.ErrorMailboxDatabaseNotUnique(this.RequestQueue.ToString())));
						this.fromMdb = mailboxDatabase.Id;
					}
					this.rjProvider.AllowInvalid = true;
					base.InternalProcessRecord();
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007870 RID: 30832 RVA: 0x001EABE8 File Offset: 0x001E8DE8
		protected override bool IsKnownException(Exception exception)
		{
			return RequestTaskHelper.IsKnownExceptionHandler(exception, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose)) || base.IsKnownException(exception);
		}

		// Token: 0x06007871 RID: 30833 RVA: 0x001EAC08 File Offset: 0x001E8E08
		protected override void TranslateException(ref Exception e, out ErrorCategory category)
		{
			LocalizedException ex = RequestTaskHelper.TranslateExceptionHandler(e);
			if (ex == null)
			{
				ErrorCategory errorCategory;
				base.TranslateException(ref e, out errorCategory);
				category = errorCategory;
				return;
			}
			e = ex;
			category = ErrorCategory.ResourceUnavailable;
		}

		// Token: 0x06007872 RID: 30834 RVA: 0x001EAC34 File Offset: 0x001E8E34
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject
			});
			TDataObject tdataObject = (TDataObject)((object)dataObject);
			try
			{
				RequestTaskHelper.GetUpdatedMRSRequestInfo(tdataObject, this.Diagnostic, this.DiagnosticArgument);
				if (tdataObject.Status == RequestStatus.Queued)
				{
					tdataObject.PositionInQueue = this.rjProvider.ComputePositionInQueue(tdataObject.RequestGuid);
				}
				base.WriteResult(tdataObject);
				if (tdataObject.ValidationResult != RequestJobBase.ValidationResultEnum.Valid)
				{
					this.WriteWarning(Strings.ErrorInvalidRequest(tdataObject.Identity.ToString(), tdataObject.ValidationMessage));
				}
				if (tdataObject.PoisonCount > 5)
				{
					this.WriteWarning(Strings.WarningJobIsPoisoned(tdataObject.Identity.ToString(), tdataObject.PoisonCount));
				}
				if (base.ParameterSetName.Equals("MigrationRequestQueue"))
				{
					base.WriteVerbose(Strings.RawRequestJobDump(CommonUtils.ConfigurableObjectToString(tdataObject)));
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007873 RID: 30835 RVA: 0x001EAD94 File Offset: 0x001E8F94
		protected bool IsFieldSet(string fieldName)
		{
			return base.Fields.IsChanged(fieldName) || base.Fields.IsModified(fieldName);
		}

		// Token: 0x06007874 RID: 30836 RVA: 0x001EADB2 File Offset: 0x001E8FB2
		internal void CheckIndexEntryLocalUserNotNull(IRequestIndexEntry index)
		{
			if (index.SourceUserId == null && index.TargetUserId == null)
			{
				base.WriteError(new ManagementObjectNotFoundException(Strings.RequestIndexEntryIsMissingLocalUserData(index.ToString())), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007875 RID: 30837 RVA: 0x001EADE6 File Offset: 0x001E8FE6
		internal void CheckIndexEntryTargetUserNotNull(IRequestIndexEntry index)
		{
			if (index.TargetUserId == null)
			{
				base.WriteError(new ManagementObjectNotFoundException(Strings.RequestIndexEntryIsMissingLocalUserData(index.ToString())), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007876 RID: 30838 RVA: 0x001EAE14 File Offset: 0x001E9014
		private IRequestIndexEntry GetEntry()
		{
			LocalizedString? notFoundError = new LocalizedString?(Strings.ErrorNotEnoughInformationToFindRequest);
			LocalizedString? multipleFoundError = new LocalizedString?(Strings.ErrorNotEnoughInformationToFindUniqueRequest);
			TIdentity identity = this.Identity;
			if (identity.IndexToUse == null)
			{
				base.WriteError(new UnknownRequestIndexPermanentException(null), ErrorCategory.InvalidArgument, this.Identity);
				return null;
			}
			TIdentity identity2 = this.Identity;
			if (identity2.IndexToUse.RequestIndexEntryType == null)
			{
				return null;
			}
			TIdentity identity3 = this.Identity;
			if (identity3.IndexToUse.RequestIndexEntryType == typeof(MRSRequestWrapper))
			{
				IIdentityParameter id = this.Identity;
				IConfigDataProvider indexProvider = this.rjProvider.IndexProvider;
				IConfigurationSession configurationSession = this.currentOrgConfigSession;
				TIdentity identity4 = this.Identity;
				return (IRequestIndexEntry)base.GetDataObject<MRSRequestWrapper>(id, indexProvider, ADHandler.GetRootId(configurationSession, identity4.RequestType), notFoundError, multipleFoundError);
			}
			TIdentity identity5 = this.Identity;
			if (identity5.IndexToUse.RequestIndexEntryType == typeof(MRSRequestMailboxEntry))
			{
				IIdentityParameter id2 = this.Identity;
				IConfigDataProvider indexProvider2 = this.rjProvider.IndexProvider;
				IConfigurationSession configurationSession2 = this.currentOrgConfigSession;
				TIdentity identity6 = this.Identity;
				return (IRequestIndexEntry)base.GetDataObject<MRSRequestMailboxEntry>(id2, indexProvider2, ADHandler.GetRootId(configurationSession2, identity6.RequestType), notFoundError, multipleFoundError);
			}
			TIdentity identity7 = this.Identity;
			base.WriteError(new UnknownRequestIndexPermanentException(identity7.IndexToUse.ToString()), ErrorCategory.InvalidArgument, this.Identity);
			return null;
		}

		// Token: 0x04003C13 RID: 15379
		public const string ParameterIncludeReport = "IncludeReport";

		// Token: 0x04003C14 RID: 15380
		public const string ParameterRequestQueue = "RequestQueue";

		// Token: 0x04003C15 RID: 15381
		public const string ParameterRequestGuid = "RequestGuid";

		// Token: 0x04003C16 RID: 15382
		public const string ParameterIdentity = "Identity";

		// Token: 0x04003C17 RID: 15383
		public const string RequestQueueSet = "MigrationRequestQueue";

		// Token: 0x04003C18 RID: 15384
		public const string ParameterDiagnostic = "Diagnostic";

		// Token: 0x04003C19 RID: 15385
		public const string ParameterDiagnosticArgument = "DiagnosticArgument";

		// Token: 0x04003C1A RID: 15386
		private static readonly MRSRequestType requestType = MRSRequestIdParameter.GetRequestType<TIdentity>();

		// Token: 0x04003C1B RID: 15387
		private ADObjectId fromMdb;

		// Token: 0x04003C1C RID: 15388
		private IRecipientSession recipSession;

		// Token: 0x04003C1D RID: 15389
		private IRecipientSession gcSession;

		// Token: 0x04003C1E RID: 15390
		private IConfigurationSession currentOrgConfigSession;

		// Token: 0x04003C1F RID: 15391
		private ITopologyConfigurationSession configSession;

		// Token: 0x04003C20 RID: 15392
		private RequestJobProvider rjProvider;
	}
}
