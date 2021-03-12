using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x02000119 RID: 281
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ADVirusScanSetting : ADObject
	{
		// Token: 0x06000AD0 RID: 2768 RVA: 0x00021584 File Offset: 0x0001F784
		public ADVirusScanSetting()
		{
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002158C File Offset: 0x0001F78C
		internal ADVirusScanSetting(IConfigurationSession session, string tenantId)
		{
			this.m_Session = session;
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x000215A7 File Offset: 0x0001F7A7
		internal ADVirusScanSetting(string tenantId)
		{
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x000215BB File Offset: 0x0001F7BB
		public override ObjectId Identity
		{
			get
			{
				return base.Id;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x000215C3 File Offset: 0x0001F7C3
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x000215D5 File Offset: 0x0001F7D5
		public ObjectId ConfigurationId
		{
			get
			{
				return (ObjectId)this[ADVirusScanSettingSchema.ConfigurationIdProp];
			}
			set
			{
				this[ADVirusScanSettingSchema.ConfigurationIdProp] = value;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x000215E3 File Offset: 0x0001F7E3
		// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x000215F5 File Offset: 0x0001F7F5
		[Parameter(Mandatory = false)]
		public VirusScanFlags Flags
		{
			get
			{
				return (VirusScanFlags)this[ADVirusScanSettingSchema.FlagsProp];
			}
			set
			{
				this[ADVirusScanSettingSchema.FlagsProp] = value;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00021608 File Offset: 0x0001F808
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x0002161A File Offset: 0x0001F81A
		[Parameter(Mandatory = false)]
		public int SenderWarningNotificationId
		{
			get
			{
				return (int)this[ADVirusScanSettingSchema.SenderWarningNotificationIdProp];
			}
			set
			{
				this[ADVirusScanSettingSchema.SenderWarningNotificationIdProp] = value;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0002162D File Offset: 0x0001F82D
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x0002163F File Offset: 0x0001F83F
		[Parameter(Mandatory = false)]
		public int SenderRejectionNotificationId
		{
			get
			{
				return (int)this[ADVirusScanSettingSchema.SenderRejectionNotificationIdProp];
			}
			set
			{
				this[ADVirusScanSettingSchema.SenderRejectionNotificationIdProp] = value;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00021652 File Offset: 0x0001F852
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x00021664 File Offset: 0x0001F864
		[Parameter(Mandatory = false)]
		public int RecipientNotificationId
		{
			get
			{
				return (int)this[ADVirusScanSettingSchema.RecipientNotificationIdProp];
			}
			set
			{
				this[ADVirusScanSettingSchema.RecipientNotificationIdProp] = value;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00021677 File Offset: 0x0001F877
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x00021689 File Offset: 0x0001F889
		[Parameter(Mandatory = false)]
		public string AdminNotificationAddress
		{
			get
			{
				return (string)this[ADVirusScanSettingSchema.AdminNotificationAddressProp];
			}
			set
			{
				this[ADVirusScanSettingSchema.AdminNotificationAddressProp] = value;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00021697 File Offset: 0x0001F897
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x000216A9 File Offset: 0x0001F8A9
		[Parameter(Mandatory = false)]
		public string OutboundAdminNotificationAddress
		{
			get
			{
				return (string)this[ADVirusScanSettingSchema.OutboundAdminNotificationAddressProp];
			}
			set
			{
				this[ADVirusScanSettingSchema.OutboundAdminNotificationAddressProp] = value;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x000216B7 File Offset: 0x0001F8B7
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADVirusScanSetting.schema;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x000216BE File Offset: 0x0001F8BE
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADVirusScanSetting.mostDerivedClass;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x000216C5 File Offset: 0x0001F8C5
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x000216CC File Offset: 0x0001F8CC
		internal override bool ShouldValidatePropertyLinkInSameOrganization(ADPropertyDefinition property)
		{
			return false;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x000216CF File Offset: 0x0001F8CF
		internal override void InitializeSchema()
		{
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000216D1 File Offset: 0x0001F8D1
		protected override void ValidateWrite(List<ValidationError> errors)
		{
		}

		// Token: 0x0400058D RID: 1421
		private static readonly ADVirusScanSettingSchema schema = ObjectSchema.GetInstance<ADVirusScanSettingSchema>();

		// Token: 0x0400058E RID: 1422
		private static string mostDerivedClass = "ADVirusScanSetting";
	}
}
