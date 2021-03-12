using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Sts;
using Microsoft.Exchange.Transport.Storage;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x02000014 RID: 20
	internal class Database
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002FEF File Offset: 0x000011EF
		public static ProtocolAnalysisTable ProtocolAnalysisTable
		{
			get
			{
				return Database.protocolAnalysisTable;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002FF6 File Offset: 0x000011F6
		public static SenderReputationTable SenderReputationTable
		{
			get
			{
				return Database.senderReputationTable;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002FFD File Offset: 0x000011FD
		public static OpenProxyStatusTable OpenProxyStatusTable
		{
			get
			{
				return Database.openProxyStatusTable;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003004 File Offset: 0x00001204
		public static ConfigurationDataTable ConfigurationDataTable
		{
			get
			{
				return Database.configurationDataTable;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000300B File Offset: 0x0000120B
		public static DataSource DataSource
		{
			get
			{
				return Database.dataSource;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003012 File Offset: 0x00001212
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00003024 File Offset: 0x00001224
		public static string DatabasePath
		{
			get
			{
				return Database.databasePath + Database.databaseName;
			}
			set
			{
				Database.databaseName = Path.GetFileName(value);
				Database.databasePath = Path.GetDirectoryName(value);
				if (string.IsNullOrEmpty(Database.databaseName))
				{
					Database.databaseName = "pasettings.edb";
				}
				if (Database.databasePath[Database.databasePath.Length - 1] != Path.DirectorySeparatorChar)
				{
					Database.databasePath += Path.DirectorySeparatorChar;
				}
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003093 File Offset: 0x00001293
		protected static bool IsDbClosed
		{
			get
			{
				return Database.refCount == 0;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000030A0 File Offset: 0x000012A0
		public static void Detach()
		{
			lock (Database.syncObject)
			{
				Database.refCount--;
				if (Database.refCount == 0)
				{
					if (Database.dataSource == null)
					{
						ExTraceGlobals.DatabaseTracer.TraceError(0L, "DbAccess.Detach: refCount == 0 && dataSource == null");
						throw new LocalizedException(DbStrings.DetachRefCountFailed);
					}
					Database.ProtocolAnalysisTable.Detach();
					Database.SenderReputationTable.Detach();
					Database.OpenProxyStatusTable.Detach();
					Database.ConfigurationDataTable.Detach();
					Database.dataSource.CloseDatabase(false);
					Database.dataSource = null;
				}
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000314C File Offset: 0x0000134C
		public static void Attach()
		{
			try
			{
				lock (Database.syncObject)
				{
					if (Database.refCount != 0 && Database.dataSource != null)
					{
						Database.refCount++;
					}
					else
					{
						if (Database.refCount != 0 || Database.dataSource != null)
						{
							ExTraceGlobals.DatabaseTracer.TraceError(0L, "DbAccess.Attach: refCount != 0 && dataSource == null");
							throw new LocalizedException(DbStrings.AttachRefCountFailed);
						}
						Database.SetDefaultDatabasePath();
						Database.dataSource = new DataSource(DbStrings.DatabaseInstanceName, Database.databasePath, Database.databaseName, Database.maxConnections, Database.perfCounterInstanceName, Database.logFilePath, null);
						Database.dataSource.OpenDatabase();
						using (DataConnection dataConnection = Database.dataSource.DemandNewConnection())
						{
							Database.ProtocolAnalysisTable.Attach(Database.dataSource, dataConnection);
							Database.SenderReputationTable.Attach(Database.dataSource, dataConnection);
							Database.OpenProxyStatusTable.Attach(Database.dataSource, dataConnection);
							Database.ConfigurationDataTable.Attach(Database.dataSource, dataConnection);
						}
						Database.refCount++;
					}
				}
			}
			catch (EsentFileAccessDeniedException innerException)
			{
				throw new ExchangeConfigurationException(Strings.DataBaseInUse("ProtocolAnalysis/Sts Database"), innerException);
			}
			catch (SchemaException innerException2)
			{
				throw new ExchangeConfigurationException(Strings.DatabaseAttachFailed("ProtocolAnalysis/Sts Database"), innerException2);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000032F4 File Offset: 0x000014F4
		public static void Initialize(string dbPath)
		{
			Database.databasePath = dbPath;
			if (!string.IsNullOrEmpty(Database.databasePath) && Database.databasePath[Database.databasePath.Length - 1] != Path.DirectorySeparatorChar)
			{
				Database.databasePath += Path.DirectorySeparatorChar;
			}
			Database.Attach();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003350 File Offset: 0x00001550
		public static void PurgeTable(TimeSpan paDeleteTimeSpan, TimeSpan opDeleteTimeSpan, Trace tracer)
		{
			DateTime cutoffTime = DateTime.UtcNow.Subtract(paDeleteTimeSpan);
			DateTime cutoffTime2 = DateTime.UtcNow.Subtract(opDeleteTimeSpan);
			try
			{
				lock (Database.syncObject)
				{
					if (!Database.IsDbClosed)
					{
						PurgingScanner<ProtocolAnalysisRowData, ProtocolAnalysisTable> purgingScanner = new PurgingScanner<ProtocolAnalysisRowData, ProtocolAnalysisTable>(cutoffTime);
						PurgingScanner<OpenProxyStatusRowData, OpenProxyStatusTable> purgingScanner2 = new PurgingScanner<OpenProxyStatusRowData, OpenProxyStatusTable>(cutoffTime2);
						purgingScanner.Scan();
						tracer.TraceDebug(0L, "Purge complete for ProtocolAnalysis table");
						purgingScanner2.Scan();
						tracer.TraceDebug(0L, "Purge complete for OpenProxyStatus table");
					}
				}
			}
			catch
			{
				tracer.TraceDebug(0L, "PurgeTable: Failed to delete data.");
				throw;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003408 File Offset: 0x00001608
		public static void UpdateSenderReputationData(byte[] senderAddressHash, int senderReputationLevel, bool openProxy, DateTime expirationTime, Trace tracer)
		{
			tracer.TraceDebug<int, bool>(0L, "Update sender reputation> srl:{0}, openproxy:{1}.", senderReputationLevel, openProxy);
			try
			{
				lock (Database.syncObject)
				{
					if (!Database.IsDbClosed)
					{
						SenderReputationRowData senderReputationRowData = DataRowAccessBase<SenderReputationTable, SenderReputationRowData>.Find(senderAddressHash);
						if (senderReputationRowData == null)
						{
							senderReputationRowData = DataRowAccessBase<SenderReputationTable, SenderReputationRowData>.NewData(senderAddressHash);
						}
						senderReputationRowData.Srl = senderReputationLevel;
						senderReputationRowData.OpenProxy = openProxy;
						senderReputationRowData.ExpirationTime = expirationTime;
						senderReputationRowData.Commit();
					}
				}
			}
			catch
			{
				tracer.TraceDebug(0L, "UpdateSenderReputationData: Failed to update data.");
				throw;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000034A8 File Offset: 0x000016A8
		public static void DeleteSenderReputationData(byte[] senderAddressHash, Trace tracer)
		{
			tracer.TraceDebug(0L, "Remove sender reputation.");
			try
			{
				lock (Database.syncObject)
				{
					if (!Database.IsDbClosed)
					{
						SenderReputationRowData senderReputationRowData = DataRowAccessBase<SenderReputationTable, SenderReputationRowData>.Find(senderAddressHash);
						if (senderReputationRowData != null)
						{
							senderReputationRowData.MarkToDelete();
							senderReputationRowData.Commit();
						}
					}
				}
			}
			catch
			{
				tracer.TraceDebug(0L, "DeleteSenderReputationData: Failed to delete data.");
				throw;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000352C File Offset: 0x0000172C
		public static void TruncateSenderReputationTable(Trace tracer)
		{
			tracer.TraceDebug(0L, "Truncate sender reputation table.");
			try
			{
				lock (Database.syncObject)
				{
					if (!Database.IsDbClosed)
					{
						PurgingScanner<SenderReputationRowData, SenderReputationTable> purgingScanner = new PurgingScanner<SenderReputationRowData, SenderReputationTable>();
						purgingScanner.Scan();
					}
				}
			}
			catch
			{
				tracer.TraceDebug(0L, "TruncateSenderReputationTable: Failed to delete data.");
				throw;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000035A8 File Offset: 0x000017A8
		public static void UpdateConfiguration(string configName, string configValue, Trace tracer)
		{
			tracer.TraceDebug<string, string>(0L, "Update configuration, name:{0}, value:{1}.", configName, configValue);
			try
			{
				lock (Database.syncObject)
				{
					if (!Database.IsDbClosed)
					{
						ConfigurationDataRowData configurationDataRowData = DataRowAccessBase<ConfigurationDataTable, ConfigurationDataRowData>.Find(configName);
						if (configurationDataRowData == null)
						{
							configurationDataRowData = DataRowAccessBase<ConfigurationDataTable, ConfigurationDataRowData>.NewData(configName);
						}
						configurationDataRowData.ConfigValue = configValue;
						configurationDataRowData.Commit();
					}
				}
			}
			catch
			{
				tracer.TraceDebug(0L, "UpdateConfiguration: Failed to update configuration.");
				throw;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003638 File Offset: 0x00001838
		public static string GetConfiguration(string configName, Trace tracer)
		{
			tracer.TraceDebug<string>(0L, "Lookup configuration, name:{0}.", configName);
			string result = string.Empty;
			try
			{
				lock (Database.syncObject)
				{
					ConfigurationDataRowData configurationDataRowData = DataRowAccessBase<ConfigurationDataTable, ConfigurationDataRowData>.Find(configName);
					if (Database.IsDbClosed)
					{
						return null;
					}
					if (configurationDataRowData == null)
					{
						tracer.TraceDebug<string>(0L, "GetConfiguration: Could not find parameter with name {0}.", configName);
					}
					else
					{
						result = configurationDataRowData.ConfigValue;
					}
				}
			}
			catch
			{
				tracer.TraceDebug<string>(0L, "GetConfiguration: Failed to get configuration value for {0}.", configName);
				throw;
			}
			return result;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000036D8 File Offset: 0x000018D8
		public static PropertyBag ScanSrlConfiguration()
		{
			PropertyBag result;
			lock (Database.syncObject)
			{
				if (Database.IsDbClosed)
				{
					result = Database.propertyBag;
				}
				else
				{
					RowBaseScanner<ConfigurationDataRowData> rowBaseScanner = new RowBaseScanner<ConfigurationDataRowData>(-1, new NextMessage<ConfigurationDataRowData>(Database.HandleSrlSettingsRecord));
					rowBaseScanner.Scan();
					result = Database.propertyBag;
				}
			}
			return result;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003740 File Offset: 0x00001940
		public static void UpdateReverseDns(string senderAddress, string reverseDns, Trace tracer)
		{
			tracer.TraceDebug<string, string>(0L, "Update reverse DNS for sender {0}, reverseDNS {1}", senderAddress, reverseDns);
			try
			{
				lock (Database.syncObject)
				{
					if (!Database.IsDbClosed)
					{
						ProtocolAnalysisRowData protocolAnalysisRowData = DataRowAccessBase<ProtocolAnalysisTable, ProtocolAnalysisRowData>.Find(senderAddress);
						if (protocolAnalysisRowData == null)
						{
							tracer.TraceDebug<string, string>(0L, "Update reverse DNS for sender {0}, reverseDNS {1} failed: sender does not exist in ProtocolAnalysisTable", senderAddress, reverseDns);
						}
						else
						{
							protocolAnalysisRowData.LastQueryTime = DateTime.UtcNow;
							protocolAnalysisRowData.LastUpdateTime = DateTime.UtcNow;
							protocolAnalysisRowData.ReverseDns = reverseDns;
							protocolAnalysisRowData.Processing = false;
							protocolAnalysisRowData.Commit();
						}
					}
				}
			}
			catch
			{
				tracer.TraceDebug<string, string>(0L, "Update reverse DNS for sender {0}, reverseDNS {1} failed.", senderAddress, reverseDns);
				throw;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000037F8 File Offset: 0x000019F8
		public static void UpdateOpenProxy(string senderAddress, OPDetectionResult status, string message, Trace tracer)
		{
			tracer.TraceDebug<string, OPDetectionResult, string>(0L, "Update open proxy status for sender:{0}, status:{1}, comment:{2}", senderAddress, status, message);
			try
			{
				lock (Database.syncObject)
				{
					if (!Database.IsDbClosed)
					{
						OpenProxyStatusRowData openProxyStatusRowData = DataRowAccessBase<OpenProxyStatusTable, OpenProxyStatusRowData>.Find(senderAddress);
						if (openProxyStatusRowData == null)
						{
							tracer.TraceDebug<string>(0L, "Update open proxy for sender {0}, failed: sender does not exist in ProtocolAnalysisTable", senderAddress);
						}
						else
						{
							openProxyStatusRowData.LastDetectionTime = DateTime.UtcNow;
							openProxyStatusRowData.LastAccessTime = DateTime.UtcNow;
							openProxyStatusRowData.OpenProxyStatus = (int)status;
							openProxyStatusRowData.Processing = false;
							openProxyStatusRowData.Message = message;
							openProxyStatusRowData.Commit();
						}
					}
				}
			}
			catch
			{
				tracer.TraceDebug<string>(0L, "Update open proxyfor sender {0}, failed.", senderAddress);
				throw;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000038B4 File Offset: 0x00001AB4
		private static void SetDefaultDatabasePath()
		{
			if (Database.databasePath == null)
			{
				string location = Assembly.GetExecutingAssembly().Location;
				string directoryName = Path.GetDirectoryName(location);
				Database.databasePath = Path.Combine(directoryName, "..\\TransportRoles\\data\\SenderReputation\\");
			}
			Database.logFilePath = Database.databasePath;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000038F4 File Offset: 0x00001AF4
		private static void HandleSrlSettingsRecord(ConfigurationDataRowData data)
		{
			if (Database.propertyBag == null)
			{
				Database.propertyBag = new PropertyBag();
			}
			try
			{
				string configName;
				switch (configName = data.ConfigName)
				{
				case "ZombieKeywords":
				{
					string configValue = data.ConfigValue;
					string[] value = configValue.Split(new char[]
					{
						';'
					});
					Database.propertyBag[data.ConfigName] = value;
					goto IL_148;
				}
				case "ConfigurationVersion":
					Database.propertyBag[data.ConfigName] = data.ConfigValue;
					goto IL_148;
				case "MinWinLen":
				case "MaxWinLen":
				case "GoodBehaviorPeriod":
				case "InitWinLen":
					Database.propertyBag[data.ConfigName] = Convert.ToInt32(data.ConfigValue, CultureInfo.InvariantCulture);
					goto IL_148;
				}
				Database.propertyBag[data.ConfigName] = Convert.ToDouble(data.ConfigValue, CultureInfo.InvariantCulture);
				IL_148:;
			}
			catch (FormatException ex)
			{
				ExTraceGlobals.DatabaseTracer.TraceError<string>(0L, "HandleSrlSettingsRecord: configuration value does not belong to SRL settings: {0}", ex.Message);
			}
			catch (OverflowException ex2)
			{
				ExTraceGlobals.DatabaseTracer.TraceError<string>(0L, "HandleSrlSettingsRecord: configuration value does not belong to SRL settings: {0}", ex2.Message);
			}
		}

		// Token: 0x04000026 RID: 38
		protected const string DefaultDatabaseFileName = "pasettings.edb";

		// Token: 0x04000027 RID: 39
		protected static string databasePath;

		// Token: 0x04000028 RID: 40
		protected static string databaseName = "pasettings.edb";

		// Token: 0x04000029 RID: 41
		protected static string logFilePath;

		// Token: 0x0400002A RID: 42
		protected static object syncObject = new object();

		// Token: 0x0400002B RID: 43
		protected static DataSource dataSource = null;

		// Token: 0x0400002C RID: 44
		private static int refCount = 0;

		// Token: 0x0400002D RID: 45
		private static string perfCounterInstanceName = "paagents";

		// Token: 0x0400002E RID: 46
		private static ProtocolAnalysisTable protocolAnalysisTable = new ProtocolAnalysisTable();

		// Token: 0x0400002F RID: 47
		private static SenderReputationTable senderReputationTable = new SenderReputationTable();

		// Token: 0x04000030 RID: 48
		private static OpenProxyStatusTable openProxyStatusTable = new OpenProxyStatusTable();

		// Token: 0x04000031 RID: 49
		private static ConfigurationDataTable configurationDataTable = new ConfigurationDataTable();

		// Token: 0x04000032 RID: 50
		private static PropertyBag propertyBag = null;

		// Token: 0x04000033 RID: 51
		private static int maxConnections = 100;
	}
}
