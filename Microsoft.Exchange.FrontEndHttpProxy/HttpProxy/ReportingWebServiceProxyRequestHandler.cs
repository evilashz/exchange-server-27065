using System;
using System.Web;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000CA RID: 202
	internal class ReportingWebServiceProxyRequestHandler : RwsPswsProxyRequestHandlerBase<EcpService>
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0002C86F File Offset: 0x0002AA6F
		protected override string ServiceName
		{
			get
			{
				return "Reporting Web Service";
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0002C876 File Offset: 0x0002AA76
		public static bool IsReportingWebServicePartnerRequest(HttpRequest request)
		{
			return !string.IsNullOrEmpty(request.Url.LocalPath) && request.Url.LocalPath.IndexOf("ReportingWebService/partner/", StringComparison.OrdinalIgnoreCase) >= 0;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0002C8A8 File Offset: 0x0002AAA8
		protected override void DoProtocolSpecificBeginProcess()
		{
			base.DoProtocolSpecificBeginProcess();
			if (!ReportingWebServiceProxyRequestHandler.IsReportingWebServicePartnerRequest(base.HttpContext.Request))
			{
				string domain;
				if (base.TryGetTenantDomain("DelegatedOrg", out domain))
				{
					base.IsDomainBasedRequest = true;
					base.Domain = domain;
				}
				return;
			}
			string domain2;
			if (base.TryGetTenantDomain("tenantDomain", out domain2))
			{
				base.IsDomainBasedRequest = true;
				base.Domain = domain2;
				return;
			}
			ExTraceGlobals.VerboseTracer.TraceError<int>((long)this.GetHashCode(), "[ReportingWebServiceProxyRequestHandler::DoProtocolSpecificBeginProcess]: Context {0}; TenantDomain parameter isn't specified in the request URL.", base.TraceContext);
			throw new HttpException(400, "TenantDomain parameter isn't specified in the request URL.");
		}

		// Token: 0x040004B0 RID: 1200
		private const string ReportingWebServicePartnerPathName = "ReportingWebService/partner/";

		// Token: 0x040004B1 RID: 1201
		private const string TenantParameterName = "tenantDomain";
	}
}
