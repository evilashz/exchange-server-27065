using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000723 RID: 1827
	[XmlType(TypeName = "ClientIntentType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public sealed class ClientIntent
	{
		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06003778 RID: 14200 RVA: 0x000C58ED File Offset: 0x000C3AED
		// (set) Token: 0x06003779 RID: 14201 RVA: 0x000C58F5 File Offset: 0x000C3AF5
		[DataMember(IsRequired = true, Order = 1)]
		[XmlElement]
		public ItemId ItemId { get; set; }

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x0600377A RID: 14202 RVA: 0x000C58FE File Offset: 0x000C3AFE
		// (set) Token: 0x0600377B RID: 14203 RVA: 0x000C5906 File Offset: 0x000C3B06
		[DataMember(IsRequired = false, Order = 2)]
		[XmlElement]
		public int? Intent { get; set; }

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x0600377C RID: 14204 RVA: 0x000C590F File Offset: 0x000C3B0F
		// (set) Token: 0x0600377D RID: 14205 RVA: 0x000C5917 File Offset: 0x000C3B17
		[XmlElement]
		[DataMember(IsRequired = false, Order = 3)]
		public int? ItemVersion { get; set; }

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x0600377E RID: 14206 RVA: 0x000C5920 File Offset: 0x000C3B20
		// (set) Token: 0x0600377F RID: 14207 RVA: 0x000C5928 File Offset: 0x000C3B28
		[DataMember(IsRequired = false, Order = 4)]
		[XmlElement]
		public bool WouldRepair { get; set; }

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06003780 RID: 14208 RVA: 0x000C5931 File Offset: 0x000C3B31
		// (set) Token: 0x06003781 RID: 14209 RVA: 0x000C5939 File Offset: 0x000C3B39
		[XmlElement]
		[DataMember(IsRequired = false, Order = 5)]
		public ClientIntentMeetingInquiryAction PredictedAction { get; set; }
	}
}
