using System;
using System.Security.AccessControl;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000E1 RID: 225
	internal static class OleConverterRegistry
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x0001BEEC File Offset: 0x0001A0EC
		private static byte[] GetBinaryDescriptor(string sddlDescriptor)
		{
			RawSecurityDescriptor rawSecurityDescriptor = new RawSecurityDescriptor(sddlDescriptor);
			byte[] array = new byte[rawSecurityDescriptor.BinaryLength];
			rawSecurityDescriptor.GetBinaryForm(array, 0);
			return array;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001BF18 File Offset: 0x0001A118
		private static void SetPermissionsForAppId(string appId)
		{
			string subkey = "AppID\\" + appId;
			using (RegistryKey registryKey = Registry.ClassesRoot.CreateSubKey(subkey))
			{
				byte[] binaryDescriptor = OleConverterRegistry.GetBinaryDescriptor("O:BAG:BAD:(A;;CCDC;;;BA)(A;;CCDC;;;SY)(A;;CCDC;;;NS)(A;;CCDC;;;LS)(A;;CCDC;;;IU)");
				registryKey.SetValue("AccessPermission", binaryDescriptor);
				byte[] binaryDescriptor2 = OleConverterRegistry.GetBinaryDescriptor("O:BAG:BAD:(A;;CCDCSW;;;BA)(A;;CCDCSW;;;SY)(A;;CCDCSW;;;NS)(A;;CCDCSW;;;LS)(A;;CCDCSW;;;IU)");
				registryKey.SetValue("LaunchPermission", binaryDescriptor2);
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001BF88 File Offset: 0x0001A188
		private static void RemovePermissionsForAppId(string appId)
		{
			string name = "AppID\\" + appId;
			using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(name, true))
			{
				if (registryKey != null)
				{
					registryKey.DeleteValue("AccessPermission", false);
					registryKey.DeleteValue("LaunchPermission", false);
				}
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001BFE8 File Offset: 0x0001A1E8
		public static void RegisterOleConverter()
		{
			OleConverterRegistry.SetPermissionsForAppId("{13147291-05DE-4577-B1AF-E0BB444B3B27}");
			OleConverterRegistry.SetPermissionsForAppId("{131473D0-EC52-4001-A295-E2DD73A7B115}");
			ComRunAsPwdUtil.SetRunAsPassword("{131473D0-EC52-4001-A295-E2DD73A7B115}", string.Empty);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001C00D File Offset: 0x0001A20D
		public static void UnregisterOleConverter()
		{
			ComRunAsPwdUtil.SetRunAsPassword("{131473D0-EC52-4001-A295-E2DD73A7B115}", null);
			OleConverterRegistry.RemovePermissionsForAppId("{13147291-05DE-4577-B1AF-E0BB444B3B27}");
			OleConverterRegistry.RemovePermissionsForAppId("{131473D0-EC52-4001-A295-E2DD73A7B115}");
		}

		// Token: 0x04000312 RID: 786
		private const string AccessPermission = "O:BAG:BAD:(A;;CCDC;;;BA)(A;;CCDC;;;SY)(A;;CCDC;;;NS)(A;;CCDC;;;LS)(A;;CCDC;;;IU)";

		// Token: 0x04000313 RID: 787
		private const string LaunchPermission = "O:BAG:BAD:(A;;CCDCSW;;;BA)(A;;CCDCSW;;;SY)(A;;CCDCSW;;;NS)(A;;CCDCSW;;;LS)(A;;CCDCSW;;;IU)";

		// Token: 0x04000314 RID: 788
		private const string OleConverterAppIdLocalService = "{131473D0-EC52-4001-A295-E2DD73A7B115}";

		// Token: 0x04000315 RID: 789
		private const string OleConverterAppIdUser = "{13147291-05DE-4577-B1AF-E0BB444B3B27}";
	}
}
