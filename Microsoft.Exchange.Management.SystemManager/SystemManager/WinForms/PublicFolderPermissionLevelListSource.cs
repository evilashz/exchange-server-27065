using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001F6 RID: 502
	internal class PublicFolderPermissionLevelListSource : EnumListSource
	{
		// Token: 0x060016C1 RID: 5825 RVA: 0x0005F917 File Offset: 0x0005DB17
		public PublicFolderPermissionLevelListSource() : base(PublicFolderPermissionLevelListSource.permissionRoleList, typeof(PublicFolderPermission))
		{
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0005F939 File Offset: 0x0005DB39
		protected override string GetValueText(object objectValue)
		{
			return this.converter.Format(null, objectValue, null);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0005F949 File Offset: 0x0005DB49
		public static bool ContainsPermission(PublicFolderPermission value)
		{
			return PublicFolderPermissionLevelListSource.permissionRoleList.Contains(value);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0005F958 File Offset: 0x0005DB58
		// Note: this type is marked as 'beforefieldinit'.
		static PublicFolderPermissionLevelListSource()
		{
			PublicFolderPermission[] array = new PublicFolderPermission[9];
			array[0] = (PublicFolderPermission.ReadItems | PublicFolderPermission.CreateItems | PublicFolderPermission.EditOwnedItems | PublicFolderPermission.DeleteOwnedItems | PublicFolderPermission.EditAllItems | PublicFolderPermission.DeleteAllItems | PublicFolderPermission.CreateSubfolders | PublicFolderPermission.FolderOwner | PublicFolderPermission.FolderContact | PublicFolderPermission.FolderVisible);
			array[1] = (PublicFolderPermission.ReadItems | PublicFolderPermission.CreateItems | PublicFolderPermission.EditOwnedItems | PublicFolderPermission.DeleteOwnedItems | PublicFolderPermission.EditAllItems | PublicFolderPermission.DeleteAllItems | PublicFolderPermission.CreateSubfolders | PublicFolderPermission.FolderVisible);
			array[2] = (PublicFolderPermission.ReadItems | PublicFolderPermission.CreateItems | PublicFolderPermission.EditOwnedItems | PublicFolderPermission.DeleteOwnedItems | PublicFolderPermission.EditAllItems | PublicFolderPermission.DeleteAllItems | PublicFolderPermission.FolderVisible);
			array[3] = (PublicFolderPermission.ReadItems | PublicFolderPermission.CreateItems | PublicFolderPermission.EditOwnedItems | PublicFolderPermission.DeleteOwnedItems | PublicFolderPermission.CreateSubfolders | PublicFolderPermission.FolderVisible);
			array[4] = (PublicFolderPermission.ReadItems | PublicFolderPermission.CreateItems | PublicFolderPermission.EditOwnedItems | PublicFolderPermission.DeleteOwnedItems | PublicFolderPermission.FolderVisible);
			array[5] = (PublicFolderPermission.ReadItems | PublicFolderPermission.CreateItems | PublicFolderPermission.DeleteOwnedItems | PublicFolderPermission.FolderVisible);
			array[6] = (PublicFolderPermission.ReadItems | PublicFolderPermission.FolderVisible);
			array[7] = (PublicFolderPermission.CreateItems | PublicFolderPermission.FolderVisible);
			PublicFolderPermissionLevelListSource.permissionRoleList = array;
		}

		// Token: 0x04000868 RID: 2152
		private static PublicFolderPermission[] permissionRoleList;

		// Token: 0x04000869 RID: 2153
		private ICustomTextConverter converter = new PublicFolderPermissionAsRoleCoverter();
	}
}
