using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x02000196 RID: 406
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[Serializable]
	public class StatelessCollectionCommandsDelete
	{
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x0001DE32 File Offset: 0x0001C032
		// (set) Token: 0x06000B64 RID: 2916 RVA: 0x0001DE3A File Offset: 0x0001C03A
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

		// Token: 0x0400068E RID: 1678
		private string serverIdField;
	}
}
