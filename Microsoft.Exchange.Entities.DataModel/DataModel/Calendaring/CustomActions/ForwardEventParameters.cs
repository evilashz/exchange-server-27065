using System;
using System.Collections.Generic;
using Microsoft.Exchange.Entities.DataModel.Items;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions
{
	// Token: 0x0200003D RID: 61
	public sealed class ForwardEventParameters : EventWorkflowParameters<ForwardEventParametersSchema>
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00003CA7 File Offset: 0x00001EA7
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00003CBA File Offset: 0x00001EBA
		public IList<Recipient<RecipientSchema>> Forwardees
		{
			get
			{
				return base.GetPropertyValueOrDefault<IList<Recipient<RecipientSchema>>>(base.Schema.ForwardeesProperty);
			}
			set
			{
				base.SetPropertyValue<IList<Recipient<RecipientSchema>>>(base.Schema.ForwardeesProperty, value);
			}
		}
	}
}
