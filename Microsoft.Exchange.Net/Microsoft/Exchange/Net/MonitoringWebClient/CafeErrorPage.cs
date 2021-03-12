using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Net.MonitoringWebClient.Owa.Parsers;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000788 RID: 1928
	internal class CafeErrorPage
	{
		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x000507FB File Offset: 0x0004E9FB
		// (set) Token: 0x0600264C RID: 9804 RVA: 0x00050803 File Offset: 0x0004EA03
		public FailureReason FailureReason { get; private set; }

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x0005080C File Offset: 0x0004EA0C
		// (set) Token: 0x0600264E RID: 9806 RVA: 0x00050814 File Offset: 0x0004EA14
		public RequestFailureContext RequestFailureContext { get; private set; }

		// Token: 0x0600264F RID: 9807 RVA: 0x0005081D File Offset: 0x0004EA1D
		private CafeErrorPage()
		{
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x00050828 File Offset: 0x0004EA28
		public static bool TryParse(HttpWebResponseWrapper response, CafeErrorPageValidationRules cafeErrorPageValidationRules, out CafeErrorPage errorPage)
		{
			RequestFailureContext requestFailureContext;
			if (!RequestFailureContext.TryCreateFromResponseHeaders(response.Headers, out requestFailureContext))
			{
				errorPage = null;
				return false;
			}
			if ((cafeErrorPageValidationRules & CafeErrorPageValidationRules.Accept401Response) == CafeErrorPageValidationRules.Accept401Response && response.StatusCode == HttpStatusCode.Unauthorized)
			{
				errorPage = null;
				return false;
			}
			OwaErrorPage owaErrorPage;
			if (OwaErrorPage.TryParse(response, out owaErrorPage))
			{
				errorPage = null;
				return false;
			}
			FailureReason failureReason = FailureReason.CafeFailure;
			if (requestFailureContext.HttpProxySubErrorCode != null)
			{
				HttpProxySubErrorCode value = requestFailureContext.HttpProxySubErrorCode.Value;
				if (value <= HttpProxySubErrorCode.BackEndRequestTimedOut)
				{
					switch (value)
					{
					case HttpProxySubErrorCode.DirectoryOperationError:
					case HttpProxySubErrorCode.MServOperationError:
					case HttpProxySubErrorCode.ServerDiscoveryError:
						break;
					case HttpProxySubErrorCode.ServerLocatorError:
						goto IL_B7;
					default:
						if (value != HttpProxySubErrorCode.BackEndRequestTimedOut)
						{
							goto IL_10C;
						}
						failureReason = FailureReason.CafeTimeoutContactingBackend;
						goto IL_10C;
					}
				}
				else
				{
					switch (value)
					{
					case HttpProxySubErrorCode.DatabaseNameNotFound:
					case HttpProxySubErrorCode.DatabaseGuidNotFound:
					case HttpProxySubErrorCode.OrganizationMailboxNotFound:
						goto IL_B7;
					default:
						if (value != HttpProxySubErrorCode.BadSamlToken)
						{
							goto IL_10C;
						}
						break;
					}
				}
				failureReason = FailureReason.CafeActiveDirectoryFailure;
				goto IL_10C;
				IL_B7:
				failureReason = FailureReason.CafeHighAvailabilityFailure;
			}
			else if (requestFailureContext.WebExceptionStatus != null)
			{
				if (BaseExceptionAnalyzer.IsNetworkRelatedError(requestFailureContext.WebExceptionStatus.Value))
				{
					failureReason = FailureReason.CafeToMailboxNetworkingFailure;
				}
			}
			else if (requestFailureContext.Error != null && requestFailureContext.Error.IndexOf("NegotiateSecurityContext", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				failureReason = FailureReason.CafeActiveDirectoryFailure;
			}
			IL_10C:
			errorPage = new CafeErrorPage();
			errorPage.FailureReason = failureReason;
			errorPage.RequestFailureContext = requestFailureContext;
			return true;
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x00050959 File Offset: 0x0004EB59
		public override string ToString()
		{
			return string.Format("ErrorPageFailureReason: {0}, RequestFailureContext: {1}", this.FailureReason, this.RequestFailureContext);
		}
	}
}
