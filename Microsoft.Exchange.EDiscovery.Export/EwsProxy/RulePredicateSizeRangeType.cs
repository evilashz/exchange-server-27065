using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001B6 RID: 438
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class RulePredicateSizeRangeType
	{
		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x00024E14 File Offset: 0x00023014
		// (set) Token: 0x060012C1 RID: 4801 RVA: 0x00024E1C File Offset: 0x0002301C
		public int MinimumSize
		{
			get
			{
				return this.minimumSizeField;
			}
			set
			{
				this.minimumSizeField = value;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x00024E25 File Offset: 0x00023025
		// (set) Token: 0x060012C3 RID: 4803 RVA: 0x00024E2D File Offset: 0x0002302D
		[XmlIgnore]
		public bool MinimumSizeSpecified
		{
			get
			{
				return this.minimumSizeFieldSpecified;
			}
			set
			{
				this.minimumSizeFieldSpecified = value;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00024E36 File Offset: 0x00023036
		// (set) Token: 0x060012C5 RID: 4805 RVA: 0x00024E3E File Offset: 0x0002303E
		public int MaximumSize
		{
			get
			{
				return this.maximumSizeField;
			}
			set
			{
				this.maximumSizeField = value;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x00024E47 File Offset: 0x00023047
		// (set) Token: 0x060012C7 RID: 4807 RVA: 0x00024E4F File Offset: 0x0002304F
		[XmlIgnore]
		public bool MaximumSizeSpecified
		{
			get
			{
				return this.maximumSizeFieldSpecified;
			}
			set
			{
				this.maximumSizeFieldSpecified = value;
			}
		}

		// Token: 0x04000D23 RID: 3363
		private int minimumSizeField;

		// Token: 0x04000D24 RID: 3364
		private bool minimumSizeFieldSpecified;

		// Token: 0x04000D25 RID: 3365
		private int maximumSizeField;

		// Token: 0x04000D26 RID: 3366
		private bool maximumSizeFieldSpecified;
	}
}
