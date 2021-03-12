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
	// Token: 0x020007E4 RID: 2020
	[Cmdlet("update", "GlobalAddressList", SupportsShouldProcess = true)]
	public sealed class UpdateGlobalAddressList : UpdateAddressBookBase<GlobalAddressListIdParameter>
	{
		// Token: 0x17001560 RID: 5472
		// (get) Token: 0x060046B5 RID: 18101 RVA: 0x001227D7 File Offset: 0x001209D7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageUpdateGlobalAddressList(this.Identity.ToString());
			}
		}

		// Token: 0x060046B6 RID: 18102 RVA: 0x001227E9 File Offset: 0x001209E9
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return GlobalAddressList.FromDataObject((AddressBookBase)dataObject);
		}
	}
}
