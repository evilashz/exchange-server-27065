using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools.Asp.Net;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003A8 RID: 936
	internal sealed class RbacSettings
	{
		// Token: 0x0600315E RID: 12638 RVA: 0x000980E0 File Offset: 0x000962E0
		public RbacSettings(HttpContext context)
		{
			ExTraceGlobals.RBACTracer.TraceInformation<string>(0, 0L, "Extracting RBAC settings from {0}.", context.GetRequestUrlForLog());
			Guid vdirId = Guid.Empty;
			Guid.TryParse(HttpContext.Current.Request.Headers["X-vDirObjectId"], out vdirId);
			this.ecpService = new Lazy<EcpService>(delegate()
			{
				if (vdirId == Guid.Empty)
				{
					return null;
				}
				ServiceTopology currentServiceTopology = ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\RBAC\\RbacSettings.cs", ".ctor", 707);
				return currentServiceTopology.FindAnyCafeService<EcpService>((EcpService service) => service.ADObjectId.ObjectGuid == vdirId, "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\RBAC\\RbacSettings.cs", ".ctor", 708);
			});
			this.LogonUserIdentity = context.User.Identity;
			this.OriginalUser = context.User;
			this.IsProxyLogon = context.Request.FilePath.EndsWith("/proxyLogon.ecp", StringComparison.OrdinalIgnoreCase);
			bool flag = context.IsAcsOAuthRequest();
			if (this.IsProxyLogon)
			{
				this.ProxySecurityAccessToken = new SerializedAccessToken(context.Request.InputStream);
			}
			else
			{
				this.ProxySecurityAccessToken = null;
			}
			string logonAccountSddlSid = context.Request.Headers["msExchLogonAccount"];
			string text = context.Request.Headers["msExchLogonMailbox"];
			string targetMailboxSddlSid = context.Request.Headers["msExchTargetMailbox"];
			Server inboundProxyCaller = RbacSettings.GetInboundProxyCaller(text, this.LogonUserIdentity as WindowsIdentity);
			if (inboundProxyCaller != null)
			{
				this.IsInboundProxyRequest = true;
				this.InboundProxyCallerName = inboundProxyCaller.Name;
				EcpLogonInformation identity = EcpLogonInformation.Create(logonAccountSddlSid, text, targetMailboxSddlSid, this.ProxySecurityAccessToken);
				this.EcpIdentity = new EcpIdentity(identity, "-ProxySession");
			}
			else
			{
				this.IsInboundProxyRequest = false;
				this.InboundProxyCallerName = string.Empty;
				string explicitUser = context.GetExplicitUser();
				string targetTenant = context.GetTargetTenant();
				string text2 = string.IsNullOrEmpty(targetTenant) ? "-RbacSession" : ("-RbacSession-@" + targetTenant);
				if (flag)
				{
					text2 += "-OAuthACS";
				}
				if (!string.IsNullOrEmpty(explicitUser))
				{
					this.EcpIdentity = new EcpIdentity(context.User, explicitUser, text2);
				}
				else
				{
					this.EcpIdentity = new EcpIdentity(this.LogonUserIdentity, text2);
				}
			}
			this.UserUniqueKeyForCanary = this.GetUserUniqueKey();
			this.IsExplicitSignOn = this.EcpIdentity.IsExplicitSignon;
			bool flag2 = null == context.Request.Cookies[RbacModule.SessionStateCookieName];
			if (flag2 && !flag)
			{
				context.Response.Cookies.Add(new HttpCookie(RbacModule.SessionStateCookieName, Guid.NewGuid().ToString())
				{
					HttpOnly = true
				});
				this.CacheKey = this.GetCacheKey();
				this.ExpireSession();
			}
			else
			{
				this.CacheKey = this.GetCacheKey();
			}
			ExTraceGlobals.RBACTracer.TraceInformation(0, 0L, "RBAC Settings for {0}: UserName: {1}, IsNewBrowserWindow={2}, IsInboundProxyRequest={3}, InboundProxyCallerName={4}, HasCachedSession={5}", new object[]
			{
				context.GetRequestUrlForLog(),
				this.UserName,
				flag2,
				this.IsInboundProxyRequest,
				this.InboundProxyCallerName,
				this.CachedSession != null
			});
		}

		// Token: 0x17001F69 RID: 8041
		// (get) Token: 0x0600315F RID: 12639 RVA: 0x000983DC File Offset: 0x000965DC
		public IIdentity OriginalLogonUserIdentity
		{
			get
			{
				return this.EcpIdentity.LogonUserIdentity;
			}
		}

		// Token: 0x17001F6A RID: 8042
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x000983E9 File Offset: 0x000965E9
		// (set) Token: 0x06003161 RID: 12641 RVA: 0x000983F1 File Offset: 0x000965F1
		public string UserUniqueKeyForCanary { get; private set; }

		// Token: 0x17001F6B RID: 8043
		// (get) Token: 0x06003162 RID: 12642 RVA: 0x000983FA File Offset: 0x000965FA
		// (set) Token: 0x06003163 RID: 12643 RVA: 0x00098402 File Offset: 0x00096602
		public IPrincipal OriginalUser { get; private set; }

		// Token: 0x17001F6C RID: 8044
		// (get) Token: 0x06003164 RID: 12644 RVA: 0x0009840B File Offset: 0x0009660B
		public bool HasFullAccess
		{
			get
			{
				return this.EcpIdentity.HasFullAccess;
			}
		}

		// Token: 0x17001F6D RID: 8045
		// (get) Token: 0x06003165 RID: 12645 RVA: 0x00098418 File Offset: 0x00096618
		public bool LogonUserEsoSelf
		{
			get
			{
				return this.EcpIdentity.LogonUserEsoSelf;
			}
		}

		// Token: 0x17001F6E RID: 8046
		// (get) Token: 0x06003166 RID: 12646 RVA: 0x00098425 File Offset: 0x00096625
		public string UserName
		{
			get
			{
				return this.EcpIdentity.UserName;
			}
		}

		// Token: 0x17001F6F RID: 8047
		// (get) Token: 0x06003167 RID: 12647 RVA: 0x00098434 File Offset: 0x00096634
		public string TenantNameForMonitoringPurpose
		{
			get
			{
				ExchangePrincipal accessedUserExchangePrincipal = this.GetAccessedUserExchangePrincipal();
				if (accessedUserExchangePrincipal != null && accessedUserExchangePrincipal.MailboxInfo.OrganizationId != null)
				{
					ADObjectId organizationalUnit = accessedUserExchangePrincipal.MailboxInfo.OrganizationId.OrganizationalUnit;
					if (organizationalUnit != null)
					{
						return organizationalUnit.Name;
					}
				}
				return null;
			}
		}

		// Token: 0x17001F70 RID: 8048
		// (get) Token: 0x06003168 RID: 12648 RVA: 0x0009847A File Offset: 0x0009667A
		public IIdentity AccessedUserIdentity
		{
			get
			{
				return this.EcpIdentity.AccessedUserIdentity;
			}
		}

		// Token: 0x17001F71 RID: 8049
		// (get) Token: 0x06003169 RID: 12649 RVA: 0x00098487 File Offset: 0x00096687
		public SecurityIdentifier AccessedUserSid
		{
			get
			{
				return this.EcpIdentity.AccessedUserSid;
			}
		}

		// Token: 0x17001F72 RID: 8050
		// (get) Token: 0x0600316A RID: 12650 RVA: 0x00098494 File Offset: 0x00096694
		public RbacSession CachedSession
		{
			get
			{
				return (RbacSession)HttpRuntime.Cache[this.CacheKey];
			}
		}

		// Token: 0x17001F73 RID: 8051
		// (get) Token: 0x0600316B RID: 12651 RVA: 0x000984AC File Offset: 0x000966AC
		public RbacSession Session
		{
			get
			{
				RbacSession rbacSession = this.CachedSession;
				if (rbacSession == null)
				{
					object obj = null;
					lock (RbacSettings.perUserLocks)
					{
						if (!RbacSettings.perUserLocks.TryGetValue(this.CacheKey, out obj))
						{
							obj = new object();
							RbacSettings.perUserLocks.Add(this.CacheKey, obj);
						}
					}
					lock (obj)
					{
						rbacSession = this.CachedSession;
						if (rbacSession == null)
						{
							rbacSession = this.CreateSession();
						}
					}
					lock (RbacSettings.perUserLocks)
					{
						RbacSettings.perUserLocks.Remove(this.CacheKey);
					}
				}
				return rbacSession;
			}
		}

		// Token: 0x17001F74 RID: 8052
		// (get) Token: 0x0600316C RID: 12652 RVA: 0x00098598 File Offset: 0x00096798
		public bool AdminEnabled
		{
			get
			{
				return Util.IsDataCenter || (this.ecpService.Value != null && this.ecpService.Value.AdminEnabled);
			}
		}

		// Token: 0x17001F75 RID: 8053
		// (get) Token: 0x0600316D RID: 12653 RVA: 0x000985C2 File Offset: 0x000967C2
		public bool OwaOptionsEnabled
		{
			get
			{
				return Util.IsDataCenter || this.ecpService.Value == null || this.ecpService.Value.OwaOptionsEnabled;
			}
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x000985FC File Offset: 0x000967FC
		internal static void AddSessionToCache(string cacheKey, RbacSession session, bool canRemove, bool isNewSession)
		{
			Cache cache = HttpRuntime.Cache;
			CacheDependency dependencies = null;
			DateTime absoluteExpiration = (DateTime)ExDateTime.UtcNow.Add(RbacSection.Instance.RbacPrincipalMaximumAge);
			TimeSpan noSlidingExpiration = Cache.NoSlidingExpiration;
			CacheItemPriority priority = canRemove ? CacheItemPriority.High : CacheItemPriority.NotRemovable;
			CacheItemRemovedCallback onRemoveCallback;
			if (!isNewSession)
			{
				onRemoveCallback = null;
			}
			else
			{
				onRemoveCallback = delegate(string key, object value, CacheItemRemovedReason reason)
				{
					((RbacSession)value).SessionEnd();
				};
			}
			cache.Insert(cacheKey, session, dependencies, absoluteExpiration, noSlidingExpiration, priority, onRemoveCallback);
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x00098664 File Offset: 0x00096864
		public void ExpireSession()
		{
			string key = this.CacheKey + "_Regional";
			if (HttpRuntime.Cache[key] != null)
			{
				HttpRuntime.Cache.Remove(key);
			}
			if (this.CachedSession != null)
			{
				HttpRuntime.Cache.Remove(this.CacheKey);
			}
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x000986B4 File Offset: 0x000968B4
		internal string GetCacheKey()
		{
			string result;
			if (!Util.IsDataCenter)
			{
				Uri requestUrl = HttpContext.Current.GetRequestUrl();
				result = string.Concat(new object[]
				{
					this.EcpIdentity.GetCacheKey(),
					"_",
					HttpContext.Current.GetSessionID(),
					"_",
					requestUrl.Host,
					"_",
					requestUrl.Port
				});
			}
			else
			{
				result = this.EcpIdentity.GetCacheKey();
			}
			return result;
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x0009873C File Offset: 0x0009693C
		internal ExchangePrincipal GetAccessedUserExchangePrincipal()
		{
			return this.EcpIdentity.GetAccessedUserExchangePrincipal();
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x00098749 File Offset: 0x00096949
		internal ExchangePrincipal GetLogonUserExchangePrincipal()
		{
			return this.EcpIdentity.GetLogonUserExchangePrincipal();
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x00098758 File Offset: 0x00096958
		private static Server GetInboundProxyCaller(string securityContextHeader, WindowsIdentity identity)
		{
			if (string.IsNullOrEmpty(securityContextHeader))
			{
				ExTraceGlobals.ProxyTracer.TraceInformation(0, 0L, "Request does not carry a security context header.");
				return null;
			}
			if (identity == null)
			{
				ExTraceGlobals.ProxyTracer.TraceInformation(0, 0L, "Request does not carry a WindowsIdentity.");
				throw new CmdletAccessDeniedException(Strings.ProxyRequiresWindowsAuthentication);
			}
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 1177, "GetInboundProxyCaller", "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\RBAC\\RbacSettings.cs");
			topologyConfigurationSession.UseConfigNC = false;
			topologyConfigurationSession.UseGlobalCatalog = true;
			ADComputer adcomputer = topologyConfigurationSession.FindComputerBySid(identity.User);
			if (adcomputer == null)
			{
				ExTraceGlobals.ProxyTracer.TraceInformation<SecurityIdentifier>(0, 0L, "Identity in the request is not from an AD computer. {0}", identity.User);
				throw new CmdletAccessDeniedException(Strings.ProxyRequiresCallerToBeCAS);
			}
			topologyConfigurationSession.UseConfigNC = true;
			Server server = topologyConfigurationSession.FindServerByName(adcomputer.Name);
			if (server == null)
			{
				ExTraceGlobals.ProxyTracer.TraceInformation<string>(0, 0L, "Identity in the request is a computer but not an Exchange server. {0}", adcomputer.Name);
				throw new CmdletAccessDeniedException(Strings.ProxyRequiresCallerToBeCAS);
			}
			if (!server.IsClientAccessServer && !server.IsFfoWebServiceRole && !server.IsCafeServer && !server.IsOSPRole)
			{
				ExTraceGlobals.ProxyTracer.TraceInformation<string>(0, 0L, "Exchange Server {0} is not a Client Access Server.", server.Name);
				throw new CmdletAccessDeniedException(Strings.ProxyRequiresCallerToBeCAS);
			}
			if (!server.IsE14OrLater)
			{
				ExTraceGlobals.ProxyTracer.TraceInformation<string, string>(0, 0L, "Exchange Server {0} is not a E14 or later. {1}", server.Name, server.SerialNumber);
				throw new CmdletAccessDeniedException(Strings.ProxyRequiresCallerToBeCAS);
			}
			ExTraceGlobals.ProxyTracer.TraceInformation<string>(0, 0L, "Detected an inbound proxy call from server {0}.", server.Name);
			return server;
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x000988CC File Offset: 0x00096ACC
		private RbacSession CreateSession()
		{
			RbacSession result;
			using (new AverageTimePerfCounter(EcpPerfCounters.AverageRbacSessionCreation, EcpPerfCounters.AverageRbacSessionCreationBase, true))
			{
				using (EcpPerformanceData.CreateRbacSession.StartRequestTimer())
				{
					RbacContext rbacContext = new RbacContext(this);
					RbacSession rbacSession = rbacContext.CreateSession();
					RbacSettings.AddSessionToCache(this.CacheKey, rbacSession, true, true);
					rbacSession.SessionStart();
					result = rbacSession;
				}
			}
			return result;
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x00098950 File Offset: 0x00096B50
		private string GetUserUniqueKey()
		{
			DelegatedPrincipal delegatedPrincipal = this.OriginalUser as DelegatedPrincipal;
			if (delegatedPrincipal != null)
			{
				return delegatedPrincipal.UserId;
			}
			if (DatacenterRegistry.IsForefrontForOffice())
			{
				return this.OriginalLogonUserIdentity.Name;
			}
			return this.OriginalLogonUserIdentity.GetSecurityIdentifier().Value;
		}

		// Token: 0x040023F3 RID: 9203
		public readonly bool IsProxyLogon;

		// Token: 0x040023F4 RID: 9204
		public readonly SerializedAccessToken ProxySecurityAccessToken;

		// Token: 0x040023F5 RID: 9205
		public readonly bool IsInboundProxyRequest;

		// Token: 0x040023F6 RID: 9206
		public readonly string InboundProxyCallerName;

		// Token: 0x040023F7 RID: 9207
		public readonly IIdentity LogonUserIdentity;

		// Token: 0x040023F8 RID: 9208
		public readonly bool IsExplicitSignOn;

		// Token: 0x040023F9 RID: 9209
		public readonly string CacheKey;

		// Token: 0x040023FA RID: 9210
		private readonly EcpIdentity EcpIdentity;

		// Token: 0x040023FB RID: 9211
		private static readonly Dictionary<string, object> perUserLocks = new Dictionary<string, object>();

		// Token: 0x040023FC RID: 9212
		private Lazy<EcpService> ecpService;
	}
}
