using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CA1 RID: 3233
	[Cmdlet("New", "MailboxRestoreRequest", SupportsShouldProcess = true, DefaultParameterSetName = "MigrationLocalMailboxRestore")]
	public sealed class NewMailboxRestoreRequest : NewRequest<MailboxRestoreRequest>
	{
		// Token: 0x17002675 RID: 9845
		// (get) Token: 0x06007C18 RID: 31768 RVA: 0x001FC580 File Offset: 0x001FA780
		public bool IsPublicFolderMailboxRestore
		{
			get
			{
				return this.restoreFlags.HasFlag(MailboxRestoreType.PublicFolderMailbox);
			}
		}

		// Token: 0x17002676 RID: 9846
		// (get) Token: 0x06007C1A RID: 31770 RVA: 0x001FC5A1 File Offset: 0x001FA7A1
		// (set) Token: 0x06007C1B RID: 31771 RVA: 0x001FC5B8 File Offset: 0x001FA7B8
		[ValidateNotNull]
		[Parameter(Mandatory = true)]
		public StoreMailboxIdParameter SourceStoreMailbox
		{
			get
			{
				return (StoreMailboxIdParameter)base.Fields["SourceStoreMailbox"];
			}
			set
			{
				base.Fields["SourceStoreMailbox"] = value;
			}
		}

		// Token: 0x17002677 RID: 9847
		// (get) Token: 0x06007C1C RID: 31772 RVA: 0x001FC5CB File Offset: 0x001FA7CB
		// (set) Token: 0x06007C1D RID: 31773 RVA: 0x001FC5E2 File Offset: 0x001FA7E2
		[Parameter(Mandatory = true, ParameterSetName = "MigrationLocalMailboxRestore")]
		[ValidateNotNull]
		public DatabaseIdParameter SourceDatabase
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["SourceDatabase"];
			}
			set
			{
				base.Fields["SourceDatabase"] = value;
			}
		}

		// Token: 0x17002678 RID: 9848
		// (get) Token: 0x06007C1E RID: 31774 RVA: 0x001FC5F5 File Offset: 0x001FA7F5
		// (set) Token: 0x06007C1F RID: 31775 RVA: 0x001FC60C File Offset: 0x001FA80C
		[ValidateNotNull]
		[Parameter(Mandatory = true)]
		public MailboxOrMailUserIdParameter TargetMailbox
		{
			get
			{
				return (MailboxOrMailUserIdParameter)base.Fields["TargetMailbox"];
			}
			set
			{
				base.Fields["TargetMailbox"] = value;
			}
		}

		// Token: 0x17002679 RID: 9849
		// (get) Token: 0x06007C20 RID: 31776 RVA: 0x001FC61F File Offset: 0x001FA81F
		// (set) Token: 0x06007C21 RID: 31777 RVA: 0x001FC636 File Offset: 0x001FA836
		[Parameter(Mandatory = false)]
		public string SourceRootFolder
		{
			get
			{
				return (string)base.Fields["SourceRootFolder"];
			}
			set
			{
				base.Fields["SourceRootFolder"] = value;
			}
		}

		// Token: 0x1700267A RID: 9850
		// (get) Token: 0x06007C22 RID: 31778 RVA: 0x001FC649 File Offset: 0x001FA849
		// (set) Token: 0x06007C23 RID: 31779 RVA: 0x001FC660 File Offset: 0x001FA860
		[Parameter(Mandatory = false)]
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

		// Token: 0x1700267B RID: 9851
		// (get) Token: 0x06007C24 RID: 31780 RVA: 0x001FC673 File Offset: 0x001FA873
		// (set) Token: 0x06007C25 RID: 31781 RVA: 0x001FC699 File Offset: 0x001FA899
		[Parameter(Mandatory = false)]
		public SwitchParameter TargetIsArchive
		{
			get
			{
				return (SwitchParameter)(base.Fields["TargetIsArchive"] ?? false);
			}
			set
			{
				base.Fields["TargetIsArchive"] = value;
			}
		}

		// Token: 0x1700267C RID: 9852
		// (get) Token: 0x06007C26 RID: 31782 RVA: 0x001FC6B1 File Offset: 0x001FA8B1
		// (set) Token: 0x06007C27 RID: 31783 RVA: 0x001FC6C8 File Offset: 0x001FA8C8
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "RemoteMailboxRestore")]
		public Guid RemoteDatabaseGuid
		{
			get
			{
				return (Guid)base.Fields["RemoteDatabaseGuid"];
			}
			set
			{
				base.Fields["RemoteDatabaseGuid"] = value;
			}
		}

		// Token: 0x1700267D RID: 9853
		// (get) Token: 0x06007C28 RID: 31784 RVA: 0x001FC6E0 File Offset: 0x001FA8E0
		// (set) Token: 0x06007C29 RID: 31785 RVA: 0x001FC6F7 File Offset: 0x001FA8F7
		[Parameter(Mandatory = true, ParameterSetName = "RemoteMailboxRestore")]
		public RemoteRestoreType RemoteRestoreType
		{
			get
			{
				return (RemoteRestoreType)base.Fields["RemoteRestoreType"];
			}
			set
			{
				base.Fields["RemoteRestoreType"] = value;
			}
		}

		// Token: 0x1700267E RID: 9854
		// (get) Token: 0x06007C2A RID: 31786 RVA: 0x001FC70F File Offset: 0x001FA90F
		// (set) Token: 0x06007C2B RID: 31787 RVA: 0x001FC717 File Offset: 0x001FA917
		[Parameter(Mandatory = true, ParameterSetName = "RemoteMailboxRestore")]
		public new Fqdn RemoteHostName
		{
			get
			{
				return base.RemoteHostName;
			}
			set
			{
				base.RemoteHostName = value;
			}
		}

		// Token: 0x1700267F RID: 9855
		// (get) Token: 0x06007C2C RID: 31788 RVA: 0x001FC720 File Offset: 0x001FA920
		// (set) Token: 0x06007C2D RID: 31789 RVA: 0x001FC728 File Offset: 0x001FA928
		[Parameter(Mandatory = false, ParameterSetName = "RemoteMailboxRestore")]
		public new PSCredential RemoteCredential
		{
			get
			{
				return base.RemoteCredential;
			}
			set
			{
				base.RemoteCredential = value;
			}
		}

		// Token: 0x17002680 RID: 9856
		// (get) Token: 0x06007C2E RID: 31790 RVA: 0x001FC731 File Offset: 0x001FA931
		// (set) Token: 0x06007C2F RID: 31791 RVA: 0x001FC739 File Offset: 0x001FA939
		[Parameter(Mandatory = false, ParameterSetName = "RemoteMailboxRestore")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMailboxRestore")]
		public new SwitchParameter AllowLegacyDNMismatch
		{
			get
			{
				return base.AllowLegacyDNMismatch;
			}
			set
			{
				base.AllowLegacyDNMismatch = value;
			}
		}

		// Token: 0x17002681 RID: 9857
		// (get) Token: 0x06007C30 RID: 31792 RVA: 0x001FC742 File Offset: 0x001FA942
		// (set) Token: 0x06007C31 RID: 31793 RVA: 0x001FC74A File Offset: 0x001FA94A
		[Parameter(Mandatory = false, ParameterSetName = "RemoteMailboxRestore")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMailboxRestore")]
		public new string[] IncludeFolders
		{
			get
			{
				return base.IncludeFolders;
			}
			set
			{
				base.IncludeFolders = value;
			}
		}

		// Token: 0x17002682 RID: 9858
		// (get) Token: 0x06007C32 RID: 31794 RVA: 0x001FC753 File Offset: 0x001FA953
		// (set) Token: 0x06007C33 RID: 31795 RVA: 0x001FC75B File Offset: 0x001FA95B
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMailboxRestore")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoteMailboxRestore")]
		public new string[] ExcludeFolders
		{
			get
			{
				return base.ExcludeFolders;
			}
			set
			{
				base.ExcludeFolders = value;
			}
		}

		// Token: 0x17002683 RID: 9859
		// (get) Token: 0x06007C34 RID: 31796 RVA: 0x001FC764 File Offset: 0x001FA964
		// (set) Token: 0x06007C35 RID: 31797 RVA: 0x001FC76C File Offset: 0x001FA96C
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMailboxRestore")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoteMailboxRestore")]
		public new SwitchParameter ExcludeDumpster
		{
			get
			{
				return base.ExcludeDumpster;
			}
			set
			{
				base.ExcludeDumpster = value;
			}
		}

		// Token: 0x17002684 RID: 9860
		// (get) Token: 0x06007C36 RID: 31798 RVA: 0x001FC775 File Offset: 0x001FA975
		// (set) Token: 0x06007C37 RID: 31799 RVA: 0x001FC77D File Offset: 0x001FA97D
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMailboxRestore")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoteMailboxRestore")]
		public new ConflictResolutionOption ConflictResolutionOption
		{
			get
			{
				return base.ConflictResolutionOption;
			}
			set
			{
				base.ConflictResolutionOption = value;
			}
		}

		// Token: 0x17002685 RID: 9861
		// (get) Token: 0x06007C38 RID: 31800 RVA: 0x001FC786 File Offset: 0x001FA986
		// (set) Token: 0x06007C39 RID: 31801 RVA: 0x001FC78E File Offset: 0x001FA98E
		[Parameter(Mandatory = false, ParameterSetName = "RemoteMailboxRestore")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMailboxRestore")]
		public new FAICopyOption AssociatedMessagesCopyOption
		{
			get
			{
				return base.AssociatedMessagesCopyOption;
			}
			set
			{
				base.AssociatedMessagesCopyOption = value;
			}
		}

		// Token: 0x17002686 RID: 9862
		// (get) Token: 0x06007C3A RID: 31802 RVA: 0x001FC797 File Offset: 0x001FA997
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewMailboxRestoreRequest(base.RequestName);
			}
		}

		// Token: 0x06007C3B RID: 31803 RVA: 0x001FC7A4 File Offset: 0x001FA9A4
		protected override void InternalStateReset()
		{
			this.sourceMailboxDN = null;
			this.sourceMailboxGuid = Guid.Empty;
			this.sourceDatabase = null;
			this.restoreFlags = MailboxRestoreType.None;
			this.targetUser = null;
			base.InternalStateReset();
		}

		// Token: 0x06007C3C RID: 31804 RVA: 0x001FC7D4 File Offset: 0x001FA9D4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				base.ValidateRootFolders(this.SourceRootFolder, this.TargetRootFolder);
				bool wildcardedSearch = false;
				if (!string.IsNullOrEmpty(base.Name))
				{
					base.ValidateName();
					base.RequestName = base.Name;
				}
				else
				{
					wildcardedSearch = true;
					base.RequestName = "MailboxRestore";
				}
				this.targetUser = RequestTaskHelper.ResolveADUser(base.RecipSession, base.GCSession, base.ServerSettings, this.TargetMailbox, base.OptionalIdentityData, base.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), true);
				this.CheckForInvalidPublicFolderRestoreParameters();
				if (this.targetUser.HasLocalArchive && this.targetUser.RecipientType == RecipientType.MailUser && this.targetUser.Database == null && !this.TargetIsArchive)
				{
					base.WriteError(new MissingArchiveParameterForRestorePermanentException(this.targetUser.ToString()), ErrorCategory.InvalidArgument, this.TargetMailbox);
				}
				if (this.targetUser.RecipientType != RecipientType.UserMailbox && (!this.TargetIsArchive || this.targetUser.RecipientType != RecipientType.MailUser))
				{
					base.WriteError(new InvalidRecipientTypePermanentException(this.targetUser.ToString(), this.targetUser.RecipientType.ToString()), ErrorCategory.InvalidArgument, this.TargetMailbox);
				}
				if (this.TargetIsArchive && (this.targetUser.ArchiveGuid == Guid.Empty || this.targetUser.ArchiveDatabase == null))
				{
					base.WriteError(new MailboxLacksArchivePermanentException(this.targetUser.ToString()), ErrorCategory.InvalidArgument, this.TargetIsArchive);
				}
				if (!this.TargetIsArchive && this.targetUser.Database == null)
				{
					base.WriteError(new MailboxLacksDatabasePermanentException(this.targetUser.ToString()), ErrorCategory.InvalidArgument, this.TargetMailbox);
				}
				if (base.ParameterSetName.Equals("RemoteMailboxRestore"))
				{
					if (!Guid.TryParse(this.SourceStoreMailbox.RawIdentity, out this.sourceMailboxGuid))
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorParameterValueNotAllowed("SourceStoreMailbox")), ErrorCategory.InvalidArgument, this.SourceStoreMailbox);
					}
					if (!base.Fields.IsModified("AllowLegacyDNMismatch") || !this.AllowLegacyDNMismatch)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorParameterValueNotAllowed("AllowLegacyDNMismatch")), ErrorCategory.InvalidArgument, this.AllowLegacyDNMismatch);
					}
					base.Flags = (RequestFlags.CrossOrg | RequestFlags.Pull);
					switch (this.RemoteRestoreType)
					{
					case RemoteRestoreType.RecoveryDatabase:
						this.restoreFlags |= MailboxRestoreType.Recovery;
						this.restoreFlags |= MailboxRestoreType.SoftDeleted;
						break;
					case RemoteRestoreType.DisconnectedMailbox:
						this.restoreFlags |= MailboxRestoreType.SoftDeleted;
						break;
					case RemoteRestoreType.SoftDeletedRecipient:
						this.restoreFlags |= MailboxRestoreType.SoftDeletedRecipient;
						break;
					default:
						base.WriteError(new RecipientTaskException(Strings.ErrorParameterValueNotAllowed("RemoteRestoreType")), ErrorCategory.InvalidArgument, this.RemoteRestoreType);
						break;
					}
				}
				else
				{
					base.Flags = (RequestFlags.IntraOrg | RequestFlags.Pull);
					string fqdn;
					string serverExchangeLegacyDn;
					ADObjectId adobjectId;
					int num;
					MailboxDatabase mailboxDatabase = base.CheckDatabase<MailboxDatabase>(this.SourceDatabase, NewRequest<MailboxRestoreRequest>.DatabaseSide.Source, this.SourceDatabase, out fqdn, out serverExchangeLegacyDn, out adobjectId, out num);
					if (mailboxDatabase.Recovery)
					{
						this.restoreFlags |= MailboxRestoreType.Recovery;
					}
					this.sourceDatabase = mailboxDatabase.Id;
					this.SourceStoreMailbox.Flags |= 1UL;
					using (MapiSession mapiSession = new MapiAdministrationSession(serverExchangeLegacyDn, Fqdn.Parse(fqdn)))
					{
						using (MailboxStatistics mailboxStatistics = (MailboxStatistics)base.GetDataObject<MailboxStatistics>(this.SourceStoreMailbox, mapiSession, MapiTaskHelper.ConvertDatabaseADObjectToDatabaseId(mailboxDatabase), new LocalizedString?(Strings.ErrorStoreMailboxNotFound(this.SourceStoreMailbox.ToString(), this.SourceDatabase.ToString())), new LocalizedString?(Strings.ErrorStoreMailboxNotUnique(this.SourceStoreMailbox.ToString(), this.SourceDatabase.ToString()))))
						{
							MailboxState? disconnectReason = mailboxStatistics.DisconnectReason;
							if (mailboxStatistics.MailboxType == StoreMailboxType.PublicFolderPrimary || mailboxStatistics.MailboxType == StoreMailboxType.PublicFolderSecondary)
							{
								this.restoreFlags |= MailboxRestoreType.PublicFolderMailbox;
							}
							bool flag = false;
							if (disconnectReason == null && !mailboxDatabase.Recovery)
							{
								mapiSession.Administration.SyncMailboxWithDS(mailboxDatabase.Guid, mailboxStatistics.MailboxGuid);
								using (MailboxStatistics mailboxStatistics2 = (MailboxStatistics)base.GetDataObject<MailboxStatistics>(this.SourceStoreMailbox, mapiSession, MapiTaskHelper.ConvertDatabaseADObjectToDatabaseId(mailboxDatabase), new LocalizedString?(Strings.ErrorStoreMailboxNotFound(this.SourceStoreMailbox.ToString(), this.SourceDatabase.ToString())), new LocalizedString?(Strings.ErrorStoreMailboxNotUnique(this.SourceStoreMailbox.ToString(), this.SourceDatabase.ToString()))))
								{
									disconnectReason = mailboxStatistics2.DisconnectReason;
									if (disconnectReason == null)
									{
										if (this.targetUser.OrganizationId != null && this.targetUser.OrganizationId.OrganizationalUnit != null && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled)
										{
											IRecipientSession recipientSession = CommonUtils.CreateRecipientSession(mailboxStatistics.ExternalDirectoryOrganizationId, null, null);
											ADRecipient adrecipient = this.TargetIsArchive ? recipientSession.FindByExchangeGuidIncludingArchive(mailboxStatistics.MailboxGuid) : recipientSession.FindByExchangeGuid(mailboxStatistics.MailboxGuid);
											flag = (adrecipient != null && adrecipient.RecipientSoftDeletedStatus != 0);
										}
										if (!this.IsPublicFolderMailboxRestore && !flag)
										{
											base.WriteError(new CannotRestoreConnectedMailboxPermanentException(this.SourceStoreMailbox.ToString()), ErrorCategory.InvalidArgument, this.SourceStoreMailbox);
										}
									}
								}
							}
							if (flag)
							{
								this.restoreFlags |= MailboxRestoreType.SoftDeletedRecipient;
							}
							else if (disconnectReason != null)
							{
								if (disconnectReason != MailboxState.SoftDeleted)
								{
									this.restoreFlags |= MailboxRestoreType.Disabled;
								}
								else
								{
									this.restoreFlags |= MailboxRestoreType.SoftDeleted;
								}
							}
							this.sourceMailboxGuid = mailboxStatistics.MailboxGuid;
							this.sourceMailboxDN = mailboxStatistics.LegacyDN;
						}
					}
					if ((this.TargetIsArchive && this.sourceMailboxGuid == this.targetUser.ArchiveGuid && this.sourceDatabase.Equals(this.targetUser.ArchiveDatabase)) || (!this.TargetIsArchive && this.sourceMailboxGuid == this.targetUser.ExchangeGuid && this.sourceDatabase.Equals(this.targetUser.Database)))
					{
						base.WriteError(new CannotRestoreIntoSelfPermanentException(this.targetUser.ToString()), ErrorCategory.InvalidArgument, this.TargetMailbox);
					}
				}
				if (this.restoreFlags.HasFlag(MailboxRestoreType.PublicFolderMailbox))
				{
					if (this.targetUser.RecipientTypeDetails != RecipientTypeDetails.PublicFolderMailbox)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorCannotRestoreFromPublicToPrivateMailbox), ErrorCategory.InvalidArgument, this.SourceStoreMailbox);
					}
				}
				else if (this.targetUser.RecipientTypeDetails == RecipientTypeDetails.PublicFolderMailbox)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorCannotRestoreFromPrivateToPublicMailbox), ErrorCategory.InvalidArgument, this.SourceStoreMailbox);
				}
				base.RescopeToOrgId(this.targetUser.OrganizationId);
				if (base.ParameterSetName.Equals("RemoteMailboxRestore"))
				{
					base.PerRecordReportEntries.Add(new ReportEntry(MrsStrings.ReportRequestAllowedMismatch(base.ExecutingUserIdentity)));
				}
				else
				{
					base.ValidateLegacyDNMatch(this.sourceMailboxDN, this.targetUser, this.TargetMailbox);
				}
				ADObjectId mdbId = null;
				ADObjectId mdbServerSite = null;
				this.LocateAndChooseMdb(null, this.TargetIsArchive ? this.targetUser.ArchiveDatabase : this.targetUser.Database, null, this.TargetMailbox, this.TargetMailbox, out mdbId, out mdbServerSite);
				base.MdbId = mdbId;
				base.MdbServerSite = mdbServerSite;
				base.RequestName = this.CheckRequestNameAvailability(base.RequestName, this.targetUser.Id, true, MRSRequestType.MailboxRestore, this.TargetMailbox, wildcardedSearch);
				base.InternalValidate();
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007C3D RID: 31805 RVA: 0x001FD028 File Offset: 0x001FB228
		protected override void SetRequestProperties(TransactionalRequestJob dataObject)
		{
			base.SetRequestProperties(dataObject);
			dataObject.RequestType = MRSRequestType.MailboxRestore;
			if (dataObject.WorkloadType == RequestWorkloadType.None)
			{
				dataObject.WorkloadType = RequestWorkloadType.Local;
			}
			if (this.targetUser != null)
			{
				dataObject.TargetUserId = this.targetUser.Id;
				dataObject.TargetUser = this.targetUser;
			}
			if (!string.IsNullOrEmpty(this.SourceRootFolder))
			{
				dataObject.SourceRootFolder = this.SourceRootFolder;
			}
			dataObject.SourceIsArchive = false;
			dataObject.SourceExchangeGuid = this.sourceMailboxGuid;
			dataObject.SourceDatabase = this.sourceDatabase;
			dataObject.MailboxRestoreFlags = new MailboxRestoreType?(this.restoreFlags);
			if (!string.IsNullOrEmpty(this.TargetRootFolder))
			{
				dataObject.TargetRootFolder = this.TargetRootFolder;
			}
			if (this.TargetIsArchive)
			{
				dataObject.TargetIsArchive = true;
				dataObject.TargetExchangeGuid = this.targetUser.ArchiveGuid;
				dataObject.TargetDatabase = ADObjectIdResolutionHelper.ResolveDN(this.targetUser.ArchiveDatabase);
			}
			else
			{
				dataObject.TargetIsArchive = false;
				dataObject.TargetExchangeGuid = this.targetUser.ExchangeGuid;
				dataObject.TargetDatabase = ADObjectIdResolutionHelper.ResolveDN(this.targetUser.Database);
			}
			dataObject.TargetAlias = this.targetUser.Alias;
			dataObject.AllowedToFinishMove = true;
			if (this.IsPublicFolderMailboxRestore)
			{
				dataObject.SkipFolderRules = true;
			}
			if (base.ParameterSetName.Equals("RemoteMailboxRestore"))
			{
				dataObject.RemoteDatabaseGuid = new Guid?(this.RemoteDatabaseGuid);
				dataObject.RemoteHostName = this.RemoteHostName;
				dataObject.RemoteCredential = RequestTaskHelper.GetNetworkCredential(this.RemoteCredential, new AuthenticationMethod?(AuthenticationMethod.WindowsIntegrated));
			}
		}

		// Token: 0x06007C3E RID: 31806 RVA: 0x001FD1B8 File Offset: 0x001FB3B8
		protected override MailboxRestoreRequest ConvertToPresentationObject(TransactionalRequestJob dataObject)
		{
			if (dataObject.IndexEntries != null && dataObject.IndexEntries.Count >= 1)
			{
				return new MailboxRestoreRequest(dataObject.IndexEntries[0]);
			}
			base.WriteError(new RequestIndexEntriesAbsentPermanentException(base.RequestName), ErrorCategory.InvalidArgument, this.TargetMailbox);
			return null;
		}

		// Token: 0x06007C3F RID: 31807 RVA: 0x001FD206 File Offset: 0x001FB406
		protected override bool IsSupportedDatabaseVersion(int serverVersion, NewRequest<MailboxRestoreRequest>.DatabaseSide databaseSide)
		{
			if (databaseSide == NewRequest<MailboxRestoreRequest>.DatabaseSide.Source)
			{
				return serverVersion >= Server.E14MinVersion;
			}
			return base.IsSupportedDatabaseVersion(serverVersion, databaseSide);
		}

		// Token: 0x06007C40 RID: 31808 RVA: 0x001FD220 File Offset: 0x001FB420
		private void CheckForInvalidPublicFolderRestoreParameters()
		{
			if (this.targetUser.RecipientTypeDetails == RecipientTypeDetails.PublicFolderMailbox && base.Fields.IsModified("TargetRootFolder"))
			{
				base.WriteError(new TaskArgumentException(Strings.ErrorInvalidParameterForPublicFolderRestore("TargetRootFolder")), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x04003D62 RID: 15714
		public const string DefaultMailboxRestoreName = "MailboxRestore";

		// Token: 0x04003D63 RID: 15715
		public const string TaskNoun = "MailboxRestoreRequest";

		// Token: 0x04003D64 RID: 15716
		public const string ParameterSetLocalMailboxRestore = "MigrationLocalMailboxRestore";

		// Token: 0x04003D65 RID: 15717
		public const string ParameterSetRemoteMailboxRestore = "RemoteMailboxRestore";

		// Token: 0x04003D66 RID: 15718
		private string sourceMailboxDN;

		// Token: 0x04003D67 RID: 15719
		private Guid sourceMailboxGuid;

		// Token: 0x04003D68 RID: 15720
		private ADObjectId sourceDatabase;

		// Token: 0x04003D69 RID: 15721
		private MailboxRestoreType restoreFlags;

		// Token: 0x04003D6A RID: 15722
		private ADUser targetUser;
	}
}
