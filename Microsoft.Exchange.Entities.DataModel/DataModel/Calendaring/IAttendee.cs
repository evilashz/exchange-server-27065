using System;
using Microsoft.Exchange.Entities.DataModel.Items;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x02000028 RID: 40
	public interface IAttendee : IRecipient
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000CB RID: 203
		// (set) Token: 0x060000CC RID: 204
		ResponseStatus Status { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000CD RID: 205
		// (set) Token: 0x060000CE RID: 206
		AttendeeType Type { get; set; }
	}
}
