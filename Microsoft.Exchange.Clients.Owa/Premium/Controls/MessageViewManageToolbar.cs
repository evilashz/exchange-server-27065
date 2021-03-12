using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003BC RID: 956
	public class MessageViewManageToolbar : Toolbar
	{
		// Token: 0x060023C0 RID: 9152 RVA: 0x000CDFD5 File Offset: 0x000CC1D5
		public MessageViewManageToolbar() : base("divMsgViewTB")
		{
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x000CDFE2 File Offset: 0x000CC1E2
		protected override void RenderButtons()
		{
			base.RenderButton(ToolbarButtons.CheckMessages);
			base.RenderFloatedSpacer(1, "divMeasure");
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x000CDFFB File Offset: 0x000CC1FB
		public override bool IsRightAligned
		{
			get
			{
				return true;
			}
		}
	}
}
