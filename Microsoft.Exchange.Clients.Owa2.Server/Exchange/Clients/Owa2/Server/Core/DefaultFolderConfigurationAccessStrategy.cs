using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000F5 RID: 245
	internal class DefaultFolderConfigurationAccessStrategy : IUserConfigurationAccessStrategy
	{
		// Token: 0x060008B7 RID: 2231 RVA: 0x0001CBF1 File Offset: 0x0001ADF1
		public DefaultFolderConfigurationAccessStrategy(DefaultFolderType folderType)
		{
			this.folderType = folderType;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001CC00 File Offset: 0x0001AE00
		public IUserConfiguration CreateConfiguration(MailboxSession mailboxSession, string configurationName, UserConfigurationTypes dataType)
		{
			return mailboxSession.UserConfigurationManager.CreateFolderConfiguration(configurationName, dataType, this.GetDefaultFolderId(mailboxSession));
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001CC16 File Offset: 0x0001AE16
		public IReadableUserConfiguration GetReadOnlyConfiguration(MailboxSession mailboxSession, string configName, UserConfigurationTypes freefetchDataTypes)
		{
			return mailboxSession.UserConfigurationManager.GetReadOnlyFolderConfiguration(configName, freefetchDataTypes, this.GetDefaultFolderId(mailboxSession));
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001CC2C File Offset: 0x0001AE2C
		public IUserConfiguration GetConfiguration(MailboxSession mailboxSession, string configName, UserConfigurationTypes freefetchDataTypes)
		{
			return mailboxSession.UserConfigurationManager.GetFolderConfiguration(configName, freefetchDataTypes, this.GetDefaultFolderId(mailboxSession));
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001CC42 File Offset: 0x0001AE42
		public OperationResult DeleteConfigurations(MailboxSession mailboxSession, params string[] configurationNames)
		{
			return mailboxSession.UserConfigurationManager.DeleteFolderConfigurations(this.GetDefaultFolderId(mailboxSession), configurationNames);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0001CC57 File Offset: 0x0001AE57
		private StoreId GetDefaultFolderId(MailboxSession mailboxSession)
		{
			if (this.folderId == null)
			{
				this.folderId = mailboxSession.GetDefaultFolderId(this.folderType);
			}
			return this.folderId;
		}

		// Token: 0x04000563 RID: 1379
		private DefaultFolderType folderType;

		// Token: 0x04000564 RID: 1380
		private StoreId folderId;
	}
}
