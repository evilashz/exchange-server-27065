using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200019A RID: 410
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class RecurringMasterItemIdRangesType : ItemIdType
	{
		// Token: 0x040009AE RID: 2478
		[XmlArrayItem("Range", IsNullable = false)]
		public OccurrencesRangeType[] Ranges;
	}
}
