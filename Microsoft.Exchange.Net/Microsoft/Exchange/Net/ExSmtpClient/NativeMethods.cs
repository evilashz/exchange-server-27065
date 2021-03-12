using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x020006E5 RID: 1765
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class NativeMethods
	{
		// Token: 0x0600217E RID: 8574
		[DllImport("secur32.dll", CharSet = CharSet.Unicode)]
		internal static extern int QuerySecurityPackageInfo(string pszPackageName, ref IntPtr ppPackageInfo);

		// Token: 0x0600217F RID: 8575
		[DllImport("secur32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, EntryPoint = "AcquireCredentialsHandleA", ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)]
		internal static extern int AcquireCredentialsHandle([In] string pszPrincipal, [In] string pszPackage, [In] int fCredentialUse, [In] IntPtr pvLogonID, [In] ref NativeMethods.AuthData pAuthData, [In] IntPtr pGetKeyFn, [In] IntPtr pvGetKeyArgument, ref NativeMethods.CredHandle phCredential, out long ptsExpiry);

		// Token: 0x06002180 RID: 8576
		[DllImport("secur32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, EntryPoint = "AcquireCredentialsHandleA", ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)]
		internal static extern int AcquireCredentialsHandle([In] string pszPrincipal, [In] string pszPackage, [In] int fCredentialUse, [In] IntPtr pvLogonID, [In] IntPtr pAuthData, [In] IntPtr pGetKeyFn, [In] IntPtr pvGetKeyArgument, ref NativeMethods.CredHandle phCredential, out long ptsExpiry);

		// Token: 0x06002181 RID: 8577
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "AcquireCredentialsHandleW")]
		internal static extern int AcquireCredentialsHandleForSchannel([In] string pszPrincipal, [In] string pszPackage, [In] int fCredentialUse, [In] IntPtr pvLogonID, [In] ref NativeMethods.AuthDataForSchannel pAuthData, [In] IntPtr pGetKeyFn, [In] IntPtr pvGetKeyArgument, ref NativeMethods.CredHandle phCredential, out long ptsExpiry);

		// Token: 0x06002182 RID: 8578
		[DllImport("secur32.dll", CharSet = CharSet.Unicode)]
		internal static extern int FreeCredentialsHandle(ref NativeMethods.CredHandle phCredential);

		// Token: 0x06002183 RID: 8579
		[DllImport("secur32.dll", CharSet = CharSet.Unicode)]
		internal static extern int FreeContextBuffer(IntPtr pvContextBuffer);

		// Token: 0x06002184 RID: 8580
		[DllImport("secur32.dll", CharSet = CharSet.Ansi)]
		internal static extern int InitializeSecurityContext(ref NativeMethods.CredHandle phCredential, [In] IntPtr phContext, [In] string pszTargetName, [In] int fContextReq, [In] int Reserved1, [In] NativeMethods.Endianness TargetDataRep, [In] IntPtr pInput, [In] int Reserved2, ref NativeMethods.CredHandle phNewContext, [In] [Out] NativeMethods.SecBufferDesc pOutput, [In] [Out] ref int pfContextAttr, out long ptsExpiry);

		// Token: 0x06002185 RID: 8581
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "QueryContextAttributesW", SetLastError = true)]
		internal static extern int QueryContextAttributes(ref NativeMethods.CredHandle phContext, NativeMethods.ContextAttribute ulAttribute, [Out] byte[] buffer);

		// Token: 0x06002186 RID: 8582
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "QueryContextAttributesW", SetLastError = true)]
		internal static extern int QueryContextAttributes(ref NativeMethods.CredHandle phContext, NativeMethods.ContextAttribute ulAttribute, out SafeCertContextHandle certContext);

		// Token: 0x06002187 RID: 8583
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "QueryContextAttributesW", SetLastError = true)]
		internal static extern int QueryContextAttributes(ref NativeMethods.CredHandle phContext, NativeMethods.ContextAttribute ulAttribute, [In] [Out] ref NativeMethods.SessionKey sessionkey);

		// Token: 0x06002188 RID: 8584
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "QueryContextAttributesW", SetLastError = true)]
		internal static extern int QueryContextAttributes(ref NativeMethods.CredHandle phContext, NativeMethods.ContextAttribute ulAttribute, [In] [Out] ref NativeMethods.EapKeyBlock keyBlock);

		// Token: 0x06002189 RID: 8585
		[DllImport("secur32.dll", CharSet = CharSet.Ansi, EntryPoint = "InitializeSecurityContext")]
		internal static extern int InitializeSecurityContext_SecondSspiBlob(ref NativeMethods.CredHandle phCredential, [In] ref NativeMethods.CredHandle phContext, [In] string pszTargetName, [In] int fContextReq, [In] int Reserved1, [In] NativeMethods.Endianness TargetDataRep, [In] NativeMethods.SecBufferDesc pInput, [In] int Reserved2, ref NativeMethods.CredHandle phNewContext, [In] [Out] NativeMethods.SecBufferDesc pOutput, [In] [Out] ref int pfContextAttr, out long ptsExpiry);

		// Token: 0x0600218A RID: 8586
		[DllImport("secur32.dll", CharSet = CharSet.Ansi, EntryPoint = "InitializeSecurityContext")]
		internal static extern int InitializeSecurityContext_NextSslBlob(ref NativeMethods.CredHandle phCredential, [In] ref NativeMethods.CredHandle phContext, [In] string pszTargetName, [In] int fContextReq, [In] int Reserved1, [In] NativeMethods.Endianness TargetDataRep, [In] NativeMethods.SecBufferDesc pInput, [In] int Reserved2, ref NativeMethods.CredHandle phNewContext, [In] [Out] NativeMethods.SecBufferDesc pOutput, [In] [Out] ref int pfContextAttr, out long ptsExpiry);

		// Token: 0x0600218B RID: 8587
		[DllImport("secur32.dll", CharSet = CharSet.Unicode)]
		internal static extern int CompleteAuthToken(NativeMethods.CredHandle phContext, NativeMethods.SecBufferDesc pToken);

		// Token: 0x0600218C RID: 8588
		[DllImport("secur32.dll", CharSet = CharSet.Unicode)]
		internal static extern int DeleteSecurityContext(ref NativeMethods.CredHandle phContext);

		// Token: 0x0600218D RID: 8589
		[DllImport("secur32.dll", CharSet = CharSet.Unicode)]
		internal static extern int EncryptMessage([In] ref NativeMethods.CredHandle phContext, [In] uint fQOP, [In] [Out] NativeMethods.SecBufferDesc pMessage, [In] uint MessageSeqNo);

		// Token: 0x0600218E RID: 8590
		[DllImport("secur32.dll", CharSet = CharSet.Unicode)]
		internal static extern int DecryptMessage([In] ref NativeMethods.CredHandle phContext, [In] [Out] NativeMethods.SecBufferDesc pMessage, [In] uint MessageSeqNo, out uint pfQOP);

		// Token: 0x0600218F RID: 8591
		[DllImport("secur32.dll", CharSet = CharSet.Unicode)]
		internal static extern int AcceptSecurityContext(ref NativeMethods.CredHandle phCredential, [In] IntPtr phContext, [In] NativeMethods.SecBufferDesc pInput, [In] NativeMethods.ContextFlags fContextReq, [In] NativeMethods.Endianness TargetDataRep, ref NativeMethods.CredHandle phNewContext, [In] [Out] NativeMethods.SecBufferDesc pOutput, [In] [Out] ref NativeMethods.ContextFlags pfContextAttr, out long ptsTimeStamp);

		// Token: 0x06002190 RID: 8592
		[DllImport("secur32.dll", CharSet = CharSet.Unicode)]
		internal static extern int AcceptSecurityContext(ref NativeMethods.CredHandle phCredential, [In] ref NativeMethods.CredHandle phContext, [In] NativeMethods.SecBufferDesc pInput, [In] NativeMethods.ContextFlags fContextReq, [In] NativeMethods.Endianness TargetDataRep, ref NativeMethods.CredHandle phNewContext, [In] [Out] NativeMethods.SecBufferDesc pOutput, [In] [Out] ref NativeMethods.ContextFlags pfContextAttr, out long ptsTimeStamp);

		// Token: 0x06002191 RID: 8593
		[DllImport("Crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int CertFreeCertificateContext(IntPtr pcertContext);

		// Token: 0x04001F8B RID: 8075
		internal const int SECPKG_CRED_OUTBOUND = 2;

		// Token: 0x04001F8C RID: 8076
		internal const int SECPKG_CRED_INBOUND = 1;

		// Token: 0x04001F8D RID: 8077
		internal const int SEC_WINNT_AUTH_IDENTITY_UNICODE = 2;

		// Token: 0x04001F8E RID: 8078
		internal const int SEC_I_CONTINUE_NEEDED = 590610;

		// Token: 0x04001F8F RID: 8079
		internal const int SEC_E_WRONG_PRINCIPAL = 590626;

		// Token: 0x04001F90 RID: 8080
		internal const string SEC_STATUS_LOGON_DENIED = "0x8009030c";

		// Token: 0x04001F91 RID: 8081
		internal const string SEC_E_INCOMPLETE_MESSAGE = "0x80090318";

		// Token: 0x04001F92 RID: 8082
		internal const int SECBUFFER_VERSION = 0;

		// Token: 0x04001F93 RID: 8083
		internal const int SECBUFFER_EMPTY = 0;

		// Token: 0x04001F94 RID: 8084
		internal const int SECBUFFER_DATA = 1;

		// Token: 0x04001F95 RID: 8085
		internal const int SECBUFFER_TOKEN = 2;

		// Token: 0x04001F96 RID: 8086
		internal const int SECBUFFER_PKG_PARAMS = 3;

		// Token: 0x04001F97 RID: 8087
		internal const int SECBUFFER_MISSING = 4;

		// Token: 0x04001F98 RID: 8088
		internal const int SECBUFFER_EXTRA = 5;

		// Token: 0x04001F99 RID: 8089
		internal const int SECBUFFER_STREAM_TRAILER = 6;

		// Token: 0x04001F9A RID: 8090
		internal const int SECBUFFER_STREAM_HEADER = 7;

		// Token: 0x04001F9B RID: 8091
		internal const int SEC_E_OK = 0;

		// Token: 0x04001F9C RID: 8092
		internal const string UNISP_NAME = "Microsoft Unified Security Protocol Provider";

		// Token: 0x020006E6 RID: 1766
		[Flags]
		internal enum ContextFlags
		{
			// Token: 0x04001F9E RID: 8094
			Zero = 0,
			// Token: 0x04001F9F RID: 8095
			Delegate = 1,
			// Token: 0x04001FA0 RID: 8096
			MutualAuth = 2,
			// Token: 0x04001FA1 RID: 8097
			ReplayDetect = 4,
			// Token: 0x04001FA2 RID: 8098
			SequenceDetect = 8,
			// Token: 0x04001FA3 RID: 8099
			Confidentiality = 16,
			// Token: 0x04001FA4 RID: 8100
			UseSessionKey = 32,
			// Token: 0x04001FA5 RID: 8101
			AllocateMemory = 256,
			// Token: 0x04001FA6 RID: 8102
			Connection = 2048,
			// Token: 0x04001FA7 RID: 8103
			InitExtendedError = 16384,
			// Token: 0x04001FA8 RID: 8104
			AcceptExtendedError = 32768,
			// Token: 0x04001FA9 RID: 8105
			InitStream = 32768,
			// Token: 0x04001FAA RID: 8106
			AcceptStream = 65536,
			// Token: 0x04001FAB RID: 8107
			InitIntegrity = 65536,
			// Token: 0x04001FAC RID: 8108
			AcceptIntegrity = 131072,
			// Token: 0x04001FAD RID: 8109
			InitManualCredValidation = 524288,
			// Token: 0x04001FAE RID: 8110
			InitUseSuppliedCreds = 128,
			// Token: 0x04001FAF RID: 8111
			InitIdentify = 131072,
			// Token: 0x04001FB0 RID: 8112
			AcceptIdentify = 524288,
			// Token: 0x04001FB1 RID: 8113
			InitHttp = 268435456,
			// Token: 0x04001FB2 RID: 8114
			AcceptHttp = 268435456
		}

		// Token: 0x020006E7 RID: 1767
		internal enum ContextAttribute
		{
			// Token: 0x04001FB4 RID: 8116
			Sizes,
			// Token: 0x04001FB5 RID: 8117
			Names,
			// Token: 0x04001FB6 RID: 8118
			Lifespan,
			// Token: 0x04001FB7 RID: 8119
			DceInfo,
			// Token: 0x04001FB8 RID: 8120
			StreamSizes,
			// Token: 0x04001FB9 RID: 8121
			KeyInfo,
			// Token: 0x04001FBA RID: 8122
			Authority,
			// Token: 0x04001FBB RID: 8123
			SECPKG_ATTR_PROTO_INFO,
			// Token: 0x04001FBC RID: 8124
			SECPKG_ATTR_PASSWORD_EXPIRY,
			// Token: 0x04001FBD RID: 8125
			SECPKG_ATTR_SESSION_KEY,
			// Token: 0x04001FBE RID: 8126
			PackageInfo,
			// Token: 0x04001FBF RID: 8127
			SECPKG_ATTR_USER_FLAGS,
			// Token: 0x04001FC0 RID: 8128
			NegotiationInfo,
			// Token: 0x04001FC1 RID: 8129
			SECPKG_ATTR_NATIVE_NAMES,
			// Token: 0x04001FC2 RID: 8130
			SECPKG_ATTR_FLAGS,
			// Token: 0x04001FC3 RID: 8131
			SECPKG_ATTR_USE_VALIDATED,
			// Token: 0x04001FC4 RID: 8132
			SECPKG_ATTR_CREDENTIAL_NAME,
			// Token: 0x04001FC5 RID: 8133
			SECPKG_ATTR_TARGET_INFORMATION,
			// Token: 0x04001FC6 RID: 8134
			SECPKG_ATTR_ACCESS_TOKEN,
			// Token: 0x04001FC7 RID: 8135
			SECPKG_ATTR_TARGET,
			// Token: 0x04001FC8 RID: 8136
			SECPKG_ATTR_AUTHENTICATION_ID,
			// Token: 0x04001FC9 RID: 8137
			RemoteCertificate = 83,
			// Token: 0x04001FCA RID: 8138
			LocalCertificate,
			// Token: 0x04001FCB RID: 8139
			RootStore,
			// Token: 0x04001FCC RID: 8140
			IssuerListInfoEx = 89,
			// Token: 0x04001FCD RID: 8141
			ConnectionInfo,
			// Token: 0x04001FCE RID: 8142
			EapKeyBlock
		}

		// Token: 0x020006E8 RID: 1768
		internal enum Endianness
		{
			// Token: 0x04001FD0 RID: 8144
			Network,
			// Token: 0x04001FD1 RID: 8145
			Native = 16
		}

		// Token: 0x020006E9 RID: 1769
		public struct SEC_STATUS
		{
			// Token: 0x06002193 RID: 8595 RVA: 0x0004222F File Offset: 0x0004042F
			public SEC_STATUS(int intValue)
			{
				this.intValue = intValue;
			}

			// Token: 0x06002194 RID: 8596 RVA: 0x00042238 File Offset: 0x00040438
			public static implicit operator NativeMethods.SEC_STATUS(int intValue)
			{
				return new NativeMethods.SEC_STATUS(intValue);
			}

			// Token: 0x06002195 RID: 8597 RVA: 0x00042240 File Offset: 0x00040440
			public static implicit operator int(NativeMethods.SEC_STATUS secStatus)
			{
				return secStatus.intValue;
			}

			// Token: 0x06002196 RID: 8598 RVA: 0x00042249 File Offset: 0x00040449
			public static implicit operator string(NativeMethods.SEC_STATUS secStatus)
			{
				return "0x" + secStatus.intValue.ToString("x", CultureInfo.InvariantCulture);
			}

			// Token: 0x06002197 RID: 8599 RVA: 0x0004226B File Offset: 0x0004046B
			public override string ToString()
			{
				return "0x" + this.intValue.ToString("x", CultureInfo.InvariantCulture);
			}

			// Token: 0x04001FD2 RID: 8146
			private int intValue;
		}

		// Token: 0x020006EA RID: 1770
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct SecurityPackageInfo
		{
			// Token: 0x06002198 RID: 8600 RVA: 0x0004228C File Offset: 0x0004048C
			public SecurityPackageInfo(IntPtr buffer)
			{
				NativeMethods.SecurityPackageInfo securityPackageInfo = (NativeMethods.SecurityPackageInfo)Marshal.PtrToStructure(buffer, typeof(NativeMethods.SecurityPackageInfo));
				this.fCapabilities = securityPackageInfo.fCapabilities;
				this.wVersion = securityPackageInfo.wVersion;
				this.wRPCID = securityPackageInfo.wRPCID;
				this.cbMaxToken = securityPackageInfo.cbMaxToken;
				this.Name = securityPackageInfo.Name;
				this.Comment = securityPackageInfo.Comment;
			}

			// Token: 0x04001FD3 RID: 8147
			internal NativeMethods.SecurityPackageInfo.cap fCapabilities;

			// Token: 0x04001FD4 RID: 8148
			internal short wVersion;

			// Token: 0x04001FD5 RID: 8149
			internal short wRPCID;

			// Token: 0x04001FD6 RID: 8150
			internal int cbMaxToken;

			// Token: 0x04001FD7 RID: 8151
			internal string Name;

			// Token: 0x04001FD8 RID: 8152
			internal string Comment;

			// Token: 0x020006EB RID: 1771
			[Flags]
			internal enum cap
			{
				// Token: 0x04001FDA RID: 8154
				Integrity = 1,
				// Token: 0x04001FDB RID: 8155
				Privacy = 2,
				// Token: 0x04001FDC RID: 8156
				TokenOnly = 4,
				// Token: 0x04001FDD RID: 8157
				Datagram = 8,
				// Token: 0x04001FDE RID: 8158
				Connection = 16,
				// Token: 0x04001FDF RID: 8159
				MultiLegRequired = 32,
				// Token: 0x04001FE0 RID: 8160
				ClientOnly = 64,
				// Token: 0x04001FE1 RID: 8161
				ExtendedError = 128,
				// Token: 0x04001FE2 RID: 8162
				Impersonation = 256,
				// Token: 0x04001FE3 RID: 8163
				AcceptsWin32Names = 512,
				// Token: 0x04001FE4 RID: 8164
				Stream = 1024,
				// Token: 0x04001FE5 RID: 8165
				Negotiable = 2048,
				// Token: 0x04001FE6 RID: 8166
				GssCompatible = 4096,
				// Token: 0x04001FE7 RID: 8167
				Logon = 8192,
				// Token: 0x04001FE8 RID: 8168
				AsciiBuffers = 16384,
				// Token: 0x04001FE9 RID: 8169
				Fragment = 32768,
				// Token: 0x04001FEA RID: 8170
				MutualAuth = 65536,
				// Token: 0x04001FEB RID: 8171
				Delegation = 131072,
				// Token: 0x04001FEC RID: 8172
				ReadonlyWithChecksum = 262144
			}
		}

		// Token: 0x020006EC RID: 1772
		internal struct SecurityPackageContextStreamSizes
		{
			// Token: 0x06002199 RID: 8601 RVA: 0x00042300 File Offset: 0x00040500
			internal unsafe SecurityPackageContextStreamSizes(byte[] memory)
			{
				fixed (IntPtr* ptr = memory)
				{
					IntPtr ptr2 = new IntPtr((void*)ptr);
					checked
					{
						this.cbHeader = (int)((uint)Marshal.ReadInt32(ptr2));
						this.cbTrailer = (int)((uint)Marshal.ReadInt32(ptr2, 4));
						this.cbMaximumMessage = (int)((uint)Marshal.ReadInt32(ptr2, 8));
						this.cBuffers = (int)((uint)Marshal.ReadInt32(ptr2, 12));
						this.cbBlockSize = (int)((uint)Marshal.ReadInt32(ptr2, 16));
					}
				}
			}

			// Token: 0x04001FED RID: 8173
			public static readonly int SizeOf = Marshal.SizeOf(typeof(NativeMethods.SecurityPackageContextStreamSizes));

			// Token: 0x04001FEE RID: 8174
			public int cbHeader;

			// Token: 0x04001FEF RID: 8175
			public int cbTrailer;

			// Token: 0x04001FF0 RID: 8176
			public int cbMaximumMessage;

			// Token: 0x04001FF1 RID: 8177
			public int cBuffers;

			// Token: 0x04001FF2 RID: 8178
			public int cbBlockSize;
		}

		// Token: 0x020006ED RID: 1773
		internal struct EapKeyBlock
		{
			// Token: 0x04001FF3 RID: 8179
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
			public byte[] rgbKeys;

			// Token: 0x04001FF4 RID: 8180
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
			public byte[] rgbIVs;
		}

		// Token: 0x020006EE RID: 1774
		internal struct SessionKey
		{
			// Token: 0x04001FF5 RID: 8181
			public int SessionKeyLength;

			// Token: 0x04001FF6 RID: 8182
			public IntPtr SessionKeyValuePtr;
		}

		// Token: 0x020006EF RID: 1775
		internal struct CRYPT_INTEGER_BLOB
		{
			// Token: 0x04001FF7 RID: 8183
			public uint cbData;

			// Token: 0x04001FF8 RID: 8184
			public IntPtr pbData;
		}

		// Token: 0x020006F0 RID: 1776
		internal struct CRYPT_OBJID_BLOB
		{
			// Token: 0x04001FF9 RID: 8185
			public uint cbData;

			// Token: 0x04001FFA RID: 8186
			public IntPtr pbData;
		}

		// Token: 0x020006F1 RID: 1777
		internal struct CERT_NAME_BLOB
		{
			// Token: 0x04001FFB RID: 8187
			public uint cbData;

			// Token: 0x04001FFC RID: 8188
			public IntPtr pbData;
		}

		// Token: 0x020006F2 RID: 1778
		internal struct CRYPT_ALGORITHM_IDENTIFIER
		{
			// Token: 0x04001FFD RID: 8189
			public string pszObjId;

			// Token: 0x04001FFE RID: 8190
			public NativeMethods.CRYPT_OBJID_BLOB Parameters;
		}

		// Token: 0x020006F3 RID: 1779
		internal struct CRYPT_BIT_BLOB
		{
			// Token: 0x04001FFF RID: 8191
			public uint cbData;

			// Token: 0x04002000 RID: 8192
			public IntPtr pbData;

			// Token: 0x04002001 RID: 8193
			public uint cUnusedBits;
		}

		// Token: 0x020006F4 RID: 1780
		internal struct FILETIME
		{
			// Token: 0x04002002 RID: 8194
			public uint dwLowDateTime;

			// Token: 0x04002003 RID: 8195
			public uint dwHighDateTime;
		}

		// Token: 0x020006F5 RID: 1781
		internal struct CERT_PUBLIC_KEY_INFO
		{
			// Token: 0x04002004 RID: 8196
			public NativeMethods.CRYPT_ALGORITHM_IDENTIFIER Algorithm;

			// Token: 0x04002005 RID: 8197
			public NativeMethods.CRYPT_BIT_BLOB PublicKey;
		}

		// Token: 0x020006F6 RID: 1782
		internal struct CertInfo
		{
			// Token: 0x04002006 RID: 8198
			public uint dwVersion;

			// Token: 0x04002007 RID: 8199
			public NativeMethods.CRYPT_INTEGER_BLOB SerialNumber;

			// Token: 0x04002008 RID: 8200
			public NativeMethods.CRYPT_ALGORITHM_IDENTIFIER SignatureAlgorithm;

			// Token: 0x04002009 RID: 8201
			public NativeMethods.CERT_NAME_BLOB Issuer;

			// Token: 0x0400200A RID: 8202
			public NativeMethods.FILETIME NotBefore;

			// Token: 0x0400200B RID: 8203
			public NativeMethods.FILETIME NotAfter;

			// Token: 0x0400200C RID: 8204
			public NativeMethods.CERT_NAME_BLOB Subject;

			// Token: 0x0400200D RID: 8205
			public NativeMethods.CERT_PUBLIC_KEY_INFO SubjectPublicKeyInfo;

			// Token: 0x0400200E RID: 8206
			public NativeMethods.CRYPT_BIT_BLOB IssuerUniqueId;

			// Token: 0x0400200F RID: 8207
			public NativeMethods.CRYPT_BIT_BLOB SubjectUniqueId;

			// Token: 0x04002010 RID: 8208
			public uint cExtension;

			// Token: 0x04002011 RID: 8209
			public IntPtr rgExtension;
		}

		// Token: 0x020006F7 RID: 1783
		internal struct CertContext
		{
			// Token: 0x04002012 RID: 8210
			public uint dwCertEncodingType;

			// Token: 0x04002013 RID: 8211
			public IntPtr pbCertEncoded;

			// Token: 0x04002014 RID: 8212
			public uint cbCertEncoded;

			// Token: 0x04002015 RID: 8213
			public IntPtr pCertInfo;

			// Token: 0x04002016 RID: 8214
			public IntPtr hCertStore;
		}

		// Token: 0x020006F8 RID: 1784
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct CredHandle
		{
			// Token: 0x170008B1 RID: 2225
			// (get) Token: 0x0600219B RID: 8603 RVA: 0x0004238D File Offset: 0x0004058D
			internal bool IsZero
			{
				get
				{
					return this.HandleHi == IntPtr.Zero && this.HandleLo == IntPtr.Zero;
				}
			}

			// Token: 0x0600219C RID: 8604 RVA: 0x000423B3 File Offset: 0x000405B3
			public override string ToString()
			{
				return this.HandleHi.ToString("x") + ":" + this.HandleLo.ToString("x");
			}

			// Token: 0x0600219D RID: 8605 RVA: 0x000423DF File Offset: 0x000405DF
			internal void SetToInvalid()
			{
				this.HandleHi = IntPtr.Zero;
				this.HandleLo = IntPtr.Zero;
			}

			// Token: 0x04002017 RID: 8215
			internal IntPtr HandleHi;

			// Token: 0x04002018 RID: 8216
			internal IntPtr HandleLo;
		}

		// Token: 0x020006F9 RID: 1785
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct SecBuffer
		{
			// Token: 0x04002019 RID: 8217
			public int cbBuffer;

			// Token: 0x0400201A RID: 8218
			public int BufferType;

			// Token: 0x0400201B RID: 8219
			public IntPtr buffer;
		}

		// Token: 0x020006FA RID: 1786
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct AuthData
		{
			// Token: 0x0400201C RID: 8220
			public string User;

			// Token: 0x0400201D RID: 8221
			public int UserLength;

			// Token: 0x0400201E RID: 8222
			public string Domain;

			// Token: 0x0400201F RID: 8223
			public int DomainLength;

			// Token: 0x04002020 RID: 8224
			public string Password;

			// Token: 0x04002021 RID: 8225
			public int PasswordLength;

			// Token: 0x04002022 RID: 8226
			public int Flags;
		}

		// Token: 0x020006FB RID: 1787
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct AuthDataForSchannel
		{
			// Token: 0x0600219E RID: 8606 RVA: 0x000423F8 File Offset: 0x000405F8
			public static NativeMethods.AuthDataForSchannel GetAuthDataForSchannel()
			{
				return new NativeMethods.AuthDataForSchannel
				{
					dwVersion = 4,
					cCreds = 0,
					paCred = IntPtr.Zero,
					hRootStore = IntPtr.Zero,
					cMappers = 0,
					aphMappers = IntPtr.Zero,
					cSupportedAlgs = 0,
					palgSupportedAlgs = IntPtr.Zero,
					grbitEnabledProtocols = SchannelProtocols.Zero,
					dwMinimumCipherStrength = 0,
					dwMaximumCipherStrength = 0,
					dwSessionLifespan = 0,
					dwFlags = 24,
					reserved = 0
				};
			}

			// Token: 0x04002023 RID: 8227
			public const int CurrentVersion = 4;

			// Token: 0x04002024 RID: 8228
			public int dwVersion;

			// Token: 0x04002025 RID: 8229
			public int cCreds;

			// Token: 0x04002026 RID: 8230
			public IntPtr paCred;

			// Token: 0x04002027 RID: 8231
			public IntPtr hRootStore;

			// Token: 0x04002028 RID: 8232
			public int cMappers;

			// Token: 0x04002029 RID: 8233
			public IntPtr aphMappers;

			// Token: 0x0400202A RID: 8234
			public int cSupportedAlgs;

			// Token: 0x0400202B RID: 8235
			public IntPtr palgSupportedAlgs;

			// Token: 0x0400202C RID: 8236
			public SchannelProtocols grbitEnabledProtocols;

			// Token: 0x0400202D RID: 8237
			public int dwMinimumCipherStrength;

			// Token: 0x0400202E RID: 8238
			public int dwMaximumCipherStrength;

			// Token: 0x0400202F RID: 8239
			public int dwSessionLifespan;

			// Token: 0x04002030 RID: 8240
			public int dwFlags;

			// Token: 0x04002031 RID: 8241
			public int reserved;

			// Token: 0x020006FC RID: 1788
			[Flags]
			public enum Flags
			{
				// Token: 0x04002033 RID: 8243
				Zero = 0,
				// Token: 0x04002034 RID: 8244
				NoSystemMapper = 2,
				// Token: 0x04002035 RID: 8245
				NoNameCheck = 4,
				// Token: 0x04002036 RID: 8246
				ValidateManual = 8,
				// Token: 0x04002037 RID: 8247
				NoDefaultCred = 16,
				// Token: 0x04002038 RID: 8248
				ValidateAuto = 32
			}
		}

		// Token: 0x020006FD RID: 1789
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal class SecBufferDesc
		{
			// Token: 0x04002039 RID: 8249
			public int ulVersion;

			// Token: 0x0400203A RID: 8250
			public int cBuffers;

			// Token: 0x0400203B RID: 8251
			public IntPtr pBuffer;
		}
	}
}
