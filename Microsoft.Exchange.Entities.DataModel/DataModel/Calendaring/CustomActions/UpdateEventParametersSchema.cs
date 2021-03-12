using System;
using System.Collections.Generic;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions
{
	// Token: 0x02000043 RID: 67
	public sealed class UpdateEventParametersSchema : TypeSchema
	{
		// Token: 0x0600015F RID: 351 RVA: 0x00003EF8 File Offset: 0x000020F8
		public UpdateEventParametersSchema()
		{
			base.RegisterPropertyDefinition(UpdateEventParametersSchema.StaticAttendeesToAddProperty);
			base.RegisterPropertyDefinition(UpdateEventParametersSchema.StaticAttendeesToRemoveProperty);
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00003F16 File Offset: 0x00002116
		public TypedPropertyDefinition<IList<Attendee>> AttendeesToAddProperty
		{
			get
			{
				return UpdateEventParametersSchema.StaticAttendeesToAddProperty;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00003F1D File Offset: 0x0000211D
		public TypedPropertyDefinition<IList<string>> AttendeesToRemoveProperty
		{
			get
			{
				return UpdateEventParametersSchema.StaticAttendeesToRemoveProperty;
			}
		}

		// Token: 0x0400008B RID: 139
		private static readonly TypedPropertyDefinition<IList<Attendee>> StaticAttendeesToAddProperty = new TypedPropertyDefinition<IList<Attendee>>("UpdateEvent.AttendeesToAdd", null, true);

		// Token: 0x0400008C RID: 140
		private static readonly TypedPropertyDefinition<IList<string>> StaticAttendeesToRemoveProperty = new TypedPropertyDefinition<IList<string>>("UpdateEvent.AttendeesToRemove", null, true);
	}
}
