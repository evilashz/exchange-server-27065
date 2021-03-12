using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005C2 RID: 1474
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum EmailAddressKeyType
	{
		// Token: 0x04001AA8 RID: 6824
		EmailAddress1,
		// Token: 0x04001AA9 RID: 6825
		EmailAddress2,
		// Token: 0x04001AAA RID: 6826
		EmailAddress3
	}
}
