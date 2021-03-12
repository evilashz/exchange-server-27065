using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000195 RID: 405
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class RecurringMasterItemIdType : BaseItemIdType
	{
		// Token: 0x040009A4 RID: 2468
		[XmlAttribute]
		public string OccurrenceId;

		// Token: 0x040009A5 RID: 2469
		[XmlAttribute]
		public string ChangeKey;
	}
}
