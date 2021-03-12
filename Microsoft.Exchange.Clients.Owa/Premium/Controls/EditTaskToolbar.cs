using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000371 RID: 881
	public class EditTaskToolbar : Toolbar
	{
		// Token: 0x0600210A RID: 8458 RVA: 0x000BE137 File Offset: 0x000BC337
		public EditTaskToolbar(bool isEmbedded, bool userCanDelete) : base(ToolbarType.Form)
		{
			this.isEmbedded = isEmbedded;
			this.userCanDelete = userCanDelete;
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x000BE150 File Offset: 0x000BC350
		protected override void RenderButtons()
		{
			base.RenderButton(ToolbarButtons.SaveAndClose);
			base.RenderButton(ToolbarButtons.MarkComplete);
			base.RenderButton(ToolbarButtons.AttachFile);
			base.RenderButton(ToolbarButtons.Recurrence);
			base.RenderButton(ToolbarButtons.Forward);
			base.RenderButton(ToolbarButtons.Delete, this.userCanDelete ? ToolbarButtonFlags.None : ToolbarButtonFlags.Disabled);
			if (!this.isEmbedded)
			{
				base.RenderButton(ToolbarButtons.Categories);
			}
		}

		// Token: 0x0400179A RID: 6042
		private bool isEmbedded;

		// Token: 0x0400179B RID: 6043
		private bool userCanDelete;
	}
}
