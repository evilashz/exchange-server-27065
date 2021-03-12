using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000028 RID: 40
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "GetWorkItem", Namespace = "http://tempuri.org/")]
	public class GetWorkItem : IExtensibleDataObject
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00002F42 File Offset: 0x00001142
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00002F4A File Offset: 0x0000114A
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

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00002F53 File Offset: 0x00001153
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00002F5B File Offset: 0x0000115B
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

		// Token: 0x04000081 RID: 129
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000082 RID: 130
		private string workItemIdField;
	}
}
