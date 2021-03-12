using System;
using System.Collections;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Calendar.Probes;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WebServices.Autodiscover;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Ews.Probes
{
	// Token: 0x02000016 RID: 22
	public abstract class AutodiscoverCommon : EWSCommon
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00007B3A File Offset: 0x00005D3A
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00007B42 File Offset: 0x00005D42
		public string LookupUsername { get; internal set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00007B4B File Offset: 0x00005D4B
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00007B53 File Offset: 0x00005D53
		protected bool VerifyAutodiscoverForOutlook { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00007B5C File Offset: 0x00005D5C
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00007B64 File Offset: 0x00005D64
		protected bool VerifyAutodiscoverHotmailRedirect { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00007B6D File Offset: 0x00005D6D
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00007B75 File Offset: 0x00005D75
		protected GetUserSettingsResponse UserSettings { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00007B7E File Offset: 0x00005D7E
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00007B86 File Offset: 0x00005D86
		protected AutodiscoverService AutodiscoverService { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00007B8F File Offset: 0x00005D8F
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00007B97 File Offset: 0x00005D97
		protected ExchangeVersion ExchangeServerVersion { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00007BA0 File Offset: 0x00005DA0
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00007BA8 File Offset: 0x00005DA8
		protected string AutodiscoverEndpoint { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00007BB1 File Offset: 0x00005DB1
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00007BB9 File Offset: 0x00005DB9
		protected bool IncludeExchangeRpcUrl { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00007BC2 File Offset: 0x00005DC2
		protected virtual string ComponentId
		{
			get
			{
				return "Autodiscover_AM_Probe";
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007BE8 File Offset: 0x00005DE8
		protected string GetEwsUrl(EWSCommon.TraceListener traceListener, string EwsUrlKey)
		{
			if (!base.IsOutsideInMonitoring)
			{
				if (base.TrustAnySSLCertificate)
				{
					RemoteCertificateValidationCallback callback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
					{
						if (base.SslValidationDelaySeconds != 0)
						{
							Thread.Sleep(base.SslValidationDelaySeconds * 1000);
						}
						return true;
					};
					CertificateValidationManager.RegisterCallback(this.ComponentId, callback);
				}
				return base.ConstructEwsUrl();
			}
			if (base.Definition.Attributes != null && base.Definition.Attributes.ContainsKey(EwsUrlKey))
			{
				return base.Definition.Attributes[EwsUrlKey];
			}
			this.AutodiscoverEndpoint = base.Definition.Endpoint;
			this.LookupUsername = base.Definition.Account;
			this.GetAutodiscoverService(base.Definition.Account, base.Definition.AccountPassword, this.ExchangeServerVersion, this.AutodiscoverEndpoint, traceListener);
			this.GetUserSettings(new ArrayList
			{
				58
			}, this.LookupUsername);
			object obj = null;
			if (!this.UserSettings.Settings.TryGetValue(58, out obj))
			{
				string newLine = Environment.NewLine;
				throw new AutodiscoverException(string.Format("The autodiscover result did not have ews endpoint url {0} Trace = {1}{2}", newLine, newLine, traceListener.RequestLog));
			}
			return (string)this.UserSettings.Settings[58];
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00007D20 File Offset: 0x00005F20
		protected void ConfigureResultName()
		{
			if (base.IsOutsideInMonitoring)
			{
				if (string.Equals(base.Definition.TargetResource, "MSIT", StringComparison.OrdinalIgnoreCase))
				{
					base.Result.ResultName = "MSIT/" + base.Result.ResultName;
					return;
				}
				base.Result.ResultName = "PROD/" + base.Result.ResultName;
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00007D90 File Offset: 0x00005F90
		protected void GetAutodiscoverService(string username, string password, ExchangeVersion exchangeVersion, string autodiscoverEndpoint, ITraceListener traceListener)
		{
			this.AutodiscoverService = new AutodiscoverService(exchangeVersion);
			this.AutodiscoverService.PreAuthenticate = true;
			base.SetupTracer(this.AutodiscoverService, traceListener);
			UriBuilder uriBuilder = new UriBuilder(autodiscoverEndpoint);
			if (base.TargetPort != -1)
			{
				uriBuilder.Port = base.TargetPort;
			}
			this.AutodiscoverService.Url = uriBuilder.Uri;
			this.AutodiscoverService.UserAgent = base.UserAgentPart;
			this.AutodiscoverService.KeepAlive = false;
			if (!base.IsOutsideInMonitoring)
			{
				this.AutodiscoverService.WebProxy = NullWebProxy.Instance;
			}
			base.ApplyAuthentication(this.AutodiscoverService, this.AutodiscoverService.Url, username, password, traceListener);
			base.VerboseResultPairs.AddUnique("AutodUrl", this.AutodiscoverService.Url.ToString());
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00007E64 File Offset: 0x00006064
		protected void CheckAutodiscoverResponse()
		{
			object obj = null;
			if (AutodiscoverCommon.verifyExternalUrl)
			{
				if (!this.UserSettings.Settings.TryGetValue(58, out obj))
				{
					this.AddTracesToExecutionContextAndThrow(58, "Autodiscover Service failed to return the ExternalEWSUrl");
				}
				new Uri((string)obj);
				object obj2 = null;
				if (!this.UserSettings.Settings.TryGetValue(44, out obj2))
				{
					this.AddTracesToExecutionContextAndThrow(44, "Autodiscover Service failed to return the ExternalEcpUrl");
				}
				new Uri((string)obj2);
				object obj3 = null;
				if (!this.UserSettings.Settings.TryGetValue(62, out obj3))
				{
					this.AddTracesToExecutionContextAndThrow(62, "Autodiscover Service failed to return the ExternalUMUrl");
				}
				new Uri((string)obj3);
			}
			if (this.IncludeExchangeRpcUrl)
			{
				object obj4 = null;
				if (!this.UserSettings.Settings.TryGetValue(87, out obj4))
				{
					this.AddTracesToExecutionContextAndThrow(87, "Autodiscover Service failed to return the ExchangeRpcUrl");
				}
			}
			if (15 == base.MailboxVersion)
			{
				if (this.AutodiscoverEndpoint.Equals(base.Definition.SecondaryEndpoint) && !AutodiscoverCommon.guidAtDomainRegex.IsMatch(((EWSCommon.TraceListener)this.AutodiscoverService.TraceListener).RequestLog))
				{
					this.AddTracesToExecutionContextAndThrow("POX Response Version Verification", "The POX Autodiscover request for an E15 mailbox was not served by an E15 server");
				}
				if (this.AutodiscoverEndpoint.Equals(base.Definition.Endpoint) && base.MailboxVersion != this.AutodiscoverService.ServerInfo.MajorVersion)
				{
					this.AddTracesToExecutionContextAndThrow("SOAP Response Version Verification", "The SOAP Autodiscover request for an E15 mailbox was not served by an E15 server");
				}
			}
			if (base.IsOutsideInMonitoring && this.VerifyAutodiscoverForOutlook && this.AutodiscoverEndpoint.Equals(base.Definition.SecondaryEndpoint))
			{
				this.VerifyOutlookPropertiesInAutodiscoverResponse();
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00008013 File Offset: 0x00006213
		protected void GetUserSettings(ArrayList userSettingNames, string lookupUsername)
		{
			this.GetUserSettings(userSettingNames, lookupUsername, true);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000081DC File Offset: 0x000063DC
		protected void GetUserSettings(ArrayList userSettingNames, string lookupUsername, bool logLatency)
		{
			if (this.IncludeExchangeRpcUrl)
			{
				userSettingNames.Add(87);
			}
			bool flag = this.AutodiscoverEndpoint.Equals(base.Definition.Endpoint, StringComparison.InvariantCultureIgnoreCase);
			string latencyAttributeName;
			string key;
			if (flag)
			{
				latencyAttributeName = "SOAP latency breakdown: ";
				key = "SOAP Latency";
			}
			else
			{
				latencyAttributeName = "POX latency breakdown: ";
				key = "POX Latency";
			}
			base.RetrySoapActionAndThrow(delegate()
			{
				int num = 0;
				StringBuilder stringBuilder = new StringBuilder();
				DateTime utcNow = DateTime.UtcNow;
				do
				{
					this.UserSettings = this.AutodiscoverService.GetUserSettings(lookupUsername, (UserSettingName[])userSettingNames.ToArray(typeof(UserSettingName)));
					stringBuilder.Append(this.AutodiscoverService.Url.ToString() + ":" + (int)(DateTime.UtcNow - utcNow).TotalMilliseconds);
					stringBuilder.Append(",");
					utcNow = DateTime.UtcNow;
					if (this.UserSettings.RedirectTarget != null)
					{
						if (this.IsOutsideInMonitoring && this.VerifyAutodiscoverHotmailRedirect && this.UserSettings.RedirectTarget.Contains("hotmail"))
						{
							break;
						}
						this.AutodiscoverService.Url = new Uri(this.UserSettings.RedirectTarget);
					}
				}
				while (this.UserSettings.ErrorCode == 2 && this.UserSettings.RedirectTarget != null && ++num < 10);
				if (logLatency)
				{
					ProbeResult result = this.Result;
					result.StateAttribute22 = result.StateAttribute22 + latencyAttributeName + Environment.NewLine;
					ProbeResult result2 = this.Result;
					result2.StateAttribute22 += stringBuilder.ToString();
					ProbeResult result3 = this.Result;
					result3.StateAttribute22 += Environment.NewLine;
				}
			}, "GetUserSettings", this.AutodiscoverService, logLatency);
			if (logLatency)
			{
				base.VitalResultPairs.AddUnique(key, base.Result.SampleValue.ToString());
				if (flag)
				{
					base.Result.StateAttribute20 = base.Result.SampleValue;
				}
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000082E8 File Offset: 0x000064E8
		protected void RunAutodiscoverProbe(Trace exTraceGlobal, string username, string password, bool logLatency)
		{
			this.exTraceGlobal = exTraceGlobal;
			AutodiscoverCommon.certName = string.Empty;
			this.GetAutodiscoverService(username, password, this.ExchangeServerVersion, this.AutodiscoverEndpoint, new EWSCommon.TraceListener(base.TraceContext, this.exTraceGlobal));
			if (base.TrustAnySSLCertificate)
			{
				RemoteCertificateValidationCallback callback = (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;
				CertificateValidationManager.RegisterCallback(this.ComponentId, callback);
				this.AutodiscoverService.HttpHeaders.Add(CertificateValidationManager.ComponentIdHeaderName, this.ComponentId);
			}
			if (this.VerifyAutodiscoverForOutlook && base.IsOutsideInMonitoring && this.AutodiscoverEndpoint.Equals(base.Definition.SecondaryEndpoint))
			{
				RemoteCertificateValidationCallback callback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
				{
					AutodiscoverCommon.certName = certificate.Subject;
					return base.TrustAnySSLCertificate || sslPolicyErrors == SslPolicyErrors.None;
				};
				CertificateValidationManager.RegisterCallback(this.ComponentId, callback, true);
				this.AutodiscoverService.HttpHeaders.Add(CertificateValidationManager.ComponentIdHeaderName, this.ComponentId);
			}
			ArrayList arrayList = new ArrayList();
			arrayList.Add(58);
			arrayList.Add(44);
			arrayList.Add(62);
			if (this.VerifyAutodiscoverForOutlook && base.IsOutsideInMonitoring && this.AutodiscoverEndpoint.Equals(base.Definition.SecondaryEndpoint))
			{
				arrayList.Add(89);
				arrayList.Add(95);
				arrayList.Add(31);
				arrayList.Add(74);
				arrayList.Add(86);
				arrayList.Add(1);
			}
			this.GetUserSettings(arrayList, this.LookupUsername, logLatency);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00008498 File Offset: 0x00006698
		protected override void Configure()
		{
			this.IncludeExchangeRpcUrl = this.ReadAttribute("IncludeExchangeRpcUrl", false);
			this.VerifyAutodiscoverHotmailRedirect = this.ReadAttribute("VerifyAutodiscoverHotmailRedirect", false);
			this.VerifyAutodiscoverForOutlook = this.ReadAttribute("VerifyAutodiscoverForOutlook", false);
			base.Configure();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000084D8 File Offset: 0x000066D8
		protected void RunAutodiscoverProbe(CancellationToken cancellationToken, string probeName)
		{
			try
			{
				base.Initialize(ExTraceGlobals.AutoDiscoverTracer);
				base.LogTrace(string.Format("Starting {0} with Username: {1}", probeName, this.LookupUsername));
				this.LookupUsername = base.Definition.Account;
				base.DecorateLogSection("Autodiscover Soap Endpoint Verification");
				this.ExchangeServerVersion = 2;
				this.AutodiscoverEndpoint = base.Definition.Endpoint;
				this.RunAutodiscoverProbe(ExTraceGlobals.AutoDiscoverTracer, base.Definition.Account, base.Definition.AccountPassword, true);
				ServicePointManager.DefaultConnectionLimit = int.MaxValue;
				this.CheckAutodiscoverResponse();
				base.DecorateLogSection("Autodiscover Xml Endpoint Verification");
				this.ExchangeServerVersion = 0;
				this.AutodiscoverEndpoint = base.Definition.SecondaryEndpoint;
				this.RunAutodiscoverProbe(ExTraceGlobals.AutoDiscoverTracer, base.Definition.Account, base.Definition.AccountPassword, true);
				this.CheckAutodiscoverResponse();
				base.WriteVitalInfoToExecutionContext();
				if (base.IsOutsideInMonitoring && this.VerifyAutodiscoverHotmailRedirect)
				{
					string text = "CheckAutodiscoverHotmailRedirectResponse@outlook.com";
					this.ExchangeServerVersion = 0;
					this.AutodiscoverEndpoint = base.Definition.SecondaryEndpoint;
					this.LookupUsername = text;
					this.RunAutodiscoverProbe(ExTraceGlobals.AutoDiscoverTracer, text, "CheckAutodiscoverHotmailRedirectResponse", false);
					this.VerifyHotmailRedirect();
				}
				base.LogTrace(string.Format("{0} succeeded", probeName));
			}
			catch (Exception exceptiondata)
			{
				base.ThrowError(string.Format("{0} failed", probeName), exceptiondata, "");
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00008654 File Offset: 0x00006854
		private void VerifyHotmailRedirect()
		{
			if (this.UserSettings.RedirectTarget != null && this.UserSettings.RedirectTarget.Contains("hotmail"))
			{
				this.PublishHotmailRedirectResult(ResultType.Succeeded);
				return;
			}
			this.PublishHotmailRedirectResult(ResultType.Failed);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00008689 File Offset: 0x00006889
		protected void PerformBasicOutlookValidation()
		{
			this.VerifyPropertyPresent(89);
			this.VerifyPropertyPresent(31);
			this.VerifyPropertyPresent(74);
			this.VerifyPropertyPresent(86);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000086B0 File Offset: 0x000068B0
		protected void VerifyOutlookPropertiesInAutodiscoverResponse()
		{
			this.PerformBasicOutlookValidation();
			this.ValidateCertPrincipalName();
			string text = this.ReadAttribute("LegacyDN", string.Empty);
			object obj = this.VerifyPropertyPresent(1);
			if (obj != null && !text.Equals(obj as string, StringComparison.OrdinalIgnoreCase))
			{
				this.AddTracesToExecutionContextAndThrow(1, string.Format("Expected and actual Lgacy DN did not match. Actual: {0}, Expected: {1}", obj as string, text));
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00008714 File Offset: 0x00006914
		protected object VerifyPropertyPresent(UserSettingName setting)
		{
			object result = null;
			if (!this.UserSettings.Settings.TryGetValue(setting, out result))
			{
				this.AddTracesToExecutionContextAndThrow(setting, "Autodiscover Service failed to return the " + setting.ToString());
			}
			return result;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000875C File Offset: 0x0000695C
		private void ValidateCertPrincipalName()
		{
			object obj = null;
			if (!this.UserSettings.Settings.TryGetValue(95, out obj))
			{
				this.AddTracesToExecutionContextAndThrow(95, "Autodiscover Service failed to return the CertPrincipalName");
			}
			if (string.IsNullOrEmpty(AutodiscoverCommon.certName))
			{
				this.AddTracesToExecutionContextAndThrow("ValidateCertPrincipalName", "Certificate subject name was not found");
			}
			string text = obj as string;
			string[] array = AutodiscoverCommon.certName.Split(new string[]
			{
				"CN=",
				","
			}, StringSplitOptions.RemoveEmptyEntries);
			string[] array2 = text.Split(new string[]
			{
				"msstd:"
			}, StringSplitOptions.RemoveEmptyEntries);
			string format = "Certificate subject name {0} did not match CertPrincipalName {1} from autodiscover response";
			if (array == null || array.Length == 0)
			{
				this.AddTracesToExecutionContextAndThrow("OutlookCertificateValidation", "Unable to extrace CN from certifiate subject");
			}
			if (array2 == null || array2.Length == 0)
			{
				this.AddTracesToExecutionContextAndThrow("OutlookCertificateValidation", "Unable to extract subject name from the autodiscover CertPrincipalName");
			}
			if (!array[0].Equals(array2[0], StringComparison.InvariantCultureIgnoreCase))
			{
				this.AddTracesToExecutionContextAndThrow("OutlookCertificateValidation", string.Format(format, array[0], array2[0]));
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00008858 File Offset: 0x00006A58
		private void PublishHotmailRedirectResult(ResultType resultType)
		{
			string arg = string.Format("Autodiscover{0}HotmailRedirect Probe", base.MailboxVersion.ToString());
			ProbeResult probeResult = new ProbeResult(base.Definition);
			probeResult.ExecutionStartTime = DateTime.UtcNow;
			probeResult.ExecutionEndTime = DateTime.UtcNow;
			probeResult.ResultName = string.Format("{0}/{1}", arg, base.Definition.TargetPartition);
			probeResult.ResultType = resultType;
			base.Broker.PublishResult(probeResult);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000088CF File Offset: 0x00006ACF
		private void AddTracesToExecutionContextAndThrow(object key, string exceptiondata)
		{
			base.TraceBuilder.AppendLine(((EWSCommon.TraceListener)this.AutodiscoverService.TraceListener).RequestLog);
			base.ThrowError(key, exceptiondata, "");
		}

		// Token: 0x0400009A RID: 154
		private const int MaxHops = 10;

		// Token: 0x0400009B RID: 155
		private const string EnvMSIT = "MSIT";

		// Token: 0x0400009C RID: 156
		private const string EnvPROD = "PROD";

		// Token: 0x0400009D RID: 157
		private static Regex guidAtDomainRegex = new Regex("<Server>(?<mailboxguid>[A-Fa-f0-9]{32}|({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?|^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}}))@(?<domain>[\\S.]+)</Server>", RegexOptions.IgnoreCase);

		// Token: 0x0400009E RID: 158
		private static bool verifyExternalUrl = VariantConfiguration.InvariantNoFlightingSnapshot.ActiveMonitoring.AutoDiscoverExternalUrlVerification.Enabled;

		// Token: 0x0400009F RID: 159
		private static string certName = string.Empty;

		// Token: 0x040000A0 RID: 160
		private Trace exTraceGlobal;
	}
}
