using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002C0 RID: 704
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetThemeRequestWrapper
	{
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x000542D7 File Offset: 0x000524D7
		// (set) Token: 0x06001844 RID: 6212 RVA: 0x000542DF File Offset: 0x000524DF
		[DataMember(Name = "theme")]
		public string Theme { get; set; }
	}
}
