using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003C7 RID: 967
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ProvisioningStatus", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public enum ProvisioningStatus
	{
		// Token: 0x04001082 RID: 4226
		[EnumMember]
		None,
		// Token: 0x04001083 RID: 4227
		[EnumMember]
		Success,
		// Token: 0x04001084 RID: 4228
		[EnumMember]
		Error,
		// Token: 0x04001085 RID: 4229
		[EnumMember]
		PendingInput,
		// Token: 0x04001086 RID: 4230
		[EnumMember]
		Disabled
	}
}
