using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200073B RID: 1851
	[XmlType(TypeName = "DelegateFolderPermissionLevelType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum DelegateFolderPermissionLevelType
	{
		// Token: 0x04001EFB RID: 7931
		Default,
		// Token: 0x04001EFC RID: 7932
		None,
		// Token: 0x04001EFD RID: 7933
		Editor,
		// Token: 0x04001EFE RID: 7934
		Reviewer,
		// Token: 0x04001EFF RID: 7935
		Author,
		// Token: 0x04001F00 RID: 7936
		Custom
	}
}
