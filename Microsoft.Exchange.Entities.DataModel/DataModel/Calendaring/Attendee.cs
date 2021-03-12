using System;
using Microsoft.Exchange.Entities.DataModel.Items;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x02000029 RID: 41
	public class Attendee : Recipient<AttendeeSchema>, IAttendee, IRecipient
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000032F8 File Offset: 0x000014F8
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x0000330B File Offset: 0x0000150B
		public ResponseStatus Status
		{
			get
			{
				return base.GetPropertyValueOrDefault<ResponseStatus>(base.Schema.StatusProperty);
			}
			set
			{
				base.SetPropertyValue<ResponseStatus>(base.Schema.StatusProperty, value);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000331F File Offset: 0x0000151F
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00003332 File Offset: 0x00001532
		public AttendeeType Type
		{
			get
			{
				return base.GetPropertyValueOrDefault<AttendeeType>(base.Schema.TypeProperty);
			}
			set
			{
				base.SetPropertyValue<AttendeeType>(base.Schema.TypeProperty, value);
			}
		}
	}
}
