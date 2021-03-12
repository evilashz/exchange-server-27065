using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions
{
	// Token: 0x02000041 RID: 65
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class UnknownAttendeeConflictData : AttendeeConflictData
	{
		// Token: 0x06000151 RID: 337 RVA: 0x000086C1 File Offset: 0x000068C1
		internal static UnknownAttendeeConflictData Create()
		{
			return new UnknownAttendeeConflictData();
		}
	}
}
