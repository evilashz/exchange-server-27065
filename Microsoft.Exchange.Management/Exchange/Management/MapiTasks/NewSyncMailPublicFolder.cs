using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Management.MapiTasks
{
	// Token: 0x02000491 RID: 1169
	[Cmdlet("New", "SyncMailPublicFolder", DefaultParameterSetName = "SyncMailPublicFolder", SupportsShouldProcess = true)]
	public sealed class NewSyncMailPublicFolder : NewRecipientObjectTaskBase<ADPublicFolder>
	{
		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06002978 RID: 10616 RVA: 0x000A448D File Offset: 0x000A268D
		// (set) Token: 0x06002979 RID: 10617 RVA: 0x000A44A4 File Offset: 0x000A26A4
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0, ParameterSetName = "SyncMailPublicFolder")]
		[ValidateNotNullOrEmpty]
		public string Name
		{
			get
			{
				return (string)base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x0600297A RID: 10618 RVA: 0x000A44B7 File Offset: 0x000A26B7
		// (set) Token: 0x0600297B RID: 10619 RVA: 0x000A44CE File Offset: 0x000A26CE
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0, ParameterSetName = "SyncMailPublicFolder")]
		public string Alias
		{
			get
			{
				return (string)base.Fields["Alias"];
			}
			set
			{
				base.Fields["Alias"] = value;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x0600297C RID: 10620 RVA: 0x000A44E1 File Offset: 0x000A26E1
		// (set) Token: 0x0600297D RID: 10621 RVA: 0x000A4507 File Offset: 0x000A2707
		[Parameter(Mandatory = false, ParameterSetName = "SyncMailPublicFolder")]
		public SwitchParameter HiddenFromAddressListsEnabled
		{
			get
			{
				return (SwitchParameter)(base.Fields["HiddenFromAddressListsEnabled"] ?? false);
			}
			set
			{
				base.Fields["HiddenFromAddressListsEnabled"] = value;
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x000A451F File Offset: 0x000A271F
		// (set) Token: 0x0600297F RID: 10623 RVA: 0x000A4536 File Offset: 0x000A2736
		[Parameter(Mandatory = true, ParameterSetName = "SyncMailPublicFolder")]
		[ValidateNotNullOrEmpty]
		public string EntryId
		{
			get
			{
				return (string)base.Fields["EntryId"];
			}
			set
			{
				base.Fields["EntryId"] = value;
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06002980 RID: 10624 RVA: 0x000A4549 File Offset: 0x000A2749
		// (set) Token: 0x06002981 RID: 10625 RVA: 0x000A456E File Offset: 0x000A276E
		[Parameter(Mandatory = false, ParameterSetName = "SyncMailPublicFolder")]
		public SmtpAddress WindowsEmailAddress
		{
			get
			{
				return (SmtpAddress)(base.Fields["WindowsEmailAddress"] ?? SmtpAddress.Empty);
			}
			set
			{
				base.Fields["WindowsEmailAddress"] = value;
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06002982 RID: 10626 RVA: 0x000A4586 File Offset: 0x000A2786
		// (set) Token: 0x06002983 RID: 10627 RVA: 0x000A45AB File Offset: 0x000A27AB
		[Parameter(Mandatory = false, ParameterSetName = "SyncMailPublicFolder")]
		public SmtpAddress ExternalEmailAddress
		{
			get
			{
				return (SmtpAddress)(base.Fields["ExternalEmailAddress"] ?? SmtpAddress.Empty);
			}
			set
			{
				base.Fields["ExternalEmailAddress"] = value;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06002984 RID: 10628 RVA: 0x000A45C3 File Offset: 0x000A27C3
		// (set) Token: 0x06002985 RID: 10629 RVA: 0x000A45DA File Offset: 0x000A27DA
		[Parameter(Mandatory = false, ParameterSetName = "SyncMailPublicFolder")]
		public ProxyAddress[] EmailAddresses
		{
			get
			{
				return (ProxyAddress[])base.Fields["EmailAddresses"];
			}
			set
			{
				base.Fields["EmailAddresses"] = value;
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06002986 RID: 10630 RVA: 0x000A45ED File Offset: 0x000A27ED
		// (set) Token: 0x06002987 RID: 10631 RVA: 0x000A4613 File Offset: 0x000A2813
		[Parameter(Mandatory = false)]
		public SwitchParameter OverrideRecipientQuotas
		{
			get
			{
				return (SwitchParameter)(base.Fields["OverrideRecipientQuotas"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["OverrideRecipientQuotas"] = value;
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06002988 RID: 10632 RVA: 0x000A462B File Offset: 0x000A282B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageEnableMailPublicFolder(this.Name);
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06002989 RID: 10633 RVA: 0x000A4638 File Offset: 0x000A2838
		private IRecipientSession RecipientSession
		{
			get
			{
				if (this.recipientSession == null)
				{
					ADSessionSettings sessionSettings;
					if (MapiTaskHelper.IsDatacenter)
					{
						sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
					}
					else
					{
						sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
					}
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.PartiallyConsistent, sessionSettings, 202, "RecipientSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MapiTasks\\PublicFolder\\NewSyncMailPublicFolder.cs");
					tenantOrRootOrgRecipientSession.UseGlobalCatalog = false;
					this.recipientSession = tenantOrRootOrgRecipientSession;
				}
				return this.recipientSession;
			}
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x000A46AC File Offset: 0x000A28AC
		protected override void PrepareRecipientObject(ADPublicFolder publicFolder)
		{
			this.DataObject = publicFolder;
			if (MapiTaskHelper.IsDatacenter)
			{
				publicFolder.OrganizationId = base.CurrentOrganizationId;
			}
			publicFolder.StampPersistableDefaultValues();
			if (this.Name.Contains("\n"))
			{
				this.Name = this.Name.Replace("\n", "_");
			}
			if (this.Name.Length > 64)
			{
				publicFolder.Name = this.Name.Substring(0, 64);
			}
			else
			{
				publicFolder.Name = this.Name;
			}
			publicFolder.DisplayName = publicFolder.Name;
			publicFolder.Alias = RecipientTaskHelper.GenerateUniqueAlias(this.RecipientSession, OrganizationId.ForestWideOrgId, this.Alias, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			if (this.WindowsEmailAddress != SmtpAddress.Empty)
			{
				publicFolder.WindowsEmailAddress = this.WindowsEmailAddress;
			}
			else
			{
				publicFolder.WindowsEmailAddress = publicFolder.PrimarySmtpAddress;
			}
			publicFolder.HiddenFromAddressListsEnabled = this.HiddenFromAddressListsEnabled;
			publicFolder.SendModerationNotifications = TransportModerationNotificationFlags.Never;
			publicFolder.RecipientTypeDetails = RecipientTypeDetails.PublicFolder;
			publicFolder.ObjectCategory = this.ConfigurationSession.SchemaNamingContext.GetChildId(publicFolder.ObjectCategoryCN);
			publicFolder.LegacyExchangeDN = PublicFolderSession.ConvertToLegacyDN("e71f13d1-0178-42a7-8c47-24206de84a77", this.EntryId);
			ADObjectId adobjectId;
			if (base.CurrentOrganizationId == OrganizationId.ForestWideOrgId)
			{
				adobjectId = base.CurrentOrgContainerId.DomainId.GetChildId("Microsoft Exchange System Objects");
			}
			else
			{
				adobjectId = base.CurrentOrganizationId.OrganizationalUnit;
			}
			ADObjectId childId = adobjectId.GetChildId(publicFolder.Name);
			ADRecipient adrecipient = this.RecipientSession.Read(childId);
			if (adrecipient != null)
			{
				Random random = new Random();
				childId = adobjectId.GetChildId(string.Format("{0} {1}", publicFolder.Name, random.Next(100000000).ToString("00000000")));
			}
			publicFolder.SetId(childId);
			if (base.IsProvisioningLayerAvailable)
			{
				base.WriteVerbose(Strings.VerboseInvokingRUS(publicFolder.Identity.ToString(), publicFolder.GetType().Name));
				ADPublicFolder adpublicFolder = new ADPublicFolder();
				adpublicFolder.CopyChangesFrom(publicFolder);
				ProvisioningLayer.UpdateAffectedIConfigurable(this, RecipientTaskHelper.ConvertRecipientToPresentationObject(adpublicFolder), false);
				publicFolder.CopyChangesFrom(adpublicFolder);
			}
			else
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorNoProvisioningHandlerAvailable), ErrorCategory.InvalidOperation, null);
			}
			if (this.ExternalEmailAddress != SmtpAddress.Empty)
			{
				publicFolder.ExternalEmailAddress = ProxyAddress.Parse(this.ExternalEmailAddress.ToString());
			}
			else
			{
				publicFolder.ExternalEmailAddress = ProxyAddress.Parse(publicFolder.WindowsEmailAddress.ToString());
			}
			MailUserTaskHelper.ValidateExternalEmailAddress(publicFolder, this.ConfigurationSession, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache);
			if (this.EmailAddresses != null)
			{
				foreach (ProxyAddress proxyAddress in this.EmailAddresses)
				{
					if (proxyAddress != null && !publicFolder.EmailAddresses.Contains(proxyAddress))
					{
						publicFolder.EmailAddresses.Add(proxyAddress);
					}
				}
			}
			adrecipient = this.RecipientSession.FindByProxyAddress(ProxyAddress.Parse("X500:" + publicFolder.LegacyExchangeDN));
			if (adrecipient != null)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorObjectAlreadyExists("ADPublicFolder object : ", this.Name)), ErrorCategory.InvalidData, null);
			}
			publicFolder.EmailAddresses.Add(ProxyAddress.Parse("X500:" + publicFolder.LegacyExchangeDN));
			RecipientTaskHelper.ValidateEmailAddressErrorOut(this.RecipientSession, publicFolder.EmailAddresses, publicFolder, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerReThrowDelegate(this.WriteError));
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x000A4A38 File Offset: 0x000A2C38
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x000A4A4B File Offset: 0x000A2C4B
		protected override void ProvisioningUpdateConfigurationObject()
		{
		}

		// Token: 0x04001E5B RID: 7771
		private const string TaskNoun = "SyncMailPublicFolder";

		// Token: 0x04001E5C RID: 7772
		private const string ParameterSetSyncMailPublicFolder = "SyncMailPublicFolder";

		// Token: 0x04001E5D RID: 7773
		private const string ParameterName = "Name";

		// Token: 0x04001E5E RID: 7774
		private const string ParameterAlias = "Alias";

		// Token: 0x04001E5F RID: 7775
		private const string ParameterHiddenFromAddressListsEnabled = "HiddenFromAddressListsEnabled";

		// Token: 0x04001E60 RID: 7776
		private const string ParameterEntryId = "EntryId";

		// Token: 0x04001E61 RID: 7777
		private const string ParameterWindowsEmailAddress = "WindowsEmailAddress";

		// Token: 0x04001E62 RID: 7778
		private const string ParameterExternalEmailAddress = "ExternalEmailAddress";

		// Token: 0x04001E63 RID: 7779
		private const string ParameterEmailAddresses = "EmailAddresses";

		// Token: 0x04001E64 RID: 7780
		private IRecipientSession recipientSession;
	}
}
