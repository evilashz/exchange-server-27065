using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions
{
	// Token: 0x0200004D RID: 77
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SuggestionDayResult
	{
		// Token: 0x0600017E RID: 382 RVA: 0x00008DFC File Offset: 0x00006FFC
		public SuggestionDayResult()
		{
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00008E0C File Offset: 0x0000700C
		internal SuggestionDayResult(ExDateTime currentDate, int inputMeetingDuration, int requiredAttendeeCount, int optionalAttendeeCount, AttendeeData[] attendees, ConfigOptions options, ExDateTime currentMeetingTime)
		{
			this.date = currentDate.LocalTime;
			ExDateTime t = ExDateTime.Now.ToUtc();
			List<Suggestion> list = new List<Suggestion>();
			ExDateTime t2 = currentDate.AddDays(1.0);
			ExDateTime exDateTime = currentDate.ToUtc();
			while (exDateTime < t2)
			{
				if (exDateTime >= t || exDateTime == currentMeetingTime)
				{
					list.Add(new Suggestion(currentDate.TimeZone.ConvertDateTime(exDateTime), inputMeetingDuration, requiredAttendeeCount, optionalAttendeeCount, attendees, options));
				}
				exDateTime = exDateTime.AddMinutes((double)options.SuggestionInterval);
			}
			this.rawSuggestionsList = list.ToArray();
			this.FilterSuggestions(list, options, currentMeetingTime);
			foreach (Suggestion suggestion in list)
			{
				if (suggestion.SuggestionQuality < this.quality)
				{
					this.quality = suggestion.SuggestionQuality;
				}
			}
			this.meetingSuggestions = list.ToArray();
		}

		// Token: 0x1700004C RID: 76
		[XmlIgnore]
		public Suggestion this[TimeSpan timeOfDay]
		{
			get
			{
				if (timeOfDay.TotalMinutes < 1440.0)
				{
					return this.rawSuggestionsList[(int)((double)this.rawSuggestionsList.Length * timeOfDay.TotalMinutes) / 1440];
				}
				throw new ArgumentException("timeOfDay must be between 0:00 and 23:59:59");
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00009060 File Offset: 0x00007260
		private void FilterSuggestions(List<Suggestion> suggestionList, ConfigOptions options, ExDateTime currentMeetingTime)
		{
			suggestionList.RemoveAll((Suggestion suggestion) => !(suggestion.MeetingTime == (DateTime)currentMeetingTime) && (suggestion.TimeSlotRating == -1L || suggestion.SuggestionQuality > options.MinimumSuggestionQuality) && options.MinimumSuggestionQuality != SuggestionQuality.Poor);
			SuggestionDayResult.Tracer.TraceDebug<object, int>((long)this.GetHashCode(), "{0}: {1} suggestions passing minimum quality.", TraceContext.Get(), suggestionList.Count);
			suggestionList.Sort(delegate(Suggestion x, Suggestion y)
			{
				if (x == y)
				{
					return 0;
				}
				if (x.MeetingTime == (DateTime)currentMeetingTime)
				{
					return -1;
				}
				if (y.MeetingTime == (DateTime)currentMeetingTime)
				{
					return 1;
				}
				if (x.TimeSlotRating == y.TimeSlotRating)
				{
					if (x.MeetingTime < y.MeetingTime)
					{
						return -1;
					}
					if (x.MeetingTime > y.MeetingTime)
					{
						return 1;
					}
					return 0;
				}
				else
				{
					if (x.TimeSlotRating > y.TimeSlotRating)
					{
						return 1;
					}
					return -1;
				}
			});
			int num = 0;
			for (int i = 0; i < suggestionList.Count; i++)
			{
				if (!suggestionList[i].IsWorkTime)
				{
					if (num >= options.MaximumNonWorkHourResultsPerDay)
					{
						suggestionList.RemoveAt(i);
						i--;
					}
					else
					{
						num++;
					}
				}
			}
			SuggestionDayResult.Tracer.TraceDebug<object, int>((long)this.GetHashCode(), "{0}: {1} suggestions after non-working hour restrictions.", TraceContext.Get(), suggestionList.Count);
			if (suggestionList.Count > options.MaximumResultsPerDay)
			{
				suggestionList.RemoveRange(options.MaximumResultsPerDay, suggestionList.Count - options.MaximumResultsPerDay);
				SuggestionDayResult.Tracer.TraceDebug<object, int>((long)this.GetHashCode(), "{0}: suggestions array length shortened to MaximumResultsPerDay ({1}).", TraceContext.Get(), options.MaximumResultsPerDay);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00009184 File Offset: 0x00007384
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000918C File Offset: 0x0000738C
		[XmlElement]
		[IgnoreDataMember]
		public DateTime Date
		{
			get
			{
				return this.date;
			}
			set
			{
				this.date = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00009195 File Offset: 0x00007395
		// (set) Token: 0x06000185 RID: 389 RVA: 0x000091A2 File Offset: 0x000073A2
		[DataMember(Name = "Date")]
		[XmlIgnore]
		public string DateString
		{
			get
			{
				return this.Date.ToIso8061();
			}
			set
			{
				this.Date = DateTime.Parse(value);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000091B0 File Offset: 0x000073B0
		// (set) Token: 0x06000187 RID: 391 RVA: 0x000091B8 File Offset: 0x000073B8
		[XmlElement]
		[IgnoreDataMember]
		public SuggestionQuality DayQuality
		{
			get
			{
				return this.quality;
			}
			set
			{
				this.quality = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000091C1 File Offset: 0x000073C1
		// (set) Token: 0x06000189 RID: 393 RVA: 0x000091CE File Offset: 0x000073CE
		[XmlIgnore]
		[DataMember(Name = "DayQuality")]
		public string DayQualityString
		{
			get
			{
				return EnumUtil.ToString<SuggestionQuality>(this.DayQuality);
			}
			set
			{
				this.DayQuality = EnumUtil.Parse<SuggestionQuality>(value);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000091DC File Offset: 0x000073DC
		// (set) Token: 0x0600018B RID: 395 RVA: 0x000091E4 File Offset: 0x000073E4
		[XmlArrayItem(Type = typeof(Suggestion), IsNullable = false)]
		[DataMember]
		[XmlArray(IsNullable = false)]
		public Suggestion[] SuggestionArray
		{
			get
			{
				return this.meetingSuggestions;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.meetingSuggestions = value;
			}
		}

		// Token: 0x0400011A RID: 282
		private static readonly Trace Tracer = ExTraceGlobals.MeetingSuggestionsTracer;

		// Token: 0x0400011B RID: 283
		private DateTime date;

		// Token: 0x0400011C RID: 284
		private Suggestion[] meetingSuggestions;

		// Token: 0x0400011D RID: 285
		private Suggestion[] rawSuggestionsList;

		// Token: 0x0400011E RID: 286
		private SuggestionQuality quality = SuggestionQuality.Poor;
	}
}
