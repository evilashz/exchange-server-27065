using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.UnifiedPolicy
{
	// Token: 0x02000A1D RID: 2589
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class UnifiedPolicySettingStatus : ADConfigurationObject
	{
		// Token: 0x0600779C RID: 30620 RVA: 0x001898F1 File Offset: 0x00187AF1
		public UnifiedPolicySettingStatus()
		{
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x17002ABB RID: 10939
		// (get) Token: 0x0600779D RID: 30621 RVA: 0x00189905 File Offset: 0x00187B05
		internal override string MostDerivedObjectClass
		{
			get
			{
				return UnifiedPolicySettingStatus.mostDerivedClass;
			}
		}

		// Token: 0x17002ABC RID: 10940
		// (get) Token: 0x0600779E RID: 30622 RVA: 0x0018990C File Offset: 0x00187B0C
		internal override ADObjectSchema Schema
		{
			get
			{
				return UnifiedPolicySettingStatus.schema;
			}
		}

		// Token: 0x17002ABD RID: 10941
		// (get) Token: 0x0600779F RID: 30623 RVA: 0x00189913 File Offset: 0x00187B13
		// (set) Token: 0x060077A0 RID: 30624 RVA: 0x00189925 File Offset: 0x00187B25
		public string SettingType
		{
			get
			{
				return this[UnifiedPolicySettingStatusSchema.SettingType] as string;
			}
			set
			{
				this[UnifiedPolicySettingStatusSchema.SettingType] = value;
			}
		}

		// Token: 0x17002ABE RID: 10942
		// (get) Token: 0x060077A1 RID: 30625 RVA: 0x00189933 File Offset: 0x00187B33
		// (set) Token: 0x060077A2 RID: 30626 RVA: 0x0018994A File Offset: 0x00187B4A
		public Guid? ParentObjectId
		{
			get
			{
				return new Guid?((Guid)this[UnifiedPolicySettingStatusSchema.ParentObjectId]);
			}
			set
			{
				this[UnifiedPolicySettingStatusSchema.ParentObjectId] = value;
			}
		}

		// Token: 0x17002ABF RID: 10943
		// (get) Token: 0x060077A3 RID: 30627 RVA: 0x0018995D File Offset: 0x00187B5D
		// (set) Token: 0x060077A4 RID: 30628 RVA: 0x0018996F File Offset: 0x00187B6F
		public string Container
		{
			get
			{
				return this[UnifiedPolicySettingStatusSchema.Container] as string;
			}
			set
			{
				this[UnifiedPolicySettingStatusSchema.Container] = value;
			}
		}

		// Token: 0x17002AC0 RID: 10944
		// (get) Token: 0x060077A5 RID: 30629 RVA: 0x0018997D File Offset: 0x00187B7D
		// (set) Token: 0x060077A6 RID: 30630 RVA: 0x0018998F File Offset: 0x00187B8F
		public Guid ObjectVersion
		{
			get
			{
				return (Guid)this[UnifiedPolicySettingStatusSchema.ObjectVersion];
			}
			set
			{
				this[UnifiedPolicySettingStatusSchema.ObjectVersion] = value;
			}
		}

		// Token: 0x17002AC1 RID: 10945
		// (get) Token: 0x060077A7 RID: 30631 RVA: 0x001899A2 File Offset: 0x00187BA2
		// (set) Token: 0x060077A8 RID: 30632 RVA: 0x001899B4 File Offset: 0x00187BB4
		public int ErrorCode
		{
			get
			{
				return (int)this[UnifiedPolicySettingStatusSchema.ErrorCode];
			}
			set
			{
				this[UnifiedPolicySettingStatusSchema.ErrorCode] = value;
			}
		}

		// Token: 0x17002AC2 RID: 10946
		// (get) Token: 0x060077A9 RID: 30633 RVA: 0x001899C7 File Offset: 0x00187BC7
		// (set) Token: 0x060077AA RID: 30634 RVA: 0x001899D9 File Offset: 0x00187BD9
		public string ErrorMessage
		{
			get
			{
				return this[UnifiedPolicySettingStatusSchema.ErrorMessage] as string;
			}
			set
			{
				this[UnifiedPolicySettingStatusSchema.ErrorMessage] = value;
			}
		}

		// Token: 0x17002AC3 RID: 10947
		// (get) Token: 0x060077AB RID: 30635 RVA: 0x001899E7 File Offset: 0x00187BE7
		// (set) Token: 0x060077AC RID: 30636 RVA: 0x001899F9 File Offset: 0x00187BF9
		public DateTime WhenProcessedUTC
		{
			get
			{
				return (DateTime)this[UnifiedPolicySettingStatusSchema.WhenProcessedUTC];
			}
			set
			{
				this[UnifiedPolicySettingStatusSchema.WhenProcessedUTC] = value;
			}
		}

		// Token: 0x17002AC4 RID: 10948
		// (get) Token: 0x060077AD RID: 30637 RVA: 0x00189A0C File Offset: 0x00187C0C
		// (set) Token: 0x060077AE RID: 30638 RVA: 0x00189A1E File Offset: 0x00187C1E
		internal StatusMode ObjectStatus
		{
			get
			{
				return (StatusMode)this[UnifiedPolicySettingStatusSchema.ObjectStatus];
			}
			set
			{
				this[UnifiedPolicySettingStatusSchema.ObjectStatus] = value;
			}
		}

		// Token: 0x17002AC5 RID: 10949
		// (get) Token: 0x060077AF RID: 30639 RVA: 0x00189A31 File Offset: 0x00187C31
		// (set) Token: 0x060077B0 RID: 30640 RVA: 0x00189A43 File Offset: 0x00187C43
		public string AdditionalDiagnostics
		{
			get
			{
				return (string)this[UnifiedPolicySettingStatusSchema.AdditionalDiagnostics];
			}
			set
			{
				this[UnifiedPolicySettingStatusSchema.AdditionalDiagnostics] = value;
			}
		}

		// Token: 0x17002AC6 RID: 10950
		// (get) Token: 0x060077B1 RID: 30641 RVA: 0x00189A51 File Offset: 0x00187C51
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x04004CA2 RID: 19618
		private static readonly UnifiedPolicySettingStatusSchema schema = ObjectSchema.GetInstance<UnifiedPolicySettingStatusSchema>();

		// Token: 0x04004CA3 RID: 19619
		private static string mostDerivedClass = "msExchUnifiedPolicySettingStatus";
	}
}
