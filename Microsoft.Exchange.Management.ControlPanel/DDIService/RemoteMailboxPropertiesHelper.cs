using System;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000530 RID: 1328
	public static class RemoteMailboxPropertiesHelper
	{
		// Token: 0x06003F04 RID: 16132 RVA: 0x000BD994 File Offset: 0x000BBB94
		public static void GetObjectPostAction(DataRow inputRow, DataTable table, DataObjectStore store)
		{
			DataRow dataRow = table.Rows[0];
			if (dataRow["SimpleEmailAddresses"] != DBNull.Value)
			{
				dataRow["EmailAddresses"] = EmailAddressList.FromProxyAddressCollection((ProxyAddressCollection)dataRow["SimpleEmailAddresses"]);
			}
			if (dataRow["ObjectCountryOrRegion"] != DBNull.Value)
			{
				dataRow["CountryOrRegion"] = ((CountryInfo)dataRow["ObjectCountryOrRegion"]).Name;
			}
			MailboxPropertiesHelper.GetMaxSendReceiveSize(inputRow, table, store);
			MailboxPropertiesHelper.GetAcceptRejectSendersOrMembers(inputRow, table, store);
			if (dataRow["EmailAddresses"] != DBNull.Value && dataRow["RemoteRoutingAddress"] != DBNull.Value)
			{
				EmailAddressList emailAddressList = (EmailAddressList)dataRow["EmailAddresses"];
				string strA = (string)dataRow["RemoteRoutingAddress"];
				foreach (EmailAddressItem emailAddressItem in emailAddressList)
				{
					string identity = emailAddressItem.Identity;
					if (string.Compare(strA, identity, true) == 0)
					{
						dataRow["RemoteRoutingAddress"] = identity;
						break;
					}
				}
			}
			dataRow["IsRemoteUserMailbox"] = ((RecipientTypeDetails)((ulong)int.MinValue)).Equals(dataRow["RecipientTypeDetails"]);
		}

		// Token: 0x06003F05 RID: 16133 RVA: 0x000BDAF4 File Offset: 0x000BBCF4
		public static void GetRecipientPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			RemoteMailboxPropertiesHelper.GetObjectPostAction(inputRow, dataTable, store);
			MailboxPropertiesHelper.GetRecipientPostAction(inputRow, dataTable, store);
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x000BDB06 File Offset: 0x000BBD06
		public static void SetObjectPreAction(DataRow inputRow, DataTable table, DataObjectStore store)
		{
			MailboxPropertiesHelper.SetMaxSendReceiveSize(inputRow, table, store);
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x000BDB10 File Offset: 0x000BBD10
		public static void GetRemoteMailboxPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow row = dataTable.Rows[0];
			RemoteMailbox remoteMailbox = store.GetDataObject("RemoteMailbox") as RemoteMailbox;
			if (remoteMailbox != null)
			{
				MailboxPropertiesHelper.TrySetColumnValue(row, "MailboxCanHaveArchive", remoteMailbox.ExchangeVersion.CompareTo(ExchangeObjectVersion.Exchange2010) >= 0 && (remoteMailbox.RecipientTypeDetails == (RecipientTypeDetails)((ulong)int.MinValue) || remoteMailbox.RecipientTypeDetails == RecipientTypeDetails.RemoteRoomMailbox || remoteMailbox.RecipientTypeDetails == RecipientTypeDetails.RemoteEquipmentMailbox || remoteMailbox.RecipientTypeDetails == RecipientTypeDetails.RemoteSharedMailbox));
				MailboxPropertiesHelper.TrySetColumnValue(row, "EnableArchive", remoteMailbox.ArchiveState != ArchiveState.None);
				MailboxPropertiesHelper.TrySetColumnValue(row, "HasArchive", remoteMailbox.ArchiveState != ArchiveState.None);
				MailboxPropertiesHelper.TrySetColumnValue(row, "RemoteArchive", remoteMailbox.ArchiveState == ArchiveState.HostedProvisioned || remoteMailbox.ArchiveState == ArchiveState.HostedPending);
			}
		}

		// Token: 0x040028C0 RID: 10432
		private const string RemoteMailboxObjectName = "RemoteMailbox";

		// Token: 0x040028C1 RID: 10433
		private const string MailboxCanHaveArchiveColumnName = "MailboxCanHaveArchive";

		// Token: 0x040028C2 RID: 10434
		private const string EnableArchiveColumnName = "EnableArchive";

		// Token: 0x040028C3 RID: 10435
		private const string HasArchiveColumnName = "HasArchive";

		// Token: 0x040028C4 RID: 10436
		private const string RemoteArchiveColumnName = "RemoteArchive";
	}
}
