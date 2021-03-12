using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000044 RID: 68
	[DataContract(Name = "AddMsodsUser", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class AddMsodsUser : IExtensibleDataObject
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000351D File Offset: 0x0000171D
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00003525 File Offset: 0x00001725
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

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000352E File Offset: 0x0000172E
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00003536 File Offset: 0x00001736
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

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000353F File Offset: 0x0000173F
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00003547 File Offset: 0x00001747
		[DataMember(Order = 1)]
		public string tenantDomainPrefix
		{
			get
			{
				return this.tenantDomainPrefixField;
			}
			set
			{
				this.tenantDomainPrefixField = value;
			}
		}

		// Token: 0x040000CC RID: 204
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000CD RID: 205
		private Guid tenantIdField;

		// Token: 0x040000CE RID: 206
		private string tenantDomainPrefixField;
	}
}
