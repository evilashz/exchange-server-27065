using System;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200054B RID: 1355
	internal class PushNotificationAppSchema : ADConfigurationObjectSchema
	{
		// Token: 0x06003C8F RID: 15503 RVA: 0x000E7718 File Offset: 0x000E5918
		private static ADPropertyDefinition CreateXmlProperty<T>(string name, Func<PushNotificationAppConfigXml, T?> getter, Action<PushNotificationAppConfigXml, T?> setter) where T : struct
		{
			return XMLSerializableBase.ConfigXmlProperty<PushNotificationAppConfigXml, T?>(name, ExchangeObjectVersion.Exchange2012, PushNotificationAppSchema.AppSettings, null, getter, setter, null, null);
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x000E7744 File Offset: 0x000E5944
		private static ADPropertyDefinition CreateXmlProperty<T>(string name, Func<PushNotificationAppConfigXml, T> getter, Action<PushNotificationAppConfigXml, T> setter) where T : class
		{
			return XMLSerializableBase.ConfigXmlProperty<PushNotificationAppConfigXml, T>(name, ExchangeObjectVersion.Exchange2012, PushNotificationAppSchema.AppSettings, default(T), getter, setter, null, null);
		}

		// Token: 0x040028F1 RID: 10481
		public static readonly ADPropertyDefinition DisplayName = SharedPropertyDefinitions.OptionalDisplayName;

		// Token: 0x040028F2 RID: 10482
		public static readonly ADPropertyDefinition Platform = new ADPropertyDefinition("Platform", ExchangeObjectVersion.Exchange2012, typeof(PushNotificationPlatform), "msExchProvisioningFlags", ADPropertyDefinitionFlags.None, PushNotificationPlatform.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040028F3 RID: 10483
		public static readonly ADPropertyDefinition RawAppSettingsXml = XMLSerializableBase.ConfigurationXmlRawProperty();

		// Token: 0x040028F4 RID: 10484
		public static readonly ADPropertyDefinition AppSettings = XMLSerializableBase.ConfigurationXmlProperty<PushNotificationAppConfigXml>(PushNotificationAppSchema.RawAppSettingsXml);

		// Token: 0x040028F5 RID: 10485
		public static readonly ADPropertyDefinition Enabled = PushNotificationAppSchema.CreateXmlProperty<bool>("Enabled", (PushNotificationAppConfigXml configXml) => configXml.Enabled, delegate(PushNotificationAppConfigXml configXml, bool? value)
		{
			configXml.Enabled = value;
		});

		// Token: 0x040028F6 RID: 10486
		public static readonly ADPropertyDefinition ExchangeMaximumVersion = PushNotificationAppSchema.CreateXmlProperty<Version>("ExchangeMaximumVersion", delegate(PushNotificationAppConfigXml configXml)
		{
			if (configXml.ExchangeMaximumVersion != null)
			{
				return new Version(configXml.ExchangeMaximumVersion);
			}
			return null;
		}, delegate(PushNotificationAppConfigXml configXml, Version value)
		{
			configXml.ExchangeMaximumVersion = ((value == null) ? null : value.ToString());
		});

		// Token: 0x040028F7 RID: 10487
		public static readonly ADPropertyDefinition ExchangeMinimumVersion = PushNotificationAppSchema.CreateXmlProperty<Version>("ExchangeMinimumVersion", delegate(PushNotificationAppConfigXml configXml)
		{
			if (configXml.ExchangeMinimumVersion != null)
			{
				return new Version(configXml.ExchangeMinimumVersion);
			}
			return null;
		}, delegate(PushNotificationAppConfigXml configXml, Version value)
		{
			configXml.ExchangeMinimumVersion = ((value == null) ? null : value.ToString());
		});

		// Token: 0x040028F8 RID: 10488
		public static readonly ADPropertyDefinition QueueSize = PushNotificationAppSchema.CreateXmlProperty<int>("QueueSize", (PushNotificationAppConfigXml configXml) => configXml.QueueSize, delegate(PushNotificationAppConfigXml configXml, int? value)
		{
			configXml.QueueSize = value;
		});

		// Token: 0x040028F9 RID: 10489
		public static readonly ADPropertyDefinition NumberOfChannels = PushNotificationAppSchema.CreateXmlProperty<int>("NumberOfChannels", (PushNotificationAppConfigXml configXml) => configXml.NumberOfChannels, delegate(PushNotificationAppConfigXml configXml, int? value)
		{
			configXml.NumberOfChannels = value;
		});

		// Token: 0x040028FA RID: 10490
		public static readonly ADPropertyDefinition BackOffTimeInSeconds = PushNotificationAppSchema.CreateXmlProperty<int>("BackOffTimeInSeconds", (PushNotificationAppConfigXml configXml) => configXml.BackOffTimeInSeconds, delegate(PushNotificationAppConfigXml configXml, int? value)
		{
			configXml.BackOffTimeInSeconds = value;
		});

		// Token: 0x040028FB RID: 10491
		public static readonly ADPropertyDefinition AuthenticationId = PushNotificationAppSchema.CreateXmlProperty<string>("AuthenticationId", (PushNotificationAppConfigXml configXml) => configXml.AuthId, delegate(PushNotificationAppConfigXml configXml, string value)
		{
			configXml.AuthId = value;
		});

		// Token: 0x040028FC RID: 10492
		public static readonly ADPropertyDefinition AuthenticationKey = PushNotificationAppSchema.CreateXmlProperty<string>("AuthenticationKey", (PushNotificationAppConfigXml configXml) => configXml.AuthKey, delegate(PushNotificationAppConfigXml configXml, string value)
		{
			configXml.AuthKey = value;
		});

		// Token: 0x040028FD RID: 10493
		public static readonly ADPropertyDefinition AuthenticationKeyFallback = PushNotificationAppSchema.CreateXmlProperty<string>("AuthenticationKey", (PushNotificationAppConfigXml configXml) => configXml.AuthKeyFallback, delegate(PushNotificationAppConfigXml configXml, string value)
		{
			configXml.AuthKeyFallback = value;
		});

		// Token: 0x040028FE RID: 10494
		public static readonly ADPropertyDefinition IsAuthenticationKeyEncrypted = PushNotificationAppSchema.CreateXmlProperty<bool>("IsAuthenticationKeyEncrypted", (PushNotificationAppConfigXml configXml) => configXml.IsAuthKeyEncrypted, delegate(PushNotificationAppConfigXml configXml, bool? value)
		{
			configXml.IsAuthKeyEncrypted = value;
		});

		// Token: 0x040028FF RID: 10495
		public static readonly ADPropertyDefinition Url = PushNotificationAppSchema.CreateXmlProperty<string>("Url", (PushNotificationAppConfigXml configXml) => configXml.Url, delegate(PushNotificationAppConfigXml configXml, string value)
		{
			configXml.Url = value;
		});

		// Token: 0x04002900 RID: 10496
		public static readonly ADPropertyDefinition Port = PushNotificationAppSchema.CreateXmlProperty<int>("Port", (PushNotificationAppConfigXml configXml) => configXml.Port, delegate(PushNotificationAppConfigXml configXml, int? value)
		{
			configXml.Port = value;
		});

		// Token: 0x04002901 RID: 10497
		public static readonly ADPropertyDefinition SecondaryUrl = PushNotificationAppSchema.CreateXmlProperty<string>("SecondaryUrl", (PushNotificationAppConfigXml configXml) => configXml.SecondaryUrl, delegate(PushNotificationAppConfigXml configXml, string value)
		{
			configXml.SecondaryUrl = value;
		});

		// Token: 0x04002902 RID: 10498
		public static readonly ADPropertyDefinition SecondaryPort = PushNotificationAppSchema.CreateXmlProperty<int>("SecondaryPort", (PushNotificationAppConfigXml configXml) => configXml.SecondaryPort, delegate(PushNotificationAppConfigXml configXml, int? value)
		{
			configXml.SecondaryPort = value;
		});

		// Token: 0x04002903 RID: 10499
		public static readonly ADPropertyDefinition UriTemplate = PushNotificationAppSchema.CreateXmlProperty<string>("UriTemplate", (PushNotificationAppConfigXml configXml) => configXml.UriTemplate, delegate(PushNotificationAppConfigXml configXml, string value)
		{
			configXml.UriTemplate = value;
		});

		// Token: 0x04002904 RID: 10500
		public static readonly ADPropertyDefinition RegistrationTemplate = PushNotificationAppSchema.CreateXmlProperty<string>("RegistrationTemplate", (PushNotificationAppConfigXml configXml) => configXml.RegistrationTemplate, delegate(PushNotificationAppConfigXml configXml, string value)
		{
			configXml.RegistrationTemplate = value;
		});

		// Token: 0x04002905 RID: 10501
		public static readonly ADPropertyDefinition RegistrationEnabled = PushNotificationAppSchema.CreateXmlProperty<bool>("RegistrationEnabled", (PushNotificationAppConfigXml configXml) => configXml.RegistrationEnabled, delegate(PushNotificationAppConfigXml configXml, bool? value)
		{
			configXml.RegistrationEnabled = value;
		});

		// Token: 0x04002906 RID: 10502
		public static readonly ADPropertyDefinition MultifactorRegistrationEnabled = PushNotificationAppSchema.CreateXmlProperty<bool>("MultifactorRegistrationEnabled", (PushNotificationAppConfigXml configXml) => configXml.MultifactorRegistrationEnabled, delegate(PushNotificationAppConfigXml configXml, bool? value)
		{
			configXml.MultifactorRegistrationEnabled = value;
		});

		// Token: 0x04002907 RID: 10503
		public static readonly ADPropertyDefinition PartitionName = PushNotificationAppSchema.CreateXmlProperty<string>("PartitionName", (PushNotificationAppConfigXml configXml) => configXml.PartitionName, delegate(PushNotificationAppConfigXml configXml, string value)
		{
			configXml.PartitionName = value;
		});

		// Token: 0x04002908 RID: 10504
		public static readonly ADPropertyDefinition IsDefaultPartitionName = PushNotificationAppSchema.CreateXmlProperty<bool>("IsDefaultPartitionName", (PushNotificationAppConfigXml configXml) => configXml.IsDefaultPartitionName, delegate(PushNotificationAppConfigXml configXml, bool? value)
		{
			configXml.IsDefaultPartitionName = value;
		});

		// Token: 0x04002909 RID: 10505
		public static readonly ADPropertyDefinition LastUpdateTimeUtc = PushNotificationAppSchema.CreateXmlProperty<DateTime>("LastUpdateTimeUtc", (PushNotificationAppConfigXml configXml) => configXml.LastUpdateTimeUtc, delegate(PushNotificationAppConfigXml configXml, DateTime? value)
		{
			configXml.LastUpdateTimeUtc = value;
		});
	}
}
