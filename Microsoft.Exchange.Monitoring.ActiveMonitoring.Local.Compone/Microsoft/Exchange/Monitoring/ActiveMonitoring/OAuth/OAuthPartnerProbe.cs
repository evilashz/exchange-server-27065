using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OAuth
{
	// Token: 0x02000246 RID: 582
	public class OAuthPartnerProbe : ProbeWorkItem
	{
		// Token: 0x0600105B RID: 4187 RVA: 0x0006CE0D File Offset: 0x0006B00D
		public OAuthPartnerProbe()
		{
			this.Tracer = ExTraceGlobals.EWSTracer;
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x0006CE20 File Offset: 0x0006B020
		// (set) Token: 0x0600105D RID: 4189 RVA: 0x0006CE28 File Offset: 0x0006B028
		private protected bool Verbose { protected get; private set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x0006CE31 File Offset: 0x0006B031
		// (set) Token: 0x0600105F RID: 4191 RVA: 0x0006CE39 File Offset: 0x0006B039
		private protected bool TrustAnySslCertificate { protected get; private set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x0006CE42 File Offset: 0x0006B042
		// (set) Token: 0x06001061 RID: 4193 RVA: 0x0006CE4A File Offset: 0x0006B04A
		private protected int ProbeTimeLimit { protected get; private set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x0006CE53 File Offset: 0x0006B053
		// (set) Token: 0x06001063 RID: 4195 RVA: 0x0006CE5B File Offset: 0x0006B05B
		private protected int HttpRequestTimeout { protected get; private set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0006CE64 File Offset: 0x0006B064
		// (set) Token: 0x06001065 RID: 4197 RVA: 0x0006CE6C File Offset: 0x0006B06C
		protected Trace Tracer { get; set; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0006CE75 File Offset: 0x0006B075
		// (set) Token: 0x06001067 RID: 4199 RVA: 0x0006CE7D File Offset: 0x0006B07D
		protected OAuthPartnerProbe.ProbeState State { get; set; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x0006CE86 File Offset: 0x0006B086
		// (set) Token: 0x06001069 RID: 4201 RVA: 0x0006CE8E File Offset: 0x0006B08E
		private protected DateTime TimeStarted { protected get; private set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x0006CE97 File Offset: 0x0006B097
		// (set) Token: 0x0600106B RID: 4203 RVA: 0x0006CE9F File Offset: 0x0006B09F
		protected DateTime TimeCompleted { get; set; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x0006CEA8 File Offset: 0x0006B0A8
		// (set) Token: 0x0600106D RID: 4205 RVA: 0x0006CEB0 File Offset: 0x0006B0B0
		protected string DiagnosticMessage { get; set; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x0006CEB9 File Offset: 0x0006B0B9
		// (set) Token: 0x0600106F RID: 4207 RVA: 0x0006CEC1 File Offset: 0x0006B0C1
		protected ADUser MonitoringUser { get; set; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0006CECA File Offset: 0x0006B0CA
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x0006CED2 File Offset: 0x0006B0D2
		protected Exception Error { get; set; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0006CEDB File Offset: 0x0006B0DB
		protected string Breadcrumbs
		{
			get
			{
				if (this.breadcrumbs != null)
				{
					return this.breadcrumbs.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0006CEF8 File Offset: 0x0006B0F8
		protected static ProbeDefinition CreateDefinition(string monitoringUser, string probeName, string targetResourceName, Uri targetEndpoint, string typeName)
		{
			return new ProbeDefinition
			{
				AssemblyPath = OAuthDiscovery.AssemblyPath,
				TypeName = typeName,
				Name = probeName,
				ServiceName = ExchangeComponent.Ews.Name,
				TargetResource = targetResourceName,
				Endpoint = ((targetEndpoint != null) ? targetEndpoint.AbsoluteUri : string.Empty),
				RecurrenceIntervalSeconds = 900,
				TimeoutSeconds = 60,
				MaxRetryAttempts = 0,
				Account = monitoringUser,
				AccountPassword = string.Empty,
				AccountDisplayName = monitoringUser
			};
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0006CF8C File Offset: 0x0006B18C
		protected static bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
		{
			return true;
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0006CF90 File Offset: 0x0006B190
		protected override void DoWork(CancellationToken cancellationToken)
		{
			this.TimeStarted = DateTime.UtcNow;
			this.State = OAuthPartnerProbe.ProbeState.PreparingRequest;
			WTFDiagnostics.TraceInformation(this.Tracer, base.TraceContext, "configuring probe", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthPartnerProbe.cs", 190);
			this.Configure();
			if (this.TrustAnySslCertificate)
			{
				CertificateValidationManager.RegisterCallback("OauthPartnerProbe:", new RemoteCertificateValidationCallback(OAuthPartnerProbe.ValidateRemoteCertificate));
			}
			string text = string.Format("accessing endpoint {0} with user {1} from probe {2}", string.IsNullOrEmpty(base.Definition.Endpoint) ? "(none)" : base.Definition.Endpoint, base.Definition.Account, base.Definition.Name);
			if (this.Verbose)
			{
				ProbeResult result = base.Result;
				result.ExecutionContext += text;
				WTFDiagnostics.TraceInformation(this.Tracer, base.TraceContext, text, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthPartnerProbe.cs", 210);
			}
			try
			{
				text = string.Format("getting user {0} from AD", base.Definition.Account);
				this.DropBreadcrumb(text);
				WTFDiagnostics.TraceInformation(this.Tracer, base.TraceContext, text, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthPartnerProbe.cs", 217);
				this.MonitoringUser = this.GetADUser();
				if (this.MonitoringUser == null)
				{
					throw new ApplicationException(string.Format("OAuthPartnerProbe FAILED: unable to retrieve monitoring user{0} from AD", base.Definition.Account));
				}
				text = "starting OAuth request";
				this.DropBreadcrumb(text);
				WTFDiagnostics.TraceInformation(this.Tracer, base.TraceContext, text, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthPartnerProbe.cs", 230);
				this.State = OAuthPartnerProbe.ProbeState.RunningRequest;
				this.State = this.RunOAuthPartnerProbe();
				text = "request completed, result is " + this.State.ToString();
				this.DropBreadcrumb(text);
				WTFDiagnostics.TraceInformation(this.Tracer, base.TraceContext, text, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthPartnerProbe.cs", 238);
				this.TimeCompleted = DateTime.UtcNow;
			}
			catch (Exception ex)
			{
				this.State = OAuthPartnerProbe.ProbeState.FailedRequest;
				this.Error = ex;
				this.TimeCompleted = DateTime.UtcNow;
				this.DropBreadcrumb("request failed: " + OAuthPartnerProbe.Flatten(ex));
				ProbeResult result2 = base.Result;
				result2.ExecutionContext += OAuthPartnerProbe.Flatten(ex);
				text = string.Format("OAuthPartnerProbe FAILED: uncaught exception thrown {0}", ex.Message);
				WTFDiagnostics.TraceInformation(this.Tracer, base.TraceContext, text, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthPartnerProbe.cs", 252);
				throw;
			}
			finally
			{
				int num = (int)(this.TimeCompleted - this.TimeStarted).TotalMilliseconds;
				WTFDiagnostics.TraceInformation<int>(this.Tracer, base.TraceContext, "probe completed in {0} ms", num, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthPartnerProbe.cs", 260);
				this.ReportMachineReadableProbeDetails();
				this.ReportHumanReadableProbeDetails();
				base.Result.SampleValue = (double)num;
			}
			OAuthPartnerProbe.ProbeState state = this.State;
			if (state == OAuthPartnerProbe.ProbeState.Passed)
			{
				WTFDiagnostics.TraceInformation(this.Tracer, base.TraceContext, "OAuthPartnerProbe PASSED", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthPartnerProbe.cs", 278);
				return;
			}
			WTFDiagnostics.TraceInformation<OAuthPartnerProbe.ProbeState>(this.Tracer, base.TraceContext, "OAuthPartnerProbe FAILED: {0}", this.State, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthPartnerProbe.cs", 285);
			throw new ApplicationException(string.Format("OAuthPartnerProbe FAILED: {0}", this.State));
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0006D310 File Offset: 0x0006B510
		protected virtual OAuthPartnerProbe.ProbeState RunOAuthPartnerProbe()
		{
			this.DiagnosticMessage = string.Empty;
			return OAuthPartnerProbe.ProbeState.Passed;
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0006D31E File Offset: 0x0006B51E
		private static string Flatten(Exception e)
		{
			return e.ToString().Replace("\r\n", "+");
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0006D338 File Offset: 0x0006B538
		private void Configure()
		{
			this.Verbose = this.ReadAttribute("Verbose", true);
			this.TrustAnySslCertificate = this.ReadAttribute("TrustAnySslCertificate", false);
			this.ProbeTimeLimit = Math.Max(base.Definition.TimeoutSeconds * 1000, 60000);
			this.HttpRequestTimeout = (int)this.ReadAttribute("HttpRequestTimeout", TimeSpan.FromMilliseconds((double)(this.ProbeTimeLimit - 1000))).TotalMilliseconds;
			if (this.Verbose)
			{
				string text = string.Format("probe defined timeout={0}ms, actual timeout={1}ms, http request timeout={2}ms\r\n", base.Definition.TimeoutSeconds * 1000, this.ProbeTimeLimit, this.HttpRequestTimeout);
				ProbeResult result = base.Result;
				result.ExecutionContext += text;
				WTFDiagnostics.TraceInformation(this.Tracer, base.TraceContext, text, null, "Configure", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthPartnerProbe.cs", 345);
			}
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0006D430 File Offset: 0x0006B630
		private ADUser GetADUser()
		{
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.EWSTracer, base.TraceContext, "OAuthDiscovery.GetADUser: Getting AD information for user {0}", base.Definition.Account, null, "GetADUser", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthPartnerProbe.cs", 359);
			SmtpAddress smtpAddress = SmtpAddress.Empty;
			try
			{
				smtpAddress = SmtpAddress.Parse(base.Definition.Account);
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.EWSTracer, base.TraceContext, "OAuthDiscovery.GetADUser: Failed to parse SMTP address for user {0} Exception: {1}", base.Definition.Account, ex.ToString(), null, "GetADUser", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthPartnerProbe.cs", 371);
				return null;
			}
			ADSessionSettings sessionSettings = ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(smtpAddress.Domain);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 383, "GetADUser", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthPartnerProbe.cs");
			QueryFilter filter = new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.UserPrincipalName, base.Definition.Account),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.PrimarySmtpAddress, base.Definition.Account)
			});
			ADRecipient[] array = tenantOrRootOrgRecipientSession.Find(null, QueryScope.SubTree, filter, null, 1);
			if (array != null && array.Length > 0)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.EWSTracer, base.TraceContext, "OAuthDiscovery.GetADUser: Successfully retrieved AD information for user {0}", base.Definition.Account, null, "GetADUser", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthPartnerProbe.cs", 408);
				return array[0] as ADUser;
			}
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.EWSTracer, base.TraceContext, "OAuthDiscovery.GetADUser: Unable to get AD information for user {0}", base.Definition.Account, null, "GetADUser", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OAuth\\OAuthPartnerProbe.cs", 417);
			return null;
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0006D5CC File Offset: 0x0006B7CC
		private void DropBreadcrumb(string s)
		{
			if (this.breadcrumbs == null)
			{
				this.breadcrumbs = new StringBuilder(512);
			}
			int num = (int)(DateTime.UtcNow - this.TimeStarted).TotalMilliseconds;
			this.breadcrumbs.AppendFormat("[{0:0000}] {1}\r\n", num, s);
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0006D624 File Offset: 0x0006B824
		private void ReportMachineReadableProbeDetails()
		{
			switch (this.State)
			{
			case OAuthPartnerProbe.ProbeState.PreparingRequest:
			case OAuthPartnerProbe.ProbeState.RunningRequest:
			case OAuthPartnerProbe.ProbeState.Passed:
				break;
			case OAuthPartnerProbe.ProbeState.WaitingResponse:
			case OAuthPartnerProbe.ProbeState.FailedRequest:
			case OAuthPartnerProbe.ProbeState.FailedResponse:
			case OAuthPartnerProbe.ProbeState.TimedOut:
			{
				List<string> list = this.SplitDiagnosticMessage(this.DiagnosticMessage);
				string[] array = list.ToArray();
				base.Result.StateAttribute21 = base.Definition.Endpoint;
				base.Result.StateAttribute22 = base.Definition.Account;
				int num = 0;
				if (num < array.Length)
				{
					base.Result.FailureContext = array[num++];
				}
				if (num < array.Length)
				{
					base.Result.StateAttribute1 = array[num++];
				}
				if (num < array.Length)
				{
					base.Result.StateAttribute2 = array[num++];
				}
				if (num < array.Length)
				{
					base.Result.StateAttribute3 = array[num++];
				}
				if (num < array.Length)
				{
					base.Result.StateAttribute4 = array[num++];
				}
				if (num < array.Length)
				{
					base.Result.StateAttribute5 = array[num++];
				}
				if (num < array.Length)
				{
					base.Result.StateAttribute11 = array[num++];
				}
				if (num < array.Length)
				{
					base.Result.StateAttribute12 = array[num++];
				}
				if (num < array.Length)
				{
					base.Result.StateAttribute13 = array[num++];
				}
				if (num < array.Length)
				{
					base.Result.StateAttribute14 = array[num++];
				}
				if (num < array.Length)
				{
					base.Result.StateAttribute15 = array[num++];
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0006D7A8 File Offset: 0x0006B9A8
		private List<string> SplitDiagnosticMessage(string diagnosticMessage)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < diagnosticMessage.Length; i += 1000)
			{
				string item = diagnosticMessage.Substring(i, (i + 1000 < diagnosticMessage.Length) ? 1000 : (diagnosticMessage.Length - i));
				list.Add(item);
			}
			return list;
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0006D800 File Offset: 0x0006BA00
		private void ReportHumanReadableProbeDetails()
		{
			StringBuilder stringBuilder = new StringBuilder(300);
			int num = (int)(this.TimeCompleted - this.TimeStarted).TotalMilliseconds;
			string text = string.IsNullOrEmpty(base.Definition.Endpoint) ? "(none)" : base.Definition.Endpoint;
			switch (this.State)
			{
			case OAuthPartnerProbe.ProbeState.PreparingRequest:
				stringBuilder.AppendFormat("Probe to partner endpoint {0} {1} with user {2} after {3} milliseconds.", new object[]
				{
					text,
					this.State,
					base.Definition.Account,
					num
				});
				goto IL_1A0;
			case OAuthPartnerProbe.ProbeState.WaitingResponse:
			case OAuthPartnerProbe.ProbeState.Passed:
			case OAuthPartnerProbe.ProbeState.TimedOut:
				stringBuilder.AppendFormat("Probe to partner endpoint {0} {1} with user {2} after {3} milliseconds.", new object[]
				{
					text,
					this.State,
					base.Definition.Account,
					num
				});
				goto IL_1A0;
			case OAuthPartnerProbe.ProbeState.FailedRequest:
			case OAuthPartnerProbe.ProbeState.FailedResponse:
				stringBuilder.AppendFormat("Probe to partner endpoint {0} {1} with user {2} after {3} milliseconds.", new object[]
				{
					text,
					this.State,
					base.Definition.Account,
					num
				});
				if (this.Error != null && this.Error.Message != null)
				{
					stringBuilder.Append(this.Error.Message.Replace("+", "plus"));
					goto IL_1A0;
				}
				goto IL_1A0;
			}
			stringBuilder.AppendFormat("new state {0} - update ReportHumanReadableProbeDetails!", this.State);
			IL_1A0:
			stringBuilder.Append("+");
			if (this.Verbose)
			{
				stringBuilder.Append(this.Breadcrumbs);
			}
			ProbeResult result = base.Result;
			result.ExecutionContext += stringBuilder.ToString();
		}

		// Token: 0x04000C3F RID: 3135
		private const int MinimumTimeLimit = 60000;

		// Token: 0x04000C40 RID: 3136
		private static readonly string ProbeTypeName = typeof(OAuthPartnerProbe).FullName;

		// Token: 0x04000C41 RID: 3137
		private StringBuilder breadcrumbs;

		// Token: 0x02000247 RID: 583
		protected enum ProbeState
		{
			// Token: 0x04000C4E RID: 3150
			PreparingRequest,
			// Token: 0x04000C4F RID: 3151
			RunningRequest,
			// Token: 0x04000C50 RID: 3152
			WaitingResponse,
			// Token: 0x04000C51 RID: 3153
			Passed,
			// Token: 0x04000C52 RID: 3154
			FailedRequest,
			// Token: 0x04000C53 RID: 3155
			FailedResponse,
			// Token: 0x04000C54 RID: 3156
			TimedOut
		}
	}
}
