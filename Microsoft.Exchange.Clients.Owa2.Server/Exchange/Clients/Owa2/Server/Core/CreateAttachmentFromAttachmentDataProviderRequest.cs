using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003B6 RID: 950
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateAttachmentFromAttachmentDataProviderRequest
	{
		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001E65 RID: 7781 RVA: 0x00076843 File Offset: 0x00074A43
		// (set) Token: 0x06001E66 RID: 7782 RVA: 0x0007684B File Offset: 0x00074A4B
		[DataMember(Name = "itemId", IsRequired = true)]
		public ItemId ItemId { get; set; }

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001E67 RID: 7783 RVA: 0x00076854 File Offset: 0x00074A54
		// (set) Token: 0x06001E68 RID: 7784 RVA: 0x0007685C File Offset: 0x00074A5C
		[DataMember(Name = "attachmentDataProviderId", IsRequired = true)]
		public string AttachmentDataProviderId { get; set; }

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x00076865 File Offset: 0x00074A65
		// (set) Token: 0x06001E6A RID: 7786 RVA: 0x0007686D File Offset: 0x00074A6D
		[DataMember(Name = "location", IsRequired = false)]
		public string Location { get; set; }

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001E6B RID: 7787 RVA: 0x00076876 File Offset: 0x00074A76
		// (set) Token: 0x06001E6C RID: 7788 RVA: 0x0007687E File Offset: 0x00074A7E
		[DataMember(Name = "attachmentId", IsRequired = true)]
		public string AttachmentId { get; set; }

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x00076887 File Offset: 0x00074A87
		// (set) Token: 0x06001E6E RID: 7790 RVA: 0x0007688F File Offset: 0x00074A8F
		[DataMember(Name = "subscriptionId", IsRequired = false)]
		public string SubscriptionId { get; set; }

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x00076898 File Offset: 0x00074A98
		// (set) Token: 0x06001E70 RID: 7792 RVA: 0x000768A0 File Offset: 0x00074AA0
		[DataMember(Name = "channelId", IsRequired = false)]
		public string ChannelId { get; set; }

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x000768A9 File Offset: 0x00074AA9
		// (set) Token: 0x06001E72 RID: 7794 RVA: 0x000768B1 File Offset: 0x00074AB1
		[DataMember(Name = "dataProviderParentItemId", IsRequired = false)]
		public string DataProviderParentItemId { get; set; }

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x000768BA File Offset: 0x00074ABA
		// (set) Token: 0x06001E74 RID: 7796 RVA: 0x000768C2 File Offset: 0x00074AC2
		[DataMember(Name = "providerEndpointUrl", IsRequired = false)]
		public string ProviderEndpointUrl { get; set; }

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x000768CB File Offset: 0x00074ACB
		// (set) Token: 0x06001E76 RID: 7798 RVA: 0x000768D3 File Offset: 0x00074AD3
		[DataMember(Name = "cancellationId", IsRequired = false)]
		public string CancellationId { get; set; }
	}
}
