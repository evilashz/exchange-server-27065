using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Passport.RPS;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x02000077 RID: 119
	internal abstract class LiveIdSTSBase : STSBase
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x000215BC File Offset: 0x0001F7BC
		static LiveIdSTSBase()
		{
			LiveIdSTSBase.usAsciiXmlWriterSettings = new XmlWriterSettings();
			LiveIdSTSBase.usAsciiXmlWriterSettings.Encoding = Encoding.ASCII;
			LiveIdSTSBase.usAsciiXmlWriterSettings.OmitXmlDeclaration = true;
			LiveIdSTSBase.usAsciiXmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
			LiveIdSTSBase.RpsTicketLifetime = 3600;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00021621 File Offset: 0x0001F821
		protected LiveIdSTSBase()
		{
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00021629 File Offset: 0x0001F829
		public LiveIdSTSBase(int traceId, LiveIdInstanceType instance, NamespaceStats stats) : base(traceId, instance, stats)
		{
			this.TokenConsumer = LiveIdSTSBase.SiteName;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0002163F File Offset: 0x0001F83F
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x00021646 File Offset: 0x0001F846
		public static string SiteName { get; internal set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0002164E File Offset: 0x0001F84E
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x00021655 File Offset: 0x0001F855
		public static int RpsTicketLifetime { get; internal set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0002165D File Offset: 0x0001F85D
		public string ConnectionGroupName
		{
			get
			{
				if (string.IsNullOrEmpty(this.connectionGroupName))
				{
					this.connectionGroupName = AuthServiceHelper.GetConnectionGroup(this.traceId);
				}
				return this.connectionGroupName;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00021683 File Offset: 0x0001F883
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x0002168B File Offset: 0x0001F88B
		public bool EnableRemoteRPS { get; internal set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00021694 File Offset: 0x0001F894
		protected static RPS RpsSession
		{
			get
			{
				if (LiveIdSTSBase.privateRpsSession == null)
				{
					lock (LiveIdSTSBase.lockRoot)
					{
						if (LiveIdSTSBase.privateRpsSession == null)
						{
							try
							{
								RPS rps = new RPS();
								rps.Initialize(null);
								LiveIdSTSBase.privateRpsSession = rps;
								ExTraceGlobals.AuthenticationTracer.TraceDebug(0L, "RPS Session initialized successfully");
							}
							catch (COMException ex)
							{
								ExTraceGlobals.AuthenticationTracer.TraceError<int>(0L, "RPS initialization failed with error {0}", ex.ErrorCode);
								STSBase.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_AuthServiceFailedToInitRPS, null, new object[]
								{
									ex.ErrorCode
								});
								throw;
							}
						}
					}
				}
				return LiveIdSTSBase.privateRpsSession;
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0002175C File Offset: 0x0001F95C
		public static bool ParseCompactTicket(string token, int traceId, out RPSProfile rpsProfile, out string errorString)
		{
			if (string.IsNullOrEmpty(token))
			{
				throw new ArgumentException("token");
			}
			rpsProfile = null;
			errorString = null;
			try
			{
				using (RPSTicket rpsticket = new RPSCompactTicket(LiveIdSTSBase.RpsSession))
				{
					rpsticket.ProcessToken(LiveIdSTSBase.SiteName, token);
					using (RPSPropBag rpspropBag = new RPSPropBag(LiveIdSTSBase.RpsSession))
					{
						if (!rpsticket.Validate(rpspropBag))
						{
							int num = (int)rpspropBag["reasonhr"];
							RPSErrorCategory rpserrorCategory = RPSErrorHandler.CategorizeRPSError(num);
							errorString = string.Format("Live token failed Validate() with {0}: {1} error=0x{2:x}.", rpserrorCategory, Enum.GetName(typeof(RPSErrorCode), num) ?? string.Empty, num);
							return false;
						}
					}
					rpsProfile = RPSCommon.ParseRPSTicket(rpsticket, LiveIdSTSBase.RpsTicketLifetime, traceId, true, out errorString, false);
				}
			}
			catch (COMException ex)
			{
				errorString = string.Format("Error parsing compact token {0} {1}", ex.ErrorCode, ex.ToString());
				return false;
			}
			return rpsProfile != null;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00021884 File Offset: 0x0001FA84
		public static string UsAsciiEncodedXml(XmlNode xml)
		{
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, LiveIdSTSBase.usAsciiXmlWriterSettings))
				{
					xml.WriteTo(xmlWriter);
					xmlWriter.Close();
					@string = Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			return @string;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000218F4 File Offset: 0x0001FAF4
		public static bool IsPossibleAppPassword(byte[] password)
		{
			if (password == null || password.Length != 16)
			{
				return false;
			}
			foreach (byte b in password)
			{
				if (b < 97 || b > 122)
				{
					return false;
				}
				if (b == 97 || b == 101 || b == 105 || b == 111 || b == 117)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000427 RID: 1063
		public abstract IAsyncResult StartRequestChain(string userId, byte[] token, AsyncCallback callback, object state);

		// Token: 0x06000428 RID: 1064
		public abstract IAsyncResult ProcessRequest(IAsyncResult asyncResult, AsyncCallback callback, object state);

		// Token: 0x06000429 RID: 1065
		public abstract string ProcessResponse(IAsyncResult asyncResult);

		// Token: 0x0600042A RID: 1066 RVA: 0x00021948 File Offset: 0x0001FB48
		public virtual void ProcessResponse(string userName, IAsyncResult asyncResult, out string compactTokenInRps, out RPSProfile rpsProfile, out string errorString)
		{
			errorString = null;
			compactTokenInRps = null;
			rpsProfile = null;
			string text = this.ProcessResponse(asyncResult);
			if (!string.IsNullOrEmpty(text))
			{
				compactTokenInRps = this.LiveToken();
			}
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(compactTokenInRps))
			{
				return;
			}
			if (base.Instance == LiveIdInstanceType.Business)
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				try
				{
					if (!this.EnableRemoteRPS)
					{
						if (!LiveIdSTSBase.ParseCompactTicket(compactTokenInRps, this.GetHashCode(), out rpsProfile, out errorString))
						{
							rpsProfile = null;
						}
					}
					else if (!MSATokenValidationClient.Instance.ParseCompactToken(2, compactTokenInRps, LiveIdSTSBase.SiteName, LiveIdSTSBase.RpsTicketLifetime, out rpsProfile, out errorString))
					{
						rpsProfile = null;
					}
					return;
				}
				finally
				{
					stopwatch.Stop();
					base.RPSParseLatency = stopwatch.ElapsedMilliseconds;
					STSBase.counters.AverageRPSCallLatency.IncrementBy(stopwatch.ElapsedMilliseconds);
					STSBase.counters.AverageRPSCallLatencyBase.Increment();
				}
			}
			rpsProfile = new RPSProfile
			{
				HexPuid = text
			};
		}

		// Token: 0x0600042B RID: 1067
		public abstract void Abort();

		// Token: 0x0600042C RID: 1068
		public abstract string LiveToken();

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00021A3C File Offset: 0x0001FC3C
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x00021A44 File Offset: 0x0001FC44
		public string LiveServer { get; protected set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00021A4D File Offset: 0x0001FC4D
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x00021A55 File Offset: 0x0001FC55
		public virtual string LogonUri { get; set; }

		// Token: 0x06000431 RID: 1073 RVA: 0x00021A5E File Offset: 0x0001FC5E
		public virtual bool UserRecoveryPossible()
		{
			return false;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00021A61 File Offset: 0x0001FC61
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x00021A69 File Offset: 0x0001FC69
		public string TokenConsumer
		{
			get
			{
				return this.tokenConsumer;
			}
			set
			{
				this.tokenConsumer = value;
				this.tokenConsumerBytes = Encoding.UTF8.GetBytes(value);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00021A84 File Offset: 0x0001FC84
		public string TokenIssuerUri
		{
			get
			{
				string text = null;
				FederationTrust federationTrust;
				if (base.Instance == LiveIdInstanceType.Consumer)
				{
					federationTrust = FederationTrustCache.GetFederationTrust("WindowsLiveID");
				}
				else
				{
					federationTrust = FederationTrustCache.GetFederationTrust("MicrosoftOnline");
				}
				if (federationTrust != null && federationTrust.TokenIssuerUri != null)
				{
					text = federationTrust.TokenIssuerUri.ToString();
					ExTraceGlobals.AuthenticationTracer.TraceDebug<string>(0L, "FederationTrust object has issuerUri: {0}", text);
				}
				if (string.IsNullOrEmpty(text))
				{
					if (base.Instance == LiveIdInstanceType.Consumer)
					{
						if (!string.IsNullOrEmpty(this.LogonUri) && this.LogonUri.IndexOf("-int.com", StringComparison.OrdinalIgnoreCase) > 0)
						{
							text = "uri:WindowsLiveIDINT";
						}
						else
						{
							text = "uri:WindowsLiveID";
						}
					}
					else if (!string.IsNullOrEmpty(this.LogonUri) && this.LogonUri.IndexOf("-int.com", StringComparison.OrdinalIgnoreCase) > 0)
					{
						text = "urn:federation:MicrosoftOnline";
					}
					else
					{
						text = "urn:federation:MicrosoftOnlineINT";
					}
					ExTraceGlobals.AuthenticationTracer.TraceWarning<string>(0L, "Could not retrieve issuerUri from FederationTrust object, returning best guess: {0}", text);
				}
				return text;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00021B68 File Offset: 0x0001FD68
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x00021C80 File Offset: 0x0001FE80
		public string ProfilePolicy
		{
			get
			{
				if (string.IsNullOrEmpty(this.privateProfilePolicy))
				{
					RPS rpsSession = LiveIdSTSBase.RpsSession;
					lock (LiveIdSTSBase.lockRoot)
					{
						if (string.IsNullOrEmpty(this.privateProfilePolicy))
						{
							try
							{
								using (RPSServerConfig rpsserverConfig = new RPSServerConfig(rpsSession))
								{
									RPSPropBag rpspropBag = (RPSPropBag)rpsserverConfig["sites"];
									RPSPropBag rpspropBag2 = (RPSPropBag)rpspropBag[LiveIdSTSBase.SiteName];
									this.privateProfilePolicy = (string)rpspropBag2["authpolicy"];
								}
							}
							catch (COMException ex)
							{
								ExTraceGlobals.AuthenticationTracer.TraceError<int>(0L, "RPSServerConfig lookup of authpolicy failed with error {0}", ex.ErrorCode);
								STSBase.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_AuthServiceFailedToInitRPS, null, new object[]
								{
									ex.ErrorCode
								});
								throw;
							}
						}
					}
				}
				return this.privateProfilePolicy;
			}
			internal set
			{
				this.privateProfilePolicy = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00021C89 File Offset: 0x0001FE89
		protected byte[] ProfilePolicyBytes
		{
			get
			{
				if (this.privateProfilePolicyBytes == null)
				{
					this.privateProfilePolicyBytes = Encoding.UTF8.GetBytes(this.ProfilePolicy);
				}
				return this.privateProfilePolicyBytes;
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00021CB0 File Offset: 0x0001FEB0
		protected void LogRequest(byte[] requestBody, HttpWebRequest request, string redactedCreds = null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Request URI: {0} {1}", request.Method, request.RequestUri);
			foreach (string text in request.Headers.AllKeys)
			{
				stringBuilder.AppendLine();
				if (redactedCreds != null && string.Equals(text, "Authorization", StringComparison.OrdinalIgnoreCase))
				{
					stringBuilder.AppendFormat("{0}: {1}", text, redactedCreds);
				}
				else
				{
					stringBuilder.AppendFormat("{0}: {1}", text, request.Headers[text]);
				}
			}
			stringBuilder.AppendLine();
			stringBuilder.Append(Encoding.UTF8.GetString(requestBody));
			string arg = stringBuilder.ToString();
			ExTraceGlobals.AuthenticationTracer.TraceDebug<string>((long)this.traceId, "LiveID STS Sending request {0}", arg);
		}

		// Token: 0x04000461 RID: 1121
		private const int bit30LiveTOU = 536870912;

		// Token: 0x04000462 RID: 1122
		private const int bit15MsnTOU = 16384;

		// Token: 0x04000463 RID: 1123
		private const int bit6LimitedConsent = 32;

		// Token: 0x04000464 RID: 1124
		private const int bit7FullConsent = 64;

		// Token: 0x04000465 RID: 1125
		private const int bit8IsChild = 128;

		// Token: 0x04000466 RID: 1126
		private const int statusMask = 224;

		// Token: 0x04000467 RID: 1127
		protected static int numberOfLiveIdRequests;

		// Token: 0x04000468 RID: 1128
		protected static int numberOfMsoIdRequests;

		// Token: 0x04000469 RID: 1129
		private string connectionGroupName;

		// Token: 0x0400046A RID: 1130
		private static object lockRoot = new object();

		// Token: 0x0400046B RID: 1131
		private static RPS privateRpsSession;

		// Token: 0x0400046C RID: 1132
		private static readonly XmlWriterSettings usAsciiXmlWriterSettings;

		// Token: 0x0400046D RID: 1133
		private static DateTime refTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x0400046E RID: 1134
		private string tokenConsumer;

		// Token: 0x0400046F RID: 1135
		protected byte[] tokenConsumerBytes;

		// Token: 0x04000470 RID: 1136
		private string privateProfilePolicy;

		// Token: 0x04000471 RID: 1137
		private byte[] privateProfilePolicyBytes;
	}
}
