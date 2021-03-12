using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Exchange.UM.UMService.Exceptions;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.UM.UMService
{
	// Token: 0x02000013 RID: 19
	internal class WorkerProcessManager
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00006B74 File Offset: 0x00004D74
		internal WorkerProcessManager(string workerPath, SafeJobHandle jobObject)
		{
			this.jobObject = jobObject;
			this.workerPath = workerPath;
			this.availablePorts.Enqueue(UMRecyclerConfig.Worker1SipPortNumber);
			this.availablePorts.Enqueue(UMRecyclerConfig.Worker2SipPortNumber);
			this.isServiceStartup = true;
			this.wpstarted = new ManualResetEvent(false);
			this.wpfatalError = new ManualResetEvent(false);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00006BFE File Offset: 0x00004DFE
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00006C06 File Offset: 0x00004E06
		internal Timer HeartbeatTimer
		{
			get
			{
				return this.heartbeatTimer;
			}
			set
			{
				this.heartbeatTimer = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00006C0F File Offset: 0x00004E0F
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00006C17 File Offset: 0x00004E17
		internal Timer ResourceTimer
		{
			get
			{
				return this.resourceTimer;
			}
			set
			{
				this.resourceTimer = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00006C20 File Offset: 0x00004E20
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00006C28 File Offset: 0x00004E28
		internal Timer PipelinHealthCheckTimer
		{
			get
			{
				return this.pipelineHealthCheckTimer;
			}
			set
			{
				this.pipelineHealthCheckTimer = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00006C31 File Offset: 0x00004E31
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00006C39 File Offset: 0x00004E39
		internal ManualResetEvent WpStarted
		{
			get
			{
				return this.wpstarted;
			}
			set
			{
				this.wpstarted = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00006C42 File Offset: 0x00004E42
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00006C4A File Offset: 0x00004E4A
		internal ManualResetEvent WpFatalError
		{
			get
			{
				return this.wpfatalError;
			}
			set
			{
				this.wpfatalError = value;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00006C54 File Offset: 0x00004E54
		internal static void Create(string workerPath, SafeJobHandle jobObject, out WorkerProcessManager manager)
		{
			manager = null;
			if (!File.Exists(workerPath))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "Worker path doesn't exists, path={0}", new object[]
				{
					workerPath
				});
				throw new UMServiceBaseException(Strings.UMInvalidWorkerProcessPath((workerPath == null) ? string.Empty : workerPath));
			}
			manager = new WorkerProcessManager(workerPath, jobObject);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006CB4 File Offset: 0x00004EB4
		internal void GetWorkerPort(out int sipPort, out int tcpPort)
		{
			lock (this)
			{
				sipPort = ((this.availablePorts.Count > 0) ? this.availablePorts.Dequeue() : 0);
				tcpPort = this.portCurrent;
				if (++this.portCurrent > 17000)
				{
					this.portCurrent = 16000;
				}
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00006D34 File Offset: 0x00004F34
		internal void RetireTimeout(object stateinfo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "WorkerProcessManager in RetireTimeout", new object[0]);
			lock (this)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "WorkerProcessManager in RetireTimeout - got the lock", new object[0]);
				if (this.retiredWorker != null && this.retireTimer != null)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_KilledMaxRetireTimeExceeded, null, new object[0]);
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Killed retiring worker process - exceeded retire time", new object[0]);
					Utils.KillProcess(this.retiredWorker.Process);
				}
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00006DF4 File Offset: 0x00004FF4
		internal void WatsonWorkerProcessDueToTimeout()
		{
			lock (this)
			{
				if (this.activeWorker != null)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Sending signal to Watson workerprocess", new object[0]);
					SocketError socketError = WorkerProcessManager.SendMessage(this.activeWorker, WorkerProcessManager.commandWatsonDueToTimeout, 0, WorkerProcessManager.commandWatsonDueToTimeout.Length, false);
					if (socketError != SocketError.Success)
					{
						CallIdTracer.TraceWarning(ExTraceGlobals.ServiceTracer, 0, "Unsuccess in signaling to Watson workerprocess", new object[0]);
					}
				}
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00006E84 File Offset: 0x00005084
		internal void MonitorHeartBeat(object stateinfo)
		{
			bool flag = false;
			byte[] array = new byte[10];
			WorkerInstance workerInstance = null;
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "in MonitorHeartBeat", new object[0]);
			lock (this)
			{
				if (this.state == WorkerProcessManager.States.Ready && this.activeWorker != null && this.activeWorker.IsConnected)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Monitoring Heartbeat", new object[0]);
					SocketError socketError = WorkerProcessManager.SendMessage(this.activeWorker, WorkerProcessManager.commandHeartBeat, 0, WorkerProcessManager.commandHeartBeat.Length, true);
					if (socketError != SocketError.Success)
					{
						this.numHeartBeatFailures++;
						ProcessLog.WriteLine("Heartbeat message could not be sent. Socket Error={0}", new object[]
						{
							socketError
						});
						CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Failed to send Heartbeat. SocketError={0}", new object[]
						{
							socketError
						});
						if (this.numHeartBeatFailures >= UMRecyclerConfig.MaxHeartBeatFailures)
						{
							UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_RecycledMaxHeartBeatsMissedExceeded, null, new object[]
							{
								this.numHeartBeatFailures,
								UMRecyclerConfig.MaxHeartBeatFailures
							});
							CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Forked worker process - exceeded maxheartbeatmissed limit, has={0}, limit={1}", new object[]
							{
								this.numHeartBeatFailures,
								UMRecyclerConfig.MaxHeartBeatFailures
							});
							this.numHeartBeatFailures = 0;
							this.activeWorker.WatsonAndCreateNew(new UMServiceHeartbeatException(string.Format(CultureInfo.InvariantCulture, "socketError#1: {0}", new object[]
							{
								socketError
							})));
						}
					}
					else
					{
						flag = true;
						workerInstance = this.activeWorker;
					}
				}
			}
			if (flag)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Waiting for Heartbeat response", new object[0]);
				SocketError socketError2;
				int num = WorkerProcessManager.RecvMessage(workerInstance, array, array.Length, UMRecyclerConfig.HeartBeatResponseTime * 1000, out socketError2);
				lock (this)
				{
					if (this.state == WorkerProcessManager.States.Ready && this.activeWorker == workerInstance)
					{
						if (num != 0)
						{
							if (WorkerProcessManager.AnalyzeBuffer(workerInstance, array, num))
							{
								this.numHeartBeatFailures = 0;
								CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Received successful heartbeat response", new object[0]);
							}
							else
							{
								this.numHeartBeatFailures++;
								CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Received bad heartbeat response", new object[0]);
							}
						}
						else
						{
							this.numHeartBeatFailures++;
							CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Failed to receive Heartbeat response", new object[0]);
						}
						if (this.numHeartBeatFailures >= UMRecyclerConfig.MaxHeartBeatFailures)
						{
							UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_RecycledMaxHeartBeatsMissedExceeded, null, new object[]
							{
								this.numHeartBeatFailures,
								UMRecyclerConfig.MaxHeartBeatFailures
							});
							CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Forked worker process - exceeded maxheartbeatmissed limit, has={0}, limit={1}", new object[]
							{
								this.numHeartBeatFailures,
								UMRecyclerConfig.MaxHeartBeatFailures
							});
							this.numHeartBeatFailures = 0;
							this.activeWorker.WatsonAndCreateNew(new UMServiceHeartbeatException(string.Format(CultureInfo.InvariantCulture, "socketError#2: {0}", new object[]
							{
								socketError2
							})));
						}
					}
				}
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00007240 File Offset: 0x00005440
		internal bool RecycleWPToChangeCerts()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "in RecycleWPToChangeCerts", new object[0]);
			bool result;
			lock (this)
			{
				if (this.state == WorkerProcessManager.States.Ready && this.activeWorker != null)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Starting recycle to change WP certificates", new object[0]);
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessRecycledToChangeCerts, null, new object[0]);
					this.RecycleStart();
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000072E0 File Offset: 0x000054E0
		internal void RecycleWorkerProcess()
		{
			lock (this)
			{
				if (this.state == WorkerProcessManager.States.Ready && this.activeWorker != null)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "UMService::RecycleWorkerProcess()", new object[0]);
					this.RecycleStart();
				}
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00007348 File Offset: 0x00005548
		internal void MonitorResources(object stateinfo)
		{
			WorkerInstance workerInstance = null;
			bool flag = false;
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "in MonitorResources", new object[0]);
			lock (this)
			{
				if (this.state == WorkerProcessManager.States.Ready && this.activeWorker != null)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "MonitorResources for the worker instance (pid={0})", new object[]
					{
						this.activeWorker.Pid
					});
					flag = true;
					workerInstance = this.activeWorker;
				}
			}
			if (flag)
			{
				bool requestRecycleWithWatson;
				bool flag3 = workerInstance.AnalyzeRecycleParams(out requestRecycleWithWatson);
				if (flag3)
				{
					lock (this)
					{
						if (this.activeWorker == workerInstance && this.state == WorkerProcessManager.States.Ready)
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Starting recycle", new object[0]);
							this.activeWorker.RequestRecycleWithWatson = requestRecycleWithWatson;
							this.RecycleStart();
						}
					}
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00007468 File Offset: 0x00005668
		internal void DropPipelineHealthCheck(object ignored)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Dropping a pipeline health check message", new object[0]);
			HealthCheckPipelineContext healthCheckPipelineContext = new HealthCheckPipelineContext(Guid.NewGuid().ToString());
			healthCheckPipelineContext.SuppressDisposeTracker();
			healthCheckPipelineContext.SaveMessage();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000074B5 File Offset: 0x000056B5
		internal void Start()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "In WorkerProcessManager Start", new object[0]);
			this.activeWorker = this.StartWorkerInstance(0, false);
			this.isFirstStart = false;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000074E8 File Offset: 0x000056E8
		internal void Stop(TimeSpan timeout)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Stop WorkerProcessManager(current state={0})", new object[]
			{
				WorkerProcessManager.GetStateString(this.state)
			});
			this.TraceWorkers();
			lock (this)
			{
				this.prevstate = this.state;
				this.state = WorkerProcessManager.States.Stopped;
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Previous State = {0}, Current State = {1}", new object[]
				{
					WorkerProcessManager.GetStateString(this.prevstate),
					WorkerProcessManager.GetStateString(this.state)
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceStateChange, null, new object[]
				{
					WorkerProcessManager.GetStateString(this.prevstate),
					WorkerProcessManager.GetStateString(this.state)
				});
				if (this.heartbeatTimer != null)
				{
					this.heartbeatTimer.Dispose();
					this.heartbeatTimer = null;
				}
				if (this.resourceTimer != null)
				{
					this.resourceTimer.Dispose();
					this.resourceTimer = null;
				}
				if (this.pipelineHealthCheckTimer != null)
				{
					this.pipelineHealthCheckTimer.Dispose();
					this.pipelineHealthCheckTimer = null;
				}
				if (this.retireTimer != null)
				{
					this.retireTimer.Dispose();
					this.retireTimer = null;
				}
				int num = 0;
				int num2 = 0;
				if (this.activeWorker != null)
				{
					num++;
				}
				if (this.passiveWorker != null)
				{
					num++;
				}
				if (this.retiredWorker != null)
				{
					num++;
				}
				ManualResetEvent[] array = new ManualResetEvent[num];
				if (this.activeWorker != null)
				{
					array[num2] = this.activeWorker.StopEvent;
					this.activeWorker.Stop();
					num2++;
				}
				if (this.passiveWorker != null)
				{
					array[num2] = this.passiveWorker.StopEvent;
					this.passiveWorker.Stop();
					num2++;
				}
				if (this.retiredWorker != null)
				{
					array[num2] = this.retiredWorker.StopEvent;
					this.retiredWorker.Stop();
					num2++;
				}
				try
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Waiting for all the stop events, count = {0}", new object[]
					{
						num
					});
					if (WaitHandle.WaitAll(array, timeout, false))
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Came out of waiting for all the stop events", new object[0]);
					}
					else
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Came out of waiting for all the stop events - though not all processes responded", new object[0]);
					}
				}
				catch (ArgumentNullException)
				{
				}
				if (this.activeWorker != null)
				{
					this.activeWorker.StopControlSocket();
					this.activeWorker = null;
				}
				if (this.passiveWorker != null)
				{
					this.passiveWorker.StopControlSocket();
					this.passiveWorker = null;
				}
				if (this.retiredWorker != null)
				{
					this.retiredWorker.StopControlSocket();
					this.retiredWorker = null;
				}
				Utils.CleanUMTempDirectory(Path.Combine(Utils.GetExchangeDirectory(), UMRecyclerConfig.Tempdir));
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000077D8 File Offset: 0x000059D8
		internal bool CanRedirect(bool isCallSecured, out int port)
		{
			bool result;
			lock (this)
			{
				WorkerInstance availableWorker = this.GetAvailableWorker();
				if (availableWorker != null)
				{
					port = availableWorker.SipPort;
					if (UMRecyclerConfig.UMStartupType == UMStartupMode.Dual && isCallSecured)
					{
						port++;
					}
					result = true;
				}
				else
				{
					port = 0;
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000783C File Offset: 0x00005A3C
		private static string GetStateString(WorkerProcessManager.States mystate)
		{
			switch (mystate)
			{
			case WorkerProcessManager.States.Init:
				return "initializing";
			case WorkerProcessManager.States.Ready:
				return "Ready and Accepting calls";
			case WorkerProcessManager.States.RecycleWaitingPassiveConnected:
				return "In midst of a Recycle - waiting for Passive to say connected";
			case WorkerProcessManager.States.RecycleWaitingRetiredExit:
				return "In midst of a Recycle - waiting for Retired to exit";
			case WorkerProcessManager.States.ActiveGoneWaitingPassiveConnected:
				return "Waiting for Passive worker process to connect. The Active one has already exited";
			case WorkerProcessManager.States.PassiveGoneWaitingRetiredExit:
				return "Waiting for Retired Worrker process to exit. The Passive one has already exited";
			case WorkerProcessManager.States.Stopped:
				return "Stopped";
			default:
				return string.Empty;
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000078A0 File Offset: 0x00005AA0
		private static SocketError SendMessage(WorkerInstance workerInstance, byte[] buffer, int offset, int count, bool isHeartBeat)
		{
			SocketError result = SocketError.Success;
			lock (workerInstance)
			{
				result = workerInstance.SendMessageToWorkerProcess(buffer, offset, count, isHeartBeat);
			}
			return result;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000078E4 File Offset: 0x00005AE4
		private static int RecvMessage(WorkerInstance workerInstance, byte[] buffer, int count, int timeout, out SocketError socketError)
		{
			int result = 0;
			lock (workerInstance)
			{
				result = workerInstance.ReadMessageFromWorkerProcess(buffer, count, timeout, out socketError);
			}
			return result;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00007928 File Offset: 0x00005B28
		private static bool AnalyzeBuffer(WorkerInstance temp, byte[] buffer, int validLength)
		{
			string text = null;
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "in AnalyzeBuffer", new object[0]);
			if (validLength < 4)
			{
				return false;
			}
			try
			{
				text = Encoding.UTF8.GetString(buffer, 0, validLength);
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "in AnalyzeBuffer, message received={0}", new object[]
				{
					text
				});
			}
			catch (ArgumentOutOfRangeException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "in AnalyzeBuffer, exception ={0}", new object[]
				{
					ex.ToString()
				});
				return false;
			}
			int num = 0;
			bool flag = false;
			char[] array = "HR".ToCharArray();
			if (text == null || text[0] != array[0] || text[1] != array[1])
			{
				return false;
			}
			try
			{
				num = int.Parse(text.Substring(2), CultureInfo.InvariantCulture);
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "In Heartbeat response, numcalls recvd ={0}", new object[]
				{
					num
				});
				flag = true;
			}
			catch (FormatException ex2)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "in AnalyzeBuffer, exception ={0}", new object[]
				{
					ex2.ToString()
				});
			}
			catch (OverflowException ex3)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "in AnalyzeBuffer, exception ={0}", new object[]
				{
					ex3.ToString()
				});
			}
			catch (ArgumentNullException ex4)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "in AnalyzeBuffer, exception ={0}", new object[]
				{
					ex4.ToString()
				});
			}
			if (temp != null && flag)
			{
				temp.SetNumCalls(num);
				return true;
			}
			return false;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00007B0C File Offset: 0x00005D0C
		private WorkerInstance GetAvailableWorker()
		{
			if ((this.state == WorkerProcessManager.States.Ready || this.state == WorkerProcessManager.States.RecycleWaitingPassiveConnected) && this.activeWorker != null)
			{
				return this.activeWorker;
			}
			if (this.state == WorkerProcessManager.States.RecycleWaitingRetiredExit && this.passiveWorker != null)
			{
				return this.passiveWorker;
			}
			return null;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00007B48 File Offset: 0x00005D48
		private void KillAll()
		{
			if (this.retiredWorker != null)
			{
				WorkerInstance workerInstance = this.retiredWorker;
				this.retiredWorker = null;
				Utils.KillProcess(workerInstance.Process);
				if (this.retireTimer != null)
				{
					this.retireTimer.Dispose();
					this.retireTimer = null;
				}
			}
			if (this.passiveWorker != null)
			{
				WorkerInstance workerInstance = this.passiveWorker;
				this.passiveWorker = null;
				Utils.KillProcess(workerInstance.Process);
			}
			if (this.activeWorker != null)
			{
				WorkerInstance workerInstance = this.activeWorker;
				this.activeWorker = null;
				this.numHeartBeatFailures = 0;
				Utils.KillProcess(workerInstance.Process);
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00007BDC File Offset: 0x00005DDC
		private WorkerInstance StartWorkerInstance(int thrashCrashCount, bool startAsPassive)
		{
			WorkerInstance workerInstance = null;
			int num;
			int tcpPort;
			this.GetWorkerPort(out num, out tcpPort);
			if (num == 0)
			{
				throw new UMServiceBaseException(Strings.SipPortsUnavailable(UMRecyclerConfig.Worker1SipPortNumber, UMRecyclerConfig.Worker2SipPortNumber));
			}
			if (!this.isFirstStart)
			{
				Util.IncrementCounter(AvailabilityCounters.WorkerProcessRecycled);
			}
			try
			{
				workerInstance = new WorkerInstance(this.workerPath, startAsPassive, new WorkerInstance.WorkerContacted(this.OnWorkerContact), new WorkerInstance.WorkerExited(this.OnWorkerExited), this.jobObject, thrashCrashCount, num, tcpPort);
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Started a new instance, pid={0}", new object[]
				{
					workerInstance.Process.Id
				});
			}
			catch (UMServiceBaseException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "Failed to start the WorkerProcessr, error={0}", new object[]
				{
					ex.ToString()
				});
				throw new UMServiceBaseException(Strings.UMWorkerProcessStartFailed(ex.Message));
			}
			return workerInstance;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00007CD8 File Offset: 0x00005ED8
		private WorkerInstance RestartWorkerInstance(WorkerInstance workerInstance)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "RestartWorkerInstance worker instance: {0}, thrashcount={1}", new object[]
			{
				this.workerPath,
				workerInstance.ThrashCrashCount
			});
			if (workerInstance.ThrashCrashCount >= UMRecyclerConfig.ThrashCountMaximum)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Worker instance keeps crashing on startup: {0}", new object[]
				{
					this.workerPath
				});
				throw new UMServiceBaseException(Strings.UMWorkerProcessExceededMaxThrashCount(UMRecyclerConfig.ThrashCountMaximum));
			}
			int thrashCrashCount = 0;
			ExDateTime t = workerInstance.StartTime.AddMilliseconds((double)(UMRecyclerConfig.StartupTime * 1000 + 500));
			if (t > ExDateTime.UtcNow)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Worker instance crashed immediately: thrashCrashCount={0}", new object[]
				{
					workerInstance.ThrashCrashCount
				});
				thrashCrashCount = workerInstance.ThrashCrashCount + 1;
			}
			return this.StartWorkerInstance(thrashCrashCount, !workerInstance.IsActive);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00007DDC File Offset: 0x00005FDC
		private void TraceWorkers()
		{
			if (this.activeWorker != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "active worker: pid {0}", new object[]
				{
					this.activeWorker.Process.Id
				});
			}
			if (this.passiveWorker != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "passive worker: pid {0}", new object[]
				{
					this.passiveWorker.Process.Id
				});
			}
			if (this.retiredWorker != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "retired worker: pid {0}", new object[]
				{
					this.retiredWorker.Process.Id
				});
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007EA0 File Offset: 0x000060A0
		private void RecycleStart()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "RecycleStart (state={0})", new object[]
			{
				this.state
			});
			this.TraceWorkers();
			this.prevstate = this.state;
			this.state = WorkerProcessManager.States.RecycleWaitingPassiveConnected;
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "PrevState {0} CurState {1}", new object[]
			{
				this.prevstate,
				this.state
			});
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceStateChange, null, new object[]
			{
				WorkerProcessManager.GetStateString(this.prevstate),
				WorkerProcessManager.GetStateString(this.state)
			});
			this.passiveWorker = this.StartWorkerInstance(0, true);
			this.TraceWorkers();
			Platform.Utilities.RecycleServiceDependencies();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00007F80 File Offset: 0x00006180
		private void ActivatePassiveAndHandleError()
		{
			if (WorkerProcessManager.SendMessage(this.passiveWorker, WorkerProcessManager.commandActivate, 0, WorkerProcessManager.commandActivate.Length, false) == SocketError.Success)
			{
				WorkerInstance workerInstance = this.passiveWorker;
				this.passiveWorker = null;
				workerInstance.IsActive = true;
				this.activeWorker = workerInstance;
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Recycle Done", new object[0]);
				this.prevstate = this.state;
				this.state = WorkerProcessManager.States.Ready;
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "PrevState {0} CurState {1}", new object[]
				{
					this.prevstate,
					this.state
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceStateChange, null, new object[]
				{
					WorkerProcessManager.GetStateString(this.prevstate),
					WorkerProcessManager.GetStateString(this.state)
				});
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Activating Passive Failed, curr state={0}", new object[]
			{
				this.state
			});
			if (this.passiveWorker != null)
			{
				Process process = this.passiveWorker.Process;
				this.passiveWorker = null;
				Utils.KillProcess(process);
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000080BC File Offset: 0x000062BC
		private void RecycleWaitingPassiveConnected()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "RecycleWaitingPassiveConnected (state={0})", new object[]
			{
				this.state
			});
			this.TraceWorkers();
			switch (this.state)
			{
			case WorkerProcessManager.States.RecycleWaitingPassiveConnected:
			{
				SocketError socketError;
				if (this.activeWorker.RequestRecycleWithWatson)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_WatsoningDueToRecycling, null, new object[0]);
					socketError = WorkerProcessManager.SendMessage(this.activeWorker, WorkerProcessManager.commandRetireWithWatson, 0, WorkerProcessManager.commandRetireWithWatson.Length, false);
				}
				else
				{
					socketError = WorkerProcessManager.SendMessage(this.activeWorker, WorkerProcessManager.commandRetire, 0, WorkerProcessManager.commandRetire.Length, false);
				}
				if (socketError == SocketError.Success)
				{
					this.retiredWorker = this.activeWorker;
					this.retiredWorker.IsActive = false;
					this.activeWorker = null;
					this.numHeartBeatFailures = 0;
					this.prevstate = this.state;
					this.state = WorkerProcessManager.States.RecycleWaitingRetiredExit;
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "PrevState {0} CurState {1}", new object[]
					{
						this.prevstate,
						this.state
					});
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceStateChange, null, new object[]
					{
						WorkerProcessManager.GetStateString(this.prevstate),
						WorkerProcessManager.GetStateString(this.state)
					});
					this.retireTimer = new Timer(new TimerCallback(this.RetireTimeout), null, UMRecyclerConfig.RetireTime * 1000, -1);
				}
				else
				{
					if (this.retiredWorker != null)
					{
						Process process = this.retiredWorker.Process;
						this.retiredWorker = null;
						Utils.KillProcess(process);
					}
					this.ActivatePassiveAndHandleError();
				}
				this.TraceWorkers();
				return;
			}
			case WorkerProcessManager.States.RecycleWaitingRetiredExit:
				break;
			case WorkerProcessManager.States.ActiveGoneWaitingPassiveConnected:
				this.ActivatePassiveAndHandleError();
				this.TraceWorkers();
				return;
			case WorkerProcessManager.States.PassiveGoneWaitingRetiredExit:
				this.prevstate = this.state;
				this.state = WorkerProcessManager.States.RecycleWaitingRetiredExit;
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "PrevState {0} CurState {1}", new object[]
				{
					this.prevstate,
					this.state
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceStateChange, null, new object[]
				{
					WorkerProcessManager.GetStateString(this.prevstate),
					WorkerProcessManager.GetStateString(this.state)
				});
				this.TraceWorkers();
				break;
			default:
				return;
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000831C File Offset: 0x0000651C
		private void RecycleWaitingRetiredExit()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "RecycleWaitingRetiredExit (state={0})", new object[]
			{
				this.state
			});
			this.TraceWorkers();
			switch (this.state)
			{
			case WorkerProcessManager.States.RecycleWaitingRetiredExit:
				this.retiredWorker = null;
				if (this.retireTimer != null)
				{
					this.retireTimer.Dispose();
					this.retireTimer = null;
				}
				this.ActivatePassiveAndHandleError();
				this.TraceWorkers();
				return;
			case WorkerProcessManager.States.ActiveGoneWaitingPassiveConnected:
				break;
			case WorkerProcessManager.States.PassiveGoneWaitingRetiredExit:
				this.retiredWorker = null;
				if (this.retireTimer != null)
				{
					this.retireTimer.Dispose();
					this.retireTimer = null;
				}
				this.prevstate = this.state;
				this.state = WorkerProcessManager.States.ActiveGoneWaitingPassiveConnected;
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "PrevState {0} CurState {1}", new object[]
				{
					this.prevstate,
					this.state
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceStateChange, null, new object[]
				{
					WorkerProcessManager.GetStateString(this.prevstate),
					WorkerProcessManager.GetStateString(this.state)
				});
				this.TraceWorkers();
				break;
			default:
				return;
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000844C File Offset: 0x0000664C
		private void RecycleExited(WorkerInstance workerInstance)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "RecycleExited (state={0}, pid={1})", new object[]
			{
				this.state,
				workerInstance.Process.Id
			});
			this.TraceWorkers();
			switch (this.state)
			{
			case WorkerProcessManager.States.RecycleWaitingPassiveConnected:
				if (workerInstance == this.passiveWorker)
				{
					this.RestartPassive(workerInstance);
					return;
				}
				if (workerInstance == this.activeWorker)
				{
					this.prevstate = this.state;
					this.state = WorkerProcessManager.States.ActiveGoneWaitingPassiveConnected;
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "PrevState {0} CurState {1}", new object[]
					{
						this.prevstate,
						this.state
					});
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceStateChange, null, new object[]
					{
						WorkerProcessManager.GetStateString(this.prevstate),
						WorkerProcessManager.GetStateString(this.state)
					});
					this.activeWorker = null;
					this.numHeartBeatFailures = 0;
				}
				return;
			case WorkerProcessManager.States.RecycleWaitingRetiredExit:
				if (workerInstance == this.retiredWorker)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "The Retired worker exited", new object[0]);
					this.RecycleWaitingRetiredExit();
					return;
				}
				if (workerInstance == this.passiveWorker)
				{
					this.prevstate = this.state;
					this.state = WorkerProcessManager.States.PassiveGoneWaitingRetiredExit;
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "PrevState {0} CurState {1}", new object[]
					{
						this.prevstate,
						this.state
					});
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceStateChange, null, new object[]
					{
						WorkerProcessManager.GetStateString(this.prevstate),
						WorkerProcessManager.GetStateString(this.state)
					});
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "The passive worker which had come up - has now exited", new object[0]);
					this.passiveWorker = this.StartWorkerInstance(0, true);
				}
				return;
			case WorkerProcessManager.States.ActiveGoneWaitingPassiveConnected:
				this.RestartPassive(workerInstance);
				return;
			case WorkerProcessManager.States.PassiveGoneWaitingRetiredExit:
				if (workerInstance == this.retiredWorker)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "The Retired worker exited", new object[0]);
					this.RecycleWaitingRetiredExit();
					return;
				}
				if (workerInstance == this.passiveWorker)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "The passive worker still didnt come up!", new object[0]);
					this.RestartPassive(workerInstance);
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000086B4 File Offset: 0x000068B4
		private void RestartPassive(WorkerInstance workerInstance)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "The passive worker crashed while startup", new object[0]);
			if (workerInstance.ThrashCrashCount >= UMRecyclerConfig.ThrashCountMaximum)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Passive Worker process keeps crashing on startup: {0}", new object[]
				{
					this.workerPath
				});
				this.passiveWorker = null;
				this.KillAll();
				throw new UMServiceBaseException(Strings.UMWorkerProcessExceededMaxThrashCount(UMRecyclerConfig.ThrashCountMaximum));
			}
			this.passiveWorker = this.StartWorkerInstance(workerInstance.ThrashCrashCount + 1, true);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00008748 File Offset: 0x00006948
		private void OnWorkerContact(WorkerInstance workerInstance)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Worker process (pid={0}) contacted.", new object[]
			{
				workerInstance.Process.Id
			});
			lock (this)
			{
				if (this.state != WorkerProcessManager.States.Stopped)
				{
					if (workerInstance == this.activeWorker && this.state == WorkerProcessManager.States.Init)
					{
						this.prevstate = this.state;
						this.state = WorkerProcessManager.States.Ready;
						CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "PrevState {0} CurState {1}", new object[]
						{
							this.prevstate,
							this.state
						});
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceStateChange, null, new object[]
						{
							WorkerProcessManager.GetStateString(this.prevstate),
							WorkerProcessManager.GetStateString(this.state)
						});
						if (this.isServiceStartup)
						{
							this.isServiceStartup = false;
							this.wpstarted.Set();
						}
					}
					else
					{
						this.RecycleWaitingPassiveConnected();
					}
				}
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000887C File Offset: 0x00006A7C
		private void OnWorkerExited(WorkerInstance workerInstance, bool resetRequested, bool fatalError)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Worker process (pid={0}) {1}", new object[]
			{
				workerInstance.Pid,
				resetRequested ? "requested reset (crashed)" : "exited"
			});
			lock (this)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessExited, null, new object[]
				{
					this.state
				});
				if (this.state == WorkerProcessManager.States.Stopped)
				{
					this.KillAll();
				}
				else if (fatalError)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Worker process (pid={0}) encountered a fatal error during startup", new object[]
					{
						workerInstance.Pid
					});
					this.crashingWorkers[workerInstance.Pid] = true;
					this.wpfatalError.Set();
				}
				else
				{
					if (resetRequested)
					{
						if (this.crashingWorkers.ContainsKey(workerInstance.Pid))
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Worker process (pid={0})requested reset earlier, Ignore request. ", new object[]
							{
								workerInstance.Pid
							});
							return;
						}
						CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Worker process (pid={0}) starting reset.", new object[]
						{
							workerInstance.Pid
						});
						if (this.availablePorts.Count != 0)
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Ports are available, Worker process (pid={0}) reset will be handled now.", new object[]
							{
								workerInstance.Pid
							});
							this.crashingWorkers[workerInstance.Pid] = true;
						}
						else
						{
							WorkerInstance availableWorker = this.GetAvailableWorker();
							if (availableWorker != null && workerInstance != availableWorker)
							{
								CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Ports are not available but the retired worker can still take calls so hang around until a worker releases SIP ports.", new object[]
								{
									workerInstance.Pid
								});
								this.crashingWorkers[workerInstance.Pid] = false;
								return;
							}
							CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Ports are not available, and UM doesn't take calls anymore, exiting so that SIP ports are freed up by the Watsoning worker processes.", new object[]
							{
								workerInstance.Pid
							});
							throw new UMServiceBaseException(Strings.SipPortsUnavailable(UMRecyclerConfig.Worker1SipPortNumber, UMRecyclerConfig.Worker2SipPortNumber));
						}
					}
					else
					{
						this.availablePorts.Enqueue(workerInstance.SipPort);
						if (this.crashingWorkers.ContainsKey(workerInstance.Pid))
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Worker process (pid={0}) finished reset.", new object[]
							{
								workerInstance.Pid
							});
							if (this.crashingWorkers[workerInstance.Pid])
							{
								CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "This worker exit was handled before, just return.", new object[]
								{
									workerInstance.Pid
								});
								this.crashingWorkers.Remove(workerInstance.Pid);
								return;
							}
							CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "This worker exit wasn't handled before.", new object[]
							{
								workerInstance.Pid
							});
						}
						else
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Worker process (pid={0}) just exited.", new object[]
							{
								workerInstance.Pid
							});
						}
					}
					if (this.state == WorkerProcessManager.States.RecycleWaitingPassiveConnected || this.state == WorkerProcessManager.States.RecycleWaitingRetiredExit || this.state == WorkerProcessManager.States.ActiveGoneWaitingPassiveConnected || this.state == WorkerProcessManager.States.PassiveGoneWaitingRetiredExit)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Handling the reset/exit of the process (PID={0})", new object[]
						{
							workerInstance.Pid
						});
						this.RecycleExited(workerInstance);
					}
					else if (this.activeWorker == workerInstance || this.activeWorker == null)
					{
						UmServiceGlobals.InitializeCurrentCallsPerformanceCounters();
						this.numHeartBeatFailures = 0;
						this.prevstate = this.state;
						this.state = WorkerProcessManager.States.Init;
						CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "PrevState {0} CurState {1}", new object[]
						{
							this.prevstate,
							this.state
						});
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceStateChange, null, new object[]
						{
							WorkerProcessManager.GetStateString(this.prevstate),
							WorkerProcessManager.GetStateString(this.state)
						});
						this.activeWorker = this.RestartWorkerInstance(workerInstance);
					}
					this.CleanWorkerInstance(workerInstance);
					this.TraceWorkers();
				}
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00008D24 File Offset: 0x00006F24
		private void CleanWorkerInstance(WorkerInstance workerInstance)
		{
			if (workerInstance == this.activeWorker)
			{
				this.activeWorker = null;
				return;
			}
			if (workerInstance == this.passiveWorker)
			{
				this.passiveWorker = null;
				return;
			}
			if (workerInstance == this.retiredWorker)
			{
				this.retiredWorker = null;
			}
		}

		// Token: 0x04000043 RID: 67
		private static byte[] commandRetire = new byte[]
		{
			82,
			13,
			10
		};

		// Token: 0x04000044 RID: 68
		private static byte[] commandRetireWithWatson = new byte[]
		{
			87,
			13,
			10
		};

		// Token: 0x04000045 RID: 69
		private static byte[] commandActivate = new byte[]
		{
			65,
			13,
			10
		};

		// Token: 0x04000046 RID: 70
		private static byte[] commandHeartBeat = new byte[]
		{
			72,
			13,
			10
		};

		// Token: 0x04000047 RID: 71
		private static byte[] commandWatsonDueToTimeout = new byte[]
		{
			84,
			13,
			10
		};

		// Token: 0x04000048 RID: 72
		private readonly string workerPath;

		// Token: 0x04000049 RID: 73
		private WorkerProcessManager.States state;

		// Token: 0x0400004A RID: 74
		private WorkerProcessManager.States prevstate;

		// Token: 0x0400004B RID: 75
		private Timer heartbeatTimer;

		// Token: 0x0400004C RID: 76
		private Timer resourceTimer;

		// Token: 0x0400004D RID: 77
		private Timer pipelineHealthCheckTimer;

		// Token: 0x0400004E RID: 78
		private ManualResetEvent wpstarted;

		// Token: 0x0400004F RID: 79
		private ManualResetEvent wpfatalError;

		// Token: 0x04000050 RID: 80
		private bool isFirstStart = true;

		// Token: 0x04000051 RID: 81
		private WorkerInstance activeWorker;

		// Token: 0x04000052 RID: 82
		private WorkerInstance passiveWorker;

		// Token: 0x04000053 RID: 83
		private WorkerInstance retiredWorker;

		// Token: 0x04000054 RID: 84
		private Dictionary<int, bool> crashingWorkers = new Dictionary<int, bool>(3);

		// Token: 0x04000055 RID: 85
		private Queue<int> availablePorts = new Queue<int>(2);

		// Token: 0x04000056 RID: 86
		private SafeJobHandle jobObject;

		// Token: 0x04000057 RID: 87
		private int numHeartBeatFailures;

		// Token: 0x04000058 RID: 88
		private Timer retireTimer;

		// Token: 0x04000059 RID: 89
		private int portCurrent = 16000;

		// Token: 0x0400005A RID: 90
		private bool isServiceStartup;

		// Token: 0x02000014 RID: 20
		internal enum States
		{
			// Token: 0x0400005C RID: 92
			Init,
			// Token: 0x0400005D RID: 93
			Ready,
			// Token: 0x0400005E RID: 94
			RecycleWaitingPassiveConnected,
			// Token: 0x0400005F RID: 95
			RecycleWaitingRetiredExit,
			// Token: 0x04000060 RID: 96
			ActiveGoneWaitingPassiveConnected,
			// Token: 0x04000061 RID: 97
			PassiveGoneWaitingRetiredExit,
			// Token: 0x04000062 RID: 98
			Stopped
		}
	}
}
