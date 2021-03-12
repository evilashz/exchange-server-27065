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
	// Token: 0x020007E3 RID: 2019
	[Cmdlet("update", "AddressList", SupportsShouldProcess = true)]
	public sealed class UpdateAddressList : UpdateAddressBookBase<AddressListIdParameter>
	{
		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x060046B2 RID: 18098 RVA: 0x001227B0 File Offset: 0x001209B0
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageUpdateAddressList(this.Identity.ToString());
			}
		}

		// Token: 0x060046B3 RID: 18099 RVA: 0x001227C2 File Offset: 0x001209C2
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return AddressList.FromDataObject((AddressBookBase)dataObject);
		}
	}
}
