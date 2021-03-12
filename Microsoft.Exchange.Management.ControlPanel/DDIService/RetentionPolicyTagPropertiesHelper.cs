using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000267 RID: 615
	public static class RetentionPolicyTagPropertiesHelper
	{
		// Token: 0x06002946 RID: 10566 RVA: 0x00081E30 File Offset: 0x00080030
		public static void GetListPostAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			List<DataRow> list = new List<DataRow>();
			dataTable.BeginLoadData();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				ElcFolderType elcFolderType = (ElcFolderType)dataRow["Type"];
				if (elcFolderType == ElcFolderType.ManagedCustomFolder || elcFolderType == ElcFolderType.RecoverableItems)
				{
					list.Add(dataRow);
				}
				else
				{
					EnhancedTimeSpan? ageLimitForRetention = dataRow["AgeLimitForRetention"].IsNullValue() ? null : ((EnhancedTimeSpan?)dataRow["AgeLimitForRetention"]);
					dataRow["FolderType"] = RetentionUtils.GetLocalizedType((ElcFolderType)dataRow["Type"]);
					dataRow["RetentionDays"] = RetentionPolicyTagPropertiesHelper.GetRetentionDays(ageLimitForRetention, (bool)dataRow["RetentionEnabled"]);
					dataRow["RetentionPeriodDays"] = RetentionPolicyTagPropertiesHelper.GetRetentionPeriodDays(ageLimitForRetention, (bool)dataRow["RetentionEnabled"]);
					dataRow["RetentionPolicyActionType"] = RetentionUtils.GetLocalizedRetentionActionType((RetentionActionType)dataRow["RetentionAction"]);
				}
			}
			foreach (DataRow row in list)
			{
				dataTable.Rows.Remove(row);
			}
			dataTable.EndLoadData();
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x00081FC4 File Offset: 0x000801C4
		public static void GetForSDOPostAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			EnhancedTimeSpan? ageLimitForRetention = dataRow["AgeLimitForRetention"].IsNullValue() ? null : ((EnhancedTimeSpan?)dataRow["AgeLimitForRetention"]);
			dataRow["FolderType"] = RetentionUtils.GetLocalizedType((ElcFolderType)dataRow["Type"]);
			dataRow["RetentionPeriodDetail"] = RetentionPolicyTagPropertiesHelper.GetRetentionPeriodDetail(ageLimitForRetention, (bool)dataRow["RetentionEnabled"]);
			dataRow["RetentionActionDescription"] = RetentionPolicyTagPropertiesHelper.GetLocalizedRetentionAction((bool)dataRow["RetentionEnabled"], (RetentionActionType)dataRow["RetentionAction"]);
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x0008208C File Offset: 0x0008028C
		public static void GetObjectPostAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			EnhancedTimeSpan? ageLimitForRetention = dataRow["AgeLimitForRetention"].IsNullValue() ? null : ((EnhancedTimeSpan?)dataRow["AgeLimitForRetention"]);
			dataRow["TypeGroup"] = RetentionPolicyTagPropertiesHelper.GetLocalizedType((ElcFolderType)dataRow["Type"]);
			dataRow["AgeLimitForRetention"] = ((ageLimitForRetention != null) ? ageLimitForRetention.Value.Days.ToString() : null);
			dataRow["FolderType"] = RetentionUtils.GetLocalizedType((ElcFolderType)dataRow["Type"]);
			dataRow["RetentionDays"] = RetentionPolicyTagPropertiesHelper.GetRetentionDays(ageLimitForRetention, (bool)dataRow["RetentionEnabled"]);
			dataRow["RetentionPeriodDays"] = RetentionPolicyTagPropertiesHelper.GetRetentionPeriodDays(ageLimitForRetention, (bool)dataRow["RetentionEnabled"]);
			dataRow["RetentionPolicyActionType"] = RetentionUtils.GetLocalizedRetentionActionType((RetentionActionType)dataRow["RetentionAction"]);
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x000821B8 File Offset: 0x000803B8
		public static void SetObjectPreAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			if (!dataRow["RetentionEnabled"].IsNullValue() && dataRow["RetentionEnabled"].IsFalse())
			{
				dataRow["AgeLimitForRetention"] = null;
			}
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x00082210 File Offset: 0x00080410
		public static void GetListForPickerPostAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			List<DataRow> list = new List<DataRow>();
			dataTable.BeginLoadData();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				ElcFolderType elcFolderType = (ElcFolderType)dataRow["Type"];
				if (elcFolderType == ElcFolderType.ManagedCustomFolder)
				{
					list.Add(dataRow);
				}
				else
				{
					EnhancedTimeSpan? ageLimitForRetention = dataRow["AgeLimitForRetention"].IsNullValue() ? null : ((EnhancedTimeSpan?)dataRow["AgeLimitForRetention"]);
					dataRow["FolderType"] = RetentionUtils.GetLocalizedType((ElcFolderType)dataRow["Type"]);
					dataRow["RetentionDays"] = RetentionPolicyTagPropertiesHelper.GetRetentionDays(ageLimitForRetention, (bool)dataRow["RetentionEnabled"]);
					dataRow["RetentionPeriodDays"] = RetentionPolicyTagPropertiesHelper.GetRetentionPeriodDays(ageLimitForRetention, (bool)dataRow["RetentionEnabled"]);
					dataRow["RetentionPolicyActionType"] = RetentionUtils.GetLocalizedRetentionActionType((RetentionActionType)dataRow["RetentionAction"]);
				}
			}
			foreach (DataRow row in list)
			{
				dataTable.Rows.Remove(row);
			}
			dataTable.EndLoadData();
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x000823A0 File Offset: 0x000805A0
		private static string GetRetentionPeriodDays(EnhancedTimeSpan? ageLimitForRetention, bool retentionEnabled)
		{
			if (ageLimitForRetention != null && retentionEnabled)
			{
				int days = ageLimitForRetention.Value.Days;
				return string.Format((days > 1) ? Strings.RPTDays : Strings.RPTDay, days);
			}
			return Strings.Unlimited;
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x000823F4 File Offset: 0x000805F4
		private static int GetRetentionDays(EnhancedTimeSpan? ageLimitForRetention, bool retentionEnabled)
		{
			if (ageLimitForRetention == null || !retentionEnabled)
			{
				return int.MaxValue;
			}
			return ageLimitForRetention.Value.Days;
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x00082422 File Offset: 0x00080622
		private static string GetRetentionPeriodDetail(EnhancedTimeSpan? ageLimitForRetention, bool retentionEnabled)
		{
			if (ageLimitForRetention != null && retentionEnabled)
			{
				return RetentionPolicyTagPropertiesHelper.GetRetentionPeriodDays(ageLimitForRetention, retentionEnabled);
			}
			return Strings.RPTNone;
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x00082444 File Offset: 0x00080644
		private static string GetLocalizedRetentionAction(bool retentionEnabled, RetentionActionType retentionActionType)
		{
			string result = LocalizedDescriptionAttribute.FromEnum(typeof(RetentionActionType), retentionActionType);
			bool flag = retentionActionType == RetentionActionType.MoveToArchive;
			if (retentionActionType == RetentionActionType.DeleteAndAllowRecovery)
			{
				result = Strings.RetentionActionDeleteAndAllowRecovery;
			}
			if (!retentionEnabled)
			{
				if (flag)
				{
					result = Strings.RetentionActionNeverMove;
				}
				else
				{
					result = Strings.RetentionActionNeverDelete;
				}
			}
			return result;
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x0008249C File Offset: 0x0008069C
		private static string GetLocalizedType(ElcFolderType folderType)
		{
			string result = Strings.RetentionTagNewSystemFolder;
			if (folderType != ElcFolderType.All)
			{
				if (folderType == ElcFolderType.Personal)
				{
					result = Strings.RetentionTagNewPersonal;
				}
			}
			else
			{
				result = Strings.RetentionTagNewAll;
			}
			return result;
		}
	}
}
