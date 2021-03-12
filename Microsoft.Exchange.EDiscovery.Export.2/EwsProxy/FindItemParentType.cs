using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000256 RID: 598
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class FindItemParentType
	{
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x00026BD5 File Offset: 0x00024DD5
		// (set) Token: 0x06001649 RID: 5705 RVA: 0x00026BDD File Offset: 0x00024DDD
		[XmlElement("Groups", typeof(ArrayOfGroupedItemsType))]
		[XmlElement("Items", typeof(ArrayOfRealItemsType))]
		public object Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x00026BE6 File Offset: 0x00024DE6
		// (set) Token: 0x0600164B RID: 5707 RVA: 0x00026BEE File Offset: 0x00024DEE
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

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x00026BF7 File Offset: 0x00024DF7
		// (set) Token: 0x0600164D RID: 5709 RVA: 0x00026BFF File Offset: 0x00024DFF
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

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x00026C08 File Offset: 0x00024E08
		// (set) Token: 0x0600164F RID: 5711 RVA: 0x00026C10 File Offset: 0x00024E10
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

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x00026C19 File Offset: 0x00024E19
		// (set) Token: 0x06001651 RID: 5713 RVA: 0x00026C21 File Offset: 0x00024E21
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

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x00026C2A File Offset: 0x00024E2A
		// (set) Token: 0x06001653 RID: 5715 RVA: 0x00026C32 File Offset: 0x00024E32
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

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x00026C3B File Offset: 0x00024E3B
		// (set) Token: 0x06001655 RID: 5717 RVA: 0x00026C43 File Offset: 0x00024E43
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

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x00026C4C File Offset: 0x00024E4C
		// (set) Token: 0x06001657 RID: 5719 RVA: 0x00026C54 File Offset: 0x00024E54
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

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x00026C5D File Offset: 0x00024E5D
		// (set) Token: 0x06001659 RID: 5721 RVA: 0x00026C65 File Offset: 0x00024E65
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

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x00026C6E File Offset: 0x00024E6E
		// (set) Token: 0x0600165B RID: 5723 RVA: 0x00026C76 File Offset: 0x00024E76
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

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x00026C7F File Offset: 0x00024E7F
		// (set) Token: 0x0600165D RID: 5725 RVA: 0x00026C87 File Offset: 0x00024E87
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

		// Token: 0x04000F45 RID: 3909
		private object itemField;

		// Token: 0x04000F46 RID: 3910
		private int indexedPagingOffsetField;

		// Token: 0x04000F47 RID: 3911
		private bool indexedPagingOffsetFieldSpecified;

		// Token: 0x04000F48 RID: 3912
		private int numeratorOffsetField;

		// Token: 0x04000F49 RID: 3913
		private bool numeratorOffsetFieldSpecified;

		// Token: 0x04000F4A RID: 3914
		private int absoluteDenominatorField;

		// Token: 0x04000F4B RID: 3915
		private bool absoluteDenominatorFieldSpecified;

		// Token: 0x04000F4C RID: 3916
		private bool includesLastItemInRangeField;

		// Token: 0x04000F4D RID: 3917
		private bool includesLastItemInRangeFieldSpecified;

		// Token: 0x04000F4E RID: 3918
		private int totalItemsInViewField;

		// Token: 0x04000F4F RID: 3919
		private bool totalItemsInViewFieldSpecified;
	}
}
