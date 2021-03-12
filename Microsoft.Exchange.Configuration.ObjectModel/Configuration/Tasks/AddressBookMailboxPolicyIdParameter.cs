using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000E1 RID: 225
	[Serializable]
	public class AddressBookMailboxPolicyIdParameter : ADIdParameter
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x0001E04C File Offset: 0x0001C24C
		public AddressBookMailboxPolicyIdParameter()
		{
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001E054 File Offset: 0x0001C254
		public AddressBookMailboxPolicyIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001E05D File Offset: 0x0001C25D
		public AddressBookMailboxPolicyIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001E066 File Offset: 0x0001C266
		public AddressBookMailboxPolicyIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001E06F File Offset: 0x0001C26F
		public static AddressBookMailboxPolicyIdParameter Parse(string identity)
		{
			return new AddressBookMailboxPolicyIdParameter(identity);
		}
	}
}
