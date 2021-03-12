using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000346 RID: 838
	internal class DatePicker : DatePickerBase
	{
		// Token: 0x06001FAE RID: 8110 RVA: 0x000B6F0C File Offset: 0x000B510C
		public DatePicker(ExDateTime month) : this(string.Empty, null, month, 0, null)
		{
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x000B6F1D File Offset: 0x000B511D
		public DatePicker(string id, params ExDateTime[] selectedDates) : this(id, selectedDates, ExDateTime.MinValue, 0, null)
		{
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x000B6F2E File Offset: 0x000B512E
		public DatePicker(string id, WorkingHours workingHours, params ExDateTime[] selectedDates) : this(id, selectedDates, ExDateTime.MinValue, 0, workingHours)
		{
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x000B6F3F File Offset: 0x000B513F
		public DatePicker(string id, ExDateTime[] selectedDates, int features) : this(id, selectedDates, ExDateTime.MinValue, features, null)
		{
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x000B6F50 File Offset: 0x000B5150
		public DatePicker(string id, ExDateTime month, int features) : this(id, null, month, features, null)
		{
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x000B6F60 File Offset: 0x000B5160
		private DatePicker(string id, ExDateTime[] selectedDates, ExDateTime month, int features, WorkingHours workingHours)
		{
			if (month == ExDateTime.MinValue)
			{
				month = selectedDates[0];
			}
			this.id = id;
			this.selectedDates = selectedDates;
			this.month = month;
			this.features = features;
			this.sessionContext = OwaContext.Current.SessionContext;
			if (workingHours == null)
			{
				this.workingHours = this.sessionContext.WorkingHours;
			}
			else
			{
				this.workingHours = workingHours;
			}
			this.InitializeDates();
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x000B6FE0 File Offset: 0x000B51E0
		public void SetBusyTypeData(BusyType[] busyType)
		{
			if (busyType == null)
			{
				throw new ArgumentNullException("busyType");
			}
			if (busyType.Length != 42)
			{
				throw new ArgumentException("The number of entries for busy type must be equal to the number of days in the date picker: " + 42);
			}
			this.busyType = busyType;
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x000B7015 File Offset: 0x000B5215
		public ExDateTime StartDate
		{
			get
			{
				return this.startDate;
			}
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x000B7020 File Offset: 0x000B5220
		private void RenderMonthListItem(DateTime date, Calendar calendar, TextWriter output, bool selected, bool onlyYear)
		{
			output.Write("<div m=\"");
			output.Write(calendar.GetMonth(date));
			output.Write("\" y=\"");
			output.Write(calendar.GetYear(date));
			output.Write("\" class=\"dpMnuItm\"");
			if (selected)
			{
				output.Write(" id=\"sm\" ");
			}
			output.Write("><span class=\"nowrap\">");
			if (onlyYear)
			{
				output.Write(date.ToString("yyyy"));
			}
			else
			{
				output.Write(date.ToString(this.sessionContext.UserCulture.DateTimeFormat.YearMonthPattern));
			}
			output.Write("</span></div>");
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x000B70C8 File Offset: 0x000B52C8
		private void InitializeDates()
		{
			Calendar calendar = new GregorianCalendar();
			int weekStartDay = (int)this.sessionContext.WeekStartDay;
			this.firstDayInCurrentMonth = new DateTime(this.month.Year, this.month.Month, 1, 0, 0, 0, 0, calendar);
			int dayOfWeek = (int)calendar.GetDayOfWeek(this.firstDayInCurrentMonth);
			this.offset = dayOfWeek - weekStartDay;
			this.offset = ((this.offset < 0) ? (7 + this.offset) : this.offset);
			this.indexMonthStart = this.offset;
			this.startDate = new ExDateTime(this.sessionContext.TimeZone, calendar.AddDays(this.firstDayInCurrentMonth, -this.offset));
			int daysInMonth = calendar.GetDaysInMonth(calendar.GetYear(this.firstDayInCurrentMonth), calendar.GetMonth(this.firstDayInCurrentMonth));
			this.indexMonthEnd = this.offset + daysInMonth - 1;
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x000B71A8 File Offset: 0x000B53A8
		private void RenderHeader(TextWriter output, Calendar calendar, DateTime monthLocal)
		{
			DateTime dateTime = calendar.AddMonths(monthLocal, -1);
			DateTime dateTime2 = calendar.AddMonths(monthLocal, 1);
			output.Write("<div id=\"divDpHdr\" class=\"");
			if (Utilities.IsFlagSet(this.features, 16) || Utilities.IsFlagSet(this.features, 2))
			{
				output.Write("dpHdrWP");
			}
			else
			{
				output.Write("dpHdr");
			}
			output.Write("\">");
			DateTime time = this.sessionContext.IsRtl ? dateTime2 : dateTime;
			this.sessionContext.RenderThemeImage(output, ThemeFileId.PreviousArrow, "vaM fltLeft", new object[]
			{
				"id=\"imgPrev\"",
				SanitizedHtmlString.Format("y=\"{0}\"", new object[]
				{
					calendar.GetYear(time)
				}),
				SanitizedHtmlString.Format("m=\"{0}\"", new object[]
				{
					calendar.GetMonth(time)
				})
			});
			DateTime time2 = this.sessionContext.IsRtl ? dateTime : dateTime2;
			this.sessionContext.RenderThemeImage(output, ThemeFileId.NextArrow, "vaM fltRight", new object[]
			{
				"id=\"imgNext\"",
				SanitizedHtmlString.Format("y=\"{0}\"", new object[]
				{
					calendar.GetYear(time2)
				}),
				SanitizedHtmlString.Format("m=\"{0}\"", new object[]
				{
					calendar.GetMonth(time2)
				})
			});
			output.Write("<span tabindex=-1 id=\"spanDate\" class=\"vaM\">");
			output.Write(monthLocal.ToString(this.sessionContext.UserCulture.DateTimeFormat.YearMonthPattern).Replace(',', ' '));
			output.Write("</span><span id=\"spanMonthListDropDown\">");
			this.sessionContext.RenderThemeImage(output, ThemeFileId.DownButton3, "vaM", new object[]
			{
				"id=\"imgDropDwn\""
			});
			output.Write("</span></div>");
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x000B739C File Offset: 0x000B559C
		private void RenderDaysOfWeek(TextWriter output, int weekStartDay)
		{
			string[] oneLetterDayNames = Culture.GetOneLetterDayNames();
			output.Write("<div class=\"");
			if (Utilities.IsFlagSet(this.features, 16) || Utilities.IsFlagSet(this.features, 2))
			{
				output.Write("dpMonthHdrWp");
			}
			else
			{
				output.Write("dpMonthHdr");
			}
			output.Write("\">");
			for (int i = 0; i < 7; i++)
			{
				output.Write("<div class=\"dpDOW\">");
				output.Write(oneLetterDayNames[(weekStartDay + i) % 7]);
				output.Write("</div>");
			}
			output.Write("</div>");
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x000B7434 File Offset: 0x000B5634
		private void RenderTodayButtons(TextWriter output, Calendar calendar, DateTime today)
		{
			if (Utilities.IsFlagSet(this.features, 4) || Utilities.IsFlagSet(this.features, 8))
			{
				output.Write("<div class=\"btnDay\">");
				bool flag = Utilities.IsFlagSet(this.features, 4) && Utilities.IsFlagSet(this.features, 8);
				if (Utilities.IsFlagSet(this.features, 4))
				{
					output.Write("<div id=\"divToday\"");
					output.Write(" y=");
					output.Write(calendar.GetYear(today));
					output.Write(" m=");
					output.Write(calendar.GetMonth(today));
					output.Write(" d=");
					output.Write(calendar.GetDayOfMonth(today));
					output.Write(" class=\"dpBtn ");
					if (!flag)
					{
						output.Write("dpOneBtn");
					}
					else
					{
						output.Write("dpTodayBtn");
					}
					output.Write("\">");
					output.Write("<span class=\"dpTodayLh\">");
					output.Write(SanitizedHtmlString.FromStringId(-367521373));
					output.Write("</span>");
					output.Write("</div>");
				}
				if (Utilities.IsFlagSet(this.features, 8))
				{
					output.Write("<div id=\"divNone\" class=\"dpBtn ");
					if (!flag)
					{
						output.Write("dpOneBtn");
					}
					else
					{
						output.Write("dpNoneBtn");
					}
					output.Write("\">");
					output.Write("<span class=\"vaM\">");
					output.Write(SanitizedHtmlString.FromStringId(1414246128));
					output.Write("</span>");
					output.Write("</div>");
				}
				output.Write("</div>");
			}
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x000B75CC File Offset: 0x000B57CC
		public void Render(TextWriter output)
		{
			output.Write("<div class=\"");
			output.Write(Utilities.IsFlagSet(this.features, 32) ? "dpDd" : "dp");
			output.Write(" ");
			if (Utilities.IsFlagSet(this.features, 2) || Utilities.IsFlagSet(this.features, 16))
			{
				output.Write("dpWp\" id=\"");
			}
			else
			{
				output.Write("dpNoWp\" id=\"");
			}
			output.Write(this.id);
			output.Write("\"");
			output.Write(" ui=");
			output.Write(this.features);
			output.Write(" ww=");
			uint value = Utilities.RotateRight((uint)this.workingHours.WorkDays, (uint)this.sessionContext.WeekStartDay, 7U);
			output.Write(value);
			this.RenderNamesArray("rgAbrDN", new DatePicker.RenderNames(RenderingUtilities.RenderAbbreviatedDayNames), output);
			this.RenderNamesArray("rgAbrMN", new DatePicker.RenderNames(RenderingUtilities.RenderAbbreviatedMonthNames), output);
			this.RenderNamesArray("rgFulDN", new DatePicker.RenderNames(RenderingUtilities.RenderFullDayNames), output);
			this.RenderNamesArray("rgFulMN", new DatePicker.RenderNames(RenderingUtilities.RenderFullMonthNames), output);
			output.Write(">");
			this.RenderMonth(output);
			output.Write("</div>");
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x000B7720 File Offset: 0x000B5920
		public void RenderMonth(TextWriter output)
		{
			Calendar calendar = new GregorianCalendar();
			int weekStartDay = (int)this.sessionContext.WeekStartDay;
			Utilities.IsFlagSet(this.features, 16);
			this.dates = new DateTime[42];
			DateTime dateTime = (DateTime)DateTimeUtilities.GetLocalTime().Date;
			int num = -this.offset;
			for (int i = 0; i < 42; i++)
			{
				this.dates[i] = calendar.AddDays(this.firstDayInCurrentMonth, num);
				num++;
			}
			DateTime dateTime2 = (DateTime)this.month;
			output.Write("<div id=\"div");
			output.Write(calendar.GetMonth(dateTime2));
			output.Write(calendar.GetYear(dateTime2));
			output.Write("\"");
			output.Write(" y=");
			output.Write(calendar.GetYear(dateTime2));
			output.Write(" m=");
			output.Write(calendar.GetMonth(dateTime2));
			output.Write(" class=\"dpMonth\">");
			this.RenderHeader(output, calendar, dateTime2);
			int num2 = this.dates.Length / 7;
			this.RenderDaysOfWeek(output, weekStartDay);
			int num3 = 0;
			int num4 = calendar.GetMonth(dateTime2);
			int year = calendar.GetYear(dateTime2);
			int num5 = calendar.GetMonth(this.dates[num3]);
			int year2 = calendar.GetYear(this.dates[num3]);
			for (int j = 0; j < num2; j++)
			{
				output.Write("<div id=\"divWeek");
				output.Write(j);
				if (Utilities.IsFlagSet(this.features, 16) || Utilities.IsFlagSet(this.features, 2))
				{
					output.Write("\" class=\"dpWeekWp\">");
				}
				else
				{
					output.Write("\" class=\"dpWeek\">");
				}
				if (Utilities.IsFlagSet(this.features, 16) || Utilities.IsFlagSet(this.features, 2))
				{
					int weekOfYear = calendar.GetWeekOfYear(this.dates[num3], this.sessionContext.FirstWeekOfYear, (DayOfWeek)weekStartDay);
					output.Write("<div");
					output.Write(" w=");
					output.Write(weekOfYear);
					output.Write(" class=\"");
					if (Utilities.IsFlagSet(this.features, 2))
					{
						output.Write("dpWeekNum ");
					}
					if (Utilities.IsFlagSet(this.features, 16))
					{
						output.Write("dpWeekPkr");
					}
					output.Write("\"");
					if (Utilities.IsFlagSet(this.features, 16))
					{
						output.Write(" title=\"");
						output.Write(SanitizedHtmlString.FromStringId(-1811092250));
						output.Write("\">");
					}
					else
					{
						output.Write('>');
					}
					if (Utilities.IsFlagSet(this.features, 2))
					{
						output.Write(weekOfYear);
					}
					else
					{
						output.Write("&nbsp;");
					}
					output.Write("</div>");
				}
				for (int k = 0; k < 7; k++)
				{
					bool flag = this.IsSelected(num3);
					output.Write("<div");
					if (flag)
					{
						output.Write(" id=s");
					}
					if (year2 != year)
					{
						output.Write(" y=");
						output.Write(year2);
					}
					if (num5 != num4)
					{
						output.Write(" m=");
						output.Write(num5);
					}
					output.Write(" class='");
					if (DateTime.Compare(dateTime, this.dates[num3].Date) == 0)
					{
						output.Write(" dpToday");
					}
					output.Write(Utilities.IsFlagSet(this.features, 32) ? " dpDayDdM" : " dpDay");
					if (flag)
					{
						output.Write(" dpSel ");
					}
					if (this.indexMonthStart > num3 || this.indexMonthEnd < num3)
					{
						output.Write(" dpPOrN");
					}
					if (this.busyType != null && this.busyType[num3] == BusyType.Busy)
					{
						output.Write(" b");
					}
					output.Write("'>");
					string text = DateTimeUtilities.GetDaysFormat(this.sessionContext.DateFormat);
					if (text == null)
					{
						text = "%d";
					}
					output.Write("<span class=\"dpDayNum\">");
					output.Write(this.dates[num3].ToString(text));
					output.Write("</span>");
					output.Write("</div>");
					num3++;
					if (num3 < this.dates.Length)
					{
						num5 = calendar.GetMonth(this.dates[num3]);
						year2 = calendar.GetYear(this.dates[num3]);
					}
				}
				output.Write("</div>");
			}
			this.RenderTodayButtons(output, calendar, dateTime);
			this.RenderMonthList(output);
			output.Write("</div>");
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x000B7BE4 File Offset: 0x000B5DE4
		private bool IsSelected(int dayIndex)
		{
			if (this.selectedDates == null)
			{
				return false;
			}
			for (int i = 0; i < this.selectedDates.Length; i++)
			{
				if (DateTime.Compare(((DateTime)this.selectedDates[i]).Date, this.dates[dayIndex]) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x000B7C48 File Offset: 0x000B5E48
		protected void RenderMonthList(TextWriter output)
		{
			Calendar calendar = new GregorianCalendar();
			output.Write("<div id=\"divMnthLst\" style=\"display: none;\">");
			DateTime dateTime = calendar.AddMonths((DateTime)this.month, -3);
			if (dateTime.Year - 2 >= DateTime.MinValue.Year)
			{
				DateTime date = new DateTime(dateTime.Year - 2, 1, dateTime.Day, calendar);
				this.RenderMonthListItem(date, calendar, output, false, true);
			}
			if (dateTime.Year - 1 >= DateTime.MinValue.Year)
			{
				DateTime date = new DateTime(dateTime.Year - 1, 1, dateTime.Day, calendar);
				this.RenderMonthListItem(date, calendar, output, false, true);
			}
			output.Write("<div class=\"dpMnuSep\"></div>");
			for (int i = 0; i < 7; i++)
			{
				this.RenderMonthListItem(dateTime, calendar, output, i == 3, false);
				dateTime = calendar.AddMonths(dateTime, 1);
			}
			dateTime = calendar.AddMonths(dateTime, -1);
			if (dateTime.Year + 1 <= DateTime.MaxValue.Year)
			{
				DateTime date = new DateTime(dateTime.Year + 1, 1, dateTime.Day, calendar);
				output.Write("<div class=\"dpMnuSep\"></div>");
				this.RenderMonthListItem(date, calendar, output, false, true);
			}
			if (dateTime.Year + 2 <= DateTime.MaxValue.Year)
			{
				DateTime date = new DateTime(dateTime.Year + 2, 1, dateTime.Day, calendar);
				this.RenderMonthListItem(date, calendar, output, false, true);
			}
			output.Write("</div>");
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x000B7DBC File Offset: 0x000B5FBC
		private void RenderNamesArray(string variableName, DatePicker.RenderNames valueOutputDelegate, TextWriter writer)
		{
			writer.Write(" ");
			writer.Write(variableName);
			writer.Write("=\"new Array(");
			StringBuilder stringBuilder = new StringBuilder();
			SanitizingStringWriter<OwaHtml> sanitizingStringWriter = new SanitizingStringWriter<OwaHtml>(stringBuilder);
			valueOutputDelegate(sanitizingStringWriter);
			sanitizingStringWriter.Close();
			writer.Write(Utilities.SanitizeHtmlEncode(stringBuilder.ToString()));
			writer.Write(")\"");
		}

		// Token: 0x04001704 RID: 5892
		private string id;

		// Token: 0x04001705 RID: 5893
		private int features;

		// Token: 0x04001706 RID: 5894
		private ExDateTime month;

		// Token: 0x04001707 RID: 5895
		private ExDateTime[] selectedDates;

		// Token: 0x04001708 RID: 5896
		private BusyType[] busyType;

		// Token: 0x04001709 RID: 5897
		private ISessionContext sessionContext;

		// Token: 0x0400170A RID: 5898
		private ExDateTime startDate;

		// Token: 0x0400170B RID: 5899
		private int indexMonthStart;

		// Token: 0x0400170C RID: 5900
		private int indexMonthEnd;

		// Token: 0x0400170D RID: 5901
		private int offset;

		// Token: 0x0400170E RID: 5902
		private DateTime[] dates;

		// Token: 0x0400170F RID: 5903
		private WorkingHours workingHours;

		// Token: 0x04001710 RID: 5904
		private DateTime firstDayInCurrentMonth;

		// Token: 0x02000347 RID: 839
		[Flags]
		public enum Features
		{
			// Token: 0x04001712 RID: 5906
			Month = 0,
			// Token: 0x04001713 RID: 5907
			MultiDaySelection = 1,
			// Token: 0x04001714 RID: 5908
			WeekNumbers = 2,
			// Token: 0x04001715 RID: 5909
			TodayButton = 4,
			// Token: 0x04001716 RID: 5910
			NoneButton = 8,
			// Token: 0x04001717 RID: 5911
			WeekSelector = 16,
			// Token: 0x04001718 RID: 5912
			DropDown = 32
		}

		// Token: 0x02000348 RID: 840
		// (Invoke) Token: 0x06001FC1 RID: 8129
		private delegate void RenderNames(TextWriter output);
	}
}
