using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007E5 RID: 2021
	[Cmdlet("Move", "AddressList", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class MoveAddressList : SystemConfigurationObjectActionTask<AddressListIdParameter, AddressBookBase>
	{
		// Token: 0x17001561 RID: 5473
		// (get) Token: 0x060046B8 RID: 18104 RVA: 0x001227FE File Offset: 0x001209FE
		// (set) Token: 0x060046B9 RID: 18105 RVA: 0x00122815 File Offset: 0x00120A15
		[Parameter(Mandatory = true)]
		public AddressListIdParameter Target
		{
			get
			{
				return (AddressListIdParameter)base.Fields["Target"];
			}
			set
			{
				base.Fields["Target"] = value;
			}
		}

		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x060046BA RID: 18106 RVA: 0x00122828 File Offset: 0x00120A28
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageMoveAddressList(this.Identity.ToString(), this.Target.ToString());
			}
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x00122848 File Offset: 0x00120A48
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.targetContainer = (AddressBookBase)base.GetDataObject<AddressBookBase>(this.Target, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorAddressListNotFound(this.Target.ToString())), new LocalizedString?(Strings.ErrorAddressListNotUnique(this.Target.ToString())));
			TaskLogger.LogExit();
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x001228B0 File Offset: 0x00120AB0
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			this.DataObject = (AddressBookBase)base.PrepareDataObject();
			if (!this.DataObject.OrganizationId.Equals(this.targetContainer.OrganizationId))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorMoveAddressListAcrossOrganization(this.DataObject.Identity.ToString(), this.targetContainer.Identity.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			if (!base.HasErrors)
			{
				if (this.DataObject.IsTopContainer)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorInvalidOperationOnContainer(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
				else if (this.targetContainer.DistinguishedName.EndsWith(this.DataObject.DistinguishedName, StringComparison.InvariantCultureIgnoreCase))
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorMoveAddressListToChildOrSelf(this.DataObject.Identity.ToString(), this.targetContainer.Identity.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
				else
				{
					this.DataObject.DistinguishedName = this.targetContainer.Id.GetChildId(this.DataObject.Name).DistinguishedName;
				}
			}
			TaskLogger.LogExit();
			return this.DataObject;
		}

		// Token: 0x060046BD RID: 18109 RVA: 0x00122A14 File Offset: 0x00120C14
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			AddressListIdParameter addressListIdParameter = new AddressListIdParameter(this.DataObject.Id);
			AddressBookBase dataObject = (AddressBookBase)base.GetDataObject<AddressBookBase>(addressListIdParameter, base.DataSession, null, new LocalizedString?(Strings.ErrorAddressListNotFound(addressListIdParameter.ToString())), new LocalizedString?(Strings.ErrorAddressListNotUnique(addressListIdParameter.ToString())));
			base.WriteObject(new AddressList(dataObject));
			TaskLogger.LogExit();
		}

		// Token: 0x04002B07 RID: 11015
		private const string ParameterTarget = "Target";

		// Token: 0x04002B08 RID: 11016
		private AddressBookBase targetContainer;
	}
}
