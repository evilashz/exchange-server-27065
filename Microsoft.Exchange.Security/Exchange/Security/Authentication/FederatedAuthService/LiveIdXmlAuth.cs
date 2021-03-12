using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.XPath;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Security;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x0200007C RID: 124
	internal class LiveIdXmlAuth : LiveIdSTSBase
	{
		// Token: 0x0600045A RID: 1114 RVA: 0x00023E38 File Offset: 0x00022038
		static LiveIdXmlAuth()
		{
			LiveIdXmlAuth.s_userAgent = string.Format("{0}/{1} {2}", LiveIdSTSBase.SiteName, "15.0.0", Environment.OSVersion.ToString());
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00023F30 File Offset: 0x00022130
		public LiveIdXmlAuth(int traceId, AuthServiceStaticConfig config, NamespaceStats stats, string clientIP, string clientInfo) : base(traceId, LiveIdInstanceType.Consumer, stats)
		{
			this.LogonUri = config.liveidXmlAuth;
			base.ProfilePolicy = config.MSAProfilePolicy;
			this.clientIP = clientIP;
			if (string.IsNullOrEmpty(clientInfo))
			{
				this.clientInfo = config.siteName;
			}
			else
			{
				this.clientInfo = clientInfo;
			}
			this.siteId = config.MSASiteId;
			this.authStatusForResponseDump = config.AuthStatusForResponseDump;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00023F9D File Offset: 0x0002219D
		public override string StsTag
		{
			get
			{
				return "XmlAuth";
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00023FA4 File Offset: 0x000221A4
		public override IAsyncResult StartRequestChain(string userId, byte[] password, AsyncCallback callback, object state)
		{
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "LiveIdXmlAuth.Entering StartRequestChain()");
			this.stopwatch = Stopwatch.StartNew();
			this.traceUserName = userId;
			this.LogonUri = string.Format("{0}{1}id={2}&kpp=2&eip={3}&wp={4}", new object[]
			{
				this.LogonUri,
				(this.LogonUri.IndexOf('?') < 0) ? "?" : "&",
				this.siteId,
				this.clientIP,
				base.ProfilePolicy
			});
			ExTraceGlobals.AuthenticationTracer.TraceDebug<string, string>((long)this.traceId, "Constructing WebRequest uri='{0}' TokenConsumer='{1}'", this.LogonUri, LiveIdSTSBase.SiteName);
			int num = this.ConstructRequestContent(userId, password, this.clientInfo, null, null);
			ExTraceGlobals.AuthenticationTracer.TraceDebug((long)this.traceId, "contentLength = " + num);
			this.request = AuthServiceHelper.CreateHttpWebRequest(this.LogonUri);
			this.request.ContentType = "text/xml";
			this.request.ContentLength = (long)num;
			this.request.UserAgent = LiveIdXmlAuth.s_userAgent;
			if (base.ExtraHeaders != null)
			{
				this.request.Headers.Add(base.ExtraHeaders);
			}
			Interlocked.Increment(ref LiveIdSTSBase.numberOfLiveIdRequests);
			STSBase.counters.NumberOfLiveIdStsRequests.RawValue = (long)LiveIdSTSBase.numberOfLiveIdRequests;
			IAsyncResult result = this.request.BeginGetRequestStream(callback, state);
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Leaving LiveIdXmlAuth.StartRequestChain()");
			return result;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00024130 File Offset: 0x00022330
		public override IAsyncResult ProcessRequest(IAsyncResult asyncResult, AsyncCallback callback, object state)
		{
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Entering LiveIdXmlAuth.ProcessRequest()");
			Stream stream = this.request.EndGetRequestStream(asyncResult);
			base.SSLConnectionLatency = this.stopwatch.ElapsedMilliseconds;
			STSBase.WriteBytes(stream, this.s_requestP1);
			STSBase.WriteBytes(stream, this.s_requestP2);
			STSBase.WriteBytes(stream, LiveIdXmlAuth.s_requestP3);
			if (this.s_requestP4 != null)
			{
				STSBase.WriteBytes(stream, this.s_requestP4);
			}
			STSBase.WriteBytes(stream, LiveIdXmlAuth.s_requestP5);
			STSBase.WriteBytes(stream, this.s_requestP6);
			stream.Close();
			if (ExTraceGlobals.AuthenticationTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					STSBase.WriteBytes(memoryStream, this.s_requestP1);
					string s = string.Format("<SignInName>{0}</SignInName><Password>{1}</Password>", this.traceUserName, "DummyPassword");
					STSBase.WriteBytes(memoryStream, Encoding.UTF8.GetBytes(s));
					STSBase.WriteBytes(memoryStream, LiveIdXmlAuth.s_requestP3);
					if (this.s_requestP4 != null)
					{
						STSBase.WriteBytes(memoryStream, this.s_requestP4);
					}
					STSBase.WriteBytes(memoryStream, LiveIdXmlAuth.s_requestP5);
					STSBase.WriteBytes(memoryStream, this.s_requestP6);
					memoryStream.Flush();
					base.LogRequest(memoryStream.GetBuffer(), this.request, null);
				}
			}
			IAsyncResult result = this.request.BeginGetResponse(callback, state);
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Leaving LiveIdXmlAuth.ProcessRequest()");
			return result;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000242A0 File Offset: 0x000224A0
		public override string ProcessResponse(IAsyncResult asyncResult)
		{
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Entering LiveIdXmlAuth.ProcessResponse()");
			string result = string.Empty;
			bool closeConnectionGroup = false;
			try
			{
				using (WebResponse webResponse = this.request.EndGetResponse(asyncResult))
				{
					this.stopwatch.Stop();
					base.Latency = this.stopwatch.ElapsedMilliseconds;
					STSBase.counters.AverageLiveIdResponseTime.IncrementBy(this.stopwatch.ElapsedMilliseconds);
					STSBase.counters.AverageLiveIdResponseTimeBase.Increment();
					ExTraceGlobals.AuthenticationTracer.TracePerformance<long>((long)this.traceId, "LiveIdXmlAuth responded in {0}ms", this.stopwatch.ElapsedMilliseconds);
					if (webResponse != null && webResponse.Headers != null)
					{
						base.LiveServer = webResponse.Headers.Get("PPServer");
					}
					result = this.ParseResponse(webResponse as HttpWebResponse);
				}
			}
			catch (WebException ex)
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
				if (httpWebResponse == null || httpWebResponse.StatusCode != HttpStatusCode.BadRequest)
				{
					base.ErrorString += ex.ToString();
					string str;
					if (ex.Status == WebExceptionStatus.TrustFailure && AuthService.CertErrorCache.TryGetValue(this.request, out str))
					{
						base.ErrorString += str;
						AuthService.CertErrorCache.Remove(this.request);
					}
					closeConnectionGroup = true;
					throw;
				}
				result = this.ParseResponse(httpWebResponse);
			}
			finally
			{
				bool flag = AuthServiceHelper.CloseConnectionGroupIfNeeded(closeConnectionGroup, this.LogonUri, base.ConnectionGroupName, this.traceId);
				if (flag)
				{
					base.ErrorString += "<ConnectionGroupClosed>";
				}
			}
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Leaving LiveIdXmlAuth.ProcessResponse()");
			return result;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000244A4 File Offset: 0x000226A4
		private string ParseResponse(HttpWebResponse httpResponse)
		{
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Entering LiveIdXmlAuth.ParseResponse()");
			ExTraceGlobals.AuthenticationTracer.Information<string, HttpStatusCode>((long)this.traceId, "LiveIdXmlAuth {0} responded with status {1:d}", this.LogonUri, httpResponse.StatusCode);
			base.ErrorString = string.Empty;
			if (httpResponse == null)
			{
				return null;
			}
			Stream responseStream = httpResponse.GetResponseStream();
			try
			{
				using (StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8))
				{
					this.xmlRawResponse = streamReader.ReadToEnd();
					ExTraceGlobals.AuthenticationTracer.TraceDebug<string>((long)this.traceId, "LiveIdXmlAuth returned response {0}", this.xmlRawResponse);
					this.xmlResponse = new SafeXmlDocument();
					this.xmlResponse.PreserveWhitespace = true;
					try
					{
						this.xmlResponse.LoadXml(this.xmlRawResponse);
					}
					catch (XmlException ex)
					{
						ExTraceGlobals.AuthenticationTracer.TraceError<string, string>((long)this.traceId, "LiveIdXmlAuth has malformed RST response.  Exception {0} XML response {1}", ex.Message, this.xmlRawResponse);
						base.ErrorString = ex.ToString();
						throw;
					}
					XmlNode xmlNode = this.xmlResponse.SelectSingleNode("/LoginResponse/@Success");
					if (xmlNode == null)
					{
						base.ErrorString = string.Format("Server:{0}. The response from LiveIdXmlAuth does not contain SuccessNode.", base.LiveServer);
						throw new XmlException(base.ErrorString);
					}
					if (!xmlNode.Value.Equals("True", StringComparison.OrdinalIgnoreCase))
					{
						XmlNode xmlNode2 = this.xmlResponse.SelectSingleNode("/LoginResponse/Error/@Code");
						if (xmlNode2 == null)
						{
							base.ErrorString = string.Format("Server:{0}. Cannot find error node.", base.LiveServer);
							throw new XmlException(base.ErrorString);
						}
						this.errorCode = xmlNode2.Value.Trim();
						ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.traceId, "LiveIdXmlAuth logon failure response has error", this.errorCode);
						if (this.errorCode.Equals("e5b", StringComparison.OrdinalIgnoreCase) || this.errorCode.Equals("e5a", StringComparison.OrdinalIgnoreCase) || this.errorCode.Equals("e11", StringComparison.OrdinalIgnoreCase))
						{
							base.IsBadCredentials = true;
							Interlocked.Increment(ref this.namespaceStats.BadPassword);
							this.namespaceStats.User = this.traceUserName;
						}
						else if (this.errorCode.Equals("e20a", StringComparison.OrdinalIgnoreCase))
						{
							base.IsExpiredCreds = true;
						}
						base.ErrorString = this.errorCode;
						if (!LiveIdXmlAuth.ignorableErrors.Contains(this.errorCode, StringComparer.OrdinalIgnoreCase))
						{
							STSBase.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_LiveIdServerError, this.traceUserName + this.errorCode, new object[]
							{
								this.LogonUri,
								base.LiveServer,
								this.errorCode,
								0,
								base.ErrorString,
								this.traceUserName
							});
						}
						if (!string.IsNullOrEmpty(this.errorCode) && this.errorCode.Equals(this.authStatusForResponseDump, StringComparison.OrdinalIgnoreCase))
						{
							base.ErrorString = this.xmlRawResponse;
						}
						return null;
					}
					else
					{
						try
						{
							this.compactTicket = this.GetRpsTicket(this.xmlResponse);
							if (string.IsNullOrEmpty(this.compactTicket))
							{
								base.ErrorString = "Missing Compact Token." + this.xmlRawResponse;
								return null;
							}
						}
						catch (XPathException ex2)
						{
							base.ErrorString = string.Format("LiveID STS has malformed RST response. Exception {0} XML response {1}", ex2.ToString(), this.xmlRawResponse);
							ExTraceGlobals.AuthenticationTracer.TraceError((long)this.traceId, base.ErrorString);
							ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Leaving LiveIdXmlAuth.ParseResponse()");
							return null;
						}
					}
				}
			}
			finally
			{
				responseStream.Close();
				responseStream.Dispose();
			}
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Leaving LiveIdXmlAuth.ParseResponse()");
			return this.compactTicket;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000248AC File Offset: 0x00022AAC
		private string GetRpsTicket(XmlDocument response)
		{
			XmlNode xmlNode = response.SelectSingleNode("/LoginResponse/Redirect");
			if (xmlNode == null)
			{
				return null;
			}
			string innerText = xmlNode.InnerText;
			string[] array = innerText.Split(new char[]
			{
				'&'
			});
			string text = null;
			foreach (string text2 in array)
			{
				if (text2.StartsWith("t=", StringComparison.OrdinalIgnoreCase))
				{
					text = text2;
					break;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00024928 File Offset: 0x00022B28
		public override void Abort()
		{
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Entering LiveIdXmlAuth.Abort()");
			if (this.request != null)
			{
				ExTraceGlobals.AuthenticationTracer.Information<string, string>((long)this.traceId, "Aborting http logon request to LiveId XmlAuth '{0}' for '{1}'", this.LogonUri, this.traceUserName);
				this.request.Abort();
			}
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Leaving LiveIdXmlAuth.Abort()");
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00024996 File Offset: 0x00022B96
		public override string LiveToken()
		{
			return this.compactTicket;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0002499E File Offset: 0x00022B9E
		public override bool UserRecoveryPossible()
		{
			return LiveIdXmlAuth.recoverableErrors.Contains(this.errorCode, StringComparer.OrdinalIgnoreCase) || (this.errorCode != null && this.errorCode.StartsWith("-2"));
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000249D4 File Offset: 0x00022BD4
		public override void ProcessResponse(string userName, IAsyncResult asyncResult, out string compactTokenInRps, out RPSProfile rpsProfile, out string errorString)
		{
			compactTokenInRps = null;
			rpsProfile = null;
			errorString = null;
			new StringBuilder(256);
			compactTokenInRps = this.ProcessResponse(asyncResult);
			if (!string.IsNullOrEmpty(compactTokenInRps))
			{
				this.stopwatch = Stopwatch.StartNew();
				try
				{
					bool flag = MSATokenValidationClient.Instance.ParseCompactToken(2, compactTokenInRps, LiveIdSTSBase.SiteName, LiveIdSTSBase.RpsTicketLifetime, out rpsProfile, out errorString);
					if (flag)
					{
						if ((rpsProfile.AuthFlags & 11U) == 11U)
						{
							base.IsUnfamiliarLocation = true;
							rpsProfile = null;
						}
						else
						{
							rpsProfile.HasSignedTOU = RPSCommon.HasUserSignedTOU(rpsProfile.TokenFlags, userName);
						}
					}
				}
				finally
				{
					this.stopwatch.Stop();
					base.RPSParseLatency = this.stopwatch.ElapsedMilliseconds;
					STSBase.counters.AverageRPSCallLatency.IncrementBy(this.stopwatch.ElapsedMilliseconds);
					STSBase.counters.AverageRPSCallLatencyBase.Increment();
				}
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00024AC4 File Offset: 0x00022CC4
		private int ConstructRequestContent(string username, byte[] password, string clientInfo, string deviceId, string deviceSyncKey)
		{
			string empty = string.Empty;
			int num = LiveIdXmlAuth.s_requestP5.Length + LiveIdXmlAuth.s_requestP3.Length;
			if (!string.IsNullOrWhiteSpace(deviceId))
			{
				string arg = string.Empty;
				if (!string.IsNullOrWhiteSpace(deviceSyncKey))
				{
					arg = string.Format("<SyncKey>{0}</SyncKey>", deviceSyncKey);
				}
				string.Format("<Device><ID>{0}</ID>{1}</Device>", deviceId, arg);
				this.s_requestP4 = Encoding.UTF8.GetBytes(deviceId);
				num += this.s_requestP4.Length;
			}
			if (LiveIdXmlAuth.requestTemplateP1Dict.ContainsKey(clientInfo))
			{
				this.s_requestP1 = LiveIdXmlAuth.requestTemplateP1Dict[clientInfo];
			}
			else
			{
				string s = string.Format("<?xml version=\"1.0\"?><LoginRequest><ClientInfo name=\"{0}\" version=\"1.35\" /><User>", clientInfo);
				this.s_requestP1 = Encoding.UTF8.GetBytes(s);
				LiveIdXmlAuth.requestTemplateP1Dict[clientInfo] = this.s_requestP1;
			}
			num += this.s_requestP1.Length;
			string s2 = string.Format("<SignInName>{0}</SignInName><Password>{1}</Password>", username, Encoding.Default.GetString(password));
			this.s_requestP2 = Encoding.UTF8.GetBytes(s2);
			num += this.s_requestP2.Length;
			this.s_requestP6 = Encoding.UTF8.GetBytes(string.Format("<!--{0}-->", "PPPPPPPPPPPPPPPP".Substring(0, Math.Max(0, "PPPPPPPPPPPPPPPP".Length - password.Length))));
			return num + this.s_requestP6.Length;
		}

		// Token: 0x040004A2 RID: 1186
		private const int UnfamiliarLocation = 11;

		// Token: 0x040004A3 RID: 1187
		private const string s_requestTemplateFormatP1 = "<?xml version=\"1.0\"?><LoginRequest><ClientInfo name=\"{0}\" version=\"1.35\" /><User>";

		// Token: 0x040004A4 RID: 1188
		private const string s_requestTemplateFormatP2 = "<SignInName>{0}</SignInName><Password>{1}</Password>";

		// Token: 0x040004A5 RID: 1189
		private const string s_requestTemplateFormatP3 = "<SavePassword>false</SavePassword></User>";

		// Token: 0x040004A6 RID: 1190
		private const string s_deviceNodeTemplateFormat = "<Device><ID>{0}</ID>{1}</Device>";

		// Token: 0x040004A7 RID: 1191
		private const string s_SyncKeyNodeTemplateFormat = "<SyncKey>{0}</SyncKey>";

		// Token: 0x040004A8 RID: 1192
		private const string s_requestTemplateFormatP5 = "</LoginRequest>";

		// Token: 0x040004A9 RID: 1193
		private const string s_signInNodePath = "/LoginRequest/User/SignInName";

		// Token: 0x040004AA RID: 1194
		private const string s_passwordNodePath = "/LoginRequest/User/Password";

		// Token: 0x040004AB RID: 1195
		private const string s_clientInfoNodePath = "/LoginRequest/ClientInfo/@name";

		// Token: 0x040004AC RID: 1196
		private const string s_paddingString = "PPPPPPPPPPPPPPPP";

		// Token: 0x040004AD RID: 1197
		private const string s_sanitizedPassword = "***";

		// Token: 0x040004AE RID: 1198
		private const string s_userAgentFormat = "{0}/{1} {2}";

		// Token: 0x040004AF RID: 1199
		private const string s_contentType = "text/xml";

		// Token: 0x040004B0 RID: 1200
		private const string s_xmlLoginUriFormat = "{0}{1}id={2}&kpp=2&eip={3}&wp={4}";

		// Token: 0x040004B1 RID: 1201
		private const string s_successNodePath = "/LoginResponse/@Success";

		// Token: 0x040004B2 RID: 1202
		private const string s_errorCodeNodePath = "/LoginResponse/Error/@Code";

		// Token: 0x040004B3 RID: 1203
		private const string s_redirectNodePath = "/LoginResponse/Redirect";

		// Token: 0x040004B4 RID: 1204
		private const string s_tCookiePrefix = "t=";

		// Token: 0x040004B5 RID: 1205
		private static ConcurrentDictionary<string, byte[]> requestTemplateP1Dict = new ConcurrentDictionary<string, byte[]>(10, 10, StringComparer.InvariantCulture);

		// Token: 0x040004B6 RID: 1206
		private static readonly string[] recoverableErrors = new string[]
		{
			"e10",
			"e20a",
			"-2147207999",
			"0x800434C1",
			"-2147208051",
			"0x8004348D",
			"-2147208000",
			"0x800434C0"
		};

		// Token: 0x040004B7 RID: 1207
		private static readonly string[] ignorableErrors = new string[]
		{
			"e5a",
			"e5b",
			"e10",
			"e11",
			"e12",
			"e20a"
		};

		// Token: 0x040004B8 RID: 1208
		private HttpWebRequest request;

		// Token: 0x040004B9 RID: 1209
		private Stopwatch stopwatch;

		// Token: 0x040004BA RID: 1210
		private string traceUserName;

		// Token: 0x040004BB RID: 1211
		private static readonly string s_userAgent;

		// Token: 0x040004BC RID: 1212
		private readonly string clientIP;

		// Token: 0x040004BD RID: 1213
		private readonly string clientInfo;

		// Token: 0x040004BE RID: 1214
		private readonly int siteId;

		// Token: 0x040004BF RID: 1215
		private string xmlRawResponse;

		// Token: 0x040004C0 RID: 1216
		private SafeXmlDocument xmlResponse;

		// Token: 0x040004C1 RID: 1217
		private readonly string authStatusForResponseDump;

		// Token: 0x040004C2 RID: 1218
		private string errorCode;

		// Token: 0x040004C3 RID: 1219
		private string compactTicket;

		// Token: 0x040004C4 RID: 1220
		private byte[] s_requestP1;

		// Token: 0x040004C5 RID: 1221
		private byte[] s_requestP2;

		// Token: 0x040004C6 RID: 1222
		private static readonly byte[] s_requestP3 = Encoding.UTF8.GetBytes("<SavePassword>false</SavePassword></User>");

		// Token: 0x040004C7 RID: 1223
		private byte[] s_requestP4;

		// Token: 0x040004C8 RID: 1224
		private static readonly byte[] s_requestP5 = Encoding.UTF8.GetBytes("</LoginRequest>");

		// Token: 0x040004C9 RID: 1225
		private byte[] s_requestP6;

		// Token: 0x0200007D RID: 125
		private static class XmlAuthErrors
		{
			// Token: 0x040004CA RID: 1226
			public const string EmptyMemberNameOrPassword = "e1";

			// Token: 0x040004CB RID: 1227
			public const string EmptyMemberName = "e2";

			// Token: 0x040004CC RID: 1228
			public const string EmptyPassword = "e3";

			// Token: 0x040004CD RID: 1229
			public const string EmptyDomainName = "e4";

			// Token: 0x040004CE RID: 1230
			public const string ChildAccount = "e6";

			// Token: 0x040004CF RID: 1231
			public const string RequestingServerNotAuthorized = "e8";

			// Token: 0x040004D0 RID: 1232
			public const string AccountLocked = "e10";

			// Token: 0x040004D1 RID: 1233
			public const string TooLongMemberNameOrPassword = "e11";

			// Token: 0x040004D2 RID: 1234
			public const string AlreadySignedInUser = "e12";

			// Token: 0x040004D3 RID: 1235
			public const string WrongPassword = "e5a";

			// Token: 0x040004D4 RID: 1236
			public const string WrongMemberName = "e5b";

			// Token: 0x040004D5 RID: 1237
			public const string UnknownError = "e5d";

			// Token: 0x040004D6 RID: 1238
			public const string WrongPostUrl = "e13a";

			// Token: 0x040004D7 RID: 1239
			public const string ExpiredPassword = "e20a";

			// Token: 0x040004D8 RID: 1240
			public const string WrongUserDomain = "80041034";

			// Token: 0x040004D9 RID: 1241
			public const string CompromisedUser1 = "-2147207999";

			// Token: 0x040004DA RID: 1242
			public const string CompromisedUser2 = "0x800434C1";

			// Token: 0x040004DB RID: 1243
			public const string EASIAccount1 = "-2147208051";

			// Token: 0x040004DC RID: 1244
			public const string EASIAccount2 = "0x8004348D";

			// Token: 0x040004DD RID: 1245
			public const string ServiceAbuseMode1 = "-2147208000";

			// Token: 0x040004DE RID: 1246
			public const string ServiceAbuseMode2 = "0x800434C0";
		}
	}
}
