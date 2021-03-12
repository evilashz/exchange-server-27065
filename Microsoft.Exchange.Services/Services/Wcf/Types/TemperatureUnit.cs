using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B5C RID: 2908
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public enum TemperatureUnit
	{
		// Token: 0x04002DEC RID: 11756
		[EnumMember]
		Default,
		// Token: 0x04002DED RID: 11757
		[EnumMember]
		Celsius,
		// Token: 0x04002DEE RID: 11758
		[EnumMember]
		Fahrenheit
	}
}
