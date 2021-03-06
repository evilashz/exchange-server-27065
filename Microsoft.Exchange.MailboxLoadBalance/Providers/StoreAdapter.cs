using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxLoadBalance.Providers
{
	// Token: 0x020000C7 RID: 199
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class StoreAdapter : IStorePort
	{
		// Token: 0x0600066B RID: 1643 RVA: 0x0001223A File Offset: 0x0001043A
		public StoreAdapter(ILogger logger)
		{
			this.logger = logger;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001224C File Offset: 0x0001044C
		public IEnumerable<MailboxTableEntry> GetMailboxTable(DirectoryDatabase database, Guid mailboxGuid, PropTag[] propertiesToLoad)
		{
			IEnumerable<MailboxTableEntry> result;
			using (ExRpcAdmin exRpcAdminForDatabase = this.GetExRpcAdminForDatabase(database))
			{
				PropTag[] propTagsRequested = propertiesToLoad ?? MailboxTablePropertyDefinitions.MailboxTablePropertiesToLoad;
				PropValue[][] mailboxTableInfo = exRpcAdminForDatabase.GetMailboxTableInfo(database.Guid, mailboxGuid, MailboxTableFlags.IncludeSoftDeletedMailbox, propTagsRequested);
				result = mailboxTableInfo.Select(new Func<PropValue[], MailboxTableEntry>(MailboxTableEntry.FromValues));
			}
			return result;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x000122AC File Offset: 0x000104AC
		public DatabaseSizeInfo GetDatabaseSize(DirectoryDatabase database)
		{
			DatabaseSizeInfo result;
			using (ExRpcAdmin exRpcAdminForDatabase = this.GetExRpcAdminForDatabase(database))
			{
				ulong bytesValue;
				ulong bytesValue2;
				exRpcAdminForDatabase.GetDatabaseSize(database.Guid, out bytesValue, out bytesValue2);
				ByteQuantifiedSize byteQuantifiedSize = ByteQuantifiedSize.FromBytes(bytesValue);
				ByteQuantifiedSize byteQuantifiedSize2 = ByteQuantifiedSize.FromBytes(bytesValue2);
				if (byteQuantifiedSize2 > byteQuantifiedSize)
				{
					this.logger.LogWarning("Database {0} has more free space ({1}) than its total size ({2}), assuming sparse EDB file with no free pages.", new object[]
					{
						database.Name,
						byteQuantifiedSize2,
						byteQuantifiedSize
					});
					byteQuantifiedSize2 = ByteQuantifiedSize.Zero;
				}
				result = new DatabaseSizeInfo
				{
					AvailableWhitespace = byteQuantifiedSize2,
					CurrentPhysicalSize = byteQuantifiedSize
				};
			}
			return result;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00012364 File Offset: 0x00010564
		private ExRpcAdmin GetExRpcAdminForDatabase(DirectoryDatabase database)
		{
			ActiveManager activeManager = LoadBalanceADSettings.Instance.Value.UseCachingActiveManager ? ActiveManager.GetCachingActiveManagerInstance() : ActiveManager.GetNoncachingActiveManagerInstance();
			DatabaseLocationInfo serverForDatabase = activeManager.GetServerForDatabase(database.Guid, true);
			return ExRpcAdmin.Create("Client=MSExchangeMailboxLoadBalance", serverForDatabase.ServerFqdn, null, null, null);
		}

		// Token: 0x04000271 RID: 625
		private readonly ILogger logger;
	}
}
