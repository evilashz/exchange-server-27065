using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000316 RID: 790
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ActiveSyncOrganizationSettings : ADContainer
	{
		// Token: 0x06002449 RID: 9289 RVA: 0x0009B796 File Offset: 0x00099996
		public ActiveSyncOrganizationSettings()
		{
			this.Name = "Mobile Mailbox Settings";
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x0600244A RID: 9290 RVA: 0x0009B7A9 File Offset: 0x000999A9
		// (set) Token: 0x0600244B RID: 9291 RVA: 0x0009B7BB File Offset: 0x000999BB
		[Parameter(Mandatory = false)]
		public DeviceAccessLevel DefaultAccessLevel
		{
			get
			{
				return (DeviceAccessLevel)this[ActiveSyncOrganizationSettingsSchema.AccessLevel];
			}
			set
			{
				this[ActiveSyncOrganizationSettingsSchema.AccessLevel] = value;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x0600244C RID: 9292 RVA: 0x0009B7CE File Offset: 0x000999CE
		// (set) Token: 0x0600244D RID: 9293 RVA: 0x0009B7E0 File Offset: 0x000999E0
		[Parameter(Mandatory = false)]
		public string UserMailInsert
		{
			get
			{
				return (string)this[ActiveSyncOrganizationSettingsSchema.UserMailInsert];
			}
			set
			{
				this[ActiveSyncOrganizationSettingsSchema.UserMailInsert] = value;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x0600244E RID: 9294 RVA: 0x0009B7EE File Offset: 0x000999EE
		// (set) Token: 0x0600244F RID: 9295 RVA: 0x0009B800 File Offset: 0x00099A00
		[Parameter(Mandatory = false)]
		public bool AllowAccessForUnSupportedPlatform
		{
			get
			{
				return (bool)this[ActiveSyncOrganizationSettingsSchema.AllowAccessForUnSupportedPlatform];
			}
			set
			{
				this[ActiveSyncOrganizationSettingsSchema.AllowAccessForUnSupportedPlatform] = value;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002450 RID: 9296 RVA: 0x0009B813 File Offset: 0x00099A13
		// (set) Token: 0x06002451 RID: 9297 RVA: 0x0009B825 File Offset: 0x00099A25
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SmtpAddress> AdminMailRecipients
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[ActiveSyncOrganizationSettingsSchema.AdminMailRecipients];
			}
			set
			{
				this[ActiveSyncOrganizationSettingsSchema.AdminMailRecipients] = value;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06002452 RID: 9298 RVA: 0x0009B833 File Offset: 0x00099A33
		// (set) Token: 0x06002453 RID: 9299 RVA: 0x0009B845 File Offset: 0x00099A45
		[Parameter(Mandatory = false)]
		public string OtaNotificationMailInsert
		{
			get
			{
				return (string)this[ActiveSyncOrganizationSettingsSchema.OtaNotificationMailInsert];
			}
			set
			{
				this[ActiveSyncOrganizationSettingsSchema.OtaNotificationMailInsert] = value;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06002454 RID: 9300 RVA: 0x0009B853 File Offset: 0x00099A53
		// (set) Token: 0x06002455 RID: 9301 RVA: 0x0009B865 File Offset: 0x00099A65
		[Parameter(Mandatory = false)]
		public ActiveSyncDeviceFilterArray DeviceFiltering
		{
			get
			{
				return (ActiveSyncDeviceFilterArray)this[ActiveSyncOrganizationSettingsSchema.DeviceFiltering];
			}
			set
			{
				this[ActiveSyncOrganizationSettingsSchema.DeviceFiltering] = value;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06002456 RID: 9302 RVA: 0x0009B873 File Offset: 0x00099A73
		// (set) Token: 0x06002457 RID: 9303 RVA: 0x0009B87B File Offset: 0x00099A7B
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06002458 RID: 9304 RVA: 0x0009B884 File Offset: 0x00099A84
		internal override ADObjectSchema Schema
		{
			get
			{
				return ActiveSyncOrganizationSettings.schema;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002459 RID: 9305 RVA: 0x0009B88B File Offset: 0x00099A8B
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchMobileMailboxSettings";
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x0009B892 File Offset: 0x00099A92
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x0600245B RID: 9307 RVA: 0x0009B899 File Offset: 0x00099A99
		// (set) Token: 0x0600245C RID: 9308 RVA: 0x0009B8A1 File Offset: 0x00099AA1
		public bool IsIntuneManaged { get; set; }

		// Token: 0x0400167D RID: 5757
		public const string ContainerName = "Mobile Mailbox Settings";

		// Token: 0x0400167E RID: 5758
		internal const string MostDerivedClassName = "msExchMobileMailboxSettings";

		// Token: 0x0400167F RID: 5759
		private static ActiveSyncOrganizationSettingsSchema schema = ObjectSchema.GetInstance<ActiveSyncOrganizationSettingsSchema>();
	}
}
