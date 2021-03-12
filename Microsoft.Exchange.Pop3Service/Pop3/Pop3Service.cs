using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Pop3;
using Microsoft.Exchange.PopImap.CoreService;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000003 RID: 3
	[ComVisible(false)]
	internal sealed class Pop3Service : CoreService
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002550 File Offset: 0x00000750
		public Pop3Service(string serviceName, string jobObjectName, bool runningAsService, Trace tracer, ExEventLog eventLogger) : base(serviceName, CoreService.GetWorkerProcessPathName("Microsoft.Exchange.Pop3.exe"), jobObjectName, runningAsService, tracer, eventLogger)
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
			ExTraceGlobals.ServerTracer.TraceDebug(0L, "Pop3Service::Main - Successfully Removed priviledges.");
			CoreService.DetermineServiceRole();
			bool runningAsService;
			CoreService.ParseArgs(args, out runningAsService);
			string text;
			string text2;
			string text3;
			if (CoreService.ServerRoleService == ServerServiceRole.mailbox)
			{
				text = "Microsoft Exchange POP3 backend";
				text2 = "MSExchangePOP3BE";
				text3 = "MSExchange POP3 backend service";
				CoreService.ShortServiceName = "POP3BE";
			}
			else
			{
				text = "Microsoft Exchange POP3";
				text2 = "MSExchangePOP3";
				text3 = "MSExchange POP3 service";
				CoreService.ShortServiceName = "POP3";
			}
			ExTraceGlobals.ServerTracer.TraceDebug(0L, "Pop3Service::Main - Service Properties. JobName:{0}, serviceName:{1},serviceEventName:{2},ShortName:{3}", new object[]
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
			Pop3Service serviceToRun = new Pop3Service(text2, text, runningAsService, ExTraceGlobals.ServerTracer, eventLogger);
			CoreService.MainProc(serviceToRun, runningAsService, args);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000026AC File Offset: 0x000008AC
		public override PopImapAdConfiguration GetConfiguration()
		{
			PopImapAdConfiguration result;
			try
			{
				ITopologyConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 187, "GetConfiguration", "f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Pop3Service\\Pop3Service.cs");
				result = PopImapAdConfiguration.FindOne<Pop3AdConfiguration>(session);
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
		private const string WorkerProcessName = "Microsoft.Exchange.Pop3.exe";

		// Token: 0x0400000B RID: 11
		private const string CafeJobObjectName = "Microsoft Exchange POP3";

		// Token: 0x0400000C RID: 12
		private const string CafePop3ServiceName = "MSExchangePOP3";

		// Token: 0x0400000D RID: 13
		private const string CafePop3ServiceEventName = "MSExchange POP3 service";

		// Token: 0x0400000E RID: 14
		private const string BackendJobObjectName = "Microsoft Exchange POP3 backend";

		// Token: 0x0400000F RID: 15
		private const string BackendPop3ServiceName = "MSExchangePOP3BE";

		// Token: 0x04000010 RID: 16
		private const string BackendPop3ServiceEventName = "MSExchange POP3 backend service";

		// Token: 0x04000011 RID: 17
		private const int PortRangeStartPop = 5020;
	}
}
