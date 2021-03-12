using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007E2 RID: 2018
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "ItemId")]
	[XmlType(TypeName = "ItemIdType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class ItemId : BaseItemId
	{
		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06003B49 RID: 15177 RVA: 0x000CFF40 File Offset: 0x000CE140
		// (set) Token: 0x06003B4A RID: 15178 RVA: 0x000CFF48 File Offset: 0x000CE148
		[XmlAttribute]
		[DataMember(IsRequired = true, Order = 0)]
		public string Id { get; set; }

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06003B4B RID: 15179 RVA: 0x000CFF51 File Offset: 0x000CE151
		// (set) Token: 0x06003B4C RID: 15180 RVA: 0x000CFF59 File Offset: 0x000CE159
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 0)]
		[XmlAttribute]
		public string ChangeKey { get; set; }

		// Token: 0x06003B4D RID: 15181 RVA: 0x000CFF62 File Offset: 0x000CE162
		public ItemId()
		{
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x000CFF6A File Offset: 0x000CE16A
		public ItemId(string id, string changeKey)
		{
			this.Id = id;
			this.ChangeKey = changeKey;
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x000CFF80 File Offset: 0x000CE180
		public override string GetId()
		{
			return this.Id;
		}

		// Token: 0x06003B50 RID: 15184 RVA: 0x000CFF88 File Offset: 0x000CE188
		public override string GetChangeKey()
		{
			return this.ChangeKey;
		}
	}
}
