using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200023B RID: 571
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class ArrayOfDLExpansionType
	{
		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x00026666 File Offset: 0x00024866
		// (set) Token: 0x060015A4 RID: 5540 RVA: 0x0002666E File Offset: 0x0002486E
		[XmlElement("Mailbox")]
		public EmailAddressType[] Mailbox
		{
			get
			{
				return this.mailboxField;
			}
			set
			{
				this.mailboxField = value;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x00026677 File Offset: 0x00024877
		// (set) Token: 0x060015A6 RID: 5542 RVA: 0x0002667F File Offset: 0x0002487F
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

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x00026688 File Offset: 0x00024888
		// (set) Token: 0x060015A8 RID: 5544 RVA: 0x00026690 File Offset: 0x00024890
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

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x00026699 File Offset: 0x00024899
		// (set) Token: 0x060015AA RID: 5546 RVA: 0x000266A1 File Offset: 0x000248A1
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

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x000266AA File Offset: 0x000248AA
		// (set) Token: 0x060015AC RID: 5548 RVA: 0x000266B2 File Offset: 0x000248B2
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

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x000266BB File Offset: 0x000248BB
		// (set) Token: 0x060015AE RID: 5550 RVA: 0x000266C3 File Offset: 0x000248C3
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

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x000266CC File Offset: 0x000248CC
		// (set) Token: 0x060015B0 RID: 5552 RVA: 0x000266D4 File Offset: 0x000248D4
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

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x000266DD File Offset: 0x000248DD
		// (set) Token: 0x060015B2 RID: 5554 RVA: 0x000266E5 File Offset: 0x000248E5
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

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x000266EE File Offset: 0x000248EE
		// (set) Token: 0x060015B4 RID: 5556 RVA: 0x000266F6 File Offset: 0x000248F6
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

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x000266FF File Offset: 0x000248FF
		// (set) Token: 0x060015B6 RID: 5558 RVA: 0x00026707 File Offset: 0x00024907
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

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x00026710 File Offset: 0x00024910
		// (set) Token: 0x060015B8 RID: 5560 RVA: 0x00026718 File Offset: 0x00024918
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

		// Token: 0x04000EDF RID: 3807
		private EmailAddressType[] mailboxField;

		// Token: 0x04000EE0 RID: 3808
		private int indexedPagingOffsetField;

		// Token: 0x04000EE1 RID: 3809
		private bool indexedPagingOffsetFieldSpecified;

		// Token: 0x04000EE2 RID: 3810
		private int numeratorOffsetField;

		// Token: 0x04000EE3 RID: 3811
		private bool numeratorOffsetFieldSpecified;

		// Token: 0x04000EE4 RID: 3812
		private int absoluteDenominatorField;

		// Token: 0x04000EE5 RID: 3813
		private bool absoluteDenominatorFieldSpecified;

		// Token: 0x04000EE6 RID: 3814
		private bool includesLastItemInRangeField;

		// Token: 0x04000EE7 RID: 3815
		private bool includesLastItemInRangeFieldSpecified;

		// Token: 0x04000EE8 RID: 3816
		private int totalItemsInViewField;

		// Token: 0x04000EE9 RID: 3817
		private bool totalItemsInViewFieldSpecified;
	}
}
