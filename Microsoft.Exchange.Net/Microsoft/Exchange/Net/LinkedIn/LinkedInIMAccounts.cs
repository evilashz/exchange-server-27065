using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.LinkedIn
{
	// Token: 0x0200074C RID: 1868
	[DataContract]
	public class LinkedInIMAccounts : IExtensibleDataObject
	{
		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06002479 RID: 9337 RVA: 0x0004C4E3 File Offset: 0x0004A6E3
		// (set) Token: 0x0600247A RID: 9338 RVA: 0x0004C4EB File Offset: 0x0004A6EB
		public ExtensionDataObject ExtensionData { get; set; }

		// Token: 0x04002225 RID: 8741
		[DataMember(Name = "values")]
		public List<LinkedInIMAccount> Accounts;
	}
}
