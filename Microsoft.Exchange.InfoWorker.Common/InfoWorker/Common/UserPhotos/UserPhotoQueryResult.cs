using System;
using System.Net;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.UserPhotos
{
	// Token: 0x02000146 RID: 326
	internal sealed class UserPhotoQueryResult : BaseQueryResult
	{
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0002671E File Offset: 0x0002491E
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x00026726 File Offset: 0x00024926
		public byte[] UserPhotoBytes { get; private set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0002672F File Offset: 0x0002492F
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x00026737 File Offset: 0x00024937
		public string CacheId { get; private set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00026740 File Offset: 0x00024940
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x00026748 File Offset: 0x00024948
		public HttpStatusCode StatusCode { get; private set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00026751 File Offset: 0x00024951
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x00026759 File Offset: 0x00024959
		public string Expires { get; private set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00026762 File Offset: 0x00024962
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x0002676A File Offset: 0x0002496A
		public bool IsPhotoServedFromADFallback { get; private set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x00026773 File Offset: 0x00024973
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x0002677B File Offset: 0x0002497B
		public string ContentType { get; private set; }

		// Token: 0x060008ED RID: 2285 RVA: 0x00026784 File Offset: 0x00024984
		internal UserPhotoQueryResult(byte[] image, string cacheId, HttpStatusCode code, string expires, string contentType, ITracer upstreamTracer)
		{
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(upstreamTracer);
			this.UserPhotoBytes = ((image == null) ? Array<byte>.Empty : image);
			this.CacheId = cacheId;
			this.StatusCode = code;
			this.Expires = expires;
			this.IsPhotoServedFromADFallback = false;
			this.ContentType = contentType;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x000267EC File Offset: 0x000249EC
		internal UserPhotoQueryResult(LocalizedException exception, ITracer upstreamTracer) : base(exception)
		{
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(upstreamTracer);
			HttpStatusCode httpStatusCode = UserPhotoQueryResult.TranslateExceptionToHttpStatusCode(exception);
			this.tracer.TraceDebug<HttpStatusCode, string>((long)this.GetHashCode(), "Translated exception in query to HTTP {0}.  Exception: {1}", httpStatusCode, (exception != null) ? exception.Message : string.Empty);
			this.StatusCode = httpStatusCode;
			this.Expires = null;
			HttpStatusCode httpStatusCode2 = httpStatusCode;
			if (httpStatusCode2 <= HttpStatusCode.NotModified)
			{
				if (httpStatusCode2 == HttpStatusCode.OK || httpStatusCode2 == HttpStatusCode.NotModified)
				{
					return;
				}
			}
			else if (httpStatusCode2 != HttpStatusCode.NotFound && httpStatusCode2 != HttpStatusCode.InternalServerError)
			{
			}
			if (this.FallbackToADPhoto(exception))
			{
				return;
			}
			this.CacheId = null;
			this.UserPhotoBytes = Array<byte>.Empty;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x000268A4 File Offset: 0x00024AA4
		private static HttpStatusCode TranslateExceptionToHttpStatusCode(Exception e)
		{
			if (e == null)
			{
				return HttpStatusCode.InternalServerError;
			}
			if (e is UserPhotoNotFoundException)
			{
				return HttpStatusCode.NotFound;
			}
			if (e is MailRecipientNotFoundException)
			{
				return HttpStatusCode.NotFound;
			}
			if (e is ProxyServerWithMinimumRequiredVersionNotFound)
			{
				return HttpStatusCode.NotFound;
			}
			if (e is AccessDeniedException)
			{
				return HttpStatusCode.Forbidden;
			}
			if (e is NoFreeBusyAccessException)
			{
				return HttpStatusCode.Forbidden;
			}
			if (e is WebException)
			{
				return UserPhotoQueryResult.GetHttpStatusCodeFromWebException((WebException)e);
			}
			if (e is ProxyWebRequestProcessingException)
			{
				return UserPhotoQueryResult.GetHttpStatusCodeFromProxyWebRequestProcessingException((ProxyWebRequestProcessingException)e);
			}
			return HttpStatusCode.InternalServerError;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00026930 File Offset: 0x00024B30
		private static HttpStatusCode GetHttpStatusCodeFromWebException(WebException e)
		{
			HttpWebResponse httpWebResponse = e.Response as HttpWebResponse;
			if (httpWebResponse == null)
			{
				return HttpStatusCode.InternalServerError;
			}
			return httpWebResponse.StatusCode;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00026958 File Offset: 0x00024B58
		private static HttpStatusCode GetHttpStatusCodeFromProxyWebRequestProcessingException(ProxyWebRequestProcessingException e)
		{
			if (e == null)
			{
				return HttpStatusCode.InternalServerError;
			}
			if (e.InnerException is WebException)
			{
				return UserPhotoQueryResult.GetHttpStatusCodeFromWebException((WebException)e.InnerException);
			}
			if (e.InnerException is AddressSpaceNotFoundException)
			{
				return HttpStatusCode.NotFound;
			}
			return HttpStatusCode.InternalServerError;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x000269A4 File Offset: 0x00024BA4
		private bool FallbackToADPhoto(Exception e)
		{
			byte[] thumbnailPhoto = this.GetThumbnailPhoto(e);
			if (thumbnailPhoto == null || thumbnailPhoto.Length == 0)
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "AD photo fall-back not possible: no AD photo.");
				this.IsPhotoServedFromADFallback = false;
				return false;
			}
			this.tracer.TraceDebug((long)this.GetHashCode(), "AD photo fall-back successful.");
			this.UserPhotoBytes = thumbnailPhoto;
			this.CacheId = null;
			this.StatusCode = HttpStatusCode.OK;
			this.Expires = null;
			this.IsPhotoServedFromADFallback = true;
			return true;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00026A20 File Offset: 0x00024C20
		private byte[] GetThumbnailPhoto(Exception e)
		{
			if (!e.Data.Contains("ThumbnailPhotoKey"))
			{
				return null;
			}
			return (byte[])e.Data["ThumbnailPhotoKey"];
		}

		// Token: 0x040006E5 RID: 1765
		private readonly ITracer tracer = ExTraceGlobals.UserPhotosTracer;
	}
}
