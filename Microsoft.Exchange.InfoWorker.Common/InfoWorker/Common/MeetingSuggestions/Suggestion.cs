using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions
{
	// Token: 0x0200003F RID: 63
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class Suggestion
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00007E1A File Offset: 0x0000601A
		public Suggestion()
		{
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00007E38 File Offset: 0x00006038
		internal Suggestion(ExDateTime meetingStartTime, int inputMeetingDuration, int inputRequiredAttendeeCount, int inputOptionalAttendeeCount, AttendeeData[] inputAttendees, ConfigOptions inputOptions)
		{
			this.options = inputOptions;
			Suggestion.Tracer.TraceDebug<object, ExDateTime>((long)this.GetHashCode(), "{0}: Suggestion.Suggestion() entered, inputStartDateTime: {1}", TraceContext.Get(), meetingStartTime);
			this.meetingStartTime = meetingStartTime;
			this.startDateTime = meetingStartTime.LocalTime;
			this.meetingDuration = inputMeetingDuration;
			this.requiredAttendeeCount = inputRequiredAttendeeCount;
			this.optionalAttendeeCount = inputOptionalAttendeeCount;
			ExDateTime endUtc = meetingStartTime.AddMinutes((double)inputMeetingDuration);
			bool flag = true;
			int num = 0;
			this.attendeeConflictDataArray = new AttendeeConflictData[inputAttendees.Length];
			foreach (AttendeeData attendeeData in inputAttendees)
			{
				BusyType busyType = attendeeData.GetBusyType(this.meetingStartTime, this.meetingDuration);
				IndividualAttendeeConflictData individualAttendeeConflictData = IndividualAttendeeConflictData.Create(attendeeData, busyType);
				if (attendeeData.AttendeeType == MeetingAttendeeType.Required || attendeeData.AttendeeType == MeetingAttendeeType.Organizer)
				{
					this.SetRequiredAttendeeAvailability(attendeeData, individualAttendeeConflictData);
					if (individualAttendeeConflictData.BusyType != BusyType.NoData)
					{
						this.excludeConflict |= attendeeData.ExcludeConflict;
					}
				}
				else if (attendeeData.AttendeeType == MeetingAttendeeType.Optional)
				{
					this.SetOptionalAttendeeAvailability(attendeeData, individualAttendeeConflictData);
				}
				else if (attendeeData.AttendeeType == MeetingAttendeeType.Room)
				{
					this.SetRoomAttendeeAvailability(attendeeData, individualAttendeeConflictData);
					if (individualAttendeeConflictData.BusyType != BusyType.NoData)
					{
						flag &= attendeeData.ExcludeConflict;
					}
				}
				else if (attendeeData.AttendeeType == MeetingAttendeeType.Resource)
				{
					this.SetResourceAttendeeAvailability(attendeeData, individualAttendeeConflictData);
				}
				else
				{
					Suggestion.Tracer.TraceError<object, MeetingAttendeeType>((long)this.GetHashCode(), "{0}: unknown attendee type: {1}", TraceContext.Get(), attendeeData.AttendeeType);
				}
				if (attendeeData.AttendeeType == MeetingAttendeeType.Organizer)
				{
					this.isWorkTime = attendeeData.WorkingHours.IsWorkTime(this.meetingStartTime, endUtc);
				}
				this.attendeeConflictDataArray[num] = individualAttendeeConflictData;
				num++;
			}
			if (this.roomsRequested)
			{
				this.excludeConflict = (this.excludeConflict || flag);
			}
			Suggestion.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: required: {1}, optional: {2}, worktime?: {3}", new object[]
			{
				TraceContext.Get(),
				this.requiredAttendeeCount,
				this.optionalAttendeeCount,
				this.isWorkTime
			});
			if (this.requiredAttendeeCount != 0)
			{
				this.sumPctReqFrontTime = this.sumReqFrontTime / (double)this.requiredAttendeeCount;
				this.weightedPctReqConflicts = this.weightedReqConflicts / (double)this.requiredAttendeeCount;
			}
			else
			{
				this.weightedPctReqConflicts = 1.0;
			}
			if (this.optionalAttendeeCount != 0)
			{
				this.pctOptConflicts = (double)this.optionalAttendeeConflictCount / (double)this.optionalAttendeeCount;
				this.pctOptOverlap = this.optOverlap / (double)this.optionalAttendeeCount;
			}
			this.timeSlotRating = this.GetRating();
			if (this.weightedPctReqConflicts == 0.0)
			{
				this.bucket = SuggestionQuality.Excellent;
			}
			else if (this.weightedPctReqConflicts * 100.0 >= 50.0)
			{
				this.bucket = SuggestionQuality.Poor;
			}
			else if (this.weightedPctReqConflicts * 100.0 <= (double)this.options.GoodThreshold)
			{
				this.bucket = SuggestionQuality.Good;
			}
			else
			{
				this.bucket = SuggestionQuality.Fair;
			}
			Suggestion.Tracer.TraceDebug<object, long, SuggestionQuality>((long)this.GetHashCode(), "{0}: final suggestion results, timeSlotRating: {1}, bucket: {2}", TraceContext.Get(), this.timeSlotRating, this.bucket);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00008164 File Offset: 0x00006364
		private void SetConvenience(AttendeeData attendee)
		{
			int convenience = attendee.WorkingHours.GetConvenience(this.meetingStartTime, this.meetingDuration);
			if (convenience > this.convenienceMax)
			{
				this.convenienceMax = convenience;
			}
			this.convenienceSum += convenience;
			if (this.convenienceSum > 256)
			{
				this.convenienceSum = 256;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000081C0 File Offset: 0x000063C0
		private double GetConflictValue(BusyType busyType)
		{
			double result = 0.0;
			switch (busyType)
			{
			case BusyType.Tentative:
			case BusyType.NoData:
				result = 0.5;
				break;
			case BusyType.Busy:
			case BusyType.OOF:
				result = 1.0;
				break;
			}
			return result;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00008210 File Offset: 0x00006410
		private void SetRequiredAttendeeAvailability(AttendeeData attendee, IndividualAttendeeConflictData attendeeConflict)
		{
			BusyType busyType = attendeeConflict.BusyType;
			if (busyType == BusyType.NoData)
			{
				this.requiredAttendeeCount--;
				return;
			}
			if (this.strongestReqConflict < busyType)
			{
				this.strongestReqConflict = busyType;
			}
			if (busyType != BusyType.Free)
			{
				this.requiredAttendeeConflictCount++;
			}
			this.SetConvenience(attendee);
			int num = this.meetingDuration / this.options.FreeBusyInterval;
			int num2 = 0;
			int num3 = 0;
			double num4 = 0.0;
			if (num > 0)
			{
				BusyType[] busyTypeRange = attendee.GetBusyTypeRange(this.meetingStartTime, this.meetingDuration);
				ExDateTime startUtc = this.meetingStartTime.ToUtc();
				foreach (BusyType busyType2 in busyTypeRange)
				{
					ExDateTime exDateTime = startUtc.AddMinutes((double)this.options.FreeBusyInterval);
					if (busyType2 != BusyType.Free)
					{
						num4 += this.GetConflictValue(busyType2);
						num3++;
					}
					else if (!attendee.WorkingHours.IsWorkTime(startUtc, exDateTime))
					{
						num4 += 0.25;
						num3++;
					}
					else if (num3 == 0)
					{
						num2++;
					}
					startUtc = exDateTime;
				}
				this.sumReqFrontTime += (double)num2 / (double)num;
				double num5 = (double)num2 / (double)num;
				if (num5 < this.minPctReqFrontTime)
				{
					this.minPctReqFrontTime = num5;
				}
				this.weightedReqConflicts += num4 / (double)num;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00008360 File Offset: 0x00006560
		private void SetOptionalAttendeeAvailability(AttendeeData attendee, IndividualAttendeeConflictData attendeeConflict)
		{
			BusyType busyType = attendeeConflict.BusyType;
			if (busyType == BusyType.NoData)
			{
				this.optionalAttendeeCount--;
				return;
			}
			if (busyType != BusyType.Free)
			{
				if (this.strongestOptConflict < busyType)
				{
					this.strongestOptConflict = busyType;
				}
				this.optionalAttendeeConflictCount++;
				this.weightedOptConflicts += (double)busyType * 0.25 + 0.25;
				BusyType[] busyTypeRange = attendee.GetBusyTypeRange(this.meetingStartTime, this.meetingDuration);
				foreach (BusyType busyType2 in busyTypeRange)
				{
					if (busyType2 != BusyType.Free)
					{
						this.optOverlap += (double)this.options.FreeBusyInterval / (double)this.meetingDuration;
						this.weightedOptOverlap += ((double)busyType * 0.25 + 0.25) * ((double)this.options.FreeBusyInterval / (double)this.meetingDuration);
					}
				}
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00008458 File Offset: 0x00006658
		private void SetRoomAttendeeAvailability(AttendeeData attendee, IndividualAttendeeConflictData attendeeConflict)
		{
			BusyType busyType = attendeeConflict.BusyType;
			if (busyType == BusyType.NoData)
			{
				return;
			}
			this.roomsRequested = true;
			this.roomCount++;
			if (busyType == BusyType.Free)
			{
				this.roomsAvailableCount++;
				return;
			}
			this.roomConflictCount++;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000084A8 File Offset: 0x000066A8
		private void SetResourceAttendeeAvailability(AttendeeData attendee, IndividualAttendeeConflictData attendeeConflict)
		{
			BusyType busyType = attendeeConflict.BusyType;
			if (busyType == BusyType.NoData)
			{
				return;
			}
			this.resourceAttendeeCount++;
			if (busyType == BusyType.Free)
			{
				this.resourceAttendeeAvailableCount++;
				return;
			}
			this.resourceAttendeeConflictCount++;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000084F0 File Offset: 0x000066F0
		private long GetRating()
		{
			long num = 0L;
			if (this.requiredAttendeeConflictCount > 0 && this.excludeConflict)
			{
				return -1L;
			}
			num += (long)Math.Round(this.weightedPctReqConflicts * 100.0) << 56;
			num += (long)this.strongestReqConflict << 53;
			num += (long)Math.Round((1.0 - this.minPctReqFrontTime) * 100.0) << 46;
			num += (long)Math.Round((1.0 - this.sumPctReqFrontTime) * 100.0) << 39;
			if (this.roomsRequested && this.roomsAvailableCount == 0)
			{
				num += 274877906944L;
			}
			num += (long)this.convenienceMax << 32;
			num += (long)this.convenienceSum << 24;
			num += (long)Math.Round(this.pctOptConflicts * 100.0) << 17;
			num += (long)this.strongestOptConflict << 14;
			return num + ((long)Math.Round(this.pctOptOverlap * 100.0) << 7);
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00008604 File Offset: 0x00006804
		// (set) Token: 0x0600013C RID: 316 RVA: 0x0000860C File Offset: 0x0000680C
		[XmlElement]
		public DateTime MeetingTime
		{
			get
			{
				return this.startDateTime;
			}
			set
			{
				this.startDateTime = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00008615 File Offset: 0x00006815
		// (set) Token: 0x0600013E RID: 318 RVA: 0x0000861D File Offset: 0x0000681D
		[XmlElement]
		public bool IsWorkTime
		{
			get
			{
				return this.isWorkTime;
			}
			set
			{
				this.isWorkTime = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00008626 File Offset: 0x00006826
		[XmlIgnore]
		public TimeSpan Time
		{
			get
			{
				return this.startDateTime.TimeOfDay;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00008633 File Offset: 0x00006833
		[XmlIgnore]
		public long TimeSlotRating
		{
			get
			{
				return this.timeSlotRating;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000863B File Offset: 0x0000683B
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00008643 File Offset: 0x00006843
		[XmlElement]
		public SuggestionQuality SuggestionQuality
		{
			get
			{
				return this.bucket;
			}
			set
			{
				this.bucket = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000864C File Offset: 0x0000684C
		[XmlIgnore]
		public int RequiredAttendeeConflictCount
		{
			get
			{
				return this.requiredAttendeeConflictCount;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00008654 File Offset: 0x00006854
		[XmlIgnore]
		public int OptionalAttendeeConflictCount
		{
			get
			{
				return this.optionalAttendeeConflictCount;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000865C File Offset: 0x0000685C
		[XmlIgnore]
		public int RequiredAttendeeCount
		{
			get
			{
				return this.requiredAttendeeCount;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00008664 File Offset: 0x00006864
		[XmlIgnore]
		public int OptionalAttendeeCount
		{
			get
			{
				return this.optionalAttendeeCount;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000866C File Offset: 0x0000686C
		[XmlIgnore]
		public int ResourceAttendeeConflictCount
		{
			get
			{
				return this.resourceAttendeeConflictCount;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00008674 File Offset: 0x00006874
		[XmlIgnore]
		public int AvailableRoomsCount
		{
			get
			{
				return this.roomsAvailableCount;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000867C File Offset: 0x0000687C
		[XmlIgnore]
		public int RoomCount
		{
			get
			{
				return this.roomCount;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00008684 File Offset: 0x00006884
		[XmlIgnore]
		public int ResourceAttendeeCount
		{
			get
			{
				return this.resourceAttendeeCount;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000868C File Offset: 0x0000688C
		[XmlIgnore]
		public int ResourceAttendeeAvailableCount
		{
			get
			{
				return this.resourceAttendeeAvailableCount;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00008694 File Offset: 0x00006894
		[XmlIgnore]
		public int RoomConflictCount
		{
			get
			{
				return this.roomConflictCount;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000869C File Offset: 0x0000689C
		// (set) Token: 0x0600014E RID: 334 RVA: 0x000086A4 File Offset: 0x000068A4
		[XmlArray(IsNullable = false)]
		[XmlArrayItem(Type = typeof(TooBigGroupAttendeeConflictData))]
		[XmlArrayItem(Type = typeof(UnknownAttendeeConflictData))]
		[XmlArrayItem(Type = typeof(IndividualAttendeeConflictData))]
		[XmlArrayItem(Type = typeof(GroupAttendeeConflictData))]
		public AttendeeConflictData[] AttendeeConflictDataArray
		{
			get
			{
				return this.attendeeConflictDataArray;
			}
			set
			{
				this.attendeeConflictDataArray = value;
			}
		}

		// Token: 0x040000C5 RID: 197
		private const int MaximumInconvenienceSum = 256;

		// Token: 0x040000C6 RID: 198
		private static readonly Trace Tracer = ExTraceGlobals.MeetingSuggestionsTracer;

		// Token: 0x040000C7 RID: 199
		private DateTime startDateTime;

		// Token: 0x040000C8 RID: 200
		private ExDateTime meetingStartTime;

		// Token: 0x040000C9 RID: 201
		private long timeSlotRating;

		// Token: 0x040000CA RID: 202
		private bool isWorkTime = true;

		// Token: 0x040000CB RID: 203
		private bool excludeConflict;

		// Token: 0x040000CC RID: 204
		private SuggestionQuality bucket;

		// Token: 0x040000CD RID: 205
		private int requiredAttendeeConflictCount;

		// Token: 0x040000CE RID: 206
		private int optionalAttendeeConflictCount;

		// Token: 0x040000CF RID: 207
		private int resourceAttendeeCount;

		// Token: 0x040000D0 RID: 208
		private int resourceAttendeeAvailableCount;

		// Token: 0x040000D1 RID: 209
		private int resourceAttendeeConflictCount;

		// Token: 0x040000D2 RID: 210
		private int roomCount;

		// Token: 0x040000D3 RID: 211
		private int roomConflictCount;

		// Token: 0x040000D4 RID: 212
		private int roomsAvailableCount;

		// Token: 0x040000D5 RID: 213
		private bool roomsRequested;

		// Token: 0x040000D6 RID: 214
		private int requiredAttendeeCount;

		// Token: 0x040000D7 RID: 215
		private int optionalAttendeeCount;

		// Token: 0x040000D8 RID: 216
		private int convenienceSum;

		// Token: 0x040000D9 RID: 217
		private int convenienceMax;

		// Token: 0x040000DA RID: 218
		private int meetingDuration;

		// Token: 0x040000DB RID: 219
		private BusyType strongestReqConflict;

		// Token: 0x040000DC RID: 220
		private BusyType strongestOptConflict;

		// Token: 0x040000DD RID: 221
		private double weightedReqConflicts;

		// Token: 0x040000DE RID: 222
		private double weightedPctReqConflicts;

		// Token: 0x040000DF RID: 223
		private double pctOptConflicts;

		// Token: 0x040000E0 RID: 224
		private double weightedOptConflicts;

		// Token: 0x040000E1 RID: 225
		private double optOverlap;

		// Token: 0x040000E2 RID: 226
		private double pctOptOverlap;

		// Token: 0x040000E3 RID: 227
		private double weightedOptOverlap;

		// Token: 0x040000E4 RID: 228
		private double sumReqFrontTime;

		// Token: 0x040000E5 RID: 229
		private double sumPctReqFrontTime;

		// Token: 0x040000E6 RID: 230
		private double minPctReqFrontTime = 1.0;

		// Token: 0x040000E7 RID: 231
		private ConfigOptions options;

		// Token: 0x040000E8 RID: 232
		private AttendeeConflictData[] attendeeConflictDataArray;
	}
}
