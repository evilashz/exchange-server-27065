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
	// Token: 0x020007E0 RID: 2016
	[Cmdlet("Set", "AddressList", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetAddressList : SetAddressBookBase<AddressListIdParameter, AddressList>
	{
		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x060046A3 RID: 18083 RVA: 0x00121CDC File Offset: 0x0011FEDC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetAddressList(this.Identity.ToString());
			}
		}

		// Token: 0x060046A4 RID: 18084 RVA: 0x00121CEE File Offset: 0x0011FEEE
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return AddressList.FromDataObject((AddressBookBase)dataObject);
		}
	}
}
