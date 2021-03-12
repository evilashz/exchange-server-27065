using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007C2 RID: 1986
	internal class HttpWebResponseWrapper
	{
		// Token: 0x060028EE RID: 10478 RVA: 0x00057996 File Offset: 0x00055B96
		private HttpWebResponseWrapper()
		{
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x000579C0 File Offset: 0x00055BC0
		public static HttpWebResponseWrapper Create(HttpWebRequestWrapper request, HttpWebResponse response)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			HttpWebResponseWrapper httpWebResponseWrapper = new HttpWebResponseWrapper();
			httpWebResponseWrapper.Request = request;
			httpWebResponseWrapper.StatusCode = response.StatusCode;
			httpWebResponseWrapper.ProtocolVersion = response.ProtocolVersion;
			httpWebResponseWrapper.Headers = response.Headers;
			httpWebResponseWrapper.ReceivedTime = ExDateTime.Now;
			if (response.Headers[HttpResponseHeader.TransferEncoding] == "chunked")
			{
				using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
				{
					string s = streamReader.ReadToEnd();
					httpWebResponseWrapper.responseBody = Encoding.UTF8.GetBytes(s);
					httpWebResponseWrapper.ContentLength = (long)httpWebResponseWrapper.responseBody.Length;
					goto IL_109;
				}
			}
			httpWebResponseWrapper.ContentLength = response.ContentLength;
			if (response.ContentLength > 0L)
			{
				using (Stream responseStream = response.GetResponseStream())
				{
					httpWebResponseWrapper.responseBody = new byte[response.ContentLength];
					int num = 0;
					while ((long)num < response.ContentLength)
					{
						num += responseStream.Read(httpWebResponseWrapper.responseBody, num, (int)response.ContentLength - num);
					}
				}
			}
			IL_109:
			httpWebResponseWrapper.StreamReadFinishedTime = ExDateTime.Now;
			return httpWebResponseWrapper;
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x00057B00 File Offset: 0x00055D00
		internal static HttpWebResponseWrapper Create(HttpWebRequestWrapper request, HttpStatusCode statusCode, Version protocolVersion, WebHeaderCollection headers, string responseBody)
		{
			HttpWebResponseWrapper httpWebResponseWrapper = new HttpWebResponseWrapper();
			httpWebResponseWrapper.Request = request;
			httpWebResponseWrapper.StatusCode = statusCode;
			httpWebResponseWrapper.ProtocolVersion = protocolVersion;
			httpWebResponseWrapper.Headers = headers;
			httpWebResponseWrapper.ReceivedTime = ExDateTime.Now;
			if (responseBody != null)
			{
				httpWebResponseWrapper.body = responseBody;
				httpWebResponseWrapper.ContentLength = (long)responseBody.Length;
			}
			httpWebResponseWrapper.StreamReadFinishedTime = ExDateTime.Now;
			return httpWebResponseWrapper;
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x060028F1 RID: 10481 RVA: 0x00057B60 File Offset: 0x00055D60
		// (set) Token: 0x060028F2 RID: 10482 RVA: 0x00057B68 File Offset: 0x00055D68
		public HttpWebRequestWrapper Request { get; private set; }

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x060028F3 RID: 10483 RVA: 0x00057B71 File Offset: 0x00055D71
		// (set) Token: 0x060028F4 RID: 10484 RVA: 0x00057B79 File Offset: 0x00055D79
		public HttpStatusCode StatusCode { get; private set; }

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x060028F5 RID: 10485 RVA: 0x00057B82 File Offset: 0x00055D82
		// (set) Token: 0x060028F6 RID: 10486 RVA: 0x00057B8A File Offset: 0x00055D8A
		public Version ProtocolVersion { get; private set; }

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x060028F7 RID: 10487 RVA: 0x00057B93 File Offset: 0x00055D93
		// (set) Token: 0x060028F8 RID: 10488 RVA: 0x00057B9B File Offset: 0x00055D9B
		public ExDateTime ReceivedTime { get; private set; }

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x060028F9 RID: 10489 RVA: 0x00057BA4 File Offset: 0x00055DA4
		// (set) Token: 0x060028FA RID: 10490 RVA: 0x00057BAC File Offset: 0x00055DAC
		public ExDateTime StreamReadFinishedTime { get; private set; }

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060028FB RID: 10491 RVA: 0x00057BB8 File Offset: 0x00055DB8
		public TimeSpan TotalLatency
		{
			get
			{
				return this.StreamReadFinishedTime.Subtract(this.Request.SentTime);
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060028FC RID: 10492 RVA: 0x00057BE0 File Offset: 0x00055DE0
		public TimeSpan ResponseLatency
		{
			get
			{
				return this.ReceivedTime.Subtract(this.Request.SentTime);
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060028FD RID: 10493 RVA: 0x00057C06 File Offset: 0x00055E06
		public TimeSpan? CasLatency
		{
			get
			{
				return this.ReadLatencyHeader("X-DiagInfoIisLatency");
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x060028FE RID: 10494 RVA: 0x00057C13 File Offset: 0x00055E13
		public TimeSpan? RpcLatency
		{
			get
			{
				return this.ReadLatencyHeader("X-DiagInfoRpcLatency");
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x060028FF RID: 10495 RVA: 0x00057C20 File Offset: 0x00055E20
		public TimeSpan? LdapLatency
		{
			get
			{
				return this.ReadLatencyHeader(new string[]
				{
					"X-DiagInfoLdapLatency",
					"X-AuthDiagInfoLdapLatency"
				});
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06002900 RID: 10496 RVA: 0x00057C4B File Offset: 0x00055E4B
		public TimeSpan? MservLatency
		{
			get
			{
				return this.ReadLatencyHeader("X-AuthDiagInfoMservLookupLatency");
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06002901 RID: 10497 RVA: 0x00057C58 File Offset: 0x00055E58
		// (set) Token: 0x06002902 RID: 10498 RVA: 0x00057C60 File Offset: 0x00055E60
		public WebHeaderCollection Headers { get; private set; }

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06002903 RID: 10499 RVA: 0x00057C69 File Offset: 0x00055E69
		// (set) Token: 0x06002904 RID: 10500 RVA: 0x00057C71 File Offset: 0x00055E71
		public long ContentLength { get; private set; }

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06002905 RID: 10501 RVA: 0x00057C7C File Offset: 0x00055E7C
		public string Body
		{
			get
			{
				if (this.body == null && this.responseBody != null)
				{
					using (Stream stream = new MemoryStream(this.responseBody))
					{
						using (StreamReader streamReader = new StreamReader(stream))
						{
							this.body = streamReader.ReadToEnd();
						}
					}
				}
				return this.body;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06002906 RID: 10502 RVA: 0x00057CF4 File Offset: 0x00055EF4
		public string RespondingFrontEndServer
		{
			get
			{
				string result = null;
				if (this.Headers["PPServer"] != null || this.Headers["MSNSERVER"] != null)
				{
					string input = this.Headers["PPServer"] ?? this.Headers["MSNSERVER"];
					Regex regex = new Regex("H:\\s*(?<server>[^\\s]*)", RegexOptions.Compiled);
					Match match = regex.Match(input);
					if (match.Success)
					{
						result = match.Result("${server}");
					}
					else
					{
						result = this.Headers["PPServer"];
					}
				}
				else if (this.Headers[this.ExchangeFrontEndServerHeader] != null)
				{
					result = this.Headers[this.ExchangeFrontEndServerHeader];
				}
				else if (this.Headers["X-DiagInfo"] != null)
				{
					result = this.Headers["X-DiagInfo"];
				}
				return result;
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06002907 RID: 10503 RVA: 0x00057DD4 File Offset: 0x00055FD4
		public bool? IsE14CasServer
		{
			get
			{
				if (this.Headers[this.ExchangeFrontEndServerHeader] != null)
				{
					return new bool?(false);
				}
				if (this.Headers["X-DiagInfo"] != null)
				{
					return new bool?(true);
				}
				return null;
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06002908 RID: 10504 RVA: 0x00057E20 File Offset: 0x00056020
		public string ProcessingServer
		{
			get
			{
				if (this.Headers[this.ExchangeBackEndServerHeader] != null)
				{
					return this.Headers[this.ExchangeBackEndServerHeader];
				}
				if (this.Headers[this.ExchangeTargetBackendServerHeader] != null)
				{
					return this.Headers[this.ExchangeTargetBackendServerHeader];
				}
				if (this.Headers[this.ExchangeFrontEndServerHeader] != null)
				{
					return this.Headers[this.ExchangeFrontEndServerHeader];
				}
				return this.RespondingFrontEndServer;
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06002909 RID: 10505 RVA: 0x00057EA2 File Offset: 0x000560A2
		public string MailboxServer
		{
			get
			{
				if (this.Headers[this.ExchangeTargetBackendServerHeader] != null)
				{
					return this.Headers[this.ExchangeTargetBackendServerHeader];
				}
				return this.Headers["X-DiagInfoMailbox"];
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x0600290A RID: 10506 RVA: 0x00057ED9 File Offset: 0x000560D9
		public string DomainController
		{
			get
			{
				return this.Headers["X-DiagInfoDomainController"];
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x0600290B RID: 10507 RVA: 0x00057EEB File Offset: 0x000560EB
		public string ARRServer
		{
			get
			{
				return this.Headers["X-DiagInfoARR"];
			}
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x00057EFD File Offset: 0x000560FD
		public string ToStringNoBody()
		{
			return this.ToString(RequestResponseStringFormatOptions.NoBody);
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x00057F06 File Offset: 0x00056106
		public override string ToString()
		{
			return this.ToString(RequestResponseStringFormatOptions.None);
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x00057F10 File Offset: 0x00056110
		public string ToString(RequestResponseStringFormatOptions formatOptions)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("HTTP/{0} {1} {2}{3}", new object[]
			{
				this.ProtocolVersion,
				(int)this.StatusCode,
				this.StatusCode,
				Environment.NewLine
			});
			foreach (object obj in this.Headers)
			{
				string text = (string)obj;
				string text2 = this.Headers[text];
				if ((formatOptions & RequestResponseStringFormatOptions.TruncateCookies) == RequestResponseStringFormatOptions.TruncateCookies && text.Equals("Set-Cookie", StringComparison.OrdinalIgnoreCase))
				{
					Regex regex = new Regex("=(?<CookieStart>[^,;]{25,25})(?<CookieValue>[^,;]+)", RegexOptions.Compiled);
					text2 = regex.Replace(text2, "=${CookieStart}...");
				}
				stringBuilder.AppendFormat("{0}: {1}{2}", text, text2, Environment.NewLine);
			}
			if ((formatOptions & RequestResponseStringFormatOptions.NoBody) != RequestResponseStringFormatOptions.NoBody && this.responseBody != null && !string.IsNullOrEmpty(this.Body))
			{
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(this.Body);
				stringBuilder.Append(Environment.NewLine);
			}
			stringBuilder.AppendFormat("Response time: {0}s{1}", this.TotalLatency.TotalSeconds, Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x00058070 File Offset: 0x00056270
		private TimeSpan? ReadLatencyHeader(string[] headerNames)
		{
			TimeSpan? timeSpan = null;
			foreach (string headerName in headerNames)
			{
				TimeSpan? timeSpan2 = this.ReadLatencyHeader(headerName);
				if (timeSpan2 != null)
				{
					if (timeSpan == null)
					{
						timeSpan = timeSpan2;
					}
					else
					{
						timeSpan += timeSpan2;
					}
				}
			}
			return timeSpan;
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x000580FC File Offset: 0x000562FC
		private TimeSpan? ReadLatencyHeader(string headerName)
		{
			string text = this.Headers[headerName];
			if (text == null)
			{
				return null;
			}
			int num;
			if (!int.TryParse(text, out num))
			{
				return null;
			}
			return new TimeSpan?(TimeSpan.FromMilliseconds((double)num));
		}

		// Token: 0x04002461 RID: 9313
		private const string ExchangeLegacyServerHeader = "X-DiagInfo";

		// Token: 0x04002462 RID: 9314
		private readonly string ExchangeFrontEndServerHeader = WellKnownHeader.XFEServer;

		// Token: 0x04002463 RID: 9315
		private readonly string ExchangeBackEndServerHeader = WellKnownHeader.XBEServer;

		// Token: 0x04002464 RID: 9316
		private readonly string ExchangeTargetBackendServerHeader = WellKnownHeader.XCalculatedBETarget;

		// Token: 0x04002465 RID: 9317
		private string body;

		// Token: 0x04002466 RID: 9318
		private byte[] responseBody;
	}
}
