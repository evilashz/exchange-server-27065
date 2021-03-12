using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000021 RID: 33
	public class CalendarOptions : OptionsBase
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00007C63 File Offset: 0x00005E63
		public CalendarOptions(OwaContext owaContext, TextWriter writer) : base(owaContext, writer)
		{
			this.CommitAndLoad();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00007C74 File Offset: 0x00005E74
		private void Load()
		{
			this.startTimeSelection = this.userContext.WorkingHours.WorkDayStartTimeInWorkingHoursTimeZone;
			this.endTimeSelection = this.userContext.WorkingHours.WorkDayEndTimeInWorkingHoursTimeZone;
			this.firstDaySelection = this.userContext.UserOptions.WeekStartDay;
			this.showWeekNumbers = this.userContext.UserOptions.ShowWeekNumbers;
			this.dateTimeFormat = CultureInfo.CurrentUICulture.DateTimeFormat;
			this.isTimeZoneDifferent = this.userContext.WorkingHours.IsTimeZoneDifferent;
			if (this.isTimeZoneDifferent || this.showHomeTimeZone)
			{
				this.homeTimeZoneString = Utilities.HtmlEncode(this.userContext.WorkingHours.CreateHomeTimeZoneString());
			}
			if (this.isTimeZoneDifferent)
			{
				this.currentTimeZoneString = Utilities.HtmlEncode(this.userContext.TimeZone.LocalizableDisplayName.ToString(Thread.CurrentThread.CurrentUICulture));
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007D60 File Offset: 0x00005F60
		private void CommitAndLoad()
		{
			this.Load();
			bool flag = false;
			if (Utilities.IsPostRequest(this.request) && !string.IsNullOrEmpty(base.Command))
			{
				string formParameter = Utilities.GetFormParameter(this.request, "selStTm", false);
				int num;
				if (string.IsNullOrEmpty(formParameter) || !int.TryParse(formParameter, out num))
				{
					throw new OwaInvalidRequestException("Cannot parse WorkDayStartTime");
				}
				string formParameter2 = Utilities.GetFormParameter(this.request, "selEndTm", false);
				int num2;
				if (string.IsNullOrEmpty(formParameter2) || !int.TryParse(formParameter2, out num2))
				{
					throw new OwaInvalidRequestException("Cannot parse WorkDayEndTime");
				}
				if (num != this.startTimeSelection || num2 != this.endTimeSelection)
				{
					if (num > num2)
					{
						base.SetInfobarMessage(LocalizedStrings.GetNonEncoded(107113300), InfobarMessageType.Error);
						return;
					}
					this.userContext.WorkingHours.SetWorkDayTimesInWorkingHoursTimeZone(num, num2);
					flag = true;
				}
				string formParameter3 = Utilities.GetFormParameter(this.request, "selFstDy", false);
				if (!string.IsNullOrEmpty(formParameter3))
				{
					string a = formParameter3;
					int num3 = (int)this.firstDaySelection;
					if (a != num3.ToString())
					{
						this.userContext.UserOptions.WeekStartDay = (DayOfWeek)int.Parse(formParameter3);
						flag = true;
					}
				}
				if (Utilities.GetFormParameter(this.request, "chkWkNm", false) != null)
				{
					if (!this.showWeekNumbers)
					{
						this.userContext.UserOptions.ShowWeekNumbers = true;
						flag = true;
					}
				}
				else
				{
					this.userContext.UserOptions.ShowWeekNumbers = false;
					flag = true;
				}
				int workDays;
				if (int.TryParse(Utilities.GetFormParameter(this.request, "hidShWk", false), out workDays))
				{
					this.userContext.WorkingHours.WorkDays = workDays;
					flag = true;
				}
				if (!string.IsNullOrEmpty(Utilities.GetFormParameter(this.request, "hidChTz", false)))
				{
					this.userContext.WorkingHours.TimeZone = this.userContext.TimeZone;
					this.showHomeTimeZone = true;
					flag = true;
				}
				if (flag)
				{
					try
					{
						this.userContext.UserOptions.CommitChanges();
						base.SetSavedSuccessfully(this.userContext.WorkingHours.CommitChanges(this.userContext.MailboxSession));
						this.Load();
					}
					catch (StoragePermanentException)
					{
						base.SetSavedSuccessfully(false);
					}
					catch (StorageTransientException)
					{
						base.SetSavedSuccessfully(false);
					}
				}
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00007F98 File Offset: 0x00006198
		public override void Render()
		{
			this.RenderCalendarOptions();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00007FA0 File Offset: 0x000061A0
		public override void RenderScript()
		{
			base.RenderJSVariable("a_iStTm", this.startTimeSelection.ToString());
			base.RenderJSVariable("a_iNdTm", this.endTimeSelection.ToString());
			string varName = "a_iFstDy";
			int num = (int)this.firstDaySelection;
			base.RenderJSVariable(varName, num.ToString());
			base.RenderJSVariable("a_fWkNm", this.showWeekNumbers.ToString().ToLowerInvariant());
			base.RenderJSVariable("a_fEndBfrSt", "false");
			base.RenderJSVariable("a_iShWk", this.userContext.WorkingHours.WorkDays.ToString());
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00008040 File Offset: 0x00006240
		private void RenderCalendarOptions()
		{
			base.RenderHeaderRow(ThemeFileId.Calendar, 719565154);
			this.writer.Write("<tr><td class=\"bd\">");
			this.RenderShowWeekNumbers();
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td class=\"bd\">");
			this.writer.Write("<table class=\"fmt\">");
			this.writer.Write("<tr>");
			this.writer.Write("<td nowrap class=\"lco\">");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(198361899));
			this.writer.Write("</td>");
			this.writer.Write("<td class=\"w100\">");
			this.RenderFirstDaySelection();
			this.writer.Write("</td>");
			this.writer.Write("</tr>");
			this.writer.Write("</table>");
			this.writer.Write("</td></tr>");
			base.RenderHeaderRow(ThemeFileId.Calendar, 579227171);
			this.writer.Write("<tr><td class=\"bd\">");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(-998002579));
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td class=\"bd\">");
			this.writer.Write("<table class=\"fmt\"><tr>");
			this.RenderWorkdayCheckbox("chkSun", DayOfWeek.Sunday);
			this.RenderWorkdayCheckbox("chkMon", DayOfWeek.Monday);
			this.RenderWorkdayCheckbox("chkTue", DayOfWeek.Tuesday);
			this.RenderWorkdayCheckbox("chkWed", DayOfWeek.Wednesday);
			this.RenderWorkdayCheckbox("chkThu", DayOfWeek.Thursday);
			this.RenderWorkdayCheckbox("chkFri", DayOfWeek.Friday);
			this.RenderWorkdayCheckbox("chkSat", DayOfWeek.Saturday);
			this.writer.Write("<td class=\"w100\"><input type=hidden name=");
			this.writer.Write("hidShWk");
			this.writer.Write(" value=");
			this.writer.Write(this.userContext.WorkingHours.WorkDays);
			this.writer.Write("></td></tr></table></td></tr>");
			this.writer.Write("<tr><td class=\"bd\">");
			this.writer.Write("<table class=\"fmt\">");
			this.writer.Write("<tr>");
			this.writer.Write("<td nowrap  class=\"lco\">");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(-1164187699));
			this.writer.Write("</td>");
			this.writer.Write("<td>");
			this.RenderDayTimeSelection(true);
			this.writer.Write("</td>");
			if (this.isTimeZoneDifferent || this.showHomeTimeZone)
			{
				this.writer.Write("<td id=\"tdSTHmTZ\" class=\"gryTZ\" nowrap>");
				this.writer.Write(string.Format(LocalizedStrings.GetHtmlEncoded(-1277540414), this.homeTimeZoneString));
				this.writer.Write("</td>");
			}
			if (this.isTimeZoneDifferent)
			{
				this.writer.Write("<td id=\"tdSTCurTZ\" class=\"gryTZ\" nowrap style=\"display:none\">");
				this.writer.Write(string.Format(LocalizedStrings.GetHtmlEncoded(-1277540414), "<b>" + this.currentTimeZoneString + "</b>"));
				this.writer.Write("</td>");
			}
			this.writer.Write("<td class=\"w100\">&nbsp;</td>");
			this.writer.Write("</tr>");
			this.writer.Write("<tr>");
			this.writer.Write("<td nowrap class=\"lco\">");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(1913350172));
			this.writer.Write("</td>");
			this.writer.Write("<td>");
			this.RenderDayTimeSelection(false);
			this.writer.Write("</td>");
			if (this.isTimeZoneDifferent || this.showHomeTimeZone)
			{
				this.writer.Write("<td id=\"tdETHmTZ\" class=\"gryTZ\" nowrap>");
				this.writer.Write(string.Format(LocalizedStrings.GetHtmlEncoded(-1277540414), this.homeTimeZoneString));
				this.writer.Write("</td>");
			}
			if (this.isTimeZoneDifferent)
			{
				this.writer.Write("<td id=\"tdETCurTZ\" class=\"gryTZ\" nowrap style=\"display:none\">");
				this.writer.Write(string.Format(LocalizedStrings.GetHtmlEncoded(-1277540414), this.currentTimeZoneString));
				this.writer.Write("</td>");
			}
			this.writer.Write("<td class=\"w100\">&nbsp;</td>");
			this.writer.Write("</tr>");
			this.writer.Write("</table>");
			this.writer.Write("</td></tr>");
			if (this.isTimeZoneDifferent)
			{
				this.writer.Write("<tr><td class=\"bd\">");
				this.writer.Write("<table id=tblWrnTZ class=\"fmt\">");
				this.writer.Write("<tr><td class=\"tz\">");
				this.writer.Write(string.Format(LocalizedStrings.GetHtmlEncoded(1761372987), "<b>" + this.homeTimeZoneString + "</b>", "<b>" + this.currentTimeZoneString + "</b>"));
				this.writer.Write("</td></tr>");
				this.writer.Write("<tr><td class=\"tz\">");
				this.writer.Write(string.Format(LocalizedStrings.GetHtmlEncoded(1195875617), "<b>" + this.currentTimeZoneString + "</b>"));
				this.writer.Write("</td></tr>");
				this.writer.Write("<tr><td class=\"ctz\">");
				this.writer.Write("<a href=# onclick=\"onClkChTZ()\">");
				this.writer.Write(string.Format(LocalizedStrings.GetHtmlEncoded(-1229601844), "<b>" + this.currentTimeZoneString + "</b>"));
				this.writer.Write("</a>");
				this.writer.Write("</td></tr>");
				this.writer.Write("</table>");
				this.writer.Write("<input type=hidden name=");
				this.writer.Write("hidChTz");
				this.writer.Write(" value=\"\">");
				this.writer.Write("</td></tr>");
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00008680 File Offset: 0x00006880
		private void RenderDayTimeSelection(bool isStartTime)
		{
			int num = 0;
			int num2 = 1410;
			string arg = isStartTime ? "selStTm" : "selEndTm";
			int num3 = isStartTime ? this.startTimeSelection : this.endTimeSelection;
			int num4 = 1439;
			if (num3 > num4)
			{
				num3 = num4;
			}
			this.writer.Write("<select name=\"{0}\"{1}{2}>", arg, " class=\"co padLR\"", " onchange=\"return onChgSel(this);\"");
			if (!isStartTime)
			{
				num = 30;
			}
			bool flag = false;
			for (int i = num; i <= num2; i += 30)
			{
				if (!flag)
				{
					if (i == num3)
					{
						flag = true;
					}
					else if (i > num3)
					{
						this.RenderDayTimeSelectionOption(num3, num3);
						flag = true;
					}
				}
				this.RenderDayTimeSelectionOption(num3, i);
			}
			if (!flag)
			{
				this.RenderDayTimeSelectionOption(num3, num3);
			}
			if (!isStartTime && num3 != num4)
			{
				this.RenderDayTimeSelectionOption(num3, num4);
			}
			this.writer.Write("</select>");
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00008750 File Offset: 0x00006950
		private void RenderDayTimeSelectionOption(int optionSelection, int minutes)
		{
			ExDateTime exDateTime = ExDateTime.UtcNow.Date + TimeSpan.FromMinutes((double)minutes);
			this.writer.Write("<option value=\"{1}\"{0}>{2}</option>", (minutes == optionSelection) ? " selected" : string.Empty, minutes, exDateTime.ToString(this.userContext.UserOptions.TimeFormat, this.dateTimeFormat));
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000087BC File Offset: 0x000069BC
		private void RenderShowWeekNumbers()
		{
			string format = "<input name=\"{0}\" id=\"{0}\" type=\"checkbox\" onclick=\"return onClkChkBx(this);\" value=\"\"{1}><label for=\"{0}\">{2}</label>";
			this.writer.Write(format, "chkWkNm", this.showWeekNumbers ? " checked" : string.Empty, LocalizedStrings.GetHtmlEncoded(-1211135725));
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00008800 File Offset: 0x00006A00
		private void RenderFirstDaySelection()
		{
			DayOfWeek firstDayOfWeek = this.dateTimeFormat.FirstDayOfWeek;
			string[] dayNames = this.dateTimeFormat.DayNames;
			this.writer.Write("<select name=\"{0}\"{1}{2}>", "selFstDy", " class=\"co padLR\"", " onchange=\"return onChgSel(this);\"");
			for (int i = (int)firstDayOfWeek; i <= 6; i++)
			{
				this.writer.Write("<option value=\"{1}\"{0}>{2}</option>", (this.userContext.UserOptions.WeekStartDay == (DayOfWeek)i) ? " selected" : string.Empty, i, dayNames[i]);
			}
			for (int j = 0; j < (int)firstDayOfWeek; j++)
			{
				this.writer.Write("<option value=\"{1}\"{0}>{2}</option>", (this.userContext.UserOptions.WeekStartDay == (DayOfWeek)j) ? " selected" : string.Empty, j, dayNames[j]);
			}
			this.writer.Write("</select>");
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000088DC File Offset: 0x00006ADC
		protected void RenderWorkdayCheckbox(string id, DayOfWeek day)
		{
			this.writer.Write("<td class=\"wd\" nowrap><input type=\"checkbox\" id=\"");
			this.writer.Write(id);
			this.writer.Write("\" name=\"");
			this.writer.Write(id);
			this.writer.Write("\"");
			if (this.userContext.WorkingHours.IsWorkDay(day))
			{
				this.writer.Write(" checked");
			}
			this.writer.Write(" onclick=\"onClkWkd();\"><label for=\"");
			this.writer.Write(id);
			this.writer.Write("\">");
			this.writer.Write(this.dateTimeFormat.AbbreviatedDayNames[(int)day]);
			this.writer.Write("</label></td>");
		}

		// Token: 0x04000093 RID: 147
		private const string Option = "<option value=\"{1}\"{0}>{2}</option>";

		// Token: 0x04000094 RID: 148
		private const string SelectText = "<select name=\"{0}\"{1}{2}>";

		// Token: 0x04000095 RID: 149
		private const string FormStartTimeSelection = "selStTm";

		// Token: 0x04000096 RID: 150
		private const string FormEndTimeSelection = "selEndTm";

		// Token: 0x04000097 RID: 151
		private const string FormFirstDaySelection = "selFstDy";

		// Token: 0x04000098 RID: 152
		private const string FormShowWeekNumbers = "chkWkNm";

		// Token: 0x04000099 RID: 153
		private const string FormShowWeekDayAs = "hidShWk";

		// Token: 0x0400009A RID: 154
		private const string FormChangeTimeZone = "hidChTz";

		// Token: 0x0400009B RID: 155
		private const string FormJavaScriptStartTimeSelection = "a_iStTm";

		// Token: 0x0400009C RID: 156
		private const string FormJavaScriptEndTimeSelection = "a_iNdTm";

		// Token: 0x0400009D RID: 157
		private const string FormJavaScriptFirstDaySelection = "a_iFstDy";

		// Token: 0x0400009E RID: 158
		private const string FormJavaScriptShowWeekNumbers = "a_fWkNm";

		// Token: 0x0400009F RID: 159
		private const string FormJavaScriptEndBeforeStartTime = "a_fEndBfrSt";

		// Token: 0x040000A0 RID: 160
		private const string FormJavaScriptShowWeekDayAs = "a_iShWk";

		// Token: 0x040000A1 RID: 161
		private int startTimeSelection;

		// Token: 0x040000A2 RID: 162
		private int endTimeSelection;

		// Token: 0x040000A3 RID: 163
		private DayOfWeek firstDaySelection;

		// Token: 0x040000A4 RID: 164
		private bool showWeekNumbers;

		// Token: 0x040000A5 RID: 165
		private bool isTimeZoneDifferent;

		// Token: 0x040000A6 RID: 166
		private bool showHomeTimeZone;

		// Token: 0x040000A7 RID: 167
		private string homeTimeZoneString;

		// Token: 0x040000A8 RID: 168
		private string currentTimeZoneString;

		// Token: 0x040000A9 RID: 169
		private DateTimeFormatInfo dateTimeFormat;
	}
}
