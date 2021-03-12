using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200045B RID: 1115
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ResolveNamesSearchScopeType
	{
		// Token: 0x04001704 RID: 5892
		ActiveDirectory,
		// Token: 0x04001705 RID: 5893
		ActiveDirectoryContacts,
		// Token: 0x04001706 RID: 5894
		Contacts,
		// Token: 0x04001707 RID: 5895
		ContactsActiveDirectory
	}
}
