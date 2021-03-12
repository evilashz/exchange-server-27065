using System;
using System.IO;
using System.Security;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Security.Compliance;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Unpublished;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200033D RID: 829
	internal class GranularWriter
	{
		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x0600218F RID: 8591 RVA: 0x0009B9C3 File Offset: 0x00099BC3
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.GranularWriterTracer;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06002190 RID: 8592 RVA: 0x0009B9CA File Offset: 0x00099BCA
		public static bool IsDebugTraceEnabled
		{
			get
			{
				return ExTraceGlobals.GranularWriterTracer.IsTraceEnabled(TraceType.DebugTrace);
			}
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x0009B9D8 File Offset: 0x00099BD8
		public static bool BytesAreIdentical(byte[] b1, byte[] b2)
		{
			if (b1 == null)
			{
				return b2 == null || b2.Length == 0;
			}
			if (b2 == null)
			{
				return b1.Length == 0;
			}
			if (b1.Length != b2.Length)
			{
				return false;
			}
			for (int i = 0; i < b1.Length; i++)
			{
				if (b1[i] != b2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x0009BA23 File Offset: 0x00099C23
		private void TrackInspectorGeneration(long gen, DateTime writeTimeUtc)
		{
			if (this.m_logCopier != null)
			{
				this.m_logCopier.TrackInspectorGeneration(gen, writeTimeUtc);
			}
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x0009BA3C File Offset: 0x00099C3C
		public GranularWriter(LogCopier logCopier, IPerfmonCounters perfmonCounters, IReplayConfiguration replayConfiguration, ISetBroken setBroken)
		{
			this.m_logCopier = logCopier;
			this.m_setBroken = setBroken;
			this.m_perfmonCounters = perfmonCounters;
			this.m_config = replayConfiguration;
			this.m_perfmonCounters.GranularReplication = 0L;
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06002194 RID: 8596 RVA: 0x0009BA8B File Offset: 0x00099C8B
		internal Guid DatabaseGuid
		{
			get
			{
				return this.m_config.IdentityGuid;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x0009BA98 File Offset: 0x00099C98
		internal string DatabaseName
		{
			get
			{
				return this.m_config.DatabaseName;
			}
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x0009BAA5 File Offset: 0x00099CA5
		private void SetSystemParameter(JET_param parameter, string value)
		{
			Api.JetSetSystemParameter(this.m_jetConsumer, JET_SESID.Nil, parameter, 0, value);
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x0009BABB File Offset: 0x00099CBB
		private void SetSystemParameter(JET_param parameter, int value)
		{
			Api.JetSetSystemParameter(this.m_jetConsumer, JET_SESID.Nil, parameter, value, null);
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x0009BAD4 File Offset: 0x00099CD4
		public void Initialize()
		{
			GranularWriter.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Initialize({0})", this.DatabaseName);
			this.m_lowestClosedGeneration = 0L;
			this.m_currentGeneration = 0L;
			this.m_currentGenerationState = GranularWriter.GranularLogState.Unknown;
			this.DiscardLogs();
			try
			{
				string name = this.m_config.DatabaseName + "_PassiveInBlockMode";
				Api.JetCreateInstance(out this.m_jetConsumer, name);
				string paramString = null;
				InstanceParameters instanceParameters = new InstanceParameters(this.m_jetConsumer);
				instanceParameters.BaseName = this.m_config.LogFilePrefix;
				instanceParameters.LogFileDirectory = this.m_config.LogInspectorPath;
				instanceParameters.SystemDirectory = this.m_config.DestinationSystemPath;
				instanceParameters.NoInformationEvent = true;
				instanceParameters.Recovery = false;
				instanceParameters.LogFileSize = 1024;
				instanceParameters.MaxTemporaryTables = 0;
				if (!RegistryParameters.DisableJetFailureItemPublish)
				{
					Api.JetSetSystemParameter(this.m_jetConsumer, JET_SESID.Nil, (JET_param)168, 1, paramString);
				}
				Api.JetInit(ref this.m_jetConsumer);
				this.m_jetConsumerInitialized = true;
				this.m_jetConsumerHealthy = true;
			}
			catch (EsentErrorException ex)
			{
				GranularWriter.Tracer.TraceError<string, EsentErrorException>((long)this.GetHashCode(), "Initialize({0}) failed:{1}", this.DatabaseName, ex);
				throw new GranularReplicationInitFailedException(ex.Message, ex);
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002199 RID: 8601 RVA: 0x0009BC14 File Offset: 0x00099E14
		public bool IsHealthy
		{
			get
			{
				return this.m_jetConsumerHealthy;
			}
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x0009BC1C File Offset: 0x00099E1C
		private string FormShortGranuleName(long gen)
		{
			return GranularReplication.FormPartialLogFileName(this.m_config.LogFilePrefix, gen);
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x0009BC2F File Offset: 0x00099E2F
		public string FormFullGranuleName(long gen)
		{
			return Path.Combine(this.m_config.LogInspectorPath, this.FormShortGranuleName(gen));
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x0009BC48 File Offset: 0x00099E48
		private string FormInspectorLogfileName(long generation)
		{
			return Path.Combine(this.m_config.LogInspectorPath, this.m_config.BuildShortLogfileName(generation));
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x0009BC68 File Offset: 0x00099E68
		public void DiscardLogs()
		{
			GranularWriter.Tracer.TraceDebug<string>((long)this.GetHashCode(), "DiscardLogs: {0}", this.m_config.LogInspectorPath);
			FailureTag failureTag = FailureTag.IoHard;
			Exception ex = null;
			string text = string.Empty;
			try
			{
				foreach (string text2 in Directory.GetFiles(this.m_config.LogInspectorPath, "*.jsl"))
				{
					text = text2;
					File.Delete(text2);
				}
			}
			catch (IOException ex2)
			{
				ex = ex2;
				failureTag = ReplicaInstance.IOExceptionToFailureTag(ex2);
			}
			catch (UnauthorizedAccessException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				GranularWriter.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "Deleting file '{0}' failed with exception {1}", text, ex);
				this.m_setBroken.SetBroken(failureTag, ReplayEventLogConstants.Tuple_CouldNotDeleteLogFile, ex, new string[]
				{
					text,
					ex.ToString()
				});
				throw new GranularReplicationTerminatedException(ex.Message, ex);
			}
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x0009BD60 File Offset: 0x00099F60
		private void ReleaseRangeOfClosedButUnfinalizedLogs(long lastToRelease, out long lowestGenReleased, out long highestGenReleased)
		{
			lowestGenReleased = 0L;
			highestGenReleased = 0L;
			while (this.m_lowestClosedGeneration <= lastToRelease)
			{
				string sourceFileName = this.FormFullGranuleName(this.m_lowestClosedGeneration);
				string destFileName = this.FormInspectorLogfileName(this.m_lowestClosedGeneration);
				File.Move(sourceFileName, destFileName);
				this.TrackInspectorGeneration(this.m_lowestClosedGeneration, DateTime.UtcNow);
				if (lowestGenReleased == 0L)
				{
					lowestGenReleased = this.m_lowestClosedGeneration;
				}
				highestGenReleased = this.m_lowestClosedGeneration;
				this.m_lowestClosedGeneration += 1L;
			}
			if (lowestGenReleased > 0L)
			{
				ReplayCrimsonEvents.ReleasedClosedButUnfinalizedLogs.Log<string, string, string, string>(this.DatabaseName, Environment.MachineName, string.Format("0x{0:x}", lowestGenReleased), string.Format("0x{0:x}", highestGenReleased));
			}
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x0009BE14 File Offset: 0x0009A014
		private void ReleaseAllClosedButUnfinalizedLogs(out long lowestGenReleased, out long highestGenReleased)
		{
			lowestGenReleased = 0L;
			highestGenReleased = 0L;
			if (this.m_lowestClosedGeneration > 0L)
			{
				this.ReleaseRangeOfClosedButUnfinalizedLogs(this.m_currentGeneration - 1L, out lowestGenReleased, out highestGenReleased);
			}
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x0009BE3C File Offset: 0x0009A03C
		public bool UsePartialLogsDuringAcll(long expectedE00Gen)
		{
			if (expectedE00Gen != this.m_currentGeneration)
			{
				GranularWriter.Tracer.TraceError<long, long>((long)this.GetHashCode(), "UsePartialLogsDuringAcll could not use granular data. ExpectedE00=0x{0:X}. GranularPosition=0x{1:X}", expectedE00Gen, this.m_currentGeneration);
				return false;
			}
			Exception ex = null;
			bool result = false;
			long num = 0L;
			long num2 = 0L;
			try
			{
				this.ReleaseAllClosedButUnfinalizedLogs(out num, out num2);
				if (this.m_currentGeneration > 0L && (this.m_currentGenerationState == GranularWriter.GranularLogState.Open || this.m_currentGenerationState == GranularWriter.GranularLogState.Aborted))
				{
					if (num == 0L)
					{
						num = this.m_currentGeneration;
					}
					num2 = this.m_currentGeneration;
					if (this.m_currentGenerationState == GranularWriter.GranularLogState.Open)
					{
						this.Terminate();
					}
					string text = this.FormFullGranuleName(this.m_currentGeneration);
					if (this.m_lastTimeFromServer != null)
					{
						FileInfo fileInfo = new FileInfo(text);
						fileInfo.LastWriteTimeUtc = this.m_lastTimeFromServer.Value;
					}
					string destFileName = this.FormInspectorLogfileName(0L);
					File.Move(text, destFileName);
					GranularWriter.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Used PartialGranuleAsExx: {0}", text);
					result = true;
				}
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (SecurityException ex3)
			{
				ex = ex3;
			}
			catch (UnauthorizedAccessException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				GranularWriter.Tracer.TraceError<Exception>((long)this.GetHashCode(), "UsePartialLogsDuringAcll fails: {0}", ex);
				ReplayCrimsonEvents.GranularLogsFailedDuringAcll.Log<string, string, string>(this.DatabaseName, Environment.MachineName, ex.Message);
			}
			GranularWriter.Tracer.TraceDebug<string, long, long>((long)this.GetHashCode(), "UsePartialLogsDuringAcll({0}): Incomplete logs used from gen 0x{1} to {2}", this.DatabaseName, num, num2);
			ReplayCrimsonEvents.GranularLogsUsedDuringAcll.Log<string, string, string, string>(this.DatabaseName, Environment.MachineName, string.Format("0x{0:x}", num), string.Format("0x{0:x}", num2));
			return result;
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x0009BFF4 File Offset: 0x0009A1F4
		public bool TerminateWithRelease(long expectedGeneration)
		{
			bool result = false;
			lock (this.m_granularLock)
			{
				try
				{
					if (expectedGeneration != this.m_currentGeneration)
					{
						GranularWriter.Tracer.TraceError<long, long>((long)this.GetHashCode(), "TerminateWithRelease could not use granular data. ExpectedGen=0x{0:X}. GranularPosition=0x{1:X}", expectedGeneration, this.m_currentGeneration);
						return false;
					}
					long num = 0L;
					long num2 = 0L;
					this.ReleaseAllClosedButUnfinalizedLogs(out num, out num2);
					result = true;
				}
				finally
				{
					this.Terminate();
				}
			}
			return result;
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x0009C088 File Offset: 0x0009A288
		public void Terminate()
		{
			GranularWriter.Tracer.TraceDebug<string>((long)this.GetHashCode(), "TerminateGranularReplication({0})", this.DatabaseName);
			this.m_perfmonCounters.GranularReplication = 0L;
			lock (this.m_granularLock)
			{
				if (this.m_currentGenerationState == GranularWriter.GranularLogState.Open)
				{
					this.m_currentGenerationState = GranularWriter.GranularLogState.Aborted;
				}
				if (this.m_jetConsumerInitialized)
				{
					try
					{
						Api.JetTerm(this.m_jetConsumer);
					}
					catch (EsentErrorException arg)
					{
						GranularWriter.Tracer.TraceError<EsentErrorException>((long)this.GetHashCode(), "Terminate failed: {0}", arg);
					}
					this.m_jetConsumerInitialized = false;
					this.m_jetConsumerHealthy = false;
				}
			}
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x0009C144 File Offset: 0x0009A344
		public void Write(JET_EMITDATACTX emitCtx, byte[] databuf, int startOffset)
		{
			if (!this.m_jetConsumerInitialized)
			{
				throw new GranularReplicationTerminatedException("Already terminated");
			}
			try
			{
				StopwatchStamp stamp = StopwatchStamp.GetStamp();
				long num = (long)emitCtx.lgposLogData.lGeneration;
				int grbitOperationalFlags = (int)emitCtx.grbitOperationalFlags;
				bool flag = false;
				if (BitMasker.IsOn(grbitOperationalFlags, 16))
				{
					flag = true;
				}
				UnpublishedApi.JetConsumeLogData(this.m_jetConsumer, emitCtx, databuf, startOffset, (int)emitCtx.cbLogData, ShadowLogConsumeGrbit.FlushData);
				if (flag)
				{
					GranularWriter.Tracer.TraceDebug<string, long>((long)this.GetHashCode(), "WriteGranular({0}): 0x{1:X} is complete", this.DatabaseName, num);
					if (this.m_lowestClosedGeneration == 0L)
					{
						this.m_lowestClosedGeneration = num;
					}
					string text = this.FormFullGranuleName(num);
					FileInfo fileInfo = new FileInfo(text);
					fileInfo.LastWriteTimeUtc = emitCtx.logtimeEmit;
					if (this.m_granularCompletionsDisabled)
					{
						string destFileName = this.FormInspectorLogfileName(num);
						File.Move(text, destFileName);
						this.TrackInspectorGeneration(num, emitCtx.logtimeEmit);
						this.m_lowestClosedGeneration = num + 1L;
					}
					this.m_currentGenerationState = GranularWriter.GranularLogState.Expected;
					this.m_currentGeneration = num + 1L;
				}
				else if (BitMasker.IsOn(grbitOperationalFlags, 8))
				{
					this.m_currentGenerationState = GranularWriter.GranularLogState.Open;
					this.m_currentGeneration = num;
					this.m_lastTimeFromServer = new DateTime?(emitCtx.logtimeEmit);
				}
				long elapsedTicks = stamp.ElapsedTicks;
				this.m_perfmonCounters.RecordBlockModeConsumerWriteLatency(elapsedTicks);
				GranularWriter.Tracer.TracePerformance((long)this.GetHashCode(), "WriteGranular({0},0x{1:X}.0x{2:X}) EmitSeq=0x{3:X} took {4} uSec", new object[]
				{
					this.DatabaseName,
					emitCtx.lgposLogData.lGeneration,
					emitCtx.lgposLogData.isec,
					emitCtx.qwSequenceNum,
					StopwatchStamp.TicksToMicroSeconds(elapsedTicks)
				});
			}
			catch (EsentErrorException ex)
			{
				GranularWriter.Tracer.TraceError<EsentErrorException>((long)this.GetHashCode(), "JetConsumeLogData threw {0}", ex);
				this.m_jetConsumerHealthy = false;
				throw new GranularReplicationTerminatedException(ex.Message, ex);
			}
			catch (IOException ex2)
			{
				GranularWriter.Tracer.TraceError<IOException>((long)this.GetHashCode(), "IOException: {0}", ex2);
				this.m_jetConsumerHealthy = false;
				throw new GranularReplicationTerminatedException(ex2.Message, ex2);
			}
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x0009C38C File Offset: 0x0009A58C
		public void EnableGranularCompletions()
		{
			this.m_granularCompletionsDisabled = false;
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x0009C398 File Offset: 0x0009A598
		public void DisableGranularCompletions()
		{
			if (!this.m_granularCompletionsDisabled)
			{
				this.m_granularCompletionsDisabled = true;
				long arg;
				long arg2;
				this.ReleaseAllClosedButUnfinalizedLogs(out arg, out arg2);
				GranularWriter.Tracer.TraceError<long, long>((long)this.GetHashCode(), "DisableGranularCompletions({0}): released 0x{1:X} to 0x{2:X}", arg, arg2);
			}
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x0009C3D6 File Offset: 0x0009A5D6
		private void ThrowUnexpectedMessage(string msgContext)
		{
			throw new GranularReplicationTerminatedException(ReplayStrings.GranularReplicationMsgSequenceError(msgContext));
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x0009C3E8 File Offset: 0x0009A5E8
		public void ProcessGranularLogCompleteMsg(GranularLogCompleteMsg msg)
		{
			if (this.m_lowestClosedGeneration == 0L || this.m_lowestClosedGeneration > msg.Generation)
			{
				GranularWriter.Tracer.TraceDebug<string, long>((long)this.GetHashCode(), "ProcessGranularLogCompleteMsg({0}): haven't found the completion of the first granule. Ignoring 0x{1:X}", this.DatabaseName, msg.Generation);
				return;
			}
			Exception ex = null;
			try
			{
				if (this.m_lowestClosedGeneration < msg.Generation)
				{
					GranularWriter.Tracer.TraceError<string, long, long>((long)this.GetHashCode(), "The server must have skipped verification.({0}): expecting 0x{1:X} got 0x{2:X}", this.DatabaseName, this.m_lowestClosedGeneration, msg.Generation);
					long num;
					long num2;
					this.ReleaseRangeOfClosedButUnfinalizedLogs(msg.Generation - 1L, out num, out num2);
				}
				string text = this.FormFullGranuleName(msg.Generation);
				ReplayStopwatch replayStopwatch = new ReplayStopwatch();
				replayStopwatch.Start();
				FileInfo fileInfo = new FileInfo(text);
				fileInfo.LastWriteTimeUtc = msg.LastWriteUtc;
				GranularWriter.Tracer.TracePerformance<string, long, ReplayStopwatch>((long)this.GetHashCode(), "GranularLogComplete({0},0x{1:X}) file timestamped at {2}", this.DatabaseName, msg.Generation, replayStopwatch);
				if (msg.ChecksumUsed == GranularLogCloseData.ChecksumAlgorithm.MD5)
				{
					this.ValidateLogFile(msg, text);
					GranularWriter.Tracer.TracePerformance<string, long, ReplayStopwatch>((long)this.GetHashCode(), "GranularLogComplete({0},0x{1:X}) validated at {2}", this.DatabaseName, msg.Generation, replayStopwatch);
				}
				string destFileName = this.FormInspectorLogfileName(msg.Generation);
				File.Move(text, destFileName);
				this.TrackInspectorGeneration(msg.Generation, msg.LastWriteUtc);
				GranularWriter.Tracer.TracePerformance<string, long, ReplayStopwatch>((long)this.GetHashCode(), "GranularLogComplete({0},0x{1:X}) moved at {2}", this.DatabaseName, msg.Generation, replayStopwatch);
				this.m_lowestClosedGeneration = msg.Generation + 1L;
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				throw new GranularReplicationTerminatedException(string.Format("ProcessGranularLogCompleteMsg on 0x{0:X} failed: {1}", msg.Generation, ex.Message), ex);
			}
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x0009C5C8 File Offset: 0x0009A7C8
		private void ValidateLogFile(GranularLogCompleteMsg msg, string fullSourceFileName)
		{
			if (msg.ChecksumUsed != GranularLogCloseData.ChecksumAlgorithm.MD5)
			{
				GranularWriter.Tracer.TraceError((long)this.GetHashCode(), "only MD5 supported for now");
				this.ThrowUnexpectedMessage("only MD5 supported for now");
			}
			Exception ex = null;
			try
			{
				using (SafeFileHandle safeFileHandle = LogCopy.OpenLogForRead(fullSourceFileName))
				{
					using (FileStream fileStream = LogCopy.OpenFileStream(safeFileHandle, true))
					{
						FileInfo fileInfo = new FileInfo(fullSourceFileName);
						if (fileInfo.Length != 1048576L)
						{
							throw new IOException(string.Format("Unexpected log file size: '{0}' has 0x{1:X} bytes", fullSourceFileName, fileInfo.Length));
						}
						byte[] buffer = new byte[1048576];
						int num = fileStream.Read(buffer, 0, 1048576);
						if (num != 1048576)
						{
							GranularWriter.Tracer.TraceError<int, int, string>((long)this.GetHashCode(), "ValidateLogFile. Expected {0} but got {1} bytes from {2}", 1048576, num, fullSourceFileName);
							throw new IOException(ReplayStrings.UnexpectedEOF(fullSourceFileName));
						}
						byte[] b;
						using (MessageDigestForNonCryptographicPurposes messageDigestForNonCryptographicPurposes = new MessageDigestForNonCryptographicPurposes())
						{
							b = messageDigestForNonCryptographicPurposes.ComputeHash(buffer);
						}
						if (!GranularWriter.BytesAreIdentical(b, msg.ChecksumBytes))
						{
							GranularWriter.Tracer.TraceError<string>((long)this.GetHashCode(), "ValidateLogFile: MD5 hash failure on '{0}'", fullSourceFileName);
							throw new GranularReplicationTerminatedException(string.Format("MD5 HASH fails on {0}", fullSourceFileName));
						}
					}
				}
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				throw new GranularReplicationTerminatedException(string.Format("ValidateLogFile on {0} failed: {1}", fullSourceFileName, ex.Message), ex);
			}
		}

		// Token: 0x04000DCC RID: 3532
		private readonly IPerfmonCounters m_perfmonCounters;

		// Token: 0x04000DCD RID: 3533
		private readonly IReplayConfiguration m_config;

		// Token: 0x04000DCE RID: 3534
		private ISetBroken m_setBroken;

		// Token: 0x04000DCF RID: 3535
		private long m_lowestClosedGeneration;

		// Token: 0x04000DD0 RID: 3536
		private long m_currentGeneration;

		// Token: 0x04000DD1 RID: 3537
		private DateTime? m_lastTimeFromServer;

		// Token: 0x04000DD2 RID: 3538
		private GranularWriter.GranularLogState m_currentGenerationState;

		// Token: 0x04000DD3 RID: 3539
		private JET_INSTANCE m_jetConsumer;

		// Token: 0x04000DD4 RID: 3540
		private bool m_jetConsumerInitialized;

		// Token: 0x04000DD5 RID: 3541
		private bool m_jetConsumerHealthy;

		// Token: 0x04000DD6 RID: 3542
		private object m_granularLock = new object();

		// Token: 0x04000DD7 RID: 3543
		private LogCopier m_logCopier;

		// Token: 0x04000DD8 RID: 3544
		private bool m_granularCompletionsDisabled = true;

		// Token: 0x0200033E RID: 830
		private enum GranularLogState
		{
			// Token: 0x04000DDA RID: 3546
			Unknown,
			// Token: 0x04000DDB RID: 3547
			Open,
			// Token: 0x04000DDC RID: 3548
			Aborted,
			// Token: 0x04000DDD RID: 3549
			Expected
		}
	}
}
