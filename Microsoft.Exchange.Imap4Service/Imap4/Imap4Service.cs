using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Imap4;
using Microsoft.Exchange.PopImap.CoreService;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000003 RID: 3
	[ComVisible(false)]
	internal sealed class Imap4Service : CoreService
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002550 File Offset: 0x00000750
		public Imap4Service(string serviceName, string jobObjectName, bool runningAsService, Trace tracer, ExEventLog eventLogger) : base(serviceName, CoreService.GetWorkerProcessPathName("Microsoft.Exchange.Imap4.exe"), jobObjectName, runningAsService, tracer, eventLogger)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000256C File Offset: 0x0000076C
		[MTAThread]
		public static void Main(string[] args)
		{
			int num = Privileges.RemoveAllExcept(new string[]
			{
				"SeAuditPrivilege",
				"SeChangeNotifyPrivilege",
				"SeCreateGlobalPrivilege",
				"SeIncreaseQuotaPrivilege",
				"SeAssignPrimaryTokenPrivilege"
			});
			if (num != 0)
			{
				Environment.Exit(num);
			}
			ExTraceGlobals.ServerTracer.TraceDebug(0L, "Imap4Service::Main - Successfully Removed priviledges.");
			CoreService.DetermineServiceRole();
			bool runningAsService;
			CoreService.ParseArgs(args, out runningAsService);
			string text;
			string text2;
			string text3;
			if (CoreService.ServerRoleService == ServerServiceRole.mailbox)
			{
				text = "Microsoft Exchange IMAP4 backend";
				text2 = "MSExchangeIMAP4BE";
				text3 = "MSExchange IMAP4 backend service";
				CoreService.ShortServiceName = "IMAP4BE";
			}
			else
			{
				text = "Microsoft Exchange IMAP4";
				text2 = "MSExchangeIMAP4";
				text3 = "MSExchange IMAP4 service";
				CoreService.ShortServiceName = "IMAP4";
			}
			ExTraceGlobals.ServerTracer.TraceDebug(0L, "Imap4Service::Main - Service Properties. JobName:{0}, serviceName:{1},serviceEventName:{2},ShortName:{3}", new object[]
			{
				text,
				text3,
				text3,
				CoreService.ShortServiceName
			});
			CoreService.TroubleshootingContext = new TroubleshootingContext(text2);
			ExWatson.Init();
			AppDomain.CurrentDomain.UnhandledException += CoreService.SendWatsonForUnhandledException;
			ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.ServerTracer.Category, text3);
			Imap4Service serviceToRun = new Imap4Service(text2, text, runningAsService, ExTraceGlobals.ServerTracer, eventLogger);
			CoreService.MainProc(serviceToRun, runningAsService, args);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000026AC File Offset: 0x000008AC
		public override PopImapAdConfiguration GetConfiguration()
		{
			PopImapAdConfiguration result;
			try
			{
				ITopologyConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 192, "GetConfiguration", "f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Imap4Service\\Imap4Service.cs");
				result = PopImapAdConfiguration.FindOne<Imap4AdConfiguration>(session);
			}
			catch (ADTransientException)
			{
				result = null;
			}
			catch (ADOperationException)
			{
				result = null;
			}
			catch (DataValidationException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0400000A RID: 10
		private const string WorkerProcessName = "Microsoft.Exchange.Imap4.exe";

		// Token: 0x0400000B RID: 11
		private const string CafeJobObjectName = "Microsoft Exchange IMAP4";

		// Token: 0x0400000C RID: 12
		private const string CafeImap4ServiceName = "MSExchangeIMAP4";

		// Token: 0x0400000D RID: 13
		private const string CafeImap4ServiceEventName = "MSExchange IMAP4 service";

		// Token: 0x0400000E RID: 14
		private const string BackendJobObjectName = "Microsoft Exchange IMAP4 backend";

		// Token: 0x0400000F RID: 15
		private const string BackendImap4ServiceName = "MSExchangeIMAP4BE";

		// Token: 0x04000010 RID: 16
		private const string BackendImap4ServiceEventName = "MSExchange IMAP4 backend service";

		// Token: 0x04000011 RID: 17
		private const int PortRangeStartImap = 5010;
	}
}
