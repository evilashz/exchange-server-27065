using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000068 RID: 104
	public interface IMailboxProvisioningConstraints
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004BC RID: 1212
		IMailboxProvisioningConstraint HardConstraint { get; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004BD RID: 1213
		IMailboxProvisioningConstraint[] SoftConstraints { get; }

		// Token: 0x060004BE RID: 1214
		bool IsMatch(MailboxProvisioningAttributes attributes);
	}
}
