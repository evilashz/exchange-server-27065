using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000017 RID: 23
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum SpecialLogonType
	{
		// Token: 0x0400005C RID: 92
		Admin,
		// Token: 0x0400005D RID: 93
		SystemService
	}
}
