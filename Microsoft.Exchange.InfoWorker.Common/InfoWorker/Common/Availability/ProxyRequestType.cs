using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000B3 RID: 179
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum ProxyRequestType
	{
		// Token: 0x04000271 RID: 625
		CrossSite,
		// Token: 0x04000272 RID: 626
		CrossForest
	}
}
