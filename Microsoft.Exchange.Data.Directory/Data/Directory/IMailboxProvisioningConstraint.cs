using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000067 RID: 103
	public interface IMailboxProvisioningConstraint
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004BA RID: 1210
		string Value { get; }

		// Token: 0x060004BB RID: 1211
		bool IsMatch(MailboxProvisioningAttributes attributes);
	}
}
