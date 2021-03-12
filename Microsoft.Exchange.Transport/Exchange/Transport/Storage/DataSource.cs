using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Isam.Esent;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Unpublished;
using Microsoft.Isam.Esent.Interop.Windows8;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000C1 RID: 193
	internal class DataSource
	{
		// Token: 0x060006A4 RID: 1700 RVA: 0x0001AB00 File Offset: 0x00018D00
		public DataSource(string instanceName, string instancePath, string databaseName, int connectionLimit, string performanceCounter, string logFilePath, DatabaseAutoRecovery databaseAutoRecovery)
		{
			this.instanceName = instanceName;
			this.instancePath = instancePath;
			this.databasePath = Path.Combine(instancePath, databaseName);
			this.connectionLimitPoint = connectionLimit;
			this.databaseAutoRecovery = databaseAutoRecovery;
			if (!string.IsNullOrEmpty(logFilePath))
			{
				logFilePath = logFilePath.Trim();
			}
			if (!string.IsNullOrEmpty(logFilePath) && !logFilePath.EndsWith("\\"))
			{
				logFilePath += "\\";
			}
			this.logFilePath = logFilePath;
			if (string.IsNullOrEmpty(performanceCounter))
			{
				performanceCounter = "other";
			}
			this.perfCounters = DatabasePerfCounters.GetInstance(performanceCounter);
			this.references = 1;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0001ABFF File Offset: 0x00018DFF
		public static ExEventLog EventLogger
		{
			get
			{
				return DataSource.eventLogger;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0001AC06 File Offset: 0x00018E06
		public static uint ConfigMaxCacheSizeInPages
		{
			get
			{
				return DataSource.configMaxCacheSizeInPages;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001AC0D File Offset: 0x00018E0D
		public static uint ConfigMinCacheSizeInPages
		{
			get
			{
				return DataSource.configMinCacheSizeInPages;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0001AC14 File Offset: 0x00018E14
		public static uint CurrentMaxCacheSizeInPages
		{
			get
			{
				return DataSource.currentMaxCacheSizeInPages;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001AC1B File Offset: 0x00018E1B
		public string DatabasePath
		{
			get
			{
				return this.databasePath;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0001AC23 File Offset: 0x00018E23
		public string InstanceName
		{
			get
			{
				return this.instanceName;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0001AC2B File Offset: 0x00018E2B
		public string LogFilePath
		{
			get
			{
				return this.logFilePath;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0001AC33 File Offset: 0x00018E33
		public bool NewDatabase
		{
			get
			{
				return this.newDatabase;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001AC3B File Offset: 0x00018E3B
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x0001AC43 File Offset: 0x00018E43
		public bool CleanupRequestInProgress
		{
			get
			{
				return this.cleanupRequestInProgress;
			}
			set
			{
				this.cleanupRequestInProgress = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0001AC4C File Offset: 0x00018E4C
		public bool IsAboveLimit
		{
			get
			{
				return this.currentConnectionCount > this.connectionLimitPoint;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0001AC5C File Offset: 0x00018E5C
		public int ConnectionLimitPoint
		{
			get
			{
				return this.connectionLimitPoint;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001AC64 File Offset: 0x00018E64
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x0001AC72 File Offset: 0x00018E72
		public uint LogFileSize
		{
			get
			{
				return this.logFileSize * 1024U;
			}
			set
			{
				this.logFileSize = value / 1024U;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001AC81 File Offset: 0x00018E81
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0001AC8F File Offset: 0x00018E8F
		public uint LogBuffers
		{
			get
			{
				return this.logBuffers * 512U;
			}
			set
			{
				this.logBuffers = value / 512U;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0001AC9E File Offset: 0x00018E9E
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x0001ACA6 File Offset: 0x00018EA6
		public uint MaxBackgroundCleanupTasks
		{
			get
			{
				return this.maxBackgroundCleanupTasks;
			}
			set
			{
				this.maxBackgroundCleanupTasks = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001ACAF File Offset: 0x00018EAF
		public DatabasePerfCountersInstance PerfCounters
		{
			get
			{
				return this.perfCounters;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0001ACB7 File Offset: 0x00018EB7
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x0001ACBF File Offset: 0x00018EBF
		public uint ExtensionSize
		{
			get
			{
				return this.extensionSize;
			}
			set
			{
				this.extensionSize = value;
			}
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001ACC8 File Offset: 0x00018EC8
		public static bool HandleIsamException(EsentErrorException ex, DataSource source)
		{
			bool result;
			lock (DataSource.exceptionHandlerLock)
			{
				ExTraceGlobals.ExpoTracer.TraceError<EsentErrorException>(0L, "Error occurred during Jet operation : {0}", ex);
				EsentOperationException ex2 = ex as EsentOperationException;
				if (ex2 != null)
				{
					result = DataSource.HandleIsamOperationException(ex2, source);
				}
				else
				{
					EsentDataException ex3 = ex as EsentDataException;
					if (ex3 != null)
					{
						result = DataSource.HandleIsamDataException(ex3, source);
					}
					else
					{
						EsentApiException ex4 = ex as EsentApiException;
						if (ex4 != null)
						{
							result = DataSource.HandleIsamApiException(ex4, source);
						}
						else
						{
							result = false;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001AD5C File Offset: 0x00018F5C
		public ulong GetAvailableFreeSpace()
		{
			if (Monitor.TryEnter(this.syncRoot))
			{
				try
				{
					if (!this.closed)
					{
						return this.GetAvailableFreeSpaceHelper();
					}
				}
				finally
				{
					Monitor.Exit(this.syncRoot);
				}
			}
			return this.lastAvailableFreeSpace;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001ADB0 File Offset: 0x00018FB0
		public DataConnection DemandNewConnection()
		{
			return this.NewConnection(true);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001ADB9 File Offset: 0x00018FB9
		public DataConnection TryNewConnection()
		{
			return this.NewConnection(false);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001ADC4 File Offset: 0x00018FC4
		public void OpenDatabase()
		{
			if (!this.closed)
			{
				throw new InvalidOperationException(Strings.DatabaseOpen);
			}
			DataSource.InitGlobal();
			this.InitInstance();
			try
			{
				if (File.Exists(this.DatabasePath))
				{
					this.newDatabase = false;
					Api.JetAttachDatabase(this.baseSession, this.DatabasePath, AttachDatabaseGrbit.None);
					Api.JetOpenDatabase(this.baseSession, this.DatabasePath, null, out this.baseDatabase, OpenDatabaseGrbit.None);
				}
				else
				{
					this.newDatabase = true;
					Api.JetCreateDatabase(this.baseSession, this.DatabasePath, null, out this.baseDatabase, CreateDatabaseGrbit.None);
				}
				this.closed = false;
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this))
				{
					throw;
				}
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001AE84 File Offset: 0x00019084
		public void CloseDatabase(bool force = false)
		{
			if (this.closed)
			{
				return;
			}
			lock (this.cleanupThreadLock)
			{
				if (!force && Interlocked.Decrement(ref this.references) != 0)
				{
					throw new InvalidOperationException(Strings.DatabaseStillInUse);
				}
				lock (this.syncRoot)
				{
					this.StopBackgroundDefrag();
					try
					{
						Api.JetCloseDatabase(this.baseSession, this.baseDatabase, CloseDatabaseGrbit.None);
						Api.JetEndSession(this.baseSession, EndSessionGrbit.None);
						Api.JetTerm(this.instance);
						this.durableCommitCallback.End();
						this.closed = true;
					}
					catch (EsentErrorException ex)
					{
						if (!DataSource.HandleIsamException(ex, this))
						{
							throw;
						}
					}
				}
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001AF74 File Offset: 0x00019174
		public void StartBackgroundDefrag(int secondsToRun, JET_CALLBACK endOnlineDefragCallback)
		{
			int num = 1;
			lock (this.syncRoot)
			{
				if (!this.closed)
				{
					try
					{
						Api.JetDefragment2(this.baseSession, this.baseDatabase, null, ref num, ref secondsToRun, endOnlineDefragCallback, DefragGrbit.BatchStart);
					}
					catch (EsentErrorException ex)
					{
						if (!DataSource.HandleIsamException(ex, this))
						{
							throw;
						}
					}
				}
			}
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001AFF4 File Offset: 0x000191F4
		public void RegisterAsyncCommitCallback(JET_COMMIT_ID commitId, Action callback)
		{
			this.lastCommitLock.EnterReadLock();
			if (commitId <= this.lastDurableCommitId)
			{
				this.lastCommitLock.ExitReadLock();
				callback();
				return;
			}
			this.pendingCommits.TryAdd(commitId, callback);
			this.lastCommitLock.ExitReadLock();
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001B048 File Offset: 0x00019248
		public string TryForceFlush()
		{
			try
			{
				lock (this.syncRoot)
				{
					Api.JetCommitTransaction(this.baseSession, CommitTransactionGrbit.WaitLastLevel0Commit);
				}
			}
			catch (EsentException ex)
			{
				return ex.ToString();
			}
			return null;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001B0AC File Offset: 0x000192AC
		public long GetCurrentVersionBuckets()
		{
			lock (this.syncRoot)
			{
				if (!this.closed)
				{
					try
					{
						long result = 0L;
						UnpublishedApi.JetGetResourceParam(this.instance, JET_resoper.CurrentUse, JET_resid.VERBUCKET, out result);
						return result;
					}
					catch (EsentErrorException ex)
					{
						if (!DataSource.HandleIsamException(ex, this))
						{
							throw;
						}
					}
				}
			}
			return 0L;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001B128 File Offset: 0x00019328
		internal static void InitGlobal()
		{
			if (DataSource.globalInitDone)
			{
				return;
			}
			lock (DataSource.globalInitLock)
			{
				if (!DataSource.globalInitDone)
				{
					DataSource.pageSize = (uint)TransportAppConfig.JetDatabaseConfig.PageSize.ToBytes();
					TransportAppConfig.JetDatabaseConfig jetDatabase = Components.TransportAppConfig.JetDatabase;
					DataSource.SetGlobalSystemParameter((JET_param)129, 1);
					string value = "reg:HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\Ese";
					try
					{
						DataSource.SetGlobalSystemParameter((JET_param)189, value);
					}
					catch (EsentErrorException)
					{
					}
					DataSource.SetGlobalSystemParameter(JET_param.DatabasePageSize, DataSource.pageSize);
					DataSource.configMinCacheSizeInPages = (uint)((long)((int)jetDatabase.MinCacheSize.ToBytes()) / (long)((ulong)DataSource.pageSize));
					DataSource.SetGlobalSystemParameter(JET_param.CacheSizeMin, DataSource.configMinCacheSizeInPages);
					DataSource.configMaxCacheSizeInPages = (uint)((long)((int)jetDatabase.MaxCacheSize.ToBytes()) / (long)((ulong)DataSource.pageSize));
					DataSource.currentMaxCacheSizeInPages = 0U;
					DataSource.SetCurrentMaxCacheSize(DataSource.configMaxCacheSizeInPages);
					DataSource.SetGlobalSystemParameter(JET_param.CheckpointDepthMax, (int)jetDatabase.CheckpointDepthMax.ToBytes());
					DataSource.globalInitDone = true;
				}
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001B240 File Offset: 0x00019440
		internal int AddRef()
		{
			return Interlocked.Increment(ref this.references);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001B250 File Offset: 0x00019450
		internal int Release()
		{
			int num = Interlocked.Decrement(ref this.references);
			if (num == 0)
			{
				this.CloseDatabase(true);
			}
			return num;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001B274 File Offset: 0x00019474
		internal void TrackTryConnectionClose()
		{
			if (this.closed)
			{
				throw new InvalidOperationException(Strings.DatabaseClosed);
			}
			Interlocked.Decrement(ref this.currentConnectionCount);
			this.perfCounters.CurrentConnections.Decrement();
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001B2B0 File Offset: 0x000194B0
		public Transaction BeginNewTransaction()
		{
			DataConnection dataConnection = this.DemandNewConnection();
			Transaction result = dataConnection.BeginTransaction();
			dataConnection.Release();
			return result;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001B2D4 File Offset: 0x000194D4
		internal void OnDataCleanup(object obj)
		{
			if (!Monitor.TryEnter(this.cleanupThreadLock))
			{
				return;
			}
			DataConnection dataConnection = null;
			try
			{
				if (!this.closed)
				{
					dataConnection = this.DemandNewConnection();
					try
					{
						Api.JetIdle(dataConnection.Session, IdleGrbit.None);
					}
					catch (EsentErrorException)
					{
					}
				}
				if (!this.closed)
				{
					try
					{
						Api.JetIdle(dataConnection.Session, IdleGrbit.None);
					}
					catch (EsentErrorException)
					{
					}
				}
			}
			finally
			{
				if (dataConnection != null)
				{
					dataConnection.Release();
				}
				Monitor.Exit(this.cleanupThreadLock);
				this.CleanupRequestInProgress = false;
			}
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001B378 File Offset: 0x00019578
		internal void HandleSchemaException(SchemaException schemaException)
		{
			if (!this.TakeErrorAction(DataSource.DatabaseErrorAction.DeemPermanent, DataSource.ProcessErrorAction.Restart, schemaException))
			{
				throw schemaException;
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001B388 File Offset: 0x00019588
		internal bool IsDatabaseDriveAccessible()
		{
			DriveInfo driveInfo = new DriveInfo(Path.GetFullPath(this.instancePath));
			return driveInfo.IsReady;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001B3AC File Offset: 0x000195AC
		private static void SetCurrentMaxCacheSize(uint maxCacheSizeInPages)
		{
			if (maxCacheSizeInPages < DataSource.configMinCacheSizeInPages || maxCacheSizeInPages > DataSource.configMaxCacheSizeInPages)
			{
				throw new ArgumentOutOfRangeException("maxCacheSizeInPages");
			}
			if (DataSource.currentMaxCacheSizeInPages != maxCacheSizeInPages)
			{
				TransportAppConfig.JetDatabaseConfig jetDatabase = Components.TransportAppConfig.JetDatabase;
				DataSource.currentMaxCacheSizeInPages = maxCacheSizeInPages;
				DataSource.SetGlobalSystemParameter(JET_param.CacheSizeMax, (int)DataSource.currentMaxCacheSizeInPages);
				uint value = DataSource.currentMaxCacheSizeInPages * jetDatabase.StartFlushThreshold / 100U;
				DataSource.SetGlobalSystemParameter(JET_param.StartFlushThreshold, value);
				value = DataSource.currentMaxCacheSizeInPages * jetDatabase.StopFlushThreshold / 100U;
				DataSource.SetGlobalSystemParameter(JET_param.StopFlushThreshold, value);
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001B42C File Offset: 0x0001962C
		private static bool HandleInitIsamException(EsentErrorException ex, JET_INSTANCE instance, string instanceName, DataSource source)
		{
			lock (DataSource.exceptionHandlerLock)
			{
				ExTraceGlobals.ExpoTracer.TraceError<EsentErrorException>(0L, "Error occurred during Jet initialization operation : {0}", ex);
				if (ex is EsentFileAccessDeniedException)
				{
					Api.JetTerm(instance);
					return false;
				}
				if (ex is EsentInstanceNameInUseException)
				{
					DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_JetInstanceNameInUse, null, new object[]
					{
						instanceName,
						ex
					});
					return source.TakeErrorAction(DataSource.DatabaseErrorAction.DeemTransient, DataSource.ProcessErrorAction.Restart, ex);
				}
			}
			return DataSource.HandleIsamException(ex, source);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001B4C8 File Offset: 0x000196C8
		private static bool HandleIsamOperationException(EsentOperationException ex, DataSource source)
		{
			DataSource.DatabaseErrorAction databaseErrorAction;
			DataSource.ProcessErrorAction processErrorAction;
			if (ex is EsentResourceException)
			{
				if (ex is EsentDiskException)
				{
					DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_JetOutOfSpaceError, null, new object[]
					{
						source.InstanceName,
						ex
					});
					string notificationReason = string.Format("{0}: An operation has encountered a fatal error. There wasn't enough disk space to complete the operation. The exception is {1}.", source.InstanceName, ex);
					EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, notificationReason, ResultSeverityLevel.Error, false);
					databaseErrorAction = DataSource.DatabaseErrorAction.DeemTransient;
					processErrorAction = DataSource.ProcessErrorAction.Stop;
				}
				else if (ex is EsentOutOfMemoryException)
				{
					DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_JetInitInstanceOutOfMemory, null, new object[]
					{
						source.InstanceName,
						ex
					});
					databaseErrorAction = DataSource.DatabaseErrorAction.SuspectTransient;
					processErrorAction = DataSource.ProcessErrorAction.Restart;
				}
				else if (ex is EsentQuotaException)
				{
					if (ex is EsentVersionStoreOutOfMemoryException)
					{
						DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_JetVersionStoreOutOfMemoryError, null, new object[]
						{
							source.InstanceName,
							ex
						});
						databaseErrorAction = DataSource.DatabaseErrorAction.DeemTransient;
						processErrorAction = DataSource.ProcessErrorAction.Rethrow;
					}
					else
					{
						DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_JetQuotaExceededError, null, new object[]
						{
							source.InstanceName,
							ex
						});
						databaseErrorAction = DataSource.DatabaseErrorAction.SuspectTransient;
						processErrorAction = DataSource.ProcessErrorAction.Restart;
					}
				}
				else
				{
					DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_JetInsufficientResourcesError, null, new object[]
					{
						source.InstanceName,
						ex
					});
					databaseErrorAction = DataSource.DatabaseErrorAction.SuspectTransient;
					processErrorAction = DataSource.ProcessErrorAction.Restart;
				}
			}
			else if (ex is EsentIOException)
			{
				if (ex is EsentDiskIOException && !source.IsDatabaseDriveAccessible())
				{
					DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_DatabaseDriveIsNotAccessible, null, new object[]
					{
						source.InstanceName,
						ex
					});
					databaseErrorAction = DataSource.DatabaseErrorAction.DeemTransient;
				}
				else
				{
					DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_JetIOError, null, new object[]
					{
						source.InstanceName,
						ex
					});
					databaseErrorAction = DataSource.DatabaseErrorAction.SuspectTransient;
				}
				processErrorAction = DataSource.ProcessErrorAction.Restart;
			}
			else
			{
				DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_JetOperationError, null, new object[]
				{
					source.InstanceName,
					ex
				});
				databaseErrorAction = DataSource.DatabaseErrorAction.SuspectTransient;
				processErrorAction = DataSource.ProcessErrorAction.Restart;
			}
			return source.TakeErrorAction(databaseErrorAction, processErrorAction, ex);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001B6D4 File Offset: 0x000198D4
		private static bool HandleIsamDataException(EsentDataException ex, DataSource source)
		{
			DataSource.DatabaseErrorAction databaseErrorAction;
			DataSource.ProcessErrorAction processErrorAction;
			if (ex is EsentFragmentationException)
			{
				DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_JetFragmentationError, null, new object[]
				{
					source.InstanceName,
					ex
				});
				databaseErrorAction = DataSource.DatabaseErrorAction.SuspectTransient;
				processErrorAction = DataSource.ProcessErrorAction.Restart;
			}
			else
			{
				if (ex is EsentInconsistentException)
				{
					ExTraceGlobals.ExpoTracer.TraceError<EsentDataException>(0L, "EsentInconsistentException caught : {0}", ex);
				}
				if (ex is EsentDatabaseDirtyShutdownException || ex is EsentMissingLogFileException || ex is EsentMissingPreviousLogFileException || ex is EsentRequiredLogFilesMissingException || ex is EsentBadLogVersionException)
				{
					DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_JetLogFileError, null, new object[]
					{
						source.InstanceName,
						ex
					});
					string notificationReason = string.Format("{0}: The database could not be opened because a log file is missing or corrupted. Manual database recovery or repair may be required. The exception is {1}.", source.InstanceName, ex);
					EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, notificationReason, ResultSeverityLevel.Error, false);
					databaseErrorAction = DataSource.DatabaseErrorAction.DeemPermanent;
					processErrorAction = DataSource.ProcessErrorAction.Restart;
				}
				else if (ex is EsentBadCheckpointSignatureException || ex is EsentCheckpointFileNotFoundException)
				{
					DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_JetCheckpointFileError, null, new object[]
					{
						source.InstanceName,
						ex
					});
					string notificationReason2 = string.Format("{0}: The database could not be opened because the checkpoint file (.chk) is missing or corrupted. The exception is {1}.", source.InstanceName, ex);
					EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, notificationReason2, ResultSeverityLevel.Error, false);
					databaseErrorAction = DataSource.DatabaseErrorAction.DeemPermanent;
					processErrorAction = DataSource.ProcessErrorAction.Restart;
				}
				else if (ex is EsentAttachedDatabaseMismatchException)
				{
					DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_JetMismatchError, null, new object[]
					{
						source.InstanceName,
						ex
					});
					string notificationReason3 = string.Format("{0}: The database could not be opened because the database file does not match the log files. The exception is {1}.", source.InstanceName, ex);
					EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, notificationReason3, ResultSeverityLevel.Error, false);
					databaseErrorAction = DataSource.DatabaseErrorAction.DeemTransient;
					processErrorAction = DataSource.ProcessErrorAction.Stop;
				}
				else if (ex is EsentDatabaseLogSetMismatchException)
				{
					DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_JetDatabaseLogSetMismatch, null, new object[]
					{
						source.InstanceName,
						ex
					});
					databaseErrorAction = DataSource.DatabaseErrorAction.DeemTransient;
					processErrorAction = DataSource.ProcessErrorAction.Stop;
				}
				else
				{
					DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_JetCorruptionError, null, new object[]
					{
						source.InstanceName,
						ex
					});
					string notificationReason4 = string.Format("{0}: An operation has encountered a fatal error. The database may be corrupted. Exception details: {1}", source.InstanceName, ex);
					EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, notificationReason4, ResultSeverityLevel.Error, false);
					databaseErrorAction = DataSource.DatabaseErrorAction.SuspectTransient;
					processErrorAction = DataSource.ProcessErrorAction.Restart;
				}
			}
			return source.TakeErrorAction(databaseErrorAction, processErrorAction, ex);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001B92C File Offset: 0x00019B2C
		private static bool HandleIsamApiException(EsentApiException ex, DataSource source)
		{
			DataSource.DatabaseErrorAction databaseErrorAction;
			DataSource.ProcessErrorAction processErrorAction;
			if (ex is EsentInvalidPathException)
			{
				DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_JetPathError, null, new object[]
				{
					source.InstanceName,
					ex
				});
				string notificationReason = string.Format("{0}: The database could not be opened because the log file path that was supplied is invalid. The Microsoft Exchange Transport service is shutting down. The exception is {1}.", source.InstanceName, ex);
				EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, notificationReason, ResultSeverityLevel.Error, false);
				databaseErrorAction = DataSource.DatabaseErrorAction.DeemTransient;
				processErrorAction = DataSource.ProcessErrorAction.Stop;
			}
			else if (ex is EsentDatabaseNotFoundException)
			{
				DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_JetDatabaseNotFound, null, new object[]
				{
					source.InstanceName,
					ex
				});
				databaseErrorAction = DataSource.DatabaseErrorAction.DeemTransient;
				processErrorAction = DataSource.ProcessErrorAction.Stop;
			}
			else if (ex is EsentObjectNotFoundException)
			{
				DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_JetTableNotFound, null, new object[]
				{
					source.InstanceName,
					ex
				});
				databaseErrorAction = DataSource.DatabaseErrorAction.SuspectTransient;
				processErrorAction = DataSource.ProcessErrorAction.Rethrow;
			}
			else if (ex is EsentFileNotFoundException)
			{
				DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_JetFileNotFound, null, new object[]
				{
					source.InstanceName,
					ex
				});
				databaseErrorAction = DataSource.DatabaseErrorAction.DeemTransient;
				processErrorAction = DataSource.ProcessErrorAction.Stop;
			}
			else if (ex is EsentDatabaseFileReadOnlyException)
			{
				DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_JetFileReadOnly, null, new object[]
				{
					source.InstanceName,
					ex
				});
				databaseErrorAction = DataSource.DatabaseErrorAction.DeemTransient;
				processErrorAction = DataSource.ProcessErrorAction.Stop;
			}
			else if (ex is EsentColumnTooBigException)
			{
				DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_ColumnTooBigException, null, new object[]
				{
					source.InstanceName,
					ex
				});
				databaseErrorAction = DataSource.DatabaseErrorAction.DeemTransient;
				processErrorAction = DataSource.ProcessErrorAction.Rethrow;
			}
			else if (ex is EsentTableLockedException)
			{
				DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_TableLockedException, null, new object[]
				{
					source.InstanceName,
					ex
				});
				databaseErrorAction = DataSource.DatabaseErrorAction.DeemTransient;
				processErrorAction = DataSource.ProcessErrorAction.Rethrow;
			}
			else
			{
				databaseErrorAction = DataSource.DatabaseErrorAction.SuspectTransient;
				processErrorAction = DataSource.ProcessErrorAction.Rethrow;
			}
			return source.TakeErrorAction(databaseErrorAction, processErrorAction, ex);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001BB00 File Offset: 0x00019D00
		private static void SetGlobalSystemParameter(JET_param parameter, int value)
		{
			Api.JetSetSystemParameter(JET_INSTANCE.Nil, JET_SESID.Nil, parameter, value, null);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001BB15 File Offset: 0x00019D15
		private static void SetGlobalSystemParameter(JET_param parameter, uint value)
		{
			Api.JetSetSystemParameter(JET_INSTANCE.Nil, JET_SESID.Nil, parameter, (int)value, null);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001BB2A File Offset: 0x00019D2A
		private static void SetGlobalSystemParameter(JET_param parameter, string value)
		{
			Api.JetSetSystemParameter(JET_INSTANCE.Nil, JET_SESID.Nil, parameter, 0, value);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001BB40 File Offset: 0x00019D40
		private bool TakeErrorAction(DataSource.DatabaseErrorAction databaseErrorAction, DataSource.ProcessErrorAction processErrorAction, Exception exception)
		{
			bool flag = false;
			DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_DatabaseErrorDetected, null, new object[]
			{
				this.instanceName,
				databaseErrorAction,
				processErrorAction,
				exception
			});
			if (this.databaseAutoRecovery != null)
			{
				switch (databaseErrorAction)
				{
				case DataSource.DatabaseErrorAction.DeemTransient:
					flag = true;
					break;
				case DataSource.DatabaseErrorAction.SuspectTransient:
					flag = this.databaseAutoRecovery.IncrementDatabaseCorruptionCount();
					break;
				case DataSource.DatabaseErrorAction.DeemPermanent:
					flag = this.databaseAutoRecovery.SetDatabaseCorruptionFlag();
					break;
				}
			}
			switch (processErrorAction)
			{
			case DataSource.ProcessErrorAction.Rethrow:
				return false;
			case DataSource.ProcessErrorAction.Restart:
				Components.StopService(Strings.JetOperationFailure, flag || databaseErrorAction != DataSource.DatabaseErrorAction.DeemPermanent, false, false);
				return true;
			case DataSource.ProcessErrorAction.Stop:
				Components.StopService(Strings.JetOperationFailure, false, false, false);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001BC18 File Offset: 0x00019E18
		private JET_err FlushCallback(JET_INSTANCE firingInstance, JET_COMMIT_ID commitId, DurableCommitCallbackGrbit grbit)
		{
			this.lastCommitLock.EnterUpgradeableReadLock();
			if (commitId > this.lastDurableCommitId)
			{
				this.lastCommitLock.EnterWriteLock();
				this.lastDurableCommitId = commitId;
				this.lastCommitLock.ExitWriteLock();
				this.lastCommitLock.ExitUpgradeableReadLock();
				ThreadPool.QueueUserWorkItem(delegate(object param0)
				{
					this.DispatchPendingCommits();
				});
			}
			else
			{
				this.lastCommitLock.ExitUpgradeableReadLock();
			}
			Transaction.PerfCounters.TransactionDurableCallbackCount.Increment();
			return JET_err.Success;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001BCD7 File Offset: 0x00019ED7
		private void DispatchPendingCommits()
		{
			Parallel.ForEach<JET_COMMIT_ID>(from transactionId in this.pendingCommits.Keys
			where transactionId <= this.lastDurableCommitId
			select transactionId, delegate(JET_COMMIT_ID transactionId)
			{
				Action action;
				if (this.pendingCommits.TryRemove(transactionId, out action))
				{
					action();
				}
			});
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001BD08 File Offset: 0x00019F08
		private ulong GetAvailableFreeSpaceHelper()
		{
			try
			{
				int num;
				Api.JetGetDatabaseInfo(this.baseSession, this.baseDatabase, out num, JET_DbInfo.PageSize);
				ulong num2 = (ulong)((long)num);
				int num3;
				Api.JetGetDatabaseInfo(this.baseSession, this.baseDatabase, out num3, JET_DbInfo.SpaceAvailable);
				ulong num4 = (ulong)((long)num3);
				this.lastAvailableFreeSpace = num2 * num4;
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this))
				{
					throw;
				}
			}
			return this.lastAvailableFreeSpace;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001BD78 File Offset: 0x00019F78
		private void StopBackgroundDefrag()
		{
			int num = 0;
			int num2 = 0;
			try
			{
				lock (this.syncRoot)
				{
					Api.JetDefragment(this.baseSession, this.baseDatabase, null, ref num, ref num2, DefragGrbit.BatchStop);
				}
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this))
				{
					throw;
				}
			}
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001BDEC File Offset: 0x00019FEC
		private void SetSystemParameter(JET_param parameter, string value)
		{
			Api.JetSetSystemParameter(this.instance, JET_SESID.Nil, parameter, 0, value);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001BE02 File Offset: 0x0001A002
		private void SetSystemParameter(JET_param parameter, int value)
		{
			Api.JetSetSystemParameter(this.instance, JET_SESID.Nil, parameter, value, null);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001BE18 File Offset: 0x0001A018
		private void SetSystemParameter(JET_param parameter, uint value)
		{
			Api.JetSetSystemParameter(this.instance, JET_SESID.Nil, parameter, (int)value, null);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001BE30 File Offset: 0x0001A030
		private void InitInstance()
		{
			try
			{
				Api.JetCreateInstance(out this.instance, this.instanceName);
				this.SetSystemParameter(JET_param.MaxSessions, 1000);
				this.SetSystemParameter(JET_param.MaxOpenTables, 3360);
				this.SetSystemParameter(JET_param.MaxCursors, 5000);
				this.SetSystemParameter(JET_param.VersionStoreTaskQueueMax, this.maxBackgroundCleanupTasks);
				this.SetSystemParameter(JET_param.MaxVerPages, 8000);
				this.SetSystemParameter(JET_param.PreferredVerPages, 6000);
				this.SetSystemParameter(JET_param.CreatePathIfNotExist, 1);
				this.SetSystemParameter(JET_param.LogFileSize, this.logFileSize);
				this.SetSystemParameter((JET_param)154, 5242880U / this.logFileSize);
				this.SetSystemParameter(JET_param.CircularLog, 1);
				this.SetSystemParameter(JET_param.CleanupMismatchedLogFiles, 1);
				this.SetSystemParameter(JET_param.LogBuffers, this.logBuffers);
				this.SetSystemParameter(JET_param.SystemPath, this.instancePath);
				this.SetSystemParameter(JET_param.LogFilePath, this.logFilePath);
				this.SetSystemParameter(JET_param.TempPath, this.logFilePath);
				this.SetSystemParameter(JET_param.BaseName, "trn");
				this.SetSystemParameter(JET_param.Recovery, "on");
				this.SetSystemParameter((JET_param)184, 0);
				this.SetSystemParameter(JET_param.DbExtensionSize, this.extensionSize / DataSource.pageSize);
				this.durableCommitCallback = new DurableCommitCallback(this.instance, new JET_PFNDURABLECOMMITCALLBACK(this.FlushCallback));
				Api.JetInit(ref this.instance);
				Api.JetBeginSession(this.instance, out this.baseSession, null, null);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleInitIsamException(ex, this.instance, this.instanceName, this))
				{
					throw;
				}
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001BFBC File Offset: 0x0001A1BC
		private DataConnection NewConnection(bool demand)
		{
			if (this.closed)
			{
				throw new InvalidOperationException(Strings.DatabaseClosed);
			}
			if (!demand)
			{
				int num = Interlocked.Increment(ref this.currentConnectionCount);
				if (num > this.connectionLimitPoint)
				{
					Interlocked.Decrement(ref this.currentConnectionCount);
					this.perfCounters.RejectedConnections.Increment();
					return null;
				}
			}
			this.perfCounters.CurrentConnections.Increment();
			return DataConnection.Open(this.instance, this);
		}

		// Token: 0x04000327 RID: 807
		private const string TransportEseRegistryPath = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\Ese";

		// Token: 0x04000328 RID: 808
		public const long MaxVersionBuckets = 8000L;

		// Token: 0x04000329 RID: 809
		private const long PreferredVersionBuckets = 6000L;

		// Token: 0x0400032A RID: 810
		private const int KB = 1024;

		// Token: 0x0400032B RID: 811
		private const int MB = 1048576;

		// Token: 0x0400032C RID: 812
		private const int GB = 1073741824;

		// Token: 0x0400032D RID: 813
		private static object globalInitLock = new object();

		// Token: 0x0400032E RID: 814
		private static bool globalInitDone;

		// Token: 0x0400032F RID: 815
		private static uint configMaxCacheSizeInPages;

		// Token: 0x04000330 RID: 816
		private static uint configMinCacheSizeInPages;

		// Token: 0x04000331 RID: 817
		private static uint currentMaxCacheSizeInPages;

		// Token: 0x04000332 RID: 818
		private static object exceptionHandlerLock = new object();

		// Token: 0x04000333 RID: 819
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.ExpoTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x04000334 RID: 820
		private static uint pageSize = 32768U;

		// Token: 0x04000335 RID: 821
		private readonly string instanceName;

		// Token: 0x04000336 RID: 822
		private readonly string instancePath;

		// Token: 0x04000337 RID: 823
		private readonly string databasePath;

		// Token: 0x04000338 RID: 824
		private readonly string logFilePath;

		// Token: 0x04000339 RID: 825
		private readonly int connectionLimitPoint;

		// Token: 0x0400033A RID: 826
		private readonly ConcurrentDictionary<JET_COMMIT_ID, Action> pendingCommits = new ConcurrentDictionary<JET_COMMIT_ID, Action>();

		// Token: 0x0400033B RID: 827
		private readonly ReaderWriterLockSlim lastCommitLock = new ReaderWriterLockSlim();

		// Token: 0x0400033C RID: 828
		private volatile JET_COMMIT_ID lastDurableCommitId;

		// Token: 0x0400033D RID: 829
		private DurableCommitCallback durableCommitCallback;

		// Token: 0x0400033E RID: 830
		private object cleanupThreadLock = new object();

		// Token: 0x0400033F RID: 831
		private ulong lastAvailableFreeSpace;

		// Token: 0x04000340 RID: 832
		private int currentConnectionCount;

		// Token: 0x04000341 RID: 833
		private uint logFileSize = 5120U;

		// Token: 0x04000342 RID: 834
		private uint logBuffers = 1024U;

		// Token: 0x04000343 RID: 835
		private uint maxBackgroundCleanupTasks = 32U;

		// Token: 0x04000344 RID: 836
		private JET_INSTANCE instance;

		// Token: 0x04000345 RID: 837
		private JET_SESID baseSession;

		// Token: 0x04000346 RID: 838
		private JET_DBID baseDatabase;

		// Token: 0x04000347 RID: 839
		private bool newDatabase;

		// Token: 0x04000348 RID: 840
		private DatabasePerfCountersInstance perfCounters;

		// Token: 0x04000349 RID: 841
		private volatile bool closed = true;

		// Token: 0x0400034A RID: 842
		private int references;

		// Token: 0x0400034B RID: 843
		private object syncRoot = new object();

		// Token: 0x0400034C RID: 844
		private DatabaseAutoRecovery databaseAutoRecovery;

		// Token: 0x0400034D RID: 845
		private bool cleanupRequestInProgress;

		// Token: 0x0400034E RID: 846
		private uint extensionSize = 10485760U;

		// Token: 0x020000C2 RID: 194
		private enum ProcessErrorAction
		{
			// Token: 0x04000350 RID: 848
			Rethrow,
			// Token: 0x04000351 RID: 849
			Restart,
			// Token: 0x04000352 RID: 850
			Stop
		}

		// Token: 0x020000C3 RID: 195
		private enum DatabaseErrorAction
		{
			// Token: 0x04000354 RID: 852
			DeemTransient,
			// Token: 0x04000355 RID: 853
			SuspectTransient,
			// Token: 0x04000356 RID: 854
			DeemPermanent
		}
	}
}
