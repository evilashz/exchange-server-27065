using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000866 RID: 2150
	[XmlType("ResolveNamesSearchScopeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ResolveNamesSearchScopeType
	{
		// Token: 0x04002376 RID: 9078
		ActiveDirectory,
		// Token: 0x04002377 RID: 9079
		ActiveDirectoryContacts,
		// Token: 0x04002378 RID: 9080
		Contacts,
		// Token: 0x04002379 RID: 9081
		ContactsActiveDirectory
	}
}
