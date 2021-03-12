using System;
using System.Collections.Generic;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions
{
	// Token: 0x02000042 RID: 66
	public sealed class UpdateEventParameters : SchematizedObject<UpdateEventParametersSchema>
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00003EA2 File Offset: 0x000020A2
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00003EB5 File Offset: 0x000020B5
		public IList<Attendee> AttendeesToAdd
		{
			get
			{
				return base.GetPropertyValueOrDefault<IList<Attendee>>(base.Schema.AttendeesToAddProperty);
			}
			set
			{
				base.SetPropertyValue<IList<Attendee>>(base.Schema.AttendeesToAddProperty, value);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00003EC9 File Offset: 0x000020C9
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00003EDC File Offset: 0x000020DC
		public IList<string> AttendeesToRemove
		{
			get
			{
				return base.GetPropertyValueOrDefault<IList<string>>(base.Schema.AttendeesToRemoveProperty);
			}
			set
			{
				base.SetPropertyValue<IList<string>>(base.Schema.AttendeesToRemoveProperty, value);
			}
		}
	}
}
