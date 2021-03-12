using System;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001E9 RID: 489
	public enum FeatureStatus
	{
		// Token: 0x040007F2 RID: 2034
		[LocDescription(Strings.IDs.FeatureStatusNone)]
		None,
		// Token: 0x040007F3 RID: 2035
		[LocDescription(Strings.IDs.FeatureStatusEnabled)]
		Enabled,
		// Token: 0x040007F4 RID: 2036
		[LocDescription(Strings.IDs.FeatureStatusDisabled)]
		Disabled,
		// Token: 0x040007F5 RID: 2037
		[LocDescription(Strings.IDs.FeatureStatusUnknown)]
		Unknown
	}
}
