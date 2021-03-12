using System;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000AB5 RID: 2741
	internal static class LsaNativeMethods
	{
		// Token: 0x06003B02 RID: 15106
		[DllImport("secur32.dll")]
		internal static extern int LsaConnectUntrusted(out SafeLsaUntrustedHandle LsaHandle);

		// Token: 0x06003B03 RID: 15107
		[DllImport("secur32.dll")]
		internal static extern int LsaLookupAuthenticationPackage(SafeLsaUntrustedHandle LsaHandle, LsaNativeMethods.LsaAnsiString PackageName, out int AuthenticationPackage);

		// Token: 0x06003B04 RID: 15108
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("secur32.dll")]
		internal static extern int LsaDeregisterLogonProcess(IntPtr LsaHandle);

		// Token: 0x06003B05 RID: 15109
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("secur32.dll")]
		internal static extern int LsaCallAuthenticationPackage(SafeLsaUntrustedHandle LsaHandle, int packageId, [In] ref LsaNativeMethods.KerberosPurgeTicketCacheRequest request, int requestLength, [In] IntPtr returnBuffer, [In] [Out] ref int returnBufferLength, out int ProtocolStatus);

		// Token: 0x06003B06 RID: 15110
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("secur32.dll", SetLastError = true)]
		internal static extern int LsaCallAuthenticationPackage([In] SafeLsaUntrustedHandle lsaHandle, [In] int authenticationPackage, [In] ref LsaNativeMethods.LiveQueryUserInfoRequest protocolSubmitBuffer, [In] int submitBufferLength, out IntPtr protocolReturnBuffer, out int returnBufferLength, out int protocolStatus);

		// Token: 0x06003B07 RID: 15111
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("secur32.dll")]
		internal static extern int LsaCallAuthenticationPackage(SafeLsaUntrustedHandle LsaHandle, int packageId, IntPtr request, int requestLength, [In] IntPtr returnBuffer, [In] [Out] ref int returnBufferLength, out int ProtocolStatus);

		// Token: 0x06003B08 RID: 15112
		[DllImport("secur32.dll")]
		internal static extern int LsaFreeReturnBuffer(IntPtr buffer);

		// Token: 0x06003B09 RID: 15113
		[DllImport("advapi32.dll")]
		internal static extern int LsaOpenPolicy(LsaNativeMethods.LsaUnicodeString target, LsaNativeMethods.LsaObjectAttributes objectAttributes, LsaNativeMethods.PolicyAccess access, out SafeLsaPolicyHandle handle);

		// Token: 0x06003B0A RID: 15114
		[DllImport("advapi32.dll")]
		internal static extern int LsaQueryInformationPolicy(SafeLsaPolicyHandle handle, LsaNativeMethods.PolicyInformationClass infoClass, out SafeLsaMemoryHandle buffer);

		// Token: 0x06003B0B RID: 15115
		[DllImport("advapi32.dll")]
		internal static extern int LsaNtStatusToWinError(int ntstatus);

		// Token: 0x06003B0C RID: 15116
		[DllImport("advapi32.dll")]
		internal static extern int LsaStorePrivateData(SafeLsaPolicyHandle policyHandle, LsaNativeMethods.LsaUnicodeString keyName, LsaNativeMethods.LsaUnicodeString privateData);

		// Token: 0x06003B0D RID: 15117
		[DllImport("advapi32.dll")]
		internal static extern int LsaAddAccountRights(SafeLsaPolicyHandle policyHandle, [MarshalAs(UnmanagedType.LPArray)] byte[] accountSid, LsaNativeMethods.LsaUnicodeStringStruct[] userRights, int countOfRights);

		// Token: 0x06003B0E RID: 15118
		[DllImport("advapi32.dll")]
		internal static extern int LsaEnumerateAccountRights(SafeLsaPolicyHandle policyHandle, [MarshalAs(UnmanagedType.LPArray)] byte[] accountSid, out SafeLsaMemoryHandle userRightsPtr, out int countOfRights);

		// Token: 0x06003B0F RID: 15119
		[DllImport("advapi32.dll")]
		internal static extern int LsaRemoveAccountRights(SafeLsaPolicyHandle policyHandle, [MarshalAs(UnmanagedType.LPArray)] byte[] accountSid, [MarshalAs(UnmanagedType.Bool)] bool allRights, LsaNativeMethods.LsaUnicodeStringStruct[] userRights, int countOfRights);

		// Token: 0x04003387 RID: 13191
		private const string ADVAPI32 = "advapi32.dll";

		// Token: 0x04003388 RID: 13192
		private const string SECUR32 = "secur32.dll";

		// Token: 0x02000AB6 RID: 2742
		[StructLayout(LayoutKind.Sequential)]
		internal class LsaObjectAttributes
		{
			// Token: 0x06003B10 RID: 15120 RVA: 0x00097E1E File Offset: 0x0009601E
			internal LsaObjectAttributes()
			{
				this.Length = Marshal.SizeOf(typeof(LsaNativeMethods.LsaObjectAttributes));
			}

			// Token: 0x04003389 RID: 13193
			private int Length;

			// Token: 0x0400338A RID: 13194
			private IntPtr RootDirectory;

			// Token: 0x0400338B RID: 13195
			private LsaNativeMethods.LsaUnicodeString ObjectName;

			// Token: 0x0400338C RID: 13196
			private int Attributes;

			// Token: 0x0400338D RID: 13197
			private IntPtr SecurityDescriptor;

			// Token: 0x0400338E RID: 13198
			private IntPtr SecurityQualityOfService;
		}

		// Token: 0x02000AB7 RID: 2743
		[StructLayout(LayoutKind.Sequential)]
		internal class LsaUnicodeString
		{
			// Token: 0x06003B11 RID: 15121 RVA: 0x00097E3B File Offset: 0x0009603B
			internal LsaUnicodeString()
			{
			}

			// Token: 0x06003B12 RID: 15122 RVA: 0x00097E43 File Offset: 0x00096043
			internal LsaUnicodeString(ushort length, ushort maxLength, IntPtr buffer)
			{
				this.length = length;
				this.maxLength = maxLength;
				this.buffer = buffer;
			}

			// Token: 0x17000EAB RID: 3755
			// (get) Token: 0x06003B13 RID: 15123 RVA: 0x00097E60 File Offset: 0x00096060
			internal string Value
			{
				get
				{
					if (this.length > 0)
					{
						return Marshal.PtrToStringUni(this.buffer, (int)(this.length / 2));
					}
					return null;
				}
			}

			// Token: 0x0400338F RID: 13199
			internal ushort length;

			// Token: 0x04003390 RID: 13200
			internal ushort maxLength;

			// Token: 0x04003391 RID: 13201
			internal IntPtr buffer;
		}

		// Token: 0x02000AB8 RID: 2744
		internal struct LsaUnicodeStringStruct
		{
			// Token: 0x06003B14 RID: 15124 RVA: 0x00097E80 File Offset: 0x00096080
			internal LsaUnicodeStringStruct(LsaNativeMethods.LsaUnicodeString value)
			{
				this.length = value.length;
				this.maxLength = value.maxLength;
				this.buffer = value.buffer;
			}

			// Token: 0x17000EAC RID: 3756
			// (get) Token: 0x06003B15 RID: 15125 RVA: 0x00097EA6 File Offset: 0x000960A6
			internal string Value
			{
				get
				{
					if (this.length > 0)
					{
						return Marshal.PtrToStringUni(this.buffer, (int)(this.length / 2));
					}
					return null;
				}
			}

			// Token: 0x17000EAD RID: 3757
			// (get) Token: 0x06003B16 RID: 15126 RVA: 0x00097EC6 File Offset: 0x000960C6
			// (set) Token: 0x06003B17 RID: 15127 RVA: 0x00097ECE File Offset: 0x000960CE
			internal ushort Length
			{
				get
				{
					return this.length;
				}
				set
				{
					this.length = value;
				}
			}

			// Token: 0x17000EAE RID: 3758
			// (get) Token: 0x06003B18 RID: 15128 RVA: 0x00097ED7 File Offset: 0x000960D7
			// (set) Token: 0x06003B19 RID: 15129 RVA: 0x00097EDF File Offset: 0x000960DF
			internal ushort MaxLength
			{
				get
				{
					return this.maxLength;
				}
				set
				{
					this.maxLength = value;
				}
			}

			// Token: 0x17000EAF RID: 3759
			// (get) Token: 0x06003B1A RID: 15130 RVA: 0x00097EE8 File Offset: 0x000960E8
			// (set) Token: 0x06003B1B RID: 15131 RVA: 0x00097EF0 File Offset: 0x000960F0
			internal IntPtr Buffer
			{
				get
				{
					return this.buffer;
				}
				set
				{
					this.buffer = value;
				}
			}

			// Token: 0x04003392 RID: 13202
			private ushort length;

			// Token: 0x04003393 RID: 13203
			private ushort maxLength;

			// Token: 0x04003394 RID: 13204
			private IntPtr buffer;
		}

		// Token: 0x02000AB9 RID: 2745
		internal class SafeLsaUnicodeString : LsaNativeMethods.LsaUnicodeString, IDisposable
		{
			// Token: 0x06003B1C RID: 15132 RVA: 0x00097EFC File Offset: 0x000960FC
			internal SafeLsaUnicodeString(string value)
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.gch = GCHandle.Alloc(value, GCHandleType.Pinned);
					this.buffer = this.gch.AddrOfPinnedObject();
					this.length = (ushort)(value.Length * 2);
					this.maxLength = this.length;
				}
			}

			// Token: 0x06003B1D RID: 15133 RVA: 0x00097F50 File Offset: 0x00096150
			public void Dispose()
			{
				if (this.gch.IsAllocated)
				{
					this.gch.Free();
				}
			}

			// Token: 0x04003395 RID: 13205
			private GCHandle gch;
		}

		// Token: 0x02000ABA RID: 2746
		[StructLayout(LayoutKind.Sequential)]
		internal class LsaAnsiString
		{
			// Token: 0x17000EB0 RID: 3760
			// (get) Token: 0x06003B1E RID: 15134 RVA: 0x00097F6A File Offset: 0x0009616A
			internal string Value
			{
				get
				{
					if (this.length > 0)
					{
						return Marshal.PtrToStringAnsi(this.buffer, (int)this.length);
					}
					return null;
				}
			}

			// Token: 0x04003396 RID: 13206
			protected ushort length;

			// Token: 0x04003397 RID: 13207
			protected ushort maxLength;

			// Token: 0x04003398 RID: 13208
			protected IntPtr buffer;
		}

		// Token: 0x02000ABB RID: 2747
		internal class SafeLsaAnsiString : LsaNativeMethods.LsaAnsiString, IDisposable
		{
			// Token: 0x06003B20 RID: 15136 RVA: 0x00097F90 File Offset: 0x00096190
			internal SafeLsaAnsiString(string value)
			{
				this.buffer = IntPtr.Zero;
				if (!string.IsNullOrEmpty(value))
				{
					this.buffer = Marshal.StringToHGlobalAnsi(value);
					this.length = (ushort)value.Length;
					this.maxLength = this.length;
				}
			}

			// Token: 0x06003B21 RID: 15137 RVA: 0x00097FD0 File Offset: 0x000961D0
			public void Dispose()
			{
				if (this.buffer != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.buffer);
					this.buffer = IntPtr.Zero;
				}
			}
		}

		// Token: 0x02000ABC RID: 2748
		internal struct LUID
		{
			// Token: 0x06003B22 RID: 15138 RVA: 0x00097FFA File Offset: 0x000961FA
			internal LUID(int lowPart, int highPart)
			{
				this.LowPart = lowPart;
				this.HighPart = highPart;
			}

			// Token: 0x04003399 RID: 13209
			public int LowPart;

			// Token: 0x0400339A RID: 13210
			public int HighPart;
		}

		// Token: 0x02000ABD RID: 2749
		internal enum KerberosProtocolMessage
		{
			// Token: 0x0400339C RID: 13212
			DebugRequest,
			// Token: 0x0400339D RID: 13213
			QueryTicketCache,
			// Token: 0x0400339E RID: 13214
			ChangeMachinePassword,
			// Token: 0x0400339F RID: 13215
			VerifyPac,
			// Token: 0x040033A0 RID: 13216
			RetrieveTicket,
			// Token: 0x040033A1 RID: 13217
			UpdateAddresses,
			// Token: 0x040033A2 RID: 13218
			PurgeTicketCache,
			// Token: 0x040033A3 RID: 13219
			ChangePassword,
			// Token: 0x040033A4 RID: 13220
			RetrieveEncodedTicket,
			// Token: 0x040033A5 RID: 13221
			DecryptData,
			// Token: 0x040033A6 RID: 13222
			AddBindingCacheEntry,
			// Token: 0x040033A7 RID: 13223
			SetPassword,
			// Token: 0x040033A8 RID: 13224
			SetPasswordEx,
			// Token: 0x040033A9 RID: 13225
			VerifyCredentials,
			// Token: 0x040033AA RID: 13226
			QueryTicketCacheEx,
			// Token: 0x040033AB RID: 13227
			PurgeTicketCacheEx,
			// Token: 0x040033AC RID: 13228
			RefreshSmartcardCredentials,
			// Token: 0x040033AD RID: 13229
			AddExtraCredentials,
			// Token: 0x040033AE RID: 13230
			QuerySupplementalCredentials,
			// Token: 0x040033AF RID: 13231
			TransferCredentials,
			// Token: 0x040033B0 RID: 13232
			QueryTicketCacheEx2
		}

		// Token: 0x02000ABE RID: 2750
		internal struct KerberosPurgeTicketCacheRequest
		{
			// Token: 0x06003B23 RID: 15139 RVA: 0x0009800A File Offset: 0x0009620A
			public KerberosPurgeTicketCacheRequest(int dummy)
			{
				this.messageType = LsaNativeMethods.KerberosProtocolMessage.PurgeTicketCache;
				this.logonId = default(LsaNativeMethods.LUID);
				this.serverName = default(LsaNativeMethods.LsaUnicodeStringStruct);
				this.realmName = default(LsaNativeMethods.LsaUnicodeStringStruct);
			}

			// Token: 0x040033B1 RID: 13233
			private LsaNativeMethods.KerberosProtocolMessage messageType;

			// Token: 0x040033B2 RID: 13234
			private LsaNativeMethods.LUID logonId;

			// Token: 0x040033B3 RID: 13235
			private LsaNativeMethods.LsaUnicodeStringStruct serverName;

			// Token: 0x040033B4 RID: 13236
			private LsaNativeMethods.LsaUnicodeStringStruct realmName;
		}

		// Token: 0x02000ABF RID: 2751
		internal struct KerberosAddCredentialsRequestStruct
		{
			// Token: 0x040033B5 RID: 13237
			public static readonly int SizeOf = Marshal.SizeOf(typeof(LsaNativeMethods.KerberosAddCredentialsRequestStruct));

			// Token: 0x040033B6 RID: 13238
			public LsaNativeMethods.KerberosProtocolMessage MessageType;

			// Token: 0x040033B7 RID: 13239
			public LsaNativeMethods.LsaUnicodeStringStruct Username;

			// Token: 0x040033B8 RID: 13240
			public LsaNativeMethods.LsaUnicodeStringStruct Domain;

			// Token: 0x040033B9 RID: 13241
			public LsaNativeMethods.LsaUnicodeStringStruct Password;

			// Token: 0x040033BA RID: 13242
			public LsaNativeMethods.LUID LogonId;

			// Token: 0x040033BB RID: 13243
			public LsaNativeMethods.KerbRequestCredentialFlags Flags;
		}

		// Token: 0x02000AC0 RID: 2752
		internal static class KerberosAddCredentialsRequest
		{
			// Token: 0x06003B25 RID: 15141 RVA: 0x00098050 File Offset: 0x00096250
			public unsafe static SafeSecureHGlobalHandle MarshalToNative(string username, string domain, SecureString password, LsaNativeMethods.KerbRequestCredentialFlags flags, LsaNativeMethods.LUID luid)
			{
				int num = LsaNativeMethods.KerberosAddCredentialsRequest.ByteLength(username);
				int num2 = LsaNativeMethods.KerberosAddCredentialsRequest.ByteLength(domain);
				int num3 = LsaNativeMethods.KerberosAddCredentialsRequest.ByteLength(password);
				int size = LsaNativeMethods.KerberosAddCredentialsRequestStruct.SizeOf + num + num2 + num3;
				SafeSecureHGlobalHandle result;
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					SafeSecureHGlobalHandle safeSecureHGlobalHandle = SafeSecureHGlobalHandle.AllocHGlobal(size);
					disposeGuard.Add<SafeSecureHGlobalHandle>(safeSecureHGlobalHandle);
					LsaNativeMethods.KerberosAddCredentialsRequestStruct* ptr = (LsaNativeMethods.KerberosAddCredentialsRequestStruct*)safeSecureHGlobalHandle.DangerousGetHandle().ToPointer();
					char* ptr2 = (char*)(ptr + 1);
					ptr->MessageType = LsaNativeMethods.KerberosProtocolMessage.AddExtraCredentials;
					LsaNativeMethods.KerberosAddCredentialsRequest.MarshalUnicodeString(username, ref ptr->Username, ref ptr2);
					LsaNativeMethods.KerberosAddCredentialsRequest.MarshalUnicodeString(domain, ref ptr->Domain, ref ptr2);
					LsaNativeMethods.KerberosAddCredentialsRequest.MarshalUnicodeString(password, ref ptr->Password, ref ptr2);
					ptr->LogonId = luid;
					ptr->Flags = flags;
					disposeGuard.Success();
					result = safeSecureHGlobalHandle;
				}
				return result;
			}

			// Token: 0x06003B26 RID: 15142 RVA: 0x0009812C File Offset: 0x0009632C
			private unsafe static void MarshalUnicodeString(string source, ref LsaNativeMethods.LsaUnicodeStringStruct destination, ref char* extraData)
			{
				destination.Length = (destination.MaxLength = (ushort)LsaNativeMethods.KerberosAddCredentialsRequest.ByteLength(source));
				if (destination.Length == 0)
				{
					destination.Buffer = IntPtr.Zero;
					return;
				}
				fixed (char* ptr = source)
				{
					destination.Buffer = (IntPtr)extraData;
					char* ptr2 = ptr;
					for (int i = 0; i < source.Length; i++)
					{
						char* ptr3;
						extraData = (ptr3 = extraData) + (IntPtr)2;
						*ptr3 = *(ptr2++);
					}
				}
			}

			// Token: 0x06003B27 RID: 15143 RVA: 0x000981AC File Offset: 0x000963AC
			private unsafe static void MarshalUnicodeString(SecureString source, ref LsaNativeMethods.LsaUnicodeStringStruct destination, ref char* extraData)
			{
				destination.Length = (destination.MaxLength = (ushort)LsaNativeMethods.KerberosAddCredentialsRequest.ByteLength(source));
				if (destination.Length == 0)
				{
					destination.Buffer = IntPtr.Zero;
					return;
				}
				using (SafeSecureHGlobalHandle safeSecureHGlobalHandle = SafeSecureHGlobalHandle.DecryptAndAllocHGlobal(source))
				{
					destination.Buffer = (IntPtr)extraData;
					char* ptr = (char*)safeSecureHGlobalHandle.DangerousGetHandle().ToPointer();
					for (int i = 0; i < source.Length; i++)
					{
						char* ptr2;
						extraData = (ptr2 = extraData) + (IntPtr)2;
						*ptr2 = *(ptr++);
					}
				}
			}

			// Token: 0x06003B28 RID: 15144 RVA: 0x00098248 File Offset: 0x00096448
			private static int ByteLength(string str)
			{
				if (!string.IsNullOrEmpty(str))
				{
					return str.Length * 2;
				}
				return 0;
			}

			// Token: 0x06003B29 RID: 15145 RVA: 0x0009825C File Offset: 0x0009645C
			private static int ByteLength(SecureString str)
			{
				if (str != null)
				{
					return str.Length * 2;
				}
				return 0;
			}
		}

		// Token: 0x02000AC1 RID: 2753
		[Flags]
		internal enum KerbRequestCredentialFlags
		{
			// Token: 0x040033BD RID: 13245
			Add = 1,
			// Token: 0x040033BE RID: 13246
			Replace = 2,
			// Token: 0x040033BF RID: 13247
			Remove = 4
		}

		// Token: 0x02000AC2 RID: 2754
		internal enum LiveProtocolMessageType
		{
			// Token: 0x040033C1 RID: 13249
			QueryUserInfoMessage = 256
		}

		// Token: 0x02000AC3 RID: 2755
		internal struct LiveQueryUserInfoRequest
		{
			// Token: 0x06003B2A RID: 15146 RVA: 0x0009826B File Offset: 0x0009646B
			public LiveQueryUserInfoRequest(int dummy)
			{
				this.messageType = LsaNativeMethods.LiveProtocolMessageType.QueryUserInfoMessage;
				this.headerLength = (ushort)Marshal.SizeOf(typeof(LsaNativeMethods.LiveQueryUserInfoRequest));
				this.logonId = default(LsaNativeMethods.LUID);
			}

			// Token: 0x040033C2 RID: 13250
			private LsaNativeMethods.LiveProtocolMessageType messageType;

			// Token: 0x040033C3 RID: 13251
			private ushort headerLength;

			// Token: 0x040033C4 RID: 13252
			private LsaNativeMethods.LUID logonId;
		}

		// Token: 0x02000AC4 RID: 2756
		internal struct LiveQueryUserInfoResponse
		{
			// Token: 0x17000EB1 RID: 3761
			// (get) Token: 0x06003B2B RID: 15147 RVA: 0x0009829A File Offset: 0x0009649A
			public ulong Cid
			{
				get
				{
					return this.cid;
				}
			}

			// Token: 0x06003B2C RID: 15148 RVA: 0x000982A4 File Offset: 0x000964A4
			public string GetEmailAddress(IntPtr basePointer, int bufferLength)
			{
				if ((long)bufferLength - (long)((ulong)this.emailAddressOffset) < 0L || (long)bufferLength - (long)((ulong)this.emailAddressOffset) - (long)((ulong)this.emailAddressLengthInChars) < 0L)
				{
					throw new Win32Exception("Email Address is pointing outside of the buffer");
				}
				IntPtr ptr = new IntPtr(basePointer.ToInt64() + (long)((ulong)this.emailAddressOffset));
				return Marshal.PtrToStringUni(ptr, (int)this.emailAddressLengthInChars);
			}

			// Token: 0x06003B2D RID: 15149 RVA: 0x00098304 File Offset: 0x00096504
			public string GetTicket(IntPtr basePointer, int bufferLength)
			{
				if ((long)bufferLength - (long)((ulong)this.ticketOffset) < 0L || (long)bufferLength - (long)((ulong)this.ticketOffset) - (long)((ulong)this.ticketLengthInChars) < 0L)
				{
					throw new Win32Exception("Ticket is pointing outside of the buffer");
				}
				IntPtr ptr = new IntPtr(basePointer.ToInt64() + (long)((ulong)this.ticketOffset));
				return Marshal.PtrToStringUni(ptr, (int)this.ticketLengthInChars);
			}

			// Token: 0x06003B2E RID: 15150 RVA: 0x00098364 File Offset: 0x00096564
			public string GetSiteName(IntPtr basePointer, int bufferLength)
			{
				if ((long)bufferLength - (long)((ulong)this.siteNameOffset) < 0L || (long)bufferLength - (long)((ulong)this.siteNameOffset) - (long)((ulong)this.siteNameLengthInChars) < 0L)
				{
					throw new Win32Exception("Site name is pointing outside of the buffer");
				}
				IntPtr ptr = new IntPtr(basePointer.ToInt64() + (long)((ulong)this.siteNameOffset));
				return Marshal.PtrToStringUni(ptr, (int)this.siteNameLengthInChars);
			}

			// Token: 0x040033C5 RID: 13253
			private LsaNativeMethods.LiveProtocolMessageType messageType;

			// Token: 0x040033C6 RID: 13254
			private ushort headerLength;

			// Token: 0x040033C7 RID: 13255
			private ulong cid;

			// Token: 0x040033C8 RID: 13256
			private uint emailAddressOffset;

			// Token: 0x040033C9 RID: 13257
			private ushort emailAddressLengthInChars;

			// Token: 0x040033CA RID: 13258
			private uint ticketOffset;

			// Token: 0x040033CB RID: 13259
			private ushort ticketLengthInChars;

			// Token: 0x040033CC RID: 13260
			private uint siteNameOffset;

			// Token: 0x040033CD RID: 13261
			private ushort siteNameLengthInChars;
		}

		// Token: 0x02000AC5 RID: 2757
		internal class PolicyDnsDomainInfo
		{
			// Token: 0x06003B2F RID: 15151 RVA: 0x000983C4 File Offset: 0x000965C4
			internal PolicyDnsDomainInfo(SafeLsaMemoryHandle memory)
			{
				LsaNativeMethods.PolicyDnsDomainInfo.PolicyDnsDomainInfoStruct policyDnsDomainInfoStruct = (LsaNativeMethods.PolicyDnsDomainInfo.PolicyDnsDomainInfoStruct)Marshal.PtrToStructure(memory.DangerousGetHandle(), typeof(LsaNativeMethods.PolicyDnsDomainInfo.PolicyDnsDomainInfoStruct));
				this.Name = policyDnsDomainInfoStruct.Name.Value;
				this.DnsDomainName = policyDnsDomainInfoStruct.DnsDomainName.Value;
				this.DnsForestName = policyDnsDomainInfoStruct.DnsForestName.Value;
				this.DomainGuid = policyDnsDomainInfoStruct.DomainGuid;
				if (policyDnsDomainInfoStruct.Sid != IntPtr.Zero)
				{
					this.Sid = new SecurityIdentifier(policyDnsDomainInfoStruct.Sid);
				}
			}

			// Token: 0x17000EB2 RID: 3762
			// (get) Token: 0x06003B30 RID: 15152 RVA: 0x0009845A File Offset: 0x0009665A
			internal bool IsDomainMember
			{
				get
				{
					return this.Sid != null;
				}
			}

			// Token: 0x040033CE RID: 13262
			internal string Name;

			// Token: 0x040033CF RID: 13263
			internal string DnsDomainName;

			// Token: 0x040033D0 RID: 13264
			internal string DnsForestName;

			// Token: 0x040033D1 RID: 13265
			internal Guid DomainGuid;

			// Token: 0x040033D2 RID: 13266
			internal SecurityIdentifier Sid;

			// Token: 0x02000AC6 RID: 2758
			private struct PolicyDnsDomainInfoStruct
			{
				// Token: 0x040033D3 RID: 13267
				internal LsaNativeMethods.LsaUnicodeStringStruct Name;

				// Token: 0x040033D4 RID: 13268
				internal LsaNativeMethods.LsaUnicodeStringStruct DnsDomainName;

				// Token: 0x040033D5 RID: 13269
				internal LsaNativeMethods.LsaUnicodeStringStruct DnsForestName;

				// Token: 0x040033D6 RID: 13270
				internal Guid DomainGuid;

				// Token: 0x040033D7 RID: 13271
				internal IntPtr Sid;
			}
		}

		// Token: 0x02000AC7 RID: 2759
		[Flags]
		internal enum PolicyAccess
		{
			// Token: 0x040033D9 RID: 13273
			ViewLocalInformation = 1,
			// Token: 0x040033DA RID: 13274
			ViewAuditInformation = 2,
			// Token: 0x040033DB RID: 13275
			GetPrivateInformation = 4,
			// Token: 0x040033DC RID: 13276
			TrustAdmin = 8,
			// Token: 0x040033DD RID: 13277
			CreateAccount = 16,
			// Token: 0x040033DE RID: 13278
			CreateSecret = 32,
			// Token: 0x040033DF RID: 13279
			CreatePrivilege = 64,
			// Token: 0x040033E0 RID: 13280
			SetDefaultQuotaLimits = 128,
			// Token: 0x040033E1 RID: 13281
			SetAuditRequirements = 256,
			// Token: 0x040033E2 RID: 13282
			AuditLogAdmin = 512,
			// Token: 0x040033E3 RID: 13283
			ServerAdmin = 1024,
			// Token: 0x040033E4 RID: 13284
			LookupNames = 2048,
			// Token: 0x040033E5 RID: 13285
			Notification = 4096,
			// Token: 0x040033E6 RID: 13286
			AllAccess = 8191
		}

		// Token: 0x02000AC8 RID: 2760
		internal enum PolicyInformationClass
		{
			// Token: 0x040033E8 RID: 13288
			AuditLogInformation = 1,
			// Token: 0x040033E9 RID: 13289
			AuditEventsInformation,
			// Token: 0x040033EA RID: 13290
			PrimaryDomainInformation,
			// Token: 0x040033EB RID: 13291
			PdAccountInformation,
			// Token: 0x040033EC RID: 13292
			AccountDomainInformation,
			// Token: 0x040033ED RID: 13293
			LsaServerRoleInformation,
			// Token: 0x040033EE RID: 13294
			ReplicaSourceInformation,
			// Token: 0x040033EF RID: 13295
			DefaultQuotaInformation,
			// Token: 0x040033F0 RID: 13296
			ModificationInformation,
			// Token: 0x040033F1 RID: 13297
			AuditFullSetInformation,
			// Token: 0x040033F2 RID: 13298
			AuditFullQueryInformation,
			// Token: 0x040033F3 RID: 13299
			DnsDomainInformation
		}
	}
}
