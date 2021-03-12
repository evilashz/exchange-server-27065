using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200056B RID: 1387
	[Serializable]
	public class RoutingGroup : ADLegacyVersionableObject
	{
		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x06003E2F RID: 15919 RVA: 0x000EC287 File Offset: 0x000EA487
		internal override ADObjectSchema Schema
		{
			get
			{
				return RoutingGroup.schema;
			}
		}

		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x06003E30 RID: 15920 RVA: 0x000EC28E File Offset: 0x000EA48E
		internal override string MostDerivedObjectClass
		{
			get
			{
				return RoutingGroup.mostDerivedClass;
			}
		}

		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x06003E31 RID: 15921 RVA: 0x000EC295 File Offset: 0x000EA495
		// (set) Token: 0x06003E32 RID: 15922 RVA: 0x000EC2A7 File Offset: 0x000EA4A7
		public ADObjectId RoutingMasterDN
		{
			get
			{
				return (ADObjectId)this[RoutingGroupSchema.RoutingMasterDN];
			}
			internal set
			{
				this[RoutingGroupSchema.RoutingMasterDN] = value;
			}
		}

		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x06003E33 RID: 15923 RVA: 0x000EC2B5 File Offset: 0x000EA4B5
		// (set) Token: 0x06003E34 RID: 15924 RVA: 0x000EC2C7 File Offset: 0x000EA4C7
		public MultiValuedProperty<ADObjectId> RoutingMembersDN
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[RoutingGroupSchema.RoutingGroupMembersDN];
			}
			internal set
			{
				this[RoutingGroupSchema.RoutingGroupMembersDN] = value;
			}
		}

		// Token: 0x06003E35 RID: 15925 RVA: 0x000EC2D5 File Offset: 0x000EA4D5
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(ADConfigurationObjectSchema.SystemFlags))
			{
				this[ADConfigurationObjectSchema.SystemFlags] = SystemFlagsEnum.None;
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x04002A20 RID: 10784
		private static RoutingGroupSchema schema = ObjectSchema.GetInstance<RoutingGroupSchema>();

		// Token: 0x04002A21 RID: 10785
		private static string mostDerivedClass = "msExchRoutingGroup";

		// Token: 0x04002A22 RID: 10786
		public static readonly string DefaultName = "Exchange Routing Group (DWBGZMFD01QNBJR)";
	}
}
