using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000600 RID: 1536
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum OnlineMeetingAccessLevel
	{
		// Token: 0x04001B9F RID: 7071
		Locked,
		// Token: 0x04001BA0 RID: 7072
		Invited,
		// Token: 0x04001BA1 RID: 7073
		Internal,
		// Token: 0x04001BA2 RID: 7074
		Everyone
	}
}
