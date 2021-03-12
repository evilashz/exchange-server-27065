using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000227 RID: 551
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class TimeZoneType
	{
		// Token: 0x04000E24 RID: 3620
		[XmlElement(DataType = "duration")]
		public string BaseOffset;

		// Token: 0x04000E25 RID: 3621
		public TimeChangeType Standard;

		// Token: 0x04000E26 RID: 3622
		public TimeChangeType Daylight;

		// Token: 0x04000E27 RID: 3623
		[XmlAttribute]
		public string TimeZoneName;
	}
}
