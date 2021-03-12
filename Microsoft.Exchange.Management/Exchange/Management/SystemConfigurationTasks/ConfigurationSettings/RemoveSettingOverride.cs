using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks.ConfigurationSettings
{
	// Token: 0x0200094C RID: 2380
	[Cmdlet("Remove", "SettingOverride", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveSettingOverride : RemoveOverrideBase
	{
		// Token: 0x1700196D RID: 6509
		// (get) Token: 0x060054F4 RID: 21748 RVA: 0x0015E9B3 File Offset: 0x0015CBB3
		protected override bool IsFlight
		{
			get
			{
				return false;
			}
		}
	}
}
