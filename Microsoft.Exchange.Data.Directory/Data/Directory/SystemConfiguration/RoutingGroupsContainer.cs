using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200056F RID: 1391
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public class RoutingGroupsContainer : ADContainer
	{
		// Token: 0x17001403 RID: 5123
		// (get) Token: 0x06003E4C RID: 15948 RVA: 0x000EC685 File Offset: 0x000EA885
		internal override ADObjectSchema Schema
		{
			get
			{
				return RoutingGroupsContainer.schema;
			}
		}

		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x06003E4D RID: 15949 RVA: 0x000EC68C File Offset: 0x000EA88C
		internal override string MostDerivedObjectClass
		{
			get
			{
				return RoutingGroupsContainer.mostDerivedClass;
			}
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x000EC693 File Offset: 0x000EA893
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(ADConfigurationObjectSchema.SystemFlags))
			{
				this[ADConfigurationObjectSchema.SystemFlags] = SystemFlagsEnum.None;
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x04002A2D RID: 10797
		private static RoutingGroupsContainerSchema schema = ObjectSchema.GetInstance<RoutingGroupsContainerSchema>();

		// Token: 0x04002A2E RID: 10798
		private static string mostDerivedClass = "msExchRoutingGroupContainer";

		// Token: 0x04002A2F RID: 10799
		public static readonly string DefaultName = "Routing Groups";
	}
}
