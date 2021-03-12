using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000C8 RID: 200
	internal static class NativeMethods
	{
		// Token: 0x06000776 RID: 1910
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr OpenSCManager(string machineName, string databaseName, ServiceControlManagerAccessFlags desiredAccess);

		// Token: 0x06000777 RID: 1911
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr OpenService(IntPtr serviceControllerManager, string serviceName, ServiceAccessFlags desiredAccess);

		// Token: 0x06000778 RID: 1912
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool CloseServiceHandle(IntPtr serviceControlHandle);

		// Token: 0x06000779 RID: 1913
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool ChangeServiceConfig2(IntPtr serviceController, ServiceConfigInfoLevels infoLevel, ref ServiceFailureActions failureActions);

		// Token: 0x0600077A RID: 1914
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool ChangeServiceConfig2(IntPtr serviceController, ServiceConfigInfoLevels infoLevel, ref ServiceFailureActionsFlag failureActionsFlag);

		// Token: 0x0600077B RID: 1915
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool ChangeServiceConfig2(IntPtr serviceController, ServiceConfigInfoLevels infoLevel, ref ServiceSidActions serviceSidActions);

		// Token: 0x0600077C RID: 1916
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool ChangeServiceConfig(IntPtr serviceController, uint serviceType, uint startType, uint errorControl, string binaryPathName, string loadOrderGroup, string tagId, string dependencies, string serviceStartName, string password, string displayName);

		// Token: 0x0600077D RID: 1917
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int QueryServiceStatus(SafeHandle handle, ref SERVICE_STATUS ss);

		// Token: 0x0600077E RID: 1918
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool QueryServiceObjectSecurity(IntPtr serviceHandle, SecurityInfos secInfo, IntPtr securityDescriptorUnmanagedBuffer, int bufSize, out int bufSizeNeeded);

		// Token: 0x0600077F RID: 1919
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool SetServiceObjectSecurity(IntPtr hService, SecurityInfos dwSecurityInformation, IntPtr lpSecurityDescriptor);

		// Token: 0x06000780 RID: 1920
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern uint QueryServiceStatusEx(IntPtr hService, ServiceQueryStatus infoLevel, ref SERVICE_STATUS_EX lpBuffer, uint cbBufSize, out uint pcbBytesNeeded);

		// Token: 0x06000781 RID: 1921
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool QueryServiceConfig(IntPtr serviceHandle, IntPtr buffer, uint cbBufSize, out uint cbBytesNeeded);

		// Token: 0x06000782 RID: 1922
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern uint GetShortPathName(string lpszLongPath, StringBuilder lpszShortPath, uint cchBuffer);

		// Token: 0x06000783 RID: 1923
		[DllImport("netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern uint NetUserChangePassword(string domainname, string username, IntPtr oldpassword, IntPtr newpassword);

		// Token: 0x04000204 RID: 516
		internal const uint SERVICE_NO_CHANGE = 4294967295U;
	}
}
