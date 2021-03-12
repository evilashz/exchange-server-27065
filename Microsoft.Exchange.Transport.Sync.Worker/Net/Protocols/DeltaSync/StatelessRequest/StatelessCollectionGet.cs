using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x02000175 RID: 373
	[DebuggerStepThrough]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DesignerCategory("code")]
	[Serializable]
	public class StatelessCollectionGet
	{
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x0001D8FC File Offset: 0x0001BAFC
		// (set) Token: 0x06000ACB RID: 2763 RVA: 0x0001D904 File Offset: 0x0001BB04
		public StatelessCollectionGetFilter Filter
		{
			get
			{
				return this.filterField;
			}
			set
			{
				this.filterField = value;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0001D90D File Offset: 0x0001BB0D
		// (set) Token: 0x06000ACD RID: 2765 RVA: 0x0001D915 File Offset: 0x0001BB15
		public StatelessCollectionGetSort Sort
		{
			get
			{
				return this.sortField;
			}
			set
			{
				this.sortField = value;
			}
		}

		// Token: 0x0400061B RID: 1563
		private StatelessCollectionGetFilter filterField;

		// Token: 0x0400061C RID: 1564
		private StatelessCollectionGetSort sortField;
	}
}
