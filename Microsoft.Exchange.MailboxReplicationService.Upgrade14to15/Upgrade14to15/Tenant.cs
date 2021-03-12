using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000014 RID: 20
	[DataContract(Name = "Tenant", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.ManagementService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class Tenant : IExtensibleDataObject
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002963 File Offset: 0x00000B63
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000296B File Offset: 0x00000B6B
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002974 File Offset: 0x00000B74
		// (set) Token: 0x0600005F RID: 95 RVA: 0x0000297C File Offset: 0x00000B7C
		[DataMember]
		public int CanceledCount
		{
			get
			{
				return this.CanceledCountField;
			}
			set
			{
				this.CanceledCountField = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002985 File Offset: 0x00000B85
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000298D File Offset: 0x00000B8D
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002996 File Offset: 0x00000B96
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000299E File Offset: 0x00000B9E
		[DataMember]
		public string CanceledReason
		{
			get
			{
				return this.CanceledReasonField;
			}
			set
			{
				this.CanceledReasonField = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000029A7 File Offset: 0x00000BA7
		// (set) Token: 0x06000065 RID: 101 RVA: 0x000029AF File Offset: 0x00000BAF
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

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000029B8 File Offset: 0x00000BB8
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000029C0 File Offset: 0x00000BC0
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

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000029C9 File Offset: 0x00000BC9
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000029D1 File Offset: 0x00000BD1
		[DataMember]
		public bool HasPilotedUsers
		{
			get
			{
				return this.HasPilotedUsersField;
			}
			set
			{
				this.HasPilotedUsersField = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000029DA File Offset: 0x00000BDA
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000029E2 File Offset: 0x00000BE2
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

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000029EB File Offset: 0x00000BEB
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000029F3 File Offset: 0x00000BF3
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

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000029FC File Offset: 0x00000BFC
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002A04 File Offset: 0x00000C04
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

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002A0D File Offset: 0x00000C0D
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002A15 File Offset: 0x00000C15
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

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002A1E File Offset: 0x00000C1E
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002A26 File Offset: 0x00000C26
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

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002A2F File Offset: 0x00000C2F
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002A37 File Offset: 0x00000C37
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

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002A40 File Offset: 0x00000C40
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002A48 File Offset: 0x00000C48
		[DataMember]
		public string PhaseName
		{
			get
			{
				return this.PhaseNameField;
			}
			set
			{
				this.PhaseNameField = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002A51 File Offset: 0x00000C51
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002A59 File Offset: 0x00000C59
		[DataMember]
		public Status PhaseStatus
		{
			get
			{
				return this.PhaseStatusField;
			}
			set
			{
				this.PhaseStatusField = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002A62 File Offset: 0x00000C62
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002A6A File Offset: 0x00000C6A
		[DataMember]
		public DateTime? PilotEndDate
		{
			get
			{
				return this.PilotEndDateField;
			}
			set
			{
				this.PilotEndDateField = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002A73 File Offset: 0x00000C73
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002A7B File Offset: 0x00000C7B
		[DataMember]
		public DateTime? PilotStartDate
		{
			get
			{
				return this.PilotStartDateField;
			}
			set
			{
				this.PilotStartDateField = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002A84 File Offset: 0x00000C84
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00002A8C File Offset: 0x00000C8C
		[DataMember]
		public DateTime? PostponeEndDate
		{
			get
			{
				return this.PostponeEndDateField;
			}
			set
			{
				this.PostponeEndDateField = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002A95 File Offset: 0x00000C95
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002A9D File Offset: 0x00000C9D
		[DataMember]
		public DateTime? PostponeStartDate
		{
			get
			{
				return this.PostponeStartDateField;
			}
			set
			{
				this.PostponeStartDateField = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002AA6 File Offset: 0x00000CA6
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002AAE File Offset: 0x00000CAE
		[DataMember]
		public string PostponedByUserUpn
		{
			get
			{
				return this.PostponedByUserUpnField;
			}
			set
			{
				this.PostponedByUserUpnField = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002AB7 File Offset: 0x00000CB7
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00002ABF File Offset: 0x00000CBF
		[DataMember]
		public short PostponedCount
		{
			get
			{
				return this.PostponedCountField;
			}
			set
			{
				this.PostponedCountField = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002AC8 File Offset: 0x00000CC8
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00002AD0 File Offset: 0x00000CD0
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

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002AD9 File Offset: 0x00000CD9
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00002AE1 File Offset: 0x00000CE1
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

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002AEA File Offset: 0x00000CEA
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00002AF2 File Offset: 0x00000CF2
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

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002AFB File Offset: 0x00000CFB
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00002B03 File Offset: 0x00000D03
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

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002B0C File Offset: 0x00000D0C
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00002B14 File Offset: 0x00000D14
		[DataMember]
		public TenantWorkload[] TenantWorkloads
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

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002B1D File Offset: 0x00000D1D
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00002B25 File Offset: 0x00000D25
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

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002B2E File Offset: 0x00000D2E
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00002B36 File Offset: 0x00000D36
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

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00002B3F File Offset: 0x00000D3F
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00002B47 File Offset: 0x00000D47
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

		// Token: 0x04000032 RID: 50
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000033 RID: 51
		private int CanceledCountField;

		// Token: 0x04000034 RID: 52
		private DateTime? CanceledDateField;

		// Token: 0x04000035 RID: 53
		private string CanceledReasonField;

		// Token: 0x04000036 RID: 54
		private string CommunicationCultureField;

		// Token: 0x04000037 RID: 55
		private bool HasPartnerField;

		// Token: 0x04000038 RID: 56
		private bool HasPilotedUsersField;

		// Token: 0x04000039 RID: 57
		private bool HasSyndicationPartnerField;

		// Token: 0x0400003A RID: 58
		private string InitialDomainField;

		// Token: 0x0400003B RID: 59
		private bool? IsAutoScheduledField;

		// Token: 0x0400003C RID: 60
		private bool IsPTenantField;

		// Token: 0x0400003D RID: 61
		private bool IsTestTenantField;

		// Token: 0x0400003E RID: 62
		private string NameField;

		// Token: 0x0400003F RID: 63
		private string PhaseNameField;

		// Token: 0x04000040 RID: 64
		private Status PhaseStatusField;

		// Token: 0x04000041 RID: 65
		private DateTime? PilotEndDateField;

		// Token: 0x04000042 RID: 66
		private DateTime? PilotStartDateField;

		// Token: 0x04000043 RID: 67
		private DateTime? PostponeEndDateField;

		// Token: 0x04000044 RID: 68
		private DateTime? PostponeStartDateField;

		// Token: 0x04000045 RID: 69
		private string PostponedByUserUpnField;

		// Token: 0x04000046 RID: 70
		private short PostponedCountField;

		// Token: 0x04000047 RID: 71
		private DateTime? PostponedDateField;

		// Token: 0x04000048 RID: 72
		private string PrimaryDomainField;

		// Token: 0x04000049 RID: 73
		private TenantEmail[] TenantEmailsField;

		// Token: 0x0400004A RID: 74
		private Guid TenantIdField;

		// Token: 0x0400004B RID: 75
		private TenantWorkload[] TenantWorkloadsField;

		// Token: 0x0400004C RID: 76
		private string TierNameField;

		// Token: 0x0400004D RID: 77
		private DateTime? UpgradeEndDateField;

		// Token: 0x0400004E RID: 78
		private DateTime? UpgradeStartDateField;
	}
}
