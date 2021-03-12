using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E66 RID: 3686
	internal class CalendarGroup : Entity
	{
		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x06005FBB RID: 24507 RVA: 0x0012ACAE File Offset: 0x00128EAE
		// (set) Token: 0x06005FBC RID: 24508 RVA: 0x0012ACC0 File Offset: 0x00128EC0
		public string Name
		{
			get
			{
				return (string)base[CalendarGroupSchema.Name];
			}
			set
			{
				base[CalendarGroupSchema.Name] = value;
			}
		}

		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x06005FBD RID: 24509 RVA: 0x0012ACCE File Offset: 0x00128ECE
		// (set) Token: 0x06005FBE RID: 24510 RVA: 0x0012ACE0 File Offset: 0x00128EE0
		public string ChangeKey
		{
			get
			{
				return (string)base[CalendarGroupSchema.ChangeKey];
			}
			set
			{
				base[CalendarGroupSchema.ChangeKey] = value;
			}
		}

		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x06005FBF RID: 24511 RVA: 0x0012ACEE File Offset: 0x00128EEE
		// (set) Token: 0x06005FC0 RID: 24512 RVA: 0x0012AD00 File Offset: 0x00128F00
		public Guid ClassId
		{
			get
			{
				return (Guid)base[CalendarGroupSchema.ClassId];
			}
			set
			{
				base[CalendarGroupSchema.ClassId] = value;
			}
		}

		// Token: 0x17001609 RID: 5641
		// (get) Token: 0x06005FC1 RID: 24513 RVA: 0x0012AD13 File Offset: 0x00128F13
		// (set) Token: 0x06005FC2 RID: 24514 RVA: 0x0012AD25 File Offset: 0x00128F25
		public IEnumerable<Calendar> Calendars
		{
			get
			{
				return (IEnumerable<Calendar>)base[CalendarGroupSchema.Calendars];
			}
			set
			{
				base[CalendarGroupSchema.Calendars] = value;
			}
		}

		// Token: 0x1700160A RID: 5642
		// (get) Token: 0x06005FC3 RID: 24515 RVA: 0x0012AD33 File Offset: 0x00128F33
		internal override EntitySchema Schema
		{
			get
			{
				return CalendarGroupSchema.SchemaInstance;
			}
		}

		// Token: 0x040033ED RID: 13293
		internal new static readonly EdmEntityType EdmEntityType = new EdmEntityType(typeof(CalendarGroup).Namespace, typeof(CalendarGroup).Name, Entity.EdmEntityType);
	}
}
