using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200004A RID: 74
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "DeleteGroup", Namespace = "http://tempuri.org/")]
	public class DeleteGroup : IExtensibleDataObject
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000362A File Offset: 0x0000182A
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00003632 File Offset: 0x00001832
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

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000363B File Offset: 0x0000183B
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00003643 File Offset: 0x00001843
		[DataMember]
		public string workloadName
		{
			get
			{
				return this.workloadNameField;
			}
			set
			{
				this.workloadNameField = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000364C File Offset: 0x0000184C
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00003654 File Offset: 0x00001854
		[DataMember(Order = 1)]
		public string[] groupName
		{
			get
			{
				return this.groupNameField;
			}
			set
			{
				this.groupNameField = value;
			}
		}

		// Token: 0x040000D9 RID: 217
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000DA RID: 218
		private string workloadNameField;

		// Token: 0x040000DB RID: 219
		private string[] groupNameField;
	}
}
