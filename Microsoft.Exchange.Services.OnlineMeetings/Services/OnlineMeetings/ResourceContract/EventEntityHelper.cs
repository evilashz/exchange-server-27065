using System;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200008E RID: 142
	internal static class EventEntityHelper
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x0000A514 File Offset: 0x00008714
		public static EventOperation GetEventOperation(string relationship)
		{
			if (relationship != null)
			{
				if (relationship == "added")
				{
					return EventOperation.Added;
				}
				if (relationship == "deleted")
				{
					return EventOperation.Deleted;
				}
				if (relationship == "updated")
				{
					return EventOperation.Updated;
				}
			}
			throw new ArgumentOutOfRangeException("relationship", relationship, "Unable to map 'relationship' to EventOperation value");
		}
	}
}
