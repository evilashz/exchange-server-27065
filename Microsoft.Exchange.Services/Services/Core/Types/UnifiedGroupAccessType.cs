using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000683 RID: 1667
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum UnifiedGroupAccessType
	{
		// Token: 0x04001CE5 RID: 7397
		None,
		// Token: 0x04001CE6 RID: 7398
		Private,
		// Token: 0x04001CE7 RID: 7399
		Secret,
		// Token: 0x04001CE8 RID: 7400
		Public
	}
}
