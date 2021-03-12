using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Permission;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000513 RID: 1299
	public static class MailboxPropertiesHelper
	{
		// Token: 0x06003E1D RID: 15901 RVA: 0x000BA7F8 File Offset: 0x000B89F8
		static MailboxPropertiesHelper()
		{
			if (EacEnvironment.Instance.IsDataCenter)
			{
				MailboxPropertiesHelper.GetListPropertySet = "PrimarySmtpAddress,DisplayName,RecipientTypeDetails,ArchiveGuid,AuthenticationType,Identity";
				MailboxPropertiesHelper.GetListWorkflowOutput = (DDIHelper.IsFFO() ? "DisplayName,PrimarySmtpAddress,Identity,MailboxType,RecipientTypeDetails,AuthenticationType,ArchiveGuid,IsUserManaged,IsKeepWindowsLiveIdAllowed,IsUserFederated,IsLinkedMailbox,IsRemoteMailbox,IsSharedMailbox,LocRecipientTypeDetails" : "DisplayName,PrimarySmtpAddress,Identity,MailboxType,RecipientTypeDetails,AuthenticationType,ArchiveGuid,IsUserManaged,IsKeepWindowsLiveIdAllowed,IsUserFederated,IsLinkedMailbox,IsRemoteMailbox,IsSharedMailbox");
				return;
			}
			MailboxPropertiesHelper.GetListPropertySet = "PrimarySmtpAddress,DisplayName,RecipientTypeDetails,ArchiveGuid,Identity";
			MailboxPropertiesHelper.GetListWorkflowOutput = "DisplayName,PrimarySmtpAddress,Identity,MailboxType,RecipientTypeDetails,IsLinkedMailbox,IsRemoteMailbox,IsSharedMailbox";
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x000BA888 File Offset: 0x000B8A88
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

		// Token: 0x06003E1F RID: 15903 RVA: 0x000BA8D0 File Offset: 0x000B8AD0
		public static MultiValuedPropertyBase ArrayToMvp(object val)
		{
			Array array = val as Array;
			if (array == null)
			{
				throw new ArgumentException("The value should be an array.", "val");
			}
			MultiValuedProperty<object> multiValuedProperty = new MultiValuedProperty<object>();
			foreach (object item in array)
			{
				multiValuedProperty.Add(item);
			}
			return multiValuedProperty;
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x000BA944 File Offset: 0x000B8B44
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
				dataRow["IssueWarningQuota"] = MailboxPropertiesHelper.UnlimitedByteQuantifiedSizeToString(mailboxUsage.IssueWarningQuota);
				dataRow["ProhibitSendQuota"] = MailboxPropertiesHelper.UnlimitedByteQuantifiedSizeToString(mailboxUsage.ProhibitSendQuota);
				dataRow["ProhibitSendReceiveQuota"] = MailboxPropertiesHelper.UnlimitedByteQuantifiedSizeToString(mailboxUsage.ProhibitSendReceiveQuota);
				dataRow["RetainDeletedItemsFor"] = mailboxUsage.RetainDeletedItemsFor.Days.ToString();
				dataRow["RetainDeletedItemsUntilBackup"] = mailboxUsage.RetainDeletedItemsUntilBackup;
			}
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x000BAAD4 File Offset: 0x000B8CD4
		public static void MailboxFeaturePostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			Mailbox mailbox = store.GetDataObject("Mailbox") as Mailbox;
			if (mailbox != null)
			{
				MailboxStatistics mailboxStatistics = store.GetDataObject("MailboxStatistics") as MailboxStatistics;
				MailboxDatabase mbxDatabase = store.GetDataObject("MailboxDatabase") as MailboxDatabase;
				MailboxStatistics archiveStatistics = store.GetDataObject("ArchiveStatistics") as MailboxStatistics;
				new MailboxPropertiesHelper.MailboxUsage(mailbox, mbxDatabase, mailboxStatistics, archiveStatistics);
				MailboxPropertiesHelper.GetMailboxPostAction(inputRow, dataTable, store);
				MailboxPropertiesHelper.GetArchiveStatisticsPostAction(inputRow, dataTable, store);
			}
			dataRow["RawIdentity"] = ((ADObjectId)dataRow["Identity"]).ToString();
			MailboxPropertiesHelper.GetMaxSendReceiveSize(inputRow, dataTable, store);
			MailboxPropertiesHelper.GetAcceptRejectSendersOrMembers(inputRow, dataTable, store);
			Unlimited<EnhancedTimeSpan> unlimited = (Unlimited<EnhancedTimeSpan>)dataRow["LitigationHoldDuration"];
			if (unlimited.IsUnlimited)
			{
				dataRow["LitigationHoldDuration"] = "unlimited";
			}
			else
			{
				dataRow["LitigationHoldDuration"] = unlimited.Value.Days.ToString();
			}
			Unlimited<int> unlimited2 = (Unlimited<int>)dataRow["RecipientLimits"];
			if (unlimited2.IsUnlimited)
			{
				dataRow["RecipientLimits"] = "unlimited";
				return;
			}
			dataRow["RecipientLimits"] = unlimited2.Value.ToString();
		}

		// Token: 0x06003E22 RID: 15906 RVA: 0x000BAC1C File Offset: 0x000B8E1C
		public static void SetObjectPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			List<string> list = new List<string>();
			Unlimited<EnhancedTimeSpan> unlimited = Unlimited<EnhancedTimeSpan>.UnlimitedValue;
			if (!DBNull.Value.Equals(dataRow["LitigationHoldDuration"]) && (string)dataRow["LitigationHoldDuration"] != "unlimited" && ((string)dataRow["LitigationHoldDuration"]).Trim() != string.Empty)
			{
				list.Add("LitigationHoldDuration");
				unlimited = Unlimited<EnhancedTimeSpan>.Parse((string)dataRow["LitigationHoldDuration"]);
				store.SetModifiedColumns(list);
			}
			inputRow["LitigationHoldDuration"] = unlimited;
			dataRow["LitigationHoldDuration"] = unlimited;
			Unlimited<int> unlimited2 = Unlimited<int>.UnlimitedValue;
			if (!DBNull.Value.Equals(dataRow["RecipientLimits"]) && (string)dataRow["RecipientLimits"] != "unlimited")
			{
				list.Add("RecipientLimits");
				unlimited2 = Unlimited<int>.Parse((string)dataRow["RecipientLimits"]);
				store.SetModifiedColumns(list);
			}
			inputRow["RecipientLimits"] = unlimited2;
			dataRow["RecipientLimits"] = unlimited2;
			MailboxPropertiesHelper.SetMaxSendReceiveSize(inputRow, dataTable, store);
			MailboxPropertiesHelper.SetRetentionPolicy(inputRow, dataTable, store);
			List<string> list2 = new List<string>();
			MailboxPropertiesHelper.SaveQuotaProperty(dataRow, "UseDatabaseQuotaDefaults", "IssueWarningQuota", list2);
			MailboxPropertiesHelper.SaveQuotaProperty(dataRow, "UseDatabaseQuotaDefaults", "ProhibitSendQuota", list2);
			MailboxPropertiesHelper.SaveQuotaProperty(dataRow, "UseDatabaseQuotaDefaults", "ProhibitSendReceiveQuota", list2);
			if (DBNull.Value != dataRow["RetainDeletedItemsFor"])
			{
				dataRow["RetainDeletedItemsFor"] = EnhancedTimeSpan.Parse((string)dataRow["RetainDeletedItemsFor"]);
				list2.Add("RetainDeletedItemsFor");
			}
			if (list2.Count != 0)
			{
				store.SetModifiedColumns(list2);
			}
		}

		// Token: 0x06003E23 RID: 15907 RVA: 0x000BAE04 File Offset: 0x000B9004
		public static void GetSuggestionPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			MailboxPropertiesHelper.FilterNoSmtpEmailAddresses(inputRow, dataTable, store);
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x000BAE10 File Offset: 0x000B9010
		public static void GetDisconnectedMailboxListPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			dataTable.BeginLoadData();
			List<DataRow> list = new List<DataRow>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (!DBNull.Value.Equals(dataRow["DisconnectReason"]) && (dataRow["DisconnectReason"] == null || (MailboxState)dataRow["DisconnectReason"] != MailboxState.Disabled))
				{
					list.Add(dataRow);
				}
			}
			foreach (DataRow row in list)
			{
				dataTable.Rows.Remove(row);
			}
			dataTable.EndLoadData();
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x000BAEF8 File Offset: 0x000B90F8
		public static void GetMailboxPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow row = dataTable.Rows[0];
			Mailbox mailbox = store.GetDataObject("Mailbox") as Mailbox;
			if (mailbox != null)
			{
				MailboxPropertiesHelper.TrySetColumnValue(row, "MailboxCanHaveArchive", mailbox.ExchangeVersion.CompareTo(ExchangeObjectVersion.Exchange2010) >= 0 && ((mailbox.RecipientType == RecipientType.UserMailbox && mailbox.RecipientTypeDetails != RecipientTypeDetails.LegacyMailbox) || mailbox.RecipientTypeDetails == (RecipientTypeDetails)((ulong)int.MinValue) || mailbox.RecipientTypeDetails == RecipientTypeDetails.RemoteRoomMailbox || mailbox.RecipientTypeDetails == RecipientTypeDetails.RemoteEquipmentMailbox || mailbox.RecipientTypeDetails == RecipientTypeDetails.RemoteSharedMailbox));
				MailboxPropertiesHelper.TrySetColumnValue(row, "EnableArchive", mailbox.ArchiveState != ArchiveState.None);
				MailboxPropertiesHelper.TrySetColumnValue(row, "HasArchive", mailbox.ArchiveState != ArchiveState.None);
				MailboxPropertiesHelper.TrySetColumnValue(row, "RemoteArchive", mailbox.ArchiveState == ArchiveState.HostedProvisioned || mailbox.ArchiveState == ArchiveState.HostedPending);
			}
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x000BB006 File Offset: 0x000B9206
		internal static void TrySetColumnValue(DataRow row, string column, object value)
		{
			if (row.Table.Columns.Contains(column))
			{
				row[column] = value;
			}
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x000BB024 File Offset: 0x000B9224
		public static void GetArchiveStatisticsPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			Mailbox mailbox = store.GetDataObject("Mailbox") as Mailbox;
			MailboxDatabase mbxDatabase = store.GetDataObject("MailboxDatabase") as MailboxDatabase;
			MailboxStatistics archiveStatistics = store.GetDataObject("ArchiveStatistics") as MailboxStatistics;
			if (mailbox != null)
			{
				MailboxPropertiesHelper.MailboxUsage mailboxUsage = new MailboxPropertiesHelper.MailboxUsage(mailbox, mbxDatabase, null, archiveStatistics);
				dataRow["ArchiveUsage"] = new StatisticsBarData(mailboxUsage.ArchiveUsagePercentage, mailboxUsage.ArchiveUsageState, mailboxUsage.ArchiveUsageText);
				dataRow["MailboxQuota"] = MailboxPropertiesHelper.UnlimitedByteQuantifiedSizeToString(mailboxUsage.ProhibitSendQuota);
			}
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x000BB0C0 File Offset: 0x000B92C0
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

		// Token: 0x06003E29 RID: 15913 RVA: 0x000BB128 File Offset: 0x000B9328
		public static void GetListPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			bool flag = RbacCheckerWrapper.RbacChecker.IsInRole("Remove-Mailbox?KeepWindowsLiveId@W:Organization");
			bool flag2 = !Util.IsDataCenter;
			dataTable.BeginLoadData();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)dataRow["RecipientTypeDetails"];
				bool flag3 = recipientTypeDetails == RecipientTypeDetails.UserMailbox;
				MailboxPropertiesHelper.FillTypeColumns(dataRow, recipientTypeDetails);
				if (flag2)
				{
					bool flag4 = false;
					bool flag5 = false;
					if (!DBNull.Value.Equals(dataRow["AuthenticationType"]))
					{
						flag4 = ((AuthenticationType)dataRow["AuthenticationType"] == AuthenticationType.Managed);
						flag5 = ((AuthenticationType)dataRow["AuthenticationType"] == AuthenticationType.Federated);
					}
					dataRow["IsUserManaged"] = (flag3 && flag4);
					dataRow["IsUserFederated"] = (flag3 && flag5);
					dataRow["IsKeepWindowsLiveIdAllowed"] = flag;
					dataRow["MailboxType"] = MailboxPropertiesHelper.TranslateMailboxTypeForListview(recipientTypeDetails, flag3 && flag5, (Guid)dataRow["ArchiveGuid"]);
				}
				else
				{
					dataRow["MailboxType"] = MailboxPropertiesHelper.TranslateMailboxTypeForListview(recipientTypeDetails, false, (Guid)dataRow["ArchiveGuid"]);
				}
			}
			dataTable.EndLoadData();
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x000BB2B8 File Offset: 0x000B94B8
		public static void GetRecipientMailboxType(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)dataRow["RecipientTypeDetails"];
			bool flag = recipientTypeDetails == RecipientTypeDetails.UserMailbox;
			bool flag2 = false;
			if (!DBNull.Value.Equals(dataRow["AuthenticationType"]))
			{
				flag2 = ((AuthenticationType)dataRow["AuthenticationType"] == AuthenticationType.Federated);
			}
			dataRow["MailboxType"] = MailboxPropertiesHelper.TranslateMailboxTypeForListview(recipientTypeDetails, flag && flag2, (Guid)dataRow["ArchiveGuid"]);
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x000BB340 File Offset: 0x000B9540
		public static void GetRecipientPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)dataRow["RecipientTypeDetails"];
			MailboxPropertiesHelper.FillTypeColumns(dataRow, recipientTypeDetails);
			MailboxPropertiesHelper.GetRecipientMailboxType(inputRow, dataTable, store);
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x000BB37C File Offset: 0x000B957C
		private static void FillTypeColumns(DataRow row, RecipientTypeDetails recipientTypeDetails)
		{
			row["IsRemoteMailbox"] = (recipientTypeDetails == RecipientTypeDetails.RemoteEquipmentMailbox || recipientTypeDetails == RecipientTypeDetails.RemoteRoomMailbox || recipientTypeDetails == (RecipientTypeDetails)((ulong)int.MinValue) || recipientTypeDetails == RecipientTypeDetails.RemoteSharedMailbox);
			row["IsSharedMailbox"] = (recipientTypeDetails == RecipientTypeDetails.SharedMailbox);
			row["IsLinkedMailbox"] = (recipientTypeDetails == RecipientTypeDetails.LinkedMailbox);
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x000BB3F4 File Offset: 0x000B95F4
		public static string TranslateMailboxTypeForListview(RecipientTypeDetails recipientTypeDetails, bool isFederatedUserMailbox, Guid archiveGuid)
		{
			string text = string.Empty;
			if (recipientTypeDetails <= RecipientTypeDetails.EquipmentMailbox)
			{
				if (recipientTypeDetails <= RecipientTypeDetails.LegacyMailbox)
				{
					if (recipientTypeDetails <= RecipientTypeDetails.SharedMailbox)
					{
						if (recipientTypeDetails < RecipientTypeDetails.UserMailbox)
						{
							goto IL_155;
						}
						switch ((int)(recipientTypeDetails - RecipientTypeDetails.UserMailbox))
						{
						case 0:
							text = (isFederatedUserMailbox ? Strings.FederatedUserMailboxText : Strings.UserMailboxText);
							goto IL_15B;
						case 1:
							text = Strings.LinkedMailboxText;
							goto IL_15B;
						case 2:
							goto IL_155;
						case 3:
							text = Strings.SharedMailboxText;
							goto IL_15B;
						}
					}
					if (recipientTypeDetails == RecipientTypeDetails.LegacyMailbox)
					{
						text = Strings.LegacyMailboxText;
						goto IL_15B;
					}
				}
				else
				{
					if (recipientTypeDetails == RecipientTypeDetails.RoomMailbox)
					{
						text = Strings.RoomMailboxText;
						goto IL_15B;
					}
					if (recipientTypeDetails == RecipientTypeDetails.EquipmentMailbox)
					{
						text = Strings.EquipmentMailboxText;
						goto IL_15B;
					}
				}
			}
			else if (recipientTypeDetails <= RecipientTypeDetails.RemoteRoomMailbox)
			{
				if (recipientTypeDetails == (RecipientTypeDetails)((ulong)-2147483648))
				{
					text = Strings.RemoteUserMailboxText;
					goto IL_15B;
				}
				if (recipientTypeDetails == RecipientTypeDetails.RemoteRoomMailbox)
				{
					text = Strings.RemoteRoomMailboxText;
					goto IL_15B;
				}
			}
			else
			{
				if (recipientTypeDetails == RecipientTypeDetails.RemoteEquipmentMailbox)
				{
					text = Strings.RemoteEquipmentMailboxText;
					goto IL_15B;
				}
				if (recipientTypeDetails == RecipientTypeDetails.RemoteSharedMailbox)
				{
					text = Strings.RemoteSharedMailboxText;
					goto IL_15B;
				}
				if (recipientTypeDetails == RecipientTypeDetails.TeamMailbox)
				{
					text = Strings.TeamMailboxText;
					goto IL_15B;
				}
			}
			IL_155:
			text = string.Empty;
			IL_15B:
			return archiveGuid.Equals(Guid.Empty) ? text : string.Format(Strings.ArchiveText, text);
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x000BB57F File Offset: 0x000B977F
		public static void FilterEntSendAsPermission(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			dataTable.Rows[0]["SendAsPermissions"] = MailboxPropertiesHelper.FindRecipientsWithSendAsPermissionEnt(store.GetDataObject("ADPermissions") as IEnumerable<object>, store);
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x000BB5B0 File Offset: 0x000B97B0
		internal static IEnumerable<AcePermissionRecipientRow> FindRecipientsWithSendAsPermissionEnt(IEnumerable<object> permissions, DataObjectStore store)
		{
			List<SecurityPrincipalIdParameter> permissionsHelper = MailboxPropertiesHelper.GetPermissionsHelper(permissions, new IsExpectedPermission(MailboxPropertiesHelper.IsSendAsPermission), store);
			return RecipientObjectResolver.Instance.ResolveSecurityPrincipalId(permissionsHelper);
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x000BB5DC File Offset: 0x000B97DC
		public static void FilterCloudSendAsPermission(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			dataTable.Rows[0]["SendAsPermissions"] = MailboxPropertiesHelper.FindRecipientsWithSendAsPermissionCloud(store.GetDataObject("RecipientPermission") as IEnumerable<object>);
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x000BB60C File Offset: 0x000B980C
		internal static List<object> FindRecipientsWithSendAsPermissionCloud(IEnumerable<object> permissions)
		{
			List<object> list = new List<object>();
			foreach (object obj in permissions)
			{
				RecipientPermission recipientPermission = obj as RecipientPermission;
				if (recipientPermission.AccessRights.Contains(RecipientAccessRight.SendAs))
				{
					list.Add(new TrusteeRow(recipientPermission.Trustee));
				}
			}
			return list;
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x000BB67C File Offset: 0x000B987C
		public static void FilterFullAccessPermission(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			IEnumerable<object> permissions = store.GetDataObject("MailboxPermissions") as IEnumerable<object>;
			dataTable.Rows[0]["FullAccessPermissions"] = MailboxPropertiesHelper.GetPermissionsHelper(permissions, new IsExpectedPermission(MailboxPropertiesHelper.IsFullAccessPermission), store);
		}

		// Token: 0x06003E33 RID: 15923 RVA: 0x000BB6E4 File Offset: 0x000B98E4
		public static void SetADPermissionPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			object[] array = inputRow["SendAsPermissionsAdded"] as object[];
			Identity[] array2;
			if (array == null)
			{
				array2 = new Identity[0];
			}
			else
			{
				array2 = Array.ConvertAll<object, Identity>(array, (object x) => x as Identity);
			}
			Identity[] array3 = array2;
			object[] array4 = inputRow["SendAsPermissionsRemoved"] as object[];
			Identity[] array5;
			if (array4 == null)
			{
				array5 = new Identity[0];
			}
			else
			{
				array5 = Array.ConvertAll<object, Identity>(array4, (object x) => x as Identity);
			}
			Identity[] array6 = array5;
			inputRow["SendAsPermissionsAdded"] = RecipientObjectResolver.Instance.ConvertGuidsToSid(Array.ConvertAll<Identity, string>(array3, (Identity x) => x.RawIdentity));
			inputRow["SendAsPermissionsRemoved"] = RecipientObjectResolver.Instance.ConvertGuidsToSid(Array.ConvertAll<Identity, string>(array6, (Identity x) => x.RawIdentity));
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x000BB7F4 File Offset: 0x000B99F4
		public static void SetMailboxPermissionPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			IEnumerable<object> allPermissions = store.GetDataObject("MailboxPermissions") as IEnumerable<object>;
			object[] array = inputRow["FullAccessPermissionsAdded"] as object[];
			Identity[] array2;
			if (array == null)
			{
				array2 = new Identity[0];
			}
			else
			{
				array2 = Array.ConvertAll<object, Identity>(array, (object x) => x as Identity);
			}
			Identity[] identitiesToAdd = array2;
			object[] array3 = inputRow["FullAccessPermissionsRemoved"] as object[];
			Identity[] array4;
			if (array3 == null)
			{
				array4 = new Identity[0];
			}
			else
			{
				array4 = Array.ConvertAll<object, Identity>(array3, (object x) => x as Identity);
			}
			Identity[] identitiesToRemove = array4;
			List<SecurityIdentifier> value;
			List<SecurityIdentifier> value2;
			Dictionary<SecurityIdentifier, bool> value3;
			Dictionary<SecurityIdentifier, bool> value4;
			Dictionary<SecurityIdentifier, bool> value5;
			MailboxPropertiesHelper.SetPermissionsHelper(allPermissions, identitiesToAdd, identitiesToRemove, out value, out value2, out value3, out value4, out value5, new IsExpectedPermission(MailboxPropertiesHelper.IsFullAccessPermission));
			inputRow["FullAccessPermissionsAdded"] = value;
			inputRow["FullAccessPermissionsRemoved"] = value2;
			inputRow["hasExplicitDenyForAdded"] = value3;
			inputRow["hasExplicitAllowForRemoved"] = value4;
			inputRow["hasInheritedAllowForRemoved"] = value5;
		}

		// Token: 0x06003E35 RID: 15925 RVA: 0x000BB8F4 File Offset: 0x000B9AF4
		internal static List<SecurityPrincipalIdParameter> GetPermissionsHelper(IEnumerable<object> permissions, IsExpectedPermission isExpectedDelegate, DataObjectStore store)
		{
			List<SecurityIdentifier> list = new List<SecurityIdentifier>();
			List<SecurityIdentifier> list2 = new List<SecurityIdentifier>();
			List<SecurityPrincipalIdParameter> list3 = new List<SecurityPrincipalIdParameter>();
			string text = null;
			if (store != null)
			{
				Mailbox mailbox = store.GetDataObject("Mailbox") as Mailbox;
				if (mailbox.IsLinked)
				{
					text = mailbox.LinkedMasterAccount;
				}
			}
			if (permissions != null)
			{
				foreach (object obj in permissions)
				{
					AcePresentationObject acePresentationObject = obj as AcePresentationObject;
					if (acePresentationObject != null)
					{
						SecurityIdentifier securityIdentifier = acePresentationObject.User.SecurityIdentifier;
						if (!list2.Contains(securityIdentifier) && !list.Contains(securityIdentifier) && isExpectedDelegate(acePresentationObject))
						{
							if (acePresentationObject.Deny)
							{
								list2.Add(securityIdentifier);
							}
							else
							{
								list.Add(securityIdentifier);
								if (text == null || text != acePresentationObject.User.ToString())
								{
									list3.Add(acePresentationObject.User);
								}
							}
						}
					}
				}
			}
			return list3;
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x000BBA00 File Offset: 0x000B9C00
		internal static bool IsFullAccessPermission(AcePresentationObject aceObject)
		{
			MailboxAcePresentationObject mailboxAcePresentationObject = aceObject as MailboxAcePresentationObject;
			foreach (MailboxRights mailboxRights in mailboxAcePresentationObject.AccessRights)
			{
				if ((mailboxRights & MailboxRights.FullAccess) == MailboxRights.FullAccess)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003E37 RID: 15927 RVA: 0x000BBA40 File Offset: 0x000B9C40
		internal static bool IsSendAsPermission(AcePresentationObject aceObject)
		{
			bool result = false;
			ADAcePresentationObject adacePresentationObject = aceObject as ADAcePresentationObject;
			if (adacePresentationObject.ExtendedRights != null)
			{
				foreach (ExtendedRightIdParameter extendedRightIdParameter in adacePresentationObject.ExtendedRights)
				{
					if (string.Compare(extendedRightIdParameter.ToString(), "send-as", true, CultureInfo.InvariantCulture) == 0)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x000BBAAC File Offset: 0x000B9CAC
		internal static void SetPermissionsHelper(IEnumerable<object> allPermissions, Identity[] identitiesToAdd, Identity[] identitiesToRemove, out List<SecurityIdentifier> sidsToAdd, out List<SecurityIdentifier> sidsToRemove, out Dictionary<SecurityIdentifier, bool> hasExplicitDenyForAdded, out Dictionary<SecurityIdentifier, bool> hasExplicitAllowForRemoved, out Dictionary<SecurityIdentifier, bool> hasInheritedAllowForRemoved, IsExpectedPermission isExpectedDelegate)
		{
			new List<string>();
			hasExplicitAllowForRemoved = new Dictionary<SecurityIdentifier, bool>();
			hasInheritedAllowForRemoved = new Dictionary<SecurityIdentifier, bool>();
			hasExplicitDenyForAdded = new Dictionary<SecurityIdentifier, bool>();
			sidsToAdd = RecipientObjectResolver.Instance.ConvertGuidsToSid(Array.ConvertAll<Identity, string>(identitiesToAdd, (Identity x) => x.RawIdentity));
			foreach (SecurityIdentifier key in sidsToAdd)
			{
				hasExplicitDenyForAdded[key] = false;
			}
			sidsToRemove = RecipientObjectResolver.Instance.ConvertGuidsToSid(Array.ConvertAll<Identity, string>(identitiesToRemove, (Identity x) => x.RawIdentity));
			foreach (SecurityIdentifier key2 in sidsToRemove)
			{
				hasExplicitAllowForRemoved[key2] = false;
				hasInheritedAllowForRemoved[key2] = false;
			}
			foreach (object obj in allPermissions)
			{
				AcePresentationObject acePresentationObject = (AcePresentationObject)obj;
				SecurityIdentifier securityIdentifier = acePresentationObject.User.SecurityIdentifier;
				bool flag = false;
				if (hasExplicitDenyForAdded.TryGetValue(securityIdentifier, out flag))
				{
					if (!flag && acePresentationObject.Deny.ToBool() && !acePresentationObject.IsInherited && isExpectedDelegate(acePresentationObject))
					{
						hasExplicitDenyForAdded[securityIdentifier] = true;
					}
				}
				else if (sidsToRemove.Contains(securityIdentifier) && isExpectedDelegate(acePresentationObject) && !acePresentationObject.Deny)
				{
					if (acePresentationObject.IsInherited)
					{
						hasInheritedAllowForRemoved[securityIdentifier] = true;
					}
					else
					{
						hasExplicitAllowForRemoved[securityIdentifier] = true;
					}
				}
			}
		}

		// Token: 0x06003E39 RID: 15929 RVA: 0x000BBC98 File Offset: 0x000B9E98
		public static bool HasExplicitPermission(object sid, object dictionary)
		{
			SecurityIdentifier key = sid as SecurityIdentifier;
			Dictionary<SecurityIdentifier, bool> dictionary2 = dictionary as Dictionary<SecurityIdentifier, bool>;
			bool flag = false;
			return dictionary2.TryGetValue(key, out flag) && flag;
		}

		// Token: 0x06003E3A RID: 15930 RVA: 0x000BBCC3 File Offset: 0x000B9EC3
		public static void AddToAllowList(DataRow row, DataTable dataTable, DataObjectStore store)
		{
			MobileMailboxService.AddToAllowList(row, dataTable, store);
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x000BBCCD File Offset: 0x000B9ECD
		public static void AddToBlockList(DataRow row, DataTable dataTable, DataObjectStore store)
		{
			MobileMailboxService.AddToBlockList(row, dataTable, store);
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x000BBCD8 File Offset: 0x000B9ED8
		internal static void GetAcceptRejectSendersOrMembers(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			if (dataRow["AcceptMessagesOnlyFromSendersOrMembers"] != DBNull.Value)
			{
				ADMultiValuedProperty<ADObjectId> admultiValuedProperty = (ADMultiValuedProperty<ADObjectId>)dataRow["AcceptMessagesOnlyFromSendersOrMembers"];
				if (admultiValuedProperty != null && admultiValuedProperty.Count == 0)
				{
					dataRow["AcceptMessagesOnlyFromSendersOrMembers"] = null;
				}
			}
			if (dataRow["RejectMessagesFromSendersOrMembers"] != DBNull.Value)
			{
				ADMultiValuedProperty<ADObjectId> admultiValuedProperty2 = (ADMultiValuedProperty<ADObjectId>)dataRow["RejectMessagesFromSendersOrMembers"];
				if (admultiValuedProperty2 != null && admultiValuedProperty2.Count == 0)
				{
					dataRow["RejectMessagesFromSendersOrMembers"] = null;
				}
			}
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x000BBD68 File Offset: 0x000B9F68
		internal static void GetMaxSendReceiveSize(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			if (dataRow["MaxSendSize"] != DBNull.Value)
			{
				Unlimited<ByteQuantifiedSize> unlimited = (Unlimited<ByteQuantifiedSize>)dataRow["MaxSendSize"];
				if (unlimited.IsUnlimited)
				{
					dataRow["MaxSendSize"] = null;
				}
				else
				{
					dataRow["MaxSendSize"] = unlimited.Value.ToKB().ToString();
				}
			}
			if (dataRow["MaxReceiveSize"] != DBNull.Value)
			{
				Unlimited<ByteQuantifiedSize> unlimited2 = (Unlimited<ByteQuantifiedSize>)dataRow["MaxReceiveSize"];
				if (unlimited2.IsUnlimited)
				{
					dataRow["MaxReceiveSize"] = null;
					return;
				}
				dataRow["MaxReceiveSize"] = unlimited2.Value.ToKB().ToString();
			}
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x000BBE3C File Offset: 0x000BA03C
		internal static void SetMaxSendReceiveSize(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			List<string> list = new List<string>();
			Unlimited<ByteQuantifiedSize> unlimited = Unlimited<ByteQuantifiedSize>.UnlimitedValue;
			Unlimited<ByteQuantifiedSize> unlimited2 = Unlimited<ByteQuantifiedSize>.UnlimitedValue;
			if (!DBNull.Value.Equals(dataRow["MaxSendSize"]))
			{
				list.Add("MaxSendSize");
				unlimited = Unlimited<ByteQuantifiedSize>.Parse((string)dataRow["MaxSendSize"], ByteQuantifiedSize.Quantifier.KB);
				store.SetModifiedColumns(list);
			}
			if (!DBNull.Value.Equals(dataRow["MaxReceiveSize"]))
			{
				list.Add("MaxReceiveSize");
				unlimited2 = Unlimited<ByteQuantifiedSize>.Parse((string)dataRow["MaxReceiveSize"], ByteQuantifiedSize.Quantifier.KB);
				store.SetModifiedColumns(list);
			}
			inputRow["MaxSendSize"] = unlimited;
			dataRow["MaxSendSize"] = unlimited;
			inputRow["MaxReceiveSize"] = unlimited2;
			dataRow["MaxReceiveSize"] = unlimited2;
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x000BBF38 File Offset: 0x000BA138
		public static void GetCountryOrRegion(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			User user = store.GetDataObject("User") as User;
			if (user != null && null != user.CountryOrRegion)
			{
				dataRow["CountryOrRegion"] = user.CountryOrRegion.Name;
			}
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x000BBF8C File Offset: 0x000BA18C
		public static void GetEmailAddresses(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			if (dataRow["SimpleEmailAddresses"] != DBNull.Value)
			{
				dataRow["EmailAddresses"] = EmailAddressList.FromProxyAddressCollection((ProxyAddressCollection)dataRow["SimpleEmailAddresses"]);
			}
			MailboxPropertiesHelper.UpdateCanSetABP(dataRow, store);
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x000BBFE0 File Offset: 0x000BA1E0
		internal static void FilterNoSmtpEmailAddresses(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow["EmailAddresses"] != DBNull.Value)
				{
					ProxyAddressCollection proxyAddressCollection = new ProxyAddressCollection();
					foreach (ProxyAddress proxyAddress in ((ProxyAddressCollection)dataRow["EmailAddresses"]))
					{
						if (proxyAddress.PrefixString.ToLower() == "smtp")
						{
							try
							{
								proxyAddressCollection.Add(proxyAddress);
							}
							catch (DataValidationException)
							{
							}
						}
					}
					dataRow["EmailAddresses"] = proxyAddressCollection;
				}
			}
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x000BC0D4 File Offset: 0x000BA2D4
		internal static void SetRetentionPolicy(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			if (!DBNull.Value.Equals(dataRow["RetentionPolicy"]) && string.IsNullOrEmpty((string)dataRow["RetentionPolicy"]))
			{
				inputRow["RetentionPolicy"] = null;
				dataRow["RetentionPolicy"] = null;
			}
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x000BC134 File Offset: 0x000BA334
		internal static void UpdateCanSetABP(DataRow row, DataObjectStore store)
		{
			Mailbox mailbox = store.GetDataObject("Mailbox") as Mailbox;
			if (mailbox != null)
			{
				MailboxPropertiesHelper.TrySetColumnValue(row, "MailboxCanSetABP", mailbox.RecipientTypeDetails != RecipientTypeDetails.LegacyMailbox && mailbox.ExchangeVersion.CompareTo(ExchangeObjectVersion.Exchange2010) >= 0);
			}
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x000BC188 File Offset: 0x000BA388
		public static void GetServersWithVersionPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DatabasePropertiesHelper.FilterRowsByAdminDisplayVersion(inputRow, dataTable, store, Server.CurrentExchangeMajorVersion, null);
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x000BC198 File Offset: 0x000BA398
		public static void SetRecipientFilterPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			if (EacEnvironment.Instance.IsForefrontForOffice)
			{
				inputRow["RecipientTypeFilter"] = inputRow["FFORecipientTypeFilter"].ToString();
			}
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x000BC1C4 File Offset: 0x000BA3C4
		public static void ViewFFOUserDetailsPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			ADObjectId adobjectId = (ADObjectId)dataRow["Identity"];
			List<Identity> list = new List<Identity>();
			if (!dataRow["Group"].IsNullValue())
			{
				List<ADObjectId> list2 = (List<ADObjectId>)dataRow["Group"];
				foreach (ADObjectId identity in list2)
				{
					list.Add(new Identity(identity));
				}
				dataRow["GroupNames"] = list.ToArray();
				return;
			}
			dataRow["GroupNames"] = string.Empty;
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x000BC290 File Offset: 0x000BA490
		public static void FetchHoldNames(DataRow inputRow, DataTable dataTable, DataObjectStore store, PowerShellResults[] results)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)dataRow["InPlaceHolds"];
			MultiValuedProperty<string> multiValuedProperty2 = new MultiValuedProperty<string>();
			if (multiValuedProperty.Count > 0)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				if (store.GetDataObject("MailboxSearchWholeObject") != null)
				{
					IEnumerable<object> enumerable = store.GetDataObject("MailboxSearchWholeObject") as IEnumerable<object>;
					if (enumerable != null)
					{
						foreach (object obj in enumerable)
						{
							MailboxSearchObject mailboxSearchObject = (MailboxSearchObject)obj;
							if (!string.IsNullOrEmpty(mailboxSearchObject.InPlaceHoldIdentity) && !dictionary.ContainsKey(mailboxSearchObject.InPlaceHoldIdentity))
							{
								dictionary.Add(mailboxSearchObject.InPlaceHoldIdentity, mailboxSearchObject.Name);
							}
						}
					}
				}
				if (dictionary.Count > 0)
				{
					foreach (string key in multiValuedProperty)
					{
						if (dictionary.ContainsKey(key))
						{
							multiValuedProperty2.Add(dictionary[key]);
						}
					}
				}
			}
			dataRow["InPlaceHoldNames"] = multiValuedProperty2;
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x000BC3E0 File Offset: 0x000BA5E0
		public static string GetHoldDescription(object holdIds)
		{
			string result = Strings.MailboxUnderNoHold;
			MultiValuedProperty<string> multiValuedProperty = holdIds as MultiValuedProperty<string>;
			if (multiValuedProperty.Count == 1)
			{
				result = Strings.MailboxUnderOneHold;
			}
			else if (multiValuedProperty.Count > 1)
			{
				result = string.Format(Strings.MailboxUnderMultipleHolds, multiValuedProperty.Count);
			}
			return result;
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x000BC43C File Offset: 0x000BA63C
		public static string ResolveExtendedOrgnaizationUnit(object ou)
		{
			Identity identity = ou as Identity;
			string result;
			if (null != identity)
			{
				result = identity.DisplayName;
			}
			else
			{
				result = (ou as string);
			}
			return result;
		}

		// Token: 0x04002859 RID: 10329
		internal const string DatacenterPropertySet = "PrimarySmtpAddress,DisplayName,RecipientTypeDetails,ArchiveGuid,AuthenticationType,Identity";

		// Token: 0x0400285A RID: 10330
		internal const string DatacenterGetListOutput = "DisplayName,PrimarySmtpAddress,Identity,MailboxType,RecipientTypeDetails,AuthenticationType,ArchiveGuid,IsUserManaged,IsKeepWindowsLiveIdAllowed,IsUserFederated,IsLinkedMailbox,IsRemoteMailbox,IsSharedMailbox";

		// Token: 0x0400285B RID: 10331
		internal const string FFODatacenterGetListOutput = "DisplayName,PrimarySmtpAddress,Identity,MailboxType,RecipientTypeDetails,AuthenticationType,ArchiveGuid,IsUserManaged,IsKeepWindowsLiveIdAllowed,IsUserFederated,IsLinkedMailbox,IsRemoteMailbox,IsSharedMailbox,LocRecipientTypeDetails";

		// Token: 0x0400285C RID: 10332
		internal const string EnterprisePropertySet = "PrimarySmtpAddress,DisplayName,RecipientTypeDetails,ArchiveGuid,Identity";

		// Token: 0x0400285D RID: 10333
		internal const string EnterpriseGetListOutput = "DisplayName,PrimarySmtpAddress,Identity,MailboxType,RecipientTypeDetails,IsLinkedMailbox,IsRemoteMailbox,IsSharedMailbox";

		// Token: 0x0400285E RID: 10334
		private const string MailboxObjectName = "Mailbox";

		// Token: 0x0400285F RID: 10335
		private const string RecipientObjectName = "Recipient";

		// Token: 0x04002860 RID: 10336
		private const string UserObjectName = "User";

		// Token: 0x04002861 RID: 10337
		private const string MailboxDatabaseObjectName = "MailboxDatabase";

		// Token: 0x04002862 RID: 10338
		private const string MailboxStatisticsObjectName = "MailboxStatistics";

		// Token: 0x04002863 RID: 10339
		private const string MailboxUsageColumnName = "MailboxUsage";

		// Token: 0x04002864 RID: 10340
		private const string CountryOrRegionColumnName = "CountryOrRegion";

		// Token: 0x04002865 RID: 10341
		private const string WarningQuotaColumnName = "IssueWarningQuota";

		// Token: 0x04002866 RID: 10342
		private const string ProhibitSendQuotaColumnName = "ProhibitSendQuota";

		// Token: 0x04002867 RID: 10343
		private const string ProhibitSendReceiveQuotaColumnName = "ProhibitSendReceiveQuota";

		// Token: 0x04002868 RID: 10344
		private const string RetentionDaysColumnName = "RetainDeletedItemsFor";

		// Token: 0x04002869 RID: 10345
		private const string RetainUntilBackUpColumnName = "RetainDeletedItemsUntilBackup";

		// Token: 0x0400286A RID: 10346
		private const string UseDatabaseQuotaDefaultsColumnName = "UseDatabaseQuotaDefaults";

		// Token: 0x0400286B RID: 10347
		private const string UseDatabaseRetentionDefaultsColumnName = "UseDatabaseRetentionDefaults";

		// Token: 0x0400286C RID: 10348
		private const string MailboxUsageUnlimitedColumnName = "IsMailboxUsageUnlimited";

		// Token: 0x0400286D RID: 10349
		private const string MailboxSearchWholeObject = "MailboxSearchWholeObject";

		// Token: 0x0400286E RID: 10350
		private const string MailboxQuotaColumnName = "MailboxQuota";

		// Token: 0x0400286F RID: 10351
		private const string MailboxCanHaveArchiveColumnName = "MailboxCanHaveArchive";

		// Token: 0x04002870 RID: 10352
		private const string MailboxCanSetABPColumnName = "MailboxCanSetABP";

		// Token: 0x04002871 RID: 10353
		private const string EnableArchiveColumnName = "EnableArchive";

		// Token: 0x04002872 RID: 10354
		private const string HasArchiveColumnName = "HasArchive";

		// Token: 0x04002873 RID: 10355
		private const string RemoteArchiveColumnName = "RemoteArchive";

		// Token: 0x04002874 RID: 10356
		private const string ArchiveStatisticsObjectName = "ArchiveStatistics";

		// Token: 0x04002875 RID: 10357
		private const string ArchiveUsageColumnName = "ArchiveUsage";

		// Token: 0x04002876 RID: 10358
		private const string ArchiveNameColumnName = "ArchiveName";

		// Token: 0x04002877 RID: 10359
		private const string LitigationHoldDurationColumnName = "LitigationHoldDuration";

		// Token: 0x04002878 RID: 10360
		private static readonly string[] PasswordParameter = new string[]
		{
			"Password"
		};

		// Token: 0x04002879 RID: 10361
		public static readonly string GetListPropertySet = null;

		// Token: 0x0400287A RID: 10362
		public static readonly string GetListWorkflowOutput = null;

		// Token: 0x0400287B RID: 10363
		public static readonly string NewSetObjectWorkflowOutput = "IsLinkedMailbox,IsRemoteMailbox,IsSharedMailbox,DisplayName,MailboxType,PrimarySmtpAddress,Identity,RecipientTypeDetails,Name,FirstName,LastName,EmailAddressesTxt,ExchangeVersion,RecipientType,Alias,OrganizationalUnit,DatabaseTxt,ArchiveStateTxt,SharingPolicy,ActiveSyncMailboxPolicyTxt,WhenChanged,EmailAddressPolicyEnabled,ArchiveDatabaseTxt,City,Company,CountryOrRegion,Department,HiddenFromAddressListsEnabled,LitigationHoldEnabled,Office,OwaMailboxPolicyTxt,Phone,PostalCode,RetentionPolicy,StateOrProvince,Title,UMEnabled,UMMailboxPolicy,CustomAttribute1,CustomAttribute2,CustomAttribute3,CustomAttribute4,CustomAttribute5,CustomAttribute6,CustomAttribute7,CustomAttribute8,CustomAttribute9,CustomAttribute10,CustomAttribute11,CustomAttribute12,CustomAttribute13,CustomAttribute14,CustomAttribute15";

		// Token: 0x0400287C RID: 10364
		public static readonly string NewObjectWorkflowOutputForLinkedMailbox = "DisplayName,MailboxType,PrimarySmtpAddress,Identity,RecipientTypeDetails,Name,FirstName,LastName,EmailAddressesTxt,ExchangeVersion,RecipientType,Alias,OrganizationalUnit,DatabaseTxt,ArchiveStateTxt,SharingPolicy,ActiveSyncMailboxPolicyTxt,WhenChanged,EmailAddressPolicyEnabled,IsLinkedMailbox";

		// Token: 0x0400287D RID: 10365
		public static readonly string ConsoleSmallSet = "DisplayName,Alias,OrganizationalUnit,PrimarySmtpAddress,EmailAddresses,HiddenFromAddressListsEnabled,Name,WhenChanged,City,Company,CountryOrRegion,DatabaseName,Department,ExpansionServer,ExternalEmailAddress,FirstName,LastName,Office,StateOrProvince,Title,UMEnabled,HasActiveSyncDevicePartnership,Manager,SharingPolicy,ArchiveGuid,IsValidSecurityPrincipal,ArchiveState,MailboxMoveTargetMDB,MailboxMoveSourceMDB,MailboxMoveFlags,MailboxMoveRemoteHostName,MailboxMoveBatchName,MailboxMoveStatus,RecipientType,RecipientTypeDetails,Identity,ExchangeVersion,OrganizationId";

		// Token: 0x02000514 RID: 1300
		public class MailboxUsage
		{
			// Token: 0x06003E52 RID: 15954 RVA: 0x000BC470 File Offset: 0x000BA670
			public MailboxUsage(Mailbox mbx, MailboxDatabase mbxDatabase, MailboxStatistics mailboxStatistics, MailboxStatistics archiveStatistics)
			{
				this.mailbox = mbx;
				this.mailboxDatabase = mbxDatabase;
				this.mailboxStatistics = mailboxStatistics;
				this.archiveStatistics = archiveStatistics;
			}

			// Token: 0x1700245E RID: 9310
			// (get) Token: 0x06003E53 RID: 15955 RVA: 0x000BC498 File Offset: 0x000BA698
			private bool UseDatabaseQuotaDefaults
			{
				get
				{
					return this.mailbox.UseDatabaseQuotaDefaults != null && this.mailbox.UseDatabaseQuotaDefaults.Value && this.mailboxDatabase != null && !Util.IsDataCenter;
				}
			}

			// Token: 0x1700245F RID: 9311
			// (get) Token: 0x06003E54 RID: 15956 RVA: 0x000BC4E1 File Offset: 0x000BA6E1
			private bool UseDatabaseRetentionDefaults
			{
				get
				{
					return this.mailbox.UseDatabaseRetentionDefaults && this.mailboxDatabase != null && !Util.IsDataCenter;
				}
			}

			// Token: 0x17002460 RID: 9312
			// (get) Token: 0x06003E55 RID: 15957 RVA: 0x000BC502 File Offset: 0x000BA702
			public EnhancedTimeSpan RetainDeletedItemsFor
			{
				get
				{
					if (!this.UseDatabaseRetentionDefaults)
					{
						return this.mailbox.RetainDeletedItemsFor;
					}
					return this.mailboxDatabase.DeletedItemRetention;
				}
			}

			// Token: 0x17002461 RID: 9313
			// (get) Token: 0x06003E56 RID: 15958 RVA: 0x000BC523 File Offset: 0x000BA723
			public bool RetainDeletedItemsUntilBackup
			{
				get
				{
					if (!this.UseDatabaseRetentionDefaults)
					{
						return this.mailbox.RetainDeletedItemsUntilBackup;
					}
					return this.mailboxDatabase.RetainDeletedItemsUntilBackup;
				}
			}

			// Token: 0x17002462 RID: 9314
			// (get) Token: 0x06003E57 RID: 15959 RVA: 0x000BC544 File Offset: 0x000BA744
			public Unlimited<ByteQuantifiedSize> ProhibitSendQuota
			{
				get
				{
					if (!this.UseDatabaseQuotaDefaults)
					{
						return this.mailbox.ProhibitSendQuota;
					}
					return this.mailboxDatabase.ProhibitSendQuota;
				}
			}

			// Token: 0x17002463 RID: 9315
			// (get) Token: 0x06003E58 RID: 15960 RVA: 0x000BC565 File Offset: 0x000BA765
			public Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota
			{
				get
				{
					if (!this.UseDatabaseQuotaDefaults)
					{
						return this.mailbox.ProhibitSendReceiveQuota;
					}
					return this.mailboxDatabase.ProhibitSendReceiveQuota;
				}
			}

			// Token: 0x17002464 RID: 9316
			// (get) Token: 0x06003E59 RID: 15961 RVA: 0x000BC586 File Offset: 0x000BA786
			public Unlimited<ByteQuantifiedSize> IssueWarningQuota
			{
				get
				{
					if (!this.UseDatabaseQuotaDefaults)
					{
						return this.mailbox.IssueWarningQuota;
					}
					return this.mailboxDatabase.IssueWarningQuota;
				}
			}

			// Token: 0x17002465 RID: 9317
			// (get) Token: 0x06003E5A RID: 15962 RVA: 0x000BC5A7 File Offset: 0x000BA7A7
			public uint MailboxUsagePercentage
			{
				get
				{
					return MailboxPropertiesHelper.MailboxUsage.CalculateUsagePercentage(this.mailboxStatistics, this.ProhibitSendQuota);
				}
			}

			// Token: 0x17002466 RID: 9318
			// (get) Token: 0x06003E5B RID: 15963 RVA: 0x000BC5BA File Offset: 0x000BA7BA
			public uint ArchiveUsagePercentage
			{
				get
				{
					return MailboxPropertiesHelper.MailboxUsage.CalculateUsagePercentage(this.archiveStatistics, this.mailbox.ArchiveQuota);
				}
			}

			// Token: 0x17002467 RID: 9319
			// (get) Token: 0x06003E5C RID: 15964 RVA: 0x000BC5D2 File Offset: 0x000BA7D2
			public StatisticsBarState MailboxUsageState
			{
				get
				{
					return MailboxPropertiesHelper.MailboxUsage.CalculateUsageState(this.mailboxStatistics, this.ProhibitSendQuota, this.IssueWarningQuota);
				}
			}

			// Token: 0x17002468 RID: 9320
			// (get) Token: 0x06003E5D RID: 15965 RVA: 0x000BC5EB File Offset: 0x000BA7EB
			public StatisticsBarState ArchiveUsageState
			{
				get
				{
					return MailboxPropertiesHelper.MailboxUsage.CalculateUsageState(this.archiveStatistics, this.mailbox.ArchiveQuota, this.mailbox.ArchiveWarningQuota);
				}
			}

			// Token: 0x17002469 RID: 9321
			// (get) Token: 0x06003E5E RID: 15966 RVA: 0x000BC60E File Offset: 0x000BA80E
			public string MailboxUsageText
			{
				get
				{
					return MailboxPropertiesHelper.MailboxUsage.BuildUsageText(this.mailboxStatistics, this.ProhibitSendQuota);
				}
			}

			// Token: 0x1700246A RID: 9322
			// (get) Token: 0x06003E5F RID: 15967 RVA: 0x000BC621 File Offset: 0x000BA821
			public string ArchiveUsageText
			{
				get
				{
					return MailboxPropertiesHelper.MailboxUsage.BuildUsageText(this.archiveStatistics, this.mailbox.ArchiveQuota);
				}
			}

			// Token: 0x06003E60 RID: 15968 RVA: 0x000BC63C File Offset: 0x000BA83C
			private static uint CalculateUsagePercentage(MailboxStatistics statistics, Unlimited<ByteQuantifiedSize> quota)
			{
				if (statistics == null)
				{
					return 0U;
				}
				if (!quota.IsUnlimited)
				{
					return (uint)(statistics.TotalItemSize.Value.ToBytes() / quota.Value.ToBytes() * 100.0);
				}
				if (statistics.TotalItemSize.IsUnlimited)
				{
					return 100U;
				}
				if (statistics.TotalItemSize.Value.ToBytes() > 0UL)
				{
					return 3U;
				}
				return 0U;
			}

			// Token: 0x06003E61 RID: 15969 RVA: 0x000BC6C0 File Offset: 0x000BA8C0
			private static StatisticsBarState CalculateUsageState(MailboxStatistics statistics, Unlimited<ByteQuantifiedSize> quota, Unlimited<ByteQuantifiedSize> warningQuota)
			{
				if (statistics != null)
				{
					if (statistics.StorageLimitStatus != null)
					{
						if (statistics.StorageLimitStatus == StorageLimitStatus.ProhibitSend || statistics.StorageLimitStatus == StorageLimitStatus.MailboxDisabled)
						{
							return StatisticsBarState.Exceeded;
						}
						if (statistics.StorageLimitStatus == StorageLimitStatus.IssueWarning)
						{
							return StatisticsBarState.Warning;
						}
					}
					else
					{
						if (!quota.IsUnlimited && statistics.TotalItemSize.Value.ToBytes() > quota.Value.ToBytes())
						{
							return StatisticsBarState.Exceeded;
						}
						if (!warningQuota.IsUnlimited && statistics.TotalItemSize.Value.ToBytes() > warningQuota.Value.ToBytes())
						{
							return StatisticsBarState.Warning;
						}
					}
				}
				return StatisticsBarState.Normal;
			}

			// Token: 0x06003E62 RID: 15970 RVA: 0x000BC7AC File Offset: 0x000BA9AC
			private static string BuildUsageText(MailboxStatistics statistics, Unlimited<ByteQuantifiedSize> quota)
			{
				string format = string.Empty;
				if (quota.IsUnlimited)
				{
					format = Strings.MailboxUsageWithUnlimitedText;
				}
				else
				{
					format = Strings.MailboxUsageText;
				}
				if (statistics == null)
				{
					return string.Format(format, new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(0UL)).ToAppropriateUnitFormatString(), MailboxPropertiesHelper.MailboxUsage.CalculateUsagePercentage(statistics, quota), quota.ToAppropriateUnitFormatString());
				}
				return string.Format(format, statistics.TotalItemSize.ToAppropriateUnitFormatString(), MailboxPropertiesHelper.MailboxUsage.CalculateUsagePercentage(statistics, quota), quota.ToAppropriateUnitFormatString());
			}

			// Token: 0x04002886 RID: 10374
			private Mailbox mailbox;

			// Token: 0x04002887 RID: 10375
			private MailboxDatabase mailboxDatabase;

			// Token: 0x04002888 RID: 10376
			private MailboxStatistics archiveStatistics;

			// Token: 0x04002889 RID: 10377
			private MailboxStatistics mailboxStatistics;
		}
	}
}
