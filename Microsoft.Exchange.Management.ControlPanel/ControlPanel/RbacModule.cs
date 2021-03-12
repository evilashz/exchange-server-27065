using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003A4 RID: 932
	internal sealed class RbacModule : IHttpModule
	{
		// Token: 0x06003140 RID: 12608 RVA: 0x0009739C File Offset: 0x0009559C
		static RbacModule()
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
			string configStringValue;
			if (Util.IsDataCenter)
			{
				configStringValue = AppConfigLoader.GetConfigStringValue("IframeablePagesForDataCenter", null);
			}
			else
			{
				configStringValue = AppConfigLoader.GetConfigStringValue("IframeablePagesForOnPremises", null);
			}
			if (!string.IsNullOrEmpty(configStringValue))
			{
				if (configStringValue == "*")
				{
					RbacModule.bypassXFrameOptions = true;
				}
				else
				{
					RbacModule.bypassXFrameOptions = false;
					foreach (string item in configStringValue.Split(new char[]
					{
						','
					}))
					{
						hashSet.Add(item);
					}
				}
			}
			RbacModule.xFrameOptionsExceptionList = hashSet;
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x00097474 File Offset: 0x00095674
		public void Init(HttpApplication application)
		{
			lock (RbacModule.initSync)
			{
				if (!RbacModule.initialized)
				{
					RbacModule.initialized = true;
					RbacModule.RegisterQueryProcessors();
				}
			}
			application.PostAuthenticateRequest += this.Application_PostAuthenticateRequest;
			application.EndRequest += this.Application_EndRequest;
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x00097544 File Offset: 0x00095744
		internal static void RegisterQueryProcessors()
		{
			OrganizationCacheQueryProcessor<bool>.XPremiseEnt.Register();
			OrganizationCacheQueryProcessor<bool>.XPremiseDC.Register();
			RbacQuery.RegisterQueryProcessor("ClosedCampus", new ClosedCampusQueryProcessor());
			RbacQuery.RegisterQueryProcessor("OrgHasManagedDomains", new AcceptedManagedDomainsQueryProcessor());
			RbacQuery.RegisterQueryProcessor("OrgHasFederatedDomains", new AcceptedFederatedDomainsQueryProcessor());
			RbacQuery.RegisterQueryProcessor("SendAddressAvailable", new SendAddressAvailableQueryProcessor());
			RbacQuery.RegisterQueryProcessor("PopImapDisabled", new PopImapDisabledQueryProcessor());
			RbacQuery.RegisterQueryProcessor("IPSafelistingEhfSyncEnabledRole", new IPSafelistingEhfSyncEnabledQueryProcessor());
			RbacQuery.RegisterQueryProcessor("IPSafelistingSmpEnabledRole", new IPSafelistingSmpEnabledQueryProcessor());
			RbacQuery.RegisterQueryProcessor("EhfAdminCenterEnabledRole", new EhfAdminCenterEnabledQueryProcessor());
			RbacQuery.RegisterQueryProcessor("BusinessLiveId", LiveIdInstanceQueryProcessor.BusinessLiveId);
			RbacQuery.RegisterQueryProcessor("ConsumerLiveId", LiveIdInstanceQueryProcessor.ConsumerLiveId);
			RbacQuery.RegisterQueryProcessor("IsDehydrated", new IsDehydratedQueryProcessor());
			RbacQuery.RegisterQueryProcessor("SoftDeletedFeatureEnabled", new SoftDeletedFeatureStatusQueryProcessor());
			RbacQuery.RegisterQueryProcessor("OfficeStoreAvailable", new OfficeStoreAvailableQueryProcessor());
			foreach (EcpFeature ecpFeature in ClientRbac.HybridEcpFeatures)
			{
				new EcpFeatureQueryProcessor(ecpFeature).Register();
			}
			new AliasQueryProcessor("ControlPanelAdmin", "OrgMgmControlPanel+Admin").Register();
			new AliasQueryProcessor("HybridAdmin", "ControlPanelAdmin+XPremiseEnt,ControlPanelAdmin+LiveID").Register();
			RbacQuery.ConditionalQueryProcessors.Regist(delegate(string rbacQuery, out RbacQuery.RbacQueryProcessor processor)
			{
				if (EacFlightUtility.FeaturePrefixs.Any((string prefix) => rbacQuery.StartsWith(prefix)))
				{
					processor = new FlightQueryProcessor(rbacQuery);
					return true;
				}
				processor = null;
				return false;
			});
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x000976BC File Offset: 0x000958BC
		public void Dispose()
		{
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x000976C0 File Offset: 0x000958C0
		private void Application_PostAuthenticateRequest(object sender, EventArgs e)
		{
			HttpContext httpContext = HttpContext.Current;
			string text = httpContext.Request.Headers["msExchProxyUri"];
			if (!string.IsNullOrEmpty(text))
			{
				Uri uri = new Uri(text);
				string text2 = (uri.Segments.Length > 1) ? uri.Segments[1].TrimEnd(new char[]
				{
					'/'
				}) : string.Empty;
				if (text2.Equals(RbacModule.ecpAppPath.Value, StringComparison.OrdinalIgnoreCase) && !text2.Equals(RbacModule.ecpAppPath.Value))
				{
					string url = "/" + RbacModule.ecpAppPath + uri.PathAndQuery.Substring(RbacModule.ecpAppPath.Value.Length + 1);
					httpContext.Response.Redirect(url, true);
					return;
				}
			}
			if (httpContext.Request.HttpMethod == "GET" && !RbacModule.bypassXFrameOptions && !RbacModule.xFrameOptionsExceptionList.Contains(httpContext.Request.AppRelativeCurrentExecutionFilePath))
			{
				httpContext.Response.Headers.Set("X-Frame-Options", "SameOrigin");
			}
			AuthenticationSettings authenticationSettings = new AuthenticationSettings(httpContext);
			httpContext.User = authenticationSettings.Session;
			authenticationSettings.Session.SetCurrentThreadPrincipal();
			if (!httpContext.IsAcsOAuthRequest())
			{
				httpContext.CheckCanary();
			}
			authenticationSettings.Session.RequestReceived();
			if (authenticationSettings.Session is RbacPrincipal)
			{
				if (!OAuthHelper.IsWebRequestAllowed(httpContext))
				{
					ErrorHandlingUtil.TransferToErrorPage("notavailableforpartner");
				}
				if (!LoginUtil.CheckUrlAccess(httpContext.Request.FilePath))
				{
					ErrorHandlingUtil.TransferToErrorPage("noroles");
					return;
				}
				this.FlightRewrite(httpContext);
			}
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x0009785C File Offset: 0x00095A5C
		private void Application_EndRequest(object sender, EventArgs e)
		{
			IRbacSession rbacSession = HttpContext.Current.User as IRbacSession;
			if (rbacSession != null)
			{
				rbacSession.RequestCompleted();
			}
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x00097884 File Offset: 0x00095A84
		private void FlightRewrite(HttpContext context)
		{
			string relativePathToAppRoot = EcpUrl.GetRelativePathToAppRoot(context.Request.FilePath);
			if (relativePathToAppRoot != null)
			{
				string rewriteUrl = EacFlightProvider.Instance.GetRewriteUrl(relativePathToAppRoot);
				if (rewriteUrl != null)
				{
					string str = EcpUrl.ReplaceRelativePath(context.Request.FilePath, rewriteUrl, true);
					context.RewritePath(str + context.Request.PathInfo);
				}
			}
		}

		// Token: 0x040023D8 RID: 9176
		public static readonly string SessionStateCookieName = "ASP.NET_SessionId";

		// Token: 0x040023D9 RID: 9177
		private static object initSync = new object();

		// Token: 0x040023DA RID: 9178
		private static bool initialized;

		// Token: 0x040023DB RID: 9179
		private static HashSet<string> xFrameOptionsExceptionList;

		// Token: 0x040023DC RID: 9180
		private static bool bypassXFrameOptions;

		// Token: 0x040023DD RID: 9181
		private static Lazy<string> ecpAppPath = new Lazy<string>(() => HttpRuntime.AppDomainAppVirtualPath.TrimStart(new char[]
		{
			'/'
		}));
	}
}
