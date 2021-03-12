using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000306 RID: 774
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ADContainer : ADConfigurationObject
	{
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x0009AE51 File Offset: 0x00099051
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADContainer.schema;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x0009AE58 File Offset: 0x00099058
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADContainer.MostDerivedClass;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x0009AE5F File Offset: 0x0009905F
		// (set) Token: 0x060023F0 RID: 9200 RVA: 0x0009AE76 File Offset: 0x00099076
		public MultiValuedProperty<DNWithBinary> OtherWellKnownObjects
		{
			get
			{
				return (MultiValuedProperty<DNWithBinary>)this.propertyBag[ADContainerSchema.OtherWellKnownObjects];
			}
			internal set
			{
				this.propertyBag[ADContainerSchema.OtherWellKnownObjects] = value;
			}
		}

		// Token: 0x04001642 RID: 5698
		private static ADContainerSchema schema = ObjectSchema.GetInstance<ADContainerSchema>();

		// Token: 0x04001643 RID: 5699
		internal static string MostDerivedClass = "container";
	}
}
