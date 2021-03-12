using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005BD RID: 1469
	[ToolboxData("<{0}:EcpHyperLink runat=\"server\" />")]
	public class EcpHyperLink : HyperLink
	{
		// Token: 0x170025F2 RID: 9714
		// (get) Token: 0x060042D9 RID: 17113 RVA: 0x000CB284 File Offset: 0x000C9484
		// (set) Token: 0x060042DA RID: 17114 RVA: 0x000CB28C File Offset: 0x000C948C
		[Bindable(true)]
		[DefaultValue(EACHelpId.Default)]
		[Category("Behavior")]
		public string HelpId
		{
			get
			{
				return this.helpId;
			}
			set
			{
				this.helpId = value;
			}
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x000CB295 File Offset: 0x000C9495
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			base.NavigateUrl = HelpLink.GetHrefNoEncoding(this.HelpId);
			base.Attributes.Add("onclick", "PopupWindowManager.showHelpClient(this.href); return false;");
		}

		// Token: 0x04002D86 RID: 11654
		private string helpId = EACHelpId.Default.ToString();
	}
}
