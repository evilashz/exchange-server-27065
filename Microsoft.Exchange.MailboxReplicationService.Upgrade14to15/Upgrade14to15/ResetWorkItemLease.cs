using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200002C RID: 44
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ResetWorkItemLease", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	public class ResetWorkItemLease : IExtensibleDataObject
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00002FEA File Offset: 0x000011EA
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00002FF2 File Offset: 0x000011F2
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

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00002FFB File Offset: 0x000011FB
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00003003 File Offset: 0x00001203
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

		// Token: 0x04000089 RID: 137
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400008A RID: 138
		private string workItemIdField;
	}
}
