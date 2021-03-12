using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000627 RID: 1575
	[ToolboxData("<{0}:OwaScriptsUtil runat=server></{0}:OwaScriptsUtil>")]
	[ClientScriptResource("OwaScriptsUtil", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	public class OwaScriptsUtil : ScriptControlBase
	{
		// Token: 0x060045A1 RID: 17825 RVA: 0x000D27EC File Offset: 0x000D09EC
		public OwaScriptsUtil() : base(HtmlTextWriterTag.Div)
		{
			this.iframeElement = new HtmlGenericControl(HtmlTextWriterTag.Iframe.ToString());
			this.iframeElement.ID = "iframe";
			this.iframeElement.Style.Add(HtmlTextWriterStyle.Display, "none");
			this.iframeElement.Style.Add(HtmlTextWriterStyle.Width, "0px");
			this.iframeElement.Style.Add(HtmlTextWriterStyle.Height, "0px");
			this.iframeElement.Attributes["tabindex"] = "-1";
			this.iframeElement.Attributes["class"] = "HiddenForScreenReader";
			if (Util.IsIE())
			{
				this.iframeElement.Attributes["src"] = ThemeResource.BlankHtmlPath;
			}
			this.Controls.Add(this.iframeElement);
			Util.RequireUpdateProgressPopUp(this);
		}

		// Token: 0x060045A2 RID: 17826 RVA: 0x000D28D9 File Offset: 0x000D0AD9
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("NameSpace", this.NameSpace, true);
			descriptor.AddProperty("EsoFullAccess", this.EsoFullAccess, true);
			descriptor.AddElementProperty("IframeElement", this.IframeElementID, this);
		}

		// Token: 0x170026D6 RID: 9942
		// (get) Token: 0x060045A3 RID: 17827 RVA: 0x000D2918 File Offset: 0x000D0B18
		// (set) Token: 0x060045A4 RID: 17828 RVA: 0x000D2920 File Offset: 0x000D0B20
		public string NameSpace { get; set; }

		// Token: 0x170026D7 RID: 9943
		// (get) Token: 0x060045A5 RID: 17829 RVA: 0x000D2929 File Offset: 0x000D0B29
		public string IframeElementID
		{
			get
			{
				return this.iframeElement.ClientID;
			}
		}

		// Token: 0x170026D8 RID: 9944
		// (get) Token: 0x060045A6 RID: 17830 RVA: 0x000D2936 File Offset: 0x000D0B36
		public bool EsoFullAccess
		{
			get
			{
				return HttpContext.Current.IsExplicitSignOn() && RbacPrincipal.Current.IsInRole("MailboxFullAccess");
			}
		}

		// Token: 0x04002F0B RID: 12043
		private HtmlGenericControl iframeElement;
	}
}
