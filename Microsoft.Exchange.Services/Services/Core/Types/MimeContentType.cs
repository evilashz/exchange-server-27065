using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005F6 RID: 1526
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class MimeContentType
	{
		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06002F0B RID: 12043 RVA: 0x000B3CA9 File Offset: 0x000B1EA9
		// (set) Token: 0x06002F0C RID: 12044 RVA: 0x000B3CB1 File Offset: 0x000B1EB1
		[XmlAttribute]
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public string CharacterSet { get; set; }

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06002F0D RID: 12045 RVA: 0x000B3CBA File Offset: 0x000B1EBA
		// (set) Token: 0x06002F0E RID: 12046 RVA: 0x000B3CC2 File Offset: 0x000B1EC2
		[XmlText]
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 1)]
		public string Value { get; set; }
	}
}
