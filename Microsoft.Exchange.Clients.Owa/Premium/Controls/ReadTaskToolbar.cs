using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000402 RID: 1026
	public class ReadTaskToolbar : Toolbar
	{
		// Token: 0x06002553 RID: 9555 RVA: 0x000D80E0 File Offset: 0x000D62E0
		public ReadTaskToolbar(bool isAssignedTask, bool userCanDelete) : base(ToolbarType.Form)
		{
			this.isAssignedTask = isAssignedTask;
			this.userCanDelete = userCanDelete;
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x000D80F8 File Offset: 0x000D62F8
		protected override void RenderButtons()
		{
			base.RenderButton(ToolbarButtons.Reply);
			base.RenderButton(ToolbarButtons.ReplyAll);
			base.RenderButton(ToolbarButtons.Forward);
			if (!this.isAssignedTask)
			{
				base.RenderButton(ToolbarButtons.Delete, this.userCanDelete ? ToolbarButtonFlags.None : ToolbarButtonFlags.Disabled);
			}
		}

		// Token: 0x040019C6 RID: 6598
		private bool isAssignedTask;

		// Token: 0x040019C7 RID: 6599
		private bool userCanDelete;
	}
}
