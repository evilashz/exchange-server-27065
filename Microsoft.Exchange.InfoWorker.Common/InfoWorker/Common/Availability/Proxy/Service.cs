using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.ApplicationLogic.UserPhotos;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.SoapWebClient;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x020000DA RID: 218
	[XmlInclude(typeof(BaseRequestType))]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "ServiceSoap", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	internal class Service : CustomSoapHttpClientProtocol, IService
	{
		// Token: 0x06000595 RID: 1429 RVA: 0x00018D1F File Offset: 0x00016F1F
		internal Service(WebServiceUri webServiceUri) : base(Globals.CertificateValidationComponentId, new RemoteCertificateValidationCallback(CertificateErrorHandler.CertValidationCallback), true, false)
		{
			base.Url = webServiceUri.Uri.OriginalString;
			this.serviceVersion = webServiceUri.ServerVersion;
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x00018D57 File Offset: 0x00016F57
		internal override XmlNamespaceDefinition[] PredefinedNamespaces
		{
			get
			{
				return Constants.EwsNamespaces;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x00018D5E File Offset: 0x00016F5E
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x00018D66 File Offset: 0x00016F66
		public RequestTypeHeader requestTypeValue
		{
			get
			{
				return this.requestTypeValueField;
			}
			set
			{
				this.requestTypeValueField = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00018D6F File Offset: 0x00016F6F
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x00018D77 File Offset: 0x00016F77
		public RequestServerVersion RequestServerVersionValue
		{
			get
			{
				return this.requestServerVersionValueField;
			}
			set
			{
				this.requestServerVersionValueField = value;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00018D80 File Offset: 0x00016F80
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x00018D88 File Offset: 0x00016F88
		public TimeZoneContext TimeZoneDefinitionContextValue
		{
			get
			{
				return this.timeZoneDefinitionContextValueField;
			}
			set
			{
				this.timeZoneDefinitionContextValueField = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00018D91 File Offset: 0x00016F91
		public int ServiceVersion
		{
			get
			{
				return this.serviceVersion;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00018D99 File Offset: 0x00016F99
		public bool SupportsProxyAuthentication
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00018D9C File Offset: 0x00016F9C
		[SoapHeader("TimeZoneDefinitionContextValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUserAvailability", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("requestTypeValue")]
		[return: XmlElement("GetUserAvailabilityResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUserAvailabilityResponse GetUserAvailability([XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", ElementName = "GetUserAvailabilityRequest")] GetUserAvailabilityRequest getUserAvailabilityRequest)
		{
			object[] array = this.Invoke("GetUserAvailability", new object[]
			{
				getUserAvailabilityRequest
			});
			return (GetUserAvailabilityResponse)array[0];
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00018DCC File Offset: 0x00016FCC
		public IAsyncResult BeginGetUserAvailability(GetUserAvailabilityRequest request, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUserAvailability", new object[]
			{
				request
			}, callback, asyncState);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00018DF4 File Offset: 0x00016FF4
		public GetUserAvailabilityResponse EndGetUserAvailability(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetUserAvailabilityResponse)array[0];
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00018E14 File Offset: 0x00017014
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetMailTips", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("requestTypeValue")]
		[return: XmlElement("GetMailTipsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetMailTipsResponseMessageType GetMailTips([XmlElement("GetMailTips", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetMailTipsType getMailTips1)
		{
			object[] array = this.Invoke("GetMailTips", new object[]
			{
				getMailTips1
			});
			return (GetMailTipsResponseMessageType)array[0];
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00018E44 File Offset: 0x00017044
		public IAsyncResult BeginGetMailTips(GetMailTipsType getMailTips1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetMailTips", new object[]
			{
				getMailTips1
			}, callback, asyncState);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00018E6C File Offset: 0x0001706C
		public GetMailTipsResponseMessageType EndGetMailTips(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetMailTipsResponseMessageType)array[0];
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00018E8C File Offset: 0x0001708C
		internal Service(WebServiceUri webServiceUri, bool traceRequest, ITracer upstreamTracer) : base(Globals.CertificateValidationComponentId, new RemoteCertificateValidationCallback(CertificateErrorHandler.CertValidationCallback), true)
		{
			this.traceRequest = traceRequest;
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(upstreamTracer);
			base.Url = webServiceUri.Uri.OriginalString;
			this.serviceVersion = webServiceUri.ServerVersion;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00018EE8 File Offset: 0x000170E8
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUserPhoto", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("requestTypeValue")]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetUserPhotoResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUserPhotoResponseMessageType GetUserPhoto([XmlElement("GetUserPhoto", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUserPhotoRequestType getUserPhoto1)
		{
			object[] array = this.Invoke("GetUserPhoto", new object[]
			{
				getUserPhoto1
			});
			return (GetUserPhotoResponseMessageType)array[0];
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00018F18 File Offset: 0x00017118
		public IAsyncResult BeginGetUserPhoto(PhotoRequest request, PhotosConfiguration configuration, AsyncCallback callback, object asyncState)
		{
			base.HttpHeaders.Add("If-None-Match", request.ETag);
			if (this.traceRequest)
			{
				PhotosDiagnostics.Instance.StampGetUserPhotoTraceEnabledHeaders(base.HttpHeaders);
			}
			return base.BeginInvoke("GetUserPhoto", new GetUserPhotoRequestType[]
			{
				Service.ConvertPhotoRequestToGetUserPhotoRequestType(request)
			}, callback, asyncState);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00018F74 File Offset: 0x00017174
		public GetUserPhotoResponseMessageType EndGetUserPhoto(IAsyncResult asyncResult)
		{
			object[] array;
			try
			{
				array = base.EndInvoke(asyncResult);
			}
			catch (WebException ex)
			{
				this.WriteTracesCollectedByRemoteServerOntoLocalTracer(PhotosDiagnostics.Instance.ReadGetUserPhotoTracesFromResponse(ex.Response));
				throw;
			}
			GetUserPhotoResponseMessageType getUserPhotoResponseMessageType = (GetUserPhotoResponseMessageType)array[0];
			this.WriteTracesCollectedByRemoteServerOntoLocalTracer(PhotosDiagnostics.Instance.ReadGetUserPhotoTracesFromResponseHeaders(base.ResponseHttpHeaders));
			if (getUserPhotoResponseMessageType.ResponseClass != ResponseClassType.Success)
			{
				this.tracer.TraceError((long)this.GetHashCode(), "Request to remote service FAILED.  Returning HTTP 500: Internal Server Error.");
				getUserPhotoResponseMessageType.StatusCode = HttpStatusCode.InternalServerError;
				return getUserPhotoResponseMessageType;
			}
			if (!getUserPhotoResponseMessageType.HasChanged)
			{
				getUserPhotoResponseMessageType.StatusCode = HttpStatusCode.NotModified;
			}
			else
			{
				getUserPhotoResponseMessageType.StatusCode = HttpStatusCode.OK;
			}
			string text;
			if (!base.ResponseHttpHeaders.TryGetValue("ETag", out text) || string.IsNullOrEmpty(text))
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "Remote service returned NO ETag or ETag is blank.");
			}
			getUserPhotoResponseMessageType.CacheId = text;
			string text2;
			if (!base.ResponseHttpHeaders.TryGetValue("Expires", out text2) || string.IsNullOrEmpty(text2))
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "Remote service returned NO Expires header or header is blank.");
			}
			getUserPhotoResponseMessageType.Expires = text2;
			this.tracer.TraceDebug((long)this.GetHashCode(), "Returning message from proxy with cacheId: {0}; HTTP status: {1};  HTTP Expires: {2};  Content-type: '{3}'", new object[]
			{
				getUserPhotoResponseMessageType.CacheId,
				getUserPhotoResponseMessageType.StatusCode,
				getUserPhotoResponseMessageType.Expires,
				getUserPhotoResponseMessageType.ContentType
			});
			return getUserPhotoResponseMessageType;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x000190E4 File Offset: 0x000172E4
		private void WriteTracesCollectedByRemoteServerOntoLocalTracer(string traces)
		{
			if (string.IsNullOrEmpty(traces))
			{
				return;
			}
			this.tracer.TraceDebug<string, string>((long)this.GetHashCode(), "Traces collected by {0}: [[ {1} ]]", base.Url, traces);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00019110 File Offset: 0x00017310
		private static GetUserPhotoRequestType ConvertPhotoRequestToGetUserPhotoRequestType(PhotoRequest request)
		{
			return new GetUserPhotoRequestType
			{
				Email = request.TargetSmtpAddress,
				SizeRequested = (UserPhotoSize)request.Size
			};
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001913C File Offset: 0x0001733C
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/FindMessageTrackingReport", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("requestTypeValue")]
		[return: XmlElement("FindMessageTrackingReportResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public FindMessageTrackingReportResponseMessageType FindMessageTrackingReport([XmlElement("FindMessageTrackingReport", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] FindMessageTrackingReportRequestType findMessageTrackingReport1)
		{
			object[] array = this.Invoke("FindMessageTrackingReport", new object[]
			{
				findMessageTrackingReport1
			});
			return (FindMessageTrackingReportResponseMessageType)array[0];
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001916C File Offset: 0x0001736C
		public IAsyncResult BeginFindMessageTrackingReport(FindMessageTrackingReportRequestType findMessageTrackingReport1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FindMessageTrackingReport", new object[]
			{
				findMessageTrackingReport1
			}, callback, asyncState);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00019194 File Offset: 0x00017394
		public FindMessageTrackingReportResponseMessageType EndFindMessageTrackingReport(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (FindMessageTrackingReportResponseMessageType)array[0];
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000191B4 File Offset: 0x000173B4
		[SoapHeader("requestTypeValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetMessageTrackingReport", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetMessageTrackingReportResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetMessageTrackingReportResponseMessageType GetMessageTrackingReport([XmlElement("GetMessageTrackingReport", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetMessageTrackingReportRequestType getMessageTrackingReport1)
		{
			object[] array = this.Invoke("GetMessageTrackingReport", new object[]
			{
				getMessageTrackingReport1
			});
			return (GetMessageTrackingReportResponseMessageType)array[0];
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000191E4 File Offset: 0x000173E4
		public IAsyncResult BeginGetMessageTrackingReport(GetMessageTrackingReportRequestType getMessageTrackingReport1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetMessageTrackingReport", new object[]
			{
				getMessageTrackingReport1
			}, callback, asyncState);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001920C File Offset: 0x0001740C
		public GetMessageTrackingReportResponseMessageType EndGetMessageTrackingReport(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetMessageTrackingReportResponseMessageType)array[0];
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00019229 File Offset: 0x00017429
		CookieContainer IService.get_CookieContainer()
		{
			return base.CookieContainer;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00019231 File Offset: 0x00017431
		void IService.set_CookieContainer(CookieContainer A_1)
		{
			base.CookieContainer = A_1;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001923A File Offset: 0x0001743A
		IWebProxy IService.get_Proxy()
		{
			return base.Proxy;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00019242 File Offset: 0x00017442
		void IService.set_Proxy(IWebProxy A_1)
		{
			base.Proxy = A_1;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001924B File Offset: 0x0001744B
		ICredentials IService.get_Credentials()
		{
			return base.Credentials;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00019253 File Offset: 0x00017453
		void IService.set_Credentials(ICredentials A_1)
		{
			base.Credentials = A_1;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001925C File Offset: 0x0001745C
		bool IService.get_EnableDecompression()
		{
			return base.EnableDecompression;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00019264 File Offset: 0x00017464
		void IService.set_EnableDecompression(bool A_1)
		{
			base.EnableDecompression = A_1;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001926D File Offset: 0x0001746D
		string IService.get_UserAgent()
		{
			return base.UserAgent;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00019275 File Offset: 0x00017475
		void IService.set_UserAgent(string A_1)
		{
			base.UserAgent = A_1;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001927E File Offset: 0x0001747E
		string IService.get_Url()
		{
			return base.Url;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00019286 File Offset: 0x00017486
		int IService.get_Timeout()
		{
			return base.Timeout;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001928E File Offset: 0x0001748E
		void IService.set_Timeout(int A_1)
		{
			base.Timeout = A_1;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00019297 File Offset: 0x00017497
		Dictionary<string, string> IService.get_HttpHeaders()
		{
			return base.HttpHeaders;
		}

		// Token: 0x04000351 RID: 849
		private RequestServerVersion requestServerVersionValueField;

		// Token: 0x04000352 RID: 850
		private RequestTypeHeader requestTypeValueField;

		// Token: 0x04000353 RID: 851
		private TimeZoneContext timeZoneDefinitionContextValueField;

		// Token: 0x04000354 RID: 852
		private int serviceVersion;

		// Token: 0x04000355 RID: 853
		private readonly ITracer tracer;

		// Token: 0x04000356 RID: 854
		private readonly bool traceRequest;
	}
}
