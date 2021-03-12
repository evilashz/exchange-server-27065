using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common.SmartCategorization
{
	// Token: 0x02000101 RID: 257
	internal class CafeFailureAnalyser : FailureAnalyserBase
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x0002EEA7 File Offset: 0x0002D0A7
		public static IFailureAnalyser Instance
		{
			get
			{
				return CafeFailureAnalyser.instance;
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0002EEAE File Offset: 0x0002D0AE
		private CafeFailureAnalyser()
		{
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0002EEB8 File Offset: 0x0002D0B8
		protected override FailureDetails AnalyseCafeFailure(RequestFailureContext requestFailureContext)
		{
			FailureDetails result;
			if (requestFailureContext.LiveIdAuthResult != null && this.TryClassifyLiveIdBasicAuthResult(requestFailureContext.LiveIdAuthResult.Value, out result))
			{
				return result;
			}
			if (requestFailureContext.HttpProxySubErrorCode != null && this.TryClassifyHttpProxySubErrorCode(requestFailureContext.HttpProxySubErrorCode.Value, out result))
			{
				return result;
			}
			if (requestFailureContext.HttpStatusCode == 401)
			{
				return new FailureDetails(FailureType.Recognized, ExchangeComponent.Monitoring, SCStrings.FailureMonitoringAccount, HttpStatusCode.Unauthorized.ToString());
			}
			return new FailureDetails(FailureType.Unrecognized, ExchangeComponent.Cafe);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0002EF52 File Offset: 0x0002D152
		protected override FailureDetails AnalyseBackendFailure(RequestFailureContext requestFailureContext)
		{
			if (requestFailureContext.HttpStatusCode == 401)
			{
				return new FailureDetails(FailureType.Recognized, ExchangeComponent.AD, SCStrings.FailureFrontendBackendAuthN, HttpStatusCode.Unauthorized.ToString());
			}
			return base.AnalyseBackendFailure(requestFailureContext);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0002EF88 File Offset: 0x0002D188
		private bool TryClassifyHttpProxySubErrorCode(HttpProxySubErrorCode errorCode, out FailureDetails failureDetails)
		{
			failureDetails = null;
			switch (errorCode)
			{
			case HttpProxySubErrorCode.DirectoryOperationError:
				failureDetails = new FailureDetails(FailureType.Recognized, ExchangeComponent.AD, SCStrings.FailureActiveDirectory, errorCode.ToString());
				return true;
			case HttpProxySubErrorCode.MServOperationError:
			case HttpProxySubErrorCode.ServerDiscoveryError:
			case HttpProxySubErrorCode.ServerLocatorError:
				failureDetails = new FailureDetails(FailureType.Recognized, ExchangeComponent.Gls, SCStrings.FailureServerLocator, errorCode.ToString());
				return true;
			default:
				return false;
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0002EFF8 File Offset: 0x0002D1F8
		private bool TryClassifyLiveIdBasicAuthResult(LiveIdAuthResult authResult, out FailureDetails failureDetails)
		{
			failureDetails = null;
			switch (authResult)
			{
			case LiveIdAuthResult.UserNotFoundInAD:
			case LiveIdAuthResult.AuthFailure:
			case LiveIdAuthResult.ExpiredCreds:
			case LiveIdAuthResult.InvalidCreds:
			case LiveIdAuthResult.RecoverableAuthFailure:
			case LiveIdAuthResult.AmbigiousMailboxFoundFailure:
				failureDetails = new FailureDetails(FailureType.Recognized, ExchangeComponent.Monitoring, string.Format(SCStrings.FailureMonitoringAccount, authResult.ToString()), authResult.ToString());
				return true;
			case LiveIdAuthResult.LiveServerUnreachable:
			case LiveIdAuthResult.FederatedStsUnreachable:
			case LiveIdAuthResult.OperationTimedOut:
			case LiveIdAuthResult.CommunicationFailure:
				failureDetails = new FailureDetails(FailureType.Recognized, ExchangeComponent.LiveId, string.Format(SCStrings.FailureLiveId, authResult.ToString()), authResult.ToString());
				return true;
			}
			return false;
		}

		// Token: 0x04000552 RID: 1362
		private static IFailureAnalyser instance = new CafeFailureAnalyser();
	}
}
