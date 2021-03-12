using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x0200017B RID: 379
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[DesignerCategory("code")]
	[Serializable]
	public class StatelessCollectionGetSort
	{
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0001D99B File Offset: 0x0001BB9B
		// (set) Token: 0x06000ADE RID: 2782 RVA: 0x0001D9A3 File Offset: 0x0001BBA3
		public string SortBy
		{
			get
			{
				return this.sortByField;
			}
			set
			{
				this.sortByField = value;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0001D9AC File Offset: 0x0001BBAC
		// (set) Token: 0x06000AE0 RID: 2784 RVA: 0x0001D9B4 File Offset: 0x0001BBB4
		public SortOrderType SortOrder
		{
			get
			{
				return this.sortOrderField;
			}
			set
			{
				this.sortOrderField = value;
			}
		}

		// Token: 0x04000627 RID: 1575
		private string sortByField;

		// Token: 0x04000628 RID: 1576
		private SortOrderType sortOrderField;
	}
}
