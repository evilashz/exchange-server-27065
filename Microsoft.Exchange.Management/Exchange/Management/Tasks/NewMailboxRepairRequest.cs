using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000D90 RID: 3472
	[Cmdlet("New", "MailboxRepairRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Mailbox")]
	public sealed class NewMailboxRepairRequest : NewTaskBase<StoreIntegrityCheckJob>
	{
		// Token: 0x1700297C RID: 10620
		// (get) Token: 0x06008562 RID: 34146 RVA: 0x00221653 File Offset: 0x0021F853
		internal override IConfigurationSession ConfigurationSession
		{
			get
			{
				return this.readOnlyConfigSession;
			}
		}

		// Token: 0x1700297D RID: 10621
		// (get) Token: 0x06008563 RID: 34147 RVA: 0x0022165B File Offset: 0x0021F85B
		internal IRecipientSession RecipientSession
		{
			get
			{
				return this.readOnlyRecipientSession;
			}
		}

		// Token: 0x1700297E RID: 10622
		// (get) Token: 0x06008564 RID: 34148 RVA: 0x00221663 File Offset: 0x0021F863
		// (set) Token: 0x06008565 RID: 34149 RVA: 0x0022167A File Offset: 0x0021F87A
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

		// Token: 0x1700297F RID: 10623
		// (get) Token: 0x06008566 RID: 34150 RVA: 0x0022168D File Offset: 0x0021F88D
		// (set) Token: 0x06008567 RID: 34151 RVA: 0x002216A4 File Offset: 0x0021F8A4
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

		// Token: 0x17002980 RID: 10624
		// (get) Token: 0x06008568 RID: 34152 RVA: 0x002216B7 File Offset: 0x0021F8B7
		// (set) Token: 0x06008569 RID: 34153 RVA: 0x002216CE File Offset: 0x0021F8CE
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

		// Token: 0x17002981 RID: 10625
		// (get) Token: 0x0600856A RID: 34154 RVA: 0x002216E1 File Offset: 0x0021F8E1
		// (set) Token: 0x0600856B RID: 34155 RVA: 0x00221707 File Offset: 0x0021F907
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

		// Token: 0x17002982 RID: 10626
		// (get) Token: 0x0600856C RID: 34156 RVA: 0x0022171F File Offset: 0x0021F91F
		// (set) Token: 0x0600856D RID: 34157 RVA: 0x00221727 File Offset: 0x0021F927
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

		// Token: 0x17002983 RID: 10627
		// (get) Token: 0x0600856E RID: 34158 RVA: 0x00221730 File Offset: 0x0021F930
		// (set) Token: 0x0600856F RID: 34159 RVA: 0x00221756 File Offset: 0x0021F956
		[Parameter(Mandatory = false)]
		public SwitchParameter DetectOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["DetectOnly"] ?? false);
			}
			set
			{
				base.Fields["DetectOnly"] = value;
			}
		}

		// Token: 0x17002984 RID: 10628
		// (get) Token: 0x06008570 RID: 34160 RVA: 0x0022176E File Offset: 0x0021F96E
		// (set) Token: 0x06008571 RID: 34161 RVA: 0x00221794 File Offset: 0x0021F994
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? false);
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x17002985 RID: 10629
		// (get) Token: 0x06008572 RID: 34162 RVA: 0x002217AC File Offset: 0x0021F9AC
		// (set) Token: 0x06008573 RID: 34163 RVA: 0x002217C3 File Offset: 0x0021F9C3
		[Parameter(Mandatory = true)]
		public MailboxCorruptionType[] CorruptionType
		{
			get
			{
				return (MailboxCorruptionType[])base.Fields["CorruptionType"];
			}
			set
			{
				base.Fields["CorruptionType"] = value;
			}
		}

		// Token: 0x17002986 RID: 10630
		// (get) Token: 0x06008574 RID: 34164 RVA: 0x002217D6 File Offset: 0x0021F9D6
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewMailboxRepairRequestMailbox(this.databaseObject.Name, this.mailboxGuid.ToString());
			}
		}

		// Token: 0x06008575 RID: 34165 RVA: 0x002217F9 File Offset: 0x0021F9F9
		public NewMailboxRepairRequest()
		{
			base.Fields["DetectOnly"] = new SwitchParameter(false);
			base.Fields["Force"] = new SwitchParameter(false);
		}

		// Token: 0x06008576 RID: 34166 RVA: 0x00221837 File Offset: 0x0021FA37
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || (exception is ServerUnavailableException || exception is RpcException || exception is InvalidIntegrityCheckJobIdentity);
		}

		// Token: 0x06008577 RID: 34167 RVA: 0x0022185F File Offset: 0x0021FA5F
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			this.DataObject = null;
			this.databaseObject = null;
			this.mailboxGuid = Guid.Empty;
			base.InternalStateReset();
			TaskLogger.LogExit();
		}

		// Token: 0x06008578 RID: 34168 RVA: 0x0022188C File Offset: 0x0021FA8C
		protected override IConfigDataProvider CreateSession()
		{
			TaskLogger.LogEnter();
			ADSessionSettings adsessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			this.sessionSettings = ADSessionSettings.RescopeToSubtree(adsessionSettings);
			this.readOnlyRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, this.sessionSettings, 270, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\OnlineIsInteg\\NewMailboxRepairRequest.cs");
			if (this.DomainController == null)
			{
				this.readOnlyRecipientSession.UseGlobalCatalog = true;
			}
			this.readOnlyConfigSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, this.sessionSettings, 281, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\OnlineIsInteg\\NewMailboxRepairRequest.cs");
			TaskLogger.LogExit();
			return this.readOnlyConfigSession;
		}

		// Token: 0x06008579 RID: 34169 RVA: 0x0022194C File Offset: 0x0021FB4C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			this.unlockMoveTarget = false;
			foreach (MailboxCorruptionType mailboxCorruptionType in this.CorruptionType)
			{
				MailboxCorruptionType mailboxCorruptionType2 = mailboxCorruptionType;
				if (mailboxCorruptionType2 == MailboxCorruptionType.LockedMoveTarget)
				{
					this.unlockMoveTarget = true;
				}
			}
			if (this.unlockMoveTarget)
			{
				ADUser aduser = ((ADRecipient)base.GetDataObject<ADRecipient>(this.Mailbox, this.RecipientSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(this.Mailbox.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(this.Mailbox.ToString())))) as ADUser;
				if (this.CorruptionType.Length != 1)
				{
					base.WriteError(new CorruptionTypeParameterIncompatibleException(MailboxCorruptionType.LockedMoveTarget.ToString()), ErrorCategory.InvalidArgument, this.CorruptionType);
				}
				if (UnlockMoveTargetUtil.IsValidLockedStatus(aduser.MailboxMoveStatus))
				{
					base.WriteError(new UnableToUnlockMailboxDueToOutstandingMoveRequestException(this.Mailbox.ToString(), aduser.MailboxMoveStatus.ToString()), ErrorCategory.InvalidArgument, this.Mailbox);
				}
				ActiveManager activeManagerInstance = ActiveManager.GetActiveManagerInstance();
				DatabaseLocationInfo serverForDatabase = activeManagerInstance.GetServerForDatabase(this.databaseObject.Guid);
				if (!UnlockMoveTargetUtil.IsMailboxLocked(serverForDatabase.ServerFqdn, this.databaseObject.Guid, aduser.ExchangeGuid))
				{
					base.WriteError(new MailboxIsNotLockedException(this.Mailbox.ToString()), ErrorCategory.InvalidArgument, this.Mailbox);
				}
			}
			if (!this.databaseObject.IsMailboxDatabase)
			{
				base.WriteError(new OnlineIsIntegException(this.databaseObject.Name, Strings.NotMailboxDatabase, null), ErrorCategory.InvalidArgument, null);
			}
			Guid guid;
			if (this.StoreMailbox != null && !Guid.TryParse(this.StoreMailbox.RawIdentity, out guid))
			{
				throw new ArgumentException("StoreMailbox");
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600857A RID: 34170 RVA: 0x00221B08 File Offset: 0x0021FD08
		protected override IConfigurable PrepareDataObject()
		{
			DatabaseIdParameter databaseIdParameter = null;
			Guid empty = Guid.Empty;
			if (base.ParameterSetName == "Database")
			{
				databaseIdParameter = this.Database;
				if (this.StoreMailbox != null && !Guid.TryParse(this.StoreMailbox.RawIdentity, out this.mailboxGuid))
				{
					base.WriteError(new OnlineIsIntegException(this.Database.ToString(), Strings.InvalidStoreMailboxIdentity, null), ErrorCategory.InvalidArgument, this.StoreMailbox);
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
				this.organizationId = aduser.OrganizationId;
			}
			this.databaseObject = (Database)base.GetDataObject<Database>(databaseIdParameter, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(databaseIdParameter.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(databaseIdParameter.ToString())));
			return new StoreIntegrityCheckJob();
		}

		// Token: 0x0600857B RID: 34171 RVA: 0x00221CE0 File Offset: 0x0021FEE0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.unlockMoveTarget)
			{
				ActiveManager activeManagerInstance = ActiveManager.GetActiveManagerInstance();
				DatabaseLocationInfo serverForDatabase = activeManagerInstance.GetServerForDatabase(this.databaseObject.Guid);
				string serverFqdn = serverForDatabase.ServerFqdn;
				if (!this.DetectOnly)
				{
					UnlockMoveTargetUtil.UnlockMoveTarget(serverFqdn, this.databaseObject.Guid, this.mailboxGuid, this.organizationId);
					this.WriteResult(this.DataObject);
				}
			}
			else
			{
				StoreIntegrityCheckRequestFlags requestFlags = this.GetRequestFlags();
				StoreIntegrityCheckJob storeIntegrityCheckJob = StoreIntegrityCheckAdminRpc.CreateStoreIntegrityCheckJob(this.databaseObject, this.mailboxGuid, requestFlags, this.CorruptionType, new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				if (storeIntegrityCheckJob != null)
				{
					this.WriteResult(storeIntegrityCheckJob);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600857C RID: 34172 RVA: 0x00221DA8 File Offset: 0x0021FFA8
		private StoreIntegrityCheckRequestFlags GetRequestFlags()
		{
			StoreIntegrityCheckRequestFlags storeIntegrityCheckRequestFlags = StoreIntegrityCheckRequestFlags.None;
			if (this.DetectOnly)
			{
				storeIntegrityCheckRequestFlags |= StoreIntegrityCheckRequestFlags.DetectOnly;
			}
			if (this.Force)
			{
				storeIntegrityCheckRequestFlags |= StoreIntegrityCheckRequestFlags.Force;
			}
			return storeIntegrityCheckRequestFlags;
		}

		// Token: 0x04004067 RID: 16487
		private const string ParameterDetectOnly = "DetectOnly";

		// Token: 0x04004068 RID: 16488
		private const string ParameterForce = "Force";

		// Token: 0x04004069 RID: 16489
		private IRecipientSession readOnlyRecipientSession;

		// Token: 0x0400406A RID: 16490
		private IConfigurationSession readOnlyConfigSession;

		// Token: 0x0400406B RID: 16491
		private Database databaseObject;

		// Token: 0x0400406C RID: 16492
		private Guid mailboxGuid;

		// Token: 0x0400406D RID: 16493
		private OrganizationId organizationId;

		// Token: 0x0400406E RID: 16494
		private ADSessionSettings sessionSettings;

		// Token: 0x0400406F RID: 16495
		private bool unlockMoveTarget;
	}
}
