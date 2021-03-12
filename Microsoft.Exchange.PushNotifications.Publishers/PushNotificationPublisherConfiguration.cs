using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000093 RID: 147
	internal class PushNotificationPublisherConfiguration : IEquatable<PushNotificationPublisherConfiguration>
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x0000FFA4 File Offset: 0x0000E1A4
		public PushNotificationPublisherConfiguration(bool ignoreErrors = true, List<IPushNotificationPublisherConfigurationProvider> providers = null)
		{
			this.ignoreErrors = ignoreErrors;
			this.rawSettings = new Dictionary<string, IPushNotificationRawSettings>();
			this.publisherSettings = new Dictionary<string, PushNotificationPublisherSettings>();
			this.unsuitablePublishers = new Dictionary<string, string>();
			this.settingsFactory = new PushNotificationPublisherSettingsFactory();
			this.hashCode = new Lazy<int>(() => Guid.NewGuid().ToString().GetHashCode());
			this.apnsPublisherSettings = new Lazy<List<ApnsPublisherSettings>>(() => this.FilterByPlatform<ApnsPublisherSettings>(PushNotificationPlatform.APNS));
			this.wnsPublisherSettings = new Lazy<List<WnsPublisherSettings>>(() => this.FilterByPlatform<WnsPublisherSettings>(PushNotificationPlatform.WNS));
			this.gcmPublisherSettings = new Lazy<List<GcmPublisherSettings>>(() => this.FilterByPlatform<GcmPublisherSettings>(PushNotificationPlatform.GCM));
			this.webAppPublisherSettings = new Lazy<List<WebAppPublisherSettings>>(() => this.FilterByPlatform<WebAppPublisherSettings>(PushNotificationPlatform.WebApp));
			this.azurePublisherSettings = new Lazy<List<AzurePublisherSettings>>(() => this.FilterByPlatform<AzurePublisherSettings>(PushNotificationPlatform.Azure));
			this.azureHubCreationPublisherSettings = new Lazy<List<AzureHubCreationPublisherSettings>>(() => this.FilterByPlatform<AzureHubCreationPublisherSettings>(PushNotificationPlatform.AzureHubCreation));
			this.azureChallengeRequestPublisherSettings = new Lazy<List<AzureChallengeRequestPublisherSettings>>(() => this.FilterByPlatform<AzureChallengeRequestPublisherSettings>(PushNotificationPlatform.AzureChallengeRequest));
			this.azureDeviceRegistrationPublisherSettings = new Lazy<List<AzureDeviceRegistrationPublisherSettings>>(() => this.FilterByPlatform<AzureDeviceRegistrationPublisherSettings>(PushNotificationPlatform.AzureDeviceRegistration));
			this.proxyPublisherSettings = new Lazy<ProxyPublisherSettings>(() => this.FilterByName<ProxyPublisherSettings>(PushNotificationCannedApp.OnPremProxy.Name));
			this.azureSendSettings = new Lazy<Dictionary<string, AzurePublisherSettings>>(delegate()
			{
				Dictionary<string, AzurePublisherSettings> dictionary = new Dictionary<string, AzurePublisherSettings>();
				foreach (AzurePublisherSettings azurePublisherSettings in this.AzurePublisherSettings)
				{
					dictionary.Add(azurePublisherSettings.AppId, azurePublisherSettings);
				}
				return dictionary;
			});
			List<IPushNotificationPublisherConfigurationProvider> list = providers;
			if (list == null)
			{
				list = new List<IPushNotificationPublisherConfigurationProvider>(2)
				{
					new RegistryConfigurationProvider(),
					new ADConfigurationProvider()
				};
			}
			foreach (IPushNotificationPublisherConfigurationProvider provider in list)
			{
				this.LoadPublisherSettings(provider);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x000101C8 File Offset: 0x0000E3C8
		public bool HasEnabledPublisherSettings
		{
			get
			{
				return this.publisherSettings.Count > 0;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x000101D8 File Offset: 0x0000E3D8
		public virtual IEnumerable<PushNotificationPublisherSettings> PublisherSettings
		{
			get
			{
				return this.publisherSettings.Values;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x000101E5 File Offset: 0x0000E3E5
		public virtual IEnumerable<string> UnsuitablePublishers
		{
			get
			{
				return this.unsuitablePublishers.Values;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x000101F2 File Offset: 0x0000E3F2
		public IEnumerable<ApnsPublisherSettings> ApnsPublisherSettings
		{
			get
			{
				return this.apnsPublisherSettings.Value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x000101FF File Offset: 0x0000E3FF
		public IEnumerable<WnsPublisherSettings> WnsPublisherSettings
		{
			get
			{
				return this.wnsPublisherSettings.Value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0001020C File Offset: 0x0000E40C
		public IEnumerable<GcmPublisherSettings> GcmPublisherSettings
		{
			get
			{
				return this.gcmPublisherSettings.Value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00010219 File Offset: 0x0000E419
		public IEnumerable<WebAppPublisherSettings> WebAppPublisherSettings
		{
			get
			{
				return this.webAppPublisherSettings.Value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00010226 File Offset: 0x0000E426
		public IEnumerable<AzurePublisherSettings> AzurePublisherSettings
		{
			get
			{
				return this.azurePublisherSettings.Value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00010233 File Offset: 0x0000E433
		public IEnumerable<AzureHubCreationPublisherSettings> AzureHubCreationPublisherSettings
		{
			get
			{
				return this.azureHubCreationPublisherSettings.Value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00010240 File Offset: 0x0000E440
		public IEnumerable<AzureChallengeRequestPublisherSettings> AzureChallengeRequestPublisherSettings
		{
			get
			{
				return this.azureChallengeRequestPublisherSettings.Value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0001024D File Offset: 0x0000E44D
		public IEnumerable<AzureDeviceRegistrationPublisherSettings> AzureDeviceRegistrationPublisherSettings
		{
			get
			{
				return this.azureDeviceRegistrationPublisherSettings.Value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0001025A File Offset: 0x0000E45A
		public ProxyPublisherSettings ProxyPublisherSettings
		{
			get
			{
				return this.proxyPublisherSettings.Value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00010267 File Offset: 0x0000E467
		public Dictionary<string, AzurePublisherSettings> AzureSendPublisherSettings
		{
			get
			{
				return this.azureSendSettings.Value;
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00010274 File Offset: 0x0000E474
		public PushNotificationPublisherManager CreatePublisherManager(Dictionary<PushNotificationPlatform, PushNotificationPublisherFactory> configFactories)
		{
			ArgumentValidator.ThrowIfNull("configFactories", configFactories);
			PushNotificationPublisherManager pushNotificationPublisherManager = new PushNotificationPublisherManager(null);
			foreach (IPushNotificationRawSettings pushNotificationRawSettings in this.rawSettings.Values)
			{
				PushNotificationPublisherFactory pushNotificationPublisherFactory;
				if (configFactories.TryGetValue(pushNotificationRawSettings.Platform, out pushNotificationPublisherFactory))
				{
					pushNotificationPublisherManager.RegisterPublisher(pushNotificationPublisherFactory.CreatePublisher(this.publisherSettings[pushNotificationRawSettings.Name]));
				}
				else
				{
					PushNotificationsCrimsonEvents.UnsupportedPlatform.Log<string, PushNotificationPlatform>(pushNotificationRawSettings.Name, pushNotificationRawSettings.Platform);
					ExTraceGlobals.PushNotificationServiceTracer.TraceWarning<string, PushNotificationPlatform>((long)this.GetHashCode(), "[PushNotificationPublisherManager:Create] App with AppId '{0}' defines a platform '{1}' that is currently not supported by this server.", pushNotificationRawSettings.Name, pushNotificationRawSettings.Platform);
				}
			}
			foreach (string publisherName in this.UnsuitablePublishers)
			{
				pushNotificationPublisherManager.RegisterUnsuitablePublisher(publisherName);
			}
			PushNotificationsCrimsonEvents.PushNotificationKnownPublishersCreated.Log<PushNotificationPublisherManager>(pushNotificationPublisherManager);
			return pushNotificationPublisherManager;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001038C File Offset: 0x0000E58C
		public MonitoringMailboxNotificationFactory CreateMonitoringNotificationFactory(Dictionary<PushNotificationPlatform, IMonitoringMailboxNotificationRecipientFactory> recipientFactories)
		{
			MonitoringMailboxNotificationFactory monitoringMailboxNotificationFactory = new MonitoringMailboxNotificationFactory();
			foreach (IPushNotificationRawSettings pushNotificationRawSettings in this.rawSettings.Values)
			{
				IMonitoringMailboxNotificationRecipientFactory recipientFactory;
				if (recipientFactories.TryGetValue(pushNotificationRawSettings.Platform, out recipientFactory))
				{
					monitoringMailboxNotificationFactory.RegisterAppToMonitor(pushNotificationRawSettings.Name, recipientFactory);
				}
			}
			return monitoringMailboxNotificationFactory;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00010404 File Offset: 0x0000E604
		public override int GetHashCode()
		{
			return this.hashCode.Value;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00010414 File Offset: 0x0000E614
		public override bool Equals(object obj)
		{
			PushNotificationPublisherConfiguration pushNotificationPublisherConfiguration = obj as PushNotificationPublisherConfiguration;
			return pushNotificationPublisherConfiguration != null && this.Equals(pushNotificationPublisherConfiguration);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00010434 File Offset: 0x0000E634
		public bool Equals(PushNotificationPublisherConfiguration that)
		{
			if (that == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, that))
			{
				return true;
			}
			if (this.publisherSettings.Count != that.publisherSettings.Count || this.unsuitablePublishers.Count != that.unsuitablePublishers.Count)
			{
				return false;
			}
			foreach (IPushNotificationRawSettings pushNotificationRawSettings in this.rawSettings.Values)
			{
				IPushNotificationRawSettings other;
				if (!that.rawSettings.TryGetValue(pushNotificationRawSettings.Name, out other) || !pushNotificationRawSettings.Equals(other))
				{
					return false;
				}
			}
			foreach (string key in this.unsuitablePublishers.Keys)
			{
				if (!that.unsuitablePublishers.ContainsKey(key))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00010548 File Offset: 0x0000E748
		public string ToFullString()
		{
			return string.Format("{0}; Unsuitable:[0]", this.rawSettings.ToNullableString(null, (IPushNotificationRawSettings provider) => provider.ToFullString()), this.unsuitablePublishers.Keys.ToNullableString(null));
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001059C File Offset: 0x0000E79C
		private List<T> FilterByPlatform<T>(PushNotificationPlatform platform) where T : PushNotificationPublisherSettings
		{
			List<T> list = new List<T>();
			foreach (IPushNotificationRawSettings pushNotificationRawSettings in this.rawSettings.Values)
			{
				if (pushNotificationRawSettings.Platform == platform)
				{
					list.Add((T)((object)this.publisherSettings[pushNotificationRawSettings.Name]));
				}
			}
			return list;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001061C File Offset: 0x0000E81C
		private T FilterByName<T>(string name) where T : PushNotificationPublisherSettings
		{
			PushNotificationPublisherSettings pushNotificationPublisherSettings;
			if (this.publisherSettings.TryGetValue(name, out pushNotificationPublisherSettings))
			{
				return (T)((object)pushNotificationPublisherSettings);
			}
			return default(T);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001064C File Offset: 0x0000E84C
		private void LoadPublisherSettings(IPushNotificationPublisherConfigurationProvider provider)
		{
			foreach (IPushNotificationRawSettings pushNotificationRawSettings in provider.LoadSettings(this.ignoreErrors))
			{
				if (!this.rawSettings.ContainsKey(pushNotificationRawSettings.Name))
				{
					if (this.unsuitablePublishers.ContainsKey(pushNotificationRawSettings.Name))
					{
						PushNotificationsCrimsonEvents.UnsuitableConfigurationOverride.Log<string>(pushNotificationRawSettings.Name);
						ExTraceGlobals.PushNotificationServiceTracer.TraceWarning<string>((long)this.GetHashCode(), "App'{0}' settings ignored because unsuitable settings had precedence.", pushNotificationRawSettings.Name);
					}
					else
					{
						PushNotificationPublisherSettings pushNotificationPublisherSettings = this.settingsFactory.Create(pushNotificationRawSettings);
						if (pushNotificationPublisherSettings.IsSuitable)
						{
							this.rawSettings[pushNotificationRawSettings.Name] = pushNotificationRawSettings;
							this.publisherSettings[pushNotificationPublisherSettings.AppId] = pushNotificationPublisherSettings;
						}
						else
						{
							this.unsuitablePublishers[pushNotificationRawSettings.Name] = pushNotificationRawSettings.Name;
						}
					}
				}
			}
		}

		// Token: 0x04000262 RID: 610
		private readonly bool ignoreErrors;

		// Token: 0x04000263 RID: 611
		private readonly Dictionary<string, IPushNotificationRawSettings> rawSettings;

		// Token: 0x04000264 RID: 612
		private readonly Dictionary<string, PushNotificationPublisherSettings> publisherSettings;

		// Token: 0x04000265 RID: 613
		private readonly Dictionary<string, string> unsuitablePublishers;

		// Token: 0x04000266 RID: 614
		private readonly Lazy<List<ApnsPublisherSettings>> apnsPublisherSettings;

		// Token: 0x04000267 RID: 615
		private readonly Lazy<List<WnsPublisherSettings>> wnsPublisherSettings;

		// Token: 0x04000268 RID: 616
		private readonly Lazy<List<GcmPublisherSettings>> gcmPublisherSettings;

		// Token: 0x04000269 RID: 617
		private readonly Lazy<List<WebAppPublisherSettings>> webAppPublisherSettings;

		// Token: 0x0400026A RID: 618
		private readonly Lazy<List<AzurePublisherSettings>> azurePublisherSettings;

		// Token: 0x0400026B RID: 619
		private readonly Lazy<List<AzureHubCreationPublisherSettings>> azureHubCreationPublisherSettings;

		// Token: 0x0400026C RID: 620
		private readonly Lazy<List<AzureChallengeRequestPublisherSettings>> azureChallengeRequestPublisherSettings;

		// Token: 0x0400026D RID: 621
		private readonly Lazy<List<AzureDeviceRegistrationPublisherSettings>> azureDeviceRegistrationPublisherSettings;

		// Token: 0x0400026E RID: 622
		private readonly Lazy<ProxyPublisherSettings> proxyPublisherSettings;

		// Token: 0x0400026F RID: 623
		private readonly Lazy<Dictionary<string, AzurePublisherSettings>> azureSendSettings;

		// Token: 0x04000270 RID: 624
		private readonly PushNotificationPublisherSettingsFactory settingsFactory;

		// Token: 0x04000271 RID: 625
		private readonly Lazy<int> hashCode;
	}
}
