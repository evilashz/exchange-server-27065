using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200068B RID: 1675
	[Guid("07662534-750b-4ed5-9cfb-1c5bc5acfd07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IStateManager
	{
		// Token: 0x06004F1C RID: 20252
		[SecurityCritical]
		void PrepareApplicationState([In] UIntPtr Inputs, ref UIntPtr Outputs);

		// Token: 0x06004F1D RID: 20253
		[SecurityCritical]
		void SetApplicationRunningState([In] uint Flags, [In] IActContext Context, [In] uint RunningState, out uint Disposition);

		// Token: 0x06004F1E RID: 20254
		[SecurityCritical]
		void GetApplicationStateFilesystemLocation([In] uint Flags, [In] IDefinitionAppId Appidentity, [In] IDefinitionIdentity ComponentIdentity, [In] UIntPtr Coordinates, [MarshalAs(UnmanagedType.LPWStr)] out string Path);

		// Token: 0x06004F1F RID: 20255
		[SecurityCritical]
		void Scavenge([In] uint Flags, out uint Disposition);
	}
}
