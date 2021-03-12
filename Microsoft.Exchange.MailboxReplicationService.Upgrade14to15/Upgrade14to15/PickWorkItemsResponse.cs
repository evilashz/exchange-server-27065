using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000075 RID: 117
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "PickWorkItemsResponse", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	public class PickWorkItemsResponse : IExtensibleDataObject
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00003E62 File Offset: 0x00002062
		// (set) Token: 0x060002DB RID: 731 RVA: 0x00003E6A File Offset: 0x0000206A
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

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00003E73 File Offset: 0x00002073
		// (set) Token: 0x060002DD RID: 733 RVA: 0x00003E7B File Offset: 0x0000207B
		[DataMember]
		public WorkItemPickResult PickWorkItemsResult
		{
			get
			{
				return this.PickWorkItemsResultField;
			}
			set
			{
				this.PickWorkItemsResultField = value;
			}
		}

		// Token: 0x04000149 RID: 329
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400014A RID: 330
		private WorkItemPickResult PickWorkItemsResultField;
	}
}
