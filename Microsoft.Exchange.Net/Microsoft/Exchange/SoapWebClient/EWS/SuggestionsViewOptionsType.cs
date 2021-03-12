using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003AD RID: 941
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SuggestionsViewOptionsType
	{
		// Token: 0x040014C3 RID: 5315
		public int GoodThreshold;

		// Token: 0x040014C4 RID: 5316
		[XmlIgnore]
		public bool GoodThresholdSpecified;

		// Token: 0x040014C5 RID: 5317
		public int MaximumResultsByDay;

		// Token: 0x040014C6 RID: 5318
		[XmlIgnore]
		public bool MaximumResultsByDaySpecified;

		// Token: 0x040014C7 RID: 5319
		public int MaximumNonWorkHourResultsByDay;

		// Token: 0x040014C8 RID: 5320
		[XmlIgnore]
		public bool MaximumNonWorkHourResultsByDaySpecified;

		// Token: 0x040014C9 RID: 5321
		public int MeetingDurationInMinutes;

		// Token: 0x040014CA RID: 5322
		[XmlIgnore]
		public bool MeetingDurationInMinutesSpecified;

		// Token: 0x040014CB RID: 5323
		public SuggestionQuality MinimumSuggestionQuality;

		// Token: 0x040014CC RID: 5324
		[XmlIgnore]
		public bool MinimumSuggestionQualitySpecified;

		// Token: 0x040014CD RID: 5325
		public Duration DetailedSuggestionsWindow;

		// Token: 0x040014CE RID: 5326
		public DateTime CurrentMeetingTime;

		// Token: 0x040014CF RID: 5327
		[XmlIgnore]
		public bool CurrentMeetingTimeSpecified;

		// Token: 0x040014D0 RID: 5328
		public string GlobalObjectId;
	}
}
