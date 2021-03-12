using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions
{
	// Token: 0x02000042 RID: 66
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class TooBigGroupAttendeeConflictData : AttendeeConflictData
	{
		// Token: 0x06000153 RID: 339 RVA: 0x000086D0 File Offset: 0x000068D0
		internal static TooBigGroupAttendeeConflictData Create()
		{
			return new TooBigGroupAttendeeConflictData();
		}
	}
}
