using System;
using System.Text;
using System.Xml.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000214 RID: 532
	[Serializable]
	public class RequestJobDurationData
	{
		// Token: 0x06001B6F RID: 7023 RVA: 0x0003A3DF File Offset: 0x000385DF
		public RequestJobDurationData(RequestState state) : this()
		{
			this.state = state;
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x0003A3EE File Offset: 0x000385EE
		public RequestJobDurationData()
		{
			this.minutes = new PerMinuteTimeSlot();
			this.hours = new HourlyTimeSlot();
			this.days = new DailyTimeSlot();
			this.months = new MonthlyTimeSlot();
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x0003A422 File Offset: 0x00038622
		// (set) Token: 0x06001B72 RID: 7026 RVA: 0x0003A42A File Offset: 0x0003862A
		[XmlIgnore]
		public RequestState State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x0003A433 File Offset: 0x00038633
		// (set) Token: 0x06001B74 RID: 7028 RVA: 0x0003A43B File Offset: 0x0003863B
		[XmlAttribute("S")]
		public int StateInt
		{
			get
			{
				return (int)this.state;
			}
			set
			{
				this.state = (RequestState)value;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x0003A444 File Offset: 0x00038644
		// (set) Token: 0x06001B76 RID: 7030 RVA: 0x0003A44C File Offset: 0x0003864C
		[XmlIgnore]
		public TimeSpan Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
				this.duration = value;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x0003A455 File Offset: 0x00038655
		// (set) Token: 0x06001B78 RID: 7032 RVA: 0x0003A462 File Offset: 0x00038662
		[XmlAttribute("D")]
		public long DurationTicks
		{
			get
			{
				return this.duration.Ticks;
			}
			set
			{
				this.duration = new TimeSpan(value);
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x0003A470 File Offset: 0x00038670
		// (set) Token: 0x06001B7A RID: 7034 RVA: 0x0003A47D File Offset: 0x0003867D
		[XmlElement(ElementName = "Minutes")]
		public ulong[] Minutes
		{
			get
			{
				return this.minutes.ToArray();
			}
			set
			{
				this.minutes = new PerMinuteTimeSlot(value);
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x0003A48B File Offset: 0x0003868B
		// (set) Token: 0x06001B7C RID: 7036 RVA: 0x0003A498 File Offset: 0x00038698
		[XmlElement(ElementName = "Hours")]
		public ulong[] Hours
		{
			get
			{
				return this.hours.ToArray();
			}
			set
			{
				this.hours = new HourlyTimeSlot(value);
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x0003A4A6 File Offset: 0x000386A6
		// (set) Token: 0x06001B7E RID: 7038 RVA: 0x0003A4B3 File Offset: 0x000386B3
		[XmlElement(ElementName = "Days")]
		public ulong[] Days
		{
			get
			{
				return this.days.ToArray();
			}
			set
			{
				this.days = new DailyTimeSlot(value);
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x0003A4C1 File Offset: 0x000386C1
		// (set) Token: 0x06001B80 RID: 7040 RVA: 0x0003A4CE File Offset: 0x000386CE
		[XmlElement(ElementName = "Months")]
		public ulong[] Months
		{
			get
			{
				return this.months.ToArray();
			}
			set
			{
				this.months = new MonthlyTimeSlot(value);
			}
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x0003A4DC File Offset: 0x000386DC
		public static RequestJobDurationData operator +(RequestJobDurationData data1, RequestJobDurationData data2)
		{
			if (data1 != null && data2 == null)
			{
				return data1;
			}
			if (data2 != null && data1 == null)
			{
				return data2;
			}
			RequestJobDurationData requestJobDurationData = new RequestJobDurationData();
			requestJobDurationData.Duration = data1.Duration + data2.Duration;
			requestJobDurationData.minutes.PopulateFrom(data1.minutes, data2.minutes);
			requestJobDurationData.hours.PopulateFrom(data1.hours, data2.hours);
			requestJobDurationData.days.PopulateFrom(data1.days, data2.days);
			requestJobDurationData.months.PopulateFrom(data1.months, data2.months);
			return requestJobDurationData;
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x0003A573 File Offset: 0x00038773
		public void Refresh(DateTime lastUpdateTime)
		{
			this.minutes.Refresh(lastUpdateTime);
			this.hours.Refresh(lastUpdateTime);
			this.days.Refresh(lastUpdateTime);
			this.months.Refresh(lastUpdateTime);
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x0003A5A8 File Offset: 0x000387A8
		public void AddTime(TimeSpan duration)
		{
			this.Duration += duration;
			this.minutes.AddTime(duration);
			this.hours.AddTime(duration);
			this.days.AddTime(duration);
			this.months.AddTime(duration);
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x0003A5F8 File Offset: 0x000387F8
		public RequestJobTimeTrackerXML.DurationRec GetDurationRec(RequestState state, bool showTimeSlots = false)
		{
			RequestJobTimeTrackerXML.DurationRec durationRec = new RequestJobTimeTrackerXML.DurationRec();
			durationRec.State = state.ToString();
			durationRec.Duration = this.duration.ToString();
			if (showTimeSlots)
			{
				durationRec.PerMinute = this.minutes.GetDiagnosticXML();
				durationRec.PerHour = this.hours.GetDiagnosticXML();
				durationRec.PerDay = this.days.GetDiagnosticXML();
				durationRec.PerMonth = this.months.GetDiagnosticXML();
			}
			return durationRec;
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x0003A67C File Offset: 0x0003887C
		public override string ToString()
		{
			long ticks = this.Duration.Ticks;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(new TimeSpan(ticks - ticks % 10000000L).ToString());
			if (this.Minutes.Length > 0)
			{
				stringBuilder.AppendFormat(" Minutes:{0}", string.Join("|", new object[]
				{
					this.minutes
				}));
			}
			if (this.Hours.Length > 0)
			{
				stringBuilder.AppendFormat(" Hours:{0}", string.Join("|", new object[]
				{
					this.hours
				}));
			}
			if (this.Days.Length > 0)
			{
				stringBuilder.AppendFormat(" Days:{0}", string.Join("|", new object[]
				{
					this.days
				}));
			}
			if (this.Months.Length > 0)
			{
				stringBuilder.AppendFormat(" Months:{0}", string.Join("|", new object[]
				{
					this.months
				}));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000BBC RID: 3004
		private RequestState state;

		// Token: 0x04000BBD RID: 3005
		private TimeSpan duration;

		// Token: 0x04000BBE RID: 3006
		private TimeSlot minutes;

		// Token: 0x04000BBF RID: 3007
		private TimeSlot hours;

		// Token: 0x04000BC0 RID: 3008
		private TimeSlot days;

		// Token: 0x04000BC1 RID: 3009
		private TimeSlot months;
	}
}
