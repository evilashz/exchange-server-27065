using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002D1 RID: 721
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UploadPhotoRequestWrapper
	{
		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x000544D5 File Offset: 0x000526D5
		// (set) Token: 0x06001881 RID: 6273 RVA: 0x000544DD File Offset: 0x000526DD
		[DataMember(Name = "request")]
		public UploadPhotoRequest Request { get; set; }
	}
}
