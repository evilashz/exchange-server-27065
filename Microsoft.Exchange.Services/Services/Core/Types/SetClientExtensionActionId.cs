using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200088A RID: 2186
	[XmlType(TypeName = "SetClientExtensionActionId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum SetClientExtensionActionId
	{
		// Token: 0x040023F1 RID: 9201
		Install,
		// Token: 0x040023F2 RID: 9202
		Uninstall,
		// Token: 0x040023F3 RID: 9203
		Configure
	}
}
