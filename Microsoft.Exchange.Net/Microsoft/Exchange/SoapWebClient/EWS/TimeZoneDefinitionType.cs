using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000188 RID: 392
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class TimeZoneDefinitionType
	{
		// Token: 0x040007B0 RID: 1968
		[XmlArrayItem("Period", IsNullable = false)]
		public PeriodType[] Periods;

		// Token: 0x040007B1 RID: 1969
		[XmlArrayItem("TransitionsGroup", IsNullable = false)]
		public ArrayOfTransitionsType[] TransitionsGroups;

		// Token: 0x040007B2 RID: 1970
		public ArrayOfTransitionsType Transitions;

		// Token: 0x040007B3 RID: 1971
		[XmlAttribute]
		public string Id;

		// Token: 0x040007B4 RID: 1972
		[XmlAttribute]
		public string Name;
	}
}
