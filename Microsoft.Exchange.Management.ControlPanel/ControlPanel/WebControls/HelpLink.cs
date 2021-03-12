using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Security.Application;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005ED RID: 1517
	[ClientScriptResource("HelpLink", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	[ToolboxData("<{0}:HelpLink runat=\"server\" />")]
	public class HelpLink : ScriptControlBase
	{
		// Token: 0x1700265A RID: 9818
		// (get) Token: 0x0600441D RID: 17437 RVA: 0x000CE06D File Offset: 0x000CC26D
		// (set) Token: 0x0600441E RID: 17438 RVA: 0x000CE074 File Offset: 0x000CC274
		public static Func<string, string> HelpUrlBuilder { get; set; } = new Func<string, string>(HelpUtil.BuildEhcHref);

		// Token: 0x0600441F RID: 17439 RVA: 0x000CE07C File Offset: 0x000CC27C
		public HelpLink() : base(HtmlTextWriterTag.Span)
		{
		}

		// Token: 0x1700265B RID: 9819
		// (get) Token: 0x06004421 RID: 17441 RVA: 0x000CE0B5 File Offset: 0x000CC2B5
		// (set) Token: 0x06004422 RID: 17442 RVA: 0x000CE0BD File Offset: 0x000CC2BD
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(EACHelpId.Default)]
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

		// Token: 0x1700265C RID: 9820
		// (get) Token: 0x06004423 RID: 17443 RVA: 0x000CE0C6 File Offset: 0x000CC2C6
		// (set) Token: 0x06004424 RID: 17444 RVA: 0x000CE0CE File Offset: 0x000CC2CE
		[DefaultValue("")]
		[Category("Behavior")]
		[Bindable(true)]
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x1700265D RID: 9821
		// (get) Token: 0x06004425 RID: 17445 RVA: 0x000CE0D7 File Offset: 0x000CC2D7
		// (set) Token: 0x06004426 RID: 17446 RVA: 0x000CE0DF File Offset: 0x000CC2DF
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool TextIsFormatString
		{
			get
			{
				return this.textIsFormatString;
			}
			set
			{
				this.textIsFormatString = value;
			}
		}

		// Token: 0x06004427 RID: 17447 RVA: 0x000CE0E8 File Offset: 0x000CC2E8
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			if (this.TextIsFormatString)
			{
				Literal literal = new Literal();
				literal.Text = string.Format(this.Text, this.GetHref());
				this.Controls.Add(literal);
			}
			else
			{
				HyperLink hyperLink = new HyperLink();
				hyperLink.NavigateUrl = this.GetHrefNoEncoding();
				hyperLink.Text = this.Text;
				hyperLink.Attributes.Add("onclick", "PopupWindowManager.showHelpClient(this.href); return false;");
				this.Controls.Add(hyperLink);
			}
			base.RenderChildren(writer);
		}

		// Token: 0x06004428 RID: 17448 RVA: 0x000CE16E File Offset: 0x000CC36E
		private string GetHref()
		{
			return HelpLink.GetHref(this.HelpId);
		}

		// Token: 0x06004429 RID: 17449 RVA: 0x000CE17B File Offset: 0x000CC37B
		private string GetHrefNoEncoding()
		{
			return HelpLink.GetHrefNoEncoding(this.HelpId);
		}

		// Token: 0x0600442A RID: 17450 RVA: 0x000CE188 File Offset: 0x000CC388
		internal static string GetHref(string helpId)
		{
			return Encoder.HtmlEncode(HelpLink.HelpUrlBuilder(helpId));
		}

		// Token: 0x0600442B RID: 17451 RVA: 0x000CE19A File Offset: 0x000CC39A
		internal static string GetHrefNoEncoding(string helpId)
		{
			return HelpLink.HelpUrlBuilder(helpId);
		}

		// Token: 0x04002DDB RID: 11739
		internal const string ShowHelpClientScript = "PopupWindowManager.showHelpClient(this.href); return false;";

		// Token: 0x04002DDC RID: 11740
		private string helpId = EACHelpId.Default.ToString();

		// Token: 0x04002DDD RID: 11741
		private string text = string.Empty;

		// Token: 0x04002DDE RID: 11742
		private bool textIsFormatString;
	}
}
