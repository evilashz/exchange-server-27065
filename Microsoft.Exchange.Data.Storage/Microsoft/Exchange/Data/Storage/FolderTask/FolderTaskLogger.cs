using System;
using System.Collections;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.IO.Compression;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Data.Storage.FolderTask
{
	// Token: 0x0200095F RID: 2399
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class FolderTaskLogger : FolderTaskLoggerBase
	{
		// Token: 0x060058DD RID: 22749 RVA: 0x0016D758 File Offset: 0x0016B958
		public FolderTaskLogger(IStoreSession storeSession, string configurationName, string lastCycleLogConfigurationName, string logComponent, string logSuffixName, Guid? correlationId = null) : base(storeSession, logComponent, logSuffixName, correlationId)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				using (Folder logFolder = this.GetLogFolder())
				{
					this.diagnosticsMetadata = UserConfiguration.GetConfiguration(logFolder, new UserConfigurationName(lastCycleLogConfigurationName, ConfigurationNameKind.Name), UserConfigurationTypes.Stream);
					this.lastCycleLogMetadata = UserConfiguration.GetConfiguration(logFolder, new UserConfigurationName(configurationName, ConfigurationNameKind.Name), UserConfigurationTypes.Dictionary);
					this.loggingStream = this.diagnosticsMetadata.GetStream();
					disposeGuard.Add<Stream>(this.loggingStream);
					this.loggingStream.SetLength(0L);
					this.loggingStream.Flush();
					this.gZipLoggingStream = new GZipStream(this.loggingStream, CompressionMode.Compress, true);
				}
				this.SetSyncMetadataValue("LastAttemptedSyncTime", ExDateTime.UtcNow);
				disposeGuard.Success();
			}
		}

		// Token: 0x17001897 RID: 6295
		// (get) Token: 0x060058DE RID: 22750 RVA: 0x0016D848 File Offset: 0x0016BA48
		// (set) Token: 0x060058DF RID: 22751 RVA: 0x0016D850 File Offset: 0x0016BA50
		public Exception LastError { get; set; }

		// Token: 0x17001898 RID: 6296
		// (get) Token: 0x060058E0 RID: 22752 RVA: 0x0016D859 File Offset: 0x0016BA59
		// (set) Token: 0x060058E1 RID: 22753 RVA: 0x0016D861 File Offset: 0x0016BA61
		public string LastSyncInfo { get; set; }

		// Token: 0x060058E2 RID: 22754 RVA: 0x0016D86A File Offset: 0x0016BA6A
		public virtual Folder GetLogFolder()
		{
			return Folder.Bind((StoreSession)this.storeSession, DefaultFolderType.Root);
		}

		// Token: 0x060058E3 RID: 22755 RVA: 0x0016D880 File Offset: 0x0016BA80
		public bool TrySave()
		{
			try
			{
				this.LogFinalFoldersStats();
				if (this.LastError == null)
				{
					this.SetSyncMetadataValue("NumberofAttemptsAfterLastSuccess", 0);
					this.SetSyncMetadataValue("FirstFailedSyncTimeAfterLastSuccess", null);
					this.LogEvent(LogEventType.Success, "Diagnostics for monitoring is successfully completed");
					this.SetSyncMetadataValue("LastSuccessfulSyncTime", ExDateTime.UtcNow);
				}
				else
				{
					this.SetSyncMetadataValue("LastSyncFailure", FolderTaskLoggerBase.GetExceptionLogString(this.LastError, FolderTaskLoggerBase.ExceptionLogOption.All));
					int num;
					this.TryGetSyncMetadataValue<int>("NumberofAttemptsAfterLastSuccess", out num);
					num++;
					this.SetSyncMetadataValue("NumberofAttemptsAfterLastSuccess", num);
					this.LogEvent(LogEventType.Error, "Diagnostics for monitoring is failed");
					ExDateTime utcNow = ExDateTime.UtcNow;
					this.SetSyncMetadataValue("LastFailedSyncTime", utcNow);
					ExDateTime exDateTime = default(ExDateTime);
					this.TryGetSyncMetadataValue<ExDateTime>("FirstFailedSyncTimeAfterLastSuccess", out exDateTime);
					if (num == 1 || exDateTime == default(ExDateTime))
					{
						exDateTime = utcNow;
						this.SetSyncMetadataValue("FirstFailedSyncTimeAfterLastSuccess", exDateTime);
					}
					this.PublishMonitoringResult(num, exDateTime, utcNow);
				}
				this.gZipLoggingStream.Dispose();
				this.gZipLoggingStream = null;
				this.loggingStream.Dispose();
				this.loggingStream = null;
				this.diagnosticsMetadata.Save();
				this.lastCycleLogMetadata.Save();
				return true;
			}
			catch (StorageTransientException exception)
			{
				FolderTaskLoggerBase.LogOnServer(exception, this.logComponent, this.logSuffixName);
			}
			catch (StoragePermanentException exception2)
			{
				FolderTaskLoggerBase.LogOnServer(exception2, this.logComponent, this.logSuffixName);
			}
			return false;
		}

		// Token: 0x060058E4 RID: 22756 RVA: 0x0016DA28 File Offset: 0x0016BC28
		public void SaveCheckPoint()
		{
			this.lastCycleLogMetadata.Save();
		}

		// Token: 0x060058E5 RID: 22757 RVA: 0x0016DA38 File Offset: 0x0016BC38
		public void SetSyncMetadataValue(string name, object value)
		{
			IDictionary dictionary = this.lastCycleLogMetadata.GetDictionary();
			dictionary[name] = value;
		}

		// Token: 0x060058E6 RID: 22758 RVA: 0x0016DA5C File Offset: 0x0016BC5C
		public bool TryGetSyncMetadataValue<T>(string name, out T propertyValue)
		{
			propertyValue = default(T);
			IDictionary dictionary = this.lastCycleLogMetadata.GetDictionary();
			if (dictionary.Contains(name))
			{
				object obj = dictionary[name];
				if (obj != null)
				{
					try
					{
						propertyValue = (T)((object)obj);
					}
					catch (InvalidCastException arg)
					{
						this.LogEvent(LogEventType.Error, string.Format("TryGetSyncMetadataValue: Got InvalidCastException: {0}", arg));
						return false;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060058E7 RID: 22759 RVA: 0x0016DACC File Offset: 0x0016BCCC
		public override void ReportError(string errorContextMessage, Exception syncException)
		{
			base.ReportError(errorContextMessage, syncException);
			this.LastError = syncException;
		}

		// Token: 0x060058E8 RID: 22760 RVA: 0x0016DAE0 File Offset: 0x0016BCE0
		public new void LogEvent(LogEventType eventType, string data)
		{
			LogRowFormatter logRowFormatter = base.LogEvent(eventType, data, FolderTaskLoggerBase.LogType.Folder);
			if (this.gZipLoggingStream != null)
			{
				logRowFormatter.Write(this.gZipLoggingStream);
			}
		}

		// Token: 0x060058E9 RID: 22761 RVA: 0x0016DB0C File Offset: 0x0016BD0C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				base.InternalDispose(disposing);
				if (this.gZipLoggingStream != null)
				{
					this.gZipLoggingStream.Dispose();
					this.gZipLoggingStream = null;
				}
				if (this.loggingStream != null)
				{
					this.loggingStream.Dispose();
					this.loggingStream = null;
				}
				if (this.diagnosticsMetadata != null)
				{
					this.diagnosticsMetadata.Dispose();
					this.diagnosticsMetadata = null;
				}
				if (this.lastCycleLogMetadata != null)
				{
					this.lastCycleLogMetadata.Dispose();
					this.lastCycleLogMetadata = null;
				}
			}
		}

		// Token: 0x060058EA RID: 22762 RVA: 0x0016DB8C File Offset: 0x0016BD8C
		protected void PublishMonitoringResult(int numberofAttemptsAfterLastSuccess, ExDateTime firstFailedSyncTimeAfterLastSuccess, ExDateTime lastFailedSyncTime)
		{
			TimeSpan t = lastFailedSyncTime - firstFailedSyncTimeAfterLastSuccess;
			if (numberofAttemptsAfterLastSuccess >= FolderTaskLogger.MinNumberOfFailedSyncAttemptsForAlert && t >= FolderTaskLogger.MinDurationOfSyncFailureForAlert)
			{
				string name = ExchangeComponent.PublicFolders.Name;
				string component = "PublicFolderMailboxSync";
				string empty = string.Empty;
				EventNotificationItem eventNotificationItem = new EventNotificationItem(name, component, empty, ResultSeverityLevel.Error);
				eventNotificationItem.StateAttribute1 = this.storeSession.MailboxGuid.ToString();
				eventNotificationItem.StateAttribute2 = this.storeSession.MdbGuid.ToString();
				eventNotificationItem.StateAttribute3 = ((this.storeSession.OrganizationId != null && this.storeSession.OrganizationId.OrganizationalUnit != null) ? this.storeSession.OrganizationId.OrganizationalUnit.Name.ToString() : string.Empty);
				if (this.LastError == null)
				{
					eventNotificationItem.Message = "No LastError but failing for at least this long: " + FolderTaskLogger.MinDurationOfSyncFailureForAlert;
				}
				else
				{
					eventNotificationItem.Message = FolderTaskLoggerBase.GetExceptionLogString(this.LastError, FolderTaskLoggerBase.ExceptionLogOption.All);
				}
				try
				{
					eventNotificationItem.Publish(false);
				}
				catch (UnauthorizedAccessException exception)
				{
					this.LogEvent(LogEventType.Warning, string.Format("PublishMonitoringResult: Failed with exception {0}", FolderTaskLoggerBase.GetExceptionLogString(exception, FolderTaskLoggerBase.ExceptionLogOption.All)));
				}
				catch (EventLogNotFoundException exception2)
				{
					this.LogEvent(LogEventType.Warning, string.Format("PublishMonitoringResult: Failed with exception {0}", FolderTaskLoggerBase.GetExceptionLogString(exception2, FolderTaskLoggerBase.ExceptionLogOption.All)));
				}
			}
		}

		// Token: 0x060058EB RID: 22763
		protected abstract void LogFinalFoldersStats();

		// Token: 0x040030B6 RID: 12470
		private static readonly TimeSpan MinDurationOfSyncFailureForAlert = EnhancedTimeSpan.FromHours(6.0);

		// Token: 0x040030B7 RID: 12471
		private static readonly int MinNumberOfFailedSyncAttemptsForAlert = 7;

		// Token: 0x040030B8 RID: 12472
		private UserConfiguration diagnosticsMetadata;

		// Token: 0x040030B9 RID: 12473
		private UserConfiguration lastCycleLogMetadata;

		// Token: 0x040030BA RID: 12474
		private Stream gZipLoggingStream;

		// Token: 0x040030BB RID: 12475
		private Stream loggingStream;
	}
}
