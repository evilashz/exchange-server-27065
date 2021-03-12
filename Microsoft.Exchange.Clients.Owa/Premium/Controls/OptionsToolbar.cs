using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003D9 RID: 985
	public class OptionsToolbar : Toolbar
	{
		// Token: 0x06002459 RID: 9305 RVA: 0x000D381A File Offset: 0x000D1A1A
		protected override void RenderButtons()
		{
			base.RenderButton(ToolbarButtons.Save);
		}
	}
}
