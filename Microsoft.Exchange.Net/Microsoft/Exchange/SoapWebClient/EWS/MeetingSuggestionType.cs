using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000264 RID: 612
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class MeetingSuggestionType : EntityType
	{
		// Token: 0x04000FB7 RID: 4023
		[XmlArrayItem("EmailUser", IsNullable = false)]
		public EmailUserType[] Attendees;

		// Token: 0x04000FB8 RID: 4024
		public string Location;

		// Token: 0x04000FB9 RID: 4025
		public string Subject;

		// Token: 0x04000FBA RID: 4026
		public string MeetingString;

		// Token: 0x04000FBB RID: 4027
		public DateTime StartTime;

		// Token: 0x04000FBC RID: 4028
		[XmlIgnore]
		public bool StartTimeSpecified;

		// Token: 0x04000FBD RID: 4029
		public DateTime EndTime;

		// Token: 0x04000FBE RID: 4030
		[XmlIgnore]
		public bool EndTimeSpecified;
	}
}
