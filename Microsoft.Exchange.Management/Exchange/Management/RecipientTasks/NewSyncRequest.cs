using System;
using System.Management.Automation;
using System.Net;
using System.Security;
using Microsoft.Exchange.Configuration.Authorization;
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
	// Token: 0x02000CBF RID: 3263
	[Cmdlet("New", "SyncRequest", SupportsShouldProcess = true, DefaultParameterSetName = "AutoDetect")]
	public sealed class NewSyncRequest : NewRequest<SyncRequest>
	{
		// Token: 0x170026C6 RID: 9926
		// (get) Token: 0x06007D0B RID: 32011 RVA: 0x001FF6FD File Offset: 0x001FD8FD
		// (set) Token: 0x06007D0C RID: 32012 RVA: 0x001FF722 File Offset: 0x001FD922
		[Parameter(Mandatory = false, ParameterSetName = "Imap", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "Pop", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "Eas", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "AutoDetect", ValueFromPipeline = true)]
		public Guid AggregatedMailboxGuid
		{
			get
			{
				return (Guid)(base.Fields["AggregatedMailboxGuid"] ?? Guid.Empty);
			}
			set
			{
				base.Fields["AggregatedMailboxGuid"] = value;
			}
		}

		// Token: 0x170026C7 RID: 9927
		// (get) Token: 0x06007D0D RID: 32013 RVA: 0x001FF73A File Offset: 0x001FD93A
		// (set) Token: 0x06007D0E RID: 32014 RVA: 0x001FF751 File Offset: 0x001FD951
		[Parameter(Mandatory = false, ParameterSetName = "Pop", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "AutoDetect", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "Eas", ValueFromPipeline = true)]
		[Parameter(Mandatory = true, ParameterSetName = "Olc", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "Imap", ValueFromPipeline = true)]
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

		// Token: 0x170026C8 RID: 9928
		// (get) Token: 0x06007D0F RID: 32015 RVA: 0x001FF764 File Offset: 0x001FD964
		// (set) Token: 0x06007D10 RID: 32016 RVA: 0x001FF77B File Offset: 0x001FD97B
		[Parameter(Mandatory = true, ParameterSetName = "Imap")]
		[Parameter(Mandatory = false, ParameterSetName = "Olc")]
		[Parameter(Mandatory = true, ParameterSetName = "Pop")]
		[Parameter(Mandatory = true, ParameterSetName = "Eas")]
		public Fqdn RemoteServerName
		{
			get
			{
				return (Fqdn)base.Fields["RemoteServerName"];
			}
			set
			{
				base.Fields["RemoteServerName"] = value;
			}
		}

		// Token: 0x170026C9 RID: 9929
		// (get) Token: 0x06007D11 RID: 32017 RVA: 0x001FF78E File Offset: 0x001FD98E
		// (set) Token: 0x06007D12 RID: 32018 RVA: 0x001FF7AF File Offset: 0x001FD9AF
		[Parameter(Mandatory = false, ParameterSetName = "Pop")]
		[Parameter(Mandatory = false, ParameterSetName = "Olc")]
		[Parameter(Mandatory = false, ParameterSetName = "Imap")]
		public int RemoteServerPort
		{
			get
			{
				return (int)(base.Fields["RemoteServerPort"] ?? 0);
			}
			set
			{
				base.Fields["RemoteServerPort"] = value;
			}
		}

		// Token: 0x170026CA RID: 9930
		// (get) Token: 0x06007D13 RID: 32019 RVA: 0x001FF7C7 File Offset: 0x001FD9C7
		// (set) Token: 0x06007D14 RID: 32020 RVA: 0x001FF7DE File Offset: 0x001FD9DE
		[Parameter(Mandatory = false, ParameterSetName = "Imap")]
		[Parameter(Mandatory = false, ParameterSetName = "Eas")]
		[Parameter(Mandatory = false, ParameterSetName = "Pop")]
		public Fqdn SmtpServerName
		{
			get
			{
				return (Fqdn)base.Fields["SmtpServerName"];
			}
			set
			{
				base.Fields["SmtpServerName"] = value;
			}
		}

		// Token: 0x170026CB RID: 9931
		// (get) Token: 0x06007D15 RID: 32021 RVA: 0x001FF7F1 File Offset: 0x001FD9F1
		// (set) Token: 0x06007D16 RID: 32022 RVA: 0x001FF812 File Offset: 0x001FDA12
		[Parameter(Mandatory = false, ParameterSetName = "Eas")]
		[Parameter(Mandatory = false, ParameterSetName = "Imap")]
		[Parameter(Mandatory = false, ParameterSetName = "Pop")]
		public int SmtpServerPort
		{
			get
			{
				return (int)(base.Fields["SmtpServerPort"] ?? 0);
			}
			set
			{
				base.Fields["SmtpServerPort"] = value;
			}
		}

		// Token: 0x170026CC RID: 9932
		// (get) Token: 0x06007D17 RID: 32023 RVA: 0x001FF82A File Offset: 0x001FDA2A
		// (set) Token: 0x06007D18 RID: 32024 RVA: 0x001FF84F File Offset: 0x001FDA4F
		[Parameter(Mandatory = true, ParameterSetName = "Eas")]
		[Parameter(Mandatory = true, ParameterSetName = "AutoDetect")]
		[Parameter(Mandatory = true, ParameterSetName = "Imap")]
		[Parameter(Mandatory = true, ParameterSetName = "Pop")]
		public SmtpAddress RemoteEmailAddress
		{
			get
			{
				return (SmtpAddress)(base.Fields["RemoteEmailAddress"] ?? SmtpAddress.Empty);
			}
			set
			{
				base.Fields["RemoteEmailAddress"] = value;
			}
		}

		// Token: 0x170026CD RID: 9933
		// (get) Token: 0x06007D19 RID: 32025 RVA: 0x001FF867 File Offset: 0x001FDA67
		// (set) Token: 0x06007D1A RID: 32026 RVA: 0x001FF87E File Offset: 0x001FDA7E
		[Parameter(Mandatory = false, ParameterSetName = "Pop")]
		[Parameter(Mandatory = false, ParameterSetName = "AutoDetect")]
		[Parameter(Mandatory = false, ParameterSetName = "Imap")]
		[Parameter(Mandatory = false, ParameterSetName = "Eas")]
		public string UserName
		{
			get
			{
				return (string)base.Fields["UserName"];
			}
			set
			{
				base.Fields["UserName"] = value;
			}
		}

		// Token: 0x170026CE RID: 9934
		// (get) Token: 0x06007D1B RID: 32027 RVA: 0x001FF891 File Offset: 0x001FDA91
		// (set) Token: 0x06007D1C RID: 32028 RVA: 0x001FF8A8 File Offset: 0x001FDAA8
		[Parameter(Mandatory = true, ParameterSetName = "AutoDetect")]
		[Parameter(Mandatory = true, ParameterSetName = "Pop")]
		[Parameter(Mandatory = true, ParameterSetName = "Eas")]
		[Parameter(Mandatory = true, ParameterSetName = "Imap")]
		public SecureString Password
		{
			get
			{
				return (SecureString)base.Fields["Password"];
			}
			set
			{
				base.Fields["Password"] = value;
			}
		}

		// Token: 0x170026CF RID: 9935
		// (get) Token: 0x06007D1D RID: 32029 RVA: 0x001FF8BB File Offset: 0x001FDABB
		// (set) Token: 0x06007D1E RID: 32030 RVA: 0x001FF8DC File Offset: 0x001FDADC
		[Parameter(Mandatory = false, ParameterSetName = "Pop")]
		[Parameter(Mandatory = false, ParameterSetName = "Imap")]
		public AuthenticationMethod Authentication
		{
			get
			{
				return (AuthenticationMethod)(base.Fields["Authentication"] ?? AuthenticationMethod.Basic);
			}
			set
			{
				base.Fields["Authentication"] = value;
			}
		}

		// Token: 0x170026D0 RID: 9936
		// (get) Token: 0x06007D1F RID: 32031 RVA: 0x001FF8F4 File Offset: 0x001FDAF4
		// (set) Token: 0x06007D20 RID: 32032 RVA: 0x001FF915 File Offset: 0x001FDB15
		[Parameter(Mandatory = false, ParameterSetName = "Pop")]
		[Parameter(Mandatory = false, ParameterSetName = "Imap")]
		public IMAPSecurityMechanism Security
		{
			get
			{
				return (IMAPSecurityMechanism)(base.Fields["Security"] ?? IMAPSecurityMechanism.None);
			}
			set
			{
				base.Fields["Security"] = value;
			}
		}

		// Token: 0x170026D1 RID: 9937
		// (get) Token: 0x06007D21 RID: 32033 RVA: 0x001FF92D File Offset: 0x001FDB2D
		// (set) Token: 0x06007D22 RID: 32034 RVA: 0x001FF953 File Offset: 0x001FDB53
		[Parameter(Mandatory = false, ParameterSetName = "Olc")]
		[Parameter(Mandatory = false, ParameterSetName = "AutoDetect")]
		[Parameter(Mandatory = false, ParameterSetName = "Eas")]
		[Parameter(Mandatory = false, ParameterSetName = "Imap")]
		[Parameter(Mandatory = false, ParameterSetName = "Pop")]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x170026D2 RID: 9938
		// (get) Token: 0x06007D23 RID: 32035 RVA: 0x001FF96B File Offset: 0x001FDB6B
		// (set) Token: 0x06007D24 RID: 32036 RVA: 0x001FF991 File Offset: 0x001FDB91
		[Parameter(Mandatory = true, ParameterSetName = "Imap")]
		public SwitchParameter Imap
		{
			get
			{
				return (SwitchParameter)(base.Fields["Imap"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Imap"] = value;
			}
		}

		// Token: 0x170026D3 RID: 9939
		// (get) Token: 0x06007D25 RID: 32037 RVA: 0x001FF9A9 File Offset: 0x001FDBA9
		// (set) Token: 0x06007D26 RID: 32038 RVA: 0x001FF9CF File Offset: 0x001FDBCF
		[Parameter(Mandatory = true, ParameterSetName = "Pop")]
		public SwitchParameter Pop
		{
			get
			{
				return (SwitchParameter)(base.Fields["Pop"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Pop"] = value;
			}
		}

		// Token: 0x170026D4 RID: 9940
		// (get) Token: 0x06007D27 RID: 32039 RVA: 0x001FF9E7 File Offset: 0x001FDBE7
		// (set) Token: 0x06007D28 RID: 32040 RVA: 0x001FFA0D File Offset: 0x001FDC0D
		[Parameter(Mandatory = true, ParameterSetName = "Eas")]
		public SwitchParameter Eas
		{
			get
			{
				return (SwitchParameter)(base.Fields["Eas"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Eas"] = value;
			}
		}

		// Token: 0x170026D5 RID: 9941
		// (get) Token: 0x06007D29 RID: 32041 RVA: 0x001FFA25 File Offset: 0x001FDC25
		// (set) Token: 0x06007D2A RID: 32042 RVA: 0x001FFA4B File Offset: 0x001FDC4B
		[Parameter(Mandatory = true, ParameterSetName = "Olc")]
		public SwitchParameter Olc
		{
			get
			{
				return (SwitchParameter)(base.Fields["Olc"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Olc"] = value;
			}
		}

		// Token: 0x170026D6 RID: 9942
		// (get) Token: 0x06007D2B RID: 32043 RVA: 0x001FFA63 File Offset: 0x001FDC63
		// (set) Token: 0x06007D2C RID: 32044 RVA: 0x001FFA7A File Offset: 0x001FDC7A
		[Parameter(Mandatory = false, ParameterSetName = "Olc")]
		public long? Puid
		{
			get
			{
				return (long?)base.Fields["Puid"];
			}
			set
			{
				base.Fields["Puid"] = value;
			}
		}

		// Token: 0x170026D7 RID: 9943
		// (get) Token: 0x06007D2D RID: 32045 RVA: 0x001FFA92 File Offset: 0x001FDC92
		// (set) Token: 0x06007D2E RID: 32046 RVA: 0x001FFAA9 File Offset: 0x001FDCA9
		[Parameter(Mandatory = false, ParameterSetName = "Olc")]
		public int? DGroup
		{
			get
			{
				return (int?)base.Fields["DGroup"];
			}
			set
			{
				base.Fields["DGroup"] = value;
			}
		}

		// Token: 0x170026D8 RID: 9944
		// (get) Token: 0x06007D2F RID: 32047 RVA: 0x001FFAC1 File Offset: 0x001FDCC1
		// (set) Token: 0x06007D30 RID: 32048 RVA: 0x001FFAC9 File Offset: 0x001FDCC9
		[Parameter(Mandatory = true, ParameterSetName = "Pop")]
		[Parameter(Mandatory = true, ParameterSetName = "Imap")]
		[Parameter(Mandatory = true, ParameterSetName = "Eas")]
		[Parameter(Mandatory = true, ParameterSetName = "AutoDetect")]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x170026D9 RID: 9945
		// (get) Token: 0x06007D31 RID: 32049 RVA: 0x001FFAD2 File Offset: 0x001FDCD2
		// (set) Token: 0x06007D32 RID: 32050 RVA: 0x001FFAE9 File Offset: 0x001FDCE9
		[Parameter(Mandatory = false, ParameterSetName = "AutoDetect")]
		[Parameter(Mandatory = false, ParameterSetName = "Pop")]
		[Parameter(Mandatory = false, ParameterSetName = "Imap")]
		[Parameter(Mandatory = false, ParameterSetName = "Eas")]
		public new DateTime StartAfter
		{
			get
			{
				return (DateTime)base.Fields["StartAfter"];
			}
			set
			{
				base.Fields["StartAfter"] = value;
			}
		}

		// Token: 0x170026DA RID: 9946
		// (get) Token: 0x06007D33 RID: 32051 RVA: 0x001FFB01 File Offset: 0x001FDD01
		// (set) Token: 0x06007D34 RID: 32052 RVA: 0x001FFB18 File Offset: 0x001FDD18
		[Parameter(Mandatory = false, ParameterSetName = "Eas")]
		[Parameter(Mandatory = false, ParameterSetName = "Imap")]
		[Parameter(Mandatory = false, ParameterSetName = "Pop")]
		[Parameter(Mandatory = false, ParameterSetName = "AutoDetect")]
		public DateTime CompleteAfter
		{
			get
			{
				return (DateTime)base.Fields["CompleteAfter"];
			}
			set
			{
				base.Fields["CompleteAfter"] = value;
			}
		}

		// Token: 0x170026DB RID: 9947
		// (get) Token: 0x06007D35 RID: 32053 RVA: 0x001FFB30 File Offset: 0x001FDD30
		// (set) Token: 0x06007D36 RID: 32054 RVA: 0x001FFB5E File Offset: 0x001FDD5E
		[Parameter(Mandatory = false, ParameterSetName = "Eas")]
		[Parameter(Mandatory = false, ParameterSetName = "Imap")]
		[Parameter(Mandatory = false, ParameterSetName = "Pop")]
		[Parameter(Mandatory = false, ParameterSetName = "AutoDetect")]
		public TimeSpan IncrementalSyncInterval
		{
			get
			{
				return (TimeSpan)(base.Fields["IncrementalSyncInterval"] ?? TimeSpan.FromHours(1.0));
			}
			set
			{
				base.Fields["IncrementalSyncInterval"] = value;
			}
		}

		// Token: 0x170026DC RID: 9948
		// (get) Token: 0x06007D37 RID: 32055 RVA: 0x001FFB76 File Offset: 0x001FDD76
		// (set) Token: 0x06007D38 RID: 32056 RVA: 0x001FFB8D File Offset: 0x001FDD8D
		[Parameter(Mandatory = false, ParameterSetName = "Eas")]
		[Parameter(Mandatory = false, ParameterSetName = "Imap")]
		[Parameter(Mandatory = false, ParameterSetName = "Olc")]
		[Parameter(Mandatory = false, ParameterSetName = "Pop")]
		[Parameter(Mandatory = false, ParameterSetName = "AutoDetect")]
		public string TargetRootFolder
		{
			get
			{
				return (string)base.Fields["TargetRootFolder"];
			}
			set
			{
				base.Fields["TargetRootFolder"] = value;
			}
		}

		// Token: 0x170026DD RID: 9949
		// (get) Token: 0x06007D39 RID: 32057 RVA: 0x001FFBA0 File Offset: 0x001FDDA0
		// (set) Token: 0x06007D3A RID: 32058 RVA: 0x001FFBC6 File Offset: 0x001FDDC6
		[Parameter(Mandatory = false)]
		public SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForestWideDomainControllerAffinityByExecutingUser"] ?? false);
			}
			set
			{
				base.Fields["ForestWideDomainControllerAffinityByExecutingUser"] = value;
			}
		}

		// Token: 0x170026DE RID: 9950
		// (get) Token: 0x06007D3B RID: 32059 RVA: 0x001FFBDE File Offset: 0x001FDDDE
		protected override RequestIndexId DefaultRequestIndexId
		{
			get
			{
				return new RequestIndexId(this.targetUser.Id);
			}
		}

		// Token: 0x170026DF RID: 9951
		// (get) Token: 0x06007D3C RID: 32060 RVA: 0x001FFBF0 File Offset: 0x001FDDF0
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewSyncRequest((this.DataObject == null) ? base.RequestName : this.DataObject.ToString());
			}
		}

		// Token: 0x06007D3D RID: 32061 RVA: 0x001FFC12 File Offset: 0x001FDE12
		protected override void CreateIndexEntries(TransactionalRequestJob dataObject)
		{
			RequestIndexEntryProvider.CreateAndPopulateRequestIndexEntries(dataObject, this.DefaultRequestIndexId);
		}

		// Token: 0x06007D3E RID: 32062 RVA: 0x001FFC20 File Offset: 0x001FDE20
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new NewSyncRequestTaskModuleFactory();
		}

		// Token: 0x06007D3F RID: 32063 RVA: 0x001FFC28 File Offset: 0x001FDE28
		protected override IConfigDataProvider CreateSession()
		{
			if (!this.Olc)
			{
				return base.CreateSession();
			}
			base.CreateSession();
			ADSessionSettings sessionSettings = ADSessionSettings.FromConsumerOrganization();
			base.GCSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, sessionSettings, 572, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\SyncRequest\\NewSyncRequest.cs");
			base.RecipSession = DirectorySessionFactory.NonCacheSessionFactory.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.FullyConsistent, sessionSettings, 578, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\SyncRequest\\NewSyncRequest.cs");
			base.CurrentOrganizationId = base.RecipSession.SessionSettings.CurrentOrganizationId;
			base.RJProvider.IndexProvider.RecipientSession = base.RecipSession;
			return base.RJProvider;
		}

		// Token: 0x06007D40 RID: 32064 RVA: 0x001FFCD8 File Offset: 0x001FDED8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				if (this.Mailbox == null)
				{
					ADObjectId adObjectId;
					if (!base.TryGetExecutingUserId(out adObjectId))
					{
						throw new ExecutingUserPropertyNotFoundException("executingUserid");
					}
					this.Mailbox = new MailboxIdParameter(adObjectId);
				}
				this.targetUser = RequestTaskHelper.ResolveADUser(base.RecipSession, base.GCSession, base.ServerSettings, this.Mailbox, base.OptionalIdentityData, base.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), true);
				base.RecipSession = (IRecipientSession)TaskHelper.UnderscopeSessionToOrganization(base.RecipSession, this.targetUser.OrganizationId, true);
				base.CurrentOrganizationId = this.targetUser.OrganizationId;
				base.RJProvider.IndexProvider.RecipientSession = base.RecipSession;
				if (this.targetUser.Database == null)
				{
					base.WriteError(new MailboxLacksDatabasePermanentException(this.targetUser.ToString()), ErrorCategory.InvalidArgument, this.Mailbox);
				}
				if (this.AggregatedMailboxGuid != Guid.Empty)
				{
					MultiValuedProperty<Guid> multiValuedProperty = this.targetUser.AggregatedMailboxGuids ?? new MultiValuedProperty<Guid>();
					if (!multiValuedProperty.Contains(this.AggregatedMailboxGuid))
					{
						base.WriteError(new AggregatedMailboxNotFoundPermanentException(this.AggregatedMailboxGuid, this.targetUser.ToString()), ErrorCategory.InvalidArgument, this.AggregatedMailboxGuid);
					}
				}
				this.DataObject.DomainControllerToUpdate = this.targetUser.OriginatingServer;
				bool wildcardedSearch = false;
				if (!string.IsNullOrEmpty(this.Name))
				{
					base.ValidateName();
					base.RequestName = this.Name;
				}
				else if (this.Olc)
				{
					base.RequestName = "OlcSync";
				}
				else
				{
					wildcardedSearch = true;
					base.RequestName = "Sync";
				}
				base.RescopeToOrgId(this.targetUser.OrganizationId);
				ADObjectId mdbId = null;
				ADObjectId mdbServerSite = null;
				RequestFlags requestFlags = this.LocateAndChooseMdb(null, this.targetUser.Database, null, this.Mailbox, this.Mailbox, out mdbId, out mdbServerSite);
				base.MdbId = mdbId;
				base.MdbServerSite = mdbServerSite;
				base.Flags = (RequestFlags.CrossOrg | requestFlags);
				base.RequestName = this.CheckRequestNameAvailability(base.RequestName, this.targetUser.Id, true, MRSRequestType.Sync, this.Mailbox, wildcardedSearch);
				if (this.Imap == true)
				{
					this.syncProtocol = SyncProtocol.Imap;
				}
				else if (this.Eas == true)
				{
					this.syncProtocol = SyncProtocol.Eas;
				}
				else if (this.Pop == true)
				{
					this.syncProtocol = SyncProtocol.Pop;
				}
				else if (this.Olc == true)
				{
					this.syncProtocol = SyncProtocol.Olc;
				}
				else
				{
					base.WriteError(new SyncProtocolNotSpecifiedPermanentException(), ErrorCategory.InvalidArgument, this.syncProtocol);
				}
				if (base.IsFieldSet("IncrementalSyncInterval"))
				{
					RequestTaskHelper.ValidateIncrementalSyncInterval(this.IncrementalSyncInterval, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
				DateTime utcNow = DateTime.UtcNow;
				if (base.IsFieldSet("StartAfter"))
				{
					RequestTaskHelper.ValidateStartAfterTime(this.StartAfter.ToUniversalTime(), new Task.TaskErrorLoggingDelegate(base.WriteError), utcNow);
				}
				if (base.IsFieldSet("StartAfter") && base.IsFieldSet("CompleteAfter"))
				{
					RequestTaskHelper.ValidateStartAfterComesBeforeCompleteAfter(new DateTime?(this.StartAfter.ToUniversalTime()), new DateTime?(this.CompleteAfter.ToUniversalTime()), new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
				base.InternalValidate();
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007D41 RID: 32065 RVA: 0x00200060 File Offset: 0x001FE260
		protected override void SetRequestProperties(TransactionalRequestJob dataObject)
		{
			base.SetRequestProperties(dataObject);
			if (this.Eas)
			{
				dataObject.JobType = MRSJobType.RequestJobE15_SubType;
			}
			dataObject.RequestType = MRSRequestType.Sync;
			if (dataObject.WorkloadType == RequestWorkloadType.None)
			{
				dataObject.WorkloadType = RequestWorkloadType.SyncAggregation;
			}
			dataObject.RemoteHostName = this.RemoteServerName;
			dataObject.RemoteHostPort = this.RemoteServerPort;
			dataObject.SmtpServerName = this.SmtpServerName;
			dataObject.SmtpServerPort = this.SmtpServerPort;
			dataObject.EmailAddress = this.RemoteEmailAddress;
			dataObject.AuthenticationMethod = new AuthenticationMethod?(this.Authentication);
			dataObject.SecurityMechanism = this.Security;
			dataObject.SyncProtocol = this.syncProtocol;
			dataObject.IncrementalSyncInterval = this.IncrementalSyncInterval;
			if (this.Olc)
			{
				dataObject.AllowLargeItems = true;
				dataObject.UserPuid = this.Puid;
				dataObject.OlcDGroup = this.DGroup;
			}
			if (base.IsFieldSet("StartAfter"))
			{
				RequestTaskHelper.SetStartAfter(new DateTime?(this.StartAfter), dataObject, null);
			}
			else
			{
				RequestTaskHelper.SetStartAfter(new DateTime?(DateTime.MinValue), dataObject, null);
			}
			if (base.IsFieldSet("CompleteAfter"))
			{
				RequestTaskHelper.SetCompleteAfter(new DateTime?(this.CompleteAfter), dataObject, null);
			}
			else
			{
				dataObject.TimeTracker.SetTimestamp(RequestJobTimestamp.CompleteAfter, new DateTime?(DateTime.MaxValue));
			}
			if (string.IsNullOrEmpty(this.UserName))
			{
				this.UserName = this.RemoteEmailAddress.ToString();
			}
			if (!string.IsNullOrEmpty(this.TargetRootFolder))
			{
				dataObject.TargetRootFolder = this.TargetRootFolder;
			}
			dataObject.RemoteCredential = new NetworkCredential(this.UserName, this.Password);
			if (this.AggregatedMailboxGuid != Guid.Empty)
			{
				dataObject.Flags |= RequestFlags.TargetIsAggregatedMailbox;
			}
			if (this.targetUser != null)
			{
				dataObject.TargetUserId = this.targetUser.Id;
				dataObject.TargetUser = this.targetUser;
				dataObject.TargetExchangeGuid = ((this.AggregatedMailboxGuid != Guid.Empty) ? this.AggregatedMailboxGuid : this.targetUser.ExchangeGuid);
				dataObject.TargetDatabase = ADObjectIdResolutionHelper.ResolveDN(this.targetUser.Database);
				dataObject.TargetAlias = this.targetUser.Alias;
			}
		}

		// Token: 0x06007D42 RID: 32066 RVA: 0x002002A2 File Offset: 0x001FE4A2
		protected override SyncRequest ConvertToPresentationObject(TransactionalRequestJob dataObject)
		{
			if (dataObject.IndexEntries != null && dataObject.IndexEntries.Count >= 1)
			{
				return new SyncRequest(dataObject.IndexEntries[0]);
			}
			return null;
		}

		// Token: 0x04003DAD RID: 15789
		public const string DefaultSyncName = "Sync";

		// Token: 0x04003DAE RID: 15790
		public const string DefaultOlcSyncName = "OlcSync";

		// Token: 0x04003DAF RID: 15791
		private const string ParameterForestWideDomainControllerAffinityByExecutingUser = "ForestWideDomainControllerAffinityByExecutingUser";

		// Token: 0x04003DB0 RID: 15792
		public const string TaskNoun = "SyncRequest";

		// Token: 0x04003DB1 RID: 15793
		public const string ParameterAggregatedMailboxGuid = "AggregatedMailboxGuid";

		// Token: 0x04003DB2 RID: 15794
		public const string ParameterMailbox = "Mailbox";

		// Token: 0x04003DB3 RID: 15795
		public const string ParameterRemoteEmailAddress = "RemoteEmailAddress";

		// Token: 0x04003DB4 RID: 15796
		public const string ParameterRemoteServerName = "RemoteServerName";

		// Token: 0x04003DB5 RID: 15797
		public const string ParameterRemoteServerPort = "RemoteServerPort";

		// Token: 0x04003DB6 RID: 15798
		public const string ParameterSmtpServerName = "SmtpServerName";

		// Token: 0x04003DB7 RID: 15799
		public const string ParameterSmtpServerPort = "SmtpServerPort";

		// Token: 0x04003DB8 RID: 15800
		public const string ParameterUserName = "UserName";

		// Token: 0x04003DB9 RID: 15801
		public const string ParameterPassword = "Password";

		// Token: 0x04003DBA RID: 15802
		public const string ParameterSecurity = "Security";

		// Token: 0x04003DBB RID: 15803
		public const string ParameterAuthentication = "Authentication";

		// Token: 0x04003DBC RID: 15804
		public const string ParameterForce = "Force";

		// Token: 0x04003DBD RID: 15805
		public const string ParameterImap = "Imap";

		// Token: 0x04003DBE RID: 15806
		public const string ParameterPop = "Pop";

		// Token: 0x04003DBF RID: 15807
		public const string ParameterEas = "Eas";

		// Token: 0x04003DC0 RID: 15808
		public const string ParameterOlc = "Olc";

		// Token: 0x04003DC1 RID: 15809
		public const string ParameterPuid = "Puid";

		// Token: 0x04003DC2 RID: 15810
		public const string ParameterDGroup = "DGroup";

		// Token: 0x04003DC3 RID: 15811
		public const string ParameterIncrementalSyncInterval = "IncrementalSyncInterval";

		// Token: 0x04003DC4 RID: 15812
		public const string ParameterSetAutoDetect = "AutoDetect";

		// Token: 0x04003DC5 RID: 15813
		public const string ParameterSetImap = "Imap";

		// Token: 0x04003DC6 RID: 15814
		public const string ParameterSetPop = "Pop";

		// Token: 0x04003DC7 RID: 15815
		public const string ParameterSetEas = "Eas";

		// Token: 0x04003DC8 RID: 15816
		public const string ParameterSetOlc = "Olc";

		// Token: 0x04003DC9 RID: 15817
		public const string ParameterTargetRootFolder = "TargetRootFolder";

		// Token: 0x04003DCA RID: 15818
		private ADUser targetUser;

		// Token: 0x04003DCB RID: 15819
		private SyncProtocol syncProtocol;
	}
}
