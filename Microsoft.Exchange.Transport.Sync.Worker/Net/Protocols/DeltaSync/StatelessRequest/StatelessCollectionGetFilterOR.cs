using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x02000179 RID: 377
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[Serializable]
	public class StatelessCollectionGetFilterOR
	{
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0001D982 File Offset: 0x0001BB82
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x0001D98A File Offset: 0x0001BB8A
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

		// Token: 0x04000621 RID: 1569
		private Clause[] clauseField;
	}
}
