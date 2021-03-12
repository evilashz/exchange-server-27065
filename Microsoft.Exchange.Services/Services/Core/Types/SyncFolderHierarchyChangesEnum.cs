using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000666 RID: 1638
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IncludeInSchema = false)]
	[Serializable]
	public enum SyncFolderHierarchyChangesEnum
	{
		// Token: 0x04001C9F RID: 7327
		Create,
		// Token: 0x04001CA0 RID: 7328
		Delete,
		// Token: 0x04001CA1 RID: 7329
		Update
	}
}
