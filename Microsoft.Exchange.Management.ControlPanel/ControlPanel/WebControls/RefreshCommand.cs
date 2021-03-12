using System;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000644 RID: 1604
	public class RefreshCommand : Command
	{
		// Token: 0x06004638 RID: 17976 RVA: 0x000D44DF File Offset: 0x000D26DF
		public RefreshCommand()
		{
			this.Name = "Refresh";
			base.ImageAltText = Strings.RefreshCommandText;
			base.ImageId = CommandSprite.SpriteId.ListViewRefresh;
		}
	}
}
