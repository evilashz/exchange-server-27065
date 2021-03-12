using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x0200024A RID: 586
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SBinary
	{
		// Token: 0x06000A6A RID: 2666 RVA: 0x00032C97 File Offset: 0x00030E97
		public SBinary(byte[] ba)
		{
			this.ba = ba;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00032CA8 File Offset: 0x00030EA8
		internal int GetBytesToMarshal()
		{
			int num = _SBinary.SizeOf + 7 & -8;
			return num + (this.ba.Length + 7 & -8);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00032CD4 File Offset: 0x00030ED4
		internal unsafe void MarshalToNative(_SBinary* psbin, ref byte* pbExtra)
		{
			psbin->cb = this.ba.Length;
			psbin->lpb = pbExtra;
			byte* ptr = pbExtra;
			pbExtra += (IntPtr)(this.ba.Length + 7 & -8);
			for (int i = 0; i < this.ba.Length; i++)
			{
				*ptr = this.ba[i];
				ptr++;
			}
		}

		// Token: 0x0400104C RID: 4172
		internal byte[] ba;
	}
}
