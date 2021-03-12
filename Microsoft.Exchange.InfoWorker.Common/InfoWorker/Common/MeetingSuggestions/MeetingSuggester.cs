using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions
{
	// Token: 0x0200003E RID: 62
	internal class MeetingSuggester
	{
		// Token: 0x06000126 RID: 294 RVA: 0x00007B00 File Offset: 0x00005D00
		private void ValidateAndProcessInput(ExDateTime startDate, ExDateTime endDate, int inputMeetingDuration, AttendeeData[] attendees)
		{
			this.requiredAttendeeCount = 0;
			this.optionalAttendeeCount = 0;
			foreach (AttendeeData attendeeData in attendees)
			{
				if (attendeeData == null)
				{
					MeetingSuggester.Tracer.TraceError((long)this.GetHashCode(), "{0}: Invalid inputAttendee array; an element is null.", new object[]
					{
						TraceContext.Get()
					});
					throw new ArgumentException("Invalid inputAttendee array; an element is null.");
				}
				switch (attendeeData.AttendeeType)
				{
				case MeetingAttendeeType.Organizer:
				case MeetingAttendeeType.Required:
					this.requiredAttendeeCount++;
					break;
				case MeetingAttendeeType.Optional:
					this.optionalAttendeeCount++;
					break;
				}
				if (startDate < attendeeData.FreeBusyStartTime || startDate >= attendeeData.FreeBusyEndTime || endDate <= attendeeData.FreeBusyStartTime || endDate > attendeeData.FreeBusyEndTime)
				{
					MeetingSuggester.Tracer.TraceError((long)this.GetHashCode(), "{0}: start and end times outside availability data.", new object[]
					{
						TraceContext.Get()
					});
					throw new InvalidParameterException(Strings.descStartAndEndTimesOutSideFreeBusyData);
				}
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007C14 File Offset: 0x00005E14
		public SuggestionDayResult[] GetSuggestionsByDateRange(ExDateTime startDate, ExDateTime endDate, ExTimeZone timeZone, int inputMeetingDuration, AttendeeData[] attendees)
		{
			this.meetingDuration = inputMeetingDuration;
			MeetingSuggester.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Entering MeetingSuggester.GetSuggestionsByDateRange()", new object[]
			{
				TraceContext.Get()
			});
			MeetingSuggester.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: startDate={1}, endDate={2}, inputMeetingDuration={3}", new object[]
			{
				TraceContext.Get(),
				startDate,
				endDate,
				inputMeetingDuration
			});
			this.ValidateAndProcessInput(startDate, endDate, inputMeetingDuration, attendees);
			List<SuggestionDayResult> list = new List<SuggestionDayResult>();
			ExDateTime exDateTime = startDate;
			while (exDateTime < endDate)
			{
				list.Add(new SuggestionDayResult(exDateTime, this.meetingDuration, this.requiredAttendeeCount, this.optionalAttendeeCount, attendees, this.options, this.currentMeetingTime));
				exDateTime = exDateTime.AddDays(1.0);
			}
			return list.ToArray();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007CF4 File Offset: 0x00005EF4
		internal void SetOptionsFromSuggestionsViewOptions(SuggestionsViewOptions svOptions, ExTimeZone timeZone)
		{
			this.options.GoodThreshold = svOptions.GoodThreshold;
			this.options.MaximumResultsPerDay = svOptions.MaximumResultsByDay;
			this.options.MaximumNonWorkHourResultsPerDay = svOptions.MaximumNonWorkHourResultsByDay;
			this.meetingDuration = svOptions.MeetingDurationInMinutes;
			this.options.MinimumSuggestionQuality = svOptions.MinimumSuggestionQuality;
			this.currentMeetingTime = new ExDateTime(timeZone, svOptions.CurrentMeetingTime);
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00007D63 File Offset: 0x00005F63
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00007D70 File Offset: 0x00005F70
		internal int MaximumResultsPerDay
		{
			get
			{
				return this.options.MaximumResultsPerDay;
			}
			set
			{
				if (value < 0 || value > 48)
				{
					throw new InvalidParameterException(Strings.descInvalidMaximumResults);
				}
				this.options.MaximumResultsPerDay = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00007D92 File Offset: 0x00005F92
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00007D9F File Offset: 0x00005F9F
		internal int MaximumNonWorkHourResultsPerDay
		{
			get
			{
				return this.options.MaximumNonWorkHourResultsPerDay;
			}
			set
			{
				if (value < 0 || value > 48)
				{
					throw new InvalidParameterException(Strings.descInvalidMaxNonWorkHourResultsPerDay);
				}
				this.options.MaximumNonWorkHourResultsPerDay = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00007DC1 File Offset: 0x00005FC1
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00007DCE File Offset: 0x00005FCE
		internal SuggestionQuality MinimumSuggestionQuality
		{
			get
			{
				return this.options.MinimumSuggestionQuality;
			}
			set
			{
				this.options.MinimumSuggestionQuality = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00007DDC File Offset: 0x00005FDC
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00007DE9 File Offset: 0x00005FE9
		internal int GoodThreshold
		{
			get
			{
				return this.options.GoodThreshold;
			}
			set
			{
				if (value < 1 || value > 49)
				{
					throw new InvalidParameterException(Strings.descInvalidGoodThreshold(1, 49));
				}
				this.options.GoodThreshold = value;
			}
		}

		// Token: 0x040000BE RID: 190
		public const int MaxMinutesInFullDayMeeting = 1440;

		// Token: 0x040000BF RID: 191
		private static readonly Trace Tracer = ExTraceGlobals.MeetingSuggestionsTracer;

		// Token: 0x040000C0 RID: 192
		private int meetingDuration;

		// Token: 0x040000C1 RID: 193
		private ConfigOptions options = new ConfigOptions();

		// Token: 0x040000C2 RID: 194
		private int requiredAttendeeCount;

		// Token: 0x040000C3 RID: 195
		private int optionalAttendeeCount;

		// Token: 0x040000C4 RID: 196
		private ExDateTime currentMeetingTime;
	}
}
