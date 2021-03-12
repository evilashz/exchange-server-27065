using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Wac
{
	// Token: 0x02000518 RID: 1304
	public class OAuthWacProbe : ProbeWorkItem
	{
		// Token: 0x06002019 RID: 8217 RVA: 0x000C47EB File Offset: 0x000C29EB
		public OAuthWacProbe()
		{
			this.Tracer = ExTraceGlobals.OWATracer;
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x0600201A RID: 8218 RVA: 0x000C47FE File Offset: 0x000C29FE
		// (set) Token: 0x0600201B RID: 8219 RVA: 0x000C4806 File Offset: 0x000C2A06
		private Trace Tracer { get; set; }

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x000C480F File Offset: 0x000C2A0F
		// (set) Token: 0x0600201D RID: 8221 RVA: 0x000C4817 File Offset: 0x000C2A17
		private OAuthWacProbe.ProbeState State { get; set; }

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x0600201E RID: 8222 RVA: 0x000C4820 File Offset: 0x000C2A20
		// (set) Token: 0x0600201F RID: 8223 RVA: 0x000C4828 File Offset: 0x000C2A28
		private DateTime TimeStarted { get; set; }

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06002020 RID: 8224 RVA: 0x000C4831 File Offset: 0x000C2A31
		// (set) Token: 0x06002021 RID: 8225 RVA: 0x000C4839 File Offset: 0x000C2A39
		private DateTime TimeCompleted { get; set; }

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x000C4842 File Offset: 0x000C2A42
		// (set) Token: 0x06002023 RID: 8227 RVA: 0x000C484A File Offset: 0x000C2A4A
		private string DiagnosticMessage { get; set; }

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x000C4853 File Offset: 0x000C2A53
		// (set) Token: 0x06002025 RID: 8229 RVA: 0x000C485B File Offset: 0x000C2A5B
		private ADUser MonitoringUser { get; set; }

		// Token: 0x06002026 RID: 8230 RVA: 0x000C4864 File Offset: 0x000C2A64
		internal static ProbeDefinition CreateDefinition(string monitoringUser, string probeName, string targetEndpoint, string secondaryEndpoint)
		{
			return new ProbeDefinition
			{
				TypeName = OAuthWacProbe.ProbeTypeName,
				Name = probeName,
				ServiceName = ExchangeComponent.OwaDependency.Name,
				Endpoint = targetEndpoint,
				SecondaryEndpoint = secondaryEndpoint,
				Account = monitoringUser,
				AccountPassword = string.Empty,
				AccountDisplayName = monitoringUser
			};
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x000C48C1 File Offset: 0x000C2AC1
		private static bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
		{
			return true;
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x000C48C4 File Offset: 0x000C2AC4
		protected override void DoWork(CancellationToken cancellationToken)
		{
			this.TimeStarted = DateTime.UtcNow;
			this.State = OAuthWacProbe.ProbeState.PreparingRequest;
			WTFDiagnostics.TraceInformation(this.Tracer, base.TraceContext, "configuring probe", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacProbe.cs", 136);
			CertificateValidationManager.RegisterCallback("OAuthWacProbe:", new RemoteCertificateValidationCallback(OAuthWacProbe.ValidateRemoteCertificate));
			string text = string.Format("accessing wac endpoint {0} and wopi endpoint {1} with user {2} from probe {3}", new object[]
			{
				string.IsNullOrEmpty(base.Definition.Endpoint) ? "(none)" : base.Definition.Endpoint,
				base.Definition.SecondaryEndpoint,
				base.Definition.Account,
				base.Definition.Name
			});
			base.Result.StateAttribute1 = text;
			WTFDiagnostics.TraceInformation(this.Tracer, base.TraceContext, text, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacProbe.cs", 151);
			try
			{
				text = string.Format("getting user {0} from AD", base.Definition.Account);
				WTFDiagnostics.TraceInformation(this.Tracer, base.TraceContext, text, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacProbe.cs", 156);
				this.MonitoringUser = this.GetADUser();
				if (this.MonitoringUser == null)
				{
					throw new ApplicationException(string.Format("OAuthWacProbe FAILED: unable to retrieve monitoring user{0} from AD", base.Definition.Account));
				}
				text = "starting OAuth request";
				WTFDiagnostics.TraceInformation(this.Tracer, base.TraceContext, text, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacProbe.cs", 168);
				this.State = OAuthWacProbe.ProbeState.RunningRequest;
				this.State = this.RunOAuthWacProbe();
				text = "request completed, result is " + this.State.ToString();
				WTFDiagnostics.TraceInformation(this.Tracer, base.TraceContext, text, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacProbe.cs", 175);
				this.TimeCompleted = DateTime.UtcNow;
			}
			catch (Exception ex)
			{
				this.State = OAuthWacProbe.ProbeState.FailedRequest;
				this.TimeCompleted = DateTime.UtcNow;
				base.Result.Error = OAuthWacProbe.Flatten(ex);
				text = string.Format("OAuthWacProbe FAILED: uncaught exception thrown {0}", ex.Message);
				WTFDiagnostics.TraceInformation(this.Tracer, base.TraceContext, text, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacProbe.cs", 187);
				throw;
			}
			finally
			{
				base.Result.ExecutionContext = this.DiagnosticMessage;
				int num = (int)(this.TimeCompleted - this.TimeStarted).TotalMilliseconds;
				WTFDiagnostics.TraceInformation<int>(this.Tracer, base.TraceContext, "probe completed in {0} ms", num, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacProbe.cs", 196);
				base.Result.SampleValue = (double)num;
			}
			OAuthWacProbe.ProbeState state = this.State;
			if (state == OAuthWacProbe.ProbeState.Passed)
			{
				WTFDiagnostics.TraceInformation(this.Tracer, base.TraceContext, "OAuthWacProbe PASSED", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacProbe.cs", 211);
				return;
			}
			string message = string.Format("OAuthWacProbe FAILED: {0} when {1} and the following execution context: {2}. Also here is the fatal exception information occured during the probe execution: {3}", new object[]
			{
				this.State,
				base.Result.StateAttribute1,
				base.Result.ExecutionContext,
				string.IsNullOrEmpty(base.Result.Error) ? "(none)" : base.Result.Error
			});
			WTFDiagnostics.TraceInformation(this.Tracer, base.TraceContext, message, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacProbe.cs", 225);
			throw new ApplicationException(message);
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x000C4C5C File Offset: 0x000C2E5C
		private ADUser GetADUser()
		{
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.OWATracer, base.TraceContext, "OAuthDiscovery.GetADUser: Getting AD information for user {0}", base.Definition.Account, null, "GetADUser", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacProbe.cs", 240);
			SmtpAddress smtpAddress = SmtpAddress.Empty;
			try
			{
				smtpAddress = SmtpAddress.Parse(base.Definition.Account);
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.OWATracer, base.TraceContext, "OAuthDiscovery.GetADUser: Failed to parse SMTP address for user {0} Exception: {1}", base.Definition.Account, ex.ToString(), null, "GetADUser", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacProbe.cs", 252);
				return null;
			}
			ADSessionSettings sessionSettings = ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(smtpAddress.Domain);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 264, "GetADUser", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacProbe.cs");
			QueryFilter filter = new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.UserPrincipalName, base.Definition.Account),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.PrimarySmtpAddress, base.Definition.Account)
			});
			ADRecipient[] array = tenantOrRootOrgRecipientSession.Find(null, QueryScope.SubTree, filter, null, 1);
			if (array != null && array.Length > 0)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.OWATracer, base.TraceContext, "OAuthDiscovery.GetADUser: Successfully retrieved AD information for user {0}", base.Definition.Account, null, "GetADUser", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacProbe.cs", 289);
				return array[0] as ADUser;
			}
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.OWATracer, base.TraceContext, "OAuthDiscovery.GetADUser: Unable to get AD information for user {0}", base.Definition.Account, null, "GetADUser", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Wac\\OAuthWacProbe.cs", 298);
			return null;
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x000C4DF8 File Offset: 0x000C2FF8
		private static string Flatten(Exception e)
		{
			return e.ToString().Replace("\r\n", "+");
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x000C4E10 File Offset: 0x000C3010
		private OAuthWacProbe.ProbeState RunOAuthWacProbe()
		{
			string empty = string.Empty;
			Microsoft.Exchange.Security.OAuth.ResultType resultType = TestOAuthWacConnectivityHelper.SendWacOAuthRequest(base.Definition.SecondaryEndpoint, base.Definition.Endpoint, this.MonitoringUser, out empty);
			this.DiagnosticMessage = empty;
			if (resultType != Microsoft.Exchange.Security.OAuth.ResultType.Error)
			{
				return OAuthWacProbe.ProbeState.Passed;
			}
			return OAuthWacProbe.ProbeState.FailedRequest;
		}

		// Token: 0x0400178E RID: 6030
		private static readonly string ProbeTypeName = typeof(OAuthWacProbe).FullName;

		// Token: 0x02000519 RID: 1305
		private enum ProbeState
		{
			// Token: 0x04001796 RID: 6038
			PreparingRequest,
			// Token: 0x04001797 RID: 6039
			RunningRequest,
			// Token: 0x04001798 RID: 6040
			WaitingResponse,
			// Token: 0x04001799 RID: 6041
			Passed,
			// Token: 0x0400179A RID: 6042
			FailedRequest,
			// Token: 0x0400179B RID: 6043
			FailedResponse,
			// Token: 0x0400179C RID: 6044
			TimedOut
		}
	}
}
