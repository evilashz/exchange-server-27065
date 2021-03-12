using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000281 RID: 641
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetExtensibilityContextRequestWrapper
	{
		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x00053B4B File Offset: 0x00051D4B
		// (set) Token: 0x0600175D RID: 5981 RVA: 0x00053B53 File Offset: 0x00051D53
		[DataMember(Name = "request")]
		public GetExtensibilityContextParameters Request { get; set; }
	}
}
