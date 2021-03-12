using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000420 RID: 1056
	public sealed class TaskViewActionToolbar : ViewActionToolbar
	{
		// Token: 0x060025CB RID: 9675 RVA: 0x000DAF54 File Offset: 0x000D9154
		protected override void RenderButtons()
		{
			if (base.ShouldUseTwistyForReplyButton)
			{
				base.RenderButton(ToolbarButtons.ReplyCombo, new Toolbar.RenderMenuItems(base.RenderReplyMenuItems));
			}
			else
			{
				base.RenderButton(ToolbarButtons.Reply);
			}
			base.RenderFloatedSpacer(3);
			base.RenderButton(ToolbarButtons.ReplyAll);
			base.RenderFloatedSpacer(3);
			base.RenderButton(ToolbarButtons.ForwardAsAttachment);
			base.RenderButton(ToolbarButtons.ForwardCombo, ToolbarButtonFlags.Hidden, new Toolbar.RenderMenuItems(base.RenderForwardMenuItems));
		}
	}
}
