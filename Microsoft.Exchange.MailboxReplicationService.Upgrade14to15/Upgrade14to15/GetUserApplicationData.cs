using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000040 RID: 64
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GetUserApplicationData", Namespace = "http://tempuri.org/")]
	public class GetUserApplicationData : IExtensibleDataObject
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00003453 File Offset: 0x00001653
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x0000345B File Offset: 0x0000165B
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

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00003464 File Offset: 0x00001664
		// (set) Token: 0x060001AB RID: 427 RVA: 0x0000346C File Offset: 0x0000166C
		[DataMember]
		public Guid tenantGuid
		{
			get
			{
				return this.tenantGuidField;
			}
			set
			{
				this.tenantGuidField = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00003475 File Offset: 0x00001675
		// (set) Token: 0x060001AD RID: 429 RVA: 0x0000347D File Offset: 0x0000167D
		[DataMember]
		public Guid userId
		{
			get
			{
				return this.userIdField;
			}
			set
			{
				this.userIdField = value;
			}
		}

		// Token: 0x040000C2 RID: 194
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000C3 RID: 195
		private Guid tenantGuidField;

		// Token: 0x040000C4 RID: 196
		private Guid userIdField;
	}
}
