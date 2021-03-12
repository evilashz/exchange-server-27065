using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007E1 RID: 2017
	[Cmdlet("Set", "GlobalAddressList", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetGlobalAddressList : SetAddressBookBase<GlobalAddressListIdParameter, GlobalAddressList>
	{
		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x060046A6 RID: 18086 RVA: 0x00121D03 File Offset: 0x0011FF03
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetGlobalAddressList(this.Identity.ToString());
			}
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x00121D18 File Offset: 0x0011FF18
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			this.isDefaultGAL = (bool)((ADObject)dataObject)[AddressBookBaseSchema.IsDefaultGlobalAddressList];
			base.StampChangesOn(dataObject);
			AddressBookBase addressBookBase = (AddressBookBase)dataObject;
			if (addressBookBase.IsModified(ADObjectSchema.Name))
			{
				addressBookBase.DisplayName = addressBookBase.Name;
			}
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x00121D68 File Offset: 0x0011FF68
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors && this.isDefaultGAL)
			{
				if (this.DataObject.IsModified(ADObjectSchema.Name) || this.DataObject.IsModified(AddressBookBaseSchema.DisplayName) || this.DataObject.IsModified(AddressBookBaseSchema.RecipientContainer))
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorInvalidOperationOnDefaultGAL(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
				base.ValidateBrokenRecipientFilterChange(GlobalAddressList.RecipientFilterForDefaultGal);
				if (this.isDefaultGAL && this.DataObject.ExchangeVersion.IsSameVersion(ExchangeObjectVersion.Exchange2007))
				{
					this.DataObject[AddressBookBaseSchema.RecipientFilterFlags] = ((RecipientFilterableObjectFlags)this.DataObject[AddressBookBaseSchema.RecipientFilterFlags] | RecipientFilterableObjectFlags.IsDefault);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x00121E53 File Offset: 0x00120053
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return GlobalAddressList.FromDataObject((AddressBookBase)dataObject);
		}

		// Token: 0x04002B06 RID: 11014
		private bool isDefaultGAL;
	}
}
