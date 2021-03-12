using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000153 RID: 339
	internal static class ServiceOperations
	{
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00039DE9 File Offset: 0x00037FE9
		public static Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ServiceOperationsTracer;
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00039DF0 File Offset: 0x00037FF0
		internal static Process GetServiceProcess(string serviceName, out Exception exception)
		{
			exception = null;
			Process result = null;
			try
			{
				NativeMethods.SERVICE_STATUS_PROCESS serviceStatusInfo = ServiceOperations.GetServiceStatusInfo(serviceName);
				if (serviceStatusInfo != null)
				{
					if (serviceStatusInfo.currentState != 1 && serviceStatusInfo.processID != 0)
					{
						result = ServiceOperations.GetProcessByIdBestEffort(serviceStatusInfo.processID, out exception);
					}
					else
					{
						exception = new AmGetServiceProcessException(serviceName, serviceStatusInfo.currentState, serviceStatusInfo.processID);
					}
				}
			}
			catch (Win32Exception ex)
			{
				exception = ex;
			}
			if (exception != null)
			{
				ServiceOperations.Tracer.TraceError<string, Exception>(0L, "GetServiceProcess({0}) failed: {1}", serviceName, exception);
			}
			return result;
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00039E74 File Offset: 0x00038074
		internal static Process GetProcessByIdBestEffort(int pid, out Exception exception)
		{
			exception = null;
			Process result = null;
			try
			{
				result = Process.GetProcessById(pid);
			}
			catch (Win32Exception ex)
			{
				exception = ex;
			}
			catch (ArgumentException ex2)
			{
				exception = ex2;
			}
			if (exception != null)
			{
				ServiceOperations.Tracer.TraceError<int, Exception>(0L, "GetProcessByIdBestEffort({0}) failed: {1}", pid, exception);
			}
			return result;
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00039ED0 File Offset: 0x000380D0
		internal static NativeMethods.SERVICE_STATUS_PROCESS GetServiceStatusInfo(string serviceName)
		{
			NativeMethods.SERVICE_STATUS_PROCESS result;
			using (ServiceController serviceController = new ServiceController(serviceName))
			{
				using (SafeHandle serviceHandle = serviceController.ServiceHandle)
				{
					NativeMethods.SERVICE_STATUS_PROCESS service_STATUS_PROCESS = null;
					int num = Marshal.SizeOf(typeof(NativeMethods.SERVICE_STATUS_PROCESS));
					IntPtr intPtr = Marshal.AllocHGlobal(num);
					try
					{
						if (!NativeMethods.QueryServiceStatusEx(serviceHandle, 0, intPtr, num, out num))
						{
							int lastWin32Error = Marshal.GetLastWin32Error();
							ServiceOperations.Tracer.TraceError<int>(0L, "QueryServiceStatusEx() failed with error: {0}", lastWin32Error);
							throw new Win32Exception();
						}
						NativeMethods.SERVICE_STATUS_PROCESS service_STATUS_PROCESS2 = (NativeMethods.SERVICE_STATUS_PROCESS)Marshal.PtrToStructure(intPtr, typeof(NativeMethods.SERVICE_STATUS_PROCESS));
						service_STATUS_PROCESS = new NativeMethods.SERVICE_STATUS_PROCESS();
						service_STATUS_PROCESS.serviceType = service_STATUS_PROCESS2.serviceType;
						service_STATUS_PROCESS.currentState = service_STATUS_PROCESS2.currentState;
						service_STATUS_PROCESS.controlsAccepted = service_STATUS_PROCESS2.controlsAccepted;
						service_STATUS_PROCESS.win32ExitCode = service_STATUS_PROCESS2.win32ExitCode;
						service_STATUS_PROCESS.serviceSpecificExitCode = service_STATUS_PROCESS2.serviceSpecificExitCode;
						service_STATUS_PROCESS.checkPoint = service_STATUS_PROCESS2.checkPoint;
						service_STATUS_PROCESS.waitHint = service_STATUS_PROCESS2.waitHint;
						service_STATUS_PROCESS.processID = service_STATUS_PROCESS2.processID;
						service_STATUS_PROCESS.serviceFlags = service_STATUS_PROCESS2.serviceFlags;
					}
					finally
					{
						Marshal.FreeHGlobal(intPtr);
					}
					result = service_STATUS_PROCESS;
				}
			}
			return result;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0003A018 File Offset: 0x00038218
		public static Exception ControlService(string serviceName, int controlCode)
		{
			Exception result = null;
			using (ServiceController serviceController = new ServiceController(serviceName))
			{
				using (SafeHandle serviceHandle = serviceController.ServiceHandle)
				{
					NativeMethods.SERVICE_STATUS service_STATUS = default(NativeMethods.SERVICE_STATUS);
					if (!NativeMethods.ControlService(serviceHandle, controlCode, ref service_STATUS))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						string message = string.Format("ControlService({0}, {1}) failed with error: {2}", serviceName, controlCode, lastWin32Error);
						ServiceOperations.Tracer.TraceError(0L, message);
						result = new Win32Exception(lastWin32Error, message);
					}
				}
			}
			return result;
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0003A120 File Offset: 0x00038320
		public static Exception KillService(string serviceName, string reason)
		{
			ServiceOperations.Tracer.TraceDebug<string>(0L, "KillService({0}) called", serviceName);
			Exception innerEx = null;
			Exception ex = null;
			try
			{
				ex = ServiceOperations.RunOperation(delegate(object param0, EventArgs param1)
				{
					using (Process serviceProcess = ServiceOperations.GetServiceProcess(serviceName, out innerEx))
					{
						if (serviceProcess != null)
						{
							serviceProcess.Kill();
							ServiceOperations.Tracer.TraceDebug<string>(0L, "Killed({0})", serviceName);
						}
					}
				});
				if (ex == null)
				{
					ex = innerEx;
				}
				if (ex != null)
				{
					ServiceOperations.Tracer.TraceError<string, Exception>(0L, "KillService({0}) fails: {1}", serviceName, ex);
				}
			}
			finally
			{
				ReplayCrimsonEvents.ServiceKilled.Log<string, string, string>(serviceName, reason, (ex != null) ? ex.Message : "<None>");
			}
			return ex;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0003A1D0 File Offset: 0x000383D0
		internal static bool IsWindowsCoreProcess(Process process)
		{
			string b = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), process.MainModule.ModuleName);
			return string.Equals(process.MainModule.FileName, b, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0003A2B8 File Offset: 0x000384B8
		public static Exception KillProcess(string processName, bool isCoreProcess)
		{
			return ServiceOperations.RunOperation(delegate(object param0, EventArgs param1)
			{
				Process[] processesByName = Process.GetProcessesByName(processName);
				if (processesByName != null)
				{
					foreach (Process process in processesByName)
					{
						using (process)
						{
							if (!isCoreProcess || ServiceOperations.IsWindowsCoreProcess(process))
							{
								ServiceOperations.Tracer.TraceDebug<string>(0L, "Killing process: {0}", process.MainModule.FileName);
								process.Kill();
							}
							else
							{
								ServiceOperations.Tracer.TraceDebug<string>(0L, "Skipped killing {0} since it does not appear to be a windows core process", process.MainModule.FileName);
							}
						}
					}
				}
			});
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0003A2EC File Offset: 0x000384EC
		internal static Exception RunOperation(EventHandler ev)
		{
			Exception result = null;
			try
			{
				ev(null, null);
			}
			catch (Win32Exception ex)
			{
				result = ex;
			}
			catch (ArgumentException ex2)
			{
				result = ex2;
			}
			catch (InvalidOperationException ex3)
			{
				result = ex3;
			}
			catch (System.ServiceProcess.TimeoutException ex4)
			{
				result = ex4;
			}
			return result;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0003A3C8 File Offset: 0x000385C8
		public static bool IsServiceRunningOnNode(string serviceName, string nodeName, out Exception ex)
		{
			ex = null;
			bool fRunning = false;
			ex = ServiceOperations.RunOperation(delegate(object param0, EventArgs param1)
			{
				ServiceController serviceController = new ServiceController(serviceName, nodeName);
				using (serviceController)
				{
					ServiceOperations.Tracer.TraceDebug<string, ServiceControllerStatus, string>(0L, "IsServiceRunningOnNode: {0} is {1} on {2}.", serviceName, serviceController.Status, nodeName);
					if (serviceController.Status == ServiceControllerStatus.Running)
					{
						fRunning = true;
					}
				}
			});
			if (ex != null)
			{
				ServiceOperations.Tracer.TraceError<string, string, Exception>(0L, "IsServiceRunningOnNode( {0}, {1} ): Caught exception {2}", serviceName, nodeName, ex);
			}
			return fRunning;
		}

		// Token: 0x0400058F RID: 1423
		public const string MsExchangeReplServiceName = "msexchangerepl";

		// Token: 0x04000590 RID: 1424
		public const string MsExchangeISServiceName = "MSExchangeIS";

		// Token: 0x04000591 RID: 1425
		public const string ClusterServiceName = "Clussvc";
	}
}
