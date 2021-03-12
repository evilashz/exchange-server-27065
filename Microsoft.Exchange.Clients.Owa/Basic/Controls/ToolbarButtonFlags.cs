using System;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200008A RID: 138
	[Flags]
	public enum ToolbarButtonFlags : uint
	{
		// Token: 0x04000300 RID: 768
		Text = 1U,
		// Token: 0x04000301 RID: 769
		Image = 2U,
		// Token: 0x04000302 RID: 770
		ImageAndText = 3U,
		// Token: 0x04000303 RID: 771
		None = 4U,
		// Token: 0x04000304 RID: 772
		NoHover = 8U,
		// Token: 0x04000305 RID: 773
		ImageAndNoHover = 10U,
		// Token: 0x04000306 RID: 774
		TextAndNoHover = 9U,
		// Token: 0x04000307 RID: 775
		Sticky = 16U,
		// Token: 0x04000308 RID: 776
		Selected = 32U,
		// Token: 0x04000309 RID: 777
		Tab = 67U,
		// Token: 0x0400030A RID: 778
		NoAction = 4096U
	}
}
