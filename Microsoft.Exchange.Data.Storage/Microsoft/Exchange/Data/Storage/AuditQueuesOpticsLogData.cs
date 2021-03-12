using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F4C RID: 3916
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AuditQueuesOpticsLogData
	{
		// Token: 0x1700239B RID: 9115
		// (get) Token: 0x0600865D RID: 34397 RVA: 0x0024DA04 File Offset: 0x0024BC04
		// (set) Token: 0x0600865E RID: 34398 RVA: 0x0024DA0C File Offset: 0x0024BC0C
		public OrganizationId OrganizationId { get; set; }

		// Token: 0x1700239C RID: 9116
		// (get) Token: 0x0600865F RID: 34399 RVA: 0x0024DA15 File Offset: 0x0024BC15
		// (set) Token: 0x06008660 RID: 34400 RVA: 0x0024DA1D File Offset: 0x0024BC1D
		public AuditQueueType QueueType { get; set; }

		// Token: 0x1700239D RID: 9117
		// (get) Token: 0x06008661 RID: 34401 RVA: 0x0024DA26 File Offset: 0x0024BC26
		// (set) Token: 0x06008662 RID: 34402 RVA: 0x0024DA2E File Offset: 0x0024BC2E
		public QueueEventType EventType { get; set; }

		// Token: 0x1700239E RID: 9118
		// (get) Token: 0x06008663 RID: 34403 RVA: 0x0024DA37 File Offset: 0x0024BC37
		// (set) Token: 0x06008664 RID: 34404 RVA: 0x0024DA3F File Offset: 0x0024BC3F
		public string CorrelationId { get; set; }

		// Token: 0x1700239F RID: 9119
		// (get) Token: 0x06008665 RID: 34405 RVA: 0x0024DA48 File Offset: 0x0024BC48
		// (set) Token: 0x06008666 RID: 34406 RVA: 0x0024DA50 File Offset: 0x0024BC50
		public int QueueLength { get; set; }

		// Token: 0x06008667 RID: 34407 RVA: 0x0024DA59 File Offset: 0x0024BC59
		public void Log()
		{
			AuditingOpticsLogger.LogAuditOpticsEntry(AuditingOpticsLoggerType.AuditQueues, AuditingOpticsLogger.GetLogColumns<AuditQueuesOpticsLogData>(this, AuditQueuesOpticsLogData.LogSchema));
		}

		// Token: 0x06008669 RID: 34409 RVA: 0x0024DADC File Offset: 0x0024BCDC
		// Note: this type is marked as 'beforefieldinit'.
		static AuditQueuesOpticsLogData()
		{
			LogTableSchema<AuditQueuesOpticsLogData>[] array = new LogTableSchema<AuditQueuesOpticsLogData>[5];
			array[0] = new LogTableSchema<AuditQueuesOpticsLogData>("Tenant", delegate(AuditQueuesOpticsLogData data)
			{
				if (!(data.OrganizationId == null))
				{
					return data.OrganizationId.ToString();
				}
				return "First Org";
			});
			array[1] = new LogTableSchema<AuditQueuesOpticsLogData>("QueueType", (AuditQueuesOpticsLogData data) => data.QueueType.ToString());
			array[2] = new LogTableSchema<AuditQueuesOpticsLogData>("EventType", (AuditQueuesOpticsLogData data) => data.EventType.ToString());
			array[3] = new LogTableSchema<AuditQueuesOpticsLogData>("QueueLength", (AuditQueuesOpticsLogData data) => data.QueueLength.ToString(CultureInfo.InvariantCulture));
			array[4] = new LogTableSchema<AuditQueuesOpticsLogData>("CorrelationId", (AuditQueuesOpticsLogData data) => data.CorrelationId);
			AuditQueuesOpticsLogData.LogSchema = array;
		}

		// Token: 0x040059F5 RID: 23029
		internal static LogTableSchema<AuditQueuesOpticsLogData>[] LogSchema;
	}
}
