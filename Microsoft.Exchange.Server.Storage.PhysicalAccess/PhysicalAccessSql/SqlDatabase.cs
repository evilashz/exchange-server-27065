using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000EF RID: 239
	internal class SqlDatabase : Database
	{
		// Token: 0x06000A45 RID: 2629 RVA: 0x00031DBC File Offset: 0x0002FFBC
		internal SqlDatabase(string mdbName, string logPath, string filePath, string fileName, DatabaseFlags databaseFlags, DatabaseOptions databaseOptions) : base(mdbName, logPath, filePath, fileName, databaseFlags, databaseOptions)
		{
			this.connectionString = DatabaseLocation.GetConnectionString(base.DisplayName);
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00031DDE File Offset: 0x0002FFDE
		public override int PageSize
		{
			get
			{
				return 8192;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x00031DE5 File Offset: 0x0002FFE5
		public override DatabaseType DatabaseType
		{
			get
			{
				return DatabaseType.Sql;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x00031DE8 File Offset: 0x0002FFE8
		internal string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x00031DF0 File Offset: 0x0002FFF0
		public override ILogReplayStatus LogReplayStatus
		{
			get
			{
				throw new StoreException((LID)56776U, ErrorCodeValue.NotSupported, "LogReplayStatus.Get is not supported for SQL database.");
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00031E0B File Offset: 0x0003000B
		public override void Configure()
		{
			this.DoFirstMountProcessing();
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00031E14 File Offset: 0x00030014
		public override bool TryOpen(bool lossyMount)
		{
			try
			{
				using (LockManager.Lock(SqlDatabase.configurationLockObject))
				{
					using (SqlConnection sqlConnection = new SqlConnection(null, "SqlDatabase.TryOpen"))
					{
						if (!this.DatabaseExists(sqlConnection))
						{
							return false;
						}
					}
					using (SqlConnection sqlConnection2 = new SqlConnection(this, "SqlDatabase.TryOpen"))
					{
						this.BeforeMountDatabase(sqlConnection2);
					}
					this.isOpen = true;
				}
			}
			catch (SqlException ex)
			{
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_SQLExceptionDetected, new object[]
				{
					ex.Message,
					ex.StackTrace,
					"SqlDatabase.TryOpen"
				});
				throw new CouldNotCreateDatabaseException(ex.Message, ex);
			}
			return true;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00031F04 File Offset: 0x00030104
		public override bool TryCreate(bool force)
		{
			try
			{
				using (LockManager.Lock(SqlDatabase.configurationLockObject))
				{
					using (SqlConnection sqlConnection = new SqlConnection(null, "SqlDatabase.TryCreate"))
					{
						string text = Path.Combine(base.FilePath, base.DisplayName + ".mdf");
						if (this.DatabaseExists(sqlConnection))
						{
							if (!force)
							{
								return false;
							}
							this.Delete(true);
						}
						else if (File.Exists(text) && !force)
						{
							return false;
						}
						string text2 = Path.Combine(base.FilePath, base.DisplayName + ".ndf");
						string text3 = Path.Combine(base.LogPath, base.DisplayName + ".ldf");
						string directoryName = Path.GetDirectoryName(typeof(Database).Assembly.Location);
						string text4 = Path.Combine(Path.GetDirectoryName(directoryName), "Logging");
						base.CreateDirectory(base.FilePath);
						base.CreateDirectory(base.LogPath);
						base.CreateDirectory(text4);
						base.DeleteFileOrDirectory(text);
						base.DeleteFileOrDirectory(text3);
						int num = 1;
						for (;;)
						{
							string text5 = text2 + num.ToString();
							if (!File.Exists(text5) && !Directory.Exists(text5))
							{
								break;
							}
							base.DeleteFileOrDirectory(text5);
							num++;
						}
						this.CreateDatabase(sqlConnection, text, text3, text2, directoryName, text4);
					}
					using (SqlConnection sqlConnection2 = new SqlConnection(this, "SqlDatabase.TryCreate"))
					{
						this.BeforeMountDatabase(sqlConnection2);
					}
					this.isOpen = true;
				}
			}
			catch (SqlException ex)
			{
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_SQLExceptionDetected, new object[]
				{
					ex.Message,
					ex.StackTrace,
					"SqlDatabase.TryCreate"
				});
				throw new CouldNotCreateDatabaseException(ex.Message, ex);
			}
			return true;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00032140 File Offset: 0x00030340
		public override void Close()
		{
			if (this.isOpen)
			{
				this.isOpen = false;
			}
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00032154 File Offset: 0x00030354
		public override void Delete(bool deleteFiles)
		{
			this.Close();
			using (LockManager.Lock(SqlDatabase.configurationLockObject))
			{
				using (SqlConnection sqlConnection = new SqlConnection(null, "DeleteDatabase"))
				{
					if (this.DatabaseExists(sqlConnection))
					{
						sqlConnection.Commit();
						using (SqlCommand sqlCommand = new SqlCommand(sqlConnection))
						{
							sqlCommand.StartNewStatement(Connection.OperationType.Other);
							sqlCommand.Append("ALTER DATABASE [");
							sqlCommand.Append(base.DisplayName);
							sqlCommand.Append("] SET OFFLINE WITH ROLLBACK IMMEDIATE");
							sqlCommand.StartNewStatement(Connection.OperationType.Other);
							sqlCommand.Append("ALTER DATABASE [");
							sqlCommand.Append(base.DisplayName);
							sqlCommand.Append("] SET ONLINE");
							sqlCommand.StartNewStatement(Connection.OperationType.Other);
							sqlCommand.Append("DROP DATABASE [");
							sqlCommand.Append(base.DisplayName);
							sqlCommand.Append("]");
							sqlCommand.ExecuteNonQuery(Connection.TransactionOption.NoTransaction);
						}
					}
				}
				SqlConnection.ClearAllPools();
				if (deleteFiles)
				{
					string path = base.FilePath + "\\" + base.DisplayName + ".mdf";
					string path2 = base.FilePath + "\\" + base.DisplayName + ".ndf1";
					string path3 = base.LogPath + "\\" + base.DisplayName + ".ldf";
					File.Delete(path);
					File.Delete(path2);
					File.Delete(path3);
				}
			}
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00032300 File Offset: 0x00030500
		public override void CreatePhysicalSchemaObjects(IConnectionProvider connectionProvider)
		{
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00032302 File Offset: 0x00030502
		public override void PopulateTableMetadataFromDatabase()
		{
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00032304 File Offset: 0x00030504
		public override bool CheckTableExists(string tableName)
		{
			throw new StoreException((LID)45052U, ErrorCodeValue.NotSupported, "CheckTableExists is not supported for SQL database.");
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0003231F File Offset: 0x0003051F
		public override void GetDatabaseSize(IConnectionProvider connectionProvider, out uint totalPages, out uint availablePages, out uint pageSize)
		{
			throw new StoreException((LID)65032U, ErrorCodeValue.NotSupported, "GetDatabaseSize is not supported for SQL database.");
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0003233A File Offset: 0x0003053A
		public override void GetSerializedDatabaseInformation(IConnectionProvider connectionProvider, out byte[] databaseInfo)
		{
			throw new StoreException((LID)40528U, ErrorCodeValue.NotSupported, "GetDatabaseInformation is not supported for SQL database.");
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00032355 File Offset: 0x00030555
		public override void GetLastBackupInformation(IConnectionProvider connectionProvider, out DateTime fullBackupTime, out DateTime incrementalBackupTime, out DateTime differentialBackupTime, out DateTime copyBackupTime, out int snapFull, out int snapIncremental, out int snapDifferential, out int snapCopy)
		{
			throw new StoreException((LID)56360U, ErrorCodeValue.NotSupported, "GetDatabaseBackupInformation is not supported for SQL database.");
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00032370 File Offset: 0x00030570
		public override void SnapshotPrepare(uint flags)
		{
			throw new StoreException((LID)62504U, ErrorCodeValue.NotSupported, "SnapshotPrepare is not supported for SQL database.");
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0003238B File Offset: 0x0003058B
		public override void SnapshotFreeze(uint flags)
		{
			throw new StoreException((LID)37928U, ErrorCodeValue.NotSupported, "SnapshotFreeze is not supported for SQL database.");
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000323A6 File Offset: 0x000305A6
		public override void SnapshotThaw(uint flags)
		{
			throw new StoreException((LID)54312U, ErrorCodeValue.NotSupported, "SnapshotThaw is not supported for SQL database.");
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000323C1 File Offset: 0x000305C1
		public override void SnapshotTruncateLogInstance(uint flags)
		{
			throw new StoreException((LID)42024U, ErrorCodeValue.NotSupported, "SnapshotTruncateLogInstance is not supported for SQL database.");
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000323DC File Offset: 0x000305DC
		public override void SnapshotStop(uint flags)
		{
			throw new StoreException((LID)58408U, ErrorCodeValue.NotSupported, "SnapshotStop is not supported for SQL database.");
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x000323F7 File Offset: 0x000305F7
		public override void StartLogReplay(Func<bool, bool> passiveDatabaseAttachDetachHandler)
		{
			throw new StoreException((LID)44488U, ErrorCodeValue.NotSupported, "StartLogReplay is not supported for SQL database.");
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00032412 File Offset: 0x00030612
		public override void FinishLogReplay()
		{
			throw new StoreException((LID)60872U, ErrorCodeValue.NotSupported, "FinishLogReplay is not supported for SQL database.");
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0003242D File Offset: 0x0003062D
		public override void CancelLogReplay()
		{
			throw new StoreException((LID)36296U, ErrorCodeValue.NotSupported, "CancelLogReplay is not supported for SQL database.");
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00032448 File Offset: 0x00030648
		public override void ForceNewLog(IConnectionProvider connectionProvider)
		{
			throw new StoreException((LID)42624U, ErrorCodeValue.NotSupported, "ForceNewLog is not supported for SQL database.");
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00032463 File Offset: 0x00030663
		public override IEnumerable<string> GetTableNames(IConnectionProvider connectionProvider)
		{
			throw new StoreException((LID)56220U, ErrorCodeValue.NotSupported, "GetTableNames is not supported for SQL database.");
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00032480 File Offset: 0x00030680
		private static void ExecuteAtStartCommand(Connection masterConnection, string statement)
		{
			using (SqlCommand sqlCommand = new SqlCommand(masterConnection))
			{
				sqlCommand.StartNewStatement(Connection.OperationType.Other);
				sqlCommand.Append(statement);
				sqlCommand.ExecuteScalar(Connection.TransactionOption.NoTransaction);
			}
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x000324C8 File Offset: 0x000306C8
		private void DoFirstMountProcessing()
		{
			if (!SqlDatabase.didFirstMount)
			{
				using (LockManager.Lock(SqlDatabase.configurationLockObject))
				{
					if (!SqlDatabase.didFirstMount)
					{
						using (SqlConnection sqlConnection = new SqlConnection(null, "SqlDatabase.Configure"))
						{
							SqlDatabase.ExecuteAtStartCommand(sqlConnection, "sp_configure 'remote login timeout', '60000'");
							SqlDatabase.ExecuteAtStartCommand(sqlConnection, "sp_configure 'remote query timeout', '60000'");
							SqlDatabase.ExecuteAtStartCommand(sqlConnection, "sp_configure 'show advanced option', '1'");
							SqlDatabase.ExecuteAtStartCommand(sqlConnection, "reconfigure with override");
							SqlDatabase.ExecuteAtStartCommand(sqlConnection, "sp_configure 'max degree of parallelism', '1'");
							SqlDatabase.ExecuteAtStartCommand(sqlConnection, "sys.sp_configure 'recovery interval (min)', '10000'");
							SqlDatabase.ExecuteAtStartCommand(sqlConnection, "sp_configure 'query wait', '60000'");
							SqlDatabase.ExecuteAtStartCommand(sqlConnection, "sp_configure 'max server memory', " + ((long)base.GetMaxCachePages() * (long)this.PageSize / 1048576L).ToString());
							SqlDatabase.ExecuteAtStartCommand(sqlConnection, "sp_configure 'min server memory', " + ((long)base.GetMinCachePages() * (long)this.PageSize / 1048576L).ToString());
							SqlDatabase.ExecuteAtStartCommand(sqlConnection, "reconfigure with override");
						}
						SqlDatabase.didFirstMount = true;
					}
				}
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000325F4 File Offset: 0x000307F4
		private void BeforeMountDatabase(SqlConnection connection)
		{
			using (SqlCommand sqlCommand = new SqlCommand(connection, "exec [Exchange].[sp_start_trace]", Connection.OperationType.Other))
			{
				sqlCommand.ExecuteNonQuery(Connection.TransactionOption.NoTransaction);
			}
			if ((base.Flags & DatabaseFlags.CircularLoggingEnabled) != DatabaseFlags.None)
			{
				int num;
				using (SqlCommand sqlCommand2 = new SqlCommand(connection, Connection.OperationType.Query))
				{
					sqlCommand2.Append("SELECT COUNT(*) FROM msdb..backupset WHERE database_name = ");
					sqlCommand2.AppendParameter(base.DisplayName);
					num = (int)sqlCommand2.ExecuteScalar(Connection.TransactionOption.NoTransaction);
				}
				if (num == 0)
				{
					using (SqlCommand sqlCommand3 = new SqlCommand(connection, Connection.OperationType.Other))
					{
						sqlCommand3.Append("BACKUP DATABASE [");
						sqlCommand3.Append(base.DisplayName);
						sqlCommand3.Append("] TO DISK = 'nul'");
						sqlCommand3.ExecuteNonQuery(Connection.TransactionOption.NoTransaction);
					}
				}
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000326D0 File Offset: 0x000308D0
		private bool DatabaseExists(Connection masterConnection)
		{
			object obj;
			using (SqlCommand sqlCommand = new SqlCommand(masterConnection, "select count(*) from dbo.sysdatabases where name = '" + base.DisplayName + "'", Connection.OperationType.Query))
			{
				obj = sqlCommand.ExecuteScalar(Connection.TransactionOption.NoTransaction);
			}
			return (int)obj != 0;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0003272C File Offset: 0x0003092C
		private void CreateDatabase(SqlConnection masterConnection, string primaryFileName, string logName, string dataName, string binaryPath, string diagnosticsPath)
		{
			masterConnection.Commit();
			for (int i = 0; i < ParameterizedSQL.CreateDatabase.Length; i++)
			{
				string text = string.Format(ParameterizedSQL.CreateDatabase[i], new object[]
				{
					base.DisplayName,
					primaryFileName,
					logName,
					dataName,
					binaryPath,
					diagnosticsPath
				});
				try
				{
					using (SqlCommand sqlCommand = new SqlCommand(masterConnection, text, Connection.OperationType.Other))
					{
						sqlCommand.ExecuteNonQuery(Connection.TransactionOption.NoTransaction);
					}
				}
				catch (SqlException ex)
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_SQLExceptionDetected, new object[]
					{
						ex.Message,
						ex.StackTrace,
						text.ToString()
					});
					ExTraceGlobals.DbInteractionSummaryTracer.TraceError<string, string>(0L, "CreateDatabase error: Message is {0} Stack trace is {1}", ex.Message, ex.StackTrace);
					throw masterConnection.ProcessSqlError(ex);
				}
			}
		}

		// Token: 0x04000360 RID: 864
		private static bool didFirstMount;

		// Token: 0x04000361 RID: 865
		private static object configurationLockObject = new object();

		// Token: 0x04000362 RID: 866
		private string connectionString;

		// Token: 0x04000363 RID: 867
		private bool isOpen;
	}
}
