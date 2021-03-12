using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.SoapWebClient;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000089 RID: 137
	internal class UMMailboxAccessorEwsBinding : ExchangeServiceBinding
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x00011284 File Offset: 0x0000F484
		public UMMailboxAccessorEwsBinding(IExchangePrincipal user, DiagnosticHelper tracer) : base("UMMailboxAccessor", new RemoteCertificateValidationCallback(UMMailboxAccessorEwsBinding.CertificateErrorHandler))
		{
			UMMailboxAccessorEwsBinding <>4__this = this;
			LatencyStopwatch latencyStopwatch = new LatencyStopwatch();
			this.tracer = tracer;
			Uri uri = latencyStopwatch.Invoke<Uri>("UMMailboxAccessorEwsBinding: BackEndLocator.GetBackEndWebServicesUrl", () => BackEndLocator.GetBackEndWebServicesUrl(user.MailboxInfo));
			if (uri != null)
			{
				base.Url = uri.ToString();
			}
			if (string.IsNullOrEmpty(base.Url))
			{
				throw new EWSUMMailboxAccessException(Strings.EWSUrlDiscoveryFailed(user.MailboxInfo.PrimarySmtpAddress.ToString()));
			}
			this.RequestServerVersionValue = UMMailboxAccessorEwsBinding.RequestServerVersion;
			base.UserAgent = "UMMailboxAccessor";
			base.Proxy = latencyStopwatch.Invoke<WebProxy>("UMMailboxAccessorEwsBinding: new WebProxy", () => new WebProxy());
			base.Authenticator = latencyStopwatch.Invoke<SoapHttpClientAuthenticator>("UMMailboxAccessorEwsBinding: SoapHttpClientAuthenticator.CreateNetworkService", () => SoapHttpClientAuthenticator.CreateNetworkService());
			latencyStopwatch.Invoke<int>("UMMailboxAccessorEwsBinding: Authenticator.AdditionalSoapHeaders.Add", () => <>4__this.Authenticator.AdditionalSoapHeaders.Add(new OpenAsAdminOrSystemServiceType
			{
				ConnectingSID = new ConnectingSIDType
				{
					Item = new SmtpAddressType
					{
						Value = user.MailboxInfo.PrimarySmtpAddress.ToString()
					}
				},
				LogonType = SpecialLogonType.SystemService,
				BudgetType = 2,
				BudgetTypeSpecified = true
			}));
			latencyStopwatch.Invoke("UMMailboxAccessorEwsBinding: NetworkServiceImpersonator.Initialize", delegate()
			{
				NetworkServiceImpersonator.Initialize();
			});
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000113F8 File Offset: 0x0000F5F8
		public static Exception ExecuteEWSOperation(Action function, DiagnosticHelper tracer)
		{
			int num = 0;
			Exception result = null;
			do
			{
				try
				{
					result = null;
					function();
					break;
				}
				catch (Exception ex)
				{
					tracer.Trace("UMMailboxAccessorEwsBinding ExecuteEWSOperation, exception  = {0}", new object[]
					{
						ex
					});
					result = ex;
					if (!UMMailboxAccessorEwsBinding.IsKnownException(ex))
					{
						throw;
					}
				}
			}
			while (++num < 3);
			return result;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00011454 File Offset: 0x0000F654
		private static bool CertificateErrorHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			if (SslConfiguration.AllowInternalUntrustedCerts || Utils.RunningInTestMode)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, sender.GetHashCode(), "UMMailboxAccessorEwsBinding::CertificateErrorHandler. Allowed SSL certificate {0} with error {1}", new object[]
				{
					certificate.Subject,
					sslPolicyErrors
				});
				return true;
			}
			return false;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000114E4 File Offset: 0x0000F6E4
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUMPrompt", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("GetUMPromptResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUMPromptResponseMessageType GetUMPrompt([XmlElement("GetUMPrompt", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUMPromptType request)
		{
			object[] array = this.InvokeWebServiceCommand(() => this.Invoke("GetUMPrompt", new object[]
			{
				request
			}));
			return (GetUMPromptResponseMessageType)array[0];
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00011558 File Offset: 0x0000F758
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUMPromptNames", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetUMPromptNamesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUMPromptNamesResponseMessageType GetUMPromptNames([XmlElement("GetUMPromptNames", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUMPromptNamesType request)
		{
			object[] array = this.InvokeWebServiceCommand(() => this.Invoke("GetUMPromptNames", new object[]
			{
				request
			}));
			return (GetUMPromptNamesResponseMessageType)array[0];
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x000115CC File Offset: 0x0000F7CC
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DeleteUMPrompts", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("DeleteUMPromptsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public DeleteUMPromptsResponseMessageType DeleteUMPrompts([XmlElement("DeleteUMPrompts", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DeleteUMPromptsType request)
		{
			object[] array = this.InvokeWebServiceCommand(() => this.Invoke("DeleteUMPrompts", new object[]
			{
				request
			}));
			return (DeleteUMPromptsResponseMessageType)array[0];
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00011640 File Offset: 0x0000F840
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateUMPrompt", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("CreateUMPromptResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CreateUMPromptResponseMessageType CreateUMPrompt([XmlElement("CreateUMPrompt", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateUMPromptType request)
		{
			object[] array = this.InvokeWebServiceCommand(() => this.Invoke("CreateUMPrompt", new object[]
			{
				request
			}));
			return (CreateUMPromptResponseMessageType)array[0];
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000116B4 File Offset: 0x0000F8B4
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateUMCallDataRecord", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("CreateUMCallDataRecordResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CreateUMCallDataRecordResponseMessageType CreateUMCallDataRecord([XmlElement("CreateUMCallDataRecord", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateUMCallDataRecordType request)
		{
			object[] array = this.InvokeWebServiceCommand(() => this.Invoke("CreateUMCallDataRecord", new object[]
			{
				request
			}));
			return (CreateUMCallDataRecordResponseMessageType)array[0];
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00011728 File Offset: 0x0000F928
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUMCallDataRecords", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetUMCallDataRecordsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUMCallDataRecordsResponseMessageType GetUMCallDataRecords([XmlElement("GetUMCallDataRecords", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUMCallDataRecordsType request)
		{
			object[] array = this.InvokeWebServiceCommand(() => this.Invoke("GetUMCallDataRecords", new object[]
			{
				request
			}));
			return (GetUMCallDataRecordsResponseMessageType)array[0];
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001179C File Offset: 0x0000F99C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUMCallSummary", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("GetUMCallSummaryResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUMCallSummaryResponseMessageType GetUMCallSummary([XmlElement("GetUMCallSummary", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUMCallSummaryType request)
		{
			object[] array = this.InvokeWebServiceCommand(() => this.Invoke("GetUMCallSummary", new object[]
			{
				request
			}));
			return (GetUMCallSummaryResponseMessageType)array[0];
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00011810 File Offset: 0x0000FA10
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/InitUMMailbox", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("InitUMMailboxResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public InitUMMailboxResponseMessageType InitUMMailbox([XmlElement("InitUMMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] InitUMMailboxType request)
		{
			object[] array = this.InvokeWebServiceCommand(() => this.Invoke("InitUMMailbox", new object[]
			{
				request
			}));
			return (InitUMMailboxResponseMessageType)array[0];
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00011884 File Offset: 0x0000FA84
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ResetUMMailbox", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("ResetUMMailboxResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ResetUMMailboxResponseMessageType ResetUMMailbox([XmlElement("ResetUMMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ResetUMMailboxType request)
		{
			object[] array = this.InvokeWebServiceCommand(() => this.Invoke("ResetUMMailbox", new object[]
			{
				request
			}));
			return (ResetUMMailboxResponseMessageType)array[0];
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000118F8 File Offset: 0x0000FAF8
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ValidateUMPin", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("ValidateUMPinResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ValidateUMPinResponseMessageType ValidateUMPin([XmlElement("ValidateUMPin", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ValidateUMPinType request)
		{
			object[] array = this.InvokeWebServiceCommand(() => this.Invoke("ValidateUMPin", new object[]
			{
				request
			}));
			return (ValidateUMPinResponseMessageType)array[0];
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001196C File Offset: 0x0000FB6C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SaveUMPin", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("SaveUMPinResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SaveUMPinResponseMessageType SaveUMPin([XmlElement("SaveUMPin", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SaveUMPinType request)
		{
			object[] array = this.InvokeWebServiceCommand(() => this.Invoke("SaveUMPin", new object[]
			{
				request
			}));
			return (SaveUMPinResponseMessageType)array[0];
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000119E0 File Offset: 0x0000FBE0
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUMPin", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("GetUMPinResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUMPinResponseMessageType GetUMPin([XmlElement("GetUMPin", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUMPinType request)
		{
			object[] array = this.InvokeWebServiceCommand(() => this.Invoke("GetUMPin", new object[]
			{
				request
			}));
			return (GetUMPinResponseMessageType)array[0];
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00011A54 File Offset: 0x0000FC54
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUMSubscriberCallAnsweringData", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetUMSubscriberCallAnsweringDataResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUMSubscriberCallAnsweringDataResponseMessageType GetUMSubscriberCallAnsweringData([XmlElement("GetUMSubscriberCallAnsweringData", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUMSubscriberCallAnsweringDataType request)
		{
			object[] array = this.InvokeWebServiceCommand(() => this.Invoke("GetUMSubscriberCallAnsweringData", new object[]
			{
				request
			}));
			return (GetUMSubscriberCallAnsweringDataResponseMessageType)array[0];
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00011A90 File Offset: 0x0000FC90
		private object[] InvokeWebServiceCommand(Func<object[]> function)
		{
			object[] result = function();
			this.LogHttpResponse();
			return result;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00011AAC File Offset: 0x0000FCAC
		private void LogHttpResponse()
		{
			this.tracer.Trace("Http Response headers for this call.", new object[0]);
			foreach (string text in base.ResponseHttpHeaders.Keys)
			{
				this.tracer.Trace("Key {0} Value {1}", new object[]
				{
					text,
					base.ResponseHttpHeaders[text]
				});
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00011B40 File Offset: 0x0000FD40
		private static bool IsKnownException(Exception e)
		{
			return e is BackEndLocatorException || e is EWSUMMailboxAccessException || e is InvalidOperationException || e is IOException || e is WebException || e is SoapException;
		}

		// Token: 0x04000301 RID: 769
		private const int MaximumTransientFailureRetries = 3;

		// Token: 0x04000302 RID: 770
		private const string ComponentId = "UMMailboxAccessor";

		// Token: 0x04000303 RID: 771
		private readonly DiagnosticHelper tracer;

		// Token: 0x04000304 RID: 772
		private static readonly RequestServerVersion RequestServerVersion = new RequestServerVersion
		{
			Version = ExchangeVersionType.Exchange2013
		};
	}
}
