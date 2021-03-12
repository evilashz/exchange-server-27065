using System;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E76 RID: 3702
	internal static class OrganizerDataEntityConverter
	{
		// Token: 0x06006052 RID: 24658 RVA: 0x0012CCB0 File Offset: 0x0012AEB0
		internal static Recipient ToRecipient(this Organizer dataEntityOrganizer)
		{
			if (dataEntityOrganizer == null)
			{
				return null;
			}
			return new Recipient
			{
				Name = dataEntityOrganizer.Name,
				Address = dataEntityOrganizer.EmailAddress
			};
		}
	}
}
