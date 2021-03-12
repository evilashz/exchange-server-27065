using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200044F RID: 1103
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[Serializable]
	public class GetServerTimeZonesType : BaseRequestType
	{
		// Token: 0x040016E8 RID: 5864
		[XmlArrayItem("Id", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Ids;

		// Token: 0x040016E9 RID: 5865
		[XmlAttribute]
		public bool ReturnFullTimeZoneData;

		// Token: 0x040016EA RID: 5866
		[XmlIgnore]
		public bool ReturnFullTimeZoneDataSpecified;
	}
}
