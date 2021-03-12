using System;
using Microsoft.Exchange.Data.Storage.Auditing;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200000C RID: 12
	internal class AsyncAuditLogger : IAuditLog
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00004787 File Offset: 0x00002987
		public AsyncAuditLogger(IAuditLog auditLogger)
		{
			this.realAuditLogger = auditLogger;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00004796 File Offset: 0x00002996
		public DateTime EstimatedLogStartTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000479D File Offset: 0x0000299D
		public DateTime EstimatedLogEndTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000047A4 File Offset: 0x000029A4
		public bool IsAsynchronous
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000047A7 File Offset: 0x000029A7
		public IAuditQueryContext<TFilter> CreateAuditQueryContext<TFilter>()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000047B0 File Offset: 0x000029B0
		public int WriteAuditRecord(IAuditLogRecord auditRecord)
		{
			IQueue<AuditData> queue = QueueFactory.GetQueue<AuditData>(Queues.AdminAuditingMainQueue);
			AuditData data = new AuditData
			{
				AuditRecord = auditRecord,
				AuditLogger = this.realAuditLogger
			};
			queue.Send(data);
			return 0;
		}

		// Token: 0x04000041 RID: 65
		private readonly IAuditLog realAuditLogger;
	}
}
