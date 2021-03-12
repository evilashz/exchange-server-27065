using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x02000057 RID: 87
	[DataContract]
	internal class DiagnosticLog
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00009829 File Offset: 0x00007A29
		// (set) Token: 0x06000302 RID: 770 RVA: 0x00009831 File Offset: 0x00007A31
		[DataMember]
		public MigrationEventType Level { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000983A File Offset: 0x00007A3A
		// (set) Token: 0x06000304 RID: 772 RVA: 0x00009842 File Offset: 0x00007A42
		[DataMember]
		public string LogEntry { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000984B File Offset: 0x00007A4B
		// (set) Token: 0x06000306 RID: 774 RVA: 0x00009853 File Offset: 0x00007A53
		[DataMember]
		public Exception Exception { get; set; }

		// Token: 0x06000307 RID: 775 RVA: 0x0000985C File Offset: 0x00007A5C
		public override string ToString()
		{
			if (this.Exception == null)
			{
				return string.Format("[{0:-5,5}] {1}", this.Level, this.LogEntry);
			}
			return string.Format("[{0:-5,5}] {1} - {2}", this.Level, this.LogEntry, this.Exception);
		}
	}
}
