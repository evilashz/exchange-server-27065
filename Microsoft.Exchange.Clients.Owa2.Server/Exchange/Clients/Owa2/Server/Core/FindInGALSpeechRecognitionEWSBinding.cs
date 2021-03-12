using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.SoapWebClient;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000371 RID: 881
	public sealed class FindInGALSpeechRecognitionEWSBinding : ExchangeServiceBinding
	{
		// Token: 0x06001C62 RID: 7266 RVA: 0x000714E0 File Offset: 0x0006F6E0
		public FindInGALSpeechRecognitionEWSBinding(SmtpAddress orgMboxSmtpAddress, Uri serviceUri, object state) : base("FindInGALSpeechRecognition", new RemoteCertificateValidationCallback(FindInGALSpeechRecognitionEWSBinding.CertificateErrorHandler))
		{
			ValidateArgument.NotNull(orgMboxSmtpAddress, "orgMboxSmtpAddress");
			ValidateArgument.NotNull(serviceUri, "serviceUri");
			ValidateArgument.NotNull(state, "state");
			base.Url = serviceUri.ToString();
			this.RequestServerVersionValue = FindInGALSpeechRecognitionEWSBinding.RequestServerVersion;
			base.UserAgent = "FindInGALSpeechRecognition";
			base.Proxy = new WebProxy();
			base.Authenticator = SoapHttpClientAuthenticator.CreateNetworkService();
			base.Authenticator.AdditionalSoapHeaders.Add(new OpenAsAdminOrSystemServiceType
			{
				ConnectingSID = new ConnectingSIDType
				{
					Item = new SmtpAddressType
					{
						Value = orgMboxSmtpAddress.ToString()
					}
				},
				LogonType = SpecialLogonType.SystemService,
				BudgetType = 2,
				BudgetTypeSpecified = true
			});
			this.State = state;
			NetworkServiceImpersonator.Initialize();
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x000715C8 File Offset: 0x0006F7C8
		// (set) Token: 0x06001C64 RID: 7268 RVA: 0x000715D0 File Offset: 0x0006F7D0
		public object State { get; private set; }

		// Token: 0x06001C65 RID: 7269 RVA: 0x000715DC File Offset: 0x0006F7DC
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/StartFindInGALSpeechRecognition", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("StartFindInGALSpeechRecognitionResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public StartFindInGALSpeechRecognitionResponseMessageType StartFindInGALSpeechRecognition([XmlElement("StartFindInGALSpeechRecognition", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] StartFindInGALSpeechRecognitionType request)
		{
			object[] array = this.Invoke("StartFindInGALSpeechRecognition", new object[]
			{
				request
			});
			return (StartFindInGALSpeechRecognitionResponseMessageType)array[0];
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x0007160C File Offset: 0x0006F80C
		public IAsyncResult BeginStartFindInGALSpeechRecognition(StartFindInGALSpeechRecognitionType request, AsyncCallback callback, object asyncState)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MobileSpeechRecoClientStartFindInGALRequestParams, null, new object[]
			{
				request.Culture,
				request.TimeZone,
				request.UserObjectGuid,
				request.TenantGuid,
				base.Url
			});
			return base.BeginInvoke("StartFindInGALSpeechRecognition", new object[]
			{
				request
			}, callback, asyncState);
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x00071678 File Offset: 0x0006F878
		public StartFindInGALSpeechRecognitionResponseMessageType EndStartFindInGALSpeechRecognition(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			StartFindInGALSpeechRecognitionResponseMessageType startFindInGALSpeechRecognitionResponseMessageType = (StartFindInGALSpeechRecognitionResponseMessageType)array[0];
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MobileSpeechRecoClientFindInGALResult, null, new object[]
			{
				startFindInGALSpeechRecognitionResponseMessageType.ResponseCode,
				(startFindInGALSpeechRecognitionResponseMessageType.RecognitionId != null) ? startFindInGALSpeechRecognitionResponseMessageType.RecognitionId.RequestId : string.Empty
			});
			return startFindInGALSpeechRecognitionResponseMessageType;
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x000716DC File Offset: 0x0006F8DC
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CompleteFindInGALSpeechRecognition", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("CompleteFindInGALSpeechRecognitionResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CompleteFindInGALSpeechRecognitionResponseMessageType CompleteFindInGALSpeechRecognition([XmlElement("CompleteFindInGALSpeechRecognition", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CompleteFindInGALSpeechRecognitionType request)
		{
			object[] array = this.Invoke("CompleteFindInGALSpeechRecognition", new object[]
			{
				request
			});
			return (CompleteFindInGALSpeechRecognitionResponseMessageType)array[0];
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x0007170C File Offset: 0x0006F90C
		public IAsyncResult BeginCompleteFindInGALSpeechRecognition(CompleteFindInGALSpeechRecognitionType request, AsyncCallback callback, object asyncState)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MobileSpeechRecoClientCompleteFindInGALRequestParams, null, new object[]
			{
				request.RecognitionId.RequestId,
				request.AudioData.Length,
				base.Url
			});
			return base.BeginInvoke("CompleteFindInGALSpeechRecognition", new object[]
			{
				request
			}, callback, asyncState);
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x00071774 File Offset: 0x0006F974
		public CompleteFindInGALSpeechRecognitionResponseMessageType EndCompleteFindInGALSpeechRecognition(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			CompleteFindInGALSpeechRecognitionResponseMessageType completeFindInGALSpeechRecognitionResponseMessageType = (CompleteFindInGALSpeechRecognitionResponseMessageType)array[0];
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MobileSpeechRecoClientFindInGALResult, null, new object[]
			{
				completeFindInGALSpeechRecognitionResponseMessageType.ResponseCode,
				(completeFindInGALSpeechRecognitionResponseMessageType.RecognitionResult != null) ? CommonUtil.ToEventLogString(completeFindInGALSpeechRecognitionResponseMessageType.RecognitionResult.Result) : string.Empty
			});
			return completeFindInGALSpeechRecognitionResponseMessageType;
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x000717DC File Offset: 0x0006F9DC
		private static bool CertificateErrorHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			if (SslConfiguration.AllowInternalUntrustedCerts)
			{
				ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<string, SslPolicyErrors>((long)sender.GetHashCode(), "FindInGALSpeechRecognitionEwsBinding::CertificateErrorHandler. Allowed SSL certificate {0} with error {1}", certificate.Subject, sslPolicyErrors);
				return true;
			}
			return false;
		}

		// Token: 0x0400100A RID: 4106
		private const string ComponentId = "FindInGALSpeechRecognition";

		// Token: 0x0400100B RID: 4107
		private static readonly RequestServerVersion RequestServerVersion = new RequestServerVersion
		{
			Version = ExchangeVersionType.Exchange2013
		};
	}
}
