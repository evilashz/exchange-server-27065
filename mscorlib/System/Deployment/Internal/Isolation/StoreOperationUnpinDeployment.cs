using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000677 RID: 1655
	internal struct StoreOperationUnpinDeployment
	{
		// Token: 0x06004EAF RID: 20143 RVA: 0x00117ECC File Offset: 0x001160CC
		[SecuritySafeCritical]
		public StoreOperationUnpinDeployment(IDefinitionAppId app, StoreApplicationReference reference)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationUnpinDeployment));
			this.Flags = StoreOperationUnpinDeployment.OpFlags.Nothing;
			this.Application = app;
			this.Reference = reference.ToIntPtr();
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x00117EFE File Offset: 0x001160FE
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x0400217D RID: 8573
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400217E RID: 8574
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationUnpinDeployment.OpFlags Flags;

		// Token: 0x0400217F RID: 8575
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x04002180 RID: 8576
		public IntPtr Reference;

		// Token: 0x02000C16 RID: 3094
		[Flags]
		public enum OpFlags
		{
			// Token: 0x0400368F RID: 13967
			Nothing = 0
		}

		// Token: 0x02000C17 RID: 3095
		public enum Disposition
		{
			// Token: 0x04003691 RID: 13969
			Failed,
			// Token: 0x04003692 RID: 13970
			Unpinned
		}
	}
}
