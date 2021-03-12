using System;
using System.Drawing;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200067A RID: 1658
	public class ViewDetailsCommand : PropertiesCommand
	{
		// Token: 0x060047CB RID: 18379 RVA: 0x000DA955 File Offset: 0x000D8B55
		public ViewDetailsCommand()
		{
			this.DialogSize = ViewDetailsCommand.DefaultDialogSize;
			this.OnClientClick = "ViewDetailsCommandHandler";
			base.ImageAltText = Strings.ViewDetailsCommandText;
		}

		// Token: 0x04003039 RID: 12345
		internal const int DefaultViewDetailWidth = 515;

		// Token: 0x0400303A RID: 12346
		internal const int DefaultViewDetailHeight = 475;

		// Token: 0x0400303B RID: 12347
		internal static readonly Size DefaultDialogSize = new Size(515, 475);
	}
}
