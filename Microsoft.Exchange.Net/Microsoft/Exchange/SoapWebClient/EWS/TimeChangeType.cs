using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000228 RID: 552
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class TimeChangeType
	{
		// Token: 0x04000E28 RID: 3624
		[XmlElement(DataType = "duration")]
		public string Offset;

		// Token: 0x04000E29 RID: 3625
		[XmlElement("RelativeYearlyRecurrence", typeof(RelativeYearlyRecurrencePatternType))]
		[XmlElement("AbsoluteDate", typeof(DateTime), DataType = "date")]
		public object Item;

		// Token: 0x04000E2A RID: 3626
		[XmlElement(DataType = "time")]
		public DateTime Time;

		// Token: 0x04000E2B RID: 3627
		[XmlAttribute]
		public string TimeZoneName;
	}
}
