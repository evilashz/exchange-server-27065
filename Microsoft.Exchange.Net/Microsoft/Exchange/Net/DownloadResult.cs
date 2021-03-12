using System;
using System.IO;
using System.Net;
using System.Security;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000732 RID: 1842
	internal struct DownloadResult
	{
		// Token: 0x06002333 RID: 9011 RVA: 0x00047DAC File Offset: 0x00045FAC
		public DownloadResult(Exception exception)
		{
			this.exception = exception;
			this.responseStream = null;
			this.lastModified = null;
			this.eTag = string.Empty;
			this.bytesDownloaded = 0L;
			this.responseUri = null;
			this.lastKnownRequestedUri = null;
			this.statusCode = HttpStatusCode.OK;
			this.responseHeaders = null;
			this.isRetryable = null;
			this.cookies = null;
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002334 RID: 9012 RVA: 0x00047E19 File Offset: 0x00046019
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002335 RID: 9013 RVA: 0x00047E21 File Offset: 0x00046021
		// (set) Token: 0x06002336 RID: 9014 RVA: 0x00047E29 File Offset: 0x00046029
		public Stream ResponseStream
		{
			get
			{
				return this.responseStream;
			}
			internal set
			{
				this.responseStream = value;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002337 RID: 9015 RVA: 0x00047E32 File Offset: 0x00046032
		// (set) Token: 0x06002338 RID: 9016 RVA: 0x00047E3A File Offset: 0x0004603A
		public DateTime? LastModified
		{
			get
			{
				return this.lastModified;
			}
			internal set
			{
				this.lastModified = value;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x00047E43 File Offset: 0x00046043
		// (set) Token: 0x0600233A RID: 9018 RVA: 0x00047E4B File Offset: 0x0004604B
		public string ETag
		{
			get
			{
				return this.eTag;
			}
			internal set
			{
				this.eTag = value;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x0600233B RID: 9019 RVA: 0x00047E54 File Offset: 0x00046054
		// (set) Token: 0x0600233C RID: 9020 RVA: 0x00047E5C File Offset: 0x0004605C
		public long BytesDownloaded
		{
			get
			{
				return this.bytesDownloaded;
			}
			internal set
			{
				this.bytesDownloaded = value;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x00047E65 File Offset: 0x00046065
		// (set) Token: 0x0600233E RID: 9022 RVA: 0x00047E6D File Offset: 0x0004606D
		public HttpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
			internal set
			{
				this.statusCode = value;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x0600233F RID: 9023 RVA: 0x00047E76 File Offset: 0x00046076
		// (set) Token: 0x06002340 RID: 9024 RVA: 0x00047E7E File Offset: 0x0004607E
		public WebHeaderCollection ResponseHeaders
		{
			get
			{
				return this.responseHeaders;
			}
			internal set
			{
				this.responseHeaders = value;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06002341 RID: 9025 RVA: 0x00047E87 File Offset: 0x00046087
		// (set) Token: 0x06002342 RID: 9026 RVA: 0x00047E8F File Offset: 0x0004608F
		public CookieCollection Cookies
		{
			get
			{
				return this.cookies;
			}
			internal set
			{
				this.cookies = value;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x00047E98 File Offset: 0x00046098
		// (set) Token: 0x06002344 RID: 9028 RVA: 0x00047EA0 File Offset: 0x000460A0
		public Uri ResponseUri
		{
			get
			{
				return this.responseUri;
			}
			internal set
			{
				this.responseUri = value;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x00047EA9 File Offset: 0x000460A9
		// (set) Token: 0x06002346 RID: 9030 RVA: 0x00047EB1 File Offset: 0x000460B1
		public Uri LastKnownRequestedUri
		{
			get
			{
				return this.lastKnownRequestedUri;
			}
			internal set
			{
				this.lastKnownRequestedUri = value;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x00047EBA File Offset: 0x000460BA
		public bool IsCanceled
		{
			get
			{
				return this.exception is DownloadCanceledException;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x00047ECA File Offset: 0x000460CA
		public bool IsSucceeded
		{
			get
			{
				return this.exception == null;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x00047ED5 File Offset: 0x000460D5
		public bool IsRetryable
		{
			get
			{
				if (this.isRetryable == null)
				{
					this.isRetryable = new bool?(DownloadResult.IsRetryableException(this.exception));
				}
				return this.isRetryable.Value;
			}
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x00047F05 File Offset: 0x00046105
		public override string ToString()
		{
			if (this.IsCanceled)
			{
				return "Canceled";
			}
			if (this.IsSucceeded)
			{
				return "Success";
			}
			return this.exception.GetType().FullName;
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x00047F34 File Offset: 0x00046134
		private static bool IsRetryableException(Exception exception)
		{
			if (exception == null || exception is SecurityException || exception is DownloadLimitExceededException || exception is ServerProtocolViolationException || exception is BadRedirectedUriException || exception is UnsupportedUriFormatException)
			{
				return false;
			}
			WebException ex = exception as WebException;
			return ex == null || DownloadResult.IsRetryableWebException(ex);
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x00047F80 File Offset: 0x00046180
		private static bool IsRetryableWebException(WebException exception)
		{
			WebExceptionStatus status = exception.Status;
			if (status != WebExceptionStatus.Success)
			{
				switch (status)
				{
				case WebExceptionStatus.ProtocolError:
					return DownloadResult.IsRetryableHttpStatusCode(((HttpWebResponse)exception.Response).StatusCode);
				case WebExceptionStatus.ConnectionClosed:
				case WebExceptionStatus.SecureChannelFailure:
					break;
				case WebExceptionStatus.TrustFailure:
				case WebExceptionStatus.ServerProtocolViolation:
					return false;
				default:
					switch (status)
					{
					case WebExceptionStatus.MessageLengthLimitExceeded:
					case WebExceptionStatus.CacheEntryNotFound:
					case WebExceptionStatus.RequestProhibitedByCachePolicy:
					case WebExceptionStatus.RequestProhibitedByProxy:
						return false;
					}
					break;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x00047FE8 File Offset: 0x000461E8
		private static bool IsRetryableHttpStatusCode(HttpStatusCode statusCode)
		{
			if (statusCode != HttpStatusCode.NotFound && statusCode != HttpStatusCode.RequestTimeout)
			{
				switch (statusCode)
				{
				case HttpStatusCode.InternalServerError:
				case HttpStatusCode.BadGateway:
				case HttpStatusCode.ServiceUnavailable:
				case HttpStatusCode.GatewayTimeout:
					return true;
				}
				return false;
			}
			return true;
		}

		// Token: 0x04002145 RID: 8517
		private Exception exception;

		// Token: 0x04002146 RID: 8518
		private Stream responseStream;

		// Token: 0x04002147 RID: 8519
		private DateTime? lastModified;

		// Token: 0x04002148 RID: 8520
		private string eTag;

		// Token: 0x04002149 RID: 8521
		private long bytesDownloaded;

		// Token: 0x0400214A RID: 8522
		private Uri responseUri;

		// Token: 0x0400214B RID: 8523
		private Uri lastKnownRequestedUri;

		// Token: 0x0400214C RID: 8524
		private HttpStatusCode statusCode;

		// Token: 0x0400214D RID: 8525
		private WebHeaderCollection responseHeaders;

		// Token: 0x0400214E RID: 8526
		private CookieCollection cookies;

		// Token: 0x0400214F RID: 8527
		private bool? isRetryable;
	}
}
