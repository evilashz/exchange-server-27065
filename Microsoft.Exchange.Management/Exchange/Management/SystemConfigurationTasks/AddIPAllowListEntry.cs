using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A5C RID: 2652
	[Cmdlet("Add", "IPAllowListEntry", SupportsShouldProcess = true, DefaultParameterSetName = "IPRange")]
	public sealed class AddIPAllowListEntry : AddIPListEntry<IPAllowListEntry>
	{
		// Token: 0x17001CA0 RID: 7328
		// (get) Token: 0x06005F07 RID: 24327 RVA: 0x0018E945 File Offset: 0x0018CB45
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("IPAddress" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageAddIPAllowListEntryIPAddress(base.IPAddress.ToString());
				}
				return Strings.ConfirmationMessageAddIPAllowListEntryIPRange(base.IPRange.ToString());
			}
		}
	}
}
