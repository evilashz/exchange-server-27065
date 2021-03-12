using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Audit;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Diagnostics.FaultInjection;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Cluster.ReplayService
{
	// Token: 0x02000005 RID: 5
	internal sealed class ReplayService : ReplayServiceBase
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002C53 File Offset: 0x00000E53
		private ReplayService()
		{
			base.CanStop = true;
			base.CanPauseAndContinue = false;
			base.ServiceName = "MSExchangeRepl";
			base.AutoLog = false;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002C7C File Offset: 0x00000E7C
		private static bool IsRunningAsService()
		{
			bool flag = false;
			bool flag2 = false;
			bool result;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				IdentityReferenceCollection groups = current.Groups;
				foreach (IdentityReference identityReference in groups)
				{
					if (identityReference is SecurityIdentifier)
					{
						SecurityIdentifier securityIdentifier = (SecurityIdentifier)identityReference;
						if (securityIdentifier.IsWellKnown(WellKnownSidType.ServiceSid))
						{
							flag = true;
						}
						if (securityIdentifier.IsWellKnown(WellKnownSidType.InteractiveSid))
						{
							flag2 = true;
						}
					}
				}
				result = (flag || (!flag && !flag2));
			}
			return result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002D2C File Offset: 0x00000F2C
		public static void Main(string[] args)
		{
			ExWatson.Register();
			int num = ReplayService.MainInternal(args);
			if (num != 0)
			{
				ExTraceGlobals.ReplayManagerTracer.TraceError<int>(0L, "Exiting process with error code {0}", num);
				Environment.Exit(num);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D60 File Offset: 0x00000F60
		[Conditional("DEBUG")]
		private static void DebugServiceStartupDelay()
		{
			int testServiceStartupDelay = RegistryParameters.TestServiceStartupDelay;
			if (testServiceStartupDelay > 0)
			{
				ExTraceGlobals.ReplayManagerTracer.TraceDebug<int>(0L, "DebugServiceStartupDelay: sleeping for {0} secs", testServiceStartupDelay);
				Thread.Sleep(testServiceStartupDelay * 1000);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002D98 File Offset: 0x00000F98
		private static bool DisableDebugPriv()
		{
			SafeTokenHandle invalidHandle = SafeTokenHandle.InvalidHandle;
			if (!NativeMethods.OpenProcessToken(NativeMethods.GetCurrentProcess(), TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges, ref invalidHandle))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				ExTraceGlobals.ReplayManagerTracer.TraceError<int>(0L, "OpenProcessToken failed with {0}", lastWin32Error);
				return false;
			}
			try
			{
				NativeMethods.LUID luid;
				luid.LowPart = 0U;
				luid.HighPart = 0U;
				if (!NativeMethods.LookupPrivilegeValue(null, "SeDebugPrivilege", ref luid))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					ExTraceGlobals.ReplayManagerTracer.TraceError<int>(0L, "LookupPrivilegeValue failed with {0}", lastWin32Error);
					return false;
				}
				NativeMethods.TOKEN_PRIVILEGE token_PRIVILEGE = default(NativeMethods.TOKEN_PRIVILEGE);
				token_PRIVILEGE.PrivilegeCount = 1U;
				token_PRIVILEGE.Privilege.Luid = luid;
				token_PRIVILEGE.Privilege.Attributes = 0U;
				NativeMethods.TOKEN_PRIVILEGE token_PRIVILEGE2 = default(NativeMethods.TOKEN_PRIVILEGE);
				uint num = 0U;
				if (!NativeMethods.AdjustTokenPrivileges(invalidHandle, false, ref token_PRIVILEGE, (uint)Marshal.SizeOf(token_PRIVILEGE2), ref token_PRIVILEGE2, ref num))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					ExTraceGlobals.ReplayManagerTracer.TraceError<int>(0L, "AdjustTokenPrivileges failed with {0}", lastWin32Error);
					return false;
				}
			}
			finally
			{
				invalidHandle.Dispose();
			}
			return true;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E9C File Offset: 0x0000109C
		private static int MainInternal(string[] args)
		{
			string userName = Environment.UserName;
			ExTraceGlobals.ReplayManagerTracer.TraceDebug<string>(0L, "Running as {0}", userName);
			string[] array = new string[]
			{
				"SeAuditPrivilege",
				"SeChangeNotifyPrivilege",
				"SeCreateGlobalPrivilege",
				"SeDebugPrivilege"
			};
			if (!ReplayService.DisableDebugPriv())
			{
				int num = array.Length - 1;
				string[] array2 = new string[num];
				Array.Copy(array, array2, num);
				array = array2;
			}
			int num2 = Privileges.RemoveAllExcept(array);
			if (num2 != 0)
			{
				return num2;
			}
			bool flag = !ReplayService.IsRunningAsService();
			bool flag2 = true;
			bool isVerbose = false;
			bool flag3 = false;
			bool isConfirm = true;
			string dbName = null;
			bool flag4 = false;
			foreach (string text in args)
			{
				string text2 = text.ToLower();
				Match match;
				if (text2 == "-console")
				{
					flag = true;
				}
				else if (text2 == "-noprompt")
				{
					flag2 = false;
				}
				else if (text2 == "-v")
				{
					isVerbose = true;
				}
				else if (text2 == "-y")
				{
					isConfirm = false;
				}
				else if ((match = Regex.Match(text2, "-p:(.+)", RegexOptions.IgnoreCase)).Success)
				{
					flag3 = true;
					dbName = match.Groups[1].Value;
				}
				else
				{
					ReplayService.ReportError("Invalid option specified :{0}\n", new object[]
					{
						text2
					});
					flag4 = true;
				}
			}
			if (flag4)
			{
				return 1;
			}
			Globals.InitializeSinglePerfCounterInstance();
			ClusterLatencyChecker.EnableClusterLatencyChecking();
			LatencyChecker.EnableClusterKill = true;
			if (!flag3)
			{
				if (flag)
				{
					ExTraceGlobals.PFDTracer.TracePfd<int, DateTime>(0L, "PFD CRS {0} Exchange Replication Service Starting in console mode {1}", 28445, DateTime.UtcNow);
					ComponentManager instance = ReplayComponentManager.Instance;
					ReplayService.StartServiceComponents(instance, flag);
					if (flag2)
					{
						Console.WriteLine("Hit <enter> to shutdown...");
					}
					Console.ReadLine();
					instance.Stop();
				}
				else
				{
					ReplayServiceBase.Run(new ReplayService());
				}
				return 0;
			}
			if (!ReplayService.RepairDatabaseActiveServer(dbName, isVerbose, isConfirm, false))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000308C File Offset: 0x0000128C
		protected override void OnStartInternal(string[] args)
		{
			ExTraceGlobals.PFDTracer.TracePfd<int, DateTime>(0L, "PFD CRS {0} Exchange Replication Service Starting {1}", 28957, DateTime.UtcNow);
			ExTraceGlobals.FaultInjectionTracer.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(ExceptionInjectionCallback.ExceptionLookup));
			base.ExRequestAdditionalTime((int)TimeSpan.FromMinutes(2.0).TotalMilliseconds);
			ReplayService.StartServiceComponents(ReplayComponentManager.Instance, false);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000030F4 File Offset: 0x000012F4
		private static void StartServiceComponents(ComponentManager componentManager, bool useConsole)
		{
			try
			{
				componentManager.Start();
			}
			catch (ReplayCriticalComponentFailedToStartException ex)
			{
				ExTraceGlobals.ReplayManagerTracer.TraceError<string>((long)ex.ComponentName.GetHashCode(), "{0} failed to start! Exiting process.", ex.ComponentName);
				ReplayEventLogConstants.Tuple_ServiceFailedToStartComponentFailure.LogEvent(null, new object[]
				{
					ex.ComponentName
				});
				if (useConsole)
				{
					Console.WriteLine("The {0} failed to start. Please review the Application EventLog for more information. The service will now exit.", ex.ComponentName);
				}
				Environment.Exit(1);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003174 File Offset: 0x00001374
		protected override void OnStopInternal()
		{
			ExTraceGlobals.PFDTracer.TracePfd<int, DateTime>(0L, "PFD CRS {0} Exchange Replication Service Stopping {1}", 20765, DateTime.UtcNow);
			StopHintSender stopHintSender = new StopHintSender(this);
			stopHintSender.Start();
			ReplayComponentManager.Instance.Stop();
			stopHintSender.Stop();
			ExTraceGlobals.PFDTracer.TracePfd<int, DateTime>(0L, "PFD CRS {0} Exchange Replication Service Stopped {1}", 24861, DateTime.UtcNow);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000031D6 File Offset: 0x000013D6
		protected override void HavePossibleHungComponentInvoke(Action toInvoke)
		{
			ReplayComponentManager.Instance.HavePossibleHungComponentInvoke(toInvoke);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000031E4 File Offset: 0x000013E4
		public static void TestOnPreShutdown()
		{
			ReplayService replayService = new ReplayService();
			ReplayComponentManager instance = ReplayComponentManager.Instance;
			instance.ThirdPartyManager.Start();
			instance.ActiveManager.Start();
			Thread.Sleep(5000);
			replayService.OnPreShutdown();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003228 File Offset: 0x00001428
		protected override void OnPreShutdown()
		{
			ExTraceGlobals.ReplayManagerTracer.TraceDebug<ExDateTime>(0L, "{0}: Enter Exchange Replication Service System PreShutdown", ExDateTime.Now);
			StopHintSender stopHintSender = new StopHintSender(this);
			stopHintSender.Start();
			AmSystemManager.Instance.SystemShutdownStartTime = new ExDateTime?(ExDateTime.Now);
			try
			{
				ActiveManagerCore.AttemptServerSwitchoverOnShutdown();
			}
			finally
			{
				AmStoreHelper.DismountAll("OnPreShutdown");
				stopHintSender.Stop();
				ExTraceGlobals.ReplayManagerTracer.TraceDebug<ExDateTime>(0L, "{0} : Leave Exchange Replication Service System PreShutdown", ExDateTime.Now);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000032AC File Offset: 0x000014AC
		private static void ReportError(string formatString, params object[] args)
		{
			ConsoleColor foregroundColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(formatString, args);
			Console.ForegroundColor = foregroundColor;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003320 File Offset: 0x00001520
		internal static bool RepairDatabaseActiveServer(string dbName, bool isVerbose, bool isConfirm, bool isThrowOnError)
		{
			RepairDatabaseAmState dbRepair = null;
			Exception ex = RepairDatabaseAmState.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				dbRepair = new RepairDatabaseAmState(dbName, isVerbose);
			});
			if (ex != null)
			{
				ReplayService.ReportError("Repair operation failed in intialization. (Error: {0})", new object[]
				{
					ex.Message
				});
				if (isThrowOnError)
				{
					throw new RepairStateException(ex.Message, ex);
				}
				return false;
			}
			else
			{
				ex = RepairDatabaseAmState.HandleKnownExceptions(delegate(object param0, EventArgs param1)
				{
					dbRepair.RunPrereqChecks();
				});
				if (ex != null)
				{
					ReplayService.ReportError("Repair operation failed in the Prereq checks. (Error: {0})", new object[]
					{
						ex.Message
					});
					if (isThrowOnError)
					{
						throw new RepairStateException(ex.Message, ex);
					}
					return false;
				}
				else
				{
					if (isConfirm)
					{
						Console.Write("Prereq checks passed. Do you want to continue and set the active server to {0}? ", AmServerName.LocalComputerName.Fqdn);
						ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(false);
						Console.WriteLine();
						if (char.ToUpper(consoleKeyInfo.KeyChar) != 'Y')
						{
							ReplayService.ReportError("Cancelled repair operation due to user input", new object[0]);
							return false;
						}
					}
					ex = RepairDatabaseAmState.HandleKnownExceptions(delegate(object param0, EventArgs param1)
					{
						dbRepair.CreateTempLogFileIfRequired();
						dbRepair.MakeLocalServerTheActiveServer(AmServerName.LocalComputerName);
					});
					if (ex == null)
					{
						Console.WriteLine("Successfuly finished repairing database state");
						if (!dbRepair.IsReplayRunning)
						{
							Console.Write("Start Microsoft Exchange Replication service and ");
						}
						Console.WriteLine("Run Mount-Database task to mount the database that you have repaired");
						return true;
					}
					ReplayService.ReportError("Repair operation failed to repair database state. (Error:{0})", new object[]
					{
						ex.Message
					});
					if (isThrowOnError)
					{
						throw new RepairStateException(ex.Message, ex);
					}
					return false;
				}
			}
		}
	}
}
