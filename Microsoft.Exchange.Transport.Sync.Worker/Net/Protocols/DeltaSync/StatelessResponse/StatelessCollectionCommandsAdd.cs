using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x02000198 RID: 408
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[DebuggerStepThrough]
	[Serializable]
	public class StatelessCollectionCommandsAdd
	{
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x0001DE75 File Offset: 0x0001C075
		// (set) Token: 0x06000B6C RID: 2924 RVA: 0x0001DE7D File Offset: 0x0001C07D
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

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0001DE86 File Offset: 0x0001C086
		// (set) Token: 0x06000B6E RID: 2926 RVA: 0x0001DE8E File Offset: 0x0001C08E
		public ApplicationDataTypeResponse ApplicationData
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

		// Token: 0x04000691 RID: 1681
		private string serverIdField;

		// Token: 0x04000692 RID: 1682
		private ApplicationDataTypeResponse applicationDataField;
	}
}
