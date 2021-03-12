using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200036E RID: 878
	public static class PublicFolderMailboxService
	{
		// Token: 0x06003012 RID: 12306 RVA: 0x00092560 File Offset: 0x00090760
		public static void MailboxUsageGetObjectPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			Mailbox mailbox = store.GetDataObject("Mailbox") as Mailbox;
			if (mailbox != null)
			{
				MailboxStatistics mailboxStatistics = store.GetDataObject("MailboxStatistics") as MailboxStatistics;
				MailboxDatabase mailboxDatabase = store.GetDataObject("MailboxDatabase") as MailboxDatabase;
				MailboxStatistics archiveStatistics = store.GetDataObject("ArchiveStatistics") as MailboxStatistics;
				MailboxPropertiesHelper.MailboxUsage mailboxUsage = new MailboxPropertiesHelper.MailboxUsage(mailbox, mailboxDatabase, mailboxStatistics, archiveStatistics);
				dataRow["MailboxUsage"] = new StatisticsBarData(mailboxUsage.MailboxUsagePercentage, mailboxUsage.MailboxUsageState, mailboxUsage.MailboxUsageText);
				if ((mailbox.UseDatabaseQuotaDefaults != null && mailbox.UseDatabaseQuotaDefaults.Value && mailboxDatabase != null && !Util.IsDataCenter) || !mailbox.ProhibitSendQuota.IsUnlimited)
				{
					dataRow["IsMailboxUsageUnlimited"] = false;
				}
				else
				{
					dataRow["IsMailboxUsageUnlimited"] = true;
				}
				Unlimited<ByteQuantifiedSize> maxReceiveSize = mailbox.MaxReceiveSize;
				if (maxReceiveSize.IsUnlimited)
				{
					dataRow["MaxReceiveSize"] = "unlimited";
				}
				else
				{
					dataRow["MaxReceiveSize"] = PublicFolderMailboxService.UnlimitedByteQuantifiedSizeToString(maxReceiveSize);
				}
				dataRow["IssueWarningQuota"] = PublicFolderMailboxService.UnlimitedByteQuantifiedSizeToString(mailboxUsage.IssueWarningQuota);
				dataRow["ProhibitSendQuota"] = PublicFolderMailboxService.UnlimitedByteQuantifiedSizeToString(mailboxUsage.ProhibitSendQuota);
				dataRow["ProhibitSendReceiveQuota"] = PublicFolderMailboxService.UnlimitedByteQuantifiedSizeToString(mailboxUsage.ProhibitSendReceiveQuota);
				dataRow["RetainDeletedItemsFor"] = mailboxUsage.RetainDeletedItemsFor.Days.ToString();
				dataRow["RetainDeletedItemsUntilBackup"] = mailboxUsage.RetainDeletedItemsUntilBackup;
			}
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x0009272C File Offset: 0x0009092C
		public static void SetObjectPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			List<string> list = new List<string>();
			PublicFolderMailboxService.SaveQuotaProperty(dataRow, "UseDatabaseQuotaDefaults", "IssueWarningQuota", list);
			PublicFolderMailboxService.SaveQuotaProperty(dataRow, "UseDatabaseQuotaDefaults", "ProhibitSendReceiveQuota", list);
			Unlimited<ByteQuantifiedSize> unlimited = Unlimited<ByteQuantifiedSize>.UnlimitedValue;
			if (!DBNull.Value.Equals(dataRow["MaxReceiveSize"]))
			{
				list.Add("MaxReceiveSize");
				unlimited = Unlimited<ByteQuantifiedSize>.Parse((string)dataRow["MaxReceiveSize"]);
				store.SetModifiedColumns(list);
			}
			inputRow["MaxReceiveSize"] = unlimited;
			dataRow["MaxReceiveSize"] = unlimited;
			if (list.Count != 0)
			{
				store.SetModifiedColumns(list);
			}
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000927E4 File Offset: 0x000909E4
		public static void SaveQuotaProperty(DataRow row, string isDefaultColumnName, string quotaPropertyColumnName, List<string> modifiedQuotaColumns)
		{
			if (DBNull.Value != row[quotaPropertyColumnName])
			{
				string text = (string)row[quotaPropertyColumnName];
				if (string.Equals(text.Trim(), Unlimited<ByteQuantifiedSize>.UnlimitedString, StringComparison.OrdinalIgnoreCase))
				{
					row[quotaPropertyColumnName] = Unlimited<ByteQuantifiedSize>.UnlimitedValue;
				}
				else
				{
					row[quotaPropertyColumnName] = Unlimited<ByteQuantifiedSize>.Parse(text);
				}
				modifiedQuotaColumns.Add(quotaPropertyColumnName);
			}
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x0009284C File Offset: 0x00090A4C
		public static string UnlimitedByteQuantifiedSizeToString(object val)
		{
			if (!(val is Unlimited<ByteQuantifiedSize>))
			{
				return string.Empty;
			}
			Unlimited<ByteQuantifiedSize> unlimited = (Unlimited<ByteQuantifiedSize>)val;
			if (!unlimited.IsUnlimited)
			{
				return unlimited.Value.ToBytes().ToString();
			}
			return "unlimited";
		}

		// Token: 0x04002340 RID: 9024
		private const string MailboxObjectName = "Mailbox";

		// Token: 0x04002341 RID: 9025
		private const string MailboxStatisticsObjectName = "MailboxStatistics";

		// Token: 0x04002342 RID: 9026
		private const string MailboxDatabaseObjectName = "MailboxDatabase";

		// Token: 0x04002343 RID: 9027
		private const string ArchiveStatisticsObjectName = "ArchiveStatistics";

		// Token: 0x04002344 RID: 9028
		private const string MailboxUsageColumnName = "MailboxUsage";

		// Token: 0x04002345 RID: 9029
		private const string MailboxUsageUnlimitedColumnName = "IsMailboxUsageUnlimited";

		// Token: 0x04002346 RID: 9030
		private const string WarningQuotaColumnName = "IssueWarningQuota";

		// Token: 0x04002347 RID: 9031
		private const string ProhibitSendQuotaColumnName = "ProhibitSendQuota";

		// Token: 0x04002348 RID: 9032
		private const string ProhibitSendReceiveQuotaColumnName = "ProhibitSendReceiveQuota";

		// Token: 0x04002349 RID: 9033
		private const string RetentionDaysColumnName = "RetainDeletedItemsFor";

		// Token: 0x0400234A RID: 9034
		private const string RetainUntilBackUpColumnName = "RetainDeletedItemsUntilBackup";

		// Token: 0x0400234B RID: 9035
		private const string MaxReceiveSizeMailbox = "MaxReceiveSize";

		// Token: 0x0400234C RID: 9036
		private const string UseDatabaseQuotaDefaultsColumnName = "UseDatabaseQuotaDefaults";
	}
}
