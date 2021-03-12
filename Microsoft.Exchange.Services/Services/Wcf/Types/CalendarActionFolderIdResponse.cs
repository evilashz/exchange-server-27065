using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A0D RID: 2573
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CalendarActionFolderIdResponse : CalendarActionItemIdResponse
	{
		// Token: 0x06004894 RID: 18580 RVA: 0x001019E5 File Offset: 0x000FFBE5
		public CalendarActionFolderIdResponse(CalendarActionError errorCode) : base(errorCode)
		{
		}

		// Token: 0x06004895 RID: 18581 RVA: 0x001019EE File Offset: 0x000FFBEE
		public CalendarActionFolderIdResponse(FolderId folderId, ItemId calendarEntryId) : base(calendarEntryId)
		{
			this.NewFolderId = folderId;
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06004896 RID: 18582 RVA: 0x001019FE File Offset: 0x000FFBFE
		// (set) Token: 0x06004897 RID: 18583 RVA: 0x00101A06 File Offset: 0x000FFC06
		[DataMember]
		public FolderId NewFolderId { get; set; }
	}
}
