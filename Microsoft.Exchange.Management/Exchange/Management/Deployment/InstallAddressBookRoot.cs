using System;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001DC RID: 476
	[Cmdlet("Install", "AddressBookRoot")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class InstallAddressBookRoot : InstallAddressListBase
	{
		// Token: 0x0600106B RID: 4203 RVA: 0x00048CE3 File Offset: 0x00046EE3
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (!base.IsContainerExisted)
			{
				base.PostExchange(this.DataObject.Id);
			}
		}
	}
}
