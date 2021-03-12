using System;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x0200002B RID: 43
	public class AttendeeSchema : RecipientSchema
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x000033C1 File Offset: 0x000015C1
		public AttendeeSchema()
		{
			base.RegisterPropertyDefinition(AttendeeSchema.StaticStatusProperty);
			base.RegisterPropertyDefinition(AttendeeSchema.StaticTypeProperty);
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000033DF File Offset: 0x000015DF
		public TypedPropertyDefinition<ResponseStatus> StatusProperty
		{
			get
			{
				return AttendeeSchema.StaticStatusProperty;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000033E6 File Offset: 0x000015E6
		public TypedPropertyDefinition<AttendeeType> TypeProperty
		{
			get
			{
				return AttendeeSchema.StaticTypeProperty;
			}
		}

		// Token: 0x04000054 RID: 84
		private static readonly TypedPropertyDefinition<ResponseStatus> StaticStatusProperty = new TypedPropertyDefinition<ResponseStatus>("Attendee.Status", null, true);

		// Token: 0x04000055 RID: 85
		private static readonly TypedPropertyDefinition<AttendeeType> StaticTypeProperty = new TypedPropertyDefinition<AttendeeType>("Attendee.Type", (AttendeeType)0, true);
	}
}
