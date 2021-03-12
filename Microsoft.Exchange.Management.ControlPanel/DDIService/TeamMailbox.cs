using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000497 RID: 1175
	public sealed class TeamMailbox
	{
		// Token: 0x06003A6F RID: 14959 RVA: 0x000B0EFC File Offset: 0x000AF0FC
		public static void NewTeamMailboxPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			TeamMailbox.TeamMailboxPostAction(inputRow, dataTable, store);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataRow["Owners"] = new ADObjectId[]
				{
					RbacPrincipal.Current.ExecutingUserId
				};
			}
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x000B0F78 File Offset: 0x000AF178
		public static void TeamMailboxPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataRow["IsOwner"] = TeamMailbox.IsExecutingUserAnOwner(dataRow["Owners"]);
				if (dataRow["Active"] is bool)
				{
					if ((bool)dataRow["Active"])
					{
						dataRow["Status"] = OwaOptionClientStrings.TeamMailboxLifecycleActive;
					}
					else
					{
						dataRow["Status"] = OwaOptionClientStrings.TeamMailboxLifecycleClosed;
						dataRow["ClosedTime"] = ((DateTime)dataRow["ClosedTime"]).UtcToUserDateString();
					}
					dataRow["IsEditable"] = ((bool)dataRow["IsOwner"] && (bool)dataRow["Active"]);
				}
				int userCount = TeamMailbox.GetUserCount(dataRow["Members"]);
				if (userCount == 0)
				{
					dataRow["MembersNumber"] = "0";
				}
				else
				{
					dataRow["MembersNumber"] = userCount;
				}
				dataRow["SPSiteLinked"] = ((dataRow["SharePointLinkedBy"] is ADObjectId) ? OwaOptionClientStrings.TeamMailboxSharePointConnectedTrue : OwaOptionClientStrings.TeamMailboxSharePointConnectedFalse);
				if (dataRow["MyRole"] is string)
				{
					string a;
					if ((a = (string)dataRow["MyRole"]) != null)
					{
						if (a == "Owner")
						{
							dataRow["MyRole"] = OwaOptionClientStrings.TeamMailboxYourRoleOwner;
							goto IL_1B1;
						}
						if (a == "Member")
						{
							dataRow["MyRole"] = OwaOptionClientStrings.TeamMailboxYourRoleMember;
							goto IL_1B1;
						}
					}
					dataRow["MyRole"] = OwaOptionClientStrings.TeamMailboxYourRoleNoAccess;
				}
				IL_1B1:
				dataRow["ExecutingUserId"] = RbacPrincipal.Current.ExecutingUserId;
				dataRow["MailToPrimarySmtpAddress"] = "mailto:" + dataRow["PrimarySmtpAddress"];
			}
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x000B11A8 File Offset: 0x000AF3A8
		public static void TeamMailboxDiagnosticsPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			TeamMailboxDiagnosticsInfo teamMailboxDiagnosticsInfo = store.GetDataObject("TeamMailboxDiagnosticsInfo") as TeamMailboxDiagnosticsInfo;
			if (dataTable.Rows.Count == 1 && teamMailboxDiagnosticsInfo != null)
			{
				if (teamMailboxDiagnosticsInfo.Status == TeamMailboxSyncStatus.NotAvailable)
				{
					dataTable.Rows[0]["DocumentSyncStatus"] = OwaOptionClientStrings.TeamMailboxSyncNotAvailable;
					dataTable.Rows[0]["MembershipSyncStatus"] = OwaOptionClientStrings.TeamMailboxSyncNotAvailable;
					dataTable.Rows[0]["MaintenanceSyncStatus"] = OwaOptionClientStrings.TeamMailboxSyncNotAvailable;
				}
				else
				{
					dataTable.Rows[0]["DocumentSyncStatus"] = ((teamMailboxDiagnosticsInfo.HierarchySyncInfo == null) ? OwaOptionClientStrings.TeamMailboxSyncNotAvailable : OwaOptionClientStrings.TeamMailboxSyncSuccess);
					dataTable.Rows[0]["MembershipSyncStatus"] = ((teamMailboxDiagnosticsInfo.MembershipSyncInfo == null) ? OwaOptionClientStrings.TeamMailboxSyncNotAvailable : OwaOptionClientStrings.TeamMailboxSyncSuccess);
					dataTable.Rows[0]["MaintenanceSyncStatus"] = ((teamMailboxDiagnosticsInfo.MaintenanceSyncInfo == null) ? OwaOptionClientStrings.TeamMailboxSyncNotAvailable : OwaOptionClientStrings.TeamMailboxSyncSuccess);
				}
				if (teamMailboxDiagnosticsInfo.Status == TeamMailboxSyncStatus.Failed || teamMailboxDiagnosticsInfo.Status == TeamMailboxSyncStatus.DocumentSyncFailureOnly || teamMailboxDiagnosticsInfo.Status == TeamMailboxSyncStatus.DocumentAndMembershipSyncFailure || teamMailboxDiagnosticsInfo.Status == TeamMailboxSyncStatus.DocumentAndMaintenanceSyncFailure)
				{
					dataTable.Rows[0]["DocumentSyncStatus"] = OwaOptionClientStrings.TeamMailboxSyncError;
				}
				if (teamMailboxDiagnosticsInfo.Status == TeamMailboxSyncStatus.Failed || teamMailboxDiagnosticsInfo.Status == TeamMailboxSyncStatus.MembershipSyncFailureOnly || teamMailboxDiagnosticsInfo.Status == TeamMailboxSyncStatus.DocumentAndMembershipSyncFailure || teamMailboxDiagnosticsInfo.Status == TeamMailboxSyncStatus.MembershipAndMaintenanceSyncFailure)
				{
					dataTable.Rows[0]["MembershipSyncStatus"] = OwaOptionClientStrings.TeamMailboxSyncError;
				}
				if (teamMailboxDiagnosticsInfo.Status == TeamMailboxSyncStatus.Failed || teamMailboxDiagnosticsInfo.Status == TeamMailboxSyncStatus.MaintenanceSyncFailureOnly || teamMailboxDiagnosticsInfo.Status == TeamMailboxSyncStatus.MembershipAndMaintenanceSyncFailure || teamMailboxDiagnosticsInfo.Status == TeamMailboxSyncStatus.DocumentAndMaintenanceSyncFailure)
				{
					dataTable.Rows[0]["MaintenanceSyncStatus"] = OwaOptionClientStrings.TeamMailboxSyncError;
				}
				dataTable.Rows[0]["MembershipSyncDate"] = ((teamMailboxDiagnosticsInfo.MembershipSyncInfo == null) ? OwaOptionClientStrings.TeamMailboxSyncNotAvailable : ((DateTime)teamMailboxDiagnosticsInfo.MembershipSyncInfo.LastAttemptedSyncTime.Value).UtcToUserDateTimeString());
				dataTable.Rows[0]["MaintenanceSyncDate"] = ((teamMailboxDiagnosticsInfo.MaintenanceSyncInfo == null) ? OwaOptionClientStrings.TeamMailboxSyncNotAvailable : ((DateTime)teamMailboxDiagnosticsInfo.MaintenanceSyncInfo.LastAttemptedSyncTime.Value).UtcToUserDateTimeString());
				dataTable.Rows[0]["DocumentSyncDate"] = ((teamMailboxDiagnosticsInfo.HierarchySyncInfo == null) ? OwaOptionClientStrings.TeamMailboxSyncNotAvailable : ((DateTime)teamMailboxDiagnosticsInfo.HierarchySyncInfo.LastAttemptedSyncTime.Value).UtcToUserDateTimeString());
				dataTable.Rows[0]["SynchronizationDetails"] = teamMailboxDiagnosticsInfo.ToString();
				dataTable.Rows[0]["MembershipSyncStatus"] = OwaOptionClientStrings.TeamMailboxSyncStatus + dataTable.Rows[0]["MembershipSyncStatus"];
				dataTable.Rows[0]["MembershipSyncDate"] = OwaOptionClientStrings.TeamMailboxSyncDate + dataTable.Rows[0]["MembershipSyncDate"];
				dataTable.Rows[0]["MaintenanceSyncStatus"] = OwaOptionClientStrings.TeamMailboxSyncStatus + dataTable.Rows[0]["MaintenanceSyncStatus"];
				dataTable.Rows[0]["MaintenanceSyncDate"] = OwaOptionClientStrings.TeamMailboxSyncDate + dataTable.Rows[0]["MaintenanceSyncDate"];
				dataTable.Rows[0]["DocumentSyncStatus"] = OwaOptionClientStrings.TeamMailboxSyncStatus + dataTable.Rows[0]["DocumentSyncStatus"];
				dataTable.Rows[0]["DocumentSyncDate"] = OwaOptionClientStrings.TeamMailboxSyncDate + dataTable.Rows[0]["DocumentSyncDate"];
				dataTable.Rows[0]["TeamMailboxMembershipString1"] = OwaOptionClientStrings.TeamMailboxMembershipString1;
				dataTable.Rows[0]["TeamMailboxMembershipString2"] = OwaOptionClientStrings.TeamMailboxMembershipString2;
				dataTable.Rows[0]["TeamMailboxMembershipString3"] = OwaOptionClientStrings.TeamMailboxMembershipString3;
				dataTable.Rows[0]["TeamMailboxMembershipString4"] = OwaOptionClientStrings.TeamMailboxMembershipString4;
				dataTable.Rows[0]["TeamMailboxStartedMembershipSync"] = OwaOptionClientStrings.TeamMailboxStartedMembershipSync;
				dataTable.Rows[0]["TeamMailboxStartedMaintenanceSync"] = OwaOptionClientStrings.TeamMailboxStartedMaintenanceSync;
				dataTable.Rows[0]["TeamMailboxStartedDocumentSync"] = OwaOptionClientStrings.TeamMailboxStartedDocumentSync;
			}
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x000B1658 File Offset: 0x000AF858
		private static bool IsExecutingUserAnOwner(object value)
		{
			if (value is IEnumerable<ADObjectId>)
			{
				IEnumerable<ADObjectId> enumerable = (IEnumerable<ADObjectId>)value;
				foreach (ADObjectId adobjectId in enumerable)
				{
					if (adobjectId.Equals(RbacPrincipal.Current.ExecutingUserId))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x000B16C4 File Offset: 0x000AF8C4
		private static int GetUserCount(object value)
		{
			int num = 0;
			if (value is IEnumerable<ADObjectId>)
			{
				IEnumerable<ADObjectId> enumerable = (IEnumerable<ADObjectId>)value;
				foreach (ADObjectId adobjectId in enumerable)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x04002707 RID: 9991
		private const string TeamMailboxDiagnosticsInfo = "TeamMailboxDiagnosticsInfo";

		// Token: 0x04002708 RID: 9992
		private const string MembershipSyncStatus = "MembershipSyncStatus";

		// Token: 0x04002709 RID: 9993
		private const string MaintenanceSyncStatus = "MaintenanceSyncStatus";

		// Token: 0x0400270A RID: 9994
		private const string DocumentSyncStatus = "DocumentSyncStatus";

		// Token: 0x0400270B RID: 9995
		private const string MembershipSyncDate = "MembershipSyncDate";

		// Token: 0x0400270C RID: 9996
		private const string MaintenanceSyncDate = "MaintenanceSyncDate";

		// Token: 0x0400270D RID: 9997
		private const string DocumentSyncDate = "DocumentSyncDate";

		// Token: 0x0400270E RID: 9998
		private const string SynchronizationDetails = "SynchronizationDetails";

		// Token: 0x0400270F RID: 9999
		private const string TeamMailboxMembershipString1 = "TeamMailboxMembershipString1";

		// Token: 0x04002710 RID: 10000
		private const string TeamMailboxMembershipString2 = "TeamMailboxMembershipString2";

		// Token: 0x04002711 RID: 10001
		private const string TeamMailboxMembershipString3 = "TeamMailboxMembershipString3";

		// Token: 0x04002712 RID: 10002
		private const string TeamMailboxMembershipString4 = "TeamMailboxMembershipString4";

		// Token: 0x04002713 RID: 10003
		private const string TeamMailboxStartedMembershipSync = "TeamMailboxStartedMembershipSync";

		// Token: 0x04002714 RID: 10004
		private const string TeamMailboxStartedMaintenanceSync = "TeamMailboxStartedMaintenanceSync";

		// Token: 0x04002715 RID: 10005
		private const string TeamMailboxStartedDocumentSync = "TeamMailboxStartedDocumentSync";
	}
}
