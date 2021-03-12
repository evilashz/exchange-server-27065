using System;
using System.Collections.Generic;
using Microsoft.Exchange.Net.MonitoringWebClient.Ecp.Parsers;
using Microsoft.Exchange.Net.MonitoringWebClient.Owa.Parsers;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp
{
	// Token: 0x020007A3 RID: 1955
	internal class EcpExceptionAnalyzer : BaseExceptionAnalyzer
	{
		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06002776 RID: 10102 RVA: 0x000539E0 File Offset: 0x00051BE0
		protected override bool AffinityCheck
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x000539E4 File Offset: 0x00051BE4
		static EcpExceptionAnalyzer()
		{
			Dictionary<FailureReason, FailingComponent> dictionary = new Dictionary<FailureReason, FailingComponent>();
			dictionary.Add(FailureReason.MissingKeyword, FailingComponent.Ecp);
			dictionary.Add(FailureReason.NameResolution, FailingComponent.Networking);
			dictionary.Add(FailureReason.NetworkConnection, FailingComponent.Networking);
			dictionary.Add(FailureReason.EcpActiveDirectoryErrorResponse, FailingComponent.ActiveDirectory);
			dictionary.Add(FailureReason.EcpErrorPage, FailingComponent.Ecp);
			dictionary.Add(FailureReason.EcpMailboxErrorResponse, FailingComponent.Mailbox);
			dictionary.Add(FailureReason.OwaActiveDirectoryErrorPage, FailingComponent.ActiveDirectory);
			dictionary.Add(FailureReason.OwaErrorPage, FailingComponent.Owa);
			dictionary.Add(FailureReason.OwaMailboxErrorPage, FailingComponent.Mailbox);
			dictionary.Add(FailureReason.RequestTimeout, FailingComponent.EcpDependency);
			dictionary.Add(FailureReason.ConnectionTimeout, FailingComponent.Ecp);
			dictionary.Add(FailureReason.UnexpectedHttpResponseCode, FailingComponent.Ecp);
			dictionary.Add(FailureReason.SslNegotiation, FailingComponent.Networking);
			dictionary.Add(FailureReason.BrokenAffinity, FailingComponent.Networking);
			dictionary.Add(FailureReason.CafeFailure, FailingComponent.Ecp);
			dictionary.Add(FailureReason.CafeActiveDirectoryFailure, FailingComponent.ActiveDirectory);
			dictionary.Add(FailureReason.CafeHighAvailabilityFailure, FailingComponent.Mailbox);
			dictionary.Add(FailureReason.CafeToMailboxNetworkingFailure, FailingComponent.Networking);
			dictionary.Add(FailureReason.CafeTimeoutContactingBackend, FailingComponent.EcpDependency);
			EcpExceptionAnalyzer.FailureMatrix.Add(RequestTarget.Ecp, dictionary);
			dictionary = new Dictionary<FailureReason, FailingComponent>();
			dictionary.Add(FailureReason.MissingKeyword, FailingComponent.LiveIdConsumer);
			dictionary.Add(FailureReason.NameResolution, FailingComponent.LiveIdConsumer);
			dictionary.Add(FailureReason.NetworkConnection, FailingComponent.LiveIdConsumer);
			dictionary.Add(FailureReason.RequestTimeout, FailingComponent.LiveIdConsumer);
			dictionary.Add(FailureReason.ConnectionTimeout, FailingComponent.LiveIdConsumer);
			dictionary.Add(FailureReason.UnexpectedHttpResponseCode, FailingComponent.LiveIdConsumer);
			dictionary.Add(FailureReason.SslNegotiation, FailingComponent.LiveIdConsumer);
			dictionary.Add(FailureReason.BadUserNameOrPassword, FailingComponent.LiveIdConsumer);
			dictionary.Add(FailureReason.AccountLocked, FailingComponent.LiveIdConsumer);
			dictionary.Add(FailureReason.LogonError, FailingComponent.LiveIdConsumer);
			EcpExceptionAnalyzer.FailureMatrix.Add(RequestTarget.LiveIdConsumer, dictionary);
			dictionary = new Dictionary<FailureReason, FailingComponent>();
			dictionary.Add(FailureReason.MissingKeyword, FailingComponent.LiveIdBusiness);
			dictionary.Add(FailureReason.NameResolution, FailingComponent.LiveIdBusiness);
			dictionary.Add(FailureReason.NetworkConnection, FailingComponent.LiveIdBusiness);
			dictionary.Add(FailureReason.RequestTimeout, FailingComponent.LiveIdBusiness);
			dictionary.Add(FailureReason.ConnectionTimeout, FailingComponent.LiveIdBusiness);
			dictionary.Add(FailureReason.UnexpectedHttpResponseCode, FailingComponent.LiveIdBusiness);
			dictionary.Add(FailureReason.SslNegotiation, FailingComponent.LiveIdBusiness);
			dictionary.Add(FailureReason.BadUserNameOrPassword, FailingComponent.LiveIdBusiness);
			dictionary.Add(FailureReason.AccountLocked, FailingComponent.LiveIdBusiness);
			dictionary.Add(FailureReason.LogonError, FailingComponent.LiveIdBusiness);
			EcpExceptionAnalyzer.FailureMatrix.Add(RequestTarget.LiveIdBusiness, dictionary);
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x00053B80 File Offset: 0x00051D80
		public EcpExceptionAnalyzer(Dictionary<string, RequestTarget> hostNameSourceMapping) : base(hostNameSourceMapping)
		{
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x00053B8C File Offset: 0x00051D8C
		public override HttpWebResponseWrapperException VerifyResponse(HttpWebRequestWrapper request, HttpWebResponseWrapper response, CafeErrorPageValidationRules cafeErrorPageValidationRules)
		{
			EcpErrorResponse ecpErrorResponse;
			if (EcpErrorResponse.TryParse(response, out ecpErrorResponse))
			{
				return new EcpErrorResponseException(MonitoringWebClientStrings.EcpErrorPage, request, response, ecpErrorResponse);
			}
			OwaErrorPage owaErrorPage;
			if (OwaErrorPage.TryParse(response, out owaErrorPage))
			{
				return new OwaErrorPageException(MonitoringWebClientStrings.OwaErrorPage, request, response, owaErrorPage);
			}
			HttpWebResponseWrapperException ex = base.VerifyResponse(request, response, cafeErrorPageValidationRules);
			if (ex != null)
			{
				return ex;
			}
			return null;
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x00053BDC File Offset: 0x00051DDC
		protected override FailureReason GetFailureReason(Exception exception, HttpWebRequestWrapper request, HttpWebResponseWrapper response)
		{
			if (exception is UnexpectedStatusCodeException)
			{
				EcpErrorResponse ecpErrorResponse;
				if (EcpErrorResponse.TryParse(response, out ecpErrorResponse))
				{
					return ecpErrorResponse.FailureReason;
				}
			}
			else
			{
				if (exception is EcpErrorResponseException)
				{
					return (exception as EcpErrorResponseException).EcpErrorResponse.FailureReason;
				}
				if (exception is OwaErrorPageException)
				{
					return (exception as OwaErrorPageException).OwaErrorPage.FailureReason;
				}
			}
			return base.GetFailureReason(exception, request, response);
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x0600277B RID: 10107 RVA: 0x00053C3D File Offset: 0x00051E3D
		protected override Dictionary<RequestTarget, Dictionary<FailureReason, FailingComponent>> ComponentMatrix
		{
			get
			{
				return EcpExceptionAnalyzer.FailureMatrix;
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x0600277C RID: 10108 RVA: 0x00053C44 File Offset: 0x00051E44
		protected override FailingComponent DefaultComponent
		{
			get
			{
				return FailingComponent.Ecp;
			}
		}

		// Token: 0x040023A2 RID: 9122
		private static readonly Dictionary<RequestTarget, Dictionary<FailureReason, FailingComponent>> FailureMatrix = new Dictionary<RequestTarget, Dictionary<FailureReason, FailingComponent>>();
	}
}
