using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200085D RID: 2141
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "RecurringMasterItemIdType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class RecurringMasterItemId : BaseItemId
	{
		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06003D8A RID: 15754 RVA: 0x000D79B3 File Offset: 0x000D5BB3
		// (set) Token: 0x06003D8B RID: 15755 RVA: 0x000D79BB File Offset: 0x000D5BBB
		[XmlAttribute]
		[DataMember(IsRequired = true, Order = 1)]
		public string OccurrenceId { get; set; }

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06003D8C RID: 15756 RVA: 0x000D79C4 File Offset: 0x000D5BC4
		// (set) Token: 0x06003D8D RID: 15757 RVA: 0x000D79CC File Offset: 0x000D5BCC
		[XmlAttribute]
		[DataMember(IsRequired = false, Order = 2)]
		public string ChangeKey { get; set; }

		// Token: 0x06003D8F RID: 15759 RVA: 0x000D79DD File Offset: 0x000D5BDD
		public override string GetId()
		{
			return this.OccurrenceId;
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x000D79E5 File Offset: 0x000D5BE5
		public override string GetChangeKey()
		{
			return this.ChangeKey;
		}
	}
}
