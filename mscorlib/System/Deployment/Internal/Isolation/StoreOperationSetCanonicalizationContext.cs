using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200067C RID: 1660
	internal struct StoreOperationSetCanonicalizationContext
	{
		// Token: 0x06004EBD RID: 20157 RVA: 0x00118222 File Offset: 0x00116422
		[SecurityCritical]
		public StoreOperationSetCanonicalizationContext(string Bases, string Exports)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationSetCanonicalizationContext));
			this.Flags = StoreOperationSetCanonicalizationContext.OpFlags.Nothing;
			this.BaseAddressFilePath = Bases;
			this.ExportsFilePath = Exports;
		}

		// Token: 0x06004EBE RID: 20158 RVA: 0x0011824E File Offset: 0x0011644E
		public void Destroy()
		{
		}

		// Token: 0x04002195 RID: 8597
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002196 RID: 8598
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationSetCanonicalizationContext.OpFlags Flags;

		// Token: 0x04002197 RID: 8599
		[MarshalAs(UnmanagedType.LPWStr)]
		public string BaseAddressFilePath;

		// Token: 0x04002198 RID: 8600
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ExportsFilePath;

		// Token: 0x02000C1E RID: 3102
		[Flags]
		public enum OpFlags
		{
			// Token: 0x040036A6 RID: 13990
			Nothing = 0
		}
	}
}
