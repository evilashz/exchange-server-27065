using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200031C RID: 796
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class ArrayOfDLExpansionType
	{
		// Token: 0x04001331 RID: 4913
		[XmlElement("Mailbox")]
		public EmailAddressType[] Mailbox;

		// Token: 0x04001332 RID: 4914
		[XmlAttribute]
		public int IndexedPagingOffset;

		// Token: 0x04001333 RID: 4915
		[XmlIgnore]
		public bool IndexedPagingOffsetSpecified;

		// Token: 0x04001334 RID: 4916
		[XmlAttribute]
		public int NumeratorOffset;

		// Token: 0x04001335 RID: 4917
		[XmlIgnore]
		public bool NumeratorOffsetSpecified;

		// Token: 0x04001336 RID: 4918
		[XmlAttribute]
		public int AbsoluteDenominator;

		// Token: 0x04001337 RID: 4919
		[XmlIgnore]
		public bool AbsoluteDenominatorSpecified;

		// Token: 0x04001338 RID: 4920
		[XmlAttribute]
		public bool IncludesLastItemInRange;

		// Token: 0x04001339 RID: 4921
		[XmlIgnore]
		public bool IncludesLastItemInRangeSpecified;

		// Token: 0x0400133A RID: 4922
		[XmlAttribute]
		public int TotalItemsInView;

		// Token: 0x0400133B RID: 4923
		[XmlIgnore]
		public bool TotalItemsInViewSpecified;
	}
}
