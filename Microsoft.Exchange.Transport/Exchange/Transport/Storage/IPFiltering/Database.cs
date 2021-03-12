using System;
using System.IO;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport.Storage.IPFiltering
{
	// Token: 0x0200011F RID: 287
	internal class Database : ITransportComponent
	{
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0002FA4C File Offset: 0x0002DC4C
		public static DataSource DataSource
		{
			get
			{
				return Database.dataSource;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0002FA53 File Offset: 0x0002DC53
		public static IPFilteringTable Table
		{
			get
			{
				return Database.table;
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0002FA5C File Offset: 0x0002DC5C
		public static void Attach()
		{
			try
			{
				Database.SetDefaultDatabasePath();
				Database.LoadConfig();
				Database.dataSource = new DataSource(Strings.IPFilterDatabaseInstanceName, Database.databasePath, "IpFiltering.edb", 1, null, Database.logFilePath, null);
				Database.dataSource.LogBuffers = Database.logBufferSize;
				Database.dataSource.LogFileSize = Database.logFileSize;
				Database.dataSource.OpenDatabase();
				Database.VersionTable versionTable = new Database.VersionTable();
				using (DataConnection dataConnection = Database.dataSource.DemandNewConnection())
				{
					versionTable.Attach(Database.dataSource, dataConnection);
					Database.table.Attach(Database.dataSource, dataConnection);
				}
				versionTable.Detach();
				IPFilterLists.Load();
			}
			catch (EsentFileAccessDeniedException ex)
			{
				Components.EventLogger.LogEvent(TransportEventLogConstants.Tuple_DatabaseInUse, null, new object[]
				{
					Strings.IPFilterDatabaseInstanceName,
					ex
				});
				string notificationReason = string.Format("Database {0} is already in use. The service will be stopped. Exception details: {1}", Strings.IPFilterDatabaseInstanceName, ex);
				EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, notificationReason, ResultSeverityLevel.Error, false);
				throw new TransportComponentLoadFailedException(Strings.DataBaseInUse(Strings.IPFilterDatabaseInstanceName), ex);
			}
			catch (SchemaException inner)
			{
				throw new TransportComponentLoadFailedException(Strings.DatabaseAttachFailed(Strings.IPFilterDatabaseInstanceName), inner);
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0002FBC8 File Offset: 0x0002DDC8
		public static void Detach()
		{
			IPFilterLists.Cleanup();
			Database.Table.Detach();
			Database.dataSource.CloseDatabase(false);
			Database.dataSource = null;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0002FBEA File Offset: 0x0002DDEA
		public void Load()
		{
			Database.Attach();
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0002FBF1 File Offset: 0x0002DDF1
		public void Unload()
		{
			Database.Detach();
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0002FBF8 File Offset: 0x0002DDF8
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0002FBFB File Offset: 0x0002DDFB
		private static void SetDefaultDatabasePath()
		{
			Database.databasePath = Path.Combine(ConfigurationContext.Setup.InstallPath, "TransportRoles\\data\\IpFilter\\");
			Database.logFilePath = Database.databasePath;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0002FC1C File Offset: 0x0002DE1C
		private static void LoadConfig()
		{
			if (!string.IsNullOrEmpty(Components.TransportAppConfig.IPFilteringDatabase.DatabasePath))
			{
				Database.databasePath = Components.TransportAppConfig.IPFilteringDatabase.DatabasePath;
			}
			if (!string.IsNullOrEmpty(Components.TransportAppConfig.IPFilteringDatabase.LogFilePath))
			{
				Database.logFilePath = Components.TransportAppConfig.IPFilteringDatabase.LogFilePath;
			}
			Database.logFileSize = Components.TransportAppConfig.IPFilteringDatabase.LogFileSize;
			Database.logBufferSize = Components.TransportAppConfig.IPFilteringDatabase.LogBufferSize;
		}

		// Token: 0x04000596 RID: 1430
		private static DataSource dataSource;

		// Token: 0x04000597 RID: 1431
		private static string databasePath;

		// Token: 0x04000598 RID: 1432
		private static string logFilePath;

		// Token: 0x04000599 RID: 1433
		private static uint logFileSize = 524288U;

		// Token: 0x0400059A RID: 1434
		private static uint logBufferSize = 5120U;

		// Token: 0x0400059B RID: 1435
		private static IPFilteringTable table = new IPFilteringTable();

		// Token: 0x02000120 RID: 288
		private class VersionTable : DataTable
		{
			// Token: 0x06000D1C RID: 3356 RVA: 0x0002FCD0 File Offset: 0x0002DED0
			protected override void AttachLoadInitValues(Transaction transaction, DataTableCursor cursor)
			{
				DataColumn<long> dataColumn = (DataColumn<long>)base.Schemas[0];
				if (cursor.Connection.Source.NewDatabase)
				{
					cursor.PrepareInsert(false, false);
					dataColumn.WriteToCursor(cursor, 2L);
					cursor.Update();
					return;
				}
				cursor.MoveFirst();
				long num = dataColumn.ReadFromCursor(cursor);
				ExTraceGlobals.GeneralTracer.TraceDebug<long, long>((long)this.GetHashCode(), "IP Filtering database opened with version: {0} required: {1}", num, 2L);
				if (num != 2L)
				{
					string text = string.Empty;
					string text2 = string.Empty;
					try
					{
						text = cursor.Connection.Source.DatabasePath;
						text2 = cursor.Connection.Source.LogFilePath;
					}
					catch (Exception)
					{
					}
					Components.EventLogger.LogEvent(TransportEventLogConstants.Tuple_DatabaseSchemaNotSupported, null, new object[]
					{
						Strings.IPFilterDatabaseInstanceName,
						num,
						2L,
						text,
						text2
					});
					throw new TransportComponentLoadFailedException(Strings.DatabaseSchemaNotSupported(Strings.IPFilterDatabaseInstanceName));
				}
			}

			// Token: 0x0400059C RID: 1436
			[DataColumnDefinition(typeof(long), ColumnAccess.CachedProp, Required = true)]
			public const int Version = 0;

			// Token: 0x0400059D RID: 1437
			private const long RequiredVersion = 2L;
		}
	}
}
