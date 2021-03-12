using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x0200019A RID: 410
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[Serializable]
	public class StatelessCollectionResponsesChange
	{
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0001DEDA File Offset: 0x0001C0DA
		// (set) Token: 0x06000B78 RID: 2936 RVA: 0x0001DEE2 File Offset: 0x0001C0E2
		public string ServerId
		{
			get
			{
				return this.serverIdField;
			}
			set
			{
				this.serverIdField = value;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0001DEEB File Offset: 0x0001C0EB
		// (set) Token: 0x06000B7A RID: 2938 RVA: 0x0001DEF3 File Offset: 0x0001C0F3
		[XmlElement(Namespace = "HMSYNC:")]
		public int Status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		// Token: 0x04000696 RID: 1686
		private string serverIdField;

		// Token: 0x04000697 RID: 1687
		private int statusField;
	}
}
