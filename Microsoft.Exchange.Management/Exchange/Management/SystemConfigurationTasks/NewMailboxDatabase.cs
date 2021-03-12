using System;
using System.Globalization;
using System.Management.Automation;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200098D RID: 2445
	[Cmdlet("New", "MailboxDatabase", SupportsShouldProcess = true, DefaultParameterSetName = "NonRecovery")]
	public sealed class NewMailboxDatabase : NewDatabaseTask<MailboxDatabase>
	{
		// Token: 0x17001A05 RID: 6661
		// (get) Token: 0x0600574B RID: 22347 RVA: 0x0016A7E2 File Offset: 0x001689E2
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("Recovery" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageNewMailboxDatabaseRecovery(this.Name.ToString());
				}
				return Strings.ConfirmationMessageNewMailboxDatabaseNonRecovery(this.Name.ToString());
			}
		}

		// Token: 0x17001A06 RID: 6662
		// (get) Token: 0x0600574C RID: 22348 RVA: 0x0016A817 File Offset: 0x00168A17
		// (set) Token: 0x0600574D RID: 22349 RVA: 0x0016A81F File Offset: 0x00168A1F
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, Position = 0, ParameterSetName = "NonRecovery")]
		[Parameter(Mandatory = false, Position = 0, ParameterSetName = "Recovery")]
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

		// Token: 0x17001A07 RID: 6663
		// (get) Token: 0x0600574E RID: 22350 RVA: 0x0016A828 File Offset: 0x00168A28
		// (set) Token: 0x0600574F RID: 22351 RVA: 0x0016A83F File Offset: 0x00168A3F
		[Parameter(ParameterSetName = "NonRecovery")]
		public DatabaseIdParameter PublicFolderDatabase
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["PublicFolderDatabase"];
			}
			set
			{
				base.Fields["PublicFolderDatabase"] = value;
			}
		}

		// Token: 0x17001A08 RID: 6664
		// (get) Token: 0x06005750 RID: 22352 RVA: 0x0016A852 File Offset: 0x00168A52
		// (set) Token: 0x06005751 RID: 22353 RVA: 0x0016A869 File Offset: 0x00168A69
		[Parameter(ParameterSetName = "NonRecovery")]
		public OfflineAddressBookIdParameter OfflineAddressBook
		{
			get
			{
				return (OfflineAddressBookIdParameter)base.Fields["OfflineAddressBook"];
			}
			set
			{
				base.Fields["OfflineAddressBook"] = value;
			}
		}

		// Token: 0x17001A09 RID: 6665
		// (get) Token: 0x06005752 RID: 22354 RVA: 0x0016A87C File Offset: 0x00168A7C
		// (set) Token: 0x06005753 RID: 22355 RVA: 0x0016A8A2 File Offset: 0x00168AA2
		[Parameter(Mandatory = true, ParameterSetName = "Recovery")]
		public SwitchParameter Recovery
		{
			get
			{
				return (SwitchParameter)(base.Fields["Recovery"] ?? false);
			}
			set
			{
				base.Fields["Recovery"] = value;
			}
		}

		// Token: 0x17001A0A RID: 6666
		// (get) Token: 0x06005754 RID: 22356 RVA: 0x0016A8BA File Offset: 0x00168ABA
		// (set) Token: 0x06005755 RID: 22357 RVA: 0x0016A8E0 File Offset: 0x00168AE0
		[Parameter(ParameterSetName = "NonRecovery")]
		public bool IsExcludedFromProvisioning
		{
			get
			{
				return (bool)(base.Fields["IsExcludedFromProvisioning"] ?? Datacenter.IsMicrosoftHostedOnly(true));
			}
			set
			{
				base.Fields["IsExcludedFromProvisioning"] = value;
			}
		}

		// Token: 0x17001A0B RID: 6667
		// (get) Token: 0x06005756 RID: 22358 RVA: 0x0016A8F8 File Offset: 0x00168AF8
		// (set) Token: 0x06005757 RID: 22359 RVA: 0x0016A919 File Offset: 0x00168B19
		[Parameter(ParameterSetName = "NonRecovery")]
		public bool IsSuspendedFromProvisioning
		{
			get
			{
				return (bool)(base.Fields["IsSuspendedProvisioning"] ?? false);
			}
			set
			{
				base.Fields["IsSuspendedProvisioning"] = value;
			}
		}

		// Token: 0x17001A0C RID: 6668
		// (get) Token: 0x06005758 RID: 22360 RVA: 0x0016A931 File Offset: 0x00168B31
		// (set) Token: 0x06005759 RID: 22361 RVA: 0x0016A948 File Offset: 0x00168B48
		[Parameter(ParameterSetName = "NonRecovery")]
		public SwitchParameter IsExcludedFromInitialProvisioning
		{
			get
			{
				return (SwitchParameter)base.Fields["IsExcludedFromInitialProvisioning"];
			}
			set
			{
				base.Fields["IsExcludedFromInitialProvisioning"] = value;
			}
		}

		// Token: 0x17001A0D RID: 6669
		// (get) Token: 0x0600575A RID: 22362 RVA: 0x0016A960 File Offset: 0x00168B60
		// (set) Token: 0x0600575B RID: 22363 RVA: 0x0016A977 File Offset: 0x00168B77
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
		public MailboxProvisioningAttributes MailboxProvisioningAttributes
		{
			get
			{
				return (MailboxProvisioningAttributes)base.Fields[DatabaseSchema.MailboxProvisioningAttributes];
			}
			set
			{
				base.Fields[DatabaseSchema.MailboxProvisioningAttributes] = value;
			}
		}

		// Token: 0x17001A0E RID: 6670
		// (get) Token: 0x0600575C RID: 22364 RVA: 0x0016A98A File Offset: 0x00168B8A
		// (set) Token: 0x0600575D RID: 22365 RVA: 0x0016A9AB File Offset: 0x00168BAB
		[Parameter(ParameterSetName = "NonRecovery")]
		public bool AutoDagExcludeFromMonitoring
		{
			get
			{
				return (bool)(base.Fields["AutoDagExcludeFromMonitoring"] ?? false);
			}
			set
			{
				base.Fields["AutoDagExcludeFromMonitoring"] = value;
			}
		}

		// Token: 0x17001A0F RID: 6671
		// (get) Token: 0x0600575E RID: 22366 RVA: 0x0016A9C3 File Offset: 0x00168BC3
		protected override NewDatabaseTask<MailboxDatabase>.ExchangeDatabaseType DatabaseType
		{
			get
			{
				return NewDatabaseTask<MailboxDatabase>.ExchangeDatabaseType.Private;
			}
		}

		// Token: 0x0600575F RID: 22367 RVA: 0x0016A9C8 File Offset: 0x00168BC8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			bool flag = false;
			try
			{
				base.InternalProcessRecord();
				this.WriteWarning(Strings.WarnAdministratorToRestartService(base.OwnerServer.Name));
				flag = true;
			}
			finally
			{
				if (!flag && this.preExistingDatabase == null)
				{
					this.RollbackOperation(this.DataObject, this.dbCopy, this.systemMailbox);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005760 RID: 22368 RVA: 0x0016AA38 File Offset: 0x00168C38
		private void RunConfigurationUpdaterRpc(Database db)
		{
			DatabaseTasksHelper.RunConfigurationUpdaterRpcAsync(base.OwnerServer.Fqdn, db, ReplayConfigChangeHints.DbCopyAdded, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
		}

		// Token: 0x06005761 RID: 22369 RVA: 0x0016AA68 File Offset: 0x00168C68
		private void RollbackOperation(MailboxDatabase mdb, DatabaseCopy dbCopy, ADSystemMailbox systemMailbox)
		{
			if (mdb == null || dbCopy == null)
			{
				if (systemMailbox != null)
				{
					try
					{
						base.WriteVerbose(Strings.VerboseDeleteSystemMailbox(systemMailbox.Id.ToString()));
						this.RecipientSessionForSystemMailbox.Delete(systemMailbox);
					}
					catch (DataSourceTransientException ex)
					{
						this.WriteWarning(Strings.FailedToDeleteSystemMailbox(systemMailbox.Identity.ToString(), ex.Message));
						TaskLogger.Trace("Failed to delete System Mailbox {0} when rolling back created database object '{1}'. {2}", new object[]
						{
							systemMailbox.Identity,
							mdb.Identity,
							ex.ToString()
						});
					}
					catch (DataSourceOperationException ex2)
					{
						this.WriteWarning(Strings.FailedToDeleteSystemMailbox(systemMailbox.Identity.ToString(), ex2.Message));
						TaskLogger.Trace("Failed to delete System Mailbox {0} when rolling back created database object '{1}'. {2}", new object[]
						{
							systemMailbox.Identity,
							mdb.Identity,
							ex2.ToString()
						});
					}
				}
				if (dbCopy != null)
				{
					try
					{
						base.WriteVerbose(Strings.VerboseDeleteDBCopy(dbCopy.Id.ToString()));
						base.DataSession.Delete(dbCopy);
					}
					catch (DataSourceTransientException ex3)
					{
						this.WriteWarning(Strings.FailedToDeleteDatabaseCopy(dbCopy.Identity.ToString(), ex3.Message));
						TaskLogger.Trace("Failed to delete Database Copy {0} when rolling back created database object '{1}'. {2}", new object[]
						{
							dbCopy.Identity,
							mdb.Identity,
							ex3.ToString()
						});
					}
					catch (DataSourceOperationException ex4)
					{
						this.WriteWarning(Strings.FailedToDeleteDatabaseCopy(dbCopy.Identity.ToString(), ex4.Message));
						TaskLogger.Trace("Failed to delete Database Copy {0} when rolling back created database object '{1}'. {2}", new object[]
						{
							dbCopy.Identity,
							mdb.Identity,
							ex4.ToString()
						});
					}
				}
				if (mdb != null)
				{
					try
					{
						base.WriteVerbose(Strings.VerboseDeleteMDB(mdb.Id.ToString()));
						base.DataSession.Delete(mdb);
						DatabaseTasksHelper.RemoveDatabaseFromClusterDB((ITopologyConfigurationSession)base.DataSession, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError), mdb);
					}
					catch (DataSourceTransientException ex5)
					{
						this.WriteWarning(Strings.FailedToDeleteMailboxDatabase(mdb.Identity.ToString(), ex5.Message));
						TaskLogger.Trace("Failed to delete Mailbox Database {0} when rolling back. {1}", new object[]
						{
							mdb.Identity,
							ex5.ToString()
						});
					}
					catch (DataSourceOperationException ex6)
					{
						this.WriteWarning(Strings.FailedToDeleteMailboxDatabase(mdb.Identity.ToString(), ex6.Message));
						TaskLogger.Trace("Failed to delete Mailbox Database {0} when rolling back. {1}", new object[]
						{
							mdb.Identity,
							ex6.ToString()
						});
					}
					catch (ClusterException ex7)
					{
						this.WriteWarning(Strings.FailedToDeleteMailboxDatabase(mdb.Identity.ToString(), ex7.Message));
						TaskLogger.Trace("Failed to delete Mailbox Database {0} when rolling back. {1}", new object[]
						{
							mdb.Identity,
							ex7.ToString()
						});
					}
				}
			}
		}

		// Token: 0x06005762 RID: 22370 RVA: 0x0016ADB8 File Offset: 0x00168FB8
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			MailboxDatabase mailboxDatabase = (MailboxDatabase)base.PrepareDataObject();
			if (this.preExistingDatabase != null)
			{
				TaskLogger.LogExit();
				return mailboxDatabase;
			}
			if (base.ParameterSetName == "Recovery")
			{
				mailboxDatabase.Recovery = true;
				mailboxDatabase.AllowFileRestore = true;
			}
			else
			{
				mailboxDatabase.Recovery = false;
			}
			this.DataObject.IsExcludedFromProvisioning = this.IsExcludedFromProvisioning;
			if (base.Fields.IsModified("IsSuspendedProvisioning"))
			{
				this.DataObject.IsSuspendedFromProvisioning = this.IsSuspendedFromProvisioning;
			}
			if (base.Fields.IsModified("IsExcludedFromInitialProvisioning"))
			{
				this.DataObject.IsExcludedFromInitialProvisioning = this.IsExcludedFromInitialProvisioning;
			}
			base.PrepareFilePaths(mailboxDatabase.Name, base.ParameterSetName == "Recovery", mailboxDatabase);
			if (base.Fields.IsModified(DatabaseSchema.MailboxProvisioningAttributes))
			{
				this.DataObject.MailboxProvisioningAttributes = this.MailboxProvisioningAttributes;
			}
			TaskLogger.LogExit();
			return mailboxDatabase;
		}

		// Token: 0x06005763 RID: 22371 RVA: 0x0016AEB0 File Offset: 0x001690B0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.preExistingDatabase != null)
			{
				TaskLogger.LogExit();
				return;
			}
			if (this.PublicFolderDatabase == null)
			{
				if (base.OwnerServerPublicFolderDatabases != null && base.OwnerServerPublicFolderDatabases.Length > 0)
				{
					this.DataObject.PublicFolderDatabase = (ADObjectId)base.OwnerServerPublicFolderDatabases[0].Identity;
				}
				else
				{
					base.WriteVerbose(Strings.VerboseFindClosestPublicFolderDatabaseFromServer(base.OwnerServer.Id.ToString()));
					this.DataObject.PublicFolderDatabase = Microsoft.Exchange.Data.Directory.SystemConfiguration.PublicFolderDatabase.FindClosestPublicFolderDatabase(base.DataSession, base.OwnerServer.Id);
				}
			}
			else
			{
				this.PublicFolderDatabase.AllowLegacy = true;
				IConfigurable dataObject = base.GetDataObject<PublicFolderDatabase>(this.PublicFolderDatabase, base.DataSession, null, new LocalizedString?(Strings.ErrorPublicFolderDatabaseNotFound(this.PublicFolderDatabase.ToString())), new LocalizedString?(Strings.ErrorPublicFolderDatabaseNotUnique(this.PublicFolderDatabase.ToString())));
				this.DataObject.PublicFolderDatabase = (ADObjectId)dataObject.Identity;
			}
			if (this.OfflineAddressBook != null)
			{
				IConfigurable dataObject2 = base.GetDataObject<OfflineAddressBook>(this.OfflineAddressBook, base.DataSession, null, new LocalizedString?(Strings.ErrorOfflineAddressBookNotFound(this.OfflineAddressBook.ToString())), new LocalizedString?(Strings.ErrorOfflineAddressBookNotUnique(this.OfflineAddressBook.ToString())));
				this.DataObject.OfflineAddressBook = (ADObjectId)dataObject2.Identity;
			}
			base.ValidateFilePaths(base.ParameterSetName == "Recovery");
			if (base.Fields.IsModified("AutoDagExcludeFromMonitoring"))
			{
				this.DataObject.AutoDagExcludeFromMonitoring = this.AutoDagExcludeFromMonitoring;
			}
			else
			{
				DatabaseAvailabilityGroup databaseAvailabilityGroup = DagTaskHelper.ReadDag(this.DataObject.MasterServerOrAvailabilityGroup, this.ConfigurationSession);
				if (databaseAvailabilityGroup != null)
				{
					DatabaseAvailabilityGroupConfiguration databaseAvailabilityGroupConfiguration = DagConfigurationHelper.ReadDagConfig(databaseAvailabilityGroup.DatabaseAvailabilityGroupConfiguration, this.ConfigurationSession);
					if (databaseAvailabilityGroupConfiguration != null)
					{
						DagConfigurationHelper dagConfigurationHelper = DagConfigurationHelper.Deserialize(databaseAvailabilityGroupConfiguration.ConfigurationXML);
						if (dagConfigurationHelper.MinCopiesPerDatabaseForMonitoring > 1)
						{
							this.DataObject.AutoDagExcludeFromMonitoring = true;
						}
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005764 RID: 22372 RVA: 0x0016B0A0 File Offset: 0x001692A0
		protected override void WriteResult()
		{
			TaskLogger.LogEnter();
			this.dbCopy = base.SaveDBCopy();
			if (this.preExistingDatabase != null)
			{
				TaskLogger.LogExit();
				return;
			}
			MailboxDatabase mailboxDatabase = (MailboxDatabase)base.GetDataObject(new DatabaseIdParameter((ADObjectId)this.DataObject.Identity));
			try
			{
				int maximumSupportedDatabaseSchemaVersion = DatabaseTasksHelper.GetMaximumSupportedDatabaseSchemaVersion((ITopologyConfigurationSession)base.DataSession, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError), mailboxDatabase);
				DatabaseTasksHelper.SetRequestedDatabaseSchemaVersion((ITopologyConfigurationSession)base.DataSession, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), null, mailboxDatabase, maximumSupportedDatabaseSchemaVersion);
			}
			catch (ClusterException)
			{
			}
			mailboxDatabase.CompleteAllCalculatedProperties();
			this.RunConfigurationUpdaterRpc(mailboxDatabase);
			this.systemMailbox = NewMailboxDatabase.SaveSystemMailbox(mailboxDatabase, base.OwnerServer, base.RootOrgContainerId, (ITopologyConfigurationSession)this.ConfigurationSession, this.RecipientSessionForSystemMailbox, this.forcedReplicationSites, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			base.WriteObject(mailboxDatabase);
			TaskLogger.LogExit();
		}

		// Token: 0x06005765 RID: 22373 RVA: 0x0016B1C8 File Offset: 0x001693C8
		internal static ADSystemMailbox SaveSystemMailbox(MailboxDatabase mdb, Server owningServer, ADObjectId rootOrgContainerId, ITopologyConfigurationSession configSession, IRecipientSession recipientSession, ADObjectId[] forcedReplicationSites, Task.TaskWarningLoggingDelegate writeWarning, Task.TaskVerboseLoggingDelegate writeVerbose)
		{
			TaskLogger.LogEnter();
			bool useConfigNC = configSession.UseConfigNC;
			bool useGlobalCatalog = configSession.UseGlobalCatalog;
			string text = "SystemMailbox" + mdb.Guid.ToString("B");
			SecurityIdentifier securityIdentifier = new SecurityIdentifier("SY");
			ADSystemMailbox adsystemMailbox = new ADSystemMailbox();
			adsystemMailbox.StampPersistableDefaultValues();
			adsystemMailbox.Name = text;
			adsystemMailbox.DisplayName = text;
			adsystemMailbox.Alias = text;
			adsystemMailbox.HiddenFromAddressListsEnabled = true;
			adsystemMailbox.Database = mdb.Id;
			if (owningServer == null)
			{
				throw new InvalidOperationException(Strings.ErrorDBOwningServerNotFound(mdb.Identity.ToString()));
			}
			adsystemMailbox.ServerLegacyDN = owningServer.ExchangeLegacyDN;
			adsystemMailbox.ExchangeGuid = Guid.NewGuid();
			AcceptedDomain defaultAcceptedDomain = configSession.GetDefaultAcceptedDomain();
			if (defaultAcceptedDomain == null || defaultAcceptedDomain.DomainName == null || defaultAcceptedDomain.DomainName.Domain == null)
			{
				throw new ManagementObjectNotFoundException(Strings.ErrorNoDefaultAcceptedDomainFound(mdb.Identity.ToString()));
			}
			adsystemMailbox.EmailAddresses.Add(ProxyAddress.Parse("SMTP:" + adsystemMailbox.Alias + "@" + defaultAcceptedDomain.DomainName.Domain.ToString()));
			adsystemMailbox.WindowsEmailAddress = adsystemMailbox.PrimarySmtpAddress;
			adsystemMailbox.SendModerationNotifications = TransportModerationNotificationFlags.Never;
			Organization organization = configSession.Read<Organization>(rootOrgContainerId);
			if (organization == null)
			{
				throw new ManagementObjectNotFoundException(Strings.ErrorOrganizationNotFound(rootOrgContainerId.Name));
			}
			string parentLegacyDN = string.Format(CultureInfo.InvariantCulture, "{0}/ou={1}/cn=Recipients", new object[]
			{
				organization.LegacyExchangeDN,
				configSession.GetAdministrativeGroupId().Name
			});
			adsystemMailbox.LegacyExchangeDN = LegacyDN.GenerateLegacyDN(parentLegacyDN, adsystemMailbox);
			ADComputer adcomputer;
			try
			{
				configSession.UseConfigNC = false;
				configSession.UseGlobalCatalog = true;
				adcomputer = configSession.FindComputerByHostName(owningServer.Name);
			}
			finally
			{
				configSession.UseConfigNC = useConfigNC;
				configSession.UseGlobalCatalog = useGlobalCatalog;
			}
			if (adcomputer == null)
			{
				throw new ManagementObjectNotFoundException(Strings.ErrorDBOwningServerNotFound(mdb.Identity.ToString()));
			}
			ADObjectId adobjectId = adcomputer.Id.DomainId;
			adobjectId = adobjectId.GetChildId("Microsoft Exchange System Objects");
			adsystemMailbox.SetId(adobjectId.GetChildId(text));
			GenericAce[] aces = new GenericAce[]
			{
				new CommonAce(AceFlags.None, AceQualifier.AccessAllowed, 131075, securityIdentifier, false, null)
			};
			DirectoryCommon.SetAclOnAlternateProperty(adsystemMailbox, aces, ADSystemAttendantMailboxSchema.ExchangeSecurityDescriptor, securityIdentifier, securityIdentifier);
			recipientSession.LinkResolutionServer = mdb.OriginatingServer;
			bool enforceDefaultScope = recipientSession.EnforceDefaultScope;
			try
			{
				writeVerbose(TaskVerboseStringHelper.GetSaveObjectVerboseString(adsystemMailbox, recipientSession, typeof(ADSystemMailbox)));
				recipientSession.EnforceDefaultScope = false;
				recipientSession.Save(adsystemMailbox);
			}
			catch (ADConstraintViolationException ex)
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ex.Server, false, ConsistencyMode.PartiallyConsistent, configSession.SessionSettings, 705, "SaveSystemMailbox", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\database\\NewMailboxDatabase.cs");
				if (!tenantOrTopologyConfigurationSession.ReplicateSingleObjectToTargetDC(mdb, ex.Server))
				{
					throw;
				}
				writeVerbose(TaskVerboseStringHelper.GetSaveObjectVerboseString(adsystemMailbox, recipientSession, typeof(ADSystemMailbox)));
				recipientSession.Save(adsystemMailbox);
			}
			finally
			{
				writeVerbose(TaskVerboseStringHelper.GetSourceVerboseString(recipientSession));
				recipientSession.EnforceDefaultScope = enforceDefaultScope;
			}
			if (forcedReplicationSites != null)
			{
				DagTaskHelper.ForceReplication(recipientSession, adsystemMailbox, forcedReplicationSites, mdb.Name, writeWarning, writeVerbose);
			}
			TaskLogger.LogExit();
			return adsystemMailbox;
		}

		// Token: 0x04003264 RID: 12900
		internal const string paramPublicFolderDatabase = "PublicFolderDatabase";

		// Token: 0x04003265 RID: 12901
		internal const string paramOfflineAddressBook = "OfflineAddressBook";

		// Token: 0x04003266 RID: 12902
		internal const string paramRecovery = "Recovery";

		// Token: 0x04003267 RID: 12903
		internal const string paramIsExcludedFromProvisioning = "IsExcludedFromProvisioning";

		// Token: 0x04003268 RID: 12904
		internal const string paramIsSuspendedFromProvisioning = "IsSuspendedProvisioning";

		// Token: 0x04003269 RID: 12905
		internal const string paramIsExcludedFromInitialProvisioning = "IsExcludedFromInitialProvisioning";

		// Token: 0x0400326A RID: 12906
		internal const string paramAutoDagExcludeFromMonitoring = "AutoDagExcludeFromMonitoring";

		// Token: 0x0400326B RID: 12907
		private ADSystemMailbox systemMailbox;
	}
}
