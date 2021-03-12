using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003B8 RID: 952
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateReferenceAttachmentFromAttachmentDataProviderRequest
	{
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001E85 RID: 7813 RVA: 0x00076952 File Offset: 0x00074B52
		// (set) Token: 0x06001E86 RID: 7814 RVA: 0x0007695A File Offset: 0x00074B5A
		[DataMember(Name = "itemId", IsRequired = true)]
		public ItemId ItemId { get; set; }

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x00076963 File Offset: 0x00074B63
		// (set) Token: 0x06001E88 RID: 7816 RVA: 0x0007696B File Offset: 0x00074B6B
		[DataMember(Name = "attachmentDataProviderId", IsRequired = true)]
		public string AttachmentDataProviderId { get; set; }

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x00076974 File Offset: 0x00074B74
		// (set) Token: 0x06001E8A RID: 7818 RVA: 0x0007697C File Offset: 0x00074B7C
		[DataMember(Name = "location", IsRequired = false)]
		public string Location { get; set; }

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x00076985 File Offset: 0x00074B85
		// (set) Token: 0x06001E8C RID: 7820 RVA: 0x0007698D File Offset: 0x00074B8D
		[DataMember(Name = "attachmentId", IsRequired = true)]
		public string AttachmentId { get; set; }

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x00076996 File Offset: 0x00074B96
		// (set) Token: 0x06001E8E RID: 7822 RVA: 0x0007699E File Offset: 0x00074B9E
		[DataMember(Name = "subscriptionId", IsRequired = false)]
		public string SubscriptionId { get; set; }

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x000769A7 File Offset: 0x00074BA7
		// (set) Token: 0x06001E90 RID: 7824 RVA: 0x000769AF File Offset: 0x00074BAF
		[DataMember(Name = "dataProviderParentItemId", IsRequired = false)]
		public string DataProviderParentItemId { get; set; }

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x000769B8 File Offset: 0x00074BB8
		// (set) Token: 0x06001E92 RID: 7826 RVA: 0x000769C0 File Offset: 0x00074BC0
		[DataMember(Name = "providerEndpointUrl", IsRequired = false)]
		public string ProviderEndpointUrl { get; set; }

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x000769C9 File Offset: 0x00074BC9
		// (set) Token: 0x06001E94 RID: 7828 RVA: 0x000769D1 File Offset: 0x00074BD1
		[DataMember(Name = "cancellationId", IsRequired = false)]
		public string CancellationId { get; set; }
	}
}
