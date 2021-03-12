using System;
using System.Web.UI.Adapters;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200062A RID: 1578
	public class PanelAdapter : ControlAdapter
	{
		// Token: 0x060045AB RID: 17835 RVA: 0x000D29E0 File Offset: 0x000D0BE0
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Control.DefaultButton != string.Empty)
			{
				this.Control.Attributes.Add("onkeypress", string.Format("javascript:return EcpWebForm_FireDefaultButton(event, '{0}')", this.Control.FindControl(this.Control.DefaultButton).ClientID));
				this.Control.DefaultButton = string.Empty;
			}
		}

		// Token: 0x170026DA RID: 9946
		// (get) Token: 0x060045AC RID: 17836 RVA: 0x000D2A55 File Offset: 0x000D0C55
		private new Panel Control
		{
			get
			{
				return (Panel)base.Control;
			}
		}

		// Token: 0x04002F0D RID: 12045
		private const string FireDefaultButton = "javascript:return EcpWebForm_FireDefaultButton(event, '{0}')";
	}
}
