using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000046 RID: 70
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "AddMsodsTenant", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	public class AddMsodsTenant : IExtensibleDataObject
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00003582 File Offset: 0x00001782
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000358A File Offset: 0x0000178A
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00003593 File Offset: 0x00001793
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000359B File Offset: 0x0000179B
		[DataMember]
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

		// Token: 0x040000D1 RID: 209
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000D2 RID: 210
		private string tenantDomainPrefixField;
	}
}
