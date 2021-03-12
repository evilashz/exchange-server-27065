using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003DC RID: 988
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetPresenceRequest
	{
		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x000778BF File Offset: 0x00075ABF
		// (set) Token: 0x06001FAA RID: 8106 RVA: 0x000778C7 File Offset: 0x00075AC7
		[DataMember(Name = "sipUris", IsRequired = true)]
		public string[] SipUris { get; set; }
	}
}
