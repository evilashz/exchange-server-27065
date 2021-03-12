using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002AB RID: 683
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SaveExtensionSettingsRequestWrapper
	{
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x00054097 File Offset: 0x00052297
		// (set) Token: 0x060017FF RID: 6143 RVA: 0x0005409F File Offset: 0x0005229F
		[DataMember(Name = "request")]
		public SaveExtensionSettingsParameters Request { get; set; }
	}
}
