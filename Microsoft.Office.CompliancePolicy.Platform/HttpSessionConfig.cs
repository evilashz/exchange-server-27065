using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x0200003F RID: 63
	internal sealed class HttpSessionConfig
	{
		// Token: 0x0600013D RID: 317 RVA: 0x0000589C File Offset: 0x00003A9C
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

		// Token: 0x0600013E RID: 318 RVA: 0x00005950 File Offset: 0x00003B50
		public HttpSessionConfig(int timeout) : this()
		{
			this.Timeout = timeout;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000595F File Offset: 0x00003B5F
		public HttpSessionConfig(IWebProxy proxy, ExecutionLog protocolLog) : this()
		{
			this.Proxy = proxy;
			this.ProtocolLog = protocolLog;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00005975 File Offset: 0x00003B75
		// (set) Token: 0x06000141 RID: 321 RVA: 0x0000597D File Offset: 0x00003B7D
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00005986 File Offset: 0x00003B86
		// (set) Token: 0x06000143 RID: 323 RVA: 0x0000598E File Offset: 0x00003B8E
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00005997 File Offset: 0x00003B97
		// (set) Token: 0x06000145 RID: 325 RVA: 0x0000599F File Offset: 0x00003B9F
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

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000059A8 File Offset: 0x00003BA8
		// (set) Token: 0x06000147 RID: 327 RVA: 0x000059B0 File Offset: 0x00003BB0
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

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000059B9 File Offset: 0x00003BB9
		// (set) Token: 0x06000149 RID: 329 RVA: 0x000059C1 File Offset: 0x00003BC1
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000059DB File Offset: 0x00003BDB
		// (set) Token: 0x0600014B RID: 331 RVA: 0x000059E3 File Offset: 0x00003BE3
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000059EC File Offset: 0x00003BEC
		// (set) Token: 0x0600014D RID: 333 RVA: 0x000059F4 File Offset: 0x00003BF4
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000059FD File Offset: 0x00003BFD
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00005A05 File Offset: 0x00003C05
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00005A0E File Offset: 0x00003C0E
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00005A16 File Offset: 0x00003C16
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00005A30 File Offset: 0x00003C30
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00005A38 File Offset: 0x00003C38
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

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005A52 File Offset: 0x00003C52
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00005A5A File Offset: 0x00003C5A
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00005A74 File Offset: 0x00003C74
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00005A7C File Offset: 0x00003C7C
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00005A85 File Offset: 0x00003C85
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00005A8D File Offset: 0x00003C8D
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00005A96 File Offset: 0x00003C96
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00005A9E File Offset: 0x00003C9E
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00005AB2 File Offset: 0x00003CB2
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00005ABA File Offset: 0x00003CBA
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

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00005ACE File Offset: 0x00003CCE
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00005AD6 File Offset: 0x00003CD6
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

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00005AF0 File Offset: 0x00003CF0
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00005AF8 File Offset: 0x00003CF8
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

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00005B01 File Offset: 0x00003D01
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00005B09 File Offset: 0x00003D09
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

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00005B12 File Offset: 0x00003D12
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00005B1A File Offset: 0x00003D1A
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

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00005B39 File Offset: 0x00003D39
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00005B41 File Offset: 0x00003D41
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

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00005B4A File Offset: 0x00003D4A
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00005B52 File Offset: 0x00003D52
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

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00005B5B File Offset: 0x00003D5B
		public int MaxETagLength
		{
			get
			{
				return 64;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00005B5F File Offset: 0x00003D5F
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00005B67 File Offset: 0x00003D67
		public ExecutionLog ProtocolLog
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

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00005B70 File Offset: 0x00003D70
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00005B78 File Offset: 0x00003D78
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00005B81 File Offset: 0x00003D81
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00005B89 File Offset: 0x00003D89
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

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00005B92 File Offset: 0x00003D92
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00005B9A File Offset: 0x00003D9A
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

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00005BA3 File Offset: 0x00003DA3
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00005BAB File Offset: 0x00003DAB
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

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00005BB4 File Offset: 0x00003DB4
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00005BBC File Offset: 0x00003DBC
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

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00005BC5 File Offset: 0x00003DC5
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00005BCD File Offset: 0x00003DCD
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

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00005BD6 File Offset: 0x00003DD6
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00005BDE File Offset: 0x00003DDE
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

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00005BE7 File Offset: 0x00003DE7
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00005BEF File Offset: 0x00003DEF
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

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00005BF8 File Offset: 0x00003DF8
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00005C00 File Offset: 0x00003E00
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

		// Token: 0x0600017F RID: 383 RVA: 0x00005C09 File Offset: 0x00003E09
		private static void ThrowIfInvalidProtocolVersion(string name, Version protocolVersion)
		{
			if (protocolVersion != HttpVersion.Version10 && protocolVersion != HttpVersion.Version11)
			{
				throw new ArgumentException("The HTTP version is set to a value other than 1.0 or 1.1.", name);
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005C34 File Offset: 0x00003E34
		private static void ThrowIfMethodNotImplemented(string method)
		{
			if (!string.Equals(method, "BPROPPATCH", StringComparison.OrdinalIgnoreCase) && !string.Equals(method, "DELETE", StringComparison.OrdinalIgnoreCase) && !string.Equals(method, "GET", StringComparison.OrdinalIgnoreCase) && !string.Equals(method, "HEAD", StringComparison.OrdinalIgnoreCase) && !string.Equals(method, "LOCK", StringComparison.OrdinalIgnoreCase) && !string.Equals(method, "MKCOL", StringComparison.OrdinalIgnoreCase) && !string.Equals(method, "MOVE", StringComparison.OrdinalIgnoreCase) && !string.Equals(method, "POST", StringComparison.OrdinalIgnoreCase) && !string.Equals(method, "PROPFIND", StringComparison.OrdinalIgnoreCase) && !string.Equals(method, "PROPPATCH", StringComparison.OrdinalIgnoreCase) && !string.Equals(method, "PUT", StringComparison.OrdinalIgnoreCase) && !string.Equals(method, "SEARCH", StringComparison.OrdinalIgnoreCase) && !string.Equals(method, "UNLOCK", StringComparison.OrdinalIgnoreCase))
			{
				throw new NotImplementedException("Only BPROPPATCH, DELETE, GET, HEAD, LOCK, MKCOL, MOVE, POST, PROPFIND, PROPPATCH, PUT, SEARCH and UNLOCK request methods are implemented.");
			}
		}

		// Token: 0x040000BF RID: 191
		private const int MaxETagLengthValue = 64;

		// Token: 0x040000C0 RID: 192
		private bool allowAutoRedirect;

		// Token: 0x040000C1 RID: 193
		private AuthenticationLevel authenticationLevel;

		// Token: 0x040000C2 RID: 194
		private RequestCachePolicy cachePolicy;

		// Token: 0x040000C3 RID: 195
		private ICredentials credentials;

		// Token: 0x040000C4 RID: 196
		private int defaultMaximumErrorResponseLength;

		// Token: 0x040000C5 RID: 197
		private DateTime? ifModifiedSince;

		// Token: 0x040000C6 RID: 198
		private TokenImpersonationLevel impersonationLevel;

		// Token: 0x040000C7 RID: 199
		private bool keepAlive;

		// Token: 0x040000C8 RID: 200
		private int maximumAutomaticRedirections;

		// Token: 0x040000C9 RID: 201
		private int maximumResponseHeadersLength;

		// Token: 0x040000CA RID: 202
		private string method;

		// Token: 0x040000CB RID: 203
		private bool pipelined;

		// Token: 0x040000CC RID: 204
		private bool preAuthenticate;

		// Token: 0x040000CD RID: 205
		private Version protocolVersion;

		// Token: 0x040000CE RID: 206
		private IWebProxy proxy;

		// Token: 0x040000CF RID: 207
		private int timeout;

		// Token: 0x040000D0 RID: 208
		private bool unsafeAuthenticatedConnectionSharing;

		// Token: 0x040000D1 RID: 209
		private string userAgent;

		// Token: 0x040000D2 RID: 210
		private long maximumResponseBodyLength;

		// Token: 0x040000D3 RID: 211
		private string ifNoneMatch;

		// Token: 0x040000D4 RID: 212
		private string ifHeader;

		// Token: 0x040000D5 RID: 213
		private ExecutionLog protocolLog;

		// Token: 0x040000D6 RID: 214
		private WebHeaderCollection headers;

		// Token: 0x040000D7 RID: 215
		private X509CertificateCollection clientCertificates;

		// Token: 0x040000D8 RID: 216
		private string contentType;

		// Token: 0x040000D9 RID: 217
		private CookieContainer cookieContainer;

		// Token: 0x040000DA RID: 218
		private Stream requestStream;

		// Token: 0x040000DB RID: 219
		private int? rows;

		// Token: 0x040000DC RID: 220
		private string accept;

		// Token: 0x040000DD RID: 221
		private bool? expect100Continue;

		// Token: 0x040000DE RID: 222
		private bool readWebExceptionResponseStream;
	}
}
