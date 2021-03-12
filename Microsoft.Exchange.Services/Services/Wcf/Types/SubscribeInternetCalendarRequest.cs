using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A38 RID: 2616
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SubscribeInternetCalendarRequest
	{
		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x060049D0 RID: 18896 RVA: 0x00102E72 File Offset: 0x00101072
		// (set) Token: 0x060049D1 RID: 18897 RVA: 0x00102E7A File Offset: 0x0010107A
		[DataMember]
		public string ICalUrl { get; set; }

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x060049D2 RID: 18898 RVA: 0x00102E83 File Offset: 0x00101083
		// (set) Token: 0x060049D3 RID: 18899 RVA: 0x00102E8B File Offset: 0x0010108B
		[DataMember]
		public string CalendarName { get; set; }

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x060049D4 RID: 18900 RVA: 0x00102E94 File Offset: 0x00101094
		// (set) Token: 0x060049D5 RID: 18901 RVA: 0x00102E9C File Offset: 0x0010109C
		[DataMember]
		private string ParentGroupGuid { get; set; }

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x060049D6 RID: 18902 RVA: 0x00102EA5 File Offset: 0x001010A5
		// (set) Token: 0x060049D7 RID: 18903 RVA: 0x00102EAD File Offset: 0x001010AD
		internal Guid GroupId { get; private set; }

		// Token: 0x060049D8 RID: 18904 RVA: 0x00102EB8 File Offset: 0x001010B8
		internal void ValidateRequest()
		{
			if (this.ICalUrl == null)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
			Guid groupId;
			if (!Guid.TryParse(this.ParentGroupGuid, out groupId))
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(CoreResources.IDs.ErrorInvalidId), FaultParty.Sender);
			}
			this.GroupId = groupId;
		}
	}
}
