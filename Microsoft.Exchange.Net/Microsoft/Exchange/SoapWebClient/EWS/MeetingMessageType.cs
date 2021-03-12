using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200023B RID: 571
	[DesignerCategory("code")]
	[XmlInclude(typeof(MeetingRequestMessageType))]
	[DebuggerStepThrough]
	[XmlInclude(typeof(MeetingCancellationMessageType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlInclude(typeof(MeetingResponseMessageType))]
	[Serializable]
	public class MeetingMessageType : MessageType
	{
		// Token: 0x04000EC3 RID: 3779
		public ItemIdType AssociatedCalendarItemId;

		// Token: 0x04000EC4 RID: 3780
		public bool IsDelegated;

		// Token: 0x04000EC5 RID: 3781
		[XmlIgnore]
		public bool IsDelegatedSpecified;

		// Token: 0x04000EC6 RID: 3782
		public bool IsOutOfDate;

		// Token: 0x04000EC7 RID: 3783
		[XmlIgnore]
		public bool IsOutOfDateSpecified;

		// Token: 0x04000EC8 RID: 3784
		public bool HasBeenProcessed;

		// Token: 0x04000EC9 RID: 3785
		[XmlIgnore]
		public bool HasBeenProcessedSpecified;

		// Token: 0x04000ECA RID: 3786
		public ResponseTypeType ResponseType;

		// Token: 0x04000ECB RID: 3787
		[XmlIgnore]
		public bool ResponseTypeSpecified;

		// Token: 0x04000ECC RID: 3788
		public string UID;

		// Token: 0x04000ECD RID: 3789
		public DateTime RecurrenceId;

		// Token: 0x04000ECE RID: 3790
		[XmlIgnore]
		public bool RecurrenceIdSpecified;

		// Token: 0x04000ECF RID: 3791
		public DateTime DateTimeStamp;

		// Token: 0x04000ED0 RID: 3792
		[XmlIgnore]
		public bool DateTimeStampSpecified;

		// Token: 0x04000ED1 RID: 3793
		public bool IsOrganizer;

		// Token: 0x04000ED2 RID: 3794
		[XmlIgnore]
		public bool IsOrganizerSpecified;
	}
}
