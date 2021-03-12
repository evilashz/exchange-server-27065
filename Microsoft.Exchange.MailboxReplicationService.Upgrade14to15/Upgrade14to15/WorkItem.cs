using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000021 RID: 33
	[DataContract(Name = "WorkItem", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.ManagementService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class WorkItem : IExtensibleDataObject
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00002D72 File Offset: 0x00000F72
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00002D7A File Offset: 0x00000F7A
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

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00002D83 File Offset: 0x00000F83
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00002D8B File Offset: 0x00000F8B
		[DataMember]
		public string Comment
		{
			get
			{
				return this.CommentField;
			}
			set
			{
				this.CommentField = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00002D94 File Offset: 0x00000F94
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00002D9C File Offset: 0x00000F9C
		[DataMember]
		public int CompletedCount
		{
			get
			{
				return this.CompletedCountField;
			}
			set
			{
				this.CompletedCountField = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00002DA5 File Offset: 0x00000FA5
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00002DAD File Offset: 0x00000FAD
		[DataMember]
		public string HandlerState
		{
			get
			{
				return this.HandlerStateField;
			}
			set
			{
				this.HandlerStateField = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00002DB6 File Offset: 0x00000FB6
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00002DBE File Offset: 0x00000FBE
		[DataMember]
		public PilotUser PilotUser
		{
			get
			{
				return this.PilotUserField;
			}
			set
			{
				this.PilotUserField = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00002DC7 File Offset: 0x00000FC7
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00002DCF File Offset: 0x00000FCF
		[DataMember]
		public DateTime ScheduledDate
		{
			get
			{
				return this.ScheduledDateField;
			}
			set
			{
				this.ScheduledDateField = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00002DD8 File Offset: 0x00000FD8
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00002DE0 File Offset: 0x00000FE0
		[DataMember]
		public Status Status
		{
			get
			{
				return this.StatusField;
			}
			set
			{
				this.StatusField = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00002DE9 File Offset: 0x00000FE9
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00002DF1 File Offset: 0x00000FF1
		[DataMember]
		public Uri StatusDetails
		{
			get
			{
				return this.StatusDetailsField;
			}
			set
			{
				this.StatusDetailsField = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00002DFA File Offset: 0x00000FFA
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00002E02 File Offset: 0x00001002
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

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00002E0B File Offset: 0x0000100B
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00002E13 File Offset: 0x00001013
		[DataMember]
		public int TotalCount
		{
			get
			{
				return this.TotalCountField;
			}
			set
			{
				this.TotalCountField = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00002E1C File Offset: 0x0000101C
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00002E24 File Offset: 0x00001024
		[DataMember]
		public string WorkItemId
		{
			get
			{
				return this.WorkItemIdField;
			}
			set
			{
				this.WorkItemIdField = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00002E2D File Offset: 0x0000102D
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00002E35 File Offset: 0x00001035
		[DataMember]
		public string WorkItemType
		{
			get
			{
				return this.WorkItemTypeField;
			}
			set
			{
				this.WorkItemTypeField = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00002E3E File Offset: 0x0000103E
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00002E46 File Offset: 0x00001046
		[DataMember]
		public string WorkloadName
		{
			get
			{
				return this.WorkloadNameField;
			}
			set
			{
				this.WorkloadNameField = value;
			}
		}

		// Token: 0x04000069 RID: 105
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400006A RID: 106
		private string CommentField;

		// Token: 0x0400006B RID: 107
		private int CompletedCountField;

		// Token: 0x0400006C RID: 108
		private string HandlerStateField;

		// Token: 0x0400006D RID: 109
		private PilotUser PilotUserField;

		// Token: 0x0400006E RID: 110
		private DateTime ScheduledDateField;

		// Token: 0x0400006F RID: 111
		private Status StatusField;

		// Token: 0x04000070 RID: 112
		private Uri StatusDetailsField;

		// Token: 0x04000071 RID: 113
		private Guid TenantIdField;

		// Token: 0x04000072 RID: 114
		private int TotalCountField;

		// Token: 0x04000073 RID: 115
		private string WorkItemIdField;

		// Token: 0x04000074 RID: 116
		private string WorkItemTypeField;

		// Token: 0x04000075 RID: 117
		private string WorkloadNameField;
	}
}
