using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200066B RID: 1643
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IncludeInSchema = false)]
	[Serializable]
	public enum SyncFolderItemsChangesEnum
	{
		// Token: 0x04001CAD RID: 7341
		Create,
		// Token: 0x04001CAE RID: 7342
		Delete,
		// Token: 0x04001CAF RID: 7343
		ReadFlagChange,
		// Token: 0x04001CB0 RID: 7344
		Update
	}
}
