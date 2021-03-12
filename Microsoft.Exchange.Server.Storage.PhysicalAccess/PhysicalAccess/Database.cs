using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200001E RID: 30
	public abstract class Database
	{
		// Token: 0x060000FA RID: 250 RVA: 0x0000D464 File Offset: 0x0000B664
		protected Database(string displayName, string logPath, string filePath, string fileName, DatabaseFlags databaseFlags, DatabaseOptions databaseOptions)
		{
			this.displayName = displayName;
			this.logPath = logPath;
			this.filePath = filePath;
			this.fileName = fileName;
			this.databaseOptions = databaseOptions;
			this.databaseFlags = databaseFlags;
			if (string.IsNullOrEmpty(this.fileName))
			{
				this.fileName = this.displayName + ".EDB";
			}
			this.perfInstance = PhysicalAccessPerformanceCounters.GetInstance(this.DisplayName);
			this.tables = new LockFreeDictionary<string, Table>();
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000D4E3 File Offset: 0x0000B6E3
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000D4EB File Offset: 0x0000B6EB
		public DatabaseFlags Flags
		{
			get
			{
				return this.databaseFlags;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000FD RID: 253
		public abstract int PageSize { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000D4F3 File Offset: 0x0000B6F3
		public DatabaseOptions DatabaseOptions
		{
			get
			{
				return this.databaseOptions;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000D4FB File Offset: 0x0000B6FB
		internal PhysicalAccessPerformanceCountersInstance PerfInstance
		{
			get
			{
				return this.perfInstance;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000D503 File Offset: 0x0000B703
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000D50B File Offset: 0x0000B70B
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000D513 File Offset: 0x0000B713
		public string LogPath
		{
			get
			{
				return this.logPath;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000103 RID: 259
		public abstract DatabaseType DatabaseType { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000104 RID: 260
		public abstract ILogReplayStatus LogReplayStatus { get; }

		// Token: 0x06000105 RID: 261 RVA: 0x0000D51B File Offset: 0x0000B71B
		public virtual void Configure()
		{
		}

		// Token: 0x06000106 RID: 262
		public abstract bool TryOpen(bool lossyMount);

		// Token: 0x06000107 RID: 263
		public abstract bool TryCreate(bool force);

		// Token: 0x06000108 RID: 264
		public abstract void Close();

		// Token: 0x06000109 RID: 265
		public abstract void Delete(bool deleteFiles);

		// Token: 0x0600010A RID: 266 RVA: 0x0000D51D File Offset: 0x0000B71D
		public virtual void ResetDatabaseEngine()
		{
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000D51F File Offset: 0x0000B71F
		public virtual DatabaseHeaderInfo GetDatabaseHeaderInfo(IConnectionProvider connectionProvider)
		{
			return new DatabaseHeaderInfo(null, DateTime.MinValue, 0, 0);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000D52E File Offset: 0x0000B72E
		public virtual bool IsDatabaseEngineBusy(out string highResourceUsageType, out long currentResourceUsage, out long maxResourceUsage)
		{
			highResourceUsageType = null;
			currentResourceUsage = 0L;
			maxResourceUsage = 0L;
			return false;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000D53C File Offset: 0x0000B73C
		public virtual void VersionStoreCleanup(IConnectionProvider connectionProvider)
		{
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000D53E File Offset: 0x0000B73E
		public virtual void ExtendDatabase(IConnectionProvider connectionProvider)
		{
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000D540 File Offset: 0x0000B740
		public virtual void ShrinkDatabase(IConnectionProvider connectionProvider)
		{
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000D542 File Offset: 0x0000B742
		public virtual void StartBackgroundChecksumming(IConnectionProvider connectionProvider)
		{
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000D544 File Offset: 0x0000B744
		internal virtual void PublishHaFailure(FailureTag failureTag)
		{
		}

		// Token: 0x06000112 RID: 274
		public abstract void CreatePhysicalSchemaObjects(IConnectionProvider connectionProvider);

		// Token: 0x06000113 RID: 275 RVA: 0x0000D546 File Offset: 0x0000B746
		public void AddTableMetadata(Table table)
		{
			this.tables.Add(table.Name, table);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000D55A File Offset: 0x0000B75A
		public void RemoveTableMetadata(string tableName)
		{
			this.tables.Remove(tableName);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000D56C File Offset: 0x0000B76C
		public Table GetTableMetadata(string tableName)
		{
			Table result;
			if (this.tables.TryGetValue(tableName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000D58C File Offset: 0x0000B78C
		public IEnumerable<Table> GetAllTableMetadata()
		{
			return this.tables.Values;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000D599 File Offset: 0x0000B799
		public virtual void PopulateTableMetadataFromDatabase()
		{
		}

		// Token: 0x06000118 RID: 280
		public abstract void GetDatabaseSize(IConnectionProvider connectionProvider, out uint totalPages, out uint availablePages, out uint pageSize);

		// Token: 0x06000119 RID: 281
		public abstract void ForceNewLog(IConnectionProvider connectionProvider);

		// Token: 0x0600011A RID: 282
		public abstract IEnumerable<string> GetTableNames(IConnectionProvider connectionProvider);

		// Token: 0x0600011B RID: 283
		public abstract void StartLogReplay(Func<bool, bool> passiveDatabaseAttachDetachHandler);

		// Token: 0x0600011C RID: 284
		public abstract void FinishLogReplay();

		// Token: 0x0600011D RID: 285
		public abstract void CancelLogReplay();

		// Token: 0x0600011E RID: 286
		public abstract bool CheckTableExists(string tableName);

		// Token: 0x0600011F RID: 287
		public abstract void GetSerializedDatabaseInformation(IConnectionProvider connectionProvider, out byte[] databaseInfo);

		// Token: 0x06000120 RID: 288
		public abstract void GetLastBackupInformation(IConnectionProvider connectionProvider, out DateTime fullBackupTime, out DateTime incrementalBackupTime, out DateTime differentialBackupTime, out DateTime copyBackupTime, out int snapFull, out int snapIncremental, out int snapDifferential, out int snapCopy);

		// Token: 0x06000121 RID: 289
		public abstract void SnapshotPrepare(uint flags);

		// Token: 0x06000122 RID: 290
		public abstract void SnapshotFreeze(uint flags);

		// Token: 0x06000123 RID: 291
		public abstract void SnapshotThaw(uint flags);

		// Token: 0x06000124 RID: 292
		public abstract void SnapshotTruncateLogInstance(uint flags);

		// Token: 0x06000125 RID: 293
		public abstract void SnapshotStop(uint flags);

		// Token: 0x06000126 RID: 294 RVA: 0x0000D59C File Offset: 0x0000B79C
		internal int GetMaxCachePages()
		{
			int num;
			if (this.databaseOptions != null && this.databaseOptions.MaxCachePages != null && this.databaseOptions.MaxCachePages > 0)
			{
				num = this.databaseOptions.MaxCachePages.Value;
			}
			else
			{
				num = this.GetCacheSize();
				if (this.databaseOptions != null && this.databaseOptions.MinCachePages != null && this.databaseOptions.MinCachePages > 0 && num < this.databaseOptions.MinCachePages)
				{
					num = this.databaseOptions.MinCachePages.Value;
				}
			}
			return num;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000D674 File Offset: 0x0000B874
		internal int GetMinCachePages()
		{
			int num;
			if (this.databaseOptions != null && this.databaseOptions.MinCachePages != null && this.databaseOptions.MinCachePages > 0)
			{
				num = this.databaseOptions.MinCachePages.Value;
			}
			else
			{
				num = this.GetCacheSize();
				if (this.databaseOptions != null && this.databaseOptions.MaxCachePages != null && this.databaseOptions.MaxCachePages > 0 && num > this.databaseOptions.MaxCachePages)
				{
					num = this.databaseOptions.MaxCachePages.Value;
				}
			}
			return num;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000D74A File Offset: 0x0000B94A
		protected void OpenPerfCounterInstance()
		{
			if (!PhysicalAccessPerformanceCounters.InstanceExists(this.DisplayName))
			{
				PhysicalAccessPerformanceCounters.GetInstance(this.DisplayName);
				return;
			}
			PhysicalAccessPerformanceCounters.ResetInstance(this.DisplayName);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000D771 File Offset: 0x0000B971
		protected void ClosePerfCounterInstance()
		{
			if (PhysicalAccessPerformanceCounters.InstanceExists(this.DisplayName))
			{
				PhysicalAccessPerformanceCounters.CloseInstance(this.DisplayName);
				PhysicalAccessPerformanceCounters.RemoveInstance(this.DisplayName);
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000D798 File Offset: 0x0000B998
		protected void DeleteFileOrDirectory(string pathName)
		{
			try
			{
				if (File.Exists(pathName))
				{
					File.Delete(pathName);
					Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_FileDeleted, new object[]
					{
						this.DisplayName,
						pathName
					});
				}
				else if (Directory.Exists(pathName))
				{
					Directory.Delete(pathName, true);
					Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_DirectoryDeleted, new object[]
					{
						this.DisplayName,
						pathName
					});
				}
			}
			catch (IOException ex)
			{
				Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_IOExceptionDetected, new object[]
				{
					ex.Message,
					ex.StackTrace,
					this.DisplayName
				});
				throw new CouldNotCreateDatabaseException(ex.Message, ex);
			}
			catch (UnauthorizedAccessException ex2)
			{
				Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_IOExceptionDetected, new object[]
				{
					ex2.Message,
					ex2.StackTrace,
					this.DisplayName
				});
				throw new CouldNotCreateDatabaseException(ex2.Message, ex2);
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000D8A4 File Offset: 0x0000BAA4
		protected void CreateDirectory(string directoryPathName)
		{
			if (Database.IsFile(directoryPathName))
			{
				this.DeleteFileOrDirectory(directoryPathName);
			}
			try
			{
				Directory.CreateDirectory(directoryPathName);
			}
			catch (IOException ex)
			{
				Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_IOExceptionDetected, new object[]
				{
					ex.Message,
					ex.StackTrace,
					this.DisplayName
				});
				throw new CouldNotCreateDatabaseException(ex.Message, ex);
			}
			catch (UnauthorizedAccessException ex2)
			{
				Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_IOExceptionDetected, new object[]
				{
					ex2.Message,
					ex2.StackTrace,
					this.DisplayName
				});
				throw new CouldNotCreateDatabaseException(ex2.Message, ex2);
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000D95C File Offset: 0x0000BB5C
		private static long GetTotalMemory()
		{
			if (Database.TotalMemoryForTest != null)
			{
				return Database.TotalMemoryForTest.Value;
			}
			NativeMemoryMethods.MemoryStatusEx memoryStatusEx = default(NativeMemoryMethods.MemoryStatusEx);
			memoryStatusEx.Init();
			ulong result = 0UL;
			if (NativeMemoryMethods.GlobalMemoryStatusEx(ref memoryStatusEx))
			{
				result = memoryStatusEx.TotalPhys;
			}
			else
			{
				Marshal.GetLastWin32Error();
				Globals.AssertRetail(false, "Call to GlobalMemoryStatusEx failed");
			}
			return (long)result;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000D9B7 File Offset: 0x0000BBB7
		private static bool IsFile(string path)
		{
			return File.Exists(path);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000D9C0 File Offset: 0x0000BBC0
		private int GetCacheSize()
		{
			long totalMemory = Database.GetTotalMemory();
			int num = (int)(totalMemory / (long)this.PageSize);
			int databaseCacheSizePercentage = DefaultSettings.Get.DatabaseCacheSizePercentage;
			int num2 = 0;
			int num3 = num * databaseCacheSizePercentage / 100;
			int num4 = (int)((long)num2 * 1024L * 1024L * 1024L / (long)this.PageSize);
			num3 -= num4;
			return Math.Max(3200, num3);
		}

		// Token: 0x0400009F RID: 159
		internal const int MinimumExpectedDatabases = 4;

		// Token: 0x040000A0 RID: 160
		internal const int MinimumPages = 3200;

		// Token: 0x040000A1 RID: 161
		[ThreadStatic]
		internal static long? TotalMemoryForTest;

		// Token: 0x040000A2 RID: 162
		private readonly PhysicalAccessPerformanceCountersInstance perfInstance;

		// Token: 0x040000A3 RID: 163
		private readonly DatabaseFlags databaseFlags;

		// Token: 0x040000A4 RID: 164
		private readonly string displayName;

		// Token: 0x040000A5 RID: 165
		private readonly string logPath;

		// Token: 0x040000A6 RID: 166
		private readonly string filePath;

		// Token: 0x040000A7 RID: 167
		private readonly string fileName;

		// Token: 0x040000A8 RID: 168
		private LockFreeDictionary<string, Table> tables;

		// Token: 0x040000A9 RID: 169
		private DatabaseOptions databaseOptions;

		// Token: 0x0200001F RID: 31
		protected enum SnapshotState
		{
			// Token: 0x040000AB RID: 171
			Null,
			// Token: 0x040000AC RID: 172
			Prepared,
			// Token: 0x040000AD RID: 173
			Frozen,
			// Token: 0x040000AE RID: 174
			Thawed
		}
	}
}
