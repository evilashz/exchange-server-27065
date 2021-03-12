using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A2F RID: 2607
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class LinkedCalendarEntry : CalendarEntry
	{
		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x0600498A RID: 18826 RVA: 0x001029AF File Offset: 0x00100BAF
		// (set) Token: 0x0600498B RID: 18827 RVA: 0x001029B7 File Offset: 0x00100BB7
		[DataMember]
		public string OwnerEmailAddress { get; set; }

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x0600498C RID: 18828 RVA: 0x001029C0 File Offset: 0x00100BC0
		// (set) Token: 0x0600498D RID: 18829 RVA: 0x001029C8 File Offset: 0x00100BC8
		[DataMember]
		public string OwnerSipUri { get; set; }

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x0600498E RID: 18830 RVA: 0x001029D1 File Offset: 0x00100BD1
		// (set) Token: 0x0600498F RID: 18831 RVA: 0x001029D9 File Offset: 0x00100BD9
		[DataMember]
		public FolderId SharedFolderId { get; set; }

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06004990 RID: 18832 RVA: 0x001029E2 File Offset: 0x00100BE2
		// (set) Token: 0x06004991 RID: 18833 RVA: 0x001029EA File Offset: 0x00100BEA
		[DataMember]
		public bool IsGeneralScheduleCalendar { get; set; }

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06004992 RID: 18834 RVA: 0x001029F3 File Offset: 0x00100BF3
		// (set) Token: 0x06004993 RID: 18835 RVA: 0x001029FB File Offset: 0x00100BFB
		[DataMember]
		public bool IsOwnerEmailAddressInvalid { get; set; }
	}
}
