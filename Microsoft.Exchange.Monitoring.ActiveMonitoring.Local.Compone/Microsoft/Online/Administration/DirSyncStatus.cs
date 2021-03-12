using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003EC RID: 1004
	[DataContract(Name = "DirSyncStatus", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum DirSyncStatus
	{
		// Token: 0x04001164 RID: 4452
		[EnumMember]
		Disabled,
		// Token: 0x04001165 RID: 4453
		[EnumMember]
		Enabled,
		// Token: 0x04001166 RID: 4454
		[EnumMember]
		PendingEnabled,
		// Token: 0x04001167 RID: 4455
		[EnumMember]
		PendingDisabled,
		// Token: 0x04001168 RID: 4456
		[EnumMember]
		Other
	}
}
