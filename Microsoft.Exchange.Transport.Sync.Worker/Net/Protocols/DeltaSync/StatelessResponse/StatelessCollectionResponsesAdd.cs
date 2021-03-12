using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x0200019C RID: 412
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[Serializable]
	public class StatelessCollectionResponsesAdd
	{
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0001DF2E File Offset: 0x0001C12E
		// (set) Token: 0x06000B82 RID: 2946 RVA: 0x0001DF36 File Offset: 0x0001C136
		public string ClientId
		{
			get
			{
				return this.clientIdField;
			}
			set
			{
				this.clientIdField = value;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x0001DF3F File Offset: 0x0001C13F
		// (set) Token: 0x06000B84 RID: 2948 RVA: 0x0001DF47 File Offset: 0x0001C147
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

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x0001DF50 File Offset: 0x0001C150
		// (set) Token: 0x06000B86 RID: 2950 RVA: 0x0001DF58 File Offset: 0x0001C158
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

		// Token: 0x0400069A RID: 1690
		private string clientIdField;

		// Token: 0x0400069B RID: 1691
		private string serverIdField;

		// Token: 0x0400069C RID: 1692
		private int statusField;
	}
}
