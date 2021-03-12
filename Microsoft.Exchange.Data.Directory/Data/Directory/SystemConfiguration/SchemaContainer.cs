using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000577 RID: 1399
	[Serializable]
	public class SchemaContainer : ADNonExchangeObject
	{
		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x06003E9C RID: 16028 RVA: 0x000ED48A File Offset: 0x000EB68A
		internal override ADObjectSchema Schema
		{
			get
			{
				return SchemaContainer.schema;
			}
		}

		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x06003E9D RID: 16029 RVA: 0x000ED491 File Offset: 0x000EB691
		internal override string MostDerivedObjectClass
		{
			get
			{
				return SchemaContainer.mostDerivedClass;
			}
		}

		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x06003E9F RID: 16031 RVA: 0x000ED4A0 File Offset: 0x000EB6A0
		public ADObjectId FsmoRoleOwner
		{
			get
			{
				return (ADObjectId)this[SchemaContainerSchema.FsmoRoleOwner];
			}
		}

		// Token: 0x04002A52 RID: 10834
		private static SchemaContainerSchema schema = ObjectSchema.GetInstance<SchemaContainerSchema>();

		// Token: 0x04002A53 RID: 10835
		private static string mostDerivedClass = "dMD";
	}
}
