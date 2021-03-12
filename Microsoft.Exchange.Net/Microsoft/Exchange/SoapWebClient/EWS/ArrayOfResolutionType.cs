using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000331 RID: 817
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ArrayOfResolutionType
	{
		// Token: 0x0400137F RID: 4991
		[XmlElement("Resolution")]
		public ResolutionType[] Resolution;

		// Token: 0x04001380 RID: 4992
		[XmlAttribute]
		public int IndexedPagingOffset;

		// Token: 0x04001381 RID: 4993
		[XmlIgnore]
		public bool IndexedPagingOffsetSpecified;

		// Token: 0x04001382 RID: 4994
		[XmlAttribute]
		public int NumeratorOffset;

		// Token: 0x04001383 RID: 4995
		[XmlIgnore]
		public bool NumeratorOffsetSpecified;

		// Token: 0x04001384 RID: 4996
		[XmlAttribute]
		public int AbsoluteDenominator;

		// Token: 0x04001385 RID: 4997
		[XmlIgnore]
		public bool AbsoluteDenominatorSpecified;

		// Token: 0x04001386 RID: 4998
		[XmlAttribute]
		public bool IncludesLastItemInRange;

		// Token: 0x04001387 RID: 4999
		[XmlIgnore]
		public bool IncludesLastItemInRangeSpecified;

		// Token: 0x04001388 RID: 5000
		[XmlAttribute]
		public int TotalItemsInView;

		// Token: 0x04001389 RID: 5001
		[XmlIgnore]
		public bool TotalItemsInViewSpecified;
	}
}
