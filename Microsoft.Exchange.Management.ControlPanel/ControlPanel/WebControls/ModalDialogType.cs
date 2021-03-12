using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000617 RID: 1559
	[DataContract]
	public enum ModalDialogType
	{
		// Token: 0x04002E80 RID: 11904
		[EnumMember(Value = "Error")]
		Error,
		// Token: 0x04002E81 RID: 11905
		[EnumMember(Value = "Warning")]
		Warning,
		// Token: 0x04002E82 RID: 11906
		[EnumMember(Value = "Information")]
		Information
	}
}
