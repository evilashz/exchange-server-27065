using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000080 RID: 128
	public abstract class SetMailContactBase<TPublicObject> : SetMailEnabledOrgPersonObjectTask<MailContactIdParameter, TPublicObject, ADContact> where TPublicObject : MailContact, new()
	{
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00026DC2 File Offset: 0x00024FC2
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetMailContact(this.Identity.ToString());
			}
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00026DD4 File Offset: 0x00024FD4
		public SetMailContactBase()
		{
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00026DDC File Offset: 0x00024FDC
		protected override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00026DDF File Offset: 0x00024FDF
		protected override bool ShouldUpgradeExchangeVersion(ADObject adObject)
		{
			return base.ShouldUpgradeExchangeVersion(adObject) || adObject.propertyBag.Changed;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00026DF8 File Offset: 0x00024FF8
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADContact adcontact = (ADContact)base.PrepareDataObject();
			if (adcontact.RecipientDisplayType == null)
			{
				adcontact.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.RemoteMailUser);
			}
			if (adcontact.IsChanged(ADRecipientSchema.ExternalEmailAddress))
			{
				MailContactTaskHelper.ValidateExternalEmailAddress(adcontact, this.ConfigurationSession, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache);
			}
			TaskLogger.LogExit();
			return adcontact;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00026E63 File Offset: 0x00025063
		protected override void InternalValidate()
		{
			base.InternalValidate();
			DistributionGroupTaskHelper.CheckModerationInMixedEnvironment(this.DataObject, new Task.TaskWarningLoggingDelegate(this.WriteWarning), Strings.WarningLegacyExchangeServerForMailContact);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00026E88 File Offset: 0x00025088
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return MailContact.FromDataObject((ADContact)dataObject);
		}
	}
}
