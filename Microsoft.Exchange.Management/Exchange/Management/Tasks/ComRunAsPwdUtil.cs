using System;
using System.ComponentModel;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000E0 RID: 224
	internal static class ComRunAsPwdUtil
	{
		// Token: 0x0600069D RID: 1693 RVA: 0x0001BE10 File Offset: 0x0001A010
		internal static void SetRunAsPassword(string appId, string password)
		{
			LsaNativeMethods.LsaObjectAttributes objectAttributes = new LsaNativeMethods.LsaObjectAttributes();
			SafeLsaPolicyHandle safeLsaPolicyHandle;
			int num = LsaNativeMethods.LsaOpenPolicy(null, objectAttributes, LsaNativeMethods.PolicyAccess.CreateSecret, out safeLsaPolicyHandle);
			num = LsaNativeMethods.LsaNtStatusToWinError(num);
			if (num != 0)
			{
				throw new Win32Exception(num, "LsaOpenPolicy() failed");
			}
			using (safeLsaPolicyHandle)
			{
				appId = "SCM:" + appId;
				using (LsaNativeMethods.SafeLsaUnicodeString safeLsaUnicodeString = new LsaNativeMethods.SafeLsaUnicodeString(appId))
				{
					if (password != null)
					{
						using (LsaNativeMethods.SafeLsaUnicodeString safeLsaUnicodeString2 = new LsaNativeMethods.SafeLsaUnicodeString(password))
						{
							num = LsaNativeMethods.LsaStorePrivateData(safeLsaPolicyHandle, safeLsaUnicodeString, safeLsaUnicodeString2);
							goto IL_75;
						}
					}
					num = LsaNativeMethods.LsaStorePrivateData(safeLsaPolicyHandle, safeLsaUnicodeString, null);
					if (num == -1073741772)
					{
						return;
					}
					IL_75:;
				}
				num = LsaNativeMethods.LsaNtStatusToWinError(num);
				if (num != 0)
				{
					throw new Win32Exception(num, "LsaStorePrivateData() failed");
				}
			}
		}

		// Token: 0x04000311 RID: 785
		private const uint StatusObjectNameNotFound = 3221225524U;
	}
}
