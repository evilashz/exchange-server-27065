using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.PushNotifications
{
	// Token: 0x02000051 RID: 81
	[Serializable]
	public sealed class PushNotificationAppPresentationObject : ADPresentationObject
	{
		// Token: 0x06000349 RID: 841 RVA: 0x0000D784 File Offset: 0x0000B984
		internal PushNotificationAppPresentationObject(PushNotificationApp pushNotificationApp) : base(pushNotificationApp)
		{
			switch (pushNotificationApp.Platform)
			{
			case PushNotificationPlatform.APNS:
				this.defaultValues = PushNotificationAppPresentationObject.ApnsDefaults;
				return;
			case PushNotificationPlatform.PendingGet:
				this.defaultValues = PushNotificationAppPresentationObject.PendingGetDefaults;
				return;
			case PushNotificationPlatform.WNS:
				this.defaultValues = PushNotificationAppPresentationObject.WnsDefaults;
				return;
			case PushNotificationPlatform.Proxy:
				this.defaultValues = PushNotificationAppPresentationObject.ProxyDefaults;
				return;
			case PushNotificationPlatform.GCM:
				this.defaultValues = PushNotificationAppPresentationObject.GcmDefaults;
				return;
			case PushNotificationPlatform.WebApp:
				this.defaultValues = PushNotificationAppPresentationObject.WebAppDefaults;
				return;
			case PushNotificationPlatform.Azure:
				this.defaultValues = PushNotificationAppPresentationObject.AzureDefaults;
				return;
			case PushNotificationPlatform.AzureHubCreation:
				this.defaultValues = PushNotificationAppPresentationObject.AzureHubCreationDefaults;
				return;
			case PushNotificationPlatform.AzureChallengeRequest:
				this.defaultValues = PushNotificationAppPresentationObject.AzureChallengeRequestDefaults;
				return;
			case PushNotificationPlatform.AzureDeviceRegistration:
				this.defaultValues = PushNotificationAppPresentationObject.AzureAzureDeviceRegistrationDefaults;
				return;
			default:
				throw new NotSupportedException("Unsupported PushNotificationPlatform: " + pushNotificationApp.Platform.ToString());
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000D868 File Offset: 0x0000BA68
		public PushNotificationAppPresentationObject()
		{
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000D870 File Offset: 0x0000BA70
		public string DisplayName
		{
			get
			{
				return (string)this[PushNotificationAppPresentationSchema.DisplayName];
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000D882 File Offset: 0x0000BA82
		public PushNotificationPlatform Platform
		{
			get
			{
				return (PushNotificationPlatform)this[PushNotificationAppPresentationSchema.Platform];
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000D894 File Offset: 0x0000BA94
		public bool? Enabled
		{
			get
			{
				return (bool?)(this[PushNotificationAppPresentationSchema.Enabled] ?? this.defaultValues.Enabled);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000D8BA File Offset: 0x0000BABA
		public Version ExchangeMinimumVersion
		{
			get
			{
				return (Version)(this[PushNotificationAppPresentationSchema.ExchangeMinimumVersion] ?? this.defaultValues.ExchangeMinimumVersion);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000D8DB File Offset: 0x0000BADB
		public Version ExchangeMaximumVersion
		{
			get
			{
				return (Version)(this[PushNotificationAppPresentationSchema.ExchangeMaximumVersion] ?? this.defaultValues.ExchangeMaximumVersion);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000D8FC File Offset: 0x0000BAFC
		public int? QueueSize
		{
			get
			{
				return (int?)(this[PushNotificationAppPresentationSchema.QueueSize] ?? this.defaultValues.QueueSize);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000D922 File Offset: 0x0000BB22
		public int? NumberOfChannels
		{
			get
			{
				return (int?)(this[PushNotificationAppPresentationSchema.NumberOfChannels] ?? this.defaultValues.NumberOfChannels);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000D948 File Offset: 0x0000BB48
		public int? BackOffTimeInSeconds
		{
			get
			{
				return (int?)(this[PushNotificationAppPresentationSchema.BackOffTimeInSeconds] ?? this.defaultValues.BackOffTimeInSeconds);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000D96E File Offset: 0x0000BB6E
		public string AuthenticationId
		{
			get
			{
				return (string)(this[PushNotificationAppPresentationSchema.AuthenticationId] ?? this.defaultValues.AuthenticationId);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000D98F File Offset: 0x0000BB8F
		public string AuthenticationKey
		{
			get
			{
				return (string)(this[PushNotificationAppPresentationSchema.AuthenticationKey] ?? this.defaultValues.AuthenticationKey);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000D9B0 File Offset: 0x0000BBB0
		public string AuthenticationKeyFallback
		{
			get
			{
				return (string)(this[PushNotificationAppPresentationSchema.AuthenticationKeyFallback] ?? this.defaultValues.AuthenticationKeyFallback);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000D9D1 File Offset: 0x0000BBD1
		internal bool? IsAuthenticationKeyEncrypted
		{
			get
			{
				return (bool?)(this[PushNotificationAppPresentationSchema.IsAuthenticationKeyEncrypted] ?? this.defaultValues.IsAuthenticationKeyEncrypted);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000D9F7 File Offset: 0x0000BBF7
		public string UriTemplate
		{
			get
			{
				return (string)(this[PushNotificationAppPresentationSchema.UriTemplate] ?? this.defaultValues.UriTemplate);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000DA18 File Offset: 0x0000BC18
		public string Url
		{
			get
			{
				return (string)(this[PushNotificationAppPresentationSchema.Url] ?? this.defaultValues.Url);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000DA39 File Offset: 0x0000BC39
		public int? Port
		{
			get
			{
				return (int?)(this[PushNotificationAppPresentationSchema.Port] ?? this.defaultValues.Port);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000DA5F File Offset: 0x0000BC5F
		public bool? RegistrationEnabled
		{
			get
			{
				return (bool?)(this[PushNotificationAppPresentationSchema.RegitrationEnabled] ?? this.defaultValues.RegistrationEnabled);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000DA85 File Offset: 0x0000BC85
		public string RegistrationTemplate
		{
			get
			{
				return (string)(this[PushNotificationAppPresentationSchema.RegistrationTemplate] ?? this.defaultValues.RegistrationTemplate);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000DAA6 File Offset: 0x0000BCA6
		public bool? MultifactorRegistrationEnabled
		{
			get
			{
				return (bool?)(this[PushNotificationAppPresentationSchema.MultifactorRegistrationEnabled] ?? this.defaultValues.MultifactorRegistrationEnabled);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000DACC File Offset: 0x0000BCCC
		public string PartitionName
		{
			get
			{
				return (string)(this[PushNotificationAppPresentationSchema.PartitionName] ?? this.defaultValues.RegistrationTemplate);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000DAED File Offset: 0x0000BCED
		public bool? IsDefaultPartitionName
		{
			get
			{
				return (bool?)(this[PushNotificationAppPresentationSchema.IsDefaultPartitionName] ?? this.defaultValues.IsDefaultPartitionName);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000DB13 File Offset: 0x0000BD13
		public string SecondaryUrl
		{
			get
			{
				return (string)(this[PushNotificationAppPresentationSchema.SecondaryUrl] ?? this.defaultValues.SecondaryUrl);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000DB34 File Offset: 0x0000BD34
		public int? SecondaryPort
		{
			get
			{
				return (int?)(this[PushNotificationAppPresentationSchema.SecondaryPort] ?? this.defaultValues.SecondaryPort);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000DB5A File Offset: 0x0000BD5A
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return PushNotificationAppPresentationObject.SchemaInstance;
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000DB64 File Offset: 0x0000BD64
		private static PushNotificationApp BuildApnsDefaults()
		{
			return new PushNotificationApp
			{
				Enabled = new bool?(true),
				ExchangeMinimumVersion = null,
				ExchangeMaximumVersion = null,
				QueueSize = new int?(10000),
				NumberOfChannels = new int?(1),
				Url = "gateway.push.apple.com",
				Port = new int?(2195),
				SecondaryUrl = "feedback.push.apple.com",
				SecondaryPort = new int?(2196),
				BackOffTimeInSeconds = new int?(600)
			};
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000DBF4 File Offset: 0x0000BDF4
		private static PushNotificationApp BuildWnsDefaults()
		{
			return new PushNotificationApp
			{
				Enabled = new bool?(true),
				ExchangeMinimumVersion = null,
				ExchangeMaximumVersion = null,
				QueueSize = new int?(10000),
				NumberOfChannels = new int?(1),
				IsAuthenticationKeyEncrypted = new bool?(true),
				SecondaryUrl = "https://login.live.com/accesstoken.srf",
				BackOffTimeInSeconds = new int?(600)
			};
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000DC68 File Offset: 0x0000BE68
		private static PushNotificationApp BuildGcmDefaults()
		{
			return new PushNotificationApp
			{
				Enabled = new bool?(true),
				ExchangeMinimumVersion = null,
				ExchangeMaximumVersion = null,
				QueueSize = new int?(10000),
				NumberOfChannels = new int?(1),
				IsAuthenticationKeyEncrypted = new bool?(true),
				Url = "https://android.googleapis.com/gcm/send",
				BackOffTimeInSeconds = new int?(600)
			};
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000DCDC File Offset: 0x0000BEDC
		private static PushNotificationApp BuildAzureDefaults()
		{
			return new PushNotificationApp
			{
				Enabled = new bool?(true),
				ExchangeMinimumVersion = null,
				ExchangeMaximumVersion = null,
				QueueSize = new int?(10000),
				NumberOfChannels = new int?(1),
				MultifactorRegistrationEnabled = new bool?(false),
				IsDefaultPartitionName = new bool?(false),
				IsAuthenticationKeyEncrypted = new bool?(true),
				UriTemplate = "https://{0}-{1}.servicebus.windows.net/exo/{2}/{3}",
				RegistrationEnabled = new bool?(false),
				BackOffTimeInSeconds = new int?(600)
			};
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000DD74 File Offset: 0x0000BF74
		private static PushNotificationApp BuildAzureHubCreationDefaults()
		{
			return new PushNotificationApp
			{
				Enabled = new bool?(true),
				ExchangeMinimumVersion = null,
				ExchangeMaximumVersion = null,
				QueueSize = new int?(10000),
				NumberOfChannels = new int?(1),
				IsAuthenticationKeyEncrypted = new bool?(true),
				UriTemplate = "https://{0}-{1}.servicebus.windows.net/exo/{2}{3}",
				Url = "https://{0}-{1}-sb.accesscontrol.windows.net/",
				SecondaryUrl = "http://{0}-{1}.servicebus.windows.net/exo/",
				BackOffTimeInSeconds = new int?(600)
			};
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000DDFC File Offset: 0x0000BFFC
		private static PushNotificationApp BuildAzureChallengeRequestDefaults()
		{
			return new PushNotificationApp
			{
				Enabled = new bool?(true),
				ExchangeMinimumVersion = null,
				ExchangeMaximumVersion = null,
				QueueSize = new int?(10000),
				NumberOfChannels = new int?(1),
				BackOffTimeInSeconds = new int?(600)
			};
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000DE58 File Offset: 0x0000C058
		private static PushNotificationApp BuildAzureDeviceRegistrationDefaults()
		{
			return new PushNotificationApp
			{
				Enabled = new bool?(true),
				ExchangeMinimumVersion = null,
				ExchangeMaximumVersion = null,
				QueueSize = new int?(10000),
				NumberOfChannels = new int?(1),
				BackOffTimeInSeconds = new int?(600)
			};
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000DEB4 File Offset: 0x0000C0B4
		private static PushNotificationApp BuildWebAppDefaults()
		{
			return new PushNotificationApp
			{
				Enabled = new bool?(true),
				ExchangeMinimumVersion = null,
				ExchangeMaximumVersion = null,
				QueueSize = new int?(10000),
				NumberOfChannels = new int?(1)
			};
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000DF00 File Offset: 0x0000C100
		private static PushNotificationApp BuildPendingGetDefaults()
		{
			return new PushNotificationApp
			{
				Enabled = new bool?(true),
				ExchangeMinimumVersion = null,
				ExchangeMaximumVersion = null,
				QueueSize = new int?(10000),
				NumberOfChannels = new int?(1)
			};
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000DF4C File Offset: 0x0000C14C
		private static PushNotificationApp BuildProxyDefaults()
		{
			return new PushNotificationApp
			{
				Enabled = new bool?(false),
				ExchangeMinimumVersion = null,
				ExchangeMaximumVersion = null,
				QueueSize = new int?(10000),
				NumberOfChannels = new int?(1)
			};
		}

		// Token: 0x040000E8 RID: 232
		private static readonly PushNotificationAppPresentationSchema SchemaInstance = ObjectSchema.GetInstance<PushNotificationAppPresentationSchema>();

		// Token: 0x040000E9 RID: 233
		private static readonly PushNotificationApp ApnsDefaults = PushNotificationAppPresentationObject.BuildApnsDefaults();

		// Token: 0x040000EA RID: 234
		private static readonly PushNotificationApp WnsDefaults = PushNotificationAppPresentationObject.BuildWnsDefaults();

		// Token: 0x040000EB RID: 235
		private static readonly PushNotificationApp GcmDefaults = PushNotificationAppPresentationObject.BuildGcmDefaults();

		// Token: 0x040000EC RID: 236
		private static readonly PushNotificationApp WebAppDefaults = PushNotificationAppPresentationObject.BuildWebAppDefaults();

		// Token: 0x040000ED RID: 237
		private static readonly PushNotificationApp PendingGetDefaults = PushNotificationAppPresentationObject.BuildPendingGetDefaults();

		// Token: 0x040000EE RID: 238
		private static readonly PushNotificationApp ProxyDefaults = PushNotificationAppPresentationObject.BuildProxyDefaults();

		// Token: 0x040000EF RID: 239
		private static readonly PushNotificationApp AzureDefaults = PushNotificationAppPresentationObject.BuildAzureDefaults();

		// Token: 0x040000F0 RID: 240
		private static readonly PushNotificationApp AzureHubCreationDefaults = PushNotificationAppPresentationObject.BuildAzureHubCreationDefaults();

		// Token: 0x040000F1 RID: 241
		private static readonly PushNotificationApp AzureChallengeRequestDefaults = PushNotificationAppPresentationObject.BuildAzureChallengeRequestDefaults();

		// Token: 0x040000F2 RID: 242
		private static readonly PushNotificationApp AzureAzureDeviceRegistrationDefaults = PushNotificationAppPresentationObject.BuildAzureDeviceRegistrationDefaults();

		// Token: 0x040000F3 RID: 243
		private PushNotificationApp defaultValues;
	}
}
