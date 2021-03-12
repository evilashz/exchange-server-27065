using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200068B RID: 1675
	[ControlValueProperty("Value")]
	[ToolboxData("<{0}:TimePeriodPicker runat=server></{0}:TimePeriodPicker>")]
	[ClientScriptResource("TimePeriodPicker", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	public class TimePeriodPicker : ScriptControlBase, INamingContainer
	{
		// Token: 0x06004851 RID: 18513 RVA: 0x000DBE13 File Offset: 0x000DA013
		public TimePeriodPicker() : base(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x06004852 RID: 18514 RVA: 0x000DBE20 File Offset: 0x000DA020
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			this.EnsureChildControls();
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddElementProperty("WorkingHoursRB", this.workingHoursRB.ClientID, this);
			descriptor.AddElementProperty("OutsideWorkingHoursRB", this.outisdeWorkingHoursRB.ClientID, this);
			descriptor.AddElementProperty("CustomTimePeriodRB", this.customTimePeriodRB.ClientID, this);
			descriptor.AddElementProperty("InitialHourDDL", this.fromHoursDDL.ClientID, this);
			descriptor.AddElementProperty("FinalHourDDL", this.toHoursDDL.ClientID, this);
			descriptor.AddElementProperty("MondayChbx", this.mondayChbx.ClientID, this);
			descriptor.AddElementProperty("TuesdayChbx", this.tuesdayChbx.ClientID, this);
			descriptor.AddElementProperty("WednesdayChbx", this.wednesdayChbx.ClientID, this);
			descriptor.AddElementProperty("ThursdayChbx", this.thursdayChbx.ClientID, this);
			descriptor.AddElementProperty("FridayChbx", this.fridayChbx.ClientID, this);
			descriptor.AddElementProperty("SaturdayChbx", this.saturdayChbx.ClientID, this);
			descriptor.AddElementProperty("SundayChbx", this.sundayChbx.ClientID, this);
		}

		// Token: 0x06004853 RID: 18515 RVA: 0x000DBF50 File Offset: 0x000DA150
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			Table table = new Table();
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			TableRow tableRow2 = new TableRow();
			TableCell tableCell2 = new TableCell();
			tableRow2.Controls.Add(tableCell2);
			tableRow.Controls.Add(tableCell);
			table.Controls.Add(tableRow);
			table.Controls.Add(tableRow2);
			Table table2 = new Table();
			table2.CellPadding = 4;
			this.workingHoursRB = new RadioButton();
			this.outisdeWorkingHoursRB = new RadioButton();
			this.customTimePeriodRB = new RadioButton();
			this.fromHoursDDL = new DropDownList();
			this.toHoursDDL = new DropDownList();
			this.mondayChbx = new CheckBox();
			this.tuesdayChbx = new CheckBox();
			this.wednesdayChbx = new CheckBox();
			this.thursdayChbx = new CheckBox();
			this.fridayChbx = new CheckBox();
			this.saturdayChbx = new CheckBox();
			this.sundayChbx = new CheckBox();
			this.fromLabel = new Label();
			this.toLabel = new Label();
			TableRow tableRow3 = new TableRow();
			this.workingHoursRB.Checked = true;
			TableCell tableCell3 = new TableCell();
			this.workingHoursRB.ID = "workingHoursID";
			this.workingHoursRB.GroupName = "TimePeriodRBGroup";
			this.workingHoursRB.Text = Strings.TimePeriodWorkingHoursText;
			tableCell3.Controls.Add(this.workingHoursRB);
			tableRow3.Controls.Add(tableCell3);
			tableCell3.ColumnSpan = 2;
			table2.Controls.Add(tableRow3);
			TableRow tableRow4 = new TableRow();
			TableCell tableCell4 = new TableCell();
			this.outisdeWorkingHoursRB.ID = "outsideWorkingHoursID";
			this.outisdeWorkingHoursRB.GroupName = "TimePeriodRBGroup";
			this.outisdeWorkingHoursRB.Text = Strings.TimePeriodOutisideWorkingHoursText;
			tableCell4.Controls.Add(this.outisdeWorkingHoursRB);
			tableRow4.Controls.Add(tableCell4);
			table2.Controls.Add(tableRow4);
			TableRow tableRow5 = new TableRow();
			TableCell tableCell5 = new TableCell();
			this.customTimePeriodRB.ID = "customTimePeriodID";
			this.customTimePeriodRB.GroupName = "TimePeriodRBGroup";
			this.customTimePeriodRB.Text = Strings.TimePeriodCustomPeriodText;
			tableCell5.Controls.Add(this.customTimePeriodRB);
			tableRow5.Cells.Add(tableCell5);
			table2.Controls.Add(tableRow5);
			tableCell.Controls.Add(table2);
			Table table3 = new Table();
			TableRow tableRow6 = new TableRow();
			TableCell tableCell6 = new TableCell();
			this.fromHoursDDL.ID = "fromHoursID";
			this.fromHoursDDL.CssClass = "HourRangeDDLs";
			this.fromLabel.ID = "fromHoursID_label";
			this.fromLabel.Text = Strings.TimePeriodInitialHourText;
			tableCell6.Controls.Add(this.fromLabel);
			tableCell6.Controls.Add(this.fromHoursDDL);
			this.toHoursDDL.ID = "toHoursID";
			this.toHoursDDL.CssClass = "HourRangeDDLs";
			this.toLabel.ID = "toHoursID_label";
			this.toLabel.Text = Strings.TimePeriodFinalHourText;
			this.toLabel.CssClass = "ToLabel";
			tableCell6.Controls.Add(this.toLabel);
			tableCell6.Controls.Add(this.toHoursDDL);
			tableRow6.Controls.Add(tableCell6);
			tableRow6.Controls.Add(tableCell6);
			table3.Controls.Add(tableRow6);
			this.sundayChbx.ID = "sundayID";
			this.sundayChbx.Text = Strings.TimePeriodSundayText;
			this.mondayChbx.ID = "mondayID";
			this.mondayChbx.Text = Strings.TimePeriodMondayText;
			this.tuesdayChbx.ID = "tuesdayID";
			this.tuesdayChbx.Text = Strings.TimePeriodTuesdayText;
			this.wednesdayChbx.ID = "wednesdayID";
			this.wednesdayChbx.Text = Strings.TimePeriodWednesdayText;
			this.thursdayChbx.ID = "thursdayID";
			this.thursdayChbx.Text = Strings.TimePeriodThursdayText;
			this.fridayChbx.ID = "fridayID";
			this.fridayChbx.Text = Strings.TimePeriodFridayText;
			this.saturdayChbx.ID = "saturdayID";
			this.saturdayChbx.Text = Strings.TimePeriodSaturdayText;
			TableRow tableRow7 = new TableRow();
			TableCell tableCell7 = new TableCell();
			tableRow7.Controls.Add(tableCell7);
			Table table4 = new Table();
			TableRow tableRow8 = new TableRow();
			TableCell tableCell8 = new TableCell();
			TableCell tableCell9 = new TableCell();
			TableCell tableCell10 = new TableCell();
			TableCell tableCell11 = new TableCell();
			tableCell8.Controls.Add(this.sundayChbx);
			tableCell9.Controls.Add(this.mondayChbx);
			tableCell10.Controls.Add(this.tuesdayChbx);
			tableCell11.Controls.Add(this.wednesdayChbx);
			tableRow8.Controls.Add(tableCell8);
			tableRow8.Controls.Add(tableCell9);
			tableRow8.Controls.Add(tableCell10);
			tableRow8.Controls.Add(tableCell11);
			table4.Controls.Add(tableRow8);
			TableRow tableRow9 = new TableRow();
			TableCell tableCell12 = new TableCell();
			TableCell tableCell13 = new TableCell();
			TableCell tableCell14 = new TableCell();
			tableCell12.Controls.Add(this.thursdayChbx);
			tableCell13.Controls.Add(this.fridayChbx);
			tableCell14.Controls.Add(this.saturdayChbx);
			tableRow9.Controls.Add(tableCell12);
			tableRow9.Controls.Add(tableCell13);
			tableRow9.Controls.Add(tableCell14);
			table4.Controls.Add(tableRow9);
			tableCell7.Controls.Add(table4);
			table3.Controls.Add(tableRow7);
			table3.CssClass = "CustomPeriodTable";
			tableCell2.Controls.Add(table3);
			this.FillHoursDropDownList();
			this.Controls.Add(table);
		}

		// Token: 0x06004854 RID: 18516 RVA: 0x000DC59C File Offset: 0x000DA79C
		private void FillHoursDropDownList()
		{
			int num = 0;
			int num2 = 1420;
			for (int i = num; i <= num2; i += 60)
			{
				DateTime dateTime = DateTime.UtcNow.Date + TimeSpan.FromMinutes((double)i);
				this.fromHoursDDL.Items.Add(new ListItem(dateTime.ToString(RbacPrincipal.Current.TimeFormat), i.ToString()));
				this.toHoursDDL.Items.Add(new ListItem(dateTime.ToString(RbacPrincipal.Current.TimeFormat), i.ToString()));
			}
		}

		// Token: 0x04003082 RID: 12418
		private RadioButton workingHoursRB;

		// Token: 0x04003083 RID: 12419
		private RadioButton outisdeWorkingHoursRB;

		// Token: 0x04003084 RID: 12420
		private RadioButton customTimePeriodRB;

		// Token: 0x04003085 RID: 12421
		private DropDownList fromHoursDDL;

		// Token: 0x04003086 RID: 12422
		private DropDownList toHoursDDL;

		// Token: 0x04003087 RID: 12423
		private CheckBox mondayChbx;

		// Token: 0x04003088 RID: 12424
		private CheckBox tuesdayChbx;

		// Token: 0x04003089 RID: 12425
		private CheckBox wednesdayChbx;

		// Token: 0x0400308A RID: 12426
		private CheckBox thursdayChbx;

		// Token: 0x0400308B RID: 12427
		private CheckBox fridayChbx;

		// Token: 0x0400308C RID: 12428
		private CheckBox saturdayChbx;

		// Token: 0x0400308D RID: 12429
		private CheckBox sundayChbx;

		// Token: 0x0400308E RID: 12430
		private Label fromLabel;

		// Token: 0x0400308F RID: 12431
		private Label toLabel;
	}
}
