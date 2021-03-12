using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C7A RID: 3194
	public abstract class SetMoveRequestBase : SetTaskBase<TransactionalRequestJob>
	{
		// Token: 0x06007A84 RID: 31364 RVA: 0x001F5CC8 File Offset: 0x001F3EC8
		public SetMoveRequestBase()
		{
			this.LocalADUser = null;
			this.WriteableSession = null;
			this.MRProvider = null;
			this.UnreachableMrsServers = new List<string>(5);
			this.ConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromRootOrgScopeSet(), 82, ".ctor", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\MoveRequest\\SetMoveRequestBase.cs");
		}

		// Token: 0x170025F7 RID: 9719
		// (get) Token: 0x06007A85 RID: 31365 RVA: 0x001F5D21 File Offset: 0x001F3F21
		// (set) Token: 0x06007A86 RID: 31366 RVA: 0x001F5D38 File Offset: 0x001F3F38
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public MoveRequestIdParameter Identity
		{
			get
			{
				return (MoveRequestIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x170025F8 RID: 9720
		// (get) Token: 0x06007A87 RID: 31367 RVA: 0x001F5D4B File Offset: 0x001F3F4B
		// (set) Token: 0x06007A88 RID: 31368 RVA: 0x001F5D53 File Offset: 0x001F3F53
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

		// Token: 0x170025F9 RID: 9721
		// (get) Token: 0x06007A89 RID: 31369 RVA: 0x001F5D5C File Offset: 0x001F3F5C
		// (set) Token: 0x06007A8A RID: 31370 RVA: 0x001F5D64 File Offset: 0x001F3F64
		internal ADUser LocalADUser { get; set; }

		// Token: 0x170025FA RID: 9722
		// (get) Token: 0x06007A8B RID: 31371 RVA: 0x001F5D6D File Offset: 0x001F3F6D
		// (set) Token: 0x06007A8C RID: 31372 RVA: 0x001F5D75 File Offset: 0x001F3F75
		internal IRecipientSession GCSession { get; private set; }

		// Token: 0x170025FB RID: 9723
		// (get) Token: 0x06007A8D RID: 31373 RVA: 0x001F5D7E File Offset: 0x001F3F7E
		// (set) Token: 0x06007A8E RID: 31374 RVA: 0x001F5D86 File Offset: 0x001F3F86
		internal IRecipientSession WriteableSession { get; private set; }

		// Token: 0x170025FC RID: 9724
		// (get) Token: 0x06007A8F RID: 31375 RVA: 0x001F5D8F File Offset: 0x001F3F8F
		// (set) Token: 0x06007A90 RID: 31376 RVA: 0x001F5D97 File Offset: 0x001F3F97
		internal ITopologyConfigurationSession ConfigSession { get; private set; }

		// Token: 0x170025FD RID: 9725
		// (get) Token: 0x06007A91 RID: 31377 RVA: 0x001F5DA0 File Offset: 0x001F3FA0
		// (set) Token: 0x06007A92 RID: 31378 RVA: 0x001F5DA8 File Offset: 0x001F3FA8
		internal RequestJobProvider MRProvider { get; private set; }

		// Token: 0x170025FE RID: 9726
		// (get) Token: 0x06007A93 RID: 31379 RVA: 0x001F5DB1 File Offset: 0x001F3FB1
		// (set) Token: 0x06007A94 RID: 31380 RVA: 0x001F5DB9 File Offset: 0x001F3FB9
		internal ADSessionSettings SessionSettings { get; private set; }

		// Token: 0x170025FF RID: 9727
		// (get) Token: 0x06007A95 RID: 31381 RVA: 0x001F5DC2 File Offset: 0x001F3FC2
		// (set) Token: 0x06007A96 RID: 31382 RVA: 0x001F5DCA File Offset: 0x001F3FCA
		private protected List<string> UnreachableMrsServers { protected get; private set; }

		// Token: 0x17002600 RID: 9728
		// (get) Token: 0x06007A97 RID: 31383 RVA: 0x001F5DD3 File Offset: 0x001F3FD3
		protected string ExecutingUserIdentity
		{
			get
			{
				return base.ExecutingUserIdentityName;
			}
		}

		// Token: 0x06007A98 RID: 31384 RVA: 0x001F5DDC File Offset: 0x001F3FDC
		internal static LocalizedException TranslateExceptionHandler(Exception e)
		{
			if (e is LocalizedException)
			{
				if (!(e is MapiRetryableException))
				{
					if (!(e is MapiPermanentException))
					{
						goto IL_42;
					}
				}
				try
				{
					LocalizedException ex = StorageGlobals.TranslateMapiException(Strings.UnableToCommunicate, (LocalizedException)e, null, null, string.Empty, new object[0]);
					if (ex != null)
					{
						return ex;
					}
				}
				catch (ArgumentException)
				{
				}
			}
			IL_42:
			return null;
		}

		// Token: 0x06007A99 RID: 31385 RVA: 0x001F5E40 File Offset: 0x001F4040
		internal static bool IsKnownExceptionHandler(Exception exception, WriteVerboseDelegate writeVerbose)
		{
			if (exception is MapiRetryableException || exception is MapiPermanentException)
			{
				return true;
			}
			if (exception is MailboxReplicationPermanentException || exception is MailboxReplicationTransientException)
			{
				writeVerbose(CommonUtils.FullExceptionMessage(exception));
				return true;
			}
			return false;
		}

		// Token: 0x06007A9A RID: 31386 RVA: 0x001F5E73 File Offset: 0x001F4073
		internal static bool CheckUserOrgIdIsTenant(OrganizationId userOrgId)
		{
			return !userOrgId.Equals(OrganizationId.ForestWideOrgId);
		}

		// Token: 0x06007A9B RID: 31387 RVA: 0x001F5E84 File Offset: 0x001F4084
		internal void ValidateMoveRequestIsActive(RequestJobBase moveRequest)
		{
			if (moveRequest == null || moveRequest.Status == RequestStatus.None)
			{
				base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorUserNotBeingMoved(this.DataObject.ToString())), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (moveRequest.ValidationResult != RequestJobBase.ValidationResultEnum.Valid)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorInvalidMoveRequest(this.LocalADUser.ToString(), moveRequest.ValidationMessage.ToString())), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A9C RID: 31388 RVA: 0x001F5F13 File Offset: 0x001F4113
		internal void ValidateMoveRequestProtectionStatus(RequestJobBase moveRequest)
		{
			if (moveRequest.Protect && SetMoveRequestBase.CheckUserOrgIdIsTenant(base.ExecutingUserOrganizationId))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorMoveRequestIsProtected(this.DataObject.ToString())), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A9D RID: 31389 RVA: 0x001F5F4C File Offset: 0x001F414C
		internal void ValidateMoveRequestIsSettable(RequestJobBase moveRequest)
		{
			if (moveRequest.Status == RequestStatus.Completed || moveRequest.Status == RequestStatus.CompletedWithWarning)
			{
				base.WriteError(new CannotModifyCompletedRequestPermanentException(this.LocalADUser.ToString()), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (moveRequest.CancelRequest)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorMoveAlreadyCanceled(this.LocalADUser.ToString())), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A9E RID: 31390 RVA: 0x001F5FB4 File Offset: 0x001F41B4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.DataObject != null)
				{
					((IDisposable)this.DataObject).Dispose();
					this.DataObject = null;
				}
				if (this.MRProvider != null)
				{
					this.MRProvider.Dispose();
					this.MRProvider = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06007A9F RID: 31391 RVA: 0x001F5FF4 File Offset: 0x001F41F4
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			this.LocalADUser = null;
			if (this.DataObject != null)
			{
				this.DataObject.Dispose();
				this.DataObject = null;
			}
		}

		// Token: 0x06007AA0 RID: 31392 RVA: 0x001F6020 File Offset: 0x001F4220
		protected override IConfigDataProvider CreateSession()
		{
			ADObjectId rootOrgContainerId = ADSystemConfigurationSession.GetRootOrgContainerId(this.DomainController, null);
			this.SessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, rootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			ADSessionSettings adsessionSettings = ADSessionSettings.RescopeToSubtree(this.SessionSettings);
			if (MapiTaskHelper.IsDatacenter || MapiTaskHelper.IsDatacenterDedicated)
			{
				adsessionSettings.IncludeSoftDeletedObjects = true;
			}
			this.GCSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, adsessionSettings, 413, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\MoveRequest\\SetMoveRequestBase.cs");
			this.WriteableSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, adsessionSettings, 419, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\MoveRequest\\SetMoveRequestBase.cs");
			if (base.CurrentTaskContext.CanBypassRBACScope)
			{
				this.WriteableSession.EnforceDefaultScope = false;
			}
			if (this.DataObject != null)
			{
				this.DataObject.Dispose();
				this.DataObject = null;
			}
			if (this.MRProvider != null)
			{
				this.MRProvider.Dispose();
				this.MRProvider = null;
			}
			this.MRProvider = new RequestJobProvider(this.WriteableSession, this.ConfigSession);
			return this.MRProvider;
		}

		// Token: 0x06007AA1 RID: 31393 RVA: 0x001F6144 File Offset: 0x001F4344
		protected override IConfigurable PrepareDataObject()
		{
			this.LocalADUser = (ADUser)RecipientTaskHelper.ResolveDataObject<ADUser>(this.WriteableSession, this.GCSession, base.ServerSettings, this.Identity, null, base.OptionalIdentityData, this.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
			this.CheckADUser();
			if (this.LocalADUser == null)
			{
				return null;
			}
			this.WriteableSession = (IRecipientSession)TaskHelper.UnderscopeSessionToOrganization(this.WriteableSession, this.LocalADUser.OrganizationId, true);
			base.CurrentOrganizationId = this.LocalADUser.OrganizationId;
			this.MRProvider.IndexProvider.RecipientSession = this.WriteableSession;
			return (TransactionalRequestJob)this.MRProvider.Read<TransactionalRequestJob>(new RequestJobObjectId(this.LocalADUser));
		}

		// Token: 0x06007AA2 RID: 31394 RVA: 0x001F6223 File Offset: 0x001F4423
		protected virtual void CheckADUser()
		{
			if (this.LocalADUser.MailboxMoveStatus == RequestStatus.None)
			{
				base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorUserNotBeingMoved(this.LocalADUser.ToString())), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007AA3 RID: 31395 RVA: 0x001F6254 File Offset: 0x001F4454
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			try
			{
				base.InternalValidate();
				TransactionalRequestJob dataObject = this.DataObject;
				this.ValidateMoveRequest(dataObject);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007AA4 RID: 31396 RVA: 0x001F62A4 File Offset: 0x001F44A4
		protected virtual void ValidateMoveRequest(TransactionalRequestJob moveRequest)
		{
		}

		// Token: 0x06007AA5 RID: 31397 RVA: 0x001F62C4 File Offset: 0x001F44C4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			try
			{
				base.WriteVerbose(Strings.SettingMoveRequest);
				TransactionalRequestJob dataObject = this.DataObject;
				int num = 1;
				for (;;)
				{
					if (dataObject.CheckIfUnderlyingMessageHasChanged())
					{
						base.WriteVerbose(Strings.ReloadingMoveRequest);
						dataObject.Refresh();
						this.ValidateMoveRequest(dataObject);
					}
					this.ModifyMoveRequest(dataObject);
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

		// Token: 0x06007AA6 RID: 31398 RVA: 0x001F6398 File Offset: 0x001F4598
		protected virtual void ModifyMoveRequest(TransactionalRequestJob moveRequest)
		{
		}

		// Token: 0x06007AA7 RID: 31399 RVA: 0x001F639A File Offset: 0x001F459A
		protected virtual void PostSaveAction()
		{
		}

		// Token: 0x06007AA8 RID: 31400 RVA: 0x001F639C File Offset: 0x001F459C
		protected override bool IsKnownException(Exception exception)
		{
			return SetMoveRequestBase.IsKnownExceptionHandler(exception, new WriteVerboseDelegate(base.WriteVerbose)) || base.IsKnownException(exception);
		}

		// Token: 0x06007AA9 RID: 31401 RVA: 0x001F63BC File Offset: 0x001F45BC
		protected override void TranslateException(ref Exception e, out ErrorCategory category)
		{
			LocalizedException ex = SetMoveRequestBase.TranslateExceptionHandler(e);
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

		// Token: 0x06007AAA RID: 31402 RVA: 0x001F63E8 File Offset: 0x001F45E8
		protected bool IsFieldSet(string fieldName)
		{
			return base.Fields.IsChanged(fieldName) || base.Fields.IsModified(fieldName);
		}

		// Token: 0x04003CAA RID: 15530
		public const string TaskNoun = "MoveRequest";

		// Token: 0x04003CAB RID: 15531
		public const string ParameterIdentity = "Identity";
	}
}
