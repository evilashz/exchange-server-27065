using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000032 RID: 50
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "AddSchedulerTenant", Namespace = "http://tempuri.org/")]
	public class AddSchedulerTenant : IExtensibleDataObject
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000030D5 File Offset: 0x000012D5
		// (set) Token: 0x0600013F RID: 319 RVA: 0x000030DD File Offset: 0x000012DD
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

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000030E6 File Offset: 0x000012E6
		// (set) Token: 0x06000141 RID: 321 RVA: 0x000030EE File Offset: 0x000012EE
		[DataMember]
		public SchedulerTenant tenant
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

		// Token: 0x04000094 RID: 148
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000095 RID: 149
		private SchedulerTenant tenantField;
	}
}
