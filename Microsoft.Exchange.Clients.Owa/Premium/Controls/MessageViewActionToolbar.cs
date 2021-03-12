using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003B7 RID: 951
	public sealed class MessageViewActionToolbar : ViewActionToolbar
	{
		// Token: 0x060023B0 RID: 9136 RVA: 0x000CD57D File Offset: 0x000CB77D
		public MessageViewActionToolbar(bool isJunkEmailFolder)
		{
			this.isJunkEmailFolder = isJunkEmailFolder;
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x000CD58C File Offset: 0x000CB78C
		protected override void RenderButtons()
		{
			if (this.isJunkEmailFolder)
			{
				if (base.UserContext.IsJunkEmailEnabled)
				{
					base.RenderButton(ToolbarButtons.NotJunk);
				}
			}
			else
			{
				base.RenderButton(ToolbarButtons.PostReply, ToolbarButtonFlags.Hidden);
				base.RenderButton(ToolbarButtons.ReplyTextOnly);
				base.RenderButton(ToolbarButtons.ReplyAllTextOnly);
				base.RenderButton(ToolbarButtons.ForwardTextOnly);
			}
			base.RenderFloatedSpacer(1, "divMeasure");
		}

		// Token: 0x040018D0 RID: 6352
		private bool isJunkEmailFolder;
	}
}
