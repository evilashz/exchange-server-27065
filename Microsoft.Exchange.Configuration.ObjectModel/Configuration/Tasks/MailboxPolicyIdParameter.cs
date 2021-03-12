using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000129 RID: 297
	[Serializable]
	public class MailboxPolicyIdParameter : ADIdParameter, IIdentityParameter
	{
		// Token: 0x06000A9F RID: 2719 RVA: 0x00022D39 File Offset: 0x00020F39
		public MailboxPolicyIdParameter(string rawString) : base(rawString)
		{
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00022D42 File Offset: 0x00020F42
		public MailboxPolicyIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00022D4B File Offset: 0x00020F4B
		public MailboxPolicyIdParameter(MailboxPolicy policy) : base(policy.Id)
		{
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00022D59 File Offset: 0x00020F59
		public MailboxPolicyIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00022D62 File Offset: 0x00020F62
		public MailboxPolicyIdParameter()
		{
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00022D6A File Offset: 0x00020F6A
		public static MailboxPolicyIdParameter Parse(string rawString)
		{
			return new MailboxPolicyIdParameter(rawString);
		}
	}
}
