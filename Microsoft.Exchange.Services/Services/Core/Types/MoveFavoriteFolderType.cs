using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005CD RID: 1485
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum MoveFavoriteFolderType
	{
		// Token: 0x04001AE7 RID: 6887
		BeforeTarget,
		// Token: 0x04001AE8 RID: 6888
		AfterTarget
	}
}
