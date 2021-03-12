using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000297 RID: 663
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class RulePredicateSizeRangeType
	{
		// Token: 0x04001175 RID: 4469
		public int MinimumSize;

		// Token: 0x04001176 RID: 4470
		[XmlIgnore]
		public bool MinimumSizeSpecified;

		// Token: 0x04001177 RID: 4471
		public int MaximumSize;

		// Token: 0x04001178 RID: 4472
		[XmlIgnore]
		public bool MaximumSizeSpecified;
	}
}
