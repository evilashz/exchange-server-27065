using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005EC RID: 1516
	[ClientScriptResource(null, "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	[ToolboxData("<{0}:HelpControl runat=\"server\" />")]
	public class HelpControl : WebControl
	{
		// Token: 0x17002654 RID: 9812
		// (get) Token: 0x0600440C RID: 17420 RVA: 0x000CDEDB File Offset: 0x000CC0DB
		// (set) Token: 0x0600440D RID: 17421 RVA: 0x000CDEE2 File Offset: 0x000CC0E2
		public static Func<string, string> HelpUrlBuilder { get; set; } = new Func<string, string>(HelpUtil.BuildEhcHref);

		// Token: 0x17002655 RID: 9813
		// (get) Token: 0x0600440F RID: 17423 RVA: 0x000CDEFD File Offset: 0x000CC0FD
		// (set) Token: 0x06004410 RID: 17424 RVA: 0x000CDF05 File Offset: 0x000CC105
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

		// Token: 0x17002656 RID: 9814
		// (get) Token: 0x06004411 RID: 17425 RVA: 0x000CDF0E File Offset: 0x000CC10E
		// (set) Token: 0x06004412 RID: 17426 RVA: 0x000CDF16 File Offset: 0x000CC116
		public string Text { get; set; }

		// Token: 0x17002657 RID: 9815
		// (get) Token: 0x06004413 RID: 17427 RVA: 0x000CDF1F File Offset: 0x000CC11F
		// (set) Token: 0x06004414 RID: 17428 RVA: 0x000CDF27 File Offset: 0x000CC127
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool ShowHelp
		{
			get
			{
				return this.showHelp;
			}
			set
			{
				this.showHelp = value;
			}
		}

		// Token: 0x17002658 RID: 9816
		// (get) Token: 0x06004415 RID: 17429 RVA: 0x000CDF30 File Offset: 0x000CC130
		// (set) Token: 0x06004416 RID: 17430 RVA: 0x000CDF38 File Offset: 0x000CC138
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool NeedPublishHelpLinkWhenHidden
		{
			get
			{
				return this.needPublishHelpLinkWhenHidden;
			}
			set
			{
				this.needPublishHelpLinkWhenHidden = value;
			}
		}

		// Token: 0x17002659 RID: 9817
		// (get) Token: 0x06004417 RID: 17431 RVA: 0x000CDF41 File Offset: 0x000CC141
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x000CDF45 File Offset: 0x000CC145
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.ShowHelp)
			{
				this.AddNormalHelpLink();
				base.Render(writer);
				return;
			}
			if (this.NeedPublishHelpLinkWhenHidden)
			{
				this.PublishHelpLink(writer);
			}
		}

		// Token: 0x06004419 RID: 17433 RVA: 0x000CDF6C File Offset: 0x000CC16C
		private string GetHref()
		{
			return HelpControl.HelpUrlBuilder(this.HelpId);
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x000CDF80 File Offset: 0x000CC180
		private void AddNormalHelpLink()
		{
			HyperLink hyperLink = new HyperLink();
			hyperLink.ID = "HelpLink";
			hyperLink.NavigateUrl = this.GetHref();
			hyperLink.CssClass = "helpLink";
			hyperLink.ToolTip = Strings.Help;
			hyperLink.Text = (string.IsNullOrEmpty(this.Text) ? Strings.Help : this.Text);
			hyperLink.Attributes.Add("onclick", "PopupWindowManager.showHelpClient(this.href); return false;");
			this.Controls.Add(hyperLink);
		}

		// Token: 0x0600441B RID: 17435 RVA: 0x000CE00C File Offset: 0x000CC20C
		private void PublishHelpLink(HtmlTextWriter writer)
		{
			string script = string.Format("window.getHelpLink = function() {{return '{0}';}};\n", HttpUtility.JavaScriptStringEncode(this.GetHref()));
			ScriptManager.RegisterStartupScript(this, base.GetType(), this.ClientID + "_init", script, true);
		}

		// Token: 0x04002DD6 RID: 11734
		private string helpId = EACHelpId.Default.ToString();

		// Token: 0x04002DD7 RID: 11735
		private bool showHelp = true;

		// Token: 0x04002DD8 RID: 11736
		private bool needPublishHelpLinkWhenHidden;
	}
}
