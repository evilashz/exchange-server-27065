using System;
using System.Collections;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.IO.Compression;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x0200094D RID: 2381
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class PublicFolderMailboxLogger : PublicFolderMailboxLoggerBase
	{
		// Token: 0x0600588E RID: 22670 RVA: 0x0016C4F0 File Offset: 0x0016A6F0
		public PublicFolderMailboxLogger(IPublicFolderSession publicFolderSession, string configurationName, string lastCycleLogConfigurationName, Guid? correlationId = null) : base(publicFolderSession, correlationId)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				IXSOFactory ixsofactory = new XSOFactory();
				using (Folder folder = ixsofactory.BindToFolder(publicFolderSession, publicFolderSession.GetTombstonesRootFolderId()) as Folder)
				{
					this.diagnosticsMetadata = UserConfiguration.GetConfiguration(folder, new UserConfigurationName(lastCycleLogConfigurationName, ConfigurationNameKind.Name), UserConfigurationTypes.Stream);
					this.lastCycleLogMetadata = UserConfiguration.GetConfiguration(folder, new UserConfigurationName(configurationName, ConfigurationNameKind.Name), UserConfigurationTypes.Dictionary);
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

		// Token: 0x17001889 RID: 6281
		// (get) Token: 0x0600588F RID: 22671 RVA: 0x0016C5EC File Offset: 0x0016A7EC
		// (set) Token: 0x06005890 RID: 22672 RVA: 0x0016C5F4 File Offset: 0x0016A7F4
		public Exception LastError { get; set; }

		// Token: 0x1700188A RID: 6282
		// (get) Token: 0x06005891 RID: 22673 RVA: 0x0016C5FD File Offset: 0x0016A7FD
		// (set) Token: 0x06005892 RID: 22674 RVA: 0x0016C605 File Offset: 0x0016A805
		public string LastSyncInfo { get; set; }

		// Token: 0x06005893 RID: 22675 RVA: 0x0016C610 File Offset: 0x0016A810
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
					this.SetSyncMetadataValue("LastSyncFailure", PublicFolderMailboxLoggerBase.GetExceptionLogString(this.LastError, PublicFolderMailboxLoggerBase.ExceptionLogOption.All));
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
				PublicFolderMailboxLoggerBase.LogOnServer(exception, this.logComponent, this.logSuffixName);
			}
			catch (StoragePermanentException exception2)
			{
				PublicFolderMailboxLoggerBase.LogOnServer(exception2, this.logComponent, this.logSuffixName);
			}
			return false;
		}

		// Token: 0x06005894 RID: 22676 RVA: 0x0016C7B8 File Offset: 0x0016A9B8
		public void SaveCheckPoint()
		{
			this.lastCycleLogMetadata.Save();
		}

		// Token: 0x06005895 RID: 22677 RVA: 0x0016C7C8 File Offset: 0x0016A9C8
		public void SetSyncMetadataValue(string name, object value)
		{
			IDictionary dictionary = this.lastCycleLogMetadata.GetDictionary();
			dictionary[name] = value;
		}

		// Token: 0x06005896 RID: 22678 RVA: 0x0016C7EC File Offset: 0x0016A9EC
		public bool TryGetSyncMetadataValue<T>(string name, out T propertyValue)
		{
			propertyValue = default(T);
			IDictionary dictionary = this.lastCycleLogMetadata.GetDictionary();
			if (dictionary.Contains(name))
			{
				object obj = dictionary[name];
				if (obj != null)
				{
					propertyValue = (T)((object)obj);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005897 RID: 22679 RVA: 0x0016C82F File Offset: 0x0016AA2F
		public override void ReportError(string errorContextMessage, Exception syncException)
		{
			base.ReportError(errorContextMessage, syncException);
			this.LastError = syncException;
		}

		// Token: 0x06005898 RID: 22680 RVA: 0x0016C840 File Offset: 0x0016AA40
		public new void LogEvent(LogEventType eventType, string data)
		{
			LogRowFormatter logRowFormatter = null;
			base.LogEvent(eventType, data, out logRowFormatter);
			if (this.gZipLoggingStream != null)
			{
				logRowFormatter.Write(this.gZipLoggingStream);
			}
		}

		// Token: 0x06005899 RID: 22681 RVA: 0x0016C870 File Offset: 0x0016AA70
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

		// Token: 0x0600589A RID: 22682 RVA: 0x0016C8F0 File Offset: 0x0016AAF0
		protected void PublishMonitoringResult(int numberofAttemptsAfterLastSuccess, ExDateTime firstFailedSyncTimeAfterLastSuccess, ExDateTime lastFailedSyncTime)
		{
			TimeSpan t = lastFailedSyncTime - firstFailedSyncTimeAfterLastSuccess;
			if (numberofAttemptsAfterLastSuccess >= PublicFolderMailboxLogger.MinNumberOfFailedSyncAttemptsForAlert && t >= PublicFolderMailboxLogger.MinDurationOfSyncFailureForAlert)
			{
				string name = ExchangeComponent.PublicFolders.Name;
				string component = "PublicFolderMailboxSync";
				string empty = string.Empty;
				EventNotificationItem eventNotificationItem = new EventNotificationItem(name, component, empty, ResultSeverityLevel.Error);
				eventNotificationItem.StateAttribute1 = this.pfSession.MailboxGuid.ToString();
				eventNotificationItem.StateAttribute2 = this.pfSession.MdbGuid.ToString();
				eventNotificationItem.StateAttribute3 = ((this.pfSession.OrganizationId.OrganizationalUnit != null) ? this.pfSession.OrganizationId.OrganizationalUnit.Name.ToString() : string.Empty);
				if (this.LastError == null)
				{
					eventNotificationItem.Message = "No LastError but failing for at least this long: " + PublicFolderMailboxLogger.MinDurationOfSyncFailureForAlert;
				}
				else
				{
					eventNotificationItem.Message = PublicFolderMailboxLoggerBase.GetExceptionLogString(this.LastError, PublicFolderMailboxLoggerBase.ExceptionLogOption.All);
				}
				try
				{
					eventNotificationItem.Publish(false);
				}
				catch (UnauthorizedAccessException e)
				{
					this.LogEvent(LogEventType.Warning, string.Format("PublishMonitoringResult: Failed with exception {0}", PublicFolderMailboxLoggerBase.GetExceptionLogString(e, PublicFolderMailboxLoggerBase.ExceptionLogOption.All)));
				}
				catch (EventLogNotFoundException e2)
				{
					this.LogEvent(LogEventType.Warning, string.Format("PublishMonitoringResult: Failed with exception {0}", PublicFolderMailboxLoggerBase.GetExceptionLogString(e2, PublicFolderMailboxLoggerBase.ExceptionLogOption.All)));
				}
			}
		}

		// Token: 0x0600589B RID: 22683
		protected abstract void LogFinalFoldersStats();

		// Token: 0x0400305F RID: 12383
		private static readonly TimeSpan MinDurationOfSyncFailureForAlert = EnhancedTimeSpan.FromHours(6.0);

		// Token: 0x04003060 RID: 12384
		private static readonly int MinNumberOfFailedSyncAttemptsForAlert = 7;

		// Token: 0x04003061 RID: 12385
		private UserConfiguration diagnosticsMetadata;

		// Token: 0x04003062 RID: 12386
		private UserConfiguration lastCycleLogMetadata;

		// Token: 0x04003063 RID: 12387
		private Stream gZipLoggingStream;

		// Token: 0x04003064 RID: 12388
		private Stream loggingStream;
	}
}
