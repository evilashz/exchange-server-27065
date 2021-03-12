using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005E9 RID: 1513
	[Serializable]
	public sealed class UceContentFilter : ADConfigurationObject
	{
		// Token: 0x170017AB RID: 6059
		// (get) Token: 0x060047E2 RID: 18402 RVA: 0x00109316 File Offset: 0x00107516
		internal override ADObjectSchema Schema
		{
			get
			{
				return UceContentFilter.schema;
			}
		}

		// Token: 0x170017AC RID: 6060
		// (get) Token: 0x060047E3 RID: 18403 RVA: 0x0010931D File Offset: 0x0010751D
		internal override ADObjectId ParentPath
		{
			get
			{
				return UceContentFilter.parentPath;
			}
		}

		// Token: 0x170017AD RID: 6061
		// (get) Token: 0x060047E4 RID: 18404 RVA: 0x00109324 File Offset: 0x00107524
		// (set) Token: 0x060047E5 RID: 18405 RVA: 0x0010933B File Offset: 0x0010753B
		public int SCLJunkThreshold
		{
			get
			{
				return (int)this.propertyBag[UceContentFilterSchema.SCLJunkThreshold];
			}
			internal set
			{
				this.propertyBag[UceContentFilterSchema.SCLJunkThreshold] = value;
			}
		}

		// Token: 0x170017AE RID: 6062
		// (get) Token: 0x060047E6 RID: 18406 RVA: 0x00109353 File Offset: 0x00107553
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchUce";
			}
		}

		// Token: 0x0400318D RID: 12685
		private const string MostDerivedClass = "msExchUce";

		// Token: 0x0400318E RID: 12686
		private static readonly ADObjectId parentPath = new ADObjectId("CN=Message Delivery,CN=Global Settings");

		// Token: 0x0400318F RID: 12687
		private static readonly UceContentFilterSchema schema = ObjectSchema.GetInstance<UceContentFilterSchema>();
	}
}
