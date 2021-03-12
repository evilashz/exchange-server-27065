using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.BackSync
{
	// Token: 0x0200009E RID: 158
	internal class BackSyncAuditLog
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000158A0 File Offset: 0x00013AA0
		public static bool IsEnabled
		{
			get
			{
				bool result;
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(BackSyncAuditLog.AuditLogRegistryKey))
				{
					int num = (registryKey != null) ? ((int)registryKey.GetValue("Enable", 1)) : 1;
					result = (num > 0);
				}
				return result;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x000158FC File Offset: 0x00013AFC
		public static BackSyncAuditLog Instance
		{
			get
			{
				if (BackSyncAuditLog.instance == null)
				{
					lock (BackSyncAuditLog.syncRoot)
					{
						if (BackSyncAuditLog.instance == null)
						{
							BackSyncAuditLog.instance = new BackSyncAuditLog();
						}
					}
				}
				return BackSyncAuditLog.instance;
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00015954 File Offset: 0x00013B54
		static BackSyncAuditLog()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(BackSyncAuditLog.AuditLogRegistryKey))
			{
				if (registryKey != null)
				{
					BackSyncAuditLog.LogDirectory = (string)registryKey.GetValue("LogDirectory", BackSyncAuditLog.LogDirectory);
					BackSyncAuditLog.LogFileMaxAge = (int)registryKey.GetValue("LogFileMaxAge", BackSyncAuditLog.LogFileMaxAge);
					BackSyncAuditLog.LogDirectoryMaxSize = (int)registryKey.GetValue("LogDirectoryMaxSize", BackSyncAuditLog.LogDirectoryMaxSize);
					BackSyncAuditLog.LogFileMaxSize = (int)registryKey.GetValue("LogFileMaxSize", BackSyncAuditLog.LogFileMaxSize);
					BackSyncAuditLog.LogCacheSize = (int)registryKey.GetValue("LogCacheSize", BackSyncAuditLog.LogCacheSize);
					BackSyncAuditLog.LogFlushInterval = (int)registryKey.GetValue("LogFlushInterval", BackSyncAuditLog.LogFlushInterval);
				}
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00015B34 File Offset: 0x00013D34
		private BackSyncAuditLog()
		{
			this.auditLogger = new Log("BackSyncAudit", new LogHeaderFormatter(BackSyncAuditLog.AuditLogSchema, true), "MSExchange Back Sync");
			this.auditLogger.Configure(BackSyncAuditLog.LogDirectory, TimeSpan.FromDays((double)BackSyncAuditLog.LogFileMaxAge), (long)BackSyncAuditLog.LogDirectoryMaxSize * 1024L * 1024L, (long)BackSyncAuditLog.LogFileMaxSize * 1024L * 1024L, BackSyncAuditLog.LogCacheSize * 1024 * 1024, TimeSpan.FromSeconds((double)BackSyncAuditLog.LogFlushInterval), true);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00015BC8 File Offset: 0x00013DC8
		public void Append(string executingUser, byte[] cookie, NameValueCollection parameters, object response, Dictionary<SyncObject, Exception> errors)
		{
			string responseIdentity = Guid.NewGuid().ToString("N");
			this.Append(BackSyncAuditLog.CreateLogRowForFirstLine(executingUser, cookie, parameters, responseIdentity));
			if (response is GetChangesResponse)
			{
				DirectoryChanges getChangesResult = ((GetChangesResponse)response).GetChangesResult;
				this.Append<DirectoryObject>(responseIdentity, getChangesResult.Objects);
				this.Append<DirectoryLink>(responseIdentity, getChangesResult.Links);
			}
			else
			{
				if (!(response is GetDirectoryObjectsResponse))
				{
					throw new NotImplementedException("Need implement writing audit log for response type: " + response.GetType().Name);
				}
				DirectoryObjectsAndLinks getDirectoryObjectsResult = ((GetDirectoryObjectsResponse)response).GetDirectoryObjectsResult;
				this.Append<DirectoryObject>(responseIdentity, getDirectoryObjectsResult.Objects);
				this.Append<DirectoryLink>(responseIdentity, getDirectoryObjectsResult.Links);
				this.Append<DirectoryObjectError>(responseIdentity, getDirectoryObjectsResult.Errors);
			}
			this.Append(responseIdentity, errors);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00015C90 File Offset: 0x00013E90
		private void Append<T>(string responseIdentity, T[] objects)
		{
			if (objects == null || objects.Length == 0)
			{
				return;
			}
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			StringBuilder stringBuilder = new StringBuilder();
			XmlWriterSettings settings = new XmlWriterSettings
			{
				OmitXmlDeclaration = true,
				Indent = false
			};
			for (int i = 0; i < objects.Length; i++)
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, settings))
				{
					xmlSerializer.Serialize(xmlWriter, objects[i]);
					this.Append(BackSyncAuditLog.CreateLogRowForSyncObject(responseIdentity, objects[i], stringBuilder.ToString()));
					stringBuilder.Length = 0;
				}
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00015D44 File Offset: 0x00013F44
		private void Append(string responseIdentity, Dictionary<SyncObject, Exception> errors)
		{
			foreach (KeyValuePair<SyncObject, Exception> keyValuePair in errors)
			{
				this.Append(BackSyncAuditLog.CreateLogRowForError(responseIdentity, keyValuePair.Key, keyValuePair.Value));
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00015DA8 File Offset: 0x00013FA8
		private void Append(LogRowFormatter row)
		{
			this.auditLogger.Append(row, 0);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00015DB8 File Offset: 0x00013FB8
		private static LogRowFormatter CreateLogRowForSyncObject(string responseIdentity, object syncObject, string objectInXml)
		{
			string objectId = null;
			string sourceId = null;
			string targetId = null;
			string contextId;
			if (syncObject is DirectoryObject)
			{
				DirectoryObject directoryObject = (DirectoryObject)syncObject;
				objectId = directoryObject.ObjectId;
				contextId = directoryObject.ContextId;
			}
			else if (syncObject is DirectoryObjectError)
			{
				DirectoryObjectError directoryObjectError = (DirectoryObjectError)syncObject;
				objectId = directoryObjectError.ObjectId;
				contextId = directoryObjectError.ContextId;
			}
			else
			{
				if (!(syncObject is DirectoryLink))
				{
					throw new NotSupportedException("Don't know how to extract IDs for new type: " + syncObject.GetType().Name);
				}
				DirectoryLink directoryLink = (DirectoryLink)syncObject;
				contextId = directoryLink.ContextId;
				sourceId = directoryLink.SourceId;
				targetId = directoryLink.TargetId;
			}
			return BackSyncAuditLog.CreateLogRow(null, null, null, responseIdentity, objectId, contextId, sourceId, targetId, objectInXml, null);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00015E68 File Offset: 0x00014068
		private static LogRowFormatter CreateLogRowForFirstLine(string executingUser, byte[] cookie, NameValueCollection parameters, string responseIdentity)
		{
			string cookieInBase = (cookie == null) ? null : Convert.ToBase64String(cookie);
			string parameters2 = (parameters == null) ? null : BackSyncAuditLog.ToParametersString(parameters);
			return BackSyncAuditLog.CreateLogRow(executingUser, cookieInBase, parameters2, responseIdentity, null, null, null, null, null, null);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00015EA0 File Offset: 0x000140A0
		private static LogRowFormatter CreateLogRowForError(string responseIdentity, SyncObject syncObject, Exception e)
		{
			return BackSyncAuditLog.CreateLogRow(null, null, null, responseIdentity, syncObject.ObjectId, syncObject.ContextId, null, null, null, e.Message);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00015ECC File Offset: 0x000140CC
		private static LogRowFormatter CreateLogRow(string executingUser, string cookieInBase64, string parameters, string responseIdentity, string objectId, string contextId, string sourceId, string targetId, string objectInXml, string error)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(BackSyncAuditLog.AuditLogSchema);
			logRowFormatter[0] = null;
			logRowFormatter[1] = executingUser;
			logRowFormatter[2] = cookieInBase64;
			logRowFormatter[3] = parameters;
			logRowFormatter[4] = responseIdentity;
			logRowFormatter[5] = objectId;
			logRowFormatter[6] = contextId;
			logRowFormatter[7] = sourceId;
			logRowFormatter[8] = targetId;
			logRowFormatter[9] = ((objectInXml == null) ? null : objectInXml.Replace('\r', ' ').Replace('\n', ' '));
			logRowFormatter[10] = ((error == null) ? null : error.Replace('\r', ' ').Replace('\n', ' '));
			return logRowFormatter;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00015F78 File Offset: 0x00014178
		private static string ToParametersString(NameValueCollection parameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in parameters)
			{
				string text = (string)obj;
				stringBuilder.AppendFormat("{0}: {1};", text, parameters[text]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400027D RID: 637
		private const string ParameterStringFormat = "{0}: {1};";

		// Token: 0x0400027E RID: 638
		private const string ComponentName = "MSExchange Back Sync";

		// Token: 0x0400027F RID: 639
		private const string LogFileNamePrefix = "BackSyncAudit";

		// Token: 0x04000280 RID: 640
		private const string LogType = "Audit Log";

		// Token: 0x04000281 RID: 641
		private const string LogEnableValueName = "Enable";

		// Token: 0x04000282 RID: 642
		private const string LogDirectoryValueName = "LogDirectory";

		// Token: 0x04000283 RID: 643
		private const string LogFileMaxAgeValueName = "LogFileMaxAge";

		// Token: 0x04000284 RID: 644
		private const string LogDirectoryMaxSizeValueName = "LogDirectoryMaxSize";

		// Token: 0x04000285 RID: 645
		private const string LogFileMaxSizeValueName = "LogFileMaxSize";

		// Token: 0x04000286 RID: 646
		private const string LogCacheSizeName = "LogCacheSize";

		// Token: 0x04000287 RID: 647
		private const string LogFlushIntervalName = "LogFlushInterval";

		// Token: 0x04000288 RID: 648
		private const int LogEnableDefaultValue = 1;

		// Token: 0x04000289 RID: 649
		private static readonly string[] LogFields = new string[]
		{
			"Timestamp",
			"ExecutingUser",
			"Cookie",
			"Parameters",
			"ResponseIdentity",
			"ObjectID",
			"ContextID",
			"SourceID",
			"TargetID",
			"Object in XML",
			"Error"
		};

		// Token: 0x0400028A RID: 650
		private static readonly string LogVersion = ConfigurationContext.Setup.GetExecutingVersion().ToString();

		// Token: 0x0400028B RID: 651
		private static readonly string AuditLogRegistryKey = Path.Combine("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\BackSync", "AuditLog");

		// Token: 0x0400028C RID: 652
		private static readonly string LogDirectory = Path.Combine(ConfigurationContext.Setup.InstallPath, "Logging\\BackSyncLogs\\");

		// Token: 0x0400028D RID: 653
		private static readonly int LogFileMaxSize = 100;

		// Token: 0x0400028E RID: 654
		private static readonly int LogFileMaxAge = 30;

		// Token: 0x0400028F RID: 655
		private static readonly int LogDirectoryMaxSize = 30720;

		// Token: 0x04000290 RID: 656
		private static readonly int LogCacheSize = 2;

		// Token: 0x04000291 RID: 657
		private static readonly int LogFlushInterval = 60;

		// Token: 0x04000292 RID: 658
		private static readonly LogSchema AuditLogSchema = new LogSchema("MSExchange Back Sync", BackSyncAuditLog.LogVersion, "Audit Log", BackSyncAuditLog.LogFields);

		// Token: 0x04000293 RID: 659
		private static object syncRoot = new object();

		// Token: 0x04000294 RID: 660
		private static BackSyncAuditLog instance;

		// Token: 0x04000295 RID: 661
		private Log auditLogger;
	}
}
