using System;
using System.Management.Automation;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x02000103 RID: 259
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class TenantIPFilterSetting : ADConfigurationObject
	{
		// Token: 0x06000A25 RID: 2597 RVA: 0x0001EC34 File Offset: 0x0001CE34
		public TenantIPFilterSetting()
		{
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0001EC3C File Offset: 0x0001CE3C
		public TenantIPFilterSetting(SerializationInfo info, StreamingContext context)
		{
			this.AllowedIPRanges = (MultiValuedProperty<IPRange>)info.GetValue("AllowedIPRanges", typeof(MultiValuedProperty<IPRange>));
			this.BlockedIPRanges = (MultiValuedProperty<IPRange>)info.GetValue("BlockedIPRanges", typeof(MultiValuedProperty<IPRange>));
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0001EC8F File Offset: 0x0001CE8F
		// (set) Token: 0x06000A28 RID: 2600 RVA: 0x0001ECA1 File Offset: 0x0001CEA1
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPRange> AllowedIPRanges
		{
			get
			{
				return (MultiValuedProperty<IPRange>)this[TenantIPFilterSettingSchema.AllowedIPRanges];
			}
			set
			{
				this[TenantIPFilterSettingSchema.AllowedIPRanges] = value;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0001ECAF File Offset: 0x0001CEAF
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x0001ECC1 File Offset: 0x0001CEC1
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPRange> BlockedIPRanges
		{
			get
			{
				return (MultiValuedProperty<IPRange>)this[TenantIPFilterSettingSchema.BlockedIPRanges];
			}
			set
			{
				this[TenantIPFilterSettingSchema.BlockedIPRanges] = value;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0001ECCF File Offset: 0x0001CECF
		internal override ADObjectSchema Schema
		{
			get
			{
				return TenantIPFilterSetting.schema;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0001ECD6 File Offset: 0x0001CED6
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchTenantIPFilterSetting";
			}
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0001ECDD File Offset: 0x0001CEDD
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("AllowedIPRanges", this.AllowedIPRanges);
			info.AddValue("BlockedIPRanges", this.BlockedIPRanges);
		}

		// Token: 0x0400053F RID: 1343
		private const string AllowedIPRangesPropertyName = "AllowedIPRanges";

		// Token: 0x04000540 RID: 1344
		private const string BlockedIPRangesPropertyName = "BlockedIPRanges";

		// Token: 0x04000541 RID: 1345
		private const string MostDerivedClass = "msExchTenantIPFilterSetting";

		// Token: 0x04000542 RID: 1346
		private static TenantIPFilterSettingSchema schema = ObjectSchema.GetInstance<TenantIPFilterSettingSchema>();
	}
}
