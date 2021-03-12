using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002CC RID: 716
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SuggestionsViewOptionsType
	{
		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x00027C49 File Offset: 0x00025E49
		// (set) Token: 0x0600183F RID: 6207 RVA: 0x00027C51 File Offset: 0x00025E51
		public int GoodThreshold
		{
			get
			{
				return this.goodThresholdField;
			}
			set
			{
				this.goodThresholdField = value;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x00027C5A File Offset: 0x00025E5A
		// (set) Token: 0x06001841 RID: 6209 RVA: 0x00027C62 File Offset: 0x00025E62
		[XmlIgnore]
		public bool GoodThresholdSpecified
		{
			get
			{
				return this.goodThresholdFieldSpecified;
			}
			set
			{
				this.goodThresholdFieldSpecified = value;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x00027C6B File Offset: 0x00025E6B
		// (set) Token: 0x06001843 RID: 6211 RVA: 0x00027C73 File Offset: 0x00025E73
		public int MaximumResultsByDay
		{
			get
			{
				return this.maximumResultsByDayField;
			}
			set
			{
				this.maximumResultsByDayField = value;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x00027C7C File Offset: 0x00025E7C
		// (set) Token: 0x06001845 RID: 6213 RVA: 0x00027C84 File Offset: 0x00025E84
		[XmlIgnore]
		public bool MaximumResultsByDaySpecified
		{
			get
			{
				return this.maximumResultsByDayFieldSpecified;
			}
			set
			{
				this.maximumResultsByDayFieldSpecified = value;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x00027C8D File Offset: 0x00025E8D
		// (set) Token: 0x06001847 RID: 6215 RVA: 0x00027C95 File Offset: 0x00025E95
		public int MaximumNonWorkHourResultsByDay
		{
			get
			{
				return this.maximumNonWorkHourResultsByDayField;
			}
			set
			{
				this.maximumNonWorkHourResultsByDayField = value;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x00027C9E File Offset: 0x00025E9E
		// (set) Token: 0x06001849 RID: 6217 RVA: 0x00027CA6 File Offset: 0x00025EA6
		[XmlIgnore]
		public bool MaximumNonWorkHourResultsByDaySpecified
		{
			get
			{
				return this.maximumNonWorkHourResultsByDayFieldSpecified;
			}
			set
			{
				this.maximumNonWorkHourResultsByDayFieldSpecified = value;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x00027CAF File Offset: 0x00025EAF
		// (set) Token: 0x0600184B RID: 6219 RVA: 0x00027CB7 File Offset: 0x00025EB7
		public int MeetingDurationInMinutes
		{
			get
			{
				return this.meetingDurationInMinutesField;
			}
			set
			{
				this.meetingDurationInMinutesField = value;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x00027CC0 File Offset: 0x00025EC0
		// (set) Token: 0x0600184D RID: 6221 RVA: 0x00027CC8 File Offset: 0x00025EC8
		[XmlIgnore]
		public bool MeetingDurationInMinutesSpecified
		{
			get
			{
				return this.meetingDurationInMinutesFieldSpecified;
			}
			set
			{
				this.meetingDurationInMinutesFieldSpecified = value;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x00027CD1 File Offset: 0x00025ED1
		// (set) Token: 0x0600184F RID: 6223 RVA: 0x00027CD9 File Offset: 0x00025ED9
		public SuggestionQuality MinimumSuggestionQuality
		{
			get
			{
				return this.minimumSuggestionQualityField;
			}
			set
			{
				this.minimumSuggestionQualityField = value;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06001850 RID: 6224 RVA: 0x00027CE2 File Offset: 0x00025EE2
		// (set) Token: 0x06001851 RID: 6225 RVA: 0x00027CEA File Offset: 0x00025EEA
		[XmlIgnore]
		public bool MinimumSuggestionQualitySpecified
		{
			get
			{
				return this.minimumSuggestionQualityFieldSpecified;
			}
			set
			{
				this.minimumSuggestionQualityFieldSpecified = value;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x00027CF3 File Offset: 0x00025EF3
		// (set) Token: 0x06001853 RID: 6227 RVA: 0x00027CFB File Offset: 0x00025EFB
		public Duration DetailedSuggestionsWindow
		{
			get
			{
				return this.detailedSuggestionsWindowField;
			}
			set
			{
				this.detailedSuggestionsWindowField = value;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x00027D04 File Offset: 0x00025F04
		// (set) Token: 0x06001855 RID: 6229 RVA: 0x00027D0C File Offset: 0x00025F0C
		public DateTime CurrentMeetingTime
		{
			get
			{
				return this.currentMeetingTimeField;
			}
			set
			{
				this.currentMeetingTimeField = value;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x00027D15 File Offset: 0x00025F15
		// (set) Token: 0x06001857 RID: 6231 RVA: 0x00027D1D File Offset: 0x00025F1D
		[XmlIgnore]
		public bool CurrentMeetingTimeSpecified
		{
			get
			{
				return this.currentMeetingTimeFieldSpecified;
			}
			set
			{
				this.currentMeetingTimeFieldSpecified = value;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x00027D26 File Offset: 0x00025F26
		// (set) Token: 0x06001859 RID: 6233 RVA: 0x00027D2E File Offset: 0x00025F2E
		public string GlobalObjectId
		{
			get
			{
				return this.globalObjectIdField;
			}
			set
			{
				this.globalObjectIdField = value;
			}
		}

		// Token: 0x04001071 RID: 4209
		private int goodThresholdField;

		// Token: 0x04001072 RID: 4210
		private bool goodThresholdFieldSpecified;

		// Token: 0x04001073 RID: 4211
		private int maximumResultsByDayField;

		// Token: 0x04001074 RID: 4212
		private bool maximumResultsByDayFieldSpecified;

		// Token: 0x04001075 RID: 4213
		private int maximumNonWorkHourResultsByDayField;

		// Token: 0x04001076 RID: 4214
		private bool maximumNonWorkHourResultsByDayFieldSpecified;

		// Token: 0x04001077 RID: 4215
		private int meetingDurationInMinutesField;

		// Token: 0x04001078 RID: 4216
		private bool meetingDurationInMinutesFieldSpecified;

		// Token: 0x04001079 RID: 4217
		private SuggestionQuality minimumSuggestionQualityField;

		// Token: 0x0400107A RID: 4218
		private bool minimumSuggestionQualityFieldSpecified;

		// Token: 0x0400107B RID: 4219
		private Duration detailedSuggestionsWindowField;

		// Token: 0x0400107C RID: 4220
		private DateTime currentMeetingTimeField;

		// Token: 0x0400107D RID: 4221
		private bool currentMeetingTimeFieldSpecified;

		// Token: 0x0400107E RID: 4222
		private string globalObjectIdField;
	}
}
