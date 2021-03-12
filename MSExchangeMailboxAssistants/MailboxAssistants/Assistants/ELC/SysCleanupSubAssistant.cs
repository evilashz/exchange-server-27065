using System;
using System.Diagnostics;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x020000A1 RID: 161
	internal class SysCleanupSubAssistant : ElcSubAssistant
	{
		// Token: 0x06000629 RID: 1577 RVA: 0x0002F6D6 File Offset: 0x0002D8D6
		public SysCleanupSubAssistant(DatabaseInfo databaseInfo, ELCAssistantType elcAssistantType, ELCHealthMonitor healthMonitor) : base(databaseInfo, healthMonitor)
		{
			base.ElcAssistantType = elcAssistantType;
			this.sysCleanupEnforcerManager = new SysCleanupEnforcerManager(this);
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0002F6F4 File Offset: 0x0002D8F4
		internal DatabaseConfig DatabaseConfig
		{
			get
			{
				if (this.databaseConfig == null)
				{
					DatabaseConfig databaseConfig = new DatabaseConfig();
					ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 85, "DatabaseConfig", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\elc\\SysCleanupAssistant\\SysCleanupSubAssistant.cs");
					MailboxDatabase mailboxDatabase = topologyConfigurationSession.Read<MailboxDatabase>(new ADObjectId(base.DatabaseInfo.Guid));
					if (mailboxDatabase != null)
					{
						databaseConfig.DatabaseDumpsterWarningQuota = new Unlimited<ByteQuantifiedSize>?(mailboxDatabase.RecoverableItemsWarningQuota);
						databaseConfig.DumpsterRetentionPeriod = new EnhancedTimeSpan?(mailboxDatabase.DeletedItemRetention);
						databaseConfig.RetainDeletedItemsUntilBackup = mailboxDatabase.RetainDeletedItemsUntilBackup;
						databaseConfig.DatabaseIssueWarningQuota = new Unlimited<ByteQuantifiedSize>?(mailboxDatabase.IssueWarningQuota);
						try
						{
							using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=TBA", mailboxDatabase.ServerName, null, null, null))
							{
								DateTime dateTime;
								DateTime dateTime2;
								exRpcAdmin.GetLastBackupTimes(base.DatabaseInfo.Guid, out dateTime, out dateTime2);
								ElcSubAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Database {1} lastFullBackup = {2}, lastIncrementalBackup = {3}.", new object[]
								{
									this,
									base.DatabaseInfo.Guid,
									dateTime,
									dateTime2
								});
								databaseConfig.LastFullBackup = dateTime;
							}
							goto IL_232;
						}
						catch (MapiExceptionNotFound mapiExceptionNotFound)
						{
							ElcSubAssistant.Tracer.TraceError((long)this.GetHashCode(), "{0}: Failed to read LastBackupTimes for database {1} ({2}). MapiExceptionNotFound {3}", new object[]
							{
								this,
								base.DatabaseInfo,
								base.DatabaseInfo.Guid,
								mapiExceptionNotFound
							});
							goto IL_232;
						}
						catch (MapiExceptionMdbOffline mapiExceptionMdbOffline)
						{
							ElcSubAssistant.Tracer.TraceError((long)this.GetHashCode(), "{0}: Failed to read LastBackupTimes for database {1} ({2}). MapiExceptionMdbOffline {3}", new object[]
							{
								this,
								base.DatabaseInfo,
								base.DatabaseInfo.Guid,
								mapiExceptionMdbOffline
							});
							goto IL_232;
						}
						catch (MapiExceptionNotInitialized mapiExceptionNotInitialized)
						{
							ElcSubAssistant.Tracer.TraceError((long)this.GetHashCode(), "{0}: Failed to read LastBackupTimes for database {1} ({2}). MapiExceptionNotInitialized {3}", new object[]
							{
								this,
								base.DatabaseInfo,
								base.DatabaseInfo.Guid,
								mapiExceptionNotInitialized
							});
							goto IL_232;
						}
					}
					ElcSubAssistant.Tracer.TraceError<SysCleanupSubAssistant, string, Guid>((long)this.GetHashCode(), "{0}: Failed to read MailboxDatabase for database {1} ({2}).", this, base.DatabaseInfo.ToString(), base.DatabaseInfo.Guid);
					IL_232:
					this.databaseConfig = databaseConfig;
				}
				return this.databaseConfig;
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0002F974 File Offset: 0x0002DB74
		public override string ToString()
		{
			if (base.DebugString == null)
			{
				base.DebugString = "System Cleanup assistant for " + base.DatabaseInfo.ToString();
			}
			return base.DebugString;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0002F99F File Offset: 0x0002DB9F
		internal override void OnWindowBegin()
		{
			ElcSubAssistant.Tracer.TraceDebug<SysCleanupSubAssistant>((long)this.GetHashCode(), "{0}: OnWindowBegin entered.", this);
			this.databaseConfig = null;
			ElcSubAssistant.Tracer.TraceDebug<SysCleanupSubAssistant>((long)this.GetHashCode(), "{0}: OnWindowBegin exited.", this);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0002F9D8 File Offset: 0x0002DBD8
		internal void Invoke(MailboxSession mailboxSession, MailboxDataForTags mailboxDataForTags, ElcParameters parameters)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				this.InvokeInternal(mailboxSession, mailboxDataForTags, parameters);
			}
			finally
			{
				mailboxDataForTags.StatisticsLogEntry.CleanupSubAssistantProcessingTime = stopwatch.ElapsedMilliseconds;
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0002FA18 File Offset: 0x0002DC18
		private void InvokeInternal(MailboxSession mailboxSession, MailboxDataForTags mailboxDataForTags, ElcParameters parameters)
		{
			this.sysCleanupEnforcerManager.Invoke(mailboxDataForTags, parameters);
			this.CheckArchiveWarningQuota(mailboxSession, mailboxDataForTags);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0002FA2F File Offset: 0x0002DC2F
		internal override void OnWindowEnd()
		{
			ElcSubAssistant.Tracer.TraceDebug<SysCleanupSubAssistant>((long)this.GetHashCode(), "{0}: OnWindowEnd entered.", this);
			this.databaseConfig = null;
			ElcSubAssistant.Tracer.TraceDebug<SysCleanupSubAssistant>((long)this.GetHashCode(), "{0}: OnWindowEnd exited.", this);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0002FA68 File Offset: 0x0002DC68
		private void CheckArchiveWarningQuota(MailboxSession mailboxSession, MailboxData mailboxData)
		{
			if (mailboxSession.MailboxOwner.MailboxInfo.IsArchive)
			{
				mailboxSession.Mailbox.ForceReload(new PropertyDefinition[]
				{
					MailboxSchema.QuotaUsedExtended
				});
				object obj = mailboxSession.Mailbox.TryGetProperty(MailboxSchema.QuotaUsedExtended);
				if (obj is long)
				{
					ulong num = (ulong)((long)obj);
					Unlimited<ByteQuantifiedSize> archiveWarningQuota = mailboxData.ElcUserInformation.ADUser.ArchiveWarningQuota;
					ElcSubAssistant.Tracer.TraceDebug<SysCleanupSubAssistant, ulong, Unlimited<ByteQuantifiedSize>>((long)this.GetHashCode(), "{0}: Archive size is {1} and archive warning quota is {2}", this, num, archiveWarningQuota);
					if (!archiveWarningQuota.IsUnlimited && archiveWarningQuota.Value.ToBytes() < num)
					{
						ElcSubAssistant.Tracer.TraceDebug<SysCleanupSubAssistant>((long)this.GetHashCode(), "{0}: Archive mailbox is over quota", this);
						Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ArchiveOverWarningQuota, null, new object[]
						{
							mailboxSession.MailboxOwner,
							archiveWarningQuota,
							num
						});
						ELCAssistant.PublishMonitoringResult(mailboxSession, null, ELCAssistant.NotificationType.ArchiveWarningQuota, string.Format("Mailbox: {0} whose archive size: {1} is over ArchiveWarningQuota: {2}.", mailboxSession.MailboxGuid, num, archiveWarningQuota));
						mailboxData.StatisticsLogEntry.IsArchiveOverWarningQuota = true;
						return;
					}
				}
				else
				{
					ElcSubAssistant.Tracer.TraceError<SysCleanupSubAssistant>((long)this.GetHashCode(), "{0}: We could not get size of this archive mailbox. Skipping it.", this);
				}
			}
		}

		// Token: 0x04000490 RID: 1168
		private SysCleanupEnforcerManager sysCleanupEnforcerManager;

		// Token: 0x04000491 RID: 1169
		private DatabaseConfig databaseConfig;
	}
}
