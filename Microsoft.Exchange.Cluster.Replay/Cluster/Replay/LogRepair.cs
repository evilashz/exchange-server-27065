using System;
using System.IO;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.Replay.MountPoint;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Unpublished;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002E1 RID: 737
	internal class LogRepair : DisposeTrackableBase
	{
		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x00084A60 File Offset: 0x00082C60
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.LogRepairTracer;
			}
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x00084A68 File Offset: 0x00082C68
		public static void EnableLogRepair(Guid dbGuid)
		{
			LogRepair.Tracer.TraceDebug<Guid>(0L, "EnableLogRepair({0})", dbGuid);
			lock (LogRepair.s_globalLock)
			{
				RegistryStateIO registryStateIO = new RegistryStateIO(null, dbGuid.ToString(), false);
				LogRepairMode logRepairMode;
				registryStateIO.TryReadEnum<LogRepairMode>("LogRepairMode", LogRepairMode.Off, out logRepairMode);
				if (logRepairMode != LogRepairMode.Enabled)
				{
					registryStateIO.WriteEnum<LogRepairMode>("LogRepairMode", LogRepairMode.Enabled, true);
					if (logRepairMode == LogRepairMode.Off)
					{
						registryStateIO.WriteLong("LogRepairRetryCount", 0L, true);
					}
				}
				else
				{
					LogRepair.Tracer.TraceDebug<Guid>(0L, "EnableLogRepair({0}) skipped because already enabled", dbGuid);
				}
			}
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x00084B14 File Offset: 0x00082D14
		public static bool ExitLogRepair(Guid dbGuid)
		{
			LogRepair.Tracer.TraceDebug<Guid>(0L, "ExitLogRepair({0})", dbGuid);
			bool result;
			lock (LogRepair.s_globalLock)
			{
				RegistryStateIO registryStateIO = new RegistryStateIO(null, dbGuid.ToString(), false);
				LogRepairMode logRepairMode;
				registryStateIO.TryReadEnum<LogRepairMode>("LogRepairMode", LogRepairMode.Off, out logRepairMode);
				if (logRepairMode == LogRepairMode.ReplayPending)
				{
					registryStateIO.WriteEnum<LogRepairMode>("LogRepairMode", LogRepairMode.Off, true);
					registryStateIO.WriteLong("LogRepairRetryCount", 0L, true);
					result = true;
				}
				else if (logRepairMode == LogRepairMode.Enabled)
				{
					LogRepair.Tracer.TraceDebug<Guid>(0L, "ExitLogRepair({0}) skipped because another corruption was reported", dbGuid);
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001D5F RID: 7519 RVA: 0x00084BCC File Offset: 0x00082DCC
		// (set) Token: 0x06001D60 RID: 7520 RVA: 0x00084BD4 File Offset: 0x00082DD4
		private ReplayConfiguration Config { get; set; }

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001D61 RID: 7521 RVA: 0x00084BDD File Offset: 0x00082DDD
		public string DatabaseName
		{
			get
			{
				return this.Config.DatabaseName;
			}
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x00084BEA File Offset: 0x00082DEA
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<LogRepair>(this);
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x00084BF4 File Offset: 0x00082DF4
		protected override void InternalDispose(bool disposing)
		{
			lock (this)
			{
				if (disposing)
				{
					if (this.EseLogVerifier != null)
					{
						this.EseLogVerifier.Dispose();
						this.EseLogVerifier = null;
					}
					if (this.m_logSource != null)
					{
						this.m_logSource.Close();
					}
					FileOperations.RemoveDirectory(this.m_workingPath);
				}
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x00084C64 File Offset: 0x00082E64
		// (set) Token: 0x06001D65 RID: 7525 RVA: 0x00084C6C File Offset: 0x00082E6C
		private LogChecksummer EseLogVerifier { get; set; }

		// Token: 0x06001D66 RID: 7526 RVA: 0x00084C78 File Offset: 0x00082E78
		public LogRepair(ReplayConfiguration cfg)
		{
			this.Config = cfg;
			LogRepair.Tracer.TraceDebug<string>((long)this.GetHashCode(), "LogRepair({0}): constructing", this.Config.DisplayName);
			this.m_workingPath = Path.Combine(this.Config.DestinationLogPath, "LogRepair");
			FileOperations.RemoveDirectory(this.m_workingPath);
			Directory.CreateDirectory(this.m_workingPath);
			this.EseLogVerifier = new LogChecksummer(this.Config.LogFilePrefix);
			this.m_verifierLogDataBuf = new byte[1048576];
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x00084D0C File Offset: 0x00082F0C
		private Exception VerifyLog(string fullLocalFileName, long expectedGen)
		{
			Exception ex = null;
			try
			{
				ReplayStopwatch replayStopwatch = new ReplayStopwatch();
				replayStopwatch.Start();
				using (SafeFileHandle safeFileHandle = LogCopy.OpenLogForRead(fullLocalFileName))
				{
					long arg = replayStopwatch.ElapsedMilliseconds;
					LogRepair.Tracer.TracePerformance<string, long>((long)this.GetHashCode(), "VerifyLog({0}): Open took: {1} ms", fullLocalFileName, arg);
					using (FileStream fileStream = LogCopy.OpenFileStream(safeFileHandle, true))
					{
						FileInfo fileInfo = new FileInfo(fullLocalFileName);
						long elapsedMilliseconds;
						try
						{
							if (fileInfo.Length != 1048576L)
							{
								throw new IOException(string.Format("Unexpected log file size: '{0}' has 0x{1:X} bytes.", fullLocalFileName, fileInfo.Length));
							}
							elapsedMilliseconds = replayStopwatch.ElapsedMilliseconds;
							int num = fileStream.Read(this.m_verifierLogDataBuf, 0, 1048576);
							if (num != 1048576)
							{
								LogRepair.Tracer.TraceError<int, int, string>((long)this.GetHashCode(), "VerifyLog. Expected {0} but got {1} bytes from {2}", 1048576, num, fullLocalFileName);
								throw new IOException(ReplayStrings.UnexpectedEOF(fullLocalFileName));
							}
							arg = replayStopwatch.ElapsedMilliseconds - elapsedMilliseconds;
							LogRepair.Tracer.TracePerformance<string, long>((long)this.GetHashCode(), "VerifyLog({0}): Read took: {1} ms", fullLocalFileName, arg);
						}
						catch (IOException ex2)
						{
							throw new CorruptLogDetectedException(fullLocalFileName, ex2.Message, ex2);
						}
						elapsedMilliseconds = replayStopwatch.ElapsedMilliseconds;
						EsentErrorException ex3 = this.EseLogVerifier.Verify(fullLocalFileName, this.m_verifierLogDataBuf);
						if (ex3 != null)
						{
							LogRepair.Tracer.TraceError<string, EsentErrorException>((long)this.GetHashCode(), "ESELogVerifier({0}) failed: {1}", fullLocalFileName, ex3);
							if (ex3 is EsentLogFileCorruptException)
							{
								throw new CorruptLogDetectedException(fullLocalFileName, ex3.Message, ex3);
							}
							throw ex3;
						}
						else
						{
							JET_LOGINFOMISC jet_LOGINFOMISC;
							UnpublishedApi.JetGetLogFileInfo(fullLocalFileName, out jet_LOGINFOMISC, JET_LogInfo.Misc2);
							if ((long)jet_LOGINFOMISC.ulGeneration != expectedGen)
							{
								throw new FileCheckLogfileGenerationException(fullLocalFileName, (long)jet_LOGINFOMISC.ulGeneration, expectedGen);
							}
							if (this.m_logfileSignature == null)
							{
								this.m_logfileSignature = new JET_SIGNATURE?(jet_LOGINFOMISC.signLog);
							}
							if (!jet_LOGINFOMISC.signLog.Equals(this.m_logfileSignature))
							{
								throw new FileCheckLogfileSignatureException(fullLocalFileName, jet_LOGINFOMISC.signLog.ToString(), this.m_logfileSignature.ToString());
							}
							arg = replayStopwatch.ElapsedMilliseconds - elapsedMilliseconds;
							LogRepair.Tracer.TracePerformance<string, long>((long)this.GetHashCode(), "VerifyLog({0}): Verify took: {1} ms", fullLocalFileName, arg);
							return null;
						}
					}
				}
			}
			catch (IOException ex4)
			{
				ex = ex4;
			}
			catch (UnauthorizedAccessException ex5)
			{
				ex = ex5;
			}
			catch (EsentErrorException ex6)
			{
				ex = ex6;
			}
			catch (CorruptLogDetectedException ex7)
			{
				ex = ex7;
			}
			catch (FileCheckException ex8)
			{
				ex = ex8;
			}
			if (ex != null)
			{
				LogRepair.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "VerifyLog({0}): failed with {1}", fullLocalFileName, ex);
			}
			return ex;
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x00085044 File Offset: 0x00083244
		public void CheckAndRepair(long gen)
		{
			string text = this.Config.BuildFullLogfileName(gen);
			Exception ex = this.VerifyLog(text, gen);
			if (ex == null)
			{
				return;
			}
			ReplayCrimsonEvents.LogRepairFailedVerify.Log<string, string, string, string>(this.DatabaseName, Environment.MachineName, text, ex.ToString());
			if (ex is CorruptLogDetectedException || ex is EsentErrorException || ex is FileCheckException || ex is IOException)
			{
				this.AttemptRepair(gen, text);
				ReplayEventLogConstants.Tuple_LogRepairSuccess.LogEvent(null, new object[]
				{
					text,
					this.Config.DisplayName,
					this.Config.SourceMachine
				});
				return;
			}
			throw new LogRepairUnexpectedVerifyException(text, ex.Message, ex);
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x000850F4 File Offset: 0x000832F4
		public void CheckForDivergence(FileState fileState)
		{
			this.m_logSource = LogSource.Construct(this.Config, null, null, LogSource.GetLogShipTimeoutInMsec(false));
			if (0L == fileState.HighestGenerationPresent || fileState.HighestGenerationPresent < fileState.HighestGenerationRequired)
			{
				LogRepair.Tracer.TraceError<string, long>((long)this.GetHashCode(), "CheckForDivergence({0}): failed with HighestGenerationPresent 0x{1:X}", this.Config.DisplayName, fileState.HighestGenerationPresent);
				throw new LogRepairNotPossibleException(ReplayStrings.LogRepairNotPossibleInsuffientToCheckDivergence);
			}
			long num = fileState.HighestGenerationPresent;
			string text = Path.Combine(this.Config.DestinationLogPath, this.Config.LogFilePrefix) + "." + this.Config.LogExtension;
			bool flag = false;
			string text2;
			if (File.Exists(text))
			{
				flag = true;
				num += 1L;
				text2 = text;
			}
			else
			{
				text2 = this.Config.BuildFullLogfileName(num);
			}
			long num2 = -1L;
			Exception ex = null;
			try
			{
				num2 = this.m_logSource.QueryEndOfLog();
			}
			catch (NetworkRemoteException ex2)
			{
				ex = ex2;
			}
			catch (NetworkTransportException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				throw new LogRepairTransientException(ex.Message, ex);
			}
			if (num2 < num)
			{
				LogRepair.Tracer.TraceError<string, long>((long)this.GetHashCode(), "CheckForDivergence({0}): failed with HighestGenerationPresent 0x{1:X}", this.Config.DisplayName, fileState.HighestGenerationPresent);
				throw new LogRepairNotPossibleException(ReplayStrings.LogRepairNotPossibleActiveIsDivergent(this.Config.SourceMachine));
			}
			ex = this.VerifyLog(text2, num);
			if (ex != null)
			{
				throw new LogRepairNotPossibleException(ReplayStrings.LogRepairDivergenceCheckFailedDueToCorruptEndOfLog(text2, ex.Message), ex);
			}
			string path = this.Config.BuildShortLogfileName(num);
			string text3 = Path.Combine(this.m_workingPath, path);
			this.CopyAndVerifyFromActive(num, text3);
			try
			{
				if (flag)
				{
					if (!EseHelper.IsLogfileSubset(text3, text, this.m_workingPath, null, null))
					{
						LogRepair.Tracer.TraceError<string, long>((long)this.GetHashCode(), "CheckForDivergence({0}): failed with e00 divergence at gen 0x{1}.", this.Config.DisplayName, num);
						throw new LogRepairNotPossibleException(ReplayStrings.LogRepairNotPossibleActiveIsDivergent(this.Config.SourceMachine));
					}
					long num3 = num - 1L;
					if (num3 <= 1L)
					{
						throw new LogRepairNotPossibleException(ReplayStrings.LogRepairNotPossibleInsuffientToCheckDivergence);
					}
					string text4 = this.Config.BuildFullLogfileName(num3);
					this.VerifyLog(text4, num3);
					if (ex != null)
					{
						throw new LogRepairNotPossibleException(ReplayStrings.LogRepairDivergenceCheckFailedDueToCorruptEndOfLog(text4, ex.Message), ex);
					}
					string text5 = this.Config.BuildShortLogfileName(num3);
					string text6 = Path.Combine(this.m_workingPath, text5);
					this.CopyAndVerifyFromActive(num3, text6);
					Exception ex4;
					if (!this.IsFileBinaryEqual(text6, text4, out ex4))
					{
						if (ex4 != null)
						{
							throw new LogRepairDivergenceCheckFailedException(text5, text6, ex4.Message, ex4);
						}
						throw new LogRepairNotPossibleException(ReplayStrings.LogRepairNotPossibleActiveIsDivergent(this.Config.SourceMachine));
					}
				}
				else if (!EseHelper.IsLogfileEqual(text3, text2, this.m_workingPath, null, null))
				{
					LogRepair.Tracer.TraceError<string, long>((long)this.GetHashCode(), "CheckForDivergence({0}): failed with divergence at gen 0x{1}.", this.Config.DisplayName, num);
					throw new LogRepairNotPossibleException(ReplayStrings.LogRepairNotPossibleActiveIsDivergent(this.Config.SourceMachine));
				}
			}
			catch (EsentErrorException ex5)
			{
				throw new LogRepairDivergenceCheckFailedException(text2, text3, ex5.Message, ex5);
			}
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x00085458 File Offset: 0x00083658
		public void BeginRepairAttempt()
		{
			long logRepairRetryCount = this.Config.ReplayState.LogRepairRetryCount;
			if (logRepairRetryCount >= 3L)
			{
				LogRepair.Tracer.TraceError<string, long>((long)this.GetHashCode(), "BeginRepairAttempt({0}): failed retryCount at {1}.", this.Config.DisplayName, logRepairRetryCount);
				throw new LogRepairRetryCountExceededException(logRepairRetryCount);
			}
			this.Config.ReplayState.LogRepairRetryCount = logRepairRetryCount + 1L;
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x000854DC File Offset: 0x000836DC
		private bool IsFileBinaryEqual(string filename1, string filename2, out Exception ex)
		{
			bool isEqual = false;
			ex = MountPointUtil.HandleIOExceptions(delegate
			{
				isEqual = IncrementalReseeder.IsFileBinaryEqual(filename1, filename2);
			});
			return isEqual;
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x00085520 File Offset: 0x00083720
		private Exception CopyActiveLog(long generation, string localFileName)
		{
			Exception result = null;
			try
			{
				this.m_logSource.CopyLog(generation, localFileName);
			}
			catch (NetworkRemoteException ex)
			{
				result = ex;
			}
			catch (NetworkTransportException ex2)
			{
				result = ex2;
			}
			catch (IOException ex3)
			{
				result = ex3;
			}
			return result;
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x00085578 File Offset: 0x00083778
		private void CopyAndVerifyFromActive(long generation, string localFilename)
		{
			Exception ex = this.CopyActiveLog(generation, localFilename);
			if (ex != null)
			{
				throw new LogRepairFailedToCopyFromActiveException(localFilename, ex.Message, ex);
			}
			ex = this.VerifyLog(localFilename, generation);
			if (ex != null)
			{
				throw new LogRepairFailedToVerifyFromActiveException(localFilename, this.Config.SourceMachine, ex.Message, ex);
			}
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x000855C4 File Offset: 0x000837C4
		private void AttemptRepair(long generation, string logToRepairFullFileName)
		{
			string path = this.Config.BuildShortLogfileName(generation);
			string text = Path.Combine(this.m_workingPath, path);
			this.CopyAndVerifyFromActive(generation, text);
			string text2 = Path.Combine(this.Config.DestinationLogPath, "CorruptLogs");
			FileOperations.CreateDirectoryIfNeeded(text2);
			string text3 = Path.Combine(text2, path);
			if (File.Exists(text3))
			{
				File.Delete(logToRepairFullFileName);
			}
			else
			{
				File.Move(logToRepairFullFileName, text3);
			}
			File.Move(text, logToRepairFullFileName);
			LogRepair.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "AttemptRepair({0}): succeeded on {1}.", this.Config.DisplayName, logToRepairFullFileName);
		}

		// Token: 0x04000C44 RID: 3140
		private const int MaxRepairCycles = 3;

		// Token: 0x04000C45 RID: 3141
		private const string WorkingDirName = "LogRepair";

		// Token: 0x04000C46 RID: 3142
		private const string CorruptLogsDirName = "CorruptLogs";

		// Token: 0x04000C47 RID: 3143
		private static object s_globalLock = new object();

		// Token: 0x04000C48 RID: 3144
		private string m_workingPath;

		// Token: 0x04000C49 RID: 3145
		private LogSource m_logSource;

		// Token: 0x04000C4A RID: 3146
		private byte[] m_verifierLogDataBuf;

		// Token: 0x04000C4B RID: 3147
		private JET_SIGNATURE? m_logfileSignature;
	}
}
