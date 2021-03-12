using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000013 RID: 19
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "AddTenant", Namespace = "http://tempuri.org/")]
	public class AddTenant : IExtensibleDataObject
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002939 File Offset: 0x00000B39
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002941 File Offset: 0x00000B41
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000294A File Offset: 0x00000B4A
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002952 File Offset: 0x00000B52
		[DataMember]
		public Tenant tenant
		{
			get
			{
				return this.tenantField;
			}
			set
			{
				this.tenantField = value;
			}
		}

		// Token: 0x04000030 RID: 48
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000031 RID: 49
		private Tenant tenantField;
	}
}
