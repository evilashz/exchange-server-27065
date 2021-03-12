using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000100 RID: 256
	public static class DatabasePropertiesHelper
	{
		// Token: 0x06001F0F RID: 7951 RVA: 0x0005CF5C File Offset: 0x0005B15C
		public static void GetObjectPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			if (!DBNull.Value.Equals(dataRow["IssueWarningQuota"]))
			{
				Unlimited<ByteQuantifiedSize> unlimited = (Unlimited<ByteQuantifiedSize>)dataRow["IssueWarningQuota"];
				dataRow["IssueWarningQuota"] = MailboxPropertiesHelper.UnlimitedByteQuantifiedSizeToString(unlimited);
			}
			if (!DBNull.Value.Equals(dataRow["ProhibitSendQuota"]))
			{
				Unlimited<ByteQuantifiedSize> unlimited2 = (Unlimited<ByteQuantifiedSize>)dataRow["ProhibitSendQuota"];
				dataRow["ProhibitSendQuota"] = MailboxPropertiesHelper.UnlimitedByteQuantifiedSizeToString(unlimited2);
			}
			if (!DBNull.Value.Equals(dataRow["ProhibitSendReceiveQuota"]))
			{
				Unlimited<ByteQuantifiedSize> unlimited3 = (Unlimited<ByteQuantifiedSize>)dataRow["ProhibitSendReceiveQuota"];
				dataRow["ProhibitSendReceiveQuota"] = MailboxPropertiesHelper.UnlimitedByteQuantifiedSizeToString(unlimited3);
			}
			dataRow["MountStatus"] = ((DDIHelper.IsEmptyValue(dataRow["Mounted"]) || !(bool)dataRow["Mounted"]) ? Strings.StatusDismounted : Strings.StatusMounted);
			if (!DBNull.Value.Equals(dataRow["DeletedItemRetention"]))
			{
				dataRow["DeletedItemRetention"] = ((EnhancedTimeSpan)dataRow["DeletedItemRetention"]).Days.ToString();
			}
			if (!DBNull.Value.Equals(dataRow["MailboxRetention"]))
			{
				dataRow["MailboxRetention"] = ((EnhancedTimeSpan)dataRow["MailboxRetention"]).Days.ToString();
			}
			MailboxDatabase mailboxDatabase = store.GetDataObject("MailboxDatabase") as MailboxDatabase;
			if (mailboxDatabase != null)
			{
				dataRow["Servers"] = mailboxDatabase.Servers;
			}
			if (!DBNull.Value.Equals(dataRow["MaintenanceSchedule"]))
			{
				Schedule schedule = (Schedule)dataRow["MaintenanceSchedule"];
				dataRow["MaintenanceSchedule"] = new ScheduleBuilder(schedule).GetEntireState();
			}
			if (!DBNull.Value.Equals(dataRow["QuotaNotificationSchedule"]))
			{
				Schedule schedule2 = (Schedule)dataRow["QuotaNotificationSchedule"];
				dataRow["QuotaNotificationSchedule"] = new ScheduleBuilder(schedule2).GetEntireState();
			}
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x0005D1A0 File Offset: 0x0005B3A0
		public static void OnPreAddDatabaseCopy(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			if (!DBNull.Value.Equals(dataRow["ReplayLagTime"]) && !DDIHelper.IsEmptyValue(dataRow["ReplayLagTime"]))
			{
				inputRow["ReplayLagTime"] = ((string)dataRow["ReplayLagTime"]).FromTimeSpan(TimeUnit.Day);
			}
			else
			{
				inputRow["ReplayLagTime"] = "0".FromTimeSpan(TimeUnit.Day);
			}
			store.SetModifiedColumns(new List<string>
			{
				"ReplayLagTime"
			});
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x0005D234 File Offset: 0x0005B434
		public static void SetObjectPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			List<string> list = new List<string>();
			MailboxPropertiesHelper.SaveQuotaProperty(dataRow, null, "IssueWarningQuota", list);
			MailboxPropertiesHelper.SaveQuotaProperty(dataRow, null, "ProhibitSendQuota", list);
			MailboxPropertiesHelper.SaveQuotaProperty(dataRow, null, "ProhibitSendReceiveQuota", list);
			if (DBNull.Value != dataRow["DeletedItemRetention"])
			{
				dataRow["DeletedItemRetention"] = EnhancedTimeSpan.Parse((string)dataRow["DeletedItemRetention"]);
				list.Add("DeletedItemRetention");
			}
			if (DBNull.Value != dataRow["MailboxRetention"])
			{
				dataRow["MailboxRetention"] = EnhancedTimeSpan.Parse((string)dataRow["MailboxRetention"]);
				list.Add("MailboxRetention");
			}
			DatabasePropertiesHelper.SetScheduleProperty(dataRow, "MaintenanceSchedule", list);
			DatabasePropertiesHelper.SetScheduleProperty(dataRow, "QuotaNotificationSchedule", list);
			if (list.Count != 0)
			{
				store.SetModifiedColumns(list);
			}
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x0005D328 File Offset: 0x0005B528
		public static void GetMailboxServerPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			int majorVersion = 0;
			string text = inputRow["Version"] as string;
			if (text == "current")
			{
				majorVersion = Server.CurrentExchangeMajorVersion;
			}
			else
			{
				int.TryParse(text, out majorVersion);
			}
			DatabasePropertiesHelper.FilterRowsByAdminDisplayVersion(inputRow, dataTable, store, majorVersion, null);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (DBNull.Value != dataRow["DataPath"])
				{
					LocalLongFullPath localLongFullPath = (LocalLongFullPath)dataRow["DataPath"];
					dataRow["DataPath"] = localLongFullPath.PathName;
				}
			}
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x0005D3F0 File Offset: 0x0005B5F0
		public static void GetServerToAddCopyPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DatabasePropertiesHelper.FilterRowsByAdminDisplayVersion(inputRow, dataTable, store, Server.CurrentExchangeMajorVersion, new Func<DataRow, DataRow, DataObjectStore, bool>(DatabasePropertiesHelper.FindServerInSameDagButDontHaveTheDatabase));
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x0005D428 File Offset: 0x0005B628
		private static bool FindServerInSameDagButDontHaveTheDatabase(DataRow inputRow, DataRow row, DataObjectStore store)
		{
			if (DBNull.Value.Equals(row["DatabaseAvailabilityGroup"]))
			{
				return false;
			}
			MailboxDatabase mailboxDatabase = (MailboxDatabase)store.GetDataObject("MailboxDatabase");
			string b = (string)row["DatabaseAvailabilityGroup"];
			string serverName = (string)row["Name"];
			return mailboxDatabase.MasterServerOrAvailabilityGroup.Name == b && !mailboxDatabase.Servers.Any((ADObjectId x) => serverName.Equals(x.Name, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x0005D4BB File Offset: 0x0005B6BB
		public static void GetServerForSeedingPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DatabasePropertiesHelper.FilterRowsByAdminDisplayVersion(inputRow, dataTable, store, Server.CurrentExchangeMajorVersion, new Func<DataRow, DataRow, DataObjectStore, bool>(DatabasePropertiesHelper.FindServerInSameDatabaseButNotCurrentServer));
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x0005D4D6 File Offset: 0x0005B6D6
		public static void GetServerToManageDAGMember(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DatabasePropertiesHelper.FilterRowsByAdminDisplayVersion(inputRow, dataTable, store, Server.CurrentExchangeMajorVersion, new Func<DataRow, DataRow, DataObjectStore, bool>(DatabasePropertiesHelper.FindServerInSameDagOrNotAnyDag));
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x0005D4F1 File Offset: 0x0005B6F1
		public static void GetServersForSwitchOver(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DatabasePropertiesHelper.FilterRowsByAdminDisplayVersion(inputRow, dataTable, store, Server.CurrentExchangeMajorVersion, new Func<DataRow, DataRow, DataObjectStore, bool>(DatabasePropertiesHelper.FindServerToSwichover));
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x0005D50C File Offset: 0x0005B70C
		public static void FilterRecoveryDatabase(DataTable dataTable)
		{
			List<DataRow> list = new List<DataRow>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (!DBNull.Value.Equals(dataRow["Recovery"]) && (bool)dataRow["Recovery"])
				{
					list.Add(dataRow);
				}
			}
			foreach (DataRow row in list)
			{
				dataTable.Rows.Remove(row);
			}
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0005D5DC File Offset: 0x0005B7DC
		public static void GetMailboxDatabasePostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DatabasePropertiesHelper.FilterRecoveryDatabase(dataTable);
			DatabasePropertiesHelper.FilterRowsByAdminDisplayVersion(inputRow, dataTable, store, 0, new Func<DataRow, DataRow, DataObjectStore, bool>(DatabasePropertiesHelper.IsAdminDisplayVersionE14OrAfter));
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x0005D5FC File Offset: 0x0005B7FC
		private static bool FindServerInSameDagOrNotAnyDag(DataRow inputRow, DataRow row, DataObjectStore store)
		{
			Guid empty = Guid.Empty;
			if (!Guid.TryParse(inputRow["DAGId"].ToString(), out empty))
			{
				return false;
			}
			object obj = row["DatabaseAvailabilityGroupRawValue"];
			if (DBNull.Value.Equals(obj))
			{
				return true;
			}
			if (((ADObjectId)obj).ObjectGuid.Equals(empty))
			{
				row["DatabaseAvailabilityGroup"] = obj.ToString();
				return true;
			}
			return false;
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0005D688 File Offset: 0x0005B888
		private static bool FindServerInSameDatabaseButNotCurrentServer(DataRow inputRow, DataRow row, DataObjectStore store)
		{
			if (DBNull.Value.Equals(row["DatabaseAvailabilityGroup"]))
			{
				return false;
			}
			MailboxDatabase mailboxDatabase = (MailboxDatabase)store.GetDataObject("MailboxDatabase");
			string b = (string)row["DatabaseAvailabilityGroup"];
			string text = (string)row["Name"];
			ADObjectId currentServerId = (ADObjectId)row["Identity"];
			string value = (string)inputRow["CurrentServer"];
			return mailboxDatabase.MasterServerOrAvailabilityGroup.Name == b && mailboxDatabase.Servers.Any((ADObjectId serverId) => serverId.Equals(currentServerId)) && !text.Equals(value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0005D74C File Offset: 0x0005B94C
		private static bool FindServerToSwichover(DataRow inputRow, DataRow row, DataObjectStore store)
		{
			if (store.GetDataObject("MailboxDatabaseWholeObject") == null)
			{
				return false;
			}
			HashSet<Guid> hashSet = null;
			if (DBNull.Value.Equals(inputRow["ServerSetForSwitchoverPicker"]))
			{
				hashSet = new HashSet<Guid>();
				new Dictionary<Guid, int>();
				IEnumerable<object> enumerable = store.GetDataObject("MailboxDatabaseWholeObject") as IEnumerable<object>;
				if (enumerable != null)
				{
					foreach (object obj in enumerable)
					{
						MailboxDatabase mailboxDatabase = obj as MailboxDatabase;
						foreach (ADObjectId adobjectId in mailboxDatabase.Servers)
						{
							if (!hashSet.Contains(adobjectId.ObjectGuid))
							{
								hashSet.Add(adobjectId.ObjectGuid);
							}
						}
					}
				}
				inputRow["ServerSetForSwitchoverPicker"] = hashSet;
			}
			else
			{
				hashSet = (inputRow["ServerSetForSwitchoverPicker"] as HashSet<Guid>);
			}
			if (hashSet != null && hashSet.Count > 0)
			{
				ADObjectId adobjectId2 = row["Identity"] as ADObjectId;
				string value = (string)inputRow["CurrentServer"];
				return adobjectId2 != null && hashSet.Contains(adobjectId2.ObjectGuid) && !adobjectId2.ObjectGuid.ToString().Equals(value, StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x0005D8AC File Offset: 0x0005BAAC
		private static bool IsAdminDisplayVersionE14OrAfter(DataRow inputRow, DataRow row, DataObjectStore store)
		{
			ServerVersion serverVersion = (ServerVersion)row["AdminDisplayVersion"];
			return serverVersion.Major >= Server.Exchange2009MajorVersion;
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x0005D8DA File Offset: 0x0005BADA
		private static void FilterMailboxServerRows(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DatabasePropertiesHelper.FilterRowsByAdminDisplayVersion(inputRow, dataTable, store, Server.CurrentExchangeMajorVersion, null);
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0005D8EC File Offset: 0x0005BAEC
		internal static void FilterRowsByAdminDisplayVersion(DataRow inputRow, DataTable dataTable, DataObjectStore store, int majorVersion, Func<DataRow, DataRow, DataObjectStore, bool> predicate)
		{
			List<DataRow> list = new List<DataRow>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				ServerVersion serverVersion = (ServerVersion)dataRow["AdminDisplayVersion"];
				if ((majorVersion != 0 && serverVersion.Major != majorVersion) || (predicate != null && !predicate(inputRow, dataRow, store)))
				{
					list.Add(dataRow);
				}
			}
			foreach (DataRow row in list)
			{
				dataTable.Rows.Remove(row);
			}
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x0005D9C8 File Offset: 0x0005BBC8
		private static void SetScheduleProperty(DataRow row, string scheduleColumnName, List<string> modifiedColumns)
		{
			if (DBNull.Value != row[scheduleColumnName])
			{
				object[] array = (object[])row[scheduleColumnName];
				bool[] array2 = new bool[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = (bool)array[i];
				}
				ScheduleBuilder scheduleBuilder = new ScheduleBuilder();
				scheduleBuilder.SetEntireState(array2);
				row[scheduleColumnName] = scheduleBuilder.Schedule;
				modifiedColumns.Add(scheduleColumnName);
			}
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x0005DA34 File Offset: 0x0005BC34
		public static void GetMailboxCopyPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			MailboxDatabase mailboxDatabase = (MailboxDatabase)store.GetDataObject("MailboxDatabaseFoGet");
			DatabaseCopyStatusEntry databaseCopyStatusEntry = (DatabaseCopyStatusEntry)store.GetDataObject("DatabaseCopyStatusEntry");
			DatabaseCopy databaseCopy = null;
			foreach (DatabaseCopy databaseCopy2 in mailboxDatabase.DatabaseCopies)
			{
				if (string.Equals(databaseCopyStatusEntry.MailboxServer, databaseCopy2.HostServerName, StringComparison.InvariantCultureIgnoreCase))
				{
					databaseCopy = databaseCopy2;
					break;
				}
			}
			if (databaseCopy != null)
			{
				dataRow["ActivationPreference"] = databaseCopy.ActivationPreference;
				dataRow["DatabaseCopiesLength"] = mailboxDatabase.DatabaseCopies.Length;
				dataRow["ReplayLagTime"] = databaseCopy.ReplayLagTime.ToString(TimeUnit.Day, 9);
			}
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x0005DB34 File Offset: 0x0005BD34
		public static void GetForSDODBStatusPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			List<object> source = (List<object>)dataRow["DatabaseCopies"];
			MailboxDatabase mailboxDatabase = (MailboxDatabase)store.GetDataObject("MailboxDatabase");
			List<object> list = new List<object>();
			KeyValuePair<ADObjectId, int>[] activationPreference = mailboxDatabase.ActivationPreference;
			for (int i = 0; i < activationPreference.Length; i++)
			{
				KeyValuePair<ADObjectId, int> pref = activationPreference[i];
				list.Add(source.First(delegate(object x)
				{
					string mailboxServer = ((DatabaseCopyStatusEntry)x).MailboxServer;
					KeyValuePair<ADObjectId, int> pref = pref;
					return mailboxServer.Equals(pref.Key.Name, StringComparison.OrdinalIgnoreCase);
				}));
			}
			dataRow["DatabaseCopies"] = list;
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x0005DBE0 File Offset: 0x0005BDE0
		public static void PrepareDBCopyForSDO(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DatabaseCopyStatusEntry statusEntry = (DatabaseCopyStatusEntry)store.GetDataObject("DatabaseCopyStatusEntry");
			DatabaseCopyStatus databaseCopyStatus = new DatabaseCopyStatus(statusEntry);
			DataRow dataRow = dataTable.Rows[0];
			dataRow["CanActivate"] = databaseCopyStatus.CanActivate;
			dataRow["CanRemove"] = databaseCopyStatus.CanRemove;
			dataRow["CanResume"] = databaseCopyStatus.CanResume;
			dataRow["CanSuspend"] = databaseCopyStatus.CanSuspend;
			dataRow["CanUpdate"] = databaseCopyStatus.CanUpdate;
			dataRow["ContentIndexStateString"] = databaseCopyStatus.ContentIndexStateString;
			dataRow["IsActive"] = databaseCopyStatus.IsActive;
			dataRow["StatusString"] = databaseCopyStatus.StatusString;
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x0005DCB8 File Offset: 0x0005BEB8
		public static void GetFirstObjectPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			if (dataRow["Identity"] is IEnumerable<Identity>)
			{
				inputRow["FirstDatabase"] = ((Identity[])dataRow["Identity"])[0];
			}
			else
			{
				inputRow["FirstDatabase"] = dataRow["Identity"];
			}
			store.SetModifiedColumns(new List<string>
			{
				"FirstDatabase"
			});
		}

		// Token: 0x04001C47 RID: 7239
		private const string IssueWarningQuotaColumnName = "IssueWarningQuota";

		// Token: 0x04001C48 RID: 7240
		private const string ProhibitSendQuotaColumnName = "ProhibitSendQuota";

		// Token: 0x04001C49 RID: 7241
		private const string ProhibitSendReceiveQuotaColumnName = "ProhibitSendReceiveQuota";

		// Token: 0x04001C4A RID: 7242
		private const string DeletedItemRetentionColumnName = "DeletedItemRetention";

		// Token: 0x04001C4B RID: 7243
		private const string MailboxRetentionColumnName = "MailboxRetention";

		// Token: 0x04001C4C RID: 7244
		private const string ServersColumnName = "Servers";

		// Token: 0x04001C4D RID: 7245
		private const string MountedColumnName = "Mounted";

		// Token: 0x04001C4E RID: 7246
		private const string MountStatusColumnName = "MountStatus";

		// Token: 0x04001C4F RID: 7247
		private const string MaintenanceScheduleColumnName = "MaintenanceSchedule";

		// Token: 0x04001C50 RID: 7248
		private const string QuotaNotificationScheduleColumnName = "QuotaNotificationSchedule";

		// Token: 0x04001C51 RID: 7249
		private const string DatabaseCopiesColumnName = "DatabaseCopies";

		// Token: 0x04001C52 RID: 7250
		private const string AdminDisplayVersionColumnName = "AdminDisplayVersion";

		// Token: 0x04001C53 RID: 7251
		private const string MailboxDatabaseObjectName = "MailboxDatabase";

		// Token: 0x04001C54 RID: 7252
		private const string MailboxDatabaseWholeObject = "MailboxDatabaseWholeObject";

		// Token: 0x04001C55 RID: 7253
		private const string DatabaseAvailabilityGroupColumnName = "DatabaseAvailabilityGroup";

		// Token: 0x04001C56 RID: 7254
		private const string DatabaseAvailabilityGroupRawValueColumnName = "DatabaseAvailabilityGroupRawValue";

		// Token: 0x04001C57 RID: 7255
		private const string ServerSetColumnName = "ServerSetForSwitchoverPicker";

		// Token: 0x04001C58 RID: 7256
		private const string ReplayLagTime = "ReplayLagTime";
	}
}
