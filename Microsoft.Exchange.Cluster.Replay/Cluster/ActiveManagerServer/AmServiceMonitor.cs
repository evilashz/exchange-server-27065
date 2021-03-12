using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Management;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200001C RID: 28
	internal abstract class AmServiceMonitor : ChangePoller
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00006B60 File Offset: 0x00004D60
		public AmServiceMonitor(string serviceName) : base(true)
		{
			this.ServiceName = serviceName;
			this.m_scm = new ServiceController(serviceName);
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00006B83 File Offset: 0x00004D83
		public static Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return ExTraceGlobals.AmServiceMonitorTracer;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00006B8A File Offset: 0x00004D8A
		protected int ProcessId
		{
			get
			{
				return this.m_serviceProcessId;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00006B92 File Offset: 0x00004D92
		protected DateTime ProcessStartedTime
		{
			get
			{
				return this.m_serviceProcessStartTime;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00006B9A File Offset: 0x00004D9A
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00006BA2 File Offset: 0x00004DA2
		protected string ServiceName { get; set; }

		// Token: 0x06000103 RID: 259 RVA: 0x00006BC0 File Offset: 0x00004DC0
		public Exception StartService()
		{
			AmServiceMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "StartService({0}) called", this.ServiceName);
			Exception ex = ServiceOperations.RunOperation(delegate(object param0, EventArgs param1)
			{
				this.m_scm.Start();
				this.WaitForStart();
			});
			if (ex != null)
			{
				ExTraceGlobals.ClusterEventsTracer.TraceError<string, Exception>(0L, "StartService({0}) fails: {1}", this.ServiceName, ex);
				ReplayEventLogConstants.Tuple_AmFailedToStartService.LogEvent(string.Empty, new object[]
				{
					this.ServiceName,
					ex.ToString()
				});
			}
			return ex;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006C40 File Offset: 0x00004E40
		public ServiceStartMode GetStartMode()
		{
			ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Service WHERE Name='" + this.ServiceName + "'");
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(query))
			{
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					using (managementObject)
					{
						AmServiceMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "service={0}", managementObject.ToString());
						string mode = (string)managementObject["StartMode"];
						return this.MapStartMode(mode);
					}
				}
				throw new ArgumentException("no such service");
			}
			ServiceStartMode result;
			return result;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006D44 File Offset: 0x00004F44
		public Exception StopService()
		{
			AmServiceMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "StopService({0}) called", this.ServiceName);
			Exception ex = ServiceOperations.RunOperation(delegate(object param0, EventArgs param1)
			{
				this.m_scm.Stop();
				this.WaitForStop();
				this.m_serviceProcessId = -1;
			});
			if (ex != null)
			{
				ExTraceGlobals.ClusterEventsTracer.TraceError<string, Exception>(0L, "StopService({0}) fails: {1}", this.ServiceName, ex);
				ReplayEventLogConstants.Tuple_AmFailedToStopService.LogEvent(string.Empty, new object[]
				{
					this.ServiceName,
					ex.ToString()
				});
			}
			return ex;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006DC4 File Offset: 0x00004FC4
		protected override void PollerThread()
		{
			AmServiceMonitor.Tracer.TraceFunction<string>((long)this.GetHashCode(), "Entering Service monitor '{0}'", this.ServiceName);
			try
			{
				this.StartMonitoring();
			}
			catch (AmServiceMonitorSystemShutdownException arg)
			{
				AmServiceMonitor.Tracer.TraceWarning<string, AmServiceMonitorSystemShutdownException>((long)this.GetHashCode(), "'{0}' service monitor is exiting since system shutdown is in progress (Exception: {1})", this.ServiceName, arg);
			}
			AmServiceMonitor.Tracer.TraceFunction<string>((long)this.GetHashCode(), "Leaving Service monitor '{0}'", this.ServiceName);
		}

		// Token: 0x06000107 RID: 263
		protected abstract void OnStart();

		// Token: 0x06000108 RID: 264
		protected abstract void OnStop();

		// Token: 0x06000109 RID: 265 RVA: 0x00006E44 File Offset: 0x00005044
		protected virtual void OnWaitingForStart()
		{
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006E46 File Offset: 0x00005046
		protected virtual void OnWaitingForStop()
		{
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006E48 File Offset: 0x00005048
		protected virtual bool IsServiceReady()
		{
			return true;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006E4C File Offset: 0x0000504C
		private void StartMonitoring()
		{
			AmServiceMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Starting service monitor for {0}", this.ServiceName);
			while (!this.m_fShutdown)
			{
				this.WaitForStart();
				if (this.m_fShutdown)
				{
					break;
				}
				AmServiceMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Service monitor detected that '{0}' is started", this.ServiceName);
				ReplayCrimsonEvents.ServiceStartDetected.Log<string>(this.ServiceName);
				this.OnStart();
				this.WaitForStop();
				if (this.m_fShutdown)
				{
					break;
				}
				AmServiceMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Service monitor detected that '{0}' is stopped", this.ServiceName);
				ReplayCrimsonEvents.ServiceStopDetected.Log<string>(this.ServiceName);
				this.OnStop();
			}
			AmServiceMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Service monitor monitoring stopped for {0}", this.ServiceName);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006F24 File Offset: 0x00005124
		private void WaitForStart()
		{
			this.m_serviceProcessId = -1;
			AmServiceMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "WaitForStart({0}) called", this.ServiceName);
			while (!this.m_fShutdown)
			{
				try
				{
					if (this.CheckServiceStatus(ServiceControllerStatus.Running) && this.IsServiceReady())
					{
						Exception ex;
						using (Process serviceProcess = ServiceOperations.GetServiceProcess(this.ServiceName, out ex))
						{
							if (serviceProcess != null)
							{
								Exception ex2 = null;
								try
								{
									this.m_serviceProcessId = serviceProcess.Id;
									this.m_serviceProcessStartTime = serviceProcess.StartTime;
									break;
								}
								catch (Win32Exception ex3)
								{
									ex2 = ex3;
								}
								catch (InvalidOperationException ex4)
								{
									ex2 = ex4;
								}
								if (ex2 != null)
								{
									AmTrace.Error("Service status for {0} is Running, but unable to read the process object details. Ex = {1}", new object[]
									{
										this.ServiceName,
										ex2
									});
									this.m_serviceProcessId = -1;
								}
							}
							else
							{
								AmTrace.Error("Service status for {0} is Running, but unable to get the process object", new object[]
								{
									this.ServiceName
								});
							}
						}
					}
					if (this.m_shutdownEvent.WaitOne(5000, false))
					{
						break;
					}
					this.OnWaitingForStart();
				}
				catch (Win32Exception e)
				{
					if (!this.HandleKnownException(e))
					{
						throw;
					}
				}
				catch (InvalidOperationException e2)
				{
					if (!this.HandleKnownException(e2))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00007090 File Offset: 0x00005290
		private void WaitForStop()
		{
			AmServiceMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "WaitForStop({0}) called", this.ServiceName);
			using (PrivilegeControl privilegeControl = new PrivilegeControl())
			{
				Exception arg;
				if (!privilegeControl.TryEnable("SeDebugPrivilege", out arg))
				{
					AmServiceMonitor.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "WaitForStop({0}) failed to set debug priv: {1}", this.ServiceName, arg);
				}
				this.WaitForStopInternal();
			}
			AmServiceMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "WaitForStop({0}) exits", this.ServiceName);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000712C File Offset: 0x0000532C
		private void WaitForStopInternal()
		{
			while (!this.m_fShutdown)
			{
				Process process = null;
				ProcessHandle processHandle = null;
				WaitHandle[] waitHandles = null;
				int millisecondsTimeout = 30000;
				try
				{
					while (!this.m_fShutdown)
					{
						if (this.CheckServiceStatus(ServiceControllerStatus.Stopped))
						{
							AmServiceMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Service stop detected for {0} based on the result from service controller", this.ServiceName);
							return;
						}
						Exception ex = null;
						if (process == null)
						{
							process = ServiceOperations.GetProcessByIdBestEffort(this.m_serviceProcessId, out ex);
							if (process == null)
							{
								AmServiceMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Service stop detected for {0} since process is not running anymore", this.ServiceName);
								return;
							}
							try
							{
								if (!process.StartTime.Equals(this.m_serviceProcessStartTime))
								{
									AmServiceMonitor.Tracer.TraceDebug<string, DateTime, DateTime>((long)this.GetHashCode(), "Service stop detected for {0} by the change in start times (prev={1}, current={2})", this.ServiceName, this.m_serviceProcessStartTime, process.StartTime);
									return;
								}
							}
							catch (InvalidOperationException ex2)
							{
								AmServiceMonitor.Tracer.TraceError<string, string>((long)this.GetHashCode(), "ps.StartTime for service {0} generated exception {1} . Assuming that the process had exited", this.ServiceName, ex2.Message);
								return;
							}
							if (processHandle == null)
							{
								processHandle = new ProcessHandle();
							}
							Exception arg;
							if (processHandle.TryGetWaitHandle(process, out arg))
							{
								millisecondsTimeout = 30000;
								waitHandles = new WaitHandle[]
								{
									this.m_shutdownEvent,
									processHandle.WaitHandle
								};
							}
							else
							{
								AmServiceMonitor.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "WaitForStop({0}) hit exception opening process handle: {1}", this.ServiceName, arg);
								waitHandles = new WaitHandle[]
								{
									this.m_shutdownEvent
								};
								millisecondsTimeout = 5000;
							}
						}
						int num = WaitHandle.WaitAny(waitHandles, millisecondsTimeout);
						if (num == 0)
						{
							AmServiceMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Shutdown signalled for {0}", this.ServiceName);
							return;
						}
						if (num == 1)
						{
							AmServiceMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Process exit detected for {0}", this.ServiceName);
							return;
						}
						this.OnWaitingForStop();
					}
				}
				catch (Win32Exception ex3)
				{
					AmServiceMonitor.Tracer.TraceError<string, string>((long)this.GetHashCode(), "WaitForStop({0}) encountered exception: {1}", this.ServiceName, ex3.Message);
					if (!this.HandleKnownException(ex3))
					{
						throw;
					}
				}
				catch (InvalidOperationException ex4)
				{
					AmServiceMonitor.Tracer.TraceError<string, string>((long)this.GetHashCode(), "WaitForStop({0}) encountered exception: {1}", this.ServiceName, ex4.Message);
					if (!this.HandleKnownException(ex4))
					{
						throw;
					}
				}
				finally
				{
					if (processHandle != null)
					{
						processHandle.Dispose();
						processHandle = null;
					}
					if (process != null)
					{
						process.Dispose();
						process = null;
					}
				}
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000073E4 File Offset: 0x000055E4
		private bool HandleKnownException(Exception e)
		{
			bool result = false;
			if (AmExceptionHelper.CheckExceptionCode(e, 1060U))
			{
				result = true;
				this.m_shutdownEvent.WaitOne(30000, false);
			}
			else if (AmExceptionHelper.CheckExceptionCode(e, 1115U))
			{
				throw new AmServiceMonitorSystemShutdownException(this.ServiceName);
			}
			return result;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00007432 File Offset: 0x00005632
		private ServiceStartMode MapStartMode(string mode)
		{
			if (SharedHelper.StringIEquals(mode, "Manual"))
			{
				return ServiceStartMode.Manual;
			}
			if (SharedHelper.StringIEquals(mode, "Disabled"))
			{
				return ServiceStartMode.Disabled;
			}
			return ServiceStartMode.Automatic;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00007453 File Offset: 0x00005653
		private bool CheckServiceStatus(ServiceControllerStatus status)
		{
			this.m_scm.Refresh();
			return this.m_scm.Status.Equals(status);
		}

		// Token: 0x0400006A RID: 106
		protected const int InvalidProcessId = -1;

		// Token: 0x0400006B RID: 107
		private const int m_WaitTimeoutFastMs = 5000;

		// Token: 0x0400006C RID: 108
		private const int m_WaitTimeoutSlowMs = 30000;

		// Token: 0x0400006D RID: 109
		private ServiceController m_scm;

		// Token: 0x0400006E RID: 110
		private int m_serviceProcessId = -1;

		// Token: 0x0400006F RID: 111
		private DateTime m_serviceProcessStartTime;
	}
}
