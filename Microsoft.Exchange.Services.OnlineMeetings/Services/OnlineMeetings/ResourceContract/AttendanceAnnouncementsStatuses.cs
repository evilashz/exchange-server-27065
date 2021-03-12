using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000052 RID: 82
	[CollectionDataContract(Name = "AttendanceAnnouncementsStatuses")]
	internal class AttendanceAnnouncementsStatuses : Resource
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x000091D8 File Offset: 0x000073D8
		public AttendanceAnnouncementsStatuses(string selfUri) : base(selfUri)
		{
		}
	}
}
