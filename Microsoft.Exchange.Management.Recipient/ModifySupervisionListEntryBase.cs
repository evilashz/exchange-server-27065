using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000109 RID: 265
	public abstract class ModifySupervisionListEntryBase : RecipientObjectActionTask<RecipientIdParameter, ADRecipient>
	{
		// Token: 0x06001334 RID: 4916 RVA: 0x00046BB4 File Offset: 0x00044DB4
		protected PropertyValidationError FindADRecipientEntry(ADRecipient adRecipient, MultiValuedProperty<ADObjectIdWithString> supervisionList, out ADObjectIdWithString foundEntry, out string[] tags)
		{
			if (adRecipient == null)
			{
				base.WriteError(new ArgumentNullException("adRecipient"), (ErrorCategory)1000, null);
			}
			if (supervisionList == null)
			{
				base.WriteError(new ArgumentNullException("supervisionList"), (ErrorCategory)1000, null);
			}
			foundEntry = null;
			tags = null;
			foreach (ADObjectIdWithString adobjectIdWithString in supervisionList)
			{
				if (adobjectIdWithString.ObjectIdValue.Equals(adRecipient.Id))
				{
					foundEntry = adobjectIdWithString;
					break;
				}
			}
			PropertyValidationError propertyValidationError = null;
			if (foundEntry != null)
			{
				SupervisionListEntryConstraint supervisionListEntryConstraint = new SupervisionListEntryConstraint(false);
				propertyValidationError = supervisionListEntryConstraint.Validate(foundEntry, null, null);
				if (propertyValidationError != null)
				{
					return propertyValidationError;
				}
				tags = foundEntry.StringValue.Split(new char[]
				{
					SupervisionListEntryConstraint.Delimiter
				});
			}
			return propertyValidationError;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x00046C8C File Offset: 0x00044E8C
		protected PropertyValidationError FindExternalAddressEntry(SmtpAddress externalAddress, MultiValuedProperty<ADObjectIdWithString> supervisionList, out ADObjectIdWithString foundEntry, out string[] tags)
		{
			if (supervisionList == null)
			{
				base.WriteError(new ArgumentNullException("supervisionList"), (ErrorCategory)1000, null);
			}
			foundEntry = null;
			tags = null;
			SupervisionListEntryConstraint supervisionListEntryConstraint = new SupervisionListEntryConstraint(true);
			PropertyValidationError propertyValidationError = null;
			string[] array = null;
			foreach (ADObjectIdWithString adobjectIdWithString in supervisionList)
			{
				propertyValidationError = supervisionListEntryConstraint.Validate(adobjectIdWithString, null, null);
				if (propertyValidationError != null)
				{
					return propertyValidationError;
				}
				array = adobjectIdWithString.StringValue.Split(new char[]
				{
					SupervisionListEntryConstraint.Delimiter
				});
				SmtpAddress smtpAddress = new SmtpAddress(array[array.Length - 1]);
				if (smtpAddress.Equals(externalAddress))
				{
					foundEntry = adobjectIdWithString;
					break;
				}
			}
			if (foundEntry != null)
			{
				Array.Resize<string>(ref array, array.Length - 1);
				tags = array;
			}
			return propertyValidationError;
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00046D68 File Offset: 0x00044F68
		protected override IConfigurable PrepareDataObject()
		{
			return (ADRecipient)base.PrepareDataObject();
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00046D82 File Offset: 0x00044F82
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			this.SupervisionListAction();
			TaskLogger.LogExit();
		}

		// Token: 0x06001338 RID: 4920
		protected abstract void SupervisionListAction();

		// Token: 0x06001339 RID: 4921 RVA: 0x00046DA4 File Offset: 0x00044FA4
		protected void ResolveEntry(RecipientIdParameter entry, out ADRecipient adRecipient, out SmtpAddress? externalAddress)
		{
			if (entry == null)
			{
				base.WriteError(new ArgumentNullException("entry"), (ErrorCategory)1000, null);
			}
			adRecipient = null;
			externalAddress = null;
			try
			{
				adRecipient = (ADRecipient)base.GetDataObject<ADRecipient>(entry, base.TenantGlobalCatalogSession, this.DataObject.OrganizationId.OrganizationalUnit, new LocalizedString?(Strings.ErrorRecipientNotFound((string)entry)), new LocalizedString?(Strings.ErrorRecipientNotUnique((string)entry)));
			}
			catch (ManagementObjectNotFoundException)
			{
				try
				{
					externalAddress = new SmtpAddress?(SmtpAddress.Parse(entry.ToString()));
				}
				catch (FormatException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidData, null);
				}
				adRecipient = null;
				return;
			}
			OrganizationId organizationId = this.DataObject.OrganizationId;
			OrganizationId organizationId2 = adRecipient.OrganizationId;
			if (!organizationId.Equals(organizationId2))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorEntryNotInRecipientOrg(entry.ToString())), (ErrorCategory)1003, null);
			}
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00046E9C File Offset: 0x0004509C
		protected MultiValuedProperty<ADObjectIdWithString> GetSupervisionListForADRecipient(bool isGroup)
		{
			if (isGroup)
			{
				return (MultiValuedProperty<ADObjectIdWithString>)this.DataObject[ADRecipientSchema.DLSupervisionList];
			}
			return (MultiValuedProperty<ADObjectIdWithString>)this.DataObject[ADRecipientSchema.InternalRecipientSupervisionList];
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00046ECC File Offset: 0x000450CC
		protected MultiValuedProperty<ADObjectIdWithString> GetSupervisionListForExternalAddress()
		{
			return (MultiValuedProperty<ADObjectIdWithString>)this.DataObject[ADRecipientSchema.OneOffSupervisionList];
		}
	}
}
