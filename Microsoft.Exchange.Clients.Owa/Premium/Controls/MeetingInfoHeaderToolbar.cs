using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200036B RID: 875
	public class MeetingInfoHeaderToolbar : Toolbar
	{
		// Token: 0x060020F3 RID: 8435 RVA: 0x000BDB72 File Offset: 0x000BBD72
		internal MeetingInfoHeaderToolbar() : base("mtgHeaderTb", ToolbarType.Form)
		{
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x000BDB80 File Offset: 0x000BBD80
		internal MeetingInfoHeaderToolbar(ResponseType responseType) : base("mtgHeaderTb", ToolbarType.Form)
		{
			this.responseType = responseType;
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x000BDB98 File Offset: 0x000BBD98
		protected override void RenderButtons()
		{
			switch (this.responseType)
			{
			case ResponseType.Tentative:
				base.RenderButton(ToolbarButtons.ResponseTentative);
				return;
			case ResponseType.Accept:
				base.RenderButton(ToolbarButtons.ResponseAccepted);
				return;
			case ResponseType.Decline:
				base.RenderButton(ToolbarButtons.ResponseDeclined);
				return;
			default:
				base.RenderButton(ToolbarButtons.MeetingCancelled);
				return;
			}
		}

		// Token: 0x0400178D RID: 6029
		private ResponseType responseType;
	}
}
