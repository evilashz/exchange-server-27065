using System;
using System.Collections.Generic;
using Microsoft.Exchange.Net.MonitoringWebClient.Rws.Parsers;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Rws
{
	// Token: 0x020007F0 RID: 2032
	internal class RwsExceptionAnalyzer : BaseExceptionAnalyzer
	{
		// Token: 0x06002A92 RID: 10898 RVA: 0x0005CC78 File Offset: 0x0005AE78
		static RwsExceptionAnalyzer()
		{
			Dictionary<FailureReason, FailingComponent> dictionary = new Dictionary<FailureReason, FailingComponent>();
			dictionary.Add(FailureReason.NameResolution, FailingComponent.Networking);
			dictionary.Add(FailureReason.RwsActiveDirectoryErrorResponse, FailingComponent.ActiveDirectory);
			dictionary.Add(FailureReason.NetworkConnection, FailingComponent.Rws);
			dictionary.Add(FailureReason.RequestTimeout, FailingComponent.Rws);
			dictionary.Add(FailureReason.RwsError, FailingComponent.Rws);
			dictionary.Add(FailureReason.UnexpectedHttpResponseCode, FailingComponent.Rws);
			dictionary.Add(FailureReason.RwsDataMartErrorResponse, FailingComponent.DataMart);
			RwsExceptionAnalyzer.FailureMatrix.Add(RequestTarget.Rws, dictionary);
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x0005CCE1 File Offset: 0x0005AEE1
		public RwsExceptionAnalyzer(Dictionary<string, RequestTarget> hostNameSourceMapping) : base(hostNameSourceMapping)
		{
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x0005CCEC File Offset: 0x0005AEEC
		public override HttpWebResponseWrapperException VerifyResponse(HttpWebRequestWrapper request, HttpWebResponseWrapper response, CafeErrorPageValidationRules cafeErrorPageValidationRules)
		{
			RwsErrorResponse rwsErrorResponse;
			if (RwsErrorResponse.TryParse(response, out rwsErrorResponse))
			{
				return new RwsErrorResponseException("The response contained an RWS error", request, response, rwsErrorResponse);
			}
			return null;
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x0005CD14 File Offset: 0x0005AF14
		protected override FailureReason GetFailureReason(Exception exception, HttpWebRequestWrapper request, HttpWebResponseWrapper response)
		{
			if (exception is UnexpectedStatusCodeException)
			{
				RwsErrorResponse rwsErrorResponse;
				if (RwsErrorResponse.TryParse(response, out rwsErrorResponse))
				{
					return rwsErrorResponse.FailureReason;
				}
			}
			else if (exception is RwsErrorResponseException)
			{
				return (exception as RwsErrorResponseException).RwsErrorResponse.FailureReason;
			}
			return base.GetFailureReason(exception, request, response);
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06002A96 RID: 10902 RVA: 0x0005CD5C File Offset: 0x0005AF5C
		protected override Dictionary<RequestTarget, Dictionary<FailureReason, FailingComponent>> ComponentMatrix
		{
			get
			{
				return RwsExceptionAnalyzer.FailureMatrix;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06002A97 RID: 10903 RVA: 0x0005CD63 File Offset: 0x0005AF63
		protected override FailingComponent DefaultComponent
		{
			get
			{
				return FailingComponent.Rws;
			}
		}

		// Token: 0x04002545 RID: 9541
		private static readonly Dictionary<RequestTarget, Dictionary<FailureReason, FailingComponent>> FailureMatrix = new Dictionary<RequestTarget, Dictionary<FailureReason, FailingComponent>>();
	}
}
