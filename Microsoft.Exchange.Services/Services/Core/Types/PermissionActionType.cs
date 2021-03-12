using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200060A RID: 1546
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum PermissionActionType
	{
		// Token: 0x04001BD1 RID: 7121
		None,
		// Token: 0x04001BD2 RID: 7122
		Owned,
		// Token: 0x04001BD3 RID: 7123
		All
	}
}
