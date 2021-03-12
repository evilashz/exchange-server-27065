using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions
{
	// Token: 0x0200003C RID: 60
	[XmlType(TypeName = "MeetingAttendeeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum MeetingAttendeeType
	{
		// Token: 0x040000B4 RID: 180
		Organizer,
		// Token: 0x040000B5 RID: 181
		Required,
		// Token: 0x040000B6 RID: 182
		Optional,
		// Token: 0x040000B7 RID: 183
		Room,
		// Token: 0x040000B8 RID: 184
		Resource
	}
}
