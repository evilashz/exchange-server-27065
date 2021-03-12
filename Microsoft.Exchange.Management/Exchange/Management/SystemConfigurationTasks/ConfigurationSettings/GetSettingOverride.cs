using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks.ConfigurationSettings
{
	// Token: 0x0200094A RID: 2378
	[Cmdlet("Get", "SettingOverride", DefaultParameterSetName = "Identity")]
	public sealed class GetSettingOverride : GetOverrideBase
	{
		// Token: 0x17001969 RID: 6505
		// (get) Token: 0x060054EA RID: 21738 RVA: 0x0015E8FE File Offset: 0x0015CAFE
		protected override bool IsFlight
		{
			get
			{
				return false;
			}
		}
	}
}
