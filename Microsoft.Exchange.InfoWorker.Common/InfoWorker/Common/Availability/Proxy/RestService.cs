using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using Microsoft.Exchange.Data.ApplicationLogic.UserPhotos;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.SoapWebClient;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x020000CB RID: 203
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class RestService : IService, CertificateValidationManager.IComponent
	{
		// Token: 0x0600050D RID: 1293 RVA: 0x0001670C File Offset: 0x0001490C
		internal RestService(HttpAuthenticator authenticator, WebServiceUri webServiceUri, bool traceRequest, ITracer upstreamTracer)
		{
			this.authenticator = authenticator;
			this.componentId = Globals.CertificateValidationComponentId;
			this.httpHeaders = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.traceRequest = traceRequest;
			this.tracer = upstreamTracer;
			this.targetUri = webServiceUri.Uri;
			CertificateValidationManager.RegisterCallback(this.componentId, new RemoteCertificateValidationCallback(CertificateErrorHandler.CertValidationCallback));
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00016773 File Offset: 0x00014973
		public Dictionary<string, string> HttpHeaders
		{
			get
			{
				return this.httpHeaders;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0001677B File Offset: 0x0001497B
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x00016783 File Offset: 0x00014983
		public CookieContainer CookieContainer { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0001678C File Offset: 0x0001498C
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x00016794 File Offset: 0x00014994
		public IWebProxy Proxy { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x0001679D File Offset: 0x0001499D
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x000167A5 File Offset: 0x000149A5
		public ICredentials Credentials { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x000167AE File Offset: 0x000149AE
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x000167B6 File Offset: 0x000149B6
		public bool EnableDecompression { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x000167BF File Offset: 0x000149BF
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x000167C7 File Offset: 0x000149C7
		public string UserAgent { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x000167D0 File Offset: 0x000149D0
		public string Url
		{
			get
			{
				return this.targetUri.OriginalString;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x000167DD File Offset: 0x000149DD
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x000167E5 File Offset: 0x000149E5
		public int Timeout { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x000167EE File Offset: 0x000149EE
		public bool SupportsProxyAuthentication
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x000167F1 File Offset: 0x000149F1
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x000167F9 File Offset: 0x000149F9
		public RequestTypeHeader requestTypeValue { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00016802 File Offset: 0x00014A02
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x0001680A File Offset: 0x00014A0A
		public RequestServerVersion RequestServerVersionValue { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00016813 File Offset: 0x00014A13
		public int ServiceVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001681A File Offset: 0x00014A1A
		public string GetComponentId()
		{
			return this.componentId;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00016822 File Offset: 0x00014A22
		public void Abort()
		{
			if (this.httpPhotoRequest != null)
			{
				this.httpPhotoRequest.Abort();
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00016837 File Offset: 0x00014A37
		public void Dispose()
		{
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001683C File Offset: 0x00014A3C
		private HttpWebRequest ConfigureRequest(HttpWebRequest request)
		{
			request.CookieContainer = this.CookieContainer;
			request.Proxy = this.Proxy;
			request.Credentials = this.Credentials;
			request.UserAgent = this.UserAgent;
			request.Timeout = this.Timeout;
			if (!string.IsNullOrEmpty(this.componentId))
			{
				CertificateValidationManager.SetComponentId(request, this.componentId);
			}
			return this.ApplyHeadersToRequest(request);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000168A8 File Offset: 0x00014AA8
		private HttpWebRequest ApplyHeadersToRequest(HttpWebRequest request)
		{
			if (this.httpHeaders == null || this.httpHeaders.Count == 0)
			{
				return request;
			}
			foreach (KeyValuePair<string, string> keyValuePair in this.httpHeaders)
			{
				request.Headers.Add(keyValuePair.Key, keyValuePair.Value);
			}
			if (this.requestTypeValue != null)
			{
				request.Headers.Add("RequestType", this.requestTypeValue.RequestType.ToString());
			}
			return request;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00016954 File Offset: 0x00014B54
		private IAsyncResult AuthenticateAndBeginInvoke(WebRequest request, AuthenticateAndExecuteHandler<IAsyncResult> methodToInvoke)
		{
			return this.authenticator.AuthenticateAndExecute<IAsyncResult>(request, methodToInvoke);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00016963 File Offset: 0x00014B63
		public IAsyncResult BeginGetUserAvailability(GetUserAvailabilityRequest request, AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0001696A File Offset: 0x00014B6A
		public GetUserAvailabilityResponse EndGetUserAvailability(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00016971 File Offset: 0x00014B71
		public IAsyncResult BeginGetMailTips(GetMailTipsType getMailTips1, AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00016978 File Offset: 0x00014B78
		public GetMailTipsResponseMessageType EndGetMailTips(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000169A8 File Offset: 0x00014BA8
		public IAsyncResult BeginGetUserPhoto(PhotoRequest request, PhotosConfiguration configuration, AsyncCallback callback, object asyncState)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			HttpWebRequest request2 = new HttpPhotoRequestBuilder(configuration, this.tracer).Build(this.targetUri, request, new PhotoRequestOutboundWebProxyProviderUsingLocalServerConfiguration(this.tracer), this.traceRequest);
			this.httpPhotoRequest = this.ConfigureRequest(request2);
			return this.AuthenticateAndBeginInvoke(this.httpPhotoRequest, () => this.httpPhotoRequest.BeginGetResponse(callback, asyncState));
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00016A38 File Offset: 0x00014C38
		public GetUserPhotoResponseMessageType EndGetUserPhoto(IAsyncResult asyncResult)
		{
			ArgumentValidator.ThrowIfNull("asyncResult", asyncResult);
			GetUserPhotoResponseMessageType result;
			try
			{
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)this.httpPhotoRequest.EndGetResponse(asyncResult))
				{
					this.WriteTracesCollectedByRemoteServerOntoLocalTracer(httpWebResponse);
					HttpStatusCode statusCode = httpWebResponse.StatusCode;
					if (statusCode <= HttpStatusCode.NotModified)
					{
						if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.NotModified)
						{
							goto IL_98;
						}
					}
					else if (statusCode == HttpStatusCode.NotFound)
					{
						goto IL_98;
					}
					this.tracer.TraceError<HttpStatusCode, string>((long)this.GetHashCode(), "REST service proxy: request to remote service FAILED.  Returning HTTP {0}: {1}", httpWebResponse.StatusCode, httpWebResponse.StatusDescription);
					return new GetUserPhotoResponseMessageType
					{
						StatusCode = httpWebResponse.StatusCode
					};
					IL_98:
					using (Stream responseStream = httpWebResponse.GetResponseStream())
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							string text = httpWebResponse.Headers[HttpResponseHeader.ETag];
							string text2 = httpWebResponse.Headers[HttpResponseHeader.Expires];
							this.tracer.TraceDebug((long)this.GetHashCode(), "REST service proxy: returning photo from proxy.  HTTP status: {0};  ETag: {1};  Expires: {2};  Content-Length: {3};  Content-Type: {4}", new object[]
							{
								httpWebResponse.StatusCode,
								text,
								text2,
								httpWebResponse.ContentLength,
								httpWebResponse.ContentType
							});
							responseStream.CopyTo(memoryStream);
							result = new GetUserPhotoResponseMessageType
							{
								StatusCode = httpWebResponse.StatusCode,
								CacheId = text,
								Expires = text2,
								HasChanged = (httpWebResponse.StatusCode != HttpStatusCode.NotModified),
								PictureData = memoryStream.ToArray(),
								ContentType = httpWebResponse.ContentType
							};
						}
					}
				}
			}
			catch (WebException ex)
			{
				this.WriteTracesCollectedByRemoteServerOntoLocalTracer(ex.Response);
				HttpStatusCode httpStatusCodeFromWebException = RestService.GetHttpStatusCodeFromWebException(ex);
				this.tracer.TraceDebug<HttpStatusCode>((long)this.GetHashCode(), "REST service proxy: caught WebException and translated it to HTTP {0}", httpStatusCodeFromWebException);
				HttpStatusCode httpStatusCode = httpStatusCodeFromWebException;
				if (httpStatusCode != HttpStatusCode.NotModified)
				{
					if (httpStatusCode != HttpStatusCode.NotFound)
					{
						if (httpStatusCode != HttpStatusCode.InternalServerError)
						{
							throw;
						}
						result = new GetUserPhotoResponseMessageType
						{
							StatusCode = HttpStatusCode.InternalServerError
						};
					}
					else
					{
						result = new GetUserPhotoResponseMessageType
						{
							Expires = RestService.GetHeaderValueFromWebException(ex, HttpResponseHeader.Expires),
							StatusCode = HttpStatusCode.NotFound
						};
					}
				}
				else
				{
					result = new GetUserPhotoResponseMessageType
					{
						StatusCode = HttpStatusCode.NotModified,
						CacheId = RestService.GetHeaderValueFromWebException(ex, HttpResponseHeader.ETag),
						Expires = RestService.GetHeaderValueFromWebException(ex, HttpResponseHeader.Expires),
						HasChanged = false
					};
				}
			}
			return result;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00016D24 File Offset: 0x00014F24
		private static HttpStatusCode GetHttpStatusCodeFromWebException(WebException e)
		{
			HttpWebResponse httpWebResponse = e.Response as HttpWebResponse;
			if (httpWebResponse == null)
			{
				return (HttpStatusCode)0;
			}
			return httpWebResponse.StatusCode;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00016D48 File Offset: 0x00014F48
		private static string GetHeaderValueFromWebException(WebException e, HttpResponseHeader header)
		{
			if (e == null || e.Response == null)
			{
				return null;
			}
			return e.Response.Headers[header];
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00016D68 File Offset: 0x00014F68
		private void WriteTracesCollectedByRemoteServerOntoLocalTracer(WebResponse response)
		{
			string text = PhotosDiagnostics.Instance.ReadGetUserPhotoTracesFromResponse(response);
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			this.tracer.TraceDebug<Uri, string>((long)this.GetHashCode(), "Traces collected by {0}: [[ {1} ]]", response.ResponseUri, text);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00016DA8 File Offset: 0x00014FA8
		public IAsyncResult BeginFindMessageTrackingReport(FindMessageTrackingReportRequestType findMessageTrackingReport1, AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00016DAF File Offset: 0x00014FAF
		public FindMessageTrackingReportResponseMessageType EndFindMessageTrackingReport(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00016DB6 File Offset: 0x00014FB6
		public IAsyncResult BeginGetMessageTrackingReport(GetMessageTrackingReportRequestType getMessageTrackingReport1, AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00016DBD File Offset: 0x00014FBD
		public GetMessageTrackingReportResponseMessageType EndGetMessageTrackingReport(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040002FA RID: 762
		private const string ProxyRequestTypeHeaderName = "RequestType";

		// Token: 0x040002FB RID: 763
		private readonly ITracer tracer;

		// Token: 0x040002FC RID: 764
		private readonly bool traceRequest;

		// Token: 0x040002FD RID: 765
		private readonly Dictionary<string, string> httpHeaders;

		// Token: 0x040002FE RID: 766
		private readonly string componentId;

		// Token: 0x040002FF RID: 767
		private readonly HttpAuthenticator authenticator;

		// Token: 0x04000300 RID: 768
		private readonly Uri targetUri;

		// Token: 0x04000301 RID: 769
		private HttpWebRequest httpPhotoRequest;
	}
}
