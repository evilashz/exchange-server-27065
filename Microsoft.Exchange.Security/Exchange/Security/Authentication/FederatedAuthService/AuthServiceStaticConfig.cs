using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Win32;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x02000062 RID: 98
	internal class AuthServiceStaticConfig
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000325 RID: 805 RVA: 0x000191A0 File Offset: 0x000173A0
		// (set) Token: 0x06000326 RID: 806 RVA: 0x000191CF File Offset: 0x000173CF
		public static AuthServiceStaticConfig Config
		{
			get
			{
				AuthServiceStaticConfig authServiceStaticConfig = AuthServiceStaticConfig.privateConfig;
				if (authServiceStaticConfig == null)
				{
					authServiceStaticConfig = new AuthServiceStaticConfig();
					authServiceStaticConfig.ReadConfig();
					Interlocked.Exchange<AuthServiceStaticConfig>(ref AuthServiceStaticConfig.privateConfig, authServiceStaticConfig);
				}
				return authServiceStaticConfig;
			}
			set
			{
				Interlocked.Exchange<AuthServiceStaticConfig>(ref AuthServiceStaticConfig.privateConfig, value);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000327 RID: 807 RVA: 0x000191E0 File Offset: 0x000173E0
		public MsoEndpointType? MsoSSLEndpointType
		{
			get
			{
				if (this.msoEndpointType == null)
				{
					try
					{
						this.msoEndpointType = new MsoEndpointType?(ConfigBase<AdDriverConfigSchema>.GetConfig<MsoEndpointType>("MsoEndpointType"));
					}
					catch (Exception ex)
					{
						this.msoEndpointType = new MsoEndpointType?(MsoEndpointType.OLD);
						AuthServiceStaticConfig.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_ReadMsoEndpointTypeFailed, "ReadMsoEndpointTypeFailed", new object[]
						{
							ex.Message
						});
					}
				}
				return this.msoEndpointType;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0001925C File Offset: 0x0001745C
		public string MsoRst2LoginUrl
		{
			get
			{
				MsoEndpointType valueOrDefault = this.MsoSSLEndpointType.GetValueOrDefault();
				MsoEndpointType? msoEndpointType;
				if (msoEndpointType != null)
				{
					switch (valueOrDefault)
					{
					case MsoEndpointType.NEW_KEEP_ALIVE:
						return this.msoRst2LoginKeepAlive;
					case MsoEndpointType.NEW_EXO_KEEP_ALIVE:
						return this.msoRst2LoginEXOKeepAlive;
					}
				}
				return this.msoRst2Login;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000329 RID: 809 RVA: 0x000192A8 File Offset: 0x000174A8
		public string MsoHttpPostLogin
		{
			get
			{
				MsoEndpointType valueOrDefault = this.MsoSSLEndpointType.GetValueOrDefault();
				MsoEndpointType? msoEndpointType;
				if (msoEndpointType != null)
				{
					switch (valueOrDefault)
					{
					case MsoEndpointType.NEW_KEEP_ALIVE:
						return this.msoHttpPostLoginKeepAlive;
					case MsoEndpointType.NEW_EXO_KEEP_ALIVE:
						return this.msoHttpPostLoginEXOKeepAlive;
					}
				}
				return this.msoHttpPostLogin;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600032A RID: 810 RVA: 0x000192F4 File Offset: 0x000174F4
		public string MsoRealmDiscoveryUri
		{
			get
			{
				MsoEndpointType valueOrDefault = this.MsoSSLEndpointType.GetValueOrDefault();
				MsoEndpointType? msoEndpointType;
				if (msoEndpointType != null)
				{
					switch (valueOrDefault)
					{
					case MsoEndpointType.NEW_KEEP_ALIVE:
						return this.msoRealmDiscoveryUriKeepAlive;
					case MsoEndpointType.NEW_EXO_KEEP_ALIVE:
						return this.msoRealmDiscoveryUriEXOKeepAlive;
					}
				}
				return this.msoRealmDiscoveryUri;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00019340 File Offset: 0x00017540
		private AuthServiceStaticConfig()
		{
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000196B4 File Offset: 0x000178B4
		private void ReadOfflineProvisioningFlagsInADAndConfigFile(ITopologyConfigurationSession session)
		{
			OfflineAuthenticationProvisioningFlags offlineAuthenticationProvisioningFlags = OfflineAuthenticationProvisioningFlags.Disabled;
			this.OfflineOrgIdProvisioningFlags = AuthServiceStaticConfig.ReadAppConfig("OfflineOrgIdProvisioningFlags", null);
			if (this.OfflineOrgIdProvisioningFlags != null)
			{
				offlineAuthenticationProvisioningFlags = (OfflineAuthenticationProvisioningFlags)this.OfflineOrgIdProvisioningFlags.Value;
			}
			else if (ExEnvironment.IsTest)
			{
				offlineAuthenticationProvisioningFlags = OfflineAuthenticationProvisioningFlags.Enabled;
			}
			else
			{
				try
				{
					Organization orgContainer = session.GetOrgContainer();
					offlineAuthenticationProvisioningFlags = orgContainer.OfflineAuthFlags;
					AuthServiceStaticConfig.counters.NumberOfAdRequestForOfflineOrgId.Increment();
				}
				catch (Exception ex)
				{
					AuthServiceStaticConfig.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_ReadOfflineAuthProvisioningFlagsFailed, "ReadOfflineProvisioningFlagsInADAndConfigFile", new object[]
					{
						ex.Message
					});
				}
			}
			AuthServiceStaticConfig.IsOfflineOrgIdEnabled = (offlineAuthenticationProvisioningFlags != OfflineAuthenticationProvisioningFlags.Disabled);
			AuthServiceStaticConfig.IsOrgIdOutage = (offlineAuthenticationProvisioningFlags == OfflineAuthenticationProvisioningFlags.SkipRealOrgId);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00019778 File Offset: 0x00017978
		private void ReadConfig()
		{
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.GetHashCode(), "Entering AuthServiceStaticConfig.ReadConfig");
			ThreadPool.GetMinThreads(out this.workerMin, out this.ioPortMin);
			this.ioPortMin = AuthServiceStaticConfig.ReadAppConfig("MinimumIOPortThreads", this.ioPortMin);
			this.workerMin = AuthServiceStaticConfig.ReadAppConfig("MinimumWorkerThreads", this.workerMin);
			ThreadPool.SetMinThreads(this.workerMin, this.ioPortMin);
			this.configLifetimeSeconds = AuthServiceStaticConfig.ReadAppConfig("ConfigLifetimeSeconds", this.configLifetimeSeconds, 1);
			this.siteName = AuthServiceStaticConfig.ReadAppConfig("LiveSiteName", null);
			if (string.IsNullOrEmpty(this.siteName))
			{
				this.siteName = "outlook.com";
			}
			ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "LiveSiteName = {0}", this.siteName);
			this.msoSiteName = AuthServiceStaticConfig.ReadAppConfig("MsoSiteName", null);
			if (string.IsNullOrEmpty(this.msoSiteName))
			{
				this.msoSiteName = "urn:federation:MicrosoftOnline";
			}
			ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "MsoSiteName = {0}", this.msoSiteName);
			this.bypassTOUCheck = AuthServiceStaticConfig.ReadAppConfig("BypassTOU", this.bypassTOUCheck);
			this.bypassAcceptedDomainUPNCheck = AuthServiceStaticConfig.ReadAppConfig("BypassAcceptedDomainUPNCheck", this.bypassAcceptedDomainUPNCheck);
			string value = AuthServiceStaticConfig.ReadAppConfig("DefaultInstance", "");
			try
			{
				this.defaultInstance = (LiveIdInstanceType)Enum.Parse(typeof(LiveIdInstanceType), value, true);
			}
			catch (ArgumentException)
			{
				this.defaultInstance = LiveIdInstanceType.Business;
			}
			ExTraceGlobals.AuthenticationTracer.Information<LiveIdInstanceType>((long)this.GetHashCode(), "DefaultInstance = {0}", this.defaultInstance);
			this.liveHrdDelay = AuthServiceStaticConfig.ReadAppConfig("LiveHrdDelayMilliseconds", this.liveHrdDelay);
			this.msoHrdDelay = AuthServiceStaticConfig.ReadAppConfig("MsoHrdDelayMilliseconds", this.msoHrdDelay);
			this.liveRst2Delay = AuthServiceStaticConfig.ReadAppConfig("LiveRst2DelayMilliseconds", this.liveRst2Delay);
			this.msoRst2Delay = AuthServiceStaticConfig.ReadAppConfig("MsoRst2DelayMilliseconds", this.msoRst2Delay);
			this.hrdCacheBucketSize = AuthServiceStaticConfig.ReadAppConfig("HrdCacheBucketSize", this.hrdCacheBucketSize, 1);
			this.hrdCacheBuckets = AuthServiceStaticConfig.ReadAppConfig("HrdCacheBuckets", this.hrdCacheBuckets, 1);
			this.hrdCacheLifetime = AuthServiceStaticConfig.ReadAppConfig("HrdCacheLifetimeMinutes", this.hrdCacheLifetime, 1);
			this.logonCacheSize = AuthServiceStaticConfig.ReadAppConfig("LogonCacheSize", this.logonCacheSize, 1);
			this.logonCacheLifetime = AuthServiceStaticConfig.ReadAppConfig("LogonCacheLifetimeMinutes", this.logonCacheLifetime, 1);
			this.badCredsLifetime = AuthServiceStaticConfig.ReadAppConfig("BadCredsLifetimeSeconds", this.badCredsLifetime, 1);
			this.badCredsRecoverableLifetime = AuthServiceStaticConfig.ReadAppConfig("BadCredsRecoverableLifetimeSeconds", this.badCredsRecoverableLifetime, 1);
			this.level1BadCredCacheSize = AuthServiceStaticConfig.ReadAppConfig("Level1BadCredCacheSize", this.level1BadCredCacheSize, 1);
			this.level2BadCredCacheSize = AuthServiceStaticConfig.ReadAppConfig("Level2BadCredCacheSize", this.level2BadCredCacheSize, 1);
			this.level1BadCredLifetime = AuthServiceStaticConfig.ReadAppConfig("Level1BadCredLifetimeMinutes", this.level1BadCredLifetime, 1);
			this.level2BadCredLifetime = AuthServiceStaticConfig.ReadAppConfig("Level2BadCredLifetimeMinutes", this.level2BadCredLifetime, 1);
			this.level2BadCredListSize = AuthServiceStaticConfig.ReadAppConfig("Level2BadCredListSize", this.level2BadCredListSize, 1);
			this.ADUserCacheBucketSize = AuthServiceStaticConfig.ReadAppConfig("ADUserCacheBucketSize", this.ADUserCacheBucketSize);
			this.ADUserCacheBuckets = AuthServiceStaticConfig.ReadAppConfig("ADUserCacheBuckets", this.ADUserCacheBuckets);
			this.ADUserEntryExpireTimeInMinutes = AuthServiceStaticConfig.ReadAppConfig("ADUserEntryExpireTimeInMinutes", this.ADUserEntryExpireTimeInMinutes);
			this.TOUByPassCacheExpireTimeInMinutes = AuthServiceStaticConfig.ReadAppConfig("TOUByPassCacheExpireTimeInMinutes", this.TOUByPassCacheExpireTimeInMinutes);
			this.TOUCacheBuckets = AuthServiceStaticConfig.ReadAppConfig("TOUCacheBuckets", this.TOUCacheBuckets);
			this.TOUCacheBucketSize = AuthServiceStaticConfig.ReadAppConfig("TOUCacheBucketSize", this.TOUCacheBucketSize);
			this.RpsTicketLifetime = AuthServiceStaticConfig.ReadAppConfig("RpsTicketLifetimeSeconds", this.RpsTicketLifetime);
			this.defaultTimeout = AuthServiceStaticConfig.ReadAppConfig("AuthenticationTimeoutSeconds", this.defaultTimeout, 1);
			this.maxShibbolethResponseSize = AuthServiceStaticConfig.ReadAppConfig("MaxShibbolethResponseSize", this.maxShibbolethResponseSize, 1);
			this.maxAdfsResponseSize = AuthServiceStaticConfig.ReadAppConfig("MaxAdfsResponseSize", this.maxAdfsResponseSize, 1);
			this.verifySamlSignatures = AuthServiceStaticConfig.ReadAppConfig("VerifySamlSignatures", this.verifySamlSignatures);
			this.blacklistTime = AuthServiceStaticConfig.ReadAppConfig("BlacklistTimeMinutes", this.blacklistTime, 0);
			this.blacklistThreshold = AuthServiceStaticConfig.ReadAppConfig("BlacklistThreshold", this.blacklistThreshold, 1);
			this.tarpitTime = AuthServiceStaticConfig.ReadAppConfig("TarpitTimeMinutes", this.tarpitTime, 0);
			this.passwordExpiryUpperBound = AuthServiceStaticConfig.ReadAppConfig("PasswordExpiryUpperBoundDays", this.passwordExpiryUpperBound, 1);
			this.statisticCacheSize = AuthServiceStaticConfig.ReadAppConfig("StatisticCacheSize", this.statisticCacheSize, 1);
			this.statisticLifetime = AuthServiceStaticConfig.ReadAppConfig("StatisticLifetimeMinutes", this.statisticLifetime, 1);
			this.timedOutResponseLower = AuthServiceStaticConfig.ReadAppConfig("TimedOutResponseLower", this.timedOutResponseLower, 0);
			this.timedOutResponseUpper = AuthServiceStaticConfig.ReadAppConfig("TimedOutResponseUpper", this.timedOutResponseUpper, 0);
			this.badUserPasswordLower = AuthServiceStaticConfig.ReadAppConfig("BadUserPasswordLower", this.badUserPasswordLower, 0);
			this.badUserPasswordUpper = AuthServiceStaticConfig.ReadAppConfig("BadUserPasswordUpper", this.badUserPasswordUpper, 0);
			this.tokenSizeExceededLower = AuthServiceStaticConfig.ReadAppConfig("TokenSizeExceededLower", this.tokenSizeExceededLower, 0);
			this.tokenSizeExceededUpper = AuthServiceStaticConfig.ReadAppConfig("TokenSizeExceededUpper", this.tokenSizeExceededUpper, 0);
			this.failedResponseLower = AuthServiceStaticConfig.ReadAppConfig("FailedResponseLower", this.failedResponseLower, 0);
			this.failedResponseUpper = AuthServiceStaticConfig.ReadAppConfig("FailedResponseUpper", this.failedResponseUpper, 0);
			this.enableFailedResponseBlacklist = AuthServiceStaticConfig.ReadAppConfig("EnableFailedResponseBlacklist", this.enableFailedResponseBlacklist);
			this.enableBadUserPasswordBlacklist = AuthServiceStaticConfig.ReadAppConfig("EnableBadUserPasswordBlacklist", this.enableBadUserPasswordBlacklist);
			this.enableTimedOutBlacklist = AuthServiceStaticConfig.ReadAppConfig("EnableTimedOutBlacklist", this.enableTimedOutBlacklist);
			this.enableTokenSizeExceededBlacklist = AuthServiceStaticConfig.ReadAppConfig("EnableTokenSizeExceededBlacklist", this.enableTokenSizeExceededBlacklist);
			this.adfsSkewThreshold = AuthServiceStaticConfig.ReadAppConfig("AdfsSkewThresholdMinutes", this.adfsSkewThreshold);
			this.shibbSkewThreshold = AuthServiceStaticConfig.ReadAppConfig("ShibbolethSkewThresholdMinutes", this.shibbSkewThreshold);
			this.percentileCountersUnitOfExpiryInSeconds = AuthServiceStaticConfig.ReadAppConfig("PercentileCountersUnitOfExpiryInSeconds", this.percentileCountersUnitOfExpiryInSeconds);
			this.percentileCountersLastMinutes = AuthServiceStaticConfig.ReadAppConfig("PercentileCountersLastMinutes", this.percentileCountersLastMinutes);
			this.percentileCountersUpdateIntervalInSeconds = AuthServiceStaticConfig.ReadAppConfig("PercentileCountersUpdateIntervalInSeconds", this.percentileCountersUpdateIntervalInSeconds);
			this.numberOfTotalRequestsToIgnoreInPercentileCounter = AuthServiceStaticConfig.ReadAppConfig("NumberOfTotalRequestsToIgnoreInPercentileCounter", this.numberOfTotalRequestsToIgnoreInPercentileCounter);
			this.PasswordConfidenceInDays = AuthServiceStaticConfig.ReadAppConfig("PasswordConfidenceInDays", this.PasswordConfidenceInDays);
			this.PasswordConfidenceInDaysForADFSDown = AuthServiceStaticConfig.ReadAppConfig("PasswordConfidenceInDaysForADFSDown", this.PasswordConfidenceInDaysForADFSDown);
			this.MaxCacheSizeOfLastLogonTime = AuthServiceStaticConfig.ReadAppConfig("MaxCacheSizeOfLastLogonTime", this.MaxCacheSizeOfLastLogonTime);
			this.ADLookupCacheLifetimeOffline = AuthServiceStaticConfig.ReadAppConfig("ADLookupCacheLifetimeOffline", this.ADLookupCacheLifetimeOffline);
			this.LiveIdStsTimeoutInSeconds = AuthServiceStaticConfig.ReadAppConfig("LiveIdStsTimeoutInSeconds", this.LiveIdStsTimeoutInSeconds);
			this.ConnectionLeaseTimeoutInSeconds = AuthServiceStaticConfig.ReadAppConfig("ConnectionLeaseTimeoutInSeconds", this.ConnectionLeaseTimeoutInSeconds);
			this.MaxServicePointIdleTimeInSeconds = AuthServiceStaticConfig.ReadAppConfig("MaxServicePointIdleTimeInSeconds", this.MaxServicePointIdleTimeInSeconds);
			this.MaxConnectionGroups = AuthServiceStaticConfig.ReadAppConfig("MaxConnectionGroups", this.MaxConnectionGroups);
			this.OfflineHrdMaxParentDomainRetry = AuthServiceStaticConfig.ReadAppConfig("OfflineHrdMaxParentDomainRetry", this.OfflineHrdMaxParentDomainRetry);
			this.OfflineOrgIdWaitTimeInSeconds = AuthServiceStaticConfig.ReadAppConfig("OfflineOrgIdWaitTimeInSeconds", this.OfflineOrgIdWaitTimeInSeconds);
			this.AllowOfflineOrgIdForADFS = AuthServiceStaticConfig.ReadAppConfig("AllowOfflineOrgIdForADFS", this.AllowOfflineOrgIdForADFS);
			this.OfflineOrgIdPrimaryUsageThreholdPercentage = AuthServiceStaticConfig.ReadAppConfig("OfflineOrgIdPrimaryUsageThreholdPercentage", this.OfflineOrgIdPrimaryUsageThreholdPercentage);
			this.ThrottleOfflineOrgIdAsPrimaryUsage = AuthServiceStaticConfig.ReadAppConfig("ThrottleOfflineOrgIdAsPrimaryUsage", this.ThrottleOfflineOrgIdAsPrimaryUsage);
			this.AppPasswordHelpUrl = AuthServiceStaticConfig.ReadAppConfig("AppPasswordHelpUrl", this.AppPasswordHelpUrl);
			this.EnablePasswordInvalidationForFederatedUser = AuthServiceStaticConfig.ReadAppConfig("EnablePasswordInvalidationForFederatedUser", this.EnablePasswordInvalidationForFederatedUser);
			this.EnablePasswordInvalidationForManagedUser = AuthServiceStaticConfig.ReadAppConfig("EnablePasswordInvalidationForManagedUser", this.EnablePasswordInvalidationForManagedUser);
			this.InternalIPFilterRegexPattern = AuthServiceStaticConfig.ReadAppConfig("InternalIPFilterRegexPattern", "^((10\\.\\d+\\.\\d+\\.\\d+)|(65\\.5[2-5]\\.\\d+\\.\\d+)|(157\\.5[6-9]\\.\\d+\\.\\d+)|(157\\.5[4-5]\\.\\d+\\.\\d+)|(132\\.245\\.\\d+\\.\\d+)|(141\\.251\\.\\d+\\.\\d+)|(157\\.60\\.\\d+\\.\\d+)|(207\\.46\\.\\d+\\.\\d+)|(70\\.37\\.(([0-9]|[1-9][0-9])|1((0[0-9]|1[0-9])|2[0-7]))\\.\\d+)|(70\\.37\\.1(2[8-9]|[3-8][0-9]|9[0-1])\\.\\d+)|(94\\.245\\.((6[4-9]|[7-9][0-9])|1((0[0-9]|1[0-9])|2[0-7]))\\.\\d+)|(111\\.221\\.((6[4-9]|[7-9][0-9])|1((0[0-9]|1[0-9])|2[0-7]))\\.\\d+)|(206\\.191\\.2(2[4-9]|[3-4][0-9]|5[0-5])\\.\\d+)|(213\\.199\\.1([6-8][0-9]|9[0-1])\\.\\d+)|(111\\.221\\.(1[6-9]|2[0-9]|3[0-1])\\.\\d+)|(23\\.(9[6-9]|10[0-3])\\.\\d+\\.\\d+)|(191\\.23[2-5]\\.\\d+\\.\\d+)|(134\\.170\\.\\d+\\.\\d+)|(141\\.252\\.\\d+\\.\\d+)|(25\\.160\\.\\d+\\.\\d+))$");
			this.AdditionalOutlookComRegexPattern = AuthServiceStaticConfig.ReadAppConfig("AdditionalOutlookComRegexPattern", this.AdditionalOutlookComRegexPattern);
			if (!string.IsNullOrEmpty(this.AdditionalOutlookComRegexPattern))
			{
				this.outlookComRegex = new Regex(this.AdditionalOutlookComRegexPattern, RegexOptions.IgnoreCase);
			}
			this.ReqStatusForResponseDump = AuthServiceStaticConfig.ReadAppConfig("ReqStatusForResponseDump", this.ReqStatusForResponseDump);
			this.AuthStatusForResponseDump = AuthServiceStaticConfig.ReadAppConfig("AuthStatusForResponseDump", this.AuthStatusForResponseDump);
			if (ExEnvironment.IsTestDomain || ExEnvironment.IsSdfDomain)
			{
				this.EnableXmlAuthForLiveId = true;
			}
			if (ExEnvironment.IsTestDomain)
			{
				this.EnableConsumerRPSSync = true;
			}
			this.EnableXmlAuthForLiveId = AuthServiceStaticConfig.ReadAppConfig("EnableXmlAuthForLiveId", this.EnableXmlAuthForLiveId);
			this.EnableConsumerRPSSync = AuthServiceStaticConfig.ReadAppConfig("EnableConsumerRPSSync", this.EnableConsumerRPSSync);
			this.UpdateExistingMbxEntryOnly = AuthServiceStaticConfig.ReadAppConfig("UpdateExistingMbxEntryOnly", this.UpdateExistingMbxEntryOnly);
			this.MSAProfilePolicy = AuthServiceStaticConfig.ReadAppConfig("MSAProfilePolicy", this.MSAProfilePolicy);
			this.EnableRemoteRPSForCompactTicket = AuthServiceStaticConfig.ReadAppConfig("EnableRemoteRPSForCompactTicket", this.EnableRemoteRPSForCompactTicket);
			this.EnableMailboxBasedPasswordConfidence = AuthServiceStaticConfig.ReadAppConfig("EnableMailboxBasedPasswordConfidence", this.EnableMailboxBasedPasswordConfidence);
			if (string.IsNullOrEmpty(this.InternalIPFilterRegexPattern))
			{
				this.InternalIPFilterRegexPattern = "^((10\\.\\d+\\.\\d+\\.\\d+)|(65\\.5[2-5]\\.\\d+\\.\\d+)|(157\\.5[6-9]\\.\\d+\\.\\d+)|(157\\.5[4-5]\\.\\d+\\.\\d+)|(132\\.245\\.\\d+\\.\\d+)|(141\\.251\\.\\d+\\.\\d+)|(157\\.60\\.\\d+\\.\\d+)|(207\\.46\\.\\d+\\.\\d+)|(70\\.37\\.(([0-9]|[1-9][0-9])|1((0[0-9]|1[0-9])|2[0-7]))\\.\\d+)|(70\\.37\\.1(2[8-9]|[3-8][0-9]|9[0-1])\\.\\d+)|(94\\.245\\.((6[4-9]|[7-9][0-9])|1((0[0-9]|1[0-9])|2[0-7]))\\.\\d+)|(111\\.221\\.((6[4-9]|[7-9][0-9])|1((0[0-9]|1[0-9])|2[0-7]))\\.\\d+)|(206\\.191\\.2(2[4-9]|[3-4][0-9]|5[0-5])\\.\\d+)|(213\\.199\\.1([6-8][0-9]|9[0-1])\\.\\d+)|(111\\.221\\.(1[6-9]|2[0-9]|3[0-1])\\.\\d+)|(23\\.(9[6-9]|10[0-3])\\.\\d+\\.\\d+)|(191\\.23[2-5]\\.\\d+\\.\\d+)|(134\\.170\\.\\d+\\.\\d+)|(141\\.252\\.\\d+\\.\\d+)|(25\\.160\\.\\d+\\.\\d+))$";
			}
			this.internalIPFilterRegex = new Regex(this.InternalIPFilterRegexPattern);
			this.LastLogonTimeUpdateFrequency = AuthServiceStaticConfig.ReadAppConfig("LastLogonTimeUpdateFrequency", this.LastLogonTimeUpdateFrequency);
			string namespaces = AuthServiceStaticConfig.ReadAppConfig("TrustedNamespaces", string.Empty);
			this.trustedNamespaces = AuthServiceStaticConfig.ParseTrustedNamespaces(namespaces);
			this.defaultTenantNegoConfig = AuthServiceStaticConfig.ReadAppConfig("DefaultTenantNegoConfig", this.defaultTenantNegoConfig);
			string text = null;
			try
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 372, "ReadConfig", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\FederatedAuthService\\AuthServiceStaticConfig.cs");
				ServiceEndpointContainer endpointContainer = topologyConfigurationSession.GetEndpointContainer();
				text = ServiceEndpointId.LiveServiceLogin2;
				ServiceEndpoint endpoint = endpointContainer.GetEndpoint(text);
				this.liveRst2Login = endpoint.Uri.ToString();
				ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "LiveRst2Logon = {0}", this.liveRst2Login);
				text = ServiceEndpointId.LiveGetUserRealm;
				ServiceEndpoint endpoint2 = endpointContainer.GetEndpoint(text);
				this.liveRealmDiscoveryUri = endpoint2.Uri.ToString();
				ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "LiveRealmDiscoveryUri = {0}", this.liveRealmDiscoveryUri);
				text = ServiceEndpointId.LiveSAMLHttpPostLogin;
				ServiceEndpoint endpoint3 = endpointContainer.GetEndpoint(text);
				this.liveHttpPostLogin = endpoint3.Uri.ToString();
				ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "LiveHttpPostLogin = {0}", this.liveHttpPostLogin);
				text = ServiceEndpointId.LiveSAMLHttpPostAssertionConsumerService;
				ADServiceConnectionPoint adserviceConnectionPoint = topologyConfigurationSession.Read<ADServiceConnectionPoint>(endpointContainer.Id.GetChildId(text));
				this.liveHttpPostAssertionConsumerService = adserviceConnectionPoint.ServiceBindingInformation[0];
				ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "LiveHttpPostAssertionConsumerService = {0}", this.liveHttpPostAssertionConsumerService);
				text = ServiceEndpointId.MsoServiceLogin2;
				ServiceEndpoint endpoint4 = endpointContainer.GetEndpoint(text);
				this.msoRst2Login = endpoint4.Uri.ToString();
				ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "MsoRst2Login = {0}", this.msoRst2Login);
				text = ServiceEndpointId.MsoServiceLogin2KeepAlive;
				ServiceEndpoint endpoint5 = endpointContainer.GetEndpoint(text);
				this.msoRst2LoginKeepAlive = endpoint5.Uri.ToString();
				ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "MsoRst2LoginKeepAlive = {0}", this.msoRst2LoginKeepAlive);
				text = ServiceEndpointId.MsoServiceLogin2EXOKeepAlive;
				ServiceEndpoint endpoint6 = endpointContainer.GetEndpoint(text);
				this.msoRst2LoginEXOKeepAlive = endpoint6.Uri.ToString();
				ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "MsoRst2LoginEXOKeepAlive = {0}", this.msoRst2LoginEXOKeepAlive);
				text = ServiceEndpointId.MsoGetUserRealm;
				ServiceEndpoint endpoint7 = endpointContainer.GetEndpoint(text);
				this.msoRealmDiscoveryUri = endpoint7.Uri.ToString();
				ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "MsoRealmDiscoveryUri = {0}", this.msoRealmDiscoveryUri);
				text = ServiceEndpointId.MsoGetUserRealmKeepAlive;
				ServiceEndpoint endpoint8 = endpointContainer.GetEndpoint(text);
				this.msoRealmDiscoveryUriKeepAlive = endpoint8.Uri.ToString();
				ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "MsoRealmDiscoveryUriKeepAlive = {0}", this.msoRealmDiscoveryUriKeepAlive);
				text = ServiceEndpointId.MsoGetUserRealmEXOKeepAlive;
				ServiceEndpoint endpoint9 = endpointContainer.GetEndpoint(text);
				this.msoRealmDiscoveryUriEXOKeepAlive = endpoint9.Uri.ToString();
				ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "MsoRealmDiscoveryUriEXOKeepAlive = {0}", this.msoRealmDiscoveryUriEXOKeepAlive);
				text = ServiceEndpointId.MsoSAMLHttpPostLogin;
				ServiceEndpoint endpoint10 = endpointContainer.GetEndpoint(text);
				this.msoHttpPostLogin = endpoint10.Uri.ToString();
				ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "MsoHttpPostLogin = {0}", this.msoHttpPostLogin);
				text = ServiceEndpointId.MsoSAMLHttpPostLoginKeepAlive;
				ServiceEndpoint endpoint11 = endpointContainer.GetEndpoint(text);
				this.msoHttpPostLoginKeepAlive = endpoint11.Uri.ToString();
				ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "msoHttpPostLoginKeepAlive = {0}", this.msoHttpPostLoginKeepAlive);
				text = ServiceEndpointId.MsoSAMLHttpPostLoginEXOKeepAlive;
				ServiceEndpoint endpoint12 = endpointContainer.GetEndpoint(text);
				this.msoHttpPostLoginEXOKeepAlive = endpoint12.Uri.ToString();
				ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "msoHttpPostLoginEXOKeepAlive = {0}", this.msoHttpPostLoginEXOKeepAlive);
				text = ServiceEndpointId.LiveIdXmlAuth;
				ServiceEndpoint endpoint13 = endpointContainer.GetEndpoint(text);
				this.liveidXmlAuth = endpoint13.Uri.ToString();
				ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "liveidXmlAuth = {0}", this.liveidXmlAuth);
				text = ServiceEndpointId.MsoSAMLHttpPostAssertionConsumerService;
				ADServiceConnectionPoint adserviceConnectionPoint2 = topologyConfigurationSession.Read<ADServiceConnectionPoint>(endpointContainer.Id.GetChildId(text));
				this.msoHttpPostAssertionConsumerService = adserviceConnectionPoint2.ServiceBindingInformation[0];
				ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "MsoHttpPostAssertionConsumerService = {0}", this.msoHttpPostAssertionConsumerService);
				this.ReadOfflineProvisioningFlagsInADAndConfigFile(topologyConfigurationSession);
			}
			catch (ServiceEndpointNotFoundException arg)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<string, ServiceEndpointNotFoundException>((long)this.GetHashCode(), "Service endpoint {0} not found {1}", text, arg);
			}
			catch (EndpointContainerNotFoundException arg2)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<EndpointContainerNotFoundException>((long)this.GetHashCode(), "EndpointContainerNotFound {0}", arg2);
			}
			catch (ADTransientException arg3)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<ADTransientException>((long)this.GetHashCode(), "Exception reading endpoints {0}", arg3);
			}
			catch (ADOperationException arg4)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<ADOperationException>((long)this.GetHashCode(), "Exception reading endpoints {0}", arg4);
			}
			ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "Live RST2 login endpoint  = {0}", this.liveRst2Login);
			ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "Live HRD endpoint  = {0}", this.liveRealmDiscoveryUri);
			ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "Live HTTP-POST endpoint  = {0}", this.liveHttpPostLogin);
			try
			{
				this.MSASiteId = AuthServiceStaticConfig.ReadSiteIdFromRegistryKey(this.MSASiteId);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<Exception>((long)this.GetHashCode(), "Reading SiteId from registry key failed. {0}", ex);
				AuthServiceStaticConfig.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_ConfigReadError, AuthServiceStaticConfig.SiteIdName, new object[]
				{
					AuthServiceStaticConfig.SiteIdName,
					this.MSASiteId,
					ex
				});
			}
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.GetHashCode(), "Leaving AuthServiceStaticConfig.ReadConfig");
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0001A640 File Offset: 0x00018840
		private static string ReadAppConfig(string setting, string defaultValue)
		{
			string text = defaultValue;
			try
			{
				string text2 = ConfigurationManager.AppSettings[setting];
				if (text2 != null)
				{
					text = text2;
					ExTraceGlobals.AuthenticationTracer.TraceDebug<string, string>(0L, "Read setting '{0}' value '{1}' from app.Config file", setting, text);
				}
			}
			catch (ConfigurationException ex)
			{
				AuthServiceStaticConfig.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_ConfigReadError, setting, new object[]
				{
					setting,
					defaultValue,
					ex
				});
				ExTraceGlobals.AuthenticationTracer.TraceWarning<string, string, string>(0L, "Exception caught reading {0} value from app.config file, using default value {1}. {2}", setting, text, ex.ToString());
			}
			ExTraceGlobals.AuthenticationTracer.Information<string, string>(0L, "{0} = {1}", setting, text);
			return text;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0001A6DC File Offset: 0x000188DC
		private static int ReadAppConfig(string setting, int defaultValue)
		{
			string text = AuthServiceStaticConfig.ReadAppConfig(setting, null);
			int num;
			if (int.TryParse(text, out num))
			{
				ExTraceGlobals.AuthenticationTracer.TraceDebug<string, int>(0L, "Parsed integer setting '{0}' value '{1}' from app.Config file", setting, num);
			}
			else
			{
				num = defaultValue;
				ExTraceGlobals.AuthenticationTracer.TraceWarning<string, string, int>(0L, "Unable to parse integer setting '{0}' value '{1}' from app.Config file, using default value {2}", setting, text, num);
			}
			ExTraceGlobals.AuthenticationTracer.Information<string, int>(0L, "{0} = {1}", setting, num);
			return num;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0001A73C File Offset: 0x0001893C
		private static int ReadAppConfig(string setting, int defaultValue, int lowerBound)
		{
			int num = AuthServiceStaticConfig.ReadAppConfig(setting, defaultValue);
			if (num < lowerBound)
			{
				ExTraceGlobals.AuthenticationTracer.TraceDebug<string, int, int>(0L, "clamping integer setting '{0}' value '{1}' to '{2}'", setting, num, lowerBound);
				num = lowerBound;
			}
			ExTraceGlobals.AuthenticationTracer.Information<string, int>(0L, "{0} = {1}", setting, num);
			return num;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001A780 File Offset: 0x00018980
		private static bool ReadAppConfig(string setting, bool defaultValue)
		{
			string text = AuthServiceStaticConfig.ReadAppConfig(setting, null);
			bool flag;
			if (bool.TryParse(text, out flag))
			{
				ExTraceGlobals.AuthenticationTracer.TraceDebug<string, bool>(0L, "Parsed bool setting '{0}' value '{1}' from app.Config file", setting, flag);
			}
			else
			{
				flag = defaultValue;
				ExTraceGlobals.AuthenticationTracer.TraceWarning<string, string, bool>(0L, "Unable to parse bool setting '{0}' value '{1}' from app.Config file, using default value {2}", setting, text, flag);
			}
			ExTraceGlobals.AuthenticationTracer.Information<string, bool>(0L, "{0} = {1}", setting, flag);
			return flag;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0001A7E0 File Offset: 0x000189E0
		private static int? ReadAppConfig(string setting, int? defaultValue)
		{
			string text = AuthServiceStaticConfig.ReadAppConfig(setting, null);
			int num;
			int? result;
			if (int.TryParse(text, out num))
			{
				result = new int?(num);
				ExTraceGlobals.AuthenticationTracer.TraceDebug<string, int>(0L, "Parsed int setting '{0}' value '{1}' from app.Config file", setting, num);
			}
			else
			{
				result = defaultValue;
				ExTraceGlobals.AuthenticationTracer.TraceWarning<string, string, int>(0L, "Unable to parse int setting '{0}' value '{1}' from app.Config file, using default value {2}", setting, text, num);
			}
			ExTraceGlobals.AuthenticationTracer.Information<string, int>(0L, "{0} = {1}", setting, num);
			return result;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0001A848 File Offset: 0x00018A48
		private static int ReadSiteIdFromRegistryKey(int defaultSiteId)
		{
			int result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(AuthServiceStaticConfig.ExchangeLiveServicesKey))
			{
				if (registryKey == null)
				{
					result = defaultSiteId;
				}
				else
				{
					object value = registryKey.GetValue(AuthServiceStaticConfig.SiteIdName, defaultSiteId);
					if (value == null || !(value is int))
					{
						result = defaultSiteId;
					}
					else
					{
						result = (int)value;
					}
				}
			}
			return result;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0001A8B4 File Offset: 0x00018AB4
		private static Dictionary<string, bool> ParseTrustedNamespaces(string namespaces)
		{
			ExTraceGlobals.AuthenticationTracer.TraceFunction(0L, "Entering ParseTrustedNamespaces");
			string[] array = namespaces.Split(new char[]
			{
				';'
			});
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>(array.Length);
			foreach (string text in array)
			{
				string text2 = text.Trim().ToLowerInvariant();
				if (!string.IsNullOrEmpty(text2))
				{
					ExTraceGlobals.AuthenticationTracer.TraceDebug<string>(0L, "Adding trusted namespace '{0}'", text2);
					dictionary.Add(text2, true);
				}
			}
			AuthServiceStaticConfig.counters.NamespaceWhitelistSize.RawValue = (long)dictionary.Count;
			ExTraceGlobals.AuthenticationTracer.TraceFunction(0L, "Leaving ParseTrustedNamespaces");
			return dictionary;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0001A964 File Offset: 0x00018B64
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0001A96B File Offset: 0x00018B6B
		internal static DateTime? OfflineOrgIdFlagLastRetrieveTime { get; private set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0001A973 File Offset: 0x00018B73
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0001A97A File Offset: 0x00018B7A
		internal static bool IsOfflineOrgIdEnabled { get; private set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0001A982 File Offset: 0x00018B82
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0001A989 File Offset: 0x00018B89
		internal static bool IsOrgIdOutage { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0001A991 File Offset: 0x00018B91
		public Regex InternalIPFilterRegex
		{
			get
			{
				if (this.internalIPFilterRegex == null)
				{
					this.internalIPFilterRegex = new Regex(this.InternalIPFilterRegexPattern);
				}
				return this.internalIPFilterRegex;
			}
		}

		// Token: 0x040002D1 RID: 721
		private const string DefaultInternalIPFilterRegexPattern = "^((10\\.\\d+\\.\\d+\\.\\d+)|(65\\.5[2-5]\\.\\d+\\.\\d+)|(157\\.5[6-9]\\.\\d+\\.\\d+)|(157\\.5[4-5]\\.\\d+\\.\\d+)|(132\\.245\\.\\d+\\.\\d+)|(141\\.251\\.\\d+\\.\\d+)|(157\\.60\\.\\d+\\.\\d+)|(207\\.46\\.\\d+\\.\\d+)|(70\\.37\\.(([0-9]|[1-9][0-9])|1((0[0-9]|1[0-9])|2[0-7]))\\.\\d+)|(70\\.37\\.1(2[8-9]|[3-8][0-9]|9[0-1])\\.\\d+)|(94\\.245\\.((6[4-9]|[7-9][0-9])|1((0[0-9]|1[0-9])|2[0-7]))\\.\\d+)|(111\\.221\\.((6[4-9]|[7-9][0-9])|1((0[0-9]|1[0-9])|2[0-7]))\\.\\d+)|(206\\.191\\.2(2[4-9]|[3-4][0-9]|5[0-5])\\.\\d+)|(213\\.199\\.1([6-8][0-9]|9[0-1])\\.\\d+)|(111\\.221\\.(1[6-9]|2[0-9]|3[0-1])\\.\\d+)|(23\\.(9[6-9]|10[0-3])\\.\\d+\\.\\d+)|(191\\.23[2-5]\\.\\d+\\.\\d+)|(134\\.170\\.\\d+\\.\\d+)|(141\\.252\\.\\d+\\.\\d+)|(25\\.160\\.\\d+\\.\\d+))$";

		// Token: 0x040002D2 RID: 722
		private static AuthServiceStaticConfig privateConfig = null;

		// Token: 0x040002D3 RID: 723
		private static readonly LiveIdBasicAuthenticationCountersInstance counters = AuthServiceHelper.PerformanceCounters;

		// Token: 0x040002D4 RID: 724
		private static readonly ExEventLog eventLogger = AuthServiceHelper.EventLogger;

		// Token: 0x040002D5 RID: 725
		private static readonly string ExchangeLiveServicesKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ExchangeLiveServices\\";

		// Token: 0x040002D6 RID: 726
		public int configLifetimeSeconds = 600;

		// Token: 0x040002D7 RID: 727
		public int workerMin;

		// Token: 0x040002D8 RID: 728
		public int ioPortMin;

		// Token: 0x040002D9 RID: 729
		public int hrdCacheBucketSize = 1250;

		// Token: 0x040002DA RID: 730
		public int hrdCacheBuckets = 16;

		// Token: 0x040002DB RID: 731
		public int certErrorCacheBucketSize = 625;

		// Token: 0x040002DC RID: 732
		public int certErrorCacheBuckets = 16;

		// Token: 0x040002DD RID: 733
		public int hrdCacheLifetime = 240;

		// Token: 0x040002DE RID: 734
		public int statisticCacheSize = 1000;

		// Token: 0x040002DF RID: 735
		public int statisticLifetime = 120;

		// Token: 0x040002E0 RID: 736
		public int logonCacheSize = 10000;

		// Token: 0x040002E1 RID: 737
		public int logonCacheLifetime = 1440;

		// Token: 0x040002E2 RID: 738
		public int ADLookupCacheLifetimeOffline = 30;

		// Token: 0x040002E3 RID: 739
		public int badCredsLifetime = 5;

		// Token: 0x040002E4 RID: 740
		public int badCredsRecoverableLifetime = 2;

		// Token: 0x040002E5 RID: 741
		public int level1BadCredCacheSize = 10000;

		// Token: 0x040002E6 RID: 742
		public int level1BadCredLifetime = 10;

		// Token: 0x040002E7 RID: 743
		public int level2BadCredCacheSize = 10000;

		// Token: 0x040002E8 RID: 744
		public int level2BadCredLifetime = 10;

		// Token: 0x040002E9 RID: 745
		public int level2BadCredListSize = 5;

		// Token: 0x040002EA RID: 746
		public int defaultTimeout = 45;

		// Token: 0x040002EB RID: 747
		public bool bypassTOUCheck = true;

		// Token: 0x040002EC RID: 748
		public bool bypassAcceptedDomainUPNCheck;

		// Token: 0x040002ED RID: 749
		public int adfsSkewThreshold = 4;

		// Token: 0x040002EE RID: 750
		public int shibbSkewThreshold = 4;

		// Token: 0x040002EF RID: 751
		public string siteName = "outlook.com";

		// Token: 0x040002F0 RID: 752
		public string msoSiteName = "outlook.com";

		// Token: 0x040002F1 RID: 753
		public int maxShibbolethResponseSize = 131072;

		// Token: 0x040002F2 RID: 754
		public int maxAdfsResponseSize = 131072;

		// Token: 0x040002F3 RID: 755
		public string liveRst2Login = "HTTPS://login.live.com/RST2.srf";

		// Token: 0x040002F4 RID: 756
		public string liveHttpPostLogin = "HTTPS://login.live.com/login.srf";

		// Token: 0x040002F5 RID: 757
		public string liveHttpPostAssertionConsumerService = "AssertionConsumerServiceIndex=\"0\"";

		// Token: 0x040002F6 RID: 758
		public string liveRealmDiscoveryUri = "HTTPS://login.live.com/GetUserRealm.srf";

		// Token: 0x040002F7 RID: 759
		public string msoRst2Login = "HTTPS://login.microsoftonline.com/RST2.srf";

		// Token: 0x040002F8 RID: 760
		private string msoRst2LoginKeepAlive = "HTTPS://login.microsoftonline.com/RST2EX.srf";

		// Token: 0x040002F9 RID: 761
		private string msoRst2LoginEXOKeepAlive = "HTTPS://loginex.microsoftonline.com/RST2EX.srf";

		// Token: 0x040002FA RID: 762
		public string msoHttpPostLogin = "HTTPS://login.microsoftonline.com/login.srf";

		// Token: 0x040002FB RID: 763
		private string msoHttpPostLoginKeepAlive = "HTTPS://login.microsoftonline.com/loginex.srf";

		// Token: 0x040002FC RID: 764
		private string msoHttpPostLoginEXOKeepAlive = "HTTPS://loginex.microsoftonline.com/loginex.srf";

		// Token: 0x040002FD RID: 765
		public string msoHttpPostAssertionConsumerService = "AssertionConsumerServiceIndex=\"0\"";

		// Token: 0x040002FE RID: 766
		public string msoRealmDiscoveryUri = "HTTPS://login.microsoftonline.com/GetUserRealm.srf";

		// Token: 0x040002FF RID: 767
		private string msoRealmDiscoveryUriKeepAlive = "HTTPS://login.microsoftonline.com/GetUserRealmex.srf";

		// Token: 0x04000300 RID: 768
		private string msoRealmDiscoveryUriEXOKeepAlive = "HTTPS://loginex.microsoftonline.com/GetUserRealmex.srf";

		// Token: 0x04000301 RID: 769
		public string liveidXmlAuth = "https://xml.login.live.com/ppsecure/clientpost.srf";

		// Token: 0x04000302 RID: 770
		public static readonly string SiteIdName = "SiteId";

		// Token: 0x04000303 RID: 771
		public int MSASiteId = 260563;

		// Token: 0x04000304 RID: 772
		public string MSAProfilePolicy = "MBI_SSL";

		// Token: 0x04000305 RID: 773
		public bool verifySamlSignatures = true;

		// Token: 0x04000306 RID: 774
		public int passwordExpiryUpperBound = 14;

		// Token: 0x04000307 RID: 775
		public int blacklistTime = 15;

		// Token: 0x04000308 RID: 776
		public int tarpitTime = 15;

		// Token: 0x04000309 RID: 777
		public int blacklistThreshold = 100;

		// Token: 0x0400030A RID: 778
		public int timedOutResponseLower = 20;

		// Token: 0x0400030B RID: 779
		public int timedOutResponseUpper = 90;

		// Token: 0x0400030C RID: 780
		public int badUserPasswordLower = 30;

		// Token: 0x0400030D RID: 781
		public int badUserPasswordUpper = 70;

		// Token: 0x0400030E RID: 782
		public int tokenSizeExceededLower = 1;

		// Token: 0x0400030F RID: 783
		public int tokenSizeExceededUpper = 50;

		// Token: 0x04000310 RID: 784
		public int failedResponseLower = 20;

		// Token: 0x04000311 RID: 785
		public int failedResponseUpper = 90;

		// Token: 0x04000312 RID: 786
		public bool enableTimedOutBlacklist = true;

		// Token: 0x04000313 RID: 787
		public bool enableBadUserPasswordBlacklist;

		// Token: 0x04000314 RID: 788
		public bool enableTokenSizeExceededBlacklist = true;

		// Token: 0x04000315 RID: 789
		public bool enableFailedResponseBlacklist = true;

		// Token: 0x04000316 RID: 790
		public Dictionary<string, bool> trustedNamespaces = new Dictionary<string, bool>();

		// Token: 0x04000317 RID: 791
		public LiveIdInstanceType defaultInstance = LiveIdInstanceType.Business;

		// Token: 0x04000318 RID: 792
		public bool defaultTenantNegoConfig;

		// Token: 0x04000319 RID: 793
		public int percentileCountersUpdateIntervalInSeconds = 60;

		// Token: 0x0400031A RID: 794
		public int percentileCountersLastMinutes = 1;

		// Token: 0x0400031B RID: 795
		public int percentileCountersUnitOfExpiryInSeconds = 1;

		// Token: 0x0400031C RID: 796
		public int numberOfTotalRequestsToIgnoreInPercentileCounter = 2;

		// Token: 0x0400031D RID: 797
		public int liveHrdDelay;

		// Token: 0x0400031E RID: 798
		public int msoHrdDelay;

		// Token: 0x0400031F RID: 799
		public int liveRst2Delay;

		// Token: 0x04000320 RID: 800
		public int msoRst2Delay;

		// Token: 0x04000321 RID: 801
		public int certErrorExpireTimeInSeconds = 60;

		// Token: 0x04000322 RID: 802
		public int ADUserCacheBucketSize = 2500;

		// Token: 0x04000323 RID: 803
		public int ADUserCacheBuckets = 5;

		// Token: 0x04000324 RID: 804
		public int ADUserEntryExpireTimeInMinutes = 30;

		// Token: 0x04000325 RID: 805
		private int? OfflineOrgIdProvisioningFlags;

		// Token: 0x04000326 RID: 806
		public int PasswordConfidenceInDays = 5;

		// Token: 0x04000327 RID: 807
		public int PasswordConfidenceInDaysForADFSDown = 1;

		// Token: 0x04000328 RID: 808
		public int LiveIdStsTimeoutInSeconds = 15;

		// Token: 0x04000329 RID: 809
		public int ConnectionLeaseTimeoutInSeconds = 120;

		// Token: 0x0400032A RID: 810
		public int MaxServicePointIdleTimeInSeconds = 120;

		// Token: 0x0400032B RID: 811
		public string ConnectionGroupPrefix = "EXO-CG-";

		// Token: 0x0400032C RID: 812
		public MsoEndpointType? msoEndpointType = null;

		// Token: 0x0400032D RID: 813
		public int MaxConnectionGroups = 1;

		// Token: 0x0400032E RID: 814
		public int MaxCacheSizeOfLastLogonTime = 100000;

		// Token: 0x0400032F RID: 815
		public int TOUByPassCacheExpireTimeInMinutes = 480;

		// Token: 0x04000330 RID: 816
		public int TOUCacheBuckets = 2;

		// Token: 0x04000331 RID: 817
		public int TOUCacheBucketSize = 1000;

		// Token: 0x04000332 RID: 818
		public int RpsTicketLifetime = 3600;

		// Token: 0x04000333 RID: 819
		public int OfflineHrdMaxParentDomainRetry = 3;

		// Token: 0x04000334 RID: 820
		public int OfflineOrgIdWaitTimeInSeconds = 2;

		// Token: 0x04000335 RID: 821
		public int OfflineOrgIdPrimaryUsageThreholdPercentage = 20;

		// Token: 0x04000336 RID: 822
		public bool ThrottleOfflineOrgIdAsPrimaryUsage = true;

		// Token: 0x04000337 RID: 823
		public bool AllowOfflineOrgIdForADFS = true;

		// Token: 0x04000338 RID: 824
		public string AppPasswordHelpUrl = "http://technet.microsoft.com/en-us/library/dn270518.aspx#howapppassword";

		// Token: 0x04000339 RID: 825
		public bool EnablePasswordInvalidationForManagedUser;

		// Token: 0x0400033A RID: 826
		public bool EnablePasswordInvalidationForFederatedUser;

		// Token: 0x0400033B RID: 827
		private string InternalIPFilterRegexPattern = "^((10\\.\\d+\\.\\d+\\.\\d+)|(65\\.5[2-5]\\.\\d+\\.\\d+)|(157\\.5[6-9]\\.\\d+\\.\\d+)|(157\\.5[4-5]\\.\\d+\\.\\d+)|(132\\.245\\.\\d+\\.\\d+)|(141\\.251\\.\\d+\\.\\d+)|(157\\.60\\.\\d+\\.\\d+)|(207\\.46\\.\\d+\\.\\d+)|(70\\.37\\.(([0-9]|[1-9][0-9])|1((0[0-9]|1[0-9])|2[0-7]))\\.\\d+)|(70\\.37\\.1(2[8-9]|[3-8][0-9]|9[0-1])\\.\\d+)|(94\\.245\\.((6[4-9]|[7-9][0-9])|1((0[0-9]|1[0-9])|2[0-7]))\\.\\d+)|(111\\.221\\.((6[4-9]|[7-9][0-9])|1((0[0-9]|1[0-9])|2[0-7]))\\.\\d+)|(206\\.191\\.2(2[4-9]|[3-4][0-9]|5[0-5])\\.\\d+)|(213\\.199\\.1([6-8][0-9]|9[0-1])\\.\\d+)|(111\\.221\\.(1[6-9]|2[0-9]|3[0-1])\\.\\d+)|(23\\.(9[6-9]|10[0-3])\\.\\d+\\.\\d+)|(191\\.23[2-5]\\.\\d+\\.\\d+)|(134\\.170\\.\\d+\\.\\d+)|(141\\.252\\.\\d+\\.\\d+)|(25\\.160\\.\\d+\\.\\d+))$";

		// Token: 0x0400033C RID: 828
		private Regex internalIPFilterRegex;

		// Token: 0x0400033D RID: 829
		private string AdditionalOutlookComRegexPattern = string.Empty;

		// Token: 0x0400033E RID: 830
		public Regex outlookComRegex;

		// Token: 0x0400033F RID: 831
		public int LastLogonTimeUpdateFrequency = 24;

		// Token: 0x04000340 RID: 832
		public string ReqStatusForResponseDump = string.Empty;

		// Token: 0x04000341 RID: 833
		public string AuthStatusForResponseDump = string.Empty;

		// Token: 0x04000342 RID: 834
		public bool EnableXmlAuthForLiveId;

		// Token: 0x04000343 RID: 835
		public bool EnableRemoteRPSForCompactTicket;

		// Token: 0x04000344 RID: 836
		public bool EnableMailboxBasedPasswordConfidence = true;

		// Token: 0x04000345 RID: 837
		public bool EnableConsumerRPSSync;

		// Token: 0x04000346 RID: 838
		public bool UpdateExistingMbxEntryOnly = true;
	}
}
