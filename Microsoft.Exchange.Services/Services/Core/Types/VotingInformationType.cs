using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000688 RID: 1672
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", TypeName = "VotingInformationType")]
	[Serializable]
	public class VotingInformationType
	{
		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06003329 RID: 13097 RVA: 0x000B84D8 File Offset: 0x000B66D8
		// (set) Token: 0x0600332A RID: 13098 RVA: 0x000B84E0 File Offset: 0x000B66E0
		[XmlArrayItem("VotingOptionData", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public VotingOptionDataType[] UserOptions { get; set; }

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x0600332B RID: 13099 RVA: 0x000B84E9 File Offset: 0x000B66E9
		// (set) Token: 0x0600332C RID: 13100 RVA: 0x000B84F1 File Offset: 0x000B66F1
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string VotingResponse { get; set; }
	}
}
