using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A07 RID: 2567
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddEventToMyCalendarRequest
	{
		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x0600487D RID: 18557 RVA: 0x001018B8 File Offset: 0x000FFAB8
		// (set) Token: 0x0600487E RID: 18558 RVA: 0x001018C0 File Offset: 0x000FFAC0
		[XmlElement("RecurringMasterItemId", typeof(RecurringMasterItemId))]
		[DataMember(Name = "ItemId", IsRequired = true)]
		[XmlElement("OccurrenceItemId", typeof(OccurrenceItemId))]
		[XmlElement("ItemId", typeof(ItemId))]
		public BaseItemId ItemId { get; set; }
	}
}
