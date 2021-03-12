using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000689 RID: 1673
	[XmlType(TypeName = "SendPromptType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public enum SendPromptType
	{
		// Token: 0x04001CF9 RID: 7417
		None,
		// Token: 0x04001CFA RID: 7418
		Send,
		// Token: 0x04001CFB RID: 7419
		VotingOption
	}
}
