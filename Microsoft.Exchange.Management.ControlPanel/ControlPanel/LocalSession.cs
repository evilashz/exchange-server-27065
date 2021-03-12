using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Caching;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000391 RID: 913
	internal abstract class LocalSession : RbacSession
	{
		// Token: 0x060030A6 RID: 12454 RVA: 0x00093F94 File Offset: 0x00092194
		protected LocalSession(RbacContext context, SessionPerformanceCounters sessionPerfCounters, EsoSessionPerformanceCounters esoSessionPerfCounters) : base(context, sessionPerfCounters, esoSessionPerfCounters)
		{
			this.IsCrossSiteMailboxLogon = context.IsCrossSiteMailboxLogon;
			foreach (string role in LocalSession.roleList)
			{
				this.logonTypeFlag <<= 1;
				this.logonTypeFlag |= (base.IsInRole(role) ? 1 : 0);
			}
		}

		// Token: 0x17001F3E RID: 7998
		// (get) Token: 0x060030A7 RID: 12455 RVA: 0x00094024 File Offset: 0x00092224
		public new static LocalSession Current
		{
			get
			{
				return (LocalSession)RbacPrincipal.Current;
			}
		}

		// Token: 0x17001F3F RID: 7999
		// (get) Token: 0x060030A8 RID: 12456 RVA: 0x00094030 File Offset: 0x00092230
		public string LogonTypeFlag
		{
			get
			{
				return this.logonTypeFlag.ToString();
			}
		}

		// Token: 0x17001F40 RID: 8000
		// (get) Token: 0x060030A9 RID: 12457 RVA: 0x0009404B File Offset: 0x0009224B
		// (set) Token: 0x060030AA RID: 12458 RVA: 0x00094059 File Offset: 0x00092259
		public override string DateFormat
		{
			get
			{
				this.WaitingForCmdlet();
				return base.DateFormat;
			}
			protected set
			{
				base.DateFormat = value;
			}
		}

		// Token: 0x17001F41 RID: 8001
		// (get) Token: 0x060030AB RID: 12459 RVA: 0x00094062 File Offset: 0x00092262
		// (set) Token: 0x060030AC RID: 12460 RVA: 0x00094070 File Offset: 0x00092270
		public override string TimeFormat
		{
			get
			{
				this.WaitingForCmdlet();
				return base.TimeFormat;
			}
			protected set
			{
				base.TimeFormat = value;
			}
		}

		// Token: 0x17001F42 RID: 8002
		// (get) Token: 0x060030AD RID: 12461 RVA: 0x00094079 File Offset: 0x00092279
		// (set) Token: 0x060030AE RID: 12462 RVA: 0x00094087 File Offset: 0x00092287
		public override ExTimeZone UserTimeZone
		{
			get
			{
				this.WaitingForCmdlet();
				return base.UserTimeZone;
			}
			protected set
			{
				base.UserTimeZone = value;
			}
		}

		// Token: 0x17001F43 RID: 8003
		// (get) Token: 0x060030AF RID: 12463 RVA: 0x00094090 File Offset: 0x00092290
		public SmtpAddress ExecutingUserPrimarySmtpAddress
		{
			get
			{
				if (this.executingUserPrimarySmtpAddress == null)
				{
					if (base.RbacConfiguration.DelegatedPrincipal != null)
					{
						if (SmtpAddress.IsValidSmtpAddress(base.RbacConfiguration.DelegatedPrincipal.UserId))
						{
							this.executingUserPrimarySmtpAddress = new SmtpAddress?(new SmtpAddress(base.RbacConfiguration.DelegatedPrincipal.UserId));
						}
					}
					else
					{
						this.executingUserPrimarySmtpAddress = new SmtpAddress?(base.RbacConfiguration.ExecutingUserPrimarySmtpAddress);
					}
					if (this.executingUserPrimarySmtpAddress == null)
					{
						this.executingUserPrimarySmtpAddress = new SmtpAddress?(SmtpAddress.Empty);
					}
				}
				return this.executingUserPrimarySmtpAddress.Value;
			}
		}

		// Token: 0x17001F44 RID: 8004
		// (get) Token: 0x060030B0 RID: 12464 RVA: 0x0009412E File Offset: 0x0009232E
		// (set) Token: 0x060030B1 RID: 12465 RVA: 0x00094136 File Offset: 0x00092336
		public bool IsCrossSiteMailboxLogon { get; private set; }

		// Token: 0x17001F45 RID: 8005
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x00094140 File Offset: 0x00092340
		public bool IsDehydrated
		{
			get
			{
				if (this.isDehydrated == null)
				{
					if (base.RbacConfiguration.OrganizationId != OrganizationId.ForestWideOrgId)
					{
						IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(base.RbacConfiguration.OrganizationId), 203, "IsDehydrated", "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\RBAC\\LocalSession.cs");
						tenantOrTopologyConfigurationSession.UseGlobalCatalog = true;
						tenantOrTopologyConfigurationSession.UseConfigNC = true;
						ADRawEntry adrawEntry = tenantOrTopologyConfigurationSession.ReadADRawEntry(base.RbacConfiguration.OrganizationId.ConfigurationUnit, new PropertyDefinition[]
						{
							OrganizationSchema.IsDehydrated
						});
						this.isDehydrated = new bool?((bool)adrawEntry[OrganizationSchema.IsDehydrated]);
					}
					else
					{
						this.isDehydrated = new bool?(false);
					}
				}
				return this.isDehydrated.Value;
			}
		}

		// Token: 0x17001F46 RID: 8006
		// (get) Token: 0x060030B3 RID: 12467 RVA: 0x00094208 File Offset: 0x00092408
		public bool IsSoftDeletedFeatureEnabled
		{
			get
			{
				if (this.isSoftDeletedFeatureEnabled == null)
				{
					if (base.RbacConfiguration.OrganizationId != OrganizationId.ForestWideOrgId)
					{
						ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromAllTenantsPartitionId(base.RbacConfiguration.OrganizationId.PartitionId), 241, "IsSoftDeletedFeatureEnabled", "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\RBAC\\LocalSession.cs");
						ExchangeConfigurationUnit exchangeConfigurationUnit = tenantConfigurationSession.Read<ExchangeConfigurationUnit>(base.RbacConfiguration.OrganizationId.ConfigurationUnit);
						bool msosyncEnabled = exchangeConfigurationUnit.MSOSyncEnabled;
						Organization rootOrgContainer = ADSystemConfigurationSession.GetRootOrgContainer(TopologyProvider.LocalForestFqdn, null, null);
						SoftDeletedFeatureStatusFlags softDeletedFeatureStatus = rootOrgContainer.SoftDeletedFeatureStatus;
						if (!this.IsFeatureEnabled(softDeletedFeatureStatus, msosyncEnabled))
						{
							softDeletedFeatureStatus = exchangeConfigurationUnit.SoftDeletedFeatureStatus;
							this.isSoftDeletedFeatureEnabled = new bool?(this.IsFeatureEnabled(softDeletedFeatureStatus, msosyncEnabled));
						}
						else
						{
							this.isSoftDeletedFeatureEnabled = new bool?(true);
						}
					}
					else
					{
						this.isSoftDeletedFeatureEnabled = new bool?(false);
					}
				}
				return this.isSoftDeletedFeatureEnabled.Value;
			}
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000942F0 File Offset: 0x000924F0
		private bool IsFeatureEnabled(SoftDeletedFeatureStatusFlags feature, bool isMSOTenant)
		{
			return (!isMSOTenant && (feature & SoftDeletedFeatureStatusFlags.EDUEnabled) == SoftDeletedFeatureStatusFlags.EDUEnabled) || (isMSOTenant && (feature & SoftDeletedFeatureStatusFlags.MSOEnabled) == SoftDeletedFeatureStatusFlags.MSOEnabled);
		}

		// Token: 0x17001F47 RID: 8007
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x00094308 File Offset: 0x00092508
		// (set) Token: 0x060030B6 RID: 12470 RVA: 0x00094310 File Offset: 0x00092510
		private bool HasRegionalSettings { get; set; }

		// Token: 0x060030B7 RID: 12471 RVA: 0x0009431C File Offset: 0x0009251C
		internal void AddRegionalSettingsToCache(RegionalSettingsConfiguration settings)
		{
			HttpRuntime.Cache.Insert(this.RegionalCacheKey, settings, null, (DateTime)ExDateTime.UtcNow.Add(TimeSpan.FromHours(24.0)), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
		}

		// Token: 0x17001F48 RID: 8008
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x00094362 File Offset: 0x00092562
		private string RegionalCacheKey
		{
			get
			{
				return base.Settings.CacheKey + "_Regional";
			}
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x0009437C File Offset: 0x0009257C
		private void InitializeRegionalSettings()
		{
			RegionalSettingsConfiguration regionalSettingsConfiguration = null;
			this.HasRegionalSettings = false;
			RegionalSettingsConfiguration regionalSettingsConfiguration2 = new RegionalSettingsConfiguration(new MailboxRegionalConfiguration());
			if (base.IsInRole("Get-MailboxRegionalConfiguration?Identity@R:Self") && base.IsInRole("!DelegatedAdmin"))
			{
				if (HttpRuntime.Cache[this.RegionalCacheKey] != null)
				{
					regionalSettingsConfiguration2 = (RegionalSettingsConfiguration)HttpRuntime.Cache[this.RegionalCacheKey];
					this.regionalCmdletStatus = LocalSession.RegionalCmdletStatus.Finished;
				}
				else
				{
					this.regionalCmdletStatus = LocalSession.RegionalCmdletStatus.Waiting;
					regionalSettingsConfiguration2 = new RegionalSettingsConfiguration(new MailboxRegionalConfiguration
					{
						Language = base.UserCulture
					});
				}
				if (base.IsInRole("MailboxFullAccess"))
				{
					regionalSettingsConfiguration = regionalSettingsConfiguration2;
					this.HasRegionalSettings = (regionalSettingsConfiguration != null && regionalSettingsConfiguration.UserCulture != null);
					if (this.regionalCmdletStatus == LocalSession.RegionalCmdletStatus.Finished)
					{
						this.HasRegionalSettings = (this.HasRegionalSettings && regionalSettingsConfiguration.TimeZone != null);
					}
				}
			}
			else
			{
				this.regionalCmdletStatus = LocalSession.RegionalCmdletStatus.NotNeeded;
			}
			bool flag = !this.HasRegionalSettings;
			HttpContext httpContext = HttpContext.Current;
			CultureInfo cultureInfo = (regionalSettingsConfiguration == null) ? null : regionalSettingsConfiguration.UserCulture;
			if (regionalSettingsConfiguration == null || (!this.CanRedirectToOwa(httpContext) && !this.HasRegionalSettings) || (cultureInfo == null && httpContext.TargetServerOrVersionSpecifiedInUrlOrCookie()))
			{
				if (regionalSettingsConfiguration != null && cultureInfo == null && regionalSettingsConfiguration.MailboxRegionalConfiguration.Language != null)
				{
					EcpEventLogConstants.Tuple_LanguagePackIsNotInstalled.LogEvent(new object[]
					{
						EcpEventLogExtensions.GetUserNameToLog(),
						regionalSettingsConfiguration.MailboxRegionalConfiguration.Language.IetfLanguageTag
					});
				}
				regionalSettingsConfiguration = this.GetDefaultRegionalSettings(regionalSettingsConfiguration2);
				this.HasRegionalSettings = true;
			}
			string text = httpContext.Request.QueryString["mkt"];
			if (flag && string.IsNullOrEmpty(text))
			{
				text = httpContext.Request.QueryString["mkt2"];
			}
			if (!string.IsNullOrEmpty(text))
			{
				httpContext.Response.Cookies.Add(new HttpCookie("mkt", text));
			}
			else
			{
				HttpCookie httpCookie = httpContext.Request.Cookies["mkt"];
				if (httpCookie != null)
				{
					text = httpCookie.Value;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					CultureInfo cultureInfoByIetfLanguageTag = CultureInfo.GetCultureInfoByIetfLanguageTag(text);
					if (Culture.IsSupportedCulture(cultureInfoByIetfLanguageTag))
					{
						MailboxRegionalConfiguration mailboxRegionalConfiguration = new MailboxRegionalConfiguration();
						mailboxRegionalConfiguration.Language = Culture.GetCultureInfoInstance(cultureInfoByIetfLanguageTag.LCID);
						mailboxRegionalConfiguration.TimeZone = regionalSettingsConfiguration.MailboxRegionalConfiguration.TimeZone;
						if (regionalSettingsConfiguration.UserCulture != null && regionalSettingsConfiguration.UserCulture.DateTimeFormat != null)
						{
							mailboxRegionalConfiguration.DateFormat = regionalSettingsConfiguration.DateFormat;
							mailboxRegionalConfiguration.TimeFormat = regionalSettingsConfiguration.TimeFormat;
						}
						regionalSettingsConfiguration = new RegionalSettingsConfiguration(mailboxRegionalConfiguration);
						this.HasRegionalSettings = true;
					}
				}
				catch (ArgumentException)
				{
				}
			}
			if (this.HasRegionalSettings)
			{
				this.UpdateRegionalSettings(regionalSettingsConfiguration);
			}
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x00094630 File Offset: 0x00092830
		private bool CanRedirectToOwa(HttpContext context)
		{
			return base.IsInRole("Mailbox+OWA") && (!context.IsAcsOAuthRequest() || !context.IsWebServiceRequest());
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x00094654 File Offset: 0x00092854
		private RegionalSettingsConfiguration GetDefaultRegionalSettings(RegionalSettingsConfiguration executingUserSettings)
		{
			return new RegionalSettingsConfiguration(new MailboxRegionalConfiguration
			{
				Language = (executingUserSettings.UserCulture ?? Culture.GetDefaultCulture(HttpContext.Current)),
				TimeZone = executingUserSettings.MailboxRegionalConfiguration.TimeZone
			});
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x00094698 File Offset: 0x00092898
		private void WaitingForCmdlet()
		{
			if (this.regionalCmdletStatus == LocalSession.RegionalCmdletStatus.Waiting)
			{
				this.regionalCmdletStatus = LocalSession.RegionalCmdletStatus.Running;
				RegionalSettingsConfiguration settings = new RegionalSettingsConfiguration(new MailboxRegionalConfiguration());
				RegionalSettings regionalSettings = new RegionalSettings();
				PowerShellResults<RegionalSettingsConfiguration> settings2 = regionalSettings.GetSettings(Microsoft.Exchange.Management.ControlPanel.Identity.FromExecutingUserId(), false);
				if (settings2.SucceededWithValue)
				{
					settings = settings2.Value;
					this.AddRegionalSettingsToCache(settings);
					this.InitializeRegionalSettings();
				}
			}
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000946F0 File Offset: 0x000928F0
		public void UpdateRegionalSettings(RegionalSettingsConfiguration settings)
		{
			this.DateFormat = settings.DateFormat;
			this.TimeFormat = settings.TimeFormat;
			base.UserCulture = RoleBasedStringMapping.GetRoleBasedCultureInfo(settings.UserCulture, base.RbacConfiguration.RoleTypes);
			this.UpdateUserTimeZone(settings.TimeZone);
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x00094740 File Offset: 0x00092940
		internal void UpdateUserTimeZone(string userTimeZone)
		{
			ExTimeZone userTimeZone2 = null;
			if (!string.IsNullOrEmpty(userTimeZone))
			{
				ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(userTimeZone, out userTimeZone2);
			}
			this.UserTimeZone = userTimeZone2;
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x0009476C File Offset: 0x0009296C
		public override void RequestReceived()
		{
			this.ThrowIfUserIsMailboxButNoMyBaseOptions();
			this.RedirectIfUserEsoSelf();
			this.RedirectIfTargetTenantSpecifiedForRegularAdmin();
			bool flag = !this.HasRegionalSettings;
			HttpContext httpContext = HttpContext.Current;
			DiagnosticsBehavior.CheckSystemProbeCookie(httpContext);
			string text = HttpContext.Current.Request.QueryString["mkt"];
			if (string.IsNullOrEmpty(text))
			{
				text = HttpContext.Current.Request.QueryString["mkt2"];
			}
			if (this.HasRegionalSettings && !string.IsNullOrEmpty(text))
			{
				try
				{
					CultureInfo cultureInfoByIetfLanguageTag = CultureInfo.GetCultureInfoByIetfLanguageTag(text);
					if (Culture.IsSupportedCulture(cultureInfoByIetfLanguageTag) && base.UserCulture.LCID != RoleBasedStringMapping.GetRoleBasedCultureInfo(cultureInfoByIetfLanguageTag, base.RbacConfiguration.RoleTypes).LCID)
					{
						flag = true;
					}
				}
				catch (ArgumentException)
				{
				}
			}
			if (flag)
			{
				this.InitializeRegionalSettings();
				if (!this.HasRegionalSettings)
				{
					if (!(httpContext.Request.HttpMethod == "GET"))
					{
						throw new RegionalSettingsNotConfiguredException(base.ExecutingUserId);
					}
					base.RbacConfiguration.ExecutingUserLanguagesChanged = true;
					string url = string.Format(EcpUrl.OwaVDir + "languageselection.aspx?url={0}", HttpUtility.UrlEncode(EcpUrl.ProcessUrl(httpContext.GetRequestUrlPathAndQuery())));
					httpContext.Response.Redirect(url, true);
				}
			}
			base.RequestReceived();
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000948B4 File Offset: 0x00092AB4
		private void ThrowIfUserIsMailboxButNoMyBaseOptions()
		{
			if (!base.IsInRole("ControlPanelAdmin") && base.IsInRole("Mailbox") && !base.IsInRole(RoleType.MyBaseOptions.ToString()))
			{
				ExTraceGlobals.RBACTracer.TraceInformation<string>(0, 0L, "{0} is not allowed to logon to ECP as the user has mailbox but doesn't have MyBaseOptions role.", base.Name);
				throw new CmdletAccessDeniedException(Strings.UserMissingMyBaseOptionsRole(base.NameForEventLog));
			}
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x00094918 File Offset: 0x00092B18
		private void RedirectIfUserEsoSelf()
		{
			if (base.Settings.LogonUserEsoSelf)
			{
				HttpContext httpContext = HttpContext.Current;
				httpContext.Response.Redirect(httpContext.GetRequestUrlPathAndQuery(), true);
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x00094950 File Offset: 0x00092B50
		private void RedirectIfTargetTenantSpecifiedForRegularAdmin()
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext.HasTargetTenant() && (base.IsInRole("LogonUserMailbox") || base.IsInRole("LogonMailUser")))
			{
				string url = EcpUrl.ProcessUrl(httpContext.GetRequestUrlPathAndQuery(), true, EcpUrl.EcpVDirForStaticResource, true, false);
				httpContext.Response.Redirect(url, true);
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x000949AC File Offset: 0x00092BAC
		public virtual void FlushCache()
		{
			if (this.isDehydrated != null)
			{
				this.isDehydrated = null;
				this.IsDehydrated.ToString();
			}
		}

		// Token: 0x17001F49 RID: 8009
		// (get) Token: 0x060030C4 RID: 12484 RVA: 0x000949E4 File Offset: 0x00092BE4
		// (set) Token: 0x060030C5 RID: 12485 RVA: 0x00094A3F File Offset: 0x00092C3F
		public int WeekStartDay
		{
			get
			{
				if (this.weekStartDay == -1)
				{
					if (!base.IsInRole("Get-MailboxCalendarConfiguration?Identity@R:Self"))
					{
						return 0;
					}
					CalendarAppearance calendarAppearance = new CalendarAppearance();
					PowerShellResults<CalendarAppearanceConfiguration> @object = calendarAppearance.GetObject(Microsoft.Exchange.Management.ControlPanel.Identity.FromExecutingUserId());
					this.weekStartDay = (@object.SucceededWithValue ? @object.Output[0].WeekStartDay : 0);
				}
				return this.weekStartDay;
			}
			set
			{
				this.weekStartDay = value;
			}
		}

		// Token: 0x04002382 RID: 9090
		internal const string RegionalKeySuffix = "_Regional";

		// Token: 0x04002383 RID: 9091
		private readonly int logonTypeFlag;

		// Token: 0x04002384 RID: 9092
		private static List<string> roleList = new List<string>
		{
			"Impersonated",
			"Mailbox",
			"MailboxFullAccess",
			"ByoidAdmin",
			"DelegatedAdmin",
			"Admin"
		};

		// Token: 0x04002385 RID: 9093
		private LocalSession.RegionalCmdletStatus regionalCmdletStatus;

		// Token: 0x04002386 RID: 9094
		private SmtpAddress? executingUserPrimarySmtpAddress;

		// Token: 0x04002387 RID: 9095
		private bool? isDehydrated;

		// Token: 0x04002388 RID: 9096
		private bool? isSoftDeletedFeatureEnabled;

		// Token: 0x04002389 RID: 9097
		private int weekStartDay = -1;

		// Token: 0x02000392 RID: 914
		private enum RegionalCmdletStatus
		{
			// Token: 0x0400238D RID: 9101
			Initial,
			// Token: 0x0400238E RID: 9102
			Waiting,
			// Token: 0x0400238F RID: 9103
			Finished,
			// Token: 0x04002390 RID: 9104
			NotNeeded,
			// Token: 0x04002391 RID: 9105
			Running
		}
	}
}
