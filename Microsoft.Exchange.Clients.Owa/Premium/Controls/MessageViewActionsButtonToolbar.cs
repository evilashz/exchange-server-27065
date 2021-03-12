using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003B6 RID: 950
	public class MessageViewActionsButtonToolbar : Toolbar
	{
		// Token: 0x060023AE RID: 9134 RVA: 0x000CD557 File Offset: 0x000CB757
		internal MessageViewActionsButtonToolbar() : base("divActBtnTB")
		{
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x000CD564 File Offset: 0x000CB764
		protected override void RenderButtons()
		{
			base.RenderButton(ToolbarButtons.Actions);
			base.RenderFloatedSpacer(1, "divMeasure");
		}
	}
}
