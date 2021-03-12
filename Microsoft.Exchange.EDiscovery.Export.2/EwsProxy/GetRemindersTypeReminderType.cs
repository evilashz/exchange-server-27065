using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200034E RID: 846
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public enum GetRemindersTypeReminderType
	{
		// Token: 0x0400124B RID: 4683
		All,
		// Token: 0x0400124C RID: 4684
		Current,
		// Token: 0x0400124D RID: 4685
		Old
	}
}
