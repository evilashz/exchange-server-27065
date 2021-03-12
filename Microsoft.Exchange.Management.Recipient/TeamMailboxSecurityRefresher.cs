using System;
using System.DirectoryServices;
using System.Security.AccessControl;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Data.Storage.LinkedFolder;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000E8 RID: 232
	internal class TeamMailboxSecurityRefresher : ITeamMailboxSecurityRefresher
	{
		// Token: 0x060011F4 RID: 4596 RVA: 0x000410BC File Offset: 0x0003F2BC
		public void Refresh(ADUser mailbox, IRecipientSession writableAdSession)
		{
			if (mailbox == null)
			{
				throw new ArgumentNullException("mailbox");
			}
			if (writableAdSession == null)
			{
				throw new ArgumentNullException("writableAdSession");
			}
			MapiMessageStoreSession mapiMessageStoreSession = null;
			try
			{
				ActiveManager activeManagerInstance = ActiveManager.GetActiveManagerInstance();
				DatabaseLocationInfo serverForDatabase = activeManagerInstance.GetServerForDatabase(mailbox.Database.ObjectGuid);
				mapiMessageStoreSession = new MapiMessageStoreSession(serverForDatabase.ServerLegacyDN, TeamMailboxSecurityRefresher.CalculateSystemAttendantMailboxLegacyDistingushName(serverForDatabase.ServerLegacyDN), Fqdn.Parse(serverForDatabase.ServerFqdn));
				MailboxId mailboxId = new MailboxId(MapiTaskHelper.ConvertDatabaseADObjectIdToDatabaseId(mailbox.Database), mailbox.ExchangeGuid);
				try
				{
					mapiMessageStoreSession.Administration.PurgeCachedMailboxObject(mailboxId.MailboxGuid);
				}
				catch (Microsoft.Exchange.Data.Mapi.Common.MailboxNotFoundException ex)
				{
					throw new ObjectNotFoundException(new LocalizedString(ex.ToString()));
				}
				catch (DatabaseUnavailableException ex2)
				{
					throw new ObjectNotFoundException(new LocalizedString(ex2.ToString()));
				}
			}
			finally
			{
				if (mapiMessageStoreSession != null)
				{
					mapiMessageStoreSession.Dispose();
				}
			}
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x000411A8 File Offset: 0x0003F3A8
		private static ActiveDirectorySecurity ConvertToActiveDirectorySecurity(RawSecurityDescriptor rawSd)
		{
			ActiveDirectorySecurity activeDirectorySecurity = new ActiveDirectorySecurity();
			byte[] array = new byte[rawSd.BinaryLength];
			rawSd.GetBinaryForm(array, 0);
			activeDirectorySecurity.SetSecurityDescriptorBinaryForm(array);
			return activeDirectorySecurity;
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x000411D7 File Offset: 0x0003F3D7
		private static string CalculateSystemAttendantMailboxLegacyDistingushName(string serverLegacyDistingushName)
		{
			return string.Format("{0}/cn=Microsoft System Attendant", serverLegacyDistingushName);
		}
	}
}
