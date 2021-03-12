using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200004E RID: 78
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SuggestionsViewOptions
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00009202 File Offset: 0x00007402
		public SuggestionsViewOptions()
		{
			this.Init();
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00009210 File Offset: 0x00007410
		// (set) Token: 0x0600018F RID: 399 RVA: 0x00009218 File Offset: 0x00007418
		[DataMember]
		public int GoodThreshold
		{
			get
			{
				return this.goodThreshold;
			}
			set
			{
				this.goodThreshold = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00009221 File Offset: 0x00007421
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00009229 File Offset: 0x00007429
		[DataMember]
		public int MaximumResultsByDay
		{
			get
			{
				return this.maximumResultsByDay;
			}
			set
			{
				this.maximumResultsByDay = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00009232 File Offset: 0x00007432
		// (set) Token: 0x06000193 RID: 403 RVA: 0x0000923A File Offset: 0x0000743A
		[DataMember]
		public int MaximumNonWorkHourResultsByDay
		{
			get
			{
				return this.maximumNonWorkHourResultsByDay;
			}
			set
			{
				this.maximumNonWorkHourResultsByDay = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00009243 File Offset: 0x00007443
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000924B File Offset: 0x0000744B
		[DataMember]
		public int MeetingDurationInMinutes
		{
			get
			{
				return this.meetingDuration;
			}
			set
			{
				this.meetingDuration = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00009254 File Offset: 0x00007454
		// (set) Token: 0x06000197 RID: 407 RVA: 0x0000925C File Offset: 0x0000745C
		[IgnoreDataMember]
		public SuggestionQuality MinimumSuggestionQuality
		{
			get
			{
				return this.minimumSuggestionQuality;
			}
			set
			{
				this.minimumSuggestionQuality = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00009265 File Offset: 0x00007465
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00009272 File Offset: 0x00007472
		[XmlIgnore]
		[DataMember(Name = "MinimumSuggestionQuality")]
		public string MinimumSuggestionQualityString
		{
			get
			{
				return EnumUtil.ToString<SuggestionQuality>(this.MinimumSuggestionQuality);
			}
			set
			{
				this.MinimumSuggestionQuality = EnumUtil.Parse<SuggestionQuality>(value);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00009280 File Offset: 0x00007480
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00009288 File Offset: 0x00007488
		[DataMember]
		public Duration DetailedSuggestionsWindow
		{
			get
			{
				return this.detailedSuggestionsWindow;
			}
			set
			{
				this.detailedSuggestionsWindow = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00009291 File Offset: 0x00007491
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00009299 File Offset: 0x00007499
		[XmlElement]
		[IgnoreDataMember]
		public DateTime CurrentMeetingTime
		{
			get
			{
				return this.currentMeetingTime;
			}
			set
			{
				this.currentMeetingTime = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600019E RID: 414 RVA: 0x000092A2 File Offset: 0x000074A2
		// (set) Token: 0x0600019F RID: 415 RVA: 0x000092AF File Offset: 0x000074AF
		[DataMember(Name = "CurrentMeetingTime")]
		[XmlIgnore]
		public string CurrentMeetingTimeString
		{
			get
			{
				return this.CurrentMeetingTime.ToIso8061();
			}
			set
			{
				this.CurrentMeetingTime = DateTime.Parse(value);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000092BD File Offset: 0x000074BD
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x000092C5 File Offset: 0x000074C5
		[DataMember]
		[XmlElement]
		public string GlobalObjectId
		{
			get
			{
				return this.globalObjectId;
			}
			set
			{
				this.globalObjectId = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000092D0 File Offset: 0x000074D0
		[XmlIgnore]
		internal byte[] GlobalObjectIdByteArray
		{
			get
			{
				if (this.globalObjectIdByteArray == null && this.globalObjectId != null)
				{
					int num = this.globalObjectId.Length / 2;
					this.globalObjectIdByteArray = new byte[num];
					for (int i = 0; i < num; i++)
					{
						this.globalObjectIdByteArray[i] = byte.Parse(this.globalObjectId.Substring(i * 2, 2), NumberStyles.AllowHexSpecifier);
					}
				}
				return this.globalObjectIdByteArray;
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000933C File Offset: 0x0000753C
		internal void Validate()
		{
			if (this.detailedSuggestionsWindow == null)
			{
				throw new MissingArgumentException(Strings.descMissingArgument("SuggestionsViewOptions.DetailedSuggestionsWindow"));
			}
			this.detailedSuggestionsWindow.Validate("SuggestionsViewOptions.DetailedSuggestionsWindow");
			if (this.detailedSuggestionsWindow.StartTime.TimeOfDay != TimeSpan.Zero)
			{
				throw new InvalidParameterException(Strings.descDateMustHaveZeroTimeSpan("SuggestionsViewOptions.DetailedSuggestionsWindow.StartDate"));
			}
			if (this.detailedSuggestionsWindow.EndTime.TimeOfDay != TimeSpan.Zero)
			{
				throw new InvalidParameterException(Strings.descDateMustHaveZeroTimeSpan("SuggestionsViewOptions.DetailedSuggestionsWindow.EndDate"));
			}
			if (this.goodThreshold < 1 || this.goodThreshold > 49)
			{
				throw new InvalidParameterException(Strings.descInvalidGoodThreshold(1, 49));
			}
			if (this.maximumResultsByDay < 0 || this.maximumResultsByDay > 48)
			{
				throw new InvalidParameterException(Strings.descInvalidMaximumResults);
			}
			if (this.maximumNonWorkHourResultsByDay < 0 || this.maximumNonWorkHourResultsByDay > 48)
			{
				throw new InvalidParameterException(Strings.descInvalidMaxNonWorkHourResultsPerDay);
			}
			if (this.meetingDuration < 30)
			{
				throw new DurationTooSmallException();
			}
			if (this.meetingDuration > 1440)
			{
				throw new DurationTooLargeException();
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000944F File Offset: 0x0000764F
		[OnDeserializing]
		private void Init(StreamingContext context)
		{
			this.Init();
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00009457 File Offset: 0x00007657
		private void Init()
		{
			this.goodThreshold = 25;
			this.maximumResultsByDay = 24;
			this.maximumNonWorkHourResultsByDay = 0;
			this.meetingDuration = 60;
			this.minimumSuggestionQuality = SuggestionQuality.Fair;
		}

		// Token: 0x0400011F RID: 287
		private int goodThreshold;

		// Token: 0x04000120 RID: 288
		private int maximumResultsByDay;

		// Token: 0x04000121 RID: 289
		private int maximumNonWorkHourResultsByDay;

		// Token: 0x04000122 RID: 290
		private int meetingDuration;

		// Token: 0x04000123 RID: 291
		private SuggestionQuality minimumSuggestionQuality;

		// Token: 0x04000124 RID: 292
		private Duration detailedSuggestionsWindow;

		// Token: 0x04000125 RID: 293
		private DateTime currentMeetingTime;

		// Token: 0x04000126 RID: 294
		private string globalObjectId;

		// Token: 0x04000127 RID: 295
		private byte[] globalObjectIdByteArray;
	}
}
