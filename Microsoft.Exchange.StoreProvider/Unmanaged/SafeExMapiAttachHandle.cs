using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002C2 RID: 706
	[ComVisible(false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SafeExMapiAttachHandle : SafeExMapiPropHandle, IExMapiAttach, IExMapiProp, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000D65 RID: 3429 RVA: 0x0003528C File Offset: 0x0003348C
		protected SafeExMapiAttachHandle()
		{
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00035294 File Offset: 0x00033494
		internal SafeExMapiAttachHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0003529D File Offset: 0x0003349D
		internal SafeExMapiAttachHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x000352A6 File Offset: 0x000334A6
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExMapiAttachHandle>(this);
		}
	}
}
