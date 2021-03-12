using System;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Messaging
{
	// Token: 0x02000063 RID: 99
	public abstract class EmailMessageSchema : ItemSchema
	{
		// Token: 0x06000308 RID: 776 RVA: 0x00006455 File Offset: 0x00004655
		protected EmailMessageSchema()
		{
			base.RegisterPropertyDefinition(EmailMessageSchema.StaticIsReadProperty);
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00006468 File Offset: 0x00004668
		public TypedPropertyDefinition<bool> IsReadProperty
		{
			get
			{
				return EmailMessageSchema.StaticIsReadProperty;
			}
		}

		// Token: 0x04000166 RID: 358
		private static readonly TypedPropertyDefinition<bool> StaticIsReadProperty = new TypedPropertyDefinition<bool>("EmailMessage.IsRead", false, true);
	}
}
