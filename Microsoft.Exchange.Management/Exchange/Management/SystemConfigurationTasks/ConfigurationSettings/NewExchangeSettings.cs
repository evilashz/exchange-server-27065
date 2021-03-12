using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks.ConfigurationSettings
{
	// Token: 0x0200093E RID: 2366
	[Cmdlet("New", "ExchangeSettings", SupportsShouldProcess = true)]
	public sealed class NewExchangeSettings : NewSystemConfigurationObjectTask<ExchangeSettings>
	{
		// Token: 0x17001921 RID: 6433
		// (get) Token: 0x06005442 RID: 21570 RVA: 0x0015C239 File Offset: 0x0015A439
		// (set) Token: 0x06005443 RID: 21571 RVA: 0x0015C25F File Offset: 0x0015A45F
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x17001922 RID: 6434
		// (get) Token: 0x06005444 RID: 21572 RVA: 0x0015C277 File Offset: 0x0015A477
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewExchangeSettings(base.Name.ToString());
			}
		}

		// Token: 0x06005445 RID: 21573 RVA: 0x0015C28C File Offset: 0x0015A48C
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ExchangeSettings exchangeSettings = (ExchangeSettings)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			if (!this.Force && !SetExchangeSettings.IsSchemaRegistered(base.Name))
			{
				base.WriteError(new ExchangeSettingsInvalidSchemaException(base.Name), ErrorCategory.InvalidOperation, null);
			}
			exchangeSettings.SetId(this.ConfigurationSession, base.Name);
			exchangeSettings.InitializeSettings();
			TaskLogger.LogExit();
			return exchangeSettings;
		}
	}
}
