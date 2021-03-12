using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using Microsoft.Exchange.AddressBook.Service;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.Diagnostics.FaultInjection;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Messages;
using Microsoft.Exchange.RpcClientAccess.Server;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.RpcClientAccess.Service
{
	// Token: 0x02000006 RID: 6
	internal sealed class RpcServiceManager : ExServiceBase, IRpcServiceManager, IDisposable
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002460 File Offset: 0x00000660
		public RpcServiceManager()
		{
			base.ServiceName = RpcServiceManager.RpcServiceManagerServiceName;
			this.eventLog = new ExEventLog(RpcServiceManager.ComponentGuid, RpcServiceManager.RpcServiceManagerServiceName);
			base.CanStop = true;
			base.CanPauseAndContinue = false;
			base.AutoLog = false;
			this.subServices = new List<IRpcService>(2);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000024BF File Offset: 0x000006BF
		protected override TimeSpan StartTimeout
		{
			get
			{
				return TimeSpan.FromMinutes(2.0);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000024CF File Offset: 0x000006CF
		protected override TimeSpan StopTimeout
		{
			get
			{
				return TimeSpan.FromSeconds(5.0);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000024DF File Offset: 0x000006DF
		private static bool IsRunningAsService
		{
			get
			{
				return !Environment.UserInteractive;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024EC File Offset: 0x000006EC
		public static void Main(string[] args)
		{
			ExWatson.Register();
			Globals.InitializeSinglePerfCounterInstance();
			RpcServiceManager.eventLogConfig = new ExEventLog(RpcServiceManager.ComponentGuid, RpcServiceManager.RpcServiceManagerServiceName);
			Configuration.EventLogger = new ConfigurationSchema.EventLogger(RpcServiceManager.LogConfigurationEventConfig);
			using (RpcServiceManager rpcServiceManager = new RpcServiceManager())
			{
				rpcServiceManager.Run(args);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002554 File Offset: 0x00000754
		public void Initialize()
		{
			this.InternalCreateSubServices();
			this.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_RpcServiceManagerStarting, string.Empty, new object[]
			{
				this.GetSubServicesNames()
			});
			int num = this.PrivilegesSetup(RpcServiceManager.privilegesToKeepPreInitialization);
			if (num != 0)
			{
				string text = string.Format("Win32Error on PrivilegesSetup:  {0}", num);
				this.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_RpcServiceManagerServiceLoadException, string.Empty, new object[]
				{
					string.Empty,
					text
				});
				if (!RpcServiceManager.IsRunningAsService)
				{
					Console.WriteLine(text);
				}
				throw new RetryServiceStartException(text);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025EC File Offset: 0x000007EC
		public void Run(string[] args)
		{
			bool suppressWatsonReports = false;
			foreach (string text in args)
			{
				if (text.StartsWith("-wait", StringComparison.OrdinalIgnoreCase))
				{
					this.waitToContinue = true;
				}
				else if (text.StartsWith("-testHook", StringComparison.OrdinalIgnoreCase))
				{
					suppressWatsonReports = true;
				}
			}
			ExWatson.SetTestHook(new RpcServiceManager.ExWatsonTestHook(suppressWatsonReports));
			ExTraceGlobals.FaultInjectionTracer.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(ExceptionInjectionCallback.ExceptionLookup));
			if (RpcServiceManager.IsRunningAsService)
			{
				this.RunService();
				return;
			}
			this.RunConsole();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000266C File Offset: 0x0000086C
		public void StopService()
		{
			if (RpcServiceManager.IsRunningAsService)
			{
				base.Stop();
				return;
			}
			Console.WriteLine("Stopping service. Please, press ENTER to continue with its shutdown.");
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002686 File Offset: 0x00000886
		public void AddTcpPort(string port)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002688 File Offset: 0x00000888
		public void AddHttpPort(string port)
		{
			RpcEndPoint.AddHttpPort(port);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002690 File Offset: 0x00000890
		public void EnableLrpc()
		{
			RpcEndPoint.EnableLrpc();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002697 File Offset: 0x00000897
		public void AddServer(Action starter, Action stopper)
		{
			RpcEndPoint.AddServer(starter, stopper);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000026A0 File Offset: 0x000008A0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.subServices != null)
			{
				foreach (IRpcService disposable in this.subServices)
				{
					Util.DisposeIfPresent(disposable);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002764 File Offset: 0x00000964
		protected override void OnStartInternal(string[] args)
		{
			RpcServiceManager.<>c__DisplayClass1 CS$<>8__locals1 = new RpcServiceManager.<>c__DisplayClass1();
			CS$<>8__locals1.<>4__this = this;
			TimeSpan startTimeout = this.StartTimeout;
			CS$<>8__locals1.timeToWaitForDeferredServiceThread = TimeSpan.FromMilliseconds(startTimeout.TotalMilliseconds * 0.9);
			base.ExRequestAdditionalTime((int)startTimeout.TotalMilliseconds);
			SettingOverrideSync.Instance.Start(true);
			ProcessAccessManager.RegisterComponent(SettingOverrideSync.Instance);
			this.DoTryFilterCatchAndStopOnExceptionServiceStart(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<OnStartInternal>b__0)));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000027D8 File Offset: 0x000009D8
		protected override void OnStopInternal()
		{
			this.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_RpcServiceManagerStopping, string.Empty, new object[]
			{
				this.GetSubServicesNames()
			});
			Thread thread = new Thread(new ThreadStart(this.DeferredServiceShutdown));
			thread.Start();
			ProcessAccessManager.UnregisterComponent(SettingOverrideSync.Instance);
			SettingOverrideSync.Instance.Stop();
			if (!thread.Join(this.StopTimeout))
			{
				this.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_RpcServiceManagerShutdownTimeoutExceeded, string.Empty, new object[]
				{
					this.StopTimeout.TotalSeconds.ToString()
				});
				Process.GetCurrentProcess().Kill();
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000289C File Offset: 0x00000A9C
		private static void LogConfigurationEventConfig(ExEventLog.EventTuple tuple, params object[] args)
		{
			string periodicKey = null;
			if (tuple.Period == ExEventLog.EventPeriod.LogPeriodic)
			{
				periodicKey = args.Aggregate(tuple.EventId.GetHashCode(), (int hashCode, object arg) => hashCode ^= ((arg != null) ? arg.GetHashCode() : 0)).ToString();
			}
			RpcServiceManager.eventLogConfig.LogEvent(tuple, periodicKey, args);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002900 File Offset: 0x00000B00
		private void InternalCreateSubServices()
		{
			try
			{
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					object obj = disposeGuard.Add<RpcClientAccessService>(new RpcClientAccessService(this));
					this.subServices.Add((IRpcService)obj);
					obj = disposeGuard.Add<AddressBookService>(new AddressBookService(this));
					this.subServices.Add((IRpcService)obj);
					disposeGuard.Success();
				}
			}
			catch (SystemException innerException)
			{
				throw new RetryServiceStartException("Failed to create subservices", innerException);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002998 File Offset: 0x00000B98
		private void RunService()
		{
			ServiceBase.Run(this);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000029A0 File Offset: 0x00000BA0
		private void RunConsole()
		{
			Console.WriteLine("Starting {0}, running in console mode.", Assembly.GetExecutingAssembly().GetName().Name);
			if (this.waitToContinue)
			{
				Console.WriteLine("Press <ENTER> to continue startup.");
				Console.ReadLine();
			}
			ExServiceBase.RunAsConsole(this);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000029D9 File Offset: 0x00000BD9
		private void LogInitializationCheckPoint(string phase)
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000029DC File Offset: 0x00000BDC
		private int PrivilegesSetup(string[] privilegesToKeep)
		{
			int num = Privileges.RemoveAllExcept(privilegesToKeep);
			if (num != 0)
			{
				string text = string.Format("Win32Error = {0}", num);
				this.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_RpcClientServiceRemovingPrivilegeErrorOnStart, string.Empty, new object[]
				{
					text
				});
			}
			return num;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002A28 File Offset: 0x00000C28
		private void DeferredServiceShutdown()
		{
			lock (this.serviceStatusChangeLock)
			{
				foreach (IRpcService rpcService in this.subServices)
				{
					rpcService.OnStopBegin();
				}
				RpcEndPoint.Stop();
				foreach (IRpcService rpcService2 in this.subServices)
				{
					rpcService2.OnStopEnd();
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002EBC File Offset: 0x000010BC
		private void DeferredServiceStartInitialization()
		{
			this.DoTryFilterCatchAndStopOnExceptionServiceStart(new TryDelegate(this, (UIntPtr)ldftn(<DeferredServiceStartInitialization>b__7)));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002ED0 File Offset: 0x000010D0
		private void DoTryCatchRpcExceptionAndRethrow(Action methodAction, Action<Exception> handleException)
		{
			try
			{
				methodAction();
			}
			catch (RpcServiceAbortException obj)
			{
				handleException(obj);
				throw;
			}
			catch (RpcException obj2)
			{
				handleException(obj2);
				throw;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002F18 File Offset: 0x00001118
		private void DoTryFilterCatchAndStopOnExceptionServiceStart(TryDelegate methodDelegate)
		{
			this.DoTryFilterCatchAndStopOnException(methodDelegate, new Action<Exception>(this.HandleUnexpectedExceptionOnStart));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002F2D File Offset: 0x0000112D
		private void DoTryFilterCatchAndStopOnExceptionServiceStop(TryDelegate methodDelegate)
		{
			this.DoTryFilterCatchAndStopOnException(methodDelegate, new Action<Exception>(this.HandleUnexpectedExceptionOnStop));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002F60 File Offset: 0x00001160
		private void DoTryFilterCatchAndStopOnException(TryDelegate methodDelegate, Action<Exception> handleUnexpectedException)
		{
			RpcServiceManager.<>c__DisplayClass17 CS$<>8__locals1 = new RpcServiceManager.<>c__DisplayClass17();
			CS$<>8__locals1.handleUnexpectedException = handleUnexpectedException;
			ILUtil.DoTryFilterCatch(methodDelegate, new FilterDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<DoTryFilterCatchAndStopOnException>b__14)), new CatchDelegate(null, (UIntPtr)ldftn(<DoTryFilterCatchAndStopOnException>b__15)));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002FAC File Offset: 0x000011AC
		private void HandleUnexpectedExceptionOnStart(Exception ex)
		{
			if (!RpcServiceManager.IsRunningAsService)
			{
				Console.WriteLine("Unexpected Exception on Start:");
				Console.WriteLine(ex.ToString());
			}
			if (ex is DuplicateRpcEndpointException)
			{
				this.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_RpcServiceManagerDuplicateRpcEndpoint, string.Empty, new object[]
				{
					ex.Message
				});
				return;
			}
			this.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_RpcServiceManagerStartException, string.Empty, new object[]
			{
				ex.Message
			});
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000302C File Offset: 0x0000122C
		private void HandleUnexpectedExceptionOnStop(Exception ex)
		{
			if (!RpcServiceManager.IsRunningAsService)
			{
				Console.WriteLine("Unexpected Exception on Stop:");
				Console.WriteLine(ex.ToString());
			}
			this.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_RpcServiceManagerStopException, string.Empty, new object[]
			{
				ex.Message
			});
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000307C File Offset: 0x0000127C
		private void HandleUnexpectedExceptionOnStartAndRestartService(Exception ex)
		{
			this.HandleUnexpectedExceptionOnStart(ex);
			base.ExitCode = 13;
			this.StopService();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003094 File Offset: 0x00001294
		private string GetSubServicesNames()
		{
			StringBuilder stringBuilder = new StringBuilder(this.subServices.Count * 16);
			foreach (IRpcService rpcService in this.subServices)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(rpcService.Name);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400000C RID: 12
		private const string WaitToContinueOption = "-wait";

		// Token: 0x0400000D RID: 13
		private const string TestHookOption = "-testHook";

		// Token: 0x0400000E RID: 14
		private const uint WatsonSuiteReportGenerated = 2472947005U;

		// Token: 0x0400000F RID: 15
		private const uint WatsonSuiteSetThrottlingPeriod = 4217777469U;

		// Token: 0x04000010 RID: 16
		internal static readonly Guid ComponentGuid = new Guid("53F12A79-F089-4312-9285-8CFDC77FB0A9");

		// Token: 0x04000011 RID: 17
		internal static readonly string RpcServiceManagerServiceName = "MSExchangeRPC";

		// Token: 0x04000012 RID: 18
		private static readonly string[] privilegesToKeepPreInitialization = new string[]
		{
			"SeAuditPrivilege",
			"SeChangeNotifyPrivilege",
			"SeImpersonatePrivilege",
			"SeCreateGlobalPrivilege",
			"SeTcbPrivilege"
		};

		// Token: 0x04000013 RID: 19
		private static readonly string[] privilegesToKeepPostInitialization = new string[]
		{
			"SeAuditPrivilege",
			"SeChangeNotifyPrivilege",
			"SeCreateGlobalPrivilege",
			"SeTcbPrivilege"
		};

		// Token: 0x04000014 RID: 20
		private static ExEventLog eventLogConfig;

		// Token: 0x04000015 RID: 21
		private readonly List<IRpcService> subServices;

		// Token: 0x04000016 RID: 22
		private ExEventLog eventLog;

		// Token: 0x04000017 RID: 23
		private object serviceStatusChangeLock = new object();

		// Token: 0x04000018 RID: 24
		private bool waitToContinue;

		// Token: 0x02000007 RID: 7
		private sealed class ExWatsonTestHook : ExWatson.IWatsonTestHook
		{
			// Token: 0x06000032 RID: 50 RVA: 0x000031A4 File Offset: 0x000013A4
			public ExWatsonTestHook(bool suppressWatsonReports)
			{
				this.suppressWatsonReports = suppressWatsonReports;
			}

			// Token: 0x06000033 RID: 51 RVA: 0x000031B4 File Offset: 0x000013B4
			public bool ShouldSubmitReport(WatsonReport report, string reportXmlFileName, string reportTextFileName, ref DiagnosticsNativeMethods.WER_SUBMIT_RESULT submitResult)
			{
				bool result = !this.suppressWatsonReports && !WatsonOnUnhandledExceptionDispatch.IsUnderWatsonSuiteTests;
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2472947005U);
				return result;
			}

			// Token: 0x06000034 RID: 52 RVA: 0x000031E5 File Offset: 0x000013E5
			public bool ShouldSkipThrottling(Exception ex)
			{
				return !WatsonOnUnhandledExceptionDispatch.IsUnderWatsonThrottlingTests;
			}

			// Token: 0x06000035 RID: 53 RVA: 0x000031F0 File Offset: 0x000013F0
			public TimeSpan GetExceptionThrottlingTimeout(Exception ex, TimeSpan defaultTimeout)
			{
				int num = 0;
				ExTraceGlobals.FaultInjectionTracer.TraceTest<int>(4217777469U, ref num);
				if (num != 0)
				{
					return TimeSpan.FromSeconds((double)num);
				}
				return defaultTimeout;
			}

			// Token: 0x0400001B RID: 27
			private readonly bool suppressWatsonReports;
		}
	}
}
