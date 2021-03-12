using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x02000012 RID: 18
	internal class AsyncQueueLog : ConfigurablePropertyBag
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00003682 File Offset: 0x00001882
		public AsyncQueueLog()
		{
			this.LogEntries = new List<AsyncQueueLogProperty>();
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003698 File Offset: 0x00001898
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.StepTransactionId.ToString());
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000036BE File Offset: 0x000018BE
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000036D0 File Offset: 0x000018D0
		public Guid StepTransactionId
		{
			get
			{
				return (Guid)this[AsyncQueueLogSchema.StepTransactionIdProperty];
			}
			set
			{
				this[AsyncQueueLogSchema.StepTransactionIdProperty] = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000036E3 File Offset: 0x000018E3
		// (set) Token: 0x06000076 RID: 118 RVA: 0x000036F5 File Offset: 0x000018F5
		public byte Priority
		{
			get
			{
				return (byte)this[AsyncQueueLogSchema.PriorityProperty];
			}
			set
			{
				this[AsyncQueueLogSchema.PriorityProperty] = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003708 File Offset: 0x00001908
		// (set) Token: 0x06000078 RID: 120 RVA: 0x0000371A File Offset: 0x0000191A
		public Guid RequestStepId
		{
			get
			{
				return (Guid)this[AsyncQueueLogSchema.RequestStepIdProperty];
			}
			set
			{
				this[AsyncQueueLogSchema.RequestStepIdProperty] = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000372D File Offset: 0x0000192D
		// (set) Token: 0x0600007A RID: 122 RVA: 0x0000373F File Offset: 0x0000193F
		public Guid RequestId
		{
			get
			{
				return (Guid)this[AsyncQueueLogSchema.RequestIdProperty];
			}
			set
			{
				this[AsyncQueueLogSchema.RequestIdProperty] = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003752 File Offset: 0x00001952
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003764 File Offset: 0x00001964
		public Guid? DependantRequestId
		{
			get
			{
				return (Guid?)this[AsyncQueueLogSchema.DependantRequestIdProperty];
			}
			set
			{
				this[AsyncQueueLogSchema.DependantRequestIdProperty] = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003777 File Offset: 0x00001977
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003789 File Offset: 0x00001989
		public string OwnerId
		{
			get
			{
				return (string)this[AsyncQueueLogSchema.OwnerIdProperty];
			}
			set
			{
				this[AsyncQueueLogSchema.OwnerIdProperty] = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003797 File Offset: 0x00001997
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000037A9 File Offset: 0x000019A9
		public string FriendlyName
		{
			get
			{
				return (string)this[AsyncQueueLogSchema.FriendlyNameProperty];
			}
			set
			{
				this[AsyncQueueLogSchema.FriendlyNameProperty] = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000037B7 File Offset: 0x000019B7
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000037C9 File Offset: 0x000019C9
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[AsyncQueueLogSchema.OrganizationalUnitRootProperty];
			}
			set
			{
				this[AsyncQueueLogSchema.OrganizationalUnitRootProperty] = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000037DC File Offset: 0x000019DC
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000037EE File Offset: 0x000019EE
		public short StepNumber
		{
			get
			{
				return (short)this[AsyncQueueLogSchema.StepNumberProperty];
			}
			set
			{
				this[AsyncQueueLogSchema.StepNumberProperty] = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003801 File Offset: 0x00001A01
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003813 File Offset: 0x00001A13
		public string StepName
		{
			get
			{
				return (string)this[AsyncQueueLogSchema.StepNameProperty];
			}
			set
			{
				this[AsyncQueueLogSchema.StepNameProperty] = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003821 File Offset: 0x00001A21
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003833 File Offset: 0x00001A33
		public short StepOrdinal
		{
			get
			{
				return (short)this[AsyncQueueLogSchema.StepOrdinalProperty];
			}
			set
			{
				this[AsyncQueueLogSchema.StepOrdinalProperty] = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003846 File Offset: 0x00001A46
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003858 File Offset: 0x00001A58
		public AsyncQueueStatus StepFromStatus
		{
			get
			{
				return (AsyncQueueStatus)this[AsyncQueueLogSchema.StepFromStatusProperty];
			}
			set
			{
				this[AsyncQueueLogSchema.StepFromStatusProperty] = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000386B File Offset: 0x00001A6B
		// (set) Token: 0x0600008C RID: 140 RVA: 0x0000387D File Offset: 0x00001A7D
		public AsyncQueueStatus StepStatus
		{
			get
			{
				return (AsyncQueueStatus)this[AsyncQueueLogSchema.StepStatusProperty];
			}
			set
			{
				this[AsyncQueueLogSchema.StepStatusProperty] = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003890 File Offset: 0x00001A90
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000038A2 File Offset: 0x00001AA2
		public int FetchCount
		{
			get
			{
				return (int)this[AsyncQueueLogSchema.FetchCountProperty];
			}
			set
			{
				this[AsyncQueueLogSchema.FetchCountProperty] = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000038B5 File Offset: 0x00001AB5
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000038C7 File Offset: 0x00001AC7
		public int ErrorCount
		{
			get
			{
				return (int)this[AsyncQueueLogSchema.ErrorCountProperty];
			}
			set
			{
				this[AsyncQueueLogSchema.ErrorCountProperty] = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000038DA File Offset: 0x00001ADA
		// (set) Token: 0x06000092 RID: 146 RVA: 0x000038EC File Offset: 0x00001AEC
		public string ProcessInstanceName
		{
			get
			{
				return (string)this[AsyncQueueLogSchema.ProcessInstanceNameProperty];
			}
			set
			{
				this[AsyncQueueLogSchema.ProcessInstanceNameProperty] = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000038FA File Offset: 0x00001AFA
		// (set) Token: 0x06000094 RID: 148 RVA: 0x0000390C File Offset: 0x00001B0C
		public DateTime ProcessStartDatetime
		{
			get
			{
				return (DateTime)this[AsyncQueueLogSchema.ProcessStartDatetimeProperty];
			}
			set
			{
				this[AsyncQueueLogSchema.ProcessStartDatetimeProperty] = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000391F File Offset: 0x00001B1F
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00003931 File Offset: 0x00001B31
		public DateTime? ProcessEndDatetime
		{
			get
			{
				return (DateTime?)this[AsyncQueueLogSchema.ProcessEndDatetimeProperty];
			}
			set
			{
				this[AsyncQueueLogSchema.ProcessEndDatetimeProperty] = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003944 File Offset: 0x00001B44
		// (set) Token: 0x06000098 RID: 152 RVA: 0x0000394C File Offset: 0x00001B4C
		public List<AsyncQueueLogProperty> LogEntries
		{
			get
			{
				return this.logEntries;
			}
			private set
			{
				this.logEntries = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003955 File Offset: 0x00001B55
		public bool IsStepCompleted
		{
			get
			{
				return this.StepStatus >= AsyncQueueStatus.Completed;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003964 File Offset: 0x00001B64
		public override Type GetSchemaType()
		{
			return typeof(AsyncQueueLogSchema);
		}

		// Token: 0x04000040 RID: 64
		private List<AsyncQueueLogProperty> logEntries;
	}
}
