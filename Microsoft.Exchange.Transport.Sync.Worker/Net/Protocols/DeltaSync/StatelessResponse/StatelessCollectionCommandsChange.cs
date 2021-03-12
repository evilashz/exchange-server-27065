using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x02000197 RID: 407
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[Serializable]
	public class StatelessCollectionCommandsChange
	{
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0001DE4B File Offset: 0x0001C04B
		// (set) Token: 0x06000B67 RID: 2919 RVA: 0x0001DE53 File Offset: 0x0001C053
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

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0001DE5C File Offset: 0x0001C05C
		// (set) Token: 0x06000B69 RID: 2921 RVA: 0x0001DE64 File Offset: 0x0001C064
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

		// Token: 0x0400068F RID: 1679
		private string serverIdField;

		// Token: 0x04000690 RID: 1680
		private ApplicationDataTypeResponse applicationDataField;
	}
}
