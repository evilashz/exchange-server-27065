using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000059 RID: 89
	internal abstract class SyncHealthLogEventHandler
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x0000BD6C File Offset: 0x00009F6C
		internal static bool TryGetEventData(ReadOnlyRow inputRow, out DateTime entryTimeStamp, out string eventId, out KeyValuePair<string, object>[] eventData, out Exception exception)
		{
			eventId = null;
			entryTimeStamp = DateTime.MinValue;
			eventData = null;
			exception = null;
			DateTime? field;
			try
			{
				field = inputRow.GetField<DateTime?>(0);
				eventId = inputRow.GetField<string>(1);
				eventData = inputRow.GetField<KeyValuePair<string, object>[]>(2);
			}
			catch (InvalidCastException ex)
			{
				exception = ex;
				return false;
			}
			bool flag = field == null;
			bool flag2 = eventId == null;
			bool flag3 = eventData == null;
			if (flag || flag2 || flag3)
			{
				exception = new SyncHealthLogInvalidDataException(flag, flag2, flag3);
				return false;
			}
			entryTimeStamp = field.Value;
			eventId = eventId.ToLowerInvariant();
			return true;
		}

		// Token: 0x060001D7 RID: 471
		internal abstract bool ProcessEntry(DateTime entryTimeStamp, KeyValuePair<string, object>[] eventData, out Exception exception);
	}
}
