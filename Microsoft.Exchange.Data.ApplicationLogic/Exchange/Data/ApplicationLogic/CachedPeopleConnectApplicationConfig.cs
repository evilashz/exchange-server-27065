using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Dkm;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x02000177 RID: 375
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CachedPeopleConnectApplicationConfig
	{
		// Token: 0x06000EA3 RID: 3747 RVA: 0x0003C2EC File Offset: 0x0003A4EC
		internal CachedPeopleConnectApplicationConfig(IPeopleConnectApplicationConfigCache cache, IPeopleConnectApplicationConfigADReader reader)
		{
			if (cache == null)
			{
				throw new ArgumentNullException("cache");
			}
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.appADConfigCache = cache;
			this.appADConfigReader = reader;
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x0003C31E File Offset: 0x0003A51E
		public static CachedPeopleConnectApplicationConfig Instance
		{
			get
			{
				return CachedPeopleConnectApplicationConfig.instance;
			}
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0003C325 File Offset: 0x0003A525
		public IPeopleConnectApplicationConfig ReadLinkedIn()
		{
			return this.ReadProvider("linkedin");
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0003C332 File Offset: 0x0003A532
		public IPeopleConnectApplicationConfig ReadFacebook()
		{
			return this.ReadProvider("facebook");
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0003C340 File Offset: 0x0003A540
		public IPeopleConnectApplicationConfig ReadProvider(string provider)
		{
			if (string.IsNullOrEmpty(provider))
			{
				throw new ArgumentNullException("provider");
			}
			try
			{
				string a;
				if ((a = provider.Trim().ToLowerInvariant()) != null)
				{
					if (a == "facebook")
					{
						return this.RetrieveCachedFacebookADConfig().OverrideWith(this.RetrieveCachedWebProxyADConfig()).OverrideWith(this.ReadFacebookConfigFromRegistry());
					}
					if (a == "linkedin")
					{
						return this.RetrieveCachedLinkedInADConfig().OverrideWith(this.RetrieveCachedWebProxyADConfig()).OverrideWith(this.ReadLinkedInConfigFromRegistry());
					}
				}
				throw new ArgumentOutOfRangeException("provider");
			}
			catch (AuthServerNotFoundException innerException)
			{
				throw new ExchangeConfigurationException(Strings.FailedToReadProviderConfigurationSeeInnerException(provider), innerException);
			}
			catch (InvalidAuthConfigurationException innerException2)
			{
				throw new ExchangeConfigurationException(Strings.FailedToReadProviderConfigurationSeeInnerException(provider), innerException2);
			}
			catch (ServiceEndpointNotFoundException innerException3)
			{
				throw new ExchangeConfigurationException(Strings.FailedToReadProviderConfigurationSeeInnerException(provider), innerException3);
			}
			catch (EndpointContainerNotFoundException innerException4)
			{
				throw new ExchangeConfigurationException(Strings.FailedToReadProviderConfigurationSeeInnerException(provider), innerException4);
			}
			catch (ADTransientException innerException5)
			{
				throw new ExchangeConfigurationException(Strings.FailedToReadProviderConfigurationSeeInnerException(provider), innerException5);
			}
			catch (ADOperationException innerException6)
			{
				throw new ExchangeConfigurationException(Strings.FailedToReadProviderConfigurationSeeInnerException(provider), innerException6);
			}
			catch (CryptographicException innerException7)
			{
				throw new ExchangeConfigurationException(Strings.FailedToReadProviderConfigurationSeeInnerException(provider), innerException7);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw;
			}
			catch (ExchangeConfigurationException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (CachedPeopleConnectApplicationConfig.Dkm.IsDkmException(ex))
				{
					throw new ExchangeConfigurationException(Strings.FailedToReadProviderConfigurationSeeInnerException(provider), ex);
				}
				throw;
			}
			IPeopleConnectApplicationConfig result;
			return result;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0003C4EC File Offset: 0x0003A6EC
		private IPeopleConnectApplicationConfig RetrieveCachedLinkedInADConfig()
		{
			if (PeopleConnectRegistryReader.Read().DogfoodInEnterprise)
			{
				return new CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig();
			}
			IPeopleConnectApplicationConfig peopleConnectApplicationConfig;
			if (this.appADConfigCache.TryGetValue("linkedin", out peopleConnectApplicationConfig))
			{
				return peopleConnectApplicationConfig;
			}
			peopleConnectApplicationConfig = this.ReadLinkedInConfigFromAD();
			this.ValidateLinkedInConfig(peopleConnectApplicationConfig);
			this.appADConfigCache.Add("linkedin", peopleConnectApplicationConfig);
			return peopleConnectApplicationConfig;
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0003C544 File Offset: 0x0003A744
		private IPeopleConnectApplicationConfig RetrieveCachedFacebookADConfig()
		{
			if (PeopleConnectRegistryReader.Read().DogfoodInEnterprise)
			{
				return new CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig();
			}
			IPeopleConnectApplicationConfig peopleConnectApplicationConfig;
			if (this.appADConfigCache.TryGetValue("facebook", out peopleConnectApplicationConfig))
			{
				return peopleConnectApplicationConfig;
			}
			peopleConnectApplicationConfig = this.ReadFacebookConfigFromAD();
			this.ValidateFacebookConfig(peopleConnectApplicationConfig);
			this.appADConfigCache.Add("facebook", peopleConnectApplicationConfig);
			return peopleConnectApplicationConfig;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0003C59C File Offset: 0x0003A79C
		private CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig ReadLinkedInConfigFromAD()
		{
			AuthServer authServer = this.appADConfigReader.ReadLinkedInAuthServer();
			return new CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig
			{
				AccessTokenEndpoint = authServer.TokenIssuingEndpoint,
				AppId = authServer.ApplicationIdentifier,
				AppSecretEncrypted = authServer.CurrentEncryptedAppSecret,
				DecryptAppSecret = new Func<string, string>(CachedPeopleConnectApplicationConfig.DecryptAppSecretWithDkm),
				RequestTokenEndpoint = authServer.AuthorizationEndpoint,
				ProfileEndpoint = this.appADConfigReader.ReadLinkedInProfileEndpoint(),
				ConnectionsEndpoint = this.appADConfigReader.ReadLinkedInConnectionsEndpoint(),
				RemoveAppEndpoint = this.appADConfigReader.ReadLinkedInInvalidateTokenEndpoint(),
				ReadTimeUtc = DateTime.UtcNow
			};
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0003C63C File Offset: 0x0003A83C
		private CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig ReadFacebookConfigFromAD()
		{
			AuthServer authServer = this.appADConfigReader.ReadFacebookAuthServer();
			return new CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig
			{
				AppId = authServer.ApplicationIdentifier,
				AppSecretEncrypted = authServer.CurrentEncryptedAppSecret,
				DecryptAppSecret = new Func<string, string>(CachedPeopleConnectApplicationConfig.DecryptAppSecretWithDkm),
				AuthorizationEndpoint = authServer.AuthorizationEndpoint,
				GraphTokenEndpoint = authServer.TokenIssuingEndpoint,
				GraphApiEndpoint = this.appADConfigReader.ReadFacebookGraphApiEndpoint(),
				ReadTimeUtc = DateTime.UtcNow
			};
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0003C6BC File Offset: 0x0003A8BC
		private CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig ReadLinkedInConfigFromRegistry()
		{
			LinkedInRegistryReader linkedInRegistryReader = LinkedInRegistryReader.Read();
			return new CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig
			{
				AccessTokenEndpoint = linkedInRegistryReader.AccessTokenEndpoint,
				AppId = linkedInRegistryReader.AppId,
				AppSecretEncrypted = linkedInRegistryReader.AppSecret,
				DecryptAppSecret = new Func<string, string>(CachedPeopleConnectApplicationConfig.NoOpDecrypt),
				ConnectionsEndpoint = linkedInRegistryReader.ConnectionsEndpoint,
				ConsentRedirectEndpoint = linkedInRegistryReader.ConsentRedirectEndpoint,
				ProfileEndpoint = linkedInRegistryReader.ProfileEndpoint,
				RequestTokenEndpoint = linkedInRegistryReader.RequestTokenEndpoint,
				WebRequestTimeout = linkedInRegistryReader.WebRequestTimeout,
				RemoveAppEndpoint = linkedInRegistryReader.RemoveAppEndpoint,
				WebProxyUri = linkedInRegistryReader.WebProxyUri,
				ReadTimeUtc = DateTime.UtcNow
			};
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003C76C File Offset: 0x0003A96C
		private CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig ReadFacebookConfigFromRegistry()
		{
			FacebookRegistryReader facebookRegistryReader = FacebookRegistryReader.Read();
			return new CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig
			{
				AppId = facebookRegistryReader.AppId,
				AppSecretEncrypted = facebookRegistryReader.AppSecret,
				ConsentRedirectEndpoint = facebookRegistryReader.ConsentRedirectEndpoint,
				DecryptAppSecret = new Func<string, string>(CachedPeopleConnectApplicationConfig.NoOpDecrypt),
				AuthorizationEndpoint = facebookRegistryReader.AuthorizationEndpoint,
				GraphApiEndpoint = facebookRegistryReader.GraphApiEndpoint,
				GraphTokenEndpoint = facebookRegistryReader.GraphTokenEndpoint,
				WebRequestTimeout = facebookRegistryReader.WebRequestTimeout,
				SkipContactUpload = facebookRegistryReader.SkipContactUpload,
				ContinueOnContactUploadFailure = facebookRegistryReader.ContinueOnContactUploadFailure,
				WaitForContactUploadCommit = facebookRegistryReader.WaitForContactUploadCommit,
				NotifyOnEachContactUpload = facebookRegistryReader.NotifyOnEachContactUpload,
				MaximumContactsToUpload = facebookRegistryReader.MaximumContactsToUpload,
				ReadTimeUtc = DateTime.UtcNow
			};
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0003C834 File Offset: 0x0003AA34
		private CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig RetrieveCachedWebProxyADConfig()
		{
			return new CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig
			{
				WebProxyUri = this.appADConfigReader.ReadWebProxyUri()
			};
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0003C85C File Offset: 0x0003AA5C
		private void ValidateLinkedInConfig(IPeopleConnectApplicationConfig config)
		{
			CachedPeopleConnectApplicationConfig.ThrowExchangeConfigurationExceptionIfBlank(config.AccessTokenEndpoint, Strings.InvalidConfigurationMissingLinkedInAccessTokenEndpoint);
			CachedPeopleConnectApplicationConfig.ThrowExchangeConfigurationExceptionIfBlank(config.AppId, Strings.InvalidConfigurationMissingLinkedInAppId);
			CachedPeopleConnectApplicationConfig.ThrowExchangeConfigurationExceptionIfMatches(config.AppId, CachedPeopleConnectApplicationConfig.KnownInvalidAppIds, StringComparer.OrdinalIgnoreCase, Strings.InvalidConfigurationLinkedInAppId);
			CachedPeopleConnectApplicationConfig.ThrowExchangeConfigurationExceptionIfBlank(config.AppSecretEncrypted, Strings.InvalidConfigurationMissingLinkedInAppSecret);
			CachedPeopleConnectApplicationConfig.ThrowExchangeConfigurationExceptionIfMatches(config.AppSecretClearText, CachedPeopleConnectApplicationConfig.KnownInvalidAppSecrets, StringComparer.OrdinalIgnoreCase, Strings.InvalidConfigurationLinkedInAppSecret);
			CachedPeopleConnectApplicationConfig.ThrowExchangeConfigurationExceptionIfBlank(config.RequestTokenEndpoint, Strings.InvalidConfigurationMissingLinkedInRequestTokenEndpoint);
			CachedPeopleConnectApplicationConfig.ThrowExchangeConfigurationExceptionIfBlank(config.ConnectionsEndpoint, Strings.InvalidConfigurationMissingLinkedInConnectionsEndpoint);
			CachedPeopleConnectApplicationConfig.ThrowExchangeConfigurationExceptionIfBlank(config.ProfileEndpoint, Strings.InvalidConfigurationMissingLinkedInProfileEndpoint);
			CachedPeopleConnectApplicationConfig.ThrowExchangeConfigurationExceptionIfBlank(config.RemoveAppEndpoint, Strings.InvalidConfigurationMissingLinkedInInvalidateTokenEndpoint);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003C910 File Offset: 0x0003AB10
		private void ValidateFacebookConfig(IPeopleConnectApplicationConfig config)
		{
			CachedPeopleConnectApplicationConfig.ThrowExchangeConfigurationExceptionIfBlank(config.AppId, Strings.InvalidConfigurationMissingFacebookAppId);
			CachedPeopleConnectApplicationConfig.ThrowExchangeConfigurationExceptionIfMatches(config.AppId, CachedPeopleConnectApplicationConfig.KnownInvalidAppIds, StringComparer.OrdinalIgnoreCase, Strings.InvalidConfigurationFacebookAppId);
			CachedPeopleConnectApplicationConfig.ThrowExchangeConfigurationExceptionIfBlank(config.AppSecretEncrypted, Strings.InvalidConfigurationMissingFacebookAppSecret);
			CachedPeopleConnectApplicationConfig.ThrowExchangeConfigurationExceptionIfMatches(config.AppSecretClearText, CachedPeopleConnectApplicationConfig.KnownInvalidAppSecrets, StringComparer.OrdinalIgnoreCase, Strings.InvalidConfigurationFacebookAppSecret);
			CachedPeopleConnectApplicationConfig.ThrowExchangeConfigurationExceptionIfBlank(config.AuthorizationEndpoint, Strings.InvalidConfigurationMissingFacebookAuthorizationEndpoint);
			CachedPeopleConnectApplicationConfig.ThrowExchangeConfigurationExceptionIfBlank(config.GraphTokenEndpoint, Strings.InvalidConfigurationMissingFacebookGraphTokenEndpoint);
			CachedPeopleConnectApplicationConfig.ThrowExchangeConfigurationExceptionIfBlank(config.GraphApiEndpoint, Strings.InvalidConfigurationMissingFacebookGraphApiEndpoint);
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0003C9A1 File Offset: 0x0003ABA1
		private static void ThrowExchangeConfigurationExceptionIfBlank(string s, LocalizedString errorMessage)
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				throw new ExchangeConfigurationException(errorMessage);
			}
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0003C9B2 File Offset: 0x0003ABB2
		private static void ThrowExchangeConfigurationExceptionIfMatches(string s, IEnumerable<string> matches, StringComparer comparer, LocalizedString errorMessage)
		{
			if (matches == null)
			{
				return;
			}
			if (matches.Contains(s, comparer))
			{
				throw new ExchangeConfigurationException(errorMessage);
			}
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0003C9CC File Offset: 0x0003ABCC
		private static string DecryptAppSecretWithDkm(string encryptedAppSecret)
		{
			string result;
			using (SecureString secureString = CachedPeopleConnectApplicationConfig.Dkm.EncryptedStringToSecureString(encryptedAppSecret))
			{
				result = secureString.AsUnsecureString();
			}
			return result;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0003CA0C File Offset: 0x0003AC0C
		private static string NoOpDecrypt(string secret)
		{
			return secret;
		}

		// Token: 0x040007D7 RID: 2007
		private const string FacebookLowerCase = "facebook";

		// Token: 0x040007D8 RID: 2008
		private const string LinkedInLowerCase = "linkedin";

		// Token: 0x040007D9 RID: 2009
		private static readonly Trace Tracer = ExTraceGlobals.PeopleConnectConfigurationTracer;

		// Token: 0x040007DA RID: 2010
		private static readonly CachedPeopleConnectApplicationConfig instance = new CachedPeopleConnectApplicationConfig(new CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfigCacheImpl(), new CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfigADReaderImpl());

		// Token: 0x040007DB RID: 2011
		private static readonly IExchangeGroupKey Dkm = PeopleConnectExchangeGroupKeyFactory.Create();

		// Token: 0x040007DC RID: 2012
		private static readonly string[] KnownInvalidAppIds = new string[]
		{
			"0"
		};

		// Token: 0x040007DD RID: 2013
		private static readonly string[] KnownInvalidAppSecrets = new string[]
		{
			"%Prod_PeopleConnectFacebookAppSecret%",
			"%Prod_PeopleConnectLinkedInAppSecret%",
			"%Gallatin_PeopleConnectLinkedInAppSecret%"
		};

		// Token: 0x040007DE RID: 2014
		private readonly IPeopleConnectApplicationConfigCache appADConfigCache;

		// Token: 0x040007DF RID: 2015
		private readonly IPeopleConnectApplicationConfigADReader appADConfigReader;

		// Token: 0x02000179 RID: 377
		private sealed class PeopleConnectApplicationConfig : CachableItem, IPeopleConnectApplicationConfig
		{
			// Token: 0x1700038E RID: 910
			// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0003CA7F File Offset: 0x0003AC7F
			// (set) Token: 0x06000ECD RID: 3789 RVA: 0x0003CA87 File Offset: 0x0003AC87
			public string AppId { get; internal set; }

			// Token: 0x1700038F RID: 911
			// (get) Token: 0x06000ECE RID: 3790 RVA: 0x0003CA90 File Offset: 0x0003AC90
			// (set) Token: 0x06000ECF RID: 3791 RVA: 0x0003CA98 File Offset: 0x0003AC98
			public string AppSecretEncrypted { get; internal set; }

			// Token: 0x17000390 RID: 912
			// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x0003CAA1 File Offset: 0x0003ACA1
			// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x0003CAA9 File Offset: 0x0003ACA9
			public Func<string, string> DecryptAppSecret { get; internal set; }

			// Token: 0x17000391 RID: 913
			// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x0003CAB2 File Offset: 0x0003ACB2
			public string AppSecretClearText
			{
				get
				{
					return this.DecryptAppSecret(this.AppSecretEncrypted);
				}
			}

			// Token: 0x17000392 RID: 914
			// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x0003CAC5 File Offset: 0x0003ACC5
			// (set) Token: 0x06000ED4 RID: 3796 RVA: 0x0003CACD File Offset: 0x0003ACCD
			public string AuthorizationEndpoint { get; internal set; }

			// Token: 0x17000393 RID: 915
			// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x0003CAD6 File Offset: 0x0003ACD6
			// (set) Token: 0x06000ED6 RID: 3798 RVA: 0x0003CADE File Offset: 0x0003ACDE
			public string GraphTokenEndpoint { get; internal set; }

			// Token: 0x17000394 RID: 916
			// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x0003CAE7 File Offset: 0x0003ACE7
			// (set) Token: 0x06000ED8 RID: 3800 RVA: 0x0003CAEF File Offset: 0x0003ACEF
			public string GraphApiEndpoint { get; internal set; }

			// Token: 0x17000395 RID: 917
			// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x0003CAF8 File Offset: 0x0003ACF8
			// (set) Token: 0x06000EDA RID: 3802 RVA: 0x0003CB00 File Offset: 0x0003AD00
			public string RequestTokenEndpoint { get; internal set; }

			// Token: 0x17000396 RID: 918
			// (get) Token: 0x06000EDB RID: 3803 RVA: 0x0003CB09 File Offset: 0x0003AD09
			// (set) Token: 0x06000EDC RID: 3804 RVA: 0x0003CB11 File Offset: 0x0003AD11
			public string AccessTokenEndpoint { get; internal set; }

			// Token: 0x17000397 RID: 919
			// (get) Token: 0x06000EDD RID: 3805 RVA: 0x0003CB1A File Offset: 0x0003AD1A
			// (set) Token: 0x06000EDE RID: 3806 RVA: 0x0003CB22 File Offset: 0x0003AD22
			public string ProfileEndpoint { get; internal set; }

			// Token: 0x17000398 RID: 920
			// (get) Token: 0x06000EDF RID: 3807 RVA: 0x0003CB2B File Offset: 0x0003AD2B
			// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x0003CB33 File Offset: 0x0003AD33
			public string ConnectionsEndpoint { get; internal set; }

			// Token: 0x17000399 RID: 921
			// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x0003CB3C File Offset: 0x0003AD3C
			// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x0003CB44 File Offset: 0x0003AD44
			public string RemoveAppEndpoint { get; internal set; }

			// Token: 0x1700039A RID: 922
			// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x0003CB4D File Offset: 0x0003AD4D
			// (set) Token: 0x06000EE4 RID: 3812 RVA: 0x0003CB55 File Offset: 0x0003AD55
			public string ConsentRedirectEndpoint { get; internal set; }

			// Token: 0x1700039B RID: 923
			// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x0003CB5E File Offset: 0x0003AD5E
			// (set) Token: 0x06000EE6 RID: 3814 RVA: 0x0003CB66 File Offset: 0x0003AD66
			public TimeSpan WebRequestTimeout { get; internal set; }

			// Token: 0x1700039C RID: 924
			// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x0003CB6F File Offset: 0x0003AD6F
			// (set) Token: 0x06000EE8 RID: 3816 RVA: 0x0003CB77 File Offset: 0x0003AD77
			public string WebProxyUri { get; internal set; }

			// Token: 0x1700039D RID: 925
			// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x0003CB80 File Offset: 0x0003AD80
			// (set) Token: 0x06000EEA RID: 3818 RVA: 0x0003CB88 File Offset: 0x0003AD88
			public bool SkipContactUpload { get; internal set; }

			// Token: 0x1700039E RID: 926
			// (get) Token: 0x06000EEB RID: 3819 RVA: 0x0003CB91 File Offset: 0x0003AD91
			// (set) Token: 0x06000EEC RID: 3820 RVA: 0x0003CB99 File Offset: 0x0003AD99
			public bool ContinueOnContactUploadFailure { get; internal set; }

			// Token: 0x1700039F RID: 927
			// (get) Token: 0x06000EED RID: 3821 RVA: 0x0003CBA2 File Offset: 0x0003ADA2
			// (set) Token: 0x06000EEE RID: 3822 RVA: 0x0003CBAA File Offset: 0x0003ADAA
			public bool WaitForContactUploadCommit { get; internal set; }

			// Token: 0x170003A0 RID: 928
			// (get) Token: 0x06000EEF RID: 3823 RVA: 0x0003CBB3 File Offset: 0x0003ADB3
			// (set) Token: 0x06000EF0 RID: 3824 RVA: 0x0003CBBB File Offset: 0x0003ADBB
			public bool NotifyOnEachContactUpload { get; internal set; }

			// Token: 0x170003A1 RID: 929
			// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x0003CBC4 File Offset: 0x0003ADC4
			// (set) Token: 0x06000EF2 RID: 3826 RVA: 0x0003CBCC File Offset: 0x0003ADCC
			public int MaximumContactsToUpload { get; internal set; }

			// Token: 0x170003A2 RID: 930
			// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0003CBD5 File Offset: 0x0003ADD5
			// (set) Token: 0x06000EF4 RID: 3828 RVA: 0x0003CBDD File Offset: 0x0003ADDD
			public DateTime ReadTimeUtc { get; internal set; }

			// Token: 0x170003A3 RID: 931
			// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0003CBE6 File Offset: 0x0003ADE6
			public override long ItemSize
			{
				get
				{
					return 1L;
				}
			}

			// Token: 0x06000EF6 RID: 3830 RVA: 0x0003CBEC File Offset: 0x0003ADEC
			public IPeopleConnectApplicationConfig OverrideWith(IPeopleConnectApplicationConfig other)
			{
				if (other == null)
				{
					return this;
				}
				if (this.Equals(other))
				{
					return this;
				}
				return new CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig
				{
					AccessTokenEndpoint = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith(this.AccessTokenEndpoint, other.AccessTokenEndpoint),
					AppId = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith(this.AppId, other.AppId),
					AppSecretEncrypted = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith<Func<string, string>>(Tuple.Create<string, Func<string, string>>(this.AppSecretEncrypted, this.DecryptAppSecret), Tuple.Create<string, Func<string, string>>(other.AppSecretEncrypted, other.DecryptAppSecret)).Item1,
					DecryptAppSecret = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith<Func<string, string>>(Tuple.Create<string, Func<string, string>>(this.AppSecretEncrypted, this.DecryptAppSecret), Tuple.Create<string, Func<string, string>>(other.AppSecretEncrypted, other.DecryptAppSecret)).Item2,
					AuthorizationEndpoint = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith(this.AuthorizationEndpoint, other.AuthorizationEndpoint),
					ProfileEndpoint = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith(this.ProfileEndpoint, other.ProfileEndpoint),
					ConnectionsEndpoint = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith(this.ConnectionsEndpoint, other.ConnectionsEndpoint),
					RemoveAppEndpoint = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith(this.RemoveAppEndpoint, other.RemoveAppEndpoint),
					ConsentRedirectEndpoint = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith(this.ConsentRedirectEndpoint, other.ConsentRedirectEndpoint),
					GraphApiEndpoint = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith(this.GraphApiEndpoint, other.GraphApiEndpoint),
					GraphTokenEndpoint = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith(this.GraphTokenEndpoint, other.GraphTokenEndpoint),
					RequestTokenEndpoint = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith(this.RequestTokenEndpoint, other.RequestTokenEndpoint),
					WebProxyUri = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith(this.WebProxyUri, other.WebProxyUri),
					WebRequestTimeout = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith(this.WebRequestTimeout, other.WebRequestTimeout),
					ReadTimeUtc = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith(this.ReadTimeUtc, other.ReadTimeUtc),
					SkipContactUpload = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith<bool>(new bool?(this.SkipContactUpload), new bool?(other.SkipContactUpload)),
					ContinueOnContactUploadFailure = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith<bool>(new bool?(this.ContinueOnContactUploadFailure), new bool?(other.ContinueOnContactUploadFailure)),
					WaitForContactUploadCommit = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith<bool>(new bool?(this.WaitForContactUploadCommit), new bool?(other.WaitForContactUploadCommit)),
					NotifyOnEachContactUpload = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith<bool>(new bool?(this.NotifyOnEachContactUpload), new bool?(other.NotifyOnEachContactUpload)),
					MaximumContactsToUpload = CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig.OverrideWith<int>(new int?(this.MaximumContactsToUpload), new int?(other.MaximumContactsToUpload))
				};
			}

			// Token: 0x06000EF7 RID: 3831 RVA: 0x0003CE44 File Offset: 0x0003B044
			private static string OverrideWith(string first, string second)
			{
				if (!string.IsNullOrWhiteSpace(second))
				{
					return second;
				}
				return first;
			}

			// Token: 0x06000EF8 RID: 3832 RVA: 0x0003CE54 File Offset: 0x0003B054
			private static T OverrideWith<T>(T? first, T? second) where T : struct
			{
				if (second != null)
				{
					return second.Value;
				}
				T? t = first;
				if (t == null)
				{
					return default(T);
				}
				return t.GetValueOrDefault();
			}

			// Token: 0x06000EF9 RID: 3833 RVA: 0x0003CE90 File Offset: 0x0003B090
			private static TimeSpan OverrideWith(TimeSpan first, TimeSpan second)
			{
				if (!TimeSpan.Zero.Equals(second))
				{
					return second;
				}
				return first;
			}

			// Token: 0x06000EFA RID: 3834 RVA: 0x0003CEB0 File Offset: 0x0003B0B0
			private static DateTime OverrideWith(DateTime first, DateTime second)
			{
				if (!DateTime.MinValue.Equals(second))
				{
					return second;
				}
				return first;
			}

			// Token: 0x06000EFB RID: 3835 RVA: 0x0003CED0 File Offset: 0x0003B0D0
			private static Tuple<string, T> OverrideWith<T>(Tuple<string, T> first, Tuple<string, T> second)
			{
				if (!string.IsNullOrWhiteSpace(second.Item1))
				{
					return second;
				}
				return first;
			}
		}

		// Token: 0x0200017B RID: 379
		private sealed class PeopleConnectApplicationConfigCacheImpl : IPeopleConnectApplicationConfigCache
		{
			// Token: 0x06000EFF RID: 3839 RVA: 0x0003CEEC File Offset: 0x0003B0EC
			public bool TryGetValue(string key, out IPeopleConnectApplicationConfig value)
			{
				CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig peopleConnectApplicationConfig;
				bool result = this.cache.TryGetValue(key, out peopleConnectApplicationConfig);
				value = peopleConnectApplicationConfig;
				return result;
			}

			// Token: 0x06000F00 RID: 3840 RVA: 0x0003CF0C File Offset: 0x0003B10C
			public void Add(string key, IPeopleConnectApplicationConfig value)
			{
				this.cache.Add(key, (CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig)value);
			}

			// Token: 0x040007F4 RID: 2036
			private readonly Cache<string, CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig> cache = new Cache<string, CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfig>(64L, TimeSpan.FromHours(8.0), TimeSpan.Zero);
		}

		// Token: 0x0200017D RID: 381
		private sealed class PeopleConnectApplicationConfigADReaderImpl : IPeopleConnectApplicationConfigADReader
		{
			// Token: 0x06000F09 RID: 3849 RVA: 0x0003CF49 File Offset: 0x0003B149
			public AuthServer ReadFacebookAuthServer()
			{
				return OAuthConfigHelper.GetFacebookAuthServer();
			}

			// Token: 0x06000F0A RID: 3850 RVA: 0x0003CF50 File Offset: 0x0003B150
			public AuthServer ReadLinkedInAuthServer()
			{
				return OAuthConfigHelper.GetLinkedInAuthServer();
			}

			// Token: 0x06000F0B RID: 3851 RVA: 0x0003CF58 File Offset: 0x0003B158
			public string ReadWebProxyUri()
			{
				Server localServer = LocalServerCache.LocalServer;
				if (localServer == null)
				{
					CachedPeopleConnectApplicationConfig.Tracer.TraceError(0L, "Could not read proxy configuration from server object because there was a problem.");
					throw new ExchangeConfigurationException(Strings.FailedToReadWebProxyConfigurationFromAD);
				}
				if (!(localServer.InternetWebProxy != null))
				{
					return string.Empty;
				}
				return localServer.InternetWebProxy.AbsoluteUri;
			}

			// Token: 0x06000F0C RID: 3852 RVA: 0x0003CFA9 File Offset: 0x0003B1A9
			public string ReadFacebookGraphApiEndpoint()
			{
				return CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfigADReaderImpl.GetEndpoint(ServiceEndpointId.FacebookGraphApi);
			}

			// Token: 0x06000F0D RID: 3853 RVA: 0x0003CFB5 File Offset: 0x0003B1B5
			public string ReadLinkedInProfileEndpoint()
			{
				return CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfigADReaderImpl.GetEndpoint(ServiceEndpointId.LinkedInProfile);
			}

			// Token: 0x06000F0E RID: 3854 RVA: 0x0003CFC1 File Offset: 0x0003B1C1
			public string ReadLinkedInConnectionsEndpoint()
			{
				return CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfigADReaderImpl.GetEndpoint(ServiceEndpointId.LinkedInConnections);
			}

			// Token: 0x06000F0F RID: 3855 RVA: 0x0003CFCD File Offset: 0x0003B1CD
			public string ReadLinkedInInvalidateTokenEndpoint()
			{
				return CachedPeopleConnectApplicationConfig.PeopleConnectApplicationConfigADReaderImpl.GetEndpoint(ServiceEndpointId.LinkedInInvalidateToken);
			}

			// Token: 0x06000F10 RID: 3856 RVA: 0x0003CFDC File Offset: 0x0003B1DC
			private static string GetEndpoint(string endpointCommonName)
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 859, "GetEndpoint", "f:\\15.00.1497\\sources\\dev\\data\\src\\ApplicationLogic\\PeopleConnect\\CachedPeopleConnectApplicationConfig.cs");
				Uri uri = topologyConfigurationSession.GetEndpointContainer().GetEndpoint(endpointCommonName).Uri;
				if (!(uri != null))
				{
					return string.Empty;
				}
				return uri.ToString();
			}
		}
	}
}
