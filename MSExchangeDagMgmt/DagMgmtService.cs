using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.DagManagement;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Cluster.DagService
{
	// Token: 0x02000003 RID: 3
	internal sealed class DagMgmtService : ExServiceBase
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002275 File Offset: 0x00000475
		private DagMgmtService(bool useConsole = false)
		{
			this.runFromConsole = useConsole;
			base.CanStop = true;
			base.CanShutdown = true;
			base.AutoLog = false;
			base.ServiceName = DagMgmtService.serviceName;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022A4 File Offset: 0x000004A4
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ServiceTracer;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022AC File Offset: 0x000004AC
		public static void Main(string[] args)
		{
			ExWatson.Register();
			int num = DagMgmtService.MainInternal(args);
			if (num != 0)
			{
				DagMgmtService.Tracer.TraceError<int>(0L, "Exiting process with error code {0}", num);
				Environment.Exit(num);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022E0 File Offset: 0x000004E0
		protected override void OnStartInternal(string[] args)
		{
			DagMgmtService.Tracer.TraceDebug<string>((long)this.GetHashCode(), "{0} service is starting up: OnStartInternal() called", base.ServiceName);
			base.ExRequestAdditionalTime(120000);
			DagMgmtService.StartServiceComponents(DagComponentManager.Instance, this.runFromConsole);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000231C File Offset: 0x0000051C
		protected override void OnStopInternal()
		{
			DagMgmtService.Tracer.TraceDebug<string>((long)this.GetHashCode(), "{0} service is stopping: OnStopInternal() called", base.ServiceName);
			DagComponentManager.Instance.Stop();
			DagMgmtService.Tracer.TraceDebug<string>((long)this.GetHashCode(), "{0} service has now stopped", base.ServiceName);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000236C File Offset: 0x0000056C
		private static int MainInternal(string[] args)
		{
			string userName = Environment.UserName;
			DagMgmtService.Tracer.TraceDebug<string>(0L, "Running as {0}", userName);
			int num = Privileges.RemoveAllExcept(DagMgmtService.requiredPrivileges);
			if (num != 0)
			{
				return num;
			}
			Globals.InitializeSinglePerfCounterInstance();
			bool flag = !DagMgmtService.IsRunningAsService();
			bool flag2 = false;
			bool flag3 = false;
			foreach (string text in args)
			{
				if (DagMgmtService.IsArgumentEqual(text, "-wait"))
				{
					flag3 = true;
				}
				else
				{
					DagMgmtService.ReportError("Invalid option specified :{0}\n", new object[]
					{
						text
					});
					flag2 = true;
				}
			}
			if (flag2)
			{
				DagMgmtService.Tracer.TraceError(0L, "Invalid argument specified. Exiting process.");
				return 1;
			}
			if (flag)
			{
				DagMgmtService.Tracer.TraceDebug<string, DateTime>(0L, "{0} Service starting in console mode at {1}", DagMgmtService.serviceName, DateTime.UtcNow);
				Console.WriteLine("Starting {0}, running in console mode.", Assembly.GetExecutingAssembly().GetName().Name);
				if (flag3)
				{
					Console.WriteLine("Press <ENTER> to continue.");
					Console.ReadLine();
				}
				ExServiceBase.RunAsConsole(new DagMgmtService(flag));
			}
			else
			{
				ServiceBase.Run(new DagMgmtService(flag));
			}
			return 0;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000247C File Offset: 0x0000067C
		private static void StartServiceComponents(ComponentManager componentManager, bool useConsole)
		{
			try
			{
				componentManager.Start();
			}
			catch (ReplayCriticalComponentFailedToStartException ex)
			{
				DagMgmtService.Tracer.TraceError<string>((long)ex.ComponentName.GetHashCode(), "{0} failed to start! Exiting process.", ex.ComponentName);
				ReplayEventLogConstants.Tuple_ServiceFailedToStartComponentFailure.LogEvent(null, new object[]
				{
					ex.ComponentName
				});
				if (useConsole)
				{
					DagMgmtService.ReportError("The {0} failed to start. Please review the Application EventLog for more information. The service will now exit.", new object[]
					{
						ex.ComponentName
					});
				}
				Environment.Exit(1);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002508 File Offset: 0x00000708
		private static bool IsArgumentEqual(string arg, string validArgOption)
		{
			return string.Equals(arg, validArgOption, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002514 File Offset: 0x00000714
		private static void ReportError(string formatString, params object[] args)
		{
			ConsoleColor foregroundColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(formatString, args);
			Console.ForegroundColor = foregroundColor;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000253C File Offset: 0x0000073C
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

		// Token: 0x06000019 RID: 25 RVA: 0x000025EC File Offset: 0x000007EC
		[Conditional("DEBUG")]
		private static void DebugServiceStartupDelay()
		{
			int testServiceStartupDelay = RegistryParameters.TestServiceStartupDelay;
			if (testServiceStartupDelay > 0)
			{
				DagMgmtService.Tracer.TraceDebug<int>(0L, "DebugServiceStartupDelay: sleeping for {0} secs", testServiceStartupDelay);
				Thread.Sleep(testServiceStartupDelay * 1000);
			}
		}

		// Token: 0x04000007 RID: 7
		private const string WaitToContinueOption = "-wait";

		// Token: 0x04000008 RID: 8
		private static readonly string[] requiredPrivileges = new string[]
		{
			"SeAuditPrivilege",
			"SeChangeNotifyPrivilege",
			"SeCreateGlobalPrivilege",
			"SeDebugPrivilege"
		};

		// Token: 0x04000009 RID: 9
		private static string serviceName = "MSExchangeDagMgmt";

		// Token: 0x0400000A RID: 10
		private readonly bool runFromConsole;
	}
}
