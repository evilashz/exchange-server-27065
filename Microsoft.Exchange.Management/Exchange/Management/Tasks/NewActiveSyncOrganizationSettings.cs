using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200007C RID: 124
	[Cmdlet("New", "ActiveSyncOrganizationSettings", SupportsShouldProcess = true)]
	public sealed class NewActiveSyncOrganizationSettings : NewMultitenancyFixedNameSystemConfigurationObjectTask<ActiveSyncOrganizationSettings>
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00010025 File Offset: 0x0000E225
		// (set) Token: 0x060003CE RID: 974 RVA: 0x00010032 File Offset: 0x0000E232
		[Parameter(Mandatory = false)]
		public DeviceAccessLevel DefaultAccessLevel
		{
			get
			{
				return this.DataObject.DefaultAccessLevel;
			}
			set
			{
				this.DataObject.DefaultAccessLevel = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00010040 File Offset: 0x0000E240
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x0001004D File Offset: 0x0000E24D
		[Parameter(Mandatory = false)]
		public string UserMailInsert
		{
			get
			{
				return this.DataObject.UserMailInsert;
			}
			set
			{
				this.DataObject.UserMailInsert = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0001005B File Offset: 0x0000E25B
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x00010068 File Offset: 0x0000E268
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SmtpAddress> AdminMailRecipients
		{
			get
			{
				return this.DataObject.AdminMailRecipients;
			}
			set
			{
				this.DataObject.AdminMailRecipients = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00010076 File Offset: 0x0000E276
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x00010083 File Offset: 0x0000E283
		[Parameter(Mandatory = false)]
		public string OtaNotificationMailInsert
		{
			get
			{
				return this.DataObject.OtaNotificationMailInsert;
			}
			set
			{
				this.DataObject.OtaNotificationMailInsert = value;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00010091 File Offset: 0x0000E291
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x0001009E File Offset: 0x0000E29E
		[Parameter(Mandatory = false)]
		public bool AllowAccessForUnSupportedPlatform
		{
			get
			{
				return this.DataObject.AllowAccessForUnSupportedPlatform;
			}
			set
			{
				this.DataObject.AllowAccessForUnSupportedPlatform = value;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x000100AC File Offset: 0x0000E2AC
		protected override IConfigurable PrepareDataObject()
		{
			ActiveSyncOrganizationSettings activeSyncOrganizationSettings = (ActiveSyncOrganizationSettings)base.PrepareDataObject();
			activeSyncOrganizationSettings.SetId((IConfigurationSession)base.DataSession, this.DataObject.Name);
			return activeSyncOrganizationSettings;
		}
	}
}
