using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000144 RID: 324
	internal sealed class CalendarPermissionSetProperty : PermissionSetPropertyBase<CalendarPermissionType>, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x060008EC RID: 2284 RVA: 0x0002BC2C File Offset: 0x00029E2C
		public CalendarPermissionSetProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0002BC35 File Offset: 0x00029E35
		public static CalendarPermissionSetProperty CreateCommand(CommandContext commandContext)
		{
			return new CalendarPermissionSetProperty(commandContext);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0002BC40 File Offset: 0x00029E40
		void IToServiceObjectCommand.ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			CalendarFolder calendarFolder = commandSettings.StoreObject as CalendarFolder;
			CalendarFolderType calendarFolderType = commandSettings.ServiceObject as CalendarFolderType;
			if (calendarFolderType != null)
			{
				calendarFolderType.PermissionSet = new CalendarPermissionRenderer(calendarFolder).Render();
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0002BC80 File Offset: 0x00029E80
		protected override void ConfirmFolderIsProperType(Folder folder)
		{
			if (!(folder is CalendarFolder))
			{
				throw new CannotSetNonCalendarPermissionOnCalendarFolderException();
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0002BC90 File Offset: 0x00029E90
		protected override BasePermissionSetType GetPermissionSet(BaseFolderType serviceObject)
		{
			return (serviceObject as CalendarFolderType).PermissionSet;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0002BCA0 File Offset: 0x00029EA0
		protected override PermissionInformationBase<CalendarPermissionType>[] ParsePermissions(BasePermissionSetType permissionSet, Folder folder)
		{
			List<CalendarPermissionInformation> list = new List<CalendarPermissionInformation>();
			foreach (CalendarPermissionType permissionElement in (permissionSet as CalendarPermissionSetType).CalendarPermissions)
			{
				list.Add(new CalendarPermissionInformation(permissionElement));
			}
			return list.ToArray();
		}
	}
}
