using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000043 RID: 67
	internal sealed class AzureChannelSettings : PushNotificationSettingsBase
	{
		// Token: 0x06000284 RID: 644 RVA: 0x000098D8 File Offset: 0x00007AD8
		public AzureChannelSettings(string appId, string serviceUriTemplate, string azureKeyName, string azureKey, bool isAzureKeyEncrypted, string registrationTemplate, bool isRegistrationEnabled, string partitionName, int requestTimeout, int requestStepTimeout, int maxDevicesRegisteredCacheSize, int backOffTimeInSeconds) : base(appId)
		{
			this.serviceUriTemplate = serviceUriTemplate;
			this.azureKeyName = azureKeyName;
			this.azureStringKey = azureKey;
			this.isAzureKeyEncrypted = isAzureKeyEncrypted;
			this.RegistrationTemplate = registrationTemplate;
			this.IsRegistrationEnabled = isRegistrationEnabled;
			this.PartitionName = partitionName;
			this.RequestTimeout = requestTimeout;
			this.RequestStepTimeout = requestStepTimeout;
			this.MaxDevicesRegistrationCacheSize = maxDevicesRegisteredCacheSize;
			this.BackOffTimeInSeconds = backOffTimeInSeconds;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00009942 File Offset: 0x00007B42
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

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000995D File Offset: 0x00007B5D
		public IAzureSasTokenProvider AzureSasTokenProvider
		{
			get
			{
				if (!base.IsSuitable)
				{
					throw new InvalidOperationException("AzureKey can only be accessed if the instance is suitable");
				}
				return this.azureTokenProvider;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00009978 File Offset: 0x00007B78
		// (set) Token: 0x06000288 RID: 648 RVA: 0x00009980 File Offset: 0x00007B80
		public string RegistrationTemplate { get; private set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00009989 File Offset: 0x00007B89
		// (set) Token: 0x0600028A RID: 650 RVA: 0x00009991 File Offset: 0x00007B91
		public bool IsRegistrationEnabled { get; private set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000999A File Offset: 0x00007B9A
		// (set) Token: 0x0600028C RID: 652 RVA: 0x000099A2 File Offset: 0x00007BA2
		public string PartitionName { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600028D RID: 653 RVA: 0x000099AB File Offset: 0x00007BAB
		// (set) Token: 0x0600028E RID: 654 RVA: 0x000099B3 File Offset: 0x00007BB3
		public int RequestTimeout { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600028F RID: 655 RVA: 0x000099BC File Offset: 0x00007BBC
		// (set) Token: 0x06000290 RID: 656 RVA: 0x000099C4 File Offset: 0x00007BC4
		public int RequestStepTimeout { get; private set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000291 RID: 657 RVA: 0x000099CD File Offset: 0x00007BCD
		// (set) Token: 0x06000292 RID: 658 RVA: 0x000099D5 File Offset: 0x00007BD5
		public int MaxDevicesRegistrationCacheSize { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000099DE File Offset: 0x00007BDE
		// (set) Token: 0x06000294 RID: 660 RVA: 0x000099E6 File Offset: 0x00007BE6
		public int BackOffTimeInSeconds { get; private set; }

		// Token: 0x06000295 RID: 661 RVA: 0x000099F0 File Offset: 0x00007BF0
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			if (string.IsNullOrWhiteSpace(this.serviceUriTemplate))
			{
				errors.Add(Strings.ValidationErrorEmptyString("ServiceUriTemplate"));
			}
			else
			{
				try
				{
					this.uriTemplate = AzureUriTemplate.CreateUriTemplate(this.serviceUriTemplate, this.PartitionName);
				}
				catch (ArgumentException ex)
				{
					errors.Add(Strings.ValidationErrorInvalidUri("ServiceUriTemplate", this.serviceUriTemplate, ex.Message));
				}
			}
			if (string.IsNullOrWhiteSpace(this.azureKeyName))
			{
				errors.Add(Strings.ValidationErrorEmptyString("AzureKeyName"));
			}
			if (string.IsNullOrWhiteSpace(this.azureStringKey))
			{
				errors.Add(Strings.ValidationErrorEmptyString("AzureKeyValue"));
			}
			if (this.IsRegistrationEnabled && string.IsNullOrWhiteSpace(this.RegistrationTemplate))
			{
				errors.Add(Strings.ValidationErrorEmptyString("RegistrationTemplate"));
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

		// Token: 0x06000296 RID: 662 RVA: 0x00009B38 File Offset: 0x00007D38
		protected override bool RunSuitabilityCheck()
		{
			bool result = base.RunSuitabilityCheck();
			try
			{
				SecureString secureString = this.isAzureKeyEncrypted ? PushNotificationDataProtector.Default.Decrypt(this.azureStringKey) : this.azureStringKey.AsSecureString();
				if (secureString == null)
				{
					throw new PushNotificationConfigurationException(Strings.ValidationErrorEmptyString("AzureKey"));
				}
				if (secureString.AsUnsecureString().Contains("{"))
				{
					try
					{
						AzureSasToken azureSasToken = JsonConverter.Deserialize<AzureSasToken>(secureString.AsUnsecureString(), null);
						if (azureSasToken == null || !azureSasToken.IsValid())
						{
							throw new PushNotificationConfigurationException(Strings.ValidationErrorInvalidSasToken(azureSasToken.ToNullableString(null)));
						}
						this.azureTokenProvider = azureSasToken;
						goto IL_AD;
					}
					catch (SerializationException)
					{
						throw new PushNotificationConfigurationException(Strings.ValidationErrorInvalidAuthenticationKey);
					}
				}
				this.azureTokenProvider = new AzureSasKey(this.azureKeyName, secureString, null);
				IL_AD:;
			}
			catch (PushNotificationConfigurationException exception)
			{
				string text = exception.ToTraceString();
				PushNotificationsCrimsonEvents.PushNotificationPublisherConfigurationError.Log<string, string, string>(base.AppId, string.Empty, text);
				ExTraceGlobals.PublisherManagerTracer.TraceError<string, string>((long)this.GetHashCode(), "[AzureChannelSettings:RunSuitabilityCheck] Channel configuration for '{0}' has suitability errors: {1}", base.AppId, text);
				result = false;
			}
			return result;
		}

		// Token: 0x0400010D RID: 269
		public const int DefaultRequestTimeout = 60000;

		// Token: 0x0400010E RID: 270
		public const int DefaultRequestStepTimeout = 500;

		// Token: 0x0400010F RID: 271
		public const int DefaultBackOffTimeInSeconds = 600;

		// Token: 0x04000110 RID: 272
		public const bool DefaultIsSasKeyEncrypted = true;

		// Token: 0x04000111 RID: 273
		public const bool DefaultIsRegistrationEnabled = false;

		// Token: 0x04000112 RID: 274
		public const string DefaultUriTemplate = "https://{0}-{1}.servicebus.windows.net/exo/{2}/{3}";

		// Token: 0x04000113 RID: 275
		public const int DefaultDevicesRegistrationCacheSize = 10000;

		// Token: 0x04000114 RID: 276
		public const bool DefaultIsDefaultPartitionName = false;

		// Token: 0x04000115 RID: 277
		private readonly string serviceUriTemplate;

		// Token: 0x04000116 RID: 278
		private readonly string azureKeyName;

		// Token: 0x04000117 RID: 279
		private readonly string azureStringKey;

		// Token: 0x04000118 RID: 280
		private readonly bool isAzureKeyEncrypted;

		// Token: 0x04000119 RID: 281
		private AzureUriTemplate uriTemplate;

		// Token: 0x0400011A RID: 282
		private IAzureSasTokenProvider azureTokenProvider;
	}
}
