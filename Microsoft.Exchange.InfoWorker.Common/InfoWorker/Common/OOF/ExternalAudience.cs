using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.OOF
{
	// Token: 0x02000021 RID: 33
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum ExternalAudience
	{
		// Token: 0x0400004A RID: 74
		None,
		// Token: 0x0400004B RID: 75
		Known,
		// Token: 0x0400004C RID: 76
		All
	}
}
