using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks.ConfigurationSettings
{
	// Token: 0x0200094D RID: 2381
	[Cmdlet("Set", "SettingOverride", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class SetSettingOverride : SetOverrideBase
	{
		// Token: 0x1700196E RID: 6510
		// (get) Token: 0x060054F6 RID: 21750 RVA: 0x0015E9BE File Offset: 0x0015CBBE
		protected override bool IsFlight
		{
			get
			{
				return false;
			}
		}
	}
}
