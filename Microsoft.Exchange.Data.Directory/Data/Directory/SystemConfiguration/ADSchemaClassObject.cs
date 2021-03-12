using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000387 RID: 903
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public sealed class ADSchemaClassObject : ADNonExchangeObject
	{
		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x0600296E RID: 10606 RVA: 0x000AE48D File Offset: 0x000AC68D
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADSchemaClassObject.schema;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x0600296F RID: 10607 RVA: 0x000AE494 File Offset: 0x000AC694
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADSchemaClassObject.mostDerivedClass;
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06002970 RID: 10608 RVA: 0x000AE49B File Offset: 0x000AC69B
		public string DisplayName
		{
			get
			{
				return (string)this[ADSchemaObjectSchema.DisplayName];
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06002971 RID: 10609 RVA: 0x000AE4AD File Offset: 0x000AC6AD
		public Guid SchemaIDGuid
		{
			get
			{
				return (Guid)this[ADSchemaObjectSchema.SchemaIDGuid];
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06002972 RID: 10610 RVA: 0x000AE4BF File Offset: 0x000AC6BF
		public MultiValuedProperty<string> MayContain
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADSchemaObjectSchema.MayContain];
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06002973 RID: 10611 RVA: 0x000AE4D1 File Offset: 0x000AC6D1
		public MultiValuedProperty<string> SystemMayContain
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADSchemaObjectSchema.SystemMayContain];
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06002974 RID: 10612 RVA: 0x000AE4E3 File Offset: 0x000AC6E3
		public MultiValuedProperty<string> MustContain
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADSchemaObjectSchema.MustContain];
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06002975 RID: 10613 RVA: 0x000AE4F5 File Offset: 0x000AC6F5
		public MultiValuedProperty<string> SystemMustContain
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADSchemaObjectSchema.SystemMustContain];
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06002976 RID: 10614 RVA: 0x000AE507 File Offset: 0x000AC707
		public ADObjectId DefaultObjectCategory
		{
			get
			{
				return (ADObjectId)this[ADSchemaObjectSchema.DefaultObjectCategory];
			}
		}

		// Token: 0x0400195A RID: 6490
		private static ADSchemaObjectSchema schema = ObjectSchema.GetInstance<ADSchemaObjectSchema>();

		// Token: 0x0400195B RID: 6491
		private static string mostDerivedClass = "classSchema";
	}
}
