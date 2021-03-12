using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000428 RID: 1064
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetMessageTrackingReportRequestType : BaseRequestType
	{
		// Token: 0x04001671 RID: 5745
		public string Scope;

		// Token: 0x04001672 RID: 5746
		public MessageTrackingReportTemplateType ReportTemplate;

		// Token: 0x04001673 RID: 5747
		public EmailAddressType RecipientFilter;

		// Token: 0x04001674 RID: 5748
		public string MessageTrackingReportId;

		// Token: 0x04001675 RID: 5749
		public bool ReturnQueueEvents;

		// Token: 0x04001676 RID: 5750
		[XmlIgnore]
		public bool ReturnQueueEventsSpecified;

		// Token: 0x04001677 RID: 5751
		public string DiagnosticsLevel;

		// Token: 0x04001678 RID: 5752
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public TrackingPropertyType[] Properties;
	}
}
