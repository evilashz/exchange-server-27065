using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000064 RID: 100
	[DataContract(Name = "ReschedulePartnerTenant", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class ReschedulePartnerTenant : IExtensibleDataObject
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00003A66 File Offset: 0x00001C66
		// (set) Token: 0x06000262 RID: 610 RVA: 0x00003A6E File Offset: 0x00001C6E
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

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00003A77 File Offset: 0x00001C77
		// (set) Token: 0x06000264 RID: 612 RVA: 0x00003A7F File Offset: 0x00001C7F
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

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00003A88 File Offset: 0x00001C88
		// (set) Token: 0x06000266 RID: 614 RVA: 0x00003A90 File Offset: 0x00001C90
		[DataMember]
		public DateTime upgradeStartDate
		{
			get
			{
				return this.upgradeStartDateField;
			}
			set
			{
				this.upgradeStartDateField = value;
			}
		}

		// Token: 0x04000115 RID: 277
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000116 RID: 278
		private Guid tenantIdField;

		// Token: 0x04000117 RID: 279
		private DateTime upgradeStartDateField;
	}
}
