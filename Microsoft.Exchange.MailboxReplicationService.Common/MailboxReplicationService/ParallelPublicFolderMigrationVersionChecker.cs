using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200017F RID: 383
	internal static class ParallelPublicFolderMigrationVersionChecker
	{
		// Token: 0x06000E77 RID: 3703 RVA: 0x00020FFF File Offset: 0x0001F1FF
		public static void ThrowIfMinimumRequiredVersionNotInstalled(int sourceServerVersion)
		{
			ParallelPublicFolderMigrationVersionChecker.ThrowIfMinimumRequiredVersionNotInstalled(new ServerVersion(sourceServerVersion));
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0002100C File Offset: 0x0001F20C
		public static void ThrowIfMinimumRequiredVersionNotInstalled(ServerVersion sourceServerVersion)
		{
			LocalizedString? localizedString = ParallelPublicFolderMigrationVersionChecker.CheckForMinimumRequiredVersion(sourceServerVersion);
			if (localizedString != null)
			{
				throw new PublicFolderMigrationNotSupportedFromVersionException(localizedString.Value);
			}
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00021036 File Offset: 0x0001F236
		public static LocalizedString? CheckForMinimumRequiredVersion(int sourceServerVersion)
		{
			return ParallelPublicFolderMigrationVersionChecker.CheckForMinimumRequiredVersion(new ServerVersion(sourceServerVersion));
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00021044 File Offset: 0x0001F244
		public static LocalizedString? CheckForMinimumRequiredVersion(ServerVersion sourceServerVersion)
		{
			LocalizedString? result = null;
			int num = sourceServerVersion.ToInt();
			if (sourceServerVersion.Major < 8)
			{
				result = new LocalizedString?(MrsStrings.PublicFolderMigrationNotSupportedFromExchange2003OrEarlier(sourceServerVersion.Major, sourceServerVersion.Minor, sourceServerVersion.Build, sourceServerVersion.Revision));
			}
			else if (sourceServerVersion.Major == 8 && num < ParallelPublicFolderMigrationVersionChecker.E12MinVersionNumber)
			{
				result = new LocalizedString?(MrsStrings.PublicFolderMigrationNotSupportedFromCurrentExchange2007Version(sourceServerVersion.Major, sourceServerVersion.Minor, sourceServerVersion.Build, sourceServerVersion.Revision));
			}
			else if (sourceServerVersion.Major == 14 && num < ParallelPublicFolderMigrationVersionChecker.E14MinVersionNumber)
			{
				result = new LocalizedString?(MrsStrings.PublicFolderMigrationNotSupportedFromCurrentExchange2010Version(sourceServerVersion.Major, sourceServerVersion.Minor, sourceServerVersion.Build, sourceServerVersion.Revision));
			}
			return result;
		}

		// Token: 0x04000822 RID: 2082
		private static readonly int E12MinVersionNumber = new ServerVersion(8, 3, 385, 0).ToInt();

		// Token: 0x04000823 RID: 2083
		private static readonly int E14MinVersionNumber = new ServerVersion(14, 3, 215, 0).ToInt();
	}
}
