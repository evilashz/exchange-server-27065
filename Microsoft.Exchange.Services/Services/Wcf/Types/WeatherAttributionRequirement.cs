using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B5D RID: 2909
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public enum WeatherAttributionRequirement
	{
		// Token: 0x04002DF0 RID: 11760
		[EnumMember]
		None,
		// Token: 0x04002DF1 RID: 11761
		[EnumMember]
		Text,
		// Token: 0x04002DF2 RID: 11762
		[EnumMember]
		Link
	}
}
