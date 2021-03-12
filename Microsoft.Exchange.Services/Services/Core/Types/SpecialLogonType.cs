using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200088F RID: 2191
	[XmlType(TypeName = "SpecialLogonTypeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum SpecialLogonType
	{
		// Token: 0x040023FE RID: 9214
		Admin,
		// Token: 0x040023FF RID: 9215
		SystemService
	}
}
