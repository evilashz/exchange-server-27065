using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000832 RID: 2098
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "OccurrenceItemIdType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class OccurrenceItemId : BaseItemId
	{
		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06003C7B RID: 15483 RVA: 0x000D5DBC File Offset: 0x000D3FBC
		// (set) Token: 0x06003C7C RID: 15484 RVA: 0x000D5DC4 File Offset: 0x000D3FC4
		[DataMember(IsRequired = true, Order = 0)]
		[XmlAttribute]
		public string RecurringMasterId { get; set; }

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06003C7D RID: 15485 RVA: 0x000D5DCD File Offset: 0x000D3FCD
		// (set) Token: 0x06003C7E RID: 15486 RVA: 0x000D5DD5 File Offset: 0x000D3FD5
		[DataMember(IsRequired = false, Order = 0)]
		[XmlAttribute]
		public string ChangeKey { get; set; }

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06003C7F RID: 15487 RVA: 0x000D5DDE File Offset: 0x000D3FDE
		// (set) Token: 0x06003C80 RID: 15488 RVA: 0x000D5DE6 File Offset: 0x000D3FE6
		[DataMember(IsRequired = true, Order = 0)]
		[XmlAttribute]
		public int InstanceIndex { get; set; }

		// Token: 0x06003C82 RID: 15490 RVA: 0x000D5DF7 File Offset: 0x000D3FF7
		public override string GetId()
		{
			return this.RecurringMasterId;
		}

		// Token: 0x06003C83 RID: 15491 RVA: 0x000D5DFF File Offset: 0x000D3FFF
		public override string GetChangeKey()
		{
			return this.ChangeKey;
		}
	}
}
