using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Diagnostics.Internal;
using Microsoft.Win32;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000B8 RID: 184
	internal static class ExWatson
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00012D13 File Offset: 0x00010F13
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x00012D1A File Offset: 0x00010F1A
		public static bool MsInternal { get; private set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00012D22 File Offset: 0x00010F22
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x00012D29 File Offset: 0x00010F29
		public static bool TestLabMachine { get; private set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00012D31 File Offset: 0x00010F31
		public static bool KillProcessAfterWatson
		{
			get
			{
				return ExWatson.killProcessAfterWatson;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00012D38 File Offset: 0x00010F38
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x00012D3F File Offset: 0x00010F3F
		public static bool WatsonAllowed { get; private set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00012D47 File Offset: 0x00010F47
		public static bool ErrorReportingAllowed
		{
			get
			{
				return ExWatson.WatsonAllowed;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00012D4E File Offset: 0x00010F4E
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x00012D55 File Offset: 0x00010F55
		public static bool ReaperThreadAllowed { get; private set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00012D5D File Offset: 0x00010F5D
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x00012D64 File Offset: 0x00010F64
		public static bool WerSubmitBypassDataThrottling { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00012DBC File Offset: 0x00010FBC
		public static string RealAppName
		{
			get
			{
				if (ExWatson.realApplicationName == null)
				{
					ExWatson.realApplicationName = Util.EvaluateOrDefault<string>(delegate()
					{
						string realAppName;
						using (Process currentProcess = Process.GetCurrentProcess())
						{
							string fileName = Path.GetFileName(currentProcess.MainModule.FileName);
							realAppName = ExWatson.GetRealAppName(fileName, Environment.CommandLine);
						}
						return realAppName;
					}, "unknown");
				}
				return ExWatson.realApplicationName;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00012E01 File Offset: 0x00011001
		public static string LabName
		{
			get
			{
				return ExWatson.labName;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00012E08 File Offset: 0x00011008
		public static Version ExchangeVersion
		{
			get
			{
				if (ExWatson.exchangeVersion == null && !ExWatson.TryGetExchangeVersionInstalled(out ExWatson.exchangeVersion))
				{
					ExWatson.exchangeVersion = ExWatson.DefaultAssemblyVersion;
				}
				return ExWatson.exchangeVersion;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00012E32 File Offset: 0x00011032
		public static string ExchangeInstallSource
		{
			get
			{
				if (ExWatson.exchangeInstallSource == null && !ExWatson.TryRegistryKeyGetValue<string>("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{4934D1EA-BE46-48B1-8847-F1AF20E892C1}", "InstallSource", string.Empty, out ExWatson.exchangeInstallSource))
				{
					ExWatson.exchangeInstallSource = string.Empty;
				}
				return ExWatson.exchangeInstallSource;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00012E65 File Offset: 0x00011065
		public static string ExchangeInstallPath
		{
			get
			{
				if (ExWatson.exchangeInstallPath == null && !ExWatson.TryRegistryKeyGetValue<string>("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup", "MsiInstallPath", string.Empty, out ExWatson.exchangeInstallPath))
				{
					ExWatson.exchangeInstallPath = string.Empty;
				}
				return ExWatson.exchangeInstallPath;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00012E98 File Offset: 0x00011098
		public static Version DefaultAssemblyVersion
		{
			get
			{
				if (ExWatson.defaultAssemblyVersion == null && !ExWatson.TryGetDefaultAssemblyVersion(out ExWatson.defaultAssemblyVersion))
				{
					ExWatson.defaultAssemblyVersion = new Version(14, 0, 0, 0);
				}
				return ExWatson.defaultAssemblyVersion;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00012EC8 File Offset: 0x000110C8
		public static Version RealApplicationVersion
		{
			get
			{
				if (ExWatson.realApplicationVersion == null)
				{
					using (Process currentProcess = Process.GetCurrentProcess())
					{
						if (!ExWatson.TryGetRealApplicationVersion(currentProcess, out ExWatson.realApplicationVersion))
						{
							ExWatson.realApplicationVersion = ExWatson.DefaultAssemblyVersion;
						}
					}
				}
				return ExWatson.realApplicationVersion;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00012F20 File Offset: 0x00011120
		public static Version ApplicationVersion
		{
			get
			{
				Version version = ExWatson.RealApplicationVersion;
				if (version == null || (ExWatson.ExchangeVersion != null && version < ExWatson.ExchangeVersion))
				{
					version = ExWatson.ExchangeVersion;
				}
				if (version == null)
				{
					version = ExWatson.DefaultAssemblyVersion;
				}
				return version;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00012F6C File Offset: 0x0001116C
		public static string ComputerName
		{
			get
			{
				if (ExWatson.computerName == null && !ExWatson.TryGetComputerName(out ExWatson.computerName))
				{
					ExWatson.computerName = "unknown";
				}
				return ExWatson.computerName;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00012F90 File Offset: 0x00011190
		public static string ProcessorArchitecture
		{
			get
			{
				if (ExWatson.processorArchitecture == null && !ExWatson.TryGetProcessorArchitecture(out ExWatson.processorArchitecture))
				{
					ExWatson.processorArchitecture = string.Empty;
				}
				return ExWatson.processorArchitecture;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00012FB4 File Offset: 0x000111B4
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x00012FCD File Offset: 0x000111CD
		public static string AppName
		{
			get
			{
				if (!string.IsNullOrEmpty(ExWatson.applicationName))
				{
					return ExWatson.applicationName;
				}
				return ExWatson.RealAppName;
			}
			set
			{
				ExWatson.applicationName = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00012FD5 File Offset: 0x000111D5
		public static int WatsonCountIn
		{
			get
			{
				return ExWatson.watsonCountIn;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00012FDC File Offset: 0x000111DC
		public static int WatsonCountOut
		{
			get
			{
				return ExWatson.watsonCountOut;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00012FE3 File Offset: 0x000111E3
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x00012FEA File Offset: 0x000111EA
		public static TimeSpan CrashReportTimeout
		{
			get
			{
				return ExWatson.crashReportTimeout;
			}
			set
			{
				ExWatson.crashReportTimeout = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00012FF2 File Offset: 0x000111F2
		public static int NativeCrashNowThreadId
		{
			get
			{
				return ExWatson.nativeCrashNowThreadId;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00012FF9 File Offset: 0x000111F9
		public static long WatsonReportCount
		{
			get
			{
				return ExWatson.watsonReportCount;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00013000 File Offset: 0x00011200
		internal static DateTime LastWatsonReport
		{
			get
			{
				return ExWatson.lastWatsonReport;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x00013007 File Offset: 0x00011207
		private static Dictionary<Type, List<WatsonReportAction>> CurrentThreadReportActions
		{
			get
			{
				if (ExWatson.currentThreadReportActions == null)
				{
					ExWatson.currentThreadReportActions = new Dictionary<Type, List<WatsonReportAction>>(0);
				}
				return ExWatson.currentThreadReportActions;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00013020 File Offset: 0x00011220
		private static Dictionary<Type, List<WatsonReportAction>> ProcessWideReportActions
		{
			get
			{
				if (ExWatson.processWideReportActions == null)
				{
					ExWatson.processWideReportActions = new Dictionary<Type, List<WatsonReportAction>>(0);
				}
				return ExWatson.processWideReportActions;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00013039 File Offset: 0x00011239
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x00013040 File Offset: 0x00011240
		private static ExWatson.IWatsonTestHook TestHook { get; set; }

		// Token: 0x060004F1 RID: 1265 RVA: 0x00013048 File Offset: 0x00011248
		public static void Register()
		{
			ExWatson.Register(null);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00013050 File Offset: 0x00011250
		public static void Register(string specifiedEventType)
		{
			lock (ExWatson.mutex)
			{
				ExWatson.Init(specifiedEventType);
				AppDomain.CurrentDomain.UnhandledException += ExWatson.HandleException;
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000130A8 File Offset: 0x000112A8
		public static void Init()
		{
			ExWatson.Init(string.Empty);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000130B4 File Offset: 0x000112B4
		public static void Init(string specifiedEventType)
		{
			lock (ExWatson.mutex)
			{
				if (!ExWatson.MsInternal)
				{
					try
					{
						string text = ("." + IPGlobalProperties.GetIPGlobalProperties().DomainName) ?? string.Empty;
						if (text.Length > 1 && (text.EndsWith(".exchangelabs.com", StringComparison.OrdinalIgnoreCase) || text.EndsWith(".exchangelabs.live-int.com", StringComparison.OrdinalIgnoreCase) || text.EndsWith(".ffo.gbl", StringComparison.OrdinalIgnoreCase) || text.EndsWith(".microsoft.com", StringComparison.OrdinalIgnoreCase) || text.EndsWith(".mgd.msft.net", StringComparison.OrdinalIgnoreCase) || text.EndsWith(".outlook.com", StringComparison.OrdinalIgnoreCase) || text.EndsWith(".outlook-int.com", StringComparison.OrdinalIgnoreCase) || text.EndsWith(".protection.gbl", StringComparison.OrdinalIgnoreCase)))
						{
							ExWatson.MsInternal = true;
						}
					}
					catch
					{
					}
				}
				int num = 45;
				if (ExWatson.TryRegistryKeyGetValue<int>("SOFTWARE\\Microsoft\\ExchangeServer\\v15", "PerIssueErrorReportingIntervalInMinutes", 45, out num))
				{
					if (num < 0)
					{
						num = 45;
					}
					if (num > 43200)
					{
						num = 43200;
					}
				}
				ExWatson.throttlingPeriod = TimeSpan.FromMinutes((double)num);
				ExWatson.eventType = ExWatson.ParseEventType(specifiedEventType);
				ExWatson.ReInit();
				ExWatson.initialized = true;
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001320C File Offset: 0x0001140C
		public static void RegisterReportAction(WatsonReportAction action, WatsonActionScope scope)
		{
			ExWatson.RegisterReportAction(action, scope, null);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00013218 File Offset: 0x00011418
		public static void RegisterReportAction(WatsonReportAction action, WatsonActionScope scope, WatsonReport report)
		{
			Dictionary<Type, List<WatsonReportAction>> reportActions;
			switch (scope)
			{
			case WatsonActionScope.Process:
				reportActions = ExWatson.ProcessWideReportActions;
				lock (reportActions)
				{
					ExWatson.MergeAction(action, reportActions);
					return;
				}
				break;
			case WatsonActionScope.Thread:
				break;
			case WatsonActionScope.Report:
				if (report == null)
				{
					throw new ArgumentNullException("report");
				}
				if (report.ReportActions == null)
				{
					report.ReportActions = new Dictionary<Type, List<WatsonReportAction>>(1);
				}
				reportActions = report.ReportActions;
				ExWatson.MergeAction(action, reportActions);
				return;
			default:
				throw new ArgumentException("Scope must be either WatsonActionScope.Process, WatsonActionScope.Thread or WatsonActionScope.Report", "scope");
			}
			reportActions = ExWatson.CurrentThreadReportActions;
			ExWatson.MergeAction(action, reportActions);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000132C4 File Offset: 0x000114C4
		public static void UnregisterReportAction(WatsonReportAction action, WatsonActionScope scope)
		{
			Dictionary<Type, List<WatsonReportAction>> dictionary;
			switch (scope)
			{
			case WatsonActionScope.Process:
				dictionary = ExWatson.ProcessWideReportActions;
				lock (dictionary)
				{
					ExWatson.RemoveAction(action, dictionary);
					return;
				}
				break;
			case WatsonActionScope.Thread:
				break;
			default:
				throw new ArgumentException("Scope must be either WatsonActionScope.Process or WatsonActionScope.Thread", "scope");
			}
			dictionary = ExWatson.CurrentThreadReportActions;
			ExWatson.RemoveAction(action, dictionary);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00013338 File Offset: 0x00011538
		public static void ClearReportActions(WatsonActionScope scope)
		{
			if (scope == WatsonActionScope.Process)
			{
				Dictionary<Type, List<WatsonReportAction>> dictionary = ExWatson.ProcessWideReportActions;
				lock (dictionary)
				{
					dictionary.Clear();
					return;
				}
			}
			if (scope == WatsonActionScope.Thread)
			{
				Dictionary<Type, List<WatsonReportAction>> dictionary = ExWatson.CurrentThreadReportActions;
				dictionary.Clear();
				return;
			}
			throw new ArgumentException("Scope must be either WatsonActionScope.Process or WatsonActionScope.Thread", "scope");
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000133A0 File Offset: 0x000115A0
		public static void HandleException(object sender, UnhandledExceptionEventArgs e)
		{
			ExWatson.HandleException(e, ReportOptions.None);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00013420 File Offset: 0x00011620
		public static void HandleException(UnhandledExceptionEventArgs e, ReportOptions options)
		{
			ExWatson.<>c__DisplayClass8 CS$<>8__locals1 = new ExWatson.<>c__DisplayClass8();
			CS$<>8__locals1.e = e;
			CS$<>8__locals1.options = options;
			if (CS$<>8__locals1.e.ExceptionObject is ExWatson.CrashNowException)
			{
				return;
			}
			using (Process thisProcess = Process.GetCurrentProcess())
			{
				ExWatson.SendEnglishReport(delegate
				{
					if (CS$<>8__locals1.e.IsTerminating)
					{
						CS$<>8__locals1.options |= ReportOptions.ReportTerminateAfterSend;
					}
					return new WatsonExceptionReport(ExWatson.eventType, thisProcess, CS$<>8__locals1.e.ExceptionObject as Exception, CS$<>8__locals1.options);
				});
			}
			if (ExWatson.killProcessAfterWatson && CS$<>8__locals1.e.IsTerminating)
			{
				ExWatson.TerminateCurrentProcess();
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000134C4 File Offset: 0x000116C4
		public static void SendReport(Exception exception)
		{
			ExWatson.SendReport(exception, ReportOptions.ReportTerminateAfterSend, null);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00013564 File Offset: 0x00011764
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public static void SendReport(Exception exception, ReportOptions options, string extraData)
		{
			if (exception != null && exception is FileNotFoundException && exception.Message.Contains("Could not load file or assembly"))
			{
				return;
			}
			if (ExWatson.ShouldSendCheckThrottle(exception))
			{
				using (Process thisProcess = Process.GetCurrentProcess())
				{
					ExWatson.SendEnglishReport(delegate
					{
						if (ExWatson.IsObjectNotDisposedException(exception))
						{
							options |= (ReportOptions.DoNotCollectDumps | ReportOptions.DeepStackTraceHash);
						}
						WatsonReport watsonReport = new WatsonExceptionReport(ExWatson.eventType, thisProcess, exception, options);
						if (!string.IsNullOrEmpty(extraData))
						{
							ExWatson.RegisterReportAction(new WatsonExtraDataReportAction(extraData), WatsonActionScope.Report, watsonReport);
						}
						return watsonReport;
					});
				}
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001363C File Offset: 0x0001183C
		public static void SendHangWatsonReport(Exception exception, Process process)
		{
			ExWatson.SendEnglishReport(() => new WatsonHangReport("E12", process, exception));
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001366E File Offset: 0x0001186E
		public static void SendReportAndCrashOnAnotherThread(Exception exception)
		{
			ExWatson.SendReportAndCrashOnAnotherThread(exception, ReportOptions.None, null, null);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001367C File Offset: 0x0001187C
		public static void SendReportAndCrashOnAnotherThread(Exception exception, ReportOptions options, ExWatson.CrashNowDelegate crashDelegate, string extraData)
		{
			options |= ReportOptions.ReportTerminateAfterSend;
			options |= ReportOptions.DoNotFreezeThreads;
			ExWatson.SendReport(exception, options, extraData);
			if (crashDelegate == null && ExWatson.killProcessAfterWatson)
			{
				ExWatson.TerminateCurrentProcess();
				return;
			}
			if (Interlocked.CompareExchange(ref ExWatson.crashNowManagedThreadId, Environment.CurrentManagedThreadId, 0) == 0)
			{
				ExWatson.crashNowException = exception;
				ExWatson.crashNowDelegate = crashDelegate;
				ExWatson.crashNowEvent.Set();
				ExWatson.crashNowThread.Join();
				return;
			}
			ExWatson.guardCrashEvent.WaitOne();
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00013720 File Offset: 0x00011920
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public static void SendClientWatsonReport(WatsonClientReport report, string extraData)
		{
			ExWatson.SendEnglishReport(delegate
			{
				if (!string.IsNullOrEmpty(extraData))
				{
					ExWatson.RegisterReportAction(new WatsonExtraDataReportAction(extraData), WatsonActionScope.Report, report);
				}
				return report;
			});
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001379C File Offset: 0x0001199C
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public static void SendLatencyWatsonReport(string triggerVersion, string locationIdentity, string exceptionName, string callstack, string methodName, string detailedExceptionInformation)
		{
			ExWatson.SendEnglishReport(() => new WatsonLatencyReport(ExWatson.eventType, triggerVersion, locationIdentity, exceptionName, callstack, methodName, detailedExceptionInformation));
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00013838 File Offset: 0x00011A38
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public static void SendTroubleshootingWatsonReport(string triggerVersion, string locationIdentity, string exceptionName, string callstack, string methodName, string detailedExceptionInformation, string traceFilePath)
		{
			ExWatson.SendEnglishReport(() => new WatsonTroubleshootingReport(ExWatson.eventType, triggerVersion, locationIdentity, exceptionName, callstack, methodName, detailedExceptionInformation, traceFilePath));
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000138F0 File Offset: 0x00011AF0
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public static void SendGenericWatsonReport(string eventType, string appVersion, string appName, string assemblyVersion, string assemblyName, string exceptionType, string callstack, string callstackHash, string methodName, string detailedExceptionInformation)
		{
			ExWatson.SendEnglishReport(() => new WatsonGenericReport(ExWatson.ParseEventType(eventType), appVersion, appName, assemblyVersion, assemblyName, exceptionType, callstack, callstackHash, methodName, detailedExceptionInformation));
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00013960 File Offset: 0x00011B60
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public static void SendThrottledGenericWatsonReport(string eventType, string appVersion, string appName, string assemblyVersion, string assemblyName, string exceptionType, string callstack, string callstackHash, string methodName, string detailedExceptionInformation, TimeSpan throttlingInterval, out bool wasThrottled)
		{
			wasThrottled = true;
			if (ExWatson.ShouldSendInfoWatson(callstackHash, throttlingInterval))
			{
				wasThrottled = false;
				ExWatson.SendGenericWatsonReport(eventType, appVersion, appName, assemblyVersion, assemblyName, exceptionType, callstack, callstackHash, methodName, detailedExceptionInformation);
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00013A1C File Offset: 0x00011C1C
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public static void SendExternalProcessWatsonReportWithFiles(Process process, string eventType, Exception exception, string detailedExceptionInformation, string[] filesToUpload, ReportOptions reportOptions)
		{
			ExWatson.SendEnglishReport(delegate
			{
				reportOptions |= ReportOptions.DoNotFreezeThreads;
				WatsonExternalProcessReport watsonExternalProcessReport = new WatsonExternalProcessReport(process, ExWatson.ParseEventType(eventType), exception, detailedExceptionInformation, reportOptions);
				if (filesToUpload != null)
				{
					foreach (string text in filesToUpload)
					{
						if (File.Exists(text))
						{
							ExWatson.RegisterReportAction(new WatsonExtraFileReportAction(text), WatsonActionScope.Report, watsonExternalProcessReport);
						}
					}
				}
				return watsonExternalProcessReport;
			});
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00013A6F File Offset: 0x00011C6F
		public static void SendReportOnUnhandledException(ExWatson.MethodDelegate methodDelegate)
		{
			ExWatson.SendReportOnUnhandledException(methodDelegate, (object exception) => true, ReportOptions.ReportTerminateAfterSend);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00013A95 File Offset: 0x00011C95
		public static void SendReportOnUnhandledException(ExWatson.MethodDelegate methodDelegate, ExWatson.IsExceptionInteresting exceptionInteresting)
		{
			ExWatson.SendReportOnUnhandledException(methodDelegate, exceptionInteresting, ReportOptions.ReportTerminateAfterSend);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00013ADC File Offset: 0x00011CDC
		public static void SendReportOnUnhandledException(ExWatson.MethodDelegate methodDelegate, ExWatson.IsExceptionInteresting exceptionInteresting, ReportOptions options)
		{
			ExWatson.<>c__DisplayClass2b CS$<>8__locals1 = new ExWatson.<>c__DisplayClass2b();
			CS$<>8__locals1.methodDelegate = methodDelegate;
			CS$<>8__locals1.exceptionInteresting = exceptionInteresting;
			CS$<>8__locals1.options = options;
			ILUtil.DoTryFilterCatch(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<SendReportOnUnhandledException>b__27)), new FilterDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<SendReportOnUnhandledException>b__28)), new CatchDelegate(null, (UIntPtr)ldftn(<SendReportOnUnhandledException>b__29)));
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00013B3E File Offset: 0x00011D3E
		public static void AddExtraData(string data)
		{
			if (!string.IsNullOrEmpty(data))
			{
				ExWatson.RegisterReportAction(new WatsonExtraDataReportAction(data), WatsonActionScope.Process);
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00013B54 File Offset: 0x00011D54
		public static bool TryAddExtraFile(string filename)
		{
			if (File.Exists(filename))
			{
				ExWatson.RegisterReportAction(new WatsonExtraFileReportAction(filename), WatsonActionScope.Process);
				return true;
			}
			return false;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00013B70 File Offset: 0x00011D70
		public static bool TryGetRealApplicationVersion(Process appProcess, out Version appVersion)
		{
			bool result = false;
			appVersion = null;
			try
			{
				if (appProcess != null && (appProcess.MainModule.FileVersionInfo.ProductName.StartsWith("Microsoft® Exchange") || appProcess.MainModule.FileVersionInfo.ProductName.StartsWith("Microsoft Exchange")))
				{
					appVersion = new Version(appProcess.MainModule.FileVersionInfo.FileVersion);
					result = true;
				}
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00013BEC File Offset: 0x00011DEC
		public static void IncrementWatsonCountIn()
		{
			Interlocked.Increment(ref ExWatson.watsonCountIn);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00013BF9 File Offset: 0x00011DF9
		public static void IncrementWatsonCountOut()
		{
			Interlocked.Increment(ref ExWatson.watsonCountOut);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00013C06 File Offset: 0x00011E06
		public static void SetWatsonReportAlreadySent(Exception exception)
		{
			if (exception.Data != null)
			{
				exception.Data["WatsonReportAlreadySent"] = null;
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00013C21 File Offset: 0x00011E21
		public static bool IsWatsonReportAlreadySent(Exception exception)
		{
			while (exception != null)
			{
				if (exception.Data != null && exception.Data.Contains("WatsonReportAlreadySent"))
				{
					return true;
				}
				exception = exception.InnerException;
			}
			return false;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00013C50 File Offset: 0x00011E50
		internal static string GetRealAppName(string appName, string commandLineArgs)
		{
			if (ExWatson.ProcessNameRemapSet.Contains(appName))
			{
				if (appName.Equals("w3wp.exe", StringComparison.OrdinalIgnoreCase))
				{
					Match match = Regex.Match(commandLineArgs, "w3wp\\.exe -ap \"(.*?)\"");
					if (match.Success)
					{
						return "w3wp#" + match.Groups[1].Value;
					}
				}
				else if (appName.Equals("svchost.exe", StringComparison.OrdinalIgnoreCase))
				{
					int num = commandLineArgs.IndexOf("svchost.exe -k", StringComparison.OrdinalIgnoreCase);
					if (num >= 0)
					{
						return "svchost#" + commandLineArgs.Substring(num + "svchost.exe -k".Length + 1);
					}
				}
				else if (appName.Equals("noderunner.exe", StringComparison.OrdinalIgnoreCase))
				{
					foreach (string text in ExWatson.NodeRunnerInstanceNames)
					{
						if (commandLineArgs.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
						{
							return "NodeRunner#" + text;
						}
					}
				}
			}
			return appName;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00013D38 File Offset: 0x00011F38
		internal static bool ShouldSendInfoWatson(string callstackHash, TimeSpan throttlingInterval)
		{
			DateTime utcNow = DateTime.UtcNow;
			if (string.IsNullOrEmpty(callstackHash) || throttlingInterval == TimeSpan.Zero)
			{
				return true;
			}
			bool result;
			lock (ExWatson.infoWatsonLock)
			{
				InfoWatsonThrottlingData infoWatsonThrottlingData;
				if (ExWatson.infoWatsonThrottling.TryGetValue(callstackHash, out infoWatsonThrottlingData))
				{
					infoWatsonThrottlingData.LastAccessTimeUtc = utcNow;
					if (utcNow >= infoWatsonThrottlingData.NextAllowableLogTimeUtc)
					{
						infoWatsonThrottlingData.NextAllowableLogTimeUtc = utcNow + throttlingInterval;
						result = true;
					}
					else
					{
						result = false;
					}
				}
				else
				{
					ExWatson.PurgeInfoWatsonThrottlingDictionary();
					infoWatsonThrottlingData = new InfoWatsonThrottlingData(callstackHash, utcNow + throttlingInterval);
					ExWatson.infoWatsonThrottling[callstackHash] = infoWatsonThrottlingData;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00013DEC File Offset: 0x00011FEC
		internal static bool IsObjectNotDisposedException(Exception exception)
		{
			Type type = exception.GetType();
			return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(ObjectNotDisposedException<>);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00013E24 File Offset: 0x00012024
		internal static void TerminateCurrentProcess()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				DiagnosticsNativeMethods.TerminateProcess(currentProcess.Handle, -559034355);
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00013E64 File Offset: 0x00012064
		internal static void FreezeAllThreads()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				int currentThreadId = DiagnosticsNativeMethods.GetCurrentThreadId();
				for (int i = ExWatson.firstUserThread; i < currentProcess.Threads.Count; i++)
				{
					ProcessThread processThread = currentProcess.Threads[i];
					int id = processThread.Id;
					if (id != currentThreadId)
					{
						using (ThreadSafeHandle threadSafeHandle = DiagnosticsNativeMethods.OpenThread(DiagnosticsNativeMethods.ThreadAccess.SuspendResume, false, id))
						{
							DiagnosticsNativeMethods.SuspendThread(threadSafeHandle);
						}
					}
				}
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00013EFC File Offset: 0x000120FC
		internal static bool TryRegistryKeyGetValue<T>(string regKeyPath, string name, T defaultValue, out T returnValue)
		{
			bool result = false;
			RegistryKey registryKey = null;
			returnValue = defaultValue;
			try
			{
				registryKey = Registry.LocalMachine.OpenSubKey(regKeyPath, false);
				if (registryKey != null)
				{
					result = ExWatson.TryRegistryKeyGetValue<T>(registryKey, name, defaultValue, out returnValue);
				}
			}
			catch
			{
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return result;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00013F5C File Offset: 0x0001215C
		internal static void EvaluateReportActions(XmlWriter writer, WatsonReport report)
		{
			Dictionary<Type, List<WatsonReportAction>> dictionary = ExWatson.processWideReportActions;
			Dictionary<Type, List<WatsonReportAction>> dictionary2 = ExWatson.currentThreadReportActions;
			Dictionary<Type, List<WatsonReportAction>> reportActions = report.ReportActions;
			if (dictionary != null)
			{
				List<WatsonReportAction> list = null;
				int num = 0;
				lock (dictionary)
				{
					foreach (List<WatsonReportAction> list2 in dictionary.Values)
					{
						num += list2.Count;
					}
					list = new List<WatsonReportAction>(num);
					foreach (List<WatsonReportAction> collection in dictionary.Values)
					{
						list.AddRange(collection);
					}
				}
				for (int i = 0; i < num; i++)
				{
					list[i].WriteResult(report, writer);
				}
			}
			if (dictionary2 != null)
			{
				foreach (KeyValuePair<Type, List<WatsonReportAction>> keyValuePair in dictionary2)
				{
					foreach (WatsonReportAction watsonReportAction in keyValuePair.Value)
					{
						watsonReportAction.WriteResult(report, writer);
					}
				}
			}
			if (reportActions != null)
			{
				foreach (KeyValuePair<Type, List<WatsonReportAction>> keyValuePair2 in reportActions)
				{
					foreach (WatsonReportAction watsonReportAction2 in keyValuePair2.Value)
					{
						watsonReportAction2.WriteResult(report, writer);
					}
				}
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00014170 File Offset: 0x00012370
		internal static bool ShouldSubmitReport(WatsonReport report, string reportXmlFileName, string reportTextFileName, ref DiagnosticsNativeMethods.WER_SUBMIT_RESULT submitResult)
		{
			ExWatson.IWatsonTestHook testHook = ExWatson.TestHook;
			if (testHook != null)
			{
				try
				{
					return testHook.ShouldSubmitReport(report, reportXmlFileName, reportTextFileName, ref submitResult);
				}
				catch
				{
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000141AC File Offset: 0x000123AC
		internal static ExWatson.IWatsonTestHook SetTestHook(ExWatson.IWatsonTestHook newTestHook)
		{
			ExWatson.IWatsonTestHook testHook = ExWatson.TestHook;
			ExWatson.TestHook = newTestHook;
			return testHook;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000141C8 File Offset: 0x000123C8
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private static void SendEnglishReport(Func<WatsonReport> reportCallback)
		{
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
			try
			{
				CultureInfo cultureInfo = CultureInfo.GetCultureInfo("en-US");
				Thread.CurrentThread.CurrentCulture = cultureInfo;
				Thread.CurrentThread.CurrentUICulture = cultureInfo;
				WatsonReport watsonReport = null;
				lock (ExWatson.mutex)
				{
					ExWatson.EnsureWatsonInitialization();
					watsonReport = reportCallback();
				}
				watsonReport.Send();
				Interlocked.Increment(ref ExWatson.watsonReportCount);
			}
			finally
			{
				Thread.CurrentThread.CurrentCulture = currentCulture;
				Thread.CurrentThread.CurrentUICulture = currentUICulture;
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00014280 File Offset: 0x00012480
		private static void PurgeInfoWatsonThrottlingDictionary()
		{
			DateTime utcNow = DateTime.UtcNow;
			if (ExWatson.infoWatsonThrottling.Count >= 512)
			{
				InfoWatsonThrottlingData infoWatsonThrottlingData = null;
				ExWatson.infoWatsonEntriesToRemove.Clear();
				foreach (InfoWatsonThrottlingData infoWatsonThrottlingData2 in ExWatson.infoWatsonThrottling.Values)
				{
					if (utcNow >= infoWatsonThrottlingData2.NextAllowableLogTimeUtc)
					{
						ExWatson.infoWatsonEntriesToRemove.Add(infoWatsonThrottlingData2);
					}
					if (infoWatsonThrottlingData == null || infoWatsonThrottlingData2.LastAccessTimeUtc < infoWatsonThrottlingData.LastAccessTimeUtc)
					{
						infoWatsonThrottlingData = infoWatsonThrottlingData2;
					}
				}
				if (ExWatson.infoWatsonEntriesToRemove.Count > 0)
				{
					using (List<InfoWatsonThrottlingData>.Enumerator enumerator2 = ExWatson.infoWatsonEntriesToRemove.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							InfoWatsonThrottlingData infoWatsonThrottlingData3 = enumerator2.Current;
							ExWatson.infoWatsonThrottling.Remove(infoWatsonThrottlingData3.Hash);
						}
						return;
					}
				}
				ExWatson.infoWatsonThrottling.Remove(infoWatsonThrottlingData.Hash);
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00014394 File Offset: 0x00012594
		private static int InitFirstUserThread()
		{
			int result;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				result = currentProcess.Threads.Count - 1;
			}
			return result;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000143D4 File Offset: 0x000125D4
		private static void ReInit()
		{
			int num = 0;
			if (ExWatson.TryRegistryKeyGetValue<int>("SOFTWARE\\Microsoft\\.NETFramework", "DbgJITDebugLaunchSetting", 0, out num))
			{
				ExWatson.killProcessAfterWatson = (num == 0);
			}
			if (ExWatson.TryRegistryKeyGetValue<int>("SOFTWARE\\Microsoft\\ExchangeServer\\v15", "DisableErrorReporting", 1, out num))
			{
				ExWatson.WatsonAllowed = (num == 0);
			}
			else if (ExWatson.TryRegistryKeyGetValue<int>("SOFTWARE\\Microsoft\\OLMA", "DisableErrorReporting", 1, out num))
			{
				ExWatson.WatsonAllowed = (num == 0);
			}
			else if (ExWatson.TryRegistryKeyGetValue<int>("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Load Generator", "DisableErrorReporting", 1, out num))
			{
				ExWatson.WatsonAllowed = (num == 0);
			}
			else if (ExWatson.TryRegistryKeyGetValue<int>("SOFTWARE\\Microsoft\\ExRCA", "DisableErrorReporting", 1, out num))
			{
				ExWatson.WatsonAllowed = (num == 0);
			}
			if (ExWatson.TryRegistryKeyGetValue<int>("SOFTWARE\\Microsoft\\ExchangeServer\\v15", "DisableReaperThread", 1, out num))
			{
				ExWatson.ReaperThreadAllowed = (num == 0);
			}
			if (ExWatson.crashNowThread == null)
			{
				ExWatson.crashNowEvent = new ManualResetEvent(false);
				ExWatson.guardCrashEvent = new ManualResetEvent(false);
				ExWatson.crashNowThreadIsRunning = new ManualResetEvent(false);
				ExWatson.crashNowThread = new Thread(new ThreadStart(ExWatson.CrashNow));
				ExWatson.crashNowThread.Name = "ExWatson CrashNow Thread";
				ExWatson.crashNowThread.IsBackground = true;
				ExWatson.crashNowThread.Start();
				ExWatson.crashNowThreadIsRunning.WaitOne();
			}
			if (ExWatson.MsInternal)
			{
				string text = string.Empty;
				if (ExWatson.TryRegistryKeyGetValue<string>("SOFTWARE\\Microsoft\\ExchangeServer\\v15", "LabName", string.Empty, out text) && !string.IsNullOrEmpty(text))
				{
					if (text.Length > 32)
					{
						text = text.Substring(0, 32);
					}
					ExWatson.labName = text + "-";
					ExWatson.TestLabMachine = ExWatson.TestLabNameSet.Contains(text);
					return;
				}
			}
			else
			{
				ExWatson.labName = "c-";
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00014584 File Offset: 0x00012784
		private static void MergeAction(WatsonReportAction action, Dictionary<Type, List<WatsonReportAction>> allActions)
		{
			List<WatsonReportAction> reportActionListForType = ExWatson.GetReportActionListForType(allActions, action.GetType());
			if (!reportActionListForType.Contains(action))
			{
				reportActionListForType.Add(action);
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000145B0 File Offset: 0x000127B0
		private static void RemoveAction(WatsonReportAction action, Dictionary<Type, List<WatsonReportAction>> allActions)
		{
			List<WatsonReportAction> reportActionListForType = ExWatson.GetReportActionListForType(allActions, action.GetType());
			reportActionListForType.Remove(action);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x000145D4 File Offset: 0x000127D4
		private static List<WatsonReportAction> GetReportActionListForType(Dictionary<Type, List<WatsonReportAction>> allActions, Type type)
		{
			List<WatsonReportAction> list = null;
			if (!allActions.TryGetValue(type, out list))
			{
				list = new List<WatsonReportAction>(1);
				allActions.Add(type, list);
			}
			return list;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00014600 File Offset: 0x00012800
		private static bool ShouldSendCheckThrottle(Exception exception)
		{
			TimeSpan exceptionThrottlingTimeout = ExWatson.throttlingPeriod;
			ExWatson.IWatsonTestHook testHook = ExWatson.TestHook;
			if (testHook != null)
			{
				try
				{
					if (testHook.ShouldSkipThrottling(exception))
					{
						return true;
					}
					exceptionThrottlingTimeout = testHook.GetExceptionThrottlingTimeout(exception, exceptionThrottlingTimeout);
				}
				catch
				{
				}
			}
			try
			{
				if (exception is ThreadAbortException && DateTime.UtcNow - ExWatson.lastWatsonReport < TimeSpan.FromMinutes(5.0))
				{
					return false;
				}
				if (exceptionThrottlingTimeout == TimeSpan.Zero)
				{
					ExWatson.lastWatsonReport = DateTime.UtcNow;
					return true;
				}
				string text = string.Empty;
				WatsonExceptionReport.TryStringHashFromStackTrace(exception, false, out text);
				text += exception.GetType().ToString();
				lock (ExWatson.watsonLock)
				{
					DateTime d;
					if (ExWatson.watsonThrottling.TryGetValue(text, out d))
					{
						if (DateTime.UtcNow - d > exceptionThrottlingTimeout)
						{
							ExWatson.watsonThrottling[text] = DateTime.UtcNow;
							ExWatson.lastWatsonReport = DateTime.UtcNow;
							return true;
						}
						return false;
					}
					else
					{
						if (ExWatson.watsonThrottling.Count > 200)
						{
							ExWatson.watsonThrottling.Clear();
						}
						ExWatson.watsonThrottling[text] = DateTime.UtcNow;
					}
				}
			}
			catch
			{
			}
			ExWatson.lastWatsonReport = DateTime.UtcNow;
			return true;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x000147A0 File Offset: 0x000129A0
		private static bool TryGetExchangeVersionInstalled(out Version installedVersion)
		{
			bool result = false;
			RegistryKey registryKey = null;
			int num = 0;
			int minor = 0;
			int build = 0;
			int revision = 0;
			installedVersion = null;
			try
			{
				registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup");
				if (registryKey != null)
				{
					ExWatson.TryRegistryKeyGetValue<int>(registryKey, "MsiProductMajor", 0, out num);
					ExWatson.TryRegistryKeyGetValue<int>(registryKey, "MsiProductMinor", 0, out minor);
					ExWatson.TryRegistryKeyGetValue<int>(registryKey, "MsiBuildMajor", 0, out build);
					ExWatson.TryRegistryKeyGetValue<int>(registryKey, "MsiBuildMinor", 0, out revision);
					if (num > 0)
					{
						installedVersion = new Version(num, minor, build, revision);
						result = true;
					}
				}
			}
			catch
			{
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return result;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00014850 File Offset: 0x00012A50
		private static bool TryGetDefaultAssemblyVersion(out Version assemblyVersion)
		{
			bool result = false;
			assemblyVersion = null;
			try
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
				if (customAttributes.Length == 1)
				{
					AssemblyFileVersionAttribute assemblyFileVersionAttribute = customAttributes[0] as AssemblyFileVersionAttribute;
					if (assemblyFileVersionAttribute != null)
					{
						assemblyVersion = new Version(assemblyFileVersionAttribute.Version);
						result = true;
					}
				}
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000148B0 File Offset: 0x00012AB0
		private static bool TryGetComputerName(out string computerName)
		{
			bool result = false;
			computerName = null;
			try
			{
				computerName = Environment.MachineName;
				result = true;
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000148E4 File Offset: 0x00012AE4
		private static bool TryGetProcessorArchitecture(out string processorArch)
		{
			bool result = true;
			processorArch = null;
			if (IntPtr.Size == 4)
			{
				processorArch = "x86";
			}
			else if (IntPtr.Size == 8)
			{
				processorArch = "AMD64";
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001491C File Offset: 0x00012B1C
		private static bool TryRegistryKeyGetValue<T>(RegistryKey regKey, string name, T defaultValue, out T returnValue)
		{
			bool result = false;
			returnValue = defaultValue;
			try
			{
				if (regKey != null)
				{
					returnValue = (T)((object)regKey.GetValue(name, defaultValue));
					result = true;
				}
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00014968 File Offset: 0x00012B68
		private static void CrashNow()
		{
			ExWatson.nativeCrashNowThreadId = DiagnosticsNativeMethods.GetCurrentThreadId();
			ExWatson.crashNowThreadIsRunning.Set();
			ExWatson.crashNowEvent.WaitOne();
			string message = string.Format("Crashing because of a request from another thread. ManagedThreadId = {0} (0x{0:x})", ExWatson.crashNowManagedThreadId);
			if (ExWatson.crashNowDelegate != null)
			{
				ExWatson.crashNowDelegate(ExWatson.crashNowException, ExWatson.crashNowManagedThreadId);
			}
			if (ExWatson.crashNowException == null)
			{
				throw new ExWatson.CrashNowException(message);
			}
			throw new ExWatson.CrashNowException(message, ExWatson.crashNowException);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000149DE File Offset: 0x00012BDE
		private static void EnsureWatsonInitialization()
		{
			if (!ExWatson.initialized)
			{
				ExWatson.Init();
				return;
			}
			ExWatson.ReInit();
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000149F2 File Offset: 0x00012BF2
		private static string ParseEventType(string eventTypeName)
		{
			if (!ExWatson.WatsonEventSet.Contains(eventTypeName))
			{
				eventTypeName = "E12IIS";
			}
			return eventTypeName;
		}

		// Token: 0x0400037A RID: 890
		public const string Exchange12 = "E12";

		// Token: 0x0400037B RID: 891
		public const string Exchange12IE = "E12IE";

		// Token: 0x0400037C RID: 892
		public const string Exchange12IIS = "E12IIS";

		// Token: 0x0400037D RID: 893
		internal const string ExchangeRegistryKeyWatsonThrottlingDWord = "PerIssueErrorReportingIntervalInMinutes";

		// Token: 0x0400037E RID: 894
		internal const int DefaultThrottlingIntervalInMinutes = 45;

		// Token: 0x0400037F RID: 895
		internal const string ExchangeRegistryKeyPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15";

		// Token: 0x04000380 RID: 896
		internal const string ExchangeRegistryKeyDisableErrorReportingDWord = "DisableErrorReporting";

		// Token: 0x04000381 RID: 897
		internal const string ExchangeRegistryKeyLabNameString = "LabName";

		// Token: 0x04000382 RID: 898
		internal const int MaxInfoWatsonThrottlingDictionarySize = 512;

		// Token: 0x04000383 RID: 899
		private const string Unknown = "unknown";

		// Token: 0x04000384 RID: 900
		private const string GALSyncRegistryKeyPath = "SOFTWARE\\Microsoft\\OLMA";

		// Token: 0x04000385 RID: 901
		private const string LoadGenRegistryKeyPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Load Generator";

		// Token: 0x04000386 RID: 902
		private const string ExRCARegistryKeyPath = "SOFTWARE\\Microsoft\\ExRCA";

		// Token: 0x04000387 RID: 903
		private const string ExchangeRegistryKeyDisableReaperThreadDWord = "DisableReaperThread";

		// Token: 0x04000388 RID: 904
		private const string ExchangeSetupRegistryKeyPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup";

		// Token: 0x04000389 RID: 905
		private const string ExchangeProductUninstallRegistryKeyPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{4934D1EA-BE46-48B1-8847-F1AF20E892C1}";

		// Token: 0x0400038A RID: 906
		private const string ExchangeProductUninstallRegistryKeyInstallSourceString = "InstallSource";

		// Token: 0x0400038B RID: 907
		private const string WatsonProductsRegistryKeyPath = "Software\\Microsoft\\PCHealth\\ErrorReporting\\DW\\Products";

		// Token: 0x0400038C RID: 908
		private const string DotNetFrameworkRegistryKeyPath = "SOFTWARE\\Microsoft\\.NETFramework";

		// Token: 0x0400038D RID: 909
		private const string DotNetFrameworkRegistryKeyDbgJITDWord = "DbgJITDebugLaunchSetting";

		// Token: 0x0400038E RID: 910
		private const int MinThrottlingIntervalInMinutes = 0;

		// Token: 0x0400038F RID: 911
		private const int MaxThrottlingIntervalInMinutes = 43200;

		// Token: 0x04000390 RID: 912
		private const int MaxThrottlingDictionarySize = 200;

		// Token: 0x04000391 RID: 913
		private const string WatsonReportAlreadySentTag = "WatsonReportAlreadySent";

		// Token: 0x04000392 RID: 914
		private static readonly HashSet<string> TestLabNameSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"DART",
			"FUZZ",
			"EXTST",
			"E15_EXTST"
		};

		// Token: 0x04000393 RID: 915
		private static readonly HashSet<string> ProcessNameRemapSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"w3wp.exe",
			"noderunner.exe",
			"svchost.exe"
		};

		// Token: 0x04000394 RID: 916
		private static readonly string[] NodeRunnerInstanceNames = new string[]
		{
			"AdminNode1",
			"ContentEngineNode1",
			"IndexNode1",
			"InteractionEngineNode1"
		};

		// Token: 0x04000395 RID: 917
		private static readonly HashSet<string> WatsonEventSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"E12",
			"E12IE",
			"E12IIS"
		};

		// Token: 0x04000396 RID: 918
		private static readonly object infoWatsonLock = new object();

		// Token: 0x04000397 RID: 919
		private static Dictionary<string, InfoWatsonThrottlingData> infoWatsonThrottling = new Dictionary<string, InfoWatsonThrottlingData>(0);

		// Token: 0x04000398 RID: 920
		private static object mutex = new object();

		// Token: 0x04000399 RID: 921
		private static bool killProcessAfterWatson = true;

		// Token: 0x0400039A RID: 922
		private static string eventType = "E12IIS";

		// Token: 0x0400039B RID: 923
		private static bool initialized;

		// Token: 0x0400039C RID: 924
		private static TimeSpan throttlingPeriod = TimeSpan.Zero;

		// Token: 0x0400039D RID: 925
		private static Dictionary<string, DateTime> watsonThrottling = new Dictionary<string, DateTime>(0);

		// Token: 0x0400039E RID: 926
		private static object watsonLock = new object();

		// Token: 0x0400039F RID: 927
		private static List<InfoWatsonThrottlingData> infoWatsonEntriesToRemove = new List<InfoWatsonThrottlingData>(256);

		// Token: 0x040003A0 RID: 928
		private static DateTime lastWatsonReport = DateTime.MinValue;

		// Token: 0x040003A1 RID: 929
		private static long watsonReportCount = 0L;

		// Token: 0x040003A2 RID: 930
		private static string applicationName;

		// Token: 0x040003A3 RID: 931
		private static string realApplicationName;

		// Token: 0x040003A4 RID: 932
		private static string computerName;

		// Token: 0x040003A5 RID: 933
		private static string labName = string.Empty;

		// Token: 0x040003A6 RID: 934
		private static Version exchangeVersion;

		// Token: 0x040003A7 RID: 935
		private static string exchangeInstallSource;

		// Token: 0x040003A8 RID: 936
		private static string exchangeInstallPath;

		// Token: 0x040003A9 RID: 937
		private static Version defaultAssemblyVersion;

		// Token: 0x040003AA RID: 938
		private static Version realApplicationVersion;

		// Token: 0x040003AB RID: 939
		private static string processorArchitecture;

		// Token: 0x040003AC RID: 940
		private static int watsonCountIn;

		// Token: 0x040003AD RID: 941
		private static int watsonCountOut;

		// Token: 0x040003AE RID: 942
		private static TimeSpan crashReportTimeout = new TimeSpan(0, 30, 0);

		// Token: 0x040003AF RID: 943
		[ThreadStatic]
		private static Dictionary<Type, List<WatsonReportAction>> currentThreadReportActions;

		// Token: 0x040003B0 RID: 944
		private static Dictionary<Type, List<WatsonReportAction>> processWideReportActions;

		// Token: 0x040003B1 RID: 945
		private static int firstUserThread = ExWatson.InitFirstUserThread();

		// Token: 0x040003B2 RID: 946
		private static Thread crashNowThread;

		// Token: 0x040003B3 RID: 947
		private static ManualResetEvent crashNowThreadIsRunning;

		// Token: 0x040003B4 RID: 948
		private static ManualResetEvent crashNowEvent;

		// Token: 0x040003B5 RID: 949
		private static ManualResetEvent guardCrashEvent;

		// Token: 0x040003B6 RID: 950
		private static Exception crashNowException;

		// Token: 0x040003B7 RID: 951
		private static ExWatson.CrashNowDelegate crashNowDelegate;

		// Token: 0x040003B8 RID: 952
		private static int crashNowManagedThreadId;

		// Token: 0x040003B9 RID: 953
		private static int nativeCrashNowThreadId;

		// Token: 0x020000B9 RID: 185
		// (Invoke) Token: 0x0600052E RID: 1326
		public delegate void MethodDelegate();

		// Token: 0x020000BA RID: 186
		// (Invoke) Token: 0x06000532 RID: 1330
		public delegate void CrashNowDelegate(Exception exception, int threadId);

		// Token: 0x020000BB RID: 187
		// (Invoke) Token: 0x06000536 RID: 1334
		public delegate bool IsExceptionInteresting(object exception);

		// Token: 0x020000BC RID: 188
		public enum EventType
		{
			// Token: 0x040003C4 RID: 964
			E12,
			// Token: 0x040003C5 RID: 965
			E12IIS,
			// Token: 0x040003C6 RID: 966
			E12IE
		}

		// Token: 0x020000BD RID: 189
		internal interface IWatsonTestHook
		{
			// Token: 0x06000539 RID: 1337
			bool ShouldSkipThrottling(Exception ex);

			// Token: 0x0600053A RID: 1338
			TimeSpan GetExceptionThrottlingTimeout(Exception ex, TimeSpan defaultTimeout);

			// Token: 0x0600053B RID: 1339
			bool ShouldSubmitReport(WatsonReport report, string reportXmlFileName, string reportTextFileName, ref DiagnosticsNativeMethods.WER_SUBMIT_RESULT submitResult);
		}

		// Token: 0x020000BE RID: 190
		private class CrashNowException : Exception
		{
			// Token: 0x0600053C RID: 1340 RVA: 0x00014B81 File Offset: 0x00012D81
			public CrashNowException(string message) : base(message)
			{
			}

			// Token: 0x0600053D RID: 1341 RVA: 0x00014B8A File Offset: 0x00012D8A
			public CrashNowException(string message, Exception innerException) : base(message, innerException)
			{
			}
		}
	}
}
