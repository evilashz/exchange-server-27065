using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000661 RID: 1633
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BirthdayCalendarFolderCreator : MessageClassBasedDefaultFolderCreator
	{
		// Token: 0x060043B2 RID: 17330 RVA: 0x0011EF9A File Offset: 0x0011D19A
		internal BirthdayCalendarFolderCreator() : base(DefaultFolderType.Calendar, "IPF.Appointment.Birthday", false)
		{
		}
	}
}
