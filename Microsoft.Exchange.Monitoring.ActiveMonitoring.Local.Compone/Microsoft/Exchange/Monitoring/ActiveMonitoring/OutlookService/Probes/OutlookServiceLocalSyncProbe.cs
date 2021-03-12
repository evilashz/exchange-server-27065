using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Office.Outlook.V1.Mail;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OutlookService.Probes
{
	// Token: 0x02000261 RID: 609
	public class OutlookServiceLocalSyncProbe : OutlookServiceSocketProbeBase
	{
		// Token: 0x06001125 RID: 4389 RVA: 0x0007246C File Offset: 0x0007066C
		public OutlookServiceLocalSyncProbe()
		{
			base.Type = 0;
			base.Timeout = TimeSpan.FromSeconds((double)OutlookServiceLocalSyncProbe.TimeoutSeconds);
			OutlookServiceLocalSyncProbe.TrustAllCerts();
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00072494 File Offset: 0x00070694
		private static void TrustAllCerts()
		{
			ServicePointManager.ServerCertificateValidationCallback = ((object param0, X509Certificate param1, X509Chain param2, SslPolicyErrors param3) => true);
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x000724B8 File Offset: 0x000706B8
		protected override void ExecuteRequest(SocketClient client)
		{
			if (string.IsNullOrEmpty(base.Definition.Account) || string.IsNullOrEmpty(base.Definition.AccountPassword))
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, string.Format("Execution will Succeed because monitoring account credentials not found", new object[0]), null, "ExecuteRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceLocalSyncProbe.cs", 98);
				base.Result.ResultType = ResultType.Succeeded;
				return;
			}
			client.Sync(new BeginSyncRequest
			{
				ViewMode = new ViewMode?(2),
				RichOptions = 1,
				ConversationHeadersCount = 3
			}, base.Timeout);
			base.Result.StateAttribute13 = client.ExtraInfo;
			if (client.ExecutionSuccess)
			{
				base.Result.ResultType = ResultType.Succeeded;
				return;
			}
			base.Result.ResultType = ResultType.Failed;
			throw new Exception(string.Format("SyncProbe execution failed, Details : {0}", client.ExtraInfo));
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00072598 File Offset: 0x00070798
		protected override void DoWork(CancellationToken cancellationToken)
		{
			MailboxDatabaseInfo localMonitoringAccount = this.GetLocalMonitoringAccount();
			if (localMonitoringAccount != null)
			{
				base.Definition.Account = localMonitoringAccount.MonitoringAccountUserPrincipalName;
				base.Definition.AccountPassword = localMonitoringAccount.MonitoringAccountPassword;
				WTFDiagnostics.TraceDebug<string, string, string>(ExTraceGlobals.HTTPTracer, base.TraceContext, "OutlookServiceLocalSyncProbe.DoWork : Endpoint={0}, Account={1}, Password={2}", base.Definition.Endpoint, base.Definition.Account, base.Definition.AccountPassword, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceLocalSyncProbe.cs", 133);
			}
			else
			{
				base.Definition.Account = string.Empty;
				base.Definition.AccountPassword = string.Empty;
				WTFDiagnostics.TraceDebug(ExTraceGlobals.HTTPTracer, base.TraceContext, "OutlookServiceLocalSyncProbe.DoWork : Monitoring user does not exist. Resetting Account to String.Empty", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceLocalSyncProbe.cs", 146);
			}
			base.DoWork(cancellationToken);
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00072668 File Offset: 0x00070868
		public override void PopulateDefinition<TDefinition>(TDefinition definition, Dictionary<string, string> propertyBag)
		{
			ProbeDefinition probeDefinition = definition as ProbeDefinition;
			if (probeDefinition == null)
			{
				throw new ArgumentException("definition must be a ProbeDefinition");
			}
			probeDefinition.Endpoint = "https://localhost:444/outlookservice";
			probeDefinition.TimeoutSeconds = OutlookServiceLocalSyncProbe.TimeoutSeconds;
			if (propertyBag.ContainsKey("Endpoint"))
			{
				probeDefinition.Endpoint = propertyBag["Endpoint"];
			}
			int timeoutSeconds;
			if (propertyBag.ContainsKey("TimeOutSeconds") && int.TryParse(propertyBag["TimeOutSeconds"], out timeoutSeconds))
			{
				probeDefinition.TimeoutSeconds = timeoutSeconds;
			}
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x000726EC File Offset: 0x000708EC
		public static ProbeDefinition CreateDefinition(string assemblyPath, string probeName, string endpoint)
		{
			return new ProbeDefinition
			{
				AssemblyPath = assemblyPath,
				TypeName = typeof(OutlookServiceLocalSyncProbe).FullName,
				Name = probeName,
				ServiceName = ExchangeComponent.HxServiceMail.Name,
				RecurrenceIntervalSeconds = OutlookServiceLocalSyncProbe.ProbeRecurrenceIntervalSeconds,
				TimeoutSeconds = OutlookServiceLocalSyncProbe.TimeoutSeconds,
				MaxRetryAttempts = 3,
				Endpoint = endpoint,
				Account = string.Empty,
				AccountPassword = string.Empty,
				Enabled = true
			};
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00072774 File Offset: 0x00070974
		private MailboxDatabaseInfo GetLocalMonitoringAccount()
		{
			TracingContext context = new TracingContext();
			string typeName = base.Definition.TypeName;
			WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, context, string.Format("{0}::GetMonitoringAccount:: Entering GetMonitoringAccount", typeName), null, "GetLocalMonitoringAccount", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceLocalSyncProbe.cs", 222);
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, context, string.Format("{0}::GetMonitoringAccount:: Checking if MailboxDatabaseEndpointExists", typeName), null, "GetLocalMonitoringAccount", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceLocalSyncProbe.cs", 225);
			if (instance.MailboxDatabaseEndpoint == null || instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.Count == 0)
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, context, string.Format("{0}::GetMonitoringAccount:: No Mailbox found on this server", typeName), null, "GetLocalMonitoringAccount", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceLocalSyncProbe.cs", 230);
				return null;
			}
			WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, context, string.Format("{0}::GetMonitoringAccount:: Looking for BackendCredentials", typeName), null, "GetLocalMonitoringAccount", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceLocalSyncProbe.cs", 235);
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend)
			{
				if (!string.IsNullOrWhiteSpace(mailboxDatabaseInfo.MonitoringAccountPassword))
				{
					WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, context, string.Format("{0}::GetMonitoringAccount:: Backend Credentials Found Account = {1} Password = {2}", typeName, mailboxDatabaseInfo.MonitoringAccountUserPrincipalName, mailboxDatabaseInfo.MonitoringAccountPassword), null, "GetLocalMonitoringAccount", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceLocalSyncProbe.cs", 244);
					return mailboxDatabaseInfo;
				}
			}
			WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, context, string.Format("OutlookServiceLocalSyncProbe::GetMonitoringAccount:: No credentials found returning null", new object[0]), null, "GetLocalMonitoringAccount", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceLocalSyncProbe.cs", 249);
			return null;
		}

		// Token: 0x04000CE5 RID: 3301
		protected const string DefaultEndpoint = "https://localhost:444/outlookservice";

		// Token: 0x04000CE6 RID: 3302
		protected const int ConversationHeaderCount = 3;

		// Token: 0x04000CE7 RID: 3303
		private const string PropertyBagEndpoint = "Endpoint";

		// Token: 0x04000CE8 RID: 3304
		private const string PropertyBagTimeOutSeconds = "TimeOutSeconds";

		// Token: 0x04000CE9 RID: 3305
		private const int MaxRetryAttempts = 3;

		// Token: 0x04000CEA RID: 3306
		public static readonly int ProbeRecurrenceIntervalSeconds = 60;

		// Token: 0x04000CEB RID: 3307
		public static readonly int TimeoutSeconds = 50;

		// Token: 0x04000CEC RID: 3308
		public static readonly string ProbeName = "OutlookServiceLocalSyncProbe";
	}
}
