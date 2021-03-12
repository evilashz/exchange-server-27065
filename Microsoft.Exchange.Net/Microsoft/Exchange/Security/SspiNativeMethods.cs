using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C7C RID: 3196
	internal static class SspiNativeMethods
	{
		// Token: 0x060046B4 RID: 18100
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal unsafe static extern SecurityStatus AcceptSecurityContext(ref SspiHandle credentialHandle, void* inContextPtr, SecurityBufferDescriptor inputBuffer, ContextFlags inFlags, Endianness endianness, ref SspiHandle outContextPtr, SecurityBufferDescriptor outputBuffer, ref ContextFlags attributes, out long timeStamp);

		// Token: 0x060046B5 RID: 18101
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "AcquireCredentialsHandleW")]
		internal unsafe static extern SecurityStatus AcquireCredentialsHandle(string principalName, string packageName, CredentialUse usage, void* logonID, ref SchannelCredential credential, void* keyCallback, void* keyArgument, ref SspiHandle handlePtr, out long timeStamp);

		// Token: 0x060046B6 RID: 18102
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "AcquireCredentialsHandleW")]
		internal unsafe static extern SecurityStatus AcquireCredentialsHandle(string principalName, string packageName, CredentialUse usage, void* logonID, ref AuthIdentity credential, void* keyCallback, void* keyArgument, ref SspiHandle handlePtr, out long timeStamp);

		// Token: 0x060046B7 RID: 18103
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "AcquireCredentialsHandleW")]
		internal unsafe static extern SecurityStatus AcquireCredentialsHandle(string principalName, string packageName, CredentialUse usage, void* logonID, IntPtr zero, void* keyCallback, void* keyArgument, ref SspiHandle handlePtr, out long timeStamp);

		// Token: 0x060046B8 RID: 18104
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "InitializeSecurityContextW")]
		internal unsafe static extern SecurityStatus InitializeSecurityContext(ref SspiHandle credentialHandle, void* inContextPtr, string targetName, ContextFlags inFlags, int reservedI, Endianness endianness, SecurityBufferDescriptor inputBuffer, int reservedII, ref SspiHandle outContextPtr, SecurityBufferDescriptor outputBuffer, ref ContextFlags attributes, out long timeStamp);

		// Token: 0x060046B9 RID: 18105
		[DllImport("secur32.dll", ExactSpelling = true)]
		internal static extern SecurityStatus FreeContextBuffer(IntPtr contextBuffer);

		// Token: 0x060046BA RID: 18106
		[DllImport("secur32.dll", ExactSpelling = true)]
		internal static extern SecurityStatus FreeCredentialsHandle(ref SspiHandle handlePtr);

		// Token: 0x060046BB RID: 18107
		[DllImport("secur32.dll", ExactSpelling = true)]
		internal static extern SecurityStatus DeleteSecurityContext(ref SspiHandle handlePtr);

		// Token: 0x060046BC RID: 18108
		[DllImport("secur32.dll", EntryPoint = "QueryContextAttributesW")]
		internal static extern SecurityStatus QueryContextAttributes(ref SspiHandle contextHandle, ContextAttribute attribute, byte[] buffer);

		// Token: 0x060046BD RID: 18109
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "QuerySecurityPackageInfoW")]
		internal static extern SecurityStatus QuerySecurityPackageInfo(string packageName, out SafeContextBuffer secPkgInfo);

		// Token: 0x060046BE RID: 18110
		[DllImport("secur32.dll", ExactSpelling = true)]
		internal static extern SecurityStatus DecryptMessage(ref SspiHandle handlePtr, SecurityBufferDescriptor inOut, uint sequenceNumber, out QualityOfProtection qualityOfProtection);

		// Token: 0x060046BF RID: 18111
		[DllImport("secur32.dll", ExactSpelling = true)]
		internal static extern SecurityStatus EncryptMessage(ref SspiHandle handlePtr, QualityOfProtection qualityOfProtection, SecurityBufferDescriptor inOut, uint sequenceNumber);

		// Token: 0x060046C0 RID: 18112
		[DllImport("crypt32.dll", ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CertFreeCertificateContext(IntPtr certContext);

		// Token: 0x060046C1 RID: 18113
		[DllImport("secur32.dll")]
		internal static extern SecurityStatus QuerySecurityContextToken(ref SspiHandle contextHandle, out SafeTokenHandle token);

		// Token: 0x060046C2 RID: 18114
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "QueryCredentialsAttributesW")]
		internal static extern SecurityStatus QueryCredentialsAttributes(ref SspiHandle handlePtr, CredentialsAttribute credentialAttribute, byte[] buffer);

		// Token: 0x060046C3 RID: 18115
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseHandle(IntPtr handle);

		// Token: 0x060046C4 RID: 18116
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "LogonUserW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool LogonUser(string username, string domain, IntPtr password, LogonType logonType, LogonProvider logonProvider, ref SafeTokenHandle token);

		// Token: 0x060046C5 RID: 18117
		[DllImport("advapi32.dll", EntryPoint = "LogonUserA", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool LogonUser(byte[] username, byte[] domain, byte[] password, LogonType logonType, LogonProvider logonProvider, ref SafeTokenHandle token);

		// Token: 0x04003BAD RID: 15277
		public const string UnifiedProviderName = "Microsoft Unified Security Protocol Provider";

		// Token: 0x04003BAE RID: 15278
		private const string SECUR32 = "secur32.dll";

		// Token: 0x04003BAF RID: 15279
		private const string CRYPT32 = "crypt32.dll";

		// Token: 0x04003BB0 RID: 15280
		private const string ADVAPI32 = "advapi32.dll";

		// Token: 0x04003BB1 RID: 15281
		private const string KERNEL32 = "kernel32.dll";
	}
}
