using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.FfoSyncLog;
using Microsoft.Exchange.FfoSyncLog;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Win32;

namespace Microsoft.Exchange.Transport.LoggingCommon
{
	// Token: 0x0200001B RID: 27
	public class TenantSettingSyncLogGenerator : IDisposable
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00003298 File Offset: 0x00001498
		protected TenantSettingSyncLogGenerator()
		{
			try
			{
				this.isSupportedOnCurrentSku = (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled || DatacenterRegistry.IsForefrontForOffice());
			}
			catch (Exception)
			{
			}
			if (!this.isSupportedOnCurrentSku)
			{
				return;
			}
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeLabs\\FfoTenantSettingsSyncConfig"))
				{
					if (registryKey != null)
					{
						string text = registryKey.GetValue("RootLogPath") as string;
						if (!string.IsNullOrWhiteSpace(text))
						{
							this.LogDirectoryPath = text;
						}
						else
						{
							this.EventLogger.LogEvent(FfoSyncLogEventLogConstants.Tuple_FfoSyncLogLogPathNotConfigured, null, new object[0]);
						}
					}
				}
				using (RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeLabs\\FfoTenantSettingsSyncConfig"))
				{
					if (registryKey2 != null)
					{
						this.MaxFileSizeMB = (int)registryKey2.GetValue("LogFileMaxSizeMB", this.MaxFileSizeMB);
						this.MaxDirSizeMB = (int)registryKey2.GetValue("LogDirectoryMaxSizeMB", this.MaxDirSizeMB);
						this.BufferSizeBytes = (int)registryKey2.GetValue("LogBufferCacheSizeBytes", this.BufferSizeBytes);
						this.MaxAgeDays = (int)registryKey2.GetValue("LogFileMaxAgeDays", this.MaxAgeDays);
						this.FlushIntervalSeconds = (int)registryKey2.GetValue("LogFlushIntervalSeconds", this.FlushIntervalSeconds);
					}
				}
			}
			catch (SecurityException ex)
			{
				this.EventLogger.LogEvent(FfoSyncLogEventLogConstants.Tuple_FfoSyncLogConfigRegistryReadAccessException, "SOFTWARE\\Microsoft\\ExchangeLabs\\FfoTenantSettingsSyncConfig", new object[]
				{
					"SOFTWARE\\Microsoft\\ExchangeLabs\\FfoTenantSettingsSyncConfig",
					ex.ToString()
				});
			}
			catch (UnauthorizedAccessException ex2)
			{
				this.EventLogger.LogEvent(FfoSyncLogEventLogConstants.Tuple_FfoSyncLogConfigRegistryReadAccessException, "SOFTWARE\\Microsoft\\ExchangeLabs\\FfoTenantSettingsSyncConfig", new object[]
				{
					"SOFTWARE\\Microsoft\\ExchangeLabs\\FfoTenantSettingsSyncConfig",
					ex2.ToString()
				});
			}
			this.schema = new LogSchema("Microsoft Exchange/FFO Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "TenantSettingSyncLog", Enum.GetNames(typeof(TenantSettingSchemaFields)));
			this.mapLogs = new Dictionary<TenantSettingSyncLogType, Log>();
			foreach (object obj in Enum.GetValues(typeof(TenantSettingSyncLogType)))
			{
				TenantSettingSyncLogType key = (TenantSettingSyncLogType)obj;
				this.mapLogs.Add(key, null);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000035C0 File Offset: 0x000017C0
		public static TenantSettingSyncLogGenerator Instance
		{
			get
			{
				if (TenantSettingSyncLogGenerator.instance == null)
				{
					lock (TenantSettingSyncLogGenerator.syncObject)
					{
						if (TenantSettingSyncLogGenerator.instance == null)
						{
							TenantSettingSyncLogGenerator.instance = new TenantSettingSyncLogGenerator();
						}
					}
				}
				return TenantSettingSyncLogGenerator.instance;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00003620 File Offset: 0x00001820
		public bool Enabled
		{
			get
			{
				return this.isSupportedOnCurrentSku && this.LogDirectoryPath != null;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00003638 File Offset: 0x00001838
		private ExEventLog EventLogger
		{
			get
			{
				if (this.eventLogger == null)
				{
					this.eventLogger = new ExEventLog(ExTraceGlobals.LogGenTracer.Category, "FfoSyncLog");
				}
				return this.eventLogger;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003662 File Offset: 0x00001862
		public bool LogChangesForSave(ADObject savedObject, Guid? externalDirectoryOrgId = null, Guid? immutableObjectId = null, List<KeyValuePair<string, object>> customProperties = null)
		{
			return this.LogChangesForSave(savedObject, this.GetLogType(savedObject), externalDirectoryOrgId, immutableObjectId, customProperties);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003678 File Offset: 0x00001878
		public bool LogChangesForSave(ADObject savedObject, TenantSettingSyncLogType logType, Guid? externalDirectoryOrgId = null, Guid? immutableObjectId = null, List<KeyValuePair<string, object>> customProperties = null)
		{
			if (!this.Enabled)
			{
				return false;
			}
			if (savedObject != null && savedObject.OrganizationId != OrganizationId.ForestWideOrgId && savedObject.OrganizationalUnitRoot != null)
			{
				Guid organizationUnitRootId = (externalDirectoryOrgId != null) ? externalDirectoryOrgId.Value : savedObject.OrganizationalUnitRoot.ObjectGuid;
				Guid value = (immutableObjectId != null) ? immutableObjectId.Value : savedObject.Guid;
				return this.AddLogLine(logType, TenantSettingChangeType.Save, organizationUnitRootId, savedObject.Name, new Guid?(value), savedObject.WhenChangedUTC, customProperties);
			}
			return false;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003702 File Offset: 0x00001902
		public bool LogChangesForDelete(ADObject deletedobject, Guid? externalDirectoryOrgId = null)
		{
			return this.LogChangesForDelete(deletedobject, this.GetLogType(deletedobject), externalDirectoryOrgId);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003714 File Offset: 0x00001914
		public bool LogChangesForDelete(ADObject deletedobject, TenantSettingSyncLogType logType, Guid? externalDirectoryOrgId = null)
		{
			if (!this.Enabled)
			{
				return false;
			}
			if (deletedobject != null && deletedobject.OrganizationId != OrganizationId.ForestWideOrgId && deletedobject.OrganizationalUnitRoot != null)
			{
				Guid organizationUnitRootId = (externalDirectoryOrgId != null) ? externalDirectoryOrgId.Value : deletedobject.OrganizationalUnitRoot.ObjectGuid;
				return this.AddLogLine(logType, TenantSettingChangeType.Delete, organizationUnitRootId, deletedobject.Name, null, null, null);
			}
			return false;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000378A File Offset: 0x0000198A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000379C File Offset: 0x0000199C
		public TenantSettingSyncLogType GetLogType(ADObject objectInstance)
		{
			Type type = objectInstance.GetType();
			if (objectInstance is TransportRule)
			{
				return TenantSettingSyncLogType.SYNCTR;
			}
			if (objectInstance is ADComplianceProgram)
			{
				return TenantSettingSyncLogType.SYNCADCP;
			}
			if (objectInstance is HostedConnectionFilterPolicy)
			{
				return TenantSettingSyncLogType.SYNCCONNPOL;
			}
			if (objectInstance is HostedOutboundSpamFilterPolicy)
			{
				return TenantSettingSyncLogType.SYNCOBSPAMPOL;
			}
			if (objectInstance is TenantInboundConnector)
			{
				return TenantSettingSyncLogType.SYNCICONN;
			}
			if (objectInstance is DomainContentConfig)
			{
				return TenantSettingSyncLogType.SYNCDOMCON;
			}
			if (objectInstance is HostedContentFilterPolicy)
			{
				return TenantSettingSyncLogType.DUALSYNCCONTPOL;
			}
			if (objectInstance is AcceptedDomain)
			{
				return TenantSettingSyncLogType.SYNCACCEPTEDDOM;
			}
			string message = string.Format(CultureInfo.InvariantCulture, "TenantSettingSync is not supported for this type: {0}", new object[]
			{
				type
			});
			throw new InvalidOperationException(message);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003824 File Offset: 0x00001A24
		internal void AddEventLogOnADException(ADOperationResult opResult)
		{
			if (this.Enabled && opResult != null && !opResult.Succeeded)
			{
				this.EventLogger.LogEvent(FfoSyncLogEventLogConstants.Tuple_FfoSyncLogADOperationException, opResult.Exception.Message, new object[]
				{
					opResult.Exception.ToString()
				});
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003878 File Offset: 0x00001A78
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.mapLogs != null)
			{
				List<TenantSettingSyncLogType> list = this.mapLogs.Keys.ToList<TenantSettingSyncLogType>();
				foreach (TenantSettingSyncLogType key in list)
				{
					if (this.mapLogs[key] != null)
					{
						this.mapLogs[key].Close();
						this.mapLogs[key] = null;
					}
				}
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003908 File Offset: 0x00001B08
		protected virtual string GetLogPrefix(TenantSettingSyncLogType logType)
		{
			return logType.ToString();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003918 File Offset: 0x00001B18
		private void ConfigureLog(TenantSettingSyncLogType logType)
		{
			if (this.mapLogs[logType] == null)
			{
				lock (TenantSettingSyncLogGenerator.syncObject)
				{
					if (this.mapLogs[logType] == null)
					{
						Log log = new Log(this.GetLogPrefix(logType), new LogHeaderFormatter(this.schema), "TenantSettingSyncLog");
						log.Configure(this.LogDirectoryPath, TimeSpan.FromDays((double)this.MaxAgeDays), (long)this.MaxDirSizeMB * 1024L * 1024L, (long)this.MaxFileSizeMB * 1024L * 1024L, this.BufferSizeBytes, TimeSpan.FromSeconds((double)this.FlushIntervalSeconds), true);
						this.mapLogs[logType] = log;
						this.EventLogger.LogEvent(FfoSyncLogEventLogConstants.Tuple_FfoSyncLogConfigured, null, new object[]
						{
							logType,
							this.LogDirectoryPath
						});
					}
				}
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003A20 File Offset: 0x00001C20
		private bool AddLogLine(TenantSettingSyncLogType logType, TenantSettingChangeType changeType, Guid organizationUnitRootId, string name, Guid? id = null, DateTime? changedTimeUTC = null, List<KeyValuePair<string, object>> customProperties = null)
		{
			this.ConfigureLog(logType);
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.schema);
			logRowFormatter[0] = ((changedTimeUTC != null) ? changedTimeUTC.Value : DateTime.UtcNow);
			logRowFormatter[1] = (int)changeType;
			logRowFormatter[2] = organizationUnitRootId;
			logRowFormatter[4] = name;
			if (id != null)
			{
				logRowFormatter[3] = id.Value;
			}
			try
			{
				logRowFormatter[5] = customProperties;
			}
			catch (ArgumentException ex)
			{
				this.EventLogger.LogEvent(FfoSyncLogEventLogConstants.Tuple_FfoSyncLogFormatException, logType.ToString(), new object[]
				{
					ex.ToString()
				});
				return false;
			}
			this.mapLogs[logType].Append(logRowFormatter, -1);
			return true;
		}

		// Token: 0x04000104 RID: 260
		private const string ConfigRegistryKey = "SOFTWARE\\Microsoft\\ExchangeLabs\\FfoTenantSettingsSyncConfig";

		// Token: 0x04000105 RID: 261
		private const string LogPathEntry = "RootLogPath";

		// Token: 0x04000106 RID: 262
		private const string MaxFileSizeEntry = "LogFileMaxSizeMB";

		// Token: 0x04000107 RID: 263
		private const string MaxDirSizeEntry = "LogDirectoryMaxSizeMB";

		// Token: 0x04000108 RID: 264
		private const string BufferSizeEntry = "LogBufferCacheSizeBytes";

		// Token: 0x04000109 RID: 265
		private const string MaxAgeEntry = "LogFileMaxAgeDays";

		// Token: 0x0400010A RID: 266
		private const string FlushIntervalEntry = "LogFlushIntervalSeconds";

		// Token: 0x0400010B RID: 267
		private const string LogSoftwareName = "Microsoft Exchange/FFO Server";

		// Token: 0x0400010C RID: 268
		private const string LogComponentName = "TenantSettingSyncLog";

		// Token: 0x0400010D RID: 269
		private readonly int MaxFileSizeMB = 10;

		// Token: 0x0400010E RID: 270
		private readonly int MaxDirSizeMB;

		// Token: 0x0400010F RID: 271
		private readonly int BufferSizeBytes = 4096;

		// Token: 0x04000110 RID: 272
		private readonly int MaxAgeDays = 30;

		// Token: 0x04000111 RID: 273
		private readonly int FlushIntervalSeconds = 30;

		// Token: 0x04000112 RID: 274
		private readonly LogSchema schema;

		// Token: 0x04000113 RID: 275
		private readonly string LogDirectoryPath;

		// Token: 0x04000114 RID: 276
		private readonly bool isSupportedOnCurrentSku;

		// Token: 0x04000115 RID: 277
		private static volatile TenantSettingSyncLogGenerator instance;

		// Token: 0x04000116 RID: 278
		private static object syncObject = new object();

		// Token: 0x04000117 RID: 279
		private Dictionary<TenantSettingSyncLogType, Log> mapLogs;

		// Token: 0x04000118 RID: 280
		private ExEventLog eventLogger;
	}
}
