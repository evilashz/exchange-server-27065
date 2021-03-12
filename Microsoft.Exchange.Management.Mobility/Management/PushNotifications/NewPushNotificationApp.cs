using System;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.PushNotifications.Publishers;

namespace Microsoft.Exchange.Management.PushNotifications
{
	// Token: 0x0200004E RID: 78
	[Cmdlet("New", "PushNotificationApp", DefaultParameterSetName = "Default", SupportsShouldProcess = true)]
	public sealed class NewPushNotificationApp : NewSystemConfigurationObjectTask<PushNotificationApp>
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000D016 File Offset: 0x0000B216
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewApp(base.Name);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000D023 File Offset: 0x0000B223
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000D030 File Offset: 0x0000B230
		[Parameter(Mandatory = false)]
		public string DisplayName
		{
			get
			{
				return this.DataObject.DisplayName;
			}
			set
			{
				this.DataObject.DisplayName = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000D03E File Offset: 0x0000B23E
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x0000D04B File Offset: 0x0000B24B
		[Parameter(Mandatory = false)]
		public bool? Enabled
		{
			get
			{
				return this.DataObject.Enabled;
			}
			set
			{
				this.DataObject.Enabled = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000D059 File Offset: 0x0000B259
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0000D066 File Offset: 0x0000B266
		[Parameter(Mandatory = false)]
		public Version ExchangeMinimumVersion
		{
			get
			{
				return this.DataObject.ExchangeMinimumVersion;
			}
			set
			{
				this.DataObject.ExchangeMinimumVersion = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000D074 File Offset: 0x0000B274
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000D081 File Offset: 0x0000B281
		[Parameter(Mandatory = false)]
		public Version ExchangeMaximumVersion
		{
			get
			{
				return this.DataObject.ExchangeMaximumVersion;
			}
			set
			{
				this.DataObject.ExchangeMaximumVersion = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000D08F File Offset: 0x0000B28F
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000D0B1 File Offset: 0x0000B2B1
		[Parameter(Mandatory = true, ParameterSetName = "Apns")]
		public SwitchParameter AsApns
		{
			get
			{
				return this.DataObject.Platform.Equals(PushNotificationPlatform.APNS);
			}
			set
			{
				this.DataObject.Platform = PushNotificationPlatform.APNS;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000D0BF File Offset: 0x0000B2BF
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		[Parameter(Mandatory = true, ParameterSetName = "Apns")]
		public string CertificateThumbprint
		{
			get
			{
				return this.DataObject.AuthenticationKey;
			}
			set
			{
				this.DataObject.AuthenticationKey = value;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000D0DA File Offset: 0x0000B2DA
		// (set) Token: 0x06000303 RID: 771 RVA: 0x0000D0E7 File Offset: 0x0000B2E7
		[Parameter(Mandatory = false, ParameterSetName = "Apns")]
		public string CertificateThumbprintFallback
		{
			get
			{
				return this.DataObject.AuthenticationKeyFallback;
			}
			set
			{
				this.DataObject.AuthenticationKeyFallback = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000D0F5 File Offset: 0x0000B2F5
		// (set) Token: 0x06000305 RID: 773 RVA: 0x0000D102 File Offset: 0x0000B302
		[Parameter(Mandatory = false, ParameterSetName = "Apns")]
		public string ApnsHost
		{
			get
			{
				return this.DataObject.Url;
			}
			set
			{
				this.DataObject.Url = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000D110 File Offset: 0x0000B310
		// (set) Token: 0x06000307 RID: 775 RVA: 0x0000D11D File Offset: 0x0000B31D
		[Parameter(Mandatory = false, ParameterSetName = "Apns")]
		public int? ApnsPort
		{
			get
			{
				return this.DataObject.Port;
			}
			set
			{
				this.DataObject.Port = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000D12B File Offset: 0x0000B32B
		// (set) Token: 0x06000309 RID: 777 RVA: 0x0000D138 File Offset: 0x0000B338
		[Parameter(Mandatory = false, ParameterSetName = "Apns")]
		public string FeedbackHost
		{
			get
			{
				return this.DataObject.SecondaryUrl;
			}
			set
			{
				this.DataObject.SecondaryUrl = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000D146 File Offset: 0x0000B346
		// (set) Token: 0x0600030B RID: 779 RVA: 0x0000D153 File Offset: 0x0000B353
		[Parameter(Mandatory = false, ParameterSetName = "Apns")]
		public int? FeedbackPort
		{
			get
			{
				return this.DataObject.SecondaryPort;
			}
			set
			{
				this.DataObject.SecondaryPort = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000D161 File Offset: 0x0000B361
		// (set) Token: 0x0600030D RID: 781 RVA: 0x0000D183 File Offset: 0x0000B383
		[Parameter(Mandatory = true, ParameterSetName = "Wns")]
		public SwitchParameter AsWns
		{
			get
			{
				return this.DataObject.Platform.Equals(PushNotificationPlatform.WNS);
			}
			set
			{
				this.DataObject.Platform = PushNotificationPlatform.WNS;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000D191 File Offset: 0x0000B391
		// (set) Token: 0x0600030F RID: 783 RVA: 0x0000D19E File Offset: 0x0000B39E
		[Parameter(Mandatory = true, ParameterSetName = "Wns")]
		public string AppSid
		{
			get
			{
				return this.DataObject.AuthenticationId;
			}
			set
			{
				this.DataObject.AuthenticationId = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000D1AC File Offset: 0x0000B3AC
		// (set) Token: 0x06000311 RID: 785 RVA: 0x0000D1B9 File Offset: 0x0000B3B9
		[Parameter(Mandatory = false, ParameterSetName = "Wns")]
		public string AppSecret
		{
			get
			{
				return this.DataObject.AuthenticationKey;
			}
			set
			{
				this.DataObject.AuthenticationKey = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000D1C7 File Offset: 0x0000B3C7
		// (set) Token: 0x06000313 RID: 787 RVA: 0x0000D1D4 File Offset: 0x0000B3D4
		[Parameter(Mandatory = false, ParameterSetName = "Wns")]
		public string AuthenticationUri
		{
			get
			{
				return this.DataObject.SecondaryUrl;
			}
			set
			{
				this.DataObject.SecondaryUrl = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000D1E2 File Offset: 0x0000B3E2
		// (set) Token: 0x06000315 RID: 789 RVA: 0x0000D208 File Offset: 0x0000B408
		[Parameter(Mandatory = false, ParameterSetName = "AzureSend")]
		[Parameter(Mandatory = false, ParameterSetName = "Gcm")]
		[Parameter(Mandatory = false, ParameterSetName = "Wns")]
		[Parameter(Mandatory = false, ParameterSetName = "AzureHubCreation")]
		public SwitchParameter UseClearTextAuthenticationKeys
		{
			get
			{
				return (SwitchParameter)(base.Fields["UseClearTextAuthenticationKeys"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["UseClearTextAuthenticationKeys"] = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000D220 File Offset: 0x0000B420
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0000D242 File Offset: 0x0000B442
		[Parameter(Mandatory = true, ParameterSetName = "Gcm")]
		public SwitchParameter AsGcm
		{
			get
			{
				return this.DataObject.Platform.Equals(PushNotificationPlatform.GCM);
			}
			set
			{
				this.DataObject.Platform = PushNotificationPlatform.GCM;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000D250 File Offset: 0x0000B450
		// (set) Token: 0x06000319 RID: 793 RVA: 0x0000D25D File Offset: 0x0000B45D
		[Parameter(Mandatory = false, ParameterSetName = "Gcm")]
		public string Sender
		{
			get
			{
				return this.DataObject.AuthenticationId;
			}
			set
			{
				this.DataObject.AuthenticationId = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000D26B File Offset: 0x0000B46B
		// (set) Token: 0x0600031B RID: 795 RVA: 0x0000D278 File Offset: 0x0000B478
		[Parameter(Mandatory = false, ParameterSetName = "Gcm")]
		public string SenderAuthToken
		{
			get
			{
				return this.DataObject.AuthenticationKey;
			}
			set
			{
				this.DataObject.AuthenticationKey = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000D286 File Offset: 0x0000B486
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0000D293 File Offset: 0x0000B493
		[Parameter(Mandatory = false, ParameterSetName = "Gcm")]
		public string GcmServiceUri
		{
			get
			{
				return this.DataObject.Url;
			}
			set
			{
				this.DataObject.Url = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000D2A1 File Offset: 0x0000B4A1
		// (set) Token: 0x0600031F RID: 799 RVA: 0x0000D2C3 File Offset: 0x0000B4C3
		[Parameter(Mandatory = true, ParameterSetName = "PendingGet")]
		public SwitchParameter AsPendingGet
		{
			get
			{
				return this.DataObject.Platform.Equals(PushNotificationPlatform.PendingGet);
			}
			set
			{
				this.DataObject.Platform = PushNotificationPlatform.PendingGet;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000D2D1 File Offset: 0x0000B4D1
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0000D2F3 File Offset: 0x0000B4F3
		[Parameter(Mandatory = true, ParameterSetName = "WebApp")]
		public SwitchParameter AsWebApp
		{
			get
			{
				return this.DataObject.Platform.Equals(PushNotificationPlatform.WebApp);
			}
			set
			{
				this.DataObject.Platform = PushNotificationPlatform.WebApp;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000D301 File Offset: 0x0000B501
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0000D323 File Offset: 0x0000B523
		[Parameter(Mandatory = true, ParameterSetName = "Proxy")]
		public SwitchParameter AsProxy
		{
			get
			{
				return this.DataObject.Platform.Equals(PushNotificationPlatform.Proxy);
			}
			set
			{
				this.DataObject.Platform = PushNotificationPlatform.Proxy;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000D331 File Offset: 0x0000B531
		// (set) Token: 0x06000325 RID: 805 RVA: 0x0000D33E File Offset: 0x0000B53E
		[Parameter(Mandatory = false, ParameterSetName = "Proxy")]
		public string Uri
		{
			get
			{
				return this.DataObject.Url;
			}
			set
			{
				this.DataObject.Url = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000D34C File Offset: 0x0000B54C
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0000D359 File Offset: 0x0000B559
		[Parameter(Mandatory = false, ParameterSetName = "Proxy")]
		public string Organization
		{
			get
			{
				return this.DataObject.AuthenticationKey;
			}
			set
			{
				this.DataObject.AuthenticationKey = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000D367 File Offset: 0x0000B567
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0000D389 File Offset: 0x0000B589
		[Parameter(Mandatory = true, ParameterSetName = "AzureSend")]
		public SwitchParameter AsAzureSend
		{
			get
			{
				return this.DataObject.Platform.Equals(PushNotificationPlatform.Azure);
			}
			set
			{
				this.DataObject.Platform = PushNotificationPlatform.Azure;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000D397 File Offset: 0x0000B597
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0000D3A4 File Offset: 0x0000B5A4
		[Parameter(Mandatory = false, ParameterSetName = "AzureSend")]
		public string SasKeyName
		{
			get
			{
				return this.DataObject.AuthenticationId;
			}
			set
			{
				this.DataObject.AuthenticationId = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000D3B2 File Offset: 0x0000B5B2
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0000D3BF File Offset: 0x0000B5BF
		[Parameter(Mandatory = false, ParameterSetName = "AzureSend")]
		public string SasKeyValue
		{
			get
			{
				return this.DataObject.AuthenticationKey;
			}
			set
			{
				this.DataObject.AuthenticationKey = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000D3CD File Offset: 0x0000B5CD
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000D3DA File Offset: 0x0000B5DA
		[Parameter(Mandatory = false, ParameterSetName = "AzureSend")]
		[Parameter(Mandatory = false, ParameterSetName = "AzureHubCreation")]
		public string UriTemplate
		{
			get
			{
				return this.DataObject.UriTemplate;
			}
			set
			{
				this.DataObject.UriTemplate = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000D3E8 File Offset: 0x0000B5E8
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000D3F5 File Offset: 0x0000B5F5
		[Parameter(Mandatory = false, ParameterSetName = "AzureSend")]
		public string RegistrationTemplate
		{
			get
			{
				return this.DataObject.RegistrationTemplate;
			}
			set
			{
				this.DataObject.RegistrationTemplate = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000D403 File Offset: 0x0000B603
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000D425 File Offset: 0x0000B625
		[Parameter(Mandatory = true, ParameterSetName = "AzureHubCreation")]
		public SwitchParameter AsAzureHubCreation
		{
			get
			{
				return this.DataObject.Platform.Equals(PushNotificationPlatform.AzureHubCreation);
			}
			set
			{
				this.DataObject.Platform = PushNotificationPlatform.AzureHubCreation;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000D433 File Offset: 0x0000B633
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0000D440 File Offset: 0x0000B640
		[Parameter(Mandatory = false, ParameterSetName = "AzureHubCreation")]
		public string AcsUserName
		{
			get
			{
				return this.DataObject.AuthenticationId;
			}
			set
			{
				this.DataObject.AuthenticationId = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000D44E File Offset: 0x0000B64E
		// (set) Token: 0x06000337 RID: 823 RVA: 0x0000D45B File Offset: 0x0000B65B
		[Parameter(Mandatory = false, ParameterSetName = "AzureHubCreation")]
		public string AcsUserPassword
		{
			get
			{
				return this.DataObject.AuthenticationKey;
			}
			set
			{
				this.DataObject.AuthenticationKey = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000D469 File Offset: 0x0000B669
		// (set) Token: 0x06000339 RID: 825 RVA: 0x0000D476 File Offset: 0x0000B676
		[Parameter(Mandatory = false, ParameterSetName = "AzureHubCreation")]
		public string AcsUriTemplate
		{
			get
			{
				return this.DataObject.Url;
			}
			set
			{
				this.DataObject.Url = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000D484 File Offset: 0x0000B684
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0000D491 File Offset: 0x0000B691
		[Parameter(Mandatory = false, ParameterSetName = "AzureHubCreation")]
		public string AcsScopeUriTemplate
		{
			get
			{
				return this.DataObject.SecondaryUrl;
			}
			set
			{
				this.DataObject.SecondaryUrl = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000D49F File Offset: 0x0000B69F
		// (set) Token: 0x0600033D RID: 829 RVA: 0x0000D4C2 File Offset: 0x0000B6C2
		[Parameter(Mandatory = true, ParameterSetName = "AzureChallengeRequest")]
		public SwitchParameter AsAzureChallengeRequest
		{
			get
			{
				return this.DataObject.Platform.Equals(PushNotificationPlatform.AzureChallengeRequest);
			}
			set
			{
				this.DataObject.Platform = PushNotificationPlatform.AzureChallengeRequest;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000D4D1 File Offset: 0x0000B6D1
		// (set) Token: 0x0600033F RID: 831 RVA: 0x0000D4F4 File Offset: 0x0000B6F4
		[Parameter(Mandatory = true, ParameterSetName = "AzureDeviceRegistration")]
		public SwitchParameter AsAzureDeviceRegistration
		{
			get
			{
				return this.DataObject.Platform.Equals(PushNotificationPlatform.AzureDeviceRegistration);
			}
			set
			{
				this.DataObject.Platform = PushNotificationPlatform.AzureDeviceRegistration;
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000D503 File Offset: 0x0000B703
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 467, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\PushNotifications\\NewPushNotificationApp.cs");
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000D528 File Offset: 0x0000B728
		protected override IConfigurable PrepareDataObject()
		{
			PushNotificationApp pushNotificationApp = (PushNotificationApp)base.PrepareDataObject();
			pushNotificationApp.SetId((IConfigurationSession)base.DataSession, base.Name);
			if (string.IsNullOrEmpty(pushNotificationApp.DisplayName))
			{
				pushNotificationApp.DisplayName = pushNotificationApp.Name;
			}
			if (base.ParameterSetName == "Wns" || base.ParameterSetName == "Gcm" || base.ParameterSetName == "AzureSend" || base.ParameterSetName == "AzureHubCreation")
			{
				pushNotificationApp.IsAuthenticationKeyEncrypted = new bool?(!this.UseClearTextAuthenticationKeys);
				if (pushNotificationApp.IsAuthenticationKeyEncrypted.Value)
				{
					PushNotificationDataProtector pushNotificationDataProtector = new PushNotificationDataProtector(null);
					pushNotificationApp.AuthenticationKey = pushNotificationDataProtector.Encrypt(pushNotificationApp.AuthenticationKey);
				}
			}
			return pushNotificationApp;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000D5FC File Offset: 0x0000B7FC
		protected override void InternalValidate()
		{
			base.InternalValidate();
			PushNotificationPublisherSettingsFactory pushNotificationPublisherSettingsFactory = new PushNotificationPublisherSettingsFactory();
			PushNotificationPublisherSettings pushNotificationPublisherSettings = pushNotificationPublisherSettingsFactory.Create(this.DataObject);
			try
			{
				pushNotificationPublisherSettings.Validate();
			}
			catch (PushNotificationConfigurationException exception)
			{
				base.WriteError(exception, ExchangeErrorCategory.ServerOperation, null);
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000D64C File Offset: 0x0000B84C
		protected override void WriteResult(IConfigurable dataObject)
		{
			PushNotificationApp pushNotificationApp = dataObject as PushNotificationApp;
			base.WriteResult(new PushNotificationAppPresentationObject(pushNotificationApp));
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000D66C File Offset: 0x0000B86C
		protected override bool IsKnownException(Exception exception)
		{
			return exception is PushNotificationConfigurationException || base.IsKnownException(exception);
		}
	}
}
