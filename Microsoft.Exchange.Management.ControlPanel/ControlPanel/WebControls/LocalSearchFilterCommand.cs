using System;
using System.Drawing;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000603 RID: 1539
	public class LocalSearchFilterCommand : PopupCommand
	{
		// Token: 0x060044F3 RID: 17651 RVA: 0x000CFF71 File Offset: 0x000CE171
		public LocalSearchFilterCommand()
		{
			this.DialogSize = new Size(670, 450);
		}

		// Token: 0x04002E32 RID: 11826
		internal const int DefaultSearchFilterPopupWidth = 670;

		// Token: 0x04002E33 RID: 11827
		internal const int DefaultSearchFilterPopupHeight = 450;
	}
}
