using System;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.EseRepl;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000342 RID: 834
	internal class LogCopier : IStartStop
	{
		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x060021BF RID: 8639 RVA: 0x0009CC07 File Offset: 0x0009AE07
		public static Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return ExTraceGlobals.LogCopierTracer;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x0009CC0E File Offset: 0x0009AE0E
		internal PassiveBlockMode PassiveBlockMode
		{
			get
			{
				return this.m_passiveBlockMode;
			}
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x0009CC16 File Offset: 0x0009AE16
		internal static string FormatLogGeneration(long gen)
		{
			return string.Format("0x{0:X}({0})", gen);
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x0009CC28 File Offset: 0x0009AE28
		private static void ReadCallback(object asyncState, int bytesAvailable, bool completionIsSynchronous, Exception e)
		{
			LogCopier logCopier = (LogCopier)asyncState;
			logCopier.ProcessReadCallback(bytesAvailable, e);
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x0009CC44 File Offset: 0x0009AE44
		private static void WakeUpCallback(object context)
		{
			LogCopier logCopier = (LogCopier)context;
			logCopier.ProcessWakeUp();
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x0009CC60 File Offset: 0x0009AE60
		private static LogCopier FindCopier(string nodeName, Guid dbGuid, bool throwOnError)
		{
			LogCopier logCopier = null;
			IFindComponent componentFinder = Dependencies.ComponentFinder;
			if (componentFinder != null)
			{
				logCopier = componentFinder.FindLogCopier(nodeName, dbGuid);
			}
			if (logCopier == null)
			{
				LogCopier.Tracer.TraceError<Guid>(0L, "FindCopier failed to find Copier {0}", dbGuid);
				if (throwOnError)
				{
					throw new ReplayServiceUnknownReplicaInstanceException("FindCopier:Copier not active", dbGuid.ToString());
				}
			}
			return logCopier;
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x0009CCB4 File Offset: 0x0009AEB4
		public static void TestDisconnectCopier(Guid dbGuid)
		{
			LogCopier logCopier = LogCopier.FindCopier(null, dbGuid, true);
			logCopier.TestDisconnect();
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x0009CCD0 File Offset: 0x0009AED0
		public LogCopier(IPerfmonCounters perfmonCounters, string fromPrefix, long fromNumber, string fromSuffix, string to, string toFinal, IReplayConfiguration replayConfiguration, FileState fileState, ISetBroken setBroken, ISetDisconnected setDisconnected, ISetGeneration setGeneration, NetworkPath netPath, bool runningAcll)
		{
			this.m_fromPrefix = fromPrefix;
			this.m_fromNumber = fromNumber;
			this.m_fromSuffix = fromSuffix;
			this.m_logCopierSetBroken = new ShipLogsSetBroken(setBroken, setDisconnected);
			this.m_setBroken = this.m_logCopierSetBroken;
			this.m_setDisconnected = this.m_logCopierSetBroken;
			this.m_setGeneration = setGeneration;
			this.m_fConstructedForAcll = runningAcll;
			this.m_initialNetPath = netPath;
			this.m_perfmonCounters = perfmonCounters;
			this.m_config = replayConfiguration;
			this.m_fileState = fileState;
			this.LocalNodeName = AmServerName.GetSimpleName(replayConfiguration.ServerName);
			LogCopier.Tracer.TraceDebug((long)this.GetHashCode(), "LogCopier constructed for database '{0}': SourceNode='{1}'. CopyTarget= '{2}'. FinalTarget= '{3}'. FromNumber= 0x{4:x}. FromPrefix='{5}'", new object[]
			{
				this.m_config.DatabaseName,
				this.m_config.SourceMachine,
				to,
				toFinal,
				fromNumber,
				fromPrefix
			});
			this.m_to = to;
			this.m_srv = this.m_config.GetAdServerObject();
			this.m_passiveBlockMode = new PassiveBlockMode(this, setBroken, this.GetMaxBlockModeDepthInBytes());
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x060021C7 RID: 8647 RVA: 0x0009CE1D File Offset: 0x0009B01D
		// (set) Token: 0x060021C8 RID: 8648 RVA: 0x0009CE25 File Offset: 0x0009B025
		internal string LocalNodeName { get; private set; }

		// Token: 0x060021C9 RID: 8649 RVA: 0x0009CE2E File Offset: 0x0009B02E
		private int GetMaxBlockModeDepthInBytes()
		{
			return PassiveBlockMode.GetMaxMemoryPerDatabase(this.m_srv);
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x0009CE3B File Offset: 0x0009B03B
		internal void SetReportingCallbacks(ISetBroken setBroken, ISetDisconnected setDisconnected)
		{
			this.m_logCopierSetBroken.SetReportingCallbacksForAcll(setBroken, setDisconnected);
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x0009CE4A File Offset: 0x0009B04A
		public IPerfmonCounters PerfmonCounters
		{
			get
			{
				return this.m_perfmonCounters;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x060021CC RID: 8652 RVA: 0x0009CE52 File Offset: 0x0009B052
		public IReplayConfiguration Configuration
		{
			get
			{
				return this.m_config;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x060021CD RID: 8653 RVA: 0x0009CE5A File Offset: 0x0009B05A
		// (set) Token: 0x060021CE RID: 8654 RVA: 0x0009CE62 File Offset: 0x0009B062
		protected long FromNumber
		{
			get
			{
				return this.m_fromNumber;
			}
			set
			{
				this.m_fromNumber = value;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x0009CE6B File Offset: 0x0009B06B
		public long NextGenExpected
		{
			get
			{
				return this.m_fromNumber;
			}
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x0009CE73 File Offset: 0x0009B073
		public void UpdateNextGenExpected(long nextGen)
		{
			this.m_fromNumber = nextGen;
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x060021D1 RID: 8657 RVA: 0x0009CE7C File Offset: 0x0009B07C
		protected string FromPrefix
		{
			get
			{
				return this.m_fromPrefix;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x0009CE84 File Offset: 0x0009B084
		protected string FromSuffix
		{
			get
			{
				return this.m_fromSuffix;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x0009CE8C File Offset: 0x0009B08C
		internal Guid DatabaseGuid
		{
			get
			{
				return this.m_config.IdentityGuid;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x0009CE99 File Offset: 0x0009B099
		internal string DatabaseName
		{
			get
			{
				return this.m_config.DatabaseName;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x060021D5 RID: 8661 RVA: 0x0009CEA6 File Offset: 0x0009B0A6
		public long HighestCopiedGeneration
		{
			get
			{
				if (this.FromNumber == 0L)
				{
					return 0L;
				}
				return this.FromNumber - 1L;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x060021D6 RID: 8662 RVA: 0x0009CEBE File Offset: 0x0009B0BE
		private NetworkChannel Channel
		{
			get
			{
				return this.m_copyClient.Channel;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x0009CECB File Offset: 0x0009B0CB
		private long LastKnownGeneration
		{
			get
			{
				return this.m_config.ReplayState.CopyNotificationGenerationNumber;
			}
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x0009CEE0 File Offset: 0x0009B0E0
		internal void TraceDebug(string format, params object[] args)
		{
			if (ExTraceGlobals.LogCopierTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				string arg = string.Format(format, args);
				ExTraceGlobals.LogCopierTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0}: {1}", this.DatabaseName, arg);
			}
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x0009CF20 File Offset: 0x0009B120
		internal void TraceError(string format, params object[] args)
		{
			string arg = string.Format(format, args);
			ExTraceGlobals.LogCopierTracer.TraceError<string, string>((long)this.GetHashCode(), "{0}: {1}", this.DatabaseName, arg);
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x0009CF52 File Offset: 0x0009B152
		[Conditional("DEBUG")]
		private void AssertWorkerLockIsHeldByMe()
		{
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x0009CF54 File Offset: 0x0009B154
		private void TestDelaySleep()
		{
			int logCopyDelayInMsec = RegistryParameters.LogCopyDelayInMsec;
			if (logCopyDelayInMsec > 0)
			{
				Thread.Sleep(logCopyDelayInMsec);
			}
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x0009CF71 File Offset: 0x0009B171
		public void TrackInspectorGeneration(long releasedGeneration, DateTime writeTimeUtc)
		{
			if (releasedGeneration == 0L)
			{
				return;
			}
			this.m_highestGenReleasedToInspector = releasedGeneration;
			this.FromNumber = this.m_highestGenReleasedToInspector + 1L;
			if (this.m_setGeneration != null)
			{
				this.m_setGeneration.SetCopyGeneration(releasedGeneration, writeTimeUtc);
			}
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x0009CFA4 File Offset: 0x0009B1A4
		public void TrackKnownEndOfLog(long highestKnownGeneration, DateTime writeTimeUtc)
		{
			if (this.m_setGeneration != null)
			{
				this.m_setGeneration.SetCopyNotificationGeneration(highestKnownGeneration, writeTimeUtc);
			}
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x0009CFBB File Offset: 0x0009B1BB
		public void TrackLastContactTime(DateTime lastContactUtc)
		{
			this.m_config.ReplayState.LatestCopierContactTime = lastContactUtc;
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x0009CFD0 File Offset: 0x0009B1D0
		private void ReceiveLogFile(CopyLogReply incomingLog)
		{
			long thisLogGeneration = incomingLog.ThisLogGeneration;
			if (this.m_setGeneration != null)
			{
				this.m_setGeneration.SetCopyNotificationGeneration(incomingLog.EndOfLogGeneration, incomingLog.EndOfLogUtc);
			}
			this.TrackLastContactTime(incomingLog.MessageUtc);
			this.TraceDebug("ReceiveLogFile 0x{0:X}. EOL=0x{1:X}", new object[]
			{
				thisLogGeneration,
				incomingLog.EndOfLogGeneration
			});
			string text = EseHelper.MakeLogfileName(this.FromPrefix, this.FromSuffix, thisLogGeneration);
			string destFileName = Path.Combine(this.m_to, text);
			string path = 'S' + text.Substring(1);
			string text2 = Path.Combine(this.m_to, path);
			CheckSummer summer = null;
			if (this.Channel.ChecksumDataTransfer)
			{
				summer = new CheckSummer();
			}
			incomingLog.ReceiveFile(text2, this.m_perfmonCounters, summer);
			if (this.m_perfmonCounters != null)
			{
				this.m_perfmonCounters.RecordLogCopyThruput(incomingLog.FileSize);
			}
			File.Move(text2, destFileName);
			this.TrackInspectorGeneration(thisLogGeneration, incomingLog.LastWriteUtc);
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x0009D0D4 File Offset: 0x0009B2D4
		public void CollectConnectionStatus(RpcDatabaseCopyStatus2 copyStatus)
		{
			string nodeNameFromFqdn = MachineName.GetNodeNameFromFqdn(this.m_config.SourceMachine);
			string network = null;
			LogCopyClient copyClient = this.m_copyClient;
			if (copyClient != null)
			{
				NetworkChannel channel = copyClient.Channel;
				if (channel != null)
				{
					NetworkPath networkPath = channel.NetworkPath;
					if (networkPath != null)
					{
						network = networkPath.NetworkName;
					}
				}
			}
			string lastFailure = null;
			if (this.m_failureOnNetwork && this.m_lastException != null)
			{
				lastFailure = this.m_lastException.Message;
			}
			ConnectionStatus obj = new ConnectionStatus(nodeNameFromFqdn, network, lastFailure, ConnectionDirection.Incoming, false);
			byte[] incomingLogCopyingNetwork = Serialization.ObjectToBytes(obj);
			copyStatus.IncomingLogCopyingNetwork = incomingLogCopyingNetwork;
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x0009D198 File Offset: 0x0009B398
		private Exception CleanupOldConnection(int timeoutInMsec)
		{
			LogCopier.Tracer.TraceDebug<string>((long)this.GetHashCode(), "CleanupOldConnection({0})", this.DatabaseName);
			LogCopyClient curConnection = this.m_copyClient;
			Exception ex = NetworkChannel.RunNetworkFunction(delegate
			{
				if (curConnection != null)
				{
					if (this.m_readIsActive)
					{
						this.WaitForReadCallback(false, timeoutInMsec);
					}
					curConnection.Close();
				}
			});
			this.m_readIsActive = false;
			this.m_copyClient = null;
			this.m_incomingMessage = null;
			this.m_readCallbackException = null;
			this.m_connectionIsBeingDiscarded = false;
			if (ex != null)
			{
				LogCopier.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "CleanupOldConnection({0}) failed: {1}", this.DatabaseName, ex);
			}
			return ex;
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x0009D238 File Offset: 0x0009B438
		private bool EstablishConnection()
		{
			if (this.m_connectionIsBeingDiscarded)
			{
				this.CleanupOldConnection(this.GetLogShipTimeoutMs());
			}
			if (this.m_copyClient == null)
			{
				LogCopier.Tracer.TraceDebug((long)this.GetHashCode(), "EstablishConnection establishing new connection");
				this.m_copyClient = new LogCopyClient(this.m_config, this.m_perfmonCounters, this.m_initialNetPath, this.GetLogShipTimeoutMs());
				this.m_initialNetPath = null;
				NetworkChannel networkChannel = this.m_copyClient.OpenChannel();
				this.m_perfmonCounters.CompressionEnabled = (networkChannel.IsCompressionEnabled ? 1L : 0L);
				this.m_perfmonCounters.EncryptionEnabled = (networkChannel.IsEncryptionEnabled ? 1L : 0L);
				this.m_lastSentPingCounter = 0L;
				this.m_setDisconnected.ClearDisconnected();
				IADServer server = Dependencies.ADConfig.GetServer(this.m_config.SourceMachine);
				bool flag = false;
				if (server == null)
				{
					LogCopier.Tracer.TraceError<string>((long)this.GetHashCode(), "AD lookup for active {0} failed.", this.m_config.SourceMachine);
				}
				else if (RegistryParameters.TreatLogCopyPartnerAsDownlevel)
				{
					LogCopier.Tracer.TraceDebug((long)this.GetHashCode(), "TreatLogCopyPartnerAsDownlevel regkey forcing V1 repl");
				}
				else
				{
					ServerVersion serverVersion = new ServerVersion(server.VersionNumber);
					ServerVersion arg = new ServerVersion(this.m_config.ServerVersion);
					LogCopier.Tracer.TraceDebug<string, ServerVersion>((long)this.GetHashCode(), "Active {0} has version {1}", this.m_config.SourceMachine, serverVersion);
					LogCopier.Tracer.TraceDebug<ServerVersion>((long)this.GetHashCode(), "Local has version {0}", arg);
					if (ServerVersion.Compare(LogCopier.FirstVersionSupportingQueryVersion, serverVersion) <= 0)
					{
						flag = true;
					}
					else if (ServerVersion.Compare(LogCopier.FirstVersionSupportingV2, serverVersion) <= 0)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					LogCopier.Tracer.TraceError((long)this.GetHashCode(), "Using V1 filemode");
					this.DeclareServerIsDownLevel();
				}
				this.m_passiveBlockMode.SetCrossSiteFlag(server, this.m_srv);
				return true;
			}
			return false;
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x0009D404 File Offset: 0x0009B604
		private void CancelConnection()
		{
			LogCopyClient copyClient = this.m_copyClient;
			if (copyClient != null)
			{
				copyClient.Cancel();
			}
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x0009D421 File Offset: 0x0009B621
		private void DiscardConnection()
		{
			LogCopier.Tracer.TraceDebug((long)this.GetHashCode(), "DiscardConnection entered");
			this.CancelConnection();
			this.m_connectionIsBeingDiscarded = true;
			LogCopier.Tracer.TraceDebug((long)this.GetHashCode(), "DiscardConnection exits");
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x0009D45C File Offset: 0x0009B65C
		private void Initialize()
		{
			long num = this.m_copyClient.QueryLogRange();
			if (num == 0L)
			{
				Exception ex = new LogCopierFailedNoLogsOnSourceException(this.m_config.SourceMachine);
				this.m_setBroken.SetBroken(FailureTag.NoOp, ReplayEventLogConstants.Tuple_LogCopierFoundNoLogsOnSource, ex, new string[]
				{
					this.m_config.SourceMachine
				});
				throw ex;
			}
			if (this.m_fromNumber == 0L)
			{
				this.m_fromNumber = num;
				ReplayCrimsonEvents.LogCopierStartingWithLowestGenOnSource.Log<string, string, Guid, string>(this.m_config.DatabaseName, Environment.MachineName, this.m_config.DatabaseGuid, LogCopier.FormatLogGeneration(num));
			}
			else if (this.m_fromNumber < num)
			{
				bool flag;
				long num2;
				long num3;
				this.m_fileState.GetLowestAndHighestGenerationsRequired(out flag, out num2, out num3);
				if (flag || this.m_fromNumber >= num2)
				{
					string text = EseHelper.MakeLogfileName(this.FromPrefix, this.FromSuffix, this.m_fromNumber);
					Exception ex = new LogCopierFailsBecauseLogGapException(this.m_config.SourceMachine, text);
					this.m_setBroken.SetBroken(FailureTag.Reseed, ReplayEventLogConstants.Tuple_LogFileGapFound, ex, new string[]
					{
						text
					});
					throw ex;
				}
				this.TraceDebug("LogCopier is skipping past log generation {0} since it is lower than the required range (lowestReqGen = {2}).", new object[]
				{
					this.m_fromNumber,
					num2
				});
				long num4 = Math.Min(num2, num);
				this.m_setGeneration.SetLogStreamStartGeneration(num4);
				ReplayEventLogConstants.Tuple_LogFileCorruptOrGapFoundOutsideRequiredRange.LogEvent(null, new object[]
				{
					this.m_config.DatabaseName,
					EseHelper.MakeLogfileName(this.m_config.LogFilePrefix, this.m_config.LogFileSuffix, this.m_fromNumber),
					num4
				});
				this.m_setBroken.RestartInstanceSoon(true);
				throw new LogCopierInitFailedActiveTruncatingException(this.m_config.SourceMachine, this.m_fromNumber, num);
			}
			LogCopier.Tracer.TraceDebug<string, long>((long)this.GetHashCode(), "First generation to copy for db '{0}' is 0x{1:X8}", this.m_config.DatabaseName, this.FromNumber);
			this.m_highestGenReleasedToInspector = this.FromNumber - 1L;
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x0009D668 File Offset: 0x0009B868
		private Exception InvokeWithCatch(LogCopier.CatchableOperation op)
		{
			Exception ex = null;
			try
			{
				op();
			}
			catch (GranularReplicationTerminatedException ex2)
			{
				ex = ex2;
			}
			catch (ArgumentException ex3)
			{
				ex = ex3;
			}
			catch (FileSharingViolationOnSourceException ex4)
			{
				ex = ex4;
			}
			catch (IOException ex5)
			{
				ex = ex5;
			}
			catch (NetworkRemoteException ex6)
			{
				ex = ex6;
			}
			catch (NetworkTransportException ex7)
			{
				ex = ex7;
			}
			catch (NotSupportedException ex8)
			{
				ex = ex8;
			}
			catch (ObjectDisposedException ex9)
			{
				ex = ex9;
			}
			catch (SecurityException ex10)
			{
				ex = ex10;
			}
			catch (UnauthorizedAccessException ex11)
			{
				ex = ex11;
			}
			catch (OperationAbortedException ex12)
			{
				ex = ex12;
			}
			catch (SetBrokenControlTransferException ex13)
			{
				ex = ex13;
			}
			catch (LogCopierInitFailedActiveTruncatingException ex14)
			{
				ex = ex14;
			}
			catch (TransientException ex15)
			{
				ex = ex15;
			}
			catch (OperationCanceledException ex16)
			{
				ex = ex16;
			}
			if (ex != null)
			{
				this.TraceError("InvokeWithCatch caught {0}", new object[]
				{
					ex
				});
			}
			return ex;
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x0009D7B8 File Offset: 0x0009B9B8
		private void ShortRetryPolicy()
		{
			this.m_nextWait = this.m_lastWait + 10000;
			if (this.m_nextWait > 30000)
			{
				this.m_nextWait = 30000;
			}
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x0009D7E4 File Offset: 0x0009B9E4
		private void HandleWorkerException(Exception e)
		{
			Exception ex;
			if (this.HandleWorkerExceptionInternal(e, out ex))
			{
				this.TraceError("Setting broken. Was remote exception: {0}", new object[]
				{
					this.m_failureOnSource
				});
				if (this.m_failureOnSource)
				{
					this.SetBrokenDueToSource(ex, false);
					return;
				}
				this.SetBrokenDueToTarget(ex);
			}
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x0009D838 File Offset: 0x0009BA38
		private bool HandleWorkerExceptionInternal(Exception e, out Exception exForBroken)
		{
			bool flag = false;
			exForBroken = e;
			if (e is LogCopierInitFailedActiveTruncatingException)
			{
				return this.RunningAcll;
			}
			if (e is SetBrokenControlTransferException)
			{
				return flag;
			}
			if (this.m_setBroken.IsBroken)
			{
				return flag;
			}
			this.m_failureOnSource = false;
			this.m_failureOnNetwork = false;
			if (e is NetworkRemoteException)
			{
				this.m_failureOnSource = true;
				this.m_resetAfterError = true;
				e = e.InnerException;
				exForBroken = e;
				LogCopier.Tracer.TraceError<Exception>((long)this.GetHashCode(), "Network forwarded remote exception from the source: {0}", e);
			}
			else
			{
				LogCopier.Tracer.TraceError<Exception>((long)this.GetHashCode(), "HandleWorkerException {0}", e);
			}
			this.m_lastException = e;
			if (e is GranularReplicationTerminatedException || e is GranularReplicationOverflowException)
			{
				return true;
			}
			if (e is NetworkTransportException)
			{
				this.m_failureOnNetwork = true;
				this.ShortRetryPolicy();
				this.DiscardConnection();
				this.m_setDisconnected.SetDisconnected(FailureTag.NoOp, ReplayEventLogConstants.Tuple_LogCopierFailedToCommunicate, new string[]
				{
					this.m_config.SourceMachine,
					e.Message
				});
				return false;
			}
			if (e is SourceDatabaseNotFoundException)
			{
				this.ShortRetryPolicy();
				this.DiscardConnection();
				this.m_setDisconnected.SetDisconnected(FailureTag.NoOp, ReplayEventLogConstants.Tuple_LogCopierReceivedSourceSideError, new string[]
				{
					this.m_config.SourceMachine,
					e.Message
				});
				return false;
			}
			flag = this.RunningAcll;
			if (this.m_failureOnSource)
			{
				Exception ex;
				if (e.TryGetExceptionOrInnerOfType(out ex) || e.TryGetExceptionOrInnerOfType(out ex) || e.TryGetExceptionOrInnerOfType(out ex))
				{
					long fromNumber = this.FromNumber;
					this.TraceError("LogCopier got source-side corrupt or missing log file exception for log gen {0}. Exception: {1}", new object[]
					{
						fromNumber,
						ex.Message
					});
					bool flag2;
					long num;
					long num2;
					this.m_fileState.GetLowestAndHighestGenerationsRequired(out flag2, out num, out num2);
					this.TraceDebug("Database IsConsistent={0}, LowestRequiredGen={1}, HighestRequiredGen={2}", new object[]
					{
						flag2,
						num,
						num2
					});
					if (flag2 || fromNumber > num2)
					{
						if (ex is SourceLogBreakStallsPassiveException)
						{
							this.TraceError("LogCopier is stalled by exception on source: {0}", new object[]
							{
								ex.Message
							});
							this.m_stalledDueToSourceLogBreak = true;
							this.m_sourceLogCorruptEx = ex;
							this.m_stalledSince = new DateTime?(DateTime.UtcNow);
							ReplayEventLogConstants.Tuple_LogCopierIsStalledDueToSource.LogEvent(null, new object[]
							{
								this.m_config.DatabaseName,
								this.m_config.SourceMachine,
								ex.Message
							});
							return flag;
						}
						if (ex is CorruptLogDetectedException)
						{
							this.m_sourceLogCorruptEx = ex;
							this.m_setBroken.SetBroken(FailureTag.Reseed, ReplayEventLogConstants.Tuple_LogCopierIsStalledDueToSource, ex, new string[]
							{
								this.m_config.SourceMachine,
								ex.Message
							});
							return false;
						}
						if (ex is FileNotFoundException)
						{
							e = ex;
							exForBroken = e;
						}
					}
					else
					{
						if (fromNumber < num)
						{
							this.TraceDebug("LogCopier is skipping past log generation {0} since it is lower than the required range.", new object[]
							{
								fromNumber
							});
							long num3 = Math.Min(num, fromNumber + 1L);
							this.m_setGeneration.SetLogStreamStartGeneration(num3);
							ReplayEventLogConstants.Tuple_LogFileCorruptOrGapFoundOutsideRequiredRange.LogEvent(null, new object[]
							{
								this.m_config.DatabaseName,
								EseHelper.MakeLogfileName(this.m_config.LogFilePrefix, this.m_config.LogFileSuffix, fromNumber),
								num3
							});
							this.m_setBroken.RestartInstanceSoon(true);
							return false;
						}
						this.m_sourceLogCorruptEx = ex;
						exForBroken = ex;
						this.SetBrokenDueToSource(ex, true);
						return false;
					}
				}
				else if (e is FileIOonSourceException)
				{
					e = e.InnerException;
					exForBroken = e;
				}
			}
			if (!flag && (e is ArgumentException || e is PathTooLongException || e is ObjectDisposedException || e is OperationCanceledException || e is OperationAbortedException || e is NotSupportedException || e is AcllFailedException))
			{
				flag = true;
			}
			if (!flag)
			{
				if (this.m_failureOnSource)
				{
					ReplayEventLogConstants.Tuple_LogCopierReceivedSourceSideError.LogEvent(this.m_config.Identity, new object[]
					{
						this.m_config.DatabaseName,
						this.m_config.SourceMachine,
						e.Message
					});
				}
				this.m_totalWait += this.m_lastWait;
				int num4 = this.m_failureOnSource ? (RegistryParameters.LogCopierStalledToFailedThresholdInSecs * 1000) : 30000;
				if (this.m_totalWait < num4)
				{
					this.ShortRetryPolicy();
					return flag;
				}
				this.TraceError("Retry time expired", new object[0]);
				flag = true;
				if (!this.m_failureOnSource)
				{
					IOException ex2 = e as IOException;
					if (ex2 != null && FileOperations.IsDiskFullException(ex2))
					{
						ReplayEventLogConstants.Tuple_LogCopierBlockedByFullDisk.LogEvent(this.m_config.Identity, new object[]
						{
							this.m_config.DatabaseName,
							this.m_to
						});
					}
				}
			}
			this.TraceError("LogCopier.HandleWorkerExceptionInternal returning '{0}' for exception: {1}", new object[]
			{
				flag,
				e
			});
			return flag;
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x0009DD40 File Offset: 0x0009BF40
		private void SetBrokenDueToSource(Exception e, bool passiveNeedsReseed)
		{
			if (e is FileNotFoundException)
			{
				this.m_setBroken.SetBroken(FailureTag.Reseed, ReplayEventLogConstants.Tuple_LogFileGapFound, e, new string[]
				{
					((FileNotFoundException)e).FileName
				});
				return;
			}
			this.m_setBroken.SetBroken(passiveNeedsReseed ? FailureTag.Reseed : FailureTag.NoOp, ReplayEventLogConstants.Tuple_LogCopierFailedDueToSource, e, new string[]
			{
				this.m_config.SourceMachine,
				e.GetMessageWithHResult()
			});
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x0009DDB8 File Offset: 0x0009BFB8
		private void SetBrokenDueToTarget(Exception e)
		{
			FailureTag failureTag = FailureTag.NoOp;
			IOException ex = e as IOException;
			if (ex != null)
			{
				if (FileOperations.IsDiskFullException(ex))
				{
					failureTag = FailureTag.Space;
				}
				else if (ex is DirectoryNotFoundException || ex is FileNotFoundException || ex is EndOfStreamException || ex is DriveNotFoundException || ex is PathTooLongException || ex is FileLoadException)
				{
					failureTag = FailureTag.AlertOnly;
				}
			}
			else if (e is SecurityException || e is UnauthorizedAccessException)
			{
				failureTag = FailureTag.AlertOnly;
			}
			this.m_setBroken.SetBroken(failureTag, ReplayEventLogConstants.Tuple_LogCopierFailedDueToTarget, e, new string[]
			{
				e.GetMessageWithHResult()
			});
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x0009E000 File Offset: 0x0009C200
		private void WorkerEntryPoint()
		{
			this.m_workerIsPreparingToExit = false;
			if (this.m_prepareToStopCalled)
			{
				this.TraceDebug("WorkerEntryPoint(): Bailing because PrepareToStop has been called", new object[0]);
				return;
			}
			this.DisableWakeUp();
			if (this.m_passiveBlockMode.IsBlockModeActive)
			{
				this.TraceDebug("WorkerEntryPoint: exits because BlockMode is active", new object[0]);
				return;
			}
			if (this.m_setBroken.IsBroken)
			{
				this.TraceDebug("WorkerEntryPoint(): Bailing because SetBroken() has been called", new object[0]);
				return;
			}
			if (this.RunningAcll)
			{
				this.TraceDebug("WorkerEntryPoint(): Bailing because ACLL is going to run.", new object[0]);
				return;
			}
			if (this.m_disconnectedForTest)
			{
				this.TraceDebug("WorkerEntryPoint(): Bailing because m_disconnectedForTest is set", new object[0]);
				return;
			}
			this.m_nextWait = RegistryParameters.LogShipTimeoutInMsec;
			Exception ex = this.InvokeWithCatch(delegate
			{
				if (!this.m_initialized)
				{
					this.EstablishConnection();
					this.Initialize();
					this.m_initialized = true;
					this.SendLogRequest(true, false);
					this.GetResponse();
				}
				else if (this.m_readIsActive)
				{
					if (this.m_readCompleteEvent.WaitOne(0, false))
					{
						this.m_readIsActive = false;
						this.m_waitingForHealthCheck = false;
						if (this.m_readCallbackException != null)
						{
							Exception readCallbackException = this.m_readCallbackException;
							this.m_readCallbackException = null;
							throw readCallbackException;
						}
						this.GetResponse();
					}
					else
					{
						this.m_responseTimer.Stop();
						long elapsedMilliseconds = this.m_responseTimer.ElapsedMilliseconds;
						if (this.m_waitingForHealthCheck)
						{
							this.m_waitingForHealthCheck = false;
							LogCopier.Tracer.TraceError<long>((long)this.GetHashCode(), "Timeout after {0} ms", elapsedMilliseconds);
							throw new NetworkTimeoutException(this.m_config.SourceMachine, ReplayStrings.NetworkReadTimeout((int)(elapsedMilliseconds / 1000L)));
						}
						this.TraceDebug("Log Copy has been idle for {0} ms. Requesting an immediate reponse from the source.", new object[]
						{
							elapsedMilliseconds
						});
						this.SendLogRequest(false, false);
						this.m_waitingForHealthCheck = true;
						return;
					}
				}
				else if (this.m_incomingMessage == null)
				{
					this.TraceError("WorkEntryPoint has no input. This should be an error retry.", new object[0]);
					bool sendInitialRequest = this.EstablishConnection();
					this.SendLogRequest(sendInitialRequest, false);
					this.GetResponse();
				}
				this.ProcessMessage();
				this.TestDelaySleep();
				if (this.RunningAcll)
				{
					this.TraceDebug("Exiting WorkEntryPoint because ACLL is active", new object[0]);
					return;
				}
				if (this.UsePullModel())
				{
					this.TraceDebug("pull model, next log will be 0x{0:X}", new object[]
					{
						this.FromNumber
					});
					this.SendLogRequest(true, false);
				}
				this.m_workerIsPreparingToExit = true;
				if (!this.PreparingToEnterBlockMode)
				{
					this.StartRead();
				}
			});
			if (ex != null)
			{
				this.HandleWorkerException(ex);
			}
			this.ScheduleWakeUp();
			if (this.m_workerIsScheduled)
			{
				this.TraceDebug("WorkerEntryPoint exits and a wakeup scheduled in {0}mSec", new object[]
				{
					this.m_nextWait
				});
				return;
			}
			this.TraceError("WorkerEntryPoint exits with no work scheduled", new object[0]);
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x0009E113 File Offset: 0x0009C313
		private bool UsePullModel()
		{
			return RegistryParameters.LogCopyPull != 0;
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x0009E120 File Offset: 0x0009C320
		private void DeclareServerIsDownLevel()
		{
			this.m_serverIsDownlevel = true;
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x0009E129 File Offset: 0x0009C329
		private void SendLogRequest(bool sendInitialRequest, bool fRunningAcll = false)
		{
			if (this.m_serverIsDownlevel)
			{
				this.SendV1LogRequest(fRunningAcll);
				return;
			}
			if (sendInitialRequest || this.m_resetAfterError)
			{
				this.SendInitialRequest(fRunningAcll);
				this.m_resetAfterError = false;
				return;
			}
			this.SendPing();
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x0009E15C File Offset: 0x0009C35C
		private void SendInitialRequest(bool fRunningAcll = false)
		{
			ContinuousLogCopyRequest2.Flags flags = ContinuousLogCopyRequest2.Flags.None;
			if (fRunningAcll)
			{
				flags |= ContinuousLogCopyRequest2.Flags.ForAcll;
				this.TraceDebug("SendLogRequest with ForAcll flag", new object[0]);
			}
			else if (GranularReplication.IsEnabled())
			{
				flags |= ContinuousLogCopyRequest2.Flags.UseGranular;
				this.TraceDebug("SendLogRequest with IsGranularReplicationEnabled flag", new object[0]);
			}
			ContinuousLogCopyRequest2 continuousLogCopyRequest = new ContinuousLogCopyRequest2(this.LocalNodeName, this.Channel, this.DatabaseGuid, this.FromNumber, flags);
			if (this.UsePullModel())
			{
				continuousLogCopyRequest.LastGeneration = this.FromNumber;
			}
			this.m_responseIsBeingTimed = true;
			this.m_responseTimer.Reset();
			this.m_responseTimer.Start();
			continuousLogCopyRequest.Send();
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x0009E1FC File Offset: 0x0009C3FC
		private void SendV1LogRequest(bool fRunningAcll = false)
		{
			ContinuousLogCopyRequest.Flags flags = ContinuousLogCopyRequest.Flags.None;
			if (fRunningAcll)
			{
				flags |= ContinuousLogCopyRequest.Flags.ForAcll;
				this.TraceDebug("SendLogRequest with ForAcll flag", new object[0]);
			}
			long lastLogNum = 0L;
			if (this.UsePullModel())
			{
				lastLogNum = this.FromNumber;
			}
			ContinuousLogCopyRequest continuousLogCopyRequest = new ContinuousLogCopyRequest(this.Channel, this.DatabaseGuid, this.FromNumber, lastLogNum, flags);
			this.m_responseIsBeingTimed = true;
			this.m_responseTimer.Reset();
			this.m_responseTimer.Start();
			continuousLogCopyRequest.Send();
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x0009E274 File Offset: 0x0009C474
		private void GetResponse()
		{
			this.m_incomingMessage = this.Channel.GetMessage();
			this.m_responseTimer.Stop();
			long elapsedMilliseconds = this.m_responseTimer.ElapsedMilliseconds;
			if (this.m_responseIsBeingTimed)
			{
				ExTraceGlobals.LogCopierTracer.TraceDebug<long>((long)this.GetHashCode(), "Log Copy Response took: {0} ms", elapsedMilliseconds);
				this.m_responseIsBeingTimed = false;
				return;
			}
			this.TraceDebug("Copier was idle for : {0} ms", new object[]
			{
				elapsedMilliseconds
			});
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x0009E2EC File Offset: 0x0009C4EC
		private void ReportHealthy()
		{
			this.m_totalWait = 0;
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x0009E2F8 File Offset: 0x0009C4F8
		private NetworkChannelMessage ProcessMessage()
		{
			NetworkChannelMessage networkChannelMessage = null;
			if (!this.m_prepareToStopCalled && !this.m_setBroken.IsBroken)
			{
				if (this.RunningAcll && !this.m_fAcllHasControl)
				{
					this.TraceDebug("ProcessMessage() exiting because ACLL has been requested on another thread. Control should transfer to ACLL.", new object[0]);
				}
				else
				{
					if (this.m_incomingMessage == null)
					{
						this.m_incomingMessage = this.Channel.GetMessage();
					}
					networkChannelMessage = this.m_incomingMessage;
					this.m_incomingMessage = null;
					CopyLogReply copyLogReply = networkChannelMessage as CopyLogReply;
					if (copyLogReply != null)
					{
						this.m_perfmonCounters.GranularReplication = 0L;
						if (copyLogReply.ThisLogGeneration != this.FromNumber)
						{
							if (copyLogReply.ThisLogGeneration != 0L || !this.RunningAcll)
							{
								string text = string.Format("Unexpected log received: 0x{0:X}, expected: 0x{1:X}", copyLogReply.ThisLogGeneration, this.FromNumber);
								this.TraceError(text, new object[0]);
								throw new NetworkUnexpectedMessageException(this.Channel.PartnerNodeName, text);
							}
							this.TraceDebug("e00 is incoming", new object[0]);
						}
						this.ReceiveLogFile(copyLogReply);
						if (copyLogReply.ThisLogGeneration == 0L)
						{
							this.m_acllSuccess = true;
							return networkChannelMessage;
						}
						this.m_setGeneration.ClearLogStreamStartGeneration();
					}
					else if (networkChannelMessage is EnterBlockModeMsg)
					{
						this.PrepareToEnterBlockMode(networkChannelMessage as EnterBlockModeMsg);
					}
					else
					{
						NotifyEndOfLogReply notifyEndOfLogReply = networkChannelMessage as NotifyEndOfLogReply;
						if (notifyEndOfLogReply != null)
						{
							this.m_setGeneration.SetCopyNotificationGeneration(notifyEndOfLogReply.EndOfLogGeneration, notifyEndOfLogReply.EndOfLogUtc);
							this.TrackLastContactTime(notifyEndOfLogReply.MessageUtc);
							this.TraceDebug("We are in sync with a V1 server. Target waiting for 0x{0:x} Source has 0x{1:x}", new object[]
							{
								this.FromNumber,
								notifyEndOfLogReply.EndOfLogGeneration
							});
						}
						else
						{
							this.Channel.ThrowUnexpectedMessage(networkChannelMessage);
						}
					}
					this.ReportHealthy();
				}
			}
			return networkChannelMessage;
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x0009E4B8 File Offset: 0x0009C6B8
		private void StartRead()
		{
			this.TraceDebug("Starting async read", new object[0]);
			if (this.m_readCompleteEvent == null)
			{
				this.m_readCompleteEvent = new ManualResetEvent(true);
			}
			this.m_responseTimer.Reset();
			this.m_responseTimer.Start();
			this.m_readIsActive = true;
			this.m_readCompleteEvent.Reset();
			bool flag = false;
			try
			{
				this.Channel.StartRead(new NetworkChannelCallback(LogCopier.ReadCallback), this);
				flag = true;
				this.m_nextWait = this.m_copyClient.DefaultTimeoutInMs;
			}
			finally
			{
				if (!flag)
				{
					this.TraceError("Failed to starting async read", new object[0]);
					this.m_readCompleteEvent.Set();
					this.m_readIsActive = false;
				}
			}
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x0009E57C File Offset: 0x0009C77C
		private void ProcessReadCallback(int bytesAvailable, Exception e)
		{
			this.TraceDebug("ProcessReadCallback. bytesAvailable={0},ex={1}", new object[]
			{
				bytesAvailable,
				e
			});
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			try
			{
				flag3 = Monitor.TryEnter(this.m_globalWorkerLock);
				if (!flag3 && this.m_workerIsPreparingToExit && !this.m_connectionIsBeingDiscarded)
				{
					flag2 = true;
					this.TraceDebug("ReadCallback will need to wait for the lock and do work", new object[0]);
				}
				if (e != null)
				{
					this.m_readCallbackException = new NetworkCommunicationException(this.m_config.SourceMachine, e.Message, e);
				}
				else if (bytesAvailable == 0)
				{
					this.m_readCallbackException = new NetworkEndOfDataException(this.m_config.SourceMachine, ReplayStrings.NetworkReadEOF);
				}
				this.m_readCompleteEvent.Set();
				flag = true;
				if (flag2)
				{
					Monitor.Enter(this.m_globalWorkerLock);
					flag3 = true;
				}
				if (flag3)
				{
					this.WorkerEntryPoint();
				}
			}
			finally
			{
				if (flag3)
				{
					Monitor.Exit(this.m_globalWorkerLock);
				}
				if (!flag)
				{
					this.m_readCompleteEvent.Set();
				}
			}
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x0009E680 File Offset: 0x0009C880
		private void WaitForReadCallback()
		{
			if (this.m_copyClient != null)
			{
				int defaultTimeoutInMs = this.m_copyClient.DefaultTimeoutInMs;
				this.WaitForReadCallback(false, defaultTimeoutInMs);
			}
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x0009E6AC File Offset: 0x0009C8AC
		private void WaitForReadCallback(bool waitForever, int msTimeout)
		{
			if (this.m_readCompleteEvent != null)
			{
				bool flag = false;
				while (!this.m_readCompleteEvent.WaitOne(msTimeout, false))
				{
					this.TraceError("Timeout waiting for ReadCallback", new object[0]);
					if (!waitForever)
					{
						this.Channel.ThrowTimeoutException(ReplayStrings.NetworkReadTimeout(msTimeout / 1000));
					}
					if (flag)
					{
						string message = string.Format("LogCopier({0}).WaitForReadCallback timeout after {1} sec", this.DatabaseName, msTimeout / 1000);
						LogCopier.Tracer.TraceDebug((long)this.GetHashCode(), message);
						TimeoutException exception = new TimeoutException(message);
						ExWatson.SendReportAndCrashOnAnotherThread(exception);
						DiagCore.RetailAssert(false, "Crash request must not return", new object[0]);
						break;
					}
					flag = true;
					this.CancelConnection();
					msTimeout = RegistryParameters.LogCopierHungIoLimitInMsec;
				}
				this.TraceDebug("WaitForReadCallback is successful, marking read as not active", new object[0]);
				this.m_readIsActive = false;
			}
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x0009E784 File Offset: 0x0009C984
		private void ProcessWakeUp()
		{
			if (Monitor.TryEnter(this.m_globalWorkerLock))
			{
				try
				{
					this.TraceDebug("ProcessWakeUp is running as the worker", new object[0]);
					if (this.PreparingToEnterBlockMode)
					{
						this.TraceError("Timed out waiting to enter blockMode", new object[0]);
						this.PreparingToEnterBlockMode = false;
					}
					if (!this.m_stalledDueToSourceLogBreak)
					{
						this.WorkerEntryPoint();
						return;
					}
					TimeSpan timeSpan = DateTime.UtcNow.Subtract(this.m_stalledSince.Value);
					string arg = timeSpan.ToString();
					LogCopier.Tracer.TraceError<string, string>((long)this.GetHashCode(), "{0}:Stalled for {1}", this.DatabaseName, arg);
					if (RegistryParameters.LogCopierStalledToFailedThresholdInSecs > 0 && timeSpan.TotalSeconds > (double)RegistryParameters.LogCopierStalledToFailedThresholdInSecs)
					{
						this.DisableWakeUp();
						this.m_setBroken.SetBroken(FailureTag.Reseed, ReplayEventLogConstants.Tuple_LogCopierIsStalledDueToSource, this.m_sourceLogCorruptEx, new string[]
						{
							this.m_config.SourceMachine,
							this.m_sourceLogCorruptEx.Message
						});
						return;
					}
					this.ScheduleWakeUp();
					return;
				}
				finally
				{
					Monitor.Exit(this.m_globalWorkerLock);
				}
			}
			this.TraceDebug("ProcessWakeUp found the worker busy", new object[0]);
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0009E8BC File Offset: 0x0009CABC
		private void DisableWakeUp()
		{
			this.m_workerIsScheduled = false;
			if (this.m_wakeTimer != null)
			{
				this.m_wakeTimer.Change(-1, -1);
			}
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x0009E8DC File Offset: 0x0009CADC
		private void ScheduleWakeUp()
		{
			if (this.m_wakeTimer != null && !this.m_prepareToStopCalled && !this.m_setBroken.IsBroken && !this.RunningAcll)
			{
				this.m_lastWait = this.m_nextWait;
				this.m_wakeTimer.Change(this.m_nextWait, this.m_nextWait + 10000);
				this.m_workerIsScheduled = true;
				return;
			}
			this.TraceDebug("ScheduleWakeUp is disabled", new object[0]);
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x0009E953 File Offset: 0x0009CB53
		public void Start()
		{
			this.m_wakeTimer = new Timer(new TimerCallback(LogCopier.WakeUpCallback), this, 0, -1);
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x0009E970 File Offset: 0x0009CB70
		public void PrepareToStop()
		{
			this.TraceDebug("PrepareToStop invoked.", new object[0]);
			lock (this)
			{
				if (!this.m_prepareToStopCalled)
				{
					this.m_prepareToStopCalled = true;
					this.CancelConnection();
					this.TerminateBlockMode();
				}
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x060021FE RID: 8702 RVA: 0x0009E9D8 File Offset: 0x0009CBD8
		private bool PrepareToStopCalled
		{
			get
			{
				return this.m_prepareToStopCalled;
			}
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x0009E9E4 File Offset: 0x0009CBE4
		public void Stop()
		{
			this.TraceDebug("Stop invoked", new object[0]);
			bool flag = false;
			lock (this)
			{
				if (!this.m_stopped)
				{
					if (!this.m_prepareToStopCalled)
					{
						this.PrepareToStop();
					}
					this.m_stopped = true;
					flag = true;
				}
			}
			if (flag)
			{
				bool flag3 = false;
				try
				{
					Monitor.Enter(this.m_globalWorkerLock);
					flag3 = true;
					this.m_workerIsPreparingToExit = false;
					this.DisableWakeUp();
					int logShipTimeoutMs = this.GetLogShipTimeoutMs();
					this.WaitForReadCallback(true, logShipTimeoutMs);
					this.CleanupOldConnection(logShipTimeoutMs);
				}
				finally
				{
					if (this.m_wakeTimer != null)
					{
						this.m_wakeTimer.Dispose();
					}
					if (flag3)
					{
						Monitor.Exit(this.m_globalWorkerLock);
					}
				}
				this.TerminateBlockMode();
				this.m_passiveBlockMode.Destroy();
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x0009EAC8 File Offset: 0x0009CCC8
		private bool RunningAcll
		{
			get
			{
				return this.m_fAttemptFinalCopyCalled;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x0009EAD0 File Offset: 0x0009CCD0
		// (set) Token: 0x06002202 RID: 8706 RVA: 0x0009EAD8 File Offset: 0x0009CCD8
		public bool GranuleUsedAsE00 { get; private set; }

		// Token: 0x06002203 RID: 8707 RVA: 0x0009EAE4 File Offset: 0x0009CCE4
		public Result AttemptFinalCopy(AcllPerformanceTracker acllPerf, out LocalizedString errorString)
		{
			LogCopier.Tracer.TraceDebug((long)this.GetHashCode(), "AttemptFinalCopy called");
			ExTraceGlobals.PFDTracer.TracePfd<int>((long)this.GetHashCode(), "PFD CRS {0} AttemptFinalCopy called", 23003);
			errorString = LocalizedString.Empty;
			Result result = Result.GiveUp;
			this.RunAcll(acllPerf);
			bool flag = true;
			if (this.IsBroken)
			{
				errorString = this.m_setBroken.ErrorMessage;
			}
			else if (this.IsDisconnected)
			{
				errorString = this.m_setDisconnected.ErrorMessage;
			}
			else if (this.PrepareToStopCalled)
			{
				errorString = ReplayStrings.PrepareToStopCalled;
				flag = false;
			}
			else if (!this.m_acllSuccess)
			{
				errorString = new LocalizedString(this.m_lastException.Message);
			}
			else
			{
				result = Result.Success;
				flag = false;
			}
			if (flag && this.m_passiveBlockMode.UsePartialLogsDuringAcll(this.FromNumber))
			{
				this.GranuleUsedAsE00 = true;
			}
			return result;
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x0009EBC7 File Offset: 0x0009CDC7
		private bool IsBroken
		{
			get
			{
				return this.m_setBroken.IsBroken;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002205 RID: 8709 RVA: 0x0009EBD4 File Offset: 0x0009CDD4
		private bool IsDisconnected
		{
			get
			{
				return this.m_setDisconnected.IsDisconnected;
			}
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x0009EC5C File Offset: 0x0009CE5C
		private void RunAcll(AcllPerformanceTracker acllPerf)
		{
			this.m_fAttemptFinalCopyCalled = true;
			this.m_fAcllHasControl = false;
			bool held = false;
			try
			{
				acllPerf.RunTimedOperation(AcllTimedOperation.AcllEnterLogCopierWorkerLock, delegate
				{
					if (Monitor.TryEnter(this.m_globalWorkerLock, RegistryParameters.LogShipACLLTimeoutInMsec))
					{
						held = true;
						return;
					}
					LogCopier.Tracer.TraceDebug((long)this.GetHashCode(), "we failed to gain control over the logCopier");
					acllPerf.IsAcllCouldNotControlLogCopier = true;
					ReplayCrimsonEvents.AcllCouldNotControlCopier.Log<string, AmServerName>(this.DatabaseName, AmServerName.LocalComputerName);
				});
				if (!held || this.m_disconnectedForTest)
				{
					NetworkTimeoutException ex = new NetworkTimeoutException(this.m_config.SourceMachine, ReplayStrings.NetworkConnectionTimeout(this.LogShipACLLTimeoutInSecs));
					if (this.m_lastException == null)
					{
						this.m_lastException = ex;
					}
					this.m_setDisconnected.SetDisconnected(FailureTag.NoOp, ReplayEventLogConstants.Tuple_LogCopierFailedToCommunicate, new string[]
					{
						this.m_config.SourceMachine,
						ex.Message
					});
				}
				else
				{
					this.m_workerIsPreparingToExit = false;
					this.m_fAcllHasControl = true;
					if (this.PrepareForAcll(acllPerf))
					{
						this.ReadAcllMessages(acllPerf);
					}
				}
			}
			finally
			{
				if (held)
				{
					this.m_fAcllHasControl = false;
					Monitor.Exit(this.m_globalWorkerLock);
				}
			}
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x0009EF48 File Offset: 0x0009D148
		private bool PrepareForAcll(AcllPerformanceTracker acllPerf)
		{
			bool success = false;
			Exception ex = this.InvokeWithCatch(delegate
			{
				this.DisableWakeUp();
				if (this.m_stalledDueToSourceLogBreak)
				{
					this.SetBrokenDueToSource(this.m_lastException, false);
					return;
				}
				ExTraceGlobals.FaultInjectionTracer.TraceTest(4167445821U);
				int timeoutInMsec = this.GetLogShipTimeoutMs();
				bool isBlockModeActive = this.m_passiveBlockMode.IsBlockModeActive;
				if (isBlockModeActive)
				{
					this.m_passiveBlockMode.PrepareForAcll();
				}
				if (this.m_connectionIsBeingDiscarded)
				{
					if (!isBlockModeActive)
					{
						ReplayCrimsonEvents.AcllFoundDeadConnection.Log<string, AmServerName>(this.DatabaseName, AmServerName.LocalComputerName);
					}
					this.CleanupOldConnection(timeoutInMsec);
				}
				if (this.m_copyClient != null)
				{
					this.m_copyClient.SetTimeoutInMsec(this.GetLogShipTimeoutMs());
				}
				this.EstablishConnection();
				acllPerf.IsLogCopierInitializedForAcll = this.m_initialized;
				if (!this.m_initialized)
				{
					acllPerf.RunTimedOperation(AcllTimedOperation.AcllLogCopierFirstInit, delegate
					{
						this.Initialize();
					});
					this.m_initialized = true;
				}
				if (isBlockModeActive)
				{
					this.m_passiveBlockMode.FinishForAcll(timeoutInMsec);
				}
				this.SendLogRequest(true, true);
				acllPerf.RunTimedOperation(AcllTimedOperation.AcllInitWaitForReadCallback, delegate
				{
					this.WaitForReadCallback(false, timeoutInMsec);
				});
				success = true;
			});
			if (ex != null)
			{
				LogCopier.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "PrepareForAcll({0}) failed: {1}", this.DatabaseName, ex);
				this.HandleWorkerException(ex);
			}
			return success;
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x0009F05C File Offset: 0x0009D25C
		private void ReadAcllMessages(AcllPerformanceTracker acllPerf)
		{
			LogCopier.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ReadAcllMessages({0}) invoked", this.DatabaseName);
			Exception ex = this.InvokeWithCatch(delegate
			{
				if (this.m_incomingMessage == null)
				{
					this.GetResponse();
				}
				for (;;)
				{
					NetworkChannelMessage networkChannelMessage = this.ProcessMessage();
					if (this.m_acllSuccess)
					{
						break;
					}
					if (networkChannelMessage == null)
					{
						goto Block_3;
					}
					if (!(networkChannelMessage is CopyLogReply) && !(networkChannelMessage is NotifyEndOfLogReply) && !(networkChannelMessage is EnterBlockModeMsg))
					{
						this.Channel.ThrowUnexpectedMessage(networkChannelMessage);
					}
					this.TestDelaySleep();
					if (this.UsePullModel())
					{
						this.TraceDebug("pull model, next log will be 0x{0:X}", new object[]
						{
							this.FromNumber
						});
						this.SendLogRequest(true, true);
					}
				}
				return;
				Block_3:
				LogCopier.Tracer.TraceError<string>((long)this.GetHashCode(), "ReadAcllMessages({0}) No message. Abnormal exit", this.DatabaseName);
			});
			if (ex != null)
			{
				LogCopier.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "PrepareForAcll({0}) failed: {1}", this.DatabaseName, ex);
				this.HandleWorkerException(ex);
			}
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x0009F0BF File Offset: 0x0009D2BF
		private int GetLogShipTimeoutMs()
		{
			return LogSource.GetLogShipTimeoutInMsec(this.RunningAcll || this.m_fConstructedForAcll);
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x0009F0D8 File Offset: 0x0009D2D8
		private void SendPing()
		{
			this.m_lastSentPingCounter = Win32StopWatch.GetSystemPerformanceCounter();
			PingMessage pingMessage = new PingMessage(this.Channel);
			pingMessage.RequestAckCounter = this.m_lastSentPingCounter;
			LogCopier.Tracer.TraceDebug<long>((long)this.GetHashCode(), "SendPing: expecting ack with 0x{0:X}", this.m_lastSentPingCounter);
			pingMessage.Send();
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x0009F12A File Offset: 0x0009D32A
		private void TerminateBlockMode()
		{
			this.m_passiveBlockMode.Terminate();
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x0009F137 File Offset: 0x0009D337
		// (set) Token: 0x0600220D RID: 8717 RVA: 0x0009F13F File Offset: 0x0009D33F
		private bool PreparingToEnterBlockMode { get; set; }

		// Token: 0x0600220E RID: 8718 RVA: 0x0009F148 File Offset: 0x0009D348
		private void PrepareToEnterBlockMode(EnterBlockModeMsg msg)
		{
			LogCopier.Tracer.TraceDebug<string>((long)this.GetHashCode(), "PrepareToEnterBlockMode({0}) received", this.DatabaseName);
			if (msg.FirstGenerationToExpect != this.FromNumber)
			{
				string text = string.Format("PrepareToEnterBlockMode({0}) failed. ExpectedGen(0x{1:X}) ServerWillSend(0x{2:X})", this.DatabaseName, this.FromNumber, msg.FirstGenerationToExpect);
				LogCopier.Tracer.TraceError((long)this.GetHashCode(), text);
				throw new NetworkUnexpectedMessageException(this.Channel.PartnerNodeName, text);
			}
			if (this.RunningAcll)
			{
				LogCopier.Tracer.TraceError((long)this.GetHashCode(), "Rejecting blockmode due to ACLL");
				msg.FlagsUsed = EnterBlockModeMsg.Flags.PassiveReject;
				msg.Send();
				return;
			}
			msg.FlagsUsed = EnterBlockModeMsg.Flags.PassiveIsReady;
			msg.Send();
			this.PreparingToEnterBlockMode = true;
			this.m_nextWait = 3 * RegistryParameters.LogShipTimeoutInMsec;
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x0009F21C File Offset: 0x0009D41C
		public static void EnterBlockMode(EnterBlockModeMsg msg, NetworkChannel channel)
		{
			LogCopier logCopier = LogCopier.FindCopier(channel.LocalNodeName, msg.DatabaseGuid, false);
			if (logCopier == null)
			{
				LogCopier.Tracer.TraceError<string>(0L, "EnterBlockMode fails because copier doesn't exist. Active={0}", msg.ActiveNodeName);
				return;
			}
			logCopier.EnterBlockModeInternal(msg, channel);
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x0009F260 File Offset: 0x0009D460
		private void EnterBlockModeInternal(EnterBlockModeMsg msg, NetworkChannel channel)
		{
			if (this.RunningAcll)
			{
				LogCopier.Tracer.TraceError<string>((long)this.GetHashCode(), "EnterBlockMode({0}) rejected due to ACLL", this.DatabaseName);
				return;
			}
			if (Monitor.TryEnter(this.m_globalWorkerLock, RegistryParameters.LogShipACLLTimeoutInMsec))
			{
				try
				{
					if (!this.PreparingToEnterBlockMode)
					{
						LogCopier.Tracer.TraceError((long)this.GetHashCode(), "EnterBlockMode fails because Preparing was cancelled");
						return;
					}
					if (this.m_prepareToStopCalled)
					{
						LogCopier.Tracer.TraceError((long)this.GetHashCode(), "EnterBlockMode fails because we are stopping.");
						return;
					}
					if (this.m_passiveBlockMode.EnterBlockMode(msg, channel, this.GetMaxBlockModeDepthInBytes()))
					{
						this.DisableWakeUp();
						this.DiscardConnection();
					}
					return;
				}
				finally
				{
					this.PreparingToEnterBlockMode = false;
					Monitor.Exit(this.m_globalWorkerLock);
				}
			}
			LogCopier.Tracer.TraceError<string>((long)this.GetHashCode(), "EnterBlockMode({0}) failed to obtain worker lock", this.DatabaseName);
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x0009F348 File Offset: 0x0009D548
		public void NotifyBlockModeTermination()
		{
			if (this.RunningAcll)
			{
				LogCopier.Tracer.TraceError<string>((long)this.GetHashCode(), "NotifyBlockModeTermination({0}) found ACLL active.", this.DatabaseName);
				return;
			}
			if (Monitor.TryEnter(this.m_globalWorkerLock, RegistryParameters.LogShipTimeoutInMsec))
			{
				try
				{
					this.m_nextWait = 0;
					this.ScheduleWakeUp();
					return;
				}
				finally
				{
					Monitor.Exit(this.m_globalWorkerLock);
				}
			}
			LogCopier.Tracer.TraceError<string>((long)this.GetHashCode(), "NotifyBlockModeTermination({0}) failed to obtain worker lock", this.DatabaseName);
			ReplayCrimsonEvents.BlockModeTerminationLockConflict.Log<string, TimeSpan>(this.DatabaseName, TimeSpan.FromMilliseconds((double)RegistryParameters.LogShipTimeoutInMsec));
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x0009F3F0 File Offset: 0x0009D5F0
		// (set) Token: 0x06002213 RID: 8723 RVA: 0x0009F3F8 File Offset: 0x0009D5F8
		public bool TestHungPassiveBlockMode { get; private set; }

		// Token: 0x06002214 RID: 8724 RVA: 0x0009F404 File Offset: 0x0009D604
		public static void SetCopyProperty(Guid dbGuid, string propName, string propVal)
		{
			LogCopier logCopier = LogCopier.FindCopier(null, dbGuid, false);
			if (logCopier == null)
			{
				throw new ArgumentException(string.Format("LogCopier for database '{0}' not active", dbGuid));
			}
			if (!SharedHelper.StringIEquals(propName, "TestHungPassiveBlockMode"))
			{
				throw new ArgumentException(string.Format("'{0}' is not recognized", propName));
			}
			bool flag;
			if (!bool.TryParse(propVal, out flag))
			{
				throw new ArgumentException("TestHungPassiveBlockMode must be a bool");
			}
			logCopier.TestHungPassiveBlockMode = flag;
			LogCopier.Tracer.TraceError<string, bool>((long)logCopier.GetHashCode(), "TestHungPassiveBlockMode({0}) set to {1}", logCopier.DatabaseName, flag);
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x0009F48C File Offset: 0x0009D68C
		private void TestDisconnect()
		{
			bool flag = false;
			LogCopier.Tracer.TraceError((long)this.GetHashCode(), "TestDisconnect invoked, disconnecting");
			try
			{
				Monitor.Enter(this.m_globalWorkerLock);
				flag = true;
				this.m_disconnectedForTest = true;
				this.DisableWakeUp();
				this.DiscardConnection();
				this.m_passiveBlockMode.Terminate();
				this.DisableWakeUp();
				this.m_lastException = new NetworkCommunicationException(this.m_config.SourceMachine, "Copier stopped by test hook.");
				this.m_setDisconnected.SetDisconnected(FailureTag.NoOp, ReplayEventLogConstants.Tuple_LogCopierFailedToCommunicate, new string[]
				{
					this.m_config.SourceMachine,
					"Disconnected by test hook"
				});
				LogCopier.Tracer.TraceError((long)this.GetHashCode(), "TestDisconnect succeeded");
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this.m_globalWorkerLock);
				}
			}
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x0009F564 File Offset: 0x0009D764
		public static void TestConnectCopier(Guid dbGuid)
		{
			LogCopier logCopier = LogCopier.FindCopier(null, dbGuid, true);
			logCopier.TestConnect();
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x0009F580 File Offset: 0x0009D780
		private void TestConnect()
		{
			bool flag = false;
			LogCopier.Tracer.TraceError((long)this.GetHashCode(), "TestConnect invoked");
			Timer timer = null;
			try
			{
				Monitor.Enter(this.m_globalWorkerLock);
				flag = true;
				if (this.m_prepareToStopCalled)
				{
					throw new ReplayServiceUnknownReplicaInstanceException("FindCopier:Copier not active", this.m_config.Identity);
				}
				if (this.m_connectionIsBeingDiscarded)
				{
					LogCopier.Tracer.TraceDebug((long)this.GetHashCode(), "TestConnect cleaning up old connection");
					this.CleanupOldConnection(this.GetLogShipTimeoutMs());
				}
				this.m_disconnectedForTest = false;
				this.m_workerIsScheduled = true;
				timer = this.m_wakeTimer;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this.m_globalWorkerLock);
				}
			}
			LogCopier.Tracer.TraceError((long)this.GetHashCode(), "TestConnect succeeded");
			timer.Change(0, 10000);
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x0009F658 File Offset: 0x0009D858
		public static void TestIgnoreGranularCompletions(Guid dbGuid)
		{
		}

		// Token: 0x04000DEE RID: 3566
		private const int MaxTotalWait = 30000;

		// Token: 0x04000DEF RID: 3567
		protected const int ShortWait = 10000;

		// Token: 0x04000DF0 RID: 3568
		protected const int LongWait = 30000;

		// Token: 0x04000DF1 RID: 3569
		private readonly string m_fromPrefix;

		// Token: 0x04000DF2 RID: 3570
		private readonly string m_fromSuffix;

		// Token: 0x04000DF3 RID: 3571
		private readonly int LogShipACLLTimeoutInSecs = (int)Math.Ceiling((double)RegistryParameters.LogShipACLLTimeoutInMsec / 1000.0);

		// Token: 0x04000DF4 RID: 3572
		private Timer m_wakeTimer;

		// Token: 0x04000DF5 RID: 3573
		private bool m_workerIsScheduled;

		// Token: 0x04000DF6 RID: 3574
		private int m_lastWait;

		// Token: 0x04000DF7 RID: 3575
		private int m_nextWait;

		// Token: 0x04000DF8 RID: 3576
		private int m_totalWait;

		// Token: 0x04000DF9 RID: 3577
		protected Exception m_lastException;

		// Token: 0x04000DFA RID: 3578
		private bool m_failureOnSource;

		// Token: 0x04000DFB RID: 3579
		private bool m_failureOnNetwork;

		// Token: 0x04000DFC RID: 3580
		private bool m_resetAfterError;

		// Token: 0x04000DFD RID: 3581
		private long m_fromNumber;

		// Token: 0x04000DFE RID: 3582
		private IADServer m_srv;

		// Token: 0x04000DFF RID: 3583
		private ShipLogsSetBroken m_logCopierSetBroken;

		// Token: 0x04000E00 RID: 3584
		private ISetBroken m_setBroken;

		// Token: 0x04000E01 RID: 3585
		private ISetDisconnected m_setDisconnected;

		// Token: 0x04000E02 RID: 3586
		private ISetGeneration m_setGeneration;

		// Token: 0x04000E03 RID: 3587
		private NetworkPath m_initialNetPath;

		// Token: 0x04000E04 RID: 3588
		private LogCopyClient m_copyClient;

		// Token: 0x04000E05 RID: 3589
		private bool m_fConstructedForAcll;

		// Token: 0x04000E06 RID: 3590
		private bool m_connectionIsBeingDiscarded;

		// Token: 0x04000E07 RID: 3591
		private long m_lastSentPingCounter;

		// Token: 0x04000E08 RID: 3592
		private DateTime? m_stalledSince = null;

		// Token: 0x04000E09 RID: 3593
		private PassiveBlockMode m_passiveBlockMode;

		// Token: 0x04000E0A RID: 3594
		private readonly string m_to;

		// Token: 0x04000E0B RID: 3595
		private readonly IPerfmonCounters m_perfmonCounters;

		// Token: 0x04000E0C RID: 3596
		private readonly IReplayConfiguration m_config;

		// Token: 0x04000E0D RID: 3597
		private readonly FileState m_fileState;

		// Token: 0x04000E0E RID: 3598
		private bool m_initialized;

		// Token: 0x04000E0F RID: 3599
		private volatile bool m_prepareToStopCalled;

		// Token: 0x04000E10 RID: 3600
		private bool m_stopped;

		// Token: 0x04000E11 RID: 3601
		private bool m_stalledDueToSourceLogBreak;

		// Token: 0x04000E12 RID: 3602
		private Exception m_sourceLogCorruptEx;

		// Token: 0x04000E13 RID: 3603
		private NetworkChannelMessage m_incomingMessage;

		// Token: 0x04000E14 RID: 3604
		private ReplayStopwatch m_responseTimer = new ReplayStopwatch();

		// Token: 0x04000E15 RID: 3605
		private bool m_responseIsBeingTimed;

		// Token: 0x04000E16 RID: 3606
		private bool m_readIsActive;

		// Token: 0x04000E17 RID: 3607
		private Exception m_readCallbackException;

		// Token: 0x04000E18 RID: 3608
		private ManualResetEvent m_readCompleteEvent;

		// Token: 0x04000E19 RID: 3609
		private object m_globalWorkerLock = new object();

		// Token: 0x04000E1A RID: 3610
		private volatile bool m_workerIsPreparingToExit;

		// Token: 0x04000E1B RID: 3611
		private long m_highestGenReleasedToInspector;

		// Token: 0x04000E1C RID: 3612
		private static readonly ServerVersion FirstVersionSupportingQueryVersion = new ServerVersion(16, 0, 472, 0);

		// Token: 0x04000E1D RID: 3613
		private static readonly ServerVersion FirstVersionSupportingV2 = new ServerVersion(15, 0, 466, 0);

		// Token: 0x04000E1E RID: 3614
		private bool m_waitingForHealthCheck;

		// Token: 0x04000E1F RID: 3615
		private bool m_serverIsDownlevel;

		// Token: 0x04000E20 RID: 3616
		private bool m_fAttemptFinalCopyCalled;

		// Token: 0x04000E21 RID: 3617
		private bool m_fAcllHasControl;

		// Token: 0x04000E22 RID: 3618
		private bool m_acllSuccess;

		// Token: 0x04000E23 RID: 3619
		private bool m_disconnectedForTest;

		// Token: 0x02000343 RID: 835
		// (Invoke) Token: 0x0600221D RID: 8733
		private delegate void CatchableOperation();
	}
}
