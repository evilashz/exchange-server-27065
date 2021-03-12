using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000D2 RID: 210
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum RetentionActionType
	{
		// Token: 0x040005D4 RID: 1492
		None,
		// Token: 0x040005D5 RID: 1493
		MoveToDeletedItems,
		// Token: 0x040005D6 RID: 1494
		MoveToFolder,
		// Token: 0x040005D7 RID: 1495
		DeleteAndAllowRecovery,
		// Token: 0x040005D8 RID: 1496
		PermanentlyDelete,
		// Token: 0x040005D9 RID: 1497
		MarkAsPastRetentionLimit,
		// Token: 0x040005DA RID: 1498
		MoveToArchive
	}
}
