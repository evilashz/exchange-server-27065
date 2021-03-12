using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000280 RID: 640
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetEncryptionCertsRequestWrapper
	{
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x00053B32 File Offset: 0x00051D32
		// (set) Token: 0x0600175A RID: 5978 RVA: 0x00053B3A File Offset: 0x00051D3A
		[DataMember(Name = "request")]
		public GetCertsRequest Request { get; set; }
	}
}
