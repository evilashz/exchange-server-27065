using System;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000192 RID: 402
	public interface IFeatureLauncherBulkEditSupport : IBulkEditSupport
	{
		// Token: 0x1400005C RID: 92
		// (add) Token: 0x06001021 RID: 4129
		// (remove) Token: 0x06001022 RID: 4130
		event EventHandler FeatureItemUpdated;
	}
}
