using System;
using System.IO;
using System.Net;
using System.Security;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x0200003A RID: 58
	internal sealed class DownloadResult
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00003982 File Offset: 0x00001B82
		public DownloadResult(Exception exception)
		{
			this.Exception = exception;
			this.StatusCode = HttpStatusCode.OK;
			this.ETag = string.Empty;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000039A7 File Offset: 0x00001BA7
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x000039AF File Offset: 0x00001BAF
		public Exception Exception { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000039B8 File Offset: 0x00001BB8
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000039C0 File Offset: 0x00001BC0
		public Stream ResponseStream { get; internal set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000039C9 File Offset: 0x00001BC9
		// (set) Token: 0x060000DD RID: 221 RVA: 0x000039D1 File Offset: 0x00001BD1
		public DateTime? LastModified { get; internal set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000039DA File Offset: 0x00001BDA
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000039E2 File Offset: 0x00001BE2
		public string ETag { get; internal set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000039EB File Offset: 0x00001BEB
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000039F3 File Offset: 0x00001BF3
		public long BytesDownloaded { get; internal set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000039FC File Offset: 0x00001BFC
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00003A04 File Offset: 0x00001C04
		public HttpStatusCode StatusCode { get; internal set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00003A0D File Offset: 0x00001C0D
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00003A15 File Offset: 0x00001C15
		public WebHeaderCollection ResponseHeaders { get; internal set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003A1E File Offset: 0x00001C1E
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00003A26 File Offset: 0x00001C26
		public CookieCollection Cookies { get; internal set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003A2F File Offset: 0x00001C2F
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00003A37 File Offset: 0x00001C37
		public Uri ResponseUri { get; internal set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00003A40 File Offset: 0x00001C40
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00003A48 File Offset: 0x00001C48
		public Uri LastKnownRequestedUri { get; internal set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003A51 File Offset: 0x00001C51
		public bool IsCanceled
		{
			get
			{
				return this.Exception is DownloadCanceledException;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00003A61 File Offset: 0x00001C61
		public bool IsSucceeded
		{
			get
			{
				return this.Exception == null;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00003A6C File Offset: 0x00001C6C
		public bool IsRetryable
		{
			get
			{
				if (this.isRetryable == null)
				{
					this.isRetryable = new bool?(DownloadResult.IsRetryableException(this.Exception));
				}
				return this.isRetryable.Value;
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003A9C File Offset: 0x00001C9C
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
			return this.Exception.GetType().FullName;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003ACC File Offset: 0x00001CCC
		private static bool IsRetryableException(Exception exception)
		{
			if (exception == null || exception is SecurityException || exception is DownloadLimitExceededException || exception is ServerProtocolViolationException || exception is BadRedirectedUriException || exception is UnsupportedUriFormatException)
			{
				return false;
			}
			WebException ex = exception as WebException;
			return ex == null || DownloadResult.IsRetryableWebException(ex);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003B18 File Offset: 0x00001D18
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

		// Token: 0x060000F2 RID: 242 RVA: 0x00003B80 File Offset: 0x00001D80
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

		// Token: 0x0400006E RID: 110
		private bool? isRetryable;
	}
}
