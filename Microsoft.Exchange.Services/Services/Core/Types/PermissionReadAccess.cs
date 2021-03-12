using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200060B RID: 1547
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum PermissionReadAccess
	{
		// Token: 0x04001BD5 RID: 7125
		None,
		// Token: 0x04001BD6 RID: 7126
		FullDetails
	}
}
