using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C8F RID: 3215
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct SecurityPackageInfo
	{
		// Token: 0x060046E8 RID: 18152 RVA: 0x000BEA18 File Offset: 0x000BCC18
		public SecurityPackageInfo(IntPtr buffer)
		{
			SecurityPackageInfo securityPackageInfo = (SecurityPackageInfo)Marshal.PtrToStructure(buffer, typeof(SecurityPackageInfo));
			this.Capabilities = securityPackageInfo.Capabilities;
			this.Version = securityPackageInfo.Version;
			this.RPCID = securityPackageInfo.RPCID;
			this.MaxToken = securityPackageInfo.MaxToken;
			this.Name = securityPackageInfo.Name;
			this.Comment = securityPackageInfo.Comment;
		}

		// Token: 0x060046E9 RID: 18153 RVA: 0x000BEA8C File Offset: 0x000BCC8C
		internal unsafe SecurityPackageInfo(byte[] memory)
		{
			fixed (IntPtr* ptr = memory)
			{
				IntPtr ptr2 = new IntPtr((void*)ptr);
				using (SafeContextBuffer safeContextBuffer = new SafeContextBuffer(Marshal.ReadIntPtr(ptr2, 0)))
				{
					SecurityPackageInfo securityPackageInfo = (SecurityPackageInfo)Marshal.PtrToStructure(safeContextBuffer.DangerousGetHandle(), typeof(SecurityPackageInfo));
					this.Capabilities = securityPackageInfo.Capabilities;
					this.Version = securityPackageInfo.Version;
					this.RPCID = securityPackageInfo.RPCID;
					this.MaxToken = securityPackageInfo.MaxToken;
					this.Name = securityPackageInfo.Name;
					this.Comment = securityPackageInfo.Comment;
				}
			}
		}

		// Token: 0x04003BFA RID: 15354
		internal SecurityPackageInfo.CapabilityFlags Capabilities;

		// Token: 0x04003BFB RID: 15355
		internal short Version;

		// Token: 0x04003BFC RID: 15356
		internal short RPCID;

		// Token: 0x04003BFD RID: 15357
		internal int MaxToken;

		// Token: 0x04003BFE RID: 15358
		internal string Name;

		// Token: 0x04003BFF RID: 15359
		internal string Comment;

		// Token: 0x04003C00 RID: 15360
		internal static readonly int Size = Marshal.SizeOf(typeof(SecurityPackageInfo));

		// Token: 0x02000C90 RID: 3216
		[Flags]
		internal enum CapabilityFlags
		{
			// Token: 0x04003C02 RID: 15362
			Integrity = 1,
			// Token: 0x04003C03 RID: 15363
			Privacy = 2,
			// Token: 0x04003C04 RID: 15364
			TokenOnly = 4,
			// Token: 0x04003C05 RID: 15365
			Datagram = 8,
			// Token: 0x04003C06 RID: 15366
			Connection = 16,
			// Token: 0x04003C07 RID: 15367
			MultiLegRequired = 32,
			// Token: 0x04003C08 RID: 15368
			ClientOnly = 64,
			// Token: 0x04003C09 RID: 15369
			ExtendedError = 128,
			// Token: 0x04003C0A RID: 15370
			Impersonation = 256,
			// Token: 0x04003C0B RID: 15371
			AcceptsWin32Names = 512,
			// Token: 0x04003C0C RID: 15372
			Stream = 1024,
			// Token: 0x04003C0D RID: 15373
			Negotiable = 2048,
			// Token: 0x04003C0E RID: 15374
			GssCompatible = 4096,
			// Token: 0x04003C0F RID: 15375
			Logon = 8192,
			// Token: 0x04003C10 RID: 15376
			AsciiBuffers = 16384,
			// Token: 0x04003C11 RID: 15377
			Fragment = 32768,
			// Token: 0x04003C12 RID: 15378
			MutualAuth = 65536,
			// Token: 0x04003C13 RID: 15379
			Delegation = 131072,
			// Token: 0x04003C14 RID: 15380
			ReadonlyWithChecksum = 262144
		}
	}
}
