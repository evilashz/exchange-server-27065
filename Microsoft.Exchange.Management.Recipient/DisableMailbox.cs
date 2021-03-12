using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000065 RID: 101
	[Cmdlet("disable", "Mailbox", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class DisableMailbox : RecipientObjectActionTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0001BD68 File Offset: 0x00019F68
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x0001BD7F File Offset: 0x00019F7F
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override MailboxIdParameter Identity
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0001BD92 File Offset: 0x00019F92
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x0001BD9A File Offset: 0x00019F9A
		[Parameter]
		public SwitchParameter IgnoreDefaultScope
		{
			get
			{
				return base.InternalIgnoreDefaultScope;
			}
			set
			{
				base.InternalIgnoreDefaultScope = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0001BDA3 File Offset: 0x00019FA3
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x0001BDC9 File Offset: 0x00019FC9
		[Parameter(Mandatory = false, ParameterSetName = "Archive")]
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

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0001BDE1 File Offset: 0x00019FE1
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x0001BE07 File Offset: 0x0001A007
		[Parameter(Mandatory = false, ParameterSetName = "RemoteArchive")]
		public SwitchParameter RemoteArchive
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoteArchive"] ?? false);
			}
			set
			{
				base.Fields["RemoteArchive"] = value;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001BE1F File Offset: 0x0001A01F
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x0001BE45 File Offset: 0x0001A045
		[Parameter(Mandatory = false, ParameterSetName = "Arbitration")]
		public SwitchParameter Arbitration
		{
			get
			{
				return (SwitchParameter)(base.Fields["Arbitration"] ?? false);
			}
			set
			{
				base.Fields["Arbitration"] = value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001BE5D File Offset: 0x0001A05D
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x0001BE83 File Offset: 0x0001A083
		[Parameter(Mandatory = false, ParameterSetName = "Arbitration")]
		public SwitchParameter DisableLastArbitrationMailboxAllowed
		{
			get
			{
				return (SwitchParameter)(base.Fields["DisableLastArbitrationMailboxAllowed"] ?? false);
			}
			set
			{
				base.Fields["DisableLastArbitrationMailboxAllowed"] = value;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001BE9B File Offset: 0x0001A09B
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x0001BEC1 File Offset: 0x0001A0C1
		[Parameter(Mandatory = false, ParameterSetName = "Arbitration")]
		public SwitchParameter DisableArbitrationMailboxWithOABsAllowed
		{
			get
			{
				return (SwitchParameter)(base.Fields["DisableArbitrationMailboxWithOABsAllowed"] ?? false);
			}
			set
			{
				base.Fields["DisableArbitrationMailboxWithOABsAllowed"] = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001BED9 File Offset: 0x0001A0D9
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x0001BEFF File Offset: 0x0001A0FF
		[Parameter(Mandatory = false, ParameterSetName = "PublicFolder")]
		public SwitchParameter PublicFolder
		{
			get
			{
				return (SwitchParameter)(base.Fields["PublicFolder"] ?? false);
			}
			set
			{
				base.Fields["PublicFolder"] = value;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001BF17 File Offset: 0x0001A117
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x0001BF3D File Offset: 0x0001A13D
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreLegalHold
		{
			get
			{
				return (SwitchParameter)(base.Fields["IgnoreLegalHold"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IgnoreLegalHold"] = value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001BF55 File Offset: 0x0001A155
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x0001BF7B File Offset: 0x0001A17B
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeSoftDeletedObjects
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeSoftDeletedMailbox"] ?? false);
			}
			set
			{
				base.Fields["IncludeSoftDeletedMailbox"] = value;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0001BF93 File Offset: 0x0001A193
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x0001BFB9 File Offset: 0x0001A1B9
		[Parameter(Mandatory = false)]
		public SwitchParameter PreserveEmailAddresses
		{
			get
			{
				return (SwitchParameter)(base.Fields["PreserveEmailAddresses"] ?? false);
			}
			set
			{
				base.Fields["PreserveEmailAddresses"] = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0001BFD1 File Offset: 0x0001A1D1
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x0001BFF7 File Offset: 0x0001A1F7
		[Parameter(Mandatory = false)]
		public SwitchParameter PreventRecordingPreviousDatabase
		{
			get
			{
				return (SwitchParameter)(base.Fields["PreventRecordingPreviousDatabase"] ?? false);
			}
			set
			{
				base.Fields["PreventRecordingPreviousDatabase"] = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001C010 File Offset: 0x0001A210
		private IConfigurationSession TenantLocalConfigurationSession
		{
			get
			{
				IConfigurationSession result;
				if ((result = this.tenantLocalConfigurationSession) == null)
				{
					result = (this.tenantLocalConfigurationSession = RecipientTaskHelper.GetTenantLocalConfigSession(base.CurrentOrganizationId, base.ExecutingUserOrganizationId, base.RootOrgContainerId, false, null, null));
				}
				return result;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001C054 File Offset: 0x0001A254
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("Archive" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageDisableArchive(this.Identity.ToString());
				}
				if ("RemoteArchive" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageDisableRemoteArchive(this.Identity.ToString());
				}
				return Strings.ConfirmationMessageDisableMailbox(this.Identity.ToString());
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001C0B7 File Offset: 0x0001A2B7
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			if (this.IncludeSoftDeletedObjects)
			{
				base.SessionSettings.IncludeSoftDeletedObjects = true;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001C0E4 File Offset: 0x0001A2E4
		protected override IConfigurable ResolveDataObject()
		{
			ADRecipient adrecipient = (ADRecipient)base.ResolveDataObject();
			MailboxTaskHelper.BlockRemoveOrDisableIfLitigationHoldEnabled((ADUser)adrecipient, new Task.ErrorLoggerDelegate(base.WriteError), true, this.IgnoreLegalHold.ToBool());
			MailboxTaskHelper.BlockRemoveOrDisableIfDiscoveryHoldEnabled((ADUser)adrecipient, new Task.ErrorLoggerDelegate(base.WriteError), true, this.IgnoreLegalHold.ToBool());
			MailboxTaskHelper.BlockRemoveOrDisableIfJournalNDRMailbox((ADUser)adrecipient, this.TenantLocalConfigurationSession, new Task.ErrorLoggerDelegate(base.WriteError), true);
			MailboxTaskHelper.ValidateNoOABsAssignedToArbitrationMailbox((ADUser)adrecipient, this.DisableArbitrationMailboxWithOABsAllowed.ToBool(), new Task.ErrorLoggerDelegate(base.WriteError), Strings.ErrorDisableArbitrationMailboxWithOABsAssigned(this.Identity.ToString()));
			if (MailboxTaskHelper.ExcludeArbitrationMailbox(adrecipient, this.Arbitration) || MailboxTaskHelper.ExcludePublicFolderMailbox(adrecipient, this.PublicFolder) || MailboxTaskHelper.ExcludeMailboxPlan(adrecipient, false))
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ADUser).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), ErrorCategory.InvalidData, this.Identity);
			}
			if (ComplianceConfigImpl.JournalArchivingHardeningEnabled)
			{
				MailboxTaskHelper.BlockRemoveOrDisableMailboxIfJournalArchiveEnabled(base.DataSession as IRecipientSession, this.ConfigurationSession, (ADUser)adrecipient, new Task.ErrorLoggerDelegate(base.WriteError), true);
			}
			return adrecipient;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001C244 File Offset: 0x0001A444
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADUser aduser = (ADUser)base.PrepareDataObject();
			this.exchangeGuid = aduser.ExchangeGuid;
			this.mdbId = aduser.Database;
			ProxyAddressCollection emailAddresses = aduser.EmailAddresses;
			if (!aduser.ExchangeVersion.IsOlderThan(ADUserSchema.ArchiveGuid.VersionAdded))
			{
				if (("Archive" == base.ParameterSetName || "RemoteArchive" == base.ParameterSetName) && aduser.RecipientType == RecipientType.UserMailbox && aduser.MailboxMoveStatus != RequestStatus.None && aduser.MailboxMoveStatus != RequestStatus.Completed && aduser.MailboxMoveStatus != RequestStatus.CompletedWithWarning)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorMailboxBeingMoved(this.Identity.ToString(), aduser.MailboxMoveStatus.ToString())), ErrorCategory.InvalidArgument, aduser);
				}
				if (aduser.ArchiveGuid != Guid.Empty)
				{
					if (!this.PreventRecordingPreviousDatabase)
					{
						aduser.DisabledArchiveGuid = aduser.ArchiveGuid;
						aduser.DisabledArchiveDatabase = aduser.ArchiveDatabase;
					}
					else
					{
						aduser.DisabledArchiveGuid = Guid.Empty;
						aduser.DisabledArchiveDatabase = null;
					}
				}
				aduser.ArchiveRelease = MailboxRelease.None;
				aduser.ArchiveGuid = Guid.Empty;
				aduser.ArchiveName = null;
				aduser.ArchiveDatabase = null;
				aduser.ArchiveDomain = null;
				if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.RecoverMailBox.Enabled && "Archive" == base.ParameterSetName)
				{
					aduser.ArchiveStatus = (aduser.ArchiveStatus &= ~ArchiveStatusFlags.Active);
				}
				if ((aduser.RemoteRecipientType & RemoteRecipientType.ProvisionArchive) == RemoteRecipientType.ProvisionArchive)
				{
					aduser.RemoteRecipientType = ((aduser.RemoteRecipientType &= ~RemoteRecipientType.ProvisionArchive) | RemoteRecipientType.DeprovisionArchive);
				}
			}
			if ("Archive" == base.ParameterSetName || "RemoteArchive" == base.ParameterSetName)
			{
				TaskLogger.Trace("DisableMailbox -Archive or -RemoteArchive skipping PrepareDataObject", new object[0]);
				TaskLogger.LogExit();
				return aduser;
			}
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.RecoverMailBox.Enabled)
			{
				if (!this.PreventRecordingPreviousDatabase)
				{
					aduser.PreviousDatabase = aduser.Database;
					aduser.PreviousExchangeGuid = aduser.ExchangeGuid;
				}
				else
				{
					aduser.PreviousDatabase = null;
					aduser.PreviousExchangeGuid = Guid.Empty;
				}
			}
			aduser.PreviousRecipientTypeDetails = aduser.RecipientTypeDetails;
			int recipientSoftDeletedStatus = aduser.RecipientSoftDeletedStatus;
			DateTime? whenSoftDeleted = aduser.WhenSoftDeleted;
			Guid disabledArchiveGuid = aduser.DisabledArchiveGuid;
			ADObjectId disabledArchiveDatabase = aduser.DisabledArchiveDatabase;
			MailboxTaskHelper.ClearExchangeProperties(aduser, RecipientConstants.DisableMailbox_PropertiesToReset);
			aduser.SetExchangeVersion(null);
			aduser.MailboxRelease = MailboxRelease.None;
			aduser.OverrideCorruptedValuesWithDefault();
			aduser.propertyBag.SetField(ADRecipientSchema.RecipientSoftDeletedStatus, recipientSoftDeletedStatus);
			aduser.propertyBag.SetField(ADRecipientSchema.WhenSoftDeleted, whenSoftDeleted);
			if (disabledArchiveGuid != Guid.Empty)
			{
				aduser.propertyBag.SetField(ADUserSchema.DisabledArchiveGuid, disabledArchiveGuid);
				aduser.propertyBag.SetField(ADUserSchema.DisabledArchiveDatabase, disabledArchiveDatabase);
			}
			if (this.PreserveEmailAddresses)
			{
				aduser.propertyBag.SetField(ADRecipientSchema.EmailAddresses, emailAddresses);
			}
			TaskLogger.LogExit();
			return aduser;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001C568 File Offset: 0x0001A768
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			if (this.PublicFolder)
			{
				MailboxTaskHelper.RemoveOrDisablePublicFolderMailbox(this.DataObject, this.exchangeGuid, this.TenantLocalConfigurationSession, new Task.ErrorLoggerDelegate(base.WriteError), true, false);
			}
			base.InternalProcessRecord();
			if ("Archive" == base.ParameterSetName || "RemoteArchive" == base.ParameterSetName)
			{
				TaskLogger.Trace("DisableMailbox -Archive or -RemoteArchive skipping InternalProcessRecord", new object[0]);
				TaskLogger.LogExit();
				return;
			}
			try
			{
				MailboxDatabase mailboxDatabase = null;
				if (this.mdbId != null)
				{
					mailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(new DatabaseIdParameter(this.mdbId)
					{
						AllowLegacy = true
					}, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.mdbId.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.mdbId.ToString())));
				}
				if (mailboxDatabase != null && this.exchangeGuid != Guid.Empty)
				{
					Server server = mailboxDatabase.GetServer();
					if (server == null)
					{
						this.WriteWarning(Strings.ErrorDBOwningServerNotFound(mailboxDatabase.Identity.ToString()));
					}
					else if (string.IsNullOrEmpty(server.ExchangeLegacyDN))
					{
						this.WriteWarning(Strings.ErrorInvalidObjectMissingCriticalProperty(typeof(Server).Name, server.Identity.ToString(), ServerSchema.ExchangeLegacyDN.Name));
					}
					else if (string.IsNullOrEmpty(server.Fqdn))
					{
						this.WriteWarning(Strings.ErrorInvalidObjectMissingCriticalProperty(typeof(Server).Name, server.Identity.ToString(), ServerSchema.Fqdn.Name));
					}
					else
					{
						base.WriteVerbose(Strings.VerboseConnectionAdminRpcInterface(server.Fqdn));
						using (MapiAdministrationSession mapiAdministrationSession = new MapiAdministrationSession(server.ExchangeLegacyDN, Fqdn.Parse(server.Fqdn)))
						{
							base.WriteVerbose(Strings.VerboseSyncMailboxWithDS(this.Identity.ToString(), this.mdbId.ToString(), server.Fqdn));
							mapiAdministrationSession.SyncMailboxWithDS(new MailboxId(MapiTaskHelper.ConvertDatabaseADObjectIdToDatabaseId(this.mdbId), this.exchangeGuid));
						}
					}
				}
			}
			catch (Microsoft.Exchange.Data.Mapi.Common.MailboxNotFoundException ex)
			{
				TaskLogger.Trace("Swallowing exception {0} from mapi.net", new object[]
				{
					ex
				});
				base.WriteVerbose(ex.LocalizedString);
			}
			catch (DataSourceTransientException ex2)
			{
				TaskLogger.Trace("Swallowing exception {0} from mapi.net", new object[]
				{
					ex2
				});
				this.WriteWarning(ex2.LocalizedString);
			}
			catch (DataSourceOperationException ex3)
			{
				TaskLogger.Trace("Swallowing exception {0} from mapi.net", new object[]
				{
					ex3
				});
				this.WriteWarning(ex3.LocalizedString);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001C880 File Offset: 0x0001AA80
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return Mailbox.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001C890 File Offset: 0x0001AA90
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.Identity != null && this.Arbitration.IsPresent)
			{
				MailboxTaskHelper.ValidateArbitrationMailboxHasNoGroups(this.DataObject, base.TenantGlobalCatalogSession, new Task.ErrorLoggerDelegate(base.WriteError), Strings.ErrorDisableMailboxWithAssociatedApprovalRecipents(this.Identity.ToString()));
				MailboxTaskHelper.ValidateNotLastArbitrationMailbox(this.DataObject, base.TenantGlobalCatalogSession, base.RootOrgContainerId, this.DisableLastArbitrationMailboxAllowed.IsPresent, new Task.ErrorLoggerDelegate(base.WriteError), Strings.ErrorCannotDisableLastArbitrationMailboxInOrganization(this.Identity.ToString()));
			}
			if (this.PublicFolder && (this.currentOrganizationId == null || this.currentOrganizationId != this.DataObject.OrganizationId))
			{
				this.currentOrganizationId = this.DataObject.OrganizationId;
				TenantPublicFolderConfigurationCache.Instance.RemoveValue(this.DataObject.OrganizationId);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001C98C File Offset: 0x0001AB8C
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is StorageTransientException || exception is StoragePermanentException;
		}

		// Token: 0x0400018D RID: 397
		private OrganizationId currentOrganizationId;

		// Token: 0x0400018E RID: 398
		private IConfigurationSession tenantLocalConfigurationSession;

		// Token: 0x0400018F RID: 399
		private Guid exchangeGuid;

		// Token: 0x04000190 RID: 400
		private ADObjectId mdbId;
	}
}
