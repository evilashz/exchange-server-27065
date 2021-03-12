using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x02000181 RID: 385
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[Serializable]
	public class StatelessCollectionCommandsAdd
	{
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x0001DA7E File Offset: 0x0001BC7E
		// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x0001DA86 File Offset: 0x0001BC86
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

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0001DA8F File Offset: 0x0001BC8F
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x0001DA97 File Offset: 0x0001BC97
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

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0001DAA0 File Offset: 0x0001BCA0
		// (set) Token: 0x06000AFD RID: 2813 RVA: 0x0001DAA8 File Offset: 0x0001BCA8
		public ApplicationDataTypeRequest ApplicationData
		{
			get
			{
				return this.applicationDataField;
			}
			set
			{
				this.applicationDataField = value;
			}
		}

		// Token: 0x04000635 RID: 1589
		private string clientIdField;

		// Token: 0x04000636 RID: 1590
		private string serverIdField;

		// Token: 0x04000637 RID: 1591
		private ApplicationDataTypeRequest applicationDataField;
	}
}
