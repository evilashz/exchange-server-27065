using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000824 RID: 2084
	[DataContract(Name = "NonIndexableItemStatisticResult", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "NonIndexableItemStatisticType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class NonIndexableItemStatisticResult
	{
		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06003C5B RID: 15451 RVA: 0x000D5BE9 File Offset: 0x000D3DE9
		// (set) Token: 0x06003C5C RID: 15452 RVA: 0x000D5BF1 File Offset: 0x000D3DF1
		[DataMember(Name = "Mailbox", IsRequired = true)]
		[XmlElement("Mailbox")]
		public string Mailbox { get; set; }

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06003C5D RID: 15453 RVA: 0x000D5BFA File Offset: 0x000D3DFA
		// (set) Token: 0x06003C5E RID: 15454 RVA: 0x000D5C02 File Offset: 0x000D3E02
		[XmlElement("ItemCount")]
		[DataMember(Name = "ItemCount", IsRequired = true)]
		public long ItemCount { get; set; }

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06003C5F RID: 15455 RVA: 0x000D5C0B File Offset: 0x000D3E0B
		// (set) Token: 0x06003C60 RID: 15456 RVA: 0x000D5C13 File Offset: 0x000D3E13
		[DataMember(Name = "ErrorMessage", IsRequired = true)]
		[XmlElement("ErrorMessage")]
		public string ErrorMessage { get; set; }
	}
}
