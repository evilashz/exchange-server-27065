using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Fast;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Mapi;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002AB RID: 683
	internal class SeederServerContext
	{
		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x00072404 File Offset: 0x00070604
		public bool IsCatalogSeed
		{
			get
			{
				return this.SeedType == SeedType.Catalog;
			}
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x0007240F File Offset: 0x0007060F
		private static FileStream OpenFileStream(SafeFileHandle fileHandle, bool openForRead)
		{
			return LogCopy.OpenFileStream(fileHandle, openForRead);
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x00072418 File Offset: 0x00070618
		internal Guid DatabaseGuid
		{
			get
			{
				return this.m_databaseGuid;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x00072420 File Offset: 0x00070620
		internal Guid? TargetServerGuid
		{
			get
			{
				return this.m_targetServerGuid;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x00072428 File Offset: 0x00070628
		internal string TargetServerName
		{
			get
			{
				if (this.m_targetServer == null && this.m_targetServerGuid != null)
				{
					IReplayAdObjectLookup replayAdObjectLookup = Dependencies.ReplayAdObjectLookup;
					this.m_targetServer = replayAdObjectLookup.ServerLookup.FindAdObjectByGuid(this.m_targetServerGuid.Value);
				}
				if (this.m_targetServer != null)
				{
					return this.m_targetServer.Name;
				}
				return this.m_channel.PartnerNodeName;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001AB4 RID: 6836 RVA: 0x0007248B File Offset: 0x0007068B
		// (set) Token: 0x06001AB5 RID: 6837 RVA: 0x00072493 File Offset: 0x00070693
		public string DatabaseName { get; private set; }

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x0007249C File Offset: 0x0007069C
		// (set) Token: 0x06001AB7 RID: 6839 RVA: 0x000724A4 File Offset: 0x000706A4
		public SeedType SeedType { get; private set; }

		// Token: 0x06001AB8 RID: 6840 RVA: 0x000724B0 File Offset: 0x000706B0
		internal SeederServerContext(NetworkChannel channel, MonitoredDatabase database, Guid? targetServerGuid, SeedType seedType)
		{
			this.m_channel = channel;
			this.m_databaseGuid = database.DatabaseGuid;
			this.DatabaseName = database.DatabaseName;
			this.SeedType = seedType;
			this.m_targetServerGuid = targetServerGuid;
			if (database.Config.IsPassiveCopy)
			{
				this.m_fPassiveSeeding = true;
				switch (seedType)
				{
				case SeedType.Database:
					this.m_passiveSeedingSourceContext = PassiveSeedingSourceContextEnum.Database;
					break;
				case SeedType.Catalog:
					this.m_passiveSeedingSourceContext = PassiveSeedingSourceContextEnum.Catalogue;
					break;
				}
			}
			else
			{
				this.m_passiveSeedingSourceContext = PassiveSeedingSourceContextEnum.None;
			}
			if (!TestSupport.IsCatalogSeedDisabled())
			{
				string indexSystemName = FastIndexVersion.GetIndexSystemName(this.m_databaseGuid);
				this.indexSeederSource = new IndexSeeder(indexSystemName);
			}
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x0007255D File Offset: 0x0007075D
		internal void TraceDebug(string format, params object[] args)
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug((long)this.GetHashCode(), format, args);
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x00072572 File Offset: 0x00070772
		internal void TraceError(string format, params object[] args)
		{
			ExTraceGlobals.SeederServerTracer.TraceError((long)this.GetHashCode(), format, args);
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00072587 File Offset: 0x00070787
		internal ConnectionStatus GetConnectionStatus()
		{
			return new ConnectionStatus(this.m_channel.PartnerNodeName, this.m_channel.NetworkName, null, ConnectionDirection.Outgoing, true);
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x000725A8 File Offset: 0x000707A8
		public static Exception RunSeedSourceAction(Action op)
		{
			Exception result = null;
			try
			{
				op();
			}
			catch (IOException ex)
			{
				result = ex;
			}
			catch (UnauthorizedAccessException ex2)
			{
				result = ex2;
			}
			catch (SeedingChannelIsClosedException ex3)
			{
				result = ex3;
			}
			catch (SeedingSourceReplicaInstanceNotFoundException ex4)
			{
				result = ex4;
			}
			catch (LocalizedException ex5)
			{
				result = ex5;
			}
			catch (OperationCanceledException ex6)
			{
				result = ex6;
			}
			return result;
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00072630 File Offset: 0x00070830
		public static void ProcessSourceSideException(Exception ex, NetworkChannel channel)
		{
			if (ex is NetworkTransportException || ex is OperationCanceledException)
			{
				channel.Close();
				return;
			}
			channel.SendException(ex);
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00072650 File Offset: 0x00070850
		internal bool IsFromSameTargetServer(SeederServerContext context)
		{
			return this.m_targetServerGuid == null || context.TargetServerGuid == null || this.m_targetServerGuid.Equals(context.TargetServerGuid);
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00072698 File Offset: 0x00070898
		internal void StartSeeding()
		{
			lock (this.m_lock)
			{
				this.CheckSeedingCancelled();
				this.TraceDebug("seeding started", new object[0]);
				this.m_channel.IsSeeding = true;
				if (this.m_fPassiveSeeding)
				{
					if (this.m_setPassiveSeedingCallback == null)
					{
						this.m_setPassiveSeedingCallback = this.GetPassiveSeederStatusCallback();
					}
					this.m_setPassiveSeedingCallback.BeginPassiveSeeding(this.m_passiveSeedingSourceContext, false);
					ReplayEventLogConstants.Tuple_SeedingSourceBegin.LogEvent(null, new object[]
					{
						this.DatabaseName,
						this.TargetServerName
					});
					ReplayCrimsonEvents.PassiveSeedingSourceBegin.Log<Guid, string, string, string>(this.m_databaseGuid, this.DatabaseName, this.TargetServerName, string.Empty);
				}
				else
				{
					if (this.m_setActiveSeedingCallback == null)
					{
						this.m_setActiveSeedingCallback = this.GetActiveSeederStatusCallback();
					}
					if (this.m_setActiveSeedingCallback != null)
					{
						this.m_setActiveSeedingCallback.BeginActiveSeeding(this.SeedType);
					}
					ReplayEventLogConstants.Tuple_ActiveSeedingSourceBegin.LogEvent(null, new object[]
					{
						this.DatabaseName,
						this.TargetServerName
					});
					ReplayCrimsonEvents.ActiveSeedingSourceBegin.Log<Guid, string, string, string>(this.m_databaseGuid, this.DatabaseName, this.TargetServerName, string.Empty);
				}
				this.m_fSeedingInProgress = true;
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2269523261U);
			}
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00072800 File Offset: 0x00070A00
		internal bool IsSeeding()
		{
			bool fSeedingInProgress;
			lock (this.m_lock)
			{
				fSeedingInProgress = this.m_fSeedingInProgress;
			}
			return fSeedingInProgress;
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x000728F8 File Offset: 0x00070AF8
		internal void Close()
		{
			lock (this.m_lock)
			{
				if (!this.m_fClosed)
				{
					this.TraceDebug("Seeder server context is being closed.", new object[0]);
					this.m_fSeedingCancelled = true;
					if (this.m_channel.IsSeeding)
					{
						this.m_channel.Abort();
					}
					if (!string.IsNullOrEmpty(this.seedingHandle) && !this.ciSeedingIsCancelled)
					{
						bool cancelNeeded = false;
						long num = 0L;
						try
						{
							num = this.PerformCiSeedingAction(delegate
							{
								int progress = this.indexSeederSource.GetProgress(this.seedingHandle);
								if (progress >= 0 && progress < 100)
								{
									cancelNeeded = true;
									this.indexSeederSource.Cancel(this.seedingHandle, "Seeder server context is being closed.");
									this.ciSeedingIsCancelled = true;
								}
							});
						}
						catch (NetworkTransportException arg)
						{
							string errText = string.Format("SeederServerContext.Close failed: {0}", arg);
							SeederServerContext.Tracer.TraceError((long)this.GetHashCode(), errText);
							ReplayCrimsonEvents.CISeedingSourceError.Log<Guid, string, string, string>(this.DatabaseGuid, this.DatabaseName, this.TargetServerName, errText);
							if (!this.ciSeedingIsCancelled)
							{
								cancelNeeded = true;
								try
								{
									this.PerformCiSeedingAction(delegate
									{
										this.indexSeederSource.Cancel(this.seedingHandle, errText);
										this.ciSeedingIsCancelled = true;
									});
								}
								catch (NetworkTransportException arg2)
								{
									errText = string.Format("SeederServerContext.Close second attempt failed: {0}", arg2);
									ReplayCrimsonEvents.CISeedingSourceError.Log<Guid, string, string, string>(this.DatabaseGuid, this.DatabaseName, this.TargetServerName, errText);
								}
							}
						}
						SeederServerContext.Tracer.TraceError<long, bool>((long)this.GetHashCode(), "CI Check/Cancel took {0}ms. Cancel={1}", num, cancelNeeded);
						if (cancelNeeded)
						{
							string text = string.Format("CI seed cancelled by Close() in {0}ms", num);
							if (this.m_fPassiveSeeding)
							{
								ReplayCrimsonEvents.PassiveSeedingSourceCancel.Log<Guid, string, string, string>(this.m_databaseGuid, this.DatabaseName, this.TargetServerName, text);
							}
							else
							{
								ReplayCrimsonEvents.ActiveSeedingSourceCancel.Log<Guid, string, string, string>(this.m_databaseGuid, this.DatabaseName, this.TargetServerName, text);
							}
						}
					}
					if (this.IsSeeding())
					{
						if (this.m_fPassiveSeeding)
						{
							this.m_setPassiveSeedingCallback.EndPassiveSeeding();
							ReplayEventLogConstants.Tuple_SeedingSourceEnd.LogEvent(null, new object[]
							{
								this.DatabaseName,
								this.TargetServerName
							});
							ReplayCrimsonEvents.PassiveSeedingSourceEnd.Log<Guid, string, string, string>(this.m_databaseGuid, this.DatabaseName, this.TargetServerName, string.Empty);
						}
						else
						{
							if (this.m_setActiveSeedingCallback != null)
							{
								this.m_setActiveSeedingCallback.EndActiveSeeding();
							}
							ReplayEventLogConstants.Tuple_ActiveSeedingSourceEnd.LogEvent(null, new object[]
							{
								this.DatabaseName,
								this.TargetServerName
							});
							ReplayCrimsonEvents.ActiveSeedingSourceEnd.Log<Guid, string, string, string>(this.m_databaseGuid, this.DatabaseName, this.TargetServerName, string.Empty);
						}
					}
					this.m_fSeedingInProgress = false;
					if (this.indexSeederSource != null)
					{
						this.indexSeederSource.Dispose();
					}
					this.m_fClosed = true;
				}
			}
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x00072C34 File Offset: 0x00070E34
		internal void CancelSeeding(LocalizedString message)
		{
			lock (this.m_lock)
			{
				this.TraceDebug("Seeder server context is being cancelled.", new object[0]);
				this.m_fSeedingCancelled = true;
				if (this.m_channel.IsSeeding)
				{
					this.m_channel.Abort();
				}
				if (this.IsSeeding())
				{
					if (this.m_fPassiveSeeding)
					{
						this.m_setPassiveSeedingCallback.EndPassiveSeeding();
						ReplayEventLogConstants.Tuple_SeedingSourceCancel.LogEvent(null, new object[]
						{
							this.DatabaseName,
							this.TargetServerName,
							message
						});
						ReplayCrimsonEvents.PassiveSeedingSourceCancel.Log<Guid, string, string, LocalizedString>(this.m_databaseGuid, this.DatabaseName, this.TargetServerName, message);
					}
					else
					{
						if (this.m_setActiveSeedingCallback != null)
						{
							this.m_setActiveSeedingCallback.EndActiveSeeding();
						}
						ReplayEventLogConstants.Tuple_ActiveSeedingSourceCancel.LogEvent(null, new object[]
						{
							this.DatabaseName,
							this.TargetServerName,
							message
						});
						ReplayCrimsonEvents.ActiveSeedingSourceCancel.Log<Guid, string, string, LocalizedString>(this.m_databaseGuid, this.DatabaseName, this.TargetServerName, message);
					}
				}
				this.m_fSeedingInProgress = false;
			}
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x00072D7C File Offset: 0x00070F7C
		private void CheckSeedingCancelled()
		{
			if (this.m_fSeedingCancelled)
			{
				throw new OperationCanceledException();
			}
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x00072D8C File Offset: 0x00070F8C
		private MonitoredDatabase GetMonitoredDatabase()
		{
			MonitoredDatabase monitoredDatabase = MonitoredDatabase.FindMonitoredDatabase(this.m_channel.LocalNodeName, this.DatabaseGuid);
			if (monitoredDatabase == null)
			{
				throw new SourceDatabaseNotFoundException(this.DatabaseGuid, this.m_channel.LocalNodeName);
			}
			return monitoredDatabase;
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x00072DCC File Offset: 0x00070FCC
		public void SendLogFiles()
		{
			MonitoredDatabase monitoredDatabase = this.GetMonitoredDatabase();
			string suffix = '.' + monitoredDatabase.Config.LogExtension;
			DirectoryInfo di = new DirectoryInfo(monitoredDatabase.Config.SourceLogPath);
			long num = ShipControl.HighestGenerationInDirectory(di, monitoredDatabase.Config.LogFilePrefix, suffix);
			long num2 = ShipControl.LowestGenerationInDirectory(di, monitoredDatabase.Config.LogFilePrefix, suffix, false);
			for (long num3 = num2; num3 <= num; num3 += 1L)
			{
				this.CheckSeedingCancelled();
				try
				{
					monitoredDatabase.SendLog(num3, this.m_channel, null);
				}
				catch (FileIOonSourceException ex)
				{
					ExTraceGlobals.SeederServerTracer.TraceDebug<long, Exception>((long)this.GetHashCode(), "failed to send generation 0x{0:X} because {1}", num3, ex.InnerException);
					this.m_channel.SendException(ex.InnerException);
					this.CancelSeeding(ex.LocalizedString);
					return;
				}
			}
			NotifyEndOfLogReply notifyEndOfLogReply = new NotifyEndOfLogReply(this.m_channel, NetworkChannelMessage.MessageType.NotifyEndOfLogReply, num, DateTime.UtcNow);
			this.TraceDebug("reached the end of log files", new object[0]);
			notifyEndOfLogReply.Send();
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x00072EDC File Offset: 0x000710DC
		public void SendDatabaseFile()
		{
			ReplayStopwatch replayStopwatch = new ReplayStopwatch();
			replayStopwatch.Start();
			bool flag = false;
			SafeFileHandle safeFileHandle = null;
			try
			{
				this.TraceDebug("PassiveSeedDatabaseFileRequest. Opening up the backup context for {0}.", new object[]
				{
					this.DatabaseGuid
				});
				MonitoredDatabase monitoredDatabase = this.GetMonitoredDatabase();
				string databaseFullPath = monitoredDatabase.GetDatabaseFullPath();
				int num = 0;
				for (;;)
				{
					this.CheckSeedingCancelled();
					try
					{
						using (IStoreMountDismount storeMountDismountInstance = Dependencies.GetStoreMountDismountInstance(null))
						{
							storeMountDismountInstance.UnmountDatabase(Guid.Empty, monitoredDatabase.Config.IdentityGuid, 16);
							this.TraceDebug("dismounted the replayer database", new object[0]);
						}
					}
					catch (MapiExceptionNotFound)
					{
						this.TraceDebug("replay database is not mounted", new object[0]);
					}
					catch (MapiExceptionTimeout mapiExceptionTimeout)
					{
						this.TraceError("Rethrowing timeout exception: {0}", new object[]
						{
							mapiExceptionTimeout
						});
						throw;
					}
					catch (MapiRetryableException ex)
					{
						if (num++ < 3)
						{
							this.TraceDebug("got {0}, but we will keep retrying for 3 times", new object[]
							{
								ex.ToString()
							});
							Thread.Sleep(1000);
							continue;
						}
						throw;
					}
					catch (MapiPermanentException ex2)
					{
						if (num++ < 3)
						{
							this.TraceDebug("got {0}, but we will keep retrying for 3 times", new object[]
							{
								ex2.ToString()
							});
							Thread.Sleep(1000);
							continue;
						}
						throw;
					}
					break;
				}
				safeFileHandle = this.OpenFile(databaseFullPath, true);
				this.m_passiveDatabaseStream = SeederServerContext.OpenFileStream(safeFileHandle, true);
				SeedDatabaseFileReply seedDatabaseFileReply = new SeedDatabaseFileReply(this.m_channel);
				seedDatabaseFileReply.FileSize = new FileInfo(databaseFullPath).Length;
				seedDatabaseFileReply.LastWriteUtc = DateTime.UtcNow;
				seedDatabaseFileReply.Send();
				ExTraceGlobals.SeederServerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "PassiveSeedDatabaseFileRequest. Sending the data for {0}.", this.DatabaseGuid);
				this.m_channel.SendSeedingDataTransferReply(seedDatabaseFileReply, new ReadDatabaseCallback(this.ReadDbCallback));
				flag = true;
			}
			finally
			{
				if (this.m_passiveDatabaseStream != null)
				{
					this.m_passiveDatabaseStream.Dispose();
				}
				ExTraceGlobals.SeederServerTracer.TraceDebug<long, string>((long)this.GetHashCode(), "PassiveSeedDatabaseFile finished streaming after {0} sec. Operation successful: {1}", replayStopwatch.ElapsedMilliseconds / 1000L, flag.ToString());
				if (safeFileHandle != null)
				{
					safeFileHandle.Dispose();
				}
			}
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x0007318C File Offset: 0x0007138C
		private SafeFileHandle OpenFile(string filename, bool openForRead)
		{
			FileMode creationDisposition;
			FileShare fileShare;
			FileAccess fileAccess;
			if (openForRead)
			{
				creationDisposition = FileMode.Open;
				fileShare = FileShare.None;
				fileAccess = FileAccess.Read;
			}
			else
			{
				creationDisposition = FileMode.Create;
				fileShare = FileShare.None;
				fileAccess = FileAccess.Write;
			}
			FileFlags flags = FileFlags.FILE_FLAG_NO_BUFFERING;
			SafeFileHandle safeFileHandle = NativeMethods.CreateFile(filename, fileAccess, fileShare, IntPtr.Zero, creationDisposition, flags, IntPtr.Zero);
			if (safeFileHandle.IsInvalid)
			{
				throw new IOException(string.Format("CreateFile({0}) = {1}", filename, Marshal.GetLastWin32Error().ToString()), new Win32Exception());
			}
			return safeFileHandle;
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x000731F8 File Offset: 0x000713F8
		private int ReadDbCallback(byte[] buf, ulong readOffset, int bytesToRead)
		{
			if (buf == null)
			{
				throw new ArgumentNullException("buf");
			}
			if (buf.Length < bytesToRead)
			{
				throw new ArgumentException("buf");
			}
			if (this.m_passiveDatabaseStream == null)
			{
				throw new InvalidOperationException("file handle is null");
			}
			this.CheckSeedingCancelled();
			return this.m_passiveDatabaseStream.Read(buf, 0, bytesToRead);
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x000732D0 File Offset: 0x000714D0
		internal void SeedToEndpoint(string endpoint, string reason)
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "SeedToEndpoint databaseGuid ({0}), endpoint ({1}).", this.m_databaseGuid, endpoint);
			this.ciSeedStartTimeUtc = ExDateTime.UtcNow;
			long arg = this.PerformCiSeedingAction(delegate
			{
				this.seedingHandle = this.indexSeederSource.SeedToEndPoint(endpoint, reason);
				ExTraceGlobals.SeederServerTracer.TraceDebug<string>((long)this.GetHashCode(), "IndexSeeder.SeedToEndPoint returned handle {0}", this.seedingHandle);
				SeedCiFileReply seedCiFileReply = new SeedCiFileReply(this.m_channel, this.seedingHandle);
				seedCiFileReply.Send();
			});
			ExTraceGlobals.SeederServerTracer.TraceDebug<long>((long)this.GetHashCode(), "SeedToEndpoint finished call after {0}ms.", arg);
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x000733C4 File Offset: 0x000715C4
		internal void HandleCancelCiFileRequest(string reason)
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "CancelSeeding databaseGuid ({0}), handle ({1}).", this.m_databaseGuid, this.seedingHandle);
			long arg = this.PerformCiSeedingAction(delegate
			{
				this.indexSeederSource.Cancel(this.seedingHandle, reason);
				this.ciSeedingIsCancelled = true;
				ExTraceGlobals.SeederServerTracer.TraceDebug((long)this.GetHashCode(), "IndexSeeder.Cancel() is called");
				CancelCiFileReply cancelCiFileReply = new CancelCiFileReply(this.m_channel);
				cancelCiFileReply.Send();
			});
			ExTraceGlobals.SeederServerTracer.TraceDebug<long>((long)this.GetHashCode(), "CancelSeeding finished call after {0}ms.", arg);
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x000734C4 File Offset: 0x000716C4
		internal void GetSeedingProgress(string handle)
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "GetSeedingProgress databaseGuid ({0}), handle ({1}).", this.m_databaseGuid, this.seedingHandle);
			long arg = this.PerformCiSeedingAction(delegate
			{
				int num = -1;
				if (RegistryParameters.TestDelayCatalogSeedSec > 0)
				{
					double totalSeconds = (ExDateTime.UtcNow - this.ciSeedStartTimeUtc).TotalSeconds;
					double num2 = (double)RegistryParameters.TestDelayCatalogSeedSec;
					if (totalSeconds < num2)
					{
						num = (int)(totalSeconds * 100.0 / num2);
					}
				}
				if (num < 0 || num >= 100)
				{
					num = this.indexSeederSource.GetProgress(this.seedingHandle);
				}
				ExTraceGlobals.SeederServerTracer.TraceDebug<int>((long)this.GetHashCode(), "IndexSeeder.GetProgress returned {0}", num);
				ProgressCiFileReply progressCiFileReply = new ProgressCiFileReply(this.m_channel, num);
				progressCiFileReply.Send();
			});
			ExTraceGlobals.SeederServerTracer.TraceDebug<long>((long)this.GetHashCode(), "GetSeedingProgress finished call after {0}ms.", arg);
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x00073520 File Offset: 0x00071720
		private long PerformCiSeedingAction(Action action)
		{
			ReplayStopwatch replayStopwatch = new ReplayStopwatch();
			replayStopwatch.Start();
			Exception ex = null;
			try
			{
				action();
			}
			catch (PerformingFastOperationException ex2)
			{
				ex = ex2;
			}
			catch (FileIOonSourceException ex3)
			{
				ex = ex3;
			}
			finally
			{
				replayStopwatch.Stop();
			}
			if (ex != null)
			{
				ReplayCrimsonEvents.CISeedingSourceError.Log<Guid, string, string, string>(this.DatabaseGuid, this.DatabaseName, this.TargetServerName, ex.ToString());
				this.m_channel.SendException(ex);
			}
			return replayStopwatch.ElapsedMilliseconds;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x000735B8 File Offset: 0x000717B8
		private ISetPassiveSeeding GetPassiveSeederStatusCallback()
		{
			IReplicaInstanceManager replicaInstanceManager = Dependencies.ReplayCoreManager.ReplicaInstanceManager;
			ISetPassiveSeeding passiveSeederStatusCallback = replicaInstanceManager.GetPassiveSeederStatusCallback(this.DatabaseGuid);
			if (passiveSeederStatusCallback == null)
			{
				this.TraceError("GetPassiveSeederStatusCallback(): The valid RI is not running. The RI might be present after a retry.", new object[0]);
				throw new SeedingSourceReplicaInstanceNotFoundException(this.DatabaseGuid, Environment.MachineName);
			}
			return passiveSeederStatusCallback;
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x00073608 File Offset: 0x00071808
		public void LinkWithNewActiveRIStatus(ISetActiveSeeding riStatus)
		{
			lock (this.m_lock)
			{
				if (this.IsSeeding())
				{
					if (this.m_setPassiveSeedingCallback != null)
					{
						this.m_setPassiveSeedingCallback.EndPassiveSeeding();
						this.m_setPassiveSeedingCallback = null;
					}
					this.m_fPassiveSeeding = false;
					this.m_passiveSeedingSourceContext = PassiveSeedingSourceContextEnum.None;
					this.m_setActiveSeedingCallback = riStatus;
					riStatus.BeginActiveSeeding(this.SeedType);
				}
			}
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x00073688 File Offset: 0x00071888
		public void LinkWithNewPassiveRIStatus(ISetPassiveSeeding riStatus)
		{
			lock (this.m_lock)
			{
				if (this.IsSeeding())
				{
					if (this.m_setActiveSeedingCallback != null)
					{
						this.m_setActiveSeedingCallback.EndActiveSeeding();
						this.m_setActiveSeedingCallback = null;
					}
					this.m_fPassiveSeeding = true;
					this.m_passiveSeedingSourceContext = PassiveSeedingSourceContextEnum.Catalogue;
					this.m_setPassiveSeedingCallback = riStatus;
					riStatus.BeginPassiveSeeding(this.m_passiveSeedingSourceContext, true);
				}
			}
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x00073708 File Offset: 0x00071908
		private ISetActiveSeeding GetActiveSeederStatusCallback()
		{
			if (TestSupport.IsZerobox())
			{
				return null;
			}
			IReplicaInstanceManager replicaInstanceManager = Dependencies.ReplayCoreManager.ReplicaInstanceManager;
			ISetActiveSeeding activeSeederStatusCallback = replicaInstanceManager.GetActiveSeederStatusCallback(this.DatabaseGuid);
			if (activeSeederStatusCallback == null)
			{
				this.TraceError("GetActiveSeederStatusCallback(): The valid RI is not running. The RI might be present after a retry.", new object[0]);
				throw new SeedingSourceReplicaInstanceNotFoundException(this.DatabaseGuid, Environment.MachineName);
			}
			return activeSeederStatusCallback;
		}

		// Token: 0x04000AAD RID: 2733
		private static readonly Trace Tracer = ExTraceGlobals.SeederServerTracer;

		// Token: 0x04000AAE RID: 2734
		private NetworkChannel m_channel;

		// Token: 0x04000AAF RID: 2735
		private IIndexSeederSource indexSeederSource;

		// Token: 0x04000AB0 RID: 2736
		private Guid m_databaseGuid;

		// Token: 0x04000AB1 RID: 2737
		private FileStream m_passiveDatabaseStream;

		// Token: 0x04000AB2 RID: 2738
		private bool m_fSeedingCancelled;

		// Token: 0x04000AB3 RID: 2739
		private Guid? m_targetServerGuid;

		// Token: 0x04000AB4 RID: 2740
		private IADServer m_targetServer;

		// Token: 0x04000AB5 RID: 2741
		private bool m_fPassiveSeeding;

		// Token: 0x04000AB6 RID: 2742
		private PassiveSeedingSourceContextEnum m_passiveSeedingSourceContext;

		// Token: 0x04000AB7 RID: 2743
		private bool m_fClosed;

		// Token: 0x04000AB8 RID: 2744
		private bool m_fSeedingInProgress;

		// Token: 0x04000AB9 RID: 2745
		private ISetPassiveSeeding m_setPassiveSeedingCallback;

		// Token: 0x04000ABA RID: 2746
		private ISetActiveSeeding m_setActiveSeedingCallback;

		// Token: 0x04000ABB RID: 2747
		private object m_lock = new object();

		// Token: 0x04000ABC RID: 2748
		private string seedingHandle;

		// Token: 0x04000ABD RID: 2749
		private bool ciSeedingIsCancelled;

		// Token: 0x04000ABE RID: 2750
		private ExDateTime ciSeedStartTimeUtc;
	}
}
