using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200019B RID: 411
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class OccurrencesRangeType
	{
		// Token: 0x040009AF RID: 2479
		[XmlAttribute]
		public DateTime Start;

		// Token: 0x040009B0 RID: 2480
		[XmlIgnore]
		public bool StartSpecified;

		// Token: 0x040009B1 RID: 2481
		[XmlAttribute]
		public DateTime End;

		// Token: 0x040009B2 RID: 2482
		[XmlIgnore]
		public bool EndSpecified;

		// Token: 0x040009B3 RID: 2483
		[XmlAttribute]
		public int Count;

		// Token: 0x040009B4 RID: 2484
		[XmlIgnore]
		public bool CountSpecified;

		// Token: 0x040009B5 RID: 2485
		[XmlAttribute]
		public bool CompareOriginalStartTime;

		// Token: 0x040009B6 RID: 2486
		[XmlIgnore]
		public bool CompareOriginalStartTimeSpecified;
	}
}
