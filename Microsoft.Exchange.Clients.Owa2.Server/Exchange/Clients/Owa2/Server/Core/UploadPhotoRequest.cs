using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003F3 RID: 1011
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class UploadPhotoRequest
	{
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x060020CE RID: 8398 RVA: 0x000791F7 File Offset: 0x000773F7
		// (set) Token: 0x060020CF RID: 8399 RVA: 0x000791FF File Offset: 0x000773FF
		[DataMember]
		public UploadPhotoCommand Command { get; set; }

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x060020D0 RID: 8400 RVA: 0x00079208 File Offset: 0x00077408
		// (set) Token: 0x060020D1 RID: 8401 RVA: 0x00079210 File Offset: 0x00077410
		[DataMember]
		public string Content { get; set; }

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x00079219 File Offset: 0x00077419
		// (set) Token: 0x060020D3 RID: 8403 RVA: 0x00079221 File Offset: 0x00077421
		[DataMember(IsRequired = true)]
		public string EmailAddress { get; set; }
	}
}
