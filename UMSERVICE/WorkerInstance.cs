using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Exchange.UM.UMService.Exceptions;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.UM.UMService
{
	// Token: 0x02000010 RID: 16
	internal class WorkerInstance
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00005554 File Offset: 0x00003754
		internal WorkerInstance(string pathName, bool passive, WorkerInstance.WorkerContacted workerContactedDelegate, WorkerInstance.WorkerExited workerExitedDelegate, SafeJobHandle jobObject, int thrashCrashCount, int workerSipPort, int tcpPort)
		{
			ProcessLog.WriteLine("WorkerIntance::Ctor", new object[0]);
			this.StopEvent = new ManualResetEvent(false);
			this.workerContactedDelegate = workerContactedDelegate;
			this.workerExitedDelegate = workerExitedDelegate;
			this.workerIsActive = !passive;
			this.thrashCrashCount = thrashCrashCount;
			string text = null;
			this.stopHandle = WorkerInstance.CreateNamedSemaphore("Global\\ExchangeUMStopKey-", out text);
			if (this.stopHandle == null)
			{
				throw new UMServiceBaseException(Strings.StopKeyFailed);
			}
			string text2 = null;
			this.resetHandle = WorkerInstance.CreateNamedSemaphore("Global\\ExchangeUMResetKey-", out text2);
			if (this.resetHandle == null)
			{
				throw new UMServiceBaseException(Strings.ResetKeyFailed);
			}
			string text3 = null;
			this.fatalHandle = WorkerInstance.CreateNamedSemaphore("Global\\ExchangeUMFatalKey-", out text3);
			if (this.fatalHandle == null)
			{
				throw new UMServiceBaseException(Strings.FatalKeyFailed);
			}
			string text4 = null;
			this.readyHandle = WorkerInstance.CreateNamedSemaphore("Global\\ExchangeUMReadyKey-", out text4);
			if (this.readyHandle == null)
			{
				throw new UMServiceBaseException(Strings.ReadyKeyFailed);
			}
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.FileName = pathName;
			processStartInfo.CreateNoWindow = true;
			processStartInfo.UseShellExecute = false;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.RedirectStandardOutput = true;
			this.port = tcpPort;
			this.SipPort = workerSipPort;
			StringBuilder stringBuilder = new StringBuilder("-port:{0} ");
			stringBuilder.Append("-stopkey:{1} ");
			stringBuilder.Append("-resetkey:{2} ");
			stringBuilder.Append("-fatalkey:{3} ");
			stringBuilder.Append("-readykey:{4} ");
			stringBuilder.Append("-tempdir:{5} ");
			stringBuilder.Append("-sipport:{6} ");
			stringBuilder.Append("-perfenabled:{7} ");
			stringBuilder.Append("-startupMode:{8} ");
			processStartInfo.Arguments = string.Format(stringBuilder.ToString(), new object[]
			{
				this.port,
				text,
				text2,
				text3,
				text4,
				UMRecyclerConfig.Tempdir,
				workerSipPort,
				UmServiceGlobals.ArePerfCountersEnabled ? 1 : 0,
				UMRecyclerConfig.UMStartupType.ToString()
			});
			if (UMRecyclerConfig.UMStartupType != UMStartupMode.TCP)
			{
				ProcessStartInfo processStartInfo2 = processStartInfo;
				processStartInfo2.Arguments += string.Format(CultureInfo.InvariantCulture, "-thumbprint:{0} ", new object[]
				{
					CertificateUtils.UMCertificate.Thumbprint
				});
			}
			if (passive)
			{
				ProcessStartInfo processStartInfo3 = processStartInfo;
				processStartInfo3.Arguments += "-passive";
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Worker thrashcount={0} commandline = {1}", new object[]
			{
				thrashCrashCount,
				processStartInfo.Arguments
			});
			this.process = new Process();
			this.process.StartInfo = processStartInfo;
			this.process.EnableRaisingEvents = true;
			this.process.Exited += this.OnExited;
			this.startTime = ExDateTime.UtcNow;
			if (!ThreadPool.QueueUserWorkItem(new WaitCallback(this.MonitorResetHandle)))
			{
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "Could not start MonitorResethandle Thread ", new object[0]);
				throw new UMServiceBaseException(Strings.ResetThreadStartFailed);
			}
			if (!ThreadPool.QueueUserWorkItem(new WaitCallback(this.WaitForProcessReady)))
			{
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "Could not start MonitorReadyHandle Thread ", new object[0]);
				throw new UMServiceBaseException(Strings.ReadyThreadStartFailed);
			}
			if (!this.process.Start())
			{
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "Failed to start worker process", new object[0]);
				throw new UMServiceBaseException(Strings.WorkerProcessStartFailed);
			}
			this.pid = this.process.Id;
			if (!jobObject.IsInvalid && !jobObject.Add(this.process))
			{
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "AssignProcessToJobObject() failed", new object[0]);
				throw new UMServiceBaseException(Strings.AssignProcessToJobObjectFailed);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00005960 File Offset: 0x00003B60
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00005968 File Offset: 0x00003B68
		internal ManualResetEvent StopEvent
		{
			get
			{
				return this.stopEvent;
			}
			set
			{
				this.stopEvent = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00005971 File Offset: 0x00003B71
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00005979 File Offset: 0x00003B79
		internal int SipPort
		{
			get
			{
				return this.sipPort;
			}
			set
			{
				this.sipPort = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00005982 File Offset: 0x00003B82
		// (set) Token: 0x0600008A RID: 138 RVA: 0x0000598A File Offset: 0x00003B8A
		internal int NumCallsHandled
		{
			get
			{
				return this.numcallsHandled;
			}
			set
			{
				this.numcallsHandled = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00005993 File Offset: 0x00003B93
		internal int Pid
		{
			get
			{
				return this.pid;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000599B File Offset: 0x00003B9B
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000059A3 File Offset: 0x00003BA3
		internal bool IsActive
		{
			get
			{
				return this.workerIsActive;
			}
			set
			{
				this.workerIsActive = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000059AC File Offset: 0x00003BAC
		internal bool IsConnected
		{
			get
			{
				return this.socketConnected;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000059B4 File Offset: 0x00003BB4
		internal Process Process
		{
			get
			{
				return this.process;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000059BC File Offset: 0x00003BBC
		internal bool ResetRequested
		{
			get
			{
				return this.resetRequested;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000059C4 File Offset: 0x00003BC4
		// (set) Token: 0x06000092 RID: 146 RVA: 0x000059CC File Offset: 0x00003BCC
		internal bool RequestRecycleWithWatson
		{
			get
			{
				return this.requestRecycleWithWatson;
			}
			set
			{
				this.requestRecycleWithWatson = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000059D5 File Offset: 0x00003BD5
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000059DD File Offset: 0x00003BDD
		internal int ThrashCrashCount
		{
			get
			{
				return this.thrashCrashCount;
			}
			set
			{
				this.thrashCrashCount = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000059E6 File Offset: 0x00003BE6
		// (set) Token: 0x06000096 RID: 150 RVA: 0x000059EE File Offset: 0x00003BEE
		internal ExDateTime StartTime
		{
			get
			{
				return this.startTime;
			}
			set
			{
				this.startTime = value;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000059F8 File Offset: 0x00003BF8
		internal void StopControlSocket()
		{
			Socket socket = this.controlSocket;
			if (socket != null)
			{
				try
				{
					this.controlSocket.Close();
				}
				catch (SocketException ex)
				{
					CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "In WorkerInstance Stop, error={0}", new object[]
					{
						ex.ToString()
					});
				}
				catch (ObjectDisposedException ex2)
				{
					CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "In WorkerInstance Stop, error={0}", new object[]
					{
						ex2.ToString()
					});
				}
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005A90 File Offset: 0x00003C90
		internal void Stop()
		{
			ProcessLog.WriteLine("WorkerIntance::Stop", new object[0]);
			if (!this.stopping)
			{
				this.stopping = true;
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Stop worker instance (pid={0})", new object[]
				{
					this.pid
				});
				this.SignalStop();
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005AED File Offset: 0x00003CED
		internal void SignalStop()
		{
			ProcessLog.WriteLine("WorkerIntance::SignalStop", new object[0]);
			Utils.SafelyReleaseAndCloseNamedSemaphore(this.stopHandle);
			this.stopHandle = null;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005B14 File Offset: 0x00003D14
		internal void WatsonAndCreateNew(UMServiceBaseException umex)
		{
			ProcessLog.WriteLine("WorkerIntance::WatsonAndCreateNew", new object[0]);
			string data = string.Empty;
			if (InstrumentationCollector.CurrentStrategy != null)
			{
				data = InstrumentationCollector.CurrentStrategy.GetSystemInformation();
				ProcessLog.WriteLine("Took System Snap Shot", new object[0]);
			}
			Process process = this.process;
			this.CallWorkerExitedDelegate(true, false);
			try
			{
				if (!process.HasExited)
				{
					using (ITempFile tempFile = Breadcrumbs.GenerateDump())
					{
						using (ITempFile tempFile2 = ProcessLog.GenerateDump())
						{
							ExWatson.TryAddExtraFile(tempFile.FilePath);
							ExWatson.TryAddExtraFile(tempFile2.FilePath);
							ExWatson.AddExtraData(data);
							ExWatson.SendHangWatsonReport(umex, process);
							Utils.KillProcess(process);
						}
					}
				}
			}
			catch (Win32Exception ex)
			{
				ProcessLog.WriteLine("WorkerIntance kill failed! '{0}'", new object[]
				{
					ex
				});
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "In KillHangingProcess - encountered at exception = ", new object[]
				{
					ex.ToString() + "Dont care about the Process exiting. Hence ignoring the exception"
				});
			}
			catch (InvalidOperationException ex2)
			{
				ProcessLog.WriteLine("WorkerIntance kill failed! '{0}'", new object[]
				{
					ex2
				});
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "In KillHangingProcess - encountered at exception = ", new object[]
				{
					ex2.ToString() + "Dont care about the Process exiting. Hence ignoring the exception"
				});
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005C9C File Offset: 0x00003E9C
		internal void SetNumCalls(int numcalls)
		{
			Interlocked.Exchange(ref this.numcallsHandled, numcalls);
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "SetNumCalls: numcalls={0}, numcallsHandled={1}", new object[]
			{
				numcalls,
				this.numcallsHandled
			});
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005CEC File Offset: 0x00003EEC
		internal int ReadMessageFromWorkerProcess(byte[] buffer, int count, int timeout, out SocketError socketError)
		{
			int num = 0;
			socketError = SocketError.SocketError;
			if (this.socketConnected && this.controlSocket != null)
			{
				try
				{
					this.controlSocket.ReceiveTimeout = timeout;
					if ((num = this.controlSocket.Receive(buffer, 0, count, SocketFlags.None, out socketError)) != 0)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Message recvd from worker process of {0} bytes", new object[]
						{
							num
						});
					}
					else
					{
						ProcessLog.WriteLine("Heartbeat message not received. Socket Error={0}", new object[]
						{
							socketError
						});
						CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "No message recvd from worker process. SocketError={0}", new object[]
						{
							socketError
						});
					}
				}
				catch (SocketException ex)
				{
					ProcessLog.WriteLine("Heartbeat message not received. Socket Error ={0}", new object[]
					{
						ex.Message
					});
					CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "In WorkerInstance ReadMessageFromWorkerProcess, error={0}, SocketError={1}", new object[]
					{
						ex.ToString(),
						ex.SocketErrorCode
					});
					socketError = ex.SocketErrorCode;
				}
				catch (ObjectDisposedException ex2)
				{
					CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "In WorkerInstance ReadMessageFromWorkerProcess, error={0}", new object[]
					{
						ex2.ToString()
					});
				}
			}
			return num;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005E5C File Offset: 0x0000405C
		internal SocketError SendMessageToWorkerProcess(byte[] buffer, int offset, int count, bool isHeartBeat)
		{
			SocketError socketError = SocketError.SocketError;
			if (this.socketConnected && this.controlSocket != null)
			{
				try
				{
					int num = this.controlSocket.Send(buffer, offset, count, SocketFlags.None, out socketError);
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Message sent to worker process: {0} bytes sent {1} bytes requested to be sent", new object[]
					{
						num,
						count
					});
				}
				catch (SocketException ex)
				{
					CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "Socket exception while sending message, errorcode={0}, error={1}", new object[]
					{
						ex.SocketErrorCode,
						ex.ToString()
					});
					socketError = ex.SocketErrorCode;
				}
				catch (ObjectDisposedException ex2)
				{
					CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "ObjectDisposed exception while sending message, error={0}", new object[]
					{
						ex2.ToString()
					});
				}
				if (socketError != SocketError.Success && !isHeartBeat)
				{
					this.socketConnected = false;
					this.controlSocket.Close();
				}
			}
			return socketError;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005F70 File Offset: 0x00004170
		internal void MonitorResetHandle(object stateinfo)
		{
			ProcessLog.WriteLine("WorkerIntance::MonitorResetHandle", new object[0]);
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "MonitorResetHandle for process (pid={0})", new object[]
			{
				this.pid
			});
			try
			{
				this.resetHandle.WaitOne();
				this.resetRequested = true;
				ProcessLog.WriteLine("MonitorResetHandle: Success.", new object[0]);
				Semaphore semaphore = Interlocked.Exchange<Semaphore>(ref this.resetHandle, null);
				if (semaphore != null)
				{
					semaphore.Close();
				}
				if (!this.stopping)
				{
					this.stopping = true;
					semaphore = Interlocked.Exchange<Semaphore>(ref this.readyHandle, null);
					Utils.SafelyReleaseAndCloseNamedSemaphore(semaphore);
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessRequestedRecycle, null, new object[0]);
					this.CallWorkerExitedDelegate(true, false);
				}
			}
			catch (NullReferenceException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "ResetHandle was null when tried to execute WaitOne in MonitorResetHandle for pid={0}, error = {1}", new object[]
				{
					this.pid,
					ex.ToString()
				});
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006080 File Offset: 0x00004280
		internal bool AnalyzeRecycleParams(out bool recycleWithRequestingWatson)
		{
			recycleWithRequestingWatson = false;
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "in AnalyzeRecycleParams", new object[0]);
			if (!this.CheckRecycleIntervalOkay())
			{
				ProcessLog.WriteLine("WorkerInstance recycle interval expired.  Returning true.", new object[0]);
				return true;
			}
			int callCount = Interlocked.Exchange(ref this.numcallsHandled, this.numcallsHandled);
			if (!this.CheckNumCallsOkay(callCount))
			{
				ProcessLog.WriteLine("WorkerInstance num calls exceeded.  Returning true.", new object[0]);
				return true;
			}
			Process process = null;
			try
			{
				process = Process.GetProcessById(this.pid);
			}
			catch (ArgumentException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "In WorkerInstance AnalyzeRecycleParams, error={0}", new object[]
				{
					ex.ToString()
				});
			}
			if (process == null)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessNoProcessData, null, new object[0]);
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Process {0}: failed to retrieve process data, so recycle", new object[]
				{
					this.pid
				});
				ProcessLog.WriteLine("WorkerInstance failed to get process data.  Returning true.", new object[0]);
				return true;
			}
			this.process = process;
			if (!this.CheckProcessMemoryPressure())
			{
				ProcessLog.WriteLine("WorkerInstance exceeded memory pressure.  Returning true.", new object[0]);
				recycleWithRequestingWatson = true;
				return true;
			}
			if (!this.CheckTempDirSizeOkay())
			{
				ProcessLog.WriteLine("WorkerInstance temp dir too large.  Returning true.", new object[0]);
				return true;
			}
			if (!HealthCheckPipelineContext.IsPipelineHealthy())
			{
				ProcessLog.WriteLine("WorkerInstance pipeline queue is unhealthy. Returning true.", new object[0]);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_PipelineStalled, Utils.VoiceMailFilePath, new object[]
				{
					".HealthCheck"
				});
				recycleWithRequestingWatson = AppConfig.Instance.Service.EnableWatsonOnPipelineStall;
				return true;
			}
			return false;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00006228 File Offset: 0x00004428
		internal void WaitForProcessReady(object stateinfo)
		{
			ProcessLog.WriteLine("WorkerInstance::WaitForProcessReady", new object[0]);
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Wait for process (pid={0}) to signal ready", new object[]
			{
				this.pid
			});
			try
			{
				int num = WaitHandle.WaitAny(new WaitHandle[]
				{
					this.fatalHandle,
					this.readyHandle
				}, 1000 * UMRecyclerConfig.StartupTime, false);
				if (num == 258)
				{
					ProcessLog.WriteLine("WaitForProcessReady: Timed out.", new object[0]);
					Semaphore semaphore = Interlocked.Exchange<Semaphore>(ref this.readyHandle, null);
					if (semaphore != null)
					{
						semaphore.Close();
					}
					Semaphore semaphore2 = Interlocked.Exchange<Semaphore>(ref this.fatalHandle, null);
					if (semaphore2 != null)
					{
						semaphore2.Close();
					}
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_KilledMaxStartuptimeExceeded, null, new object[]
					{
						UMRecyclerConfig.StartupTime
					});
					this.WatsonAndCreateNew(new UMWorkerStartTimeoutException(UMRecyclerConfig.StartupTime));
				}
				else if (num == 1)
				{
					Semaphore semaphore3 = Interlocked.Exchange<Semaphore>(ref this.readyHandle, null);
					if (semaphore3 != null)
					{
						semaphore3.Close();
					}
					Semaphore semaphore4 = Interlocked.Exchange<Semaphore>(ref this.fatalHandle, null);
					if (semaphore4 != null)
					{
						semaphore4.Close();
					}
					if (!this.stopping)
					{
						ProcessLog.WriteLine("WaitForProcessReady: Success.", new object[0]);
						CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Process (pid={0}) signaled ready", new object[]
						{
							this.pid
						});
						string text;
						if (!this.StartConnecting(out text))
						{
							CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "Process (pid={0}) could not establish control channel. Killing it !", new object[]
							{
								this.pid
							});
							UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_KilledCouldntEstablishControlChannel, null, new object[]
							{
								text
							});
							ProcessLog.WriteLine("WaitForProcessReady: Failed to establish control channel.", new object[0]);
							this.WatsonAndCreateNew(new UMServiceControlChannelException(this.port, text));
						}
						else if (this.workerContactedDelegate != null)
						{
							this.workerContactedDelegate(this);
						}
					}
				}
				else if (num == 0)
				{
					Semaphore semaphore5 = Interlocked.Exchange<Semaphore>(ref this.readyHandle, null);
					if (semaphore5 != null)
					{
						semaphore5.Close();
					}
					Semaphore semaphore6 = Interlocked.Exchange<Semaphore>(ref this.fatalHandle, null);
					if (semaphore6 != null)
					{
						semaphore6.Close();
					}
					if (!this.stopping)
					{
						this.stopping = true;
						Semaphore semaphore7 = Interlocked.Exchange<Semaphore>(ref this.resetHandle, null);
						Utils.SafelyReleaseAndCloseNamedSemaphore(semaphore7);
						this.CallWorkerExitedDelegate(false, true);
						ProcessLog.WriteLine("WaitForProcessReady: Fatal.", new object[0]);
						CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Process (pid={0}) signaled fatal", new object[]
						{
							this.pid
						});
					}
				}
			}
			catch (ArgumentNullException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "One of the arguments to Semaphore wait in WaitForProcessReady is null, err={0}", new object[]
				{
					ex.ToString()
				});
				ProcessLog.WriteLine("WaitForProcessReady: Argument Error.", new object[0]);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000655C File Offset: 0x0000475C
		private static ulong SafeGetFileSize(FileInfo fi)
		{
			ulong result = 0UL;
			try
			{
				result = (ulong)fi.Length;
			}
			catch (IOException)
			{
			}
			return result;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000658C File Offset: 0x0000478C
		private static Semaphore CreateNamedSemaphore(string prefix, out string name)
		{
			bool flag = false;
			Semaphore semaphore = null;
			int num = 0;
			name = null;
			while (!flag && num++ < 10)
			{
				if (flag && semaphore != null)
				{
					semaphore.Dispose();
				}
				name = prefix + Guid.NewGuid();
				semaphore = new Semaphore(0, 1, name, ref flag);
			}
			if (!flag)
			{
				return null;
			}
			return semaphore;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000065E0 File Offset: 0x000047E0
		private bool CheckNumCallsOkay(int callCount)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "in CheckNumCallsOkay", new object[0]);
			if (UMRecyclerConfig.MaxCallsBeforeRecycle == 0)
			{
				return true;
			}
			if (callCount >= UMRecyclerConfig.MaxCallsBeforeRecycle)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_RecycledMaxCallsExceeded, null, new object[]
				{
					callCount,
					UMRecyclerConfig.MaxCallsBeforeRecycle
				});
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Process {0}: 'CallCount' exceeded: callcount={1}, maxAllowed={2}", new object[]
				{
					this.pid,
					callCount,
					UMRecyclerConfig.MaxCallsBeforeRecycle
				});
				return false;
			}
			return true;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000668C File Offset: 0x0000488C
		private bool CheckProcessMemoryPressure()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "in CheckProcessMemoryPressure", new object[0]);
			if (UMRecyclerConfig.MaxPrivateBytesPercent == 0.0)
			{
				return true;
			}
			bool result = true;
			try
			{
				double num;
				if (MemoryMonitorHelperMethods.IsRecommendedMemoryPressureExceeded(this.process.Handle, UMRecyclerConfig.MaxPrivateBytesPercent, out num))
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_RecycledMaxMemoryPressureExceeded, null, new object[]
					{
						num,
						UMRecyclerConfig.MaxPrivateBytesPercent
					});
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Process {0}: threshold 'MaxPrivateBytesPercent' exceeded: currentPercent={1}, MaxPrivateBytesPercent=={2}", new object[]
					{
						this.pid,
						num,
						UMRecyclerConfig.MaxPrivateBytesPercent
					});
					result = false;
				}
			}
			catch (SystemException)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessNoProcessData, null, new object[0]);
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Process {0}: failed to retrieve process data, so recycle", new object[]
				{
					this.pid
				});
				result = false;
			}
			return result;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000067B0 File Offset: 0x000049B0
		private bool CheckRecycleIntervalOkay()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "in CheckRecycleIntervalOkay", new object[0]);
			if (UMRecyclerConfig.RecycleInterval == 0UL)
			{
				return true;
			}
			ExDateTime utcNow = ExDateTime.UtcNow;
			ulong recycleInterval = UMRecyclerConfig.RecycleInterval;
			ExDateTime exDateTime = this.startTime.AddSeconds(recycleInterval);
			if (exDateTime > utcNow)
			{
				return true;
			}
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_RecycledMaxUptimeExceeded, null, new object[]
			{
				this.startTime,
				exDateTime
			});
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Process {0}: threshold 'RecycleInterval' exceeded: startTime={1}, retireTime=={2}, now={3}", new object[]
			{
				this.pid,
				this.startTime,
				exDateTime,
				utcNow
			});
			return false;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000688C File Offset: 0x00004A8C
		private ulong DirSize(DirectoryInfo d)
		{
			ulong num = 0UL;
			FileInfo[] files = d.GetFiles();
			foreach (FileInfo fi in files)
			{
				num += WorkerInstance.SafeGetFileSize(fi);
			}
			DirectoryInfo[] directories = d.GetDirectories();
			foreach (DirectoryInfo d2 in directories)
			{
				num += this.DirSize(d2);
			}
			return num / 1048576UL;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00006900 File Offset: 0x00004B00
		private bool CheckTempDirSizeOkay()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "in CheckTempDirSizeOkay", new object[0]);
			if (UMRecyclerConfig.MaxTempDirSize == 0UL)
			{
				return true;
			}
			DirectoryInfo d = new DirectoryInfo(UMRecyclerConfig.TempFilePath);
			ulong num = this.DirSize(d);
			if (num < UMRecyclerConfig.MaxTempDirSize)
			{
				return true;
			}
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_RecycledMaxTempDirSizeExceeded, null, new object[]
			{
				num,
				UMRecyclerConfig.MaxTempDirSize
			});
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Process {0}: threshold 'MaxTempDirSize' exceeded: tempDirSize={1}, MaxTempDirSize=={2}", new object[]
			{
				this.pid,
				num,
				UMRecyclerConfig.MaxTempDirSize
			});
			return false;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000069C1 File Offset: 0x00004BC1
		private void CallWorkerExitedDelegate(bool resetRequested, bool fatalError)
		{
			this.workerExitedDelegate(this, resetRequested, fatalError);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000069D4 File Offset: 0x00004BD4
		private void OnExited(object sender, EventArgs e)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Process {0} Exited", new object[]
			{
				this.pid
			});
			this.stopping = true;
			Semaphore semaphore = Interlocked.Exchange<Semaphore>(ref this.readyHandle, null);
			Utils.SafelyReleaseAndCloseNamedSemaphore(semaphore);
			semaphore = Interlocked.Exchange<Semaphore>(ref this.resetHandle, null);
			Utils.SafelyReleaseAndCloseNamedSemaphore(semaphore);
			this.StopEvent.Set();
			this.CallWorkerExitedDelegate(false, false);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006A50 File Offset: 0x00004C50
		private bool StartConnecting(out string error)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "StartConnecting: pid={0}", new object[]
			{
				this.pid
			});
			error = null;
			try
			{
				IPAddress loopbackControlIPAddress = Utils.GetLoopbackControlIPAddress();
				this.controlSocket = new Socket(loopbackControlIPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				this.controlSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);
				this.controlSocket.Connect(new IPEndPoint(loopbackControlIPAddress, this.port));
				this.socketConnected = true;
			}
			catch (SocketException ex)
			{
				error = ex.Message;
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "Socket exception in StartConnecting, errorcode={0}, error={1}", new object[]
				{
					ex.SocketErrorCode,
					ex.ToString()
				});
			}
			catch (ObjectDisposedException ex2)
			{
				error = ex2.Message;
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "ObjectDisposed exception in StartConnecting, error={1}", new object[]
				{
					ex2.ToString()
				});
			}
			return error == null;
		}

		// Token: 0x0400002F RID: 47
		private readonly int pid;

		// Token: 0x04000030 RID: 48
		private readonly int port;

		// Token: 0x04000031 RID: 49
		private int numcallsHandled;

		// Token: 0x04000032 RID: 50
		private int sipPort;

		// Token: 0x04000033 RID: 51
		private ManualResetEvent stopEvent;

		// Token: 0x04000034 RID: 52
		private Process process;

		// Token: 0x04000035 RID: 53
		private ExDateTime startTime;

		// Token: 0x04000036 RID: 54
		private int thrashCrashCount;

		// Token: 0x04000037 RID: 55
		private Semaphore stopHandle;

		// Token: 0x04000038 RID: 56
		private Semaphore resetHandle;

		// Token: 0x04000039 RID: 57
		private Semaphore readyHandle;

		// Token: 0x0400003A RID: 58
		private Semaphore fatalHandle;

		// Token: 0x0400003B RID: 59
		private Socket controlSocket;

		// Token: 0x0400003C RID: 60
		private bool workerIsActive;

		// Token: 0x0400003D RID: 61
		private bool resetRequested;

		// Token: 0x0400003E RID: 62
		private bool socketConnected;

		// Token: 0x0400003F RID: 63
		private bool stopping;

		// Token: 0x04000040 RID: 64
		private bool requestRecycleWithWatson;

		// Token: 0x04000041 RID: 65
		private WorkerInstance.WorkerContacted workerContactedDelegate;

		// Token: 0x04000042 RID: 66
		private WorkerInstance.WorkerExited workerExitedDelegate;

		// Token: 0x02000011 RID: 17
		// (Invoke) Token: 0x060000AC RID: 172
		internal delegate void WorkerContacted(WorkerInstance workerInstance);

		// Token: 0x02000012 RID: 18
		// (Invoke) Token: 0x060000B0 RID: 176
		internal delegate void WorkerExited(WorkerInstance workerInstance, bool resetRequested, bool fatalError);
	}
}
