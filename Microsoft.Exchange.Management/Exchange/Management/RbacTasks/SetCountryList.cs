using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x0200065E RID: 1630
	[Cmdlet("Set", "CountryList", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetCountryList : SetSystemConfigurationObjectTask<CountryListIdParameter, CountryList>
	{
		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x06003900 RID: 14592 RVA: 0x000EEBA3 File Offset: 0x000ECDA3
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetCountryList(this.Identity.ToString());
			}
		}
	}
}
