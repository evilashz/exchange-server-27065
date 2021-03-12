using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000660 RID: 1632
	[DefaultProperty("Components")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("SlabTable", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	[ParseChildren(true, "Components")]
	public class SlabTable : DockPanel
	{
		// Token: 0x060046EC RID: 18156 RVA: 0x000D6858 File Offset: 0x000D4A58
		public SlabTable()
		{
			this.CssClass = "baseFrm";
			this.Components = new List<SlabComponent>();
			this.DockEnabled = true;
		}

		// Token: 0x1700274E RID: 10062
		// (get) Token: 0x060046ED RID: 18157 RVA: 0x000D68A9 File Offset: 0x000D4AA9
		// (set) Token: 0x060046EE RID: 18158 RVA: 0x000D68B1 File Offset: 0x000D4AB1
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

		// Token: 0x1700274F RID: 10063
		// (get) Token: 0x060046EF RID: 18159 RVA: 0x000D68BA File Offset: 0x000D4ABA
		// (set) Token: 0x060046F0 RID: 18160 RVA: 0x000D68C2 File Offset: 0x000D4AC2
		public bool UseGlobalSaveButton { get; set; }

		// Token: 0x17002750 RID: 10064
		// (get) Token: 0x060046F1 RID: 18161 RVA: 0x000D68CB File Offset: 0x000D4ACB
		// (set) Token: 0x060046F2 RID: 18162 RVA: 0x000D68D3 File Offset: 0x000D4AD3
		public bool DockEnabled { get; set; }

		// Token: 0x17002751 RID: 10065
		// (get) Token: 0x060046F3 RID: 18163 RVA: 0x000D68DC File Offset: 0x000D4ADC
		// (set) Token: 0x060046F4 RID: 18164 RVA: 0x000D68E4 File Offset: 0x000D4AE4
		internal bool IsSingleSlabPage { get; set; }

		// Token: 0x17002752 RID: 10066
		// (get) Token: 0x060046F5 RID: 18165 RVA: 0x000D68ED File Offset: 0x000D4AED
		// (set) Token: 0x060046F6 RID: 18166 RVA: 0x000D68F5 File Offset: 0x000D4AF5
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public List<SlabComponent> Components { get; private set; }

		// Token: 0x060046F7 RID: 18167 RVA: 0x000D68FE File Offset: 0x000D4AFE
		protected override void OnInit(EventArgs e)
		{
			this.DispatchComponents();
			this.Refactor();
			this.PopulateChildControls();
			this.AddCssFileLinks();
			base.OnInit(e);
			this.ReconfigFVA();
		}

		// Token: 0x060046F8 RID: 18168 RVA: 0x000D6925 File Offset: 0x000D4B25
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("DockEnabled", this.DockEnabled);
		}

		// Token: 0x060046F9 RID: 18169 RVA: 0x000D6944 File Offset: 0x000D4B44
		protected override void Render(HtmlTextWriter writer)
		{
			this.PrepareSlabsForScreenReader();
			this.ShowButtonPanelIfNeeded();
			if (this.saveButtonClientID != null)
			{
				base.Attributes.Add("onkeydown", string.Format("javascript:return EcpSlabTable_FireSaveButton(event, '{0}')", this.saveButtonClientID));
			}
			base.Render(writer);
		}

		// Token: 0x060046FA RID: 18170 RVA: 0x000D6984 File Offset: 0x000D4B84
		private void DispatchComponents()
		{
			this.columns = new List<SlabColumn>();
			this.topRows = new List<SlabRow>();
			this.bottomRows = new List<SlabRow>();
			int num = 0;
			foreach (SlabComponent slabComponent in this.Components)
			{
				SlabColumn slabColumn = slabComponent as SlabColumn;
				if (slabColumn != null)
				{
					if (num == 0)
					{
						num = 1;
					}
					else if (num == 2)
					{
						throw new InvalidOperationException("Don't add SlabRow between SlabColumns! SlabRow can only be added before or after SlabClolumns.");
					}
					this.columns.Add(slabColumn);
				}
				else if (num == 0)
				{
					this.topRows.Add((SlabRow)slabComponent);
				}
				else
				{
					num = 2;
					this.bottomRows.Add((SlabRow)slabComponent);
				}
			}
		}

		// Token: 0x060046FB RID: 18171 RVA: 0x000D6A4C File Offset: 0x000D4C4C
		private void Refactor()
		{
			double num = 0.0;
			double num2 = 0.0;
			bool flag = false;
			foreach (SlabRow slabRow in this.topRows)
			{
				slabRow.Refactor();
			}
			foreach (SlabRow slabRow2 in this.bottomRows)
			{
				slabRow2.Refactor();
			}
			for (int i = this.columns.Count - 1; i >= 0; i--)
			{
				SlabColumn slabColumn = this.columns[i];
				slabColumn.Refactor();
				bool flag2 = false;
				if (slabColumn.Slabs.Count == 0)
				{
					this.columns.RemoveAt(i);
					flag2 = true;
				}
				if (slabColumn.Width.Type == UnitType.Percentage)
				{
					num += slabColumn.Width.Value;
					if (flag2)
					{
						flag = true;
					}
					else
					{
						num2 += slabColumn.Width.Value;
					}
				}
			}
			if (flag && num2 != 0.0)
			{
				double num3 = num / num2;
				foreach (SlabColumn slabColumn2 in this.columns)
				{
					if (slabColumn2.Width.Type == UnitType.Percentage)
					{
						slabColumn2.Width = new Unit(slabColumn2.Width.Value * num3, UnitType.Percentage);
					}
				}
			}
		}

		// Token: 0x060046FC RID: 18172 RVA: 0x000D6C14 File Offset: 0x000D4E14
		private void PopulateChildControls()
		{
			this.Controls.Clear();
			Table table = this.PopulateSlabColumnAndRows();
			table.Attributes.Add("role", "presentation");
			bool flag = this.AddCaptionPanelOrHelpControl();
			this.contentPanel = new Panel();
			this.contentPanel.CssClass = "slbTblCtnPnl";
			if (flag)
			{
				Panel panel = this.contentPanel;
				panel.CssClass += " topSpc";
			}
			this.contentPanel.Attributes.Add("dock", "fill");
			this.contentPanel.Controls.Add(table);
			this.Controls.Add(this.contentPanel);
			this.showCloseButton = this.CheckCloseButton();
			this.AddButtonPanel();
		}

		// Token: 0x060046FD RID: 18173 RVA: 0x000D6CD8 File Offset: 0x000D4ED8
		private void PrepareSlabsForScreenReader()
		{
			foreach (SlabFrame slabFrame in this.slabFrames)
			{
				SlabControl slab = slabFrame.Slab;
				slabFrame.Attributes["tabindex"] = "0";
				slabFrame.Attributes["role"] = "region";
				slabFrame.Attributes["aria-labelledby"] = slabFrame.CaptionLabel.ClientID;
			}
		}

		// Token: 0x060046FE RID: 18174 RVA: 0x000D6D70 File Offset: 0x000D4F70
		private Table PopulateSlabColumnAndRows()
		{
			Table table = new Table();
			table.CssClass = "slbTbl";
			table.CellPadding = 0;
			table.CellSpacing = 0;
			bool showHelp = ((EcpContentPage)this.Page).ShowHelp;
			this.PopulateSlabRows(table, this.topRows);
			bool flag = this.slabFrames.Count == 0;
			if (!flag)
			{
				SlabFrame slabFrame = this.slabFrames[0];
				slabFrame.CssClass += " slbTp";
			}
			bool flag2 = table.Rows.Count > 0;
			int columnSpan = 0;
			if (this.columns.Count > 0)
			{
				TableRow tableRow = new TableRow();
				table.Rows.Add(tableRow);
				this.PopulateSlabColumns(tableRow, showHelp, flag);
				if (flag2)
				{
					this.AddDummyTopRow(table, tableRow);
				}
				columnSpan = tableRow.Cells.Count;
			}
			this.PopulateSlabRows(table, this.bottomRows);
			if (this.columns.Count > 0)
			{
				foreach (TableCell tableCell in this.fullRowCells)
				{
					tableCell.ColumnSpan = columnSpan;
				}
			}
			return table;
		}

		// Token: 0x060046FF RID: 18175 RVA: 0x000D6EAC File Offset: 0x000D50AC
		private void AddDummyTopRow(Table table, TableRow columnRow)
		{
			TableRow tableRow = new TableRow();
			tableRow.Height = new Unit(0.0, UnitType.Pixel);
			foreach (object obj in columnRow.Cells)
			{
				TableCell tableCell = (TableCell)obj;
				TableCell tableCell2 = new TableCell();
				tableCell2.Width = tableCell.Width;
				tableRow.Cells.Add(tableCell2);
			}
			table.Rows.AddAt(0, tableRow);
		}

		// Token: 0x06004700 RID: 18176 RVA: 0x000D6F48 File Offset: 0x000D5148
		private void PopulateSlabRows(Table table, List<SlabRow> rows)
		{
			foreach (SlabRow slabRow in rows)
			{
				foreach (Control control in slabRow.Content)
				{
					TableRow tableRow = new TableRow();
					table.Rows.Add(tableRow);
					TableCell tableCell = new TableCell();
					tableRow.Cells.Add(tableCell);
					this.fullRowCells.Add(tableCell);
					SlabControl slabControl = control as SlabControl;
					if (slabControl != null)
					{
						SlabFrame slabFrame = new SlabFrame(slabControl);
						SlabFrame slabFrame2 = slabFrame;
						slabFrame2.CssClass += " slbLf";
						if (slabControl.UsePropertyPageStyle)
						{
							SlabFrame slabFrame3 = slabFrame;
							slabFrame3.CssClass += " slbPrpg";
						}
						tableCell.Controls.Add(slabFrame);
						this.slabFrames.Add(slabFrame);
					}
					else
					{
						tableCell.Controls.Add(control);
					}
				}
			}
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x000D7080 File Offset: 0x000D5280
		private void PopulateSlabColumns(TableRow row, bool showHelp, bool noSlabInTopRow)
		{
			row.Attributes.Add("srow", "f");
			int num = 0;
			bool flag = false;
			int num2 = 0;
			foreach (SlabColumn slabColumn in this.columns)
			{
				bool flag2 = num2 == 0;
				int count = this.columns.Count;
				TableCell tableCell = new TableCell();
				UnitType type = slabColumn.Width.Type;
				if (type == UnitType.Percentage)
				{
					num += (int)slabColumn.Width.Value;
				}
				else
				{
					if (!slabColumn.Width.IsEmpty)
					{
						throw new NotSupportedException("Slab column width of type other than percentage is not supported.");
					}
					flag = true;
				}
				tableCell.Width = slabColumn.Width;
				tableCell.VerticalAlign = VerticalAlign.Top;
				row.Cells.Add(tableCell);
				int num3 = 0;
				foreach (SlabControl slabControl in slabColumn.Slabs)
				{
					SlabFrame slabFrame = new SlabFrame(slabControl);
					if (flag2)
					{
						SlabFrame slabFrame2 = slabFrame;
						slabFrame2.CssClass += " slbLf";
					}
					if (slabControl.UsePropertyPageStyle)
					{
						SlabFrame slabFrame3 = slabFrame;
						slabFrame3.CssClass += " slbPrpg";
					}
					if (noSlabInTopRow && num3 == 0)
					{
						SlabFrame slabFrame4 = slabFrame;
						slabFrame4.CssClass += " slbTp";
					}
					tableCell.Controls.Add(slabFrame);
					this.slabFrames.Add(slabFrame);
					num3++;
				}
				num2++;
			}
			if (!flag && num < 100)
			{
				TableCell tableCell2 = new TableCell();
				tableCell2.Width = new Unit((double)(100 - num), UnitType.Percentage);
				row.Cells.Add(tableCell2);
			}
		}

		// Token: 0x06004702 RID: 18178 RVA: 0x000D728C File Offset: 0x000D548C
		private bool CheckCloseButton()
		{
			int num = 0;
			for (int i = 0; i < this.slabFrames.Count; i++)
			{
				SlabFrame slabFrame = this.slabFrames[i];
				if (slabFrame.Slab.ShowCloseButton)
				{
					num++;
				}
			}
			return num > 0 && num == this.slabFrames.Count;
		}

		// Token: 0x06004703 RID: 18179 RVA: 0x000D72E4 File Offset: 0x000D54E4
		private bool AddCaptionPanelOrHelpControl()
		{
			bool flag = this.slabFrames.Count == 1;
			bool showHelp = ((EcpContentPage)this.Page).ShowHelp;
			bool result = false;
			if (flag)
			{
				SlabFrame slabFrame = this.slabFrames[0];
				slabFrame.ShowHelp = showHelp;
				slabFrame.PublishHelp = true;
				if (this.IsSingleSlabPage)
				{
					slabFrame.Attributes.Add("fill", "100");
				}
			}
			else if (showHelp)
			{
				CaptionPanel captionPanel = new CaptionPanel();
				captionPanel.HelpId = this.HelpId;
				captionPanel.Attributes.Add("dock", "top");
				captionPanel.ShowCaption = false;
				captionPanel.ShowHelp = true;
				this.Controls.Add(captionPanel);
				result = true;
			}
			else
			{
				HelpControl helpControl = new HelpControl();
				helpControl.HelpId = this.HelpId;
				helpControl.ShowHelp = false;
				helpControl.NeedPublishHelpLinkWhenHidden = true;
				this.Controls.Add(helpControl);
			}
			return result;
		}

		// Token: 0x06004704 RID: 18180 RVA: 0x000D73D4 File Offset: 0x000D55D4
		[Conditional("DEBUG")]
		private void CheckOnePageHasAtMostOnePrimarySlab()
		{
			if (!((EcpContentPage)this.Page).ShowHelp)
			{
				int num = 0;
				foreach (SlabFrame slabFrame in this.slabFrames)
				{
					if (slabFrame.Slab.IsPrimarySlab)
					{
						num++;
					}
				}
			}
		}

		// Token: 0x06004705 RID: 18181 RVA: 0x000D7448 File Offset: 0x000D5648
		[Conditional("DEBUG")]
		private void CheckControlsHaveSameFeatureSet()
		{
			FeatureSet? featureSet = null;
			foreach (SlabFrame slabFrame in this.slabFrames)
			{
				if (featureSet == null)
				{
					featureSet = new FeatureSet?(slabFrame.Slab.FeatureSet);
				}
				else if (featureSet.Value != slabFrame.Slab.FeatureSet)
				{
					break;
				}
			}
		}

		// Token: 0x06004706 RID: 18182 RVA: 0x000D74CC File Offset: 0x000D56CC
		private void AddButtonPanel()
		{
			this.saveSlabFrames = new List<SlabFrame>();
			List<WebServiceMethod> list = new List<WebServiceMethod>();
			foreach (SlabFrame slabFrame in this.slabFrames)
			{
				if (slabFrame.PropertiesControl != null && slabFrame.PropertiesControl.HasSaveMethod)
				{
					this.saveSlabFrames.Add(slabFrame);
					slabFrame.InitSaveButton();
					WebServiceMethod saveWebServiceMethod = slabFrame.PropertiesControl.SaveWebServiceMethod;
					if (saveWebServiceMethod != null)
					{
						list.Add(saveWebServiceMethod);
					}
				}
			}
			if (this.ShowGlobalSaveButton() || this.showCloseButton)
			{
				this.buttonsPanel = new ButtonsPanel();
				if (this.showCloseButton)
				{
					this.buttonsPanel.State = ButtonsPanelState.ReadOnly;
					ButtonsPanel buttonsPanel = this.buttonsPanel;
					buttonsPanel.CssClass += " glbClsPnl";
					this.buttonsPanel.CloseWindowOnCancel = true;
				}
				else
				{
					this.buttonsPanel.State = ButtonsPanelState.Save;
					ButtonsPanel buttonsPanel2 = this.buttonsPanel;
					buttonsPanel2.CssClass += " glbSvPnl";
					this.buttonsPanel.SaveWebServiceMethods.AddRange(list);
				}
				this.buttonsPanel.Attributes.Add("dock", "bottom");
				this.Controls.Add(this.buttonsPanel);
			}
			if (this.saveSlabFrames.Count > 0)
			{
				base.Attributes.Add("data-type", "MultiPropertyPageViewModel");
			}
		}

		// Token: 0x06004707 RID: 18183 RVA: 0x000D7648 File Offset: 0x000D5848
		private void ReconfigFVA()
		{
			if (this.columns.Count == 1 && (this.columns[0].Width.Equals(Unit.Empty) || (this.columns[0].Width.Type == UnitType.Percentage && this.columns[0].Width.Value > 0.95)))
			{
				using (List<SlabControl>.Enumerator enumerator = this.columns[0].Slabs.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SlabControl slabControl = enumerator.Current;
						if (this.IsSimpleFormWithFVAEnabled(slabControl))
						{
							WebControl webControl = slabControl.Parent as WebControl;
							WebControl webControl2 = webControl;
							webControl2.CssClass += " reservedSpaceForFVA";
						}
					}
					return;
				}
			}
			if (this.columns.Count > 1)
			{
				foreach (SlabColumn slabColumn in this.columns)
				{
					foreach (SlabControl slabControl2 in slabColumn.Slabs)
					{
						if (this.IsSimpleFormWithFVAEnabled(slabControl2))
						{
							slabControl2.FieldValidationAssistantExtender.Canvas = this.contentPanel.ClientID;
						}
					}
				}
			}
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x000D77F8 File Offset: 0x000D59F8
		private bool IsSimpleFormWithFVAEnabled(SlabControl slab)
		{
			return slab.FieldValidationAssistantExtender != null && slab.PropertiesControl != null;
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x000D7830 File Offset: 0x000D5A30
		private void ShowButtonPanelIfNeeded()
		{
			bool flag = false;
			if (this.showCloseButton)
			{
				flag = true;
			}
			else if (this.saveSlabFrames.Count > 0)
			{
				int count = this.saveSlabFrames.Count;
				this.saveSlabFrames = (from frame in this.saveSlabFrames
				where !frame.PropertiesControl.ReadOnly
				select frame).ToList<SlabFrame>();
				if (this.ShowGlobalSaveButton())
				{
					if (count != this.saveSlabFrames.Count)
					{
						List<WebServiceMethod> collection = (from saveFrame in this.saveSlabFrames
						select saveFrame.PropertiesControl.SaveWebServiceMethod).ToList<WebServiceMethod>();
						this.buttonsPanel.SaveWebServiceMethods.Clear();
						this.buttonsPanel.SaveWebServiceMethods.AddRange(collection);
					}
					SlabFrame.SetFocusCssOnSaveButton(this.buttonsPanel);
					flag = true;
					this.saveButtonClientID = this.buttonsPanel.CommitButtonClientID;
				}
				else if (this.saveSlabFrames.Count == 1)
				{
					SlabFrame slabFrame = this.saveSlabFrames[0];
					if (slabFrame.Slab.AlwaysDockSaveButton)
					{
						slabFrame.Attributes.Add("fill", "100");
					}
					slabFrame.ShowSaveButton();
					this.saveButtonClientID = slabFrame.SaveButtonClientID;
				}
			}
			if (flag)
			{
				Panel panel = this.contentPanel;
				panel.CssClass += " btmSpc";
				return;
			}
			if (this.buttonsPanel != null)
			{
				this.buttonsPanel.Visible = false;
			}
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x000D79A5 File Offset: 0x000D5BA5
		private bool ShowGlobalSaveButton()
		{
			return this.saveSlabFrames.Count > 1 || (this.UseGlobalSaveButton && this.saveSlabFrames.Count == 1);
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x000D79D0 File Offset: 0x000D5BD0
		private void AddCssFileLinks()
		{
			CommonMaster commonMaster = (CommonMaster)this.Page.Master;
			foreach (SlabFrame slabFrame in this.slabFrames)
			{
				commonMaster.AddCssFiles(slabFrame.Slab.IncludeCssFiles);
			}
		}

		// Token: 0x04002FD5 RID: 12245
		private List<SlabColumn> columns;

		// Token: 0x04002FD6 RID: 12246
		private List<SlabRow> topRows;

		// Token: 0x04002FD7 RID: 12247
		private List<SlabRow> bottomRows;

		// Token: 0x04002FD8 RID: 12248
		private Panel contentPanel;

		// Token: 0x04002FD9 RID: 12249
		private ButtonsPanel buttonsPanel;

		// Token: 0x04002FDA RID: 12250
		private bool showCloseButton;

		// Token: 0x04002FDB RID: 12251
		private List<SlabFrame> saveSlabFrames;

		// Token: 0x04002FDC RID: 12252
		private string saveButtonClientID;

		// Token: 0x04002FDD RID: 12253
		private List<TableCell> fullRowCells = new List<TableCell>();

		// Token: 0x04002FDE RID: 12254
		private List<SlabFrame> slabFrames = new List<SlabFrame>();

		// Token: 0x04002FDF RID: 12255
		private string helpId = string.Empty;
	}
}
