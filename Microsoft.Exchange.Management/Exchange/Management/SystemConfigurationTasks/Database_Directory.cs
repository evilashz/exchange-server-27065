using System;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200098A RID: 2442
	internal static class Database_Directory
	{
		// Token: 0x0600572C RID: 22316 RVA: 0x00169634 File Offset: 0x00167834
		internal static FileSystemAccessRule[] GetDomainWidePermissions()
		{
			FileSystemAccessRule[] result;
			try
			{
				NTAccount ntaccount = new NTAccount(NativeHelpers.GetDomainName() + "\\Organization Management");
				NTAccount ntaccount2 = new NTAccount(NativeHelpers.GetDomainName() + "\\View-Only Organization Management");
				FileSystemAccessRule fileSystemAccessRule = new FileSystemAccessRule(ntaccount.Translate(typeof(SecurityIdentifier)) as SecurityIdentifier, FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);
				FileSystemAccessRule fileSystemAccessRule2 = new FileSystemAccessRule(ntaccount2.Translate(typeof(SecurityIdentifier)) as SecurityIdentifier, FileSystemRights.ReadAndExecute, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);
				result = new FileSystemAccessRule[]
				{
					Database_Directory.builtInAdmin,
					Database_Directory.builtInLocalSystem,
					fileSystemAccessRule,
					fileSystemAccessRule2
				};
			}
			catch (Exception)
			{
				result = new FileSystemAccessRule[]
				{
					Database_Directory.builtInAdmin,
					Database_Directory.builtInLocalSystem
				};
			}
			return result;
		}

		// Token: 0x04003252 RID: 12882
		private const string exchangeOrgAdminName = "Organization Management";

		// Token: 0x04003253 RID: 12883
		private const string exchangeViewOnlyAdminName = "View-Only Organization Management";

		// Token: 0x04003254 RID: 12884
		private static FileSystemAccessRule builtInAdmin = new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null), FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);

		// Token: 0x04003255 RID: 12885
		private static FileSystemAccessRule builtInLocalSystem = new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null), FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);
	}
}
