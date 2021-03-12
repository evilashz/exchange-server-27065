using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.PushNotifications.Publishers;

namespace Microsoft.Exchange.Management.PushNotifications
{
	// Token: 0x02000059 RID: 89
	[Cmdlet("Set", "PushNotificationApp", SupportsShouldProcess = true, DefaultParameterSetName = "AzureSend")]
	public sealed class SetPushNotificationApp : SetSystemConfigurationObjectTask<PushNotificationAppIdParameter, PushNotificationApp>
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000E93C File Offset: 0x0000CB3C
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0000E953 File Offset: 0x0000CB53
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override PushNotificationAppIdParameter Identity
		{
			get
			{
				return (PushNotificationAppIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000E966 File Offset: 0x0000CB66
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0000E97D File Offset: 0x0000CB7D
		[Parameter(Mandatory = false, ParameterSetName = "Apns")]
		public string CertificateThumbprint
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.AuthenticationKey];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.AuthenticationKey] = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000E990 File Offset: 0x0000CB90
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0000E9A7 File Offset: 0x0000CBA7
		[Parameter(Mandatory = false, ParameterSetName = "Apns")]
		public string CertificateThumbprintFallback
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.AuthenticationKeyFallback];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.AuthenticationKeyFallback] = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000E9BA File Offset: 0x0000CBBA
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0000E9D1 File Offset: 0x0000CBD1
		[Parameter(Mandatory = false, ParameterSetName = "Apns")]
		public string ApnsHost
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.Url];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.Url] = value;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0000E9FB File Offset: 0x0000CBFB
		[Parameter(Mandatory = false, ParameterSetName = "Apns")]
		public int? ApnsPort
		{
			get
			{
				return (int?)base.Fields[PushNotificationAppSchema.Port];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.Port] = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000EA13 File Offset: 0x0000CC13
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000EA2A File Offset: 0x0000CC2A
		[Parameter(Mandatory = false, ParameterSetName = "Apns")]
		public string FeedbackHost
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.SecondaryUrl];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.SecondaryUrl] = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000EA3D File Offset: 0x0000CC3D
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x0000EA54 File Offset: 0x0000CC54
		[Parameter(Mandatory = false, ParameterSetName = "Apns")]
		public int? FeedbackPort
		{
			get
			{
				return (int?)base.Fields[PushNotificationAppSchema.SecondaryPort];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.SecondaryPort] = value;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000EA6C File Offset: 0x0000CC6C
		// (set) Token: 0x060003BA RID: 954 RVA: 0x0000EA83 File Offset: 0x0000CC83
		[Parameter(Mandatory = false, ParameterSetName = "Wns")]
		public string AppSid
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.AuthenticationId];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.AuthenticationId] = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000EA96 File Offset: 0x0000CC96
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0000EAAD File Offset: 0x0000CCAD
		[Parameter(Mandatory = false, ParameterSetName = "Wns")]
		public string AppSecret
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.AuthenticationKey];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.AuthenticationKey] = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000EAC0 File Offset: 0x0000CCC0
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000EAD7 File Offset: 0x0000CCD7
		[Parameter(Mandatory = false, ParameterSetName = "Wns")]
		public string AuthenticationUri
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.SecondaryUrl];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.SecondaryUrl] = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000EAEA File Offset: 0x0000CCEA
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0000EB10 File Offset: 0x0000CD10
		[Parameter(Mandatory = false, ParameterSetName = "AzureHubCreation")]
		[Parameter(Mandatory = false, ParameterSetName = "Wns")]
		[Parameter(Mandatory = false, ParameterSetName = "Gcm")]
		[Parameter(Mandatory = false, ParameterSetName = "AzureSend")]
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

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000EB28 File Offset: 0x0000CD28
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x0000EB3F File Offset: 0x0000CD3F
		[Parameter(Mandatory = false, ParameterSetName = "Gcm")]
		public string Sender
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.AuthenticationId];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.AuthenticationId] = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000EB52 File Offset: 0x0000CD52
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000EB69 File Offset: 0x0000CD69
		[Parameter(Mandatory = false, ParameterSetName = "Gcm")]
		public string SenderAuthToken
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.AuthenticationKey];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.AuthenticationKey] = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000EB7C File Offset: 0x0000CD7C
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x0000EB93 File Offset: 0x0000CD93
		[Parameter(Mandatory = false, ParameterSetName = "Gcm")]
		public string GcmServiceUri
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.Url];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.Url] = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000EBA6 File Offset: 0x0000CDA6
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x0000EBBD File Offset: 0x0000CDBD
		[Parameter(Mandatory = false, ParameterSetName = "Proxy")]
		public string Uri
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.Url];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.Url] = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
		// (set) Token: 0x060003CA RID: 970 RVA: 0x0000EBE7 File Offset: 0x0000CDE7
		[Parameter(Mandatory = false, ParameterSetName = "Proxy")]
		public string Organization
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.AuthenticationKey];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.AuthenticationKey] = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000EBFA File Offset: 0x0000CDFA
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0000EC11 File Offset: 0x0000CE11
		[Parameter(Mandatory = false, ParameterSetName = "AzureSend")]
		public string SasKeyName
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.AuthenticationId];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.AuthenticationId] = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000EC24 File Offset: 0x0000CE24
		// (set) Token: 0x060003CE RID: 974 RVA: 0x0000EC3B File Offset: 0x0000CE3B
		[Parameter(Mandatory = false, ParameterSetName = "AzureSend")]
		public string SasKeyValue
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.AuthenticationKey];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.AuthenticationKey] = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000EC4E File Offset: 0x0000CE4E
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x0000EC65 File Offset: 0x0000CE65
		[Parameter(Mandatory = false, ParameterSetName = "AzureHubCreation")]
		[Parameter(Mandatory = false, ParameterSetName = "AzureSend")]
		public string UriTemplate
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.UriTemplate];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.UriTemplate] = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000EC78 File Offset: 0x0000CE78
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x0000EC8F File Offset: 0x0000CE8F
		[Parameter(Mandatory = false, ParameterSetName = "AzureSend")]
		public string RegistrationTemplate
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.RegistrationTemplate];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.RegistrationTemplate] = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000ECA2 File Offset: 0x0000CEA2
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x0000ECB9 File Offset: 0x0000CEB9
		[Parameter(Mandatory = false, ParameterSetName = "AzureSend")]
		public bool? RegistrationEnabled
		{
			get
			{
				return (bool?)base.Fields[PushNotificationAppSchema.RegistrationEnabled];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.RegistrationEnabled] = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000ECD1 File Offset: 0x0000CED1
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x0000ECE8 File Offset: 0x0000CEE8
		[Parameter(Mandatory = false, ParameterSetName = "AzureSend")]
		public bool? MultifactorRegistrationEnabled
		{
			get
			{
				return (bool?)base.Fields[PushNotificationAppSchema.MultifactorRegistrationEnabled];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.MultifactorRegistrationEnabled] = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000ED00 File Offset: 0x0000CF00
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x0000ED17 File Offset: 0x0000CF17
		[Parameter(Mandatory = false, ParameterSetName = "AzureSend")]
		public string PartitionName
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.PartitionName];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.PartitionName] = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000ED2A File Offset: 0x0000CF2A
		// (set) Token: 0x060003DA RID: 986 RVA: 0x0000ED41 File Offset: 0x0000CF41
		[Parameter(Mandatory = false, ParameterSetName = "AzureSend")]
		public bool? IsDefaultPartitionName
		{
			get
			{
				return (bool?)base.Fields[PushNotificationAppSchema.IsDefaultPartitionName];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.IsDefaultPartitionName] = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000ED59 File Offset: 0x0000CF59
		// (set) Token: 0x060003DC RID: 988 RVA: 0x0000ED70 File Offset: 0x0000CF70
		[Parameter(Mandatory = false, ParameterSetName = "AzureHubCreation")]
		public string AcsUserName
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.AuthenticationId];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.AuthenticationId] = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000ED83 File Offset: 0x0000CF83
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0000ED9A File Offset: 0x0000CF9A
		[Parameter(Mandatory = false, ParameterSetName = "AzureHubCreation")]
		public string AcsUserPassword
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.AuthenticationKey];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.AuthenticationKey] = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000EDAD File Offset: 0x0000CFAD
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0000EDC4 File Offset: 0x0000CFC4
		[Parameter(Mandatory = false, ParameterSetName = "AzureHubCreation")]
		public string AcsUriTemplate
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.Url];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.Url] = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000EDD7 File Offset: 0x0000CFD7
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000EDEE File Offset: 0x0000CFEE
		[Parameter(Mandatory = false, ParameterSetName = "AzureHubCreation")]
		public string AcsScopeUriTemplate
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.SecondaryUrl];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.SecondaryUrl] = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000EE01 File Offset: 0x0000D001
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetApp(this.Identity.ToString());
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000EE13 File Offset: 0x0000D013
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 357, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\PushNotifications\\SetPushNotificationApp.cs");
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000EF90 File Offset: 0x0000D190
		protected override IConfigurable PrepareDataObject()
		{
			PushNotificationApp dataObject = (PushNotificationApp)base.PrepareDataObject();
			if (base.ParameterSetName == "Apns")
			{
				this.SetIfModified<string>(PushNotificationAppSchema.AuthenticationKey, delegate(string x)
				{
					dataObject.AuthenticationKey = x;
				});
				this.SetIfModified<string>(PushNotificationAppSchema.AuthenticationKeyFallback, delegate(string x)
				{
					dataObject.AuthenticationKeyFallback = x;
				});
				this.SetIfModified<string>(PushNotificationAppSchema.Url, delegate(string x)
				{
					dataObject.Url = x;
				});
				this.SetIfModified<int?>(PushNotificationAppSchema.Port, delegate(int? x)
				{
					dataObject.Port = x;
				});
				this.SetIfModified<string>(PushNotificationAppSchema.SecondaryUrl, delegate(string x)
				{
					dataObject.SecondaryUrl = x;
				});
				this.SetIfModified<int?>(PushNotificationAppSchema.SecondaryPort, delegate(int? x)
				{
					dataObject.SecondaryPort = x;
				});
			}
			if (base.ParameterSetName == "Wns")
			{
				this.SetIfModified<string>(PushNotificationAppSchema.AuthenticationId, delegate(string x)
				{
					dataObject.AuthenticationId = x;
				});
				this.SetIfModified<string>(PushNotificationAppSchema.SecondaryUrl, delegate(string x)
				{
					dataObject.SecondaryUrl = x;
				});
				this.SetEncryptedAuthenticationKeyIfModified(dataObject);
			}
			if (base.ParameterSetName == "Gcm")
			{
				this.SetIfModified<string>(PushNotificationAppSchema.AuthenticationId, delegate(string x)
				{
					dataObject.AuthenticationId = x;
				});
				this.SetIfModified<string>(PushNotificationAppSchema.Url, delegate(string x)
				{
					dataObject.Url = x;
				});
				this.SetEncryptedAuthenticationKeyIfModified(dataObject);
			}
			if (base.ParameterSetName == "Proxy")
			{
				this.SetIfModified<string>(PushNotificationAppSchema.AuthenticationKey, delegate(string x)
				{
					dataObject.AuthenticationKey = x;
				});
				this.SetIfModified<string>(PushNotificationAppSchema.Url, delegate(string x)
				{
					dataObject.Url = x;
				});
			}
			if (base.ParameterSetName == "AzureSend")
			{
				this.SetIfModified<string>(PushNotificationAppSchema.AuthenticationId, delegate(string x)
				{
					dataObject.AuthenticationId = x;
				});
				this.SetIfModified<string>(PushNotificationAppSchema.Url, delegate(string x)
				{
					dataObject.Url = x;
				});
				this.SetIfModified<string>(PushNotificationAppSchema.UriTemplate, delegate(string x)
				{
					dataObject.UriTemplate = x;
				});
				this.SetIfModified<bool?>(PushNotificationAppSchema.RegistrationEnabled, delegate(bool? x)
				{
					dataObject.RegistrationEnabled = x;
				});
				this.SetIfModified<string>(PushNotificationAppSchema.RegistrationTemplate, delegate(string x)
				{
					dataObject.RegistrationTemplate = x;
				});
				this.SetIfModified<bool?>(PushNotificationAppSchema.MultifactorRegistrationEnabled, delegate(bool? x)
				{
					dataObject.MultifactorRegistrationEnabled = x;
				});
				this.SetIfModified<string>(PushNotificationAppSchema.PartitionName, delegate(string x)
				{
					dataObject.PartitionName = x;
				});
				this.SetIfModified<bool?>(PushNotificationAppSchema.IsDefaultPartitionName, delegate(bool? x)
				{
					dataObject.IsDefaultPartitionName = x;
				});
				this.SetEncryptedAuthenticationKeyIfModified(dataObject);
			}
			if (base.ParameterSetName == "AzureHubCreation")
			{
				this.SetIfModified<string>(PushNotificationAppSchema.AuthenticationId, delegate(string x)
				{
					dataObject.AuthenticationId = x;
				});
				this.SetIfModified<string>(PushNotificationAppSchema.Url, delegate(string x)
				{
					dataObject.Url = x;
				});
				this.SetIfModified<string>(PushNotificationAppSchema.UriTemplate, delegate(string x)
				{
					dataObject.UriTemplate = x;
				});
				this.SetIfModified<string>(PushNotificationAppSchema.SecondaryUrl, delegate(string x)
				{
					dataObject.SecondaryUrl = x;
				});
				this.SetEncryptedAuthenticationKeyIfModified(dataObject);
			}
			return dataObject;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000F3A0 File Offset: 0x0000D5A0
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

		// Token: 0x060003E7 RID: 999 RVA: 0x0000F3F0 File Offset: 0x0000D5F0
		protected override bool IsKnownException(Exception exception)
		{
			return exception is PushNotificationConfigurationException || base.IsKnownException(exception);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000F403 File Offset: 0x0000D603
		private void SetIfModified<T>(ADPropertyDefinition property, Action<T> setValue)
		{
			if (base.Fields.IsModified(property))
			{
				setValue((T)((object)base.Fields[property]));
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000F42C File Offset: 0x0000D62C
		private void SetEncryptedAuthenticationKeyIfModified(PushNotificationApp dataObject)
		{
			if (base.Fields.IsModified(PushNotificationAppSchema.AuthenticationKey))
			{
				dataObject.IsAuthenticationKeyEncrypted = new bool?(!this.UseClearTextAuthenticationKeys);
				if (dataObject.IsAuthenticationKeyEncrypted.Value)
				{
					PushNotificationDataProtector pushNotificationDataProtector = new PushNotificationDataProtector(null);
					dataObject.AuthenticationKey = pushNotificationDataProtector.Encrypt((string)base.Fields[PushNotificationAppSchema.AuthenticationKey]);
					return;
				}
				dataObject.AuthenticationKey = (string)base.Fields[PushNotificationAppSchema.AuthenticationKey];
			}
		}
	}
}
