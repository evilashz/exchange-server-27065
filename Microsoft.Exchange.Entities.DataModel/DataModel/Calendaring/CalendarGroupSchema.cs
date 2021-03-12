using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x02000033 RID: 51
	public sealed class CalendarGroupSchema : StorageEntitySchema
	{
		// Token: 0x06000107 RID: 263 RVA: 0x00003785 File Offset: 0x00001985
		public CalendarGroupSchema()
		{
			base.RegisterPropertyDefinition(CalendarGroupSchema.StaticClassIdProperty);
			base.RegisterPropertyDefinition(CalendarGroupSchema.StaticNameProperty);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000037A3 File Offset: 0x000019A3
		public TypedPropertyDefinition<Guid> ClassIdProperty
		{
			get
			{
				return CalendarGroupSchema.StaticClassIdProperty;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000037AA File Offset: 0x000019AA
		public TypedPropertyDefinition<string> NameProperty
		{
			get
			{
				return CalendarGroupSchema.StaticNameProperty;
			}
		}

		// Token: 0x0400006E RID: 110
		private static readonly TypedPropertyDefinition<Guid> StaticClassIdProperty = new TypedPropertyDefinition<Guid>("CalendarGroup.ClassId", default(Guid), true);

		// Token: 0x0400006F RID: 111
		private static readonly TypedPropertyDefinition<string> StaticNameProperty = new TypedPropertyDefinition<string>("CalendarGroup.Name", null, true);
	}
}
