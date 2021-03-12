using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Office.Compliance.Audit.Schema;
using Microsoft.Office.Compliance.Audit.Schema.Admin;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020000A9 RID: 169
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AdminAuditWriter
	{
		// Token: 0x0600073F RID: 1855 RVA: 0x0001C3EB File Offset: 0x0001A5EB
		public AdminAuditWriter(Microsoft.Exchange.Diagnostics.Trace tracer)
		{
			this.Tracer = tracer;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001C424 File Offset: 0x0001A624
		public void Write(ExchangeAdminAuditRecord record)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			using (AdminAuditOpticsLogData adminAuditOpticsLogData = new AdminAuditOpticsLogData())
			{
				try
				{
					adminAuditOpticsLogData.Tenant = (string.IsNullOrEmpty(record.OrganizationName) ? "First Org" : record.OrganizationName);
					adminAuditOpticsLogData.CmdletName = record.Operation;
					adminAuditOpticsLogData.ExternalAccess = record.ExternalAccess;
					adminAuditOpticsLogData.OperationSucceeded = (record.Succeeded ?? false);
					adminAuditOpticsLogData.RecordId = record.Id;
					adminAuditOpticsLogData.Asynchronous = true;
					IAuditLog auditLog = this.GetAuditLog(record.OrganizationId ?? string.Empty, record);
					IAuditLogRecord auditRecord = this.ConvertAuditRecord(record);
					adminAuditOpticsLogData.RecordSize = auditLog.WriteAuditRecord(auditRecord);
					adminAuditOpticsLogData.LoggingError = null;
					adminAuditOpticsLogData.AuditSucceeded = true;
				}
				catch (Exception loggingError)
				{
					adminAuditOpticsLogData.LoggingError = loggingError;
					adminAuditOpticsLogData.AuditSucceeded = false;
					throw;
				}
				finally
				{
					adminAuditOpticsLogData.LoggingTime = stopwatch.ElapsedMilliseconds;
				}
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001C53C File Offset: 0x0001A73C
		private IAuditLogRecord ConvertAuditRecord(AuditRecord record)
		{
			return new AuditLogRecord(record, this.Tracer);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001C550 File Offset: 0x0001A750
		private IAuditLog GetAuditLog(string organizationIdEncoded, AuditRecord auditRecord)
		{
			CacheEntry<IAuditLog> cacheEntry;
			IAuditLog auditLog = this.auditLogs.TryGetValue(organizationIdEncoded, DateTime.UtcNow, out cacheEntry) ? cacheEntry.Value : null;
			ExchangePrincipal exchangePrincipal = this.GetExchangePrincipal(organizationIdEncoded);
			EwsAuditClient ewsClient = null;
			FolderIdType folderIdType = null;
			if (AuditFeatureManager.IsPartitionedAdminLogEnabled(exchangePrincipal) && (auditLog == null || auditLog.EstimatedLogEndTime < auditRecord.CreationTime))
			{
				this.GetClientAndRootFolderId(exchangePrincipal, ref ewsClient, ref folderIdType);
				EwsAuditLogCollection ewsAuditLogCollection = new EwsAuditLogCollection(ewsClient, folderIdType);
				if (ewsAuditLogCollection.FindLog(auditRecord.CreationTime, true, out auditLog))
				{
					this.auditLogs.Set(organizationIdEncoded, DateTime.UtcNow, new CacheEntry<IAuditLog>(auditLog));
				}
				else
				{
					auditLog = null;
				}
			}
			if (auditLog == null)
			{
				this.GetClientAndRootFolderId(exchangePrincipal, ref ewsClient, ref folderIdType);
				auditLog = new EwsAuditLog(ewsClient, folderIdType, DateTime.MinValue, DateTime.MaxValue);
				this.auditLogs.Set(organizationIdEncoded, DateTime.UtcNow, new CacheEntry<IAuditLog>(auditLog));
			}
			return auditLog;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001C625 File Offset: 0x0001A825
		private void GetClientAndRootFolderId(ExchangePrincipal principal, ref EwsAuditClient ewsClient, ref FolderIdType auditRootFolderId)
		{
			ewsClient = (ewsClient ?? new EwsAuditClient(new EwsConnectionManager(principal, OpenAsAdminOrSystemServiceBudgetTypeType.Unthrottled, this.Tracer), EwsAuditClient.DefaultSoapClientTimeout, this.Tracer));
			auditRootFolderId = (auditRootFolderId ?? AdminAuditWriter.GetAuditRootFolderId(ewsClient));
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001C65C File Offset: 0x0001A85C
		private static FolderIdType GetAuditRootFolderId(EwsAuditClient ewsClient)
		{
			FolderIdType folderIdType;
			ewsClient.CheckAndCreateWellKnownFolder(DistinguishedFolderIdNameType.root, DistinguishedFolderIdNameType.recoverableitemsroot, out folderIdType);
			FolderIdType result;
			ewsClient.CheckAndCreateWellKnownFolder(DistinguishedFolderIdNameType.recoverableitemsroot, DistinguishedFolderIdNameType.adminauditlogs, out result);
			return result;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001C684 File Offset: 0x0001A884
		private ExchangePrincipal GetExchangePrincipal(string organizationIdEncoded)
		{
			CacheEntry<ExchangePrincipal> cacheEntry;
			ExchangePrincipal exchangePrincipal;
			if (this.principals.TryGetValue(organizationIdEncoded, DateTime.UtcNow, out cacheEntry))
			{
				exchangePrincipal = cacheEntry.Value;
			}
			else
			{
				OrganizationId organizationId = AuditRecordDatabaseWriterVisitor.GetOrganizationId(organizationIdEncoded);
				ADUser tenantArbitrationMailbox = AdminAuditWriter.GetTenantArbitrationMailbox(organizationId);
				exchangePrincipal = ExchangePrincipal.FromADUser(tenantArbitrationMailbox, null);
				this.principals.TryAdd(organizationIdEncoded, DateTime.UtcNow, new CacheEntry<ExchangePrincipal>(exchangePrincipal));
			}
			return exchangePrincipal;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001C6E0 File Offset: 0x0001A8E0
		private static ADUser GetTenantArbitrationMailbox(OrganizationId organizationId)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), organizationId, null, false);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 244, "GetTenantArbitrationMailbox", "f:\\15.00.1497\\sources\\dev\\data\\src\\ApplicationLogic\\AuditLog\\AdminAuditWriter.cs");
			return MailboxDataProvider.GetDiscoveryMailbox(tenantOrRootOrgRecipientSession);
		}

		// Token: 0x04000345 RID: 837
		public const int PrincipalCacheSize = 128;

		// Token: 0x04000346 RID: 838
		public const int AuditLogCacheSize = 16;

		// Token: 0x04000347 RID: 839
		public static TimeSpan PrincipalLifeTime = TimeSpan.FromMinutes(15.0);

		// Token: 0x04000348 RID: 840
		public static TimeSpan AuditLogLifeTime = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000349 RID: 841
		private readonly Microsoft.Exchange.Diagnostics.Trace Tracer;

		// Token: 0x0400034A RID: 842
		private readonly CacheWithExpiration<string, CacheEntry<ExchangePrincipal>> principals = new CacheWithExpiration<string, CacheEntry<ExchangePrincipal>>(128, AdminAuditWriter.PrincipalLifeTime, null);

		// Token: 0x0400034B RID: 843
		private readonly CacheWithExpiration<string, CacheEntry<IAuditLog>> auditLogs = new CacheWithExpiration<string, CacheEntry<IAuditLog>>(16, AdminAuditWriter.AuditLogLifeTime, null);
	}
}
