using System;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E6C RID: 3692
	internal static class AttendeeDataEntityConverter
	{
		// Token: 0x0600602A RID: 24618 RVA: 0x0012C4D0 File Offset: 0x0012A6D0
		internal static Attendee ToAttendee(this Attendee dataEntityAttendee)
		{
			if (dataEntityAttendee == null)
			{
				return null;
			}
			return new Attendee
			{
				Name = dataEntityAttendee.Name,
				Address = dataEntityAttendee.EmailAddress,
				Status = dataEntityAttendee.Status.ToResponseStatus(),
				Type = EnumConverter.CastEnumType<AttendeeType>(dataEntityAttendee.Type)
			};
		}

		// Token: 0x0600602B RID: 24619 RVA: 0x0012C52C File Offset: 0x0012A72C
		internal static Attendee ToDataEntityAttendee(this Attendee attendee)
		{
			if (attendee == null)
			{
				return null;
			}
			return new Attendee
			{
				Name = attendee.Name,
				EmailAddress = attendee.Address,
				Status = attendee.Status.ToDataEntityResponseStatus(),
				Type = EnumConverter.CastEnumType<AttendeeType>(attendee.Type)
			};
		}
	}
}
