using System;
using System.Security;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000AB4 RID: 2740
	internal static class Kerberos
	{
		// Token: 0x06003AFA RID: 15098 RVA: 0x00097CA4 File Offset: 0x00095EA4
		public static void FlushTicketCache()
		{
			using (SafeLsaUntrustedHandle safeLsaUntrustedHandle = SafeLsaUntrustedHandle.Create())
			{
				int packageId = safeLsaUntrustedHandle.LookupPackage("Kerberos");
				safeLsaUntrustedHandle.PurgeTicketCache(packageId);
			}
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x00097CE8 File Offset: 0x00095EE8
		public static void AddExtraCredentials(string username, string domain, SecureString password)
		{
			Kerberos.AddExtraCredentials(username, domain, password, Kerberos.LuidSelf);
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x00097CF8 File Offset: 0x00095EF8
		public static void AddExtraCredentials(string username, string domain, SecureString password, LsaNativeMethods.LUID luid)
		{
			using (SafeLsaUntrustedHandle safeLsaUntrustedHandle = SafeLsaUntrustedHandle.Create())
			{
				int packageId = safeLsaUntrustedHandle.LookupPackage("Kerberos");
				safeLsaUntrustedHandle.AddExtraCredentials(packageId, username, domain, password, LsaNativeMethods.KerbRequestCredentialFlags.Add, luid);
			}
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x00097D40 File Offset: 0x00095F40
		public static void RemoveExtraCredentials(string username, string domain)
		{
			Kerberos.RemoveExtraCredentials(username, domain, Kerberos.LuidSelf);
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x00097D50 File Offset: 0x00095F50
		public static void RemoveExtraCredentials(string username, string domain, LsaNativeMethods.LUID luid)
		{
			using (SafeLsaUntrustedHandle safeLsaUntrustedHandle = SafeLsaUntrustedHandle.Create())
			{
				int packageId = safeLsaUntrustedHandle.LookupPackage("Kerberos");
				safeLsaUntrustedHandle.AddExtraCredentials(packageId, username, domain, null, LsaNativeMethods.KerbRequestCredentialFlags.Remove, luid);
			}
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x00097D98 File Offset: 0x00095F98
		public static void ReplaceExtraCredentials(string username, string domain, SecureString password)
		{
			Kerberos.ReplaceExtraCredentials(username, domain, password, Kerberos.LuidSelf);
		}

		// Token: 0x06003B00 RID: 15104 RVA: 0x00097DA8 File Offset: 0x00095FA8
		public static void ReplaceExtraCredentials(string username, string domain, SecureString password, LsaNativeMethods.LUID luid)
		{
			using (SafeLsaUntrustedHandle safeLsaUntrustedHandle = SafeLsaUntrustedHandle.Create())
			{
				int packageId = safeLsaUntrustedHandle.LookupPackage("Kerberos");
				safeLsaUntrustedHandle.AddExtraCredentials(packageId, username, domain, password, LsaNativeMethods.KerbRequestCredentialFlags.Replace, luid);
			}
		}

		// Token: 0x04003383 RID: 13187
		private const string PackageName = "Kerberos";

		// Token: 0x04003384 RID: 13188
		public static readonly LsaNativeMethods.LUID LuidSelf = new LsaNativeMethods.LUID(0, 0);

		// Token: 0x04003385 RID: 13189
		public static readonly LsaNativeMethods.LUID LuidLocalSystem = new LsaNativeMethods.LUID(999, 0);

		// Token: 0x04003386 RID: 13190
		public static readonly LsaNativeMethods.LUID LuidNetworkService = new LsaNativeMethods.LUID(996, 0);
	}
}
