using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005DF RID: 1503
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Term")]
	[Serializable]
	public class HighlightTermType
	{
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06002D3E RID: 11582 RVA: 0x000B1F1B File Offset: 0x000B011B
		// (set) Token: 0x06002D3F RID: 11583 RVA: 0x000B1F23 File Offset: 0x000B0123
		[DataMember(Order = 1)]
		public string Scope { get; set; }

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06002D40 RID: 11584 RVA: 0x000B1F2C File Offset: 0x000B012C
		// (set) Token: 0x06002D41 RID: 11585 RVA: 0x000B1F34 File Offset: 0x000B0134
		[DataMember(Order = 2)]
		public string Value { get; set; }
	}
}
