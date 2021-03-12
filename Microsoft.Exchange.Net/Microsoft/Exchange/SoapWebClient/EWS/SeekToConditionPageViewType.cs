using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003E7 RID: 999
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class SeekToConditionPageViewType : BasePagingType
	{
		// Token: 0x04001599 RID: 5529
		public RestrictionType Condition;

		// Token: 0x0400159A RID: 5530
		[XmlAttribute]
		public IndexBasePointType BasePoint;
	}
}
