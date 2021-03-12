﻿using System;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000020 RID: 32
	[SecurityCritical]
	internal sealed class SafeViewOfFileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600016B RID: 363 RVA: 0x000047ED File Offset: 0x000029ED
		[SecurityCritical]
		internal SafeViewOfFileHandle() : base(true)
		{
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000047F6 File Offset: 0x000029F6
		[SecurityCritical]
		internal SafeViewOfFileHandle(IntPtr handle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004806 File Offset: 0x00002A06
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			if (Win32Native.UnmapViewOfFile(this.handle))
			{
				this.handle = IntPtr.Zero;
				return true;
			}
			return false;
		}
	}
}
