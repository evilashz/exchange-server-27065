using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000F3 RID: 243
	internal interface IUserConfigurationAccessStrategy
	{
		// Token: 0x060008AE RID: 2222
		IUserConfiguration CreateConfiguration(MailboxSession mailboxSession, string configurationName, UserConfigurationTypes dataType);

		// Token: 0x060008AF RID: 2223
		IReadableUserConfiguration GetReadOnlyConfiguration(MailboxSession mailboxSession, string configName, UserConfigurationTypes dataType);

		// Token: 0x060008B0 RID: 2224
		IUserConfiguration GetConfiguration(MailboxSession mailboxSession, string configName, UserConfigurationTypes dataType);

		// Token: 0x060008B1 RID: 2225
		OperationResult DeleteConfigurations(MailboxSession mailboxSession, params string[] configurationNames);
	}
}
