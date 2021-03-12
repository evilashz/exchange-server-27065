using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000296 RID: 662
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class RulePredicateDateRangeType
	{
		// Token: 0x04001171 RID: 4465
		public DateTime StartDateTime;

		// Token: 0x04001172 RID: 4466
		[XmlIgnore]
		public bool StartDateTimeSpecified;

		// Token: 0x04001173 RID: 4467
		public DateTime EndDateTime;

		// Token: 0x04001174 RID: 4468
		[XmlIgnore]
		public bool EndDateTimeSpecified;
	}
}
