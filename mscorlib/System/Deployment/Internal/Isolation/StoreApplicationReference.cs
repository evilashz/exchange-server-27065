using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000675 RID: 1653
	internal struct StoreApplicationReference
	{
		// Token: 0x06004EA9 RID: 20137 RVA: 0x00117DE9 File Offset: 0x00115FE9
		public StoreApplicationReference(Guid RefScheme, string Id, string NcData)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreApplicationReference));
			this.Flags = StoreApplicationReference.RefFlags.Nothing;
			this.GuidScheme = RefScheme;
			this.Identifier = Id;
			this.NonCanonicalData = NcData;
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x00117E1C File Offset: 0x0011601C
		[SecurityCritical]
		public IntPtr ToIntPtr()
		{
			IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf<StoreApplicationReference>(this));
			Marshal.StructureToPtr<StoreApplicationReference>(this, intPtr, false);
			return intPtr;
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x00117E48 File Offset: 0x00116048
		[SecurityCritical]
		public static void Destroy(IntPtr ip)
		{
			if (ip != IntPtr.Zero)
			{
				Marshal.DestroyStructure(ip, typeof(StoreApplicationReference));
				Marshal.FreeCoTaskMem(ip);
			}
		}

		// Token: 0x04002173 RID: 8563
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002174 RID: 8564
		[MarshalAs(UnmanagedType.U4)]
		public StoreApplicationReference.RefFlags Flags;

		// Token: 0x04002175 RID: 8565
		public Guid GuidScheme;

		// Token: 0x04002176 RID: 8566
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Identifier;

		// Token: 0x04002177 RID: 8567
		[MarshalAs(UnmanagedType.LPWStr)]
		public string NonCanonicalData;

		// Token: 0x02000C13 RID: 3091
		[Flags]
		public enum RefFlags
		{
			// Token: 0x04003687 RID: 13959
			Nothing = 0
		}
	}
}
