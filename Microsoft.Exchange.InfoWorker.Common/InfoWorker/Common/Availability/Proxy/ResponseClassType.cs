using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x0200010D RID: 269
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ResponseClassType
	{
		// Token: 0x04000464 RID: 1124
		Success,
		// Token: 0x04000465 RID: 1125
		Warning,
		// Token: 0x04000466 RID: 1126
		Error
	}
}
