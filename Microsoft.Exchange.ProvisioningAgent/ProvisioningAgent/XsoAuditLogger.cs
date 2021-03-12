using System;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Provisioning.LoadBalancing;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000020 RID: 32
	internal class XsoAuditLogger : IAuditLog
	{
		// Token: 0x060000FF RID: 255 RVA: 0x000062E8 File Offset: 0x000044E8
		public XsoAuditLogger(ExchangePrincipal principal, bool canUsePartitionedLogs)
		{
			this.principal = principal;
			IMailboxLocation location = principal.MailboxInfo.Location;
			if (location != null && location.ServerSite != null)
			{
				this.isLocalSite = PhysicalResourceLoadBalancing.IsDatabaseInLocalSite(location.ServerSite, location.ServerFqdn, location.DatabaseName, delegate(string message)
				{
				});
			}
			this.canUsePartitionedLogs = canUsePartitionedLogs;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000635A File Offset: 0x0000455A
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00006362 File Offset: 0x00004562
		public bool IsLocalSite
		{
			get
			{
				return this.isLocalSite;
			}
			set
			{
				this.isLocalSite = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000636B File Offset: 0x0000456B
		public DateTime EstimatedLogStartTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00006372 File Offset: 0x00004572
		public DateTime EstimatedLogEndTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00006379 File Offset: 0x00004579
		public bool IsAsynchronous
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000637C File Offset: 0x0000457C
		public IAuditQueryContext<TFilter> CreateAuditQueryContext<TFilter>()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006383 File Offset: 0x00004583
		public int WriteAuditRecord(IAuditLogRecord auditRecord)
		{
			if (this.isLocalSite)
			{
				return this.LogLocal(auditRecord);
			}
			return this.LogRemote(auditRecord);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000639C File Offset: 0x0000459C
		private int LogLocal(IAuditLogRecord auditRecord)
		{
			if (XsoAuditLogger.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				XsoAuditLogger.Tracer.TraceDebug<string>(0L, "Writing log to local site mailbox {0}.", this.principal.MailboxInfo.PrimarySmtpAddress.ToString());
			}
			int result;
			using (MailboxSession mailboxSession = MailboxSessionManager.CreateMailboxSession(this.principal))
			{
				result = this.LogWithSession(auditRecord, mailboxSession);
			}
			return result;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006418 File Offset: 0x00004618
		private int LogRemote(IAuditLogRecord auditRecord)
		{
			if (XsoAuditLogger.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				XsoAuditLogger.Tracer.TraceDebug<string>(0L, "Writing log to remote site mailbox {0}.", this.principal.MailboxInfo.PrimarySmtpAddress.ToString());
			}
			MailboxSession userMailboxSessionFromCache = MailboxSessionManager.GetUserMailboxSessionFromCache(this.principal);
			try
			{
				return this.LogWithSession(auditRecord, userMailboxSessionFromCache);
			}
			catch (StorageTransientException)
			{
				if (userMailboxSessionFromCache != null)
				{
					MailboxSessionManager.ReturnMailboxSessionToCache(ref userMailboxSessionFromCache, true);
					userMailboxSessionFromCache = MailboxSessionManager.GetUserMailboxSessionFromCache(this.principal);
					if (userMailboxSessionFromCache != null)
					{
						return this.LogWithSession(auditRecord, userMailboxSessionFromCache);
					}
				}
			}
			finally
			{
				if (userMailboxSessionFromCache != null)
				{
					MailboxSessionManager.ReturnMailboxSessionToCache(ref userMailboxSessionFromCache, false);
				}
			}
			return 0;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000064E4 File Offset: 0x000046E4
		private int LogWithSession(IAuditLogRecord auditRecord, MailboxSession session)
		{
			IAuditLog auditLog = null;
			if (this.canUsePartitionedLogs && AuditFeatureManager.IsPartitionedAdminLogEnabled(this.principal))
			{
				AuditLogCollection auditLogCollection = new AuditLogCollection(session, this.GetLogFolderId(session), XsoAuditLogger.Tracer, (IAuditLogRecord record, MessageItem message) => AuditLogParseSerialize.SerializeAdminAuditRecord(record, message));
				if (!auditLogCollection.FindLog(auditRecord.CreationTime, true, out auditLog))
				{
					auditLog = null;
				}
			}
			if (auditLog == null)
			{
				auditLog = new AuditLog(session, this.GetLogFolderId(session), DateTime.MinValue, DateTime.MaxValue, 0, (IAuditLogRecord record, MessageItem message) => AuditLogParseSerialize.SerializeAdminAuditRecord(record, message));
			}
			return auditLog.WriteAuditRecord(auditRecord);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000658B File Offset: 0x0000478B
		protected virtual StoreObjectId GetLogFolderId(MailboxSession mailboxSession)
		{
			return AdminAuditLogHelper.GetOrCreateAdminAuditLogsFolderId(mailboxSession);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006594 File Offset: 0x00004794
		private void SaveMessage(MessageItem message)
		{
			ConflictResolutionResult conflictResolutionResult = message.Save(SaveMode.ResolveConflicts);
			if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				if (XsoAuditLogger.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					XsoAuditLogger.Tracer.TraceDebug<XsoAuditLogger, string>((long)this.GetHashCode(), "{0} Unable to save log in the AdminAuditLogs folder due to irresolvable conflict. Details: {1}", this, this.ConvertConflictResolutionResultToString(conflictResolutionResult));
				}
				throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(message.InternalObjectId), conflictResolutionResult);
			}
			if (XsoAuditLogger.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				XsoAuditLogger.Tracer.TraceDebug<XsoAuditLogger>((long)this.GetHashCode(), "{0} Successfully saved log in the AdminAuditLogs folder under discovery mailbox.", this);
				return;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006614 File Offset: 0x00004814
		private string ConvertConflictResolutionResultToString(ConflictResolutionResult result)
		{
			if (result == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder("Save results: ");
			stringBuilder.AppendLine(result.SaveStatus.ToString());
			if (result.PropertyConflicts != null && result.PropertyConflicts.Length > 0)
			{
				for (int i = 0; i < result.PropertyConflicts.Length; i++)
				{
					PropertyConflict propertyConflict = result.PropertyConflicts[i];
					stringBuilder.AppendFormat("Resolvable: {0} Property: {1}\n", propertyConflict.ConflictResolvable, propertyConflict.PropertyDefinition);
					stringBuilder.AppendFormat("\tOriginal value: {0}\n", propertyConflict.OriginalValue);
					stringBuilder.AppendFormat("\tClient value: {0}\n", propertyConflict.ClientValue);
					stringBuilder.AppendFormat("\tServer value: {0}\n", propertyConflict.ServerValue);
					stringBuilder.AppendFormat("\tResolved value: {0}\n", propertyConflict.ResolvedValue);
				}
			}
			else
			{
				stringBuilder.Append("Zero properties in conflict");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400008E RID: 142
		private static readonly Trace Tracer = ExTraceGlobals.AdminAuditLogTracer;

		// Token: 0x0400008F RID: 143
		private readonly ExchangePrincipal principal;

		// Token: 0x04000090 RID: 144
		private readonly bool canUsePartitionedLogs;

		// Token: 0x04000091 RID: 145
		private bool isLocalSite;
	}
}
