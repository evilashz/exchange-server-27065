using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x0200019B RID: 411
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class StatelessCollectionResponsesDelete
	{
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0001DF04 File Offset: 0x0001C104
		// (set) Token: 0x06000B7D RID: 2941 RVA: 0x0001DF0C File Offset: 0x0001C10C
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

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x0001DF15 File Offset: 0x0001C115
		// (set) Token: 0x06000B7F RID: 2943 RVA: 0x0001DF1D File Offset: 0x0001C11D
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

		// Token: 0x04000698 RID: 1688
		private string serverIdField;

		// Token: 0x04000699 RID: 1689
		private int statusField;
	}
}
