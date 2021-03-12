using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks.ConfigurationSettings
{
	// Token: 0x02000944 RID: 2372
	public abstract class RemoveOverrideBase : RemoveSystemConfigurationObjectTask<SettingOverrideIdParameter, SettingOverride>
	{
		// Token: 0x17001954 RID: 6484
		// (get) Token: 0x060054BE RID: 21694
		protected abstract bool IsFlight { get; }

		// Token: 0x17001955 RID: 6485
		// (get) Token: 0x060054BF RID: 21695 RVA: 0x0015E4A8 File Offset: 0x0015C6A8
		protected override ObjectId RootId
		{
			get
			{
				return SettingOverride.GetContainerId(this.IsFlight);
			}
		}

		// Token: 0x17001956 RID: 6486
		// (get) Token: 0x060054C0 RID: 21696 RVA: 0x0015E4B5 File Offset: 0x0015C6B5
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveExchangeSettings(this.Identity.ToString());
			}
		}
	}
}
