using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Assistants;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000321 RID: 801
	[Cmdlet("Start", "ManagedFolderAssistant", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public class StartElcAssistant : RecipientObjectActionTask<MailboxOrMailUserIdParameter, ADUser>
	{
		// Token: 0x06001B31 RID: 6961 RVA: 0x00078B24 File Offset: 0x00076D24
		public StartElcAssistant()
		{
			this.InternalStateResetAction = new Action(base.InternalStateReset);
			this.InactiveMailboxEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.InactiveMailbox.Enabled;
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x00078B66 File Offset: 0x00076D66
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageStartELCAssistant;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x00078B6D File Offset: 0x00076D6D
		// (set) Token: 0x06001B34 RID: 6964 RVA: 0x00078B84 File Offset: 0x00076D84
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true)]
		public override MailboxOrMailUserIdParameter Identity
		{
			get
			{
				return (MailboxOrMailUserIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x00078B97 File Offset: 0x00076D97
		// (set) Token: 0x06001B36 RID: 6966 RVA: 0x00078BBD File Offset: 0x00076DBD
		[Parameter]
		public SwitchParameter HoldCleanup
		{
			get
			{
				return (SwitchParameter)(base.Fields["HoldCleanup"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["HoldCleanup"] = value;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x00078BD5 File Offset: 0x00076DD5
		// (set) Token: 0x06001B38 RID: 6968 RVA: 0x00078BFB File Offset: 0x00076DFB
		[Parameter]
		public SwitchParameter EHAHiddenFolderCleanup
		{
			get
			{
				return (SwitchParameter)(base.Fields["EHAHiddenFolderCleanup"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["EHAHiddenFolderCleanup"] = value;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x00078C13 File Offset: 0x00076E13
		// (set) Token: 0x06001B3A RID: 6970 RVA: 0x00078C39 File Offset: 0x00076E39
		[Parameter]
		public SwitchParameter InactiveMailbox
		{
			get
			{
				return (SwitchParameter)(base.Fields["InactiveMailbox"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["InactiveMailbox"] = value;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x00078C51 File Offset: 0x00076E51
		// (set) Token: 0x06001B3C RID: 6972 RVA: 0x00078C59 File Offset: 0x00076E59
		internal bool InactiveMailboxEnabled { get; set; }

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x00078C62 File Offset: 0x00076E62
		// (set) Token: 0x06001B3E RID: 6974 RVA: 0x00078C6A File Offset: 0x00076E6A
		internal Action InternalStateResetAction { get; set; }

		// Token: 0x06001B3F RID: 6975 RVA: 0x00078C74 File Offset: 0x00076E74
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			if (this.InactiveMailboxEnabled && this.InactiveMailbox)
			{
				using (new ADSessionSettingsFactory.InactiveMailboxVisibilityEnabler())
				{
					this.InternalStateResetAction();
				}
				base.OptionalIdentityData.AdditionalFilter = StartElcAssistant.InactiveMailboxFilter;
			}
			else
			{
				this.InternalStateResetAction();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x00078CEC File Offset: 0x00076EEC
		internal override IConfigurationSession CreateConfigurationSession(ADSessionSettings sessionSettings)
		{
			if (sessionSettings.ConfigScopes == ConfigScopes.TenantLocal)
			{
				return base.CreateConfigurationSession(ADSessionSettings.RescopeToSubtree(sessionSettings));
			}
			return base.CreateConfigurationSession(sessionSettings);
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x00078D0B File Offset: 0x00076F0B
		internal override IRecipientSession CreateTenantGlobalCatalogSession(ADSessionSettings sessionSettings)
		{
			if (sessionSettings.ConfigScopes == ConfigScopes.TenantLocal)
			{
				return base.CreateTenantGlobalCatalogSession(ADSessionSettings.RescopeToSubtree(sessionSettings));
			}
			return base.CreateTenantGlobalCatalogSession(sessionSettings);
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x00078D2C File Offset: 0x00076F2C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			string mailboxAddress = this.Identity.ToString();
			ADUser dataObject = this.DataObject;
			ELCTaskHelper.VerifyIsInScopes(dataObject, base.ScopeSet, new Task.TaskErrorLoggingDelegate(base.WriteError));
			if (dataObject.ExchangeGuid != Guid.Empty && dataObject.RecipientType != RecipientType.MailUser && dataObject.Database != null)
			{
				MailboxDatabase mailboxDatabase = base.GlobalConfigSession.Read<MailboxDatabase>(dataObject.Database);
				if (mailboxDatabase == null)
				{
					base.WriteError(new ArgumentException(Strings.ElcMdbNotFound(mailboxAddress), "Mailbox"), ErrorCategory.InvalidArgument, null);
				}
				this.InternalProcessOneMailbox(dataObject.ExchangeGuid, mailboxDatabase.Guid);
			}
			if (dataObject.ArchiveState == ArchiveState.Local)
			{
				ADObjectId entryId = dataObject.ArchiveDatabase ?? dataObject.Database;
				MailboxDatabase mailboxDatabase2 = base.GlobalConfigSession.Read<MailboxDatabase>(entryId);
				if (mailboxDatabase2 == null)
				{
					base.WriteError(new ArgumentException(Strings.ElcMdbNotFound(mailboxAddress), "Archive Mailbox"), ErrorCategory.InvalidArgument, null);
				}
				if (StoreRetentionPolicyTagHelper.HasOnPremArchiveMailbox(dataObject))
				{
					try
					{
						StoreRetentionPolicyTagHelper.SyncOptionalTagsFromPrimaryToArchive(dataObject);
					}
					catch (ElcUserConfigurationException)
					{
						this.WriteWarning(Strings.WarningArchiveMailboxNotReachable(mailboxAddress));
					}
				}
				this.InternalProcessOneMailbox(dataObject.ArchiveGuid, mailboxDatabase2.Guid);
			}
			if (!this.processed)
			{
				this.WriteWarning(Strings.ElcNoLocalMbxOrArchive(mailboxAddress));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x00078E74 File Offset: 0x00077074
		private void InternalProcessOneMailbox(Guid mailboxGuid, Guid mdbGuid)
		{
			ExchangePrincipal exchangePrincipal = null;
			try
			{
				ADSessionSettings orgWideSessionSettings = base.OrgWideSessionSettings;
				if (VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.InactiveMailbox.Enabled)
				{
					orgWideSessionSettings.IncludeInactiveMailbox = true;
				}
				exchangePrincipal = ExchangePrincipal.FromMailboxGuid(orgWideSessionSettings, mailboxGuid, null);
			}
			catch (ObjectNotFoundException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, this.Identity);
			}
			string serverFqdn = exchangePrincipal.MailboxInfo.Location.ServerFqdn;
			AssistantsRpcClient client = new AssistantsRpcClient(serverFqdn);
			this.InternalProcessOneRequest(client, serverFqdn, mailboxGuid, mdbGuid);
			this.processed = true;
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x00078F04 File Offset: 0x00077104
		private void InternalProcessOneRequest(AssistantsRpcClient client, string serverName, Guid mailboxGuid, Guid mdbGuid)
		{
			int num = 3;
			try
			{
				IL_02:
				client.StartWithParams("ElcAssistant", mailboxGuid, mdbGuid, this.GetElcParameters().ToString());
			}
			catch (RpcException ex)
			{
				num--;
				if ((ex.ErrorCode == 1753 || ex.ErrorCode == 1727) && num > 0)
				{
					goto IL_02;
				}
				base.WriteError(new TaskException(RpcUtility.MapRpcErrorCodeToMessage(ex.ErrorCode, serverName)), ErrorCategory.InvalidOperation, null);
				goto IL_02;
			}
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x00078F90 File Offset: 0x00077190
		private ElcParameters GetElcParameters()
		{
			ElcParameters elcParameters = ElcParameters.None;
			if (this.HoldCleanup)
			{
				elcParameters |= ElcParameters.HoldCleanup;
			}
			if (this.EHAHiddenFolderCleanup)
			{
				elcParameters |= ElcParameters.EHAHiddenFolderCleanup;
			}
			return elcParameters;
		}

		// Token: 0x04000BBD RID: 3005
		private static readonly QueryFilter InactiveMailboxFilter = new BitMaskAndFilter(ADRecipientSchema.RecipientSoftDeletedStatus, 8UL);

		// Token: 0x04000BBE RID: 3006
		private bool processed;
	}
}
