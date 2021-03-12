using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000220 RID: 544
	public class PendingMessageTrace : BaseForm
	{
		// Token: 0x06002762 RID: 10082 RVA: 0x0007BC1A File Offset: 0x00079E1A
		protected override void OnPreRender(EventArgs e)
		{
			base.FooterPanel.CancelButton.Attributes.Add("onclick", "top.postMessage('cancelExternalPagePopup', '*')");
			base.OnPreRender(e);
		}
	}
}
