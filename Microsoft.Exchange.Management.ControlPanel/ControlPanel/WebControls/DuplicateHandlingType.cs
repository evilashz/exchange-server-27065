using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005B3 RID: 1459
	[DataContract]
	public enum DuplicateHandlingType
	{
		// Token: 0x04002BCF RID: 11215
		[EnumMember]
		RemoveDuplicateCaseInsensitive,
		// Token: 0x04002BD0 RID: 11216
		[EnumMember]
		RemoveDuplicateCaseSensitive,
		// Token: 0x04002BD1 RID: 11217
		[EnumMember]
		AllowDuplicate
	}
}
