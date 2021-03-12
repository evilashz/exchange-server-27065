using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200001B RID: 27
	[DebuggerStepThrough]
	[DataContract(Name = "GetTenantResponse", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetTenantResponse : IExtensibleDataObject
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002C65 File Offset: 0x00000E65
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00002C6D File Offset: 0x00000E6D
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002C76 File Offset: 0x00000E76
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00002C7E File Offset: 0x00000E7E
		[DataMember]
		public Tenant GetTenantResult
		{
			get
			{
				return this.GetTenantResultField;
			}
			set
			{
				this.GetTenantResultField = value;
			}
		}

		// Token: 0x0400005C RID: 92
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400005D RID: 93
		private Tenant GetTenantResultField;
	}
}
