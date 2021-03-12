using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000026 RID: 38
	[DataContract(Name = "DeleteWorkItem", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class DeleteWorkItem : IExtensibleDataObject
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00002EFF File Offset: 0x000010FF
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00002F07 File Offset: 0x00001107
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00002F10 File Offset: 0x00001110
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00002F18 File Offset: 0x00001118
		[DataMember]
		public string workItemId
		{
			get
			{
				return this.workItemIdField;
			}
			set
			{
				this.workItemIdField = value;
			}
		}

		// Token: 0x0400007E RID: 126
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400007F RID: 127
		private string workItemIdField;
	}
}
