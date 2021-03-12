using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200023C RID: 572
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class MeetingCancellationMessageType : MeetingMessageType
	{
		// Token: 0x04000ED3 RID: 3795
		public DateTime Start;

		// Token: 0x04000ED4 RID: 3796
		[XmlIgnore]
		public bool StartSpecified;

		// Token: 0x04000ED5 RID: 3797
		public DateTime End;

		// Token: 0x04000ED6 RID: 3798
		[XmlIgnore]
		public bool EndSpecified;

		// Token: 0x04000ED7 RID: 3799
		public string Location;

		// Token: 0x04000ED8 RID: 3800
		public RecurrenceType Recurrence;

		// Token: 0x04000ED9 RID: 3801
		public string CalendarItemType;

		// Token: 0x04000EDA RID: 3802
		public EnhancedLocationType EnhancedLocation;
	}
}
