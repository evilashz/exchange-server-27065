using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks.ConfigurationSettings
{
	// Token: 0x0200093F RID: 2367
	[Cmdlet("Remove", "ExchangeSettings", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveExchangeSettings : RemoveSystemConfigurationObjectTask<ExchangeSettingsIdParameter, ExchangeSettings>
	{
		// Token: 0x17001923 RID: 6435
		// (get) Token: 0x06005447 RID: 21575 RVA: 0x0015C307 File Offset: 0x0015A507
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveExchangeSettings(this.Identity.ToString());
			}
		}
	}
}
