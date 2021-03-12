using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EDA RID: 3802
	[Serializable]
	public class TimeSlot
	{
		// Token: 0x170022D5 RID: 8917
		// (get) Token: 0x06008338 RID: 33592 RVA: 0x0023AE22 File Offset: 0x00239022
		// (set) Token: 0x06008339 RID: 33593 RVA: 0x0023AE35 File Offset: 0x00239035
		[XmlElement]
		public string Start
		{
			get
			{
				this.ValidateBeforeSerialize();
				return TimeSlot.GetTimeString(this.StartTimeInMinutes);
			}
			set
			{
				this.StartTimeInMinutes = TimeSlot.GetMinutesFromMidnight(value);
			}
		}

		// Token: 0x170022D6 RID: 8918
		// (get) Token: 0x0600833A RID: 33594 RVA: 0x0023AE43 File Offset: 0x00239043
		// (set) Token: 0x0600833B RID: 33595 RVA: 0x0023AE56 File Offset: 0x00239056
		[XmlElement]
		public string End
		{
			get
			{
				this.ValidateBeforeSerialize();
				return TimeSlot.GetTimeString(this.EndTimeInMinutes);
			}
			set
			{
				this.EndTimeInMinutes = TimeSlot.GetMinutesFromMidnight(value);
			}
		}

		// Token: 0x0600833C RID: 33596 RVA: 0x0023AE64 File Offset: 0x00239064
		public TimeSlot()
		{
		}

		// Token: 0x0600833D RID: 33597 RVA: 0x0023AE6C File Offset: 0x0023906C
		internal TimeSlot(int startTimeInMinutes, int endTimeInMinutes)
		{
			this.StartTimeInMinutes = startTimeInMinutes;
			this.EndTimeInMinutes = endTimeInMinutes;
		}

		// Token: 0x0600833E RID: 33598 RVA: 0x0023AE84 File Offset: 0x00239084
		private static string GetTimeString(int minutesFromMidnight)
		{
			return TimeSpan.FromMinutes((double)minutesFromMidnight).ToString();
		}

		// Token: 0x0600833F RID: 33599 RVA: 0x0023AEA8 File Offset: 0x002390A8
		private static int GetMinutesFromMidnight(string timeString)
		{
			TimeSpan timeSpan = TimeSpan.MinValue;
			Exception ex = null;
			try
			{
				timeSpan = TimeSpan.Parse(timeString);
			}
			catch (FormatException ex2)
			{
				ex = ex2;
			}
			catch (ArgumentNullException ex3)
			{
				ex = ex3;
			}
			catch (OverflowException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.WorkHoursTracer.TraceError<string, Exception>(-1L, "Unable to parse time '{0}', exception: {1}", timeString, ex);
				throw new WorkingHoursXmlMalformedException(ServerStrings.InvalidTimeSlot, ex);
			}
			return (int)timeSpan.TotalMinutes;
		}

		// Token: 0x170022D7 RID: 8919
		// (get) Token: 0x06008340 RID: 33600 RVA: 0x0023AF2C File Offset: 0x0023912C
		// (set) Token: 0x06008341 RID: 33601 RVA: 0x0023AF34 File Offset: 0x00239134
		[XmlIgnore]
		internal int StartTimeInMinutes
		{
			get
			{
				return this.startTimeInMinutes;
			}
			set
			{
				if (value < 0 || value > 1440)
				{
					throw new InvalidWorkingHourParameterException(ServerStrings.InvalidWorkingPeriod("StartTimeInMinutes", 0, 1440));
				}
				this.startTimeInMinutes = value;
			}
		}

		// Token: 0x170022D8 RID: 8920
		// (get) Token: 0x06008342 RID: 33602 RVA: 0x0023AF5F File Offset: 0x0023915F
		// (set) Token: 0x06008343 RID: 33603 RVA: 0x0023AF67 File Offset: 0x00239167
		[XmlIgnore]
		internal int EndTimeInMinutes
		{
			get
			{
				return this.endTimeInMinutes;
			}
			set
			{
				if (value < 0 || value > 1440)
				{
					throw new InvalidWorkingHourParameterException(ServerStrings.InvalidWorkingPeriod("EndTimeInMinutes", 0, 1440));
				}
				this.endTimeInMinutes = value;
			}
		}

		// Token: 0x06008344 RID: 33604 RVA: 0x0023AF92 File Offset: 0x00239192
		private void ValidateBeforeSerialize()
		{
			if (this.StartTimeInMinutes > this.EndTimeInMinutes)
			{
				throw new InvalidWorkingHourParameterException(ServerStrings.InvalidTimesInTimeSlot);
			}
		}

		// Token: 0x040057EC RID: 22508
		private int startTimeInMinutes;

		// Token: 0x040057ED RID: 22509
		private int endTimeInMinutes;

		// Token: 0x040057EE RID: 22510
		private static readonly Trace Tracer = Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common.ExTraceGlobals.WorkingHoursTracer;
	}
}
