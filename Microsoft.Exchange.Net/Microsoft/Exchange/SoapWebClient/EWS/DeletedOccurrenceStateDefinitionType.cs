using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200039E RID: 926
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DeletedOccurrenceStateDefinitionType : BaseCalendarItemStateDefinitionType
	{
		// Token: 0x040014A2 RID: 5282
		public DateTime OccurrenceDate;

		// Token: 0x040014A3 RID: 5283
		public bool IsOccurrencePresent;

		// Token: 0x040014A4 RID: 5284
		[XmlIgnore]
		public bool IsOccurrencePresentSpecified;
	}
}
