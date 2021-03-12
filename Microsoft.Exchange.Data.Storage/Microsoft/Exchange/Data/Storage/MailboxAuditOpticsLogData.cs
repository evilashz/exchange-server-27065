using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200061B RID: 1563
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxAuditOpticsLogData : IDisposable
	{
		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x06004043 RID: 16451 RVA: 0x0010DD8B File Offset: 0x0010BF8B
		// (set) Token: 0x06004044 RID: 16452 RVA: 0x0010DD93 File Offset: 0x0010BF93
		public string Tenant { get; set; }

		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x06004045 RID: 16453 RVA: 0x0010DD9C File Offset: 0x0010BF9C
		// (set) Token: 0x06004046 RID: 16454 RVA: 0x0010DDA4 File Offset: 0x0010BFA4
		public string Mailbox { get; set; }

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x06004047 RID: 16455 RVA: 0x0010DDAD File Offset: 0x0010BFAD
		// (set) Token: 0x06004048 RID: 16456 RVA: 0x0010DDB5 File Offset: 0x0010BFB5
		public string Operation { get; set; }

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x06004049 RID: 16457 RVA: 0x0010DDBE File Offset: 0x0010BFBE
		// (set) Token: 0x0600404A RID: 16458 RVA: 0x0010DDC6 File Offset: 0x0010BFC6
		public string LogonType { get; set; }

		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x0600404B RID: 16459 RVA: 0x0010DDCF File Offset: 0x0010BFCF
		// (set) Token: 0x0600404C RID: 16460 RVA: 0x0010DDD7 File Offset: 0x0010BFD7
		public OperationResult OperationSucceeded { get; set; }

		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x0600404D RID: 16461 RVA: 0x0010DDE0 File Offset: 0x0010BFE0
		// (set) Token: 0x0600404E RID: 16462 RVA: 0x0010DDE8 File Offset: 0x0010BFE8
		public long LoggingTime { get; set; }

		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x0600404F RID: 16463 RVA: 0x0010DDF1 File Offset: 0x0010BFF1
		// (set) Token: 0x06004050 RID: 16464 RVA: 0x0010DDF9 File Offset: 0x0010BFF9
		public int RecordSize { get; set; }

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x06004051 RID: 16465 RVA: 0x0010DE02 File Offset: 0x0010C002
		// (set) Token: 0x06004052 RID: 16466 RVA: 0x0010DE0A File Offset: 0x0010C00A
		public bool AuditSucceeded { get; set; }

		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x06004053 RID: 16467 RVA: 0x0010DE13 File Offset: 0x0010C013
		// (set) Token: 0x06004054 RID: 16468 RVA: 0x0010DE1B File Offset: 0x0010C01B
		public Exception LoggingError { get; set; }

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x06004055 RID: 16469 RVA: 0x0010DE24 File Offset: 0x0010C024
		// (set) Token: 0x06004056 RID: 16470 RVA: 0x0010DE2C File Offset: 0x0010C02C
		public string ActionContext { get; set; }

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x06004057 RID: 16471 RVA: 0x0010DE35 File Offset: 0x0010C035
		// (set) Token: 0x06004058 RID: 16472 RVA: 0x0010DE3D File Offset: 0x0010C03D
		public string ClientRequestId { get; set; }

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x06004059 RID: 16473 RVA: 0x0010DE46 File Offset: 0x0010C046
		// (set) Token: 0x0600405A RID: 16474 RVA: 0x0010DE4E File Offset: 0x0010C04E
		public bool ExternalAccess { get; set; }

		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x0600405B RID: 16475 RVA: 0x0010DE57 File Offset: 0x0010C057
		// (set) Token: 0x0600405C RID: 16476 RVA: 0x0010DE5F File Offset: 0x0010C05F
		public bool Asynchronous { get; set; }

		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x0600405D RID: 16477 RVA: 0x0010DE68 File Offset: 0x0010C068
		// (set) Token: 0x0600405E RID: 16478 RVA: 0x0010DE70 File Offset: 0x0010C070
		public Guid RecordId { get; set; }

		// Token: 0x0600405F RID: 16479 RVA: 0x0010DE79 File Offset: 0x0010C079
		public void Dispose()
		{
			AuditingOpticsLogger.LogAuditOpticsEntry(AuditingOpticsLoggerType.MailboxAudit, AuditingOpticsLogger.GetLogColumns<MailboxAuditOpticsLogData>(this, MailboxAuditOpticsLogData.logSchema));
		}

		// Token: 0x06004061 RID: 16481 RVA: 0x0010DF88 File Offset: 0x0010C188
		// Note: this type is marked as 'beforefieldinit'.
		static MailboxAuditOpticsLogData()
		{
			LogTableSchema<MailboxAuditOpticsLogData>[] array = new LogTableSchema<MailboxAuditOpticsLogData>[15];
			array[0] = new LogTableSchema<MailboxAuditOpticsLogData>("Tenant", (MailboxAuditOpticsLogData data) => data.Tenant);
			array[1] = new LogTableSchema<MailboxAuditOpticsLogData>("Mailbox", (MailboxAuditOpticsLogData data) => data.Mailbox);
			array[2] = new LogTableSchema<MailboxAuditOpticsLogData>("Operation", (MailboxAuditOpticsLogData data) => data.Operation);
			array[3] = new LogTableSchema<MailboxAuditOpticsLogData>("LogonType", (MailboxAuditOpticsLogData data) => data.LogonType);
			array[4] = new LogTableSchema<MailboxAuditOpticsLogData>("OperationSucceeded", delegate(MailboxAuditOpticsLogData data)
			{
				if (data.OperationSucceeded != OperationResult.Succeeded)
				{
					return "0";
				}
				return "1";
			});
			array[5] = new LogTableSchema<MailboxAuditOpticsLogData>("AuditSucceeded", delegate(MailboxAuditOpticsLogData data)
			{
				if (!data.AuditSucceeded)
				{
					return "0";
				}
				return "1";
			});
			array[6] = new LogTableSchema<MailboxAuditOpticsLogData>("LoggingError", (MailboxAuditOpticsLogData data) => AuditingOpticsLogger.GetExceptionNamesForTrace(data.LoggingError));
			array[7] = new LogTableSchema<MailboxAuditOpticsLogData>("LoggingTime", (MailboxAuditOpticsLogData data) => data.LoggingTime.ToString());
			array[8] = new LogTableSchema<MailboxAuditOpticsLogData>("RecordSize", (MailboxAuditOpticsLogData data) => data.RecordSize.ToString());
			array[9] = new LogTableSchema<MailboxAuditOpticsLogData>("ActionContext", (MailboxAuditOpticsLogData data) => data.ActionContext);
			array[10] = new LogTableSchema<MailboxAuditOpticsLogData>("ClientRequestId", (MailboxAuditOpticsLogData data) => data.ClientRequestId);
			array[11] = new LogTableSchema<MailboxAuditOpticsLogData>("ExternalAccess", delegate(MailboxAuditOpticsLogData data)
			{
				if (!data.ExternalAccess)
				{
					return "0";
				}
				return "1";
			});
			array[12] = new LogTableSchema<MailboxAuditOpticsLogData>("DiagnosticContext", (MailboxAuditOpticsLogData data) => AuditingOpticsLogger.GetDiagnosticContext(data.LoggingError));
			array[13] = new LogTableSchema<MailboxAuditOpticsLogData>("Asynchronous", delegate(MailboxAuditOpticsLogData data)
			{
				if (!data.Asynchronous)
				{
					return "0";
				}
				return "1";
			});
			array[14] = new LogTableSchema<MailboxAuditOpticsLogData>("RecordId", (MailboxAuditOpticsLogData data) => data.RecordId.ToString());
			MailboxAuditOpticsLogData.logSchema = array;
		}

		// Token: 0x04002382 RID: 9090
		internal static LogTableSchema<MailboxAuditOpticsLogData>[] logSchema;
	}
}
