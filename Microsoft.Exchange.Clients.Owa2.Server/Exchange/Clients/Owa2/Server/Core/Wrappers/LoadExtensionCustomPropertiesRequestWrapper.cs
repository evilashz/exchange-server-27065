using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200029A RID: 666
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class LoadExtensionCustomPropertiesRequestWrapper
	{
		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x00053E55 File Offset: 0x00052055
		// (set) Token: 0x060017BA RID: 6074 RVA: 0x00053E5D File Offset: 0x0005205D
		[DataMember(Name = "request")]
		public LoadExtensionCustomPropertiesParameters Request { get; set; }
	}
}
