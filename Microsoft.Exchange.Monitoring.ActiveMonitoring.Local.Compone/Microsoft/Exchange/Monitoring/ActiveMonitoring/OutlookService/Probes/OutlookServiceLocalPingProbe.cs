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

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OutlookService.Probes
{
	// Token: 0x02000260 RID: 608
	public class OutlookServiceLocalPingProbe : OutlookServicePingProbe
	{
		// Token: 0x0600111C RID: 4380 RVA: 0x0007203A File Offset: 0x0007023A
		static OutlookServiceLocalPingProbe()
		{
			OutlookServiceLocalPingProbe.TrustAllCerts();
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0007205C File Offset: 0x0007025C
		private static void TrustAllCerts()
		{
			ServicePointManager.ServerCertificateValidationCallback = ((object param0, X509Certificate param1, X509Chain param2, SslPolicyErrors param3) => true);
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00072080 File Offset: 0x00070280
		public override void PopulateDefinition<TDefinition>(TDefinition definition, Dictionary<string, string> propertyBag)
		{
			ProbeDefinition probeDefinition = definition as ProbeDefinition;
			if (probeDefinition == null)
			{
				throw new ArgumentException("definition must be a ProbeDefinition");
			}
			probeDefinition.Endpoint = "https://localhost:444/outlookservice";
			probeDefinition.TimeoutSeconds = OutlookServiceLocalPingProbe.Timeout;
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

		// Token: 0x0600111F RID: 4383 RVA: 0x00072104 File Offset: 0x00070304
		public static ProbeDefinition CreateDefinition(string assemblyPath, string probeName, string endpoint)
		{
			return new ProbeDefinition
			{
				AssemblyPath = assemblyPath,
				TypeName = typeof(OutlookServiceLocalPingProbe).FullName,
				Name = probeName,
				ServiceName = ExchangeComponent.HxServiceMail.Name,
				RecurrenceIntervalSeconds = OutlookServiceLocalPingProbe.PingProbeRecurrenceIntervalSeconds,
				TimeoutSeconds = OutlookServiceLocalPingProbe.Timeout,
				MaxRetryAttempts = 3,
				Endpoint = endpoint,
				Account = string.Empty,
				AccountPassword = string.Empty,
				Enabled = true
			};
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0007218C File Offset: 0x0007038C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			MailboxDatabaseInfo localMonitoringAccount = this.GetLocalMonitoringAccount();
			if (localMonitoringAccount != null)
			{
				base.Definition.Account = localMonitoringAccount.MonitoringAccountUserPrincipalName;
				base.Definition.AccountPassword = localMonitoringAccount.MonitoringAccountPassword;
				WTFDiagnostics.TraceDebug<string, string, string>(ExTraceGlobals.HTTPTracer, base.TraceContext, "OutlookServiceLocalPingProbe.DoWork : Endpoint={0}, Account={1}, Password={2}", base.Definition.Endpoint, base.Definition.Account, base.Definition.AccountPassword, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceLocalPingProbe.cs", 152);
			}
			else
			{
				base.Definition.Account = string.Empty;
				base.Definition.AccountPassword = string.Empty;
				WTFDiagnostics.TraceDebug(ExTraceGlobals.HTTPTracer, base.TraceContext, "OutlookServiceLocalPingProbe.DoWork : Monitoring user does not exist. Resetting Account to String.Empty", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceLocalPingProbe.cs", 165);
			}
			base.DoWork(cancellationToken);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0007225C File Offset: 0x0007045C
		protected override void ExecuteRequest(SocketClient client)
		{
			if (string.IsNullOrEmpty(base.Definition.Account) || string.IsNullOrEmpty(base.Definition.AccountPassword))
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, string.Format("Execution will succeed because monitoring account not yet loaded", new object[0]), null, "ExecuteRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceLocalPingProbe.cs", 182);
				base.Result.ResultType = ResultType.Succeeded;
				return;
			}
			base.ExecuteRequest(client);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x000722D4 File Offset: 0x000704D4
		private MailboxDatabaseInfo GetLocalMonitoringAccount()
		{
			TracingContext context = new TracingContext();
			string typeName = base.Definition.TypeName;
			WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, context, string.Format("{0}::GetMonitoringAccount:: Entering GetMonitoringAccount", typeName), null, "GetLocalMonitoringAccount", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceLocalPingProbe.cs", 197);
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, context, string.Format("{0}::GetMonitoringAccount:: Checking if MailboxDatabaseEndpointExists", typeName), null, "GetLocalMonitoringAccount", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceLocalPingProbe.cs", 200);
			if (instance.MailboxDatabaseEndpoint == null || instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.Count == 0)
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, context, string.Format("{0}::GetMonitoringAccount:: No Mailbox found on this server", typeName), null, "GetLocalMonitoringAccount", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceLocalPingProbe.cs", 205);
				return null;
			}
			WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, context, string.Format("{0}::GetMonitoringAccount:: Looking for BackendCredentials", typeName), null, "GetLocalMonitoringAccount", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceLocalPingProbe.cs", 210);
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend)
			{
				if (!string.IsNullOrWhiteSpace(mailboxDatabaseInfo.MonitoringAccountPassword))
				{
					WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, context, string.Format("{0}::GetMonitoringAccount:: Backend Credentials Found Account = {1} Password = {2}", typeName, mailboxDatabaseInfo.MonitoringAccount, mailboxDatabaseInfo.MonitoringAccountPassword), null, "GetLocalMonitoringAccount", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceLocalPingProbe.cs", 219);
					return mailboxDatabaseInfo;
				}
			}
			WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, context, string.Format("OutlookServiceLocalPingProbe::GetMonitoringAccount:: No credentials found returning null", new object[0]), null, "GetLocalMonitoringAccount", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceLocalPingProbe.cs", 224);
			return null;
		}

		// Token: 0x04000CDD RID: 3293
		protected const string DefaultEndpoint = "https://localhost:444/outlookservice";

		// Token: 0x04000CDE RID: 3294
		private const string PropertyBagEndpoint = "Endpoint";

		// Token: 0x04000CDF RID: 3295
		private const string PropertyBagTimeOutSeconds = "TimeOutSeconds";

		// Token: 0x04000CE0 RID: 3296
		private const int MaxRetryAttempts = 3;

		// Token: 0x04000CE1 RID: 3297
		public static readonly int PingProbeRecurrenceIntervalSeconds = 60;

		// Token: 0x04000CE2 RID: 3298
		public new static readonly int Timeout = 50;

		// Token: 0x04000CE3 RID: 3299
		public static readonly string ProbeName = "OutlookServiceLocalPingProbe";
	}
}
