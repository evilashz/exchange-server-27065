using System;
using System.DirectoryServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x020002F1 RID: 753
	internal sealed class AdsUtils
	{
		// Token: 0x060019D4 RID: 6612 RVA: 0x00072FEC File Offset: 0x000711EC
		public static bool GetAdsServiceExists(int ldapPort)
		{
			bool result = false;
			try
			{
				result = DirectoryEntry.Exists(AdsUtils.GetRootPath(ldapPort));
			}
			catch (COMException ex)
			{
				if (-2147016646 == ex.ErrorCode)
				{
					return false;
				}
				throw;
			}
			return result;
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00073030 File Offset: 0x00071230
		public static DirectoryEntry GetRootDirectoryEntry(int ldapPort)
		{
			return new DirectoryEntry
			{
				Path = AdsUtils.GetRootPath(ldapPort)
			};
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00073050 File Offset: 0x00071250
		private static string GetRootPath(int ldapPort)
		{
			return string.Format("{0}:{1}/{2}", "LDAP://localhost", ldapPort, "RootDse");
		}

		// Token: 0x04000B31 RID: 2865
		public const string HostSpecificAdsPathRoot = "LDAP://localhost";

		// Token: 0x04000B32 RID: 2866
		public const string DseAdsPath = "RootDse";

		// Token: 0x04000B33 RID: 2867
		public const string CommonNamePropertyName = "cn";

		// Token: 0x04000B34 RID: 2868
		public const string RootDseSchemaNamingContextPropertyName = "SchemaNamingContext";

		// Token: 0x04000B35 RID: 2869
		public const string RootDseConfigNamingContextPropertyName = "ConfigurationNamingContext";

		// Token: 0x04000B36 RID: 2870
		public const string PredefSchemaNamingContextMacro = "#schemaNamingContext";

		// Token: 0x04000B37 RID: 2871
		public const string PredefConfigNamingContextMacro = "#configurationNamingContext";
	}
}
