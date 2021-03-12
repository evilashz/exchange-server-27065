using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000AE RID: 174
	[DataContract(Name = "ProtocolConnectionCollectionSetting", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class ProtocolConnectionCollectionSetting : UserSetting
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00017F22 File Offset: 0x00016122
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x00017F2A File Offset: 0x0001612A
		[DataMember(Name = "ProtocolConnections", IsRequired = true)]
		public ProtocolConnectionCollection ProtocolConnections { get; set; }
	}
}
