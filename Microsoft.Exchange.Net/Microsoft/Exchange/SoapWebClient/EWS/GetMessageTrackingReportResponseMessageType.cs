using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200029B RID: 667
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetMessageTrackingReportResponseMessageType : ResponseMessageType
	{
		// Token: 0x0400118D RID: 4493
		public MessageTrackingReportType MessageTrackingReport;

		// Token: 0x0400118E RID: 4494
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Diagnostics;

		// Token: 0x0400118F RID: 4495
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ArrayOfTrackingPropertiesType[] Errors;

		// Token: 0x04001190 RID: 4496
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public TrackingPropertyType[] Properties;
	}
}
