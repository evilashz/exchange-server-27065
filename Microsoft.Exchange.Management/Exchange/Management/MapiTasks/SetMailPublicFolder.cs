using System;
using System.Management.Automation;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MapiTasks
{
	// Token: 0x02000493 RID: 1171
	[Cmdlet("Set", "MailPublicFolder", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetMailPublicFolder : SetMailEnabledRecipientObjectTask<MailPublicFolderIdParameter, MailPublicFolder, ADPublicFolder>
	{
		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06002998 RID: 10648 RVA: 0x000A4E0D File Offset: 0x000A300D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetMailPublicFolder(this.Identity.ToString());
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x0600299A RID: 10650 RVA: 0x000A4E27 File Offset: 0x000A3027
		// (set) Token: 0x0600299B RID: 10651 RVA: 0x000A4E3E File Offset: 0x000A303E
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] Contacts
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[MailPublicFolderSchema.Contacts];
			}
			set
			{
				base.Fields[MailPublicFolderSchema.Contacts] = value;
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x0600299C RID: 10652 RVA: 0x000A4E51 File Offset: 0x000A3051
		// (set) Token: 0x0600299D RID: 10653 RVA: 0x000A4E68 File Offset: 0x000A3068
		[Parameter(Mandatory = false)]
		public RecipientIdParameter ForwardingAddress
		{
			get
			{
				return (RecipientIdParameter)base.Fields[MailPublicFolderSchema.ForwardingAddress];
			}
			set
			{
				base.Fields[MailPublicFolderSchema.ForwardingAddress] = value;
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x0600299E RID: 10654 RVA: 0x000A4E7B File Offset: 0x000A307B
		// (set) Token: 0x0600299F RID: 10655 RVA: 0x000A4E92 File Offset: 0x000A3092
		[Parameter(Mandatory = false)]
		public ProxyAddress ExternalEmailAddress
		{
			get
			{
				return (ProxyAddress)base.Fields[MailPublicFolderSchema.ExternalEmailAddress];
			}
			set
			{
				base.Fields[MailPublicFolderSchema.ExternalEmailAddress] = value;
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x060029A0 RID: 10656 RVA: 0x000A4EA5 File Offset: 0x000A30A5
		// (set) Token: 0x060029A1 RID: 10657 RVA: 0x000A4EBC File Offset: 0x000A30BC
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string EntryId
		{
			get
			{
				return (string)base.Fields[MailPublicFolderSchema.EntryId];
			}
			set
			{
				base.Fields[MailPublicFolderSchema.EntryId] = value;
			}
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x000A4ED0 File Offset: 0x000A30D0
		private ADObjectId GetRecipientIdentityAndValidateTypeForContacts(RecipientIdParameter recipientIdParameter, Task.ErrorLoggerDelegate errorHandler)
		{
			ADRecipient adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(recipientIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(recipientIdParameter.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(recipientIdParameter.ToString())), ExchangeErrorCategory.Client);
			if (RecipientType.UserMailbox != adrecipient.RecipientType && RecipientType.MailUser != adrecipient.RecipientType && RecipientType.MailContact != adrecipient.RecipientType)
			{
				errorHandler(new RecipientTaskException(Strings.ErrorIndividualRecipientNeeded(recipientIdParameter.ToString())), ExchangeErrorCategory.Client, recipientIdParameter);
				return null;
			}
			return (ADObjectId)adrecipient.Identity;
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x000A4F5C File Offset: 0x000A315C
		private ADObjectId GetRecipientIdentityAndValidateTypeForFwd(RecipientIdParameter recipientIdParameter, Task.ErrorLoggerDelegate errorHandler)
		{
			ADRecipient adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(recipientIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(recipientIdParameter.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(recipientIdParameter.ToString())), ExchangeErrorCategory.Client);
			if (adrecipient.RecipientType == RecipientType.Invalid || RecipientType.User == adrecipient.RecipientType || RecipientType.Contact == adrecipient.RecipientType || RecipientType.Group == adrecipient.RecipientType || RecipientType.PublicDatabase == adrecipient.RecipientType || RecipientType.SystemAttendantMailbox == adrecipient.RecipientType || RecipientType.SystemMailbox == adrecipient.RecipientType || RecipientType.MicrosoftExchange == adrecipient.RecipientType)
			{
				errorHandler(new RecipientTaskException(Strings.ErrorInvalidRecipientType(recipientIdParameter.ToString(), adrecipient.RecipientType.ToString())), ExchangeErrorCategory.Client, recipientIdParameter);
				return null;
			}
			return (ADObjectId)adrecipient.Identity;
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x000A5028 File Offset: 0x000A3228
		private OrganizationId ResolveCurrentOrganization()
		{
			if (MapiTaskHelper.IsDatacenter)
			{
				OrganizationIdParameter organization = MapiTaskHelper.ResolveTargetOrganizationIdParameter(null, this.Identity, base.CurrentOrganizationId, new Task.ErrorLoggerDelegate(base.ThrowTerminatingError), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
				return MapiTaskHelper.ResolveTargetOrganization(base.DomainController, organization, ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), base.CurrentOrganizationId, base.ExecutingUserOrganizationId);
			}
			return base.CurrentOrganizationId ?? base.ExecutingUserOrganizationId;
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x000A5098 File Offset: 0x000A3298
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.CurrentOrganizationId = this.ResolveCurrentOrganization();
			base.InternalBeginProcessing();
			MailPublicFolder mailPublicFolder = (MailPublicFolder)this.GetDynamicParameters();
			if (base.Fields.IsModified(MailPublicFolderSchema.Contacts))
			{
				mailPublicFolder.Contacts.Clear();
				if (this.Contacts != null)
				{
					foreach (RecipientIdParameter recipientIdParameter in this.Contacts)
					{
						ADObjectId recipientIdentityAndValidateTypeForContacts = this.GetRecipientIdentityAndValidateTypeForContacts(recipientIdParameter, new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
						mailPublicFolder.Contacts.Add(recipientIdentityAndValidateTypeForContacts);
					}
				}
			}
			if (base.Fields.IsModified(MailPublicFolderSchema.ForwardingAddress))
			{
				mailPublicFolder.ForwardingAddress = null;
				if (this.ForwardingAddress != null)
				{
					ADObjectId recipientIdentityAndValidateTypeForFwd = this.GetRecipientIdentityAndValidateTypeForFwd(this.ForwardingAddress, new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
					mailPublicFolder.ForwardingAddress = recipientIdentityAndValidateTypeForFwd;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x000A5174 File Offset: 0x000A3374
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.DataObject.IsModified(MailPublicFolderSchema.Contacts))
			{
				foreach (ADObjectId adObjectId in this.DataObject.Contacts.Added)
				{
					this.GetRecipientIdentityAndValidateTypeForContacts(new RecipientIdParameter(adObjectId), new Task.ErrorLoggerDelegate(base.WriteError));
				}
			}
			if (this.DataObject.IsModified(MailPublicFolderSchema.ForwardingAddress) && this.DataObject.ForwardingAddress != null)
			{
				this.GetRecipientIdentityAndValidateTypeForFwd(new RecipientIdParameter(this.DataObject.ForwardingAddress), new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(MailPublicFolderSchema.ExternalEmailAddress))
			{
				this.DataObject.ExternalEmailAddress = this.ExternalEmailAddress;
				MailUserTaskHelper.ValidateExternalEmailAddress(this.DataObject, this.ConfigurationSession, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache);
			}
			ADObjectId adobjectId = null;
			if (base.Fields.IsModified(MailPublicFolderSchema.EntryId) && this.IsValidToUpdateEntryId(this.EntryId, out adobjectId))
			{
				this.DataObject.EntryId = this.EntryId;
				if (adobjectId != null)
				{
					this.DataObject.ContentMailbox = adobjectId;
				}
			}
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x000A52A6 File Offset: 0x000A34A6
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x000A52B9 File Offset: 0x000A34B9
		protected override bool ShouldSupportPreResolveOrgIdBasedOnIdentity()
		{
			return false;
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x000A52BC File Offset: 0x000A34BC
		private bool IsValidToUpdateEntryId(string entryId, out ADObjectId contentMailbox)
		{
			contentMailbox = null;
			if (string.IsNullOrEmpty(entryId))
			{
				return false;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(entryId, this.DataObject.EntryId))
			{
				return false;
			}
			StoreObjectId storeObjectId = null;
			try
			{
				storeObjectId = StoreObjectId.FromHexEntryId(entryId);
			}
			catch (FormatException innerException)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorSetMailPublicFolderEntryIdWrongFormat(entryId), innerException), ExchangeErrorCategory.Client, this.DataObject.Identity);
			}
			using (PublicFolderDataProvider publicFolderDataProvider = new PublicFolderDataProvider(this.ConfigurationSession, "Set-MailPublicFolder", Guid.Empty))
			{
				Guid exchangeGuid = Guid.Empty;
				try
				{
					PublicFolder publicFolder = (PublicFolder)publicFolderDataProvider.Read<PublicFolder>(new PublicFolderId(storeObjectId));
					exchangeGuid = publicFolder.ContentMailboxGuid;
				}
				catch (ObjectNotFoundException innerException2)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorSetMailPublicFolderEntryIdNotExist(entryId), innerException2), ExchangeErrorCategory.Client, this.DataObject.Identity);
				}
				ADRecipient adrecipient = base.TenantGlobalCatalogSession.FindByExchangeGuid(exchangeGuid);
				if (adrecipient != null)
				{
					contentMailbox = adrecipient.Id;
				}
				if (string.IsNullOrEmpty(this.DataObject.EntryId))
				{
					return true;
				}
				StoreObjectId storeObjectId2 = StoreObjectId.FromHexEntryId(this.DataObject.EntryId);
				try
				{
					PublicFolder publicFolder2 = (PublicFolder)publicFolderDataProvider.Read<PublicFolder>(new PublicFolderId(storeObjectId2));
				}
				catch (ObjectNotFoundException)
				{
					return true;
				}
				if (ArrayComparer<byte>.Comparer.Equals(storeObjectId2.LongTermFolderId, storeObjectId.LongTermFolderId))
				{
					return true;
				}
			}
			return false;
		}
	}
}
