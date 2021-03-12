using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common.SmartCategorization;
using Microsoft.Exchange.WebServices.Autodiscover;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Ews.Probes
{
	// Token: 0x02000014 RID: 20
	public abstract class EWSCommon : ProbeWorkItem
	{
		// Token: 0x06000083 RID: 131 RVA: 0x0000604C File Offset: 0x0000424C
		protected EWSCommon()
		{
			this.VerboseResultPairs = new Dictionary<string, string>();
			this.VitalResultPairs = new Dictionary<string, string>();
			this.TraceBuilder = new StringBuilder();
			this.LatencyMeasurement = new Stopwatch();
			this.Tracer = ExTraceGlobals.EWSTracer;
			this.Breadcrumbs = new Breadcrumbs(1024);
			this.InitErrorClassificationDictionaries();
			this.ProbeStartTime = DateTime.UtcNow;
			this.Breadcrumbs.Drop("EWSCommon start: {0}", new object[]
			{
				this.ProbeStartTime
			});
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000060DD File Offset: 0x000042DD
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000060E5 File Offset: 0x000042E5
		protected Microsoft.Exchange.Diagnostics.Trace Tracer { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000060EE File Offset: 0x000042EE
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000060F6 File Offset: 0x000042F6
		protected string ClientStatistics { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000060FF File Offset: 0x000042FF
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00006107 File Offset: 0x00004307
		protected Dictionary<string, string> VerboseResultPairs { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00006110 File Offset: 0x00004310
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00006118 File Offset: 0x00004318
		protected StringBuilder TraceBuilder { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00006121 File Offset: 0x00004321
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00006129 File Offset: 0x00004329
		protected Dictionary<string, string> VitalResultPairs { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00006132 File Offset: 0x00004332
		// (set) Token: 0x0600008F RID: 143 RVA: 0x0000613A File Offset: 0x0000433A
		protected Stopwatch LatencyMeasurement { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00006143 File Offset: 0x00004343
		// (set) Token: 0x06000091 RID: 145 RVA: 0x0000614B File Offset: 0x0000434B
		protected int ApiRetryCount { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00006154 File Offset: 0x00004354
		// (set) Token: 0x06000093 RID: 147 RVA: 0x0000615C File Offset: 0x0000435C
		protected int ApiRetrySleepInMilliseconds { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00006165 File Offset: 0x00004365
		// (set) Token: 0x06000095 RID: 149 RVA: 0x0000616D File Offset: 0x0000436D
		protected int MailboxVersion { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00006176 File Offset: 0x00004376
		// (set) Token: 0x06000097 RID: 151 RVA: 0x0000617E File Offset: 0x0000437E
		protected string UnifiedNamespace { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00006187 File Offset: 0x00004387
		// (set) Token: 0x06000099 RID: 153 RVA: 0x0000618F File Offset: 0x0000438F
		protected bool CafeVipModeWhenPossible { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00006198 File Offset: 0x00004398
		// (set) Token: 0x0600009B RID: 155 RVA: 0x000061A0 File Offset: 0x000043A0
		protected string AuthenticationMethod { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000061A9 File Offset: 0x000043A9
		// (set) Token: 0x0600009D RID: 157 RVA: 0x000061B1 File Offset: 0x000043B1
		protected bool UseXropEndpoint { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000061BA File Offset: 0x000043BA
		// (set) Token: 0x0600009F RID: 159 RVA: 0x000061C2 File Offset: 0x000043C2
		protected int TargetPort { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000061CB File Offset: 0x000043CB
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000061D3 File Offset: 0x000043D3
		protected string AuthenticationDomain { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000061DC File Offset: 0x000043DC
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000061E4 File Offset: 0x000043E4
		protected ProbeAuthNType PrimaryAuthN { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000061ED File Offset: 0x000043ED
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000061F5 File Offset: 0x000043F5
		protected ProbeAuthNType AlternateAuthN { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000061FE File Offset: 0x000043FE
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00006206 File Offset: 0x00004406
		protected bool IsOutsideInMonitoring { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000620F File Offset: 0x0000440F
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00006217 File Offset: 0x00004417
		public Datacenter.ExchangeSku ExchangeSku { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00006220 File Offset: 0x00004420
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00006228 File Offset: 0x00004428
		protected bool TrustAnySSLCertificate { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00006231 File Offset: 0x00004431
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00006239 File Offset: 0x00004439
		private protected int ProbeTimeLimit { protected get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00006242 File Offset: 0x00004442
		// (set) Token: 0x060000AF RID: 175 RVA: 0x0000624A File Offset: 0x0000444A
		private protected int HttpRequestTimeout { protected get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00006253 File Offset: 0x00004453
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x0000625B File Offset: 0x0000445B
		private protected int SslValidationDelaySeconds { protected get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00006264 File Offset: 0x00004464
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x0000626C File Offset: 0x0000446C
		private protected int PreRequestDelaySeconds { protected get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00006275 File Offset: 0x00004475
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000627D File Offset: 0x0000447D
		protected string UserAgentPart { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00006286 File Offset: 0x00004486
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000628E File Offset: 0x0000448E
		protected bool Verbose { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00006297 File Offset: 0x00004497
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x0000629F File Offset: 0x0000449F
		protected DateTime ProbeStartTime { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000062A8 File Offset: 0x000044A8
		protected ProbeAuthNType EffectiveAuthN
		{
			get
			{
				if (this.PrimaryAuthN == ProbeAuthNType.LiveIdOrNegotiate)
				{
					if (Datacenter.ExchangeSku.ExchangeDatacenter != this.ExchangeSku)
					{
						return ProbeAuthNType.Negotiate;
					}
					return ProbeAuthNType.LiveId;
				}
				else
				{
					if (this.PrimaryAuthN != ProbeAuthNType.LiveIdOrCafe)
					{
						return this.PrimaryAuthN;
					}
					if (Datacenter.ExchangeSku.ExchangeDatacenter != this.ExchangeSku)
					{
						return ProbeAuthNType.Cafe;
					}
					return ProbeAuthNType.LiveId;
				}
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000062DB File Offset: 0x000044DB
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000062E3 File Offset: 0x000044E3
		private protected Breadcrumbs Breadcrumbs { protected get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000062EC File Offset: 0x000044EC
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000062F3 File Offset: 0x000044F3
		private static Dictionary<int, EWSAutoDProtocolDependency> KnownHTTPErrors { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000062FB File Offset: 0x000044FB
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00006302 File Offset: 0x00004502
		private static Dictionary<ServiceError, EWSAutoDProtocolDependency> KnownEWSErrorCodes { get; set; }

		// Token: 0x060000C1 RID: 193 RVA: 0x0000630C File Offset: 0x0000450C
		protected string TransformResultPairsToString(Dictionary<string, string> keyPairs)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in keyPairs.Keys)
			{
				string arg = text;
				EWSExtensions.TryConvertRegularLogKey(text, out arg);
				if (string.IsNullOrEmpty(keyPairs[text]))
				{
					stringBuilder.AppendFormat("{0}{1}", arg, Environment.NewLine);
				}
				else
				{
					stringBuilder.AppendFormat("{0}={1}{2}", arg, keyPairs[text], Environment.NewLine);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000063AC File Offset: 0x000045AC
		protected ExchangeService GetExchangeService(string username, string password, ITraceListener traceListener, ExchangeVersion version)
		{
			return this.GetExchangeService(username, password, traceListener, base.Definition.Endpoint, version);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000063C4 File Offset: 0x000045C4
		protected ExchangeService GetExchangeService(string username, string password, ITraceListener traceListener, string ewsURL, ExchangeVersion version)
		{
			this.VerboseResultPairs.AddUnique("UserName/Password", string.Format("{0}/{1}", username, this.ReadAttribute("HidePasswordInLog", true) ? "******" : password));
			ExchangeService exchangeService = new ExchangeService(version);
			this.SetupTracer(exchangeService, traceListener);
			exchangeService.PreAuthenticate = true;
			exchangeService.Url = new Uri(ewsURL);
			exchangeService.UserAgent = this.UserAgentPart;
			exchangeService.KeepAlive = false;
			if (!this.IsOutsideInMonitoring)
			{
				exchangeService.WebProxy = NullWebProxy.Instance;
			}
			this.ApplyAuthentication(exchangeService, exchangeService.Url, username, password, traceListener);
			if (this.HttpRequestTimeout > 0)
			{
				this.Breadcrumbs.Drop("using HTTP request timeout: {0} ms", new object[]
				{
					this.HttpRequestTimeout
				});
				exchangeService.Timeout = this.HttpRequestTimeout;
			}
			else
			{
				this.Breadcrumbs.Drop("using HTTP request timeout: NONE", new object[]
				{
					this.HttpRequestTimeout
				});
			}
			return exchangeService;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000064C0 File Offset: 0x000046C0
		protected void ApplyAuthentication(ExchangeServiceBase service, Uri url, string username, string password, ITraceListener traceListener)
		{
			string text = (service is AutodiscoverService) ? "Autodiscover" : "EWS";
			switch (this.EffectiveAuthN)
			{
			case ProbeAuthNType.LiveIdOrNegotiate:
				throw new InvalidOperationException("EWSCommon.ApplyAuthentication: EffectiveAuthentication accessor is broken!");
			case ProbeAuthNType.LiveId:
				this.AuthenticationMethod = "Basic";
				this.LogTrace(string.Format("{0} URL: {1} Using {2} authentication.", text, url, this.AuthenticationMethod));
				if (text.Equals("Autodiscover") && this.IsOutsideInMonitoring)
				{
					string arg = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", username, password)));
					service.HttpHeaders["Authorization"] = string.Format("Basic {0}", arg);
				}
				else
				{
					service.Credentials = new CredentialCache
					{
						{
							url,
							"Basic",
							new NetworkCredential(username, password)
						}
					};
				}
				break;
			case ProbeAuthNType.Negotiate:
				this.AuthenticationMethod = "Negotiate";
				this.LogTrace(string.Format("{0} URL: {1} Using {2} authentication.", text, url, this.AuthenticationMethod));
				if (!string.IsNullOrEmpty(this.AuthenticationDomain))
				{
					SmtpAddress smtpAddress = SmtpAddress.Parse(username);
					this.LogTrace(string.Format("user: {0} domain: {1}", smtpAddress.Local, this.AuthenticationDomain));
					service.Credentials = new NetworkCredential(smtpAddress.Local, password, this.AuthenticationDomain);
				}
				else
				{
					this.LogTrace(string.Format("user: {0}", username));
					service.Credentials = new NetworkCredential(username, password);
				}
				break;
			case ProbeAuthNType.Cafe:
				this.AuthenticationMethod = "CAFE";
				this.LogTrace(string.Format("{0} URL: {1} Using {2} authentication.", text, url, this.AuthenticationMethod));
				service.ApplyCafeAuthentication(username, password);
				break;
			case ProbeAuthNType.Web:
				this.AuthenticationMethod = "Web";
				this.LogTrace(string.Format("{0} URL: {1} Using {2} authentication.", text, url, this.AuthenticationMethod));
				service.Credentials = new WebCredentials(username, password);
				break;
			default:
				throw new InvalidOperationException("EWSCommon.ApplyAuthentication: do not attempt to use ProbeAuthNType.Unused!");
			}
			this.Breadcrumbs.Drop("using authN: {0} {1} {2}", new object[]
			{
				this.AuthenticationMethod,
				username,
				this.ReadAttribute("HidePasswordInLog", true) ? "******" : password
			});
			this.VerboseResultPairs.AddUnique("AuthMethod", this.AuthenticationMethod);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006712 File Offset: 0x00004912
		protected void Initialize(Microsoft.Exchange.Diagnostics.Trace tracer)
		{
			this.Tracer = tracer;
			this.ProbeStartTime = DateTime.UtcNow;
			this.Configure();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000672C File Offset: 0x0000492C
		protected virtual void Configure()
		{
			this.Breadcrumbs.Drop("Configuring EWScommon");
			this.ApiRetryCount = this.ReadAttribute("ApiRetryCount", 1);
			this.ApiRetrySleepInMilliseconds = this.ReadAttribute("ApiRetrySleepInMilliseconds", 5000);
			this.UseXropEndpoint = this.ReadAttribute("UseXropEndPoint", false);
			this.TargetPort = this.ReadAttribute("TargetPort", -1);
			this.MailboxVersion = this.ReadAttribute("MailboxVersion", 14);
			this.UnifiedNamespace = this.ReadAttribute("UnifiedNamespace", "outlook.com");
			this.CafeVipModeWhenPossible = this.ReadAttribute("Cas15VipModeWhenPossible", false);
			this.AuthenticationDomain = this.ReadAttribute("Domain", null);
			this.IsOutsideInMonitoring = this.ReadAttribute("IsOutsideInMonitoring", true);
			if (this.IsOutsideInMonitoring)
			{
				this.ExchangeSku = this.ReadAttribute("ExchangeSku", Datacenter.ExchangeSku.ExchangeDatacenter);
			}
			else
			{
				this.ExchangeSku = Datacenter.GetExchangeSku();
			}
			this.PrimaryAuthN = this.ReadAttribute("PrimaryAuthN", ProbeAuthNType.LiveIdOrNegotiate);
			this.AlternateAuthN = this.ReadAttribute("AlternateAuthN", ProbeAuthNType.Unused);
			this.TrustAnySSLCertificate = this.ReadAttribute("TrustAnySslCertificate", false);
			this.UserAgentPart = this.ReadAttribute("UserAgentPart", "AMProbe");
			this.Verbose = this.ReadAttribute("Verbose", true);
			int val = 1000 * ((base.Definition.TimeoutSeconds > 0) ? base.Definition.TimeoutSeconds : 180);
			this.ProbeTimeLimit = Math.Max(val, 5000);
			this.HttpRequestTimeout = (int)this.ReadAttribute("HttpRequestTimeoutSpan", TimeSpan.FromMilliseconds((double)((this.ProbeTimeLimit - 1000) / (this.ApiRetryCount + 1)))).TotalMilliseconds;
			this.Breadcrumbs.Drop("Probe time limit: {0}ms, HTTP timeout: {1}ms, RetryCount: {2}", new object[]
			{
				this.ProbeTimeLimit,
				this.HttpRequestTimeout,
				this.ApiRetryCount
			});
			this.SslValidationDelaySeconds = Utils.LoadAppconfigIntSetting("SslValidationDelaySeconds", 0);
			this.PreRequestDelaySeconds = Utils.LoadAppconfigIntSetting("PreRequestDelaySeconds", 0);
			if (this.IsOutsideInMonitoring)
			{
				if (Datacenter.ExchangeSku.ExchangeDatacenter == this.ExchangeSku)
				{
					this.PrimaryAuthN = ProbeAuthNType.LiveId;
					this.AlternateAuthN = ProbeAuthNType.Negotiate;
				}
				if (this.UserAgentPart.Equals("AMProbe"))
				{
					this.UserAgentPart = "AMProbe/OutsideIn/" + Guid.NewGuid().ToString();
					this.Breadcrumbs.Drop("UserAgent: {0}", new object[]
					{
						this.UserAgentPart
					});
				}
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000069C4 File Offset: 0x00004BC4
		protected ProbeAuthNType ReadAttribute(string name, ProbeAuthNType defaultValue)
		{
			string value = this.ReadAttribute(name, string.Empty);
			ProbeAuthNType result;
			if (!string.IsNullOrEmpty(value) && Enum.TryParse<ProbeAuthNType>(value, true, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000069F4 File Offset: 0x00004BF4
		protected Datacenter.ExchangeSku ReadAttribute(string name, Datacenter.ExchangeSku defaultValue)
		{
			string value = this.ReadAttribute(name, string.Empty);
			Datacenter.ExchangeSku result;
			if (!string.IsNullOrEmpty(value) && Enum.TryParse<Datacenter.ExchangeSku>(value, true, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006A24 File Offset: 0x00004C24
		protected void SetClientStatistics(string requestId, string soapAction, double clientResponseTimeInMs)
		{
			this.ClientStatistics = string.Format("MessageId={0},SoapAction={1},ResponseTime={2};", requestId, soapAction, clientResponseTimeInMs);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006A3E File Offset: 0x00004C3E
		protected void SetupTracer(ExchangeServiceBase service, ITraceListener traceListener)
		{
			service.TraceListener = traceListener;
			service.TraceFlags = 54L;
			service.TraceEnabled = true;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006A57 File Offset: 0x00004C57
		protected void LogTrace(string msg)
		{
			WTFDiagnostics.TraceInformation(this.Tracer, base.TraceContext, msg, null, "LogTrace", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Ews\\EWSCommon.cs", 686);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006A7C File Offset: 0x00004C7C
		protected void WriteErrorData(object key, object exceptiondata, string logDetails = "")
		{
			this.LatencyMeasurement.Stop();
			base.Result.StateAttribute20 = (double)this.LatencyMeasurement.ElapsedMilliseconds;
			this.VitalResultPairs.AddUnique(string.Format("Latency(failed_tag={0})", key), base.Result.StateAttribute20.ToString());
			this.VerboseResultPairs.AddUnique(string.Format("{0}:Exception", key), exceptiondata.ToString());
			if (!string.IsNullOrEmpty(logDetails))
			{
				this.TraceBuilder.AppendLine(logDetails);
			}
			if (this.Verbose)
			{
				ProbeResult result = base.Result;
				result.ExecutionContext += this.TransformResultPairsToString(this.VerboseResultPairs);
				this.SplitTraceDataInto1024ChunksAndWriteitToDatabaseFields();
			}
			this.WriteVitalInfoToExecutionContext();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00006B3C File Offset: 0x00004D3C
		protected void SplitTraceDataInto1024ChunksAndWriteitToDatabaseFields()
		{
			int num = 1024;
			string text = this.TraceBuilder.ToString();
			List<string> list = new List<string>(text.Length / num + 1);
			for (int i = 0; i < text.Length; i += num)
			{
				list.Add(text.Substring(i, Math.Min(num, text.Length - i)));
			}
			int num2 = 0;
			if (list.Count > num2)
			{
				base.Result.FailureContext = list[num2];
				num2++;
			}
			if (list.Count > num2)
			{
				base.Result.StateAttribute22 = list[num2];
				num2++;
			}
			if (list.Count > num2)
			{
				base.Result.StateAttribute23 = list[num2];
				num2++;
			}
			if (list.Count > num2)
			{
				base.Result.StateAttribute24 = list[num2];
				num2++;
			}
			if (list.Count > num2)
			{
				base.Result.StateAttribute25 = list[num2];
				num2++;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00006C49 File Offset: 0x00004E49
		protected void ThrowError(object key, object exceptiondata, string logDetails = "")
		{
			this.SetDefaultValuesForStateAttributes1And2();
			this.WriteErrorData(key, exceptiondata, logDetails);
			throw new Exception(exceptiondata.ToString());
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006C65 File Offset: 0x00004E65
		protected string ConstructEwsUrl()
		{
			return this.ConstructEwsUrl(base.Definition.Endpoint);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00006C78 File Offset: 0x00004E78
		protected string ConstructEwsUrl(string endPoint)
		{
			Uri uri = new Uri(endPoint);
			string text = this.UseXropEndpoint ? "xrop-" : string.Empty;
			string text2 = uri.Authority;
			if (CafeUtils.RunningUnderCafeVipMode && 15 == this.MailboxVersion)
			{
				text = string.Empty;
				text2 = CafeUtils.CurrentCafeVipAddress.ToString();
			}
			Uri uri2;
			if (this.TargetPort != -1)
			{
				uri2 = new Uri(string.Format("https://{0}{1}:{2}{3}", new object[]
				{
					text,
					text2,
					this.TargetPort,
					uri.PathAndQuery
				}));
			}
			else
			{
				uri2 = new Uri(string.Format("https://{0}{1}{2}", text, text2, uri.PathAndQuery));
			}
			this.VerboseResultPairs.AddUnique("EwsUrl", uri2.ToString());
			return uri2.ToString();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00006D48 File Offset: 0x00004F48
		protected void LogIpAddresses(Uri uri)
		{
			string key = string.Format("ip-addresses/{0}", uri.Host);
			try
			{
				IPAddress[] hostAddresses = Dns.GetHostAddresses(uri.Host);
				StringBuilder stringBuilder = new StringBuilder(1024);
				if (hostAddresses != null && hostAddresses.Length > 0)
				{
					stringBuilder.Append(hostAddresses[0]);
					for (int i = 1; i < hostAddresses.Length; i++)
					{
						stringBuilder.AppendFormat("{0}{1}", "; ", hostAddresses[i]);
					}
					this.VerboseResultPairs.AddUnique(key, stringBuilder.ToString());
				}
			}
			catch (Exception)
			{
				this.VerboseResultPairs.AddUnique(key, "could not be resolved");
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00006DEC File Offset: 0x00004FEC
		protected void DecorateLogSection(string title)
		{
			this.VerboseResultPairs.AddUnique(string.Empty, string.Empty);
			this.VerboseResultPairs.AddUnique(string.Format(">>> {0}", title), string.Empty);
			this.VerboseResultPairs.AddUnique(string.Empty, string.Empty);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00006E3E File Offset: 0x0000503E
		protected void WriteVitalInfoToExecutionContext()
		{
			ProbeResult result = base.Result;
			result.ExecutionContext += "Important:\r\n";
			ProbeResult result2 = base.Result;
			result2.ExecutionContext += this.TransformResultPairsToString(this.VitalResultPairs);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00006E7D File Offset: 0x0000507D
		protected void RetrySoapActionAndThrow(Action operation, string soapAction, ExchangeServiceBase service)
		{
			this.RetrySoapActionAndThrow(operation, soapAction, service, CancellationToken.None, false);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006E8E File Offset: 0x0000508E
		protected void RetrySoapActionAndThrow(Action operation, string soapAction, ExchangeServiceBase service, CancellationToken cancellationToken)
		{
			this.RetrySoapActionAndThrow(operation, soapAction, service, cancellationToken, false);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006E9C File Offset: 0x0000509C
		protected void RetrySoapActionAndThrow(Action operation, string soapAction, ExchangeServiceBase service, bool trackLatency)
		{
			this.RetrySoapActionAndThrow(operation, soapAction, service, CancellationToken.None, trackLatency);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006EE8 File Offset: 0x000050E8
		protected void RetrySoapActionAndThrow(Action operation, string soapAction, ExchangeServiceBase service, CancellationToken cancellationToken, bool trackLatency)
		{
			this.Breadcrumbs.Drop("Entering RetrySoapActionAndThrow. soapAction = {0}.", new object[]
			{
				soapAction
			});
			DateTime utcNow = DateTime.UtcNow;
			int num = this.ApiRetryCount;
			Stopwatch stopwatch = new Stopwatch();
			if (trackLatency)
			{
				base.Result.SampleValue = 0.0;
			}
			for (int i = 0; i <= num; i++)
			{
				try
				{
					this.Breadcrumbs.Drop("Action iteration {0}", new object[]
					{
						i
					});
					if (this.ClientStatistics != null)
					{
						service.HttpHeaders["X-ClientStatistics"] = this.ClientStatistics;
						this.ClientStatistics = null;
					}
					utcNow = DateTime.UtcNow;
					TimeSpan timeSpan = DateTime.UtcNow - base.Result.ExecutionStartTime;
					int num2 = Math.Max(0, this.ProbeTimeLimit - (int)timeSpan.TotalMilliseconds);
					this.Breadcrumbs.Drop("Starting (total time left {0} ms)", new object[]
					{
						num2
					});
					Task task = new Task(delegate()
					{
						if (this.PreRequestDelaySeconds > 0)
						{
							Thread.Sleep(this.PreRequestDelaySeconds * 1000);
						}
						operation();
					});
					if (trackLatency)
					{
						stopwatch.Start();
					}
					task.Start();
					if (!task.IsCompleted && num2 > 0)
					{
						task.Wait(num2);
					}
					if (trackLatency)
					{
						stopwatch.Stop();
						base.Result.SampleValue = base.Result.SampleValue + (double)stopwatch.ElapsedMilliseconds;
					}
					DateTime utcNow2 = DateTime.UtcNow;
					if (!task.IsCompleted || cancellationToken.IsCancellationRequested)
					{
						this.Breadcrumbs.Drop("Action timed out!");
						string message = string.Format("Iteration {0}; {1} seconds elapsed", i, (utcNow2 - utcNow).TotalSeconds);
						throw new TimeoutException(message);
					}
					this.Breadcrumbs.Drop("Action completed normally");
					this.UpdateStatsAndLogs(service, soapAction, utcNow, DateTime.UtcNow, "success", i);
					break;
				}
				catch (Exception innerException)
				{
					if (trackLatency)
					{
						stopwatch.Stop();
						base.Result.SampleValue = base.Result.SampleValue + (double)stopwatch.ElapsedMilliseconds;
					}
					AggregateException ex = innerException as AggregateException;
					if (ex != null)
					{
						innerException = ex.Flatten().InnerException;
					}
					this.Breadcrumbs.Drop("Action threw {0}: {1}", new object[]
					{
						innerException.GetType().ToString(),
						innerException.Message
					});
					if (innerException.Message.Contains("The underlying connection was closed") && num == 0)
					{
						this.Breadcrumbs.Drop("Exception was related to connection being closed. Setting internalRetryCount to 1.");
						num = 1;
					}
					else
					{
						this.VitalResultPairs.AddUnique(string.Format("{0} (Attempt #{1})/exception", soapAction, i), innerException.Message);
						this.UpdateStatsAndLogs(service, soapAction, utcNow, DateTime.UtcNow, innerException.Message, i);
						if (num == i)
						{
							this.ClassifyErrorAndUpdateStateAttributes(innerException, service);
							this.Breadcrumbs.Drop("EWS call {0} failed after retrying {1} time(s)", new object[]
							{
								soapAction,
								i
							});
							this.TraceBuilder.AppendLine(((EWSCommon.TraceListener)service.TraceListener).RequestLog);
							StringBuilder stringBuilder = new StringBuilder(this.TransformResultPairsToString(this.VerboseResultPairs));
							this.VerboseResultPairs.Clear();
							this.VitalResultPairs.Clear();
							throw new Exception(stringBuilder.ToString());
						}
					}
					this.LogTrace("EWS call failed - attempt count: " + i.ToString());
					Thread.Sleep(TimeSpan.FromMilliseconds((double)this.ApiRetrySleepInMilliseconds));
				}
				finally
				{
					ProbeResult result = base.Result;
					result.StateAttribute21 += this.Breadcrumbs.ToString();
					this.Breadcrumbs.Clear();
				}
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000072F4 File Offset: 0x000054F4
		protected string GetFailureContext(IDictionary<string, string> headerContent)
		{
			RequestFailureContext requestFailureContext;
			if (RequestFailureContext.TryCreateFromResponseHeaders(headerContent, out requestFailureContext))
			{
				string text = CafeFailureAnalyser.Instance.Analyse(RequestContext.Monitoring, requestFailureContext).ToString() + "\n" + requestFailureContext.ToString();
				this.LogTrace(text);
				return text;
			}
			return string.Empty;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000733C File Offset: 0x0000553C
		protected void UpdateLogWithHeader(string[] headers, IDictionary<string, string> headerContent, int iteration, StateAttribute[] updateAttributePositions)
		{
			if (headers == null)
			{
				throw new ArgumentException("headers can not be null.");
			}
			if (headerContent == null)
			{
				throw new ArgumentException("headerContent can not be null.");
			}
			if (updateAttributePositions == null)
			{
				throw new ArgumentException("updateAttributePositions can not be null.");
			}
			if (headers.Length != updateAttributePositions.Length)
			{
				throw new ArgumentException("headers array size and updateAttributePositions need to be equal.");
			}
			for (int i = 0; i < headers.Length; i++)
			{
				if (headerContent.ContainsKey(headers[i]))
				{
					this.VerboseResultPairs.AddUnique(string.Format("(Attempt #{0}) {1}", iteration, headers[i]), headerContent[headers[i]]);
					switch (updateAttributePositions[i])
					{
					case StateAttribute.StateAttribute3:
						base.Result.StateAttribute3 = headerContent[headers[i]];
						break;
					case StateAttribute.StateAttribute4:
						base.Result.StateAttribute4 = headerContent[headers[i]];
						break;
					case StateAttribute.StateAttribute11:
						base.Result.StateAttribute11 = headerContent[headers[i]];
						break;
					case StateAttribute.StateAttribute12:
						base.Result.StateAttribute12 = headerContent[headers[i]];
						break;
					case StateAttribute.StateAttribute13:
						base.Result.StateAttribute13 = headerContent[headers[i]];
						break;
					case StateAttribute.StateAttribute14:
						base.Result.StateAttribute14 = headerContent[headers[i]];
						break;
					case StateAttribute.StateAttribute15:
						base.Result.StateAttribute15 = headerContent[headers[i]];
						break;
					}
				}
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000074B4 File Offset: 0x000056B4
		private void ClassifyErrorAndUpdateStateAttributes(Exception ewsException, ExchangeServiceBase service)
		{
			Exception ex = ewsException;
			this.SetDefaultValuesForStateAttributes1And2();
			try
			{
				FailureDetails failureDetails = null;
				RequestFailureContext requestFailureContext;
				if (RequestFailureContext.TryCreateFromResponseHeaders(service.HttpResponseHeaders, out requestFailureContext))
				{
					failureDetails = CafeFailureAnalyser.Instance.Analyse(RequestContext.Monitoring, requestFailureContext);
				}
				if (failureDetails != null)
				{
					if (failureDetails.Component == ExchangeComponent.Cafe)
					{
						base.Result.FailureCategory = 4;
						base.Result.StateAttribute1 = EWSAutoDProtocolDependency.Cafe.ToString();
					}
					else if (failureDetails.Component == ExchangeComponent.AD)
					{
						base.Result.FailureCategory = 5;
						base.Result.StateAttribute1 = EWSAutoDProtocolDependency.AD.ToString();
					}
					else if (failureDetails.Component == ExchangeComponent.Gls)
					{
						base.Result.FailureCategory = 6;
						base.Result.StateAttribute1 = EWSAutoDProtocolDependency.Gls.ToString();
					}
					else if (failureDetails.Component == ExchangeComponent.LiveId)
					{
						base.Result.FailureCategory = 7;
						base.Result.StateAttribute1 = EWSAutoDProtocolDependency.LiveId.ToString();
					}
					else if (failureDetails.Component == ExchangeComponent.Monitoring)
					{
						base.Result.FailureCategory = 8;
						base.Result.StateAttribute1 = EWSAutoDProtocolDependency.Monitoring.ToString();
					}
					base.Result.StateAttribute2 = failureDetails.Categorization;
				}
				else if (ewsException.GetType().Equals(typeof(ServiceResponseException)))
				{
					ServiceResponseException ex2 = ewsException as ServiceResponseException;
					if (ex2 != null)
					{
						base.Result.StateAttribute2 = ex2.ErrorCode.ToString();
						if (EWSCommon.KnownEWSErrorCodes.ContainsKey(ex2.ErrorCode))
						{
							base.Result.FailureCategory = (int)EWSCommon.KnownEWSErrorCodes[ex2.ErrorCode];
							base.Result.StateAttribute1 = EWSCommon.KnownEWSErrorCodes[ex2.ErrorCode].ToString();
						}
						else
						{
							base.Result.FailureCategory = 1;
							base.Result.StateAttribute1 = EWSAutoDProtocolDependency.EWS.ToString();
						}
					}
				}
				else if (ewsException.GetType().Equals(typeof(ServiceRequestException)))
				{
					while (ex.InnerException != null)
					{
						ex = ex.InnerException;
					}
					if (ex.GetType().Equals(typeof(WebException)))
					{
						HttpWebResponse httpWebResponse = ((WebException)ex).Response as HttpWebResponse;
						if (httpWebResponse != null)
						{
							int statusCode = (int)httpWebResponse.StatusCode;
							base.Result.StateAttribute2 = httpWebResponse.StatusCode.ToString();
							if (EWSCommon.KnownHTTPErrors.ContainsKey(statusCode))
							{
								base.Result.FailureCategory = (int)EWSCommon.KnownHTTPErrors[statusCode];
								base.Result.StateAttribute1 = EWSCommon.KnownHTTPErrors[statusCode].ToString();
							}
							else
							{
								base.Result.FailureCategory = 1;
								base.Result.StateAttribute1 = EWSAutoDProtocolDependency.EWS.ToString();
							}
						}
					}
					else if (ex.GetType().Equals(typeof(SocketException)))
					{
						base.Result.StateAttribute2 = ((SocketException)ex).SocketErrorCode.ToString();
						base.Result.FailureCategory = 2;
						base.Result.StateAttribute1 = EWSAutoDProtocolDependency.Network.ToString();
					}
				}
			}
			catch (Exception)
			{
				this.SetDefaultValuesForStateAttributes1And2();
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00007844 File Offset: 0x00005A44
		private void SetDefaultValuesForStateAttributes1And2()
		{
			if (string.IsNullOrEmpty(base.Result.StateAttribute2))
			{
				base.Result.StateAttribute2 = "Unknown";
			}
			if (string.IsNullOrEmpty(base.Result.StateAttribute1))
			{
				base.Result.FailureCategory = 1;
				base.Result.StateAttribute1 = EWSAutoDProtocolDependency.EWS.ToString();
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000078A8 File Offset: 0x00005AA8
		private void InitErrorClassificationDictionaries()
		{
			EWSCommon.KnownHTTPErrors = new Dictionary<int, EWSAutoDProtocolDependency>();
			EWSCommon.KnownHTTPErrors[401] = EWSAutoDProtocolDependency.Auth;
			EWSCommon.KnownEWSErrorCodes = new Dictionary<ServiceError, EWSAutoDProtocolDependency>();
			EWSCommon.KnownEWSErrorCodes[263] = EWSAutoDProtocolDependency.Store;
			EWSCommon.KnownEWSErrorCodes[262] = EWSAutoDProtocolDependency.Store;
			EWSCommon.KnownEWSErrorCodes[6] = EWSAutoDProtocolDependency.AD;
			EWSCommon.KnownEWSErrorCodes[7] = EWSAutoDProtocolDependency.AD;
			EWSCommon.KnownEWSErrorCodes[8] = EWSAutoDProtocolDependency.AD;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00007920 File Offset: 0x00005B20
		private void UpdateStatsAndLogs(ExchangeServiceBase service, string soapAction, DateTime startTime, DateTime endTime, string traceMsg, int attemptCount)
		{
			string arg = string.Format("{0} (Attempt #{1})", soapAction, attemptCount);
			if (service.HttpResponseHeaders.ContainsKey("RequestId"))
			{
				this.SetClientStatistics(service.HttpResponseHeaders["RequestId"], soapAction, (endTime - startTime).TotalMilliseconds);
			}
			this.VerboseResultPairs.AddUnique(string.Format("{0} Status", arg), traceMsg);
			this.VerboseResultPairs.AddUnique(string.Format("{0} Latency", arg), (endTime - startTime).TotalMilliseconds.ToString());
			this.VitalResultPairs.AddUnique(string.Format("{0} Latency", arg), (endTime - startTime).TotalMilliseconds.ToString());
			if (service.HttpResponseHeaders != null)
			{
				this.UpdateLogWithHeader(new string[]
				{
					"X-DiagInfo",
					"X-FEServer",
					"X-BEServer",
					"X-TargetBEServer"
				}, service.HttpResponseHeaders, attemptCount, new StateAttribute[]
				{
					StateAttribute.StateAttribute11,
					StateAttribute.StateAttribute12,
					StateAttribute.StateAttribute13,
					StateAttribute.StateAttribute14
				});
				base.Result.StateAttribute5 = this.GetFailureContext(service.HttpResponseHeaders);
			}
			this.LogTrace(traceMsg);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00007A74 File Offset: 0x00005C74
		private bool HidePasswordInLog()
		{
			bool result;
			if (base.Definition.Attributes.ContainsKey("HidePasswordInLog"))
			{
				string value = base.Definition.Attributes["HidePasswordInLog"];
				if (!bool.TryParse(value, out result))
				{
					result = false;
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x02000015 RID: 21
		protected class TraceListener : ITraceListener
		{
			// Token: 0x060000DF RID: 223 RVA: 0x00007ABF File Offset: 0x00005CBF
			public TraceListener(TracingContext tracingContext, Microsoft.Exchange.Diagnostics.Trace exTraceGlobal)
			{
				this.tracingContext = tracingContext;
				this.exTraceGlobal = exTraceGlobal;
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x060000E0 RID: 224 RVA: 0x00007AD5 File Offset: 0x00005CD5
			// (set) Token: 0x060000E1 RID: 225 RVA: 0x00007ADD File Offset: 0x00005CDD
			public string RequestLog { get; internal set; }

			// Token: 0x060000E2 RID: 226 RVA: 0x00007AE8 File Offset: 0x00005CE8
			public void Trace(string traceType, string traceMessage)
			{
				this.RequestLog += string.Format("{0}: {1}\n", traceType, traceMessage);
				WTFDiagnostics.TraceInformation<string, string>(this.exTraceGlobal, this.tracingContext, "Tracetype: {0}\nTraceMessage: {1}\n", traceType, traceMessage, null, "Trace", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Ews\\EWSCommon.cs", 1464);
			}

			// Token: 0x04000097 RID: 151
			private TracingContext tracingContext;

			// Token: 0x04000098 RID: 152
			private Microsoft.Exchange.Diagnostics.Trace exTraceGlobal;
		}
	}
}
