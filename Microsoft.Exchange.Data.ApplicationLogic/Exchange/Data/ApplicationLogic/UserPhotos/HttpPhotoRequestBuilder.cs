using System;
using System.Net;
using System.Web;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001E3 RID: 483
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class HttpPhotoRequestBuilder
	{
		// Token: 0x060011C1 RID: 4545 RVA: 0x0004A8BC File Offset: 0x00048ABC
		public HttpPhotoRequestBuilder(PhotosConfiguration configuration, ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			ArgumentValidator.ThrowIfNull("upstreamTracer", upstreamTracer);
			this.configuration = configuration;
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(upstreamTracer);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0004A918 File Offset: 0x00048B18
		public HttpWebRequest Build(Uri ewsUri, PhotoRequest request, IPhotoRequestOutboundWebProxyProvider proxyProvider, bool traceRequest)
		{
			ArgumentValidator.ThrowIfNull("ewsUri", ewsUri);
			ArgumentValidator.ThrowIfInvalidValue<Uri>("ewsUri", ewsUri, (Uri x) => !string.IsNullOrEmpty(x.AbsolutePath));
			ArgumentValidator.ThrowIfNull("request", request);
			ArgumentValidator.ThrowIfNull("proxyProvider", proxyProvider);
			Uri uri = this.CreateUri(ewsUri, request);
			this.tracer.TraceDebug<Uri>((long)this.GetHashCode(), "REQUEST BUILDER: request URI: {0}", uri);
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
			httpWebRequest.Headers.Set(HttpRequestHeader.IfNoneMatch, request.ETag);
			PhotoRequestorHeader.Default.Serialize(request.Requestor, httpWebRequest);
			httpWebRequest.Proxy = proxyProvider.Create();
			if (request.Trace || traceRequest)
			{
				PhotosDiagnostics.Instance.StampGetUserPhotoTraceEnabledHeaders(httpWebRequest);
			}
			return httpWebRequest;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0004A9E4 File Offset: 0x00048BE4
		public PhotoRequest Parse(HttpRequest httpRequest, IPerformanceDataLogger perfLogger)
		{
			ArgumentValidator.ThrowIfNull("httpRequest", httpRequest);
			ArgumentValidator.ThrowIfNull("perfLogger", perfLogger);
			return new PhotoRequest
			{
				ETag = httpRequest.Headers["If-None-Match"],
				PerformanceLogger = perfLogger,
				Preview = this.GetPreviewValue(httpRequest),
				Size = this.GetRequestedPhotoSize(httpRequest),
				TargetSmtpAddress = this.GetRequestedIdentity(httpRequest),
				Requestor = this.DeserializeRequestorFromContext(httpRequest),
				HandlersToSkip = PhotosDiagnostics.Instance.GetHandlersToSkip(httpRequest),
				Trace = PhotosDiagnostics.Instance.ShouldTraceGetUserPhotoRequest(httpRequest)
			};
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0004AA84 File Offset: 0x00048C84
		public Uri CreateUri(Uri ewsUri, string emailAddress)
		{
			ArgumentValidator.ThrowIfNull("ewsUri", ewsUri);
			return new UriBuilder(ewsUri)
			{
				Path = HttpPhotoRequestBuilder.RemoveTrailingSlash(ewsUri.AbsolutePath) + this.configuration.PhotoServiceEndpointRelativeToEwsWithLeadingSlash,
				Query = "email=" + emailAddress
			}.Uri;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0004AADC File Offset: 0x00048CDC
		private Uri CreateUri(Uri ewsUri, PhotoRequest request)
		{
			return new UriBuilder(ewsUri)
			{
				Path = HttpPhotoRequestBuilder.RemoveTrailingSlash(ewsUri.AbsolutePath) + this.configuration.PhotoServiceEndpointRelativeToEwsWithLeadingSlash,
				Query = string.Concat(new string[]
				{
					PhotoSizeArgumentStrings.Get(request.Size),
					"&email=",
					HttpPhotoRequestBuilder.GetTargetEmailAddress(request),
					HttpPhotoRequestBuilder.GetPreviewQueryStringParameter(request),
					HttpPhotoRequestBuilder.GetHandlersToSkipQueryStringParametersWithLeadingAmpersand(request)
				})
			}.Uri;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0004AB5C File Offset: 0x00048D5C
		private bool GetPreviewValue(HttpRequest request)
		{
			string text = request.QueryString["isPreview"];
			if (text == null)
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "REQUEST BUILDER: photo preview not requested");
				return false;
			}
			this.tracer.TraceDebug<string>((long)this.GetHashCode(), "REQUEST BUILDER: photo preview request value {0}", text);
			return true;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0004ABAF File Offset: 0x00048DAF
		private string GetRequestedIdentity(HttpRequest request)
		{
			return request.QueryString["email"];
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0004ABC4 File Offset: 0x00048DC4
		private UserPhotoSize GetRequestedPhotoSize(HttpRequest request)
		{
			string text = request.QueryString["size"];
			if (string.IsNullOrEmpty(text))
			{
				this.tracer.TraceDebug<UserPhotoSize>((long)this.GetHashCode(), "REQUEST BUILDER: request did not specify a photo size.  Using default: {0}", UserPhotoSize.HR96x96);
				return UserPhotoSize.HR96x96;
			}
			UserPhotoSize result;
			if (!UserPhotoSizeUppercaseStrings.TryMapStringToSize(text, out result))
			{
				this.tracer.TraceDebug<string, UserPhotoSize>((long)this.GetHashCode(), "REQUEST BUILDER: photo size {0} not recognized.  Using default: {1}", text, UserPhotoSize.HR96x96);
				return UserPhotoSize.HR96x96;
			}
			return result;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0004AC2A File Offset: 0x00048E2A
		private PhotoPrincipal DeserializeRequestorFromContext(HttpRequest request)
		{
			return PhotoRequestorHeader.Default.Deserialize(request, this.tracer);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0004AC3D File Offset: 0x00048E3D
		private static string RemoveTrailingSlash(string path)
		{
			if (path.EndsWith("/", StringComparison.OrdinalIgnoreCase))
			{
				return path.Substring(0, path.Length - 1);
			}
			return path;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0004AC5E File Offset: 0x00048E5E
		private static string GetPreviewQueryStringParameter(PhotoRequest request)
		{
			if (request.Preview)
			{
				return "&isPreview=true";
			}
			return string.Empty;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0004AC73 File Offset: 0x00048E73
		private static string GetTargetEmailAddress(PhotoRequest request)
		{
			if (!string.IsNullOrEmpty(request.TargetPrimarySmtpAddress))
			{
				return request.TargetPrimarySmtpAddress;
			}
			return request.TargetSmtpAddress;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0004AC8F File Offset: 0x00048E8F
		private static string GetHandlersToSkipQueryStringParametersWithLeadingAmpersand(PhotoRequest request)
		{
			return PhotosDiagnostics.Instance.GetHandlersToSkipQueryStringParametersWithLeadingAmpersand(request);
		}

		// Token: 0x0400096E RID: 2414
		internal const string SizeParameterName = "size";

		// Token: 0x0400096F RID: 2415
		private const string PreviewParameterName = "isPreview";

		// Token: 0x04000970 RID: 2416
		private const string IfNoneMatchHeader = "If-None-Match";

		// Token: 0x04000971 RID: 2417
		private const UserPhotoSize DefaultPhotoSize = UserPhotoSize.HR96x96;

		// Token: 0x04000972 RID: 2418
		private const string EmailParameterName = "email";

		// Token: 0x04000973 RID: 2419
		private const string EmailParameterWithTrailingEqualsSign = "email=";

		// Token: 0x04000974 RID: 2420
		private const string EmailParameterWithLeadingAmpersandAndTrailingEqualsSign = "&email=";

		// Token: 0x04000975 RID: 2421
		private const string PreviewQueryParameterString = "&isPreview=true";

		// Token: 0x04000976 RID: 2422
		private readonly ITracer tracer = ExTraceGlobals.UserPhotosTracer;

		// Token: 0x04000977 RID: 2423
		private readonly PhotosConfiguration configuration;
	}
}
