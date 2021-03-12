using System;
using System.Collections.Generic;
using System.Security;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000DE RID: 222
	internal sealed class WnsChannelSettings : PushNotificationSettingsBase
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x000170F4 File Offset: 0x000152F4
		public WnsChannelSettings(string appId, string appSid, string appSecret, bool isAppSecretEncrypted, string authenticationUri, int requestTimeout, int requestStepTimeout, int authenticateRetryDelay, int authenticateRetryMax, int backOffTimeInSeconds) : base(appId)
		{
			this.AppSid = appSid;
			this.appSecretString = appSecret;
			this.authenticationUriString = authenticationUri;
			this.isAppSecretEncrypted = isAppSecretEncrypted;
			this.RequestTimeout = requestTimeout;
			this.RequestStepTimeout = requestStepTimeout;
			this.AuthenticateRetryDelay = authenticateRetryDelay;
			this.AuthenticateRetryMax = authenticateRetryMax;
			this.BackOffTimeInSeconds = backOffTimeInSeconds;
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x0001714E File Offset: 0x0001534E
		// (set) Token: 0x06000749 RID: 1865 RVA: 0x00017156 File Offset: 0x00015356
		public string AppSid { get; private set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001715F File Offset: 0x0001535F
		public SecureString AppSecret
		{
			get
			{
				if (!base.IsSuitable)
				{
					throw new InvalidOperationException("AppSecret can only be accessed if the instance is suitable");
				}
				return this.appSecret;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001717A File Offset: 0x0001537A
		public Uri AuthenticationUri
		{
			get
			{
				if (!base.IsValid)
				{
					throw new InvalidOperationException("AuthenticationUri can only be accessed if the instance is valid");
				}
				return this.authenticationUri;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x00017195 File Offset: 0x00015395
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x0001719D File Offset: 0x0001539D
		public int RequestTimeout { get; private set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x000171A6 File Offset: 0x000153A6
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x000171AE File Offset: 0x000153AE
		public int RequestStepTimeout { get; private set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x000171B7 File Offset: 0x000153B7
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x000171BF File Offset: 0x000153BF
		public int AuthenticateRetryDelay { get; private set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x000171C8 File Offset: 0x000153C8
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x000171D0 File Offset: 0x000153D0
		public int AuthenticateRetryMax { get; private set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x000171D9 File Offset: 0x000153D9
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x000171E1 File Offset: 0x000153E1
		public int BackOffTimeInSeconds { get; private set; }

		// Token: 0x06000756 RID: 1878 RVA: 0x000171EC File Offset: 0x000153EC
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if (string.IsNullOrWhiteSpace(this.AppSid))
			{
				errors.Add(Strings.ValidationErrorEmptyString("AppSid"));
			}
			if (string.IsNullOrWhiteSpace(this.authenticationUriString))
			{
				errors.Add(Strings.ValidationErrorEmptyString("AuthenticationUri"));
			}
			else
			{
				try
				{
					this.authenticationUri = new Uri(this.authenticationUriString, UriKind.Absolute);
				}
				catch (UriFormatException ex)
				{
					errors.Add(Strings.ValidationErrorInvalidUri("AuthenticationUri", this.authenticationUriString, ex.Message));
				}
			}
			if (this.RequestTimeout < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("RequestTimeout", this.RequestTimeout));
			}
			if (this.RequestStepTimeout < 0 || this.RequestStepTimeout > this.RequestTimeout)
			{
				errors.Add(Strings.ValidationErrorRangeInteger("RequestStepTimeout", 0, this.RequestTimeout, this.RequestStepTimeout));
			}
			if (this.AuthenticateRetryDelay < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("AuthenticateRetryDelay", this.AuthenticateRetryDelay));
			}
			if (this.AuthenticateRetryMax < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("AuthenticateRetryMax", this.AuthenticateRetryMax));
			}
			if (this.BackOffTimeInSeconds < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("BackOffTimeInSeconds", this.BackOffTimeInSeconds));
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00017330 File Offset: 0x00015530
		protected override bool RunSuitabilityCheck()
		{
			bool result = base.RunSuitabilityCheck();
			try
			{
				this.appSecret = (this.isAppSecretEncrypted ? PushNotificationDataProtector.Default.Decrypt(this.appSecretString) : this.appSecretString.AsSecureString());
				if (this.appSecret == null)
				{
					throw new PushNotificationConfigurationException(Strings.ValidationErrorEmptyString("AppSecret"));
				}
			}
			catch (PushNotificationConfigurationException exception)
			{
				string text = exception.ToTraceString();
				PushNotificationsCrimsonEvents.PushNotificationPublisherConfigurationError.Log<string, string, string>(base.AppId, string.Empty, text);
				ExTraceGlobals.PublisherManagerTracer.TraceError<string, string>((long)this.GetHashCode(), "[WnsChannelSettings:RunSuitabilityCheck] Channel configuration for '{0}' has suitability errors: {1}", base.AppId, text);
				result = false;
			}
			return result;
		}

		// Token: 0x040003A3 RID: 931
		public const string DefaultAuthenticationUri = "https://login.live.com/accesstoken.srf";

		// Token: 0x040003A4 RID: 932
		public const bool DefaultIsAppSecretEncrypted = true;

		// Token: 0x040003A5 RID: 933
		public const int DefaultRequestTimeout = 60000;

		// Token: 0x040003A6 RID: 934
		public const int DefaultRequestStepTimeout = 500;

		// Token: 0x040003A7 RID: 935
		public const int DefaultAuthenticateRetryDelay = 1500;

		// Token: 0x040003A8 RID: 936
		public const int DefaultAuthenticateRetryMax = 2;

		// Token: 0x040003A9 RID: 937
		public const int DefaultBackOffTimeInSeconds = 600;

		// Token: 0x040003AA RID: 938
		private readonly string authenticationUriString;

		// Token: 0x040003AB RID: 939
		private readonly string appSecretString;

		// Token: 0x040003AC RID: 940
		private readonly bool isAppSecretEncrypted;

		// Token: 0x040003AD RID: 941
		private Uri authenticationUri;

		// Token: 0x040003AE RID: 942
		private SecureString appSecret;
	}
}
