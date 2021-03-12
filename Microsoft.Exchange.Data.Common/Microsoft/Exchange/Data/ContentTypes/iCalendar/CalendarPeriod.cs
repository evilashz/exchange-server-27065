using System;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Internal;

namespace Microsoft.Exchange.Data.ContentTypes.iCalendar
{
	// Token: 0x020000D8 RID: 216
	public struct CalendarPeriod
	{
		// Token: 0x06000873 RID: 2163 RVA: 0x0002E97B File Offset: 0x0002CB7B
		public CalendarPeriod(DateTime start, DateTime end)
		{
			this.start = start;
			this.end = end;
			this.duration = start - end;
			this.isExplicitPeriod = true;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0002E99F File Offset: 0x0002CB9F
		public CalendarPeriod(DateTime start, TimeSpan duration)
		{
			this.start = start;
			this.end = start + duration;
			this.duration = duration;
			this.isExplicitPeriod = false;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0002E9C4 File Offset: 0x0002CBC4
		internal CalendarPeriod(string s, ComplianceTracker tracker)
		{
			int num = s.IndexOf('/');
			if (num <= 0 || s.Length - 1 == num)
			{
				tracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidTimeFormat);
				this.start = CalendarCommon.MinDateTime;
				this.end = CalendarCommon.MinDateTime;
				this.duration = TimeSpan.Zero;
				this.isExplicitPeriod = false;
				return;
			}
			DateTime dateTime = CalendarCommon.ParseDateTime(s.Substring(0, num), tracker);
			char c = s[num + 1];
			if (c == '+' || c == '-' || c == 'P')
			{
				TimeSpan t = CalendarCommon.ParseDuration(s.Substring(num + 1), tracker);
				this.start = dateTime;
				this.end = dateTime + t;
				this.duration = t;
				this.isExplicitPeriod = false;
				return;
			}
			DateTime d = CalendarCommon.ParseDateTime(s.Substring(num + 1), tracker);
			this.start = dateTime;
			this.end = d;
			this.duration = dateTime - d;
			this.isExplicitPeriod = true;
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x0002EAB2 File Offset: 0x0002CCB2
		// (set) Token: 0x06000877 RID: 2167 RVA: 0x0002EABA File Offset: 0x0002CCBA
		public DateTime Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0002EAC3 File Offset: 0x0002CCC3
		// (set) Token: 0x06000879 RID: 2169 RVA: 0x0002EACB File Offset: 0x0002CCCB
		public DateTime End
		{
			get
			{
				return this.end;
			}
			set
			{
				this.end = value;
				this.isExplicitPeriod = true;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x0002EADB File Offset: 0x0002CCDB
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x0002EAE3 File Offset: 0x0002CCE3
		public TimeSpan Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
				this.duration = value;
				this.isExplicitPeriod = false;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x0002EAF3 File Offset: 0x0002CCF3
		public bool IsExplicit
		{
			get
			{
				return this.isExplicitPeriod;
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0002EAFC File Offset: 0x0002CCFC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(CalendarCommon.FormatDateTime(this.start));
			stringBuilder.Append('/');
			if (this.isExplicitPeriod)
			{
				stringBuilder.Append(CalendarCommon.FormatDateTime(this.end));
			}
			else
			{
				stringBuilder.Append(CalendarCommon.FormatDuration(this.duration));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000730 RID: 1840
		private DateTime start;

		// Token: 0x04000731 RID: 1841
		private DateTime end;

		// Token: 0x04000732 RID: 1842
		private TimeSpan duration;

		// Token: 0x04000733 RID: 1843
		private bool isExplicitPeriod;
	}
}
