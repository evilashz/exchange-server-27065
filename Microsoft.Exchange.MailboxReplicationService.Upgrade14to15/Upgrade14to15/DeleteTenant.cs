using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000018 RID: 24
	[DebuggerStepThrough]
	[DataContract(Name = "DeleteTenant", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class DeleteTenant : IExtensibleDataObject
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00002BF8 File Offset: 0x00000DF8
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00002C00 File Offset: 0x00000E00
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00002C09 File Offset: 0x00000E09
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00002C11 File Offset: 0x00000E11
		[DataMember]
		public Guid tenantId
		{
			get
			{
				return this.tenantIdField;
			}
			set
			{
				this.tenantIdField = value;
			}
		}

		// Token: 0x04000057 RID: 87
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000058 RID: 88
		private Guid tenantIdField;
	}
}
