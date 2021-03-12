using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005E2 RID: 1506
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("FilterTextBox", "Microsoft.Exchange.Management.ControlPanel.Client.List.js")]
	[ToolboxData("<{0}:FilterTextBox runat=server></{0}:FilterTextBox>")]
	public class FilterTextBox : TextBox, IScriptControl
	{
		// Token: 0x17002635 RID: 9781
		// (get) Token: 0x0600439C RID: 17308 RVA: 0x000CC77D File Offset: 0x000CA97D
		// (set) Token: 0x0600439D RID: 17309 RVA: 0x000CC785 File Offset: 0x000CA985
		[Bindable(true)]
		[DefaultValue(null)]
		public CommandSprite.SpriteId? SearchButtonImageId { get; set; }

		// Token: 0x0600439E RID: 17310 RVA: 0x000CC790 File Offset: 0x000CA990
		public FilterTextBox()
		{
			this.AutoPostBack = false;
			this.CssClass = "filterTextBox";
			this.MaxLength = 256;
			this.SearchButtonImageId = new CommandSprite.SpriteId?(CommandSprite.SpriteId.SearchDefault);
		}

		// Token: 0x17002636 RID: 9782
		// (get) Token: 0x0600439F RID: 17311 RVA: 0x000CC806 File Offset: 0x000CAA06
		// (set) Token: 0x060043A0 RID: 17312 RVA: 0x000CC80E File Offset: 0x000CAA0E
		public string SearchButtonToolTip
		{
			get
			{
				return this.searchButtonToolTip;
			}
			set
			{
				this.searchButtonToolTip = value;
			}
		}

		// Token: 0x17002637 RID: 9783
		// (get) Token: 0x060043A1 RID: 17313 RVA: 0x000CC817 File Offset: 0x000CAA17
		// (set) Token: 0x060043A2 RID: 17314 RVA: 0x000CC81F File Offset: 0x000CAA1F
		public string ClearButtonToolTip
		{
			get
			{
				return this.clearButtonToolTip;
			}
			set
			{
				this.clearButtonToolTip = value;
			}
		}

		// Token: 0x17002638 RID: 9784
		// (get) Token: 0x060043A3 RID: 17315 RVA: 0x000CC828 File Offset: 0x000CAA28
		// (set) Token: 0x060043A4 RID: 17316 RVA: 0x000CC830 File Offset: 0x000CAA30
		public string WatermarkText
		{
			get
			{
				return this.watermarkText;
			}
			set
			{
				this.watermarkText = value;
			}
		}

		// Token: 0x17002639 RID: 9785
		// (get) Token: 0x060043A5 RID: 17317 RVA: 0x000CC839 File Offset: 0x000CAA39
		// (set) Token: 0x060043A6 RID: 17318 RVA: 0x000CC841 File Offset: 0x000CAA41
		[ClientPropertyName("EnableAutoSuggestion")]
		public bool EnableAutoSuggestion
		{
			get
			{
				return this.enableAutoSuggestion;
			}
			set
			{
				this.enableAutoSuggestion = value;
			}
		}

		// Token: 0x1700263A RID: 9786
		// (get) Token: 0x060043A7 RID: 17319 RVA: 0x000CC84A File Offset: 0x000CAA4A
		// (set) Token: 0x060043A8 RID: 17320 RVA: 0x000CC852 File Offset: 0x000CAA52
		public string SuggestionServicePath
		{
			get
			{
				return this.suggestionServicePath;
			}
			set
			{
				this.suggestionServicePath = value;
			}
		}

		// Token: 0x1700263B RID: 9787
		// (get) Token: 0x060043A9 RID: 17321 RVA: 0x000CC85B File Offset: 0x000CAA5B
		// (set) Token: 0x060043AA RID: 17322 RVA: 0x000CC863 File Offset: 0x000CAA63
		public string SuggestionServiceWorkFlow
		{
			get
			{
				return this.suggestionServiceWorkflow;
			}
			set
			{
				this.suggestionServiceWorkflow = value;
			}
		}

		// Token: 0x1700263C RID: 9788
		// (get) Token: 0x060043AB RID: 17323 RVA: 0x000CC86C File Offset: 0x000CAA6C
		// (set) Token: 0x060043AC RID: 17324 RVA: 0x000CC874 File Offset: 0x000CAA74
		public string SuggestionServiceMethod
		{
			get
			{
				return this.suggestionServiceMethod;
			}
			set
			{
				this.suggestionServiceMethod = value;
			}
		}

		// Token: 0x1700263D RID: 9789
		// (get) Token: 0x060043AD RID: 17325 RVA: 0x000CC87D File Offset: 0x000CAA7D
		// (set) Token: 0x060043AE RID: 17326 RVA: 0x000CC885 File Offset: 0x000CAA85
		public string AutoSuggestionPropertyNames
		{
			get
			{
				return this.autoSuggestionPropertyNames;
			}
			set
			{
				this.autoSuggestionPropertyNames = value;
			}
		}

		// Token: 0x1700263E RID: 9790
		// (get) Token: 0x060043AF RID: 17327 RVA: 0x000CC88E File Offset: 0x000CAA8E
		// (set) Token: 0x060043B0 RID: 17328 RVA: 0x000CC896 File Offset: 0x000CAA96
		public string AutoSuggestionPropertyValues
		{
			get
			{
				return this.autoSuggestionPropertyValues;
			}
			set
			{
				this.autoSuggestionPropertyValues = value;
			}
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x000CC8A0 File Offset: 0x000CAAA0
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.Write("<table id='{0}' cellspacing='0' cellpadding='0' class='{1}' role=\"presentation\" >", this.ClientID + "_container", "filterTextBoxContainer");
			writer.Write("<tr>");
			writer.Write("<td class='{0}'>", "filterTextBoxTd");
			if (this.hiddenForSRLabel != null)
			{
				this.hiddenForSRLabel.RenderControl(writer);
			}
			base.RenderBeginTag(writer);
		}

		// Token: 0x060043B2 RID: 17330 RVA: 0x000CC904 File Offset: 0x000CAB04
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			base.RenderEndTag(writer);
			writer.Write("</td>");
			writer.Write("<td class='{0} EnabledToolBarItem' id='{1}'>", "filterIndicatorTd", this.ClientID + "_indicatorTd");
			this.imageButtonFilter.RenderControl(writer);
			if (!string.IsNullOrEmpty(this.WatermarkText))
			{
				this.watermarkExtender.RenderControl(writer);
			}
			if (this.EnableAutoSuggestion)
			{
				this.autoCompleteExtender.RenderControl(writer);
			}
			writer.Write("</td>");
			writer.Write("</tr>");
			writer.Write("</table>");
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x000CC9A0 File Offset: 0x000CABA0
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("FilterTextBox", this.ClientID);
			scriptControlDescriptor.AddProperty("SearchButtonToolTip", this.SearchButtonToolTip);
			scriptControlDescriptor.AddProperty("ClearButtonToolTip", this.ClearButtonToolTip);
			scriptControlDescriptor.AddProperty("EnableAutoSuggestion", this.EnableAutoSuggestion);
			return new ScriptDescriptor[]
			{
				scriptControlDescriptor
			};
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x000CCA02 File Offset: 0x000CAC02
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(typeof(FilterTextBox));
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x000CCA14 File Offset: 0x000CAC14
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			string text = "filter or search";
			if (!string.IsNullOrEmpty(this.WatermarkText))
			{
				text = this.WatermarkText;
			}
			this.hiddenForSRLabel = Util.CreateHiddenForSRLabel(text, this.ID);
			this.Controls.Add(this.hiddenForSRLabel);
			this.imageButtonFilter = new HyperLink();
			this.imageButtonFilter.NavigateUrl = "#";
			this.imageButtonFilter.Attributes.Add("onclick", "javascript:return false;");
			this.imageButtonFilter.ToolTip = this.SearchButtonToolTip;
			this.imageButtonFilter.ID = this.ID + "_SearchButton";
			CommandSprite commandSprite = new CommandSprite();
			if (this.SearchButtonImageId != null)
			{
				commandSprite.ImageId = this.SearchButtonImageId.Value;
			}
			else
			{
				commandSprite.ImageId = CommandSprite.SpriteId.SearchDefault;
			}
			commandSprite.ID = this.imageButtonFilter.ID + "_ImageSearchButton";
			commandSprite.AlternateText = this.SearchButtonToolTip;
			this.imageButtonFilter.Controls.Add(commandSprite);
			EncodingLabel encodingLabel = new EncodingLabel();
			encodingLabel.Text = RtlUtil.SearchDefaultMock;
			encodingLabel.ToolTip = this.SearchButtonToolTip;
			encodingLabel.CssClass = "filterIndicatorImageAlter";
			this.imageButtonFilter.Controls.Add(encodingLabel);
			this.Controls.Add(this.imageButtonFilter);
			this.watermarkExtender = new TextBoxWatermarkExtender();
			this.watermarkExtender.TargetControlID = this.UniqueID;
			this.watermarkExtender.WatermarkCssClass = "TextBoxWatermark";
			this.watermarkExtender.WatermarkText = this.WatermarkText;
			this.Controls.Add(this.watermarkExtender);
			if (this.enableAutoSuggestion)
			{
				this.autoCompleteExtender = new EcpAutoCompleteExtender();
				this.autoCompleteExtender.TargetControlID = this.UniqueID;
				WebServiceMethod webServiceMethod = new WebServiceMethod();
				webServiceMethod.ServiceUrl = new WebServiceReference(string.Format("{0}&workflow={1}", this.suggestionServicePath, this.SuggestionServiceWorkFlow));
				webServiceMethod.ID = this.autoCompleteExtender.ID + "WebServiceMethod";
				webServiceMethod.Method = this.SuggestionServiceMethod;
				webServiceMethod.ParameterNames = (WebServiceParameterNames)Enum.Parse(typeof(WebServiceParameterNames), "GetList");
				this.autoCompleteExtender.WebServiceMethod = webServiceMethod;
				this.autoCompleteExtender.AutoSuggestionPropertyNames = this.autoSuggestionPropertyNames;
				this.autoCompleteExtender.AutoSuggestionPropertyValues = this.autoSuggestionPropertyValues;
				this.Controls.Add(this.autoCompleteExtender);
			}
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x000CCCA1 File Offset: 0x000CAEA1
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x000CCCB6 File Offset: 0x000CAEB6
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			ScriptManager.GetCurrent(this.Page).RegisterScriptControl<FilterTextBox>(this);
			ScriptObjectBuilder.RegisterCssReferences(this);
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x000CCCD6 File Offset: 0x000CAED6
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x04002DB1 RID: 11697
		private const int DefaultMaxLength = 256;

		// Token: 0x04002DB2 RID: 11698
		private HyperLink imageButtonFilter;

		// Token: 0x04002DB3 RID: 11699
		private TextBoxWatermarkExtender watermarkExtender;

		// Token: 0x04002DB4 RID: 11700
		private EcpAutoCompleteExtender autoCompleteExtender;

		// Token: 0x04002DB5 RID: 11701
		private EncodingLabel hiddenForSRLabel;

		// Token: 0x04002DB6 RID: 11702
		private string searchButtonToolTip = Strings.SearchButtonTooltip;

		// Token: 0x04002DB7 RID: 11703
		private string clearButtonToolTip = Strings.ClearButtonTooltip;

		// Token: 0x04002DB8 RID: 11704
		private string watermarkText;

		// Token: 0x04002DB9 RID: 11705
		private bool enableAutoSuggestion;

		// Token: 0x04002DBA RID: 11706
		private string suggestionServicePath;

		// Token: 0x04002DBB RID: 11707
		private string suggestionServiceWorkflow = "GetSuggestion";

		// Token: 0x04002DBC RID: 11708
		private string suggestionServiceMethod = "GetList";

		// Token: 0x04002DBD RID: 11709
		private string autoSuggestionPropertyNames;

		// Token: 0x04002DBE RID: 11710
		private string autoSuggestionPropertyValues;
	}
}
