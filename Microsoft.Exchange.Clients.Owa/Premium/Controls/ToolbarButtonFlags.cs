using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200042C RID: 1068
	[Flags]
	public enum ToolbarButtonFlags : uint
	{
		// Token: 0x04001A4E RID: 6734
		None = 0U,
		// Token: 0x04001A4F RID: 6735
		Text = 1U,
		// Token: 0x04001A50 RID: 6736
		Image = 2U,
		// Token: 0x04001A51 RID: 6737
		ImageAndText = 3U,
		// Token: 0x04001A52 RID: 6738
		Menu = 4U,
		// Token: 0x04001A53 RID: 6739
		ComboMenu = 8U,
		// Token: 0x04001A54 RID: 6740
		Sticky = 16U,
		// Token: 0x04001A55 RID: 6741
		AlwaysPressed = 32U,
		// Token: 0x04001A56 RID: 6742
		Pressed = 64U,
		// Token: 0x04001A57 RID: 6743
		Disabled = 128U,
		// Token: 0x04001A58 RID: 6744
		Hidden = 256U,
		// Token: 0x04001A59 RID: 6745
		ComboDropDown = 512U,
		// Token: 0x04001A5A RID: 6746
		Radio = 1024U,
		// Token: 0x04001A5B RID: 6747
		Tooltip = 2048U,
		// Token: 0x04001A5C RID: 6748
		CustomMenu = 4096U,
		// Token: 0x04001A5D RID: 6749
		NoAction = 8192U,
		// Token: 0x04001A5E RID: 6750
		ImageAfterText = 16384U,
		// Token: 0x04001A5F RID: 6751
		BigSize = 32768U,
		// Token: 0x04001A60 RID: 6752
		HasInnerControl = 65536U
	}
}
