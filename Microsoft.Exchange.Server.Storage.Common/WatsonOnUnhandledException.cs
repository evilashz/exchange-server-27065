using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000CE RID: 206
	public static class WatsonOnUnhandledException
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0001D996 File Offset: 0x0001BB96
		// (set) Token: 0x0600096E RID: 2414 RVA: 0x0001D9A9 File Offset: 0x0001BBA9
		internal static bool DisableExceptionFilter
		{
			get
			{
				return WatsonOnUnhandledException.disableExceptionFilter && !WatsonOnUnhandledException.isUnderWatsonSuiteTests;
			}
			set
			{
				WatsonOnUnhandledException.disableExceptionFilter = value;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x0001D9B1 File Offset: 0x0001BBB1
		// (set) Token: 0x06000970 RID: 2416 RVA: 0x0001D9B8 File Offset: 0x0001BBB8
		internal static bool DisableGenerateDumpFile
		{
			get
			{
				return WatsonOnUnhandledException.disableGenerateDumpFile;
			}
			set
			{
				WatsonOnUnhandledException.disableGenerateDumpFile = value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x0001D9C0 File Offset: 0x0001BBC0
		public static bool IsUnderWatsonSuiteTests
		{
			get
			{
				return WatsonOnUnhandledException.isUnderWatsonSuiteTests;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x0001D9C7 File Offset: 0x0001BBC7
		public static bool ProcessKilled
		{
			get
			{
				return WatsonOnUnhandledException.processKilled;
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0001D9D0 File Offset: 0x0001BBD0
		public static void Initialize()
		{
			MethodInfo method = typeof(WatsonOnUnhandledException).GetMethod("ExceptionFilter", BindingFlags.Static | BindingFlags.NonPublic);
			RuntimeHelpers.PrepareMethod(method.MethodHandle);
			method = typeof(WatsonOnUnhandledException).GetMethod("FailFastOnOutOfMemoryException", BindingFlags.Static | BindingFlags.NonPublic);
			RuntimeHelpers.PrepareMethod(method.MethodHandle);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0001DA21 File Offset: 0x0001BC21
		internal static void ResetForTest(bool testModeEnabled, TimeSpan singleWatsonReportSemaphoreTimeout)
		{
			WatsonOnUnhandledException.isUnderWatsonSuiteTests = testModeEnabled;
			WatsonOnUnhandledException.processKilled = false;
			WatsonOnUnhandledException.singleWatsonReportSemaphore = new Semaphore(1, 1);
			WatsonOnUnhandledException.singleWatsonReportSemaphoreTimeout = singleWatsonReportSemaphoreTimeout;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0001DA41 File Offset: 0x0001BC41
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void FailFastOnOutOfMemoryException()
		{
			Environment.FailFast("OutOfMemoryException");
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0001DA4D File Offset: 0x0001BC4D
		private static bool IsFlowControlException(object exGeneric)
		{
			return exGeneric is FailRpcException || exGeneric is RpcException;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0001DA62 File Offset: 0x0001BC62
		private static void InternalExit()
		{
			Environment.Exit(-559034355);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001DA70 File Offset: 0x0001BC70
		private static bool UnhandledExceptionEventHandler(IExecutionDiagnostics executionDiagnostics, object exception)
		{
			if (WatsonOnUnhandledException.DisableExceptionFilter)
			{
				return false;
			}
			if (!WatsonOnUnhandledException.IsExceptionInteresting(exception))
			{
				return false;
			}
			if (executionDiagnostics != null)
			{
				ExWatson.RegisterReportAction(new WatsonOnUnhandledException.DiagnosticWatsonReportAction(executionDiagnostics), WatsonActionScope.Thread);
			}
			Exception ex = exception as Exception;
			Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_UnhandledException, new object[]
			{
				ex
			});
			try
			{
				if (executionDiagnostics != null)
				{
					executionDiagnostics.OnUnhandledException(ex);
				}
			}
			catch (Exception exception2)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(exception2);
			}
			if (WatsonOnUnhandledException.DisableGenerateDumpFile)
			{
				return false;
			}
			ErrorHelper.CheckForDebugger();
			try
			{
				if (!WatsonOnUnhandledException.singleWatsonReportSemaphore.WaitOne(WatsonOnUnhandledException.singleWatsonReportSemaphoreTimeout) && WatsonOnUnhandledException.IsUnderWatsonSuiteTests)
				{
					return false;
				}
				if (ExWatson.KillProcessAfterWatson || WatsonOnUnhandledException.IsUnderWatsonSuiteTests)
				{
					ReportOptions options = ReportOptions.ReportTerminateAfterSend | ReportOptions.DoNotFreezeThreads;
					ExWatson.SendReport(ex, options, null);
				}
				else
				{
					ExWatson.SendReportAndCrashOnAnotherThread(ex);
				}
			}
			finally
			{
				WatsonOnUnhandledException.KillCurrentProcess();
			}
			return false;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001DB4C File Offset: 0x0001BD4C
		private static bool ExceptionFilter(object exception, IExecutionDiagnostics executionDiagnostics)
		{
			if (exception is OutOfMemoryException)
			{
				WatsonOnUnhandledException.FailFastOnOutOfMemoryException();
			}
			return WatsonOnUnhandledException.UnhandledExceptionEventHandler(executionDiagnostics, exception);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001DB62 File Offset: 0x0001BD62
		public static void Guard(IExecutionDiagnostics executionDiagnostics, TryDelegate body)
		{
			ILUtil.DoTryFilterCatch<IExecutionDiagnostics>(body, WatsonOnUnhandledException.filterDelegate, WatsonOnUnhandledException.catchDelegate, executionDiagnostics);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0001DB78 File Offset: 0x0001BD78
		private static bool IsExceptionInteresting(object exGeneric)
		{
			return !(exGeneric is Exception) || !WatsonOnUnhandledException.IsFlowControlException(exGeneric);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0001DB9C File Offset: 0x0001BD9C
		internal static void KillCurrentProcess()
		{
			WatsonOnUnhandledException.processKilled = true;
			if (WatsonOnUnhandledException.IsUnderWatsonSuiteTests)
			{
				return;
			}
			try
			{
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					currentProcess.Kill();
				}
			}
			catch (Win32Exception exception)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
			}
			WatsonOnUnhandledException.InternalExit();
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0001DC00 File Offset: 0x0001BE00
		private static bool IsGrayException(Exception exception)
		{
			return GrayException.IsGrayException(exception);
		}

		// Token: 0x0400078E RID: 1934
		private static bool disableExceptionFilter = false;

		// Token: 0x0400078F RID: 1935
		private static bool disableGenerateDumpFile = false;

		// Token: 0x04000790 RID: 1936
		private static bool isUnderWatsonSuiteTests;

		// Token: 0x04000791 RID: 1937
		private static bool processKilled;

		// Token: 0x04000792 RID: 1938
		private static Semaphore singleWatsonReportSemaphore = new Semaphore(1, 1);

		// Token: 0x04000793 RID: 1939
		private static TimeSpan singleWatsonReportSemaphoreTimeout = TimeSpan.FromMilliseconds(-1.0);

		// Token: 0x04000794 RID: 1940
		private static GenericFilterDelegate<IExecutionDiagnostics> filterDelegate = new GenericFilterDelegate<IExecutionDiagnostics>(null, (UIntPtr)ldftn(ExceptionFilter));

		// Token: 0x04000795 RID: 1941
		private static GenericCatchDelegate<IExecutionDiagnostics> catchDelegate = new GenericCatchDelegate<IExecutionDiagnostics>(null, (UIntPtr)ldftn(<.cctor>b__0));

		// Token: 0x020000CF RID: 207
		private class DiagnosticWatsonReportAction : WatsonReportAction
		{
			// Token: 0x06000980 RID: 2432 RVA: 0x0001DC77 File Offset: 0x0001BE77
			internal DiagnosticWatsonReportAction(IExecutionDiagnostics executionDiagnostics) : base(null, true)
			{
				this.executionDiagnostics = executionDiagnostics;
			}

			// Token: 0x17000207 RID: 519
			// (get) Token: 0x06000981 RID: 2433 RVA: 0x0001DC88 File Offset: 0x0001BE88
			public override string ActionName
			{
				get
				{
					return "ExecutionDiagnostics";
				}
			}

			// Token: 0x06000982 RID: 2434 RVA: 0x0001DC90 File Offset: 0x0001BE90
			public override string Evaluate(WatsonReport watsonReport)
			{
				string result;
				try
				{
					result = (this.executionDiagnostics.DiagnosticInformationForWatsonReport ?? string.Empty);
				}
				catch (Exception ex)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
					result = ex.ToString();
				}
				return result;
			}

			// Token: 0x04000797 RID: 1943
			private readonly IExecutionDiagnostics executionDiagnostics;
		}
	}
}
