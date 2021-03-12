using System;
using System.Reflection;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000058 RID: 88
	internal sealed class ElcAuditLog
	{
		// Token: 0x06000309 RID: 777 RVA: 0x00013620 File Offset: 0x00011820
		static ElcAuditLog()
		{
			ElcAuditLog.Fields[0] = "Date-Time";
			ElcAuditLog.Fields[1] = "Managed-Folder-Assistant-Action";
			ElcAuditLog.Fields[2] = "Folder-Full-Path";
			ElcAuditLog.Fields[3] = "Mailbox-Owner";
			ElcAuditLog.Fields[4] = "Message-Subject";
			ElcAuditLog.Fields[5] = "Message-Received-Date";
			ElcAuditLog.Fields[6] = "Message-Sender";
			ElcAuditLog.Fields[7] = "Message-Internet-Id";
			ElcAuditLog.Fields[8] = "Message-Class";
			ElcAuditLog.Fields[9] = "Retention-Action-Taken";
			ElcAuditLog.Fields[10] = "Journaling-Destination-Address";
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000136DC File Offset: 0x000118DC
		internal ElcAuditLog(DatabaseInfo databaseInfo)
		{
			this.logSchema = new LogSchema("Microsoft Exchange Mailbox Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Managed Folder Assistant", ElcAuditLog.Fields);
			this.logHeaderFormatter = new LogHeaderFormatter(this.logSchema);
			this.databaseInfo = databaseInfo;
			if (!string.IsNullOrEmpty(databaseInfo.DatabaseName))
			{
				this.filenamePrefix = "Managed_Folder_Assistant[" + LocalLongFullPath.ConvertInvalidCharactersInFileName(databaseInfo.DatabaseName) + "]";
				return;
			}
			this.filenamePrefix = "Managed_Folder_Assistant[Database Guid " + databaseInfo.Guid + "]";
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0001378A File Offset: 0x0001198A
		internal bool ExpirationLoggingEnabled
		{
			get
			{
				return this.expirationLoggingEnabled;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00013792 File Offset: 0x00011992
		internal bool AutocopyLoggingEnabled
		{
			get
			{
				return this.autocopyLoggingEnabled;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0001379A File Offset: 0x0001199A
		internal bool FolderLoggingEnabled
		{
			get
			{
				return this.folderLoggingEnabled;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600030E RID: 782 RVA: 0x000137A2 File Offset: 0x000119A2
		internal bool SubjectLoggingEnabled
		{
			get
			{
				return this.subjectLoggingEnabled;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600030F RID: 783 RVA: 0x000137AA File Offset: 0x000119AA
		internal bool LoggingEnabled
		{
			get
			{
				return this.expirationLoggingEnabled || this.autocopyLoggingEnabled || this.folderLoggingEnabled;
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000137C4 File Offset: 0x000119C4
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "ELC Audit Log for " + this.databaseInfo.ToString();
			}
			return this.toString;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000138E8 File Offset: 0x00011AE8
		internal void Open(string nonLocalizedAssistantName)
		{
			ElcAuditLog.<>c__DisplayClass1 CS$<>8__locals1 = new ElcAuditLog.<>c__DisplayClass1();
			CS$<>8__locals1.<>4__this = this;
			ElcAuditLog.Tracer.TraceDebug<ElcAuditLog>((long)this.GetHashCode(), "{0}: About to read server config object from AD and configure log.", this);
			CS$<>8__locals1.server = this.GetServerObjectFromAD(nonLocalizedAssistantName);
			this.expirationLoggingEnabled = CS$<>8__locals1.server.RetentionLogForManagedFoldersEnabled;
			this.autocopyLoggingEnabled = CS$<>8__locals1.server.JournalingLogForManagedFoldersEnabled;
			this.folderLoggingEnabled = CS$<>8__locals1.server.FolderLogForManagedFoldersEnabled;
			this.subjectLoggingEnabled = CS$<>8__locals1.server.SubjectLogForManagedFoldersEnabled;
			if (!this.LoggingEnabled)
			{
				ElcAuditLog.Tracer.TraceDebug<ElcAuditLog>((long)this.GetHashCode(), "{0}: Nothing is enabled. Did not create audit log.", this);
				return;
			}
			this.log = new Log(this.filenamePrefix, this.logHeaderFormatter, "Managed Folder Assistant Logs", false);
			if (CS$<>8__locals1.server.LogPathForManagedFolders == null)
			{
				ElcAuditLog.Tracer.TraceError<ElcAuditLog>((long)this.GetHashCode(), "{0}: The ELC Audit log is enabled, but the log path is null. Bad data in AD.", this);
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_MissingAuditLogPath, null, new object[]
				{
					this.databaseInfo.DisplayName
				});
				throw new TransientMailboxException(Strings.descMissingAuditLogPath(this.databaseInfo.DisplayName));
			}
			LocalizedException exception = null;
			try
			{
				Util.CatchMeIfYouCan(delegate
				{
					try
					{
						CS$<>8__locals1.<>4__this.log.Configure(CS$<>8__locals1.server.LogPathForManagedFolders.PathName, CS$<>8__locals1.server.LogFileAgeLimitForManagedFolders, (long)(CS$<>8__locals1.server.LogDirectorySizeLimitForManagedFolders.IsUnlimited ? 0UL : CS$<>8__locals1.server.LogDirectorySizeLimitForManagedFolders.Value.ToBytes()), (long)(CS$<>8__locals1.server.LogFileSizeLimitForManagedFolders.IsUnlimited ? 0UL : CS$<>8__locals1.server.LogFileSizeLimitForManagedFolders.Value.ToBytes()));
					}
					catch (LogException exception2)
					{
						exception = exception2;
					}
				}, nonLocalizedAssistantName);
			}
			catch (AIGrayException exception)
			{
				AIGrayException exception3;
				exception = exception3;
			}
			if (exception != null)
			{
				ElcAuditLog.Tracer.TraceError<ElcAuditLog, LocalizedException>((long)this.GetHashCode(), "{0}: The ELC Assistant failed to configure the audit log. Exception: {1}", this, exception);
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ConfigureAuditLogFailed, null, new object[]
				{
					this.databaseInfo.DisplayName,
					exception
				});
				throw new TransientDatabaseException(Strings.descConfigureAuditLogFailed(this.databaseInfo.DisplayName), exception, null);
			}
			ElcAuditLog.Tracer.TraceDebug<ElcAuditLog>((long)this.GetHashCode(), "{0}: Configured audit log with parameters from AD.", this);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00013AEC File Offset: 0x00011CEC
		internal void Append(ItemData itemData, string nonLocalizedAssistantName)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
			logRowFormatter[4] = itemData.ItemAuditLogData.MessageSubject;
			logRowFormatter[5] = ((itemData.MessageReceivedDate == DateTime.MinValue) ? string.Empty : itemData.MessageReceivedDate.ToString());
			logRowFormatter[6] = itemData.ItemAuditLogData.MessageSender;
			logRowFormatter[7] = itemData.ItemAuditLogData.MessageInternetId;
			logRowFormatter[8] = itemData.ItemAuditLogData.MessageClass;
			this.AppendFolder(itemData.ItemAuditLogData.FolderAuditLogData, logRowFormatter, nonLocalizedAssistantName);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00013B94 File Offset: 0x00011D94
		internal void Append(FolderAuditLogData folderAuditLogData, string nonLocalizedAssistantName)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
			this.AppendFolder(folderAuditLogData, logRowFormatter, nonLocalizedAssistantName);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00013BB8 File Offset: 0x00011DB8
		internal void Append(string comment, string nonLocalizedAssistantName)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
			logRowFormatter[11] = comment;
			this.Append(logRowFormatter, nonLocalizedAssistantName);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00013BE2 File Offset: 0x00011DE2
		internal void Close()
		{
			if (this.log != null)
			{
				this.log.Close();
				this.log = null;
			}
			ElcAuditLog.Tracer.TraceDebug<ElcAuditLog>((long)this.GetHashCode(), "{0}: Closed log.", this);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00013C18 File Offset: 0x00011E18
		private Server GetServerObjectFromAD(string nonLocalizedAssistantName)
		{
			ElcAuditLog.Tracer.TraceDebug((long)this.GetHashCode(), "Starting to read the Server object from AD.");
			Server server = ExchangeServer.TryGetServer(nonLocalizedAssistantName);
			if (server == null)
			{
				ElcAuditLog.Tracer.TraceError((long)this.GetHashCode(), "Unable to get Exchange server object from AD.");
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_FailedToReadAuditLogArgsFromAD, null, new object[]
				{
					this.databaseInfo.DisplayName
				});
				throw new TransientMailboxException(Strings.descMissingServerConfig);
			}
			return server;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00013C90 File Offset: 0x00011E90
		private void AppendFolder(FolderAuditLogData folderAuditLogData, LogRowFormatter logRowFormatter, string nonLocalizedAssistantName)
		{
			logRowFormatter[1] = folderAuditLogData.ElcAction;
			logRowFormatter[2] = FolderProcessor.NormalizeFolderPath(folderAuditLogData.FolderFullPath);
			logRowFormatter[3] = folderAuditLogData.MailboxOwner;
			logRowFormatter[9] = folderAuditLogData.ExpirationAction;
			logRowFormatter[10] = folderAuditLogData.AutoCopyAddress;
			this.Append(logRowFormatter, nonLocalizedAssistantName);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00013D38 File Offset: 0x00011F38
		private void Append(LogRowFormatter logRowFormatter, string nonLocalizedAssistantName)
		{
			LocalizedException exception = null;
			try
			{
				Util.CatchMeIfYouCan(delegate
				{
					try
					{
						this.log.Append(logRowFormatter, 0);
					}
					catch (LogException exception2)
					{
						exception = exception2;
					}
				}, nonLocalizedAssistantName);
			}
			catch (AIGrayException exception)
			{
				AIGrayException exception3;
				exception = exception3;
			}
			if (exception != null)
			{
				ElcAuditLog.Tracer.TraceError<ElcAuditLog, LocalizedException>((long)this.GetHashCode(), "{0}: The ELC Assistant failed to write to the audit log. Exception: {1}", this, exception);
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_AppendAuditLogFailed, null, new object[]
				{
					this.databaseInfo.DisplayName,
					exception
				});
				throw new TransientDatabaseException(Strings.descAppendAuditLogFailed(this.databaseInfo.DisplayName), exception, null);
			}
		}

		// Token: 0x04000288 RID: 648
		private const string SoftwareTitle = "Microsoft Exchange Mailbox Server";

		// Token: 0x04000289 RID: 649
		private const string LogType = "Managed Folder Assistant";

		// Token: 0x0400028A RID: 650
		private const string LogComponent = "Managed Folder Assistant Logs";

		// Token: 0x0400028B RID: 651
		private const string PrefixConstantPart = "Managed_Folder_Assistant";

		// Token: 0x0400028C RID: 652
		private static readonly Trace Tracer = ExTraceGlobals.ExpirationEnforcerTracer;

		// Token: 0x0400028D RID: 653
		private static readonly string[] Fields = new string[Enum.GetValues(typeof(ElcAuditLog.Field)).Length];

		// Token: 0x0400028E RID: 654
		private bool expirationLoggingEnabled;

		// Token: 0x0400028F RID: 655
		private bool autocopyLoggingEnabled;

		// Token: 0x04000290 RID: 656
		private bool folderLoggingEnabled;

		// Token: 0x04000291 RID: 657
		private bool subjectLoggingEnabled = true;

		// Token: 0x04000292 RID: 658
		private LogSchema logSchema;

		// Token: 0x04000293 RID: 659
		private LogHeaderFormatter logHeaderFormatter;

		// Token: 0x04000294 RID: 660
		private Log log;

		// Token: 0x04000295 RID: 661
		private DatabaseInfo databaseInfo;

		// Token: 0x04000296 RID: 662
		private string toString;

		// Token: 0x04000297 RID: 663
		private string filenamePrefix;

		// Token: 0x02000059 RID: 89
		internal enum Field
		{
			// Token: 0x04000299 RID: 665
			Datetime,
			// Token: 0x0400029A RID: 666
			ELCActionType,
			// Token: 0x0400029B RID: 667
			FolderFullPath,
			// Token: 0x0400029C RID: 668
			MailboxOwner,
			// Token: 0x0400029D RID: 669
			MessageSubject,
			// Token: 0x0400029E RID: 670
			MessageReceivedDate,
			// Token: 0x0400029F RID: 671
			MessageSender,
			// Token: 0x040002A0 RID: 672
			MessageInternetId,
			// Token: 0x040002A1 RID: 673
			MessageClass,
			// Token: 0x040002A2 RID: 674
			ExpirationActionTaken,
			// Token: 0x040002A3 RID: 675
			AutocopyDestinationAddress,
			// Token: 0x040002A4 RID: 676
			Comment
		}
	}
}
