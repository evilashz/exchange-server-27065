using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003FF RID: 1023
	[DataContract(Name = "SetUserThemeResponse", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetUserThemeResponse : BaseJsonResponse
	{
		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06002171 RID: 8561 RVA: 0x0007A140 File Offset: 0x00078340
		// (set) Token: 0x06002172 RID: 8562 RVA: 0x0007A148 File Offset: 0x00078348
		[DataMember(Name = "OwaSuccess", Order = 1)]
		public bool OwaSuccess { get; set; }

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06002173 RID: 8563 RVA: 0x0007A151 File Offset: 0x00078351
		// (set) Token: 0x06002174 RID: 8564 RVA: 0x0007A159 File Offset: 0x00078359
		[DataMember(Name = "O365Success", Order = 2)]
		public bool O365Success { get; set; }
	}
}
