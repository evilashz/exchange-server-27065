using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005FB RID: 1531
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum Priority
	{
		// Token: 0x04001B90 RID: 7056
		Low,
		// Token: 0x04001B91 RID: 7057
		Normal,
		// Token: 0x04001B92 RID: 7058
		High
	}
}
