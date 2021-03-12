using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000024 RID: 36
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "UpdateWorkItem", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	public class UpdateWorkItem : IExtensibleDataObject
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00002EBC File Offset: 0x000010BC
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00002EC4 File Offset: 0x000010C4
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

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00002ECD File Offset: 0x000010CD
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00002ED5 File Offset: 0x000010D5
		[DataMember]
		public WorkItem workItem
		{
			get
			{
				return this.workItemField;
			}
			set
			{
				this.workItemField = value;
			}
		}

		// Token: 0x0400007B RID: 123
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400007C RID: 124
		private WorkItem workItemField;
	}
}
