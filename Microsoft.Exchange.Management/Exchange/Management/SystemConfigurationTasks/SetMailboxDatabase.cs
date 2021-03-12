using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000993 RID: 2451
	[Cmdlet("Set", "MailboxDatabase", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailboxDatabase : SetDatabaseTask<MailboxDatabase>
	{
		// Token: 0x17001A18 RID: 6680
		// (get) Token: 0x06005790 RID: 22416 RVA: 0x0016D581 File Offset: 0x0016B781
		// (set) Token: 0x06005791 RID: 22417 RVA: 0x0016D598 File Offset: 0x0016B798
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
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

		// Token: 0x17001A19 RID: 6681
		// (get) Token: 0x06005792 RID: 22418 RVA: 0x0016D5AB File Offset: 0x0016B7AB
		// (set) Token: 0x06005793 RID: 22419 RVA: 0x0016D5C2 File Offset: 0x0016B7C2
		[Parameter(Mandatory = false)]
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

		// Token: 0x17001A1A RID: 6682
		// (get) Token: 0x06005794 RID: 22420 RVA: 0x0016D5D5 File Offset: 0x0016B7D5
		// (set) Token: 0x06005795 RID: 22421 RVA: 0x0016D5EC File Offset: 0x0016B7EC
		[Parameter(Mandatory = false)]
		public RecipientIdParameter JournalRecipient
		{
			get
			{
				return (RecipientIdParameter)base.Fields["JournalRecipient"];
			}
			set
			{
				base.Fields["JournalRecipient"] = value;
			}
		}

		// Token: 0x17001A1B RID: 6683
		// (get) Token: 0x06005796 RID: 22422 RVA: 0x0016D5FF File Offset: 0x0016B7FF
		// (set) Token: 0x06005797 RID: 22423 RVA: 0x0016D616 File Offset: 0x0016B816
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

		// Token: 0x17001A1C RID: 6684
		// (get) Token: 0x06005798 RID: 22424 RVA: 0x0016D629 File Offset: 0x0016B829
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetMailboxDatabase(this.Identity.ToString());
			}
		}

		// Token: 0x06005799 RID: 22425 RVA: 0x0016D63B File Offset: 0x0016B83B
		internal override ADPropertyDefinition[,] GetPropertiesCannotBeSet()
		{
			return SetMailboxDatabase.propertiesCannotBeSet;
		}

		// Token: 0x0600579A RID: 22426 RVA: 0x0016D642 File Offset: 0x0016B842
		internal override ADPropertyDefinition[] GetDeprecatedProperties()
		{
			return SetMailboxDatabase.deprecatedProperties;
		}

		// Token: 0x0600579B RID: 22427 RVA: 0x0016D64C File Offset: 0x0016B84C
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			MailboxDatabase mailboxDatabase = (MailboxDatabase)this.GetDynamicParameters();
			if (base.Fields.IsModified("PublicFolderDatabase"))
			{
				this.PublicFolderDatabase.AllowLegacy = true;
				PublicFolderDatabase publicFolderDatabase = (PublicFolderDatabase)base.GetDataObject<PublicFolderDatabase>(this.PublicFolderDatabase, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorPublicFolderDatabaseNotFound(this.PublicFolderDatabase.ToString())), new LocalizedString?(Strings.ErrorPublicFolderDatabaseNotUnique(this.PublicFolderDatabase.ToString())));
				mailboxDatabase.PublicFolderDatabase = (ADObjectId)publicFolderDatabase.Identity;
			}
			if (base.Fields.IsModified("OfflineAddressBook"))
			{
				if (this.OfflineAddressBook == null)
				{
					mailboxDatabase.OfflineAddressBook = null;
				}
				else
				{
					OfflineAddressBook offlineAddressBook = (OfflineAddressBook)base.GetDataObject<OfflineAddressBook>(this.OfflineAddressBook, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorOfflineAddressBookNotFound(this.OfflineAddressBook.ToString())), new LocalizedString?(Strings.ErrorOfflineAddressBookNotUnique(this.OfflineAddressBook.ToString())));
					mailboxDatabase.OfflineAddressBook = (ADObjectId)offlineAddressBook.Identity;
				}
			}
			if (base.Fields.IsModified("JournalRecipient"))
			{
				if (this.JournalRecipient == null)
				{
					mailboxDatabase.JournalRecipient = null;
				}
				else
				{
					ADRecipient adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(this.JournalRecipient, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(this.JournalRecipient.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(this.JournalRecipient.ToString())));
					mailboxDatabase.JournalRecipient = (ADObjectId)adrecipient.Identity;
				}
			}
			if (base.Fields.IsModified(DatabaseSchema.MailboxProvisioningAttributes))
			{
				mailboxDatabase.MailboxProvisioningAttributes = this.MailboxProvisioningAttributes;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600579C RID: 22428 RVA: 0x0016D7F4 File Offset: 0x0016B9F4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!this.DataObject.IsExchange2009OrLater)
			{
				if (!base.Fields.IsModified("PublicFolderDatabase"))
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorModifyE12DatabaseNotAllowed), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
				foreach (object obj in this.DataObject.propertyBag.Keys)
				{
					ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)obj;
					if (this.DataObject.IsModified(adpropertyDefinition) && string.Compare(adpropertyDefinition.Name, MailboxDatabaseSchema.PublicFolderDatabase.Name) != 0 && string.Compare(adpropertyDefinition.Name, ADObjectSchema.ObjectState.Name) != 0)
					{
						base.WriteError(new InvalidOperationException(Strings.ErrorModifyE12DatabaseNotAllowed), ErrorCategory.InvalidOperation, this.DataObject.Identity);
					}
				}
			}
			if (this.DataObject.Recovery)
			{
				for (int i = 0; i < SetMailboxDatabase.PropertiesCannotChangedForRecoveryMailboxDatabase.GetLength(0); i++)
				{
					if (this.DataObject.IsModified(SetMailboxDatabase.PropertiesCannotChangedForRecoveryMailboxDatabase[i, 0]))
					{
						base.WriteError(new InvalidOperationException(Strings.ErrorSpecifiedPropertyOfRecoveryMailboxDatabaseCannotChange((SetMailboxDatabase.PropertiesCannotChangedForRecoveryMailboxDatabase[i, 1] ?? SetMailboxDatabase.PropertiesCannotChangedForRecoveryMailboxDatabase[i, 0]).ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
					}
				}
			}
			if (!base.Fields.IsModified("PublicFolderDatabase") && this.DataObject.IsChanged(MailboxDatabaseSchema.PublicFolderDatabase) && this.Instance.PublicFolderDatabase != null && (PublicFolderDatabase)base.GetDataObject<PublicFolderDatabase>(new DatabaseIdParameter(this.Instance.PublicFolderDatabase)
			{
				AllowLegacy = true
			}, base.DataSession, null, new LocalizedString?(Strings.ErrorPublicFolderDatabaseNotFound(this.Instance.PublicFolderDatabase.ToString())), new LocalizedString?(Strings.ErrorPublicFolderDatabaseNotUnique(this.Instance.PublicFolderDatabase.ToString()))) == null)
			{
				return;
			}
			if (!base.Fields.IsModified("OfflineAddressBook") && this.DataObject.IsChanged(MailboxDatabaseSchema.OfflineAddressBook) && this.Instance.OfflineAddressBook != null && base.GetDataObject<OfflineAddressBook>(new OfflineAddressBookIdParameter(this.Instance.OfflineAddressBook), base.DataSession, null, new LocalizedString?(Strings.ErrorOfflineAddressBookNotFound(this.Instance.OfflineAddressBook.ToString())), new LocalizedString?(Strings.ErrorOfflineAddressBookNotUnique(this.Instance.OfflineAddressBook.ToString()))) == null)
			{
				return;
			}
			if (!base.Fields.IsModified("JournalRecipient") && this.DataObject.IsChanged(MailboxDatabaseSchema.JournalRecipient) && this.Instance.JournalRecipient != null && base.GetDataObject<ADRecipient>(new RecipientIdParameter(this.Instance.JournalRecipient), base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(this.Instance.JournalRecipient.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(this.Instance.JournalRecipient.ToString()))) == null)
			{
				return;
			}
			if (this.DataObject.IsChanged(DatabaseSchema.BackgroundDatabaseMaintenance))
			{
				this.WriteWarning(Strings.WarningBackgroundDatabaseMaintenanceChangeRequiresRemount(this.DataObject.Identity.ToString()));
			}
			if (base.Fields.IsChanged(DatabaseSchema.MailboxProvisioningAttributes))
			{
				this.ValidateHighestActivationPreferenceServerLocation();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600579D RID: 22429 RVA: 0x0016DBA0 File Offset: 0x0016BDA0
		private void ValidateHighestActivationPreferenceServerLocation()
		{
			if (!this.MailboxProvisioningAttributes.Attributes.Any((MailboxProvisioningAttribute attribute) => attribute.Key.Equals("Location")))
			{
				return;
			}
			ADObjectId key = this.DataObject.ActivationPreference[0].Key;
			Server server = (Server)base.GetDataObject<Server>(new ServerIdParameter(key), base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound(key.Name)), new LocalizedString?(Strings.ErrorServerNotUnique(key.Name)));
			if (server.MailboxProvisioningAttributes != null)
			{
				MailboxProvisioningAttribute mailboxProvisioningAttribute = server.MailboxProvisioningAttributes.Attributes.FirstOrDefault((MailboxProvisioningAttribute anAttribute) => string.Equals(anAttribute.Key, "Location"));
				if (mailboxProvisioningAttribute != null)
				{
					string value = mailboxProvisioningAttribute.Value;
					string value2 = this.MailboxProvisioningAttributes.Attributes.First((MailboxProvisioningAttribute attribute) => attribute.Key.Equals("Location")).Value;
					if (!string.Equals(value, value2, StringComparison.OrdinalIgnoreCase))
					{
						this.WriteWarning(Strings.Error_DatabaseLocationDoesNotMatchHighestActivationPreferenceServerLocation("Location", server.Name, value, value2));
					}
					return;
				}
			}
		}

		// Token: 0x060057A2 RID: 22434 RVA: 0x0016DCD0 File Offset: 0x0016BED0
		// Note: this type is marked as 'beforefieldinit'.
		static SetMailboxDatabase()
		{
			ADPropertyDefinition[,] array = new ADPropertyDefinition[6, 2];
			array[0, 0] = DatabaseSchema.DatabaseCreated;
			array[1, 0] = DatabaseSchema.EdbFilePath;
			array[2, 0] = DatabaseSchema.Server;
			array[3, 0] = DatabaseSchema.MasterServerOrAvailabilityGroup;
			array[4, 0] = MailboxDatabaseSchema.OriginalDatabase;
			array[5, 0] = DatabaseSchema.Recovery;
			SetMailboxDatabase.propertiesCannotBeSet = array;
			ADPropertyDefinition[,] array2 = new ADPropertyDefinition[19, 2];
			array2[0, 0] = MailboxDatabaseSchema.MailboxRetention;
			array2[1, 0] = MailboxDatabaseSchema.OfflineAddressBook;
			array2[2, 0] = MailboxDatabaseSchema.PublicFolderDatabase;
			array2[3, 0] = MailboxDatabaseSchema.ProhibitSendReceiveQuota;
			array2[4, 0] = DatabaseSchema.DelItemAfterBackupEnum;
			array2[4, 1] = DatabaseSchema.RetainDeletedItemsUntilBackup;
			array2[5, 0] = DatabaseSchema.EdbOfflineAtStartup;
			array2[5, 1] = DatabaseSchema.MountAtStartup;
			array2[6, 0] = DatabaseSchema.FixedFont;
			array2[7, 0] = DatabaseSchema.DeletedItemRetention;
			array2[8, 0] = DatabaseSchema.MaintenanceScheduleBitmaps;
			array2[8, 1] = DatabaseSchema.MaintenanceSchedule;
			array2[9, 0] = DatabaseSchema.MaintenanceScheduleMode;
			array2[10, 0] = MailboxDatabaseSchema.ProhibitSendQuota;
			array2[11, 0] = DatabaseSchema.QuotaNotificationMode;
			array2[12, 0] = DatabaseSchema.QuotaNotificationScheduleBitmaps;
			array2[12, 1] = DatabaseSchema.QuotaNotificationSchedule;
			array2[13, 0] = DatabaseSchema.RestoreInProgress;
			array2[14, 0] = DatabaseSchema.SMimeSignatureEnabled;
			array2[15, 0] = DatabaseSchema.IssueWarningQuota;
			array2[16, 0] = ADConfigurationObjectSchema.AdminDisplayName;
			array2[17, 0] = ADObjectSchema.Name;
			array2[18, 0] = DatabaseSchema.EventHistoryRetentionPeriod;
			SetMailboxDatabase.PropertiesCannotChangedForRecoveryMailboxDatabase = array2;
			SetMailboxDatabase.deprecatedProperties = new ADPropertyDefinition[]
			{
				DatabaseSchema.MaintenanceSchedule,
				MailboxDatabaseSchema.PublicFolderDatabase,
				DatabaseSchema.QuotaNotificationSchedule
			};
		}

		// Token: 0x0400327A RID: 12922
		internal const string paramPublicFolderDatabase = "PublicFolderDatabase";

		// Token: 0x0400327B RID: 12923
		internal const string paramOfflineAddressBook = "OfflineAddressBook";

		// Token: 0x0400327C RID: 12924
		internal const string paramJournalRecipient = "JournalRecipient";

		// Token: 0x0400327D RID: 12925
		private static readonly ADPropertyDefinition[,] propertiesCannotBeSet;

		// Token: 0x0400327E RID: 12926
		private static readonly ADPropertyDefinition[,] PropertiesCannotChangedForRecoveryMailboxDatabase;

		// Token: 0x0400327F RID: 12927
		private static readonly ADPropertyDefinition[] deprecatedProperties;
	}
}
