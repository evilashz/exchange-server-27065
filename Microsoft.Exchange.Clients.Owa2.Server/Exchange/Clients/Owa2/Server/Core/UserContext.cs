using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Caching;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Clients.Owa2.Server.Web;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Configuration;
using Microsoft.Exchange.Data.Storage.LinkedFolder;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000BB RID: 187
	internal sealed class UserContext : MailboxContextBase, IUserContext, IMailboxContext, IDisposable
	{
		// Token: 0x0600073D RID: 1853 RVA: 0x00016764 File Offset: 0x00014964
		internal UserContext(UserContextKey key, string userAgent) : base(key, userAgent)
		{
			this.themeKey = key + "Theme";
			this.LogEventCommonData = LogEventCommonData.NullInstance;
			if (!Globals.Owa2ServerUnitTestsHook)
			{
				this.lastUserRequestTime = Globals.ApplicationTime;
				this.signIntoIMTime = Globals.ApplicationTime;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x000167E9 File Offset: 0x000149E9
		public string Canary
		{
			get
			{
				return base.Key.UserContextId;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x000167F8 File Offset: 0x000149F8
		internal AttachmentDataProviderManager AttachmentDataProviderManager
		{
			get
			{
				if (this.attachmentDataProviderManager == null)
				{
					lock (this.syncRoot)
					{
						if (this.attachmentDataProviderManager == null)
						{
							this.attachmentDataProviderManager = new AttachmentDataProviderManager();
						}
					}
				}
				return this.attachmentDataProviderManager;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x00016854 File Offset: 0x00014A54
		internal CancelAttachmentManager CancelAttachmentManager
		{
			get
			{
				if (this.cancelAttachmentManager == null)
				{
					lock (this.syncRoot)
					{
						if (this.cancelAttachmentManager == null)
						{
							this.cancelAttachmentManager = new CancelAttachmentManager(this);
						}
					}
				}
				return this.cancelAttachmentManager;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x000168B0 File Offset: 0x00014AB0
		// (set) Token: 0x06000742 RID: 1858 RVA: 0x000168B8 File Offset: 0x00014AB8
		internal bool HasActiveHierarchySubscription
		{
			get
			{
				return this.hasActiveHierarchySubscription;
			}
			set
			{
				this.hasActiveHierarchySubscription = value;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x000168C4 File Offset: 0x00014AC4
		public InstantMessagingTypeOptions InstantMessageType
		{
			get
			{
				if (this.instantMessageType == null)
				{
					if (base.ExchangePrincipal != null)
					{
						ConfigurationContext configurationContext = new ConfigurationContext(this);
						this.instantMessageType = new InstantMessagingTypeOptions?((configurationContext != null) ? configurationContext.InstantMessagingType : InstantMessagingTypeOptions.None);
					}
					else
					{
						this.instantMessageType = new InstantMessagingTypeOptions?(InstantMessagingTypeOptions.None);
					}
				}
				return this.instantMessageType.Value;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0001691D File Offset: 0x00014B1D
		// (set) Token: 0x06000745 RID: 1861 RVA: 0x00016925 File Offset: 0x00014B25
		internal string BposSkuCapability { get; private set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00016930 File Offset: 0x00014B30
		public ulong AllowedCapabilitiesFlags
		{
			get
			{
				if (this.allowedCapabilitiesFlags == 0UL)
				{
					if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.CheckFeatureRestrictions.Enabled && base.MailboxIdentity != null && !Globals.IsPreCheckinApp)
					{
						OWAMiniRecipient owaminiRecipient = base.MailboxIdentity.GetOWAMiniRecipient();
						if (owaminiRecipient[ADUserSchema.PersistedCapabilities] != null)
						{
							foreach (object obj in Enum.GetValues(typeof(Feature)))
							{
								ulong num = (ulong)obj;
								if (num != 18446744073709551615UL)
								{
									string text = string.Format("Owa{0}Restrictions", Enum.GetName(typeof(Feature), num));
									try
									{
										if (ExchangeRunspaceConfiguration.IsFeatureValidOnObject(text, owaminiRecipient))
										{
											this.allowedCapabilitiesFlags |= num;
										}
										else
										{
											ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Feature {0} is restricted by user capabilities", text);
										}
									}
									catch (ArgumentException ex)
									{
										ExTraceGlobals.UserContextTracer.TraceDebug<string, string>(0L, "There was an exception in ExchangeRunspaceConfiguration.IsFeatureValidOnObject when validating feature: {0}. {1}", text, ex.Message);
									}
								}
							}
						}
					}
					if (this.allowedCapabilitiesFlags == 0UL)
					{
						this.allowedCapabilitiesFlags = ulong.MaxValue;
					}
				}
				return this.allowedCapabilitiesFlags;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00016A88 File Offset: 0x00014C88
		public long LastUserRequestTime
		{
			get
			{
				return this.lastUserRequestTime;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00016A90 File Offset: 0x00014C90
		public string SipUri
		{
			get
			{
				return this.sipUri;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x00016A98 File Offset: 0x00014C98
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x00016AB4 File Offset: 0x00014CB4
		public CultureInfo UserCulture
		{
			get
			{
				if (this.userCulture == null)
				{
					return Culture.GetPreferredCultureInfo(base.ExchangePrincipal);
				}
				return this.userCulture;
			}
			set
			{
				this.userCulture = value;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x00016ABD File Offset: 0x00014CBD
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x00016AC5 File Offset: 0x00014CC5
		internal bool IsGroupUserContext { get; private set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x00016ACE File Offset: 0x00014CCE
		internal bool IsUserCultureExplicitlySet
		{
			get
			{
				return this.userCulture != null;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x00016ADC File Offset: 0x00014CDC
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x00016AE4 File Offset: 0x00014CE4
		public long SignIntoIMTime
		{
			get
			{
				return this.signIntoIMTime;
			}
			set
			{
				this.signIntoIMTime = value;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x00016AED File Offset: 0x00014CED
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x00016B2C File Offset: 0x00014D2C
		public bool IsPublicLogon
		{
			get
			{
				if (this.isPublicLogon == null)
				{
					this.isPublicLogon = new bool?(UserContextUtilities.IsPublicLogon(base.ExchangePrincipal.MailboxInfo.OrganizationId, HttpContext.Current));
				}
				return this.isPublicLogon.Value;
			}
			set
			{
				this.isPublicLogon = new bool?(value);
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00016B3C File Offset: 0x00014D3C
		internal List<IConnectedAccountsNotificationManager> GetConnectedAccountNotificationManagers(MailboxSession mailboxSession)
		{
			if (UserAgentUtilities.IsMonitoringRequest(HttpContext.Current.Request.UserAgent))
			{
				return new List<IConnectedAccountsNotificationManager>();
			}
			if (this.isConnectedAccountsNotificationSetupDone)
			{
				return this.connectedAccountNotificationManagers;
			}
			this.SetupTxSyncNotificationManager(mailboxSession);
			this.SetupMrsNotificationManager(mailboxSession);
			this.isConnectedAccountsNotificationSetupDone = true;
			return this.connectedAccountNotificationManagers;
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00016B8F File Offset: 0x00014D8F
		public PlayOnPhoneNotificationManager PlayOnPhoneNotificationManager
		{
			get
			{
				if (this.playonPhoneNotificationManager == null)
				{
					this.playonPhoneNotificationManager = new PlayOnPhoneNotificationManager(this);
				}
				return this.playonPhoneNotificationManager;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00016BAB File Offset: 0x00014DAB
		public InstantMessageManager InstantMessageManager
		{
			get
			{
				if (this.IsInstantMessageEnabled && this.instantMessageManager == null)
				{
					this.instantMessageManager = new InstantMessageManager(this);
				}
				return this.instantMessageManager;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00016BCF File Offset: 0x00014DCF
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x00016BD7 File Offset: 0x00014DD7
		internal bool IsBposUser { get; private set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x00016BE0 File Offset: 0x00014DE0
		public BposNavBarInfoAssetReader BposNavBarInfoAssetReader
		{
			get
			{
				if (this.IsBposUser && this.bposNavBarInfoAssetReader == null)
				{
					lock (this.syncRoot)
					{
						if (this.bposNavBarInfoAssetReader == null)
						{
							this.bposNavBarInfoAssetReader = new BposNavBarInfoAssetReader(base.LogonIdentity.GetOWAMiniRecipient().UserPrincipalName, this.UserCulture);
						}
					}
				}
				return this.bposNavBarInfoAssetReader;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x00016C5C File Offset: 0x00014E5C
		public BposShellInfoAssetReader BposShellInfoAssetReader
		{
			get
			{
				if (this.IsBposUser && this.bposShellInfoAssetReader == null)
				{
					lock (this.syncRoot)
					{
						if (this.bposShellInfoAssetReader == null)
						{
							BposHeaderFlight currentHeaderFlight = BposHeaderFlight.E15Parity;
							if (this.FeaturesManager.ClientServerSettings.O365G2Header.Enabled)
							{
								currentHeaderFlight = BposHeaderFlight.E16Gemini2;
							}
							else if (this.FeaturesManager.ClientServerSettings.O365Header.Enabled)
							{
								currentHeaderFlight = BposHeaderFlight.E16Gemini1;
							}
							this.bposShellInfoAssetReader = new BposShellInfoAssetReader(base.LogonIdentity.GetOWAMiniRecipient().UserPrincipalName, this.UserCulture, currentHeaderFlight, this);
						}
					}
				}
				return this.bposShellInfoAssetReader;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x00016D1C File Offset: 0x00014F1C
		public bool IsInstantMessageEnabled
		{
			get
			{
				return this.InstantMessageType == InstantMessagingTypeOptions.Ocs && InstantMessageOCSProvider.EndpointManager != null;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x00016D3C File Offset: 0x00014F3C
		internal InstantSearchManager InstantSearchManager
		{
			get
			{
				if (this.instantSearchManager != null)
				{
					return this.instantSearchManager;
				}
				lock (this.syncRoot)
				{
					if (this.instantSearchManager == null)
					{
						this.instantSearchManager = new InstantSearchManager(() => this.CreateMailboxSessionForInstantSearch());
					}
				}
				return this.instantSearchManager;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x00016DBC File Offset: 0x00014FBC
		internal IInstantSearchNotificationHandler InstantSearchNotificationHandler
		{
			get
			{
				if (this.instantSearchNotificationHandler != null)
				{
					return this.instantSearchNotificationHandler;
				}
				lock (this.syncRoot)
				{
					if (this.instantSearchNotificationHandler == null)
					{
						if (base.IsExplicitLogon)
						{
							this.instantSearchNotificationHandler = new InstantSearchRemoteNotificationHandler(this);
						}
						else
						{
							this.instantSearchNotificationHandler = new InstantSearchNotificationHandler(this);
						}
					}
				}
				return this.instantSearchNotificationHandler;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x00016E44 File Offset: 0x00015044
		// (set) Token: 0x0600075D RID: 1885 RVA: 0x00016E4C File Offset: 0x0001504C
		internal LogEventCommonData LogEventCommonData { get; private set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00016E58 File Offset: 0x00015058
		internal Theme Theme
		{
			get
			{
				Theme theme = (Theme)HttpRuntime.Cache.Get(this.themeKey);
				if (theme == null)
				{
					lock (this.syncRoot)
					{
						theme = (Theme)HttpRuntime.Cache.Get(this.themeKey);
						if (theme == null)
						{
							theme = this.LoadUserTheme();
							HttpRuntime.Cache.Insert(this.themeKey, theme, null, DateTime.UtcNow.AddMinutes(1.0), Cache.NoSlidingExpiration);
						}
					}
				}
				return theme;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00016EFC File Offset: 0x000150FC
		internal Theme DefaultTheme
		{
			get
			{
				ConfigurationContext configurationContext = new ConfigurationContext(this);
				string text = configurationContext.DefaultTheme;
				if (this.defaultTheme == null || this.defaultTheme.StorageId != text)
				{
					this.defaultTheme = ThemeManagerFactory.GetInstance(this.CurrentOwaVersion).GetDefaultTheme(text);
				}
				return this.defaultTheme;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x00016F51 File Offset: 0x00015151
		public FeaturesManager FeaturesManager
		{
			get
			{
				if (this.featuresManagerFactory == null)
				{
					return null;
				}
				return this.featuresManagerFactory.GetFeaturesManager(HttpContext.Current);
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00016F6D File Offset: 0x0001516D
		public override SessionDataCache SessionDataCache
		{
			get
			{
				if (this.sessionDataCache == null)
				{
					this.sessionDataCache = new SessionDataCache();
				}
				return this.sessionDataCache;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00016F88 File Offset: 0x00015188
		internal bool IsOptimizedForAccessibility
		{
			get
			{
				if (this.isOptimizedForAccessibility == null)
				{
					try
					{
						if (base.IsExplicitLogon)
						{
							this.isOptimizedForAccessibility = new bool?(false);
						}
						else
						{
							base.LockAndReconnectMailboxSession(3000);
							UserConfigurationPropertyDefinition propertyDefinition = UserOptionPropertySchema.Instance.GetPropertyDefinition(UserConfigurationPropertyId.IsOptimizedForAccessibility);
							UserOptionsType userOptionsType = new UserOptionsType();
							userOptionsType.Load(base.MailboxSession, new UserConfigurationPropertyDefinition[]
							{
								propertyDefinition
							}, true);
							this.isOptimizedForAccessibility = new bool?(userOptionsType.IsOptimizedForAccessibility);
						}
					}
					catch (Exception ex)
					{
						ExTraceGlobals.CoreTracer.TraceError<string, string>(0L, "Failed to retrieve IsOptimizedForAccessibility from user options. Error: {0}. Stack: {1}.", ex.Message, ex.StackTrace);
						this.isOptimizedForAccessibility = null;
						throw;
					}
					finally
					{
						base.UnlockAndDisconnectMailboxSession();
					}
					base.LogTrace("IsOptimizedForAccessibility", "userOptions.IsOptimizedForAccessibility loaded");
				}
				return this.isOptimizedForAccessibility.Value;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x00017074 File Offset: 0x00015274
		public ADObjectId GlobalAddressListId
		{
			get
			{
				if (this.globalAddressListId == null)
				{
					IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, base.ExchangePrincipal.MailboxInfo.OrganizationId.ToADSessionSettings(), 899, "GlobalAddressListId", "f:\\15.00.1497\\sources\\dev\\clients\\src\\Owa2\\Server\\Core\\common\\UserContext.cs");
					this.globalAddressListId = DirectoryHelper.GetGlobalAddressListFromAddressBookPolicy(base.ExchangePrincipal.MailboxInfo.Configuration.AddressBookPolicy, tenantOrTopologyConfigurationSession);
				}
				return this.globalAddressListId;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x000170E4 File Offset: 0x000152E4
		public string CurrentOwaVersion
		{
			get
			{
				if (string.IsNullOrEmpty(this.currentOwaVersion))
				{
					if (this.FeaturesManager.ServerSettings.OwaVNext.Enabled)
					{
						this.currentOwaVersion = OwaVersionId.VNext;
					}
					else
					{
						this.currentOwaVersion = OwaVersionId.Current;
					}
				}
				return this.currentOwaVersion;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x00017138 File Offset: 0x00015338
		public bool IsWacEditingEnabled
		{
			get
			{
				if (this.isWacEditingEnabled == null)
				{
					WacConfigData wacEditingEnabled = AttachmentPolicy.ReadAggregatedWacData(this, null);
					this.SetWacEditingEnabled(wacEditingEnabled);
				}
				return this.isWacEditingEnabled.Value;
			}
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001716C File Offset: 0x0001536C
		internal void SetWacEditingEnabled(WacConfigData wacData)
		{
			bool flag = AttachmentPolicy.IsAttachmentDataProviderAvailable(wacData);
			this.isWacEditingEnabled = new bool?(wacData.IsWacEditingEnabled && flag);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00017198 File Offset: 0x00015398
		public string[] GetClientWatsonHistory()
		{
			string[] result;
			lock (this.clientWatsonHistoryLock)
			{
				result = this.clientWatsonHistory.ToArray();
			}
			return result;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000171E0 File Offset: 0x000153E0
		public void SaveToClientWatsonHistory(params string[] clientWatsonsData)
		{
			lock (this.clientWatsonHistoryLock)
			{
				int num = (clientWatsonsData.Length <= 5) ? 0 : (clientWatsonsData.Length - 5);
				for (int i = num; i < clientWatsonsData.Length; i++)
				{
					if (this.clientWatsonHistory.Count == 5)
					{
						this.clientWatsonHistory.Dequeue();
					}
					this.clientWatsonHistory.Enqueue(clientWatsonsData[i]);
				}
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00017260 File Offset: 0x00015460
		public void UpdateLastUserRequestTime()
		{
			this.lastUserRequestTime = Globals.ApplicationTime;
			if (this.IsInstantMessageEnabled && this.InstantMessageManager != null)
			{
				this.instantMessageManager.ResetPresence();
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00017288 File Offset: 0x00015488
		public void Touch()
		{
			HttpRuntime.Cache.Get(base.Key.ToString());
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x000172A0 File Offset: 0x000154A0
		public void ClearCachedTheme()
		{
			lock (this.syncRoot)
			{
				HttpRuntime.Cache.Remove(this.themeKey);
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000172EC File Offset: 0x000154EC
		public bool LockAndReconnectMailboxSession()
		{
			return base.LockAndReconnectMailboxSession(3000);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000172FC File Offset: 0x000154FC
		public void DoLogoffCleanup()
		{
			base.LogBreadcrumb("DoLogoffCleanup");
			if (!this.isMailboxSessionCreated)
			{
				ExTraceGlobals.UserContextTracer.TraceDebug((long)this.GetHashCode(), "DoLogoffCleanup - No mailbox session on the user context, no cleanup necessary");
				return;
			}
			try
			{
				this.LockAndReconnectMailboxSession();
				UserConfigurationPropertyDefinition propertyDefinition = UserOptionPropertySchema.Instance.GetPropertyDefinition(UserConfigurationPropertyId.EmptyDeletedItemsOnLogoff);
				UserOptionsType userOptionsType = new UserOptionsType();
				userOptionsType.Load(base.MailboxSession, new UserConfigurationPropertyDefinition[]
				{
					propertyDefinition
				});
				if (userOptionsType.EmptyDeletedItemsOnLogoff)
				{
					ExTraceGlobals.UserContextTracer.TraceDebug((long)this.GetHashCode(), "DoLogoffCleanup - Emptying deleted items folder.");
					base.MailboxSession.DeleteAllObjects(DeleteItemFlags.SoftDelete, base.MailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems));
				}
			}
			catch (OwaLockTimeoutException)
			{
				ExTraceGlobals.UserContextTracer.TraceDebug((long)this.GetHashCode(), "DoLogoffCleanup - Encountered OwaLockTimeoutException");
			}
			finally
			{
				base.UnlockAndDisconnectMailboxSession();
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000173F4 File Offset: 0x000155F4
		protected override void DoLoad(OwaIdentity logonIdentity, OwaIdentity mailboxIdentity, UserContextStatistics stats)
		{
			HttpContext httpContext = HttpContext.Current;
			RequestDetailsLogger current = RequestDetailsLoggerBase<RequestDetailsLogger>.GetCurrent(httpContext);
			RequestDetailsLogger.LogEvent(current, OwaServerLogger.LoggerData.UserContextLoadBegin);
			base.DoLoad(logonIdentity, mailboxIdentity, stats);
			RequestDetailsLogger.LogEvent(current, OwaServerLogger.LoggerData.GetOWAMiniRecipientBegin);
			Stopwatch stopwatch = Stopwatch.StartNew();
			OWAMiniRecipient owaminiRecipient = base.LogonIdentity.GetOWAMiniRecipient();
			stats.MiniRecipientCreationTime = (int)stopwatch.ElapsedMilliseconds;
			RequestDetailsLogger.LogEvent(current, OwaServerLogger.LoggerData.GetOWAMiniRecipientEnd);
			base.LogTrace("UserContext.Load", "GetOWAMiniRecipient finished");
			this.sipUri = ADPersonToContactConverter.GetSipUri(owaminiRecipient);
			stopwatch.Restart();
			this.IsBposUser = CapabilityHelper.HasBposSKUCapability(owaminiRecipient.PersistedCapabilities);
			stats.SKUCapabilityTestTime = (int)stopwatch.ElapsedMilliseconds;
			base.LogTrace("UserContext.Load", "HasBposSKUCapability finished");
			if (Globals.IsFirstReleaseFlightingEnabled)
			{
				this.CreateFeatureManagerFactory(owaminiRecipient);
			}
			else
			{
				RecipientTypeDetails recipientTypeDetails = base.ExchangePrincipal.RecipientTypeDetails;
				this.featuresManagerFactory = new FeaturesManagerFactory(owaminiRecipient, new ConfigurationContext(this), new ScopeFlightsSettingsProvider(), (VariantConfigurationSnapshot c) => new FeaturesStateOverride(c, recipientTypeDetails), string.Empty, false);
			}
			this.BposSkuCapability = string.Empty;
			if (this.IsBposUser)
			{
				Capability? skucapability = CapabilityHelper.GetSKUCapability(owaminiRecipient.PersistedCapabilities);
				if (skucapability != null)
				{
					this.BposSkuCapability = skucapability.ToString();
				}
			}
			this.LogEventCommonData = new LogEventCommonData(this);
			this.IsGroupUserContext = (base.IsExplicitLogon && base.ExchangePrincipal.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox);
			RequestDetailsLogger.LogEvent(current, OwaServerLogger.LoggerData.UserContextLoadEnd);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001757C File Offset: 0x0001577C
		protected override INotificationManager GetNotificationManager(MailboxContextBase mailboxContext)
		{
			if (this.FeaturesManager.ClientServerSettings.NotificationBroker.Enabled)
			{
				return new BrokerNotificationManager(mailboxContext);
			}
			return base.GetNotificationManager(mailboxContext);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000175B4 File Offset: 0x000157B4
		private MailboxSession CreateMailboxSessionForInstantSearch()
		{
			MailboxSession mailboxSession = base.LogonIdentity.CreateInstantSearchMailboxSession(base.ExchangePrincipal, Thread.CurrentThread.CurrentCulture);
			if (mailboxSession == null)
			{
				throw new OwaInvalidOperationException("CreateMailboxSession cannot create a mailbox session");
			}
			return mailboxSession;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x000175EC File Offset: 0x000157EC
		public override void ValidateLogonPermissionIfNecessary()
		{
			if (!base.IsExplicitLogon)
			{
				return;
			}
			if (base.ExchangePrincipal.RecipientTypeDetails == RecipientTypeDetails.TeamMailbox)
			{
				ExTraceGlobals.ConnectedAccountsTracer.TraceDebug(0L, "Validate explicit logon permission by creating a mailbox session.");
				base.CreateMailboxSessionIfNeeded();
				return;
			}
			this.ValidateBackendServerIfNeeded();
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001762C File Offset: 0x0001582C
		private void SetupTxSyncNotificationManager(MailboxSession mailboxSession)
		{
			if (!VariantConfiguration.InvariantNoFlightingSnapshot.OwaDeployment.ConnectedAccountsSync.Enabled || !ConnectedAccountsConfiguration.Instance.NotificationsEnabled)
			{
				ExTraceGlobals.ConnectedAccountsTracer.TraceDebug((long)this.GetHashCode(), "UserContext.SetupTxSyncNotificationManager - ConnectedAccountsNotificationManager was not set because no DC or Notifications not enabled.");
				return;
			}
			IConnectedAccountsNotificationManager connectedAccountsNotificationManager = TxSyncConnectedAccountsNotificationManager.Create(mailboxSession, this);
			if (connectedAccountsNotificationManager != null)
			{
				this.connectedAccountNotificationManagers.Add(connectedAccountsNotificationManager);
			}
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001768C File Offset: 0x0001588C
		internal UserConfigurationManager.IAggregationContext TryConsumeBootAggregation()
		{
			UserConfigurationManager.IAggregationContext comparand = this.bootAggregationContext;
			return Interlocked.CompareExchange<UserConfigurationManager.IAggregationContext>(ref this.bootAggregationContext, null, comparand);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000176B0 File Offset: 0x000158B0
		internal void SetupMrsNotificationManager(MailboxSession mailboxSession)
		{
			if (!VariantConfiguration.InvariantNoFlightingSnapshot.OwaDeployment.MrsConnectedAccountsSync.Enabled || !ConnectedAccountsConfiguration.Instance.NotificationsEnabled)
			{
				ExTraceGlobals.ConnectedAccountsTracer.TraceDebug((long)this.GetHashCode(), "UserContext.SetupMrsNotificationManager - ConnectedAccountsNotificationManager was not set because no DC or Notifications not enabled.");
				return;
			}
			IConnectedAccountsNotificationManager connectedAccountsNotificationManager = MrsConnectedAccountsNotificationManager.Create(mailboxSession, this);
			if (connectedAccountsNotificationManager != null)
			{
				this.connectedAccountNotificationManagers.Add(connectedAccountsNotificationManager);
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00017710 File Offset: 0x00015910
		internal void RetireMailboxSessionForGroupMailbox()
		{
			if (!this.IsGroupUserContext)
			{
				throw new InvalidOperationException("RetireMailboxSessionForGroupMailbox is only supported for group mailbox");
			}
			try
			{
				base.InternalRetireMailboxSession();
			}
			catch (LocalizedException ex)
			{
				OwaServerTraceLogger.AppendToLog(new TraceLogEvent("RetUcMbSess", this, "UserContext.RetireMailboxSessionForGroupMailbox", ex.ToString()));
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00017768 File Offset: 0x00015968
		internal AddressBookBase GetGlobalAddressList(IBudget budget)
		{
			if (!this.isGlobalAddressListLoaded)
			{
				IConfigurationSession configurationSession = UserContextUtilities.CreateADSystemConfigurationSession(true, ConsistencyMode.IgnoreInvalid, this, budget);
				IRecipientSession recipientSession = UserContextUtilities.CreateADRecipientSession(CultureInfo.CurrentCulture.LCID, true, ConsistencyMode.IgnoreInvalid, false, this, false, budget);
				this.globalAddressList = AddressBookBase.GetGlobalAddressList(base.LogonIdentity.ClientSecurityContext, configurationSession, recipientSession, this.GlobalAddressListId);
				this.isGlobalAddressListLoaded = true;
			}
			return this.globalAddressList;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000177C8 File Offset: 0x000159C8
		internal void RefreshUserSettings(CultureInfo userCulture, EcpUserSettings userSettings)
		{
			if (userCulture != null)
			{
				this.userCulture = userCulture;
				Culture.InternalSetThreadPreferredCulture(userCulture);
			}
			this.RefreshMailboxSession(userSettings);
			this.ClearBposShellData();
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000177E7 File Offset: 0x000159E7
		internal long GetNextClientActivitySequenceNumber()
		{
			return Interlocked.Increment(ref this.nextActivitySequenceNumber);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001780C File Offset: 0x00015A0C
		internal static OwaFlightConfigData ReadAggregatedFlightConfigData(UserConfigurationManager.IAggregationContext aggregator, OrganizationId orgId)
		{
			return UserContextUtilities.ReadAggregatedType<OwaFlightConfigData>(aggregator, "OWA.FlightConfiguration", () => UserContext.ReadFlightConfigDataFromAD(orgId));
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00017840 File Offset: 0x00015A40
		protected override void DisposeMailboxSessionReferencingObjects()
		{
			base.UserContextDiposeGraph.Append(".uc1");
			if (this.instantMessageManager != null)
			{
				base.UserContextDiposeGraph.Append(".uc2");
				this.instantMessageManager.Dispose();
				this.instantMessageManager = null;
			}
			base.DisposeMailboxSessionReferencingObjects();
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00017890 File Offset: 0x00015A90
		protected override void DisposeNonMailboxSessionReferencingObjects()
		{
			base.UserContextDiposeGraph.Append(".ub1");
			if (this.playonPhoneNotificationManager != null)
			{
				base.UserContextDiposeGraph.Append(".ub2");
				this.playonPhoneNotificationManager.Dispose();
				this.playonPhoneNotificationManager = null;
			}
			if (this.instantSearchManager != null)
			{
				base.UserContextDiposeGraph.Append(".ub3");
				this.instantSearchManager.Dispose();
				this.instantSearchManager = null;
			}
			if (this.instantSearchNotificationHandler != null)
			{
				base.UserContextDiposeGraph.Append(".ub4");
				IDisposable disposable = this.instantSearchNotificationHandler as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				this.instantSearchNotificationHandler = null;
			}
			int num = 5;
			for (int i = 0; i < this.connectedAccountNotificationManagers.Count; i++)
			{
				base.UserContextDiposeGraph.Append(".ub" + num);
				num++;
				this.connectedAccountNotificationManagers[i].Dispose();
				this.connectedAccountNotificationManagers[i] = null;
			}
			this.connectedAccountNotificationManagers = null;
			if (this.sessionDataCache != null)
			{
				this.sessionDataCache.Dispose();
				this.sessionDataCache = null;
			}
			using (this.TryConsumeBootAggregation())
			{
				this.bootAggregationContext = null;
			}
			base.DisposeNonMailboxSessionReferencingObjects();
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000179EC File Offset: 0x00015BEC
		protected override MailboxSession CreateMailboxSession()
		{
			if (base.IsDisposed || this.isInProcessOfDisposing)
			{
				string message = string.Format(CultureInfo.InvariantCulture, "Cannot call UserContext.CreateMailboxSession when object is disposed. isDisposed={0}, isInProcessOfDisposing={1}.", new object[]
				{
					base.IsDisposed,
					this.isInProcessOfDisposing
				});
				throw new ObjectDisposedException("UserContext", message);
			}
			if (base.LogonIdentity == null)
			{
				throw new OwaInvalidOperationException("Cannot call CreateMailboxSession when logonIdentity is null");
			}
			MailboxSession result;
			try
			{
				if (base.ExchangePrincipal.RecipientTypeDetails == RecipientTypeDetails.TeamMailbox)
				{
					Exception ex = null;
					ExTraceGlobals.UserContextTracer.TraceDebug(0L, "Creating Mailbox session for TeamMailbox");
					SharepointAccessManager.Instance.UpdateAccessTokenIfNeeded(base.ExchangePrincipal, OauthUtils.GetOauthCredential(base.LogonIdentity.GetOWAMiniRecipient()), base.LogonIdentity.ClientSecurityContext, out ex, false);
					if (ex != null)
					{
						ExTraceGlobals.UserContextTracer.TraceDebug<Exception>(0L, "CreateMailboxSession for TeamMailbox hit exception while updating AccessToken: {0}", ex);
					}
				}
				CultureInfo cultureInfo = Culture.GetPreferredCultureInfo(base.ExchangePrincipal) ?? Thread.CurrentThread.CurrentCulture;
				MailboxSession mailboxSession;
				if (base.ExchangePrincipal.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox)
				{
					mailboxSession = base.LogonIdentity.CreateDelegateMailboxSession(base.ExchangePrincipal, cultureInfo);
				}
				else
				{
					mailboxSession = base.LogonIdentity.CreateMailboxSession(base.ExchangePrincipal, cultureInfo);
				}
				if (mailboxSession == null)
				{
					throw new OwaInvalidOperationException("CreateMailboxSession cannot create a mailbox session");
				}
				result = mailboxSession;
			}
			catch (AccessDeniedException innerException)
			{
				throw new OwaExplicitLogonException("user has no access rights to the mailbox", "errorexplicitlogonaccessdenied", innerException);
			}
			return result;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00017B60 File Offset: 0x00015D60
		private static OwaFlightConfigData ReadFlightConfigDataFromAD(OrganizationId organizationId)
		{
			if (organizationId == null || organizationId.ConfigurationUnit == null)
			{
				return new OwaFlightConfigData
				{
					RampId = string.Empty,
					IsFirstRelease = false
				};
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 1563, "ReadFlightConfigDataFromAD", "f:\\15.00.1497\\sources\\dev\\clients\\src\\Owa2\\Server\\Core\\common\\UserContext.cs");
			ExchangeConfigurationUnit configurationUnit = tenantOrTopologyConfigurationSession.Read<ExchangeConfigurationUnit>(organizationId.ConfigurationUnit);
			return new OwaFlightConfigData
			{
				RampId = ExchangeConfigurationUnitVariantConfigurationParser.GetRampId(configurationUnit),
				IsFirstRelease = ExchangeConfigurationUnitVariantConfigurationParser.IsFirstRelease(configurationUnit)
			};
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00017BEC File Offset: 0x00015DEC
		private void ClearBposShellData()
		{
			if (!this.IsBposUser)
			{
				return;
			}
			lock (this.syncRoot)
			{
				this.bposNavBarInfoAssetReader = null;
				this.bposShellInfoAssetReader = null;
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00017C40 File Offset: 0x00015E40
		private void RefreshMailboxSession(EcpUserSettings userSettings)
		{
			base.UserContextDiposeGraph.Append(".ur1");
			if ((userSettings & EcpUserSettings.Regional) == EcpUserSettings.Regional)
			{
				ExTimeZone exTimeZone = null;
				try
				{
					if (this.mailboxSessionLock.LockWriterElastic(3000))
					{
						base.UserContextDiposeGraph.Append(".ur2");
						if (base.NotificationManager != null)
						{
							base.UserContextDiposeGraph.Append(".ur3");
							base.NotificationManager.CleanupSubscriptions();
						}
						if (this.mailboxSession != null)
						{
							base.UserContextDiposeGraph.Append(".ur4");
							base.DisposeMailboxSession();
						}
						this.mailboxSession = this.CreateMailboxSession();
						this.isMailboxSessionCreated = true;
						UserContextUtilities.ReconnectStoreSession(this.mailboxSession, this);
						exTimeZone = TimeZoneHelper.GetUserTimeZone(this.mailboxSession);
					}
				}
				finally
				{
					if (this.mailboxSessionLock.IsWriterLockHeld)
					{
						if (this.mailboxSession != null)
						{
							base.UserContextDiposeGraph.Append(".ur5");
							base.UnlockAndDisconnectMailboxSession();
						}
						else
						{
							base.UserContextDiposeGraph.Append(".ur6");
							this.mailboxSessionLock.ReleaseWriterLock();
						}
					}
				}
				if (exTimeZone != null && base.NotificationManager != null)
				{
					base.NotificationManager.RefreshSubscriptions(exTimeZone);
				}
			}
			base.UserContextDiposeGraph.Append(".ur7");
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00017D88 File Offset: 0x00015F88
		private Theme LoadUserTheme()
		{
			ConfigurationContext configurationContext = new ConfigurationContext(this);
			Theme result;
			if (configurationContext.IsFeatureEnabled(Feature.Themes))
			{
				string text = null;
				try
				{
					base.LockAndReconnectMailboxSession(30000);
					UserConfigurationPropertyDefinition propertyDefinition = UserOptionPropertySchema.Instance.GetPropertyDefinition(UserConfigurationPropertyId.ThemeStorageId);
					UserOptionsType userOptionsType = new UserOptionsType();
					userOptionsType.Load(base.MailboxSession, new UserConfigurationPropertyDefinition[]
					{
						propertyDefinition
					});
					text = userOptionsType.ThemeStorageId;
				}
				catch (Exception)
				{
					ExTraceGlobals.ThemesTracer.TraceError(0L, "Failed to find the user's theme from UserOptions");
				}
				finally
				{
					base.UnlockAndDisconnectMailboxSession();
				}
				if (string.IsNullOrEmpty(text))
				{
					result = this.DefaultTheme;
				}
				else
				{
					uint idFromStorageId = ThemeManagerFactory.GetInstance(this.CurrentOwaVersion).GetIdFromStorageId(text);
					if (idFromStorageId == 4294967295U)
					{
						result = this.DefaultTheme;
					}
					else
					{
						result = ThemeManagerFactory.GetInstance(this.CurrentOwaVersion).Themes[(int)((UIntPtr)idFromStorageId)];
					}
				}
			}
			else
			{
				result = this.DefaultTheme;
			}
			return result;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00017E84 File Offset: 0x00016084
		private void ValidateBackendServerIfNeeded()
		{
			if (this.isBackendServerValidated)
			{
				return;
			}
			IMailboxInfo mailboxInfo = base.ExchangePrincipal.MailboxInfo;
			string localServerFqdn = LocalServerCache.LocalServerFqdn;
			string serverFqdn = mailboxInfo.Location.ServerFqdn;
			ExTraceGlobals.CoreTracer.TraceDebug<string, string>((long)this.GetHashCode(), "UserContext.ValidateBackendServerIfNeeded: Target Mailbox location {0}. Current Server Name: {1}.", serverFqdn, localServerFqdn);
			if (!localServerFqdn.Equals(serverFqdn, StringComparison.OrdinalIgnoreCase))
			{
				throw new WrongServerException(ServerStrings.IncorrectServerError(mailboxInfo.PrimarySmtpAddress, serverFqdn), mailboxInfo.GetDatabaseGuid(), serverFqdn, mailboxInfo.Location.ServerVersion, null);
			}
			this.isBackendServerValidated = true;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00017F06 File Offset: 0x00016106
		private void CreateFeatureManagerFactory(OWAMiniRecipient miniRecipient)
		{
			if (this.featuresManagerFactory != null)
			{
				return;
			}
			if (!base.IsExplicitLogon)
			{
				this.CreateFeatureManagerFactoryFromMailbox(miniRecipient);
				return;
			}
			this.CreateFeatureManagerFactoryFromAD(miniRecipient);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00017F40 File Offset: 0x00016140
		private void CreateFeatureManagerFactoryFromMailbox(OWAMiniRecipient miniRecipient)
		{
			UserConfigurationManager.IAggregationContext aggregationContext = null;
			try
			{
				this.LockAndReconnectMailboxSession();
				aggregationContext = base.MailboxSession.UserConfigurationManager.AttachAggregator(AggregatedUserConfigurationSchema.Instance.OwaUserConfiguration);
				base.UnlockAndDisconnectMailboxSession();
				OwaFlightConfigData owaFlightConfigData = UserContext.ReadAggregatedFlightConfigData(aggregationContext, base.ExchangePrincipal.MailboxInfo.OrganizationId);
				RecipientTypeDetails recipientTypeDetails = base.ExchangePrincipal.RecipientTypeDetails;
				this.featuresManagerFactory = new FeaturesManagerFactory(miniRecipient, new ConfigurationContext(this), new ScopeFlightsSettingsProvider(), (VariantConfigurationSnapshot c) => new FeaturesStateOverride(c, recipientTypeDetails), owaFlightConfigData.RampId, owaFlightConfigData.IsFirstRelease);
			}
			finally
			{
				if (aggregationContext != null)
				{
					aggregationContext.Detach();
					this.bootAggregationContext = aggregationContext;
				}
				if (base.MailboxSessionLockedByCurrentThread())
				{
					base.UnlockAndDisconnectMailboxSession();
				}
			}
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00018020 File Offset: 0x00016220
		private void CreateFeatureManagerFactoryFromAD(OWAMiniRecipient miniRecipient)
		{
			OwaFlightConfigData owaFlightConfigData = UserContext.ReadFlightConfigDataFromAD(base.ExchangePrincipal.MailboxInfo.OrganizationId);
			RecipientTypeDetails recipientTypeDetails = base.ExchangePrincipal.RecipientTypeDetails;
			this.featuresManagerFactory = new FeaturesManagerFactory(miniRecipient, new ConfigurationContext(this), new ScopeFlightsSettingsProvider(), (VariantConfigurationSnapshot c) => new FeaturesStateOverride(c, recipientTypeDetails), owaFlightConfigData.RampId, owaFlightConfigData.IsFirstRelease);
		}

		// Token: 0x04000403 RID: 1027
		private const int MaxClientWatsonHistoryCount = 5;

		// Token: 0x04000404 RID: 1028
		private const string ThemeKeySuffix = "Theme";

		// Token: 0x04000405 RID: 1029
		private const int ThemeCacheLifeInMinutes = 1;

		// Token: 0x04000406 RID: 1030
		private readonly object clientWatsonHistoryLock = new object();

		// Token: 0x04000407 RID: 1031
		private readonly Queue<string> clientWatsonHistory = new Queue<string>(5);

		// Token: 0x04000408 RID: 1032
		private long lastUserRequestTime;

		// Token: 0x04000409 RID: 1033
		private bool hasActiveHierarchySubscription;

		// Token: 0x0400040A RID: 1034
		private PlayOnPhoneNotificationManager playonPhoneNotificationManager;

		// Token: 0x0400040B RID: 1035
		private InstantMessageManager instantMessageManager;

		// Token: 0x0400040C RID: 1036
		private BposNavBarInfoAssetReader bposNavBarInfoAssetReader;

		// Token: 0x0400040D RID: 1037
		private BposShellInfoAssetReader bposShellInfoAssetReader;

		// Token: 0x0400040E RID: 1038
		private long signIntoIMTime;

		// Token: 0x0400040F RID: 1039
		private CultureInfo userCulture;

		// Token: 0x04000410 RID: 1040
		private InstantMessagingTypeOptions? instantMessageType;

		// Token: 0x04000411 RID: 1041
		private string sipUri;

		// Token: 0x04000412 RID: 1042
		private Theme defaultTheme;

		// Token: 0x04000413 RID: 1043
		private bool isGlobalAddressListLoaded;

		// Token: 0x04000414 RID: 1044
		private AddressBookBase globalAddressList;

		// Token: 0x04000415 RID: 1045
		private ADObjectId globalAddressListId;

		// Token: 0x04000416 RID: 1046
		private List<IConnectedAccountsNotificationManager> connectedAccountNotificationManagers = new List<IConnectedAccountsNotificationManager>(2);

		// Token: 0x04000417 RID: 1047
		private bool isConnectedAccountsNotificationSetupDone;

		// Token: 0x04000418 RID: 1048
		private ulong allowedCapabilitiesFlags;

		// Token: 0x04000419 RID: 1049
		private readonly string themeKey;

		// Token: 0x0400041A RID: 1050
		private AttachmentDataProviderManager attachmentDataProviderManager;

		// Token: 0x0400041B RID: 1051
		private CancelAttachmentManager cancelAttachmentManager;

		// Token: 0x0400041C RID: 1052
		private long nextActivitySequenceNumber = -1L;

		// Token: 0x0400041D RID: 1053
		private bool? isOptimizedForAccessibility = null;

		// Token: 0x0400041E RID: 1054
		private FeaturesManagerFactory featuresManagerFactory;

		// Token: 0x0400041F RID: 1055
		private volatile InstantSearchManager instantSearchManager;

		// Token: 0x04000420 RID: 1056
		private volatile IInstantSearchNotificationHandler instantSearchNotificationHandler;

		// Token: 0x04000421 RID: 1057
		private bool isBackendServerValidated;

		// Token: 0x04000422 RID: 1058
		private SessionDataCache sessionDataCache;

		// Token: 0x04000423 RID: 1059
		private bool? isPublicLogon;

		// Token: 0x04000424 RID: 1060
		private string currentOwaVersion;

		// Token: 0x04000425 RID: 1061
		private UserConfigurationManager.IAggregationContext bootAggregationContext;

		// Token: 0x04000426 RID: 1062
		private bool? isWacEditingEnabled;
	}
}
