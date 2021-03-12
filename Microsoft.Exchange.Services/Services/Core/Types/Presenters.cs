using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000601 RID: 1537
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum Presenters
	{
		// Token: 0x04001BA4 RID: 7076
		Disabled,
		// Token: 0x04001BA5 RID: 7077
		Internal,
		// Token: 0x04001BA6 RID: 7078
		Everyone
	}
}
