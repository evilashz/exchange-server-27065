using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200059F RID: 1439
	[ToolboxData("<{0}:DateChooser runat=server></{0}:DateChooser>")]
	[ClientScriptResource("DateChooser", "Microsoft.Exchange.Management.ControlPanel.Client.Reporting.js")]
	[SupportsEventValidation]
	[ControlValueProperty("Value")]
	public class DateChooser : ScriptControlBase, INamingContainer
	{
		// Token: 0x1700257A RID: 9594
		// (get) Token: 0x060041CF RID: 16847 RVA: 0x000C81B8 File Offset: 0x000C63B8
		// (set) Token: 0x060041D0 RID: 16848 RVA: 0x000C81C0 File Offset: 0x000C63C0
		public int MinYear { get; set; }

		// Token: 0x1700257B RID: 9595
		// (get) Token: 0x060041D1 RID: 16849 RVA: 0x000C81C9 File Offset: 0x000C63C9
		// (set) Token: 0x060041D2 RID: 16850 RVA: 0x000C81D1 File Offset: 0x000C63D1
		public bool IsRelativeMinYear { get; set; }

		// Token: 0x1700257C RID: 9596
		// (get) Token: 0x060041D3 RID: 16851 RVA: 0x000C81DA File Offset: 0x000C63DA
		// (set) Token: 0x060041D4 RID: 16852 RVA: 0x000C81E2 File Offset: 0x000C63E2
		public bool ShowToday { get; set; }

		// Token: 0x1700257D RID: 9597
		// (get) Token: 0x060041D5 RID: 16853 RVA: 0x000C81EB File Offset: 0x000C63EB
		// (set) Token: 0x060041D6 RID: 16854 RVA: 0x000C81F3 File Offset: 0x000C63F3
		public int MaxYear { get; set; }

		// Token: 0x1700257E RID: 9598
		// (get) Token: 0x060041D7 RID: 16855 RVA: 0x000C81FC File Offset: 0x000C63FC
		// (set) Token: 0x060041D8 RID: 16856 RVA: 0x000C8204 File Offset: 0x000C6404
		public bool IsRelativeMaxYear { get; set; }

		// Token: 0x060041D9 RID: 16857 RVA: 0x000C8210 File Offset: 0x000C6410
		public DateChooser() : base(HtmlTextWriterTag.Div)
		{
			this.CssClass = "dateChooser";
			this.ddlYear = new DropDownList();
			this.ddlMonth = new DropDownList();
			this.ddlDay = new DropDownList();
			this.MinYear = 1990;
			this.MaxYear = ExDateTime.Now.Year + 1;
			this.IsRelativeMaxYear = false;
			this.IsRelativeMinYear = false;
		}

		// Token: 0x060041DA RID: 16858 RVA: 0x000C827F File Offset: 0x000C647F
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.LoadParam();
		}

		// Token: 0x060041DB RID: 16859 RVA: 0x000C8290 File Offset: 0x000C6490
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!this.Enabled)
			{
				this.ddlYear.Enabled = false;
				this.ddlMonth.Enabled = false;
				this.ddlDay.Enabled = false;
			}
			else
			{
				this.ddlMonth.Enabled = this.IsValid;
				this.ddlDay.Enabled = this.IsValid;
			}
			if (this.ShowToday)
			{
				this.Value = (DateTime)ExDateTime.Now.ToUserExDateTime();
			}
		}

		// Token: 0x060041DC RID: 16860 RVA: 0x000C8314 File Offset: 0x000C6514
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			TableCell tableCell = new TableCell();
			tableCell.CssClass = "dateChooserYearColumn";
			tableCell.Controls.Add(this.ddlYear);
			TableCell tableCell2 = new TableCell();
			tableCell2.CssClass = "dateChooserMonthColumn";
			tableCell2.Controls.Add(this.ddlMonth);
			TableCell tableCell3 = new TableCell();
			tableCell3.CssClass = "dateChooserDayColumn";
			tableCell3.Controls.Add(this.ddlDay);
			TableRow tableRow = new TableRow();
			tableRow.Cells.Add(tableCell);
			tableRow.Cells.Add(tableCell2);
			tableRow.Cells.Add(tableCell3);
			Table table = new Table();
			table.CellPadding = 0;
			table.CellSpacing = 0;
			table.Rows.Add(tableRow);
			this.Controls.Add(table);
			EncodingLabel child = Util.CreateHiddenForSRLabel("year", this.ddlYear.ID);
			EncodingLabel child2 = Util.CreateHiddenForSRLabel("month", this.ddlMonth.ID);
			EncodingLabel child3 = Util.CreateHiddenForSRLabel("day", this.ddlDay.ID);
			this.ddlYear.Parent.Controls.Add(child);
			this.ddlMonth.Parent.Controls.Add(child2);
			this.ddlDay.Parent.Controls.Add(child3);
		}

		// Token: 0x1700257F RID: 9599
		// (get) Token: 0x060041DD RID: 16861 RVA: 0x000C8476 File Offset: 0x000C6676
		// (set) Token: 0x060041DE RID: 16862 RVA: 0x000C8488 File Offset: 0x000C6688
		protected int Month
		{
			get
			{
				return int.Parse(this.ddlMonth.SelectedValue);
			}
			set
			{
				this.ddlMonth.SelectedValue = value.ToString();
			}
		}

		// Token: 0x17002580 RID: 9600
		// (get) Token: 0x060041DF RID: 16863 RVA: 0x000C849C File Offset: 0x000C669C
		// (set) Token: 0x060041E0 RID: 16864 RVA: 0x000C84AE File Offset: 0x000C66AE
		protected int Day
		{
			get
			{
				return int.Parse(this.ddlDay.SelectedValue);
			}
			set
			{
				this.ddlDay.SelectedValue = value.ToString();
			}
		}

		// Token: 0x17002581 RID: 9601
		// (get) Token: 0x060041E1 RID: 16865 RVA: 0x000C84C2 File Offset: 0x000C66C2
		// (set) Token: 0x060041E2 RID: 16866 RVA: 0x000C84D4 File Offset: 0x000C66D4
		protected int Year
		{
			get
			{
				return int.Parse(this.ddlYear.SelectedValue);
			}
			set
			{
				this.ddlYear.SelectedValue = value.ToString();
			}
		}

		// Token: 0x17002582 RID: 9602
		// (get) Token: 0x060041E3 RID: 16867 RVA: 0x000C84E8 File Offset: 0x000C66E8
		protected bool IsValid
		{
			get
			{
				return this.ddlYear.SelectedIndex != 0 && this.ddlMonth.SelectedIndex != 0 && this.ddlDay.SelectedIndex != 0;
			}
		}

		// Token: 0x17002583 RID: 9603
		// (get) Token: 0x060041E4 RID: 16868 RVA: 0x000C8518 File Offset: 0x000C6718
		// (set) Token: 0x060041E5 RID: 16869 RVA: 0x000C8550 File Offset: 0x000C6750
		public DateTime Value
		{
			get
			{
				if (!this.IsValid)
				{
					return default(DateTime);
				}
				return new DateTime(this.Year, this.Month, this.Day);
			}
			set
			{
				this.ddlYear.SelectedValue = value.Year.ToString();
				this.ddlMonth.SelectedValue = value.Month.ToString();
				this.ddlDay.SelectedValue = value.Day.ToString();
			}
		}

		// Token: 0x17002584 RID: 9604
		// (get) Token: 0x060041E6 RID: 16870 RVA: 0x000C85AB File Offset: 0x000C67AB
		public string DdlYearID
		{
			get
			{
				return this.ddlYear.ClientID;
			}
		}

		// Token: 0x17002585 RID: 9605
		// (get) Token: 0x060041E7 RID: 16871 RVA: 0x000C85B8 File Offset: 0x000C67B8
		public string DdlMonthID
		{
			get
			{
				return this.ddlMonth.ClientID;
			}
		}

		// Token: 0x17002586 RID: 9606
		// (get) Token: 0x060041E8 RID: 16872 RVA: 0x000C85C5 File Offset: 0x000C67C5
		public string DdlDayID
		{
			get
			{
				return this.ddlDay.ClientID;
			}
		}

		// Token: 0x060041E9 RID: 16873 RVA: 0x000C85D4 File Offset: 0x000C67D4
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddElementProperty("DdlYear", this.DdlYearID, this);
			descriptor.AddElementProperty("DdlMonth", this.DdlMonthID, this);
			descriptor.AddElementProperty("DdlDay", this.DdlDayID, this);
			descriptor.AddProperty("UserDateFormat", EcpDateTimeHelper.GetUserDateFormat());
		}

		// Token: 0x060041EA RID: 16874 RVA: 0x000C8630 File Offset: 0x000C6830
		private void LoadParam()
		{
			this.ddlYear.ID = "ddlYear";
			this.ddlYear.Items.Add(new ListItem(Strings.DateChooserYear, "0"));
			int num = this.IsRelativeMinYear ? (ExDateTime.Now.Year + this.MinYear) : this.MinYear;
			int num2 = this.IsRelativeMaxYear ? (ExDateTime.Now.Year + this.MaxYear) : this.MaxYear;
			for (int i = num; i <= num2; i++)
			{
				this.ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
			}
			this.ddlMonth.ID = "ddlMonth";
			this.ddlMonth.Items.Add(new ListItem(Strings.DateChooserMonth, "0"));
			this.ddlMonth.Items.Add(new ListItem(Strings.January, "1"));
			this.ddlMonth.Items.Add(new ListItem(Strings.February, "2"));
			this.ddlMonth.Items.Add(new ListItem(Strings.March, "3"));
			this.ddlMonth.Items.Add(new ListItem(Strings.April, "4"));
			this.ddlMonth.Items.Add(new ListItem(Strings.May, "5"));
			this.ddlMonth.Items.Add(new ListItem(Strings.June, "6"));
			this.ddlMonth.Items.Add(new ListItem(Strings.July, "7"));
			this.ddlMonth.Items.Add(new ListItem(Strings.August, "8"));
			this.ddlMonth.Items.Add(new ListItem(Strings.September, "9"));
			this.ddlMonth.Items.Add(new ListItem(Strings.October, "10"));
			this.ddlMonth.Items.Add(new ListItem(Strings.November, "11"));
			this.ddlMonth.Items.Add(new ListItem(Strings.December, "12"));
			this.ddlDay.ID = "ddlDay";
			this.ddlDay.Items.Add(new ListItem(Strings.DateChooserDay, "0"));
			for (int j = 1; j <= 31; j++)
			{
				this.ddlDay.Items.Add(new ListItem(j.ToString(), j.ToString()));
			}
		}

		// Token: 0x04002B66 RID: 11110
		private DropDownList ddlYear;

		// Token: 0x04002B67 RID: 11111
		private DropDownList ddlMonth;

		// Token: 0x04002B68 RID: 11112
		private DropDownList ddlDay;
	}
}
