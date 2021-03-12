using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A25 RID: 2597
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCalendarFoldersResponse : CalendarActionResponse
	{
		// Token: 0x06004936 RID: 18742 RVA: 0x001024F9 File Offset: 0x001006F9
		internal GetCalendarFoldersResponse(CalendarGroup[] calendarGroups, CalendarFolderType[] calendarFolders)
		{
			this.CalendarGroups = calendarGroups;
			this.CalendarFolders = calendarFolders;
		}

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x06004937 RID: 18743 RVA: 0x0010250F File Offset: 0x0010070F
		// (set) Token: 0x06004938 RID: 18744 RVA: 0x00102517 File Offset: 0x00100717
		[DataMember]
		public CalendarGroup[] CalendarGroups { get; set; }

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x06004939 RID: 18745 RVA: 0x00102520 File Offset: 0x00100720
		// (set) Token: 0x0600493A RID: 18746 RVA: 0x00102528 File Offset: 0x00100728
		[DataMember]
		public CalendarFolderType[] CalendarFolders { get; set; }
	}
}
