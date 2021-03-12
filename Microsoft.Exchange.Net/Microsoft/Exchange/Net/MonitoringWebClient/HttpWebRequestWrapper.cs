using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007C0 RID: 1984
	internal class HttpWebRequestWrapper
	{
		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x060028CB RID: 10443 RVA: 0x0005742D File Offset: 0x0005562D
		// (set) Token: 0x060028CC RID: 10444 RVA: 0x00057435 File Offset: 0x00055635
		public Uri RequestUri { get; private set; }

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x060028CD RID: 10445 RVA: 0x0005743E File Offset: 0x0005563E
		// (set) Token: 0x060028CE RID: 10446 RVA: 0x00057446 File Offset: 0x00055646
		public TestId StepId { get; private set; }

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x060028CF RID: 10447 RVA: 0x0005744F File Offset: 0x0005564F
		// (set) Token: 0x060028D0 RID: 10448 RVA: 0x00057457 File Offset: 0x00055657
		public Uri RequestIpAddressUri { get; internal set; }

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x060028D1 RID: 10449 RVA: 0x00057460 File Offset: 0x00055660
		// (set) Token: 0x060028D2 RID: 10450 RVA: 0x00057468 File Offset: 0x00055668
		public string Method { get; private set; }

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x060028D3 RID: 10451 RVA: 0x00057471 File Offset: 0x00055671
		// (set) Token: 0x060028D4 RID: 10452 RVA: 0x00057479 File Offset: 0x00055679
		public WebHeaderCollection Headers { get; private set; }

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x060028D5 RID: 10453 RVA: 0x00057482 File Offset: 0x00055682
		// (set) Token: 0x060028D6 RID: 10454 RVA: 0x0005748A File Offset: 0x0005568A
		public List<DynamicHeader> DynamicHeaders { get; private set; }

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060028D7 RID: 10455 RVA: 0x00057493 File Offset: 0x00055693
		// (set) Token: 0x060028D8 RID: 10456 RVA: 0x0005749B File Offset: 0x0005569B
		public RequestBody RequestBody { get; set; }

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x060028D9 RID: 10457 RVA: 0x000574A4 File Offset: 0x000556A4
		// (set) Token: 0x060028DA RID: 10458 RVA: 0x000574AC File Offset: 0x000556AC
		public string ConnectionGroupName { get; set; }

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x060028DB RID: 10459 RVA: 0x000574B5 File Offset: 0x000556B5
		// (set) Token: 0x060028DC RID: 10460 RVA: 0x000574BD File Offset: 0x000556BD
		public Version ProtocolVersion
		{
			get
			{
				return this.protocolVersion;
			}
			private set
			{
				this.protocolVersion = value;
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x060028DD RID: 10461 RVA: 0x000574C6 File Offset: 0x000556C6
		// (set) Token: 0x060028DE RID: 10462 RVA: 0x000574CE File Offset: 0x000556CE
		public ExDateTime SentTime { get; set; }

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x060028DF RID: 10463 RVA: 0x000574D7 File Offset: 0x000556D7
		// (set) Token: 0x060028E0 RID: 10464 RVA: 0x000574DF File Offset: 0x000556DF
		public TimeSpan DnsLatency { get; set; }

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x060028E1 RID: 10465 RVA: 0x000574E8 File Offset: 0x000556E8
		// (set) Token: 0x060028E2 RID: 10466 RVA: 0x000574F0 File Offset: 0x000556F0
		public string TargetVipName { get; set; }

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060028E3 RID: 10467 RVA: 0x000574F9 File Offset: 0x000556F9
		// (set) Token: 0x060028E4 RID: 10468 RVA: 0x00057501 File Offset: 0x00055701
		public string TargetVipForestName { get; set; }

		// Token: 0x060028E5 RID: 10469 RVA: 0x0005750C File Offset: 0x0005570C
		public static HttpWebRequestWrapper CreateRequest(TestId stepId, Uri requestUri, string method, RequestBody body, ExCookieContainer cookieContainer, Dictionary<string, string> persistentHeaders, string userAgent, List<DynamicHeader> dynamicHeaders = null)
		{
			HttpWebRequestWrapper httpWebRequestWrapper = new HttpWebRequestWrapper();
			httpWebRequestWrapper.StepId = stepId;
			httpWebRequestWrapper.RequestUri = requestUri;
			httpWebRequestWrapper.Method = method;
			httpWebRequestWrapper.Headers = new WebHeaderCollection();
			httpWebRequestWrapper.DynamicHeaders = dynamicHeaders;
			httpWebRequestWrapper.Headers.Add("User-Agent", userAgent);
			httpWebRequestWrapper.Headers.Add("Accept", "*/*");
			httpWebRequestWrapper.Headers.Add("Cache-Control", "no-cache");
			if (cookieContainer != null)
			{
				string cookieHeader = cookieContainer.GetCookieHeader(new Uri(requestUri.GetLeftPart(UriPartial.Authority) + requestUri.AbsolutePath));
				if (!string.IsNullOrEmpty(cookieHeader))
				{
					httpWebRequestWrapper.Headers.Add("Cookie", cookieHeader);
				}
			}
			if (body != null)
			{
				httpWebRequestWrapper.RequestBody = body;
			}
			foreach (string text in persistentHeaders.Keys)
			{
				httpWebRequestWrapper.Headers.Add(text, persistentHeaders[text]);
			}
			if (httpWebRequestWrapper.DynamicHeaders != null)
			{
				foreach (DynamicHeader dynamicHeader in httpWebRequestWrapper.DynamicHeaders)
				{
					dynamicHeader.UpdateHeaders(httpWebRequestWrapper);
				}
			}
			return httpWebRequestWrapper;
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x0005766C File Offset: 0x0005586C
		public HttpWebRequest CreateHttpWebRequest()
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.RequestIpAddressUri);
			httpWebRequest.Host = this.RequestUri.Host;
			httpWebRequest.Method = this.Method;
			string[] allKeys = this.Headers.AllKeys;
			int i = 0;
			while (i < allKeys.Length)
			{
				string text = allKeys[i];
				string text2 = this.Headers[text];
				string a;
				if ((a = text) == null)
				{
					goto IL_A0;
				}
				if (!(a == "User-Agent"))
				{
					if (!(a == "Accept"))
					{
						if (!(a == "Content-Type"))
						{
							goto IL_A0;
						}
						httpWebRequest.ContentType = text2;
					}
					else
					{
						httpWebRequest.Accept = text2;
					}
				}
				else
				{
					httpWebRequest.UserAgent = text2;
				}
				IL_AD:
				i++;
				continue;
				IL_A0:
				httpWebRequest.Headers.Add(text, text2);
				goto IL_AD;
			}
			if (this.DynamicHeaders != null)
			{
				foreach (DynamicHeader dynamicHeader in this.DynamicHeaders)
				{
					dynamicHeader.UpdateHeaders(httpWebRequest);
				}
			}
			if (this.Method == "POST" && this.RequestBody == null)
			{
				httpWebRequest.ContentLength = 0L;
			}
			return httpWebRequest;
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x000577A8 File Offset: 0x000559A8
		public string ToStringNoBody()
		{
			return this.ToString(RequestResponseStringFormatOptions.NoBody);
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x000577B1 File Offset: 0x000559B1
		public override string ToString()
		{
			return this.ToString(RequestResponseStringFormatOptions.None);
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000577BC File Offset: 0x000559BC
		public string ToString(RequestResponseStringFormatOptions formatOptions)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0} {1} HTTP/{2}{3}", new object[]
			{
				this.Method,
				this.RequestUri,
				this.ProtocolVersion,
				Environment.NewLine
			});
			foreach (object obj in this.Headers)
			{
				string text = (string)obj;
				string text2 = this.Headers[text];
				if ((formatOptions & RequestResponseStringFormatOptions.TruncateCookies) == RequestResponseStringFormatOptions.TruncateCookies && text.Equals("Cookie", StringComparison.OrdinalIgnoreCase))
				{
					Regex regex = new Regex("=(?<CookieStart>[^,;]{25,25})(?<CookieValue>[^,;]+)", RegexOptions.Compiled);
					text2 = regex.Replace(text2, "=${CookieStart}...");
				}
				stringBuilder.AppendFormat("{0}: {1}{2}", text, text2, Environment.NewLine);
			}
			if ((formatOptions & RequestResponseStringFormatOptions.NoBody) == RequestResponseStringFormatOptions.NoBody && this.RequestBody != null)
			{
				stringBuilder.Append(Environment.NewLine + this.RequestBody.ToString() + Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002450 RID: 9296
		private const string DefaultAcceptHeader = "*/*";

		// Token: 0x04002451 RID: 9297
		private const string DefaultCacheHeader = "no-cache";

		// Token: 0x04002452 RID: 9298
		private Version protocolVersion = new Version("1.1");
	}
}
