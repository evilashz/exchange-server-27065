using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005C8 RID: 1480
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum TransitionTargetKindType
	{
		// Token: 0x04001AD0 RID: 6864
		Period,
		// Token: 0x04001AD1 RID: 6865
		Group
	}
}
