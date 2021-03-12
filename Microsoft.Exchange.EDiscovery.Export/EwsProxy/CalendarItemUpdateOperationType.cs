using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000397 RID: 919
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum CalendarItemUpdateOperationType
	{
		// Token: 0x04001334 RID: 4916
		SendToNone,
		// Token: 0x04001335 RID: 4917
		SendOnlyToAll,
		// Token: 0x04001336 RID: 4918
		SendOnlyToChanged,
		// Token: 0x04001337 RID: 4919
		SendToAllAndSaveCopy,
		// Token: 0x04001338 RID: 4920
		SendToChangedAndSaveCopy
	}
}
