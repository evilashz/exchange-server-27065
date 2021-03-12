using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000679 RID: 1657
	internal struct StoreOperationUninstallDeployment
	{
		// Token: 0x06004EB4 RID: 20148 RVA: 0x00117F73 File Offset: 0x00116173
		[SecuritySafeCritical]
		public StoreOperationUninstallDeployment(IDefinitionAppId appid, StoreApplicationReference AppRef)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationUninstallDeployment));
			this.Flags = StoreOperationUninstallDeployment.OpFlags.Nothing;
			this.Application = appid;
			this.Reference = AppRef.ToIntPtr();
		}

		// Token: 0x06004EB5 RID: 20149 RVA: 0x00117FA5 File Offset: 0x001161A5
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x04002185 RID: 8581
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002186 RID: 8582
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationUninstallDeployment.OpFlags Flags;

		// Token: 0x04002187 RID: 8583
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x04002188 RID: 8584
		public IntPtr Reference;

		// Token: 0x02000C1A RID: 3098
		[Flags]
		public enum OpFlags
		{
			// Token: 0x0400369B RID: 13979
			Nothing = 0
		}

		// Token: 0x02000C1B RID: 3099
		public enum Disposition
		{
			// Token: 0x0400369D RID: 13981
			Failed,
			// Token: 0x0400369E RID: 13982
			DidNotExist,
			// Token: 0x0400369F RID: 13983
			Uninstalled
		}
	}
}
