using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005CE RID: 1486
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum EmailPositionType
	{
		// Token: 0x04001AEA RID: 6890
		LatestReply,
		// Token: 0x04001AEB RID: 6891
		Other,
		// Token: 0x04001AEC RID: 6892
		Subject,
		// Token: 0x04001AED RID: 6893
		Signature
	}
}
