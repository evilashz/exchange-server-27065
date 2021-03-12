using System;

namespace Microsoft.Exchange.Transport.Agent.AntiSpam.Common
{
	// Token: 0x02000005 RID: 5
	public enum AntispamUpdateMode
	{
		// Token: 0x04000010 RID: 16
		[LocDescription(Strings.IDs.UpdateModeDisabled)]
		Disabled,
		// Token: 0x04000011 RID: 17
		[LocDescription(Strings.IDs.UpdateModeManual)]
		Manual,
		// Token: 0x04000012 RID: 18
		[LocDescription(Strings.IDs.UpdateModeEnabled)]
		Automatic
	}
}
