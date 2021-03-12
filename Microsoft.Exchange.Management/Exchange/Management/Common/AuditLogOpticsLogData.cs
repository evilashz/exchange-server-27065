using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x020000E6 RID: 230
	internal class AuditLogOpticsLogData : IDisposable
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0001C1A8 File Offset: 0x0001A3A8
		// (set) Token: 0x060006AD RID: 1709 RVA: 0x0001C1B0 File Offset: 0x0001A3B0
		public OrganizationId OrganizationId { get; set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x0001C1B9 File Offset: 0x0001A3B9
		// (set) Token: 0x060006AF RID: 1711 RVA: 0x0001C1C1 File Offset: 0x0001A3C1
		public string SearchType { get; set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0001C1CA File Offset: 0x0001A3CA
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x0001C1D2 File Offset: 0x0001A3D2
		public int QueryComplexity { get; set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0001C1DB File Offset: 0x0001A3DB
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x0001C1E3 File Offset: 0x0001A3E3
		public long ResultsReturned { get; set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0001C1EC File Offset: 0x0001A3EC
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x0001C1F4 File Offset: 0x0001A3F4
		public bool CallResult { get; set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0001C1FD File Offset: 0x0001A3FD
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x0001C205 File Offset: 0x0001A405
		public Exception ErrorType { get; set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0001C20E File Offset: 0x0001A40E
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x0001C216 File Offset: 0x0001A416
		public int ErrorCount { get; set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0001C21F File Offset: 0x0001A41F
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x0001C227 File Offset: 0x0001A427
		public bool IsAsynchronous { get; set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0001C230 File Offset: 0x0001A430
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x0001C238 File Offset: 0x0001A438
		public bool ShowDetails { get; set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0001C241 File Offset: 0x0001A441
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x0001C249 File Offset: 0x0001A449
		public int MailboxCount { get; set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001C252 File Offset: 0x0001A452
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x0001C25A File Offset: 0x0001A45A
		private Stopwatch Stopwatch { get; set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001C263 File Offset: 0x0001A463
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x0001C26B File Offset: 0x0001A46B
		public DateTime? SearchStartDateTime { get; set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001C274 File Offset: 0x0001A474
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x0001C27C File Offset: 0x0001A47C
		public DateTime? SearchEndDateTime { get; set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001C285 File Offset: 0x0001A485
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x0001C28D File Offset: 0x0001A48D
		public string CorrelationID { get; set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001C296 File Offset: 0x0001A496
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x0001C29E File Offset: 0x0001A49E
		public int Retry { get; set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001C2A7 File Offset: 0x0001A4A7
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x0001C2AF File Offset: 0x0001A4AF
		public bool RequestDeleted { get; set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0001C2B8 File Offset: 0x0001A4B8
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x0001C2C0 File Offset: 0x0001A4C0
		public string LastProcessedMailbox { get; set; }

		// Token: 0x060006CE RID: 1742 RVA: 0x0001C2CC File Offset: 0x0001A4CC
		public AuditLogOpticsLogData()
		{
			this.Stopwatch = Stopwatch.StartNew();
			IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
			if (currentActivityScope != null)
			{
				this.requestId = currentActivityScope.ActivityId;
				return;
			}
			this.activityScopeToDispose = ActivityContext.Start(null);
			this.requestId = this.activityScopeToDispose.ActivityId;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001C31D File Offset: 0x0001A51D
		public void Dispose()
		{
			this.Stopwatch.Stop();
			if (this.activityScopeToDispose != null)
			{
				this.activityScopeToDispose.Dispose();
				this.activityScopeToDispose = null;
			}
			AuditingOpticsLogger.LogAuditOpticsEntry(AuditingOpticsLoggerType.AuditSearch, AuditingOpticsLogger.GetLogColumns<AuditLogOpticsLogData>(this, AuditLogOpticsLogData.LogSchema));
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001C534 File Offset: 0x0001A734
		// Note: this type is marked as 'beforefieldinit'.
		static AuditLogOpticsLogData()
		{
			LogTableSchema<AuditLogOpticsLogData>[] array = new LogTableSchema<AuditLogOpticsLogData>[19];
			array[0] = new LogTableSchema<AuditLogOpticsLogData>("Tenant", delegate(AuditLogOpticsLogData data)
			{
				if (!(data.OrganizationId == null))
				{
					return data.OrganizationId.ToString();
				}
				return "First Org";
			});
			array[1] = new LogTableSchema<AuditLogOpticsLogData>("SearchType", (AuditLogOpticsLogData data) => data.SearchType);
			array[2] = new LogTableSchema<AuditLogOpticsLogData>("QueryComplexity", (AuditLogOpticsLogData data) => data.QueryComplexity.ToString(CultureInfo.InvariantCulture));
			array[3] = new LogTableSchema<AuditLogOpticsLogData>("ExecutionTime", (AuditLogOpticsLogData data) => data.Stopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
			array[4] = new LogTableSchema<AuditLogOpticsLogData>("CallResult", delegate(AuditLogOpticsLogData data)
			{
				if (!data.CallResult)
				{
					return "0";
				}
				return "1";
			});
			array[5] = new LogTableSchema<AuditLogOpticsLogData>("ErrorType", (AuditLogOpticsLogData data) => AuditingOpticsLogger.GetExceptionNamesForTrace(data.ErrorType, AuditLogExceptionFormatter.Instance));
			array[6] = new LogTableSchema<AuditLogOpticsLogData>("ResultsReturned", (AuditLogOpticsLogData data) => data.ResultsReturned.ToString(CultureInfo.InvariantCulture));
			array[7] = new LogTableSchema<AuditLogOpticsLogData>("ErrorCount", (AuditLogOpticsLogData data) => data.ErrorCount.ToString(CultureInfo.InvariantCulture));
			array[8] = new LogTableSchema<AuditLogOpticsLogData>("Async", delegate(AuditLogOpticsLogData data)
			{
				if (!data.IsAsynchronous)
				{
					return "0";
				}
				return "1";
			});
			array[9] = new LogTableSchema<AuditLogOpticsLogData>("ShowDetails", delegate(AuditLogOpticsLogData data)
			{
				if (!data.ShowDetails)
				{
					return "0";
				}
				return "1";
			});
			array[10] = new LogTableSchema<AuditLogOpticsLogData>("MailboxCount", (AuditLogOpticsLogData data) => data.MailboxCount.ToString(CultureInfo.InvariantCulture));
			array[11] = new LogTableSchema<AuditLogOpticsLogData>("SearchStartDateTime", (AuditLogOpticsLogData data) => data.SearchStartDateTime.ToString());
			array[12] = new LogTableSchema<AuditLogOpticsLogData>("SearchEndDateTime", (AuditLogOpticsLogData data) => data.SearchEndDateTime.ToString());
			array[13] = new LogTableSchema<AuditLogOpticsLogData>("CorrelationID", (AuditLogOpticsLogData data) => data.CorrelationID);
			array[14] = new LogTableSchema<AuditLogOpticsLogData>("Retry", (AuditLogOpticsLogData data) => data.Retry.ToString(CultureInfo.InvariantCulture));
			array[15] = new LogTableSchema<AuditLogOpticsLogData>("RequestDeleted", delegate(AuditLogOpticsLogData data)
			{
				if (!data.RequestDeleted)
				{
					return "0";
				}
				return "1";
			});
			array[16] = new LogTableSchema<AuditLogOpticsLogData>("LastProcessedMailbox", (AuditLogOpticsLogData data) => data.LastProcessedMailbox);
			array[17] = new LogTableSchema<AuditLogOpticsLogData>("DiagnosticContext", (AuditLogOpticsLogData data) => AuditingOpticsLogger.GetDiagnosticContext(data.ErrorType));
			array[18] = new LogTableSchema<AuditLogOpticsLogData>("RequestId", (AuditLogOpticsLogData data) => data.requestId.ToString("D"));
			AuditLogOpticsLogData.LogSchema = array;
		}

		// Token: 0x04000317 RID: 791
		public const string MailboxSearchType = "Mailbox";

		// Token: 0x04000318 RID: 792
		public const string AdminSearchType = "Admin";

		// Token: 0x04000319 RID: 793
		private const string LogTrue = "1";

		// Token: 0x0400031A RID: 794
		private const string LogFalse = "0";

		// Token: 0x0400031B RID: 795
		internal static LogTableSchema<AuditLogOpticsLogData>[] LogSchema;

		// Token: 0x0400031C RID: 796
		private readonly Guid requestId;

		// Token: 0x0400031D RID: 797
		private ActivityScope activityScopeToDispose;
	}
}
