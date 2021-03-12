using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000D92 RID: 3474
	public static class UnlockMoveTargetUtil
	{
		// Token: 0x06008587 RID: 34183 RVA: 0x00221F94 File Offset: 0x00220194
		public static bool IsValidLockedStatus(RequestStatus status)
		{
			if (status != RequestStatus.None)
			{
				switch (status)
				{
				case RequestStatus.Completed:
				case RequestStatus.CompletedWithWarning:
					break;
				default:
					if (status != RequestStatus.Failed)
					{
						return true;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x06008588 RID: 34184 RVA: 0x00221FC0 File Offset: 0x002201C0
		public static bool IsMailboxLocked(string serverFQDN, Guid dbGuid, Guid mbxGuid)
		{
			bool result;
			using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=Management", serverFQDN, null, null, null))
			{
				PropValue[][] array;
				try
				{
					array = exRpcAdmin.GetMailboxTableInfo(dbGuid, mbxGuid, new PropTag[]
					{
						PropTag.UserGuid,
						PropTag.MailboxMiscFlags
					});
				}
				catch (MapiExceptionNotFound)
				{
					array = null;
				}
				bool flag = false;
				if (array != null)
				{
					foreach (PropValue[] array3 in array)
					{
						if (array3 != null && array3.Length == 2 && array3[0].PropTag == PropTag.UserGuid)
						{
							byte[] bytes = array3[0].GetBytes();
							Guid a = (bytes != null && bytes.Length == 16) ? new Guid(bytes) : Guid.Empty;
							if (a == mbxGuid)
							{
								MailboxMiscFlags mailboxMiscFlags = (MailboxMiscFlags)((array3[1].PropTag == PropTag.MailboxMiscFlags) ? array3[1].GetInt() : 0);
								flag = ((mailboxMiscFlags & MailboxMiscFlags.CreatedByMove) != MailboxMiscFlags.None);
								break;
							}
						}
					}
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x06008589 RID: 34185 RVA: 0x002220E4 File Offset: 0x002202E4
		public static void UnlockMoveTarget(string serverFQDN, Guid dbGuid, Guid mbxGuid, OrganizationId ordID)
		{
			using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=Management", serverFQDN, null, null, null))
			{
				exRpcAdmin.PurgeCachedMailboxObject(mbxGuid);
			}
			ExchangePrincipal mailboxOwner = ExchangePrincipal.FromMailboxData(mbxGuid, dbGuid, ordID ?? OrganizationId.ForestWideOrgId, UnlockMoveTargetUtil.EmptyCultures, RemotingOptions.AllowCrossSite);
			using (MailboxSession mailboxSession = MailboxSession.OpenAsSystemService(mailboxOwner, CultureInfo.InvariantCulture, "Client=MSExchangeMigration;Action=MailboxRepairRequestUnlockMailbox"))
			{
				mailboxSession.Mailbox.SetProperties(new PropertyDefinition[]
				{
					MailboxSchema.InTransitStatus
				}, new object[]
				{
					InTransitStatus.SyncDestination
				});
				mailboxSession.Mailbox.Save();
				mailboxSession.Mailbox.Load();
				mailboxSession.Mailbox.SetProperties(new PropertyDefinition[]
				{
					MailboxSchema.InTransitStatus
				}, new object[]
				{
					InTransitStatus.NotInTransit
				});
				mailboxSession.Mailbox.Save();
			}
		}

		// Token: 0x04004075 RID: 16501
		private static readonly CultureInfo[] EmptyCultures = new CultureInfo[0];
	}
}
