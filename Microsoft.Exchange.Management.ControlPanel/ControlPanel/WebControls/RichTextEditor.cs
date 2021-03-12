using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Clients;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000648 RID: 1608
	[RequiredScript(typeof(ExtenderControlBase))]
	[ClientScriptResource("RichTextEditor", "richtexteditor.js")]
	[ClientScriptResource("RichTextEditor", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[ControlValueProperty("Value")]
	public class RichTextEditor : ScriptControlBase
	{
		// Token: 0x06004641 RID: 17985 RVA: 0x000D4603 File Offset: 0x000D2803
		public RichTextEditor() : base(HtmlTextWriterTag.Div)
		{
			this.formatBar = new Panel();
			this.errorBar = new Panel();
			this.iframeBody = new HtmlGenericControl();
		}

		// Token: 0x17002712 RID: 10002
		// (get) Token: 0x06004642 RID: 17986 RVA: 0x000D4630 File Offset: 0x000D2830
		public string[] ButtonsBIU
		{
			get
			{
				if (this.buttonsBIU == null)
				{
					this.buttonsBIU = new string[]
					{
						Strings.ButtonB,
						Strings.ButtonI,
						Strings.ButtonU
					};
				}
				return this.buttonsBIU;
			}
		}

		// Token: 0x17002713 RID: 10003
		// (get) Token: 0x06004643 RID: 17987 RVA: 0x000D4674 File Offset: 0x000D2874
		public string[] ButtonTooltips
		{
			get
			{
				if (this.buttonTooltips == null)
				{
					this.buttonTooltips = new string[]
					{
						Strings.FormatbarTooltipsBold,
						Strings.FormatbarTooltipsItalics,
						Strings.FormatbarTooltipsUnderline,
						Strings.FormatbarTooltipsStrikethrough,
						Strings.FormatbarTooltipsAlignLeft,
						Strings.FormatbarTooltipsCenter,
						Strings.FormatbarTooltipsAlignRight,
						Strings.FormatbarTooltipsBullets,
						Strings.FormatbarTooltipsNumbering,
						Strings.FormatbarTooltipsDecreaseIndent,
						Strings.FormatbarTooltipsIncreaseIndent,
						Strings.FormatbarTooltipsHighlight,
						Strings.FormatbarTooltipsFontColor,
						Strings.FormatbarTooltipsRemoveFormattingEraser,
						Strings.FormatbarTooltipsInsertHorizontalRule,
						Strings.FormatbarTooltipsUndo,
						Strings.FormatbarTooltipsRedo,
						Strings.FormatbarTooltipsInsertHyperlink,
						Strings.FormatbarTooltipsRemoveHyperlink,
						Strings.FormatbarTooltipsSuperscript,
						Strings.FormatbarTooltipsSubscript,
						Strings.FormatbarTooltipsLeftToRight,
						Strings.FormatbarTooltipsRightToLeft,
						Strings.FormatbarTooltipsCustomize
					};
				}
				return this.buttonTooltips;
			}
		}

		// Token: 0x17002714 RID: 10004
		// (get) Token: 0x06004644 RID: 17988 RVA: 0x000D4770 File Offset: 0x000D2970
		public string[] CustomizeTooltips
		{
			get
			{
				if (this.customizeTooltips == null)
				{
					this.customizeTooltips = new string[]
					{
						Strings.CustomizePaneTooltipsBoldItalicsUnderline,
						Strings.CustomizePaneTooltipsStrikeThrough,
						Strings.CustomizePaneTooltipsAlign,
						Strings.CustomizePaneTooltipsBulletsNumbering,
						Strings.CustomizePaneTooltipsIndent,
						Strings.CustomizePaneTooltipsHighlight,
						Strings.CustomizePaneTooltipsFontColor,
						Strings.CustomizePaneTooltipsRemoveFormattingEraser,
						Strings.CustomizePaneTooltipsInsertHorizontalRule,
						Strings.CustomizePaneTooltipsUndoRedo,
						Strings.CustomizePaneTooltipsInsertRemoveHyperlink,
						Strings.CustomizePaneTooltipsSuperscriptSubscript,
						Strings.CustomizePaneTooltipsLtrRtl,
						Strings.CustomizePaneTooltipsCustomize
					};
				}
				return this.customizeTooltips;
			}
		}

		// Token: 0x17002715 RID: 10005
		// (get) Token: 0x06004645 RID: 17989 RVA: 0x000D4812 File Offset: 0x000D2A12
		public string NoneButtonString
		{
			get
			{
				if (this.noneButtonString == null)
				{
					this.noneButtonString = Strings.None;
				}
				return this.noneButtonString;
			}
		}

		// Token: 0x17002716 RID: 10006
		// (get) Token: 0x06004646 RID: 17990 RVA: 0x000D482D File Offset: 0x000D2A2D
		// (set) Token: 0x06004647 RID: 17991 RVA: 0x000D4835 File Offset: 0x000D2A35
		public bool IsMessageFontEditor { get; set; }

		// Token: 0x17002717 RID: 10007
		// (get) Token: 0x06004648 RID: 17992 RVA: 0x000D483E File Offset: 0x000D2A3E
		// (set) Token: 0x06004649 RID: 17993 RVA: 0x000D4846 File Offset: 0x000D2A46
		[DefaultValue(0)]
		public int MaxLength { get; set; }

		// Token: 0x0600464A RID: 17994 RVA: 0x000D484F File Offset: 0x000D2A4F
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			base.RenderEndTag(writer);
			if (Util.IsIE())
			{
				writer.Write(string.Format("<div id=\"{0}_endDiv\" class=\"OffScreen\" tabindex=\"0\"></div>", this.ClientID));
			}
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x000D4878 File Offset: 0x000D2A78
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			Panel panel = new Panel();
			Panel panel2 = new Panel();
			this.CssClass = "richTextEditor";
			this.formatBar.ID = "divFmtBr";
			this.formatBar.CssClass = "formatBarInUMC";
			if (Util.IsIE())
			{
				base.Attributes.Add("tabindex", "0");
			}
			this.errorBar.ID = "divErrBr";
			this.iframeBody.TagName = HtmlTextWriterTag.Iframe.ToString();
			this.iframeBody.ID = "ifBdy";
			this.iframeBody.Attributes["class"] = "richTextFrame";
			this.iframeBody.Attributes["frameborder"] = "0";
			if (Util.IsIE())
			{
				this.iframeBody.Attributes["src"] = ThemeResource.BlankHtmlPath;
			}
			panel.Controls.Add(this.formatBar);
			panel2.Controls.Add(this.errorBar);
			panel2.Controls.Add(this.iframeBody);
			this.Controls.Add(panel);
			this.Controls.Add(panel2);
			if (this.IsMessageFontEditor)
			{
				this.iframeBody.Attributes["class"] = "richTextFrame messageFontPreview";
			}
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x000D49D8 File Offset: 0x000D2BD8
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			CommonMaster commonMaster = (CommonMaster)this.Page.Master;
			if (commonMaster != null)
			{
				commonMaster.AddCssFiles("nbsprite1.mouse.css");
				commonMaster.AddCssFiles("EditorStyles.mouse.css");
			}
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x000D4A18 File Offset: 0x000D2C18
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("FormatBarID", this.formatBar.ClientID);
			descriptor.AddElementProperty("IFrameBody", this.iframeBody.ClientID);
			descriptor.AddElementProperty("ErrorBar", this.errorBar.ClientID);
			if (Util.IsIE())
			{
				descriptor.AddElementProperty("EndDiv", this.ClientID + "_endDiv");
			}
			descriptor.AddScriptProperty("ButtonsBIU", this.ButtonsBIU.ToJsonString(null));
			descriptor.AddScriptProperty("ButtonTooltips", this.ButtonTooltips.ToJsonString(null));
			descriptor.AddScriptProperty("CustomizeTooltips", this.CustomizeTooltips.ToJsonString(null));
			descriptor.AddScriptProperty("NoneButtonString", this.NoneButtonString.ToJsonString(null));
			descriptor.AddProperty("IsMessageFontEditor", this.IsMessageFontEditor);
			if (this.MaxLength > 0)
			{
				descriptor.AddProperty("MaxLength", this.MaxLength - RichTextEditor.ReservedSpace);
			}
		}

		// Token: 0x04002F83 RID: 12163
		private const string EndDivFmt = "<div id=\"{0}_endDiv\" class=\"OffScreen\" tabindex=\"0\"></div>";

		// Token: 0x04002F84 RID: 12164
		private static readonly int ReservedSpace = TextConverterHelper.SanitizeHtml("<br/>").Length - 5;

		// Token: 0x04002F85 RID: 12165
		private string[] buttonsBIU;

		// Token: 0x04002F86 RID: 12166
		private string[] buttonTooltips;

		// Token: 0x04002F87 RID: 12167
		private string[] customizeTooltips;

		// Token: 0x04002F88 RID: 12168
		private string noneButtonString;

		// Token: 0x04002F89 RID: 12169
		private Panel formatBar;

		// Token: 0x04002F8A RID: 12170
		private Panel errorBar;

		// Token: 0x04002F8B RID: 12171
		private HtmlGenericControl iframeBody;
	}
}
