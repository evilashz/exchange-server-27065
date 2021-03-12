using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000286 RID: 646
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct MAPIERROR
	{
		// Token: 0x06000BAD RID: 2989 RVA: 0x00033BC7 File Offset: 0x00031DC7
		public string ErrorText(bool unicodeEncoded)
		{
			if (this.lpszError == IntPtr.Zero)
			{
				return null;
			}
			if (!unicodeEncoded)
			{
				return Marshal.PtrToStringAnsi(this.lpszError);
			}
			return Marshal.PtrToStringUni(this.lpszError);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00033BF7 File Offset: 0x00031DF7
		public string Component(bool unicodeEncoded)
		{
			if (this.lpszComponent == IntPtr.Zero)
			{
				return null;
			}
			if (!unicodeEncoded)
			{
				return Marshal.PtrToStringAnsi(this.lpszComponent);
			}
			return Marshal.PtrToStringUni(this.lpszComponent);
		}

		// Token: 0x04001101 RID: 4353
		public static readonly int LowLevelErrorOffset = (int)Marshal.OffsetOf(typeof(MAPIERROR), "ulLowLevelError");

		// Token: 0x04001102 RID: 4354
		internal int ulVersion;

		// Token: 0x04001103 RID: 4355
		private IntPtr lpszError;

		// Token: 0x04001104 RID: 4356
		private IntPtr lpszComponent;

		// Token: 0x04001105 RID: 4357
		internal int ulLowLevelError;

		// Token: 0x04001106 RID: 4358
		internal int ulContext;
	}
}
