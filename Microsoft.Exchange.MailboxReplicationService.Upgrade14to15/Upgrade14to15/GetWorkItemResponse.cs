using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000029 RID: 41
	[DebuggerStepThrough]
	[DataContract(Name = "GetWorkItemResponse", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetWorkItemResponse : IExtensibleDataObject
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00002F6C File Offset: 0x0000116C
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00002F74 File Offset: 0x00001174
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

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00002F7D File Offset: 0x0000117D
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00002F85 File Offset: 0x00001185
		[DataMember]
		public WorkItem GetWorkItemResult
		{
			get
			{
				return this.GetWorkItemResultField;
			}
			set
			{
				this.GetWorkItemResultField = value;
			}
		}

		// Token: 0x04000083 RID: 131
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000084 RID: 132
		private WorkItem GetWorkItemResultField;
	}
}
