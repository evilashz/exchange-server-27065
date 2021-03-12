using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200001E RID: 30
	[DataContract(Name = "UpdateTenantUpgradeStartDate", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class UpdateTenantUpgradeStartDate : IExtensibleDataObject
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00002CF4 File Offset: 0x00000EF4
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00002CFC File Offset: 0x00000EFC
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00002D05 File Offset: 0x00000F05
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00002D0D File Offset: 0x00000F0D
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00002D16 File Offset: 0x00000F16
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00002D1E File Offset: 0x00000F1E
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

		// Token: 0x04000063 RID: 99
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000064 RID: 100
		private Guid tenantIdField;

		// Token: 0x04000065 RID: 101
		private DateTime upgradeStartDateField;
	}
}
