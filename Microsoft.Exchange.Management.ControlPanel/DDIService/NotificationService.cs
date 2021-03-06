using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000243 RID: 579
	public static class NotificationService
	{
		// Token: 0x0600283D RID: 10301 RVA: 0x0007DB48 File Offset: 0x0007BD48
		public static void PostGetNotification(DataRow inputRow, DataTable table, DataObjectStore store)
		{
			int num = 0;
			object value = new string[0];
			IEnumerable<object> enumerable = store.GetDataObject("NotificationSummary") as IEnumerable<object>;
			if (enumerable != null)
			{
				num = enumerable.Count<object>();
				value = (from notification in enumerable
				where SeverityLevelAttribute.FromEnum(((AsyncOperationNotification)notification).Status.GetType(), ((AsyncOperationNotification)notification).Status) == SeverityLevel.Error
				select ((AsyncOperationNotification)notification).AlternativeId).ToArray<string>();
			}
			DataRow dataRow = table.NewRow();
			dataRow["TotalCount"] = num.ToString();
			dataRow["ErrorItems"] = value;
			table.Rows.Add(dataRow);
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x0007DC08 File Offset: 0x0007BE08
		public static string GetItemImg(object notificationType, object notificationStatus, object originalExtAttributes)
		{
			string result = SeverityLevelAttribute.FromEnum(notificationStatus.GetType(), notificationStatus).ToString() + "Img";
			if ((AsyncOperationType)notificationType == AsyncOperationType.Migration && originalExtAttributes != DBNull.Value)
			{
				KeyValuePair<string, LocalizedString>[] source = (KeyValuePair<string, LocalizedString>[])originalExtAttributes;
				KeyValuePair<string, LocalizedString> keyValuePair = source.FirstOrDefault((KeyValuePair<string, LocalizedString> x) => x.Key == AsyncNotificationAdapter.TotalFailedCount);
				int num;
				if (keyValuePair.Key == AsyncNotificationAdapter.TotalFailedCount && int.TryParse(keyValuePair.Value.ToString(), out num) && num > 0)
				{
					result = "WarningImg";
				}
			}
			return result;
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x0007DCB0 File Offset: 0x0007BEB0
		public static string GetStartedByText(object type, object status, object startedBy, object startTime)
		{
			AsyncOperationType asyncOperationType = (AsyncOperationType)type;
			AsyncOperationStatus asyncOperationStatus = (AsyncOperationStatus)status;
			string result = Strings.ProcessStartedBy((startedBy as string) ?? string.Empty, (string)startTime);
			AsyncOperationType asyncOperationType2 = asyncOperationType;
			if (asyncOperationType2 != AsyncOperationType.Migration)
			{
				if (asyncOperationType2 == AsyncOperationType.CertExpiry)
				{
					result = string.Empty;
				}
			}
			else if (asyncOperationStatus == AsyncOperationStatus.Created || asyncOperationStatus == AsyncOperationStatus.Removing)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x0007DD24 File Offset: 0x0007BF24
		public static string GetItemText(object type, object status, object displayName, object percentComplete, object originalExtAttributes)
		{
			AsyncOperationType type2 = (AsyncOperationType)type;
			AsyncOperationStatus status2 = (AsyncOperationStatus)status;
			switch (type2)
			{
			case AsyncOperationType.ImportPST:
			case AsyncOperationType.ExportPST:
				if (originalExtAttributes != DBNull.Value)
				{
					KeyValuePair<string, LocalizedString>[] source = (KeyValuePair<string, LocalizedString>[])originalExtAttributes;
					KeyValuePair<string, LocalizedString> keyValuePair = source.FirstOrDefault((KeyValuePair<string, LocalizedString> x) => x.Key == "Mailbox");
					if (keyValuePair.Key == "Mailbox")
					{
						displayName = keyValuePair.Value.ToString();
					}
				}
				return NotificationService.GetItemTextForProcessingPST(type2, status2, displayName, percentComplete);
			case AsyncOperationType.Migration:
				return NotificationService.GetItemTextForMigration(type2, status2, displayName, originalExtAttributes);
			case AsyncOperationType.CertExpiry:
				return NotificationService.GetItemTextForCertExpiry(status2, displayName, originalExtAttributes);
			}
			return NotificationService.GetItemTextDefault(type2, status2, displayName, percentComplete);
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x0007DE00 File Offset: 0x0007C000
		public static string GetDetailPageId(object type, object status)
		{
			AsyncOperationType asyncOperationType = (AsyncOperationType)type;
			AsyncOperationStatus asyncOperationStatus = (AsyncOperationStatus)status;
			string result = null;
			AsyncOperationType asyncOperationType2 = asyncOperationType;
			if (asyncOperationType2 != AsyncOperationType.Migration)
			{
				if (asyncOperationType2 == AsyncOperationType.CertExpiry)
				{
					result = "Certificates";
				}
			}
			else
			{
				result = "MigrationBatches";
			}
			return result;
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x0007DE38 File Offset: 0x0007C038
		public static string GetLastModifiedTimeString(object datetime)
		{
			return ((DateTime)datetime).ToUniversalTime().ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x0007DE94 File Offset: 0x0007C094
		private static string GetItemTextForCertExpiry(AsyncOperationStatus status, object displayName, object originalExtAttributes)
		{
			string result = string.Empty;
			string server = string.Empty;
			string text = string.Empty;
			if (originalExtAttributes != DBNull.Value)
			{
				KeyValuePair<string, LocalizedString>[] source = (KeyValuePair<string, LocalizedString>[])originalExtAttributes;
				Dictionary<string, object> dictionary = source.ToDictionary((KeyValuePair<string, LocalizedString> item) => item.Key.ToString(), (KeyValuePair<string, LocalizedString> item) => item.Value.ToString());
				server = (string)dictionary["ServerName"];
				text = (string)dictionary["ExpireDate"];
			}
			if (status == AsyncOperationStatus.CertExpired)
			{
				result = Strings.CertExpired((string)displayName, server);
			}
			else
			{
				long fileTime;
				if (long.TryParse(text, out fileTime))
				{
					text = ExDateTime.FromFileTimeUtc(fileTime).ToShortDateString();
				}
				result = Strings.CertExpiring((string)displayName, server, text);
			}
			return result;
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x0007DF70 File Offset: 0x0007C170
		private static string GetItemTextForProcessingPST(AsyncOperationType type, AsyncOperationStatus status, object displayName, object percentComplete)
		{
			return NotificationService.GetItemTextImp(type, status, displayName, percentComplete, new Func<string, string, LocalizedString>(Strings.ProcessPSTQueued), new Func<string, string, string, LocalizedString>(Strings.ProcessPSTInProgressWithPercentage), new Func<string, string, LocalizedString>(Strings.ProcessPSTInProgress), new Func<string, string, LocalizedString>(Strings.ProcessPSTSuspended), new Func<string, string, LocalizedString>(Strings.ProcessPSTCompleted), new Func<string, string, LocalizedString>(Strings.ProcessPSTFailed), new Func<string, string, LocalizedString>(Strings.AsyncProcessWaitingFinalization), null, null, null);
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x0007E06C File Offset: 0x0007C26C
		private static string GetItemTextForMigration(AsyncOperationType type, AsyncOperationStatus status, object displayName, object originalExtAttributes)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			string empty3 = string.Empty;
			if (originalExtAttributes != DBNull.Value)
			{
				KeyValuePair<string, LocalizedString>[] source = (KeyValuePair<string, LocalizedString>[])originalExtAttributes;
				Dictionary<string, string> dictionary = source.ToDictionary((KeyValuePair<string, LocalizedString> item) => item.Key.ToString(), (KeyValuePair<string, LocalizedString> item) => item.Value.ToString());
				dictionary.TryGetValue(AsyncNotificationAdapter.TotalSyncedCount, out empty);
				dictionary.TryGetValue(AsyncNotificationAdapter.TotalItemCount, out empty2);
				dictionary.TryGetValue(AsyncNotificationAdapter.TotalFailedCount, out empty3);
			}
			int totalSyncedCount;
			int.TryParse(empty, out totalSyncedCount);
			int totalItemCount;
			int.TryParse(empty2, out totalItemCount);
			int totalFailedCount;
			int.TryParse(empty3, out totalFailedCount);
			return NotificationService.GetItemTextImp(type, status, displayName, DBNull.Value, new Func<string, string, LocalizedString>(Strings.AsyncProcessQueued), new Func<string, string, string, LocalizedString>(Strings.AsyncProcessInProgressWithPercentage), delegate(string operation, string name)
			{
				if (totalFailedCount > 0)
				{
					return Strings.MigrationInProgressWithFailedCount(operation, name, totalSyncedCount, totalItemCount, totalFailedCount);
				}
				if (totalItemCount > 0)
				{
					return Strings.MigrationInProgressWithSyncedCount(operation, name, totalSyncedCount, totalItemCount);
				}
				return Strings.AsyncProcessInProgress(operation, name);
			}, new Func<string, string, LocalizedString>(Strings.AsyncProcessSuspended), new Func<string, string, LocalizedString>(Strings.AsyncProcessCompleted), new Func<string, string, LocalizedString>(Strings.AsyncProcessFailed), new Func<string, string, LocalizedString>(Strings.AsyncProcessWaitingFinalization), new Func<string, string, LocalizedString>(Strings.AsyncProcessCreated), new Func<string, string, LocalizedString>(Strings.AsyncProcessCompleting), new Func<string, string, LocalizedString>(Strings.AsyncProcessRemoving));
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x0007E1C0 File Offset: 0x0007C3C0
		private static string GetItemTextDefault(AsyncOperationType type, AsyncOperationStatus status, object displayName, object percentComplete)
		{
			return NotificationService.GetItemTextImp(type, status, displayName, percentComplete, new Func<string, string, LocalizedString>(Strings.AsyncProcessQueued), new Func<string, string, string, LocalizedString>(Strings.AsyncProcessInProgressWithPercentage), new Func<string, string, LocalizedString>(Strings.AsyncProcessInProgress), new Func<string, string, LocalizedString>(Strings.AsyncProcessSuspended), new Func<string, string, LocalizedString>(Strings.AsyncProcessCompleted), new Func<string, string, LocalizedString>(Strings.AsyncProcessFailed), new Func<string, string, LocalizedString>(Strings.AsyncProcessWaitingFinalization), null, null, null);
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x0007E230 File Offset: 0x0007C430
		private static string GetItemTextImp(AsyncOperationType type, AsyncOperationStatus status, object displayNameObj, object percentComplete, Func<string, string, LocalizedString> asyncProcessQueued, Func<string, string, string, LocalizedString> asyncProcessInProgressWithPercentage, Func<string, string, LocalizedString> asyncProcessInProgress, Func<string, string, LocalizedString> asyncProcessSuspended, Func<string, string, LocalizedString> asyncProcessCompleted, Func<string, string, LocalizedString> asyncProcessFailed, Func<string, string, LocalizedString> asyncProcessWaitingFinalization, Func<string, string, LocalizedString> asyncProcessCreated = null, Func<string, string, LocalizedString> asyncProcessCompleting = null, Func<string, string, LocalizedString> asyncProcessRemoving = null)
		{
			string arg = (displayNameObj as string) ?? string.Empty;
			string result = string.Empty;
			string arg2 = DDIUtil.EnumToLocalizedString(type);
			switch (status)
			{
			case AsyncOperationStatus.Queued:
				result = asyncProcessQueued(arg2, arg);
				break;
			case AsyncOperationStatus.InProgress:
				result = ((percentComplete != DBNull.Value) ? asyncProcessInProgressWithPercentage(arg2, arg, percentComplete.ToString()) : asyncProcessInProgress(arg2, arg));
				break;
			case AsyncOperationStatus.Suspended:
				result = asyncProcessSuspended(arg2, arg);
				break;
			case AsyncOperationStatus.Completed:
				result = asyncProcessCompleted(arg2, arg);
				break;
			case AsyncOperationStatus.Failed:
				result = asyncProcessFailed(arg2, arg);
				break;
			case AsyncOperationStatus.WaitingForFinalization:
				result = asyncProcessWaitingFinalization(arg2, arg);
				break;
			case AsyncOperationStatus.Created:
				if (asyncProcessCreated != null)
				{
					result = asyncProcessCreated(arg2, arg);
				}
				break;
			case AsyncOperationStatus.Completing:
				if (asyncProcessCompleting != null)
				{
					result = asyncProcessCompleting(arg2, arg);
				}
				break;
			case AsyncOperationStatus.Removing:
				if (asyncProcessRemoving != null)
				{
					result = asyncProcessRemoving(arg2, arg);
				}
				break;
			}
			return result;
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x0007E35C File Offset: 0x0007C55C
		public static string GetTypeText(object notificationType)
		{
			switch ((AsyncOperationType)notificationType)
			{
			case AsyncOperationType.ImportPST:
				return Strings.NotificationTypeImportPST;
			case AsyncOperationType.ExportPST:
				return Strings.NotificationTypeExportPST;
			case AsyncOperationType.Migration:
				return Strings.NotificationTypeMigration;
			case AsyncOperationType.MailboxRestore:
				return Strings.NotificationTypeMailboxRestore;
			case AsyncOperationType.CertExpiry:
				return Strings.NotificationTypeCertExpiry;
			}
			return Strings.NotificationTypeUnknown;
		}
	}
}
