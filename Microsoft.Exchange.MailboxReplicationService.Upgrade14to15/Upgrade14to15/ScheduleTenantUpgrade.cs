using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000030 RID: 48
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ScheduleTenantUpgrade", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	public class ScheduleTenantUpgrade : IExtensibleDataObject
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00003081 File Offset: 0x00001281
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00003089 File Offset: 0x00001289
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

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00003092 File Offset: 0x00001292
		// (set) Token: 0x06000137 RID: 311 RVA: 0x0000309A File Offset: 0x0000129A
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

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000030A3 File Offset: 0x000012A3
		// (set) Token: 0x06000139 RID: 313 RVA: 0x000030AB File Offset: 0x000012AB
		[DataMember]
		public DateTime upgradeDate
		{
			get
			{
				return this.upgradeDateField;
			}
			set
			{
				this.upgradeDateField = value;
			}
		}

		// Token: 0x04000090 RID: 144
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000091 RID: 145
		private Guid tenantIdField;

		// Token: 0x04000092 RID: 146
		private DateTime upgradeDateField;
	}
}
