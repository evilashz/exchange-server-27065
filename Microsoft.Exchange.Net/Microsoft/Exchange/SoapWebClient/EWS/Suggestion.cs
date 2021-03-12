using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000356 RID: 854
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class Suggestion
	{
		// Token: 0x0400141C RID: 5148
		public DateTime MeetingTime;

		// Token: 0x0400141D RID: 5149
		public bool IsWorkTime;

		// Token: 0x0400141E RID: 5150
		public SuggestionQuality SuggestionQuality;

		// Token: 0x0400141F RID: 5151
		[XmlArrayItem(typeof(IndividualAttendeeConflictData))]
		[XmlArrayItem(typeof(TooBigGroupAttendeeConflictData))]
		[XmlArrayItem(typeof(GroupAttendeeConflictData))]
		[XmlArrayItem(typeof(UnknownAttendeeConflictData))]
		public AttendeeConflictData[] AttendeeConflictDataArray;
	}
}
