using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000184 RID: 388
	internal class PublicFolderPermissionAsRoleCoverter : TextConverter
	{
		// Token: 0x06000F40 RID: 3904 RVA: 0x0003B0E8 File Offset: 0x000392E8
		protected override string FormatObject(string format, object arg, IFormatProvider formatProvider)
		{
			if (Enum.IsDefined(typeof(PublicFolderPermissionRole), (int)arg))
			{
				return LocalizedDescriptionAttribute.FromEnum(typeof(PublicFolderPermissionRole), (int)arg);
			}
			if (PublicFolderPermission.None.Equals(arg))
			{
				return Strings.PublicFolderPermissionRoleNone;
			}
			return Strings.PublicFolderPermissionRoleCustom;
		}
	}
}
