using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007EE RID: 2030
	[XmlType(TypeName = "HoldActionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum HoldAction
	{
		// Token: 0x040020C4 RID: 8388
		Create,
		// Token: 0x040020C5 RID: 8389
		Update,
		// Token: 0x040020C6 RID: 8390
		Remove
	}
}
