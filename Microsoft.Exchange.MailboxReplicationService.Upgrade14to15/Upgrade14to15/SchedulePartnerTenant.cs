using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000058 RID: 88
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "SchedulePartnerTenant", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	public class SchedulePartnerTenant : IExtensibleDataObject
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00003819 File Offset: 0x00001A19
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00003821 File Offset: 0x00001A21
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

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000382A File Offset: 0x00001A2A
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00003832 File Offset: 0x00001A32
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

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000383B File Offset: 0x00001A3B
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00003843 File Offset: 0x00001A43
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

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000384C File Offset: 0x00001A4C
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00003854 File Offset: 0x00001A54
		[DataMember(Order = 2)]
		public Tuple<string[], string>[] redirectWorkLoads
		{
			get
			{
				return this.redirectWorkLoadsField;
			}
			set
			{
				this.redirectWorkLoadsField = value;
			}
		}

		// Token: 0x040000F8 RID: 248
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000F9 RID: 249
		private Tenant tenantField;

		// Token: 0x040000FA RID: 250
		private DateTime upgradeStartDateField;

		// Token: 0x040000FB RID: 251
		private Tuple<string[], string>[] redirectWorkLoadsField;
	}
}
