using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000309 RID: 777
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class IndexedPageViewType : BasePagingType
	{
		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x0002889C File Offset: 0x00026A9C
		// (set) Token: 0x060019B5 RID: 6581 RVA: 0x000288A4 File Offset: 0x00026AA4
		[XmlAttribute]
		public int Offset
		{
			get
			{
				return this.offsetField;
			}
			set
			{
				this.offsetField = value;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x000288AD File Offset: 0x00026AAD
		// (set) Token: 0x060019B7 RID: 6583 RVA: 0x000288B5 File Offset: 0x00026AB5
		[XmlAttribute]
		public IndexBasePointType BasePoint
		{
			get
			{
				return this.basePointField;
			}
			set
			{
				this.basePointField = value;
			}
		}

		// Token: 0x0400114E RID: 4430
		private int offsetField;

		// Token: 0x0400114F RID: 4431
		private IndexBasePointType basePointField;
	}
}
