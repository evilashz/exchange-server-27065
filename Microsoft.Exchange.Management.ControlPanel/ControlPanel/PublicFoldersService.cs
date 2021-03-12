using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Permission;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.DDIService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000311 RID: 785
	public static class PublicFoldersService
	{
		// Token: 0x17001EAE RID: 7854
		// (get) Token: 0x06002E83 RID: 11907 RVA: 0x0008DD32 File Offset: 0x0008BF32
		// (set) Token: 0x06002E84 RID: 11908 RVA: 0x0008DD39 File Offset: 0x0008BF39
		public static string CurrentPath
		{
			get
			{
				return PublicFoldersService.currentPath;
			}
			set
			{
				PublicFoldersService.currentPath = value;
			}
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x0008DD44 File Offset: 0x0008BF44
		public static void GetListPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			List<string> list = new List<string>();
			string value = "\\";
			if (inputRow["SearchText"] != DBNull.Value && !string.IsNullOrWhiteSpace((string)inputRow["SearchText"]))
			{
				value = (((string)inputRow["SearchText"]).StartsWith("\\") ? ((string)inputRow["SearchText"]) : ((string)inputRow["SearchText"]).Insert(0, "\\"));
				inputRow["Identity"] = value;
				list.Add("Identity");
			}
			PublicFoldersService.CurrentPath = value;
			if (list.Count > 0)
			{
				store.SetModifiedColumns(list);
			}
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x0008DE00 File Offset: 0x0008C000
		public static void GetObjectPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			PublicFolder publicFolder = store.GetDataObject("PublicFolder") as PublicFolder;
			PublicFolderStatistics publicFolderStatistics = store.GetDataObject("PublicFolderStatistics") as PublicFolderStatistics;
			if (dataTable.Rows.Count == 1)
			{
				DataRow dataRow = dataTable.Rows[0];
				if (publicFolder != null)
				{
					dataRow["IsStorageQuotasSet"] = (publicFolder.IssueWarningQuota == null || (publicFolder.IssueWarningQuota.Value.IsUnlimited && publicFolder.ProhibitPostQuota.Value.IsUnlimited && publicFolder.MaxItemSize.Value.IsUnlimited));
					dataRow["IssueWarningQuota"] = MailboxPropertiesHelper.UnlimitedByteQuantifiedSizeToString(publicFolder.IssueWarningQuota);
					dataRow["ProhibitPostQuota"] = MailboxPropertiesHelper.UnlimitedByteQuantifiedSizeToString(publicFolder.ProhibitPostQuota);
					dataRow["MaxItemSize"] = MailboxPropertiesHelper.UnlimitedByteQuantifiedSizeToString(publicFolder.MaxItemSize);
					dataRow["IsRetainDeletedItemsForSet"] = (publicFolder.RetainDeletedItemsFor == null);
					dataRow["RetainDeletedItemsFor"] = ((publicFolder.RetainDeletedItemsFor != null) ? publicFolder.RetainDeletedItemsFor.Value.Days.ToString() : "5");
					dataRow["IsAgeLimitSet"] = (publicFolder.AgeLimit == null);
					dataRow["AgeLimit"] = ((publicFolder.AgeLimit != null && publicFolder.AgeLimit != null) ? publicFolder.AgeLimit.Value.Days.ToString() : "5");
				}
				if (publicFolderStatistics != null)
				{
					dataRow["TotalItemSize"] = publicFolderStatistics.TotalItemSize.ToMB().ToString();
					dataRow["TotalAssociatedItemSize"] = ((!publicFolderStatistics.TotalAssociatedItemSize.IsNullValue()) ? publicFolderStatistics.TotalAssociatedItemSize.ToMB() : 0UL);
					dataRow["TotalDeletedItemSize"] = ((!publicFolderStatistics.TotalDeletedItemSize.IsNullValue()) ? publicFolderStatistics.TotalDeletedItemSize.ToMB() : 0UL);
				}
			}
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x0008E088 File Offset: 0x0008C288
		public static void SetObjectPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			List<string> list = new List<string>();
			if (DBNull.Value != inputRow["IssueWarningQuota"] || DBNull.Value != inputRow["ProhibitPostQuota"] || DBNull.Value != inputRow["MaxItemSize"] || DBNull.Value != inputRow["IsStorageQuotasSet"])
			{
				if (DBNull.Value != inputRow["IsStorageQuotasSet"] && (bool)inputRow["IsStorageQuotasSet"])
				{
					dataRow["IssueWarningQuota"] = Unlimited<ByteQuantifiedSize>.UnlimitedString;
					dataRow["ProhibitPostQuota"] = Unlimited<ByteQuantifiedSize>.UnlimitedString;
					dataRow["MaxItemSize"] = Unlimited<ByteQuantifiedSize>.UnlimitedString;
				}
				MailboxPropertiesHelper.SaveQuotaProperty(dataRow, "IsStorageQuotasSet", "IssueWarningQuota", list);
				MailboxPropertiesHelper.SaveQuotaProperty(dataRow, "IsStorageQuotasSet", "ProhibitPostQuota", list);
				MailboxPropertiesHelper.SaveQuotaProperty(dataRow, "IsStorageQuotasSet", "MaxItemSize", list);
			}
			if (DBNull.Value != inputRow["RetainDeletedItemsFor"] || DBNull.Value != inputRow["IsRetainDeletedItemsForSet"])
			{
				if (DBNull.Value != inputRow["IsRetainDeletedItemsForSet"] && (bool)inputRow["IsRetainDeletedItemsForSet"])
				{
					dataRow["RetainDeletedItemsFor"] = null;
				}
				else
				{
					dataRow["RetainDeletedItemsFor"] = EnhancedTimeSpan.Parse((string)dataRow["RetainDeletedItemsFor"]);
				}
				list.Add("RetainDeletedItemsFor");
				list.Add("IsRetainDeletedItemsForSet");
			}
			if (DBNull.Value != inputRow["AgeLimit"] || DBNull.Value != inputRow["IsAgeLimitSet"])
			{
				if (DBNull.Value != inputRow["IsAgeLimitSet"] && (bool)inputRow["IsAgeLimitSet"])
				{
					dataRow["AgeLimit"] = null;
				}
				else
				{
					dataRow["AgeLimit"] = EnhancedTimeSpan.Parse((string)dataRow["AgeLimit"]);
				}
				list.Add("AgeLimit");
				list.Add("IsAgeLimitSet");
			}
			MailboxPropertiesHelper.SetMaxSendReceiveSize(inputRow, dataTable, store);
			if (list.Count > 0)
			{
				store.SetModifiedColumns(list);
			}
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x0008E2B4 File Offset: 0x0008C4B4
		public static void MailFlowSettingsPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			MailboxPropertiesHelper.GetMaxSendReceiveSize(inputRow, dataTable, store);
			MailboxPropertiesHelper.GetAcceptRejectSendersOrMembers(inputRow, dataTable, store);
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x0008E2C8 File Offset: 0x0008C4C8
		public static void GetEmailAddresses(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			if (dataRow["SimpleEmailAddresses"] != DBNull.Value)
			{
				dataRow["EmailAddresses"] = EmailAddressList.FromProxyAddressCollection((ProxyAddressCollection)dataRow["SimpleEmailAddresses"]);
			}
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x0008E314 File Offset: 0x0008C514
		public static void FilterEntSendAsPermission(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			IEnumerable<object> permissions = store.GetDataObject("ADPermissions") as IEnumerable<object>;
			dataTable.Rows[0]["SendAsPermissions"] = PublicFoldersService.FindRecipientsWithSendAsPermissionEnt(permissions, store);
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x0008E350 File Offset: 0x0008C550
		internal static IEnumerable<AcePermissionRecipientRow> FindRecipientsWithSendAsPermissionEnt(IEnumerable<object> permissions, DataObjectStore store)
		{
			List<SecurityPrincipalIdParameter> permissionsHelper = PublicFoldersService.GetPermissionsHelper(permissions, new IsExpectedPermission(PublicFoldersService.IsSendAsPermission), store);
			return RecipientObjectResolver.Instance.ResolveSecurityPrincipalId(permissionsHelper);
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x0008E37C File Offset: 0x0008C57C
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

		// Token: 0x06002E8D RID: 11917 RVA: 0x0008E3D8 File Offset: 0x0008C5D8
		internal static List<SecurityPrincipalIdParameter> GetPermissionsHelper(IEnumerable<object> permissions, IsExpectedPermission isExpectedDelegate, DataObjectStore store)
		{
			List<SecurityIdentifier> list = new List<SecurityIdentifier>();
			List<SecurityIdentifier> list2 = new List<SecurityIdentifier>();
			List<SecurityPrincipalIdParameter> list3 = new List<SecurityPrincipalIdParameter>();
			if (store != null)
			{
				store.GetDataObject("MailPublicFolder");
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
								list3.Add(acePresentationObject.User);
							}
						}
					}
				}
			}
			return list3;
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x0008E4AC File Offset: 0x0008C6AC
		public static void FilterCloudSendAsPermission(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			dataTable.Rows[0]["SendAsPermissions"] = PublicFoldersService.FindRecipientsWithSendAsPermissionCloud(store.GetDataObject("RecipientPermission") as IEnumerable<object>);
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x0008E4DC File Offset: 0x0008C6DC
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

		// Token: 0x040022BA RID: 8890
		internal const string IsRetainDeletedItemsForSetColumnName = "IsRetainDeletedItemsForSet";

		// Token: 0x040022BB RID: 8891
		internal const string RetainDeletedItemsForColumnName = "RetainDeletedItemsFor";

		// Token: 0x040022BC RID: 8892
		internal const string IsAgeLimitSetColumnName = "IsAgeLimitSet";

		// Token: 0x040022BD RID: 8893
		internal const string AgeLimitColumnName = "AgeLimit";

		// Token: 0x040022BE RID: 8894
		internal const string IsStorageQuotasSetColumnName = "IsStorageQuotasSet";

		// Token: 0x040022BF RID: 8895
		internal const string StorageQuotasColumnName = "StorageQuotas";

		// Token: 0x040022C0 RID: 8896
		internal const string MaxItemSizeColumnName = "MaxItemSize";

		// Token: 0x040022C1 RID: 8897
		internal const string ProhibitPostQuotaColumnName = "ProhibitPostQuota";

		// Token: 0x040022C2 RID: 8898
		internal const string IssueWarningQuotaColumnName = "IssueWarningQuota";

		// Token: 0x040022C3 RID: 8899
		internal const string TotalAssociatedItemSizeColumnName = "TotalAssociatedItemSize";

		// Token: 0x040022C4 RID: 8900
		internal const string TotalDeletedItemSizeColumnName = "TotalDeletedItemSize";

		// Token: 0x040022C5 RID: 8901
		internal const string TotalItemSizeColumnName = "TotalItemSize";

		// Token: 0x040022C6 RID: 8902
		internal const string SearchTextColumnName = "SearchText";

		// Token: 0x040022C7 RID: 8903
		internal const string DefaultAgeLimit = "5";

		// Token: 0x040022C8 RID: 8904
		internal const string DefaultRetainDeletedItemsForDays = "5";

		// Token: 0x040022C9 RID: 8905
		private static string currentPath = "\\";
	}
}
