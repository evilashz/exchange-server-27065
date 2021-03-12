using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.TenantMonitoring;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003A3 RID: 931
	internal sealed class RbacContext
	{
		// Token: 0x06003129 RID: 12585 RVA: 0x00096350 File Offset: 0x00094550
		static RbacContext()
		{
			RbacContext.EsoAllowedCmdlets = new List<RoleEntry>
			{
				new CmdletRoleEntry("Clear-ActiveSyncDevice", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-ActiveSyncDevice", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Remove-ActiveSyncDevice", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-ActiveSyncDeviceClass", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-ActiveSyncDeviceStatistics", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-ActiveSyncMailboxPolicy", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("New-ActiveSyncMailboxPolicy", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Remove-ActiveSyncMailboxPolicy", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Set-ActiveSyncMailboxPolicy", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-ActiveSyncOrganizationSettings", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Set-ActiveSyncOrganizationSettings", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-CalendarDiagnosticLog", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-CalendarNotification", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Set-CalendarNotification", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-CASMailbox", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Set-CASMailbox", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-ActiveSyncDeviceAccessRule", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("New-ActiveSyncDeviceAccessRule", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Remove-ActiveSyncDeviceAccessRule", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Set-ActiveSyncDeviceAccessRule", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Disable-InboxRule", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Enable-InboxRule", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-InboxRule", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("New-InboxRule", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Remove-InboxRule", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Set-InboxRule", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-RetentionPolicyTag", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Set-RetentionPolicyTag", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Clear-MobileDevice", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-MobileDevice", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Remove-MobileDevice", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-MobileDeviceStatistics", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-MobileDeviceMailboxPolicy", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("New-MobileDeviceMailboxPolicy", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Set-MobileDeviceMailboxPolicy", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Remove-MobileDeviceMailboxPolicy", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-MailboxAutoReplyConfiguration", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Set-MailboxAutoReplyConfiguration", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-MailboxCalendarConfiguration", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Set-MailboxCalendarConfiguration", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-CalendarProcessing", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Set-CalendarProcessing", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-MailboxFolder", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("New-MailboxFolder", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-MailboxCalendarFolder", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Set-MailboxCalendarFolder", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-MailboxFolderPermission", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Set-MailboxFolderPermission", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Remove-MailboxFolderPermission", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-MailboxJunkEmailConfiguration", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Set-MailboxJunkEmailConfiguration", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-MailboxMessageConfiguration", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Set-MailboxMessageConfiguration", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-MailboxRegionalConfiguration", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Set-MailboxRegionalConfiguration", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("New-RoleAssignmentPolicy", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Set-RoleAssignmentPolicy", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Remove-RoleAssignmentPolicy", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("New-MailMessage", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-MessageClassification", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-MessageCategory", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-MessageTrackingReport", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Search-MessageTrackingReport", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-Recipient", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-SiteMailbox", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Clear-TextMessagingAccount", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Get-TextMessagingAccount", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Set-TextMessagingAccount", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Compare-TextMessagingVerificationCode", "Microsoft.Exchange.Management.PowerShell.E2010", null),
				new CmdletRoleEntry("Send-TextMessagingVerificationCode", "Microsoft.Exchange.Management.PowerShell.E2010", null)
			};
			RbacContext.EsoAllowedCmdlets.Sort(RoleEntry.NameComparer);
			string value = ConfigurationManager.AppSettings["ExchangeRunspaceConfigurationSnapinSet"];
			if (!string.IsNullOrEmpty(value))
			{
				try
				{
					RbacContext.snapinSet = (SnapinSet)Enum.Parse(typeof(SnapinSet), value);
					goto IL_6CD;
				}
				catch
				{
					RbacContext.snapinSet = SnapinSet.Admin;
					goto IL_6CD;
				}
			}
			RbacContext.snapinSet = SnapinSet.Admin;
			IL_6CD:
			string value2 = ConfigurationManager.AppSettings["PullHostedTenantRbac"];
			if (!bool.TryParse(value2, out RbacContext.PullHostedTenantRbac))
			{
				RbacContext.PullHostedTenantRbac = false;
			}
			RbacContext.clientAppId = RbacContext.GetClientApplication();
		}

		// Token: 0x17001F5A RID: 8026
		// (get) Token: 0x0600312A RID: 12586 RVA: 0x00096A68 File Offset: 0x00094C68
		public static ExchangeRunspaceConfigurationSettings.ExchangeApplication ClientAppId
		{
			get
			{
				return RbacContext.clientAppId;
			}
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x00096D10 File Offset: 0x00094F10
		public RbacContext(RbacSettings settings)
		{
			RbacContext <>4__this = this;
			ExTraceGlobals.RBACTracer.TraceInformation<string>(0, 0L, "Creating RBAC context for {0}", settings.UserName);
			this.Settings = settings;
			this.roles = new LazilyInitialized<ExchangeRunspaceConfiguration>(delegate()
			{
				ExchangeRunspaceConfiguration exchangeRunspaceConfiguration;
				if (DatacenterRegistry.IsForefrontForOffice())
				{
					Assembly assembly = Assembly.Load("Microsoft.Exchange.Hygiene.Security.Authorization");
					string siteName = HostingEnvironment.ApplicationHost.GetSiteName();
					try
					{
						string name = (RbacContext.PullHostedTenantRbac && (bool)HttpContext.Current.Items["IsHostedTenant"]) ? "Microsoft.Exchange.Hygiene.Security.Authorization.ForefrontRunspaceConfigurationForHostedTenant" : "Microsoft.Exchange.Hygiene.Security.Authorization.ForefrontRunspaceConfiguration";
						Type type = assembly.GetType(name);
						exchangeRunspaceConfiguration = (ExchangeRunspaceConfiguration)Activator.CreateInstance(type, new object[]
						{
							<>4__this.Settings.OriginalLogonUserIdentity,
							siteName
						});
						goto IL_222;
					}
					catch (TargetInvocationException ex)
					{
						throw ex.InnerException ?? ex;
					}
				}
				IList<RoleType> list = <>4__this.GetAllowedRoleTypes(settings);
				ExchangeRunspaceConfigurationSettings.ExchangeUserType user = ExchangeRunspaceConfigurationSettings.ExchangeUserType.Unknown;
				string text = (HttpContext.Current.Request == null) ? null : HttpContext.Current.Request.UserAgent;
				IEnumerable<KeyValuePair<string, string>> customizedConstraints = <>4__this.GetCustomizedConstraints(settings);
				if (!string.IsNullOrEmpty(text) && (text == "User-Agent:+Mozilla/4.0+(compatible;+MSIE+7.0)" || text == "Mozilla/4.0+(compatible;+MSIE+9.0;+Windows+NT+6.1;+MSEXCHMON;+TESTCONN2)" || text.IndexOf("ACTIVEMONITORING", StringComparison.OrdinalIgnoreCase) >= 0 || text.IndexOf("MONITORINGWEBCLIENT", StringComparison.OrdinalIgnoreCase) >= 0))
				{
					user = ExchangeRunspaceConfigurationSettings.ExchangeUserType.Monitoring;
				}
				ExchangeRunspaceConfigurationSettings settings2 = new ExchangeRunspaceConfigurationSettings(null, RbacContext.ClientAppId, null, ExchangeRunspaceConfigurationSettings.SerializationLevel.None, PSLanguageMode.NoLanguage, ExchangeRunspaceConfigurationSettings.ProxyMethod.RPS, false, false, false, user, customizedConstraints);
				if (<>4__this.Settings.IsExplicitSignOn)
				{
					if (<>4__this.Settings.HasFullAccess)
					{
						IList<RoleType> list2;
						if (list == null)
						{
							IList<RoleType> esoAllowedRoleTypes = RbacContext.EsoAllowedRoleTypes;
							list2 = esoAllowedRoleTypes;
						}
						else
						{
							list2 = list.Intersect(RbacContext.EsoAllowedRoleTypes).ToList<RoleType>();
						}
						list = list2;
						exchangeRunspaceConfiguration = new ExchangeRunspaceConfiguration(<>4__this.Settings.OriginalLogonUserIdentity, <>4__this.Settings.AccessedUserIdentity, settings2, list, RbacContext.EsoAllowedCmdlets, null, true, RbacContext.snapinSet);
					}
					else
					{
						exchangeRunspaceConfiguration = new ExchangeRunspaceConfiguration(<>4__this.Settings.OriginalLogonUserIdentity, <>4__this.Settings.AccessedUserIdentity, settings2, list, null, RbacContext.EsoRequiredRoleTypesForAdmin, false, RbacContext.snapinSet);
					}
				}
				else
				{
					exchangeRunspaceConfiguration = new ExchangeRunspaceConfiguration(<>4__this.Settings.AccessedUserIdentity, null, settings2, list, null, null, false, RbacContext.snapinSet);
				}
				IL_222:
				if (!exchangeRunspaceConfiguration.ExecutingUserIsAllowedECP)
				{
					ExTraceGlobals.RBACTracer.TraceInformation<string>(0, 0L, "{0} is not allowed to use ECP.", <>4__this.Settings.UserName);
					throw new CmdletAccessDeniedException(Strings.UserIsNotEcpEnabled(<>4__this.Settings.UserName));
				}
				return exchangeRunspaceConfiguration;
			});
			this.mailbox = new LazilyInitialized<ExchangePrincipal>(() => this.Settings.GetAccessedUserExchangePrincipal());
		}

		// Token: 0x17001F5B RID: 8027
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x00096D9C File Offset: 0x00094F9C
		// (set) Token: 0x0600312D RID: 12589 RVA: 0x00096DA4 File Offset: 0x00094FA4
		public RbacSettings Settings { get; private set; }

		// Token: 0x17001F5C RID: 8028
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x00096DAD File Offset: 0x00094FAD
		public bool IsCrossSiteMailboxLogon
		{
			get
			{
				if (this.isCrossSiteMailboxLogon == null)
				{
					this.isCrossSiteMailboxLogon = new bool?(this.IsMailbox && HttpContext.Current.TargetServerOrVersionSpecifiedInUrlOrCookie());
				}
				return this.isCrossSiteMailboxLogon.Value;
			}
		}

		// Token: 0x17001F5D RID: 8029
		// (get) Token: 0x0600312F RID: 12591 RVA: 0x00096DE7 File Offset: 0x00094FE7
		// (set) Token: 0x06003130 RID: 12592 RVA: 0x00096DEF File Offset: 0x00094FEF
		public bool IsCrossSiteMailboxLogonAllowed { get; internal set; }

		// Token: 0x17001F5E RID: 8030
		// (get) Token: 0x06003131 RID: 12593 RVA: 0x00096DF8 File Offset: 0x00094FF8
		public bool IsMailbox
		{
			get
			{
				return this.mailbox.Value != null;
			}
		}

		// Token: 0x17001F5F RID: 8031
		// (get) Token: 0x06003132 RID: 12594 RVA: 0x00096E0B File Offset: 0x0009500B
		public ServerVersion MailboxServerVersion
		{
			get
			{
				return new ServerVersion(this.Mailbox.MailboxInfo.Location.ServerVersion);
			}
		}

		// Token: 0x17001F60 RID: 8032
		// (get) Token: 0x06003133 RID: 12595 RVA: 0x00096E27 File Offset: 0x00095027
		public ExchangeRunspaceConfiguration Roles
		{
			get
			{
				return this.roles;
			}
		}

		// Token: 0x17001F61 RID: 8033
		// (get) Token: 0x06003134 RID: 12596 RVA: 0x00096E34 File Offset: 0x00095034
		private ExchangePrincipal Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x17001F62 RID: 8034
		// (get) Token: 0x06003135 RID: 12597 RVA: 0x00097044 File Offset: 0x00095244
		private IEnumerable<RbacSession.Factory> RbacSessionFactories
		{
			get
			{
				yield return new InboundProxySession.ProxyLogonNeededFactory(this);
				yield return new InboundProxySession.Factory(this);
				this.CheckMailboxVersion();
				if (OutboundProxySession.Factory.ProxyToLocalHost)
				{
					yield return new OutboundProxySession.Factory(this);
				}
				bool isFromCafe = HttpContext.Current.Request.Headers["X-IsFromCafe"] == "1";
				if (isFromCafe)
				{
					this.IsCrossSiteMailboxLogonAllowed = true;
					ExTraceGlobals.RedirectTracer.TraceInformation<bool>(0, 0L, "Redirection will be skipped because the request comes from CAFE (IsFromCafe={0}).", isFromCafe);
				}
				else
				{
					ExTraceGlobals.RedirectTracer.TraceInformation<bool>(0, 0L, "Redirection will be tried because of (IsFromCafe={0}).", isFromCafe);
					if (!this.IsCrossSiteMailboxLogonAllowed && this.IsCrossSiteMailboxLogon)
					{
						yield return new OutboundProxySession.Factory(this);
					}
				}
				yield return new StandardSession.Factory(this);
				yield break;
			}
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x00097061 File Offset: 0x00095261
		public IList<EcpService> GetServicesInMailboxSite(ClientAccessType clientAccessType, Predicate<EcpService> serviceFilter)
		{
			return ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\RBAC\\RbacContext.cs", "GetServicesInMailboxSite", 568).FindAll<EcpService>(this.Mailbox, clientAccessType, serviceFilter, "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\RBAC\\RbacContext.cs", "GetServicesInMailboxSite", 568);
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x00097094 File Offset: 0x00095294
		public RbacSession CreateSession()
		{
			ExTraceGlobals.RBACTracer.TraceInformation<string>(0, 0L, "Creating RBAC session for {0}", this.Settings.UserName);
			TenantMonitor.LogActivity(CounterType.ECPSessionCreationAttempts, this.Settings.TenantNameForMonitoringPurpose);
			foreach (RbacSession.Factory factory in this.RbacSessionFactories)
			{
				RbacSession rbacSession = factory.CreateSession();
				if (rbacSession != null)
				{
					TenantMonitor.LogActivity(CounterType.ECPSessionCreationSuccesses, this.Settings.TenantNameForMonitoringPurpose);
					return rbacSession;
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x00097130 File Offset: 0x00095330
		[Conditional("DEBUG")]
		private static void BackupAllowedList()
		{
			RbacContext.esoAllowedCmdletsCopy = new List<RoleEntry>(RbacContext.EsoAllowedCmdlets.Count);
			foreach (RoleEntry roleEntry in RbacContext.EsoAllowedCmdlets)
			{
				CmdletRoleEntry cmdletRoleEntry = (CmdletRoleEntry)roleEntry;
				RbacContext.esoAllowedCmdletsCopy.Add(new CmdletRoleEntry(cmdletRoleEntry.Name, cmdletRoleEntry.PSSnapinName, null));
			}
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000971B0 File Offset: 0x000953B0
		[Conditional("DEBUG")]
		private static void ThrowIfAllowedListChanged()
		{
			for (int i = 0; i < RbacContext.esoAllowedCmdletsCopy.Count; i++)
			{
				RoleEntry.CompareRoleEntriesByName(RbacContext.esoAllowedCmdletsCopy[i], RbacContext.EsoAllowedCmdlets[i]);
			}
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x000971F0 File Offset: 0x000953F0
		private static ExchangeRunspaceConfigurationSettings.ExchangeApplication GetClientApplication()
		{
			try
			{
				string text = ConfigurationManager.AppSettings["IsOSPEnvironment"];
				if (text != null && text.ToUpperInvariant().Equals("TRUE"))
				{
					RbacContext.clientAppId = ExchangeRunspaceConfigurationSettings.ExchangeApplication.OSP;
				}
				else
				{
					RbacContext.clientAppId = ExchangeRunspaceConfigurationSettings.ExchangeApplication.ECP;
				}
			}
			catch (ConfigurationErrorsException ex)
			{
				ExTraceGlobals.RBACTracer.TraceWarning<string>(0, 0L, "Cannot read AppSettings: {0}", ex.Message);
				RbacContext.clientAppId = ExchangeRunspaceConfigurationSettings.ExchangeApplication.ECP;
			}
			return RbacContext.clientAppId;
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x0009726C File Offset: 0x0009546C
		private void CheckMailboxVersion()
		{
			ExchangePrincipal accessedUserExchangePrincipal = this.Settings.GetAccessedUserExchangePrincipal();
			if (accessedUserExchangePrincipal != null)
			{
				this.CheckVersion(accessedUserExchangePrincipal.MailboxInfo.Location.ServerVersion);
			}
			if (this.Settings.IsExplicitSignOn && !this.Settings.HasFullAccess)
			{
				ExchangePrincipal logonUserExchangePrincipal = this.Settings.GetLogonUserExchangePrincipal();
				if (logonUserExchangePrincipal != null)
				{
					this.CheckVersion(logonUserExchangePrincipal.MailboxInfo.Location.ServerVersion);
				}
			}
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x000972E0 File Offset: 0x000954E0
		private void CheckVersion(int versionNumber)
		{
			ServerVersion serverVersion = new ServerVersion(versionNumber);
			if (serverVersion.Major < 14 && !HttpContext.Current.TargetServerOrVersionSpecifiedInUrlOrCookie())
			{
				throw new LowVersionUserDeniedException();
			}
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x00097310 File Offset: 0x00095510
		private IList<RoleType> GetAllowedRoleTypes(RbacSettings settings)
		{
			IList<RoleType> result = null;
			if (!Util.IsDataCenter)
			{
				RoleTypeSegment roleTypeSegment = new RoleTypeSegment(settings);
				result = roleTypeSegment.GetAllowedFeatures();
			}
			return result;
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x00097338 File Offset: 0x00095538
		private IEnumerable<KeyValuePair<string, string>> GetCustomizedConstraints(RbacSettings settings)
		{
			KeyValuePair<string, string>? oauthUserConstraint = OAuthHelper.GetOAuthUserConstraint(settings.LogonUserIdentity);
			if (oauthUserConstraint != null)
			{
				return new KeyValuePair<string, string>[]
				{
					oauthUserConstraint.Value
				};
			}
			return null;
		}

		// Token: 0x040023C8 RID: 9160
		private const int EcpMinimumSupportedVersionMajor = 14;

		// Token: 0x040023C9 RID: 9161
		private const string EnvironmentKey = "IsOSPEnvironment";

		// Token: 0x040023CA RID: 9162
		private const string IsHostedTenantKey = "IsHostedTenant";

		// Token: 0x040023CB RID: 9163
		private const string PullHostedTenantRbacKey = "PullHostedTenantRbac";

		// Token: 0x040023CC RID: 9164
		internal static readonly ReadOnlyCollection<RoleType> EsoAllowedRoleTypes = new List<RoleType>
		{
			RoleType.MyBaseOptions,
			RoleType.MyMailSubscriptions,
			RoleType.MyProfileInformation,
			RoleType.MyContactInformation,
			RoleType.MyRetentionPolicies,
			RoleType.MyTextMessaging,
			RoleType.MyVoiceMail
		}.AsReadOnly();

		// Token: 0x040023CD RID: 9165
		internal static readonly List<RoleEntry> EsoAllowedCmdlets;

		// Token: 0x040023CE RID: 9166
		internal static readonly ReadOnlyCollection<RoleType> EsoRequiredRoleTypesForAdmin = new List<RoleType>
		{
			RoleType.UserOptions
		}.AsReadOnly();

		// Token: 0x040023CF RID: 9167
		private static readonly bool PullHostedTenantRbac;

		// Token: 0x040023D0 RID: 9168
		private static List<RoleEntry> esoAllowedCmdletsCopy;

		// Token: 0x040023D1 RID: 9169
		private static SnapinSet snapinSet;

		// Token: 0x040023D2 RID: 9170
		private static ExchangeRunspaceConfigurationSettings.ExchangeApplication clientAppId = ExchangeRunspaceConfigurationSettings.ExchangeApplication.Unknown;

		// Token: 0x040023D3 RID: 9171
		private LazilyInitialized<ExchangePrincipal> mailbox;

		// Token: 0x040023D4 RID: 9172
		private LazilyInitialized<ExchangeRunspaceConfiguration> roles;

		// Token: 0x040023D5 RID: 9173
		private bool? isCrossSiteMailboxLogon;
	}
}
