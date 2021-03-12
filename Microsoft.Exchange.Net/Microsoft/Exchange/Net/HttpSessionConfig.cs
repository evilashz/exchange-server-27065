using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.Logging;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000737 RID: 1847
	internal class HttpSessionConfig
	{
		// Token: 0x06002398 RID: 9112 RVA: 0x00049D70 File Offset: 0x00047F70
		public HttpSessionConfig()
		{
			this.allowAutoRedirect = true;
			this.authenticationLevel = AuthenticationLevel.MutualAuthRequested;
			this.cachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
			this.credentials = CredentialCache.DefaultNetworkCredentials;
			this.defaultMaximumErrorResponseLength = 64;
			this.impersonationLevel = TokenImpersonationLevel.None;
			this.keepAlive = true;
			this.maximumAutomaticRedirections = 50;
			this.maximumResponseHeadersLength = 64;
			this.method = "GET";
			this.pipelined = true;
			this.protocolVersion = HttpVersion.Version11;
			this.proxy = WebRequest.DefaultWebProxy;
			this.timeout = 100000;
			this.userAgent = "MicrosoftExchangeServer-HttpClient";
			this.maximumResponseBodyLength = 6291456L;
			this.expect100Continue = null;
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x00049E24 File Offset: 0x00048024
		public HttpSessionConfig(int timeout) : this()
		{
			this.Timeout = timeout;
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x00049E33 File Offset: 0x00048033
		public HttpSessionConfig(IWebProxy proxy, ProtocolLog protocolLog) : this()
		{
			this.Proxy = proxy;
			this.ProtocolLog = protocolLog;
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x0600239B RID: 9115 RVA: 0x00049E49 File Offset: 0x00048049
		// (set) Token: 0x0600239C RID: 9116 RVA: 0x00049E51 File Offset: 0x00048051
		public bool AllowAutoRedirect
		{
			get
			{
				return this.allowAutoRedirect;
			}
			set
			{
				this.allowAutoRedirect = value;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x0600239D RID: 9117 RVA: 0x00049E5A File Offset: 0x0004805A
		// (set) Token: 0x0600239E RID: 9118 RVA: 0x00049E62 File Offset: 0x00048062
		public AuthenticationLevel AuthenticationLevel
		{
			get
			{
				return this.authenticationLevel;
			}
			set
			{
				this.authenticationLevel = value;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x0600239F RID: 9119 RVA: 0x00049E6B File Offset: 0x0004806B
		// (set) Token: 0x060023A0 RID: 9120 RVA: 0x00049E73 File Offset: 0x00048073
		public RequestCachePolicy CachePolicy
		{
			get
			{
				return this.cachePolicy;
			}
			set
			{
				this.cachePolicy = value;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x060023A1 RID: 9121 RVA: 0x00049E7C File Offset: 0x0004807C
		// (set) Token: 0x060023A2 RID: 9122 RVA: 0x00049E84 File Offset: 0x00048084
		public ICredentials Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.credentials = value;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x060023A3 RID: 9123 RVA: 0x00049E8D File Offset: 0x0004808D
		// (set) Token: 0x060023A4 RID: 9124 RVA: 0x00049E95 File Offset: 0x00048095
		public int DefaultMaximumErrorResponseLength
		{
			get
			{
				return this.defaultMaximumErrorResponseLength;
			}
			set
			{
				ArgumentValidator.ThrowIfOutOfRange<int>("DefaultMaximumErrorResponseLength", value, -1, int.MaxValue);
				this.defaultMaximumErrorResponseLength = value;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x060023A5 RID: 9125 RVA: 0x00049EAF File Offset: 0x000480AF
		// (set) Token: 0x060023A6 RID: 9126 RVA: 0x00049EB7 File Offset: 0x000480B7
		public DateTime? IfModifiedSince
		{
			get
			{
				return this.ifModifiedSince;
			}
			set
			{
				this.ifModifiedSince = value;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x060023A7 RID: 9127 RVA: 0x00049EC0 File Offset: 0x000480C0
		// (set) Token: 0x060023A8 RID: 9128 RVA: 0x00049EC8 File Offset: 0x000480C8
		public TokenImpersonationLevel ImpersonationLevel
		{
			get
			{
				return this.impersonationLevel;
			}
			set
			{
				this.impersonationLevel = value;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060023A9 RID: 9129 RVA: 0x00049ED1 File Offset: 0x000480D1
		// (set) Token: 0x060023AA RID: 9130 RVA: 0x00049ED9 File Offset: 0x000480D9
		public bool KeepAlive
		{
			get
			{
				return this.keepAlive;
			}
			set
			{
				this.keepAlive = value;
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x060023AB RID: 9131 RVA: 0x00049EE2 File Offset: 0x000480E2
		// (set) Token: 0x060023AC RID: 9132 RVA: 0x00049EEA File Offset: 0x000480EA
		public int MaximumAutomaticRedirections
		{
			get
			{
				return this.maximumAutomaticRedirections;
			}
			set
			{
				ArgumentValidator.ThrowIfOutOfRange<int>("MaximumAutomaticRedirections", value, 1, int.MaxValue);
				this.maximumAutomaticRedirections = value;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x00049F04 File Offset: 0x00048104
		// (set) Token: 0x060023AE RID: 9134 RVA: 0x00049F0C File Offset: 0x0004810C
		public int MaximumResponseHeadersLength
		{
			get
			{
				return this.maximumResponseHeadersLength;
			}
			set
			{
				ArgumentValidator.ThrowIfOutOfRange<int>("MaximumResponseHeadersLength", value, -1, int.MaxValue);
				this.maximumResponseHeadersLength = value;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x00049F26 File Offset: 0x00048126
		// (set) Token: 0x060023B0 RID: 9136 RVA: 0x00049F2E File Offset: 0x0004812E
		public string Method
		{
			get
			{
				return this.method;
			}
			set
			{
				ArgumentValidator.ThrowIfNull("Method", value);
				HttpSessionConfig.ThrowIfMethodNotImplemented(value);
				this.method = value;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060023B1 RID: 9137 RVA: 0x00049F48 File Offset: 0x00048148
		// (set) Token: 0x060023B2 RID: 9138 RVA: 0x00049F50 File Offset: 0x00048150
		public bool Pipelined
		{
			get
			{
				return this.pipelined;
			}
			set
			{
				this.pipelined = value;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060023B3 RID: 9139 RVA: 0x00049F59 File Offset: 0x00048159
		// (set) Token: 0x060023B4 RID: 9140 RVA: 0x00049F61 File Offset: 0x00048161
		public bool PreAuthenticate
		{
			get
			{
				return this.preAuthenticate;
			}
			set
			{
				this.preAuthenticate = value;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x00049F6A File Offset: 0x0004816A
		// (set) Token: 0x060023B6 RID: 9142 RVA: 0x00049F72 File Offset: 0x00048172
		public Version ProtocolVersion
		{
			get
			{
				return this.protocolVersion;
			}
			set
			{
				HttpSessionConfig.ThrowIfInvalidProtocolVersion("ProtocolVersion", value);
				this.protocolVersion = value;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x060023B7 RID: 9143 RVA: 0x00049F86 File Offset: 0x00048186
		// (set) Token: 0x060023B8 RID: 9144 RVA: 0x00049F8E File Offset: 0x0004818E
		public IWebProxy Proxy
		{
			get
			{
				return this.proxy;
			}
			set
			{
				ArgumentValidator.ThrowIfNull("Proxy", value);
				this.proxy = value;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x060023B9 RID: 9145 RVA: 0x00049FA2 File Offset: 0x000481A2
		// (set) Token: 0x060023BA RID: 9146 RVA: 0x00049FAA File Offset: 0x000481AA
		public int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				ArgumentValidator.ThrowIfOutOfRange<int>("Timeout", value, -1, int.MaxValue);
				this.timeout = value;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x00049FC4 File Offset: 0x000481C4
		// (set) Token: 0x060023BC RID: 9148 RVA: 0x00049FCC File Offset: 0x000481CC
		public bool UnsafeAuthenticatedConnectionSharing
		{
			get
			{
				return this.unsafeAuthenticatedConnectionSharing;
			}
			set
			{
				this.unsafeAuthenticatedConnectionSharing = value;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x060023BD RID: 9149 RVA: 0x00049FD5 File Offset: 0x000481D5
		// (set) Token: 0x060023BE RID: 9150 RVA: 0x00049FDD File Offset: 0x000481DD
		public string UserAgent
		{
			get
			{
				return this.userAgent;
			}
			set
			{
				this.userAgent = value;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x060023BF RID: 9151 RVA: 0x00049FE6 File Offset: 0x000481E6
		// (set) Token: 0x060023C0 RID: 9152 RVA: 0x00049FEE File Offset: 0x000481EE
		public long MaximumResponseBodyLength
		{
			get
			{
				return this.maximumResponseBodyLength;
			}
			set
			{
				ArgumentValidator.ThrowIfOutOfRange<long>("MaximumResponseBodyLength", value, -1L, long.MaxValue);
				this.maximumResponseBodyLength = value;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x060023C1 RID: 9153 RVA: 0x0004A00D File Offset: 0x0004820D
		// (set) Token: 0x060023C2 RID: 9154 RVA: 0x0004A015 File Offset: 0x00048215
		public string IfNoneMatch
		{
			get
			{
				return this.ifNoneMatch;
			}
			set
			{
				this.ifNoneMatch = value;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x060023C3 RID: 9155 RVA: 0x0004A01E File Offset: 0x0004821E
		// (set) Token: 0x060023C4 RID: 9156 RVA: 0x0004A026 File Offset: 0x00048226
		public string IfHeader
		{
			get
			{
				return this.ifHeader;
			}
			set
			{
				this.ifHeader = value;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x060023C5 RID: 9157 RVA: 0x0004A02F File Offset: 0x0004822F
		public int MaxETagLength
		{
			get
			{
				return 64;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x0004A033 File Offset: 0x00048233
		// (set) Token: 0x060023C7 RID: 9159 RVA: 0x0004A03B File Offset: 0x0004823B
		public ProtocolLog ProtocolLog
		{
			get
			{
				return this.protocolLog;
			}
			set
			{
				this.protocolLog = value;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x060023C8 RID: 9160 RVA: 0x0004A044 File Offset: 0x00048244
		// (set) Token: 0x060023C9 RID: 9161 RVA: 0x0004A04C File Offset: 0x0004824C
		public WebHeaderCollection Headers
		{
			get
			{
				return this.headers;
			}
			set
			{
				this.headers = value;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x060023CA RID: 9162 RVA: 0x0004A055 File Offset: 0x00048255
		// (set) Token: 0x060023CB RID: 9163 RVA: 0x0004A05D File Offset: 0x0004825D
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				return this.clientCertificates;
			}
			set
			{
				this.clientCertificates = value;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x060023CC RID: 9164 RVA: 0x0004A066 File Offset: 0x00048266
		// (set) Token: 0x060023CD RID: 9165 RVA: 0x0004A06E File Offset: 0x0004826E
		public string ContentType
		{
			get
			{
				return this.contentType;
			}
			set
			{
				this.contentType = value;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x060023CE RID: 9166 RVA: 0x0004A077 File Offset: 0x00048277
		// (set) Token: 0x060023CF RID: 9167 RVA: 0x0004A07F File Offset: 0x0004827F
		public CookieContainer CookieContainer
		{
			get
			{
				return this.cookieContainer;
			}
			set
			{
				this.cookieContainer = value;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x0004A088 File Offset: 0x00048288
		// (set) Token: 0x060023D1 RID: 9169 RVA: 0x0004A090 File Offset: 0x00048290
		public Stream RequestStream
		{
			get
			{
				return this.requestStream;
			}
			set
			{
				this.requestStream = value;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x060023D2 RID: 9170 RVA: 0x0004A099 File Offset: 0x00048299
		// (set) Token: 0x060023D3 RID: 9171 RVA: 0x0004A0A1 File Offset: 0x000482A1
		public int? Rows
		{
			get
			{
				return this.rows;
			}
			set
			{
				this.rows = value;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x060023D4 RID: 9172 RVA: 0x0004A0AA File Offset: 0x000482AA
		// (set) Token: 0x060023D5 RID: 9173 RVA: 0x0004A0B2 File Offset: 0x000482B2
		public string Accept
		{
			get
			{
				return this.accept;
			}
			set
			{
				this.accept = value;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x0004A0BB File Offset: 0x000482BB
		// (set) Token: 0x060023D7 RID: 9175 RVA: 0x0004A0C3 File Offset: 0x000482C3
		public bool? Expect100Continue
		{
			get
			{
				return this.expect100Continue;
			}
			set
			{
				this.expect100Continue = value;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x060023D8 RID: 9176 RVA: 0x0004A0CC File Offset: 0x000482CC
		// (set) Token: 0x060023D9 RID: 9177 RVA: 0x0004A0D4 File Offset: 0x000482D4
		public bool ReadWebExceptionResponseStream
		{
			get
			{
				return this.readWebExceptionResponseStream;
			}
			set
			{
				this.readWebExceptionResponseStream = value;
			}
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x0004A0DD File Offset: 0x000482DD
		private static void ThrowIfInvalidProtocolVersion(string name, Version protocolVersion)
		{
			if (protocolVersion != HttpVersion.Version10 && protocolVersion != HttpVersion.Version11)
			{
				throw new ArgumentException("The HTTP version is set to a value other than 1.0 or 1.1.", name);
			}
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x0004A108 File Offset: 0x00048308
		private static void ThrowIfMethodNotImplemented(string method)
		{
			if (string.Compare(method, "BPROPPATCH", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(method, "DELETE", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(method, "GET", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(method, "HEAD", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(method, "LOCK", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(method, "MKCOL", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(method, "MOVE", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(method, "POST", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(method, "PROPFIND", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(method, "PROPPATCH", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(method, "PUT", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(method, "SEARCH", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(method, "UNLOCK", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(method, "MERGE", StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new NotImplementedException("Only BPROPPATCH, DELETE, GET, HEAD, LOCK, MKCOL, MOVE, POST, PROPFIND, PROPPATCH, PUT, SEARCH, UNLOCK and MERGE request methods are implemented.");
			}
		}

		// Token: 0x04002198 RID: 8600
		private const int MaxETagLengthValue = 64;

		// Token: 0x04002199 RID: 8601
		private bool allowAutoRedirect;

		// Token: 0x0400219A RID: 8602
		private AuthenticationLevel authenticationLevel;

		// Token: 0x0400219B RID: 8603
		private RequestCachePolicy cachePolicy;

		// Token: 0x0400219C RID: 8604
		private ICredentials credentials;

		// Token: 0x0400219D RID: 8605
		private int defaultMaximumErrorResponseLength;

		// Token: 0x0400219E RID: 8606
		private DateTime? ifModifiedSince;

		// Token: 0x0400219F RID: 8607
		private TokenImpersonationLevel impersonationLevel;

		// Token: 0x040021A0 RID: 8608
		private bool keepAlive;

		// Token: 0x040021A1 RID: 8609
		private int maximumAutomaticRedirections;

		// Token: 0x040021A2 RID: 8610
		private int maximumResponseHeadersLength;

		// Token: 0x040021A3 RID: 8611
		private string method;

		// Token: 0x040021A4 RID: 8612
		private bool pipelined;

		// Token: 0x040021A5 RID: 8613
		private bool preAuthenticate;

		// Token: 0x040021A6 RID: 8614
		private Version protocolVersion;

		// Token: 0x040021A7 RID: 8615
		private IWebProxy proxy;

		// Token: 0x040021A8 RID: 8616
		private int timeout;

		// Token: 0x040021A9 RID: 8617
		private bool unsafeAuthenticatedConnectionSharing;

		// Token: 0x040021AA RID: 8618
		private string userAgent;

		// Token: 0x040021AB RID: 8619
		private long maximumResponseBodyLength;

		// Token: 0x040021AC RID: 8620
		private string ifNoneMatch;

		// Token: 0x040021AD RID: 8621
		private string ifHeader;

		// Token: 0x040021AE RID: 8622
		private ProtocolLog protocolLog;

		// Token: 0x040021AF RID: 8623
		private WebHeaderCollection headers;

		// Token: 0x040021B0 RID: 8624
		private X509CertificateCollection clientCertificates;

		// Token: 0x040021B1 RID: 8625
		private string contentType;

		// Token: 0x040021B2 RID: 8626
		private CookieContainer cookieContainer;

		// Token: 0x040021B3 RID: 8627
		private Stream requestStream;

		// Token: 0x040021B4 RID: 8628
		private int? rows;

		// Token: 0x040021B5 RID: 8629
		private string accept;

		// Token: 0x040021B6 RID: 8630
		private bool? expect100Continue;

		// Token: 0x040021B7 RID: 8631
		private bool readWebExceptionResponseStream;
	}
}
