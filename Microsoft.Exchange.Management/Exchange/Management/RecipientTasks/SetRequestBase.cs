using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C66 RID: 3174
	public abstract class SetRequestBase<TIdentity> : SetTaskBase<TransactionalRequestJob> where TIdentity : MRSRequestIdParameter
	{
		// Token: 0x06007909 RID: 30985 RVA: 0x001ED154 File Offset: 0x001EB354
		public SetRequestBase()
		{
			this.WriteableSession = null;
			this.RJProvider = null;
			this.UnreachableMrsServers = new List<string>(5);
			this.RequestName = null;
			this.ConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromRootOrgScopeSet(), 84, ".ctor", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\RequestBase\\SetRequestBase.cs");
		}

		// Token: 0x17002575 RID: 9589
		// (get) Token: 0x0600790A RID: 30986 RVA: 0x001ED1AD File Offset: 0x001EB3AD
		// (set) Token: 0x0600790B RID: 30987 RVA: 0x001ED1C4 File Offset: 0x001EB3C4
		public virtual TIdentity Identity
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

		// Token: 0x17002576 RID: 9590
		// (get) Token: 0x0600790C RID: 30988 RVA: 0x001ED1DC File Offset: 0x001EB3DC
		// (set) Token: 0x0600790D RID: 30989 RVA: 0x001ED1E4 File Offset: 0x001EB3E4
		[Parameter(Mandatory = false)]
		[ValidateNotNull]
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

		// Token: 0x17002577 RID: 9591
		// (get) Token: 0x0600790E RID: 30990 RVA: 0x001ED1ED File Offset: 0x001EB3ED
		// (set) Token: 0x0600790F RID: 30991 RVA: 0x001ED1F5 File Offset: 0x001EB3F5
		internal IRequestIndexEntry IndexEntry { get; set; }

		// Token: 0x17002578 RID: 9592
		// (get) Token: 0x06007910 RID: 30992 RVA: 0x001ED1FE File Offset: 0x001EB3FE
		// (set) Token: 0x06007911 RID: 30993 RVA: 0x001ED206 File Offset: 0x001EB406
		internal IRecipientSession GCSession { get; private set; }

		// Token: 0x17002579 RID: 9593
		// (get) Token: 0x06007912 RID: 30994 RVA: 0x001ED20F File Offset: 0x001EB40F
		// (set) Token: 0x06007913 RID: 30995 RVA: 0x001ED217 File Offset: 0x001EB417
		internal IRecipientSession WriteableSession { get; private set; }

		// Token: 0x1700257A RID: 9594
		// (get) Token: 0x06007914 RID: 30996 RVA: 0x001ED220 File Offset: 0x001EB420
		// (set) Token: 0x06007915 RID: 30997 RVA: 0x001ED228 File Offset: 0x001EB428
		internal IConfigurationSession CurrentOrgConfigSession { get; private set; }

		// Token: 0x1700257B RID: 9595
		// (get) Token: 0x06007916 RID: 30998 RVA: 0x001ED231 File Offset: 0x001EB431
		// (set) Token: 0x06007917 RID: 30999 RVA: 0x001ED239 File Offset: 0x001EB439
		internal ITopologyConfigurationSession ConfigSession { get; private set; }

		// Token: 0x1700257C RID: 9596
		// (get) Token: 0x06007918 RID: 31000 RVA: 0x001ED242 File Offset: 0x001EB442
		// (set) Token: 0x06007919 RID: 31001 RVA: 0x001ED24A File Offset: 0x001EB44A
		internal RequestJobProvider RJProvider { get; private set; }

		// Token: 0x1700257D RID: 9597
		// (get) Token: 0x0600791A RID: 31002 RVA: 0x001ED253 File Offset: 0x001EB453
		// (set) Token: 0x0600791B RID: 31003 RVA: 0x001ED25B File Offset: 0x001EB45B
		internal string RequestName { get; private set; }

		// Token: 0x1700257E RID: 9598
		// (get) Token: 0x0600791C RID: 31004 RVA: 0x001ED264 File Offset: 0x001EB464
		protected virtual RequestIndexId DefaultRequestIndexId
		{
			get
			{
				return new RequestIndexId(RequestIndexLocation.AD);
			}
		}

		// Token: 0x1700257F RID: 9599
		// (get) Token: 0x0600791D RID: 31005 RVA: 0x001ED26C File Offset: 0x001EB46C
		// (set) Token: 0x0600791E RID: 31006 RVA: 0x001ED274 File Offset: 0x001EB474
		private protected List<string> UnreachableMrsServers { protected get; private set; }

		// Token: 0x17002580 RID: 9600
		// (get) Token: 0x0600791F RID: 31007 RVA: 0x001ED27D File Offset: 0x001EB47D
		protected string ExecutingUserIdentity
		{
			get
			{
				return base.ExecutingUserIdentityName;
			}
		}

		// Token: 0x06007920 RID: 31008 RVA: 0x001ED288 File Offset: 0x001EB488
		internal void ValidateRequestIsActive(RequestJobBase requestJob)
		{
			if (requestJob == null || requestJob.Status == RequestStatus.None)
			{
				TIdentity identity = this.Identity;
				base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorCouldNotFindRequest(identity.ToString())), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (requestJob.ValidationResult != RequestJobBase.ValidationResultEnum.Valid)
			{
				TIdentity identity2 = this.Identity;
				base.WriteError(new InvalidRequestPermanentException(identity2.ToString(), requestJob.ValidationMessage), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007921 RID: 31009 RVA: 0x001ED325 File Offset: 0x001EB525
		internal void ValidateRequestProtectionStatus(RequestJobBase requestJob)
		{
			if (requestJob.Protect && RequestTaskHelper.CheckUserOrgIdIsTenant(base.ExecutingUserOrganizationId))
			{
				base.WriteError(new RequestIsProtectedPermanentException(requestJob.Name), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007922 RID: 31010 RVA: 0x001ED35C File Offset: 0x001EB55C
		internal void ValidateRequestIsRunnable(RequestJobBase requestJob)
		{
			if (requestJob.Status == RequestStatus.Completed || requestJob.Status == RequestStatus.CompletedWithWarning)
			{
				base.WriteError(new CannotModifyCompletedRequestPermanentException(requestJob.Name), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (requestJob.RehomeRequest)
			{
				base.WriteError(new CannotModifyRehomingRequestTransientException(requestJob.Name), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007923 RID: 31011 RVA: 0x001ED3BF File Offset: 0x001EB5BF
		internal void ValidateRequestIsNotCancelled(RequestJobBase requestJob)
		{
			if (requestJob.CancelRequest)
			{
				base.WriteError(new CannotSetCancelledRequestPermanentException(requestJob.Name), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007924 RID: 31012 RVA: 0x001ED3E6 File Offset: 0x001EB5E6
		protected void ValidateRequestType(TransactionalRequestJob requestJob)
		{
			if (requestJob.RequestType != SetRequestBase<TIdentity>.RequestType)
			{
				base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorNotEnoughInformationToFindRequestOfCorrectType), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007925 RID: 31013 RVA: 0x001ED411 File Offset: 0x001EB611
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.DataObject != null)
				{
					((IDisposable)this.DataObject).Dispose();
					this.DataObject = null;
				}
				if (this.RJProvider != null)
				{
					this.RJProvider.Dispose();
					this.RJProvider = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06007926 RID: 31014 RVA: 0x001ED451 File Offset: 0x001EB651
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			if (this.DataObject != null)
			{
				this.DataObject.Dispose();
				this.DataObject = null;
			}
		}

		// Token: 0x06007927 RID: 31015 RVA: 0x001ED474 File Offset: 0x001EB674
		protected override IConfigDataProvider CreateSession()
		{
			ADSessionSettings adsessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			if (MapiTaskHelper.IsDatacenter || MapiTaskHelper.IsDatacenterDedicated)
			{
				adsessionSettings.IncludeSoftDeletedObjects = true;
				adsessionSettings.IncludeInactiveMailbox = true;
			}
			this.CurrentOrgConfigSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, null, adsessionSettings, 375, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\RequestBase\\SetRequestBase.cs");
			adsessionSettings = ADSessionSettings.RescopeToSubtree(adsessionSettings);
			this.GCSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, adsessionSettings, 386, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\RequestBase\\SetRequestBase.cs");
			adsessionSettings.IncludeInactiveMailbox = true;
			this.WriteableSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, adsessionSettings, 394, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\RequestBase\\SetRequestBase.cs");
			if (base.CurrentTaskContext.CanBypassRBACScope)
			{
				this.WriteableSession.EnforceDefaultScope = false;
			}
			if (this.DataObject != null)
			{
				this.DataObject.Dispose();
				this.DataObject = null;
			}
			if (this.RJProvider != null)
			{
				this.RJProvider.Dispose();
				this.RJProvider = null;
			}
			this.RJProvider = new RequestJobProvider(this.WriteableSession, this.CurrentOrgConfigSession);
			return this.RJProvider;
		}

		// Token: 0x06007928 RID: 31016 RVA: 0x001ED5BC File Offset: 0x001EB7BC
		protected override IConfigurable PrepareDataObject()
		{
			TIdentity identity = this.Identity;
			if (identity.OrganizationId != null)
			{
				IDirectorySession writeableSession = this.WriteableSession;
				TIdentity identity2 = this.Identity;
				if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(writeableSession, identity2.OrganizationId))
				{
					IDirectorySession writeableSession2 = this.WriteableSession;
					TIdentity identity3 = this.Identity;
					this.WriteableSession = (IRecipientSession)TaskHelper.UnderscopeSessionToOrganization(writeableSession2, identity3.OrganizationId, true);
				}
				IDirectorySession currentOrgConfigSession = this.CurrentOrgConfigSession;
				TIdentity identity4 = this.Identity;
				if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(currentOrgConfigSession, identity4.OrganizationId))
				{
					IDirectorySession currentOrgConfigSession2 = this.CurrentOrgConfigSession;
					TIdentity identity5 = this.Identity;
					this.CurrentOrgConfigSession = (ITenantConfigurationSession)TaskHelper.UnderscopeSessionToOrganization(currentOrgConfigSession2, identity5.OrganizationId, true);
					this.RJProvider.IndexProvider.ConfigSession = this.CurrentOrgConfigSession;
				}
			}
			TIdentity identity6 = this.Identity;
			if (!string.IsNullOrEmpty(identity6.MailboxName))
			{
				IRecipientSession writeableSession3 = this.WriteableSession;
				IRecipientSession gcsession = this.GCSession;
				ADServerSettings serverSettings = base.ServerSettings;
				TIdentity identity7 = this.Identity;
				ADUser aduser = RequestTaskHelper.ResolveADUser(writeableSession3, gcsession, serverSettings, new UserIdParameter(identity7.MailboxName), base.OptionalIdentityData, this.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), true);
				if (aduser != null)
				{
					TIdentity identity8 = this.Identity;
					identity8.MailboxId = aduser.Id;
					if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(this.WriteableSession, aduser))
					{
						this.WriteableSession = (IRecipientSession)TaskHelper.UnderscopeSessionToOrganization(this.WriteableSession, aduser.OrganizationId, true);
					}
					if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(this.CurrentOrgConfigSession, aduser))
					{
						this.CurrentOrgConfigSession = (ITenantConfigurationSession)TaskHelper.UnderscopeSessionToOrganization(this.CurrentOrgConfigSession, aduser.OrganizationId, true);
						this.RJProvider.IndexProvider.ConfigSession = this.CurrentOrgConfigSession;
					}
				}
			}
			TIdentity identity9 = this.Identity;
			if (!string.IsNullOrEmpty(identity9.OrganizationName))
			{
				IConfigurationSession configurationSession = RequestTaskHelper.CreateOrganizationFindingSession(base.CurrentOrganizationId, base.ExecutingUserOrganizationId);
				TIdentity identity10 = this.Identity;
				IIdentityParameter id = new OrganizationIdParameter(identity10.OrganizationName);
				IConfigDataProvider session = configurationSession;
				ObjectId rootID = null;
				TIdentity identity11 = this.Identity;
				LocalizedString? notFoundError = new LocalizedString?(Strings.ErrorOrganizationNotFound(identity11.OrganizationName));
				TIdentity identity12 = this.Identity;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(id, session, rootID, notFoundError, new LocalizedString?(Strings.ErrorOrganizationNotUnique(identity12.OrganizationName)));
				if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(this.WriteableSession, adorganizationalUnit))
				{
					this.WriteableSession = (IRecipientSession)TaskHelper.UnderscopeSessionToOrganization(this.WriteableSession, adorganizationalUnit.OrganizationId, true);
				}
				if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(this.CurrentOrgConfigSession, adorganizationalUnit))
				{
					this.CurrentOrgConfigSession = (ITenantConfigurationSession)TaskHelper.UnderscopeSessionToOrganization(this.CurrentOrgConfigSession, adorganizationalUnit.OrganizationId, true);
					this.RJProvider.IndexProvider.ConfigSession = this.CurrentOrgConfigSession;
				}
			}
			TIdentity identity13 = this.Identity;
			identity13.SetDefaultIndex(this.DefaultRequestIndexId);
			this.IndexEntry = this.GetEntry();
			this.CheckIndexEntry();
			if (this.IndexEntry == null)
			{
				return null;
			}
			RequestJobObjectId requestJobId = this.IndexEntry.GetRequestJobId();
			if (this.IndexEntry.TargetUserId != null)
			{
				requestJobId.TargetUser = this.ResolveADUser(this.IndexEntry.TargetUserId);
			}
			if (this.IndexEntry.SourceUserId != null)
			{
				requestJobId.SourceUser = this.ResolveADUser(this.IndexEntry.SourceUserId);
			}
			return (TransactionalRequestJob)this.RJProvider.Read<TransactionalRequestJob>(requestJobId);
		}

		// Token: 0x06007929 RID: 31017 RVA: 0x001ED950 File Offset: 0x001EBB50
		protected virtual void CheckIndexEntry()
		{
			if (this.IndexEntry.SourceUserId == null && this.IndexEntry.TargetUserId == null)
			{
				base.WriteError(new ManagementObjectNotFoundException(Strings.RequestIndexEntryIsMissingLocalUserData(this.IndexEntry.Name)), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x0600792A RID: 31018 RVA: 0x001ED9A0 File Offset: 0x001EBBA0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			try
			{
				using (new ADSessionSettingsFactory.InactiveMailboxVisibilityEnabler())
				{
					base.InternalValidate();
				}
				TransactionalRequestJob dataObject = this.DataObject;
				this.RequestName = dataObject.Name;
				this.ValidateRequest(dataObject);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x0600792B RID: 31019 RVA: 0x001EDA1C File Offset: 0x001EBC1C
		protected virtual void ValidateRequest(TransactionalRequestJob requestJob)
		{
			this.ValidateRequestType(requestJob);
		}

		// Token: 0x0600792C RID: 31020 RVA: 0x001EDA40 File Offset: 0x001EBC40
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			try
			{
				base.WriteVerbose(Strings.SettingRequest);
				TransactionalRequestJob dataObject = this.DataObject;
				int num = 1;
				for (;;)
				{
					if (dataObject.CheckIfUnderlyingMessageHasChanged())
					{
						base.WriteVerbose(Strings.ReloadingRequest);
						dataObject.Refresh();
						this.ValidateRequest(dataObject);
					}
					this.ModifyRequest(dataObject);
					try
					{
						base.InternalProcessRecord();
						RequestJobLog.Write(dataObject);
					}
					catch (MapiExceptionObjectChanged)
					{
						if (num >= 5 || base.Stopping)
						{
							throw;
						}
						num++;
						continue;
					}
					break;
				}
				CommonUtils.CatchKnownExceptions(delegate
				{
					this.PostSaveAction();
				}, delegate(Exception ex)
				{
					this.WriteWarning(MrsStrings.PostSaveActionFailed(CommonUtils.FullExceptionMessage(ex)));
				});
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x0600792D RID: 31021 RVA: 0x001EDB14 File Offset: 0x001EBD14
		protected virtual void ModifyRequest(TransactionalRequestJob requestJob)
		{
		}

		// Token: 0x0600792E RID: 31022 RVA: 0x001EDB16 File Offset: 0x001EBD16
		protected virtual void PostSaveAction()
		{
		}

		// Token: 0x0600792F RID: 31023 RVA: 0x001EDB18 File Offset: 0x001EBD18
		protected override bool IsKnownException(Exception exception)
		{
			return RequestTaskHelper.IsKnownExceptionHandler(exception, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose)) || base.IsKnownException(exception);
		}

		// Token: 0x06007930 RID: 31024 RVA: 0x001EDB38 File Offset: 0x001EBD38
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

		// Token: 0x06007931 RID: 31025 RVA: 0x001EDB64 File Offset: 0x001EBD64
		protected virtual ADUser ResolveADUser(ADObjectId userId)
		{
			return RequestTaskHelper.ResolveADUser(this.WriteableSession, this.GCSession, base.ServerSettings, new MailboxOrMailUserIdParameter(userId), base.OptionalIdentityData, this.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), true);
		}

		// Token: 0x06007932 RID: 31026 RVA: 0x001EDBC4 File Offset: 0x001EBDC4
		protected bool IsFieldSet(string fieldName)
		{
			return base.Fields.IsChanged(fieldName) || base.Fields.IsModified(fieldName);
		}

		// Token: 0x06007933 RID: 31027 RVA: 0x001EDBE4 File Offset: 0x001EBDE4
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
				IConfigDataProvider indexProvider = this.RJProvider.IndexProvider;
				IConfigurationSession currentOrgConfigSession = this.CurrentOrgConfigSession;
				TIdentity identity4 = this.Identity;
				return (IRequestIndexEntry)base.GetDataObject<MRSRequestWrapper>(id, indexProvider, ADHandler.GetRootId(currentOrgConfigSession, identity4.RequestType), notFoundError, multipleFoundError);
			}
			TIdentity identity5 = this.Identity;
			if (identity5.IndexToUse.RequestIndexEntryType == typeof(MRSRequestMailboxEntry))
			{
				IIdentityParameter id2 = this.Identity;
				IConfigDataProvider indexProvider2 = this.RJProvider.IndexProvider;
				IConfigurationSession currentOrgConfigSession2 = this.CurrentOrgConfigSession;
				TIdentity identity6 = this.Identity;
				return (IRequestIndexEntry)base.GetDataObject<MRSRequestMailboxEntry>(id2, indexProvider2, ADHandler.GetRootId(currentOrgConfigSession2, identity6.RequestType), notFoundError, multipleFoundError);
			}
			TIdentity identity7 = this.Identity;
			base.WriteError(new UnknownRequestIndexPermanentException(identity7.IndexToUse.ToString()), ErrorCategory.InvalidArgument, this.Identity);
			return null;
		}

		// Token: 0x04003C3F RID: 15423
		public const string ParameterIdentity = "Identity";

		// Token: 0x04003C40 RID: 15424
		private static readonly MRSRequestType RequestType = MRSRequestIdParameter.GetRequestType<TIdentity>();
	}
}
