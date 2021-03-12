using System;
using System.Collections.Generic;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions
{
	// Token: 0x0200003E RID: 62
	public sealed class ForwardEventParametersSchema : EventWorkflowParametersSchema
	{
		// Token: 0x06000140 RID: 320 RVA: 0x00003CD6 File Offset: 0x00001ED6
		public ForwardEventParametersSchema()
		{
			base.RegisterPropertyDefinition(ForwardEventParametersSchema.StaticForwardeesProperty);
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00003CE9 File Offset: 0x00001EE9
		public TypedPropertyDefinition<IList<Recipient<RecipientSchema>>> ForwardeesProperty
		{
			get
			{
				return ForwardEventParametersSchema.StaticForwardeesProperty;
			}
		}

		// Token: 0x04000085 RID: 133
		private static readonly TypedPropertyDefinition<IList<Recipient<RecipientSchema>>> StaticForwardeesProperty = new TypedPropertyDefinition<IList<Recipient<RecipientSchema>>>("ForwardEvent.Forwardees", null, true);
	}
}
