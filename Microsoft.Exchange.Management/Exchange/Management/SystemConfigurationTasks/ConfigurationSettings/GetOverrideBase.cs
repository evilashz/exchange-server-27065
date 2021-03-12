using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks.ConfigurationSettings
{
	// Token: 0x02000942 RID: 2370
	public abstract class GetOverrideBase : GetSystemConfigurationObjectTask<SettingOverrideIdParameter, SettingOverride>
	{
		// Token: 0x17001948 RID: 6472
		// (get) Token: 0x060054A3 RID: 21667
		protected abstract bool IsFlight { get; }

		// Token: 0x17001949 RID: 6473
		// (get) Token: 0x060054A4 RID: 21668 RVA: 0x0015E105 File Offset: 0x0015C305
		protected override ObjectId RootId
		{
			get
			{
				return SettingOverride.GetContainerId(this.IsFlight);
			}
		}

		// Token: 0x060054A5 RID: 21669 RVA: 0x0015E112 File Offset: 0x0015C312
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(ConfigurationSettingsException).IsInstanceOfType(exception);
		}
	}
}
