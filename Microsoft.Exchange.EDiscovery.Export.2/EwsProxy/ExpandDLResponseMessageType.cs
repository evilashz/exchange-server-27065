using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200023A RID: 570
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class ExpandDLResponseMessageType : ResponseMessageType
	{
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x000265A3 File Offset: 0x000247A3
		// (set) Token: 0x0600158D RID: 5517 RVA: 0x000265AB File Offset: 0x000247AB
		public ArrayOfDLExpansionType DLExpansion
		{
			get
			{
				return this.dLExpansionField;
			}
			set
			{
				this.dLExpansionField = value;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x000265B4 File Offset: 0x000247B4
		// (set) Token: 0x0600158F RID: 5519 RVA: 0x000265BC File Offset: 0x000247BC
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

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x000265C5 File Offset: 0x000247C5
		// (set) Token: 0x06001591 RID: 5521 RVA: 0x000265CD File Offset: 0x000247CD
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

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x000265D6 File Offset: 0x000247D6
		// (set) Token: 0x06001593 RID: 5523 RVA: 0x000265DE File Offset: 0x000247DE
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

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x000265E7 File Offset: 0x000247E7
		// (set) Token: 0x06001595 RID: 5525 RVA: 0x000265EF File Offset: 0x000247EF
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

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x000265F8 File Offset: 0x000247F8
		// (set) Token: 0x06001597 RID: 5527 RVA: 0x00026600 File Offset: 0x00024800
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

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x00026609 File Offset: 0x00024809
		// (set) Token: 0x06001599 RID: 5529 RVA: 0x00026611 File Offset: 0x00024811
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

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x0002661A File Offset: 0x0002481A
		// (set) Token: 0x0600159B RID: 5531 RVA: 0x00026622 File Offset: 0x00024822
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

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x0002662B File Offset: 0x0002482B
		// (set) Token: 0x0600159D RID: 5533 RVA: 0x00026633 File Offset: 0x00024833
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

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x0002663C File Offset: 0x0002483C
		// (set) Token: 0x0600159F RID: 5535 RVA: 0x00026644 File Offset: 0x00024844
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

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x0002664D File Offset: 0x0002484D
		// (set) Token: 0x060015A1 RID: 5537 RVA: 0x00026655 File Offset: 0x00024855
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

		// Token: 0x04000ED4 RID: 3796
		private ArrayOfDLExpansionType dLExpansionField;

		// Token: 0x04000ED5 RID: 3797
		private int indexedPagingOffsetField;

		// Token: 0x04000ED6 RID: 3798
		private bool indexedPagingOffsetFieldSpecified;

		// Token: 0x04000ED7 RID: 3799
		private int numeratorOffsetField;

		// Token: 0x04000ED8 RID: 3800
		private bool numeratorOffsetFieldSpecified;

		// Token: 0x04000ED9 RID: 3801
		private int absoluteDenominatorField;

		// Token: 0x04000EDA RID: 3802
		private bool absoluteDenominatorFieldSpecified;

		// Token: 0x04000EDB RID: 3803
		private bool includesLastItemInRangeField;

		// Token: 0x04000EDC RID: 3804
		private bool includesLastItemInRangeFieldSpecified;

		// Token: 0x04000EDD RID: 3805
		private int totalItemsInViewField;

		// Token: 0x04000EDE RID: 3806
		private bool totalItemsInViewFieldSpecified;
	}
}
