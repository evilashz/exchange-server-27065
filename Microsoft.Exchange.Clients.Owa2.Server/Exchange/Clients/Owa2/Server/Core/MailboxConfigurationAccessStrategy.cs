using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000F4 RID: 244
	internal class MailboxConfigurationAccessStrategy : IUserConfigurationAccessStrategy
	{
		// Token: 0x060008B2 RID: 2226 RVA: 0x0001CBAE File Offset: 0x0001ADAE
		public IUserConfiguration CreateConfiguration(MailboxSession mailboxSession, string configurationName, UserConfigurationTypes dataType)
		{
			return mailboxSession.UserConfigurationManager.CreateMailboxConfiguration(configurationName, dataType);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0001CBBD File Offset: 0x0001ADBD
		public IReadableUserConfiguration GetReadOnlyConfiguration(MailboxSession mailboxSession, string configName, UserConfigurationTypes freefetchDataTypes)
		{
			return mailboxSession.UserConfigurationManager.GetReadOnlyMailboxConfiguration(configName, freefetchDataTypes);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001CBCC File Offset: 0x0001ADCC
		public IUserConfiguration GetConfiguration(MailboxSession mailboxSession, string configName, UserConfigurationTypes freefetchDataTypes)
		{
			return mailboxSession.UserConfigurationManager.GetMailboxConfiguration(configName, freefetchDataTypes);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0001CBDB File Offset: 0x0001ADDB
		public OperationResult DeleteConfigurations(MailboxSession mailboxSession, params string[] configurationNames)
		{
			return mailboxSession.UserConfigurationManager.DeleteMailboxConfigurations(configurationNames);
		}
	}
}
