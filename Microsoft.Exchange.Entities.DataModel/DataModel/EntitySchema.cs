using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel
{
	// Token: 0x02000019 RID: 25
	public abstract class EntitySchema : TypeSchema
	{
		// Token: 0x0600004F RID: 79 RVA: 0x000028B7 File Offset: 0x00000AB7
		protected EntitySchema()
		{
			base.RegisterPropertyDefinition(EntitySchema.StaticIdProperty);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000028CA File Offset: 0x00000ACA
		public TypedPropertyDefinition<string> IdProperty
		{
			get
			{
				return EntitySchema.StaticIdProperty;
			}
		}

		// Token: 0x0400001E RID: 30
		private static readonly TypedPropertyDefinition<string> StaticIdProperty = new TypedPropertyDefinition<string>("Entity.Id", null, true);
	}
}
