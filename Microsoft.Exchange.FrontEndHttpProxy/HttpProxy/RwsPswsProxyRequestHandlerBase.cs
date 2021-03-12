using System;
using System.Net;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000C6 RID: 198
	internal abstract class RwsPswsProxyRequestHandlerBase<ServiceType> : BEServerCookieProxyRequestHandler<ServiceType> where ServiceType : HttpService
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060006D8 RID: 1752
		protected abstract string ServiceName { get; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0002BAC1 File Offset: 0x00029CC1
		protected override ClientAccessType ClientAccessType
		{
			get
			{
				return ClientAccessType.Internal;
			}
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0002BAC4 File Offset: 0x00029CC4
		protected override Uri GetTargetBackEndServerUrl()
		{
			Uri targetBackEndServerUrl = base.GetTargetBackEndServerUrl();
			if (base.AnchoredRoutingTarget.BackEndServer.Version < Server.E15MinVersion)
			{
				string arg = Utilities.FormatServerVersion(base.AnchoredRoutingTarget.BackEndServer.Version);
				ExTraceGlobals.VerboseTracer.TraceError<string, AnchoredRoutingTarget, string>((long)this.GetHashCode(), "[RwsPswsProxyRequestHandlerBase::GetTargetBackEndServerUrl]: Backend server doesn't support {0}. Backend server version: {1}; AnchoredRoutingTarget: {2}", arg, base.AnchoredRoutingTarget, this.ServiceName);
				string message = string.Format("The target site (version {0}) doesn't support {1}.", arg, this.ServiceName);
				throw new HttpProxyException(HttpStatusCode.NotFound, HttpProxySubErrorCode.EndpointNotFound, message);
			}
			return targetBackEndServerUrl;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0002BB4C File Offset: 0x00029D4C
		protected bool TryGetTenantDomain(string parameterName, out string tenantDomain)
		{
			tenantDomain = base.HttpContext.Request.QueryString[parameterName];
			if (string.IsNullOrEmpty(tenantDomain))
			{
				return false;
			}
			if (!SmtpAddress.IsValidDomain(tenantDomain))
			{
				ExTraceGlobals.VerboseTracer.TraceError<int, string, string>((long)this.GetHashCode(), "[RwsPswsProxyRequestHandlerBase::TryGetTenantDomain]: Context {0}; TenantDomain parameter is invalid. ParameterName: {1}; Value: {2}.", base.TraceContext, parameterName, tenantDomain);
				string message = string.Format("{0} parameter is invalid.", parameterName);
				throw new HttpException(400, message);
			}
			return true;
		}
	}
}
