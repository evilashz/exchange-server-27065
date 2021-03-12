using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000663 RID: 1635
	[DataContract(Name = "VotingResponseItem", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class VotingResponseItemType : ResponseObjectType
	{
		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x0600321E RID: 12830 RVA: 0x000B780A File Offset: 0x000B5A0A
		// (set) Token: 0x0600321F RID: 12831 RVA: 0x000B7812 File Offset: 0x000B5A12
		[DataMember(EmitDefaultValue = false, Order = 1)]
		[XmlIgnore]
		public string Response { get; set; }
	}
}
