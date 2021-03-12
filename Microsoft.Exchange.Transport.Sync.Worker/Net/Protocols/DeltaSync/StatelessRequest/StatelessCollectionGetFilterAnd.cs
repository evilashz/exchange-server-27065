using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x02000177 RID: 375
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[Serializable]
	public class StatelessCollectionGetFilterAnd
	{
		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0001D950 File Offset: 0x0001BB50
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x0001D958 File Offset: 0x0001BB58
		[XmlElement("Clause")]
		public Clause[] Clause
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

		// Token: 0x0400061F RID: 1567
		private Clause[] clauseField;
	}
}
