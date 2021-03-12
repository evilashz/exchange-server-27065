using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200064F RID: 1615
	[XmlType(TypeName = "SetClutterStateCommand", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum SetClutterStateCommand
	{
		// Token: 0x04001C91 RID: 7313
		EnableClutter,
		// Token: 0x04001C92 RID: 7314
		DisableClutter
	}
}
