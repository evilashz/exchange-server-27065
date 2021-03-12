using System;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Server.Storage.AdminInterface;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.DirectoryServices;
using Microsoft.Exchange.Server.Storage.MapiDisp;
using Microsoft.Exchange.Server.Storage.StartupShutdown;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Server.Storage.Worker
{
	// Token: 0x02000004 RID: 4
	internal static class WorkerProcess
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000023D4 File Offset: 0x000005D4
		internal static void Main(string[] args)
		{
			try
			{
				ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Starting MSExchangeIS worker process");
				int num = Privileges.RemoveAllExcept(new string[]
				{
					"SeAuditPrivilege",
					"SeChangeNotifyPrivilege",
					"SeImpersonatePrivilege",
					"SeCreateGlobalPrivilege"
				});
				if (num != 0)
				{
					ExTraceGlobals.StartupShutdownTracer.TraceDebug<int>(0L, "Service: Error removing unnecessary privileges: {0}", num);
					Environment.Exit(num);
				}
				ExWatson.Register("E12");
				int startupDelaySeconds = WorkerProcess.GetStartupDelaySeconds();
				if (startupDelaySeconds > 0)
				{
					Thread.Sleep(TimeSpan.FromSeconds((double)startupDelaySeconds));
				}
				Semaphore semaphore = null;
				bool testMode = false;
				int timeoutTime = 0;
				int num2 = 0;
				bool flag = false;
				PipeStream pipeStream;
				Guid guid;
				Guid dagOrServerGuid;
				if (WorkerProcess.ParseArguments(args, out pipeStream, out guid, out dagOrServerGuid, out semaphore, out testMode, out num2, out timeoutTime, out flag))
				{
					if (!testMode)
					{
						timeoutTime = 30000;
					}
					using (pipeStream)
					{
						using (semaphore)
						{
							using (ManualResetEvent timeoutEvent = new ManualResetEvent(!testMode || 0 == timeoutTime))
							{
								using (ControlListener controlListener = new ControlListener(guid, pipeStream, delegate()
								{
									ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Shutdown operation timed out");
									if (!testMode)
									{
										throw new TimeoutException("Process shutdown did not complete in time");
									}
									if (timeoutTime > 0)
									{
										timeoutEvent.Set();
									}
								}, timeoutTime, testMode && timeoutTime > 0))
								{
									try
									{
										if (!controlListener.Initialize())
										{
											ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Failed to initialize control listener object");
										}
										else if (!testMode || !flag)
										{
											if (testMode || WorkerProcess.Initialize(guid, dagOrServerGuid))
											{
												if (testMode && num2 > 0)
												{
													Thread.Sleep(num2);
												}
												ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "MSExchangeIS worker process started successfully");
												if (semaphore != null)
												{
													semaphore.Release();
												}
												controlListener.WaitForShutdown();
												if (testMode && timeoutTime > 0)
												{
													timeoutEvent.WaitOne();
												}
											}
										}
									}
									finally
									{
										controlListener.IsShuttingDown = true;
										WorkerProcess.Terminate();
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "MSExchangeIS worker process stopped");
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000026D4 File Offset: 0x000008D4
		private static bool ParseArguments(string[] args, out PipeStream controlPipe, out Guid instanceGuid, out Guid dagOrServerGuid, out Semaphore readySemaphore, out bool testMode, out int startupTimeoutTime, out int timeout, out bool failureMode)
		{
			controlPipe = null;
			instanceGuid = Guid.Empty;
			dagOrServerGuid = Guid.Empty;
			readySemaphore = null;
			testMode = false;
			startupTimeoutTime = 0;
			timeout = 0;
			failureMode = false;
			string text = null;
			string text2 = null;
			string g = null;
			string text3 = null;
			foreach (string text4 in args)
			{
				if (text4.StartsWith("-pipe:", StringComparison.OrdinalIgnoreCase))
				{
					text = text4.Remove(0, "-pipe:".Length);
				}
				else if (text4.StartsWith("-id:", StringComparison.OrdinalIgnoreCase))
				{
					text2 = text4.Remove(0, "-id:".Length);
				}
				else if (text4.StartsWith("-dag:", StringComparison.OrdinalIgnoreCase))
				{
					g = text4.Remove(0, "-dag:".Length);
				}
				else if (text4.StartsWith("-readykey:", StringComparison.OrdinalIgnoreCase))
				{
					text3 = text4.Remove(0, "-readykey:".Length);
				}
				else
				{
					if (!text4.StartsWith("-test", StringComparison.OrdinalIgnoreCase))
					{
						bool result;
						if (text4.StartsWith("-timeout:", StringComparison.OrdinalIgnoreCase))
						{
							if (!int.TryParse(text4.Remove(0, "-timeout:".Length), out timeout))
							{
								ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Invalid test timeout parameter");
								result = false;
							}
							else
							{
								if (timeout < 0)
								{
									timeout = 0;
									goto IL_1AC;
								}
								goto IL_1AC;
							}
						}
						else if (text4.StartsWith("-startuptimeout:", StringComparison.OrdinalIgnoreCase))
						{
							if (!int.TryParse(text4.Remove(0, "-startuptimeout:".Length), out startupTimeoutTime))
							{
								ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Invalid test timeout parameter");
								result = false;
							}
							else
							{
								if (startupTimeoutTime < 0)
								{
									startupTimeoutTime = 0;
									goto IL_1AC;
								}
								goto IL_1AC;
							}
						}
						else
						{
							if (text4.StartsWith("-fail", StringComparison.OrdinalIgnoreCase))
							{
								failureMode = true;
								goto IL_1AC;
							}
							goto IL_1AC;
						}
						return result;
					}
					testMode = true;
				}
				IL_1AC:;
			}
			if (text == null || text2 == null)
			{
				ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Invalid comand line parameters");
				return false;
			}
			long value;
			if (!long.TryParse(text, out value))
			{
				ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Invalid control pipe handle value");
				return false;
			}
			try
			{
				instanceGuid = new Guid(text2);
			}
			catch (FormatException exception)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
				ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Invalid instance ID value");
				return false;
			}
			try
			{
				dagOrServerGuid = new Guid(g);
			}
			catch (FormatException exception2)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(exception2);
				ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Invalid DAG ID value");
				return false;
			}
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				SafeFileHandle safeFileHandle = disposeGuard.Add<SafeFileHandle>(new SafeFileHandle(new IntPtr(value), true));
				try
				{
					controlPipe = new PipeStream(safeFileHandle, FileAccess.Read, true);
				}
				catch (ArgumentException ex)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
					ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Invalid pipe handle caused the exception:\n" + ex.ToString());
					safeFileHandle.SetHandleAsInvalid();
					return false;
				}
				disposeGuard.Success();
			}
			if (!string.IsNullOrEmpty(text3) && !WorkerProcess.OpenSemaphore(text3, out readySemaphore))
			{
				ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Invalid ready handle value");
				return false;
			}
			return true;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002A24 File Offset: 0x00000C24
		private static bool Initialize(Guid instanceGuid, Guid dagOrServerGuid)
		{
			Microsoft.Exchange.Server.Storage.DirectoryServices.Directory.InitializeConfiguration();
			int num = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.InitializePhaseOne(new Guid?(instanceGuid), new Guid?(dagOrServerGuid));
			if (num != 0)
			{
				ExTraceGlobals.StartupShutdownTracer.TraceDebug<int>(0L, "InitializePhaseOne returned 0x{0:X}", num);
				return false;
			}
			uint maxRpcThreadCount = WorkerProcess.GetMaxRpcThreadCount(instanceGuid);
			Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.MaxRPCThreadCount = maxRpcThreadCount;
			uint num2 = maxRpcThreadCount;
			string[] protocolSequences = new string[]
			{
				"ncalrpc",
				"ncacn_ip_tcp"
			};
			string[] protocolEndpoints = new string[2];
			RpcServerBase.StartGlobalServer(protocolSequences, protocolEndpoints, maxRpcThreadCount + num2);
			WorkerProcess.rpcServerStarted = true;
			if (!AdminRpcEndpoint.Instance.StartInterface(new Guid?(instanceGuid), false))
			{
				ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "AdminRpcEndpoint.StartInterface returned failure.");
				return false;
			}
			if (!PoolRpcServer.StartInterface(new Guid?(instanceGuid), maxRpcThreadCount, false))
			{
				ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "PoolRpcServer.StartInterface returned failure.");
				return false;
			}
			num = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.InitializePhaseTwo(true, new Guid?(instanceGuid));
			if (num != 0)
			{
				ExTraceGlobals.StartupShutdownTracer.TraceDebug<int>(0L, "InitializePhaseTwo returned 0x{0:X}", num);
				return false;
			}
			string cachedInstanceName = instanceGuid.ToString();
			ExTraceGlobals.FaultInjectionTracer.RegisterComponentInjectionCallback(() => cachedInstanceName);
			return true;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002B40 File Offset: 0x00000D40
		private static void Terminate()
		{
			int num = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.TerminatePhaseOne();
			if (num != 0)
			{
				ExTraceGlobals.StartupShutdownTracer.TraceDebug<int>(0L, "TerminatePhaseOne returned 0x{1:X}", num);
			}
			Microsoft.Exchange.Server.Storage.DirectoryServices.Directory.TerminateConfiguration();
			PoolRpcServer.StopInterface();
			AdminRpcEndpoint.Instance.StopInterface();
			if (WorkerProcess.rpcServerStarted)
			{
				RpcServerBase.StopGlobalServer();
				WorkerProcess.rpcServerStarted = false;
			}
			num = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.TerminatePhaseTwo();
			if (num != 0)
			{
				ExTraceGlobals.StartupShutdownTracer.TraceDebug<int>(0L, "TerminatePhaseTwo returned 0x{1:X}", num);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002BAC File Offset: 0x00000DAC
		private static bool OpenSemaphore(string name, out Semaphore semaphore)
		{
			semaphore = null;
			try
			{
				semaphore = Semaphore.OpenExisting(name);
				return true;
			}
			catch (WaitHandleCannotBeOpenedException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			return false;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002BF0 File Offset: 0x00000DF0
		private static int GetStartupDelaySeconds()
		{
			IRegistryReader instance = RegistryReader.Instance;
			return instance.GetValue<int>(Registry.LocalMachine, "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem", "StartupDelaySeconds", 0);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002C1C File Offset: 0x00000E1C
		private static uint GetMaxRpcThreadCount(Guid instanceGuid)
		{
			ServerInfo serverInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetServerInfo(NullExecutionContext.Instance);
			if (serverInfo == null || serverInfo.MaxRpcThreads == null || serverInfo.MaxRpcThreads.Value <= 0)
			{
				return 50U;
			}
			DatabaseInfo databaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(NullExecutionContext.Instance, instanceGuid);
			uint num = (uint)serverInfo.MaxRpcThreads.Value;
			if (databaseInfo != null && databaseInfo.Options != null && databaseInfo.Options.TotalDatabasesOnServer != null)
			{
				num /= (uint)Math.Max(1, databaseInfo.Options.TotalDatabasesOnServer.Value);
			}
			if (num < 10U)
			{
				num = 10U;
			}
			return num;
		}

		// Token: 0x0400000A RID: 10
		private const string InstanceIdOption = "-id:";

		// Token: 0x0400000B RID: 11
		private const string DagOrServerGuidOption = "-dag:";

		// Token: 0x0400000C RID: 12
		private const string PipeOption = "-pipe:";

		// Token: 0x0400000D RID: 13
		private const string ReadyKeyOption = "-readykey:";

		// Token: 0x0400000E RID: 14
		private const string TestModeOption = "-test";

		// Token: 0x0400000F RID: 15
		private const string StartupTimeoutOption = "-startuptimeout:";

		// Token: 0x04000010 RID: 16
		private const string TimeoutOption = "-timeout:";

		// Token: 0x04000011 RID: 17
		private const string FailOption = "-fail";

		// Token: 0x04000012 RID: 18
		private const string ProtocolLRPC = "ncalrpc";

		// Token: 0x04000013 RID: 19
		private const string ProtocolTCP = "ncacn_ip_tcp";

		// Token: 0x04000014 RID: 20
		private const uint DefaultMaximumRpcThreads = 50U;

		// Token: 0x04000015 RID: 21
		private const uint MinimumRpcThreads = 10U;

		// Token: 0x04000016 RID: 22
		private static bool rpcServerStarted;
	}
}
