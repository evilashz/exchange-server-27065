using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000678 RID: 1656
	internal struct StoreOperationInstallDeployment
	{
		// Token: 0x06004EB1 RID: 20145 RVA: 0x00117F0B File Offset: 0x0011610B
		public StoreOperationInstallDeployment(IDefinitionAppId App, StoreApplicationReference reference)
		{
			this = new StoreOperationInstallDeployment(App, true, reference);
		}

		// Token: 0x06004EB2 RID: 20146 RVA: 0x00117F18 File Offset: 0x00116118
		[SecuritySafeCritical]
		public StoreOperationInstallDeployment(IDefinitionAppId App, bool UninstallOthers, StoreApplicationReference reference)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationInstallDeployment));
			this.Flags = StoreOperationInstallDeployment.OpFlags.Nothing;
			this.Application = App;
			if (UninstallOthers)
			{
				this.Flags |= StoreOperationInstallDeployment.OpFlags.UninstallOthers;
			}
			this.Reference = reference.ToIntPtr();
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x00117F66 File Offset: 0x00116166
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x04002181 RID: 8577
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002182 RID: 8578
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationInstallDeployment.OpFlags Flags;

		// Token: 0x04002183 RID: 8579
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x04002184 RID: 8580
		public IntPtr Reference;

		// Token: 0x02000C18 RID: 3096
		[Flags]
		public enum OpFlags
		{
			// Token: 0x04003694 RID: 13972
			Nothing = 0,
			// Token: 0x04003695 RID: 13973
			UninstallOthers = 1
		}

		// Token: 0x02000C19 RID: 3097
		public enum Disposition
		{
			// Token: 0x04003697 RID: 13975
			Failed,
			// Token: 0x04003698 RID: 13976
			AlreadyInstalled,
			// Token: 0x04003699 RID: 13977
			Installed
		}
	}
}
