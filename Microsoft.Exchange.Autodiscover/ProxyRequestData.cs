using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
using Microsoft.Exchange.Autodiscover.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.Autodiscover
{
	// Token: 0x02000019 RID: 25
	internal sealed class ProxyRequestData
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00005C98 File Offset: 0x00003E98
		static ProxyRequestData()
		{
			string text = ConfigurationManager.AppSettings[ProxyRequestData.proxyRequestTimeOutInMilliSecondsKey];
			if (string.IsNullOrEmpty(text) || !int.TryParse(text, out ProxyRequestData.proxyRequestTimeOutInMilliSeconds))
			{
				ProxyRequestData.proxyRequestTimeOutInMilliSeconds = (int)TimeSpan.FromMinutes(1.0).TotalMilliseconds;
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005CF8 File Offset: 0x00003EF8
		public ProxyRequestData(HttpRequest request, HttpResponse response, ref Stream bodyStream)
		{
			this.originalUri = request.Url;
			this.headers = request.Headers;
			this.userAgent = request.Headers.Get("UserAgent");
			this.authorization = request.Headers.Get("Authorization");
			this.httpMethod = request.HttpMethod;
			this.contentType = request.ContentType;
			this.originalClientIP = request.Headers.Get("X-Autodiscover-OriginalClientIP");
			if (string.IsNullOrEmpty(this.originalClientIP))
			{
				this.originalClientIP = request.UserHostAddress;
			}
			this.forwardForValue = this.headers.Get(WellKnownHeader.XForwardedFor);
			this.requestBody = this.GetRequestBody(bodyStream);
			bodyStream = null;
			this.Response = response;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00005DC4 File Offset: 0x00003FC4
		public Stream RequestStream
		{
			get
			{
				return new MemoryStream(this.requestBody);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00005DD1 File Offset: 0x00003FD1
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00005DD9 File Offset: 0x00003FD9
		public string AutodiscoverProxyHeader { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005DE2 File Offset: 0x00003FE2
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00005DEA File Offset: 0x00003FEA
		public HttpResponse Response { get; private set; }

		// Token: 0x060000CD RID: 205 RVA: 0x00005DF3 File Offset: 0x00003FF3
		public static bool IsIncomingProxyRequest(HttpRequest request)
		{
			return request.Headers.Get("X-Autodiscover-Proxy") != null;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005E0C File Offset: 0x0000400C
		public HttpWebRequest CloneRequest(string redirectHost)
		{
			ExTraceGlobals.AuthenticationTracer.TraceDebug<Uri, string>(0L, "ProxyRequestData::CloneRequest. Entry. originalRequest = {0}, redirectHost = {1}.", this.originalUri, redirectHost);
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new UriBuilder(this.originalUri)
			{
				Host = redirectHost
			}.Uri);
			httpWebRequest.Method = this.httpMethod;
			httpWebRequest.Headers["Authorization"] = this.authorization;
			httpWebRequest.ContentType = this.contentType;
			httpWebRequest.UserAgent = this.userAgent;
			httpWebRequest.Timeout = ProxyRequestData.proxyRequestTimeOutInMilliSeconds;
			httpWebRequest.Proxy = null;
			httpWebRequest.AllowAutoRedirect = false;
			foreach (object obj in this.headers)
			{
				string text = (string)obj;
				if (text.StartsWith("X-MS-", StringComparison.OrdinalIgnoreCase))
				{
					httpWebRequest.Headers[text] = this.headers[text];
					RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendGenericInfo(text, this.headers[text]);
				}
			}
			if (!string.IsNullOrEmpty(this.forwardForValue))
			{
				httpWebRequest.Headers[WellKnownHeader.XForwardedFor] = this.forwardForValue;
				RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendGenericInfo(WellKnownHeader.XForwardedFor, this.forwardForValue);
			}
			if (FaultInjection.TraceTest<bool>((FaultInjection.LIDs)2804297021U))
			{
				this.AppendProxyHeader(httpWebRequest, redirectHost);
			}
			this.AppendProxyHeader(httpWebRequest, redirectHost);
			using (Stream requestStream = httpWebRequest.GetRequestStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(requestStream))
				{
					binaryWriter.Write(this.requestBody);
					binaryWriter.Flush();
				}
				ExTraceGlobals.AuthenticationTracer.TraceDebug<int>(0L, "ProxyRequestData::CloneRequest. Exit. RequestBody = {0}.", this.requestBody.Length);
			}
			return httpWebRequest;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005FF4 File Offset: 0x000041F4
		public bool IsOriginalRequestProxyRequest()
		{
			return this.headers.Get("X-Autodiscover-Proxy") != null;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000600C File Offset: 0x0000420C
		public override string ToString()
		{
			return string.Format("[{0}\t{1}\t{2}]", this.originalUri, this.originalClientIP, this.headers.Get("X-Autodiscover-Proxy"));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00006034 File Offset: 0x00004234
		private static void StreamCopy(Stream source, Stream destination)
		{
			byte[] array = new byte[1024];
			for (int i = source.Read(array, 0, array.Length); i > 0; i = source.Read(array, 0, array.Length))
			{
				destination.Write(array, 0, i);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00006074 File Offset: 0x00004274
		private byte[] GetRequestBody(Stream bodyStream)
		{
			byte[] result;
			using (BinaryReader binaryReader = new BinaryReader(bodyStream))
			{
				result = binaryReader.ReadBytes((int)bodyStream.Length);
			}
			return result;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000060B4 File Offset: 0x000042B4
		private void AppendProxyHeader(HttpWebRequest request, string redirectServer)
		{
			string text = request.Headers.Get("X-Autodiscover-Proxy") ?? string.Empty;
			if (text.IndexOf(redirectServer + ";", 0, StringComparison.OrdinalIgnoreCase) >= 0)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<string, string>(0L, "ProxyRequestData::AppendProxyHeader. The redirect server has been visited before. We are now in a loop. header = {0}, redirectServer = {1}", text, redirectServer);
				throw new ProxyLoopException(redirectServer);
			}
			this.AutodiscoverProxyHeader = string.Format("{0}{1};", text, redirectServer);
			request.Headers["X-Autodiscover-Proxy"] = this.AutodiscoverProxyHeader;
		}

		// Token: 0x04000103 RID: 259
		internal const string XAutodiscoverProxyHeader = "X-Autodiscover-Proxy";

		// Token: 0x04000104 RID: 260
		private const string XAutodiscoverOriginalClientIPHeader = "X-Autodiscover-OriginalClientIP";

		// Token: 0x04000105 RID: 261
		private const string XMSHeadersPrefix = "X-MS-";

		// Token: 0x04000106 RID: 262
		private static string proxyRequestTimeOutInMilliSecondsKey = "ProxyRequestTimeOutInMilliSeconds";

		// Token: 0x04000107 RID: 263
		private static int proxyRequestTimeOutInMilliSeconds = -1;

		// Token: 0x04000108 RID: 264
		private readonly Uri originalUri;

		// Token: 0x04000109 RID: 265
		private readonly NameValueCollection headers;

		// Token: 0x0400010A RID: 266
		private readonly string userAgent;

		// Token: 0x0400010B RID: 267
		private readonly string authorization;

		// Token: 0x0400010C RID: 268
		private readonly byte[] requestBody;

		// Token: 0x0400010D RID: 269
		private readonly string httpMethod;

		// Token: 0x0400010E RID: 270
		private readonly string contentType;

		// Token: 0x0400010F RID: 271
		private readonly string originalClientIP;

		// Token: 0x04000110 RID: 272
		private readonly string forwardForValue;
	}
}
