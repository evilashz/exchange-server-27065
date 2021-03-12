using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003E1 RID: 993
	[DataContract(Name = "RetentionActionType", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "RetentionActionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public enum RetentionActionType
	{
		// Token: 0x04001254 RID: 4692
		None,
		// Token: 0x04001255 RID: 4693
		MoveToDeletedItems,
		// Token: 0x04001256 RID: 4694
		MoveToFolder,
		// Token: 0x04001257 RID: 4695
		DeleteAndAllowRecovery,
		// Token: 0x04001258 RID: 4696
		PermanentlyDelete,
		// Token: 0x04001259 RID: 4697
		MarkAsPastRetentionLimit,
		// Token: 0x0400125A RID: 4698
		MoveToArchive
	}
}
