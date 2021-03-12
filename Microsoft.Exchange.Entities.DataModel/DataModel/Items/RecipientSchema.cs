using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x0200002A RID: 42
	public class RecipientSchema : TypeSchema
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x0000334E File Offset: 0x0000154E
		public RecipientSchema()
		{
			base.RegisterPropertyDefinition(RecipientSchema.StaticEmailAddressProperty);
			base.RegisterPropertyDefinition(RecipientSchema.StaticNameProperty);
			base.RegisterPropertyDefinition(RecipientSchema.StaticRoutingTypeProperty);
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003377 File Offset: 0x00001577
		public TypedPropertyDefinition<string> EmailAddressProperty
		{
			get
			{
				return RecipientSchema.StaticEmailAddressProperty;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000337E File Offset: 0x0000157E
		public TypedPropertyDefinition<string> NameProperty
		{
			get
			{
				return RecipientSchema.StaticNameProperty;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003385 File Offset: 0x00001585
		public TypedPropertyDefinition<string> RoutingTypeProperty
		{
			get
			{
				return RecipientSchema.StaticRoutingTypeProperty;
			}
		}

		// Token: 0x04000051 RID: 81
		private static readonly TypedPropertyDefinition<string> StaticEmailAddressProperty = new TypedPropertyDefinition<string>("Recipient.EmailAddress", null, false);

		// Token: 0x04000052 RID: 82
		private static readonly TypedPropertyDefinition<string> StaticNameProperty = new TypedPropertyDefinition<string>("Recipient.Name", null, false);

		// Token: 0x04000053 RID: 83
		private static readonly TypedPropertyDefinition<string> StaticRoutingTypeProperty = new TypedPropertyDefinition<string>("Recipient.RoutingType", null, false);
	}
}
