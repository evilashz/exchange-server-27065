using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000476 RID: 1142
	public class RecurrenceForm : OwaForm
	{
		// Token: 0x06002BB2 RID: 11186 RVA: 0x000F458E File Offset: 0x000F278E
		protected void RenderStartTime()
		{
			TimeDropDownList.RenderTimePicker(base.SanitizingResponse, DateTimeUtilities.GetLocalTime(), "divSTR");
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000F45A5 File Offset: 0x000F27A5
		protected void RenderEndTime()
		{
			TimeDropDownList.RenderTimePicker(base.SanitizingResponse, DateTimeUtilities.GetLocalTime(), "divETR");
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000F45BC File Offset: 0x000F27BC
		protected void RenderDurationTime()
		{
			DurationDropDownList.RenderDurationPicker(base.SanitizingResponse, 30, "divDurR");
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x000F45D0 File Offset: 0x000F27D0
		protected void RenderRecurrenceTypeList()
		{
			this.RenderRecurrenceType(OwaRecurrenceType.None, SanitizedHtmlString.FromStringId(1414246128));
			this.RenderRecurrenceType(OwaRecurrenceType.Daily, SanitizedHtmlString.FromStringId(593489113));
			this.RenderRecurrenceType(OwaRecurrenceType.Weekly, SanitizedHtmlString.FromStringId(-1857560235));
			this.RenderRecurrenceType(OwaRecurrenceType.Monthly, SanitizedHtmlString.FromStringId(400181745));
			this.RenderRecurrenceType(OwaRecurrenceType.Yearly, SanitizedHtmlString.FromStringId(-1360160856));
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x000F4634 File Offset: 0x000F2834
		private static SanitizedHtmlString BuildNthDropdownList(int order)
		{
			SanitizingStringWriter<OwaHtml> sanitizingStringWriter = new SanitizingStringWriter<OwaHtml>();
			sanitizingStringWriter.Write("</div><div class=\"fltBefore nthDropdownList recurrenceDialogText\">");
			DropDownList.RenderDropDownList(sanitizingStringWriter, "divNthLst", order.ToString(CultureInfo.InvariantCulture), RecurrenceForm.nThList);
			sanitizingStringWriter.Write("</div><div class=\"fltBefore recurrenceDialogText\">");
			sanitizingStringWriter.Close();
			return sanitizingStringWriter.ToSanitizedString<SanitizedHtmlString>();
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x000F4688 File Offset: 0x000F2888
		private static SanitizedHtmlString BuildDayDropdownList(DaysOfWeek daysOfWeek)
		{
			string[] dayNames = CultureInfo.CurrentCulture.DateTimeFormat.DayNames;
			DropDownListItem[] listItems = new DropDownListItem[]
			{
				new DropDownListItem(127, 696030412),
				new DropDownListItem(62, 394490012),
				new DropDownListItem(65, 1137128015),
				new DropDownListItem(1, dayNames[0]),
				new DropDownListItem(2, dayNames[1]),
				new DropDownListItem(4, dayNames[2]),
				new DropDownListItem(8, dayNames[3]),
				new DropDownListItem(16, dayNames[4]),
				new DropDownListItem(32, dayNames[5]),
				new DropDownListItem(64, dayNames[6])
			};
			SanitizingStringWriter<OwaHtml> sanitizingStringWriter = new SanitizingStringWriter<OwaHtml>();
			sanitizingStringWriter.Write("</div><div class=\"fltBefore rcrDropdown recurrenceDialogText\">");
			TextWriter writer = sanitizingStringWriter;
			string id = "divDLst";
			int num = (int)daysOfWeek;
			DropDownList.RenderDropDownList(writer, id, num.ToString(CultureInfo.InvariantCulture), listItems);
			sanitizingStringWriter.Write("</div><div class=\"fltBefore recurrenceDialogText\">");
			sanitizingStringWriter.Close();
			return sanitizingStringWriter.ToSanitizedString<SanitizedHtmlString>();
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x000F47AC File Offset: 0x000F29AC
		private static SanitizedHtmlString BuildMonthDropdownList()
		{
			string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
			DropDownListItem[] listItems = new DropDownListItem[]
			{
				new DropDownListItem("1", monthNames[0]),
				new DropDownListItem("2", monthNames[1]),
				new DropDownListItem("3", monthNames[2]),
				new DropDownListItem("4", monthNames[3]),
				new DropDownListItem("5", monthNames[4]),
				new DropDownListItem("6", monthNames[5]),
				new DropDownListItem("7", monthNames[6]),
				new DropDownListItem("8", monthNames[7]),
				new DropDownListItem("9", monthNames[8]),
				new DropDownListItem("10", monthNames[9]),
				new DropDownListItem("11", monthNames[10]),
				new DropDownListItem("12", monthNames[11])
			};
			SanitizingStringWriter<OwaHtml> sanitizingStringWriter = new SanitizingStringWriter<OwaHtml>();
			sanitizingStringWriter.Write("</div><div class=\"fltBefore rcrDropdown recurrenceDialogText\">");
			DropDownList.RenderDropDownList(sanitizingStringWriter, "divMLst", "1", listItems);
			sanitizingStringWriter.Write("</div><div class=\"fltBefore recurrenceDialogText\" id=\"divRcrDayInput\">");
			sanitizingStringWriter.Close();
			return sanitizingStringWriter.ToSanitizedString<SanitizedHtmlString>();
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x000F48D4 File Offset: 0x000F2AD4
		private void RenderRecurrenceType(OwaRecurrenceType recurrenceType, SanitizedHtmlString label)
		{
			int num = (int)recurrenceType;
			string value = num.ToString(CultureInfo.InvariantCulture);
			base.SanitizingResponse.Write("<div id=\"divRcrTypeName\"><input type=radio name=rcrT id=rdoRcr");
			base.SanitizingResponse.Write(value);
			base.SanitizingResponse.Write(" value=");
			base.SanitizingResponse.Write(value);
			if (OwaRecurrenceType.None == recurrenceType)
			{
				base.SanitizingResponse.Write(" checked");
			}
			base.SanitizingResponse.Write(">");
			base.SanitizingResponse.Write("<label for=rdoRcr");
			base.SanitizingResponse.Write(value);
			base.SanitizingResponse.Write(">");
			base.SanitizingResponse.Write(label);
			base.SanitizingResponse.Write("</label></div>");
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000F4994 File Offset: 0x000F2B94
		protected void RenderDailyRecurrence()
		{
			base.SanitizingResponse.Write(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(554874232), new object[]
			{
				"<input type=text maxlength=3 size=4 id=txtRcrDC>"
			}));
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x000F49CC File Offset: 0x000F2BCC
		protected void RenderRegenerateDailyRecurrence()
		{
			base.SanitizingResponse.Write(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-1894393874), new object[]
			{
				"<input type=text maxlength=3 size=4 id=txtRcrRGDC>"
			}));
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x000F4A04 File Offset: 0x000F2C04
		protected void RenderWeeklyRecurrenceLabel()
		{
			base.SanitizingResponse.Write(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-1397571568), new object[]
			{
				"<input type=text maxlength=2 size=2 id=txtRcrWC>"
			}));
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x000F4A3C File Offset: 0x000F2C3C
		protected void RenderWeeklyRecurrenceWeekDays()
		{
			base.SanitizingResponse.Write("<div class=\"fltBefore\"><div id=\"divRcrWeekName\">");
			this.RenderWeekDayCheckbox(DayOfWeek.Sunday, DaysOfWeek.Sunday);
			base.SanitizingResponse.Write("</div><div id=\"divRcrWeekName\">");
			this.RenderWeekDayCheckbox(DayOfWeek.Thursday, DaysOfWeek.Thursday);
			base.SanitizingResponse.Write("</div></div>");
			base.SanitizingResponse.Write("<div class=\"fltBefore\"><div id=\"divRcrWeekName\">");
			this.RenderWeekDayCheckbox(DayOfWeek.Monday, DaysOfWeek.Monday);
			base.SanitizingResponse.Write("</div><div id=\"divRcrWeekName\">");
			this.RenderWeekDayCheckbox(DayOfWeek.Friday, DaysOfWeek.Friday);
			base.SanitizingResponse.Write("</div></div>");
			base.SanitizingResponse.Write("<div class=\"fltBefore\"><div id=\"divRcrWeekName\">");
			this.RenderWeekDayCheckbox(DayOfWeek.Tuesday, DaysOfWeek.Tuesday);
			base.SanitizingResponse.Write("</div><div id=\"divRcrWeekName\">");
			this.RenderWeekDayCheckbox(DayOfWeek.Saturday, DaysOfWeek.Saturday);
			base.SanitizingResponse.Write("</div></div>");
			base.SanitizingResponse.Write("<div><div id=\"divRcrWeekName\">");
			this.RenderWeekDayCheckbox(DayOfWeek.Wednesday, DaysOfWeek.Wednesday);
			base.SanitizingResponse.Write("</div><div id=\"divRcrWeekName\"></div></div>");
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000F4B34 File Offset: 0x000F2D34
		protected void RenderWeeklyRecurrence()
		{
			base.SanitizingResponse.Write("<div>");
			this.RenderWeeklyRecurrenceLabel();
			base.SanitizingResponse.Write("</div>");
			this.RenderWeeklyRecurrenceWeekDays();
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000F4B64 File Offset: 0x000F2D64
		protected void RenderRegenerateWeeklyRecurrence()
		{
			base.SanitizingResponse.Write(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-127679442), new object[]
			{
				"<input type=text maxlength=3 size=4 id=txtRcrRGWC>"
			}));
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x000F4B9C File Offset: 0x000F2D9C
		private void RenderWeekDayCheckbox(DayOfWeek dayOfWeek, DaysOfWeek daysOfWeek)
		{
			int num = (int)daysOfWeek;
			string value = num.ToString(CultureInfo.InvariantCulture);
			base.SanitizingResponse.Write("<input type=checkbox id=chkRcrW");
			base.SanitizingResponse.Write(value);
			base.SanitizingResponse.Write(" value=");
			base.SanitizingResponse.Write(value);
			base.SanitizingResponse.Write("> <label for=");
			base.SanitizingResponse.Write("chkRcrW");
			base.SanitizingResponse.Write(value);
			base.SanitizingResponse.Write(">");
			base.SanitizingResponse.Write(CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)dayOfWeek]);
			base.SanitizingResponse.Write("</label>");
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x000F4C58 File Offset: 0x000F2E58
		protected void RenderMonthlyRecurrence()
		{
			base.SanitizingResponse.Write(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(2088839449), new object[]
			{
				"<input type=text maxlength=2 size=3 id=txtRcrMD>",
				"<input type=text maxlength=2 size=3 id=txtRcrMM>"
			}));
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x000F4C98 File Offset: 0x000F2E98
		protected void RenderRegenerateMonthlyRecurrence()
		{
			base.SanitizingResponse.Write(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-1507556038), new object[]
			{
				"<input type=\"text\" maxlength=\"3\" size=\"4\" id=\"txtRcrRGMC\">"
			}));
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x000F4CD0 File Offset: 0x000F2ED0
		protected void RenderMonthlyThRecurrence()
		{
			base.SanitizingResponse.Write(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(153081917), new object[]
			{
				RecurrenceForm.BuildNthDropdownList(1),
				RecurrenceForm.BuildDayDropdownList(DaysOfWeek.Sunday),
				"<input type=\"text\" maxlength=\"2\" size=\"2\" id=\"txtRcrMThM\">"
			}));
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x000F4D1C File Offset: 0x000F2F1C
		protected void RenderYearlyRecurrence()
		{
			base.SanitizingResponse.Write(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-642038370), new object[]
			{
				RecurrenceForm.BuildMonthDropdownList(),
				"<input type=\"text\" maxlength=\"2\" size=\"2\" id=\"txtRcrYD\">"
			}));
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x000F4D5C File Offset: 0x000F2F5C
		protected void RenderRegenerateYearlyRecurrence()
		{
			base.SanitizingResponse.Write(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(1794632949), new object[]
			{
				"<input type=\"text\" maxlength=\"3\" size=\"4\" id=\"txtRcrRGYC\">"
			}));
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x000F4D94 File Offset: 0x000F2F94
		protected void RenderYearlyThRecurrence()
		{
			base.SanitizingResponse.Write(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-1599607980), new object[]
			{
				RecurrenceForm.BuildNthDropdownList(1),
				RecurrenceForm.BuildDayDropdownList(DaysOfWeek.Sunday),
				RecurrenceForm.BuildMonthDropdownList()
			}));
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x000F4DDD File Offset: 0x000F2FDD
		protected void RenderRangeStartDatePicker()
		{
			DatePickerDropDownCombo.RenderDatePicker(base.SanitizingResponse, "divRngS", DateTimeUtilities.GetLocalTime());
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x000F4DF4 File Offset: 0x000F2FF4
		protected void RenderRangeEndAfterOccurences()
		{
			base.SanitizingResponse.Write(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(245952142), new object[]
			{
				"<input maxlength=3 size=4 type=text id=txtOC value=10>"
			}));
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x000F4E2B File Offset: 0x000F302B
		protected void RenderRangeEndByDatePicker()
		{
			DatePickerDropDownCombo.RenderDatePicker(base.SanitizingResponse, "divRngE", DateTimeUtilities.GetLocalTime());
		}

		// Token: 0x04001CFD RID: 7421
		private static readonly DropDownListItem[] nThList = new DropDownListItem[]
		{
			new DropDownListItem("1", -555757312),
			new DropDownListItem("2", -1339960366),
			new DropDownListItem("3", 869183319),
			new DropDownListItem("4", 1031963858),
			new DropDownListItem("-1", 49675370)
		};
	}
}
