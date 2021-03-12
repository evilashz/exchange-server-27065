using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A5D RID: 2653
	[Cmdlet("Add", "IPBlockListEntry", SupportsShouldProcess = true, DefaultParameterSetName = "IPRange")]
	public sealed class AddIPBlockListEntry : AddIPListEntry<IPBlockListEntry>
	{
		// Token: 0x17001CA1 RID: 7329
		// (get) Token: 0x06005F09 RID: 24329 RVA: 0x0018E982 File Offset: 0x0018CB82
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("IPAddress" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageAddIPBlockListEntryIPAddress(base.IPAddress.ToString());
				}
				return Strings.ConfirmationMessageAddIPBlockListEntryIPRange(base.IPRange.ToString());
			}
		}
	}
}
