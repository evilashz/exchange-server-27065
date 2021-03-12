using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005A4 RID: 1444
	[ControlValueProperty("Value")]
	[ClientScriptResource("DaysOfWeekSelector", "Microsoft.Exchange.Management.ControlPanel.Client.Reporting.js")]
	[SupportsEventValidation]
	public class DaysOfWeekSelector : ScriptControlBase
	{
		// Token: 0x06004209 RID: 16905 RVA: 0x000C923C File Offset: 0x000C743C
		public DaysOfWeekSelector() : base(HtmlTextWriterTag.Div)
		{
			this.chkSunday = new CheckBox();
			this.chkMonday = new CheckBox();
			this.chkTuesday = new CheckBox();
			this.chkWednesday = new CheckBox();
			this.chkThursday = new CheckBox();
			this.chkFriday = new CheckBox();
			this.chkSaturday = new CheckBox();
		}

		// Token: 0x0600420A RID: 16906 RVA: 0x000C92A0 File Offset: 0x000C74A0
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddElementProperty("ChkSunday", this.ChkSundayID);
			descriptor.AddElementProperty("ChkMonday", this.ChkMondayID);
			descriptor.AddElementProperty("ChkTuesday", this.ChkTuesdayID);
			descriptor.AddElementProperty("ChkWednesday", this.ChkWednesdayID);
			descriptor.AddElementProperty("ChkThursday", this.ChkThursdayID);
			descriptor.AddElementProperty("ChkFriday", this.ChkFridayID);
			descriptor.AddElementProperty("ChkSaturday", this.ChkSaturdayID);
		}

		// Token: 0x0600420B RID: 16907 RVA: 0x000C932C File Offset: 0x000C752C
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			this.chkSunday.Text = OwaOptionStrings.SundayCheckBoxText;
			webControl.Controls.Add(this.chkSunday);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Div);
			this.chkMonday.Text = OwaOptionStrings.MondayCheckBoxText;
			webControl2.Controls.Add(this.chkMonday);
			WebControl webControl3 = new WebControl(HtmlTextWriterTag.Div);
			this.chkTuesday.Text = OwaOptionStrings.TuesdayCheckBoxText;
			webControl3.Controls.Add(this.chkTuesday);
			WebControl webControl4 = new WebControl(HtmlTextWriterTag.Div);
			this.chkWednesday.Text = OwaOptionStrings.WednesdayCheckBoxText;
			webControl4.Controls.Add(this.chkWednesday);
			WebControl webControl5 = new WebControl(HtmlTextWriterTag.Div);
			this.chkThursday.Text = OwaOptionStrings.ThursdayCheckBoxText;
			webControl5.Controls.Add(this.chkThursday);
			WebControl webControl6 = new WebControl(HtmlTextWriterTag.Div);
			this.chkFriday.Text = OwaOptionStrings.FridayCheckBoxText;
			webControl6.Controls.Add(this.chkFriday);
			WebControl webControl7 = new WebControl(HtmlTextWriterTag.Div);
			this.chkSaturday.Text = OwaOptionStrings.SaturdayCheckBoxText;
			webControl7.Controls.Add(this.chkSaturday);
			this.Controls.Add(webControl);
			this.Controls.Add(webControl2);
			this.Controls.Add(webControl3);
			this.Controls.Add(webControl4);
			this.Controls.Add(webControl5);
			this.Controls.Add(webControl6);
			this.Controls.Add(webControl7);
		}

		// Token: 0x17002597 RID: 9623
		// (get) Token: 0x0600420C RID: 16908 RVA: 0x000C94DE File Offset: 0x000C76DE
		public string ChkSundayID
		{
			get
			{
				return this.chkSunday.ClientID;
			}
		}

		// Token: 0x17002598 RID: 9624
		// (get) Token: 0x0600420D RID: 16909 RVA: 0x000C94EB File Offset: 0x000C76EB
		public string ChkMondayID
		{
			get
			{
				return this.chkMonday.ClientID;
			}
		}

		// Token: 0x17002599 RID: 9625
		// (get) Token: 0x0600420E RID: 16910 RVA: 0x000C94F8 File Offset: 0x000C76F8
		public string ChkTuesdayID
		{
			get
			{
				return this.chkTuesday.ClientID;
			}
		}

		// Token: 0x1700259A RID: 9626
		// (get) Token: 0x0600420F RID: 16911 RVA: 0x000C9505 File Offset: 0x000C7705
		public string ChkWednesdayID
		{
			get
			{
				return this.chkWednesday.ClientID;
			}
		}

		// Token: 0x1700259B RID: 9627
		// (get) Token: 0x06004210 RID: 16912 RVA: 0x000C9512 File Offset: 0x000C7712
		public string ChkThursdayID
		{
			get
			{
				return this.chkThursday.ClientID;
			}
		}

		// Token: 0x1700259C RID: 9628
		// (get) Token: 0x06004211 RID: 16913 RVA: 0x000C951F File Offset: 0x000C771F
		public string ChkFridayID
		{
			get
			{
				return this.chkFriday.ClientID;
			}
		}

		// Token: 0x1700259D RID: 9629
		// (get) Token: 0x06004212 RID: 16914 RVA: 0x000C952C File Offset: 0x000C772C
		public string ChkSaturdayID
		{
			get
			{
				return this.chkSaturday.ClientID;
			}
		}

		// Token: 0x04002B7A RID: 11130
		private CheckBox chkSunday;

		// Token: 0x04002B7B RID: 11131
		private CheckBox chkMonday;

		// Token: 0x04002B7C RID: 11132
		private CheckBox chkTuesday;

		// Token: 0x04002B7D RID: 11133
		private CheckBox chkWednesday;

		// Token: 0x04002B7E RID: 11134
		private CheckBox chkThursday;

		// Token: 0x04002B7F RID: 11135
		private CheckBox chkFriday;

		// Token: 0x04002B80 RID: 11136
		private CheckBox chkSaturday;
	}
}
