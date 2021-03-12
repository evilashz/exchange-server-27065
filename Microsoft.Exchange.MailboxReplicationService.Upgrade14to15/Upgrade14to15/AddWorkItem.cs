using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000020 RID: 32
	[DataContract(Name = "AddWorkItem", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class AddWorkItem : IExtensibleDataObject
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00002D48 File Offset: 0x00000F48
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00002D50 File Offset: 0x00000F50
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

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00002D59 File Offset: 0x00000F59
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00002D61 File Offset: 0x00000F61
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

		// Token: 0x04000067 RID: 103
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000068 RID: 104
		private WorkItem workItemField;
	}
}
