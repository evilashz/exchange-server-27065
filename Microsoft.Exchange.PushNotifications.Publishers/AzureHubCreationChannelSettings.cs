using System;
using System.Collections.Generic;
using System.Security;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200007F RID: 127
	internal sealed class AzureHubCreationChannelSettings : PushNotificationSettingsBase
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x0000EF80 File Offset: 0x0000D180
		public AzureHubCreationChannelSettings(string appId, string serviceAcsUriTemplate, string serviceScopeUriTemplate, string serviceUriTemplate, string acsUserName, string acsUserPassword, bool isAzureKeyEncrypted, int requestTimeout, int requestStepTimeout, int authenticateRetryDelay, int maxHubCacheSize, int backOffTimeInSeconds) : base(appId)
		{
			this.acsServiceUriTemplate = serviceAcsUriTemplate;
			this.serviceScopeUriTemplate = serviceScopeUriTemplate;
			this.serviceUriTemplate = serviceUriTemplate;
			this.AcsUserName = acsUserName;
			this.acsUserPasswordString = acsUserPassword;
			this.isPasswordEncrypted = isAzureKeyEncrypted;
			this.RequestTimeout = requestTimeout;
			this.RequestStepTimeout = requestStepTimeout;
			this.AuthenticateRetryDelay = authenticateRetryDelay;
			this.MaxHubCreatedCacheSize = maxHubCacheSize;
			this.BackOffTimeInSeconds = backOffTimeInSeconds;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000EFEA File Offset: 0x0000D1EA
		public AcsUriTemplate AcsUriTemplate
		{
			get
			{
				if (!base.IsSuitable)
				{
					throw new InvalidOperationException("UriTemplate can only be accessed if the instance is suitable");
				}
				return this.acsUriTemplate;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000F005 File Offset: 0x0000D205
		public AzureUriTemplate UriTemplate
		{
			get
			{
				if (!base.IsSuitable)
				{
					throw new InvalidOperationException("UriTemplate can only be accessed if the instance is suitable");
				}
				return this.uriTemplate;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000F020 File Offset: 0x0000D220
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x0000F028 File Offset: 0x0000D228
		public string AcsUserName { get; private set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0000F031 File Offset: 0x0000D231
		public SecureString AcsUserPassword
		{
			get
			{
				if (!base.IsSuitable)
				{
					throw new InvalidOperationException("AcsUserPassword can only be accessed if the instance is suitable");
				}
				return this.acsUserPassword;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000F04C File Offset: 0x0000D24C
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x0000F054 File Offset: 0x0000D254
		public int RequestTimeout { get; private set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000F05D File Offset: 0x0000D25D
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0000F065 File Offset: 0x0000D265
		public int RequestStepTimeout { get; private set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000F06E File Offset: 0x0000D26E
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0000F076 File Offset: 0x0000D276
		public int AuthenticateRetryDelay { get; private set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000F07F File Offset: 0x0000D27F
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0000F087 File Offset: 0x0000D287
		public int BackOffTimeInSeconds { get; private set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000F090 File Offset: 0x0000D290
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0000F098 File Offset: 0x0000D298
		public int MaxHubCreatedCacheSize { get; private set; }

		// Token: 0x06000485 RID: 1157 RVA: 0x0000F0A4 File Offset: 0x0000D2A4
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			if (!PushNotificationCannedApp.AzureHubCreation.Name.Equals(base.AppId))
			{
				errors.Add(Strings.ValidationErrorHubCreationAppId(base.AppId, PushNotificationCannedApp.AzureHubCreation.Name));
			}
			if (string.IsNullOrWhiteSpace(this.acsServiceUriTemplate))
			{
				errors.Add(Strings.ValidationErrorEmptyString("AcsServiceUriTemplate"));
			}
			else if (string.IsNullOrWhiteSpace(this.serviceScopeUriTemplate))
			{
				errors.Add(Strings.ValidationErrorEmptyString("AcsServiceScopeUriTemplate"));
			}
			else
			{
				try
				{
					this.acsUriTemplate = new AcsUriTemplate(this.acsServiceUriTemplate, this.serviceScopeUriTemplate);
				}
				catch (ArgumentException ex)
				{
					errors.Add(Strings.ValidationErrorInvalidUri("ServiceAcsUriTemplate", this.acsServiceUriTemplate, ex.Message));
				}
			}
			if (string.IsNullOrWhiteSpace(this.serviceUriTemplate))
			{
				errors.Add(Strings.ValidationErrorEmptyString("ServiceUriTemplate"));
			}
			else
			{
				try
				{
					this.uriTemplate = AzureUriTemplate.CreateUriTemplate(this.serviceUriTemplate, null);
				}
				catch (ArgumentException ex2)
				{
					errors.Add(Strings.ValidationErrorInvalidUri("ServiceUriTemplate", this.serviceUriTemplate, ex2.Message));
				}
			}
			if (string.IsNullOrWhiteSpace(this.AcsUserName))
			{
				errors.Add(Strings.ValidationErrorEmptyString("AcsKeyName"));
			}
			if (string.IsNullOrWhiteSpace(this.acsUserPasswordString))
			{
				errors.Add(Strings.ValidationErrorEmptyString("AcsKeyValue"));
			}
			if (this.RequestTimeout < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("RequestTimeout", this.RequestTimeout));
			}
			if (this.RequestStepTimeout < 0 || this.RequestStepTimeout > this.RequestTimeout)
			{
				errors.Add(Strings.ValidationErrorRangeInteger("RequestStepTimeout", 0, this.RequestTimeout, this.RequestStepTimeout));
			}
			if (this.BackOffTimeInSeconds < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("BackOffTimeInSeconds", this.BackOffTimeInSeconds));
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000F274 File Offset: 0x0000D474
		protected override bool RunSuitabilityCheck()
		{
			bool result = base.RunSuitabilityCheck();
			try
			{
				SecureString secureString = this.isPasswordEncrypted ? PushNotificationDataProtector.Default.Decrypt(this.acsUserPasswordString) : this.acsUserPasswordString.AsSecureString();
				if (secureString == null)
				{
					throw new PushNotificationConfigurationException(Strings.ValidationErrorEmptyString("AcsUserPassword"));
				}
				this.acsUserPassword = secureString;
			}
			catch (PushNotificationConfigurationException exception)
			{
				string text = exception.ToTraceString();
				PushNotificationsCrimsonEvents.PushNotificationPublisherConfigurationError.Log<string, string, string>(base.AppId, string.Empty, text);
				ExTraceGlobals.PublisherManagerTracer.TraceError<string, string>((long)this.GetHashCode(), "[AzureHubCreationChannelSettings:RunSuitabilityCheck] Channel configuration for '{0}' has suitability errors: {1}", base.AppId, text);
				result = false;
			}
			return result;
		}

		// Token: 0x0400021F RID: 543
		public const int DefaultRequestTimeout = 60000;

		// Token: 0x04000220 RID: 544
		public const int DefaultRequestStepTimeout = 500;

		// Token: 0x04000221 RID: 545
		public const int DefaultBackOffTimeInSeconds = 600;

		// Token: 0x04000222 RID: 546
		public const int DefaultHubCacheSize = 10000;

		// Token: 0x04000223 RID: 547
		public const int DefaultAuthenticateRetryDelay = 1500;

		// Token: 0x04000224 RID: 548
		public const bool DefaultIsAcsPasswordEncrypted = true;

		// Token: 0x04000225 RID: 549
		public const string DefaultUriTemplate = "https://{0}-{1}.servicebus.windows.net/exo/{2}{3}";

		// Token: 0x04000226 RID: 550
		public const string DefaultAcsUriTemplate = "https://{0}-{1}-sb.accesscontrol.windows.net/";

		// Token: 0x04000227 RID: 551
		public const string DefaultAcsScopeUriTemplate = "http://{0}-{1}.servicebus.windows.net/exo/";

		// Token: 0x04000228 RID: 552
		private readonly string acsServiceUriTemplate;

		// Token: 0x04000229 RID: 553
		private readonly string serviceScopeUriTemplate;

		// Token: 0x0400022A RID: 554
		private readonly string serviceUriTemplate;

		// Token: 0x0400022B RID: 555
		private readonly string acsUserPasswordString;

		// Token: 0x0400022C RID: 556
		private readonly bool isPasswordEncrypted;

		// Token: 0x0400022D RID: 557
		private AcsUriTemplate acsUriTemplate;

		// Token: 0x0400022E RID: 558
		private AzureUriTemplate uriTemplate;

		// Token: 0x0400022F RID: 559
		private SecureString acsUserPassword;
	}
}
