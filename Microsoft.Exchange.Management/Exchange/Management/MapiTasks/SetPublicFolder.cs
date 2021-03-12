using System;
using System.Management.Automation;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MapiTasks
{
	// Token: 0x02000494 RID: 1172
	[Cmdlet("Set", "PublicFolder", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetPublicFolder : SetTenantADTaskBase<PublicFolderIdParameter, PublicFolder, PublicFolder>
	{
		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x060029AA RID: 10666 RVA: 0x000A5444 File Offset: 0x000A3644
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
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.PartiallyConsistent, sessionSettings, 75, "RecipientSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MapiTasks\\PublicFolder\\SetPublicFolder.cs");
					tenantOrRootOrgRecipientSession.UseGlobalCatalog = true;
					this.recipientSession = tenantOrRootOrgRecipientSession;
				}
				return this.recipientSession;
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x060029AB RID: 10667 RVA: 0x000A54B5 File Offset: 0x000A36B5
		// (set) Token: 0x060029AC RID: 10668 RVA: 0x000A54BD File Offset: 0x000A36BD
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override PublicFolderIdParameter Identity
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

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x060029AD RID: 10669 RVA: 0x000A54C6 File Offset: 0x000A36C6
		// (set) Token: 0x060029AE RID: 10670 RVA: 0x000A54CE File Offset: 0x000A36CE
		[Parameter(Mandatory = false)]
		public PublicFolderIdParameter Path { get; set; }

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x060029AF RID: 10671 RVA: 0x000A54D7 File Offset: 0x000A36D7
		// (set) Token: 0x060029B0 RID: 10672 RVA: 0x000A54DF File Offset: 0x000A36DF
		[Parameter(Mandatory = false)]
		[ValidateNotNull]
		public MailboxIdParameter OverrideContentMailbox { get; set; }

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x060029B1 RID: 10673 RVA: 0x000A54E8 File Offset: 0x000A36E8
		// (set) Token: 0x060029B2 RID: 10674 RVA: 0x000A54F0 File Offset: 0x000A36F0
		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x060029B3 RID: 10675 RVA: 0x000A54F9 File Offset: 0x000A36F9
		// (set) Token: 0x060029B4 RID: 10676 RVA: 0x000A5501 File Offset: 0x000A3701
		[Parameter(Mandatory = false)]
		public bool? MailEnabled { get; set; }

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x060029B5 RID: 10677 RVA: 0x000A550A File Offset: 0x000A370A
		// (set) Token: 0x060029B6 RID: 10678 RVA: 0x000A5512 File Offset: 0x000A3712
		[Parameter(Mandatory = false)]
		public Guid? MailRecipientGuid { get; set; }

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x060029B7 RID: 10679 RVA: 0x000A551B File Offset: 0x000A371B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetPublicFolderIdentity(this.Identity.ToString());
			}
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x000A5530 File Offset: 0x000A3730
		protected override void InternalProcessRecord()
		{
			try
			{
				if (this.Path != null)
				{
					PublicFolder publicFolder = (PublicFolder)base.GetDataObject<PublicFolder>(this.Path, base.DataSession, null, new LocalizedString?(Strings.ErrorPublicFolderNotFound(this.Path.ToString())), new LocalizedString?(Strings.ErrorPublicFolderNotUnique(this.Path.ToString())));
					using (this.publicFolderDataProvider.PublicFolderSession.GetRestrictedOperationToken())
					{
						using (CoreFolder coreFolder = CoreFolder.Bind(this.publicFolderDataProvider.PublicFolderSession, publicFolder.InternalFolderIdentity.ObjectId))
						{
							coreFolder.MoveFolder(coreFolder, this.DataObject.InternalFolderIdentity.ObjectId);
							this.folderUpdated = true;
						}
					}
				}
				if (this.OverrideContentMailbox != null && (this.Force || base.ShouldContinue(Strings.ConfirmationMessageOverrideContentMailbox)))
				{
					PublicFolder publicFolder2 = (PublicFolder)base.GetDataObject<PublicFolder>(this.Identity, base.DataSession, null, new LocalizedString?(Strings.ErrorPublicFolderNotFound(this.Identity.ToString())), new LocalizedString?(Strings.ErrorPublicFolderNotUnique(this.Identity.ToString())));
					using (this.publicFolderDataProvider.PublicFolderSession.GetRestrictedOperationToken())
					{
						using (CoreFolder coreFolder2 = CoreFolder.Bind(this.publicFolderDataProvider.PublicFolderSession, publicFolder2.InternalFolderIdentity.ObjectId))
						{
							coreFolder2.PropertyBag.SetProperty(CoreFolderSchema.ReplicaList, new string[]
							{
								this.contentMailboxGuid.ToString()
							});
							coreFolder2.PropertyBag.SetProperty(CoreFolderSchema.LastMovedTimeStamp, ExDateTime.UtcNow);
							coreFolder2.Save(SaveMode.NoConflictResolution);
							this.folderUpdated = true;
						}
					}
				}
				base.InternalProcessRecord();
			}
			catch (NotSupportedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidType, this.Identity);
			}
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x000A5798 File Offset: 0x000A3998
		protected override bool IsObjectStateChanged()
		{
			return this.folderUpdated || base.IsObjectStateChanged();
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x000A57AC File Offset: 0x000A39AC
		protected override void InternalValidate()
		{
			try
			{
				base.InternalValidate();
			}
			catch (NotSupportedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidType, this.DataObject.Identity);
			}
			if (this.DataObject.FolderPath.IsSubtreeRoot)
			{
				base.WriteError(new ModificationDisallowedException(Strings.ExceptionModifyIpmSubtree), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			if (this.DataObject.IsChanged(PublicFolderSchema.Name))
			{
				PropertyValidationError propertyValidationError = PublicFolderSchema.Name.ValidateValue(this.DataObject.Name, false);
				if (propertyValidationError != null)
				{
					base.WriteError(new DataValidationException(propertyValidationError), ErrorCategory.InvalidArgument, this.DataObject.Identity);
				}
			}
			ValidationError[] array = Database.ValidateAscendingQuotas(this.DataObject.propertyBag, new ProviderPropertyDefinition[]
			{
				PublicFolderSchema.IssueWarningQuota,
				PublicFolderSchema.ProhibitPostQuota
			}, this.DataObject.Identity);
			if (array.Length > 0)
			{
				base.WriteError(new DataValidationException(array[0]), ErrorCategory.InvalidArgument, this.DataObject.Identity);
			}
			this.ValidateAndUpdateMailPublicFolderParameters();
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x000A58B4 File Offset: 0x000A3AB4
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x000A58C7 File Offset: 0x000A3AC7
		protected override IConfigurable ResolveDataObject()
		{
			return base.GetDataObject(this.Identity);
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x000A58D5 File Offset: 0x000A3AD5
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.publicFolderDataProvider != null)
			{
				this.publicFolderDataProvider.Dispose();
				this.publicFolderDataProvider = null;
			}
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000A58FB File Offset: 0x000A3AFB
		protected override void InternalStateReset()
		{
			this.folderUpdated = false;
			base.InternalStateReset();
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x000A590C File Offset: 0x000A3B0C
		protected override IConfigDataProvider CreateSession()
		{
			OrganizationIdParameter organization = null;
			if (MapiTaskHelper.IsDatacenter)
			{
				organization = MapiTaskHelper.ResolveTargetOrganizationIdParameter(null, this.Identity, base.CurrentOrganizationId, new Task.ErrorLoggerDelegate(base.ThrowTerminatingError), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			base.CurrentOrganizationId = MapiTaskHelper.ResolveTargetOrganization(base.DomainController, organization, ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), base.CurrentOrganizationId, base.ExecutingUserOrganizationId);
			if (this.publicFolderDataProvider == null || base.CurrentOrganizationId != this.publicFolderDataProvider.CurrentOrganizationId)
			{
				if (this.OverrideContentMailbox != null)
				{
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, 337, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MapiTasks\\PublicFolder\\SetPublicFolder.cs");
					ADUser aduser = (ADUser)base.GetDataObject<ADUser>(this.OverrideContentMailbox, tenantOrRootOrgRecipientSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(this.OverrideContentMailbox.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(this.OverrideContentMailbox.ToString())));
					if (aduser == null || aduser.RecipientTypeDetails != RecipientTypeDetails.PublicFolderMailbox)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorInvalidContentMailbox(this.OverrideContentMailbox.ToString())), ErrorCategory.InvalidArgument, aduser);
					}
					if (TenantPublicFolderConfigurationCache.Instance.GetValue(base.CurrentOrganizationId).GetLocalMailboxRecipient(aduser.ExchangeGuid) == null)
					{
						TenantPublicFolderConfigurationCache.Instance.RemoveValue(base.CurrentOrganizationId);
					}
					this.contentMailboxGuid = aduser.ExchangeGuid;
				}
				if (this.publicFolderDataProvider != null)
				{
					this.publicFolderDataProvider.Dispose();
					this.publicFolderDataProvider = null;
				}
				try
				{
					this.publicFolderDataProvider = new PublicFolderDataProvider(this.ConfigurationSession, "Set-PublicFolder", Guid.Empty);
				}
				catch (AccessDeniedException exception)
				{
					base.WriteError(exception, ErrorCategory.PermissionDenied, this.Identity);
				}
			}
			return this.publicFolderDataProvider;
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x000A5AD4 File Offset: 0x000A3CD4
		private void ValidateAndUpdateMailPublicFolderParameters()
		{
			if (this.MailRecipientGuid == null && this.MailEnabled == null)
			{
				return;
			}
			Guid? mailRecipientGuid = this.MailRecipientGuid;
			Guid? guid = (mailRecipientGuid != null) ? new Guid?(mailRecipientGuid.GetValueOrDefault()) : this.DataObject.MailRecipientGuid;
			bool flag = this.MailEnabled ?? this.DataObject.MailEnabled;
			if (this.MailRecipientGuid != null && !flag)
			{
				base.WriteError(new ArgumentException(Strings.ErrorSetPublicFolderMailMailEnabledFalse), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (this.MailEnabled != null && this.MailEnabled.Value && guid == null)
			{
				base.WriteError(new ArgumentException(Strings.ErrorSetPublicFolderMailMailRecipientGuidNull), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (this.MailEnabled != null && !this.MailEnabled.Value)
			{
				this.DataObject.MailEnabled = false;
				this.DataObject.ProxyGuid = null;
				return;
			}
			if (this.MailRecipientGuid != null || (this.MailEnabled != null && this.MailEnabled.Value))
			{
				ADObjectId identity = new ADObjectId(guid.Value);
				ADPublicFolder adpublicFolder = this.RecipientSession.Read<ADPublicFolder>(identity) as ADPublicFolder;
				if (adpublicFolder == null)
				{
					base.WriteError(new ObjectNotFoundException(Strings.ErrorSetPublicFolderMailRecipientGuidNotFoundInAd(guid.Value.ToString())), ErrorCategory.InvalidData, this.Identity);
				}
				StoreObjectId storeObjectId = StoreObjectId.FromHexEntryId(adpublicFolder.EntryId);
				StoreObjectId storeObjectId2 = StoreObjectId.FromHexEntryId(this.DataObject.EntryId);
				if (!ArrayComparer<byte>.Comparer.Equals(storeObjectId.LongTermFolderId, storeObjectId2.LongTermFolderId))
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorSetPublicFolderMailRecipientGuidLongTermIdNotMatch(adpublicFolder.Id.ToString())), ErrorCategory.InvalidData, this.Identity);
				}
				if (this.DataObject.MailRecipientGuid == null || this.DataObject.MailRecipientGuid.Value != guid.Value)
				{
					this.DataObject.MailRecipientGuid = new Guid?(guid.Value);
				}
				if (!this.DataObject.MailEnabled)
				{
					this.DataObject.MailEnabled = true;
				}
			}
		}

		// Token: 0x04001E68 RID: 7784
		private PublicFolderDataProvider publicFolderDataProvider;

		// Token: 0x04001E69 RID: 7785
		private bool folderUpdated;

		// Token: 0x04001E6A RID: 7786
		private Guid contentMailboxGuid = Guid.Empty;

		// Token: 0x04001E6B RID: 7787
		private IRecipientSession recipientSession;
	}
}
