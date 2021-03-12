using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008BA RID: 2234
	[XmlType(TypeName = "DistinguishedUserType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum DistinguishedUserType
	{
		// Token: 0x04002441 RID: 9281
		None,
		// Token: 0x04002442 RID: 9282
		Default,
		// Token: 0x04002443 RID: 9283
		Anonymous
	}
}
