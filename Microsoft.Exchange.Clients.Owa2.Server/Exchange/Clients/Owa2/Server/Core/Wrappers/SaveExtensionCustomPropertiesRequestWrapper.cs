using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002AA RID: 682
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SaveExtensionCustomPropertiesRequestWrapper
	{
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x0005407E File Offset: 0x0005227E
		// (set) Token: 0x060017FC RID: 6140 RVA: 0x00054086 File Offset: 0x00052286
		[DataMember(Name = "request")]
		public SaveExtensionCustomPropertiesParameters Request { get; set; }
	}
}
