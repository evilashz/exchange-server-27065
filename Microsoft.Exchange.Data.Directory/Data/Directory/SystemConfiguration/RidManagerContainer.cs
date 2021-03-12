using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000567 RID: 1383
	[Serializable]
	public class RidManagerContainer : ADNonExchangeObject
	{
		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x06003E10 RID: 15888 RVA: 0x000EBDF7 File Offset: 0x000E9FF7
		internal override ADObjectSchema Schema
		{
			get
			{
				return RidManagerContainer.schema;
			}
		}

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x06003E11 RID: 15889 RVA: 0x000EBDFE File Offset: 0x000E9FFE
		internal override string MostDerivedObjectClass
		{
			get
			{
				return RidManagerContainer.mostDerivedClass;
			}
		}

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x06003E13 RID: 15891 RVA: 0x000EBE0D File Offset: 0x000EA00D
		public ADObjectId FsmoRoleOwner
		{
			get
			{
				return (ADObjectId)this[RidManagerContainerSchema.FsmoRoleOwner];
			}
		}

		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x06003E14 RID: 15892 RVA: 0x000EBE1F File Offset: 0x000EA01F
		public MultiValuedProperty<string> ReplicationAttributeMetadata
		{
			get
			{
				return (MultiValuedProperty<string>)this[RidManagerContainerSchema.ReplicationAttributeMetadata];
			}
		}

		// Token: 0x04002A11 RID: 10769
		private static RidManagerContainerSchema schema = ObjectSchema.GetInstance<RidManagerContainerSchema>();

		// Token: 0x04002A12 RID: 10770
		private static string mostDerivedClass = "rIDManager";
	}
}
