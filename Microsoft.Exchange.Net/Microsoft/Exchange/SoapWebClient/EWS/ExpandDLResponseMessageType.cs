using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200031B RID: 795
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class ExpandDLResponseMessageType : ResponseMessageType
	{
		// Token: 0x04001326 RID: 4902
		public ArrayOfDLExpansionType DLExpansion;

		// Token: 0x04001327 RID: 4903
		[XmlAttribute]
		public int IndexedPagingOffset;

		// Token: 0x04001328 RID: 4904
		[XmlIgnore]
		public bool IndexedPagingOffsetSpecified;

		// Token: 0x04001329 RID: 4905
		[XmlAttribute]
		public int NumeratorOffset;

		// Token: 0x0400132A RID: 4906
		[XmlIgnore]
		public bool NumeratorOffsetSpecified;

		// Token: 0x0400132B RID: 4907
		[XmlAttribute]
		public int AbsoluteDenominator;

		// Token: 0x0400132C RID: 4908
		[XmlIgnore]
		public bool AbsoluteDenominatorSpecified;

		// Token: 0x0400132D RID: 4909
		[XmlAttribute]
		public bool IncludesLastItemInRange;

		// Token: 0x0400132E RID: 4910
		[XmlIgnore]
		public bool IncludesLastItemInRangeSpecified;

		// Token: 0x0400132F RID: 4911
		[XmlAttribute]
		public int TotalItemsInView;

		// Token: 0x04001330 RID: 4912
		[XmlIgnore]
		public bool TotalItemsInViewSpecified;
	}
}
