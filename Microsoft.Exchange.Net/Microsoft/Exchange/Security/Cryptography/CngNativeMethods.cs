using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Security.Cryptography
{
	// Token: 0x02000A9C RID: 2716
	[SuppressUnmanagedCodeSecurity]
	[ComVisible(false)]
	internal static class CngNativeMethods
	{
		// Token: 0x06003A82 RID: 14978
		[DllImport("Ncrypt.dll", CharSet = CharSet.Unicode)]
		public static extern int NCryptOpenStorageProvider(out SafeNCryptHandle provider, string providerName, uint reserved);

		// Token: 0x06003A83 RID: 14979
		[DllImport("Ncrypt.dll")]
		public static extern int NCryptFreeObject(IntPtr handle);

		// Token: 0x06003A84 RID: 14980
		[DllImport("Ncrypt.dll", CharSet = CharSet.Unicode)]
		public static extern int NCryptOpenKey(SafeNCryptHandle provider, out SafeNCryptHandle key, string keyName, uint legacyKeySpecifier, CngNativeMethods.KeyOptions options);

		// Token: 0x06003A85 RID: 14981
		[DllImport("Ncrypt.dll", CharSet = CharSet.Unicode)]
		public static extern int NCryptGetProperty(SafeNCryptHandle owner, string property, out uint value, uint valueSize, out uint bytes, CngNativeMethods.PropertyOptions options);

		// Token: 0x06003A86 RID: 14982
		[DllImport("Ncrypt.dll", CharSet = CharSet.Unicode)]
		public static extern int NCryptGetProperty(SafeNCryptHandle owner, string property, [Out] byte[] value, uint valueSize, out uint bytes, CngNativeMethods.PropertyOptions options);

		// Token: 0x06003A87 RID: 14983
		[DllImport("Ncrypt.dll", CharSet = CharSet.Unicode)]
		public static extern int NCryptSetProperty(SafeNCryptHandle owner, string property, [In] byte[] value, uint valueSize, CngNativeMethods.PropertyOptions options);

		// Token: 0x040032DE RID: 13022
		private const string NCRYPT = "Ncrypt.dll";

		// Token: 0x040032DF RID: 13023
		public const string ImplementationTypeProperty = "Impl Type";

		// Token: 0x040032E0 RID: 13024
		public const string LengthProperty = "Length";

		// Token: 0x040032E1 RID: 13025
		public const string SecurityDescriptorProperty = "Security Descr";

		// Token: 0x040032E2 RID: 13026
		public const string ExportPolicyProperty = "Export Policy";

		// Token: 0x02000A9D RID: 2717
		public enum SecurityStatus : uint
		{
			// Token: 0x040032E4 RID: 13028
			BadKeyset = 2148073494U,
			// Token: 0x040032E5 RID: 13029
			SilentContext = 2148073506U
		}

		// Token: 0x02000A9E RID: 2718
		[Flags]
		public enum KeyOptions : uint
		{
			// Token: 0x040032E7 RID: 13031
			MachineKeyset = 32U,
			// Token: 0x040032E8 RID: 13032
			Silent = 64U
		}

		// Token: 0x02000A9F RID: 2719
		[Flags]
		public enum ImplemenationType : uint
		{
			// Token: 0x040032EA RID: 13034
			Hardware = 1U,
			// Token: 0x040032EB RID: 13035
			Software = 2U,
			// Token: 0x040032EC RID: 13036
			Removable = 8U,
			// Token: 0x040032ED RID: 13037
			HardwareRandomNumberGenerator = 16U
		}

		// Token: 0x02000AA0 RID: 2720
		public enum PropertyOptions : uint
		{
			// Token: 0x040032EF RID: 13039
			OwnerSecurityInformation = 1U,
			// Token: 0x040032F0 RID: 13040
			GroupSecurityInformation,
			// Token: 0x040032F1 RID: 13041
			DACLSecurityInformation = 4U,
			// Token: 0x040032F2 RID: 13042
			SACLSecurityInformation = 8U
		}

		// Token: 0x02000AA1 RID: 2721
		[Flags]
		public enum AllowExportPolicy
		{
			// Token: 0x040032F4 RID: 13044
			Exportable = 1,
			// Token: 0x040032F5 RID: 13045
			PlaintextExportable = 2,
			// Token: 0x040032F6 RID: 13046
			Archivable = 4,
			// Token: 0x040032F7 RID: 13047
			PlaintextArchivable = 8
		}
	}
}
