using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002A0 RID: 672
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class PerformInstantSearchRequestWrapper
	{
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x00053F2F File Offset: 0x0005212F
		// (set) Token: 0x060017D4 RID: 6100 RVA: 0x00053F37 File Offset: 0x00052137
		[DataMember(Name = "request")]
		public PerformInstantSearchRequest Request { get; set; }
	}
}
