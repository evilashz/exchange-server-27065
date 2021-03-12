using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x02000091 RID: 145
	internal class AdminAuditOpticsLogData : IDisposable
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x000176EE File Offset: 0x000158EE
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x000176F6 File Offset: 0x000158F6
		public string Tenant { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x000176FF File Offset: 0x000158FF
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x00017707 File Offset: 0x00015907
		public string CmdletName { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00017710 File Offset: 0x00015910
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x00017718 File Offset: 0x00015918
		public bool ExternalAccess { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00017721 File Offset: 0x00015921
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x00017729 File Offset: 0x00015929
		public long LoggingTime { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00017732 File Offset: 0x00015932
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x0001773A File Offset: 0x0001593A
		public bool OperationSucceeded { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x00017743 File Offset: 0x00015943
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x0001774B File Offset: 0x0001594B
		public int RecordSize { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00017754 File Offset: 0x00015954
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x0001775C File Offset: 0x0001595C
		public bool AuditSucceeded { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00017765 File Offset: 0x00015965
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x0001776D File Offset: 0x0001596D
		public Exception LoggingError { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00017776 File Offset: 0x00015976
		// (set) Token: 0x0600065E RID: 1630 RVA: 0x0001777E File Offset: 0x0001597E
		public OrganizationId ExecutingUserOrganizationId { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00017787 File Offset: 0x00015987
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x0001778F File Offset: 0x0001598F
		public bool Asynchronous { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00017798 File Offset: 0x00015998
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x000177A0 File Offset: 0x000159A0
		public Guid RecordId { get; set; }

		// Token: 0x06000663 RID: 1635 RVA: 0x000177AC File Offset: 0x000159AC
		public AdminAuditOpticsLogData()
		{
			IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
			if (currentActivityScope != null)
			{
				this.requestId = currentActivityScope.ActivityId;
				return;
			}
			this.activityScopeToDispose = ActivityContext.Start(null);
			this.requestId = this.activityScopeToDispose.ActivityId;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000177F2 File Offset: 0x000159F2
		public void Disable()
		{
			this.isDisabled = true;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x000177FB File Offset: 0x000159FB
		public void Dispose()
		{
			if (this.activityScopeToDispose != null)
			{
				this.activityScopeToDispose.Dispose();
				this.activityScopeToDispose = null;
			}
			if (this.isDisabled)
			{
				return;
			}
			AuditingOpticsLogger.LogAuditOpticsEntry(AuditingOpticsLoggerType.AdminAudit, AuditingOpticsLogger.GetLogColumns<AdminAuditOpticsLogData>(this, AdminAuditOpticsLogData.logSchema));
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001795C File Offset: 0x00015B5C
		// Note: this type is marked as 'beforefieldinit'.
		static AdminAuditOpticsLogData()
		{
			LogTableSchema<AdminAuditOpticsLogData>[] array = new LogTableSchema<AdminAuditOpticsLogData>[14];
			array[0] = new LogTableSchema<AdminAuditOpticsLogData>("Tenant", (AdminAuditOpticsLogData data) => data.Tenant);
			array[1] = new LogTableSchema<AdminAuditOpticsLogData>("CmdletName", (AdminAuditOpticsLogData data) => data.CmdletName);
			array[2] = new LogTableSchema<AdminAuditOpticsLogData>("CmdletSucceeded", delegate(AdminAuditOpticsLogData data)
			{
				if (!data.OperationSucceeded)
				{
					return "0";
				}
				return "1";
			});
			array[3] = new LogTableSchema<AdminAuditOpticsLogData>("CmdletError", (AdminAuditOpticsLogData data) => string.Empty);
			array[4] = new LogTableSchema<AdminAuditOpticsLogData>("AuditSucceeded", delegate(AdminAuditOpticsLogData data)
			{
				if (!data.AuditSucceeded)
				{
					return "0";
				}
				return "1";
			});
			array[5] = new LogTableSchema<AdminAuditOpticsLogData>("LoggingError", (AdminAuditOpticsLogData data) => AuditingOpticsLogger.GetExceptionNamesForTrace(data.LoggingError, AuditLogExceptionFormatter.Instance));
			array[6] = new LogTableSchema<AdminAuditOpticsLogData>("LoggingTime", (AdminAuditOpticsLogData data) => data.LoggingTime.ToString());
			array[7] = new LogTableSchema<AdminAuditOpticsLogData>("RecordSize", (AdminAuditOpticsLogData data) => data.RecordSize.ToString());
			array[8] = new LogTableSchema<AdminAuditOpticsLogData>("ExternalAccess", delegate(AdminAuditOpticsLogData data)
			{
				if (!data.ExternalAccess)
				{
					return "0";
				}
				return "1";
			});
			array[9] = new LogTableSchema<AdminAuditOpticsLogData>("ExecutingUserOrganizationId", delegate(AdminAuditOpticsLogData data)
			{
				if (!(data.ExecutingUserOrganizationId != null))
				{
					return string.Empty;
				}
				return data.ExecutingUserOrganizationId.ToString();
			});
			array[10] = new LogTableSchema<AdminAuditOpticsLogData>("DiagnosticContext", (AdminAuditOpticsLogData data) => AuditingOpticsLogger.GetDiagnosticContext(data.LoggingError));
			array[11] = new LogTableSchema<AdminAuditOpticsLogData>("RequestId", (AdminAuditOpticsLogData data) => data.requestId.ToString("D"));
			array[12] = new LogTableSchema<AdminAuditOpticsLogData>("Asynchronous", delegate(AdminAuditOpticsLogData data)
			{
				if (!data.Asynchronous)
				{
					return "0";
				}
				return "1";
			});
			array[13] = new LogTableSchema<AdminAuditOpticsLogData>("RecordId", (AdminAuditOpticsLogData data) => data.RecordId.ToString());
			AdminAuditOpticsLogData.logSchema = array;
		}

		// Token: 0x040002BD RID: 701
		internal static LogTableSchema<AdminAuditOpticsLogData>[] logSchema;

		// Token: 0x040002BE RID: 702
		private bool isDisabled;

		// Token: 0x040002BF RID: 703
		private readonly Guid requestId;

		// Token: 0x040002C0 RID: 704
		private ActivityScope activityScopeToDispose;
	}
}
