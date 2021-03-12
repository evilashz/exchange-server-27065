using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200035C RID: 860
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class SerializableTimeZone
	{
		// Token: 0x0400142F RID: 5167
		public int Bias;

		// Token: 0x04001430 RID: 5168
		public SerializableTimeZoneTime StandardTime;

		// Token: 0x04001431 RID: 5169
		public SerializableTimeZoneTime DaylightTime;
	}
}
