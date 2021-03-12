using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000ADC RID: 2780
	[Cmdlet("New", "OfflineAddressBook", SupportsShouldProcess = true)]
	public sealed class NewOfflineAddressBook : NewOfflineAddressBookInternal
	{
		// Token: 0x17001DFD RID: 7677
		// (get) Token: 0x060062D7 RID: 25303 RVA: 0x0019D0B7 File Offset: 0x0019B2B7
		// (set) Token: 0x060062D8 RID: 25304 RVA: 0x0019D0BF File Offset: 0x0019B2BF
		[Parameter(Mandatory = true)]
		public override AddressBookBaseIdParameter[] AddressLists
		{
			get
			{
				return base.AddressLists;
			}
			set
			{
				base.AddressLists = value;
			}
		}
	}
}
