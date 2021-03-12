using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;

namespace Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions
{
	// Token: 0x02000046 RID: 70
	public class AttendeeData
	{
		// Token: 0x0600016E RID: 366 RVA: 0x00008BD8 File Offset: 0x00006DD8
		public AttendeeData(MeetingAttendeeType attendeeType, string identity, bool excludeConflict, ExDateTime freeBusyStart, ExDateTime freeBusyEnd, string mergedFreeBusy, ExchangeVersionType requestSchemaVersion, AttendeeWorkHours workingHours)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (workingHours == null)
			{
				throw new ArgumentNullException("workingHours");
			}
			if (freeBusyStart.TimeZone != freeBusyEnd.TimeZone)
			{
				throw new ArgumentException("freeBusyStart.TimeZone != freeBusyEnd.TimeZone");
			}
			this.attendeeType = attendeeType;
			this.identity = identity;
			this.excludeConflict = excludeConflict;
			this.freeBusyStart = freeBusyStart;
			this.freeBusyEnd = freeBusyEnd;
			this.mergedFreeBusy = mergedFreeBusy;
			this.workingHours = workingHours;
			this.requestSchemaVersion = requestSchemaVersion;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00008C60 File Offset: 0x00006E60
		private BusyType[] GetMergedFreeBusy(ExDateTime start, int duration)
		{
			int num = (int)((start - this.freeBusyStart).TotalMinutes / 30.0);
			int num2 = duration / 30;
			int num3 = (this.mergedFreeBusy != null) ? this.mergedFreeBusy.Length : 0;
			BusyType[] array = new BusyType[num2];
			for (int i = 0; i < num2; i++)
			{
				int num4 = num + i;
				if (num4 < 0 || num4 >= num3)
				{
					array[i] = BusyType.NoData;
				}
				else
				{
					array[i] = (BusyType)(this.mergedFreeBusy[num4] - '0');
					if (this.requestSchemaVersion < ExchangeVersionType.Exchange2012 && array[i] == BusyType.WorkingElsewhere)
					{
						array[i] = BusyType.NoData;
					}
				}
			}
			return array;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00008D07 File Offset: 0x00006F07
		public BusyType[] GetBusyTypeRange(ExDateTime start, int duration)
		{
			return this.GetMergedFreeBusy(start, duration);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00008D14 File Offset: 0x00006F14
		public BusyType GetBusyType(ExDateTime start, int duration)
		{
			BusyType[] array = this.GetMergedFreeBusy(start, duration);
			BusyType busyType = BusyType.Free;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] > busyType)
				{
					busyType = array[i];
				}
			}
			return busyType;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00008D45 File Offset: 0x00006F45
		public MeetingAttendeeType AttendeeType
		{
			get
			{
				return this.attendeeType;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00008D4D File Offset: 0x00006F4D
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00008D55 File Offset: 0x00006F55
		public bool ExcludeConflict
		{
			get
			{
				return this.excludeConflict;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00008D5D File Offset: 0x00006F5D
		public AttendeeWorkHours WorkingHours
		{
			get
			{
				return this.workingHours;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00008D65 File Offset: 0x00006F65
		public ExDateTime FreeBusyStartTime
		{
			get
			{
				return this.freeBusyStart;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00008D6D File Offset: 0x00006F6D
		public ExDateTime FreeBusyEndTime
		{
			get
			{
				return this.freeBusyEnd;
			}
		}

		// Token: 0x040000F6 RID: 246
		private const byte FreeBusyInterval = 30;

		// Token: 0x040000F7 RID: 247
		private MeetingAttendeeType attendeeType;

		// Token: 0x040000F8 RID: 248
		private string identity;

		// Token: 0x040000F9 RID: 249
		private bool excludeConflict;

		// Token: 0x040000FA RID: 250
		private AttendeeWorkHours workingHours;

		// Token: 0x040000FB RID: 251
		private ExDateTime freeBusyStart;

		// Token: 0x040000FC RID: 252
		private ExDateTime freeBusyEnd;

		// Token: 0x040000FD RID: 253
		private string mergedFreeBusy;

		// Token: 0x040000FE RID: 254
		private ExchangeVersionType requestSchemaVersion;
	}
}
