using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002DC RID: 732
	internal abstract class ShipControl : IStartStop
	{
		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x00082918 File Offset: 0x00080B18
		protected virtual long MaxGapSize
		{
			get
			{
				return 10L;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x0008291D File Offset: 0x00080B1D
		protected virtual FailureTag TagUsedOnLogGapDetected
		{
			get
			{
				return FailureTag.LogGapFatal;
			}
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x00082924 File Offset: 0x00080B24
		protected ShipControl(string fromDir, string fromPrefix, long fromNumber, string fromSuffix, ISetBroken setBroken, IReplicaProgress replicaProgress)
		{
			this.m_className = base.GetType().Name;
			this.m_fromDir = fromDir;
			this.m_fromPrefix = fromPrefix;
			this.m_fromNumber = fromNumber;
			this.m_fromSuffix = fromSuffix;
			this.m_shipLogsSetBroken = new ShipLogsSetBroken(setBroken, null);
			this.m_setBroken = this.m_shipLogsSetBroken;
			this.m_replicaProgress = replicaProgress;
			this.m_countdownToGapTest = 6L;
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001D0C RID: 7436 RVA: 0x0008298F File Offset: 0x00080B8F
		protected virtual string FromDir
		{
			get
			{
				return this.m_fromDir;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x00082997 File Offset: 0x00080B97
		protected string FromPrefix
		{
			get
			{
				return this.m_fromPrefix;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x0008299F File Offset: 0x00080B9F
		// (set) Token: 0x06001D0F RID: 7439 RVA: 0x000829A7 File Offset: 0x00080BA7
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

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x000829B0 File Offset: 0x00080BB0
		protected string FromSuffix
		{
			get
			{
				return this.m_fromSuffix;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001D11 RID: 7441 RVA: 0x000829B8 File Offset: 0x00080BB8
		protected bool PrepareToStopCalled
		{
			get
			{
				return this.m_prepareToStopCalled;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x000829C2 File Offset: 0x00080BC2
		protected bool Initialized
		{
			get
			{
				return this.m_initialized;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001D13 RID: 7443 RVA: 0x000829CA File Offset: 0x00080BCA
		// (set) Token: 0x06001D14 RID: 7444 RVA: 0x000829D2 File Offset: 0x00080BD2
		protected ManualResetEvent GoingIdleEvent
		{
			get
			{
				return this.m_goingIdleEvent;
			}
			set
			{
				this.m_goingIdleEvent = value;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x000829DB File Offset: 0x00080BDB
		protected bool ShiplogsActive
		{
			get
			{
				return this.m_active;
			}
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x000829E3 File Offset: 0x00080BE3
		public static string GenerationString(long generationNumber)
		{
			return string.Format("{0:X8}", generationNumber);
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x000829F8 File Offset: 0x00080BF8
		public static long LowestGenerationInDirectory(DirectoryInfo di, string prefix, string suffix, bool ignoreDirectoryMissing = false)
		{
			long result = 0L;
			if (ignoreDirectoryMissing && !di.Exists)
			{
				return 0L;
			}
			using (EseLogEnumerator eseLogEnumerator = new EseLogEnumerator(di, prefix, suffix))
			{
				result = eseLogEnumerator.FindLowestGeneration();
			}
			return result;
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x00082A44 File Offset: 0x00080C44
		public static long HighestGenerationInDirectory(DirectoryInfo di, string prefix, string suffix)
		{
			long result = 0L;
			using (EseLogEnumerator eseLogEnumerator = new EseLogEnumerator(di, prefix, suffix))
			{
				string text = eseLogEnumerator.FindHighestGenerationLogFile();
				if (!string.IsNullOrEmpty(text))
				{
					ShipControl.GetGenerationNumberFromFilename(Path.GetFileName(text), prefix, out result);
				}
			}
			return result;
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x00082A98 File Offset: 0x00080C98
		public static bool GenerationAvailableInDirectory(DirectoryInfo di, string prefix, string suffix, long generation)
		{
			string searchPattern = EseHelper.MakeLogfileName(prefix, suffix, generation);
			FileInfo[] files = di.GetFiles(searchPattern);
			return files.Length != 0;
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x00082AC0 File Offset: 0x00080CC0
		public void Start()
		{
			this.m_wakeTimer = new TimerState();
			this.m_wakeTimer.M_thisShip = this;
			TimerCallback callback = new TimerCallback(ShipControl.ShipTimeout);
			this.m_wakeTimer.M_thisTimer = new Timer(callback, this.m_wakeTimer, -1, -1);
			ThreadPool.QueueUserWorkItem(new WaitCallback(ShipControl.ShipWorkThread), this);
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x00082B20 File Offset: 0x00080D20
		public void Stop()
		{
			ExTraceGlobals.ShipLogTracer.TraceFunction<string>((long)this.GetHashCode(), "{0}: Entering Stop()", this.m_className);
			lock (this)
			{
				if (this.m_fStopped)
				{
					return;
				}
				if (!this.m_prepareToStopCalled)
				{
					this.PrepareToStop();
				}
				this.StartOfStopTime = DateTime.UtcNow;
				if (this.m_stopEvent == null && this.m_active)
				{
					ExTraceGlobals.ShipLogTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: Creating m_stopEvent because m_active is true", this.m_className);
					this.m_stopEvent = new ManualResetEvent(false);
				}
				ExTraceGlobals.ShipLogTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: ShipLog setting m_active=true in Stop()", this.m_className);
				this.m_active = true;
				if (this.m_goingIdleEvent != null)
				{
					this.m_goingIdleEvent.Set();
				}
				this.m_fStopped = true;
			}
			if (this.m_stopEvent != null)
			{
				ExTraceGlobals.ShipLogTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: Dispose wait", this.m_className);
				this.m_stopEvent.WaitOne();
				this.m_stopEvent = null;
				ExTraceGlobals.ShipLogTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: Dispose wait finished", this.m_className);
			}
			lock (this)
			{
				if (this.m_wakeTimer != null)
				{
					this.m_wakeTimer.Dispose();
					this.m_wakeTimer = null;
				}
			}
			this.EndOfStopTime = DateTime.UtcNow;
			this.StopInternal();
			this.DisposeWatcherState();
			ExTraceGlobals.ShipLogTracer.TraceFunction<string>((long)this.GetHashCode(), "{0}: Exiting Stop()", this.m_className);
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x00082CD4 File Offset: 0x00080ED4
		public virtual void ShipNotification(long logFileNumber)
		{
			ExTraceGlobals.ShipLogTracer.TraceDebug<long>((long)this.GetHashCode(), "ShipNotification called, logFileNumber = {0}", logFileNumber);
		}

		// Token: 0x06001D1D RID: 7453
		public abstract Result ShipAction(long fromNumber);

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x00082CED File Offset: 0x00080EED
		// (set) Token: 0x06001D1F RID: 7455 RVA: 0x00082CF5 File Offset: 0x00080EF5
		protected DateTime PrepareToStopTime { get; set; }

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x00082CFE File Offset: 0x00080EFE
		// (set) Token: 0x06001D21 RID: 7457 RVA: 0x00082D06 File Offset: 0x00080F06
		protected DateTime StartOfStopTime { get; set; }

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x00082D0F File Offset: 0x00080F0F
		// (set) Token: 0x06001D23 RID: 7459 RVA: 0x00082D17 File Offset: 0x00080F17
		protected DateTime EndOfStopTime { get; set; }

		// Token: 0x06001D24 RID: 7460 RVA: 0x00082D20 File Offset: 0x00080F20
		public void PrepareToStop()
		{
			ExTraceGlobals.ShipLogTracer.TraceFunction<string>((long)this.GetHashCode(), "{0}: Entering PrepareToStop()", this.m_className);
			if (!this.m_prepareToStopCalled)
			{
				this.PrepareToStopTime = DateTime.UtcNow;
			}
			this.m_prepareToStopCalled = true;
			if (this.m_wakeTimer != null)
			{
				this.m_wakeTimer.M_thisTimer.Change(-1, -1);
			}
			this.PrepareToStopInternal();
			ExTraceGlobals.ShipLogTracer.TraceFunction<string>((long)this.GetHashCode(), "{0}: Exiting PrepareToStop()", this.m_className);
		}

		// Token: 0x06001D25 RID: 7461
		public abstract void LogError(string inputFile, Exception ex);

		// Token: 0x06001D26 RID: 7462 RVA: 0x00082DA4 File Offset: 0x00080FA4
		protected static bool GetGenerationNumberFromFilename(string filename, string prefix, out long generation)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
			return long.TryParse(fileNameWithoutExtension.Substring(prefix.Length), NumberStyles.HexNumber, null, out generation);
		}

		// Token: 0x06001D27 RID: 7463
		protected abstract void StopInternal();

		// Token: 0x06001D28 RID: 7464 RVA: 0x00083078 File Offset: 0x00081278
		protected void RunFileIoOperation(ref Result result, ShipControl.FileIoOperation operation)
		{
			Result innerResult = result;
			try
			{
				Dependencies.Watson.SendReportOnUnhandledException(delegate
				{
					try
					{
						operation();
					}
					catch (FileNotFoundException lastException8)
					{
						innerResult = Result.LongWaitRetry;
						this.m_lastException = lastException8;
					}
					catch (MapiRetryableException lastException9)
					{
						innerResult = Result.ShortWaitRetry;
						this.m_lastException = lastException9;
					}
					catch (FileSharingViolationOnSourceException lastException10)
					{
						innerResult = Result.LongWaitRetry;
						this.m_lastException = lastException10;
					}
					catch (DirectoryNotFoundException lastException11)
					{
						innerResult = Result.LongWaitRetry;
						this.m_lastException = lastException11;
					}
					catch (IOException lastException12)
					{
						innerResult = Result.ShortWaitRetry;
						this.m_lastException = lastException12;
					}
					catch (WebException lastException13)
					{
						innerResult = Result.ShortWaitRetry;
						this.m_lastException = lastException13;
					}
					catch (NetworkRemoteException lastException14)
					{
						this.m_lastException = lastException14;
						innerResult = Result.ShortWaitRetry;
					}
					catch (NetworkTransportException lastException15)
					{
						this.m_lastException = lastException15;
						innerResult = Result.ShortWaitRetry;
					}
					catch (SecurityException lastException16)
					{
						innerResult = Result.LongWaitRetry;
						this.m_lastException = lastException16;
					}
					catch (UnauthorizedAccessException lastException17)
					{
						innerResult = Result.LongWaitRetry;
						this.m_lastException = lastException17;
					}
					catch (TransientException lastException18)
					{
						innerResult = Result.ShortWaitRetry;
						this.m_lastException = lastException18;
					}
					catch (EsentFileAccessDeniedException lastException19)
					{
						innerResult = Result.LongWaitRetry;
						this.m_lastException = lastException19;
					}
					catch (EsentDiskIOException lastException20)
					{
						innerResult = Result.LongWaitRetry;
						this.m_lastException = lastException20;
					}
					catch (EsentFileNotFoundException lastException21)
					{
						innerResult = Result.LongWaitRetry;
						this.m_lastException = lastException21;
					}
					catch (OperationAbortedException lastException22)
					{
						innerResult = Result.GiveUp;
						this.m_lastException = lastException22;
					}
					catch (OperationCanceledException lastException23)
					{
						innerResult = Result.GiveUp;
						this.m_lastException = lastException23;
					}
					catch (ArgumentException lastException24)
					{
						innerResult = Result.GiveUp;
						this.m_lastException = lastException24;
					}
				});
			}
			catch (PathTooLongException lastException)
			{
				result = Result.GiveUp;
				this.m_lastException = lastException;
			}
			catch (NotSupportedException lastException2)
			{
				result = Result.GiveUp;
				this.m_lastException = lastException2;
			}
			catch (ObjectDisposedException lastException3)
			{
				result = Result.GiveUp;
				this.m_lastException = lastException3;
			}
			catch (EsentLogFileCorruptException lastException4)
			{
				result = Result.GiveUp;
				this.m_lastException = lastException4;
			}
			catch (EsentBadLogVersionException lastException5)
			{
				result = Result.GiveUp;
				this.m_lastException = lastException5;
			}
			catch (EsentFileIOBeyondEOFException lastException6)
			{
				result = Result.GiveUp;
				this.m_lastException = lastException6;
			}
			catch (EsentFileInvalidTypeException lastException7)
			{
				result = Result.GiveUp;
				this.m_lastException = lastException7;
			}
			if (innerResult != Result.Success && result != Result.GiveUp)
			{
				result = innerResult;
			}
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x0008319C File Offset: 0x0008139C
		protected void ShipLogs()
		{
			this.ShipLogs(false);
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x00083434 File Offset: 0x00081634
		protected void ShipLogs(bool fAttemptFinalCopy)
		{
			int num = 10000;
			Result result = Result.Success;
			bool fBreakOutOfLoop = false;
			ExTraceGlobals.ShipLogTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: Entering ShipLogs()", this.m_className);
			Monitor.Enter(this);
			bool monitorHeld = true;
			if (!this.m_active && !this.PrepareToStopCalled)
			{
				if (this.m_fAttemptFinalCopyCalled)
				{
					if (!fAttemptFinalCopy)
					{
						goto IL_376;
					}
				}
				try
				{
					this.m_active = true;
					string filenameToTry = null;
					if (this.m_wakeTimer != null)
					{
						this.m_wakeTimer.M_thisTimer.Change(-1, -1);
					}
					DirectoryInfo di = null;
					di = new DirectoryInfo(this.FromDir);
					do
					{
						this.m_lastException = null;
						if (!this.m_initialized)
						{
							Monitor.Exit(this);
							monitorHeld = false;
							result = this.ShipRestartFindFirst();
							Monitor.Enter(this);
							monitorHeld = true;
							if (result != Result.Success)
							{
								break;
							}
							this.m_initialized = true;
						}
						this.m_notified = false;
						Monitor.Exit(this);
						monitorHeld = false;
						this.RunFileIoOperation(ref result, delegate
						{
							long fromNumber = this.FromNumber;
							filenameToTry = this.GetFilenameFromGenerationNumber(fromNumber);
							ExTraceGlobals.ShipLogTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "{0}: Trying to find file {1}\\{2}", this.m_className, this.FromDir, filenameToTry);
							bool flag = di.GetFiles(filenameToTry).Length == 1;
							if (flag)
							{
								ExTraceGlobals.ShipLogTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0}: Found file {1}", this.m_className, filenameToTry);
								ExTraceGlobals.PFDTracer.TracePfd<int, string, string>((long)this.GetHashCode(), "PFD CRS {0} {1}: Found file {2}", 18395, this.m_className, filenameToTry);
								this.m_countdownToGapTest = 6L;
								this.TestDelaySleep();
								result = this.ShipAction(fromNumber);
								Monitor.Enter(this);
								monitorHeld = true;
								if (result != Result.Success)
								{
									fBreakOutOfLoop = true;
									return;
								}
								this.m_lastWait = 0;
								this.m_totalWait = 0;
								this.m_fromNumber += 1L;
								return;
							}
							else
							{
								ShipControl <>4__this = this;
								long countdownToGapTest;
								<>4__this.m_countdownToGapTest = (countdownToGapTest = <>4__this.m_countdownToGapTest) - 1L;
								if (countdownToGapTest == 0L)
								{
									this.CheckForGaps(fromNumber);
								}
								Monitor.Enter(this);
								monitorHeld = true;
								if (this.m_lastWait != 0)
								{
									fBreakOutOfLoop = true;
									result = Result.ShortWaitRetry;
									return;
								}
								this.m_totalWait = 0;
								if (!this.m_notified)
								{
									fBreakOutOfLoop = true;
								}
								return;
							}
						});
						if (fBreakOutOfLoop)
						{
							break;
						}
						if (result != Result.Success)
						{
							ExTraceGlobals.ShipLogTracer.TraceError<Result, Exception>((long)this.GetHashCode(), "ShipLogs failed, result={0}:{1}", result, this.m_lastException);
						}
					}
					while (!this.m_prepareToStopCalled && result == Result.Success);
					if (this.m_initialized && result != Result.Success && this.m_lastWait == 0)
					{
						this.LogError(filenameToTry, this.m_lastException);
					}
					return;
				}
				finally
				{
					if (!monitorHeld)
					{
						Monitor.Enter(this);
						monitorHeld = true;
					}
					this.m_active = false;
					if (this.m_goingIdleEvent != null)
					{
						this.m_goingIdleEvent.Set();
					}
					if (!this.m_prepareToStopCalled)
					{
						if (result != Result.Success)
						{
							if (result == Result.ShortWaitRetry)
							{
								num = this.m_lastWait + 1000;
								if (num > 10000)
								{
									num = 10000;
								}
							}
							else if (result == Result.LongWaitRetry)
							{
								num = 10000;
							}
							else if (result == Result.GiveUp)
							{
								num = -1;
							}
							if (this.m_lastShipLogsResult != Result.Success && this.m_lastShipLogsResult != Result.GiveUp && result != Result.GiveUp && this.m_lastException != null)
							{
								this.m_totalWait += this.m_lastWait;
							}
							else
							{
								this.m_totalWait = 0;
							}
							this.m_lastWait = num;
						}
						if (this.m_wakeTimer != null)
						{
							this.m_wakeTimer.M_thisTimer.Change(num, -1);
						}
					}
					if (this.m_stopEvent != null)
					{
						ExTraceGlobals.ShipLogTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: Setting stopEvent at end of ShipLogs", this.m_className);
						this.m_stopEvent.Set();
					}
					this.m_lastShipLogsResult = result;
					if (!this.m_prepareToStopCalled && !this.m_setBroken.IsBroken && (this.m_lastShipLogsResult == Result.GiveUp || this.m_totalWait >= 30000))
					{
						this.m_totalWait = 0;
						string text = string.Empty;
						if (this.m_lastException != null)
						{
							text = this.m_lastException.Message;
						}
						this.m_setBroken.SetBroken(FailureTag.NoOp, ReplayEventLogConstants.Tuple_ShipLogFailed, new string[]
						{
							this.m_className,
							text
						});
					}
					Monitor.Exit(this);
					monitorHeld = false;
				}
			}
			IL_376:
			this.m_notified = true;
			Monitor.Exit(this);
			monitorHeld = false;
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x000837E8 File Offset: 0x000819E8
		protected string GetFilenameFromGenerationNumber(long generation)
		{
			return this.FromPrefix + ((generation == 0L) ? string.Empty : ShipControl.GenerationString(generation)) + this.FromSuffix;
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x00083810 File Offset: 0x00081A10
		protected DateTime GetFiletimeOfLog(string logfile)
		{
			FileInfo fileInfo = new FileInfo(logfile);
			return fileInfo.LastWriteTimeUtc;
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x0008382A File Offset: 0x00081A2A
		protected virtual bool GapsAreAcceptable()
		{
			return false;
		}

		// Token: 0x06001D2E RID: 7470
		protected abstract void TestDelaySleep();

		// Token: 0x06001D2F RID: 7471
		protected abstract Result InitializeStartContext();

		// Token: 0x06001D30 RID: 7472
		protected abstract void PrepareToStopInternal();

		// Token: 0x06001D31 RID: 7473 RVA: 0x00083830 File Offset: 0x00081A30
		private static void ShipWorkThread(object source)
		{
			ShipControl shipControl = (ShipControl)source;
			shipControl.ShipLogs();
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x0008384C File Offset: 0x00081A4C
		private static void ShipDirectoryChange(object source, FileSystemEventArgs logFile)
		{
			WatcherState watcherState = source as WatcherState;
			long num = 0L;
			if (ShipControl.GetGenerationNumberFromFilename(logFile.Name, watcherState.M_thisShip.FromPrefix, out num) && num != 0L)
			{
				watcherState.M_thisShip.ShipNotification(num);
			}
			watcherState.M_thisShip.ShipLogs();
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x0008389C File Offset: 0x00081A9C
		private static void ShipTimeout(object source)
		{
			TimerState timerState = (TimerState)source;
			timerState.M_thisShip.ShipLogs();
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x000838BB File Offset: 0x00081ABB
		private void DisposeWatcherState()
		{
			if (this.m_sourceDirWatcher != null)
			{
				this.m_sourceDirWatcher.Dispose();
				this.m_sourceDirWatcher = null;
			}
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x00083B44 File Offset: 0x00081D44
		private Result ShipRestartFindFirst()
		{
			Result result = Result.Success;
			bool createNewWatcher = false;
			bool fNoFileFound = false;
			ExTraceGlobals.ShipLogTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: Entering ShipRestartFindFirst", this.m_className);
			long lowestSource;
			this.RunFileIoOperation(ref result, delegate
			{
				DirectoryInfo di = new DirectoryInfo(this.FromDir);
				if (this.m_sourceDirWatcher == null)
				{
					createNewWatcher = true;
				}
				else if (this.m_sourceDirWatcher.Path != this.FromDir || this.m_sourceDirWatcher.Filter != this.FromPrefix + '*' + this.FromSuffix)
				{
					this.m_sourceDirWatcher.Dispose();
					createNewWatcher = true;
				}
				if (createNewWatcher)
				{
					this.m_sourceDirWatcher = new WatcherState();
					this.m_sourceDirWatcher.M_thisShip = this;
					this.m_sourceDirWatcher.Path = this.FromDir;
					this.m_sourceDirWatcher.Filter = this.FromPrefix + '*' + this.FromSuffix;
					this.m_sourceDirWatcher.NotifyFilter = (NotifyFilters.FileName | NotifyFilters.LastWrite);
					this.m_sourceDirWatcher.Created += ShipControl.ShipDirectoryChange;
					this.m_sourceDirWatcher.Renamed += new RenamedEventHandler(ShipControl.ShipDirectoryChange);
					this.m_sourceDirWatcher.EnableRaisingEvents = true;
				}
				lowestSource = ShipControl.LowestGenerationInDirectory(di, this.FromPrefix, this.FromSuffix, false);
				if (lowestSource == 0L)
				{
					fNoFileFound = true;
					result = Result.LongWaitRetry;
					return;
				}
				if (this.m_fromNumber == 0L)
				{
					this.m_fromNumber = lowestSource;
				}
				else if (this.m_fromNumber < lowestSource)
				{
					result = Result.GiveUp;
					this.m_setBroken.SetBroken(this.TagUsedOnLogGapDetected, ReplayEventLogConstants.Tuple_LogFileGapFound, new string[]
					{
						this.m_fromNumber.ToString()
					});
				}
				if (!this.m_derivedInitialized)
				{
					result = this.InitializeStartContext();
					if (result == Result.Success)
					{
						this.m_derivedInitialized = true;
					}
				}
			});
			if (result != Result.Success && !fNoFileFound)
			{
				ExTraceGlobals.ShipLogTracer.TraceError<Result, Exception>((long)this.GetHashCode(), "ShipRestartFindFirst failed, result={0}:{1}", result, this.m_lastException);
			}
			return result;
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x00083BE0 File Offset: 0x00081DE0
		protected virtual void CheckForGaps(long fromNumber)
		{
			if (this.GapsAreAcceptable())
			{
				ExTraceGlobals.ShipLogTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: CheckForGaps() is triggering ShipRestartFindFirst().", this.m_className);
				this.m_initialized = false;
				return;
			}
			new DirectoryInfo(this.FromDir);
			this.m_countdownToGapTest = 6L;
			bool flag = false;
			string path = Path.Combine(this.FromDir, this.GetFilenameFromGenerationNumber(fromNumber));
			long num = 1L;
			while (num <= this.MaxGapSize)
			{
				string path2 = Path.Combine(this.FromDir, this.GetFilenameFromGenerationNumber(fromNumber + num));
				if (File.Exists(path2))
				{
					if (File.Exists(path))
					{
						ExTraceGlobals.ShipLogTracer.TraceDebug<long>((long)this.GetHashCode(), "CheckForGaps() shouldn't be called since fromNumber {0} generation is there.", fromNumber);
						return;
					}
					flag = true;
					break;
				}
				else
				{
					num += 1L;
				}
			}
			if (flag)
			{
				string text = Path.Combine(this.FromDir, this.FromPrefix + ShipControl.GenerationString(fromNumber) + this.FromSuffix);
				ExTraceGlobals.ShipLogTracer.TraceError<string>((long)this.GetHashCode(), "Gap in log file generations after file {0}", text);
				this.m_setBroken.SetBroken(this.TagUsedOnLogGapDetected, ReplayEventLogConstants.Tuple_LogFileGapFound, new string[]
				{
					text
				});
			}
		}

		// Token: 0x04000C14 RID: 3092
		public const int ERROR_SHARING_VIOLATION = 32;

		// Token: 0x04000C15 RID: 3093
		protected const int ShortWait = 1000;

		// Token: 0x04000C16 RID: 3094
		protected const int LongWait = 10000;

		// Token: 0x04000C17 RID: 3095
		protected const long PeriodForGapTest = 6L;

		// Token: 0x04000C18 RID: 3096
		private const int MaxTotalWait = 30000;

		// Token: 0x04000C19 RID: 3097
		protected bool m_derivedInitialized;

		// Token: 0x04000C1A RID: 3098
		protected volatile bool m_fAttemptFinalCopyCalled;

		// Token: 0x04000C1B RID: 3099
		protected Exception m_lastException;

		// Token: 0x04000C1C RID: 3100
		protected string m_className;

		// Token: 0x04000C1D RID: 3101
		protected ShipLogsSetBroken m_shipLogsSetBroken;

		// Token: 0x04000C1E RID: 3102
		protected ISetBroken m_setBroken;

		// Token: 0x04000C1F RID: 3103
		protected IReplicaProgress m_replicaProgress;

		// Token: 0x04000C20 RID: 3104
		private readonly string m_fromPrefix;

		// Token: 0x04000C21 RID: 3105
		private readonly string m_fromSuffix;

		// Token: 0x04000C22 RID: 3106
		private string m_fromDir;

		// Token: 0x04000C23 RID: 3107
		private long m_fromNumber;

		// Token: 0x04000C24 RID: 3108
		private bool m_initialized;

		// Token: 0x04000C25 RID: 3109
		private bool m_active;

		// Token: 0x04000C26 RID: 3110
		private bool m_notified;

		// Token: 0x04000C27 RID: 3111
		private volatile bool m_prepareToStopCalled;

		// Token: 0x04000C28 RID: 3112
		private WatcherState m_sourceDirWatcher;

		// Token: 0x04000C29 RID: 3113
		private TimerState m_wakeTimer;

		// Token: 0x04000C2A RID: 3114
		private int m_lastWait;

		// Token: 0x04000C2B RID: 3115
		private ManualResetEvent m_stopEvent;

		// Token: 0x04000C2C RID: 3116
		private ManualResetEvent m_goingIdleEvent;

		// Token: 0x04000C2D RID: 3117
		private Result m_lastShipLogsResult;

		// Token: 0x04000C2E RID: 3118
		protected long m_countdownToGapTest;

		// Token: 0x04000C2F RID: 3119
		private bool m_fStopped;

		// Token: 0x04000C30 RID: 3120
		private int m_totalWait;

		// Token: 0x020002DD RID: 733
		// (Invoke) Token: 0x06001D38 RID: 7480
		protected delegate void FileIoOperation();
	}
}
