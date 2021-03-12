using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002B0 RID: 688
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetCalendarColorRequestWrapper
	{
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x00054114 File Offset: 0x00052314
		// (set) Token: 0x0600180E RID: 6158 RVA: 0x0005411C File Offset: 0x0005231C
		[DataMember(Name = "itemId")]
		public ItemId ItemId { get; set; }

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x00054125 File Offset: 0x00052325
		// (set) Token: 0x06001810 RID: 6160 RVA: 0x0005412D File Offset: 0x0005232D
		[DataMember(Name = "calendarColor")]
		public CalendarColor CalendarColor { get; set; }
	}
}
