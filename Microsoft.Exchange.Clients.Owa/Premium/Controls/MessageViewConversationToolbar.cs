using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003BA RID: 954
	public sealed class MessageViewConversationToolbar : Toolbar
	{
		// Token: 0x060023BA RID: 9146 RVA: 0x000CDE20 File Offset: 0x000CC020
		public MessageViewConversationToolbar(bool isNewestOnTop) : base("divCnvTB")
		{
			this.isNewestOnTop = isNewestOnTop;
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x000CDE34 File Offset: 0x000CC034
		protected override void RenderButtons()
		{
			base.RenderButton(ToolbarButtons.NewestOnTop, this.isNewestOnTop ? ToolbarButtonFlags.None : ToolbarButtonFlags.Hidden);
			base.RenderButton(ToolbarButtons.OldestOnTop, (!this.isNewestOnTop) ? ToolbarButtonFlags.None : ToolbarButtonFlags.Hidden);
			base.RenderButton(ToolbarButtons.ExpandAll);
			base.RenderButton(ToolbarButtons.CollapseAll, ToolbarButtonFlags.Hidden);
			base.RenderFloatedSpacer(1, "divMeasure");
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x060023BC RID: 9148 RVA: 0x000CDE9E File Offset: 0x000CC09E
		public override bool IsRightAligned
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040018D6 RID: 6358
		private bool isNewestOnTop;
	}
}
