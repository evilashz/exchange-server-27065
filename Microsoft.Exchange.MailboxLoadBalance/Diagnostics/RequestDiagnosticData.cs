using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x02000055 RID: 85
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class RequestDiagnosticData
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00009662 File Offset: 0x00007862
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0000966A File Offset: 0x0000786A
		[DataMember]
		public string BatchName { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00009673 File Offset: 0x00007873
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x0000967B File Offset: 0x0000787B
		[DataMember]
		public Exception Exception { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00009684 File Offset: 0x00007884
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x000096F3 File Offset: 0x000078F3
		public TimeSpan ExecutionDuration
		{
			get
			{
				if (this.executionDuration != null)
				{
					return this.executionDuration.Value;
				}
				if (this.ExecutionStartedTimestamp == null)
				{
					return TimeSpan.Zero;
				}
				DateTime d = this.ExecutionFinishedTimestamp ?? DateTime.UtcNow;
				return d - this.ExecutionStartedTimestamp.Value;
			}
			set
			{
				this.executionDuration = new TimeSpan?(value);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00009701 File Offset: 0x00007901
		// (set) Token: 0x060002EA RID: 746 RVA: 0x00009709 File Offset: 0x00007909
		[DataMember]
		public DateTime? ExecutionFinishedTimestamp { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00009712 File Offset: 0x00007912
		// (set) Token: 0x060002EC RID: 748 RVA: 0x0000971A File Offset: 0x0000791A
		[DataMember]
		public DateTime? ExecutionStartedTimestamp { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00009723 File Offset: 0x00007923
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0000972B File Offset: 0x0000792B
		[DataMember]
		public Guid MovedMailboxGuid { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00009734 File Offset: 0x00007934
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000973C File Offset: 0x0000793C
		[DataMember]
		public Guid Queue { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00009748 File Offset: 0x00007948
		public TimeSpan QueueDuration
		{
			get
			{
				if (this.QueuedTimestamp == null)
				{
					return TimeSpan.Zero;
				}
				DateTime d = this.ExecutionStartedTimestamp ?? DateTime.UtcNow;
				return d - this.QueuedTimestamp.Value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000979E File Offset: 0x0000799E
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x000097A6 File Offset: 0x000079A6
		[DataMember]
		public DateTime? QueuedTimestamp { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x000097AF File Offset: 0x000079AF
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x000097B7 File Offset: 0x000079B7
		[DataMember]
		public string RequestKind { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x000097C0 File Offset: 0x000079C0
		public string Result
		{
			get
			{
				if (this.Exception != null)
				{
					return "Failed";
				}
				return "Success";
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x000097D5 File Offset: 0x000079D5
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x000097DD File Offset: 0x000079DD
		[DataMember]
		public string SourceDatabaseName { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x000097E6 File Offset: 0x000079E6
		// (set) Token: 0x060002FA RID: 762 RVA: 0x000097EE File Offset: 0x000079EE
		[DataMember]
		public string TargetDatabaseName { get; set; }

		// Token: 0x040000D4 RID: 212
		[DataMember]
		private TimeSpan? executionDuration;
	}
}
