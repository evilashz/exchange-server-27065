using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x0200009C RID: 156
	public class EditRecurrence : OwaForm, IRegistryOnlyForm
	{
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0002A64B File Offset: 0x0002884B
		protected string FolderId
		{
			get
			{
				if (!string.IsNullOrEmpty(this.folderId))
				{
					return this.folderId;
				}
				return string.Empty;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0002A666 File Offset: 0x00028866
		protected string CalendarItemId
		{
			get
			{
				if (!string.IsNullOrEmpty(this.calendarItemId))
				{
					return this.calendarItemId;
				}
				return string.Empty;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0002A681 File Offset: 0x00028881
		protected bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0002A689 File Offset: 0x00028889
		protected bool IsCalendarItemDirty
		{
			get
			{
				return this.isCalendarItemDirty;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0002A691 File Offset: 0x00028891
		protected bool DoConfirmPatternChange
		{
			get
			{
				return this.doConfirmPatternChange;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0002A699 File Offset: 0x00028899
		protected string Subject
		{
			get
			{
				if (this.calendarItemData == null || string.IsNullOrEmpty(this.calendarItemData.Subject))
				{
					return LocalizedStrings.GetNonEncoded(-776227687);
				}
				return this.calendarItemData.Subject;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0002A6CB File Offset: 0x000288CB
		protected string Location
		{
			get
			{
				if (this.calendarItemData == null || string.IsNullOrEmpty(this.calendarItemData.Location))
				{
					return string.Empty;
				}
				return this.calendarItemData.Location;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0002A6F8 File Offset: 0x000288F8
		protected OwaRecurrenceType OwaRecurrenceType
		{
			get
			{
				return this.recurrenceType;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0002A700 File Offset: 0x00028900
		protected OwaRecurrenceType CoreRecurrenceType
		{
			get
			{
				return this.recurrenceType & OwaRecurrenceType.CoreTypeMask;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0002A710 File Offset: 0x00028910
		protected int RecurrenceInterval
		{
			get
			{
				if (this.calendarItemData.Recurrence.Pattern is WeeklyRecurrencePattern)
				{
					WeeklyRecurrencePattern weeklyRecurrencePattern = (WeeklyRecurrencePattern)this.calendarItemData.Recurrence.Pattern;
					return weeklyRecurrencePattern.RecurrenceInterval;
				}
				if (this.calendarItemData.Recurrence.Pattern is DailyRecurrencePattern)
				{
					DailyRecurrencePattern dailyRecurrencePattern = (DailyRecurrencePattern)this.calendarItemData.Recurrence.Pattern;
					return dailyRecurrencePattern.RecurrenceInterval;
				}
				if (this.calendarItemData.Recurrence.Pattern is MonthlyRecurrencePattern)
				{
					MonthlyRecurrencePattern monthlyRecurrencePattern = (MonthlyRecurrencePattern)this.calendarItemData.Recurrence.Pattern;
					return monthlyRecurrencePattern.RecurrenceInterval;
				}
				if (this.calendarItemData.Recurrence.Pattern is MonthlyThRecurrencePattern)
				{
					MonthlyThRecurrencePattern monthlyThRecurrencePattern = (MonthlyThRecurrencePattern)this.calendarItemData.Recurrence.Pattern;
					return monthlyThRecurrencePattern.RecurrenceInterval;
				}
				throw new ArgumentException("This type of recurrence doesn't have an Interval.");
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0002A7F8 File Offset: 0x000289F8
		protected string RecurrenceIntervalString
		{
			get
			{
				return this.RecurrenceInterval.ToString(CultureInfo.CurrentCulture.NumberFormat);
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0002A820 File Offset: 0x00028A20
		protected int NumberOccurrences
		{
			get
			{
				if (this.calendarItemData.Recurrence.Range is NumberedRecurrenceRange)
				{
					NumberedRecurrenceRange numberedRecurrenceRange = (NumberedRecurrenceRange)this.calendarItemData.Recurrence.Range;
					return numberedRecurrenceRange.NumberOfOccurrences;
				}
				return 10;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0002A864 File Offset: 0x00028A64
		protected string NumberOccurrencesString
		{
			get
			{
				int numberOccurrences = this.NumberOccurrences;
				if (numberOccurrences != -1)
				{
					return numberOccurrences.ToString(CultureInfo.CurrentCulture.NumberFormat);
				}
				return string.Empty;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0002A894 File Offset: 0x00028A94
		protected ExDateTime EndRangeDateTime
		{
			get
			{
				ExDateTime result = ExDateTime.MinValue;
				if (this.calendarItemData.Recurrence.Range is EndDateRecurrenceRange)
				{
					result = ((EndDateRecurrenceRange)this.calendarItemData.Recurrence.Range).EndDate;
				}
				else
				{
					result = this.calendarItemData.StartTime.Date.AddMonths(2);
				}
				return result;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0002A8F9 File Offset: 0x00028AF9
		protected bool IsRangeNoEndDate
		{
			get
			{
				return this.calendarItemData.Recurrence.Range is NoEndRecurrenceRange;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0002A913 File Offset: 0x00028B13
		protected bool IsRangeNumbered
		{
			get
			{
				return this.calendarItemData.Recurrence.Range is NumberedRecurrenceRange;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x0002A92D File Offset: 0x00028B2D
		protected bool IsRangeEndDate
		{
			get
			{
				return this.calendarItemData.Recurrence.Range is EndDateRecurrenceRange;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0002A948 File Offset: 0x00028B48
		protected int DurationMinutes
		{
			get
			{
				return (int)(this.calendarItemData.EndTime - this.calendarItemData.StartTime).TotalMinutes;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0002A97C File Offset: 0x00028B7C
		protected int InDayDurationMinutes
		{
			get
			{
				ExDateTime dt = new ExDateTime(base.UserContext.TimeZone, this.calendarItemData.StartTime.Year, this.calendarItemData.StartTime.Month, this.calendarItemData.StartTime.Day, this.calendarItemData.EndTime.Hour, this.calendarItemData.EndTime.Minute, this.calendarItemData.EndTime.Second);
				return (int)(dt - this.calendarItemData.StartTime).TotalMinutes;
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0002AA2C File Offset: 0x00028C2C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			CalendarItemData calendarItemData = base.OwaContext.PreFormActionData as CalendarItemData;
			if (calendarItemData != null)
			{
				this.calendarItemData = new CalendarItemData(calendarItemData);
				if (this.calendarItemData.Recurrence == null)
				{
					this.calendarItemData.Recurrence = CalendarUtilities.CreateDefaultRecurrence(this.calendarItemData.StartTime);
				}
				if (this.calendarItemData.FolderId != null)
				{
					this.folderId = this.calendarItemData.FolderId.ToBase64String();
				}
				if (this.calendarItemData.Id != null)
				{
					this.calendarItemId = this.calendarItemData.Id.ToBase64String();
				}
				if (!string.IsNullOrEmpty(Utilities.GetQueryStringParameter(base.Request, "d", false)))
				{
					this.isDirty = true;
				}
				if (!string.IsNullOrEmpty(Utilities.GetQueryStringParameter(base.Request, "cd", false)))
				{
					this.isCalendarItemDirty = true;
				}
				if (!string.IsNullOrEmpty(Utilities.GetQueryStringParameter(base.Request, "pcp", false)))
				{
					this.doConfirmPatternChange = true;
				}
				this.toolbar = new Toolbar(base.Response.Output, true);
				this.recurrenceType = CalendarUtilities.MapRecurrenceType(this.calendarItemData.Recurrence);
				CalendarModuleViewState calendarModuleViewState = base.UserContext.LastClientViewState as CalendarModuleViewState;
				if (calendarModuleViewState != null)
				{
					this.lastAccessedYear = calendarModuleViewState.DateTime.Year;
				}
				return;
			}
			throw new ArgumentException("No calendar item data passed in context, or wrong type.");
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0002AB90 File Offset: 0x00028D90
		public void RenderNavigation()
		{
			Navigation navigation = new Navigation(NavigationModule.Calendar, base.OwaContext, base.Response.Output);
			navigation.Render();
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0002ABBC File Offset: 0x00028DBC
		public void RenderHeaderToolbar()
		{
			this.toolbar.RenderStart();
			this.toolbar.RenderButton(ToolbarButtons.Save);
			this.toolbar.RenderDivider();
			this.toolbar.RenderButton(ToolbarButtons.Cancel);
			this.toolbar.RenderDivider();
			this.toolbar.RenderButton(ToolbarButtons.RemoveRecurrence);
			this.toolbar.RenderFill();
			this.toolbar.RenderButton(ToolbarButtons.CloseImage);
			this.toolbar.RenderEnd();
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0002AC40 File Offset: 0x00028E40
		public void RenderFooterToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, false);
			toolbar.RenderStart();
			toolbar.RenderFill();
			toolbar.RenderEnd();
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0002AC71 File Offset: 0x00028E71
		public void RenderStartTime()
		{
			CalendarUtilities.RenderTimeSelect(base.Response.Output, "sttm", this.calendarItemData.StartTime, base.UserContext.UserOptions.TimeFormat, "onChg();", "padLR");
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0002ACAD File Offset: 0x00028EAD
		public void RenderDuration()
		{
			CalendarUtilities.RenderDurationSelect(base.Response.Output, "drtn", this.DurationMinutes, "onChg();", "padLR");
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0002ACD4 File Offset: 0x00028ED4
		public void RenderRangeStartDateControls(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			CalendarUtilities.RenderDateTimeTable(writer, "selS", this.calendarItemData.Recurrence.Range.StartDate, this.lastAccessedYear, null, string.Empty, "onChg();", string.Empty);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0002AD25 File Offset: 0x00028F25
		public void RenderRangeEndByControls(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			CalendarUtilities.RenderDateTimeTable(writer, "selE", this.EndRangeDateTime, this.lastAccessedYear, null, string.Empty, "onChg();", string.Empty);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0002AD5C File Offset: 0x00028F5C
		protected void RenderDailyRecurrence()
		{
			string arg = "<input type=text onchange=\"onChg(this);\" maxlength=3 size=3 name=\"txtinterval\" value=\"" + this.RecurrenceIntervalString + "\">";
			base.Response.Write(string.Format(LocalizedStrings.GetHtmlEncoded(554874232), arg));
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0002AD9C File Offset: 0x00028F9C
		protected void RenderWeeklyRecurrenceCheckboxes()
		{
			base.Response.Write("<table class=\"cb\" cellpadding=\"0\" cellspacing=\"2\"><tbody><tr>");
			this.RenderWeekDayCheckbox(DayOfWeek.Sunday);
			this.RenderWeekDayCheckbox(DayOfWeek.Monday);
			this.RenderWeekDayCheckbox(DayOfWeek.Tuesday);
			this.RenderWeekDayCheckbox(DayOfWeek.Wednesday);
			base.Response.Write("</tr><tr>");
			this.RenderWeekDayCheckbox(DayOfWeek.Thursday);
			this.RenderWeekDayCheckbox(DayOfWeek.Friday);
			this.RenderWeekDayCheckbox(DayOfWeek.Saturday);
			base.Response.Write("</tr></tbody></table>");
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0002AE0C File Offset: 0x0002900C
		private void RenderWeekDayCheckbox(DayOfWeek dayOfWeek)
		{
			DaysOfWeek daysOfWeek = CalendarUtilities.ConvertDayOfWeekToDaysOfWeek(dayOfWeek);
			int num = (int)daysOfWeek;
			string text = num.ToString(CultureInfo.InvariantCulture);
			string text2 = "chkRcrW" + text;
			string text3 = this.GetIsDaySelected(dayOfWeek) ? " checked" : string.Empty;
			base.Response.Write(string.Concat(new string[]
			{
				"<td class=\"nw\"><input type=checkbox name=",
				text2,
				" id=",
				text2,
				text3,
				" value=",
				text,
				" onclick=\"onChg(this);\"><label for=",
				text2,
				">"
			}));
			base.Response.Write(CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)dayOfWeek]);
			base.Response.Write("</label></td>");
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0002AEE0 File Offset: 0x000290E0
		private bool GetIsDaySelected(DayOfWeek dayOfWeek)
		{
			if (this.calendarItemData.Recurrence.Pattern is WeeklyRecurrencePattern)
			{
				DaysOfWeek daysOfWeek = CalendarUtilities.ConvertDayOfWeekToDaysOfWeek(dayOfWeek);
				WeeklyRecurrencePattern weeklyRecurrencePattern = (WeeklyRecurrencePattern)this.calendarItemData.Recurrence.Pattern;
				return (weeklyRecurrencePattern.DaysOfWeek & daysOfWeek) != DaysOfWeek.None;
			}
			return false;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0002AF34 File Offset: 0x00029134
		protected void RenderMonthlyRecurrence()
		{
			MonthlyRecurrencePattern monthlyRecurrencePattern;
			if (this.calendarItemData.Recurrence.Pattern is MonthlyRecurrencePattern)
			{
				monthlyRecurrencePattern = (MonthlyRecurrencePattern)this.calendarItemData.Recurrence.Pattern;
			}
			else
			{
				monthlyRecurrencePattern = (MonthlyRecurrencePattern)CalendarUtilities.CreateDefaultRecurrencePattern(OwaRecurrenceType.Monthly, this.calendarItemData.StartTime);
			}
			string arg = "<input type=text onchange=\"onChg(this);\" maxlength=2 size=2 name=\"txtRcrMM\" value=\"" + monthlyRecurrencePattern.RecurrenceInterval + "\">";
			string arg2 = CalendarUtilities.BuildDayIndexDropdownList(monthlyRecurrencePattern.DayOfMonth, "txtRcrMD", null, "onChg(this);");
			base.Response.Write(string.Format(LocalizedStrings.GetHtmlEncoded(2088839449), arg2, arg));
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0002AFD8 File Offset: 0x000291D8
		protected void RenderMonthlyThRecurrence()
		{
			MonthlyThRecurrencePattern monthlyThRecurrencePattern;
			if (this.calendarItemData.Recurrence.Pattern is MonthlyThRecurrencePattern)
			{
				monthlyThRecurrencePattern = (MonthlyThRecurrencePattern)this.calendarItemData.Recurrence.Pattern;
			}
			else
			{
				monthlyThRecurrencePattern = (MonthlyThRecurrencePattern)CalendarUtilities.CreateDefaultRecurrencePattern(OwaRecurrenceType.MonthlyTh, this.calendarItemData.StartTime);
			}
			string arg = "<input type=text onchange=\"onChg(this);\" maxlength=2 size=2 name=\"txtRcrMThM\" value=\"" + monthlyThRecurrencePattern.RecurrenceInterval + "\">";
			string arg2 = CalendarUtilities.BuildNthDropdownList((int)monthlyThRecurrencePattern.Order, "selRcrYTI", null, "onChg(this);");
			string arg3 = CalendarUtilities.BuildDayDropdownList(monthlyThRecurrencePattern.DaysOfWeek, "selRcrThD", null, "onChg(this);");
			base.Response.Write(string.Format(LocalizedStrings.GetHtmlEncoded(153081917), arg2, arg3, arg));
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0002B098 File Offset: 0x00029298
		protected void RenderYearlyRecurrence()
		{
			YearlyRecurrencePattern yearlyRecurrencePattern;
			if (this.calendarItemData.Recurrence.Pattern is YearlyRecurrencePattern)
			{
				yearlyRecurrencePattern = (YearlyRecurrencePattern)this.calendarItemData.Recurrence.Pattern;
			}
			else
			{
				yearlyRecurrencePattern = (YearlyRecurrencePattern)CalendarUtilities.CreateDefaultRecurrencePattern(OwaRecurrenceType.Yearly, this.calendarItemData.StartTime);
			}
			string arg = CalendarUtilities.BuildMonthDropdownList(yearlyRecurrencePattern.Month, "selRcrYM", null, "onChg(this);");
			string arg2 = CalendarUtilities.BuildDayIndexDropdownList(yearlyRecurrencePattern.DayOfMonth, "selRcrYD", null, "onChg(this);");
			base.Response.Write(string.Format(LocalizedStrings.GetHtmlEncoded(-642038370), arg, arg2));
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0002B138 File Offset: 0x00029338
		protected void RenderYearlyThRecurrence()
		{
			YearlyThRecurrencePattern yearlyThRecurrencePattern;
			if (this.calendarItemData.Recurrence.Pattern is YearlyThRecurrencePattern)
			{
				yearlyThRecurrencePattern = (YearlyThRecurrencePattern)this.calendarItemData.Recurrence.Pattern;
			}
			else
			{
				yearlyThRecurrencePattern = (YearlyThRecurrencePattern)CalendarUtilities.CreateDefaultRecurrencePattern(OwaRecurrenceType.YearlyTh, this.calendarItemData.StartTime);
			}
			string arg = "&nbsp;&nbsp;&nbsp;" + CalendarUtilities.BuildNthDropdownList((int)yearlyThRecurrencePattern.Order, "selRcrYTI", null, "onChg(this);");
			string arg2 = CalendarUtilities.BuildDayDropdownList(yearlyThRecurrencePattern.DaysOfWeek, "selRcrThD", null, "onChg(this);");
			string arg3 = CalendarUtilities.BuildMonthDropdownList(yearlyThRecurrencePattern.Month, "selRcrYTM", null, "onChg(this);");
			base.Response.Write(string.Format(LocalizedStrings.GetHtmlEncoded(-1599607980), arg, arg2, arg3));
		}

		// Token: 0x0400040A RID: 1034
		public const int DefaultNOccurrences = 10;

		// Token: 0x0400040B RID: 1035
		public const string DirtyQueryParameter = "d";

		// Token: 0x0400040C RID: 1036
		public const string CalendarItemDirtyQueryParameter = "cd";

		// Token: 0x0400040D RID: 1037
		public const string PatternChangePromptQueryParameter = "pcp";

		// Token: 0x0400040E RID: 1038
		private CalendarItemData calendarItemData;

		// Token: 0x0400040F RID: 1039
		private Toolbar toolbar;

		// Token: 0x04000410 RID: 1040
		private string folderId;

		// Token: 0x04000411 RID: 1041
		private string calendarItemId;

		// Token: 0x04000412 RID: 1042
		private bool isDirty;

		// Token: 0x04000413 RID: 1043
		private bool isCalendarItemDirty;

		// Token: 0x04000414 RID: 1044
		private OwaRecurrenceType recurrenceType;

		// Token: 0x04000415 RID: 1045
		private bool doConfirmPatternChange;

		// Token: 0x04000416 RID: 1046
		private int lastAccessedYear = -1;
	}
}
