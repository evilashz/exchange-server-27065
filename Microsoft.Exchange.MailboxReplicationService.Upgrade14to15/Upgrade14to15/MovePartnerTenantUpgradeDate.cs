using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200005A RID: 90
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "MovePartnerTenantUpgradeDate", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	public class MovePartnerTenantUpgradeDate : IExtensibleDataObject
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000387E File Offset: 0x00001A7E
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00003886 File Offset: 0x00001A86
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

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000388F File Offset: 0x00001A8F
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00003897 File Offset: 0x00001A97
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

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600022B RID: 555 RVA: 0x000038A0 File Offset: 0x00001AA0
		// (set) Token: 0x0600022C RID: 556 RVA: 0x000038A8 File Offset: 0x00001AA8
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

		// Token: 0x040000FD RID: 253
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000FE RID: 254
		private Guid tenantIdField;

		// Token: 0x040000FF RID: 255
		private DateTime upgradeStartDateField;
	}
}
