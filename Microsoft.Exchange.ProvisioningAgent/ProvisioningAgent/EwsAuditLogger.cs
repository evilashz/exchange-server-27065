using System;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000019 RID: 25
	internal class EwsAuditLogger : IAuditLog
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00005495 File Offset: 0x00003695
		public EwsAuditLogger(ExchangePrincipal principal)
		{
			this.ewsClient = new EwsAuditClient(new EwsConnectionManager(principal, OpenAsAdminOrSystemServiceBudgetTypeType.Unthrottled, EwsAuditLogger.Tracer), EwsAuditClient.DefaultSoapClientTimeout, EwsAuditLogger.Tracer);
			this.exchangePrincipal = principal;
			this.InitializeAdminAuditLogsFolder();
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000054CB File Offset: 0x000036CB
		public DateTime EstimatedLogStartTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000054D2 File Offset: 0x000036D2
		public DateTime EstimatedLogEndTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000054D9 File Offset: 0x000036D9
		public bool IsAsynchronous
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000054DC File Offset: 0x000036DC
		public IAuditQueryContext<TFilter> CreateAuditQueryContext<TFilter>()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000054E4 File Offset: 0x000036E4
		public int WriteAuditRecord(IAuditLogRecord auditRecord)
		{
			if (AuditFeatureManager.IsPartitionedAdminLogEnabled(this.exchangePrincipal) && (this.auditLog == null || this.auditLog.EstimatedLogEndTime < auditRecord.CreationTime))
			{
				EwsAuditLogCollection ewsAuditLogCollection = new EwsAuditLogCollection(this.ewsClient, this.auditRootFolderId);
				if (!ewsAuditLogCollection.FindLog(auditRecord.CreationTime, true, out this.auditLog))
				{
					this.auditLog = null;
				}
			}
			if (this.auditLog == null)
			{
				this.auditLog = new EwsAuditLog(this.ewsClient, this.auditRootFolderId, DateTime.MinValue, DateTime.MaxValue);
			}
			return this.auditLog.WriteAuditRecord(auditRecord);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005584 File Offset: 0x00003784
		private void InitializeAdminAuditLogsFolder()
		{
			FolderIdType folderIdType = null;
			this.ewsClient.CheckAndCreateWellKnownFolder(DistinguishedFolderIdNameType.root, DistinguishedFolderIdNameType.recoverableitemsroot, out folderIdType);
			this.ewsClient.CheckAndCreateWellKnownFolder(DistinguishedFolderIdNameType.recoverableitemsroot, DistinguishedFolderIdNameType.adminauditlogs, out this.auditRootFolderId);
		}

		// Token: 0x0400006A RID: 106
		private static readonly Trace Tracer = ExTraceGlobals.AdminAuditLogTracer;

		// Token: 0x0400006B RID: 107
		private EwsAuditClient ewsClient;

		// Token: 0x0400006C RID: 108
		private FolderIdType auditRootFolderId;

		// Token: 0x0400006D RID: 109
		private IAuditLog auditLog;

		// Token: 0x0400006E RID: 110
		private ExchangePrincipal exchangePrincipal;
	}
}
