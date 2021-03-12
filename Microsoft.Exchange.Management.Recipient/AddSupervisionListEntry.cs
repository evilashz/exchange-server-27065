using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200010A RID: 266
	[Cmdlet("Add", "SupervisionListEntry", SupportsShouldProcess = true)]
	public sealed class AddSupervisionListEntry : ModifySupervisionListEntryBase
	{
		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x00046EEB File Offset: 0x000450EB
		// (set) Token: 0x0600133E RID: 4926 RVA: 0x00046F02 File Offset: 0x00045102
		[Parameter(Mandatory = true)]
		[ValidateNotNull]
		public RecipientIdParameter Entry
		{
			get
			{
				return (RecipientIdParameter)base.Fields["Entry"];
			}
			set
			{
				base.Fields["Entry"] = value;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x00046F15 File Offset: 0x00045115
		// (set) Token: 0x06001340 RID: 4928 RVA: 0x00046F2C File Offset: 0x0004512C
		[Parameter(Mandatory = true)]
		[ValidateNotNull]
		public string Tag
		{
			get
			{
				return (string)base.Fields["Tag"];
			}
			set
			{
				base.Fields["Tag"] = value;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x00046F3F File Offset: 0x0004513F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationAddSupervisionListEntry(this.Entry.ToString(), this.Tag);
			}
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00046F58 File Offset: 0x00045158
		protected override void SupervisionListAction()
		{
			TaskLogger.LogEnter();
			ADObjectId configContainerId = null;
			this.CheckTagValidForRecipientOrg(out configContainerId);
			ADRecipient adrecipient = null;
			SmtpAddress? smtpAddress = null;
			base.ResolveEntry(this.Entry, out adrecipient, out smtpAddress);
			if (adrecipient != null)
			{
				bool isGroup = ADRecipient.IsAllowedDeliveryRestrictionGroup(adrecipient.RecipientType);
				this.AddADRecipientSupervisionListEntry(isGroup, adrecipient);
			}
			else if (smtpAddress != null)
			{
				this.AddExternalRecipientSupervisionListEntry(smtpAddress.Value, configContainerId);
			}
			else
			{
				base.WriteError(new ArgumentNullException("adRecipientToAdd, externalAddressToAdd"), (ErrorCategory)1000, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00046FDC File Offset: 0x000451DC
		private void CheckTagValidForRecipientOrg(out ADObjectId configContainerId)
		{
			IConfigurationSession configurationSession = this.ConfigurationSession;
			ADObjectId rootId;
			if (this.DataObject.OrganizationId == OrganizationId.ForestWideOrgId)
			{
				rootId = configurationSession.GetOrgContainerId();
			}
			else
			{
				rootId = this.DataObject.OrganizationId.ConfigurationUnit;
			}
			TransportConfigContainer[] array = configurationSession.Find<TransportConfigContainer>(rootId, QueryScope.SubTree, null, null, 2);
			if (array == null || array.Length == 0)
			{
				base.WriteError(new ManagementObjectNotFoundException(Strings.ExceptionObjectNotFound(typeof(TransportConfigContainer).ToString())), (ErrorCategory)1003, null);
			}
			if (array.Length > 1)
			{
				base.WriteError(new ManagementObjectAmbiguousException(Strings.ExceptionObjectAmbiguous(typeof(TransportConfigContainer).ToString())), (ErrorCategory)1003, null);
			}
			TransportConfigContainer transportConfigContainer = array[0];
			bool flag = false;
			foreach (string value in transportConfigContainer.SupervisionTags)
			{
				if (this.Tag.Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorSupervisionTagNotValid(this.Tag)), (ErrorCategory)1000, null);
			}
			configContainerId = transportConfigContainer.Id;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0004710C File Offset: 0x0004530C
		private void AddExternalRecipientSupervisionListEntry(SmtpAddress externalRecipientToAdd, ADObjectId configContainerId)
		{
			if (configContainerId == null)
			{
				base.WriteError(new ArgumentNullException("configContainerId"), (ErrorCategory)1000, null);
			}
			MultiValuedProperty<ADObjectIdWithString> supervisionListForExternalAddress = base.GetSupervisionListForExternalAddress();
			ADObjectIdWithString adobjectIdWithString = null;
			string[] array = null;
			PropertyValidationError propertyValidationError = base.FindExternalAddressEntry(externalRecipientToAdd, supervisionListForExternalAddress, out adobjectIdWithString, out array);
			if (propertyValidationError != null)
			{
				return;
			}
			if (adobjectIdWithString != null)
			{
				foreach (string text in array)
				{
					if (text.Equals(this.Tag, StringComparison.OrdinalIgnoreCase))
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorSupervisionEntryAlreadyPresent(this.Entry.ToString(), this.Tag.ToLower())), (ErrorCategory)1003, null);
					}
				}
				string stringValue = this.Tag.ToLower() + "," + adobjectIdWithString.StringValue;
				ADObjectIdWithString item = new ADObjectIdWithString(stringValue, adobjectIdWithString.ObjectIdValue);
				supervisionListForExternalAddress.Remove(adobjectIdWithString);
				supervisionListForExternalAddress.Add(item);
			}
			else
			{
				string stringValue2 = this.Tag.ToLower() + "," + externalRecipientToAdd.ToString();
				ADObjectIdWithString item2 = new ADObjectIdWithString(stringValue2, configContainerId);
				supervisionListForExternalAddress.Add(item2);
			}
			if (base.IsVerboseOn)
			{
				base.WriteVerbose(Strings.SupervisionListEntryAdded(this.Entry.ToString(), Strings.ExternalAddress, this.Tag));
			}
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00047250 File Offset: 0x00045450
		private void AddADRecipientSupervisionListEntry(bool isGroup, ADRecipient adRecipientToAdd)
		{
			MultiValuedProperty<ADObjectIdWithString> supervisionListForADRecipient = base.GetSupervisionListForADRecipient(isGroup);
			ADObjectIdWithString adobjectIdWithString = null;
			string[] array = null;
			PropertyValidationError propertyValidationError = base.FindADRecipientEntry(adRecipientToAdd, supervisionListForADRecipient, out adobjectIdWithString, out array);
			if (propertyValidationError != null)
			{
				return;
			}
			if (adobjectIdWithString != null)
			{
				foreach (string text in array)
				{
					if (text.Equals(this.Tag, StringComparison.OrdinalIgnoreCase))
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorSupervisionEntryAlreadyPresent(this.Entry.ToString(), this.Tag.ToLower())), (ErrorCategory)1003, null);
					}
				}
				string stringValue = this.Tag.ToLower() + "," + adobjectIdWithString.StringValue;
				ADObjectIdWithString item = new ADObjectIdWithString(stringValue, adobjectIdWithString.ObjectIdValue);
				supervisionListForADRecipient.Remove(adobjectIdWithString);
				supervisionListForADRecipient.Add(item);
			}
			else
			{
				string stringValue2 = this.Tag.ToLower();
				ADObjectIdWithString item2 = new ADObjectIdWithString(stringValue2, adRecipientToAdd.Id);
				supervisionListForADRecipient.Add(item2);
			}
			if (base.IsVerboseOn)
			{
				base.WriteVerbose(Strings.SupervisionListEntryAdded(this.Entry.ToString(), isGroup ? Strings.DistributionGroup : Strings.IndividualRecipient, this.Tag));
			}
		}
	}
}
