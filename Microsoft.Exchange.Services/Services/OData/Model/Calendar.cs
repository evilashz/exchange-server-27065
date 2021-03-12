using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E64 RID: 3684
	internal class Calendar : Entity
	{
		// Token: 0x170015FC RID: 5628
		// (get) Token: 0x06005FA7 RID: 24487 RVA: 0x0012AA17 File Offset: 0x00128C17
		// (set) Token: 0x06005FA8 RID: 24488 RVA: 0x0012AA29 File Offset: 0x00128C29
		public string Name
		{
			get
			{
				return (string)base[CalendarSchema.Name];
			}
			set
			{
				base[CalendarSchema.Name] = value;
			}
		}

		// Token: 0x170015FD RID: 5629
		// (get) Token: 0x06005FA9 RID: 24489 RVA: 0x0012AA37 File Offset: 0x00128C37
		// (set) Token: 0x06005FAA RID: 24490 RVA: 0x0012AA49 File Offset: 0x00128C49
		public string ChangeKey
		{
			get
			{
				return (string)base[CalendarSchema.ChangeKey];
			}
			set
			{
				base[CalendarSchema.ChangeKey] = value;
			}
		}

		// Token: 0x170015FE RID: 5630
		// (get) Token: 0x06005FAB RID: 24491 RVA: 0x0012AA57 File Offset: 0x00128C57
		// (set) Token: 0x06005FAC RID: 24492 RVA: 0x0012AA69 File Offset: 0x00128C69
		public IEnumerable<Event> Events
		{
			get
			{
				return (IEnumerable<Event>)base[CalendarSchema.Events];
			}
			set
			{
				base[CalendarSchema.Events] = value;
			}
		}

		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x06005FAD RID: 24493 RVA: 0x0012AA77 File Offset: 0x00128C77
		internal override EntitySchema Schema
		{
			get
			{
				return CalendarSchema.SchemaInstance;
			}
		}

		// Token: 0x040033E1 RID: 13281
		internal new static readonly EdmEntityType EdmEntityType = new EdmEntityType(typeof(Calendar).Namespace, typeof(Calendar).Name, Entity.EdmEntityType);
	}
}
