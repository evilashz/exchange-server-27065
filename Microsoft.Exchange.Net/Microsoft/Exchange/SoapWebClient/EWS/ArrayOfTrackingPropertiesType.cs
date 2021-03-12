using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200029F RID: 671
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ArrayOfTrackingPropertiesType
	{
		// Token: 0x040011A9 RID: 4521
		[XmlElement("TrackingPropertyType")]
		public TrackingPropertyType[] Items;
	}
}
