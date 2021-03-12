using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x02000178 RID: 376
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[Serializable]
	public class StatelessCollectionGetFilterNot
	{
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0001D969 File Offset: 0x0001BB69
		// (set) Token: 0x06000AD8 RID: 2776 RVA: 0x0001D971 File Offset: 0x0001BB71
		public Clause Clause
		{
			get
			{
				return this.clauseField;
			}
			set
			{
				this.clauseField = value;
			}
		}

		// Token: 0x04000620 RID: 1568
		private Clause clauseField;
	}
}
