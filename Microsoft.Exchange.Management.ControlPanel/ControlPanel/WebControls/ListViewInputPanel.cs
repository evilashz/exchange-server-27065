using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000600 RID: 1536
	[ClientScriptResource("ListViewInputPanel", "Microsoft.Exchange.Management.ControlPanel.Client.List.js")]
	[ToolboxData("<{0}:ListViewInputPanel runat=server></{0}:ListViewInputPanel>")]
	public class ListViewInputPanel : Panel, IScriptControl, INamingContainer
	{
		// Token: 0x060044C6 RID: 17606 RVA: 0x000CFA88 File Offset: 0x000CDC88
		public ListViewInputPanel()
		{
			this.textBox = new TextBox();
			this.textBox.AutoPostBack = false;
			this.textBox.ID = "TextBox";
			this.textBox.CssClass = "InputTextBox";
			this.watermarkExtender = new TextBoxWatermarkExtender();
			this.watermarkExtender.ID = "WaterMarkExtender";
			this.CssClass = "InputPanel";
		}

		// Token: 0x060044C7 RID: 17607 RVA: 0x000CFAF8 File Offset: 0x000CDCF8
		protected override void OnPreRender(EventArgs e)
		{
			ScriptManager.GetCurrent(this.Page).RegisterScriptControl<ListViewInputPanel>(this);
			base.OnPreRender(e);
		}

		// Token: 0x060044C8 RID: 17608 RVA: 0x000CFB12 File Offset: 0x000CDD12
		protected override void Render(HtmlTextWriter writer)
		{
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
			base.Render(writer);
		}

		// Token: 0x17002692 RID: 9874
		// (get) Token: 0x060044C9 RID: 17609 RVA: 0x000CFB34 File Offset: 0x000CDD34
		// (set) Token: 0x060044CA RID: 17610 RVA: 0x000CFB3C File Offset: 0x000CDD3C
		public string WatermarkText { get; set; }

		// Token: 0x060044CB RID: 17611 RVA: 0x000CFB48 File Offset: 0x000CDD48
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			Table table = new Table();
			table.Width = Unit.Percentage(100.0);
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.CssClass = "InputCell";
			this.watermarkExtender.TargetControlID = this.textBox.ClientID;
			this.watermarkExtender.WatermarkCssClass = "TextBoxWatermark";
			this.watermarkExtender.WatermarkText = (this.WatermarkText.IsNullOrBlank() ? string.Empty : this.WatermarkText);
			this.textBox.MaxLength = this.MaxLength;
			tableCell.Controls.Add(this.textBox);
			EncodingLabel child = Util.CreateHiddenForSRLabel(this.watermarkExtender.WatermarkText, this.textBox.ClientID);
			tableCell.Controls.Add(child);
			TableCell tableCell2 = new TableCell();
			tableCell2.CssClass = "ButtonCell";
			ToolBarButton toolBarButton = new ToolBarButton(new Command
			{
				ImageId = CommandSprite.SpriteId.MetroAdd,
				ImageAltText = Strings.InlineEditorAddAlternateText
			});
			toolBarButton.BorderWidth = Unit.Pixel(0);
			ToolBarButton toolBarButton2 = toolBarButton;
			toolBarButton2.CssClass += " EnabledToolBarItem";
			toolBarButton.ID = "addBtn";
			tableCell2.Controls.Add(toolBarButton);
			tableCell2.ID = "imageCell";
			tableRow.Cells.Add(tableCell);
			tableRow.Cells.Add(tableCell2);
			table.Rows.Add(tableRow);
			this.Controls.Add(table);
			this.Controls.Add(this.watermarkExtender);
		}

		// Token: 0x17002693 RID: 9875
		// (get) Token: 0x060044CC RID: 17612 RVA: 0x000CFCED File Offset: 0x000CDEED
		// (set) Token: 0x060044CD RID: 17613 RVA: 0x000CFCFA File Offset: 0x000CDEFA
		public int MaxLength
		{
			get
			{
				return this.textBox.MaxLength;
			}
			set
			{
				this.textBox.MaxLength = value;
			}
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x000CFD08 File Offset: 0x000CDF08
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("ListViewInputPanel", this.ClientID);
			return new ScriptDescriptor[]
			{
				scriptControlDescriptor
			};
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x000CFD32 File Offset: 0x000CDF32
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(typeof(ListViewInputPanel));
		}

		// Token: 0x04002E1B RID: 11803
		private TextBoxWatermarkExtender watermarkExtender;

		// Token: 0x04002E1C RID: 11804
		private TextBox textBox;
	}
}
