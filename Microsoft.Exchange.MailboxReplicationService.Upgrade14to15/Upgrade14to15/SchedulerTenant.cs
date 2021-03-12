using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000033 RID: 51
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "SchedulerTenant", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.ManagementService")]
	public class SchedulerTenant : IExtensibleDataObject
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000030FF File Offset: 0x000012FF
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00003107 File Offset: 0x00001307
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

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00003110 File Offset: 0x00001310
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00003118 File Offset: 0x00001318
		[DataMember]
		public DateTime? CanceledDate
		{
			get
			{
				return this.CanceledDateField;
			}
			set
			{
				this.CanceledDateField = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00003121 File Offset: 0x00001321
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00003129 File Offset: 0x00001329
		[DataMember]
		public string CommunicationCulture
		{
			get
			{
				return this.CommunicationCultureField;
			}
			set
			{
				this.CommunicationCultureField = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00003132 File Offset: 0x00001332
		// (set) Token: 0x0600014A RID: 330 RVA: 0x0000313A File Offset: 0x0000133A
		[DataMember]
		public DataSource DataSource
		{
			get
			{
				return this.DataSourceField;
			}
			set
			{
				this.DataSourceField = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00003143 File Offset: 0x00001343
		// (set) Token: 0x0600014C RID: 332 RVA: 0x0000314B File Offset: 0x0000134B
		[DataMember]
		public bool HasPartner
		{
			get
			{
				return this.HasPartnerField;
			}
			set
			{
				this.HasPartnerField = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00003154 File Offset: 0x00001354
		// (set) Token: 0x0600014E RID: 334 RVA: 0x0000315C File Offset: 0x0000135C
		[DataMember]
		public bool HasSyndicationPartner
		{
			get
			{
				return this.HasSyndicationPartnerField;
			}
			set
			{
				this.HasSyndicationPartnerField = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00003165 File Offset: 0x00001365
		// (set) Token: 0x06000150 RID: 336 RVA: 0x0000316D File Offset: 0x0000136D
		[DataMember]
		public string InitialDomain
		{
			get
			{
				return this.InitialDomainField;
			}
			set
			{
				this.InitialDomainField = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00003176 File Offset: 0x00001376
		// (set) Token: 0x06000152 RID: 338 RVA: 0x0000317E File Offset: 0x0000137E
		[DataMember]
		public bool? IsAutoScheduled
		{
			get
			{
				return this.IsAutoScheduledField;
			}
			set
			{
				this.IsAutoScheduledField = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00003187 File Offset: 0x00001387
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000318F File Offset: 0x0000138F
		[DataMember]
		public bool IsPTenant
		{
			get
			{
				return this.IsPTenantField;
			}
			set
			{
				this.IsPTenantField = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00003198 File Offset: 0x00001398
		// (set) Token: 0x06000156 RID: 342 RVA: 0x000031A0 File Offset: 0x000013A0
		[DataMember]
		public bool IsTestTenant
		{
			get
			{
				return this.IsTestTenantField;
			}
			set
			{
				this.IsTestTenantField = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000031A9 File Offset: 0x000013A9
		// (set) Token: 0x06000158 RID: 344 RVA: 0x000031B1 File Offset: 0x000013B1
		[DataMember]
		public string Name
		{
			get
			{
				return this.NameField;
			}
			set
			{
				this.NameField = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000031BA File Offset: 0x000013BA
		// (set) Token: 0x0600015A RID: 346 RVA: 0x000031C2 File Offset: 0x000013C2
		[DataMember]
		public DateTime? PostponedDate
		{
			get
			{
				return this.PostponedDateField;
			}
			set
			{
				this.PostponedDateField = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000031CB File Offset: 0x000013CB
		// (set) Token: 0x0600015C RID: 348 RVA: 0x000031D3 File Offset: 0x000013D3
		[DataMember]
		public string PrimaryDomain
		{
			get
			{
				return this.PrimaryDomainField;
			}
			set
			{
				this.PrimaryDomainField = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000031DC File Offset: 0x000013DC
		// (set) Token: 0x0600015E RID: 350 RVA: 0x000031E4 File Offset: 0x000013E4
		[DataMember]
		public SchedulerState SchedulerState
		{
			get
			{
				return this.SchedulerStateField;
			}
			set
			{
				this.SchedulerStateField = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000031ED File Offset: 0x000013ED
		// (set) Token: 0x06000160 RID: 352 RVA: 0x000031F5 File Offset: 0x000013F5
		[DataMember]
		public TenantEmail[] TenantEmails
		{
			get
			{
				return this.TenantEmailsField;
			}
			set
			{
				this.TenantEmailsField = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000031FE File Offset: 0x000013FE
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00003206 File Offset: 0x00001406
		[DataMember]
		public Guid TenantId
		{
			get
			{
				return this.TenantIdField;
			}
			set
			{
				this.TenantIdField = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000320F File Offset: 0x0000140F
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00003217 File Offset: 0x00001417
		[DataMember]
		public SchedulerTenantWorkload[] TenantWorkloads
		{
			get
			{
				return this.TenantWorkloadsField;
			}
			set
			{
				this.TenantWorkloadsField = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00003220 File Offset: 0x00001420
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00003228 File Offset: 0x00001428
		[DataMember]
		public string TierName
		{
			get
			{
				return this.TierNameField;
			}
			set
			{
				this.TierNameField = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00003231 File Offset: 0x00001431
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00003239 File Offset: 0x00001439
		[DataMember]
		public DateTime? UpgradeEndDate
		{
			get
			{
				return this.UpgradeEndDateField;
			}
			set
			{
				this.UpgradeEndDateField = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00003242 File Offset: 0x00001442
		// (set) Token: 0x0600016A RID: 362 RVA: 0x0000324A File Offset: 0x0000144A
		[DataMember]
		public DateTime? UpgradeStartDate
		{
			get
			{
				return this.UpgradeStartDateField;
			}
			set
			{
				this.UpgradeStartDateField = value;
			}
		}

		// Token: 0x04000096 RID: 150
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000097 RID: 151
		private DateTime? CanceledDateField;

		// Token: 0x04000098 RID: 152
		private string CommunicationCultureField;

		// Token: 0x04000099 RID: 153
		private DataSource DataSourceField;

		// Token: 0x0400009A RID: 154
		private bool HasPartnerField;

		// Token: 0x0400009B RID: 155
		private bool HasSyndicationPartnerField;

		// Token: 0x0400009C RID: 156
		private string InitialDomainField;

		// Token: 0x0400009D RID: 157
		private bool? IsAutoScheduledField;

		// Token: 0x0400009E RID: 158
		private bool IsPTenantField;

		// Token: 0x0400009F RID: 159
		private bool IsTestTenantField;

		// Token: 0x040000A0 RID: 160
		private string NameField;

		// Token: 0x040000A1 RID: 161
		private DateTime? PostponedDateField;

		// Token: 0x040000A2 RID: 162
		private string PrimaryDomainField;

		// Token: 0x040000A3 RID: 163
		private SchedulerState SchedulerStateField;

		// Token: 0x040000A4 RID: 164
		private TenantEmail[] TenantEmailsField;

		// Token: 0x040000A5 RID: 165
		private Guid TenantIdField;

		// Token: 0x040000A6 RID: 166
		private SchedulerTenantWorkload[] TenantWorkloadsField;

		// Token: 0x040000A7 RID: 167
		private string TierNameField;

		// Token: 0x040000A8 RID: 168
		private DateTime? UpgradeEndDateField;

		// Token: 0x040000A9 RID: 169
		private DateTime? UpgradeStartDateField;
	}
}
