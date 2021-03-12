using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Exchange.Clients.Owa.Core.Internal
{
	// Token: 0x0200023D RID: 573
	internal static class SafeNativeMethods
	{
		// Token: 0x06001349 RID: 4937
		[DllImport("netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern uint NetUserChangePassword(string domainname, string username, IntPtr oldpassword, IntPtr newpassword);

		// Token: 0x0600134A RID: 4938
		[DllImport("msi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int MsiOpenDatabase(string szDatabasePath, int szPersist, out SafeMsiHandle hDatabase);

		// Token: 0x0600134B RID: 4939
		[DllImport("msi.dll", SetLastError = true)]
		public static extern int MsiDatabaseOpenView(SafeMsiHandle hDatabase, string szQuery, out SafeMsiHandle hView);

		// Token: 0x0600134C RID: 4940
		[DllImport("msi.dll", SetLastError = true)]
		public static extern int MsiViewExecute(SafeMsiHandle hView, SafeMsiHandle hRecord);

		// Token: 0x0600134D RID: 4941
		[DllImport("msi.dll", SetLastError = true)]
		public static extern int MsiViewFetch(SafeMsiHandle hView, out SafeMsiHandle hRecord);

		// Token: 0x0600134E RID: 4942
		[DllImport("msi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int MsiRecordGetString(SafeMsiHandle hRecord, int iField, StringBuilder szValueBuf, ref int pcchValueBuf);

		// Token: 0x0600134F RID: 4943
		[DllImport("msi.dll", ExactSpelling = true)]
		public static extern int MsiCloseHandle(IntPtr hAny);

		// Token: 0x06001350 RID: 4944
		[DllImport("msi.dll", ExactSpelling = true)]
		public static extern SafeMsiHandle MsiGetLastErrorRecord();

		// Token: 0x06001351 RID: 4945
		[DllImport("msi.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int MsiFormatRecord(SafeMsiHandle hInstall, SafeMsiHandle hRecord, StringBuilder szValueBuf, ref int pcchValueBuf);

		// Token: 0x04000D3F RID: 3391
		private const string MSIDLL = "msi.dll";
	}
}
