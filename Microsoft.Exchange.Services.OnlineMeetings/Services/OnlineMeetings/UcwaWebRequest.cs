using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.OnlineMeetings.Autodiscover;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000028 RID: 40
	internal class UcwaWebRequest
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00005708 File Offset: 0x00003908
		public UcwaWebRequest(HttpWebRequest webRequest)
		{
			this.webRequest = webRequest;
			this.PreAuthenticate = true;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000571E File Offset: 0x0000391E
		// (set) Token: 0x06000117 RID: 279 RVA: 0x0000572B File Offset: 0x0000392B
		public RequestCachePolicy CachePolicy
		{
			get
			{
				return this.webRequest.CachePolicy;
			}
			set
			{
				this.webRequest.CachePolicy = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00005739 File Offset: 0x00003939
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00005746 File Offset: 0x00003946
		public AuthenticationLevel AuthenticationLevel
		{
			get
			{
				return this.webRequest.AuthenticationLevel;
			}
			set
			{
				this.webRequest.AuthenticationLevel = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005754 File Offset: 0x00003954
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00005761 File Offset: 0x00003961
		public TokenImpersonationLevel ImpersonationLevel
		{
			get
			{
				return this.webRequest.ImpersonationLevel;
			}
			set
			{
				this.webRequest.ImpersonationLevel = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000576F File Offset: 0x0000396F
		// (set) Token: 0x0600011D RID: 285 RVA: 0x0000577C File Offset: 0x0000397C
		public bool AllowAutoRedirect
		{
			get
			{
				return this.webRequest.AllowAutoRedirect;
			}
			set
			{
				this.webRequest.AllowAutoRedirect = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000578A File Offset: 0x0000398A
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00005797 File Offset: 0x00003997
		public bool AllowWriteStreamBuffering
		{
			get
			{
				return this.webRequest.AllowWriteStreamBuffering;
			}
			set
			{
				this.webRequest.AllowWriteStreamBuffering = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000057A5 File Offset: 0x000039A5
		public bool HaveResponse
		{
			get
			{
				return this.webRequest.HaveResponse;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000057B2 File Offset: 0x000039B2
		// (set) Token: 0x06000122 RID: 290 RVA: 0x000057BF File Offset: 0x000039BF
		public bool KeepAlive
		{
			get
			{
				return this.webRequest.KeepAlive;
			}
			set
			{
				this.webRequest.KeepAlive = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000057CD File Offset: 0x000039CD
		// (set) Token: 0x06000124 RID: 292 RVA: 0x000057DA File Offset: 0x000039DA
		public bool Pipelined
		{
			get
			{
				return this.webRequest.Pipelined;
			}
			set
			{
				this.webRequest.Pipelined = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000057E8 File Offset: 0x000039E8
		// (set) Token: 0x06000126 RID: 294 RVA: 0x000057F5 File Offset: 0x000039F5
		public bool PreAuthenticate
		{
			get
			{
				return this.webRequest.PreAuthenticate;
			}
			set
			{
				this.webRequest.PreAuthenticate = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00005803 File Offset: 0x00003A03
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00005810 File Offset: 0x00003A10
		public bool UnsafeAuthenticatedConnectionSharing
		{
			get
			{
				return this.webRequest.UnsafeAuthenticatedConnectionSharing;
			}
			set
			{
				this.webRequest.UnsafeAuthenticatedConnectionSharing = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000581E File Offset: 0x00003A1E
		// (set) Token: 0x0600012A RID: 298 RVA: 0x0000582B File Offset: 0x00003A2B
		public bool SendChunked
		{
			get
			{
				return this.webRequest.SendChunked;
			}
			set
			{
				this.webRequest.SendChunked = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00005839 File Offset: 0x00003A39
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00005846 File Offset: 0x00003A46
		public DecompressionMethods AutomaticDecompression
		{
			get
			{
				return this.webRequest.AutomaticDecompression;
			}
			set
			{
				this.webRequest.AutomaticDecompression = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00005854 File Offset: 0x00003A54
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00005861 File Offset: 0x00003A61
		public int MaximumResponseHeadersLength
		{
			get
			{
				return this.webRequest.MaximumResponseHeadersLength;
			}
			set
			{
				this.webRequest.MaximumResponseHeadersLength = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000586F File Offset: 0x00003A6F
		// (set) Token: 0x06000130 RID: 304 RVA: 0x0000587C File Offset: 0x00003A7C
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				return this.webRequest.ClientCertificates;
			}
			set
			{
				this.webRequest.ClientCertificates = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000588A File Offset: 0x00003A8A
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00005897 File Offset: 0x00003A97
		public CookieContainer CookieContainer
		{
			get
			{
				return this.webRequest.CookieContainer;
			}
			set
			{
				this.webRequest.CookieContainer = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000058A5 File Offset: 0x00003AA5
		public Uri RequestUri
		{
			get
			{
				return this.webRequest.RequestUri;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000058B2 File Offset: 0x00003AB2
		// (set) Token: 0x06000135 RID: 309 RVA: 0x000058BF File Offset: 0x00003ABF
		public long ContentLength
		{
			get
			{
				return this.webRequest.ContentLength;
			}
			set
			{
				this.webRequest.ContentLength = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000058CD File Offset: 0x00003ACD
		// (set) Token: 0x06000137 RID: 311 RVA: 0x000058DA File Offset: 0x00003ADA
		public int Timeout
		{
			get
			{
				return this.webRequest.Timeout;
			}
			set
			{
				this.webRequest.Timeout = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000058E8 File Offset: 0x00003AE8
		// (set) Token: 0x06000139 RID: 313 RVA: 0x000058F5 File Offset: 0x00003AF5
		public int ReadWriteTimeout
		{
			get
			{
				return this.webRequest.ReadWriteTimeout;
			}
			set
			{
				this.webRequest.ReadWriteTimeout = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00005903 File Offset: 0x00003B03
		public Uri Address
		{
			get
			{
				return this.webRequest.Address;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00005910 File Offset: 0x00003B10
		// (set) Token: 0x0600013C RID: 316 RVA: 0x0000591D File Offset: 0x00003B1D
		public HttpContinueDelegate ContinueDelegate
		{
			get
			{
				return this.webRequest.ContinueDelegate;
			}
			set
			{
				this.webRequest.ContinueDelegate = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000592B File Offset: 0x00003B2B
		public ServicePoint ServicePoint
		{
			get
			{
				return this.webRequest.ServicePoint;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00005938 File Offset: 0x00003B38
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00005945 File Offset: 0x00003B45
		public string Host
		{
			get
			{
				return this.webRequest.Host;
			}
			set
			{
				this.webRequest.Host = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00005953 File Offset: 0x00003B53
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00005960 File Offset: 0x00003B60
		public int MaximumAutomaticRedirections
		{
			get
			{
				return this.webRequest.MaximumAutomaticRedirections;
			}
			set
			{
				this.webRequest.MaximumAutomaticRedirections = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000596E File Offset: 0x00003B6E
		// (set) Token: 0x06000143 RID: 323 RVA: 0x0000597B File Offset: 0x00003B7B
		public string Method
		{
			get
			{
				return this.webRequest.Method;
			}
			set
			{
				this.webRequest.Method = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00005989 File Offset: 0x00003B89
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00005996 File Offset: 0x00003B96
		public ICredentials Credentials
		{
			get
			{
				return this.webRequest.Credentials;
			}
			set
			{
				this.webRequest.Credentials = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000059A4 File Offset: 0x00003BA4
		// (set) Token: 0x06000147 RID: 327 RVA: 0x000059B1 File Offset: 0x00003BB1
		public bool UseDefaultCredentials
		{
			get
			{
				return this.webRequest.UseDefaultCredentials;
			}
			set
			{
				this.webRequest.UseDefaultCredentials = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000059BF File Offset: 0x00003BBF
		// (set) Token: 0x06000149 RID: 329 RVA: 0x000059CC File Offset: 0x00003BCC
		public string ConnectionGroupName
		{
			get
			{
				return this.webRequest.ConnectionGroupName;
			}
			set
			{
				this.webRequest.ConnectionGroupName = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000059DA File Offset: 0x00003BDA
		// (set) Token: 0x0600014B RID: 331 RVA: 0x000059E7 File Offset: 0x00003BE7
		public WebHeaderCollection Headers
		{
			get
			{
				return this.webRequest.Headers;
			}
			set
			{
				this.webRequest.Headers = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000059F5 File Offset: 0x00003BF5
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00005A02 File Offset: 0x00003C02
		public IWebProxy Proxy
		{
			get
			{
				return this.webRequest.Proxy;
			}
			set
			{
				this.webRequest.Proxy = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00005A10 File Offset: 0x00003C10
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00005A1D File Offset: 0x00003C1D
		public Version ProtocolVersion
		{
			get
			{
				return this.webRequest.ProtocolVersion;
			}
			set
			{
				this.webRequest.ProtocolVersion = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00005A2B File Offset: 0x00003C2B
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00005A38 File Offset: 0x00003C38
		public string ContentType
		{
			get
			{
				return this.webRequest.ContentType;
			}
			set
			{
				this.webRequest.ContentType = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00005A46 File Offset: 0x00003C46
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00005A53 File Offset: 0x00003C53
		public string MediaType
		{
			get
			{
				return this.webRequest.MediaType;
			}
			set
			{
				this.webRequest.MediaType = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005A61 File Offset: 0x00003C61
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00005A6E File Offset: 0x00003C6E
		public string TransferEncoding
		{
			get
			{
				return this.webRequest.TransferEncoding;
			}
			set
			{
				this.webRequest.TransferEncoding = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00005A7C File Offset: 0x00003C7C
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00005A89 File Offset: 0x00003C89
		public string Connection
		{
			get
			{
				return this.webRequest.Connection;
			}
			set
			{
				this.webRequest.Connection = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00005A97 File Offset: 0x00003C97
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public string Accept
		{
			get
			{
				return this.webRequest.Accept;
			}
			set
			{
				this.webRequest.Accept = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00005AB2 File Offset: 0x00003CB2
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00005ABF File Offset: 0x00003CBF
		public string Referer
		{
			get
			{
				return this.webRequest.Referer;
			}
			set
			{
				this.webRequest.Referer = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00005ACD File Offset: 0x00003CCD
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00005ADA File Offset: 0x00003CDA
		public string UserAgent
		{
			get
			{
				return this.webRequest.UserAgent;
			}
			set
			{
				this.webRequest.UserAgent = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00005AE8 File Offset: 0x00003CE8
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00005AF5 File Offset: 0x00003CF5
		public string Expect
		{
			get
			{
				return this.webRequest.Expect;
			}
			set
			{
				this.webRequest.Expect = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00005B03 File Offset: 0x00003D03
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00005B10 File Offset: 0x00003D10
		public DateTime IfModifiedSince
		{
			get
			{
				return this.webRequest.IfModifiedSince;
			}
			set
			{
				this.webRequest.IfModifiedSince = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00005B1E File Offset: 0x00003D1E
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00005B2B File Offset: 0x00003D2B
		public DateTime Date
		{
			get
			{
				return this.webRequest.Date;
			}
			set
			{
				this.webRequest.Date = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00005B39 File Offset: 0x00003D39
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00005B46 File Offset: 0x00003D46
		public virtual string RequestContentType
		{
			get
			{
				return this.webRequest.ContentType;
			}
			set
			{
				this.webRequest.ContentType = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00005B54 File Offset: 0x00003D54
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00005B61 File Offset: 0x00003D61
		public virtual string AcceptType
		{
			get
			{
				return this.webRequest.Accept;
			}
			set
			{
				this.webRequest.Accept = value;
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005B6F File Offset: 0x00003D6F
		public object GetLifetimeService()
		{
			return this.webRequest.GetLifetimeService();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005B7C File Offset: 0x00003D7C
		public object InitializeLifetimeService()
		{
			return this.webRequest.InitializeLifetimeService();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005B89 File Offset: 0x00003D89
		public ObjRef CreateObjRef(Type requestedType)
		{
			return this.webRequest.CreateObjRef(requestedType);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005B97 File Offset: 0x00003D97
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			((ISerializable)this.webRequest).GetObjectData(info, context);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005BA6 File Offset: 0x00003DA6
		public IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			return this.webRequest.BeginGetRequestStream(callback, state);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005BB5 File Offset: 0x00003DB5
		public Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			return this.webRequest.EndGetRequestStream(asyncResult);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005BC3 File Offset: 0x00003DC3
		public Stream EndGetRequestStream(IAsyncResult asyncResult, out TransportContext context)
		{
			return this.webRequest.EndGetRequestStream(asyncResult, out context);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005BD2 File Offset: 0x00003DD2
		public Stream GetRequestStream()
		{
			return this.webRequest.GetRequestStream();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005BDF File Offset: 0x00003DDF
		public Stream GetRequestStream(out TransportContext context)
		{
			return this.webRequest.GetRequestStream(out context);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005BED File Offset: 0x00003DED
		public IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			return this.webRequest.BeginGetResponse(callback, state);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005BFC File Offset: 0x00003DFC
		public WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			return this.webRequest.EndGetResponse(asyncResult);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005C0A File Offset: 0x00003E0A
		public WebResponse GetResponse()
		{
			return this.webRequest.GetResponse();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005C17 File Offset: 0x00003E17
		public void Abort()
		{
			this.webRequest.Abort();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005C24 File Offset: 0x00003E24
		public void AddRange(int from, int to)
		{
			this.webRequest.AddRange(from, to);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005C33 File Offset: 0x00003E33
		public void AddRange(long from, long to)
		{
			this.webRequest.AddRange(from, to);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005C42 File Offset: 0x00003E42
		public void AddRange(int range)
		{
			this.webRequest.AddRange(range);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005C50 File Offset: 0x00003E50
		public void AddRange(long range)
		{
			this.webRequest.AddRange(range);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005C5E File Offset: 0x00003E5E
		public void AddRange(string rangeSpecifier, int from, int to)
		{
			this.webRequest.AddRange(rangeSpecifier, from, to);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005C6E File Offset: 0x00003E6E
		public void AddRange(string rangeSpecifier, long from, long to)
		{
			this.webRequest.AddRange(rangeSpecifier, from, to);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005C7E File Offset: 0x00003E7E
		public void AddRange(string rangeSpecifier, int range)
		{
			this.webRequest.AddRange(rangeSpecifier, range);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005C8D File Offset: 0x00003E8D
		public void AddRange(string rangeSpecifier, long range)
		{
			this.webRequest.AddRange(rangeSpecifier, range);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005C9C File Offset: 0x00003E9C
		public virtual Task<Stream> GetRequestStreamAsync()
		{
			return Task<Stream>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.webRequest.BeginGetRequestStream), new Func<IAsyncResult, Stream>(this.webRequest.EndGetRequestStream), new object(), TaskCreationOptions.None);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public virtual Task<WebResponse> GetResponseAsync()
		{
			ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string, string>(0, 0L, "[OnlineMeetings][UcwaWebRequest.GetResponseAsync] Request to {0} contains headers:{1}", this.webRequest.RequestUri.ToString(), this.webRequest.GetRequestHeadersAsString());
			return Task<WebResponse>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.webRequest.BeginGetResponse), new Func<IAsyncResult, WebResponse>(this.webRequest.EndGetResponse), new object(), TaskCreationOptions.None);
		}

		// Token: 0x04000117 RID: 279
		private readonly HttpWebRequest webRequest;
	}
}
