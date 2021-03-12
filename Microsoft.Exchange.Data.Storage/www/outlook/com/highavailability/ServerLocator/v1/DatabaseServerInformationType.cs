using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace www.outlook.com.highavailability.ServerLocator.v1
{
	// Token: 0x02000D38 RID: 3384
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DatabaseServerInformationType", Namespace = "http://www.outlook.com/highavailability/ServerLocator/v1/")]
	public enum DatabaseServerInformationType
	{
		// Token: 0x04005196 RID: 20886
		[EnumMember]
		TransientError = 1000,
		// Token: 0x04005197 RID: 20887
		[EnumMember]
		PermanentError = 2000
	}
}
