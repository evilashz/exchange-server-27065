using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000D8F RID: 3471
	[Cmdlet("Get", "MailboxRepairRequest", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxRepairRequest : GetObjectWithIdentityTaskBase<StoreIntegrityCheckJobIdParameter, StoreIntegrityCheckJob>
	{
		// Token: 0x17002973 RID: 10611
		// (get) Token: 0x0600854C RID: 34124 RVA: 0x0022113C File Offset: 0x0021F33C
		internal override IConfigurationSession ConfigurationSession
		{
			get
			{
				return this.readOnlyConfigSession;
			}
		}

		// Token: 0x17002974 RID: 10612
		// (get) Token: 0x0600854D RID: 34125 RVA: 0x00221144 File Offset: 0x0021F344
		internal IRecipientSession RecipientSession
		{
			get
			{
				return this.readOnlyRecipientSession;
			}
		}

		// Token: 0x17002975 RID: 10613
		// (get) Token: 0x0600854E RID: 34126 RVA: 0x0022114C File Offset: 0x0021F34C
		// (set) Token: 0x0600854F RID: 34127 RVA: 0x00221172 File Offset: 0x0021F372
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter Detailed
		{
			get
			{
				return (SwitchParameter)(base.Fields["Detailed"] ?? false);
			}
			set
			{
				base.Fields["Detailed"] = value;
			}
		}

		// Token: 0x17002976 RID: 10614
		// (get) Token: 0x06008550 RID: 34128 RVA: 0x0022118A File Offset: 0x0021F38A
		// (set) Token: 0x06008551 RID: 34129 RVA: 0x002211A1 File Offset: 0x0021F3A1
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override StoreIntegrityCheckJobIdParameter Identity
		{
			get
			{
				return (StoreIntegrityCheckJobIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17002977 RID: 10615
		// (get) Token: 0x06008552 RID: 34130 RVA: 0x002211B4 File Offset: 0x0021F3B4
		// (set) Token: 0x06008553 RID: 34131 RVA: 0x002211CB File Offset: 0x0021F3CB
		[Parameter(Mandatory = true, Position = 0, ParameterSetName = "Database", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public DatabaseIdParameter Database
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["Database"];
			}
			set
			{
				base.Fields["Database"] = value;
			}
		}

		// Token: 0x17002978 RID: 10616
		// (get) Token: 0x06008554 RID: 34132 RVA: 0x002211DE File Offset: 0x0021F3DE
		// (set) Token: 0x06008555 RID: 34133 RVA: 0x002211F5 File Offset: 0x0021F3F5
		[Parameter(Mandatory = false, Position = 1, ParameterSetName = "Database", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public StoreMailboxIdParameter StoreMailbox
		{
			get
			{
				return (StoreMailboxIdParameter)base.Fields["StoreMailbox"];
			}
			set
			{
				base.Fields["StoreMailbox"] = value;
			}
		}

		// Token: 0x17002979 RID: 10617
		// (get) Token: 0x06008556 RID: 34134 RVA: 0x00221208 File Offset: 0x0021F408
		// (set) Token: 0x06008557 RID: 34135 RVA: 0x0022121F File Offset: 0x0021F41F
		[Parameter(Mandatory = true, Position = 0, ParameterSetName = "Mailbox", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x1700297A RID: 10618
		// (get) Token: 0x06008558 RID: 34136 RVA: 0x00221232 File Offset: 0x0021F432
		// (set) Token: 0x06008559 RID: 34137 RVA: 0x00221258 File Offset: 0x0021F458
		[Parameter(Mandatory = false, ParameterSetName = "Mailbox")]
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

		// Token: 0x1700297B RID: 10619
		// (get) Token: 0x0600855A RID: 34138 RVA: 0x00221270 File Offset: 0x0021F470
		// (set) Token: 0x0600855B RID: 34139 RVA: 0x00221278 File Offset: 0x0021F478
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

		// Token: 0x0600855C RID: 34140 RVA: 0x00221281 File Offset: 0x0021F481
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || (exception is ServerUnavailableException || exception is RpcException || exception is InvalidIntegrityCheckJobIdentity);
		}

		// Token: 0x0600855D RID: 34141 RVA: 0x002212A9 File Offset: 0x0021F4A9
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			this.databaseObject = null;
			this.mailboxGuid = Guid.Empty;
			base.InternalStateReset();
			TaskLogger.LogExit();
		}

		// Token: 0x0600855E RID: 34142 RVA: 0x002212D0 File Offset: 0x0021F4D0
		protected override IConfigDataProvider CreateSession()
		{
			TaskLogger.LogEnter();
			ADSessionSettings adsessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			this.sessionSettings = ADSessionSettings.RescopeToSubtree(adsessionSettings);
			this.readOnlyRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, this.sessionSettings, 235, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\OnlineIsInteg\\GetMailboxRepairRequest.cs");
			if (this.DomainController == null)
			{
				this.readOnlyRecipientSession.UseGlobalCatalog = true;
			}
			this.readOnlyConfigSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, this.sessionSettings, 246, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\OnlineIsInteg\\GetMailboxRepairRequest.cs");
			TaskLogger.LogExit();
			return this.readOnlyConfigSession;
		}

		// Token: 0x0600855F RID: 34143 RVA: 0x00221390 File Offset: 0x0021F590
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			DatabaseIdParameter databaseIdParameter = null;
			Guid empty = Guid.Empty;
			if (base.ParameterSetName == "Identity")
			{
				if (this.Identity != null)
				{
					StoreIntegrityCheckJobIdentity storeIntegrityCheckJobIdentity = new StoreIntegrityCheckJobIdentity(this.Identity.RawIdentity);
					databaseIdParameter = new DatabaseIdParameter(new ADObjectId(storeIntegrityCheckJobIdentity.DatabaseGuid));
					this.requestGuid = storeIntegrityCheckJobIdentity.RequestGuid;
					this.jobGuid = storeIntegrityCheckJobIdentity.JobGuid;
				}
				else
				{
					base.WriteError(new ArgumentNullException("identity"), ErrorCategory.InvalidArgument, null);
				}
			}
			else if (base.ParameterSetName == "Database")
			{
				databaseIdParameter = this.Database;
				if (this.StoreMailbox != null)
				{
					Guid.TryParse(this.StoreMailbox.RawIdentity, out this.mailboxGuid);
				}
			}
			else if (base.ParameterSetName == "Mailbox")
			{
				ADUser aduser = ((ADRecipient)base.GetDataObject<ADRecipient>(this.Mailbox, this.RecipientSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(this.Mailbox.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(this.Mailbox.ToString())))) as ADUser;
				if (!this.Archive)
				{
					databaseIdParameter = new DatabaseIdParameter(aduser.Database);
					this.mailboxGuid = aduser.ExchangeGuid;
				}
				else if (aduser.ArchiveDomain == null && aduser.ArchiveGuid != Guid.Empty)
				{
					databaseIdParameter = new DatabaseIdParameter(aduser.ArchiveDatabase ?? aduser.Database);
					this.mailboxGuid = aduser.ArchiveGuid;
				}
				else if (aduser.ArchiveDomain != null)
				{
					base.WriteError(new MdbAdminTaskException(Strings.ErrorRemoteArchiveNoStats(aduser.ToString())), (ErrorCategory)1003, this.Mailbox);
				}
				else
				{
					base.WriteError(new MdbAdminTaskException(Strings.ErrorArchiveNotEnabled(aduser.ToString())), ErrorCategory.InvalidArgument, this.Mailbox);
				}
			}
			this.databaseObject = (Database)base.GetDataObject<Database>(databaseIdParameter, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(databaseIdParameter.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(databaseIdParameter.ToString())));
			TaskLogger.LogExit();
		}

		// Token: 0x06008560 RID: 34144 RVA: 0x002215A8 File Offset: 0x0021F7A8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			IntegrityCheckQueryFlags flags = IntegrityCheckQueryFlags.None;
			Guid guid = this.requestGuid;
			if (this.jobGuid != Guid.Empty)
			{
				flags = IntegrityCheckQueryFlags.QueryJob;
				guid = this.jobGuid;
			}
			bool details = true;
			if (base.ParameterSetName == "Identity" && this.Detailed == false)
			{
				details = false;
			}
			List<StoreIntegrityCheckJob> storeIntegrityCheckJob = StoreIntegrityCheckAdminRpc.GetStoreIntegrityCheckJob(this.databaseObject, this.mailboxGuid, guid, flags, details, new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			if (storeIntegrityCheckJob != null)
			{
				this.WriteResult<StoreIntegrityCheckJob>(storeIntegrityCheckJob);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04004060 RID: 16480
		private IRecipientSession readOnlyRecipientSession;

		// Token: 0x04004061 RID: 16481
		private IConfigurationSession readOnlyConfigSession;

		// Token: 0x04004062 RID: 16482
		private Database databaseObject;

		// Token: 0x04004063 RID: 16483
		private Guid mailboxGuid;

		// Token: 0x04004064 RID: 16484
		private Guid requestGuid;

		// Token: 0x04004065 RID: 16485
		private Guid jobGuid;

		// Token: 0x04004066 RID: 16486
		private ADSessionSettings sessionSettings;
	}
}
