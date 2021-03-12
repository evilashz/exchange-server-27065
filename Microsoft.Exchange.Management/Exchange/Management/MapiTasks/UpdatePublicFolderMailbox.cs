using System;
using System.Collections;
using System.Globalization;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Assistants;

namespace Microsoft.Exchange.Management.MapiTasks
{
	// Token: 0x02000495 RID: 1173
	[Cmdlet("Update", "PublicFolderMailbox", SupportsShouldProcess = true, DefaultParameterSetName = "InvokeMailboxAssistant")]
	public sealed class UpdatePublicFolderMailbox : RecipientObjectActionTask<MailboxIdParameter, ADRecipient>
	{
		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x060029C2 RID: 10690 RVA: 0x000A5D6A File Offset: 0x000A3F6A
		// (set) Token: 0x060029C3 RID: 10691 RVA: 0x000A5D72 File Offset: 0x000A3F72
		[Parameter(Mandatory = true, ParameterSetName = "InvokeMailboxAssistant", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[Parameter(Mandatory = true, ParameterSetName = "InvokeSynchronizer", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override MailboxIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x060029C4 RID: 10692 RVA: 0x000A5D7B File Offset: 0x000A3F7B
		// (set) Token: 0x060029C5 RID: 10693 RVA: 0x000A5DA1 File Offset: 0x000A3FA1
		[Parameter(Mandatory = false, ParameterSetName = "InvokeSynchronizer")]
		public SwitchParameter InvokeSynchronizer
		{
			get
			{
				return (SwitchParameter)(base.Fields["InvokeSynchronizer"] ?? false);
			}
			set
			{
				base.Fields["InvokeSynchronizer"] = value;
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x060029C6 RID: 10694 RVA: 0x000A5DB9 File Offset: 0x000A3FB9
		// (set) Token: 0x060029C7 RID: 10695 RVA: 0x000A5DDF File Offset: 0x000A3FDF
		[Parameter(Mandatory = false, ParameterSetName = "InvokeSynchronizer")]
		public SwitchParameter FullSync
		{
			get
			{
				return (SwitchParameter)(base.Fields["InvokeFullSync"] ?? false);
			}
			set
			{
				base.Fields["InvokeFullSync"] = value;
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x000A5DF7 File Offset: 0x000A3FF7
		// (set) Token: 0x060029C9 RID: 10697 RVA: 0x000A5E1D File Offset: 0x000A401D
		[Parameter(Mandatory = false, ParameterSetName = "InvokeSynchronizer")]
		public SwitchParameter SuppressStatus
		{
			get
			{
				return (SwitchParameter)(base.Fields["SuppressStatus"] ?? false);
			}
			set
			{
				base.Fields["SuppressStatus"] = value;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x060029CA RID: 10698 RVA: 0x000A5E35 File Offset: 0x000A4035
		// (set) Token: 0x060029CB RID: 10699 RVA: 0x000A5E5B File Offset: 0x000A405B
		[Parameter(Mandatory = false, ParameterSetName = "InvokeSynchronizer")]
		public SwitchParameter ReconcileFolders
		{
			get
			{
				return (SwitchParameter)(base.Fields["ReconcileFolders"] ?? false);
			}
			set
			{
				base.Fields["ReconcileFolders"] = value;
			}
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x000A5E73 File Offset: 0x000A4073
		public UpdatePublicFolderMailbox()
		{
			if (ExEnvironment.IsTest)
			{
				UpdatePublicFolderMailbox.timeToWaitInMilliseconds = 600000;
				return;
			}
			UpdatePublicFolderMailbox.timeToWaitInMilliseconds = 60000;
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x060029CD RID: 10701 RVA: 0x000A5E97 File Offset: 0x000A4097
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageUpdatePublicFolderMailbox(this.Identity.ToString());
			}
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x000A5EA9 File Offset: 0x000A40A9
		protected override bool IsKnownException(Exception exception)
		{
			return exception is PublicFolderSyncTransientException || exception is PublicFolderSyncPermanentException || base.IsKnownException(exception);
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x000A5EC4 File Offset: 0x000A40C4
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			if (this.FullSync && !this.InvokeSynchronizer)
			{
				base.ThrowTerminatingError(new TaskArgumentException(Strings.ErrorInvalidMandatoryParameterForPublicFolderTasks("InvokeFullSync", "InvokeSynchronizer")), ExchangeErrorCategory.Client, null);
			}
			else if (this.SuppressStatus && !this.InvokeSynchronizer)
			{
				base.ThrowTerminatingError(new TaskArgumentException(Strings.ErrorInvalidMandatoryParameterForPublicFolderTasks("SuppressStatus", "InvokeSynchronizer")), ExchangeErrorCategory.Client, null);
			}
			else if (this.ReconcileFolders && !this.InvokeSynchronizer)
			{
				base.ThrowTerminatingError(new TaskArgumentException(Strings.ErrorInvalidMandatoryParameterForPublicFolderTasks("ReconcileFolders", "InvokeSynchronizer")), ExchangeErrorCategory.Client, null);
			}
			base.InternalBeginProcessing();
			TaskLogger.LogExit();
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x000A5F94 File Offset: 0x000A4194
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				ADUser aduser = this.DataObject as ADUser;
				if (aduser == null || aduser.RecipientTypeDetails != RecipientTypeDetails.PublicFolderMailbox)
				{
					base.WriteError(new ObjectNotFoundException(Strings.PublicFolderMailboxNotFound), ExchangeErrorCategory.Client, aduser);
				}
				ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(base.SessionSettings, aduser);
				string serverFqdn = exchangePrincipal.MailboxInfo.Location.ServerFqdn;
				if (this.InvokeSynchronizer)
				{
					TenantPublicFolderConfiguration value = TenantPublicFolderConfigurationCache.Instance.GetValue(aduser.OrganizationId);
					Organization orgContainer = this.ConfigurationSession.GetOrgContainer();
					if (orgContainer.DefaultPublicFolderMailbox.HierarchyMailboxGuid != value.GetHierarchyMailboxInformation().HierarchyMailboxGuid || value.GetLocalMailboxRecipient(aduser.ExchangeGuid) == null)
					{
						TenantPublicFolderConfigurationCache.Instance.RemoveValue(aduser.OrganizationId);
					}
					if (aduser.ExchangeGuid == value.GetHierarchyMailboxInformation().HierarchyMailboxGuid)
					{
						base.WriteError(new TaskArgumentException(Strings.ErrorSecondaryMailboxIdRequired), ExchangeErrorCategory.Client, exchangePrincipal);
					}
					if (this.FullSync)
					{
						using (PublicFolderSession publicFolderSession = PublicFolderSession.OpenAsAdmin(this.DataObject.OrganizationId, null, aduser.ExchangeGuid, null, CultureInfo.CurrentCulture, "Client=Management;Action=UpdatePublicFolderMailbox", null))
						{
							using (Folder folder = Folder.Bind(publicFolderSession, publicFolderSession.GetTombstonesRootFolderId()))
							{
								using (UserConfiguration configuration = UserConfiguration.GetConfiguration(folder, new UserConfigurationName("PublicFolderSyncInfo", ConfigurationNameKind.Name), UserConfigurationTypes.Dictionary))
								{
									IDictionary dictionary = configuration.GetDictionary();
									dictionary["SyncState"] = null;
									configuration.Save();
								}
							}
						}
					}
					PublicFolderSyncJobState publicFolderSyncJobState = PublicFolderSyncJobRpc.StartSyncHierarchy(exchangePrincipal, this.ReconcileFolders);
					if (!this.SuppressStatus)
					{
						base.WriteObject(new UpdatePublicFolderMailboxResult(Strings.StatusMessageStartUpdatePublicFolderMailbox(this.Identity.ToString())));
						int num = 0;
						while (publicFolderSyncJobState.JobStatus != PublicFolderSyncJobState.Status.Completed && publicFolderSyncJobState.LastError == null && num++ < UpdatePublicFolderMailbox.timeToWaitInMilliseconds / UpdatePublicFolderMailbox.QueryIntervalInMilliseconds)
						{
							base.WriteObject(new UpdatePublicFolderMailboxResult(Strings.StatusMessageUpdatePublicFolderMailboxUnderProgress(publicFolderSyncJobState.JobStatus.ToString())));
							Thread.Sleep(UpdatePublicFolderMailbox.QueryIntervalInMilliseconds);
							publicFolderSyncJobState = PublicFolderSyncJobRpc.QueryStatusSyncHierarchy(exchangePrincipal);
						}
						if (publicFolderSyncJobState.LastError != null)
						{
							base.WriteError(publicFolderSyncJobState.LastError, ExchangeErrorCategory.ServerOperation, publicFolderSyncJobState);
						}
						if (publicFolderSyncJobState.JobStatus == PublicFolderSyncJobState.Status.Completed)
						{
							base.WriteObject(new UpdatePublicFolderMailboxResult(Strings.StatusMessageUpdatePublicFolderMailboxCompleted));
						}
						else
						{
							base.WriteObject(new UpdatePublicFolderMailboxResult(Strings.StatusMessageSynchronizerRunningInBackground));
						}
					}
				}
				else if (aduser.ExchangeGuid != Guid.Empty)
				{
					MailboxDatabase mailboxDatabase = base.GlobalConfigSession.Read<MailboxDatabase>(aduser.Database);
					if (mailboxDatabase == null)
					{
						base.WriteError(new TaskArgumentException(Strings.ElcMdbNotFound(this.Identity.ToString())), ExchangeErrorCategory.Client, null);
					}
					this.EnsureMailboxExistsOnDatabase(aduser.ExchangeGuid);
					AssistantsRpcClient assistantsRpcClient = new AssistantsRpcClient(serverFqdn);
					try
					{
						assistantsRpcClient.Start("PublicFolderAssistant", aduser.ExchangeGuid, mailboxDatabase.Guid);
					}
					catch (RpcException ex)
					{
						base.WriteError(new TaskException(RpcUtility.MapRpcErrorCodeToMessage(ex.ErrorCode, serverFqdn)), ExchangeErrorCategory.Client, null);
					}
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x000A6350 File Offset: 0x000A4550
		private void EnsureMailboxExistsOnDatabase(Guid mailboxGuid)
		{
			TenantPublicFolderConfiguration value = TenantPublicFolderConfigurationCache.Instance.GetValue(this.DataObject.OrganizationId);
			if (value.GetLocalMailboxRecipient(mailboxGuid) == null)
			{
				TenantPublicFolderConfigurationCache.Instance.RemoveValue(this.DataObject.OrganizationId);
			}
			using (PublicFolderSession.OpenAsAdmin(this.DataObject.OrganizationId, null, mailboxGuid, null, CultureInfo.CurrentCulture, "Client=Management;Action=UpdatePublicFolderMailbox", null))
			{
			}
		}

		// Token: 0x04001E71 RID: 7793
		private const string SyncState = "SyncState";

		// Token: 0x04001E72 RID: 7794
		private const string ParameterSetSynchronizer = "InvokeSynchronizer";

		// Token: 0x04001E73 RID: 7795
		private const string InvokeSynchronizerField = "InvokeSynchronizer";

		// Token: 0x04001E74 RID: 7796
		private const string ParameterSetAssistant = "InvokeMailboxAssistant";

		// Token: 0x04001E75 RID: 7797
		private const string InvokeFullSyncField = "InvokeFullSync";

		// Token: 0x04001E76 RID: 7798
		private const string SuppressStatusField = "SuppressStatus";

		// Token: 0x04001E77 RID: 7799
		private const string ReconcileFoldersField = "ReconcileFolders";

		// Token: 0x04001E78 RID: 7800
		private static int timeToWaitInMilliseconds;

		// Token: 0x04001E79 RID: 7801
		private static int QueryIntervalInMilliseconds = 3000;
	}
}
