using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.DirectoryServices;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x0200000B RID: 11
	internal class AdminRpcPermissionChecks
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000256D File Offset: 0x0000076D
		public static ErrorCode EcDefaultCheck(Context context, DatabaseInfo databaseInfo)
		{
			return AdminRpcPermissionChecks.defaultChecker.Value.EcCheckPermissions(context, databaseInfo);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002580 File Offset: 0x00000780
		public static ErrorCode EcCheckConstrainedDelegationRights(Context context, DatabaseInfo databaseInfo)
		{
			return AdminRpcPermissionChecks.constrainedDelegationChecker.Value.EcCheckPermissions(context, databaseInfo);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002593 File Offset: 0x00000793
		internal static IDisposable SetDefaultCheckTestHook(AdminRpcPermissionChecks.IChecker testChecker)
		{
			return AdminRpcPermissionChecks.defaultChecker.SetTestHook(testChecker);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025A0 File Offset: 0x000007A0
		internal static IDisposable SetConstrainedDelegationCheckTestHook(AdminRpcPermissionChecks.IChecker testChecker)
		{
			return AdminRpcPermissionChecks.constrainedDelegationChecker.SetTestHook(testChecker);
		}

		// Token: 0x04000045 RID: 69
		private static Hookable<AdminRpcPermissionChecks.IChecker> defaultChecker = Hookable<AdminRpcPermissionChecks.IChecker>.Create(true, new AdminRpcPermissionChecks.DefaultChecker());

		// Token: 0x04000046 RID: 70
		private static Hookable<AdminRpcPermissionChecks.IChecker> constrainedDelegationChecker = Hookable<AdminRpcPermissionChecks.IChecker>.Create(true, new AdminRpcPermissionChecks.ConstrainedDelegationChecker());

		// Token: 0x0200000C RID: 12
		public interface IChecker
		{
			// Token: 0x06000028 RID: 40
			ErrorCode EcCheckPermissions(Context context, DatabaseInfo databaseInfo);
		}

		// Token: 0x0200000D RID: 13
		private class DefaultChecker : AdminRpcPermissionChecks.IChecker
		{
			// Token: 0x06000029 RID: 41 RVA: 0x000025D8 File Offset: 0x000007D8
			public ErrorCode EcCheckPermissions(Context context, DatabaseInfo databaseInfo)
			{
				ErrorCode result = ErrorCode.NoError;
				if (!context.SecurityContext.IsAuthenticated)
				{
					result = ErrorCode.CreateNoAccess((LID)50813U);
				}
				else if (databaseInfo != null && !SecurityHelper.CheckAdministrativeRights(context.SecurityContext, databaseInfo.NTSecurityDescriptor))
				{
					result = ErrorCode.CreateNoAccess((LID)34991U);
				}
				return result;
			}
		}

		// Token: 0x0200000E RID: 14
		private class ConstrainedDelegationChecker : AdminRpcPermissionChecks.IChecker
		{
			// Token: 0x0600002B RID: 43 RVA: 0x0000263C File Offset: 0x0000083C
			public ErrorCode EcCheckPermissions(Context context, DatabaseInfo databaseInfo)
			{
				ErrorCode result = ErrorCode.NoError;
				if (!SecurityHelper.CheckConstrainedDelegationPrivilege(context.SecurityContext, Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetServerInfo(context).NTSecurityDescriptor))
				{
					result = ErrorCode.CreateNoAccess((LID)51375U);
				}
				return result;
			}
		}
	}
}
