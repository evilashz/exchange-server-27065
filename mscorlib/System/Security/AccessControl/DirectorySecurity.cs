using System;
using System.IO;
using System.Security.Permissions;

namespace System.Security.AccessControl
{
	// Token: 0x0200021D RID: 541
	public sealed class DirectorySecurity : FileSystemSecurity
	{
		// Token: 0x06001F5E RID: 8030 RVA: 0x0006DB0C File Offset: 0x0006BD0C
		[SecuritySafeCritical]
		public DirectorySecurity() : base(true)
		{
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x0006DB18 File Offset: 0x0006BD18
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public DirectorySecurity(string name, AccessControlSections includeSections) : base(true, name, includeSections, true)
		{
			string fullPathInternal = Path.GetFullPathInternal(name);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.NoAccess, AccessControlActions.View, fullPathInternal, false, false);
		}
	}
}
