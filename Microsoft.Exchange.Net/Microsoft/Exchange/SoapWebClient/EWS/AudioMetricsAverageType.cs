using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001A6 RID: 422
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class AudioMetricsAverageType
	{
		// Token: 0x040009DB RID: 2523
		public double TotalValue;

		// Token: 0x040009DC RID: 2524
		public double TotalCount;
	}
}
