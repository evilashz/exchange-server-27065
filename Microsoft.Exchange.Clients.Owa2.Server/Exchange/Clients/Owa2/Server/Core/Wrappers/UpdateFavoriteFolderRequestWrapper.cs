using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002CD RID: 717
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateFavoriteFolderRequestWrapper
	{
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x00054471 File Offset: 0x00052671
		// (set) Token: 0x06001875 RID: 6261 RVA: 0x00054479 File Offset: 0x00052679
		[DataMember(Name = "request")]
		public UpdateFavoriteFolderRequest Request { get; set; }
	}
}
