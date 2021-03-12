using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003F8 RID: 1016
	[DataContract(Name = "GetRegionalConfigurationResponse", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetRegionalConfigurationResponse : BaseJsonResponse
	{
		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x0007940D File Offset: 0x0007760D
		// (set) Token: 0x060020FF RID: 8447 RVA: 0x00079415 File Offset: 0x00077615
		[DataMember(Name = "SupportedCultures", IsRequired = false)]
		public CultureInfoData[] SupportedCultures { get; set; }
	}
}
