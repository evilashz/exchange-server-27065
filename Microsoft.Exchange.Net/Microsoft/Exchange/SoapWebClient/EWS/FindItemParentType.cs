using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000337 RID: 823
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class FindItemParentType
	{
		// Token: 0x04001397 RID: 5015
		[XmlElement("Items", typeof(ArrayOfRealItemsType))]
		[XmlElement("Groups", typeof(ArrayOfGroupedItemsType))]
		public object Item;

		// Token: 0x04001398 RID: 5016
		[XmlAttribute]
		public int IndexedPagingOffset;

		// Token: 0x04001399 RID: 5017
		[XmlIgnore]
		public bool IndexedPagingOffsetSpecified;

		// Token: 0x0400139A RID: 5018
		[XmlAttribute]
		public int NumeratorOffset;

		// Token: 0x0400139B RID: 5019
		[XmlIgnore]
		public bool NumeratorOffsetSpecified;

		// Token: 0x0400139C RID: 5020
		[XmlAttribute]
		public int AbsoluteDenominator;

		// Token: 0x0400139D RID: 5021
		[XmlIgnore]
		public bool AbsoluteDenominatorSpecified;

		// Token: 0x0400139E RID: 5022
		[XmlAttribute]
		public bool IncludesLastItemInRange;

		// Token: 0x0400139F RID: 5023
		[XmlIgnore]
		public bool IncludesLastItemInRangeSpecified;

		// Token: 0x040013A0 RID: 5024
		[XmlAttribute]
		public int TotalItemsInView;

		// Token: 0x040013A1 RID: 5025
		[XmlIgnore]
		public bool TotalItemsInViewSpecified;
	}
}
