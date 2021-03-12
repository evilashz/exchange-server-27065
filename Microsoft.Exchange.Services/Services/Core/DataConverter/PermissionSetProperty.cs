using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200014E RID: 334
	internal sealed class PermissionSetProperty : PermissionSetPropertyBase<PermissionType>, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x0600092A RID: 2346 RVA: 0x0002CC48 File Offset: 0x0002AE48
		public PermissionSetProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0002CC51 File Offset: 0x0002AE51
		public static PermissionSetProperty CreateCommand(CommandContext commandContext)
		{
			return new PermissionSetProperty(commandContext);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0002CC5C File Offset: 0x0002AE5C
		void IToServiceObjectCommand.ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			Folder folder = commandSettings.StoreObject as Folder;
			BaseFolderType baseFolderType = commandSettings.ServiceObject as BaseFolderType;
			FolderType folderType = baseFolderType as FolderType;
			ContactsFolderType contactsFolderType = baseFolderType as ContactsFolderType;
			if (folderType != null)
			{
				folderType.PermissionSet = new PermissionRenderer(folder).Render();
				return;
			}
			if (contactsFolderType != null)
			{
				contactsFolderType.PermissionSet = new PermissionRenderer(folder).Render();
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0002CCC2 File Offset: 0x0002AEC2
		protected override void ConfirmFolderIsProperType(Folder folder)
		{
			if (folder is CalendarFolder)
			{
				throw new CannotSetCalendarPermissionOnNonCalendarFolderException();
			}
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0002CCD4 File Offset: 0x0002AED4
		protected override PermissionInformationBase<PermissionType>[] ParsePermissions(BasePermissionSetType permissionSet, Folder folder)
		{
			List<PermissionInformation> list = new List<PermissionInformation>();
			foreach (PermissionType permissionType in (permissionSet as PermissionSetType).Permissions)
			{
				list.Add(new PermissionInformation(permissionType));
			}
			return list.ToArray();
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0002CD18 File Offset: 0x0002AF18
		protected override BasePermissionSetType GetPermissionSet(BaseFolderType serviceObject)
		{
			FolderType folderType = serviceObject as FolderType;
			ContactsFolderType contactsFolderType = serviceObject as ContactsFolderType;
			if (folderType != null)
			{
				return folderType.PermissionSet;
			}
			if (contactsFolderType != null)
			{
				return contactsFolderType.PermissionSet;
			}
			throw new InvalidOperationException("[PermissionSetProperty::GetPermissionSet]");
		}
	}
}
