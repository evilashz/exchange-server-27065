using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005C5 RID: 1477
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ContactSourceType
	{
		// Token: 0x04001AC4 RID: 6852
		ActiveDirectory,
		// Token: 0x04001AC5 RID: 6853
		Store
	}
}
