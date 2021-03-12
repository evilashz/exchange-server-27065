using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.MapiTasks;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200026D RID: 621
	public class PublicFolderClientPermissionHelper
	{
		// Token: 0x06001A9F RID: 6815 RVA: 0x000757EC File Offset: 0x000739EC
		private static void AddEntry(DataTable table, ADObjectId identity, string name, MultiValuedProperty<PublicFolderAccessRight> accessRights)
		{
			DataRow dataRow = table.NewRow();
			dataRow["Identity"] = identity;
			dataRow["Name"] = name;
			dataRow["AccessRights"] = PublicFolderAccessRight.CalculatePublicFolderPermission(accessRights);
			table.Rows.Add(dataRow);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00075870 File Offset: 0x00073A70
		public static DataTable GenerateClientPermissionDataSource(object clientPermission)
		{
			AutomatedObjectPicker automatedObjectPicker = new AutomatedObjectPicker("PublicFolderClientPermissionConfigurable");
			DataTable dataTable = automatedObjectPicker.ObjectPickerProfile.DataTable.Clone();
			List<object> list = clientPermission as List<object>;
			if (list != null)
			{
				list.RemoveAll(delegate(object entry)
				{
					PublicFolderUserId user = (entry as PublicFolderClientPermissionEntry).User;
					return user.ActiveDirectoryIdentity == null && !user.IsAnonymous && !user.IsDefault;
				});
				foreach (object obj in list)
				{
					PublicFolderClientPermissionEntry publicFolderClientPermissionEntry = (PublicFolderClientPermissionEntry)obj;
					ADObjectId identity = PublicFolderClientPermissionHelper.ConvertUserToAdObjectId(publicFolderClientPermissionEntry.User);
					string name = (publicFolderClientPermissionEntry.User.ActiveDirectoryIdentity != null) ? publicFolderClientPermissionEntry.User.ActiveDirectoryIdentity.Name : publicFolderClientPermissionEntry.User.ToString();
					PublicFolderClientPermissionHelper.AddEntry(dataTable, identity, name, publicFolderClientPermissionEntry.AccessRights);
				}
				dataTable.DefaultView.Sort = "Identity asc";
				dataTable.AcceptChanges();
			}
			return dataTable;
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x0007596C File Offset: 0x00073B6C
		internal static object ConvertAdObjectIdToUser(ADObjectId id)
		{
			object result = id;
			if (id == PublicFolderClientPermissionHelper.DefaultUserId)
			{
				result = PublicFolderUserId.DefaultUserId;
			}
			if (id == PublicFolderClientPermissionHelper.AnonymousUserId)
			{
				result = PublicFolderUserId.AnonymousUserId;
			}
			return result;
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x00075998 File Offset: 0x00073B98
		internal static ADObjectId ConvertUserToAdObjectId(PublicFolderUserId id)
		{
			ADObjectId result = id.ActiveDirectoryIdentity;
			if (id.IsDefault)
			{
				result = PublicFolderClientPermissionHelper.DefaultUserId;
			}
			if (id.IsAnonymous)
			{
				result = PublicFolderClientPermissionHelper.AnonymousUserId;
			}
			return result;
		}

		// Token: 0x040009E3 RID: 2531
		internal static readonly ADObjectId DefaultUserId = new ADObjectId(Guid.NewGuid());

		// Token: 0x040009E4 RID: 2532
		internal static readonly ADObjectId AnonymousUserId = new ADObjectId(Guid.NewGuid());
	}
}
