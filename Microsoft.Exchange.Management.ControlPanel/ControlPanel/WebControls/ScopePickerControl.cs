﻿using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200053F RID: 1343
	[ToolboxData("<{0}:ScopePickerControl runat=server></{0}:NumberRangeControl>")]
	[ClientScriptResource("ScopePickerControl", "Microsoft.Exchange.Management.ControlPanel.Client.Users.js")]
	[ControlValueProperty("Value")]
	public class ScopePickerControl : ScriptControlBase, INamingContainer, IScriptControl
	{
		// Token: 0x06003F62 RID: 16226 RVA: 0x000BEBC1 File Offset: 0x000BCDC1
		public ScopePickerControl() : base(HtmlTextWriterTag.Div)
		{
			this.CssClass = "scopePickerControl";
		}

		// Token: 0x170024BF RID: 9407
		// (get) Token: 0x06003F63 RID: 16227 RVA: 0x000BEBD6 File Offset: 0x000BCDD6
		// (set) Token: 0x06003F64 RID: 16228 RVA: 0x000BEBDE File Offset: 0x000BCDDE
		public WebServiceReference ServiceUrl
		{
			get
			{
				return this.serviceUrl;
			}
			set
			{
				this.serviceUrl = value;
			}
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x000BEBE8 File Offset: 0x000BCDE8
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.comboScope = new WebServiceDropDown();
			this.comboScope.ServiceUrl = this.serviceUrl;
			this.comboScope.ID = "comboScope";
			this.comboScope.Width = new Unit(97.0, UnitType.Percentage);
			this.comboScope.Attributes["SetRoles"] = "Get-ManagementScope";
			this.comboScope.Attributes["helpId"] = "EditRoleGroup_Scope";
			this.lblMultiScope = new Label();
			this.lblMultiScope.Text = Strings.MultipleScopeInRoleGroup;
			this.lblMultiScope.ID = "lblMultipleScopeScenario";
			if (Util.IsDataCenter)
			{
				this.Controls.Add(this.comboScope);
				this.Controls.Add(this.lblMultiScope);
				return;
			}
			this.rbScope = new RadioButton();
			this.rbScope.ID = "rbScope";
			this.rbScope.Checked = true;
			this.rbScope.Attributes["helpId"] = "EditRoleGroup_Scope";
			this.rbScope.GroupName = "EditRoleGroupScope";
			this.rbOrganizationalUnit = new RadioButton();
			this.rbOrganizationalUnit.ID = "rbOU";
			this.rbOrganizationalUnit.CssClass = "scopeControlRadioCell";
			this.rbOrganizationalUnit.Attributes["helpId"] = "EditRoleGroup_OU";
			this.rbOrganizationalUnit.GroupName = "EditRoleGroupScope";
			this.tbxOrganizationalUnit = new TextBox();
			this.tbxOrganizationalUnit.ID = "tbxOU";
			this.tbxOrganizationalUnit.Width = new Unit(95.0, UnitType.Percentage);
			this.tbxOrganizationalUnit.CssClass = "ouTextBoxStyle";
			this.tbxOrganizationalUnit.Attributes["SetRoles"] = "Get-OrganizationalUnit";
			this.tbxOrganizationalUnit.Attributes["helpId"] = "EditRoleGroup_OU";
			this.lblOrganizationUnit = new Label();
			this.lblOrganizationUnit.Text = Strings.OULabel;
			this.lblOrganizationUnit.ID = "tbxOU_label";
			this.layoutTable = new HtmlTable();
			this.layoutTable.ID = "ScopeTable";
			this.layoutTable.Attributes.Add("class", "scopePickerTable");
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			HtmlTableCell htmlTableCell2 = new HtmlTableCell();
			htmlTableCell2.Attributes.Add("class", "scopeControlComboCell");
			htmlTableCell.Controls.Add(this.rbScope);
			htmlTableCell.Attributes.Add("class", "scopeControlRadioCell");
			htmlTableCell2.Controls.Add(this.comboScope);
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableRow.Cells.Add(htmlTableCell2);
			this.layoutTable.Rows.Add(htmlTableRow);
			HtmlTableRow htmlTableRow2 = new HtmlTableRow();
			htmlTableRow2.Attributes.Add("class", "scopePickerEmptyRow");
			this.layoutTable.Rows.Add(htmlTableRow2);
			HtmlTableRow htmlTableRow3 = new HtmlTableRow();
			HtmlTableCell cell = new HtmlTableCell();
			HtmlTableCell htmlTableCell3 = new HtmlTableCell();
			htmlTableCell3.Controls.Add(this.lblOrganizationUnit);
			htmlTableRow3.Cells.Add(cell);
			htmlTableRow3.Cells.Add(htmlTableCell3);
			this.layoutTable.Rows.Add(htmlTableRow3);
			HtmlTableRow htmlTableRow4 = new HtmlTableRow();
			HtmlTableCell htmlTableCell4 = new HtmlTableCell();
			HtmlTableCell htmlTableCell5 = new HtmlTableCell();
			htmlTableCell5.Attributes.Add("class", "scopeControlComboCell");
			htmlTableCell4.Attributes.Add("class", "scopeControlRadioCell");
			htmlTableCell4.Controls.Add(this.rbOrganizationalUnit);
			htmlTableCell5.Controls.Add(this.tbxOrganizationalUnit);
			htmlTableRow4.Cells.Add(htmlTableCell4);
			htmlTableRow4.Cells.Add(htmlTableCell5);
			this.layoutTable.Rows.Add(htmlTableRow4);
			this.Controls.Add(this.layoutTable);
			this.Controls.Add(this.lblMultiScope);
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x000BF010 File Offset: 0x000BD210
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			this.EnsureChildControls();
			descriptor.AddComponentProperty("ScopeComboBox", this.comboScope.ClientID, this);
			descriptor.AddElementProperty("MultipleScopeLabel", this.lblMultiScope.ClientID, this);
			if (!Util.IsDataCenter)
			{
				descriptor.AddElementProperty("ScopeTable", this.layoutTable.ClientID, this);
				descriptor.AddElementProperty("OrganizationalUnitLabel", this.lblOrganizationUnit.ClientID, this);
				descriptor.AddElementProperty("OrganizationalUnitTextBox", this.tbxOrganizationalUnit.ClientID, this);
				descriptor.AddElementProperty("OrganizationalUnitRadioButton", this.rbOrganizationalUnit.ClientID, this);
				descriptor.AddElementProperty("ScopeRadioButton", this.rbScope.ClientID, this);
			}
		}

		// Token: 0x040028F7 RID: 10487
		private TextBox tbxOrganizationalUnit;

		// Token: 0x040028F8 RID: 10488
		private WebServiceDropDown comboScope;

		// Token: 0x040028F9 RID: 10489
		private RadioButton rbOrganizationalUnit;

		// Token: 0x040028FA RID: 10490
		private RadioButton rbScope;

		// Token: 0x040028FB RID: 10491
		private WebServiceReference serviceUrl;

		// Token: 0x040028FC RID: 10492
		private Label lblOrganizationUnit;

		// Token: 0x040028FD RID: 10493
		private Label lblMultiScope;

		// Token: 0x040028FE RID: 10494
		private HtmlTable layoutTable;
	}
}
