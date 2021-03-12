using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000567 RID: 1383
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "ConversationViewData")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ConversationViewDataType
	{
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060026B2 RID: 9906 RVA: 0x000A68B5 File Offset: 0x000A4AB5
		// (set) Token: 0x060026B3 RID: 9907 RVA: 0x000A68BD File Offset: 0x000A4ABD
		[DataMember(IsRequired = true, Order = 1)]
		[XmlElement("FolderId")]
		public BaseFolderId FolderId { get; set; }

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060026B4 RID: 9908 RVA: 0x000A68C6 File Offset: 0x000A4AC6
		// (set) Token: 0x060026B5 RID: 9909 RVA: 0x000A68CE File Offset: 0x000A4ACE
		[DataMember(EmitDefaultValue = true, Order = 2)]
		[XmlElement("TotalConversationsInView")]
		public int TotalConversationsInView { get; set; }

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x000A68D7 File Offset: 0x000A4AD7
		// (set) Token: 0x060026B7 RID: 9911 RVA: 0x000A68DF File Offset: 0x000A4ADF
		[XmlElement("OldestDeliveryTime")]
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public string OldestDeliveryTime { get; set; }

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x000A68E8 File Offset: 0x000A4AE8
		// (set) Token: 0x060026B9 RID: 9913 RVA: 0x000A68F0 File Offset: 0x000A4AF0
		[XmlElement("MoreItemsOnServer")]
		[DataMember(EmitDefaultValue = true, Order = 4)]
		public bool MoreItemsOnServer { get; set; }
	}
}
