using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200037A RID: 890
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ResolveNamesSearchScopeType
	{
		// Token: 0x040012B2 RID: 4786
		ActiveDirectory,
		// Token: 0x040012B3 RID: 4787
		ActiveDirectoryContacts,
		// Token: 0x040012B4 RID: 4788
		Contacts,
		// Token: 0x040012B5 RID: 4789
		ContactsActiveDirectory
	}
}
