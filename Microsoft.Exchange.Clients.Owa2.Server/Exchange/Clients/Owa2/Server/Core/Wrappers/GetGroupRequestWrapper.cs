using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000282 RID: 642
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetGroupRequestWrapper
	{
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x00053B64 File Offset: 0x00051D64
		// (set) Token: 0x06001760 RID: 5984 RVA: 0x00053B6C File Offset: 0x00051D6C
		[DataMember(Name = "itemId")]
		public ItemId ItemId { get; set; }

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x00053B75 File Offset: 0x00051D75
		// (set) Token: 0x06001762 RID: 5986 RVA: 0x00053B7D File Offset: 0x00051D7D
		[DataMember(Name = "adObjectId")]
		public string AdObjectId { get; set; }

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x00053B86 File Offset: 0x00051D86
		// (set) Token: 0x06001764 RID: 5988 RVA: 0x00053B8E File Offset: 0x00051D8E
		[DataMember(Name = "emailAddress")]
		public EmailAddressWrapper EmailAddress { get; set; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x00053B97 File Offset: 0x00051D97
		// (set) Token: 0x06001766 RID: 5990 RVA: 0x00053B9F File Offset: 0x00051D9F
		[DataMember(Name = "paging")]
		public IndexedPageView Paging { get; set; }

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x00053BA8 File Offset: 0x00051DA8
		// (set) Token: 0x06001768 RID: 5992 RVA: 0x00053BB0 File Offset: 0x00051DB0
		[DataMember(Name = "resultSet")]
		public GetGroupResultSet ResultSet { get; set; }
	}
}
