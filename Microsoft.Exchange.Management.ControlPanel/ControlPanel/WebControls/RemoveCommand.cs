using System;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000646 RID: 1606
	public class RemoveCommand : Command
	{
		// Token: 0x0600463D RID: 17981 RVA: 0x000D4560 File Offset: 0x000D2760
		public RemoveCommand(bool useDeleteImage = true)
		{
			this.Name = "Delete";
			base.ShortCut = "Delete";
			this.SelectionMode = SelectionMode.SupportsMultipleSelection;
			if (useDeleteImage)
			{
				base.ImageId = CommandSprite.SpriteId.ToolBarDelete;
				base.ImageAltText = Strings.DeleteCommandText;
				return;
			}
			base.ImageId = CommandSprite.SpriteId.ToolBarRemove;
			base.ImageAltText = Strings.RemoveCommandText;
		}
	}
}
