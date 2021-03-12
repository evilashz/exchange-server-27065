using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005FA RID: 1530
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum Hours
	{
		// Token: 0x04001B8C RID: 7052
		Personal,
		// Token: 0x04001B8D RID: 7053
		Working,
		// Token: 0x04001B8E RID: 7054
		Any
	}
}
