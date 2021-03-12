using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x02000173 RID: 371
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[XmlRoot(Namespace = "DeltaSyncV2:", IsNullable = false)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[Serializable]
	public class Stateless
	{
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0001D875 File Offset: 0x0001BA75
		// (set) Token: 0x06000ABB RID: 2747 RVA: 0x0001D87D File Offset: 0x0001BA7D
		[XmlArrayItem("Collection", IsNullable = false)]
		public StatelessCollection[] Collections
		{
			get
			{
				return this.collectionsField;
			}
			set
			{
				this.collectionsField = value;
			}
		}

		// Token: 0x04000614 RID: 1556
		private StatelessCollection[] collectionsField;
	}
}
