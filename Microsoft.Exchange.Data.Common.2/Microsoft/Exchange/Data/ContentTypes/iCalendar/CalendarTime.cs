using System;
using System.Globalization;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Internal;

namespace Microsoft.Exchange.Data.ContentTypes.iCalendar
{
	// Token: 0x020000B3 RID: 179
	public struct CalendarTime
	{
		// Token: 0x06000737 RID: 1847 RVA: 0x000287A9 File Offset: 0x000269A9
		public CalendarTime(TimeSpan time, bool isUtc)
		{
			this.time = time;
			this.isUtc = isUtc;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x000287BC File Offset: 0x000269BC
		internal CalendarTime(string s, ComplianceTracker tracker)
		{
			this.isUtc = false;
			if (s.Length != 6 && s.Length != 7)
			{
				tracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidTimeStringLength);
				this.time = TimeSpan.Zero;
				return;
			}
			if (s.Length == 7)
			{
				if (s[6] != 'Z')
				{
					tracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.ExpectedZ);
					this.time = TimeSpan.Zero;
					return;
				}
				this.isUtc = true;
				s = s.Substring(0, 6);
			}
			DateTime dateTime;
			if (!DateTime.TryParseExact(s, "HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
			{
				tracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidTimeFormat);
				this.time = TimeSpan.Zero;
				return;
			}
			this.time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00028890 File Offset: 0x00026A90
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x00028898 File Offset: 0x00026A98
		public TimeSpan Time
		{
			get
			{
				return this.time;
			}
			set
			{
				this.time = value;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x000288A1 File Offset: 0x00026AA1
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x000288A9 File Offset: 0x00026AA9
		public bool IsUtc
		{
			get
			{
				return this.isUtc;
			}
			set
			{
				this.isUtc = value;
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x000288B4 File Offset: 0x00026AB4
		public override string ToString()
		{
			return new DateTime(1, 1, 1, this.time.Hours, this.time.Minutes, this.time.Seconds).ToString(this.isUtc ? "HHmmss\\Z" : "HHmmss");
		}

		// Token: 0x040005DE RID: 1502
		private const string TimeFormatUtc = "HHmmss\\Z";

		// Token: 0x040005DF RID: 1503
		private const string TimeFormat = "HHmmss";

		// Token: 0x040005E0 RID: 1504
		private TimeSpan time;

		// Token: 0x040005E1 RID: 1505
		private bool isUtc;
	}
}
