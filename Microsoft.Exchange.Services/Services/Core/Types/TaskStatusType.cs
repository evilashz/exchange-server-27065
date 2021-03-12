using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008A1 RID: 2209
	[XmlType(TypeName = "TaskStatusType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum TaskStatusType
	{
		// Token: 0x04002422 RID: 9250
		NotStarted,
		// Token: 0x04002423 RID: 9251
		InProgress,
		// Token: 0x04002424 RID: 9252
		Completed,
		// Token: 0x04002425 RID: 9253
		WaitingOnOthers,
		// Token: 0x04002426 RID: 9254
		Deferred
	}
}
