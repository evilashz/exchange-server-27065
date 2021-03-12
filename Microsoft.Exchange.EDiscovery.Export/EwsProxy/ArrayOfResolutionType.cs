using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000250 RID: 592
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class ArrayOfResolutionType
	{
		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x0600161B RID: 5659 RVA: 0x00026A59 File Offset: 0x00024C59
		// (set) Token: 0x0600161C RID: 5660 RVA: 0x00026A61 File Offset: 0x00024C61
		[XmlElement("Resolution")]
		public ResolutionType[] Resolution
		{
			get
			{
				return this.resolutionField;
			}
			set
			{
				this.resolutionField = value;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x0600161D RID: 5661 RVA: 0x00026A6A File Offset: 0x00024C6A
		// (set) Token: 0x0600161E RID: 5662 RVA: 0x00026A72 File Offset: 0x00024C72
		[XmlAttribute]
		public int IndexedPagingOffset
		{
			get
			{
				return this.indexedPagingOffsetField;
			}
			set
			{
				this.indexedPagingOffsetField = value;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x00026A7B File Offset: 0x00024C7B
		// (set) Token: 0x06001620 RID: 5664 RVA: 0x00026A83 File Offset: 0x00024C83
		[XmlIgnore]
		public bool IndexedPagingOffsetSpecified
		{
			get
			{
				return this.indexedPagingOffsetFieldSpecified;
			}
			set
			{
				this.indexedPagingOffsetFieldSpecified = value;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x00026A8C File Offset: 0x00024C8C
		// (set) Token: 0x06001622 RID: 5666 RVA: 0x00026A94 File Offset: 0x00024C94
		[XmlAttribute]
		public int NumeratorOffset
		{
			get
			{
				return this.numeratorOffsetField;
			}
			set
			{
				this.numeratorOffsetField = value;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x00026A9D File Offset: 0x00024C9D
		// (set) Token: 0x06001624 RID: 5668 RVA: 0x00026AA5 File Offset: 0x00024CA5
		[XmlIgnore]
		public bool NumeratorOffsetSpecified
		{
			get
			{
				return this.numeratorOffsetFieldSpecified;
			}
			set
			{
				this.numeratorOffsetFieldSpecified = value;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x00026AAE File Offset: 0x00024CAE
		// (set) Token: 0x06001626 RID: 5670 RVA: 0x00026AB6 File Offset: 0x00024CB6
		[XmlAttribute]
		public int AbsoluteDenominator
		{
			get
			{
				return this.absoluteDenominatorField;
			}
			set
			{
				this.absoluteDenominatorField = value;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x00026ABF File Offset: 0x00024CBF
		// (set) Token: 0x06001628 RID: 5672 RVA: 0x00026AC7 File Offset: 0x00024CC7
		[XmlIgnore]
		public bool AbsoluteDenominatorSpecified
		{
			get
			{
				return this.absoluteDenominatorFieldSpecified;
			}
			set
			{
				this.absoluteDenominatorFieldSpecified = value;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x00026AD0 File Offset: 0x00024CD0
		// (set) Token: 0x0600162A RID: 5674 RVA: 0x00026AD8 File Offset: 0x00024CD8
		[XmlAttribute]
		public bool IncludesLastItemInRange
		{
			get
			{
				return this.includesLastItemInRangeField;
			}
			set
			{
				this.includesLastItemInRangeField = value;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x00026AE1 File Offset: 0x00024CE1
		// (set) Token: 0x0600162C RID: 5676 RVA: 0x00026AE9 File Offset: 0x00024CE9
		[XmlIgnore]
		public bool IncludesLastItemInRangeSpecified
		{
			get
			{
				return this.includesLastItemInRangeFieldSpecified;
			}
			set
			{
				this.includesLastItemInRangeFieldSpecified = value;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x00026AF2 File Offset: 0x00024CF2
		// (set) Token: 0x0600162E RID: 5678 RVA: 0x00026AFA File Offset: 0x00024CFA
		[XmlAttribute]
		public int TotalItemsInView
		{
			get
			{
				return this.totalItemsInViewField;
			}
			set
			{
				this.totalItemsInViewField = value;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x00026B03 File Offset: 0x00024D03
		// (set) Token: 0x06001630 RID: 5680 RVA: 0x00026B0B File Offset: 0x00024D0B
		[XmlIgnore]
		public bool TotalItemsInViewSpecified
		{
			get
			{
				return this.totalItemsInViewFieldSpecified;
			}
			set
			{
				this.totalItemsInViewFieldSpecified = value;
			}
		}

		// Token: 0x04000F2D RID: 3885
		private ResolutionType[] resolutionField;

		// Token: 0x04000F2E RID: 3886
		private int indexedPagingOffsetField;

		// Token: 0x04000F2F RID: 3887
		private bool indexedPagingOffsetFieldSpecified;

		// Token: 0x04000F30 RID: 3888
		private int numeratorOffsetField;

		// Token: 0x04000F31 RID: 3889
		private bool numeratorOffsetFieldSpecified;

		// Token: 0x04000F32 RID: 3890
		private int absoluteDenominatorField;

		// Token: 0x04000F33 RID: 3891
		private bool absoluteDenominatorFieldSpecified;

		// Token: 0x04000F34 RID: 3892
		private bool includesLastItemInRangeField;

		// Token: 0x04000F35 RID: 3893
		private bool includesLastItemInRangeFieldSpecified;

		// Token: 0x04000F36 RID: 3894
		private int totalItemsInViewField;

		// Token: 0x04000F37 RID: 3895
		private bool totalItemsInViewFieldSpecified;
	}
}
