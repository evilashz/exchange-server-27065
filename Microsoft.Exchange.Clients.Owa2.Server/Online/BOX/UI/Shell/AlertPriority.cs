using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.UI.Shell
{
	// Token: 0x02000082 RID: 130
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "AlertPriority", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.UI.Shell")]
	public enum AlertPriority
	{
		// Token: 0x040002A3 RID: 675
		[EnumMember]
		Unknown,
		// Token: 0x040002A4 RID: 676
		[EnumMember]
		Low,
		// Token: 0x040002A5 RID: 677
		[EnumMember]
		Medium,
		// Token: 0x040002A6 RID: 678
		[EnumMember]
		High
	}
}
