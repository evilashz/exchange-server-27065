using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200004C RID: 76
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "UpdateCapacity", Namespace = "http://tempuri.org/")]
	public class UpdateCapacity : IExtensibleDataObject
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000367E File Offset: 0x0000187E
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00003686 File Offset: 0x00001886
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

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000368F File Offset: 0x0000188F
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00003697 File Offset: 0x00001897
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

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000036A0 File Offset: 0x000018A0
		// (set) Token: 0x060001EF RID: 495 RVA: 0x000036A8 File Offset: 0x000018A8
		[DataMember(Order = 1)]
		public GroupCapacity capacity
		{
			get
			{
				return this.capacityField;
			}
			set
			{
				this.capacityField = value;
			}
		}

		// Token: 0x040000DD RID: 221
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000DE RID: 222
		private string workloadNameField;

		// Token: 0x040000DF RID: 223
		private GroupCapacity capacityField;
	}
}
