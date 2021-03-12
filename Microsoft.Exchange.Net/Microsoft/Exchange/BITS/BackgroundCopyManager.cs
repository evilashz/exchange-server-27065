using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Microsoft.Exchange.BITS
{
	// Token: 0x02000652 RID: 1618
	[Guid("4991D34B-80A1-4291-83B6-3328366B9097")]
	[ClassInterface(ClassInterfaceType.None)]
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	[ComImport]
	internal class BackgroundCopyManager
	{
		// Token: 0x06001D67 RID: 7527
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern BackgroundCopyManager();
	}
}
