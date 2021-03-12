using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200023D RID: 573
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class MeetingResponseMessageType : MeetingMessageType
	{
		// Token: 0x04000EDB RID: 3803
		public DateTime Start;

		// Token: 0x04000EDC RID: 3804
		[XmlIgnore]
		public bool StartSpecified;

		// Token: 0x04000EDD RID: 3805
		public DateTime End;

		// Token: 0x04000EDE RID: 3806
		[XmlIgnore]
		public bool EndSpecified;

		// Token: 0x04000EDF RID: 3807
		public string Location;

		// Token: 0x04000EE0 RID: 3808
		public RecurrenceType Recurrence;

		// Token: 0x04000EE1 RID: 3809
		public string CalendarItemType;

		// Token: 0x04000EE2 RID: 3810
		public DateTime ProposedStart;

		// Token: 0x04000EE3 RID: 3811
		[XmlIgnore]
		public bool ProposedStartSpecified;

		// Token: 0x04000EE4 RID: 3812
		public DateTime ProposedEnd;

		// Token: 0x04000EE5 RID: 3813
		[XmlIgnore]
		public bool ProposedEndSpecified;

		// Token: 0x04000EE6 RID: 3814
		public EnhancedLocationType EnhancedLocation;
	}
}
