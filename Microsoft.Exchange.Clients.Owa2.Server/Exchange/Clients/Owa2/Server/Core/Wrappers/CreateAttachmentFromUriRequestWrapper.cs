using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000265 RID: 613
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateAttachmentFromUriRequestWrapper
	{
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x000537B2 File Offset: 0x000519B2
		// (set) Token: 0x060016EF RID: 5871 RVA: 0x000537BA File Offset: 0x000519BA
		[DataMember(Name = "itemId")]
		public ItemId ItemId { get; set; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x000537C3 File Offset: 0x000519C3
		// (set) Token: 0x060016F1 RID: 5873 RVA: 0x000537CB File Offset: 0x000519CB
		[DataMember(Name = "uri")]
		public string Uri { get; set; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x000537D4 File Offset: 0x000519D4
		// (set) Token: 0x060016F3 RID: 5875 RVA: 0x000537DC File Offset: 0x000519DC
		[DataMember(Name = "name")]
		public string Name { get; set; }

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x000537E5 File Offset: 0x000519E5
		// (set) Token: 0x060016F5 RID: 5877 RVA: 0x000537ED File Offset: 0x000519ED
		[DataMember(Name = "subscriptionId")]
		public string SubscriptionId { get; set; }
	}
}
