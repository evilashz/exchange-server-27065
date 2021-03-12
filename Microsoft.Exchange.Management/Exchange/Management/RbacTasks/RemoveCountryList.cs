using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x0200065D RID: 1629
	[Cmdlet("Remove", "CountryList", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveCountryList : RemoveSystemConfigurationObjectTask<CountryListIdParameter, CountryList>
	{
		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x060038FE RID: 14590 RVA: 0x000EEB89 File Offset: 0x000ECD89
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveCountryList(this.Identity.ToString());
			}
		}
	}
}
