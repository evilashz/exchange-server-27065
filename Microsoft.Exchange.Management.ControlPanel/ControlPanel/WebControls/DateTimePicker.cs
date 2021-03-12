using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005A3 RID: 1443
	[ClientScriptResource("DateTimePicker", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[RequiredScript(typeof(ExtenderControlBase))]
	[ControlValueProperty("Value")]
	[SupportsEventValidation]
	public class DateTimePicker : ScriptControlBase
	{
		// Token: 0x060041F4 RID: 16884 RVA: 0x000C8CA0 File Offset: 0x000C6EA0
		public DateTimePicker() : base(HtmlTextWriterTag.Div)
		{
			this.datePicker = new Panel();
			Panel panel = this.datePicker;
			panel.CssClass += " datePicker DropDown";
			this.datePicker.ID = "datePicker";
			this.dateComboBox = new Panel();
			this.dateComboBox.ID = "dateComboBox";
			this.dateText = new Label();
			this.dateText.ID = "dateText";
			this.dateText.Text = "---";
			this.calendar = new Panel();
			this.calendar.ID = "calendar";
			this.dropArrow = new CommonSprite();
			this.dropArrow.ID = "dropArrow";
			this.timePicker = new DropDownList();
			this.timePicker.ID = "timePicker";
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x000C8D82 File Offset: 0x000C6F82
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.dropArrow.ImageId = CommonSprite.SpriteId.ArrowDown;
			this.FillTimeDropDownList();
		}

		// Token: 0x060041F6 RID: 16886 RVA: 0x000C8DA0 File Offset: 0x000C6FA0
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddElementProperty("DatePicker", this.DatePickerID);
			descriptor.AddElementProperty("DateComboBox", this.DateComboBoxID);
			descriptor.AddElementProperty("DateText", this.DateTextID);
			descriptor.AddElementProperty("Calendar", this.CalendarID);
			descriptor.AddElementProperty("TimePicker", this.TimePickerID);
			descriptor.AddProperty("HasTimePicker", this.HasTimePicker);
			descriptor.AddProperty("Today", this.Today);
			descriptor.AddProperty("WeekStartDay", this.WeekStartDay);
			descriptor.AddProperty("GeneralizedDateTimeFormat", this.GeneralizedDateTimeFormat);
			descriptor.AddProperty("WeekdayDateFormat", this.WeekdayDateFormat);
			descriptor.AddProperty("YearMonthFormat", this.YearMonthFormat);
			descriptor.AddScriptProperty("OneLetterDayNames", this.OneLetterDayNames.ToJsonString(null));
			descriptor.AddScriptProperty("AbbreviatedDayNames", this.AbbreviatedDayNames.ToJsonString(null));
			descriptor.AddScriptProperty("AbbreviatedMonthNames", this.AbbreviatedMonthNames.ToJsonString(null));
			descriptor.AddScriptProperty("MonthNames", this.MonthNames.ToJsonString(null));
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x000C8ED8 File Offset: 0x000C70D8
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			Table table = new Table();
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			Table table2 = new Table();
			TableRow tableRow2 = new TableRow();
			TableCell tableCell2 = new TableCell();
			tableCell2.CssClass = "ddBoxTxtTd";
			if (Util.IsIE())
			{
				this.dateText.CssClass = "dateTextIE";
			}
			tableCell2.Controls.Add(this.dateText);
			TableCell tableCell3 = new TableCell();
			tableCell3.CssClass = "ddBoxImgTd";
			tableCell3.Controls.Add(this.dropArrow);
			tableRow2.Cells.Add(tableCell2);
			tableRow2.Cells.Add(tableCell3);
			table2.Rows.Add(tableRow2);
			table2.CssClass = "ddBoxTbl";
			this.dateComboBox.Controls.Add(table2);
			this.calendar.CssClass = "dpDd";
			this.calendar.Style[HtmlTextWriterStyle.Display] = "none";
			this.dateComboBox.CssClass = "datePicker dropDownBox";
			this.datePicker.Controls.Add(this.dateComboBox);
			this.datePicker.Controls.Add(this.calendar);
			tableCell.Controls.Add(this.datePicker);
			tableRow.Cells.Add(tableCell);
			TableCell tableCell4 = new TableCell();
			this.timePicker.CssClass = "timePicker";
			if (!this.HasTimePicker)
			{
				this.timePicker.Enabled = false;
				tableCell4.Style[HtmlTextWriterStyle.Display] = "none";
			}
			EncodingLabel child = Util.CreateHiddenForSRLabel(string.Empty, this.timePicker.ID);
			tableCell4.Controls.Add(child);
			tableCell4.Controls.Add(this.timePicker);
			tableRow.Cells.Add(tableCell4);
			table.Rows.Add(tableRow);
			table.CellSpacing = 0;
			table.CellPadding = 5;
			this.Controls.Add(table);
			this.CssClass = "dateTimePicker";
		}

		// Token: 0x17002588 RID: 9608
		// (get) Token: 0x060041F8 RID: 16888 RVA: 0x000C90E9 File Offset: 0x000C72E9
		// (set) Token: 0x060041F9 RID: 16889 RVA: 0x000C90F1 File Offset: 0x000C72F1
		public bool HasTimePicker { get; set; }

		// Token: 0x17002589 RID: 9609
		// (get) Token: 0x060041FA RID: 16890 RVA: 0x000C90FA File Offset: 0x000C72FA
		public string DatePickerID
		{
			get
			{
				return this.datePicker.ClientID;
			}
		}

		// Token: 0x1700258A RID: 9610
		// (get) Token: 0x060041FB RID: 16891 RVA: 0x000C9107 File Offset: 0x000C7307
		public string DateComboBoxID
		{
			get
			{
				return this.dateComboBox.ClientID;
			}
		}

		// Token: 0x1700258B RID: 9611
		// (get) Token: 0x060041FC RID: 16892 RVA: 0x000C9114 File Offset: 0x000C7314
		public string DateTextID
		{
			get
			{
				return this.dateText.ClientID;
			}
		}

		// Token: 0x1700258C RID: 9612
		// (get) Token: 0x060041FD RID: 16893 RVA: 0x000C9121 File Offset: 0x000C7321
		public string CalendarID
		{
			get
			{
				return this.calendar.ClientID;
			}
		}

		// Token: 0x1700258D RID: 9613
		// (get) Token: 0x060041FE RID: 16894 RVA: 0x000C912E File Offset: 0x000C732E
		public string TimePickerID
		{
			get
			{
				return this.timePicker.ClientID;
			}
		}

		// Token: 0x1700258E RID: 9614
		// (get) Token: 0x060041FF RID: 16895 RVA: 0x000C913B File Offset: 0x000C733B
		public string Today
		{
			get
			{
				return ExDateTime.GetNow(EcpDateTimeHelper.GetCurrentUserTimeZone()).ToUserDateTimeGeneralFormatString();
			}
		}

		// Token: 0x1700258F RID: 9615
		// (get) Token: 0x06004200 RID: 16896 RVA: 0x000C914C File Offset: 0x000C734C
		public int WeekStartDay
		{
			get
			{
				return LocalSession.Current.WeekStartDay;
			}
		}

		// Token: 0x17002590 RID: 9616
		// (get) Token: 0x06004201 RID: 16897 RVA: 0x000C9158 File Offset: 0x000C7358
		public string GeneralizedDateTimeFormat
		{
			get
			{
				return "yyyy/MM/dd HH:mm:ss";
			}
		}

		// Token: 0x17002591 RID: 9617
		// (get) Token: 0x06004202 RID: 16898 RVA: 0x000C915F File Offset: 0x000C735F
		public string WeekdayDateFormat
		{
			get
			{
				return EcpDateTimeHelper.GetWeekdayDateFormat(false);
			}
		}

		// Token: 0x17002592 RID: 9618
		// (get) Token: 0x06004203 RID: 16899 RVA: 0x000C9167 File Offset: 0x000C7367
		public string YearMonthFormat
		{
			get
			{
				return CultureInfo.CurrentCulture.DateTimeFormat.YearMonthPattern.Replace("MMMM", "'MMMM'");
			}
		}

		// Token: 0x17002593 RID: 9619
		// (get) Token: 0x06004204 RID: 16900 RVA: 0x000C9187 File Offset: 0x000C7387
		public string[] OneLetterDayNames
		{
			get
			{
				return Culture.GetOneLetterDayNames();
			}
		}

		// Token: 0x17002594 RID: 9620
		// (get) Token: 0x06004205 RID: 16901 RVA: 0x000C918E File Offset: 0x000C738E
		public string[] AbbreviatedDayNames
		{
			get
			{
				return CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames;
			}
		}

		// Token: 0x17002595 RID: 9621
		// (get) Token: 0x06004206 RID: 16902 RVA: 0x000C919F File Offset: 0x000C739F
		public string[] AbbreviatedMonthNames
		{
			get
			{
				return CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames;
			}
		}

		// Token: 0x17002596 RID: 9622
		// (get) Token: 0x06004207 RID: 16903 RVA: 0x000C91B0 File Offset: 0x000C73B0
		public string[] MonthNames
		{
			get
			{
				return CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
			}
		}

		// Token: 0x06004208 RID: 16904 RVA: 0x000C91C4 File Offset: 0x000C73C4
		private void FillTimeDropDownList()
		{
			int num = 0;
			int num2 = this.HasTimePicker ? 1410 : 0;
			for (int i = num; i <= num2; i += 30)
			{
				DateTime dateTime = DateTime.UtcNow.Date + TimeSpan.FromMinutes((double)i);
				this.timePicker.Items.Add(new ListItem(dateTime.ToString(RbacPrincipal.Current.TimeFormat), i.ToString()));
			}
		}

		// Token: 0x04002B73 RID: 11123
		private Panel datePicker;

		// Token: 0x04002B74 RID: 11124
		private Panel dateComboBox;

		// Token: 0x04002B75 RID: 11125
		private Label dateText;

		// Token: 0x04002B76 RID: 11126
		private Panel calendar;

		// Token: 0x04002B77 RID: 11127
		private CommonSprite dropArrow;

		// Token: 0x04002B78 RID: 11128
		private DropDownList timePicker;
	}
}
