using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000615 RID: 1557
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
	internal class G711WAVEFORMAT : WaveFormat
	{
		// Token: 0x06001C0D RID: 7181 RVA: 0x00032E7B File Offset: 0x0003107B
		internal G711WAVEFORMAT(G711Format format) : base(8000, 8, 1)
		{
			base.FormatTag = (short)format;
		}
	}
}
